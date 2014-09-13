using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Network;

//  PVPEnemyGetPktReq.cs
//  Author: sanvey
//  2014-2-8

/// <summary>
/// 竞技场排名信息获取
/// </summary>
public class PVPEnemyGetPktReq : HTTPPacketBase
{
    public int m_iPid;  //Pid

    public PVPEnemyGetPktReq()
    {
        this.m_strAction = PACKET_DEFINE.PVP_ENEMY_GET_REQ;
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