//  FriendUnlockLikePktReq.cs
//  Author: Cheng Xia
//  2013-1-13

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

/// <summary>
/// 好友喜欢解除获取协议
/// </summary>
public class FriendUnlockLikePktReq : HTTPPacketRequest
{
    public int m_iPID;  //玩家ID
    public List<int> m_lstFriendPID;

    public FriendUnlockLikePktReq()
    {
        this.m_strAction = PACKET_DEFINE.FRIEND_UNLOCKLIKE_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	//发送好友喜欢解锁//
	public static void SendFriendUnlockLikeReq(int pid, List<int> lstFriendPID)
	{
		FriendUnlockLikePktReq req = new FriendUnlockLikePktReq();
		req.m_iPID = pid;
		req.m_lstFriendPID = lstFriendPID;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}
