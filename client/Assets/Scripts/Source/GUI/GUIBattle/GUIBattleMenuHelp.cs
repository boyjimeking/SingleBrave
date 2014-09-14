using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Resource;
using UnityEngine;

//战斗菜单-帮助
//Author sunyi
//2014-3-26
public class GUIBattleMenuHelp : GUIBase
{
    private const string RES_MAIN = "GUI_BattleMenuHelp";//主资源地址

    private const string HELP_PANEL = "BattleHelp";//帮助面板地址

    private GameObject m_cHelpPanel;//帮助面板

    public GUIBattleMenuHelp(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_BATTLE_MENU_HELP, GUILAYER.GUI_PANEL3)
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
			this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            this.m_cHelpPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, HELP_PANEL);
            GUIComponentEvent hidenHelpEvent = this.m_cHelpPanel.AddComponent<GUIComponentEvent>();
            hidenHelpEvent.AddIntputDelegate(HidenBattleHelpGui);
        }

        SetLocalPos(Vector3.zero);
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        ResourceMgr.UnloadResource(RES_MAIN);

        Destory();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        this.m_cHelpPanel = null;

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
    /// <summary>
    /// 隐藏帮助面板
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void HidenBattleHelpGui(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            GUIBattleMenu menu = (GUIBattleMenu)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BATTLEMENU);
            menu.HidenBattleHelpGui();

            Hiden();
        }
    }
}

