using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;


//  HeroBookPktReq.cs
//  Author: Lu Zexi
//  2013-12-30


/// <summary>
/// 英雄图鉴信息请求
/// </summary>
public class HeroBookPktReq : HTTPPacketRequest
{
    public int m_iPid;  //玩家ID

    public HeroBookPktReq()
        : base()
    {
        this.m_strAction = PACKET_DEFINE.HERO_BOOK_REQ;
    }

}



/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	
	/// <summary>
	/// 发送英雄图鉴请求
	/// </summary>
	/// <param name="pid"></param>
	public static void SendHeroBookReq(int pid)
	{
		HeroBookPktReq req = new HeroBookPktReq();
		req.m_iPid = pid;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}
