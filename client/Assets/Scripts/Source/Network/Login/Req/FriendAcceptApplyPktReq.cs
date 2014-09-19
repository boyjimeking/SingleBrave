//  FriendAcceptApplyPktReq.cs
//  Author: Cheng Xia
//  2013-1-13

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

/// <summary>
/// 接受好友申请协议
/// </summary>
public class FriendAcceptApplyPktReq : HTTPPacketRequest
{
    public int m_iPID;  //玩家ID
    public int m_iFriendPID;

    public FriendAcceptApplyPktReq()
    {
        this.m_strAction = PACKET_DEFINE.FRIEND_ACCEPTAPPLY_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	
	//答应好友申请
	public static void SendFriendAcceptApply(int pid, int fPid)
	{
		FriendAcceptApplyPktReq req = new FriendAcceptApplyPktReq();
		req.m_iPID = pid;
		req.m_iFriendPID = fPid;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}
