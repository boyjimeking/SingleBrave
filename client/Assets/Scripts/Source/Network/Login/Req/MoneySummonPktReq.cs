using UnityEngine;
using System.Collections;
using Game.Network;

//  MoneySummonPktReq.cs
//  Author: sanvey
//  2013-12-17

/// <summary>
/// 金钱召唤
/// </summary>
public class MoneySummonPktReq : HTTPPacketRequest
{
    public int m_iPID;  //用户Pid

    public MoneySummonPktReq()
    {
        this.m_strAction = PACKET_DEFINE.MONEY_SUMMON_REQ;
    }
}



/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 金钱点召唤数据请求
	/// </summary>
	/// <param name="pid"></param>
	public static void SendMoneySummonPktReq(int pid)
	{
		MoneySummonPktReq req = new MoneySummonPktReq();
		req.m_iPID = pid;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}
}
