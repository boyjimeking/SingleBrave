using UnityEngine;
using System.Collections;
using Game.Network;
using System.Collections.Generic;

//  GuestZhaoDaiPktReq.cs
//  Author: sanvey
//  2013-12-19

/// <summary>
/// 招待请求
/// </summary>
public class GuestZhaoDaiPktReq : HTTPPacketBase
{
    public string m_strZhaoDaiId; //招待ID
    public int m_iPid;  //Pid

    public GuestZhaoDaiPktReq()
    {
        this.m_strAction = PACKET_DEFINE.GUEST_ZHAODAI_REQ;
    }

    /// <summary>
    /// 获取请求参数
    /// </summary>
    /// <returns></returns>
    public override string GetRequire()
    {
        string req = string.Format("pid={0}&zdid={1}", m_iPid.ToString(), m_strZhaoDaiId);

        PACKET_HEAD.PACKET_REQ_END(ref req);

        return req;
    }
}