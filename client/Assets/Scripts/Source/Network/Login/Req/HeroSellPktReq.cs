using UnityEngine;
using System.Collections;
using Game.Network;

//  HeroSellPktReq.cs
//  Author: sanvey
//  2013-12-19

/// <summary>
/// 英雄出售请求
/// </summary>
public class HeroSellPktReq : HTTPPacketBase
{
    public int[] m_vecHeros;  //出售英雄列表
    public int m_iPid;  //Pid

    public HeroSellPktReq()
    {
        this.m_strAction = PACKET_DEFINE.HERO_SELL_REQ;
    }

    /// <summary>
    /// 获取请求参数
    /// </summary>
    /// <returns></returns>
    public override string GetRequire()
    {
        string data = string.Empty;
        foreach (var item in m_vecHeros)
        {
            data += item.ToString() + "|";
        }
        if (data.EndsWith("|"))
        {
            data = data.Remove(data.Length - 1);
        }

        string req = string.Format("pid={0}&heros={1}", m_iPid.ToString(), data);

        PACKET_HEAD.PACKET_REQ_END(ref req);

        return req;
    }
}