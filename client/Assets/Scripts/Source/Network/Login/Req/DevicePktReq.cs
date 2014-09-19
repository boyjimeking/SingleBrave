using System;
using System.Collections.Generic;
using Game.Network;

//  DevicePktReq.cs
//  Author: Lu Zexi
//  2014-04-24




/// <summary>
/// 设备号请求
/// </summary>
public class DevicePktReq : HTTPPacketRequest
{

    public DevicePktReq()
        : base()
    {
        this.m_strAction = PACKET_DEFINE.DEVICE_REQ;
    }
}



/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 获取唯一ID
	/// </summary>
	public static void SendGetDeviceID()
	{
		DevicePktReq req = new DevicePktReq();
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}
}

