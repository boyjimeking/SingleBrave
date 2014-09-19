using UnityEngine;
using System.Collections;
using Game.Network;

//  HeroSellPktReq.cs
//  Author: sanvey
//  2013-12-19

/// <summary>
/// 英雄出售请求
/// </summary>
public class HeroSellPktReq : HTTPPacketRequest
{
    public int[] m_vecHeros;  //出售英雄列表
    public int m_iPid;  //Pid

    public HeroSellPktReq()
    {
        this.m_strAction = PACKET_DEFINE.HERO_SELL_REQ;
    }
}



/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	
	/// <summary>
	/// 出售英雄数据请求
	/// </summary>
	/// <param name="pid">玩家ID</param>
	/// <param name="heroIds">要出售的英雄ID数组</param>
	public static void SendHeroSellPktReq(int pid, int[] heroIds)
	{
		HeroSellPktReq req = new HeroSellPktReq();
		req.m_iPid = pid;
		req.m_vecHeros = heroIds;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}
