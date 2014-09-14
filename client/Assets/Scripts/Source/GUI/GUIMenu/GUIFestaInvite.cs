using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using Game.Resource;

//特典-邀请好友
//Author:sunyi
//2013-11-27

public class GUIFestaInvite : GUIBase
{

    private const string RES_MAIN = "GUI_FestaInvite";//主资源地址

    private const string TOPPANEL = "TopPanel";//导航栏地址
    private const string MAINPANEL = "MainPanel";//主面板地址
    private const string BACKBUTTON = "TopPanel/Btn_Back";//返回按钮地址
    private const string BTN_CREATE = "AlertView/Btn_Send";  //生成按钮
    private const string INTPU_CODE = "AlertView/Input";  //输入框
    private const string BTN_USE = "Btn_Use";  //使用邀请码

    private GameObject m_cTopPanel;//导航栏
    private GameObject m_cMainPanel;//主面板
    private GameObject m_cBtnBack;//返回按钮地址
    private GameObject m_cBtnCreate;
    private UIInput m_cInput;
    private GameObject m_cBtnUse;

    public GUIFestaInvite(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_FESTAINVITE, GUILAYER.GUI_PANEL)
    { }

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

            this.m_cTopPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, TOPPANEL);
            this.m_cTopPanel.transform.localPosition = new Vector3(-420, 270, 0);

            this.m_cMainPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, MAINPANEL);
            this.m_cMainPanel.transform.localPosition = new Vector3(640, 0, 0);

            this.m_cBtnBack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BACKBUTTON);
            GUIComponentEvent backEvent = this.m_cBtnBack.AddComponent<GUIComponentEvent>();
            backEvent.AddIntputDelegate(OnClickBackButton);

            this.m_cBtnCreate = GUI_FINDATION.GET_GAME_OBJECT(this.m_cMainPanel, BTN_CREATE);
            GUIComponentEvent create = this.m_cBtnCreate.AddComponent<GUIComponentEvent>();
            create.AddIntputDelegate(SendCode_OnEvent);

            this.m_cBtnUse = GUI_FINDATION.GET_GAME_OBJECT(this.m_cMainPanel, BTN_USE);
            GUIComponentEvent use = this.m_cBtnUse.AddComponent<GUIComponentEvent>();
            use.AddIntputDelegate(UseCode_OnEvent);

            this.m_cInput = GUI_FINDATION.GET_OBJ_COMPONENT<UIInput>(this.m_cMainPanel, INTPU_CODE);
        }

        this.m_cGUIMgr.SetCurGUIID(this.m_iID);
        SetLocalPos(Vector3.zero);

        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-420, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //base.Hiden();

        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        CTween.TweenPosition(this.m_cTopPanel,GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-420, 270, 0));
        CTween.TweenPosition(this.m_cMainPanel,GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0) , Destory);

        ResourceMgr.UnloadUnusedResources();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        this.m_cTopPanel = null;
        this.m_cMainPanel = null;
        this.m_cBtnBack = null;
        this.m_cBtnCreate = null;
        this.m_cInput = null;
        this.m_cBtnUse = null;

        base.Destory();
    }

    /// <summary>
    /// 返回按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickBackButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            GUIMenu menu = (GUIMenu)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_MENU);
            menu.Show();
        }
    }

    /// <summary>
    /// 切换到生成邀请码界面
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void UseCode_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.Hiden();

            this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FESTA).Show();
        }
    }

    /// <summary>
    /// 输入邀请码
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void SendCode_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (string.IsNullOrEmpty(this.m_cInput.value))
            {
                GUI_FUNCTION.MESSAGEM(null, "请输入邀请码");
                return;
            }

            if (this.m_cInput.value == Role.role.GetBaseProperty().m_strZhaoDaiId)
            {
                GUI_FUNCTION.MESSAGEM(null, "不能使用自己的邀请码");
                return;
            }

            //成功以后回调
            GuestZhaoDaiHandle.CallBack = (() =>
            {
                GUI_FUNCTION.MESSAGEM(null, "邀请码输入成功");
            });
            SendAgent.SendGuestZhaoDaiReq(Role.role.GetBaseProperty().m_iPlayerId, this.m_cInput.value);
        }
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

}

