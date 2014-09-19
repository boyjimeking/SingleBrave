using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Network;

//  PVPBattleRankGetPktReq.cs
//  Author: sanvey
//  2014-2-8

/// <summary>
/// 竞技场排名信息获取
/// </summary>
public class PVPBattleRankGetPktReq : HTTPPacketRequest
{
    public int m_iPid;  //Pid

    public PVPBattleRankGetPktReq()
    {
        this.m_strAction = PACKET_DEFINE.PVP_BATTLE_RANK_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 获取竞技场排行信息请求
	/// </summary>
	/// <param name="pid"></param>
	public static void SendPVPBattleRankReq(int pid)
	{
		PVPBattleRankGetPktReq req = new PVPBattleRankGetPktReq();
		req.m_iPid = pid;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}

