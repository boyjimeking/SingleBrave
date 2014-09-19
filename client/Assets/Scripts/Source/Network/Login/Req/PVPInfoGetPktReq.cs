using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Network;

//  PVPInfoGetPktReq.cs
//  Author: sanvey
//  2014-2-8

/// <summary>
/// 竞技场基本信息获取
/// </summary>
public class PVPInfoGetPktReq : HTTPPacketRequest
{
    public int m_iPid;  //Pid

    public PVPInfoGetPktReq()
    {
        this.m_strAction = PACKET_DEFINE.PVP_INFO_GET_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 发送竞技场基本信息请求
	/// </summary>
	/// <param name="pid"></param>
	public static void SendPVPInfoGetReq(int pid)
	{
		PVPInfoGetPktReq req = new PVPInfoGetPktReq();
		req.m_iPid = pid;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}
