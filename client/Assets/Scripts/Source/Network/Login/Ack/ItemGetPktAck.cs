using System;
using System.Collections.Generic;
using CodeTitans.JSon;
using Game.Network;

//  ItemGetPktAck.cs
//  Author: sanvey
//  2013-12-23


/// <summary>
/// 获取物品应答数据包
/// </summary>
public class ItemGetPktAck : HTTPPacketBase
{
    public List<ItemData> m_lstItems;

    public class ItemData
    {
        public int m_iId;
        public int m_iPid;
        public int m_iItem_id;
        public int m_iItem_num;
    }


    public ItemGetPktAck()
    {
        this.m_strAction = PACKET_DEFINE.ITEM_GET_REQ;
    }
}


/// <summary>
/// 获取物品应答数据包工厂类
/// </summary>
public class ItemGetPktAckFactory : HTTPPacketFactory
{

    /// <summary>
    /// 获取数据包Action
    /// </summary>
    /// <returns></returns>
    public override string GetPacketAction()
    {
        return PACKET_DEFINE.ITEM_GET_REQ;
    }

    /// <summary>
    /// 创建数据包
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public override HTTPPacketBase Create(IJSonObject json)
    {
        ItemGetPktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<ItemGetPktAck>(json);

        if (pkt.m_iErrorCode != 0)
        {
            return pkt;
        }

        IEnumerable<IJSonObject> data = json["data"].ArrayItems;
        pkt.m_lstItems = new List<ItemGetPktAck.ItemData>();

        foreach (var item in data)
        {
            ItemGetPktAck.ItemData tmp = new ItemGetPktAck.ItemData();
            tmp.m_iId = item["id"].Int32Value;
            tmp.m_iItem_id = item["item_id"].Int32Value;
            tmp.m_iItem_num = item["item_num"].Int32Value;
            pkt.m_lstItems.Add(tmp);
        }

        return pkt;
    }
}