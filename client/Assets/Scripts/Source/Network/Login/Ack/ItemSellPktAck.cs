using System;
using System.Collections.Generic;

using Game.Network;

//  ItemSellPktAck.cs
//  Author: sanvey
//  2014-1-2


/// <summary>
/// 物品出售应答数据包
/// </summary>
public class ItemSellPktAck : HTTPPacketAck
{
    public int m_iGold;
    public int m_iItemID;
    public int m_iItemNum;

//     public ItemSellPktAck()
//     {
//         this.m_strAction = PACKET_DEFINE.ITEM_SELL_REQ;
//     }
}


// /// <summary>
// /// 物品出售应答数据包工厂类
// /// </summary>
// public class ItemSellPktAckFactory : HTTPPacketFactory
// {

//     /// <summary>
//     /// 获取数据包Action
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.ITEM_SELL_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(IJSonObject json)
//     {
//         ItemSellPktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<ItemSellPktAck>(json);

//         if (pkt.header.code != 0)
//         {
//             return pkt;
//         }

//         IJSonObject data = json["data"];

//         pkt.m_iGold = data["gold"].Int32Value;
//         pkt.m_iItemID = data["id"].Int32Value;
//         pkt.m_iItemNum = data["new_num"].Int32Value;

//         return pkt;
//     }
// }