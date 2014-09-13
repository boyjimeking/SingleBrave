using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;

//特典类
//Author: sunyi
//2013-11-27

public class GUIFesta : GUIBase
{
    private const string RES_MAIN = "GUI_Festa";//主资源地址
    private const string RES_ITEM = "GUI_GiftTableItem";   //item地址

    private const string TOPPANEL = "TopPanel";//导航栏地址
    private const string BACKBUTTON = "TopPanel/Btn_Back";//返回按钮地址
    private const string MAINPANEL = "MainPanel";//主面板地址
    private const string BTN_UseCode = "Btn_Use"; //使用邀请码按钮
    private const string BTN_CreateCode = "Panel/Content/AlertView/Btn_Send";
    private const string INPUT_CODE = "Panel/Content/AlertView/Input";
    private const string TABLE = "Panel/Content/GiftView/AlertView/PanView/Content"; //列表地址

    private const string PAN_MAIN = "Panel";  //主panel 裁切
    private const string PAN_GIFT = "Panel/Content/GiftView/AlertView/PanView";  //奖励表裁切

    private GameObject m_cTopPanel;//导航栏
    private GameObject m_cMainPanel;//主面板
    private GameObject m_cBtnBack;//返回按钮地址
    private GameObject m_cBtnUse;
    private GameObject m_cBtnCreate;
    private UIInput m_cInput;
    private GameObject m_cTable; //列表地址
    private UnityEngine.Object m_cItem;
    private UIPanel m_cPanMain;
    private UIPanel m_cPanGift;

    private List<FestaGiftItem> m_lstGiftItems = new List<FestaGiftItem>();  //显示奖励物品列表

    private const int m_iLstYoffest = -70;
    private const int m_iLstYTop = 0;

    /// <summary>
    /// 奖励物品Item
    /// </summary>
    public class FestaGiftItem
    {
        public GameObject m_cItem;  //item整个显示对象

        private const string SP_ITEM = "Item";
        private const string LB_NEEDNUM = "Lb_NeedNum";
        private const string LB_TITLE = "Lb_Title";

        public UISprite m_cSpItem;
        public UILabel m_cLbNeedNum;
        public UILabel m_cLbTitle;

