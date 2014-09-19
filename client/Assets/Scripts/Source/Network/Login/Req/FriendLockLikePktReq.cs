//  FriendLockLikePktReq.cs
//  Author: Cheng Xia
//  2013-1-13

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

/// <summary>
/// 好友喜欢标识获取协议
/// </summary>
public class FriendLockLikePktReq : HTTPPacketRequest
{
    public int m_iPID;  //玩家ID
    public List<int> m_lstFriendPID;

    public FriendLockLikePktReq()
    {
        this.m_strAction = PACKET_DEFINE.FRIEND_LOCKLIKE_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	//发送好友标志喜欢//
	public static void SendFriendLockLikeReq(int pid, List<int> lstFriendPID)
	{
		FriendLockLikePktReq req = new FriendLockLikePktReq();
		req.m_iPID = pid;
		req.m_lstFriendPID = lstFriendPID;
		
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}
