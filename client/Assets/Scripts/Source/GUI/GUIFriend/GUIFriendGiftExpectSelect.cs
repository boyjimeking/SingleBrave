//  GUIFriendGiftExpectSelect.cs
//  Author: Cheng Xia
//  2013-1-15

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;

/// <summary>
/// 好友礼物界面GUI
/// </summary>
public class GUIFriendGiftExpectSelect : GUIFriendGiftSelectBase
{
    //private const string RES_MAIN = "GUI_FriendGiftSelect";                   //主资源地址

    private const string BTN_CANCEL = "Title_Cancel/Btn_Cancel";        //取消按钮地址
    private const string PAN_CANCEL = "Title_Cancel";                   //取消Pan地址
    private const string PAN_RIGHT = "PanInfo";                         //滑出Panel地址
    private const string RES_TABLE = "PanInfo/Panel/Table";             //Table地址

    private GameObject m_cPanSlide;       //panel滑动
    private GameObject m_cBtnCancel;      //取消按钮Panel
    private UITable m_cTable;             //table

    public GUIFriendGiftExpectSelect(GUIManager mgr)
        : base(mgr, GUI_DEFINE.GUIID_FRIENDGIFTEXPECTSELECT, GUILAYER.GUI_PANEL)
    {
    }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        this.m_eLoadingState = LOADING_STATE.START;
        GUI_FUNCTION.AYSNCLOADING_SHOW();
        ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_PROPSITEM);
        if (this.m_cGUIObject == null)
        {
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_MAIN_BASE);
        }
    }

    /// <summary>
    /// 初始化GUI
    /// </summary>
    protected override void InitGUI()
    {
        GUI_FUNCTION.AYSNCLOADING_HIDEN();

        this.m_lstShows = FriendGiftItemTableManager.GetInstance().GetItemAll();

        if (this.m_cGUIObject == null)
        {
            base.Show();
            m_cUITittle.text = "素材选择";

            //设置返回输入接口
            GUIComponentEvent ce = this.m_cBtnBack.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnCancel);
        }
        else
        {
            base.Show();
        }

        //设置点击物品接口
        for (int i = 0; i < this.m_lstItemShow.Count; i++)
        {
            ItemShowItem item = m_lstItemShow[i];
            GUIComponentEvent tmp = item.m_cRes.GetComponent<GUIComponentEvent>();
            if (tmp == null)
                tmp = item.m_cRes.AddComponent<GUIComponentEvent>();
            tmp.AddIntputDelegate(ItemSelect_OnEvent, i);
        }

        this.m_cGUIMgr.SetCurGUIID(this.ID);

        SetLocalPos(Vector3.zero);
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        Destory();

        ResourceMgr.UnloadUnusedResources();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();
        
        this.m_cPanSlide = null;
        this.m_cBtnCancel = null;
        this.m_cTable = null;

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

    private void OnCancel(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.Hiden();
            this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FRIENDGIFT).Show();
        }
    }

    private void ItemSelect_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            FriendGiftItem item = m_lstItemShow[(int)args[0]].m_cItem;
            Hiden();

            GUIFriendGift gui_friendGift = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FRIENDGIFT) as GUIFriendGift;
            Role.role.GetBaseProperty().m_vecWantItems[gui_friendGift.m_iSelectPos] = FriendGiftItemTableManager.GetInstance().GetItemByIndex((int)args[0]).ID;
            gui_friendGift.Show();
            gui_friendGift.m_bSend = true;
        }
    }
}