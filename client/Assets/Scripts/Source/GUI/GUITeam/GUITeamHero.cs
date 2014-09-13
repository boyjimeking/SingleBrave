//  GUITeamHero.cs
//  Author: Cheng Xia
//  2013-12-19

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;

/// <summary>
/// 团队英雄选择界面
/// </summary>
public class GUITeamHero : GUIHeroSelectBase
{
    private const string TITLE = "英雄选择";
    private const string RES_BtnBack = "GUI_BtnNull";

    private HeroTeam m_cHeroTeam;
    private GameObject m_btnBack;

    public GUITeamHero(GUIManager mgr)
        : base(mgr, GUI_DEFINE.GUIID_TEAMHERO, GUILAYER.GUI_PANEL)
    {
    }
    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        //this.m_eLoadingState = LOADING_STATE.NONE;

        //if (this.m_cGUIObject == null)
        //{
            this.m_eLoadingState = LOADING_STATE.START;
            GUI_FUNCTION.AYSNCLOADING_SHOW();

            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_MAIN);
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_HEROITEM);
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_BtnBack);
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, BTN_NULL);
        //}
        //else
        //{
        //    InitGUI();
        //}
    }

    protected override void InitGUI()
    {
        //刷新数据//
        Reflash();

        GUITeamEditor teamEditor = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_TEAM_EDITOR) as GUITeamEditor;
        int selectedPos = teamEditor.m_iSelectHeroPos;

        if (Role.role.GetHeroProperty().GetHero(m_cHeroTeam.m_vecTeam[selectedPos]) != null)
        {
            //Debug.Log("111");
            this.m_iShowOffsetX = 1;
        }
        else
        {
            this.m_iShowOffsetX = 0;
        }

        if (this.m_cGUIObject == null)
        {
            base.InitGUI();

            this.m_cUITittle.text = TITLE;

            GUIComponentEvent ce = this.m_cCancelBtn.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnCancel);

            //Debug.Log("m_lstHeroShow_" + m_lstHeroShow.Count);

        }
        else
        {
            base.InitGUI();
        }

        if (m_btnBack != null)
        {
            GameObject.Destroy(m_btnBack);
        }

        int maxCost = RoleExpTableManager.GetInstance().GetMaxCost(Role.role.GetBaseProperty().m_iLevel);
        int remainCost = maxCost - m_cHeroTeam.GetCostValue();
        if (Role.role.GetHeroProperty().GetHero(m_cHeroTeam.m_vecTeam[selectedPos]) != null)
        {
            GameObject btnBack = ResourcesManager.GetInstance().Load(RES_BtnBack) as GameObject;
            m_btnBack = GameObject.Instantiate(btnBack) as GameObject;
            m_btnBack.transform.parent = this.m_cIconParent.transform;
            m_btnBack.transform.localPosition = Vector3.zero;
            m_btnBack.transform.localScale = Vector3.one;
            GUIComponentEvent btnCe = m_btnBack.GetComponentInChildren<Collider>().gameObject.AddComponent<GUIComponentEvent>();
            btnCe.AddIntputDelegate(OnBtnBack);

            Hero hero = Role.role.GetHeroProperty().GetHero(m_cHeroTeam.m_vecTeam[selectedPos]);
            remainCost = remainCost + hero.m_iCost;
        }

        foreach (HeroShowItem item in m_lstHeroShow)
        {
            if ((remainCost - item.m_cHero.m_iCost) >= 0)
            {
                GUIComponentEvent ce = item.m_cItem.AddComponent<GUIComponentEvent>();
                ce.AddIntputDelegate(OnHero, item.m_cHero.m_iID);
            }
            else
            {
                item.m_cItemCover.enabled = true;
            }
        }

        this.m_cGUIMgr.SetCurGUIID(this.ID);

        GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_HERO_EDITOR3);
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
                if (ResourcesManager.GetInstance().GetProgress() >= 1f && ResourcesManager.GetInstance().IsComplete())
                {
                    this.m_eLoadingState++;
                }
                return false;
            case LOADING_STATE.END:
                InitGUI();

                GUI_FUNCTION.AYSNCLOADING_HIDEN();
                this.m_eLoadingState++;
                break;
        }

        return base.Update();
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        ResourcesManager.GetInstance().UnloadUnusedResources();

        base.Hiden();
    }

    public override void Destory()
    {
        m_btnBack = null;

        base.Destory();
    }

    private void Reflash()
    {
        this.m_lstHero.Clear();

        //获取当前队伍
        m_cHeroTeam = Role.role.GetTeamProperty().GetTeam(Role.role.GetBaseProperty().m_iCurrentTeam);

        foreach (Hero hero in Role.role.GetHeroProperty().GetAllHero())
        {
            if (m_cHeroTeam.GetHeroIndex(hero) == -1)
            {
                this.m_lstHero.Add(hero);
            }
        }

        //Debug.Log(this.m_lstHero.Count);
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
            Hiden();
            GUITeamEditor teamEditor = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_TEAM_EDITOR) as GUITeamEditor;
            teamEditor.Show();
        }
    } 


    private void OnBtnBack(GUI_INPUT_INFO info, object[] args)
    {
        
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            GUITeamEditor teamEditor = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_TEAM_EDITOR) as GUITeamEditor;
            int selectedPos = teamEditor.m_iSelectHeroPos;

            if (selectedPos != m_cHeroTeam.GetLeaderIndex())
            {
                m_cHeroTeam.m_vecTeam[selectedPos] = -1;
            }
            teamEditor.Show();
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
            //Debug.Log("OnHero");

            int heroID = (int)args[0];
            Hiden();

            GUITeamEditor teamEditor = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_TEAM_EDITOR) as GUITeamEditor;
            int selectedPos = teamEditor.m_iSelectHeroPos;

            if (selectedPos == m_cHeroTeam.GetLeaderIndex())
            {
                m_cHeroTeam.m_iLeadID = heroID;
            }

            m_cHeroTeam.m_vecTeam[selectedPos] = heroID;

            

            teamEditor.Show();
        }
    }
}