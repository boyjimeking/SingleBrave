using UnityEngine;
using System.Collections;
using Game.Network;

//  PlayerInfoGetPktReq.cs
//  Author: sanvey
//  2013-12-11

/// <summary>
/// 获取玩家信息请求包
/// </summary>
public class PlayerInfoGetPktReq : HTTPPacketBase
{
    public int m_iUID;  //用户UID

    public PlayerInfoGetPktReq()
    {
        this.m_strAction = PACKET_DEFINE.GET_PLAYINFO_REQ;
    }

    /// <summary>
    /// 获取请求参数
    /// </summary>
    /// <returns></returns>
    public override string GetRequire()
    {
        string req = "uid=" + this.m_iUID;

        PACKET_HEAD.PACKET_REQ_END(ref req);

        return req;
    }
}