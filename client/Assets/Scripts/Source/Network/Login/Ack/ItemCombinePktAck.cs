using System;
using System.Collections.Generic;
using CodeTitans.JSon;
using Game.Network;

//  ItemCombinePktAck.cs
//  Author: sanvey
//  2013-12-24


/// <summary>
/// 物品合成应答数据包
/// </summary>
public class ItemCombinePktAck : HTTPPacketBase
{
    public List<ItemData> m_lstItems=new List<ItemData>();
    public List<int> m_lstDeleItemIDs = new List<int>();
    public int m_iFarmpoint;

    public class ItemData
    {
        public int m_iId;
        public int m_iPid;
        public int m_iItem_id;
        public int m_iItem_num;
    }


    public ItemCombinePktAck()
    {
        this.m_strAction = PACKET_DEFINE.ITEM_COMBINED_REQ;
    }
}


/// <summary>
/// 物品合成应答数据包工厂类
/// </summary>
public class ItemCombinePktAckFactory : HTTPPacketFactory
{

    /// <summary>
    /// 获取数据包Action
    /// </summary>
    /// <returns></returns>
    public override string GetPacketAction()
    {
        return PACKET_DEFINE.ITEM_COMBINED_REQ;
    }

    /// <summary>
    /// 创建数据包
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public override HTTPPacketBase Create(IJSonObject json)
    {
        ItemCombinePktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<ItemCombinePktAck>(json);

        if (pkt.m_iErrorCode != 0)
        {
            return pkt;
        }

        IJSonObject data = json["data"];
        pkt.m_iFarmpoint=data["farmpoint"].Int32Value;

        IEnumerable<IJSonObject> allItems = data["updateItems"].ArrayItems;

        foreach (IJSonObject item in allItems)
        {
            ItemCombinePktAck.ItemData tmp = new ItemCombinePktAck.ItemData();
            tmp.m_iId = item["id"].Int32Value;
            tmp.m_iItem_id = item["item_id"].Int32Value;
            tmp.m_iItem_num = item["item_num"].Int32Value;
            pkt.m_lstItems.Add(tmp);
        }

        if (data.Contains("delItems"))
        {
            IEnumerable<IJSonObject> deleItems = data["delItems"].ArrayItems;

            foreach (IJSonObject item in deleItems)
            {
                pkt.m_lstDeleItemIDs.Add(item.Int32Value);
            }
        }



        return pkt;
    }
}
