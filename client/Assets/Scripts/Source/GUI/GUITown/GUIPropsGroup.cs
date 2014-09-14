using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using Game.Resource;
using Game.Base;

//道具编成
//Author:Sunyi
//2013-12-5

public class GUIPropsGroup : GUIBase
{
    public class PropsGroupItem
    {
        public GameObject m_cItem;
        //item中各个组件的地址
        private const string LABEL_COUNT = "Lab_Count";//X99标签地址
        private const string SPR_ICON = "Spr_Icon";//物品图标地址
        private const string SPR_BASE = "Spr_Base";//底座图标地址
        private const string LABEL_EQUIPT = "Lab_Equipt";//装备标签地址
        private const string LB_NAME = "Lab_Name";

        public UILabel m_cLbCount;
        public UILabel m_cLbNoItem;
        public UISprite m_cSpItem;
        public UISprite m_cSpBase;
        public UILabel m_cLbName;

        public PropsGroupItem(GameObject parent)
        {
            m_cItem = parent;
            m_cLbCount = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(parent, LABEL_COUNT);
            m_cLbNoItem = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(parent, LABEL_EQUIPT);
            m_cSpBase = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(parent, SPR_BASE);
            m_cSpItem = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(parent, SPR_ICON);
            m_cLbName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(parent, LB_NAME);
        
        }
    }

    private const string RES_MAIN = "GUI_PropsGroup";//主资源地址
    private const string RES_PROPSITEM0 = "GUI_PropsGroupItem0";//物品项地址
    private const string RES_PROPSITEM1 = "GUI_PropsGroupItem1";//物品项地址
    private const string RES_PROPSITEM2 = "GUI_PropsGroupItem2";//物品项地址
    private const string RES_PROPSITEM3 = "GUI_PropsGroupItem3";//物品项地址
    private const string RES_PROPSITEM4 = "GUI_PropsGroupItem4";//物品项地址

    private const string TOPPANEL = "TopPanel";//导航栏地址
    private const string MAINPANEL = "MainPanel";//主面板地址
    private const string BACK_BUTTON = "TopPanel/Button_Back";//房名返回按钮地址
    private const string FULL_BUTTON = "MainPanel/Btn_Full";//装满按钮地址
    private const string BTN_CLEAR = "MainPanel/Btn_Clear";  //清空按钮地址

    private GameObject m_cTopPanel;//导航栏
    private GameObject m_cMainPanel;//主面板地址
    private GameObject m_cBtnBack;//返回按钮
    private GameObject m_cBtnFull;//装满按钮
    private GameObject m_cBtnClear; //清空按钮

    private GameObject propsItem0;
    private GameObject propsItem1;
    private GameObject propsItem2;
    private GameObject propsItem3;
    private GameObject propsItem4;

    private int m_iSelectIndex = 0;//当前点击的itemid
    private int m_iOldGUIID; //上一次界面ID

    public bool m_bBackToFightReady = false;

    public GUIPropsGroup(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_PROPSGROUP, GUILAYER.GUI_PANEL)
    { }

