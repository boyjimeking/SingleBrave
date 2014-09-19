using UnityEngine;
using System.Collections;
using Game.Network;
using System.Collections.Generic;

//  HeroUnlockPktReq.cs
//  Author: sanvey
//  2013-12-19

/// <summary>
/// 英雄解锁请求
/// </summary>
public class HeroUnlockPktReq : HTTPPacketRequest
{
    public List<int> m_lstHeros;  //解锁英雄列表
    public int m_iPid;  //Pid

    public HeroUnlockPktReq()
    {
        this.m_strAction = PACKET_DEFINE.HERO_UNLOCK_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 发视英雄解锁数据请求
	/// </summary>
	/// <param name="pid"></param>
	/// <param name="heros"></param>
	public static void SendHeroUnlockReq(int pid, List<int> heros)
	{
		HeroUnlockPktReq req = new HeroUnlockPktReq();
		req.m_iPid = pid;
		req.m_lstHeros = heros;
		
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}