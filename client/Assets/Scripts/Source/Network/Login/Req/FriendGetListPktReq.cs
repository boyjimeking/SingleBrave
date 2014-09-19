//  FriendGetListPktReq.cs
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
public class FriendGetListPktReq : HTTPPacketRequest
{
    public int m_iPID;  //玩家ID

    public FriendGetListPktReq()
    {
        this.m_strAction = PACKET_DEFINE.FRIEND_GETLIST_REQ;
    }

}



/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	
	//发送获取好友列表//
	public static void SendFriendGetListReq(int pid)
	{
		FriendGetListPktReq req = new FriendGetListPktReq();
		req.m_iPID = pid;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}


}