using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kogarasi.WebView;
using UnityEngine;
using Game.Base;


//  PlatformPPIOS.cs
//  Author: Lu Zexi
//  2014-03-28



/// <summary>
/// PPIOS平台
/// </summary>
public class PlatformPPIOS : PlatformForIOS
{
    private int APP_ID = 3179;  //应用ID
    private string APP_KEY = "43a954670f5d26c2cc681b8d97c2ec1e";    //APP KEY
    private const int paramRechargeAmount = 10 ; //设置充值页面的默认金额
    private const bool paramIsNSlogData = false;   //设置是否控制台打印日志（仅iOS端）
	private const bool paramIsLongComet = false;   //设置游戏客户端与游戏服务端链接方式是否为长连接【如果游戏服务端能主动与游戏客户端交互。例如发放道具则为长连接。此处设置影响充值并兑换的方式】
	private const bool paramIsLogOutPushLoginView = false; //设置从PP中心注销后是否自动弹出登录页面
	private const bool paramIsOpenRecharge = true;    //设置是否开启充值PP币功能
	private const bool paramIsDeviceOrientationLandscapeLeft = false;  //设置游戏是否支持设备当前Home键盘在左方向
	private const bool paramIsDeviceOrientationLandscapeRight = false; //设置游戏是否支持设备当前Home键盘在右方向
    private const bool paramIsDeviceOrientationPortrait = true;  //设置游戏是否支持设备当前Home键盘在下方向
	private const bool paramIsDeviceOrientationPortraitUpsideDown = false;  //设置游戏是否支持设备当前Home键盘在上方向
    private const string paramSendMsgNotiClass = "IOSPPP"; //GameObject的名称,指定UnitySendMessage将通知发送到哪个对象

    private const int channel_id = 1;   //分区编号

    //不间断刷新数据
    private float m_iDis;
    private const float SEND_PER = 2f;  //每10s发送一次是否成功数据
    public PayState m_eState;

    /// <summary>
    /// 登陆状态
    /// </summary>
    public enum PayState
    {
        Nomal = 0,
        ReStart, //重新发送查询充值接口
        ClientSend ,  //客户端发送支付成功，不断发送等待
        Sucess,  //服务器返回验证结果
    }

    public PlatformPPIOS()
        :base()
    { 
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public override void Init()
    {
#if UNITY_IPHONE && !UNITY_EDITOR
        this.m_cWebView.Init("MAIN");
#endif
#if IOSPP && !UNITY_EDITOR
        Bonjour.initSDK(APP_ID, APP_KEY, paramIsNSlogData, paramRechargeAmount, paramIsLongComet, paramIsLogOutPushLoginView, paramIsOpenRecharge, "close recharge message", paramIsDeviceOrientationLandscapeLeft, paramIsDeviceOrientationLandscapeRight, paramIsDeviceOrientationPortrait, paramIsDeviceOrientationPortraitUpsideDown, paramSendMsgNotiClass);
#endif
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override bool Update()
    {
        switch (m_eState)
        {
            case PayState.Nomal:
                break;
            case PayState.ReStart:
                GUI_FUNCTION.LOADING_SHOW();
                this.m_iDis = GAME_TIME.TIME_FIXED();
                this.m_eState++;
                break;
            case PayState.ClientSend:
                float dis = GAME_TIME.TIME_FIXED() - m_iDis;
                if (dis >= SEND_PER)
                {
                    this.m_eState = PayState.Nomal;  //关闭重新发送，在发送handle中，接受到请求后再进入2s重新发送
                    SendAgent.SendPayIOSPPVerify(Role.role.GetBaseProperty().m_iPlayerId, Role.role.GetPayProperty().m_iPayID);
                }
                break;
            case PayState.Sucess:
                GUI_FUNCTION.LOADING_HIDEN();
                break;
            default:
                break;
        }
        //Role.role.GetPayProperty().m_iPayID;

        return base.Update();
    }

    /// <summary>
    /// 获取渠道名字
    /// </summary>
    /// <returns></returns>
    public override string GetChannelName()
    {
		return GAME_SETTING.CHANNEL_NAME;
    }

    /// <summary>
    /// 登录
    /// </summary>
    public override void Login()
    {
#if IOSPP && !UNITY_EDITOR
        Bonjour.showLoginView();
#endif
    }

    /// <summary>
    /// 展示帐号登录
    /// </summary>
    public override void ShowLogin()
    {
#if IOSPP && !UNITY_EDITOR
        Bonjour.showLoginView();
#endif
    }

    /// <summary>
    /// 登出
    /// </summary>
    public override void ShowLogout()
    {
#if IOSPP && !UNITY_EDITOR
       Bonjour.PPlogout();
       Debug.Log("PP LogOut");
#endif
    }

    /// <summary>
    /// 展示支付
    /// </summary>
    /// <param name="arg"></param>
    public override void ShowPayment(object[] arg)
    {
        int num = (int)(arg[2]);
        int price = (int)(arg[4]);
        int good_id = (int)(arg[1]);
        string pay_id = "" + (int)(arg[0]);
        string tittle = "" + num + "个钻石";
#if IOSPP && !UNITY_EDITOR
        Bonjour.exchangeGoods(price, pay_id, tittle, "" + Role.role.GetBaseProperty().m_iPlayerId, channel_id);
#endif
    }

    /// <summary>
    /// 显示PP用户中心
    /// </summary>
    public void ShowPlatformCenter()
    {
#if IOSPP && !UNITY_EDITOR
         Bonjour.showCenterView();
#endif
    }

    /// <summary>
    /// 支付成功
    /// </summary>
    /// <param name="arg"></param>
    public override void OnPaymentSuccessCallBack(string arg)
    {
        SendAgent.SendPayIOSPPVerify(Role.role.GetBaseProperty().m_iPlayerId, Role.role.GetPayProperty().m_iPayID);
    }

    /// <summary>
    /// 支付失败
    /// </summary>
    /// <param name="arg"></param>
    public override void OnPaymentFailCallBack(string arg)
    {
        GUI_FUNCTION.LOADING_HIDEN();
        GUI_FUNCTION.MESSAGEM(null, "支付失败");
        return;
    }

}
