//  GUIEvolution.cs
//  Author: Cheng Xia
//  2013-12-30

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;
using Game.Gfx;
using Game.Base;

/// <summary>
/// 进化英雄选择界面
/// </summary>
public class GUIHeroEvolutionMain : GUIHeroSelectBase
{
    public GUIHeroEvolutionMain(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_EVOLUTION, GUILAYER.GUI_PANEL)
    {}

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

    protected override void InitGUI()
    {

        GUI_FUNCTION.AYSNCLOADING_HIDEN();

        this.m_lstHero = Role.role.GetHeroProperty().GetAllHero();

        if (this.m_cGUIObject == null)
        {
            base.InitGUI();

            this.m_cUITittle.text = "进化合成";

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

            if (item.m_cHero.m_iEvolutionID == 0)
            {
                item.m_cItemCover.enabled = true;
            }
        }

        this.m_cGUIMgr.SetCurGUIID(this.ID);

        //记录已经提示过得可进化英雄ID
        bool flag = false;
        List<Hero> list = Role.role.GetHeroProperty().GetAllHero();
        foreach (Hero hero in list)
        {
            if (hero.m_iEvolutionID != 0)
            {
                float spentGold;
                if (GAME_DEFINE.m_vecEvolutionHeroID.Contains(hero.m_iTableID))
                {
                    spentGold = GAME_DEFINE.m_iEvolutionSpent[hero.m_iStarLevel - 1];
                }
                else
                {
                    spentGold = GAME_DEFINE.m_iOtherEvolutionSpent[hero.m_iStarLevel - 1];
                }

                bool canEvo = true;  //素材是否足够
                for (int i = 0; i < hero.m_vecEvolution.Length; i++)
                {
                    if (hero.m_vecEvolution[i] != 0)
                    {
                        int deleIndex = list.FindIndex(q => { return q.m_iTableID == hero.m_vecEvolution[i]; });
                        if (deleIndex < 0)
                        {
                            canEvo = false;
                        }
                    }
                }
                //判断进化条件是否全部满足
                if (spentGold <= Role.role.GetBaseProperty().m_iGold && canEvo && hero.m_iLevel == hero.m_iMaxLevel)
                {
                    if (GAME_SETTING.s_dicWarnHeroJinhua[hero.m_iTableID] == 1)
                    {
                        GAME_SETTING.s_dicWarnHeroJinhua[hero.m_iTableID] = 0;
                        flag = true;
                    }
                }
            }
        }
        if(flag)
            GAME_SETTING.SaveWarnHeroJinhua();

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_SELECT_HERO));
        gui.HeroWarn();
    }

    public override void Hiden()
    {
        ResourceMgr.UnloadUnusedResources();

        //SetLocalPos(Vector3.one * 0xFFFF);

        base.Hiden();

        CameraManager.GetInstance().HidenUIModelCamera();
        Destory();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
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

                    herodetail.Show(this.ShowNotChange, Role.role.GetHeroProperty().GetHero(heroid));
               
                }
            }
        }
        return base.Update();
    }

    /// <summary>
    /// 进入英雄图鉴时回调方法
    /// </summary>
    private void ShowNotChange()
    {
        GUIHeroDetail herodetail = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERODETAIL) as GUIHeroDetail;
        this.m_cPanelClipRange = herodetail.m_cClipRange;
        this.m_cPanelLocalposition = herodetail.m_cLocalposition;

        Show();
    }

    /// <summary>
    /// 返回点击事件
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

            Hero tmp = Role.role.GetHeroProperty().GetHero(heroID);
            if (tmp!=null)
            {
                if (tmp.m_iEvolutionID == 0) return;
            }
         
            Hiden();

            GUIHeroEvolution evolutionHero = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_EVOLUTIONHERO) as GUIHeroEvolution;
            evolutionHero.m_selectID = heroID;
            evolutionHero.m_selectTableID = Role.role.GetHeroProperty().GetHero(heroID).m_iTableID;
            evolutionHero.Show();
        }
    }
}
