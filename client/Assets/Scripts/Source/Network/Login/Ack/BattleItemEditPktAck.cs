using System;
using System.Collections.Generic;
using CodeTitans.JSon;
using Game.Network;

//  BattleItemEditPktAck.cs
//  Author: sanvey
//  2013-12-17


/// <summary>
/// 战斗道具编辑应答数据包
/// </summary>
public class BattleItemEditPktAck : HTTPPacketBase
{

    public BattleItemEditPktAck()
    {
        this.m_strAction = PACKET_DEFINE.BATTLE_ITEM_EDIT_REQ;
    }
}


/// <summary>
/// 战斗道具编辑应答数据包工厂类
/// </summary>
public class BattleItemEditPktAckFactory : HTTPPacketFactory
{

    /// <summary>
    /// 获取数据包Action
    /// </summary>
    /// <returns></returns>
    public override string GetPacketAction()
    {
        return PACKET_DEFINE.BATTLE_ITEM_EDIT_REQ;
    }

    /// <summary>
    /// 创建数据包
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public override HTTPPacketBase Create(IJSonObject json)
    {
        BattleItemEditPktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<BattleItemEditPktAck>(json);

        if (pkt.m_iErrorCode != 0)
        {
            return pkt;
        }
        return pkt;
    }
}
