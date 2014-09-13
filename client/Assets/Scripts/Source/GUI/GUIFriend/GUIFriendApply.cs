//  GUIFriendApply.cs
//  Author: Cheng Xia
//  2013-1-8

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;

/// <summary>
/// 好友申请界面GUI
/// </summary>
public class GUIFriendApply : GUIBase
{

    private enum ApplyState
    {
        NORMAL,
        PUPUP
    }

    private const string RES_MAIN = "GUI_FriendApply";                  //菜单资源地址
    private const string BTN_CANCEL = "Title_Cancel/Btn_Cancel";        //取消按钮地址
    private const string PAN_CANCEL = "Title_Cancel";                   //取消Pan地址
    private const string PAN_RIGHT = "PanInfo";                         //滑出Panel地址
    private const string RES_TABLE = "PanInfo/Panel/Table";             //Table地址
    private const string GUI_PANEL = "Panel";   //信息面板
    private const string GUI_BACK = "Panel/Black";  //背景碰撞
    private const string GUI_PANEL_PARENT = "PanInfo/Panel"; //拖拽信息父节点

    private UnityEngine.Object m_cResItem;  //单元资源

    private UIPanel m_cPanelParent; //拖拽面板父节点
    private GameObject m_cGUIBack;  //背景碰撞
    private GameObject m_cPanSlide;       //panel滑动
    private GameObject m_cPanel;    //隐藏面板//
    
    private GameObject m_cBtnCancel;      //取消按钮Panel
    private UITable m_cTable;             //table

    private List<FriendApplyItem> m_clstFriends = new List<FriendApplyItem>();

    private FriendPanel m_cFriendPanel;

    public Friend m_cFirend;

    private bool m_bIsShowed;//判断是否要显示

    public GUIFriendApply(GUIManager mgr)
        : base(mgr, GUI_DEFINE.GUIID_FRIENDAPPLY, GUILAYER.GUI_PANEL)
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

        if (this.m_clstFriends != null)
        {
            this.m_clstFriends.Clear();
        }
        
        this.m_cResItem = null;

        this.m_cPanelParent = null;
        this.m_cGUIBack = null;
        this.m_cPanSlide = null;
        this.m_cPanel = null;

        this.m_cBtnCancel = null;
        this.m_cTable = null; 

