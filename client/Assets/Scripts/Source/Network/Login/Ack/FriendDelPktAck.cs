//  FriendDelPktAck.cs
//  Author: Cheng Xia
//  2013-1-15

using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Base;
using CodeTitans.JSon;
using Game.Network;


/// <summary>
/// 好友删除应答数据
/// </summary>
public class FriendDelPktAck : HTTPPacketBase
{
    public FriendDelPktAck()
    {
        this.m_strAction = PACKET_DEFINE.FRIEND_DEL_REQ;
    }
}

/// <summary>
/// 好友删除应答工厂类
/// </summary>
public class FriendDelPktAckFactory : HTTPPacketFactory
{
    /// <summary>
    /// 获取action
    /// </summary>
    /// <returns></returns>
    public override string GetPacketAction()
    {
        return PACKET_DEFINE.FRIEND_DEL_REQ;
    }

    /// <summary>
    /// 创建数据包
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public override HTTPPacketBase Create(IJSonObject json)
    {
        FriendDelPktAck ack = PACKET_HEAD.PACKET_ACK_HEAD<FriendDelPktAck>(json);

        return ack;
    }
}
