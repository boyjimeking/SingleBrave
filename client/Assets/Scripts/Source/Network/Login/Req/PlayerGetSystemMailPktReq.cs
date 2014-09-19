using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Network;
//获取系统邮件数据请求类
//Author sunyi
//2014-1-17
public class PlayerGetSystemMailPktReq : HTTPPacketRequest
{
    public int m_iPlayerId;//玩家id

    public PlayerGetSystemMailPktReq()
    {
        this.m_strAction = PACKET_DEFINE.PLAYER_GET_SYSTEM_MAIL_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 发送获取邮件信息列表
	/// </summary>
	/// <param name="pid"></param>
	public static void SendPlayerGetSystemMail(int pid)
	{
		PlayerGetSystemMailPktReq req = new PlayerGetSystemMailPktReq();
		req.m_iPlayerId = pid;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}

