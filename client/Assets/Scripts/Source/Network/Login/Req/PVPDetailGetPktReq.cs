using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Network;

//  PVPDetailGetPktReq.cs
//  Author: sanvey
//  2014-2-8

/// <summary>
/// 竞技场详细信息获取
/// </summary>
public class PVPDetailGetPktReq : HTTPPacketRequest
{
    public int m_iPid;  //Pid

    public PVPDetailGetPktReq()
    {
        this.m_strAction = PACKET_DEFINE.PVP_DETAIL_GET_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 发送竞技场详细信息请求
	/// </summary>
	/// <param name="pid"></param>
	public static void SendPVPDetailGetReq(int pid)
	{
		PVPDetailGetPktReq req = new PVPDetailGetPktReq();
		req.m_iPid = pid;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}