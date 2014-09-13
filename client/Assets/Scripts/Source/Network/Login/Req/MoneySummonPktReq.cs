using UnityEngine;
using System.Collections;
using Game.Network;

//  MoneySummonPktReq.cs
//  Author: sanvey
//  2013-12-17

/// <summary>
/// 金钱召唤
/// </summary>
public class MoneySummonPktReq : HTTPPacketRequest
{
    public int m_iPID;  //用户Pid

    public MoneySummonPktReq()
    {
        this.m_strAction = PACKET_DEFINE.MONEY_SUMMON_REQ;
    }

    // /// <summary>
    // /// 获取请求参数
    // /// </summary>
    // /// <returns></returns>
    // public override string GetRequire()
    // {
    //     string req = string.Format("pid={0}", m_iPID);

    //     PACKET_HEAD.PACKET_REQ_END(ref req);

    //     return req;
    // }
}
