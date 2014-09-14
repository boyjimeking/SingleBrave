using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Resource;
using Game.Media;

//  GUITittle.cs
//  Author: Lu Zexi
//  2013-11-13



/// <summary>
/// 标题GUI
/// </summary>
public class GUITittle : GUIBase
{
    private const string RES_MAIN = "_GUI_TITTLE";   //主资源地址
    private const string GUI_BUTTON = "Button"; //按钮地址
    private const string BUTTON_ACOUNT = "Btn_Account";//账号按钮地址
    private const string BUTTON_BACK = "Btn_Back";//返回按钮地址

    private GameObject m_cButton;   //按钮
    private GameObject m_cBtnAccount;//账号按钮
    private GameObject m_cBtnBack;//返回按钮

    private bool m_bIsShowAcountPanel;//是否显示账号面板

    public GUITittle( GUIManager mgr)
        : base(mgr, GUI_DEFINE.GUIID_TITTLE, GUILAYER.GUI_PANEL)
    {
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public override void Initialize()
    {
        base.Initialize();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Destory();
    }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        base.Show();
        if (this.m_cGUIObject == null)
        {
            UnityEngine.Object res_obj = Resources.Load(RES_MAIN);
            this.m_cGUIObject = GameObject.Instantiate(res_obj) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            this.m_cButton = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_BUTTON);
            GUIComponentEvent gui_event = this.m_cButton.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(OnButton);

            this.m_cBtnAccount = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_ACOUNT);
            GUIComponentEvent acountEvent = this.m_cBtnAccount.AddComponent<GUIComponentEvent>();
            acountEvent.AddIntputDelegate(OnClickButtonAcount);

            this.m_cBtnBack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_BACK);
            GUIComponentEvent backEvent = this.m_cBtnBack.AddComponent<GUIComponentEvent>();
            backEvent.AddIntputDelegate(OnClickButtonAcount);
            this.m_cBtnBack.SetActive(false);
            
        }

        this.m_bIsShowAcountPanel = false;

        AudioClip clip = Resources.Load(SOUND_DEFINE.BGM_MAIN) as AudioClip;
        MediaMgr.sInstance.PlayBGM(clip);

        SetLocalPos(Vector3.zero);
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        base.Hiden();

        
        //SetLocalPos(Vector3.one * 0xFFFF);
        Destory();
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    /// <returns></returns>
    public override bool Update()
    {
        return base.Update();
    }

    /// <summary>
    /// 按钮事件
    /// </summary>
    /// <param name="arg"></param>
    private void OnButton(GUI_INPUT_INFO info, object[] arg)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Debug.Log("in");
            MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_TITTLE_JOIN_IN);
            PlatformManager.GetInstance().Login();
            //SendAgent.SendVersionReq();
            //if (string.IsNullOrEmpty(GAME_SETTING.s_strUserName) || string.IsNullOrEmpty(GAME_SETTING.s_strPassWord))
            //{
            //    //自动注册
            //    SendAgent.SendAccountAutoRegistReq();
            //}
            //else
            //{
            //    //登录
            //    SendAgent.SendAccountLogin(GAME_SETTING.s_strUserName, GAME_SETTING.s_strPassWord);
            //}
        }
    }

    /// <summary>
    /// 显示账号登录面板
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickButtonAcount(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (!this.m_bIsShowAcountPanel)
            {
                //this.m_cBtnAccount.SetActive(false);
                //this.m_cBtnBack.SetActive(true);
                //Debug.Log("Show");
                this.m_bIsShowAcountPanel = true;
                PlatformManager.GetInstance().ShowPlatformLogin();
            }
            else {
                Debug.Log("Hiden");
                GUIAccount acount = (GUIAccount)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_ACCOUNT);
                acount.HidenImmediately();
                //this.m_bIsShowAcountPanel = false;
                //this.m_cBtnAccount.SetActive(true);
                //this.m_cBtnBack.SetActive(false);
                this.m_bIsShowAcountPanel = false;
                //PlatformManager.GetInstance().ShowPlatformLogin();
            }

            //GUIAccount guiAccount = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_ACCOUNT) as GUIAccount;

            //if (!guiAccount.IsShow())
            //{
            //    guiAccount.ShowImmediately();
            //}
            //else
            //{
            //    guiAccount.HidenImmediately();
            //}
        }
    }

    /// <summary>
    /// 设置按钮的显示
    /// </summary>
    public void SetButtons(bool ifShow)
    {
        if (ifShow)
        {
            if (this.m_cBtnAccount != null && this.m_cBtnBack != null)
            {
                this.m_bIsShowAcountPanel = true;
                this.m_cBtnAccount.SetActive(false);
                this.m_cBtnBack.SetActive(true);
            }

        }
        else {
            if (this.m_cBtnAccount != null && this.m_cBtnBack != null)
            {
                this.m_bIsShowAcountPanel = false;
                this.m_cBtnAccount.SetActive(true);
                this.m_cBtnBack.SetActive(false);
            }
        }
    }
}

