using System;
using System.Collections.Generic;

using Game.Network;
using UnityEngine;

//  ItemCollectPktAck.cs
//  Author: sanvey
//  2013-12-24


/// <summary>
/// 物品采集应答数据包
/// </summary>
public class ItemCollectPktAck : HTTPPacketAck
{
    public ItemClickInfo m_cShan;
    public ItemClickInfo m_cChuan;
    public ItemClickInfo m_cTian;
    public ItemClickInfo m_cLin;
    public int m_iGold;
    public int m_iFarmPoint;
    public long m_iServerTime;
    public List<ItemData> m_lstItems=new List<ItemData>();

    public class ItemData
    {
        public int m_iId;
        public int m_iPid;
        public int m_iItem_id;
        public int m_iItem_num;
    }

    /// <summary>
    /// 采集数量和采集时间
    /// </summary>
    public class ItemClickInfo
    {
        public long m_lCollectTime;
        public int m_iCollectNum;
    }

    // public ItemCollectPktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.ITEM_COLLECT_REQ;
    // }
}


// /// <summary>
// /// 物品采集应答数据包工厂类
// /// </summary>
// public class ItemCollectPktAckFactory : HTTPPacketFactory
// {

//     /// <summary>
//     /// 获取数据包Action
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.ITEM_COLLECT_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(IJSonObject json)
//     {
//         ItemCollectPktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<ItemCollectPktAck>(json);

//         if (pkt.m_iErrorCode != 0)
//         {
//             return pkt;
//         }

//         IJSonObject data = json["data"];

//         pkt.m_cShan = new ItemCollectPktAck.ItemClickInfo();
//         pkt.m_cShan.m_iCollectNum = data["shan"]["collect_num"].Int32Value;
//         pkt.m_cShan.m_lCollectTime = data["shan"]["collect_time"].Int64Value;

//         pkt.m_cChuan = new ItemCollectPktAck.ItemClickInfo();
//         pkt.m_cChuan.m_iCollectNum = data["chuan"]["collect_num"].Int32Value;
//         pkt.m_cChuan.m_lCollectTime = data["chuan"]["collect_time"].Int64Value;

//         pkt.m_cTian = new ItemCollectPktAck.ItemClickInfo();
//         pkt.m_cTian.m_iCollectNum = data["tian"]["collect_num"].Int32Value;
//         pkt.m_cTian.m_lCollectTime = data["tian"]["collect_time"].Int64Value;

//         pkt.m_cLin = new ItemCollectPktAck.ItemClickInfo();
//         pkt.m_cLin.m_iCollectNum = data["lin"]["collect_num"].Int32Value;
//         pkt.m_cLin.m_lCollectTime = data["lin"]["collect_time"].Int64Value;

//         pkt.m_iGold = data["gold"].Int32Value;
//         pkt.m_iFarmPoint = data["farmpoint"].Int32Value;

//         IEnumerable<IJSonObject> lst = data["items"].ArrayItems;
//         foreach (IJSonObject item in lst)
//         {
//             ItemCollectPktAck.ItemData tmp = new ItemCollectPktAck.ItemData();
//             tmp.m_iId = item["id"].Int32Value;
//             tmp.m_iItem_id = item["item_id"].Int32Value;
//             tmp.m_iItem_num = item["item_num"].Int32Value;

//             pkt.m_lstItems.Add(tmp);
//         }

//         return pkt;
//     }
// }