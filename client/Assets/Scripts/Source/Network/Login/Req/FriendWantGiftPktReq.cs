//  FriendWantGiftPktReq.cs
//  Author: Cheng Xia
//  2013-1-20

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

/// <summary>
/// 好友期望礼物协议
/// </summary>
public class FriendWantGiftPktReq : HTTPPacketRequest
{
    public int m_iPID;  //玩家ID
    public int[] m_iWantGiftIDs = new int[3];    //希望获取的m_iWantGiftID;

    public FriendWantGiftPktReq()
    {
        this.m_strAction = PACKET_DEFINE.FRIEND_WANTGIFT_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	//希望好友礼物//
	public static void SendFriendWantGift(int pid, int[] wantGifts)
	{
		FriendWantGiftPktReq req = new FriendWantGiftPktReq();
		req.m_iPID = pid;
		req.m_iWantGiftIDs = wantGifts;
		SessionManager.GetInstance().SendReady(SESSION_DEFINE.LOGIN_SESSION, req);
	}
}
