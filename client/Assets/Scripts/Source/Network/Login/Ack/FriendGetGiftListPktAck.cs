//  FriendGetGiftListPktAck.cs
//  Author: Cheng Xia
//  2013-1-17

using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Base;
using CodeTitans.JSon;
using Game.Network;


/// <summary>
/// 好友礼物列表应答数据
/// </summary>
public class FriendGetGiftListPktAck : HTTPPacketBase
{
    public List<FriendGiftData> m_lstFriendGiftData = new List<FriendGiftData>();
    //public List<FriendData> m_lstFriendData = new List<FriendData>();

    public FriendGetGiftListPktAck()
    {
        this.m_strAction = PACKET_DEFINE.FRIEND_GETGIFTLIST_REQ;
    }
}

/// <summary>
/// 好友申请列表应答工厂类
/// </summary>
public class FriendGetGiftListPktAckFactory : HTTPPacketFactory
{
    /// <summary>
    /// 获取action
    /// </summary>
    /// <returns></returns>
    public override string GetPacketAction()
    {
        return PACKET_DEFINE.FRIEND_GETGIFTLIST_REQ;
    }

    /// <summary>
    /// 创建数据包
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public override HTTPPacketBase Create(IJSonObject json)
    {
        FriendGetGiftListPktAck ack = PACKET_HEAD.PACKET_ACK_HEAD<FriendGetGiftListPktAck>(json);

        if (ack.m_iErrorCode != 0)
        {
            GAME_LOG.ERROR("Error . code desc " + ack.m_strErrorDes);
            return ack;
        }

        IJSonObject data = json["data"];
        foreach (IJSonObject item in data.ArrayItems)
        {
            FriendGiftData fd = new FriendGiftData();
            fd.m_iID = item["id"].Int32Value;
            fd.m_iPID = item["pid"].Int32Value;
            fd.m_iFID = item["fid"].Int32Value;
            fd.m_iNum = item["gift_num"].Int32Value;
            fd.m_eType = (GiftType)item["gift_type"].Int32Value;
            fd.m_iGiftID = item["gift_id"].Int32Value;

            ack.m_lstFriendGiftData.Add(fd);
        }

        //Debug.Log("ack_" + ack.m_lstFriendGiftData.Count);

        return ack;
    }
}
