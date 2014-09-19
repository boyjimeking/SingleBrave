//  FriendGetApplyListPktReq.cs
//  Author: Cheng Xia
//  2013-1-13

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

/// <summary>
/// 好友申请列表获取协议
/// </summary>
public class FriendGetApplyListPktReq : HTTPPacketRequest
{
    public int m_iPID;  //玩家ID

    public FriendGetApplyListPktReq()
    {
        this.m_strAction = PACKET_DEFINE.FRIEND_GETAPPLYLIST_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	//发送好友申请列表//
	public static void SendFriendGetApplyListReq(int pid)
	{
		FriendGetApplyListPktReq req = new FriendGetApplyListPktReq();
		req.m_iPID = pid;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}
