using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Resource;
using UnityEngine;

//战斗菜单-放弃战斗
//Author sunyi
//2014-3-26
public class GUIBattleMenuGiveUp : GUIBase
{
    private const string RES_MAIN = "GUI_BattleMenuGiveUp";

    private const string BTN_OK = "GiveUp/Btn_Ok";//放弃战斗界面确定按钮地址
    private const string BTN_CANCEL = "GiveUp/Btn_Cancel";//放弃战斗界面取消按钮地址

    private GameObject m_cBtnOk;//放弃战斗界面确定按钮
    private GameObject m_cBtnCancel;//放弃战斗界面取消按钮

    public GUIBattleMenuGiveUp(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_BATTLE_MENU_GIVE_UP, GUILAYER.GUI_PANEL3)
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

            this.m_cBtnOk = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_OK);
            GUIComponentEvent giveUpOkEvent = this.m_cBtnOk.AddComponent<GUIComponentEvent>();
            giveUpOkEvent.AddIntputDelegate(OnClickGiveUpOkButton);

            this.m_cBtnCancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_CANCEL);
            GUIComponentEvent giveUpCancelEvent = this.m_cBtnCancel.AddComponent<GUIComponentEvent>();
            giveUpCancelEvent.AddIntputDelegate(OnClickGiveUpCancelButton);
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

        this.m_cBtnOk = null;
        this.m_cBtnCancel = null;

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

    private void OnClickGiveUpCancelButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            GUIBattleMenu menu = (GUIBattleMenu)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BATTLEMENU);
            menu.OnClickGiveUpCancelButton();

            Hiden();
        }
    }

    private void OnClickGiveUpOkButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            GUIBattleMenu menu = (GUIBattleMenu)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BATTLEMENU);
            menu.OnClickGiveUpOkButton();

            Hiden();
        }
    }
}

