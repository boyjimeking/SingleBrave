//  FriendApplyPktAck.cs
//  Author: Cheng Xia
//  2013-1-13

using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Base;
using CodeTitans.JSon;
using Game.Network;


/// <summary>
/// 好友申请应答数据
/// </summary>
public class FriendApplyPktAck : HTTPPacketBase
{
    public bool m_isOk;

    public FriendApplyPktAck()
    {
        this.m_strAction = PACKET_DEFINE.FRIEND_APPLY_REQ;
    }
}

/// <summary>
/// 好友申请应答工厂类
/// </summary>
public class FriendApplyPktAckFactory : HTTPPacketFactory
{
    /// <summary>
    /// 获取action
    /// </summary>
    /// <returns></returns>
    public override string GetPacketAction()
    {
        return PACKET_DEFINE.FRIEND_APPLY_REQ;
    }

    /// <summary>
    /// 创建数据包
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public override HTTPPacketBase Create(IJSonObject json)
    {
        FriendApplyPktAck ack = PACKET_HEAD.PACKET_ACK_HEAD<FriendApplyPktAck>(json);

        if (ack.m_iErrorCode != 0)
        {
            return ack;
        }

        IJSonObject data = json["data"];
        Debug.Log(data["ok"]);
        ack.m_isOk = data["ok"].BooleanValue;
        Debug.Log(ack.m_isOk + " -- " + data["ok"].BooleanValue);
        return ack;
    }
}
