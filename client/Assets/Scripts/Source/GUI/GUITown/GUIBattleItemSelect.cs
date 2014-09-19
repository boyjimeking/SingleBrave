using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;

public class GUIBattleItemSelect : GUIItemSelectBase
{
    public int m_iSelectIndex = 0;
    private GUIGroupsDetail m_cBattleDetail;

    private const string BTN_NULL = "GUI_BtnNull";
    private const string SP_NULL = "SP_Null";
    private UISprite m_cBtnNull;
    public bool m_bBackToFightReady;

    public GUIBattleItemSelect(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_BATTLE_ITEM_SELECT, UILAYER.GUI_PANEL)
    { }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {

        this.m_eLoadingState = LOADING_STATE.START;
        GUI_FUNCTION.AYSNCLOADING_SHOW();

        ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + BTN_NULL);
        ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_PROPSITEM);
        ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + GUIGroupsDetail.RES_MAIN);
        if (this.m_cGUIObject == null)
        {
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_MAIN);
        }
    }

    public override void Hiden()
    {
        base.Hiden();
        ResourceMgr.UnloadUnusedResources();
        Destory();
    }

    public override void Destory()
    {
        m_cBtnNull = null;
        if (m_cBattleDetail != null)
            m_cBattleDetail.Destory();
        m_cBattleDetail = null;

        base.Destory();
    }

    protected override void InitGUI()
    {
        this.m_lstItems = Role.role.GetItemProperty().GetAllItem().FindAll(new Predicate<Item>((q) =>
        {
            return q.m_eType == ITEM_TYPE.CONSUME;
        }));

        this.m_lstItems.ForEach(q => { q.m_iDummyNum = q.m_iNum; });

        //为置空按钮预留空间
        this.m_iShowOffsetX = 1;

        if (this.m_cGUIObject == null)
        {
            //生成画面
            base.InitGUI();
            //设置页面头
            this.m_cUITittle.text = "道具选择";

            //设置返回输入接口
            GUIComponentEvent ce = this.m_cBtnBack.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnCancel);

        }
        else
        {
            base.InitGUI();
        }

        if (m_cBtnNull != null)
        {
            GameObject.Destroy(m_cBtnNull.gameObject);
        }

        //加入置空按钮
		GameObject btnnull = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(BTN_NULL)) as GameObject;
        m_cBtnNull = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(btnnull, SP_NULL);
        btnnull.transform.parent = this.m_cListView.transform;
        btnnull.transform.localPosition = Vector3.zero;
        btnnull.transform.localScale = Vector3.one;
        GUIComponentEvent btnCe = m_cBtnNull.gameObject.AddComponent<GUIComponentEvent>();
        btnCe.AddIntputDelegate(OnBtnNull);



        //设置点击英雄接口
        for (int i = 0; i < this.m_lstItemShow.Count; i++)
        {
            ItemShowItem item = m_lstItemShow[i];
            Item[] battleItems = Role.role.GetItemProperty().GetAllBattleItem();
            bool isBattle = false;
            bool isSelect = false;
            for (int j = 0; j < battleItems.Length; j++)
            {
                if (j == m_iSelectIndex)
                {
                    if (battleItems[j] != null && battleItems[j].m_iTableID == item.m_cItem.m_iTableID)
                    {
                        isSelect = true;
                    }
                    continue;
                }
                if (battleItems[j] != null && battleItems[j].m_iTableID == item.m_cItem.m_iTableID)
                {
                    isBattle = true;
                    break;
                }
            }
            if (isBattle)
            {
                item.m_cItemE.enabled = true;
                item.m_cItemBg.depth += 2;
                continue;
            }
            if (isSelect)
            {
                item.m_cItemE.enabled = true;
            }
            GUIComponentEvent tmp = item.m_cRes.GetComponent<GUIComponentEvent>();
            if (tmp == null)
                tmp = item.m_cRes.AddComponent<GUIComponentEvent>();
            tmp.AddIntputDelegate(ItemSelect_OnEvent, i);
        }

        if (m_cBattleDetail != null)
        {
            GameObject.DestroyImmediate(m_cBattleDetail.m_cMain);
            m_cBattleDetail = null;
        }

        //设置整体GUI点击GUIID
        GUITown town = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_TOWN) as GUITown;
        town.SetTownChildId(this.ID);
        town.SetTownBlack(false);
        GUIBackFrameTop backtop = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP) as GUIBackFrameTop;
        backtop.Show();
        //this.m_cGUIMgr.SetCurGUIID(this.ID);

        //新手引导
        GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_TOWN12);
    }

    /// <summary>
    /// 取消按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnCancel(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {

            GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

            this.Hiden();
            GUIPropsGroup tmp = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_PROPSGROUP) as GUIPropsGroup;
            tmp.m_bBackToFightReady = this.m_bBackToFightReady;
            tmp.Show();
        }
    }

    /// <summary>
    /// 道具英雄
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void ItemSelect_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            int itemIndex = (int)args[0];
            Item tmp = m_lstItems[itemIndex];

            this.m_cBack.enabled = true;

            if (m_cBattleDetail != null)
            {
                GameObject.DestroyImmediate(m_cBattleDetail.m_cMain);
                m_cBattleDetail = null;
            }

            m_cBattleDetail = new GUIGroupsDetail(this.m_cMainPanel, tmp);
            m_cBattleDetail.m_cMain.SetActive(true);
            m_cBattleDetail.m_cBtnSell.AddComponent<GUIComponentEvent>().AddIntputDelegate(ItemEquip_OnEvent);

        }
    }

    /// <summary>
    /// 设置战斗装备位置
    /// </summary>
    /// <param name="index"></param>
    public void SetBattleIndex(int index)
    {
        m_iSelectIndex = index;
    }

    /// <summary>
    /// 物品装备
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void ItemEquip_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            //Item it = Role.role.GetItemProperty().GetItemByTableID(m_cBattleDetail.m_cProp.m_iTableID);
            ////获取所有装备的和没有装备的item总数，减去选中的
            //it.m_iNum = Role.role.GetItemProperty().GetItemCountByTableId(m_cBattleDetail.m_cProp.m_iTableID) - m_cBattleDetail.m_iSelectItemNum;
            ////更新装备后失去的消耗品数量
            //Role.role.GetItemProperty().UpdateItem(it);
            //添加装备消耗品数量
            Role.role.GetItemProperty().UpdateBattleItem(m_cBattleDetail.m_cProp.m_iTableID, m_cBattleDetail.m_iSelectItemNum, m_iSelectIndex);

            this.Hiden();

            GUIPropsGroup tmp = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_PROPSGROUP) as GUIPropsGroup;
            tmp.m_bBackToFightReady = this.m_bBackToFightReady;
            tmp.Show();
        }
    }

    /// <summary>
    /// 置空按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void OnBtnNull(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            //根据位置，获得该位置要清空的物品
            Item[] allBattle = Role.role.GetItemProperty().GetAllBattleItem();
            Item tmp = allBattle[m_iSelectIndex];
            if (tmp != null)
            {
                Role.role.GetItemProperty().UpdateBattleItem(-1, 0, m_iSelectIndex);
            }

            this.Hiden();
            GUIPropsGroup tmps = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_PROPSGROUP) as GUIPropsGroup;
            tmps.m_bBackToFightReady = this.m_bBackToFightReady;
            tmps.Show();
        }
    }

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
}