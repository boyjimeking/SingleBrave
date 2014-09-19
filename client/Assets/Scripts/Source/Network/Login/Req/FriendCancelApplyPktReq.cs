//  FriendCancelApplyPktReq.cs
//  Author: Cheng Xia
//  2013-1-13

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

/// <summary>
/// 好友申请取消协议
/// </summary>
public class FriendCancelApplyPktReq : HTTPPacketRequest
{
    public int m_iPID;  //玩家ID
    public int m_iFriendPID;

    public FriendCancelApplyPktReq()
    {
        this.m_strAction = PACKET_DEFINE.FRIEND_CANCELAPPLY_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	
	//好友取消//
	public static void SendFriendCancelApply(int pid, int fPid)
	{
		FriendCancelApplyPktReq req = new FriendCancelApplyPktReq();
		req.m_iPID = pid;
		req.m_iFriendPID = fPid;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}
