using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Network;

//商城钻石价格数据应答类
//Author sunyi
//2014-02-28
public class StoreDiamonGetPktAck : HTTPPacketAck
{
    public List<StoreDiamondPrice> m_lstStoreDiamondPrice = new List<StoreDiamondPrice>();//列表


    // public StoreDiamonGetPktAck() 
    // {
    //     this.m_strAction = PACKET_DEFINE.STORE_DIAMOND_GET_REQ;
    // }
}

// /// <summary>
// /// 商城钻石价格数据应答工厂类
// /// </summary>
// public class StoreDiamonGetPktAckFactory : HTTPPacketFactory
// {
//     /// <summary>
//     /// 获取数据包Action
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.STORE_DIAMOND_GET_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(CodeTitans.JSon.IJSonObject json)
//     {
//         StoreDiamonGetPktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<StoreDiamonGetPktAck>(json);
//         if (pkt.header.code != 0)
//         {
//             return pkt;
//         }

//         var data = json["data"].ArrayItems;

//         foreach (var item in data)
//         {
//             StoreDiamondPrice price = new StoreDiamondPrice();

//             price.m_iId = item["id"].Int32Value;
//             price.m_iCount = item["num"].Int32Value;
//             price.m_iTotal = item["total_price"].Int32Value;
//             price.m_strTypeID = item["type_id"].StringValue;

//             pkt.m_lstStoreDiamondPrice.Add(price);
//         }

//         return pkt;
//     }
// }