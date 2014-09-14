using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


//  PlatformAndroid.cs
//  Author: Lu Zexi
//  2014-02-12




/// <summary>
/// 安卓平台
/// </summary>
public class PlatformAndroid : PlatformBase
{
    /// <summary>
    /// 初始化
    /// </summary>
    public override void Init()
    {
        base.Init();
    }

    /// <summary>
    /// 获取设备号
    /// </summary>
    /// <returns></returns>
    public override string GetDeviceID()
    {
        return "";
    }

    /// <summary>
    /// 获取渠道号
    /// </summary>
    /// <returns></returns>
    public override string GetChannelName()
    {
        return "";
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
            if (Role.role.GetBaseProperty().m_iPlayerId > 0 && CScene.Is(typeof(GameScene)))
                SendAgent.SendSystemPush(Role.role.GetBaseProperty().m_iPlayerId);
        }
    }

    /// <summary>
    /// 打开URL
    /// </summary>
    /// <param name="path"></param>
    public override void OpenNotice(string path, int left, int top, int right, int bottom)
    {
#if UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("OpenWEBView",path , left , top , right , bottom);
#endif
    }

    /// <summary>
    /// 关闭url
    /// </summary>
    public override void CloseNotice()
    {
#if UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("CloseWEBView");
#endif
    }

    /// <summary>
    /// 更新版本
    /// </summary>
    /// <param name="path"></param>
    public override void UpdateVersion(string path)
    {
#if UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("UpdateVersion",path);
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

//    /// <summary>
//    /// 展示登录或者自动登录
//    /// </summary>
//    public override void ShowLogin()
//    {
//#if UNITY_ANDROID

//        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
//        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
//        jo.Call("Login");
//#endif
//    }

    /// <summary>
    /// 玩家登出
    /// </summary>
    public override void ShowLogout()
    {
#if UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("Logout");
#endif
    }
    /// <summary>
    /// 展示平台社区
    /// </summary>
    public override void ShowCommunity()
    {
#if UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("OpenMemberCenter");
#endif
    }

    /// <summary>
    /// 隐藏平台社区
    /// </summary>
    public override void HidenCommunity()
    {
#if UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("OpenMemberCenter");
#endif
    }

    /// <summary>
    /// 展示平台论坛
    /// </summary>
    public override void ShowForum()
    {
#if UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("Forum");
#endif
    }

    /// <summary>
    /// 平台的问题反馈
    /// </summary>
    public override void ShowFeedBack()
    {
#if UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("FeedBack");
#endif
    }

    /// <summary>
    /// 程序退出
    /// </summary>
    public override void AppExit()
    {
#if UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("AppExit");
#endif
    }

    /// <summary>
    /// 程序暂停
    /// </summary>
    public override void AppPause()
    {
#if UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("AppPause");
#endif
    }

    /// <summary>
    /// 展示付款
    /// </summary>
    public override void ShowPayment(object[] arg)
    {
#if UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("Payment",arg);
#endif
    }


    /// <summary>
    /// 登录回调
    /// </summary>
    /// <param name="arg"></param>
    public override void OnLoginCallBack(string arg)
    {
        //
    }

    /// <summary>
    /// 登录取消回调
    /// </summary>
    /// <param name="arg"></param>
    public override void OnLoginCancelCallBack(string arg)
    {
        //
    }


    /// <summary>
    /// 切换帐号回调
    /// </summary>
    /// <param name="arg"></param>
    public override void SwitchAccountCallBack(string arg)
    {
    }

    /// <summary>
    /// 成功登出成功回调
    /// </summary>
    public override void OnLogoutSuccessCallBack()
    {
        //
    }

    /// <summary>
    /// 登出失败回调
    /// </summary>
    public override void OnLogoutFailCallBack()
    {
        //
    }

    /// <summary>
    /// 支付成功回调
    /// </summary>
    /// <param name="arg"></param>
    public override void OnPaymentSuccessCallBack(string arg)
    {
        //
    }

    /// <summary>
    /// 支付失败回调
    /// </summary>
    /// <param name="arg"></param>
    public override void OnPaymentFailCallBack(string arg)
    {
        //
    }

}
