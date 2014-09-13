//  FriendWantGiftPktAck.cs
//  Author: Cheng Xia
//  2013-1-20

using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Base;
using CodeTitans.JSon;
using Game.Network;


/// <summary>
/// 好友期望礼物数据
/// </summary>
public class FriendWantGiftPktAck : HTTPPacketBase
{
    public FriendWantGiftPktAck()
    {
        this.m_strAction = PACKET_DEFINE.FRIEND_WANTGIFT_REQ;
    }
}

/// <summary>
/// 好友期望礼物工厂类
/// </summary>
public class FriendWantGiftPktAckFactory : HTTPPacketFactory
{
    /// <summary>
    /// 获取action
    /// </summary>
    /// <returns></returns>
    public override string GetPacketAction()
    {
        return PACKET_DEFINE.FRIEND_WANTGIFT_REQ;
    }

    /// <summary>
    /// 创建数据包
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public override HTTPPacketBase Create(IJSonObject json)
    {
        FriendWantGiftPktAck ack = PACKET_HEAD.PACKET_ACK_HEAD<FriendWantGiftPktAck>(json);

        if (ack.m_iErrorCode != 0)
        {
            GAME_LOG.ERROR("Error . code desc " + ack.m_strErrorDes);
            return ack;
        }

        IJSonObject data = json["data"];


        return ack;
    }
}
