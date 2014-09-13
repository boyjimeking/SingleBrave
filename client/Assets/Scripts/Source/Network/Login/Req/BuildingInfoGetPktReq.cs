using UnityEngine;
using System.Collections;
using Game.Network;

//  BuildingInfoGetPktReq.cs
//  Author: sanvey
//  2013-12-13

/// <summary>
/// 获取建筑信息请求包
/// </summary>
public class BuildingInfoGetPktReq : HTTPPacketRequest
{
    public int m_iPID;  //用户Pid

    public BuildingInfoGetPktReq()
    {
        this.m_strAction = PACKET_DEFINE.GET_BUILDING_GET_REQ;
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