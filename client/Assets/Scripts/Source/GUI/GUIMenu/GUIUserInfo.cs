//Micro.Sanvey
//2013.11.27
//sanvey.china@gmail.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;

/// <summary>
/// 角色信息GUI
/// </summary>
public class GUIUserInfo : GUIBase
{
    private const string RES_MAIN = "GUI_UserInfo";         //菜单资源地址
    private const string BTN_CANCEL = "Title_Cancel/Btn_Cancel";        //取消按钮地址
    private const string PAN_CANCEL = "Title_Cancel";        //取消Pan地址
    private const string PAN_RIGHT = "PanRight";  //滑出Panel地址
    private const string PLAYER_INFO = "PanRight/PlayerInfoParent"; //玩家情报显示 地址
    private const string BTN_EDIT = "PanRight/PlayerInfoParent/Btn_Edit";   //编辑按钮地址
    private const string SP_BACK = "Back";  //遮罩地址
    private const string Pan_Info = "PanRight/PlayerInfoParent/PlayInfo";  //显示角色信息
    private const string GUI_ACCOUNT = "PanRight/Btn_Acount";   //帐号按钮
    private const string GUI_TOP_LAB = "Title_Cancel/Lb_Info"; //顶部返回按钮旁文字
    private const string BTN_PP_CENTER = "Btn_PPUserCenter";  //PP用户中心按钮

    private GameObject m_cBtnAccount;   //帐号按钮
    private GameObject m_cPanSlide;   //panel滑动
    private GameObject m_cBtnCancel;  //取消按钮Panel
    private GameObject m_cPlayerInfo; //玩家情报
    private UISprite m_cSpBack;       //遮罩背景
    private UISprite m_cBtnAccountSprite; //账号按钮底图
    private UIImageButton m_cImageBtnAccount; //账号按钮ImageButton组件
    private BriefInfoShow m_cUserInfo; //用户信息显示块
    private UILabel m_cTopLabel;//顶部文字
    private GameObject m_cBtnPPCenter;  //PP用户中心按钮

    private bool m_bHasShow = false;  //加载过showobject
    private bool m_bPlayerInfo; //玩家情报是否正在显示

    public GUIUserInfo(GUIManager mgr)
        : base(mgr, GUI_DEFINE.GUIID_USERINFO, UILAYER.GUI_PANEL)
    {
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
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_MAIN);
        }
        else
        {
            InitGUI();
        }
        m_bPlayerInfo = true;

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
            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;
            //滑出动画panel
            this.m_cPanSlide = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_RIGHT);
            //取消按钮
            var cancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_CANCEL);
            GUIComponentEvent gui_event = cancel.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnCancel_OnEvent);
            this.m_cBtnCancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_CANCEL);
            //编辑按钮
            var edit = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_EDIT);
            gui_event = edit.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnEdit_OnEvent);
            //遮罩背景
            this.m_cSpBack = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SP_BACK);
            this.m_cSpBack.gameObject.SetActive(false);
            gui_event = m_cSpBack.gameObject.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnBackClick_OnEvent);

            //玩家情报
            this.m_cPlayerInfo = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PLAYER_INFO);

            //帐号按钮
            this.m_cBtnAccount = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_ACCOUNT);
            gui_event = this.m_cBtnAccount.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(OnAccount);
            this.m_cImageBtnAccount = this.m_cBtnAccount.GetComponent<UIImageButton>();
            this.m_cBtnAccountSprite = this.m_cBtnAccount.GetComponentInChildren<UISprite>();

            //顶部文字
            this.m_cTopLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, GUI_TOP_LAB);

            m_cUserInfo = new BriefInfoShow(this.m_cPlayerInfo);

            m_cUserInfo.m_cUiInput.label.text = "";

            this.m_cUserInfo.m_cSign.overflowMethod = UILabel.Overflow.ResizeHeight;
            this.m_cUserInfo.m_cSign.width = 480;

            //PP用户中心按钮
            this.m_cBtnPPCenter = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_PP_CENTER);

#if  IOSPP && !UNITY_EDITOR
            this.m_cBtnPPCenter.SetActive(true);
            this.m_cBtnPPCenter.AddComponent<GUIComponentEvent>().AddIntputDelegate(PP_Center_OnEvent);
#else
            this.m_cBtnPPCenter.SetActive(false);
