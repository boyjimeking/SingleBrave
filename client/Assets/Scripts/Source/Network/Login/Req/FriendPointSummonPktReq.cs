using UnityEngine;
using System.Collections;
using Game.Network;

//  FriendPointSummonPktReq.cs
//  Author: sanvey
//  2013-12-17

/// <summary>
/// 友情点召唤
/// </summary>
public class FriendPointSummonPktReq : HTTPPacketRequest
{
    public int m_iPID;  //用户Pid

    public FriendPointSummonPktReq()
    {
        this.m_strAction = PACKET_DEFINE.FRIENDPOINT_SUMMON_REQ;
    }
}



/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	
	/// <summary>
	/// 友情点召唤数据请求
	/// </summary>
	/// <param name="pid"></param>
	public static void SendFriendPointSummonPktReq(int pid)
	{
		FriendPointSummonPktReq req = new FriendPointSummonPktReq();
		req.m_iPID = pid;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}