        base.Destory();
    }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        this.m_eLoadingState = LOADING_STATE.START;
        GUI_FUNCTION.AYSNCLOADING_SHOW();
        ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, FriendApplyItem.RES_ITEM);
        if (this.m_cGUIObject == null)
        {
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_MAIN);
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, FriendPanel.RES_FRIENDPANEL);
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
            //隐藏面板//
            m_cPanel = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, GUI_PANEL);

            m_cFriendPanel = new FriendPanel();
            m_cFriendPanel.SetParent(m_cPanel.transform);

            this.m_cPanelParent = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(this.m_cGUIObject, GUI_PANEL_PARENT);

            //取消按钮
            GameObject cancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_CANCEL);
            GUIComponentEvent gui_event = cancel.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnCancel_OnEvent);
            this.m_cBtnCancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_CANCEL);

            m_cTable = GUI_FINDATION.GET_OBJ_COMPONENT<UITable>(this.m_cGUIObject, RES_TABLE);

            this.m_cGUIBack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_BACK);
            gui_event = this.m_cGUIBack.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(OnInfoBack);
        }

        CreateGUI();

        m_cPanel.SetActive(false);

        this.m_cGUIMgr.SetCurGUIID(this.ID);

        CTween.TweenPosition(this.m_cBtnCancel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-540, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cPanSlide, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);

        SetLocalPos(Vector3.zero);

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_ADD_FRIEND));
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //base.Hiden();

        ResourcesManager.GetInstance().UnloadUnusedResources();

        if (Role.role.GetFriendProperty().GetAllApply() != null)
        {
            Role.role.GetBaseProperty().m_iFriendApplyCount = Role.role.GetFriendProperty().GetAllApply().Count(q => { return q.m_iState != 0; }); 
        }

        GUI_FUNCTION.AYSNCLOADING_HIDEN();

        CTween.TweenPosition(this.m_cPanSlide, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0));
        CTween.TweenPosition(this.m_cBtnCancel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-540, 270, 0) , Destory);

        //SetLocalPos(Vector3.one * 0xFFFF);
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
    /// 创建GUI
    /// </summary>
    private void CreateGUI()
    {
        this.m_cPanelParent.transform.localPosition = new Vector3(0, -18, 0);
        this.m_cPanelParent.clipRange = new Vector4(0, -45, this.m_cPanelParent.clipRange.z, this.m_cPanelParent.clipRange.w);

        foreach (FriendApplyItem fli in m_clstFriends)
        {
            fli.DestoryObj();
        }

        m_clstFriends.Clear();

        List<Friend> frilst = Role.role.GetFriendProperty().GetAllApply();



        for (int i = 0; i < frilst.Count; i++)
        {
            if (frilst[i] == null)
            {
                continue;
            }

            FriendApplyItem tmp = new FriendApplyItem();
            tmp.m_cItem.transform.parent = m_cTable.gameObject.transform;
            tmp.m_cItem.transform.localScale = Vector3.one;

            GUIComponentEvent gui_event = tmp.m_cBtnInfo.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(ItemSelect_OnEvent, i);
            gui_event = tmp.m_cBtnApply.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(ItemApply_OnEvent, i);
            gui_event = tmp.m_cBtnDelete.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(ItemDelete_OnEvent, i);

            tmp.SetMonsterInfo(frilst[i]);

            m_clstFriends.Add(tmp);
        }

        m_cTable.repositionNow = true;
    }

    /// <summary>
    /// 信息返回
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnInfoBack(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.m_cPanel.SetActive(false);
        }
    }

    /// <summary>
    /// 取消点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void BtnCancel_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

            Hiden();

            GUIFriendMenu frimenu = (GUIFriendMenu)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FRIENDMENU);
            frimenu.Show();
        }
    }

    /// <summary>
    /// 好友申请
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void ItemApply_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            List<Friend> friends = Role.role.GetFriendProperty().GetAllApply();
            m_cFirend = friends[(int)args[0]];

            m_cPanel.SetActive(true);
            m_cFriendPanel.m_cFriendPanel.SetActive(false);
            if (Role.role.GetFriendProperty().GetAll().Count >= RoleExpTableManager.GetInstance().GetMaxFriend(Role.role.GetBaseProperty().m_iLevel))
            {
                m_cPanel.SetActive(false);
                GUI_FUNCTION.MESSAGEM(null, "当前好友数量已满！");
            }
            else {
                GUI_FUNCTION.MESSAGEM_(SureApplyFirend, "是否接受好友申请");
            }
        }
    }

    /// <summary>
    /// 取消申请
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void ItemDelete_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            List<Friend> friends = Role.role.GetFriendProperty().GetAllApply();
            m_cFirend = friends[(int)args[0]];

            m_cPanel.SetActive(true);
            m_cFriendPanel.m_cFriendPanel.SetActive(false);

            GUI_FUNCTION.MESSAGEM_(CancelApplyFirend, "是否删除好友申请");
        }
    }

    /// <summary>
    /// 好友查看
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>

    public void ItemSelect_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            List<Friend> friends = Role.role.GetFriendProperty().GetAllApply();
            Friend fri = friends[(int)args[0]];
            m_cFriendPanel.ReflashInfo(fri);

            m_cPanel.SetActive(true);
            //m_cPopup.SetActive(false);
            m_cFriendPanel.m_cFriendPanel.SetActive(true);
        }
    }

    //是否接受好友//
    private void SureApplyFirend(bool isApply)
    {
        if (isApply)
        {
            SendAgent.SendFriendAcceptApply(Role.role.GetBaseProperty().m_iPlayerId, m_cFirend.m_iID);
        }

        m_cPanel.SetActive(false);
    }

    //是否取消好友申请//
    private void CancelApplyFirend(bool isApply)
    {
        if (isApply)
        {
            BattleFriend tmp = Role.role.GetBattleFriendProperty().GetBattleFriend(m_cFirend.m_iID);
            if (tmp != null)
            {
                tmp.m_bIsFriend = false;
            }

            SendAgent.SendFriendCancelApply(Role.role.GetBaseProperty().m_iPlayerId, m_cFirend.m_iID);

        }


        m_cPanel.SetActive(false);
    }
}

