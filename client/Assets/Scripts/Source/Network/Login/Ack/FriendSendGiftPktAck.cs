//  FriendSendGiftPktAck.cs
//  Author: Cheng Xia
//  2013-1-17

using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Base;

using Game.Network;


/// <summary>
/// 赠送好友礼物应答数据
/// </summary>
public class FriendSendGiftPktAck : HTTPPacketAck
{
    // public FriendSendGiftPktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.FRIEND_SENDGIFT_REQ;
    // }
}

// /// <summary>
// /// 好友查找应答工厂类
// /// </summary>
// public class FriendSendGiftPktAckFactory : HTTPPacketFactory
// {
//     /// <summary>
//     /// 获取action
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.FRIEND_SENDGIFT_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(IJSonObject json)
//     {
//         FriendSendGiftPktAck ack = PACKET_HEAD.PACKET_ACK_HEAD<FriendSendGiftPktAck>(json);

//         Debug.Log("FriendSendGiftPktAck");

//         if (ack.m_iErrorCode != 0)
//         {
//             //GAME_LOG.ERROR("Error . code desc " + ack.m_strErrorDes);
//             return ack;
//         }

//         IJSonObject data = json["data"];

//         return ack;
//     }
// }
