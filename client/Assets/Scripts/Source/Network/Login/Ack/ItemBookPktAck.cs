using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Network;


//  ItemBookPktAck.cs
//  Author: Lu Zexi
//  2013-12-30



/// <summary>
/// 物品图鉴应答数据包
/// </summary>
public class ItemBookPktAck : HTTPPacketAck
{
    public List<int> m_lstItem = new List<int>();

    // public ItemBookPktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.ITEM_BOOK_REQ;
    // }

}


// /// <summary>
// /// 物品图鉴数据包工厂类
// /// </summary>
// public class ItemBookPktFactory : HTTPPacketFactory
// {
//     public ItemBookPktFactory()
//     { 
//     }

//     /// <summary>
//     /// 获取action
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.ITEM_BOOK_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(IJSonObject json)
//     {
//         ItemBookPktAck ack = PACKET_HEAD.PACKET_ACK_HEAD<ItemBookPktAck>(json);

//         if (ack.m_iErrorCode != 0)
//         {
//             return ack;
//         }

//         IJSonObject data = json["data"];

//         foreach(IJSonObject item in data.ArrayItems)
//         {
//             ack.m_lstItem.Add(item.Int32Value);
//         }

//         return ack;
        
//     }
// }