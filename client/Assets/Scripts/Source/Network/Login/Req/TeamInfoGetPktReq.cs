using UnityEngine;
using System.Collections;
using Game.Network;

//  TeamInfoGetPktReq.cs
//  Author: sanvey
//  2013-12-11

/// <summary>
/// 获取玩家队伍信息请求包
/// </summary>
public class TeamInfoGetPktReq : HTTPPacketRequest
{
    public int m_iPid;  //玩家Pid

    public TeamInfoGetPktReq()
    {
        this.m_strAction = PACKET_DEFINE.GET_TEAMS_REQ;
    }

    // /// <summary>
    // /// 获取请求参数
    // /// </summary>
    // /// <returns></returns>
    // public override string GetRequire()
    // {
    //     string req = string.Format("pid={0}", m_iPid);

    //     PACKET_HEAD.PACKET_REQ_END(ref req);

    //     return req;
    // }
}