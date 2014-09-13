using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;


//  FubenStoryPktReq.cs
//  Author: Lu Zexi
//  2014-02-27



/// <summary>
/// 副本剧情请求
/// </summary>
public class FubenStoryPktReq : HTTPPacketRequest
{
    public int m_iPID;  //玩家ID
    public int m_iWorldID;  //世界ID
    public int m_iAreaIndex;    //区域索引
    public int m_iFubenIndex;   //副本索引
    public int m_iGateIndex;    //关卡索引

    public FubenStoryPktReq()
    {
        this.m_strAction = PACKET_DEFINE.FUBEN_STORY_REQ;
    }


    // /// <summary>
    // /// 获取请求
    // /// </summary>
    // /// <returns></returns>
    // public override string GetRequire()
    // {
    //     string req = "pid=" + this.m_iPID + "&world_id=" + this.m_iWorldID + "&area_index=" + this.m_iAreaIndex + "&fuben_index=" + this.m_iFubenIndex + "&gate_index=" + this.m_iGateIndex;

    //     PACKET_HEAD.PACKET_REQ_END(ref req);

    //     return req;
    // }
}
