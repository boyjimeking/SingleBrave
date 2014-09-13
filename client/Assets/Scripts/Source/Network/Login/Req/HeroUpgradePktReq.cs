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
public class HeroUpgradePktReq : HTTPPacketBase
{
    public int m_iPID;  //玩家ID
    public int m_iHeroID;   //英雄ID//
    public List<int> m_iCostHeroIDs;    //被吞其他英雄ID//


    public HeroUpgradePktReq()
    {
        this.m_strAction = PACKET_DEFINE.HERO_UPGRADE_REQ;
    }

    /// <summary>
    /// 获取数据
    /// </summary>
    /// <returns></returns>
    public override string GetRequire()
    {
        string reqStr = "pid=" + m_iPID + "&";
        reqStr += "hero_id=" + m_iHeroID + "&";
        reqStr += "sacrifice=";
        for (int i = 0; i < m_iCostHeroIDs.Count; i++)
        {
            reqStr += m_iCostHeroIDs[i].ToString();

            if (0 < (m_iCostHeroIDs.Count - 1))
            {
                reqStr += "|";
            }
        }

        PACKET_HEAD.PACKET_REQ_END(ref reqStr);

        return reqStr;
    }
}
