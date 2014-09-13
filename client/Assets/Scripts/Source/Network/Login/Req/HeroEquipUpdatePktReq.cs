using UnityEngine;
using System.Collections;
using Game.Network;

//  HeroEquipUpdatePktReq.cs
//  Author: sanvey
//  2013-12-20

/// <summary>
/// 英雄装备道具请求
/// </summary>
public class HeroEquipUpdatePktReq : HTTPPacketBase
{
    public int[] m_vecHeros;  //英雄列表
    public int[] m_vecItems;  //装备列表
    public int m_iPid;        //Pid

    public HeroEquipUpdatePktReq()
    {
        this.m_strAction = PACKET_DEFINE.HERO_EQUIP_UPDATE_REQ;
    }

    /// <summary>
    /// 获取请求参数
    /// </summary>
    /// <returns></returns>
    public override string GetRequire()
    {
        string data = string.Empty;
        for (int i = 0; i < m_vecItems.Length; i++)
        {
            data += m_vecHeros[i] + ":" + m_vecItems[i] + "|";
        }
        if (data.EndsWith("|"))
        {
            data = data.Remove(data.Length - 1);
        }

        string req = string.Format("pid={0}&equip_info={1}", m_iPid.ToString(), data);

        PACKET_HEAD.PACKET_REQ_END(ref req);

        return req;
    }
}