#endif
        }
        if (this.m_bPlayerInfo)
        {
            this.m_cPlayerInfo.SetActive(true);
            this.m_cTopLabel.text = "玩家情报";
            this.m_cBtnAccountSprite.spriteName = "btn_accountmgr";
            this.m_cImageBtnAccount.normalSprite = "btn_accountmgr";
            this.m_cImageBtnAccount.hoverSprite = "btn_accountmgr1";
            this.m_cImageBtnAccount.pressedSprite = "btn_accountmgr1";
            this.m_cImageBtnAccount.disabledSprite = "btn_accountmgr1";
        }

        UpdateData();

        this.m_cGUIMgr.SetCurGUIID(this.ID);

        CTween.TweenPosition(this.m_cBtnCancel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-540, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cPanSlide, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);
        SetLocalPos(Vector3.zero);

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_PLAYER));
    }


    /// <summary>
    /// 更新
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
                if (ResourceMgr.GetProgress() >= 1f && ResourceMgr.IsComplete())
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
    /// 更新显示数据
    /// </summary>
    private void UpdateData()
    {
        var info = Role.role.GetBaseProperty();
        var leaderID = Role.role.GetTeamProperty().GetTeam(info.m_iCurrentTeam).m_iLeadID;
        var heroLeader = Role.role.GetHeroProperty().GetHero(leaderID);

        GUI_FUNCTION.SET_HeroBorderAndBack(m_cUserInfo.m_cSpBorder, m_cUserInfo.m_cSpFrame, heroLeader.m_eNature);
        GUI_FUNCTION.SET_AVATORS(m_cUserInfo.m_cSpHero, heroLeader.m_strAvatarM);
        GUI_FUNCTION.SET_NATURES(m_cUserInfo.m_cSpNature, heroLeader.m_eNature);

        m_cUserInfo.m_cUiInput.value = Role.role.GetBaseProperty().m_strSignature;
        m_cUserInfo.m_cUiInput.label.text = Role.role.GetBaseProperty().m_strSignature;

        m_cUserInfo.m_cLbHeroLv.text = "Lv." + heroLeader.m_iLevel.ToString();
        m_cUserInfo.m_cLbAttack.text = heroLeader.GetAttack().ToString();
        m_cUserInfo.m_cLbDefense.text = heroLeader.GetDefence().ToString();
        m_cUserInfo.m_cLbHP.text = heroLeader.GetMaxHP().ToString();
        m_cUserInfo.m_cLbInfo.text = AthleticsExpTableManager.GetInstance().GetAthleticsNameByPoint(info.m_iPVPExp);
        m_cUserInfo.m_cLbLV.text = heroLeader.m_iLevel.ToString();
        m_cUserInfo.m_cLbName.text = heroLeader.m_strName;
        m_cUserInfo.m_cLbRecovery.text = heroLeader.GetRecover().ToString();
        m_cUserInfo.m_cLbTitleId.text = info.m_iPlayerId.ToString();
        m_cUserInfo.m_cLbTitleLV.text = info.m_iLevel.ToString();
        m_cUserInfo.m_cLbTitleName.text = info.m_strUserName;
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //base.Hiden();

        if (!this.m_bPlayerInfo)
        {
            this.m_cPlayerInfo.SetActive(false);
            this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_ACCOUNT).Hiden();
        }
        this.m_cPlayerInfo.transform.localPosition = Vector3.zero;
        if (this.m_cSpBack != null)
        {
            this.m_cSpBack.gameObject.SetActive(false);
        }

        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        CTween.TweenPosition(this.m_cPanSlide, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0));
        CTween.TweenPosition(this.m_cBtnCancel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-540, 270, 0), Destory);

        ResourceMgr.UnloadUnusedResources();

    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        m_cBtnAccount = null;   //帐号按钮
        m_cPanSlide = null;   //panel滑动
        m_cBtnCancel = null;  //取消按钮Panel
        m_cPlayerInfo = null; //玩家情报
        m_cSpBack = null;       //遮罩背景
        m_cBtnAccountSprite = null; //账号按钮底图
        m_cImageBtnAccount = null; //账号按钮ImageButton组件
        m_cUserInfo = null; //用户信息显示块
        m_cTopLabel = null;//顶部文字
        m_cBtnPPCenter = null;  //PP用户中心按钮


        base.Hiden();
        base.Destory();
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

            GUIMenu menu = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_MENU) as GUIMenu;
            menu.Show();

        }
    }

    /// <summary>
    /// 帐号按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnAccount(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
