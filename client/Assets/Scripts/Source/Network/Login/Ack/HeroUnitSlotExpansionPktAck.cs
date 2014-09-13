using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

//单位槽扩张应答类
//Author sunyi
//2013-12-27
public class HeroUnitSlotExpansionPktAck : HTTPPacketAck
{
    public int m_iDiamondCount;//宝石数量
    public int m_iMaxHeroCount;//单位槽英雄最大数量

    // public HeroUnitSlotExpansionPktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.UNIT_EXPANSION_REQ;
    // }
}

// /// <summary>
// /// 单位槽扩张应答工厂类
// /// </summary>
// public class UnitSlotExpansionPktAckFactory : HTTPPacketFactory
// {
//     /// <summary>
//     /// 获取数据包Action
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.UNIT_EXPANSION_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(CodeTitans.JSon.IJSonObject json)
//     {
//         HeroUnitSlotExpansionPktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<HeroUnitSlotExpansionPktAck>(json);
//         if (pkt.m_iErrorCode != 0)
//         {
//             return pkt;
//         }

//         pkt.m_iDiamondCount = json["data"]["diamond"].Int32Value;
//         pkt.m_iMaxHeroCount = json["data"]["max_hero"].Int32Value;

//         return pkt;
//     }
// }

