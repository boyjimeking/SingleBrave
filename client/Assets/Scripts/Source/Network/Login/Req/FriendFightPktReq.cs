using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

//获取战友信息接口请求包
//Author sunyi
//2013-12-23
public class FriendFightPktReq : HTTPPacketRequest
{
    public int m_iPid;

    public FriendFightPktReq()
    {
        this.m_strAction = PACKET_DEFINE.FRIEND_FIGHT_REQ;
    }
}



/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 战友请求
	/// </summary>
	/// <param name="pid"></param>
	public static void SendFriendFightReq(int pid)
	{
		FriendFightPktReq req = new FriendFightPktReq();
		req.m_iPid = pid;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}