#if  IOSPP && !UNITY_EDITOR
            PlatformPPIOS tmp = PlatformManager.GetInstance().Platform as PlatformPPIOS;
            tmp.ShowPlatformCenter();
            return;
#endif

            GUIAccount ga = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_ACCOUNT) as GUIAccount;
            if (this.m_bPlayerInfo)
            {
                ga.Show();
                ga.Box.enabled = false;
                //隐藏玩家情报 更换按钮图片
                CTween.TweenPosition(this.m_cPlayerInfo, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0));

                this.m_cTopLabel.text = "账号管理";
                this.m_cBtnAccountSprite.spriteName = "btn_playerIntelligence";
                this.m_cImageBtnAccount.normalSprite = "btn_playerIntelligence";
                this.m_cImageBtnAccount.hoverSprite = "btn_playerIntelligence1";
                this.m_cImageBtnAccount.pressedSprite = "btn_playerIntelligence1";
                this.m_cImageBtnAccount.disabledSprite = "btn_playerIntelligence1";
                this.m_bPlayerInfo = !this.m_bPlayerInfo;
            }
            else
            {
                this.m_bPlayerInfo = !this.m_bPlayerInfo;
                if (!this.m_cPlayerInfo.activeSelf)
                    this.m_cPlayerInfo.SetActive(true);
                ga.Hiden();
                //显示玩家情报 更换按钮图片
                CTween.TweenPosition(this.m_cPlayerInfo, GAME_DEFINE.FADEIN_GUI_TIME, GAME_DEFINE.FADEOUT_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);
                this.m_cUserInfo.m_cInput.GetComponentInChildren<UIInput>().enabled = false;

                this.m_cTopLabel.text = "玩家情报";
                this.m_cBtnAccountSprite.spriteName = "btn_accountmgr";
                this.m_cImageBtnAccount.normalSprite = "btn_accountmgr";
                this.m_cImageBtnAccount.hoverSprite = "btn_accountmgr1";
                this.m_cImageBtnAccount.pressedSprite = "btn_accountmgr1";
                this.m_cImageBtnAccount.disabledSprite = "btn_accountmgr1";


            }
        }
    }

    /// <summary>
    /// 编辑点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnEdit_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            CTween.TweenPosition(m_cUserInfo.m_cInput, 0f, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0F, 12.5F, 0F), new Vector3(0, 150, 0));
            this.m_cSpBack.gameObject.SetActive(true);

            this.m_cUserInfo.m_cInput.GetComponentInChildren<UIInput>().enabled = true;
            this.m_cUserInfo.m_cInput.GetComponentInChildren<UIInput>().isSelected = true;

            this.m_cUserInfo.m_cSign.overflowMethod = UILabel.Overflow.ResizeHeight;
            this.m_cUserInfo.m_cSign.width = 480;
        }
    }

    /// <summary>
    /// 遮罩背景点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnBackClick_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.m_cUserInfo.m_cInput.GetComponentInChildren<UIInput>().isSelected = false;
            this.m_cUserInfo.m_cInput.GetComponentInChildren<UIInput>().enabled = false;

            SendAgent.SendSignatureUpdateReq(
                Role.role.GetBaseProperty().m_iPlayerId
                , this.m_cUserInfo.m_cSign.text);


            CTween.TweenPosition(m_cUserInfo.m_cInput, 0f, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 150, 0), new Vector3(0F, 12.5F, 0F));
            this.m_cSpBack.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 显示玩家情报
    /// </summary>
    public void ShowPlayerInfo()
    {
        //显示玩家情报 更换按钮图片
        this.m_bPlayerInfo = !this.m_bPlayerInfo;
        if (!this.m_cPlayerInfo.activeSelf)
            this.m_cPlayerInfo.SetActive(true);
        CTween.TweenPosition(this.m_cPlayerInfo, GAME_DEFINE.FADEIN_GUI_TIME, GAME_DEFINE.FADEOUT_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);
        this.m_cUserInfo.m_cInput.GetComponentInChildren<UIInput>().enabled = false;

        this.m_cBtnAccountSprite.spriteName = "btn_accountmgr";
        this.m_cImageBtnAccount.normalSprite = "btn_accountmgr";
        this.m_cImageBtnAccount.hoverSprite = "btn_accountmgr1";
        this.m_cImageBtnAccount.pressedSprite = "btn_accountmgr1";
        this.m_cImageBtnAccount.disabledSprite = "btn_accountmgr1";
    }

    /// <summary>
    /// 账号注销弹出框 确定取消
    /// </summary>
    /// <param name="result"></param>
    public void Message_Result(bool result)
    {
        Debug.Log("logoff result: " + result);
        if (result)
        {
            PlatformManager.GetInstance().ShowPlatformLoginOut();
        }
    }

    /// <summary>
    /// PP中心点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void PP_Center_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
