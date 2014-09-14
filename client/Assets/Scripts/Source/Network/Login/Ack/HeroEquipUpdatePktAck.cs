using System;
using System.Collections.Generic;

using Game.Network;

//  HeroEquipUpdatePktAck.cs
//  Author: sanvey
//  2013-12-17


/// <summary>
/// 英雄装备更新应答数据包
/// </summary>
public class HeroEquipUpdatePktAck : HTTPPacketAck
{
    // public HeroEquipUpdatePktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.HERO_EQUIP_UPDATE_REQ;
    // }
}


// /// <summary>
// /// 英雄装备更新应答数据包工厂类
// /// </summary>
// public class HeroEquipUpdatePktAckFactory : HTTPPacketFactory
// {

//     /// <summary>
//     /// 获取数据包Action
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.HERO_EQUIP_UPDATE_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(IJSonObject json)
//     {
//         HeroEquipUpdatePktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<HeroEquipUpdatePktAck>(json);

//         if (pkt.header.code != 0)
//         {
//             return pkt;
//         }

//         return pkt;
//     }
// }

