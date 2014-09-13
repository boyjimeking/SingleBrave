using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine;
using Game.Resource;

//公告类
//Author Sunyi
//2014-1-17
public class GUIAnnouncement :GUIBase
{
    private const string RES_MAIN = "GUI_Announcement";//主资源地址
    private const string WEBVIEW = "WebView";//webview地址
	private const string BTN_CLOSE = "Btn_Close";//关闭按钮地址

    //private GameObject m_cWebView;//网络视图
    //private WebViewCallback m_callBack = new WebViewCallback();//回调函数
	private GameObject m_cBtnClose;//关闭按钮
    //private WebViewBehavior webView;

    public GUIAnnouncement(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_ANNOUNCEMENT, GUILAYER.GUI_MESSAGE1)
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
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_MAIN);
        }
        else
        {
            InitGUI();
        }
    }

    protected override void InitGUI()
    {
        base.Show();
        GUI_FUNCTION.AYSNCLOADING_HIDEN();
        if (this.m_cGUIObject == null)
        {
            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            this.m_cBtnClose = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_CLOSE);
            GUIComponentEvent closeEvent = this.m_cBtnClose.AddComponent<GUIComponentEvent>();
            closeEvent.AddIntputDelegate(OnClickCloseButton);

            //this.m_cWebView = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, WEBVIEW);
        }

        //float stSWidth = 640f;
        //float stSHeight = 960f;

        //int x = 5;
        //int y = 90;
        //int width = 630;
        //int height = 860;

        //float now_rate = Screen.width / Screen.height;
        //float default_rate = stSWidth / stSHeight;
        //float rate = now_rate / default_rate;

        //float sX = Screen.width / stSWidth;
        //float sY = Screen.height / stSHeight;

        //float resScale = 0;

        //if (sX > sY)
        //{
        //    resScale = sX / (Screen.height / stSHeight);
        //}
        //else
        //{
        //    resScale = sY / (Screen.width / stSWidth);
        //}

        //float nowWidth = stSWidth * resScale;
        //float nowHeight = stSHeight * resScale;

        //x = (int)(x * resScale + (Screen.width - nowWidth) * 0.5f);
        //y = (int)(y * resScale + (Screen.height - nowHeight)*0.5f);
        //width = (int)(width * resScale);
        //height = (int)(height * resScale);

        Rect rect = new Rect(5, 90, 630, 860);

        rect = CMath.PosToScreen(rect, 640, 960, Screen.width, Screen.height);

        PlatformManager.GetInstance().OpenNotice(GAME_SETTING.SESSION_LOGIN_PATH + GAME_DEFINE.NOTIC_URL, (int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height);

        SetLocalPos(Vector3.zero);
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {   
        base.Hiden();
        //SetLocalPos(Vector3.one * 0XFFFF);

        ResourcesManager.GetInstance().UnloadUnusedResources();

        Destory();

        if (this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_MAIN).IsShow())
        {
            GUILoginReward reward = (GUILoginReward)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_LOGINREWARD);
            reward.Show();
        }
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        this.m_cBtnClose = null;
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

        return base.Update();
    }

    /// <summary>
	/// Raises the click close button event.
	/// </summary>
	/// <param name='info'>
	/// 关闭网页视图
	/// </param>
	/// <param name='args'>
	/// Arguments.
	/// </param>
	private void OnClickCloseButton(GUI_INPUT_INFO info, object[] args)
	{
		if(info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
		{			
            //this.webView.SetVisibility(false);
			//PlatformManager.GetInstance().HidenPlatformCommunity();
            PlatformManager.GetInstance().CloseNotice();
			Hiden();
		}
	}
}
