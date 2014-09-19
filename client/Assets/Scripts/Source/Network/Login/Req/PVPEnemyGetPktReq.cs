using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Network;

//  PVPEnemyGetPktReq.cs
//  Author: sanvey
//  2014-2-8

/// <summary>
/// 竞技场排名信息获取
/// </summary>
public class PVPEnemyGetPktReq : HTTPPacketRequest
{
    public int m_iPid;  //Pid

    public PVPEnemyGetPktReq()
    {
        this.m_strAction = PACKET_DEFINE.PVP_ENEMY_GET_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 发送竞技场对手获取请求
	/// </summary>
	/// <param name="pid"></param>
	public static void SendPVPEnemyGetReq(int pid)
	{
		PVPEnemyGetPktReq req = new PVPEnemyGetPktReq();
		req.m_iPid = pid;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}