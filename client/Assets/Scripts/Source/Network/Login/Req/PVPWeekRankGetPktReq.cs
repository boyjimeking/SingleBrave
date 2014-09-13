using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Network;

//  PVPWeekRankGetPktReq.cs
//  Author: sanvey
//  2014-2-8

/// <summary>
/// 竞技场基本信息获取
/// </summary>
public class PVPWeekRankGetPktReq : HTTPPacketBase
{
    public int m_iPid;  //Pid

    public PVPWeekRankGetPktReq()
    {
        this.m_strAction = PACKET_DEFINE.PVP_WEEK_RANK_GET_REQ;
    }

    /// <summary>
    /// 获取请求参数
    /// </summary>
    /// <returns></returns>
    public override string GetRequire()
    {
        string req = string.Format("pid={0}", m_iPid.ToString());

        PACKET_HEAD.PACKET_REQ_END(ref req);

        return req;
    }
}