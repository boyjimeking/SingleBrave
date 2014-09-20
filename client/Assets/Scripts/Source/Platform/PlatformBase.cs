using UnityEngine;
using System;

//  PlatformBase.cs
//  Author: Lu Zexi
//  2014-01-22

/// <summary>
/// 平台基类
/// </summary>
public abstract class PlatformBase
{
    /// <summary>
    /// 初始化
    /// </summary>
    public virtual void Init()
    { 
        
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    /// <returns></returns>
    public virtual bool Update()
    {
        return true;
    }

    /// <summary>
    /// 获取设备号
    /// </summary>
    /// <returns></returns>
    public virtual string GetDeviceID()
	{
		string guid = PlayerPrefs.GetString("DEVICE_ID");
		if(guid == "")
		{
			guid = Guid.NewGuid().ToString();
			PlayerPrefs.SetString("DEVICE_ID",guid);
		}
		return guid;
	}

    /// <summary>
    /// 获取渠道号
    /// </summary>
    /// <returns></returns>
    public abstract string GetChannelName();

    /// <summary>
    /// 暂停事件
    /// </summary>
    /// <param name="pause"></param>
    public virtual void OnApplicationPause(bool pause)
    { 
        //
    }

    /// <summary>
    /// 打开URL
    /// </summary>
    /// <param name="path"></param>
    public virtual void OpenNotice(string path, int left, int top, int right, int bottom)
    { 
        //
    }

    /// <summary>
    /// 关闭url
    /// </summary>
    public virtual void CloseNotice()
    { 
        //
    }

    /// <summary>
    /// 更新版本
    /// </summary>
    /// <param name="path"></param>
    public virtual void UpdateVersion(string path)
    { 
        //
    }

    /// <summary>
    /// 登录
    /// </summary>
    public virtual void Login()
    { 
        //
    }

    /// <summary>
    /// 展示登录或者自动登录
    /// </summary>
    public virtual void ShowLogin()
    {
        //
    }

    /// <summary>
    /// 玩家登出
    /// </summary>
    public virtual void ShowLogout()
    {
        //
    }
    /// <summary>
    /// 展示平台社区
    /// </summary>
    public virtual void ShowCommunity()
    {
        //
    }
	
	 /// <summary>
    /// 隐藏平台社区
    /// </summary>
    public virtual void HidenCommunity()
    {
        //
    }

    /// <summary>
    /// 展示平台论坛
    /// </summary>
    public virtual void ShowForum()
    {
        //
    }

    /// <summary>
    /// 平台的问题反馈
    /// </summary>
    public virtual void ShowFeedBack()
    {
        //
    }

    /// <summary>
    /// 程序退出
    /// </summary>
    public virtual void AppExit()
    {
        //
    }

    /// <summary>
    /// 程序暂停
    /// </summary>
    public virtual void AppPause()
    {
        //
    }

    /// <summary>
    /// 展示付款
    /// </summary>
    public virtual void ShowPayment(object[] arg)
    {
        //
    }

    
    /// <summary>
    /// 登录回调
    /// </summary>
    /// <param name="arg"></param>
    public virtual void OnLoginCallBack(string arg)
    {
        //
    }

    /// <summary>
    /// 登录取消回调
    /// </summary>
    /// <param name="arg"></param>
    public virtual void OnLoginCancelCallBack(string arg)
    {
        //
    }


    /// <summary>
    /// 切换帐号回调
    /// </summary>
    /// <param name="arg"></param>
    public virtual void SwitchAccountCallBack(string arg)
    { 
    }

    /// <summary>
    /// 成功登出成功回调
    /// </summary>
    public virtual void OnLogoutSuccessCallBack()
    {
        //
    }

    /// <summary>
    /// 登出失败回调
    /// </summary>
    public virtual void OnLogoutFailCallBack()
    {
        //
    }

    /// <summary>
    /// 支付成功回调
    /// </summary>
    /// <param name="arg"></param>
    public virtual void OnPaymentSuccessCallBack(string arg)
    {
        //
    }

    /// <summary>
    /// 支付失败回调
    /// </summary>
    /// <param name="arg"></param>
    public virtual void OnPaymentFailCallBack(string arg)
    {
        //
    }

}
