using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kogarasi.WebView;
using UnityEngine;

//iOS平台类
//Author Sunyi
//2014-1-23
public class PlatformForIOS : PlatformBase
{
    protected WebViewIOS m_cWebView;  //WEB视窗
    protected IOSPay m_cIOSPay;   //支付类

    public PlatformForIOS()
    {
        this.m_cWebView = new WebViewIOS();
        this.m_cIOSPay = new IOSPay();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public override void Init()
    {
#if IOS && !UNITY_EDITOR
        m_cWebView.Init("MAIN");
        this.m_cIOSPay.Init();
#endif
    }

    /// <summary>
    /// 更新版本
    /// </summary>
    /// <param name="path"></param>
    public override void UpdateVersion(string path)
    {
#if IOS && !UNITY_EDITOR
        this.m_cIOSPay.UpdateVersion(path);
#endif
        //Application.OpenURL(path);
    }

    /// <summary>
    /// 获取设备号
    /// </summary>
    /// <returns></returns>
    public override string GetDeviceID()
    {
        return GAME_SETTING.DEVICE_ID;
    }

    /// <summary>
    /// 获取渠道号
    /// </summary>
    /// <returns></returns>
    public override string GetChannelName()
    {
        return "AppStore";
    }

    /// <summary>
    /// 暂停与恢复事件
    /// </summary>
    /// <param name="pause"></param>
    public override void OnApplicationPause(bool pause)
    {
        //发送数据
        if (!pause)
        {
            GUIBase genGui = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_GEM);
            GUIBase guiLoading = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_LOADING);
            GUIBase aysncLoading = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_AYSNC_LOADING);
            if( Role.role.GetPayProperty().m_iState == 0 &&
                genGui != null && !genGui.IsShow() && //商城界面
                guiLoading != null && !guiLoading.IsShow() && //加载界面
                aysncLoading != null && !aysncLoading.IsShow() &&   //加载界面
                Role.role.GetBaseProperty().m_iPlayerId > 0  &&
                CScene.Is(typeof(GameScene)))
                SendAgent.SendSystemPush( Role.role.GetBaseProperty().m_iPlayerId);
        }
    }

    /// <summary>
    /// 展示平台社区（公告）
    /// </summary>
    public override void OpenNotice( string path , int left , int top , int right , int bottom )
    {
#if UNITY_IPHONE && !UNITY_EDITOR
        m_cWebView.LoadURL(path);
        m_cWebView.SetVisibility(true);
        m_cWebView.SetMargins(left, top, right, bottom);
#endif
    }
	
    /// <summary>
    /// 隐藏平台社区（公告）
    /// </summary>
    public override void CloseNotice()
    {
#if UNITY_IPHONE && !UNITY_EDITOR
        m_cWebView.SetVisibility(false);
#endif
    }

    /// <summary>
    /// 登录
    /// </summary>
    public override void Login()
    {
        if (string.IsNullOrEmpty(GAME_SETTING.s_strUserName) || string.IsNullOrEmpty(GAME_SETTING.s_strPassWord))
        {
            //自动注册
            SendAgent.SendAccountAutoRegistReq();
        }
        else
        {
            GUIAccount gui = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_ACCOUNT) as GUIAccount;
            gui.m_strUser = GAME_SETTING.s_strUserName;
            gui.m_strPassword = GAME_SETTING.s_strPassWord;

            //登录
            SendAgent.SendAccountLogin(GAME_SETTING.s_strUserName, GAME_SETTING.s_strPassWord);
        }
    }

    /// <summary>
    /// 展示登录
    /// </summary>
    public override void ShowLogin()
    {
        GUIAccount guiAccount = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_ACCOUNT) as GUIAccount;
        guiAccount.ShowImmediately();
    }

    /// <summary>
    /// 展示支付
    /// </summary>
    /// <param name="arg"></param>
    public override void ShowPayment( object[] arg)
    {
        GUI_FUNCTION.LOADING_SHOW();

        string type_id = arg[3] as string;
        Debug.Log("Pay id " + type_id);
        int res = 0;
#if IOS && !UNITY_EDITOR
        res =  this.m_cIOSPay.Pay(type_id);
#endif
        Debug.Log(res + " pay res.");
        if (res != 100)
        {
            Role.role.GetPayProperty().m_iState = 0;
            GUI_FUNCTION.LOADING_HIDEN();
            if(res == 101)
                GUI_FUNCTION.MESSAGEL(null, "该设备未开启应用内支付");
            else
                GUI_FUNCTION.MESSAGEL(null, "支付失败");
            return;
        }
    }

    /// <summary>
    /// 支付成功
    /// </summary>
    /// <param name="arg"></param>
    public override void OnPaymentSuccessCallBack(string arg)
    {
        GUI_FUNCTION.LOADING_HIDEN();
        Role.role.GetPayProperty().m_iState = 0;
        string verify = arg;
        Debug.Log(verify + " verify");
        Role.role.GetPayProperty().m_strVerify = verify;
        SendAgent.SendPayIOSVerify(Role.role.GetBaseProperty().m_iPlayerId, Role.role.GetPayProperty().m_iPayID, verify);
    }

    /// <summary>
    /// 支付失败
    /// </summary>
    /// <param name="arg"></param>
    public override void OnPaymentFailCallBack(string arg)
    {
        GUI_FUNCTION.LOADING_HIDEN();
        GUI_FUNCTION.MESSAGEM(null, "支付失败!请重新支付.");
    }
}

