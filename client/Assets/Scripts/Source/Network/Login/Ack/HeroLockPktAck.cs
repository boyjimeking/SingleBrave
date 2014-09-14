using System;
using System.Collections.Generic;

using Game.Network;

//  HeroLockPktAck.cs
//  Author: sanvey
//  2013-12-17


/// <summary>
/// 英雄锁定应答数据包
/// </summary>
public class HeroLockPktAck : HTTPPacketAck
{
    // public HeroLockPktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.HERO_LOCK_REQ;
    // }
}


// /// <summary>
// /// 英雄锁定应答数据包工厂类
// /// </summary>
// public class HeroLockPktAckFactory : HTTPPacketFactory
// {

//     /// <summary>
//     /// 获取数据包Action
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.HERO_LOCK_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(IJSonObject json)
//     {
//         HeroLockPktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<HeroLockPktAck>(json);

//         if (pkt.header.code != 0)
//         {
//             return pkt;
//         }

//         return pkt;
//     }
// }
