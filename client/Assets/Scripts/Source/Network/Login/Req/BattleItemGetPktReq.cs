using UnityEngine;
using System.Collections;
using Game.Network;

//  BattleItemGetPktReq.cs
//  Author: sanvey
//  2013-12-25

/// <summary>
/// 英雄出售请求
/// </summary>
public class BattleItemGetPktReq : HTTPPacketRequest
{
    public int m_iPid;  //Pid

    public BattleItemGetPktReq()
    {
        this.m_strAction = PACKET_DEFINE.BATTLE_ITEM_GET_REQ;
    }

    // /// <summary>
    // /// 获取请求参数
    // /// </summary>
    // /// <returns></returns>
    // public override string GetRequire()
    // {
    //     string req =string.Format("pid={0}", m_iPid.ToString());

    //     PACKET_HEAD.PACKET_REQ_END(ref req);

    //     return req;
    // }
}