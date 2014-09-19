//  FriendAcceptGiftPktReq.cs
//  Author: Cheng Xia
//  2013-1-17

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

/// <summary>
/// 接受好友礼物协议
/// </summary>
public class FriendAcceptGiftPktReq : HTTPPacketRequest
{  
    public int m_iPID;  //玩家ID
    public int[] m_vecWantsGift;    //想要的礼物
    public List<int> m_lstFriendGifts = new List<int>();    //接收的礼物
      
    public FriendAcceptGiftPktReq()
    {
        this.m_vecWantsGift = new int[3];
        this.m_strAction = PACKET_DEFINE.FRIEND_ACCEPTGIFT_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	//接受好友礼物//
	public static void SendFriendAcceptGift(int pid, int[] vecWants , List<int> lstGifts)
	{
		FriendAcceptGiftPktReq req = new FriendAcceptGiftPktReq();
		req.m_iPID = pid;
		req.m_vecWantsGift = vecWants;
		req.m_lstFriendGifts = lstGifts;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}
