using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Game.Resource;

//  GUIFestaInput.cs
//  Author: Sanvey
//  2013-12-13

/// <summary>
/// 创建角色GUI
/// </summary>
public class GUIFestaInput : GUIBase
{
    private const string RES_MAIN = "GUI_FestaInput";

    private const string ROLE_NAME_INPUT = "Input";  //角色名输入框
    private const string GUI_SURE_BTN = "Btn_Ok"; //确定按钮
    private const string GUI_SKIP_BTN = "Btn_Skip"; //跳过按钮

    private UIInput m_cInput;   //输入狂
    private GameObject m_cSureBtn;  //确定按钮
    private GameObject m_cSkipBtn; //跳过按钮
    private string m_strName;   //角色名字

    public GUIFestaInput(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_FESTA_INPUT, GUILAYER.GUI_PANEL)
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
    protected override void  InitGUI()
    {
        //ResourceMgr.ClearProgress();
        GUI_FUNCTION.AYSNCLOADING_HIDEN();
        base.Show();
        if (this.m_cGUIObject == null)
        {
            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GUI_FINDATION.FIND_GAME_OBJECT(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            this.m_cInput = GUI_FINDATION.GET_OBJ_COMPONENT<UIInput>(this.m_cGUIObject, ROLE_NAME_INPUT);
            GUIComponentEvent ce = this.m_cInput.gameObject.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnName);

            this.m_cSureBtn = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_SURE_BTN);
            ce = this.m_cSureBtn.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnSureBtn);

            this.m_cSkipBtn = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_SKIP_BTN);
            ce = this.m_cSkipBtn.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnSkipBtn);
        }

        //进入过就不再展示了，无论是否输入或跳过
        GAME_SETTING.SaveFirstCodeShow(true);
        this.m_cInput.value = "";
        SetLocalPos(Vector3.one);
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    /// <returns></returns>
    public override bool Update()
    {
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
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        ResourceMgr.UnloadResource(RES_MAIN);
        //SetLocalPos(Vector3.one * 0xFFFF);
        Destory();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        this.m_cInput = null;
        this.m_cSureBtn = null;
        this.m_cSkipBtn = null;

        base.Destory();
    }

    /// <summary>
    /// 输入
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnName(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.INPUT)
        {
            //
        }
    }

    /// <summary>
    /// 确定按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnSureBtn(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (string.IsNullOrEmpty(this.m_cInput.value))
            {
                GUI_FUNCTION.MESSAGEM(null, "请输入邀请码");
                return;
            }

            //成功以后切换场景，进入游戏
            GuestZhaoDaiHandle.CallBack = (() =>
            {
                this.Hiden();
				CScene.Switch<GameScene>();
            });
            SendAgent.SendGuestZhaoDaiReq(Role.role.GetBaseProperty().m_iPlayerId, this.m_cInput.value);
        }
    }

    /// <summary>
    /// 跳过按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnSkipBtn(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.Hiden();

			CScene.Switch<GameScene>();
        }
    }
}