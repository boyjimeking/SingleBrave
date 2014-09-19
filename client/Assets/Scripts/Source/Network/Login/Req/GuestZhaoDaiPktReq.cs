using UnityEngine;
using System.Collections;
using Game.Network;
using System.Collections.Generic;

//  GuestZhaoDaiPktReq.cs
//  Author: sanvey
//  2013-12-19

/// <summary>
/// 招待请求
/// </summary>
public class GuestZhaoDaiPktReq : HTTPPacketRequest
{
    public string m_strZhaoDaiId; //招待ID
    public int m_iPid;  //Pid

    public GuestZhaoDaiPktReq()
    {
        this.m_strAction = PACKET_DEFINE.GUEST_ZHAODAI_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 发送招待ID请求数据
	/// </summary>
	/// <param name="pid"></param>
	/// <param name="zhaodaiID"></param>
	public static void SendGuestZhaoDaiReq(int pid, string zhaodaiID)
	{
		GuestZhaoDaiPktReq req = new GuestZhaoDaiPktReq();
		req.m_iPid = pid;
		req.m_strZhaoDaiId = zhaodaiID;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}