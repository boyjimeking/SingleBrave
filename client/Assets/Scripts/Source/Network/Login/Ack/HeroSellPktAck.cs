using System;
using System.Collections.Generic;

using Game.Network;

//  HeroSellAckPkt.cs
//  Author: sanvey
//  2013-12-17


/// <summary>
/// 英雄出售应答数据包
/// </summary>
public class HeroSellPktAck : HTTPPacketAck
{
    public int m_iGold;  //总共金币
    public List<int> m_lstDeleteIDs;
    public int m_iGetGold;  //出售获得

    // public HeroSellPktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.HERO_SELL_REQ;
    // }
}


// /// <summary>
// /// 英雄出售应答数据包工厂类
// /// </summary>
// public class HeroSellPktAckFactory : HTTPPacketFactory
// {

//     /// <summary>
//     /// 获取数据包Action
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.HERO_SELL_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(IJSonObject json)
//     {
//         HeroSellPktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<HeroSellPktAck>(json);

//         if (pkt.header.code != 0)
//         {
//             return pkt;
//         }

//         IJSonObject data = json["data"];
//         pkt.m_iGold = data["gold"].Int32Value;
//         pkt.m_iGetGold = data["get_gold"].Int32Value;
//         pkt.m_lstDeleteIDs = new List<int>();

//         if (!data["del_id"].IsNull)
//         {
//             foreach (IJSonObject item in data["del_id"].ArrayItems)
//             {
//                 int id = item.Int32Value;
//                 pkt.m_lstDeleteIDs.Add(id);
//             }
//         }

//         return pkt;
//     }
// }