#if  IOSPP && !UNITY_EDITOR
            PlatformPPIOS tmp = PlatformManager.GetInstance().Platform as PlatformPPIOS;
            tmp.ShowPlatformCenter();
#endif
        }
    }

}

/// <summary>
/// 简要展示界面
/// Eg; 好友信息，用户信息
/// </summary>
public class BriefInfoShow
{
    private const string LB_ATTACK = "PlayInfo/Lb_Attack";
    private const string LB_DEFENCE = "PlayInfo/Lb_Defense";
    private const string LB_HP = "PlayInfo/Lb_HP";
    private const string LB_INFO = "PlayInfo/Lb_Info";
    private const string LB_LV = "PlayInfo/Lb_LV";
    private const string LB_NAME = "PlayInfo/Lb_Name";
    private const string LB_RECOVERY = "PlayInfo/Lb_Recovery";
    private const string LB_TITLEID = "PlayInfo/Lb_Title_ID";
    private const string LB_TITLELV = "PlayInfo/Lb_Title_LV";
    private const string INPUT = "PlayInfo/Input";  //输入框地址
    private const string LB_Sign = "Lb_Sign";
    private const string RES_INPUT = "Input";
    private const string SPR_BORDER = "PlayInfo/GUI_MonsterItem/ItemBorder";
    private const string SPR_FRAME = "PlayInfo/GUI_MonsterItem/ItemFrame";
    private const string SPR_HERO = "PlayInfo/GUI_MonsterItem/ItemMonster";
    private const string LB_HEROLV = "PlayInfo/GUI_MonsterItem/LabelBottom";
    private const string SP_NATURE = "PlayInfo/SP_Nature";
    private const string LB_TITLE_NAME = "PlayInfo/Lb_Title_Name";

    public UILabel m_cLbAttack;     //攻击
    public UILabel m_cLbDefense;
    public UILabel m_cLbHP;
    public UILabel m_cLbInfo;
    public UILabel m_cLbLV;
    public UILabel m_cLbName;
    public UILabel m_cLbRecovery;
    public UILabel m_cLbTitleId;
    public UILabel m_cLbTitleLV;
    public GameObject m_cInput;      //输入框
    public UISprite m_cSpBorder;
    public UISprite m_cSpFrame;
    public UISprite m_cSpHero;
    public UILabel m_cLbHeroLv;
    public UILabel m_cSign;
    public UIInput m_cUiInput;
    public UISprite m_cSpNature;
    public UILabel m_cLbTitleName;

    public BriefInfoShow(GameObject parentObj)
    {
        m_cLbAttack = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(parentObj, LB_ATTACK);
        m_cLbDefense = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(parentObj, LB_DEFENCE);
        m_cLbHP = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(parentObj, LB_HP);
        m_cLbInfo = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(parentObj, LB_INFO);
        m_cLbLV = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(parentObj, LB_LV);
        m_cLbName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(parentObj, LB_NAME);
        m_cLbRecovery = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(parentObj, LB_RECOVERY);
        m_cLbTitleId = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(parentObj, LB_TITLEID);
        m_cLbTitleLV = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(parentObj, LB_TITLELV);
        //输入框
        this.m_cInput = GUI_FINDATION.GET_GAME_OBJECT(parentObj, INPUT);
        this.m_cSign = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cInput, LB_Sign);
        this.m_cUiInput = GUI_FINDATION.GET_OBJ_COMPONENT<UIInput>(m_cInput, RES_INPUT);
        //英雄头像
        m_cSpBorder = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(parentObj, SPR_BORDER);
        m_cSpFrame = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(parentObj, SPR_FRAME);
        m_cSpHero = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(parentObj, SPR_HERO);
        m_cLbHeroLv = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(parentObj, LB_HEROLV);

        this.m_cSpNature = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(parentObj, SP_NATURE);
        this.m_cLbTitleName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(parentObj, LB_TITLE_NAME);
    }
}