﻿using UnityEngine;
using System.Collections;
using Game.Network;
using System.Collections.Generic;

//  HeroLockPktReq.cs
//  Author: sanvey
//  2013-12-19

/// <summary>
/// 英雄锁定请求
/// </summary>
public class HeroLockPktReq : HTTPPacketRequest
{
    public List<int> m_lstHeros;  //锁定英雄列表
    public int m_iPid;  //Pid

    public HeroLockPktReq()
    {
        this.m_strAction = PACKET_DEFINE.HERO_LOCK_REQ;
    }

    // /// <summary>
    // /// 获取请求参数
    // /// </summary>
    // /// <returns></returns>
    // public override string GetRequire()
    // {
    //     string data = string.Empty;
    //     foreach (var item in m_lstHeros)
    //     {
    //         data += item.ToString() + "|";
    //     }
    //     if (data.EndsWith("|"))
    //     {
    //         data = data.Remove(data.Length - 1);
    //     }

    //     string req = string.Format("pid={0}&heros={1}", m_iPid.ToString(), data);

    //     PACKET_HEAD.PACKET_REQ_END(ref req);

    //     return req;
    // }
}