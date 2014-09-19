//  HeroEvolutionPktReq.cs
//  Author: Cheng Xia
//  2013-12-25

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;


class HeroEvolutionPktReq : HTTPPacketRequest
{

    public int m_iPID;  //玩家ID
    public int m_iHeroID;   //英雄ID//
    //public List<int> m_iCostHeroIDs;    //被吞其他英雄ID//

    public HeroEvolutionPktReq()
    {
        this.m_strAction = PACKET_DEFINE.HERO_EVOLUTION_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	public static void SendHeroEvolution(int pid, int heroID)
	{
		HeroEvolutionPktReq req = new HeroEvolutionPktReq();
		req.m_iPID = pid;
		req.m_iHeroID = heroID;
		
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}
