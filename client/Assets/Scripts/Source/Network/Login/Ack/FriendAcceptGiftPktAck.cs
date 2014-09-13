//  FriendAcceptGiftPktAck.cs
//  Author: Cheng Xia
//  2013-1-13

using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Base;
using CodeTitans.JSon;
using Game.Network;


/// <summary>
/// 好友接受申请应答数据
/// </summary>
public class FriendAcceptGiftPktAck : HTTPPacketBase
{
    public int m_iGold; //金币
    public int m_iFarmpoint;    //农场点
    public int m_iFriendpoint;  //友情点
    public List<Item> m_lstItems = new List<Item>();   //物品集//

    public FriendAcceptGiftPktAck()
    {
        this.m_strAction = PACKET_DEFINE.FRIEND_ACCEPTGIFT_REQ;
    }
}

/// <summary>
/// 好友接受申请应答工厂类
/// </summary>
public class FriendAcceptGiftPktAckFactory : HTTPPacketFactory
{
    /// <summary>
    /// 获取action
    /// </summary>
    /// <returns></returns>
    public override string GetPacketAction()
    {
        return PACKET_DEFINE.FRIEND_ACCEPTGIFT_REQ;
    }

    /// <summary>
    /// 创建数据包
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public override HTTPPacketBase Create(IJSonObject json)
    {
        FriendAcceptGiftPktAck ack = PACKET_HEAD.PACKET_ACK_HEAD<FriendAcceptGiftPktAck>(json);

        if (ack.m_iErrorCode != 0)
        {
            return ack;
        }

        IJSonObject data = json["data"];
        ack.m_iGold = data["gold"].Int32Value;
        ack.m_iFarmpoint = data["farmpoint"].Int32Value;
        ack.m_iFriendpoint = data["friendpoint"].Int32Value;

        if (data["others"].Count > 0)
        {
            IJSonObject items = data["others"]["items"];
            foreach (IJSonObject item in items.ArrayItems)
            {
                Item i = new Item(item["item_id"].Int32Value);
                i.m_iID = item["id"].Int32Value;
                i.m_iNum = item["item_num"].Int32Value;
                ack.m_lstItems.Add(i);
            }
        }

        return ack;
    }
}
