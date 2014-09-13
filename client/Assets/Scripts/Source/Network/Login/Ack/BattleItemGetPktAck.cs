using System;
using System.Collections.Generic;
using CodeTitans.JSon;
using UnityEngine;
using Game.Network;

//  BattleItemGetPktAck.cs
//  Author: sanvey
//  2013-12-17


/// <summary>
/// 战斗道具获得应答数据包
/// </summary>
public class BattleItemGetPktAck : HTTPPacketBase
{
    public int m_iPos0;
    public int m_iPos1;
    public int m_iPos2;
    public int m_iPos3;
    public int m_iPos4;

    public int m_iPos0_n;
    public int m_iPos1_n;
    public int m_iPos2_n;
    public int m_iPos3_n;
    public int m_iPos4_n;

    public BattleItemGetPktAck()
    {
        this.m_strAction = PACKET_DEFINE.BATTLE_ITEM_GET_REQ;
    }
}


/// <summary>
/// 战斗道具获得应答数据包工厂类
/// </summary>
public class BattleItemGetPktAckFactory : HTTPPacketFactory
{

    /// <summary>
    /// 获取数据包Action
    /// </summary>
    /// <returns></returns>
    public override string GetPacketAction()
    {
        return PACKET_DEFINE.BATTLE_ITEM_GET_REQ;
    }

    /// <summary>
    /// 创建数据包
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public override HTTPPacketBase Create(IJSonObject json)
    {
        BattleItemGetPktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<BattleItemGetPktAck>(json);

        if (pkt.m_iErrorCode != 0)
        {
            return pkt;
        }

        IJSonObject data = json["data"];
        pkt.m_iPos0 = data["pos0"].Int32Value;
        pkt.m_iPos1 = data["pos1"].Int32Value;
        pkt.m_iPos2 = data["pos2"].Int32Value;
        pkt.m_iPos3 = data["pos3"].Int32Value;
        pkt.m_iPos4 = data["pos4"].Int32Value;
        pkt.m_iPos0_n = data["pos0_n"].Int32Value;
        pkt.m_iPos1_n = data["pos1_n"].Int32Value;
        pkt.m_iPos2_n = data["pos2_n"].Int32Value;
        pkt.m_iPos3_n = data["pos3_n"].Int32Value;
        pkt.m_iPos4_n = data["pos4_n"].Int32Value;

        return pkt;
    }
}
