//  HeroUpgradePktReq.cs
//  Author: Cheng Xia
//  2013-12-25

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;


/// <summary>
/// 英雄升级数据包
/// </summary>
public class HeroUpgradePktReq : HTTPPacketRequest
{
    public int m_iPID;  //玩家ID
    public int m_iHeroID;   //英雄ID//
    public List<int> m_iCostHeroIDs;    //被吞其他英雄ID//


    public HeroUpgradePktReq()
    {
        this.m_strAction = PACKET_DEFINE.HERO_UPGRADE_REQ;
    }
}



/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	
	/// <summary>
	/// 发送英雄升级数据
	/// </summary>
	/// <param name="pid"></param>
	public static void SendHeroUpgrade(int pid, int heroID, List<int> costHeroIDs)
	{
		HeroUpgradePktReq req = new HeroUpgradePktReq();
		req.m_iPID = pid;
		req.m_iHeroID = heroID;
		req.m_iCostHeroIDs = costHeroIDs;
		
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}
