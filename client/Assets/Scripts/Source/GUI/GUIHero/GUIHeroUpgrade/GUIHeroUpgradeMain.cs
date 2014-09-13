//  GUIUpgrade.cs
//  Author: Cheng Xia
//  2013-12-20

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;
using Game.Gfx;
using Game.Base;

/// <summary>
/// 英雄升级主英雄选择界面
/// </summary>
public class GUIHeroUpgradeMain : GUIHeroSelectBase
{
    public GUIHeroUpgradeMain(GUIManager mgr)
        : base(mgr, GUI_DEFINE.GUIID_UPGRADE, GUILAYER.GUI_PANEL)
    {
    }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        this.m_eLoadingState = LOADING_STATE.START;
        GUI_FUNCTION.AYSNCLOADING_SHOW();
        ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, BTN_NULL);
        if (this.m_cGUIObject == null)
        {
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_MAIN);
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_HEROITEM);
        }
    }

    protected override void InitGUI()
    {

        //刷新数据//
        this.m_lstHero = Role.role.GetHeroProperty().GetAllHero();

        if (this.m_cGUIObject == null)
        {
            base.InitGUI();
            this.m_cUITittle.text = "强化合成";

            GUIComponentEvent ce = this.m_cCancelBtn.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnCancel);

        }
        else
        {
            base.InitGUI();
        }
        GUIHeroUpgrade gui_upgradeHero = m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_UPGRADEHERO) as GUIHeroUpgrade;

        if ((gui_upgradeHero != null) && gui_upgradeHero.m_bIsClear)
        {

            if (gui_upgradeHero.m_lstSelectID.Count != null)
            {
                foreach (GfxObject gfx in gui_upgradeHero.m_gfxObjHeros)
                {
                    gfx.Destory();
                }
                gui_upgradeHero.m_lstSelectID.Clear();
            }
        }

        foreach (HeroShowItem item in this.m_lstHeroShow)
        {
            if (item.m_cHero.m_iTableID >= 171 && item.m_cHero.m_iTableID <= 202)
            {
                item.m_cItemCover.enabled = true;
            }
            else
            {
                GUIComponentEvent ce = item.m_cItem.AddComponent<GUIComponentEvent>();
                ce.AddIntputDelegate(OnHero, item.m_cHero.m_iID);
            }
        }
        this.m_cGUIMgr.SetCurGUIID(this.ID);

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_STRENGTHEN_HERO));

        GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_HERO_UP2);
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        ResourcesManager.GetInstance().UnloadUnusedResources();

        //SetLocalPos(Vector3.one * 0xFFFF);
        base.Hiden();

        CameraManager.GetInstance().HidenUIModelCamera();
        Destory();
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

        if (this.IsShow())
        {
            if (m_bHasDrag)  //如果是拖动
            {
                return true;
            }
            if (this.m_bHasPress)  //如果是按下,开始计时
            {
                float dis = GAME_TIME.TIME_FIXED() - m_fDis;
                if (dis >= PRESS_TIME)
                {
                    m_bBeLongPress = true;

                    int heroid = this.m_iSelectHeroIndex;

                    GUIHeroDetail herodetail = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERODETAIL) as GUIHeroDetail;

            
                    herodetail.m_cLocalposition = this.m_cClipPanel.transform.localPosition;
                    herodetail.m_cClipRange = this.m_cClipPanel.clipRange;

                    this.Hiden();
                    herodetail.Show(ShowNotChange, Role.role.GetHeroProperty().GetHero(heroid));
         
                }
            }
        }
        return base.Update();
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
            Hiden();

            GUIHeroUpgrade upgradeHero = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_UPGRADEHERO) as GUIHeroUpgrade;
            if (upgradeHero.m_lstSelectID != null && upgradeHero.m_lstSelectID.Count != 0)
                upgradeHero.m_lstSelectID.Clear();
            
            this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERO_MENU).Show();
        }
    }

    /// <summary>
    /// 点击英雄
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnHero(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.PRESS)
        {
            if (!this.m_bHasPress)
            {
                this.m_bHasPress = true;
                //开始长按钮计时
                this.m_fDis = GAME_TIME.TIME_FIXED();
                this.m_iSelectHeroIndex = (int)args[0];
            }
            else
            {
                //弹起
                this.m_bHasPress = false;
                this.m_bHasDrag = false;
            }
        }
        else if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.DRAG)
        {
            this.m_bHasDrag = true;
        }
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (m_bBeLongPress)
            {
                return;
            }

            int heroID = (int)args[0];

            //Hero q = Role.role.GetHeroProperty().GetHero(heroID);
            //if (q.m_iMaxLevel == q.m_iLevel && q.m_iBBSkillLevel == 10)
            //{
            //    GUI_FUNCTION.MESSAGEM(null, "英雄等级和英雄技能等级都已满级！");
            //    return;
            //}

            Hiden();

            GUIHeroUpgrade upgradeHero = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_UPGRADEHERO) as GUIHeroUpgrade;
            upgradeHero.SetUpgradeData(heroID);
            if (upgradeHero.m_lstSelectID != null)
            {
                if (upgradeHero.m_lstSelectID.Contains(heroID))
                {
                    upgradeHero.m_lstSelectID.Remove(heroID);
                }
            }

            upgradeHero.Show();
        }
    }
}