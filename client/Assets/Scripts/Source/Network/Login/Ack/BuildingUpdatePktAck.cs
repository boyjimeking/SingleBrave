using System;
using System.Collections.Generic;

using Game.Network;

//  BuildingUpdatePktAck.cs
//  Author: sanvey
//  2013-12-13


/// <summary>
/// 建筑升级信息应答数据包
/// </summary>
public class BuildingUpdatePktAck : HTTPPacketAck
{

//     public BuildingUpdatePktAck()
//     {
//         this.m_strAction = PACKET_DEFINE.BUILDING_UPDATE_REQ;
//     }
 }


// /// <summary>
// /// 建筑升级信息应答数据包工厂类
// /// </summary>
// public class BuildingUpdatePktAckFactory : HTTPPacketFactory
// {

//     /// <summary>
//     /// 获取数据包Action
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.BUILDING_UPDATE_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(IJSonObject json)
//     {
//         BuildingUpdatePktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<BuildingUpdatePktAck>(json);

//         if (pkt.m_iErrorCode != 0)
//         {
//             return pkt;
//         }

//         return pkt;

//     }
// }