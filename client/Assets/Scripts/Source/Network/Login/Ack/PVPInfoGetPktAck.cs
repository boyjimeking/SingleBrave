using System;
using System.Collections.Generic;

using Game.Network;

//  PVPInfoGetPktAck.cs
//  Author: sanvey
//  2014-2-8


/// <summary>
/// 竞技场基本信息获得
/// </summary>
public class PVPInfoGetPktAck : HTTPPacketAck
{
    public int m_iPVP_point;
    public int m_iPVP_WeekPoint;
    public int m_iPVPwin_num;
    public int m_iPVPlose_num;
    public int m_iPVPRank;
    public int m_iPVPWeekRank;
    public int m_iPVPMaxExp;

    // public PVPInfoGetPktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.PVP_INFO_GET_REQ;
    // }
}


// /// <summary>
// /// 竞技场基本信息获得数据包工厂类
// /// </summary>
// public class PVPInfoGetPktAckFactory : HTTPPacketFactory
// {

//     /// <summary>
//     /// 获取数据包Action
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.PVP_INFO_GET_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(IJSonObject json)
//     {
//         PVPInfoGetPktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<PVPInfoGetPktAck>(json);

//         if (pkt.header.code != 0)
//         {
//             return pkt;
//         }

//         IJSonObject data = json["data"];

//         pkt.m_iPVP_point = data["pvp_point"].Int32Value;
//         pkt.m_iPVP_WeekPoint = data["pvp_week_point"].Int32Value;
//         pkt.m_iPVPwin_num = data["win_num"].Int32Value;
//         pkt.m_iPVPlose_num = data["lose_num"].Int32Value;
//         pkt.m_iPVPRank = data["whole_rank"].Int32Value;
//         pkt.m_iPVPWeekRank = data["week_rank"].Int32Value;
//         pkt.m_iPVPMaxExp=data["pvp_max_point"].Int32Value;

//         return pkt;
//     }
// }