/// <summary>
/// 好友显示列表项
/// </summary>
public class FriendApplyItem
{
    public const string RES_ITEM = "GUI_FriendApplyItem";   //Item主资源
    private const string LAB_APPLYTIME = "LabTime";      //申请时间
    private const string LAB_NAME = "LabName";                //名称
    private const string LAB_STATE = "LabState";              //状态
    private const string BTN_DELETE = "BtnDelete";          //删除好友
    private const string BTN_APPLY = "BtnAffirm";            //接受好友
    private const string BTN_INFO = "BtnInfo";
    private const string SP_BORDER = "GUI_MonsterItem/ItemBorder"; //边框//
    private const string SP_MONSTER = "GUI_MonsterItem/ItemMonster";   //怪物头像//
    private const string SP_FRAME = "GUI_MonsterItem/ItemFrame";   //背景//
    private const string LAB_BOTTOM = "GUI_MonsterItem/LabBottom";
    
    public GameObject m_cItem;  //好友申请单位
    public GameObject m_cBtnDelete; //删除按钮
    public GameObject m_cBtnApply;  //同意按钮
    public GameObject m_cBtnInfo;   //信息按钮
    private UILabel m_labTime;  //时间
    private UILabel m_labState; //状态
    private UILabel m_labName;  //名字
    private UISprite m_spBorder;    //边框
    private UISprite m_spMonster;   //头像
    private UISprite m_spFrame; //边框
    private UILabel m_labBottom;    //底框

    public FriendApplyItem()
    {
        m_cItem = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_ITEM)) as GameObject;
        m_cBtnApply = GUI_FINDATION.GET_GAME_OBJECT(m_cItem, BTN_APPLY);
        m_cBtnDelete = GUI_FINDATION.GET_GAME_OBJECT(m_cItem, BTN_DELETE);
        m_cBtnInfo = GUI_FINDATION.GET_GAME_OBJECT(m_cItem, BTN_INFO);
        m_labTime = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cItem, LAB_APPLYTIME);
        m_labState = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cItem, LAB_STATE);
        m_labName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cItem, LAB_NAME);
        m_spBorder = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, SP_BORDER);
        m_spFrame = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, SP_FRAME);
        m_spMonster = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, SP_MONSTER);
        m_labBottom = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cItem, LAB_BOTTOM);
    }

    //设置人物属性//
    public void SetMonsterInfo(Friend friend)
    {

        if (friend == null)
        {
            return;
        }
        
        m_labName.text = friend.m_strName;
        m_labTime.text = GAME_FUNCTION.UNIXTimeToCDateTime(friend.m_lApplyTime).ToString("yyyy-MM-dd hh:mm:ss");

        if (friend.m_iState == 0)
        {
            m_labState.text = "申请中";
            m_cBtnApply.SetActive(false);
        }
        else
        {
            m_labState.text = "[f4af21]等待确认";
            m_cBtnApply.SetActive(true);
        }

        Hero hero = friend.m_cHero;
        HeroTable table = HeroTableManager.GetInstance().GetHeroTable(hero.m_iTableID);
        m_labBottom.text = "Lv." + hero.m_iLevel;
        GUI_FUNCTION.SET_AVATORS(m_spMonster, table.AvatorMRes);
        GUI_FUNCTION.SET_HeroBorderAndBack(m_spBorder, m_spFrame, (Nature)table.Property);

    }

    //销毁//
    public void DestoryObj()
    {
        GameObject.Destroy(m_cItem);

    }
}
