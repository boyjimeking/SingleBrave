using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Network;
//接收系统礼物请求类
//Author Sunyi
//2014-1-20
public class PlayerReceiveSystemEmailPktReq : HTTPPacketRequest
{
    public int m_iPlayerId;//玩家id
    public string m_strGiftIds;//接收的礼物id字符串

    public PlayerReceiveSystemEmailPktReq()
    {
        this.m_strAction = PACKET_DEFINE.PLAYER_RECEIVE_SYSTEM_MAIL_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 发送接收系统礼物请求
	/// </summary>
	/// <param name="pid"></param>
	public static void SendPlayerReceiveSystemGift(int pid, string giftIds)
	{
		PlayerReceiveSystemEmailPktReq req = new PlayerReceiveSystemEmailPktReq();
		req.m_iPlayerId = pid;
		req.m_strGiftIds = giftIds;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}

