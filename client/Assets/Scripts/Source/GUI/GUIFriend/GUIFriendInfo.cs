//  GUIFriendInfo.cs
//  Author: Cheng Xia
//  2013-1-7

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;

/// <summary>
/// 好友信息界面GUI
/// </summary>
public class GUIFriendInfo : GUIBase
{
    private const string RES_MAIN = "GUI_FriendInfo";                   //菜单资源地址
    private const string BTN_CANCEL = "Title_Cancel/Btn_Cancel";        //取消按钮地址
    private const string PAN_CANCEL = "Title_Cancel";                   //取消Pan地址
    private const string BTN_APPLY = "BtnApply";         //好友解除
    private const string BTN_NOTAPPLY = "BtnNotApply";       //好友喜欢  
    private const string PAN_RIGHT = "PanInfo";  //滑出Panel地址
    //private const string RES_POPUP = "Popup";   //提示//
    private const string BTN_SURE = "BtnSure";  //确定//
    
    private GameObject m_cPanSlide;   //panel滑动
    //private GameObject m_cPopup;    //提示框按钮//
    private GameObject m_cBtnCancel;  //取消按钮Panel
    private GameObject m_cPlayInfo; //玩家信息界面

    private Friend m_cFriend;   //搜索到的玩家

    private FriendPanel m_cFriendPanel; //玩家信息面板实例

