using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Network;

//  PVPWeekRankGetPktReq.cs
//  Author: sanvey
//  2014-2-8

/// <summary>
/// 竞技场基本信息获取
/// </summary>
public class PVPWeekRankGetPktReq : HTTPPacketRequest
{
    public int m_iPid;  //Pid

    public PVPWeekRankGetPktReq()
    {
        this.m_strAction = PACKET_DEFINE.PVP_WEEK_RANK_GET_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 发送周排行获取接口请求
	/// </summary>
	/// <param name="pid"></param>
	public static void SendPVPWeekRankReq(int pid)
	{
		PVPWeekRankGetPktReq req = new PVPWeekRankGetPktReq();
		req.m_iPid = pid;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}

