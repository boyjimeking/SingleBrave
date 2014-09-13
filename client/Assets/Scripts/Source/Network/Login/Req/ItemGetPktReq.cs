using UnityEngine;
using System.Collections;
using Game.Network;

//  ItemGetPktReq.cs
//  Author: sanvey
//  2013-12-23

/// <summary>
/// 获取物品请求
/// </summary>
public class ItemGetPktReq : HTTPPacketBase
{
    public int m_iPid;  //Pid

    public ItemGetPktReq()
    {
        this.m_strAction = PACKET_DEFINE.ITEM_GET_REQ;
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