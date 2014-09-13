using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Resource;
using UnityEngine;

//设备屋
//Author sunyi
//2013.12.6
public class GUIEquipmentHouse : GUIBase
{
    /// <summary>
    /// 列表展示对象
    /// </summary>
    public class EquipHouseShowItem
    {
        private const string LB_Count = "Lab_Count";  //装备数量
        private const string LB_Desc = "Lab_Desc";  //描述
        private const string LB_Name = "Lab_Name";  //名称
        private const string SP_Equip = "Spr_Icon"; //图像spr
        private const string SP_BACK = "Spr_Back";  //遮罩
        private const string SP_NOITEM = "Spr_NoItem";  //素材不足
        private const string SP_NEW = "Spr_New"; //是否显示new

        public GameObject m_cItem;
        public UILabel m_cLbCount;
        public UILabel m_cLbDesc;
        public UILabel m_cLbName;
        public UISprite m_cEquipPath;
        public UISprite m_cSpBack;
        public UISprite m_cSpNoItem;
        public UISprite m_cSpNew;

        public int m_cEquipTableId;

        public EquipHouseShowItem(UnityEngine.Object parent)
        {
            m_cItem = GameObject.Instantiate(parent) as GameObject;
            m_cEquipPath = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, SP_Equip);
            m_cLbCount = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cItem, LB_Count);
            m_cLbDesc = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cItem, LB_Desc);
            m_cLbName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cItem, LB_Name);
            m_cSpBack=GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem,SP_BACK);
            m_cSpNoItem = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, SP_NOITEM);
            m_cSpNew = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, SP_NEW);

        }
    }

    private const string RES_MAIN = "GUI_EquiptHouse";//主资源地址
    private const string RES_LISTITEM = "GUI_EquiptHouseItemCell";//列表项资源地址

    private const string LISTVIEW = "ClipPanel/ListView";//宝石列表资源地址
    private const string MAINPANEL = "ClipPanel";//宝石列表资源地址
    private const string PANEL_TOPPANEL = "TopPanel";//导航栏地址
    private const string BUTTON_BACK = "TopPanel/Btn_Back";//返回按钮地址

    private GameObject m_cListView;//列表项
    private GameObject m_cTopPanel;//导航栏
    private GameObject m_cBtnBack; //返回按钮
    private GameObject m_cMainPanel;//主面板
    private UnityEngine.Object m_cItemSourse;

    private List<EquipHouseShowItem> m_lstEquipShows;  //显示列表
    private List<int> m_lstEquipTableIDs;           //可升级装备TableID

    private List<int> m_lstCombinedItemTableIDs;  //合成物品ID
    private List<int> m_lstEquipNums;   //合成物品数量

    private const float m_fY_Offest = 145f;  //单个合成列表向上偏移量
    public float m_fClipParentY = 4.000009F;   //剪裁父节点Y轴坐标
    public float m_fClipCenterY = -57.00001F;   //剪裁中间点Y轴坐标
    public float m_fClipSizeY = 530; //剪裁Y轴大小
    private Vector3 m_cOldVec3 = Vector3.zero;
    private Vector4 m_cOldVec4 = Vector4.zero;

    public GUIEquipmentHouse(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_EQUIPMENTHOUSE, GUILAYER.GUI_PANEL)
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
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_LISTITEM);
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

            this.m_cBtnBack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_BACK);
            GUIComponentEvent backEvent = this.m_cBtnBack.AddComponent<GUIComponentEvent>();
            backEvent.AddIntputDelegate(OnClickBackButton);

            this.m_cMainPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, MAINPANEL);
            this.m_cMainPanel.transform.localPosition = new Vector3(640, 0, 0);

            this.m_cListView = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LISTVIEW);

            this.m_cTopPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PANEL_TOPPANEL);
            this.m_cTopPanel.transform.localPosition = new Vector3(-420, 270, 0);

            this.m_cItemSourse = (UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_LISTITEM);

        }

        if (m_cOldVec3 == Vector3.zero)
        {
            //调整裁切
            UIPanel pan = this.m_cListView.GetComponent<UIPanel>();
            if (pan != null)
            {
                pan.transform.localPosition = new Vector3(pan.transform.localPosition.x, this.m_fClipParentY, pan.transform.localPosition.z);
                pan.clipRange = new Vector4(pan.clipRange.x, this.m_fClipCenterY, pan.clipRange.z, this.m_fClipSizeY);
            }
        }
        else
        {
            //调整裁切
            UIPanel pan = this.m_cListView.GetComponent<UIPanel>();
            if (pan != null)
            {
                pan.transform.localPosition = m_cOldVec3;
                pan.clipRange = m_cOldVec4;
            }
        }


        if (this.m_lstEquipShows != null)
        {
            this.m_lstEquipShows.ForEach((item) => { GameObject.DestroyImmediate(item.m_cItem); });
            m_lstEquipShows.Clear();
        }
        m_lstEquipShows = new List<EquipHouseShowItem>();

        //获得当前建筑等级可以合成的装备
        m_lstEquipTableIDs = BuildingTableManager.GetInstance().GetBuildingEquipItem(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.EQUIP).m_iLevel);

        for (int i = 0; i < m_lstEquipTableIDs.Count; i++)
        {
            EquipHouseShowItem tmp = new EquipHouseShowItem(m_cItemSourse);
            tmp.m_cItem.transform.parent = this.m_cListView.transform;
            tmp.m_cItem.transform.localScale = Vector3.one;
            tmp.m_cItem.transform.localPosition = new Vector3(0, m_fY_Offest - 130 * i, 0);
            tmp.m_cItem.AddComponent<GUIComponentEvent>().AddIntputDelegate(ItemSelect_OnEvent, i);
            m_lstEquipShows.Add(tmp);
        }

        //装备是否制作记录
        if (GAME_SETTING.EQUIPMENT_LIST.Count == 0)
        {
            foreach (int key in m_lstEquipTableIDs)
            {
                GAME_SETTING.EQUIPMENT_LIST.Add(key, 0);
            }
        }
        else
        {
            if (m_lstEquipTableIDs.Count > GAME_SETTING.EQUIPMENT_LIST.Count)
            {
                for (int i = 0; i < m_lstEquipTableIDs.Count; ++i)
                {
                    if (!GAME_SETTING.EQUIPMENT_LIST.ContainsKey(m_lstEquipTableIDs[i]))
                        GAME_SETTING.EQUIPMENT_LIST.Add(m_lstEquipTableIDs[i], 0);
                }
            }
        }

        UpdateShow();

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

        if (GAME_SETTING.s_iWarnHouseEquip == 1)
        {
            GAME_SETTING.s_iWarnHouseEquip = 0;
            GAME_SETTING.SaveWarnHouseEquip();
        }
        if (GAME_SETTING.s_bEquipLevelUp)
        {
            GAME_SETTING.s_bEquipLevelUp = false;
            GAME_SETTING.SaveEquipScane();
        }
        if (GAME_SETTING.s_iEquipLevelAdd != 0)
        {
            GAME_SETTING.s_iEquipLevelAdd = 0;
            GAME_SETTING.SaveEquipLevelAdd();
        }
        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_EQUIPMENTHOUSE));
        gui.TownWarn();
    }

    /// <summary>
    /// 更新数据显示
    /// </summary>
    private void UpdateShow()
    {
        for (int i = 0; i < m_lstEquipTableIDs.Count; i++)
        {
            int tableID = m_lstEquipTableIDs[i];
            m_lstEquipShows[i].m_cEquipTableId = tableID;
            m_lstEquipShows[i].m_cLbCount.text = (Role.role.GetItemProperty().GetItemCountByTableIdForDummy(tableID) - Role.role.GetItemProperty().GetEquipNumByTableId(tableID)).ToString();
            ItemTable table = ItemTableManager.GetInstance().GetItem(tableID);
            m_lstEquipShows[i].m_cLbDesc.text = table.Desc;
            m_lstEquipShows[i].m_cLbName.text = table.Name;

            GUI_FUNCTION.SET_ITEMM(m_lstEquipShows[i].m_cEquipPath, table.SpiritName);

            if (CanCombined(tableID))
            {
                m_lstEquipShows[i].m_cSpBack.enabled = false;
                m_lstEquipShows[i].m_cSpNoItem.enabled = false;
            }
            else
            {
                m_lstEquipShows[i].m_cSpBack.enabled = true;
                m_lstEquipShows[i].m_cSpNoItem.enabled = true;
            }

            //判断是否显示new
            if (GAME_SETTING.EQUIPMENT_LIST.ContainsKey(tableID))
            {
                if (GAME_SETTING.EQUIPMENT_LIST[tableID] == 1)
                {
                    m_lstEquipShows[i].m_cSpNew.enabled = false;
                }
                else
                {
                    m_lstEquipShows[i].m_cSpNew.enabled = true;
                    TweenAlpha.Begin(m_lstEquipShows[i].m_cSpNew.gameObject, 1, 0).style = UITweener.Style.PingPong;
                }
            }
            else
            {
                GAME_SETTING.EQUIPMENT_LIST.Add(tableID, 0);
                m_lstEquipShows[i].m_cSpNew.enabled = true;
                TweenAlpha.Begin(m_lstEquipShows[i].m_cSpNew.gameObject, 1, 0).style = UITweener.Style.PingPong;
            }
        }

        GAME_SETTING.SaveEquipment();
        //todo

        //UIDraggablePanel dpan = this.m_cListView.GetComponent<UIDraggablePanel>();
        //if (dpan != null)
        //{
        //    dpan.repositionClipping = true;
        //}
    }

    /// <summary>
    /// 素材和农场点是否足够合成所需
    /// </summary>
    /// <param name="tableID"></param>
    /// <returns></returns>
    private bool CanCombined(int tableID)
    {
        bool m_bCanCombine = true;
        //物品合成表
        ItemCompositeTable m_clstTable = ItemCompositeTableManager.GetInstance().GetItemCompositeTable(tableID);
        for (int i = 0; i < m_clstTable.LstNeedID.Count; i++)
        {
            ItemTable ned = ItemTableManager.GetInstance().GetItem(m_clstTable.LstNeedID[i]);
            int nedcount = m_clstTable.LstNeedNum[i];

            int nowCount = 0;
            nowCount = Role.role.GetItemProperty().GetItemCountByTableIdForDummy(ned.ID) -  Role.role.GetItemProperty().GetEquipNumByTableId(ned.ID);  //现在拥有的数量

            if (nedcount > nowCount)
            {
                m_bCanCombine = false;
            }
        }

        //装备需要消耗农场点，消耗品不需要消耗农场点，隐藏提示
        if (ItemTableManager.GetInstance().GetItem(tableID).Type == (int)ITEM_TYPE.EQUIP)
        {
            if (Role.role.GetBaseProperty().m_iFarmPoint < m_clstTable.NeedFarmPoint)
            {
                m_bCanCombine = false;
            }
        }

        return m_bCanCombine;
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        if (m_lstCombinedItemTableIDs != null && m_lstEquipNums.Count != 0)
        {
            SendAgent.SendItemCombinedReq(Role.role.GetBaseProperty().m_iPlayerId,
                m_lstCombinedItemTableIDs, m_lstEquipNums);
        }

        //base.Hiden();

        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 0, 0), new Vector3(640, 0, 0));
        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-420, 270, 0),Destory);

        ResourcesManager.GetInstance().UnloadUnusedResources();
    }

    private void HidenNotSend()
    {
        //base.Hiden();

        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 0, 0), new Vector3(640, 0, 0));
        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-420, 270, 0),Destory);
        ResourcesManager.GetInstance().UnloadUnusedResources();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        m_lstEquipShows = null;
        m_lstEquipTableIDs = null;
        m_lstCombinedItemTableIDs = null;
        m_lstEquipNums = null;

        m_cListView = null;
        m_cTopPanel = null;
        m_cBtnBack = null;
        m_cMainPanel = null;
        m_cItemSourse = null;

        base.Destory();
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
    /// 返回按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickBackButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {

            GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

            Hiden();

            if (SessionManager.GetInstance().Refresh())
            {
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_TOWN).Show);
            }
            else
            {
                //this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_TOWN).Show();
                //this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_TOWN).Show();
                GUIBackFrameTop backtop = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP) as GUIBackFrameTop;
                backtop.Hiden();

                //设置整体GUI点击GUIID
                GUITown town = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_TOWN) as GUITown;
                town.SetTownChildId(0);
                town.SetTownBlack(true);
            }

            //m_lstCombinedItemTableIDs = null;
            //m_lstEquipNums = null;

        }
    }

    /// <summary>
    /// 装备选中更新事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void ItemSelect_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {

            int selectIndex = (int)args[0];

            UIPanel pan = this.m_cListView.GetComponent<UIPanel>();
            m_cOldVec3 = pan.transform.localPosition;
            m_cOldVec4 = pan.clipRange;

            GUIReconceCombineDetail tmpshow = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_RECONCE_COMBINE) as GUIReconceCombineDetail;
            tmpshow.SetSelectCombineItem(m_lstEquipShows[selectIndex].m_cEquipTableId);
            tmpshow.SetOldGuiID(this.ID);
            tmpshow.SetSendData(m_lstCombinedItemTableIDs, m_lstEquipNums);

            HidenNotSend();

            tmpshow.Show();

            if (GAME_SETTING.EQUIPMENT_LIST.ContainsKey(m_lstEquipShows[selectIndex].m_cEquipTableId))
            {
                if (GAME_SETTING.EQUIPMENT_LIST[m_lstEquipShows[selectIndex].m_cEquipTableId] == 0)
                {
                    GAME_SETTING.EQUIPMENT_LIST[m_lstEquipShows[selectIndex].m_cEquipTableId] = 1;
                }
            }
            else
            {
                GAME_SETTING.EQUIPMENT_LIST.Add(m_lstEquipShows[selectIndex].m_cEquipTableId, 1);
            }

            GAME_SETTING.SaveEquipment();
        }
    }

    /// <summary>
    /// 将合成的物品记录到列表里面，当返回时，一起上传
    /// </summary>
    /// <param name="itemTableID"></param>
    public void AddCombinedItems(int itemTableID)
    {
        if (m_lstCombinedItemTableIDs == null)
        {
            m_lstCombinedItemTableIDs = new List<int>();
            m_lstEquipNums = new List<int>();
        }

        m_lstCombinedItemTableIDs.Add(itemTableID);
        m_lstEquipNums.Add(1);

    }

    /// <summary>
    /// 设置发送数据
    /// </summary>
    /// <param name="list"></param>
    /// <param name="m_lstEquipNums"></param>
    public void SetSendData(List<int> tableID, List<int> nums)
    {
        if (tableID != null)
        {
            this.m_lstCombinedItemTableIDs = new List<int>(tableID);
        }
        if (nums != null)
        {
            this.m_lstEquipNums = new List<int>(nums);
        }
    }
}