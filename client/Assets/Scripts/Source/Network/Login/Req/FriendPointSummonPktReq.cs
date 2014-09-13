using UnityEngine;
using System.Collections;
using Game.Network;

//  FriendPointSummonPktReq.cs
//  Author: sanvey
//  2013-12-17

/// <summary>
/// 友情点召唤
/// </summary>
public class FriendPointSummonPktReq : HTTPPacketRequest
{
    public int m_iPID;  //用户Pid

    public FriendPointSummonPktReq()
    {
        this.m_strAction = PACKET_DEFINE.FRIENDPOINT_SUMMON_REQ;
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