        public FestaGiftItem(UnityEngine.Object obj)
        {
            m_cItem = GameObject.Instantiate(obj) as GameObject;
            m_cSpItem = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, SP_ITEM);
            m_cLbNeedNum = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cItem, LB_NEEDNUM);
            m_cLbTitle = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cItem, LB_TITLE);
        }

    }

    public GUIFesta(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_FESTA, GUILAYER.GUI_PANEL)
    { }


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
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_ITEM);
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
            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            this.m_cTopPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, TOPPANEL);
            this.m_cTopPanel.transform.localPosition = new Vector3(-420, 270, 0);

            this.m_cMainPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, MAINPANEL);
            this.m_cMainPanel.transform.localPosition = new Vector3(640, 0, 0);

            this.m_cBtnBack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BACKBUTTON);
            GUIComponentEvent backEvent = this.m_cBtnBack.AddComponent<GUIComponentEvent>();
            backEvent.AddIntputDelegate(OnClickBackButton);

            this.m_cInput = GUI_FINDATION.GET_OBJ_COMPONENT<UIInput>(this.m_cMainPanel, INPUT_CODE);

            this.m_cBtnCreate = GUI_FINDATION.GET_GAME_OBJECT(this.m_cMainPanel, BTN_CreateCode);
            this.m_cBtnCreate.AddComponent<GUIComponentEvent>().AddIntputDelegate(CreateCode_OnEvent);

            this.m_cBtnUse = GUI_FINDATION.GET_GAME_OBJECT(this.m_cMainPanel, BTN_UseCode);
            this.m_cBtnUse.AddComponent<GUIComponentEvent>().AddIntputDelegate(UseCode_OnEvent);

            this.m_cPanMain = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(m_cMainPanel, PAN_MAIN);
            this.m_cPanGift = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(m_cMainPanel, PAN_GIFT);
            this.m_cTable = GUI_FINDATION.GET_GAME_OBJECT(this.m_cMainPanel, TABLE);

            this.m_cItem = (UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_ITEM);
        }


        //读取邀请码奖励表，加入显示
        List<GuestsAwardTable> lst = GuestsAwardTableManager.GetInstance().GetAllGuestAward();
        foreach (FestaGiftItem tmpg in m_lstGiftItems)
        {
            GameObject.DestroyImmediate(tmpg.m_cItem);
        }
        for (int i = 0; i < lst.Count; i++)
        {
            GuestsAwardTable tmpt = lst[i];
            FestaGiftItem tmpg = new FestaGiftItem(this.m_cItem);
            tmpg.m_cItem.transform.parent = this.m_cTable.transform;
            tmpg.m_cItem.transform.localScale = Vector3.one;
            tmpg.m_cItem.transform.localPosition = new Vector3(0, m_iLstYTop + i * m_iLstYoffest, 0);
            tmpg.m_cLbNeedNum.text = tmpt.GuestNum.ToString();
            tmpg.m_cLbTitle.text = tmpt.Title;
            switch (tmpt.GiftType)
            {
                case GiftType.Item:
                    ItemTable table = ItemTableManager.GetInstance().GetItem(tmpt.GiftId);
                    GUI_FUNCTION.SET_ITEMS(tmpg.m_cSpItem, table.SpiritName);
                    break;
                case GiftType.FarmPoint:
                    tmpg.m_cSpItem.spriteName = "karma_thum";
                    tmpg.m_cSpItem.width = 40;
                    tmpg.m_cSpItem.height = 40;
                    break;
                case GiftType.Gold: tmpg.m_cSpItem.spriteName = "zell_thum";
                    tmpg.m_cSpItem.width = 40;
                    tmpg.m_cSpItem.height = 40;
                    break;
                case GiftType.Diamond: tmpg.m_cSpItem.spriteName = "gem";
                    tmpg.m_cSpItem.width = 40;
                    tmpg.m_cSpItem.height = 40;
                    break;
                case GiftType.FriendPoint: tmpg.m_cSpItem.spriteName = "friend_p_thum";
                    tmpg.m_cSpItem.width = 40;
                    tmpg.m_cSpItem.height = 40;
                    break;
                case GiftType.Hero:
                    HeroTable table2 = HeroTableManager.GetInstance().GetHeroTable(tmpt.GiftId);
                    GUI_FUNCTION.SET_AVATORS(tmpg.m_cSpItem, table2.AvatorMRes);
                    tmpg.m_cSpItem.width = 40;
                    tmpg.m_cSpItem.height = 40;
                    break;
                default:
                    break;
            }

            m_lstGiftItems.Add(tmpg);
        }

        this.m_cGUIMgr.SetCurGUIID(this.m_iID);
        SetLocalPos(Vector3.zero);

        this.m_cInput.enabled = false;

        if (GAME_SETTING.s_bCodeHasView)
        {
            this.m_cInput.value = Role.role.GetBaseProperty().m_strZhaoDaiId;
            this.m_cBtnCreate.GetComponent<UIImageButton>().isEnabled = false;
        }

        this.m_cPanMain.depth = 0;
        this.m_cPanGift.depth = 0;

        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-420, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //base.Hiden();
        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-420, 270, 0));
        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0) , Destory);

        ResourcesManager.GetInstance().UnloadUnusedResources();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        this.m_cTopPanel = null;
        this.m_cMainPanel = null;
        this.m_cBtnBack = null;
        this.m_cBtnUse = null;
        this.m_cBtnCreate = null;
        this.m_cInput = null;
        this.m_cTable = null;
        this.m_cItem = null;
        this.m_cPanMain = null;
        this.m_cPanGift = null;

        base.Destory();
    }

    /// <summary>
    /// 返回按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickBackButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            GUIMenu menu = (GUIMenu)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_MENU);
            menu.Show();
        }
    }

    /// <summary>
    /// 生成邀请码
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void CreateCode_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (!GAME_SETTING.s_bCodeHasView)
            {
                if (Role.role.GetBaseProperty().m_iLevel <10)
                {
                    GUI_FUNCTION.MESSAGEM(null, "等级到达10级后才能生成邀请码");
                }
                else
                {
                    GAME_SETTING.SaveCodeHasView(true);
                    this.m_cInput.value = Role.role.GetBaseProperty().m_strZhaoDaiId.ToString();
                    this.m_cBtnCreate.GetComponent<UIImageButton>().isEnabled = false;
                }
            }
        }
    }

    /// <summary>
    /// 使用邀请码切换
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void UseCode_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.Hiden();
            this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FESTAINVITE).Show();
        }
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
}

