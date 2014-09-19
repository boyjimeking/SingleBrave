//  FrindApplyPktReq.cs
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
public class FriendApplyPktReq : HTTPPacketRequest
{
    public int m_iPID;  //玩家ID
    public int m_iFriendPID;

    public FriendApplyPktReq()
    {
        this.m_strAction = PACKET_DEFINE.FRIEND_APPLY_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	
	//好友申请
	public static void SendFriendApply(int pid, int fPid)
	{
		FriendApplyPktReq req = new FriendApplyPktReq();
		req.m_iPID = pid;
		req.m_iFriendPID = fPid;
		
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}
