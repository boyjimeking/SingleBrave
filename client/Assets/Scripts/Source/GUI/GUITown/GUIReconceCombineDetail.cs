using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using Game.Resource;
using Game.Base;

// GUI_ReconceCombineDetail
// sanvey
// 2013-12-10

/// <summary>
/// 消耗品合成详细界面
/// </summary>
public class GUIReconceCombineDetail : GUIBase
{
    /// <summary>
    /// 单个需要合成的物品显示
    /// </summary>
    public class ReconceDetailItem
    {
        public GameObject m_cItem;
        public UISprite m_cSpIcon;
        public UISprite m_cSpBorder;
        public UILabel m_cLbName;
        public UILabel m_cNeedNum;
        public UILabel m_cNowNum;

        private const string SP_ITEM = "Item/Icon";
        private const string SP_BORDER = "Item/Bg";
        private const string LB_NEED_COUNT = "Lab_Need";
        private const string LB_NOW_COUNT = "Lab_NowCount";
        private const string LB_NAME = "Label";

        public ReconceDetailItem(UnityEngine.Object sourse)
        {
            m_cItem = GameObject.Instantiate(sourse) as GameObject;

            m_cSpIcon = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, SP_ITEM);
            m_cSpBorder = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, SP_BORDER);
            m_cLbName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cItem, LB_NAME);
            m_cNeedNum = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cItem, LB_NEED_COUNT);
            m_cNowNum = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cItem, LB_NOW_COUNT);
        }
    }

    private const string RES_MAIN = "GUI_PropsReconcileCombine"; //主资源地址
    private const string RES_LISTITEM = "GUI_PropsCombineItem";  //列表项资源地址

    private const string MAINPANEL = "ClipPanel";
    private const string PANEL_TOPPANEL = "TopPanel";//导航栏地址
    private const string BUTTON_BACK = "TopPanel/Btn_Back";//返回按钮地址
    private const string BTN_BACK_LB = "TopPanel/Label";
    private const string LB_ITEM_DESC = "TopItem/Lab_Desc";
    private const string LB_ITEM_NAME = "TopItem/Lab_Name";
    //private const string LB_ITEM_NAME_S = "TopItem/Lab_NameItem";
    private const string LB_ITEM_COUNT = "TopItem/Lab_Num";
    private const string SP_ITEM = "TopItem/Spri_Icon";
    private const string SP_ITEM_BORDER = "TopItem/Spr_IconFrame";
    private const string BTN_COMBINE = "CenterItem/BtnCombine";
    private const string RES_LIST = "CenterItem/Panel/List";
    private const string RES_PANEL = "CenterItem/Panel";
    private const string LB_FARMPOINT = "CenterItem/Lb_FarmPoint";  //需要农场点
    private const string LB_FPOINT = "CenterItem/Text_FPoint";  //需要农场点字符
    private const string TEX_RE = "CenterItem/Tex_Frame";  //消耗品
    private const string TEX_EQ = "CenterItem/Tex_Frame2"; //装备

    private const string GUI_EFFECT = "GUI_EFFECT";//3d特效资源地址
    private const string EFFECT_CENTER_ANCHOR = "ANCHOR_CENTER";//3d特效父对象
    private const string EFFECT_COMBINE = "effect_GUI_itemhecheng";  //物品合成

    private const string BTN_RE = "btn_tiaohe";
    private const string BTN_EQ = "btn_shengcheng";

    private GameObject m_cGuiEffect;    //3d特效资源
    private GameObject m_cEffectParent; //3d特效父对象
    private UnityEngine.Object m_cEffectCombine; //合成特效
    private List<GameObject> m_lstEffects=new List<GameObject>();  //加入的特效集合，用于纪录销毁
    private float m_fDis;  //自动销毁特效
    private const float EFFECT_TIME = 1.5f;  //每个合成动画1.5s后销毁

    private GameObject m_cTopPanel;//导航栏
    private GameObject m_cBtnBack; //返回按钮
    private GameObject m_cMainPanel;//主面板
    private UILabel m_cLbItemName;
    private UILabel m_cLbItemDesc;
    //private UILabel m_cLbItemNameS;
    private UILabel m_cLbItemCount;
    private UISprite m_cSpItemIcon;
    private UISprite m_cSpItemBorder;
    private GameObject m_cBtnCombine;
    private GameObject m_cParenList;
    public UILabel m_cLbFarmPoint;
    public UILabel m_cLbFPiont;
    public UIPanel m_cPan;  //素材合成里面的panel
    private GameObject m_cTexRe;
    private GameObject m_cTexEq;
    private UILabel m_cLbBackBtn;

    private UnityEngine.Object m_cItemSourse;
    List<ReconceDetailItem> m_lstShowItem=new List<ReconceDetailItem>();
    ItemCompositeTable m_clstTable;
    private int m_cCombineTableId=-1;  //目标合成物品
    private bool m_bCanCombine = false;
    private int m_iOldGUIID = -1;

    private List<int> m_lstCombinedItemTableIDs;  //合成物品ID
    private List<int> m_lstEquipNums;   //合成物品数量

    private string m_strWarnMsg = "";

    public GUIReconceCombineDetail(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_RECONCE_COMBINE, GUILAYER.GUI_PANEL)
    { }

    protected override void InitGUI()
    {
        base.Show();

        GUI_FUNCTION.AYSNCLOADING_HIDEN();

        if (this.m_cGUIObject == null)
        {
            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance()
                .Load(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            this.m_cBtnBack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_BACK);
            GUIComponentEvent backEvent = this.m_cBtnBack.AddComponent<GUIComponentEvent>();
            backEvent.AddIntputDelegate(OnClickBackButton);

            this.m_cMainPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, MAINPANEL);
            this.m_cMainPanel.transform.localPosition = new Vector3(640, 0, 0);

            this.m_cTopPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PANEL_TOPPANEL);
            this.m_cTopPanel.transform.localPosition = new Vector3(-420, 270, 0);

            m_cLbItemName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cMainPanel, LB_ITEM_NAME);
            //m_cLbItemNameS = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cMainPanel, LB_ITEM_NAME_S);
            m_cLbItemDesc = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cMainPanel, LB_ITEM_DESC);
            m_cLbItemCount = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cMainPanel, LB_ITEM_COUNT);
            m_cSpItemIcon = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cMainPanel, SP_ITEM);
            m_cSpItemBorder = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cMainPanel, SP_ITEM_BORDER);
            m_cBtnCombine = GUI_FINDATION.GET_GAME_OBJECT(m_cMainPanel, BTN_COMBINE);
            m_cLbFarmPoint = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cMainPanel, LB_FARMPOINT);
            m_cLbFPiont = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cMainPanel, LB_FPOINT);
            m_cBtnCombine.AddComponent<GUIComponentEvent>().AddIntputDelegate(ItemCombine_OnEvent);
            m_cParenList = GUI_FINDATION.GET_GAME_OBJECT(m_cMainPanel, RES_LIST);
            m_cTexEq = GUI_FINDATION.GET_GAME_OBJECT(this.m_cMainPanel, TEX_EQ);
            m_cTexRe = GUI_FINDATION.GET_GAME_OBJECT(this.m_cMainPanel, TEX_RE);
            m_cLbBackBtn = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, BTN_BACK_LB);

            this.m_cItemSourse = (UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_LISTITEM);

            this.m_cPan = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(this.m_cMainPanel, RES_PANEL);

            this.m_cGuiEffect = GUI_FINDATION.FIND_GAME_OBJECT(GUI_EFFECT);

            this.m_cEffectParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGuiEffect, EFFECT_CENTER_ANCHOR);

            this.m_cEffectCombine = (UnityEngine.Object)ResourcesManager.GetInstance().Load(EFFECT_COMBINE);
        }

        if (m_iOldGUIID == GUI_DEFINE.GUIID_RECONCELIHOUSE)  //消耗品合成
        {
            m_cTexEq.SetActive(false);
            m_cTexRe.SetActive(true);
            m_cBtnCombine.GetComponent<UISprite>().spriteName = BTN_RE;
            m_cLbBackBtn.text = "调合屋";
        }
        else
        {
            m_cTexEq.SetActive(true);
            m_cTexRe.SetActive(false);
            m_cBtnCombine.GetComponent<UISprite>().spriteName = BTN_EQ;
            m_cLbBackBtn.text = "装备屋";
        }

        m_clstTable = ItemCompositeTableManager.GetInstance().GetItemCompositeTable(m_cCombineTableId);

        m_lstEffects.Clear();
        CameraManager.GetInstance().ShowGUIEffectCamera();

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
        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, -6, 0), new Vector3(0, -6, 0));

        this.m_cPan.depth = 301;

        //新手引导
        GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_TOWN7);
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
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_LISTITEM);
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_EFFECT_PATH, GAME_DEFINE.RES_VERSION, EFFECT_COMBINE);
        }
        else
        {
            InitGUI();
        }
    }

    /// <summary>
    /// 刷新显示
    /// </summary>
    private void UpdateShow()
    {
        if (m_cCombineTableId == -1)
        {
            return;
        }

        ItemTable m_cCombineItem = ItemTableManager.GetInstance().GetItem(m_cCombineTableId);

        m_cLbItemName.text = m_cCombineItem.Name;
        //m_cLbItemNameS.text = m_cCombineItem.ShortName;
        m_cLbItemDesc.text = m_cCombineItem.Desc;
        if (m_cCombineItem.Type == (int)ITEM_TYPE.EQUIP)  //如果是装备，正常展示所有数量，如果是消耗品，展示所有数量减去已经装备进入战斗的物品
        {
            m_cLbItemCount.text =(Role.role.GetItemProperty().GetItemCountByTableIdForDummy(m_cCombineTableId) - Role.role.GetItemProperty().GetEquipNumByTableId(m_cCombineTableId)).ToString();  //获取所有物品虚拟数量  
        }
        else
        {
            int allcount = Role.role.GetItemProperty().GetItemCountByTableIdForDummy(m_cCombineTableId);  //获取所有物品虚拟数量
            Item batitem = Role.role.GetItemProperty().GetBattleItemByTableID(m_cCombineTableId);
            if (batitem != null)
            {
                allcount -= batitem.m_iNum;
            }
            if (allcount > 99)
            {
                allcount = 99;
            }
            m_cLbItemCount.text = allcount.ToString();  //获取所有物品虚拟数量
        }
      
        GUI_FUNCTION.SET_ITEMM(m_cSpItemIcon, m_cCombineItem.SpiritName);
        GUI_FUNCTION.SET_ITEM_BORDER(m_cSpItemBorder, (ITEM_TYPE)m_cCombineItem.Type);

        m_lstShowItem.ForEach((item) => { GameObject.DestroyImmediate(item.m_cItem); });
        m_lstShowItem.Clear();

        m_bCanCombine = true;
        for (int i = 0; i < m_clstTable.LstNeedID.Count; i++)
        {
            ReconceDetailItem tmp = new ReconceDetailItem(m_cItemSourse);
            tmp.m_cItem.transform.parent = m_cParenList.transform;
            tmp.m_cItem.transform.localScale = Vector3.one;
            tmp.m_cItem.transform.localPosition = new Vector3(0, 55 * -i, 0);

            ItemTable ned = ItemTableManager.GetInstance().GetItem(m_clstTable.LstNeedID[i]);
            int nedcount = m_clstTable.LstNeedNum[i];

            tmp.m_cLbName.text = ned.Name;
            tmp.m_cNeedNum.text = nedcount.ToString();  //需要的数量
            GUI_FUNCTION.SET_ITEMM(tmp.m_cSpIcon, ned.SpiritName);
            GUI_FUNCTION.SET_ITEM_BORDER(tmp.m_cSpBorder, (ITEM_TYPE)ned.Type);

            int nowCount = 0;
            if (ned.Type == (int)ITEM_TYPE.EQUIP)
            {
                nowCount = Role.role.GetItemProperty().GetItemCountByTableIdForDummy(ned.ID);
                nowCount -= Role.role.GetItemProperty().GetEquipNumByTableId(ned.ID);
            }
            else if (ned.Type == (int)ITEM_TYPE.CONSUME)
            {
                nowCount = Role.role.GetItemProperty().GetItemCountByTableIdForDummy(ned.ID);  //获取所有物品虚拟数量
                Item batitem = Role.role.GetItemProperty().GetBattleItemByTableID(ned.ID);
                if (batitem != null)
                {
                    nowCount -= batitem.m_iNum;
                }
                if (nowCount > 99)
                {
                    nowCount = 99;
                }
            }
            else
                nowCount = Role.role.GetItemProperty().GetItemCountByTableIdForDummy(ned.ID);  //现在虚拟拥有的数量

            tmp.m_cNowNum.text = nowCount.ToString();

            if (nedcount > nowCount)
            {
                tmp.m_cNowNum.color = Color.red;
                m_bCanCombine = false;
                m_strWarnMsg = "素材不足";
            }

            m_lstShowItem.Add(tmp);
        }
        
        //装备需要消耗农场点，消耗品不需要消耗农场点，隐藏提示
        if (ItemTableManager.GetInstance().GetItem(m_cCombineTableId).Type == (int)ITEM_TYPE.EQUIP)
        {
            this.m_cLbFarmPoint.enabled = true;
            this.m_cLbFPiont.enabled = true;
            if (Role.role.GetBaseProperty().m_iFarmPoint < m_clstTable.NeedFarmPoint)
            {
                m_bCanCombine = false;
                this.m_cLbFarmPoint.color = Color.red;
                m_strWarnMsg = "元气不足";
            }
            else
            {
                this.m_cLbFarmPoint.color = Color.white;
            }
            this.m_cLbFarmPoint.text = m_clstTable.NeedFarmPoint.ToString();

        }
        else
        {
            this.m_cLbFarmPoint.enabled = false;
            this.m_cLbFPiont.enabled = false;
        }
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {

        //关闭合成特效摄像头
        CameraManager.GetInstance().HidenGUIEffectCamera();

        if (m_lstCombinedItemTableIDs != null && m_lstEquipNums.Count != 0)
        {
            SendAgent.SendItemCombinedReq(Role.role.GetBaseProperty().m_iPlayerId,
                m_lstCombinedItemTableIDs, m_lstEquipNums);
        }

        //base.Hiden();

        //GUI_FUNCTION.AYSNCLOADING_HIDEN();

        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, -6, 0), new Vector3(640, -6, 0));
        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-420, 270, 0),Destory);
    }

    //隐藏不发送数据
    public void HidenNotSend()
    {
        //base.Hiden();
        GUI_FUNCTION.AYSNCLOADING_HIDEN();

        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEOUT_GUI_TIME, new Vector3(0, -6, 0), new Vector3(640, -6, 0));
        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEOUT_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-420, 270, 0),Destory);

        //关闭合成特效摄像头
        CameraManager.GetInstance().HidenGUIEffectCamera();
    }

    /// <summary>
    /// 销毁对象
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        m_cGuiEffect = null;    //3d特效资源
        m_cEffectParent = null; //3d特效父对象
        m_cEffectCombine = null; //合成特效


        m_cTopPanel = null;//导航栏
        m_cBtnBack = null; //返回按钮
        m_cMainPanel = null;//主面板
        m_cLbItemName = null;
        m_cLbItemDesc = null;
        m_cLbItemCount = null;
        m_cSpItemIcon = null;
        m_cSpItemBorder = null;
        m_cBtnCombine = null;
        m_cParenList = null;
        m_cLbFarmPoint = null;
        m_cLbFPiont = null;
        m_cPan = null;  //素材合成里面的panel
        m_cTexRe = null;
        m_cTexEq = null;
        m_cLbBackBtn = null;

        m_cItemSourse = null;

        m_lstShowItem.Clear();


        foreach (GameObject item in m_lstEffects)
        {
            GameObject.DestroyImmediate(item);
        }

        m_lstEffects.Clear();

        base.Destory();
    }
    /// <summary>
    /// 更新逻辑
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

        if (this.IsShow())
        {
            //自动销毁特效
            if (m_lstEffects.Count > 0)
            {
                float fl = GAME_TIME.TIME_FIXED() - m_fDis;
                if (fl >= EFFECT_TIME)
                {
                    foreach (GameObject item in m_lstEffects)
                    {
                        GameObject.DestroyImmediate(item);
                    }
                    m_lstEffects.Clear();
                }
            }
        }

        return true;
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

            if (m_iOldGUIID == GUI_DEFINE.GUIID_RECONCELIHOUSE)
            {
                GUIReconceliHouse hou = this.m_cGUIMgr.GetGUI(m_iOldGUIID) as GUIReconceliHouse;
                hou.SetSendData(this.m_lstCombinedItemTableIDs, m_lstEquipNums);
                if (m_lstEquipNums != null)

                HidenNotSend();
                hou.Show();
            }
            else
            {
                GUIEquipmentHouse hou = this.m_cGUIMgr.GetGUI(m_iOldGUIID) as GUIEquipmentHouse;
                hou.SetSendData(this.m_lstCombinedItemTableIDs, m_lstEquipNums);

                HidenNotSend();
                hou.Show();
            }
        }
    }

    /// <summary>
    /// 合成按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void ItemCombine_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {

            if (m_bCanCombine)
            {

                //计算将要采集到的物品是否会造成物品栏超出最大限度
                List<Item> roleItems = Role.role.GetItemProperty().GetAllItem();
                List<Item> TmpItems = new List<Item>();
                for (int i = 0; i < roleItems.Count; i++)
                {
                    Item tmp = new Item(roleItems[i].m_iTableID);
                    tmp.m_iID = roleItems[i].m_iID;
                    tmp.m_iNum = roleItems[i].m_iNum;
                    TmpItems.Add(tmp);
                }
                Item newItem2 = new Item(m_cCombineTableId);
                newItem2.m_iNum = 1;
                newItem2.m_iID = -2;
                TmpItems = Role.role.GetItemProperty().AddItem(TmpItems, newItem2);
                if (Role.role.GetItemProperty().GetAllItemCount(TmpItems) > Role.role.GetBaseProperty().m_iMaxItem)
                {
                    GUI_FUNCTION.MESSAGEM(null, "仓库已满，无法合成\n#ff0000]" + newItem2.m_strName);
                    return;
                }


                foreach (GameObject item in m_lstEffects)
                {
                    GameObject.DestroyImmediate(item);
                }
                m_lstEffects.Clear();
                GameObject eff = GameObject.Instantiate(this.m_cEffectCombine) as GameObject;
                eff.transform.parent = this.m_cEffectParent.transform;
                eff.transform.localScale = Vector3.one;
                eff.transform.localPosition = Vector3.zero;
                m_lstEffects.Add(eff);
                m_fDis = GAME_TIME.TIME_FIXED();

                //添加到对应的合成物品列表，准备发送数据
                if (m_iOldGUIID == GUI_DEFINE.GUIID_RECONCELIHOUSE)  //消耗品合成
                {
                    GUIReconceliHouse tmp = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_RECONCELIHOUSE) as GUIReconceliHouse;
                    tmp.AddCombinedItems(m_cCombineTableId);
                    AddCombinedItems(m_cCombineTableId);

                    //更新本地列表 删除合成掉的物品
                    for (int i = 0; i < m_clstTable.LstNeedID.Count; i++)
                    {
                        Item tmpitem = Role.role.GetItemProperty().GetItemByTableID(m_clstTable.LstNeedID[i]);
                        tmpitem.m_iDummyNum -= m_clstTable.LstNeedNum[i];

                        Role.role.GetItemProperty().UpdateItem(tmpitem);
                    }
                    //新的合成物品临时加入本地，id为负数，等待上传服务器更新以后，会全量替换本地的数据，届时会使用服务器生成的物品ID
                    Item newItem = Role.role.GetItemProperty().GetItemByTableID(m_cCombineTableId);
                    if (newItem == null)
                    {
                        newItem = new Item(m_cCombineTableId);
                        newItem.m_iDummyNum = 1;   //新合成不存在的物品，虚拟增加了1
                        newItem.m_iNum = 0;               //新合成不存在的物品，实际为0
                        newItem.m_iID = -2;
                        Role.role.GetItemProperty().AddItem(newItem);
                    }
                    else
                    {
                        newItem.m_iDummyNum += 1;
                        Role.role.GetItemProperty().UpdateItem(newItem);
                    }

                    //播放消耗品合成音效
                    SoundManager.GetInstance().PlaySound(SOUND_DEFINE.SE_CONSUME_COMBINE);
                }
                else if (m_iOldGUIID == GUI_DEFINE.GUIID_EQUIPMENTHOUSE)  //装备合成
                {
                    GUIEquipmentHouse tmp = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_EQUIPMENTHOUSE) as GUIEquipmentHouse;
                    tmp.AddCombinedItems(m_cCombineTableId);
                    AddCombinedItems(m_cCombineTableId);
                    //如果是装备合成 先扣除农场点
                    Role.role.GetBaseProperty().m_iFarmPoint -= m_clstTable.NeedFarmPoint;
                    GUIBackFrameTop topframe = m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP) as GUIBackFrameTop;
                    topframe.UpdateFarmPiont();

                    //更新本地列表 删除合成掉的装备，每个装备单独删除
                    for (int i = 0; i < m_clstTable.LstNeedID.Count; i++)
                    {
                        ItemTable ned = ItemTableManager.GetInstance().GetItem(m_clstTable.LstNeedID[i]);
                        if (ned.Type == (int)ITEM_TYPE.EQUIP)
                        {
                            List<Item> list = Role.role.GetItemProperty().GetItemListByTableId(m_clstTable.LstNeedID[i]);
                            for (int j = 0; j < m_clstTable.LstNeedNum[i]; j++) //删除被合成的装备,更新虚拟数量为0，而非真正删除
                            {
                                foreach (Item item in list)
                                {
                                    if (item.m_iDummyNum != 0)
                                    {
                                        item.m_iDummyNum = 0;
                                        Role.role.GetItemProperty().UpdateItemByID(item);
  
                                        break;
                                    }
                                }
                            }

                        }
                        else
                        {
                            Item tmpitem = Role.role.GetItemProperty().GetItemByTableID(m_clstTable.LstNeedID[i]);
                            tmpitem.m_iDummyNum -= m_clstTable.LstNeedNum[i];
                            Role.role.GetItemProperty().UpdateItem(tmpitem);
                        }
                    }
                    //新的合成物品临时加入本地，id为负数，等待上传服务器更新以后，会全量替换本地的数据，届时会使用服务器生成的物品ID
                    Item newItem = new Item(m_cCombineTableId);
                    newItem.m_iDummyNum = 1;
                    newItem.m_iNum = 0;  //该合成新装备，实际为0
                    newItem.m_iID = -2;
                    Role.role.GetItemProperty().AddItem(newItem);

                    //播放装备合成音效
                    SoundManager.GetInstance().PlaySound(SOUND_DEFINE.SE_EQUIP_COMBINE);
                }

                UpdateShow();
            }
            else
            {
                GUI_FUNCTION.MESSAGEM(null, m_strWarnMsg);

            }
        }
    }

    /// <summary>
    /// 设置目标合成物品
    /// </summary>
    /// <param name="comItem"></param>
    public void SetSelectCombineItem(int tableID)
    {
        m_cCombineTableId = tableID;
    }

    /// <summary>
    /// 设置上一层guiID
    /// </summary>
    /// <param name="guiid"></param>
    public void SetOldGuiID(int guiid)
    {
        m_iOldGUIID = guiid;
    }

    /// <summary>
    /// 设置发送数据
    /// </summary>
    /// <param name="m_lstCombinedItemTableIDs"></param>
    /// <param name="m_lstEquipNums"></param>
    public void SetSendData(List<int> CombinedItemTableIDs, List<int> EquipNums)
    {
        if (CombinedItemTableIDs != null)
        {
            this.m_lstCombinedItemTableIDs = new List<int>(CombinedItemTableIDs);
        }
        if (EquipNums != null)
        {
            this.m_lstEquipNums = new List<int>(EquipNums);
        }
    }

    /// <summary>
    /// 将合成的物品记录到列表里面，当返回时，一起上传
    /// </summary>
    /// <param name="itemTableID"></param>
    private void AddCombinedItems(int itemTableID)
    {
        if (m_lstCombinedItemTableIDs == null)
        {
            m_lstCombinedItemTableIDs = new List<int>();
            m_lstEquipNums = new List<int>();
        }

        m_lstCombinedItemTableIDs.Add(itemTableID);
        m_lstEquipNums.Add(1);
    }
}