    PropsGroupItem[] m_lstProps;  //道具编成列表


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
			this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            this.m_cMainPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, MAINPANEL);
            this.m_cMainPanel.transform.localPosition = new Vector3(0, 640, 0);

            this.m_cTopPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, TOPPANEL);
            this.m_cTopPanel.transform.localPosition = new Vector3(0, 270, 0);

            this.m_cBtnBack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BACK_BUTTON);
            GUIComponentEvent backEvent = this.m_cBtnBack.AddComponent<GUIComponentEvent>();
            backEvent.AddIntputDelegate(OnClickBackButton);

            this.m_cBtnFull = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, FULL_BUTTON);
            GUIComponentEvent fullEvent = this.m_cBtnFull.AddComponent<GUIComponentEvent>();
            fullEvent.AddIntputDelegate(OnClickFullButton);

            this.m_cBtnClear = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_CLEAR);
            GUIComponentEvent clearEvent = this.m_cBtnClear.AddComponent<GUIComponentEvent>();
            clearEvent.AddIntputDelegate(OnClickClearButton);

            propsItem0 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cMainPanel, RES_PROPSITEM0);
            propsItem1 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cMainPanel, RES_PROPSITEM1);
            propsItem2 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cMainPanel, RES_PROPSITEM2);
            propsItem3 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cMainPanel, RES_PROPSITEM3);
            propsItem4 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cMainPanel, RES_PROPSITEM4);

            propsItem0.AddComponent<GUIComponentEvent>().AddIntputDelegate(DidSelectItem, 0);
            propsItem1.AddComponent<GUIComponentEvent>().AddIntputDelegate(DidSelectItem, 1);
            propsItem2.AddComponent<GUIComponentEvent>().AddIntputDelegate(DidSelectItem, 2);
            propsItem3.AddComponent<GUIComponentEvent>().AddIntputDelegate(DidSelectItem, 3);
            propsItem4.AddComponent<GUIComponentEvent>().AddIntputDelegate(DidSelectItem, 4);
        }

        Item[] m_lstPropsItem = Role.role.GetItemProperty().GetAllBattleItem();

        if (m_lstProps == null)
        {
            m_lstProps = new PropsGroupItem[m_lstPropsItem.Length];
        }


        m_lstProps[0] = new PropsGroupItem(propsItem0);
        m_lstProps[1] = new PropsGroupItem(propsItem1);
        m_lstProps[2] = new PropsGroupItem(propsItem2);
        m_lstProps[3] = new PropsGroupItem(propsItem3);
        m_lstProps[4] = new PropsGroupItem(propsItem4);



        UpdateShowList();

        //设置整体GUI点击GUIID
        GUITown town = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_TOWN) as GUITown;
        town.SetTownChildId(this.ID);
        town.SetTownBlack(false);
        GUIBackFrameTop backtop = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP) as GUIBackFrameTop;
        backtop.Show();
        //this.m_cGUIMgr.SetCurGUIID(this.ID);

        SetLocalPos(Vector3.zero);

        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-420, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_EQUIP_ITEM));

        //新手引导
        GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_TOWN11);
        GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_TOWN13);
    }

    /// <summary>
    /// 更具列表刷新显示
    /// </summary>
    public void UpdateShowList()
    {
        Item[] m_lstPropsItem = Role.role.GetItemProperty().GetAllBattleItem();
        for (int i = 0; i < m_lstPropsItem.Length; i++)
        {
            if (null == m_lstPropsItem[i])
            {
                m_lstProps[i].m_cSpItem.enabled = false;
                m_lstProps[i].m_cLbCount.enabled = false;
                m_lstProps[i].m_cSpBase.enabled = false;
                m_lstProps[i].m_cLbName.enabled = false;
                m_lstProps[i].m_cLbNoItem.enabled = true;
            }
            else
            {
                m_lstProps[i].m_cSpItem.enabled = true;
                m_lstProps[i].m_cLbCount.enabled = true;
                m_lstProps[i].m_cSpBase.enabled = true;
                m_lstProps[i].m_cLbName.enabled = true;
                m_lstProps[i].m_cLbNoItem.enabled = false;

                m_lstProps[i].m_cLbName.text = m_lstPropsItem[i].m_strShortName;
                m_lstProps[i].m_cLbCount.text = "×" + m_lstPropsItem[i].m_iNum.ToString();
                GUI_FUNCTION.SET_ITEMM(m_lstProps[i].m_cSpItem, m_lstPropsItem[i].m_strSprName);
            }
        }
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        base.Hiden();

        Item[] bitem = Role.role.GetItemProperty().GetAllBattleItem();
        int[] tids = new int[5];
        int[] tnums = new int[5];
        for (int i = 0; i < 5; i++)
        {
            if (bitem[i] == null)
            {
                tids[i] = -1;
                tnums[i] = 0;
            }
            else
            {
                tids[i] = bitem[i].m_iTableID;
                tnums[i] = bitem[i].m_iNum;
            }
        }
        SendAgent.SendBattleItemEdit(Role.role.GetBaseProperty().m_iPlayerId, tids, tnums);

        //GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-420, 270, 0));
        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0),Destory);

        ResourceMgr.UnloadUnusedResources();
    }

    /// <summary>
    /// 隐藏不发送数据，点击进入选择的时候会隐藏该界面，但不需要发送数据
    /// </summary>
    public void HidenNotSend()
    {
        //base.Hiden();
        GUI_FUNCTION.AYSNCLOADING_HIDEN();

        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-420, 270, 0));
        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0),Destory);
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        m_cTopPanel = null;//导航栏
        m_cMainPanel = null;//主面板地址
        m_cBtnBack = null;//返回按钮
        m_cBtnFull = null;//装满按钮
        m_cBtnClear = null; //清空按钮

        propsItem0 = null;
        propsItem1 = null;
        propsItem2 = null;
        propsItem3 = null;
        propsItem4 = null;

        m_bBackToFightReady = false;

        base.Hiden();
        base.Destory();
    }

    /// <summary>
    /// 道具点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void DidSelectItem(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            HidenNotSend();

            m_iSelectIndex = (int)args[0];

            GUIBattleItemSelect detail = (GUIBattleItemSelect)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BATTLE_ITEM_SELECT);
            detail.SetBattleIndex(m_iSelectIndex);
            detail.m_bBackToFightReady = this.m_bBackToFightReady;
            detail.Show();
        }
    }

    /// <summary>
    /// 装满按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickFullButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Item[] m_lstPropsItem = Role.role.GetItemProperty().GetAllBattleItem();
            for (int i = 0; i < 5; i++)
            {
                if (m_lstPropsItem[i] != null)
                {
                    int tableId = m_lstPropsItem[i].m_iTableID;
                    //最大消耗品可装备数量  eg 10
                    int maxBat = ItemTableManager.GetInstance().GetBattleMaxNum(tableId);
                    //现在所有的数量  eg 2
                    int nowCount = Role.role.GetItemProperty().GetItemCountByTableId(tableId);
                    //最终可以最大化数量  eg 2
                    int maxNum = nowCount > maxBat ? maxBat : nowCount;

                    //Item it = Role.role.GetItemProperty().GetItemByTableID(m_lstPropsItem[i].m_iTableID);
                    //it.m_iNum = nowCount - maxNum;  //eg
                    //Role.role.GetItemProperty().UpdateItem(it);

                    Role.role.GetItemProperty().UpdateBattleItem(tableId, maxNum, i);

                }
            }

            m_lstPropsItem = Role.role.GetItemProperty().GetAllBattleItem();
            UpdateShowList();
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
    /// 清空按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickClearButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Item[] m_lstPropsItem = Role.role.GetItemProperty().GetAllBattleItem();
            for (int i = 0; i < 5; i++)
            {
                if (m_lstPropsItem[i] != null)
                {
                    Role.role.GetItemProperty().UpdateBattleItem(-1, 0, i);
                }
            }

            m_lstPropsItem = Role.role.GetItemProperty().GetAllBattleItem();
            UpdateShowList();
        }
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

            if (m_bBackToFightReady)
            {
                GUIBackFrameBottom tmpbottom = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
                tmpbottom.HiddenHalf();
                tmpbottom.OpenOrCloseDarkFrame(false);
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_TOWN).Hiden();

                if (false)
                {
                    SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FIGHTREADY).Show);
                }
                else
                {
                    this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FIGHTREADY).Show();
                }
            }
            else
            {
                if (false)
                {
                    SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(m_iOldGUIID).Show);
                }
                else
                {
                    this.m_cGUIMgr.GetGUI(m_iOldGUIID).Show();
                }
            }
        }
    }

    /// <summary>
    /// 设置上一界面  一个是道具编程界面，一个是战斗准备界面
    /// </summary>
    /// <param name="oldID"></param>
    public void SetOldID(int oldID)
    {
        m_iOldGUIID = oldID;
    }

}