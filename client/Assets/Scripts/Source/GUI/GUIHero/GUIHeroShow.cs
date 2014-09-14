using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Game.Resource;


//  GUIHeroShow.cs
//  Author: Lu Zexi
//  2013-12-18

/// <summary>
/// 英雄展示
/// </summary>
public class GUIHeroShow : GUIHeroSelectBase
{
    public GUIHeroShow(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_HERO_SHOW, GUILAYER.GUI_PANEL)
    {
        //
    }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        this.m_eLoadingState = LOADING_STATE.START;
        GUI_FUNCTION.AYSNCLOADING_SHOW();
        ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + BTN_NULL);
        if (this.m_cGUIObject == null)
        {
			ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_MAIN);
			ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_HEROITEM);
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
    /// 展示
    /// </summary>
    protected override void InitGUI()
    {

        GUI_FUNCTION.AYSNCLOADING_HIDEN();
        //优化每次进入都读取最新所有英雄，否则，如果界面没有被销毁，进入界面选择英雄是老数据
        this.m_lstHero = Role.role.GetHeroProperty().GetAllHero();

        if (this.m_cGUIObject == null)
        {
            //设置要显示的英雄列表
            this.m_iShowOffsetX = 0;

            //生成画面
            base.InitGUI();
            //设置页面头
            this.m_cUITittle.text = "英雄一览";

            //设置返回输入接口
            GUIComponentEvent ce = this.m_cCancelBtn.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnCancel);


        }
        else
        {
            base.InitGUI();
        }

        //设置点击英雄接口
        foreach (HeroShowItem item in this.m_lstHeroShow)
        {
            GUIComponentEvent tmp = item.m_cItem.GetComponent<GUIComponentEvent>();
            if (tmp == null)
                tmp = item.m_cItem.AddComponent<GUIComponentEvent>();
            tmp.AddIntputDelegate(OnHero, item.m_cHero.m_iID);
        }

        //设置整体GUI点击GUIID
        this.m_cGUIMgr.SetCurGUIID(this.ID);

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_SCANE_HERO));
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

            this.m_cPanelClipRange = Vector4.zero;
            this.m_cPanelLocalposition = Vector3.zero;
            this.Hiden();
            m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERO_MENU).Show();
        }
    }

    /// <summary>
    /// 点击英雄
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnHero(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {

            GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

            this.Hiden();
            int heroid = (int)args[0];

            GUIHeroDetail herodetail = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERODETAIL) as GUIHeroDetail;
            herodetail.Show(ShowNotChange, Role.role.GetHeroProperty().GetHero(heroid));
            herodetail.m_cLocalposition = this.m_cClipPanel.transform.localPosition;
            herodetail.m_cClipRange = this.m_cClipPanel.clipRange;
        }
    }

    /// <summary>
    ///查看英雄信息回调
    /// </summary>
    private void ShowNotChange()
    {
        GUIHeroDetail herodetail = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERODETAIL) as GUIHeroDetail;
        this.m_cPanelClipRange = herodetail.m_cClipRange;
        this.m_cPanelLocalposition = herodetail.m_cLocalposition;

        this.Show();
    }
}