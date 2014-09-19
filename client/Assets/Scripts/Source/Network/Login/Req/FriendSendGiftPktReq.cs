//  FriendSendGiftPktReq.cs
//  Author: Cheng Xia
//  2013-1-17

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

/// <summary>
/// 赠送好友礼物协议
/// </summary>
public class FriendSendGiftPktReq : HTTPPacketRequest
{
    public int m_iPID;  //玩家ID
    public List<FriendSendData> m_lstFriendSendData;
     
    public FriendSendGiftPktReq()
    {
        this.m_strAction = PACKET_DEFINE.FRIEND_SENDGIFT_REQ;
    }

}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	//赠送好友礼物//
	public static void SendFriendSendGift(int pid, List<FriendSendData> lstFriendSendData)
	{
		FriendSendGiftPktReq req = new FriendSendGiftPktReq();
		req.m_iPID = pid;
		req.m_lstFriendSendData = lstFriendSendData;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}
