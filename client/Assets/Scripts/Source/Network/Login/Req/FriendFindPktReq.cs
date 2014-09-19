//  FriendFindPktReq.cs
//  Author: Cheng Xia
//  2013-1-13

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

/// <summary>
/// 好友列表获取协议
/// </summary>
public class FriendFindPktReq : HTTPPacketRequest
{
    public int m_iPID;  //玩家ID
    public int m_iFriendPID;

    public FriendFindPktReq()
    {
        this.m_strAction = PACKET_DEFINE.FRIEND_FIND_REQ;
    }

}



/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	
	//好友查找//
	public static void SendFriendFind(int pid, int fPid)
	{
		FriendFindPktReq req = new FriendFindPktReq();
		req.m_iPID = pid;
		req.m_iFriendPID = fPid;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}