    public GUIFriendInfo(GUIManager mgr)
        : base(mgr, GUI_DEFINE.GUIID_FRIENDINFO, GUILAYER.GUI_PANEL)
    {
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public override void Initialize()
    {
        base.Initialize();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        this.m_cPanSlide = null;
        this.m_cBtnCancel = null;
        this.m_cPlayInfo = null;

        if (m_cFriendPanel!=null)
        {
            m_cFriendPanel.Destory();
            m_cFriendPanel = null;
        }

        base.Destory();
    }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        this.m_eLoadingState = LOADING_STATE.NONE;
        if (this.m_cGUIObject == null)
        {
            this.m_eLoadingState = LOADING_STATE.START;
            GUI_FUNCTION.AYSNCLOADING_SHOW();
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_MAIN);
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, FriendPanel.RES_FRIENDPANEL);
        }
        else
        {
            InitGUI();
        }
    }

    /// <summary>
    /// 初始化GUI
    /// </summary>
    protected override void InitGUI()
    {

        base.Show();
        GUI_FUNCTION.AYSNCLOADING_HIDEN();
        if (this.m_cGUIObject == null)
        {
            //实例化GameObject
            //Main主资源
            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            //滑出动画panel
            this.m_cPanSlide = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_RIGHT);
            //m_cPopup = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, RES_POPUP);

            //取消按钮//
            GameObject cancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_CANCEL);
            GUIComponentEvent gui_event = cancel.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnCancel_OnEvent);
            this.m_cBtnCancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_CANCEL);
            //好友申请//
            GameObject friendApply = GUI_FINDATION.GET_GAME_OBJECT(m_cPanSlide, BTN_APPLY);
            gui_event = friendApply.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnFriendApply_OnEvent);

            //好友取消申请//
            GameObject friendNotApply = GUI_FINDATION.GET_GAME_OBJECT(m_cPanSlide, BTN_NOTAPPLY);
            gui_event = friendNotApply.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnFriendNotApply_OnEvent);

            m_cFriendPanel = new FriendPanel();
            m_cFriendPanel.SetParent(m_cPanSlide.transform);

            //确定按钮//
            //GameObject btnSure = GUI_FINDATION.GET_GAME_OBJECT(m_cPopup, BTN_SURE);
            //gui_event = btnSure.AddComponent<GUIComponentEvent>();
            //gui_event.AddIntputDelegate(BtnCancel_OnEvent);

        }

        m_cPanSlide.SetActive(true);

        m_cFriendPanel.ReflashInfo(m_cFriend);

        this.m_cGUIMgr.SetCurGUIID(this.ID);


        CTween.TweenPosition(this.m_cPanSlide, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);
        CTween.TweenPosition(this.m_cBtnCancel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-540, 270, 0), new Vector3(0, 270, 0));


        SetLocalPos(Vector3.zero);
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //base.Hiden();

        ResourcesManager.GetInstance().UnloadUnusedResources();

        GUI_FUNCTION.AYSNCLOADING_HIDEN();

        CTween.TweenPosition(this.m_cPanSlide, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0));
        CTween.TweenPosition(this.m_cBtnCancel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-540, 270, 0),Destory);

    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    /// <returns></returns>
    public override bool Update()
    {
        //资源加载等待
        switch (this.m_eLoadingState)
        {
            case LOADING_STATE.START:
                this.m_eLoadingState++;
                return false;
            case LOADING_STATE.LOADING:
                if (ResourcesManager.GetInstance().GetProgress() >= 1f && ResourcesManager.GetInstance().IsComplete())
                {
                    this.m_eLoadingState++;
                }
                return false;
            case LOADING_STATE.END:
                InitGUI();
                this.m_eLoadingState++;
                break;
        }

        return base.Update();
    }

    /// <summary>
    /// 取消点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnCancel_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            GUIFriendSearch frisearch = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FRIENDSEARCH) as GUIFriendSearch;
            frisearch.Show();
        }
    }

    /// <summary>
    /// 好友申请点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnFriendApply_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (Role.role.GetFriendProperty().GetAll().Count >= RoleExpTableManager.GetInstance().GetMaxFriend(Role.role.GetBaseProperty().m_iLevel))
            {
                GUI_FUNCTION.MESSAGEM(null, "当前好友数量已满！");
            }
            else
            {
                FriendApplyHandle.CallBack = FriendApplyResutl;
                SendAgent.SendFriendApply(Role.role.GetBaseProperty().m_iPlayerId, m_cFriend.m_iID);
            }

        }
    }

    /// <summary>
    ///  好友申请结果回调
    /// </summary>
    /// <param name="ok"></param>
    private void FriendApplyResutl(bool ok)
    {
        GUI_FUNCTION.LOADING_HIDEN();
        ApplyBack(ok);
    }

    /// <summary>
    /// 好友不申请点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnFriendNotApply_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            GUIFriendSearch frisearch = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FRIENDSEARCH) as GUIFriendSearch;
            frisearch.Show();
        }
    }

    /// <summary>
    /// 设置要显示的好友信息
    /// </summary>
    /// <param name="friend"></param>
    public void SetFrindInfo(Friend friend)
    {
        m_cFriend = friend;
    }

    //初始化UI//
    private void UIInit()
    {
        
         
    }

    //提示确定//
    private void OnSure()
    {
        Hiden();

        GUIFriendSearch frisearch = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FRIENDSEARCH) as GUIFriendSearch;
        frisearch.Show();
    }

    //弹出框//
    public void ApplyBack(bool isAdd)
    {
        m_cPanSlide.SetActive(false);
        if (isAdd)
        {
            Role.role.GetFriendProperty().AddFriendApply(m_cFriend);
            GUI_FUNCTION.MESSAGEM(OnSure, "申请成功！");
        }
        else
        {
            GUI_FUNCTION.MESSAGEM(OnSure, "已经申请，等待对方确认等信息！");
        }
    }

}

/// <summary>
/// 好友面板类
/// </summary>

public class FriendPanel
{
    public const string RES_FRIENDPANEL = "GUI_FriendPanel";    //玩家信息//

    private const string INFO_MONSTER_ITEMBORDER = "GUI_MonsterItem/ItemBorder";
    private const string INFO_MONSTER_ITEMFRAME = "GUI_MonsterItem/ItemFrame";
    private const string INFO_MONSTER_ITEMMONSTER = "GUI_MonsterItem/ItemMonster";
    private const string INFO_LAB_SIGN = "LabSign";
    private const string INFO_LAB_ATTACK = "LabAttack";
    private const string INFO_LAB_DEFENSE = "LabDefense";
    private const string INFO_LAB_HP = "LabHP";
    private const string INFO_LAB_INFO = "LabInfo";
    private const string INFO_LAB_LV = "LabLV";
    private const string INFO_LAB_NAME = "LabName";
    private const string INFO_LAB_RECOVERY = "LabRecovery";
    private const string INFO_LAB_TITLEID = "LabTitleID";
    private const string INFO_LAB_TITLELV = "LabTitleLV";
    private const string INFO_LAB_TITLENAME = "LabTitleName";
    private const string INFO_PROPERTY = "Property";

