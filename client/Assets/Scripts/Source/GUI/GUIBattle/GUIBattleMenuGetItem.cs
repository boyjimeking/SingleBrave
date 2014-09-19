using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Resource;
using UnityEngine;

//战斗菜单-获得物品
//Author sunyi
//2014-3-26
public class GUIBattleMenuGetItem : GUIBase
{
    private const string RES_MAIN = "GUI_BattleMenuGetItem";
    private const string RES_GETITEM = "GUI_BattleMenuGetItemCell";//物品资源地址

    private const string PROPSITEM_PARENT = "GetItem/ListView/Table/ItemAll";//获取物品item父对象地址
    private const string PROPSITEM_LISTVIEW = "GetItem/ListView";//获取物品item滑动视图地址
    private const string PROPSITEMNONE_PARENT = "GetItem/None";//获取物品为0时显示的面板地址

    private const string LABEL_ITEMNAME = "Lab_Name";//物品名称标签地址
    private const string LABEL_ITEMCOUNT = "Lab_Count";//物品数量标签地址
    private const string SPR_ITEMICON = "Spr_Icon";//物品图标标签地址


    private UnityEngine.Object m_cGetPropsItem;//获取的物品列表item
    private GameObject m_cGetItemNone;//没有获取任何物品时显示的面板
    private GameObject m_cGetItemListView;//获得素材滚动视图
    private GameObject m_cPropsItemParent;//获取英雄item父对象


    private List<int> m_lstItemId = new List<int>();//当前战斗中已经获得的物品id
    private List<int> m_lstItemCount = new List<int>();//当前战斗中已经获得的物品个数

    private bool m_bIsFirstGetItem;

    public GUIBattleMenuGetItem(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_BATTLE_MENU_GETITEM, UILAYER.GUI_PANEL3)
    { 
        //
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
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_GETITEM);
        }
        else
        {
            InitGUI();
        }
    }

    /// <summary>
    /// 创建GUI
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

            this.m_cPropsItemParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PROPSITEM_PARENT);
            this.m_cGetItemNone = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PROPSITEMNONE_PARENT);
			this.m_cGetPropsItem = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_GETITEM);
            this.m_cGetItemListView = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PROPSITEM_LISTVIEW);

        }

        ShowGetItem();

        SetLocalPos(Vector3.zero);
    }


    private void ShowGetItem()
    {
        this.m_cGetItemListView.transform.localPosition = new Vector3(0, 0, 0);
        UIPanel panel = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(this.m_cGUIObject, PROPSITEM_LISTVIEW);
        float y = -295.0f;
        panel.clipRange = new Vector4(panel.clipRange.x, y, panel.clipRange.z, panel.clipRange.w);

        UITable table = GUI_FINDATION.GET_OBJ_COMPONENT<UITable>(this.m_cGUIObject, PROPSITEM_PARENT);

        if (this.m_lstItemId != null)
        {
            if (this.m_lstItemId.Count > 0)
            {
                this.m_cGetItemNone.SetActive(false);
                this.m_cPropsItemParent.SetActive(true);
                if (this.m_bIsFirstGetItem)
                {
                    foreach (Transform tsf in this.m_cPropsItemParent.transform)
                    {
                        GameObject.Destroy(tsf.gameObject);
                    }
                    for (int i = 0; i < this.m_lstItemId.Count; i++)
                    {
                        GameObject getPropsItem = GameObject.Instantiate(this.m_cGetPropsItem) as GameObject;
                        getPropsItem.transform.parent = this.m_cPropsItemParent.transform;
                        getPropsItem.transform.localScale = Vector3.one;

                        ItemTable itemTable = ItemTableManager.GetInstance().GetItem(this.m_lstItemId[i]);

                        UILabel labName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(getPropsItem, LABEL_ITEMNAME);
                        labName.text = itemTable.ShortName;

                        UILabel labCount = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(getPropsItem, LABEL_ITEMCOUNT);
                        labCount.text = "X" + this.m_lstItemCount[i].ToString();

                        UISprite sprIcon = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(getPropsItem, SPR_ITEMICON);
                        GUI_FUNCTION.SET_ITEMM(sprIcon, itemTable.SpiritName);
                        table.repositionNow = true;
                        this.m_bIsFirstGetItem = false;
                    }
                }

            }
            else
            {
                this.m_cGetItemNone.SetActive(true);
                this.m_cPropsItemParent.SetActive(false);
            }
        }
    }

    public void SetGetItem(List<int> lstItemId, List<int> lstItemCount)
    {
        this.m_lstItemCount = lstItemCount;
        this.m_lstItemId = lstItemId;
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        ResourceMgr.UnloadResource(RES_MAIN);
        ResourceMgr.UnloadResource(RES_GETITEM);
        Destory();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        this.m_cGetPropsItem = null;
        this.m_cPropsItemParent = null;
        this.m_cGetItemNone = null;
        this.m_cGetItemListView = null;

        base.Destory();
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    /// <returns></returns>
    public override bool Update()
    {
        base.Hiden();

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

    public void SetIsFirstGetItem(bool isFirstGetItem)
    {
        this.m_bIsFirstGetItem = isFirstGetItem;
    }
}

