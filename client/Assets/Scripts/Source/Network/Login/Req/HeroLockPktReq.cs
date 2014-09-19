using UnityEngine;
using System.Collections;
using Game.Network;
using System.Collections.Generic;

//  HeroLockPktReq.cs
//  Author: sanvey
//  2013-12-19

/// <summary>
/// 英雄锁定请求
/// </summary>
public class HeroLockPktReq : HTTPPacketRequest
{
    public List<int> m_lstHeros;  //锁定英雄列表
    public int m_iPid;  //Pid

    public HeroLockPktReq()
    {
        this.m_strAction = PACKET_DEFINE.HERO_LOCK_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 发送英雄锁定数据请求
	/// </summary>
	/// <param name="pid"></param>
	/// <param name="heros"></param>
	public static void SendHeroLockReq(int pid, List<int> heros)
	{
		HeroLockPktReq req = new HeroLockPktReq();
		req.m_iPid = pid;
		req.m_lstHeros = heros;
		
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}