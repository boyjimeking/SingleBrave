//  FriendUnlockLikePktAck.cs
//  Author: Cheng Xia
//  2013-1-13

using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Base;

using Game.Network;


/// <summary>
/// 好友标记解锁应答数据
/// </summary>
public class FriendUnlockLikePktAck : HTTPPacketAck
{
    // public FriendUnlockLikePktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.FRIEND_UNLOCKLIKE_REQ;
    // }
}

// /// <summary>
// /// 好友标记喜欢解锁答工厂类
// /// </summary>
// public class FriendUnlockLikePktAckFactory : HTTPPacketFactory
// {
//     /// <summary>
//     /// 获取action
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.FRIEND_UNLOCKLIKE_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(IJSonObject json)
//     {
//         FriendUnlockLikePktAck ack = PACKET_HEAD.PACKET_ACK_HEAD<FriendUnlockLikePktAck>(json);
//         return ack;
//     }
// }
