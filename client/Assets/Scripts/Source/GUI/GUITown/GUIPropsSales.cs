using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using Game.Resource;

//道具售卖
//Author:Sunyi
//2013-12-5

public class GUIPropsSales : GUIItemSelectBase
{
    private GUIPropsSalesDetail m_cSaleDetail;

    public GUIPropsSales(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_PROPSSALES, GUILAYER.GUI_PANEL)
    { }

    protected override void InitGUI()
    {
        this.m_lstItems = Role.role.GetItemProperty().GetAllItem();
        this.m_lstItems.ForEach(q => { q.m_iDummyNum = q.m_iNum; });

        this.m_bIfWithBattle = true;
        this.m_bEqEnable = true;

        if (this.m_cGUIObject == null)
        {
            //设置要显示的英雄列表
            this.m_iShowOffsetX = 0;

            //生成画面
            base.InitGUI();
            //设置页面头
            this.m_cUITittle.text = "道具出售";

            //设置返回输入接口
            GUIComponentEvent ce = this.m_cBtnBack.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnCancel);
        }
        else
        {
            base.InitGUI();
        }

        //设置点击英雄接口
        for (int i = 0; i < this.m_lstItemShow.Count; i++)
        {
            ItemShowItem item = m_lstItemShow[i];
            GUIComponentEvent tmp = item.m_cRes.GetComponent<GUIComponentEvent>();
            if (tmp == null)
                tmp = item.m_cRes.AddComponent<GUIComponentEvent>();
            tmp.AddIntputDelegate(ItemSelect_OnEvent, i);
        }

        //设置整体GUI点击GUIID
        GUITown town = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_TOWN) as GUITown;
        town.SetTownChildId(this.ID);
        town.SetTownBlack(false);
        GUIBackFrameTop backtop = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP) as GUIBackFrameTop;
        backtop.Show();
        //this.m_cGUIMgr.SetCurGUIID(this.ID);

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_SALE_ITEM));
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
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_PROPSITEM);
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + GUIPropsSalesDetail.RES_MAIN);
        }
        else
        {
            InitGUI();
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

    /// <summary>
    /// 返回按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnCancel(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (this.m_cBack.enabled)
            {
                this.m_cBack.enabled = false;
                m_cSaleDetail.m_cMain.SetActive(false);
            }
            else
            {
                this.Hiden();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_PROPSWAREHOUSE).Show();
            }
        }
    }

    /// <summary>
    /// 物品项点击事件
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

            if (m_cSaleDetail != null)
            {
                GameObject.DestroyImmediate(m_cSaleDetail.m_cMain);
            }

            m_cSaleDetail = new GUIPropsSalesDetail(this.m_cMainPanel, tmp);
            m_cSaleDetail.m_cMain.SetActive(true);
        }
    }

    /// <summary>
    /// 隐藏出售详情界面
    /// </summary>
    public void HidenSellDetail()
    {
        if (m_cSaleDetail != null)
        {
            m_cSaleDetail.m_cMain.SetActive(false);
            GameObject.DestroyImmediate(m_cSaleDetail.m_cMain);

            Show();
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
        if (m_cSaleDetail != null)
        {
            m_cSaleDetail.Destroy();
        }
        m_cSaleDetail = null;

        base.Destory();
    }
}