    public GameObject m_cFriendPanel;

    private UISprite m_spItemBorder;
    private UISprite m_spItemFrame;
    private UISprite m_spItemMonster;
    private UILabel m_labSign;
    private UILabel m_labAttack;
    private UILabel m_labDefense;
    private UILabel m_labHp;
    private UILabel m_labInfo;
    private UILabel m_labLv;
    private UILabel m_labName;
    private UILabel m_labRecovery;
    private UILabel m_labTitleID;
    private UILabel m_labTitleLV;
    private UILabel m_labTitleName;
    private UISprite m_spProperty;

    //构造//
    public FriendPanel()
    {
        m_cFriendPanel = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_FRIENDPANEL)) as GameObject;

        m_spItemBorder = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cFriendPanel, INFO_MONSTER_ITEMBORDER);
        m_spItemFrame = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cFriendPanel, INFO_MONSTER_ITEMFRAME);
        m_spItemMonster = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cFriendPanel, INFO_MONSTER_ITEMMONSTER);
        m_labSign = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cFriendPanel, INFO_LAB_SIGN);
        m_labAttack = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cFriendPanel, INFO_LAB_ATTACK);
        m_labDefense = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cFriendPanel, INFO_LAB_DEFENSE);
        m_labHp = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cFriendPanel, INFO_LAB_HP);
        m_labInfo = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cFriendPanel, INFO_LAB_INFO);
        m_labLv = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cFriendPanel, INFO_LAB_LV);
        m_labName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cFriendPanel, INFO_LAB_NAME);
        m_labRecovery = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cFriendPanel, INFO_LAB_RECOVERY);
        m_labTitleID = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cFriendPanel, INFO_LAB_TITLEID);
        m_labTitleLV = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cFriendPanel, INFO_LAB_TITLELV);
        m_labTitleName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cFriendPanel, INFO_LAB_TITLENAME);
        m_spProperty = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cFriendPanel, INFO_PROPERTY);
    }

    //设置panel位置//
    public void SetParent(Transform parent)
    {
        m_cFriendPanel.transform.parent = parent;
        m_cFriendPanel.transform.localPosition = Vector3.zero;
        m_cFriendPanel.transform.localScale = Vector3.one;
    }


    public void ReflashInfo(Friend friend)
    {
        //好友信息//
        m_labTitleLV.text = friend.m_iLevel.ToString();
        m_labTitleName.text = friend.m_strName;
        m_labTitleID.text = "[F8C000]ID " + friend.m_iID;
        m_labInfo.text = AthleticsExpTableManager.GetInstance().GetAthleticsNameByPoint(friend.m_iAthleticPoint);
        m_labSign.text = friend.m_strSignature;

        //好友单位信息//
        Hero hero = friend.m_cHero;
        HeroTable table = HeroTableManager.GetInstance().GetHeroTable(hero.m_iTableID);

        GUI_FUNCTION.SET_AVATORS(m_spItemMonster, table.AvatorMRes);
        GUI_FUNCTION.SET_HeroBorderAndBack(m_spItemBorder, m_spItemFrame, (Nature)table.Property);
        GUI_FUNCTION.SET_NATURES(m_spProperty, hero.m_eNature);

        m_labName.text = hero.m_strName;
        m_labLv.text = hero.m_iLevel.ToString();
        m_labHp.text = hero.m_iHp.ToString();
        m_labDefense.text = hero.m_iDefence.ToString();
        m_labAttack.text = hero.m_iAttack.ToString();
        m_labRecovery.text = hero.m_iRevert.ToString();
    }

    public void Destory()
    {
        m_cFriendPanel = null;

        m_spItemBorder = null;
        m_spItemFrame = null;
        m_spItemMonster = null;
        m_labSign = null;
        m_labAttack = null;
        m_labDefense = null;
        m_labHp = null;
        m_labInfo = null;
        m_labLv = null;
        m_labName = null;
        m_labRecovery = null;
        m_labTitleID = null;
        m_labTitleLV = null;
        m_labTitleName = null;
        m_spProperty = null;
    }
}