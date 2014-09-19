//  FriendGetGiftListPktReq.cs
//  Author: Cheng Xia
//  2013-1-17

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

/// <summary>
/// 好友礼物列表获取协议
/// </summary>
public class FriendGetGiftListPktReq : HTTPPacketRequest
{ 
    public int m_iPID;  //玩家ID
      
    public FriendGetGiftListPktReq()
    {
        this.m_strAction = PACKET_DEFINE.FRIEND_GETGIFTLIST_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	//发送好友礼物列表//
	public static void SendFriendGetGiftList(int pid)
	{
		FriendGetGiftListPktReq req = new FriendGetGiftListPktReq();
		req.m_iPID = pid;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}
