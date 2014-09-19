using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

//玩家英雄列表
//Author:sunyi
//2013-12-11

//获取玩家英雄列表请求数据包
public class PlayerHeroInfoPacketReq : HTTPPacketRequest
{

    public int m_iPlayerId;//玩家id

    public PlayerHeroInfoPacketReq()
    {
        this.m_strAction = PACKET_DEFINE.GET_PALYERHEROINFO_REQ;
    }
}



/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 发送英雄数据请求
	/// </summary>
	/// <param name="pid"></param>
	public static void SendPlayerHeroInfoGetPktReq(int pid)
	{
		PlayerHeroInfoPacketReq req = new PlayerHeroInfoPacketReq();
		req.m_iPlayerId = pid;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}


