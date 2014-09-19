//  FriendDelPktReq.cs
//  Author: Cheng Xia
//  2013-1-15

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

/// <summary>
/// 好友删除协议
/// </summary>
public class FriendDelPktReq : HTTPPacketRequest
{
    public int m_iPID;  //玩家ID
    public int m_iFriendPID;

    public FriendDelPktReq()
    {
        this.m_strAction = PACKET_DEFINE.FRIEND_DEL_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	//好友删除//
	public static void SendFriendDel(int pid, int fPid)
	{
		FriendDelPktReq req = new FriendDelPktReq();
		req.m_iPID = pid;
		req.m_iFriendPID = fPid;
		
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}
}
