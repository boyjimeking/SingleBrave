//  FriendAcceptApplyPktAck.cs
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
public class FriendAcceptApplyPktAck : HTTPPacketBase
{
    public FriendData m_cNewFriend;

    public FriendAcceptApplyPktAck()
    {
        this.m_strAction = PACKET_DEFINE.FRIEND_ACCEPTAPPLY_REQ;
    }
}

/// <summary>
/// 好友接受申请应答工厂类
/// </summary>
public class FriendAcceptApplyPktAckFactory : HTTPPacketFactory
{
    /// <summary>
    /// 获取action
    /// </summary>
    /// <returns></returns>
    public override string GetPacketAction()
    {
        return PACKET_DEFINE.FRIEND_ACCEPTAPPLY_REQ;
    }

    /// <summary>
    /// 创建数据包
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public override HTTPPacketBase Create(IJSonObject json)
    {
        FriendAcceptApplyPktAck ack = PACKET_HEAD.PACKET_ACK_HEAD<FriendAcceptApplyPktAck>(json);

        if (ack.m_iErrorCode != 0)
        {
            return ack;
        }

        IJSonObject item = json["data"];
        ack.m_cNewFriend = new FriendData();
        ack.m_cNewFriend.m_iPID = item["pid"].Int32Value;
        ack.m_cNewFriend.m_strNickName = item["nickname"].ToString();
        ack.m_cNewFriend.m_iRoloLevel = item["lv"].Int32Value;
        ack.m_cNewFriend.m_lstWantGift[0] = item["want_item1"].Int32Value;
        ack.m_cNewFriend.m_lstWantGift[1] = item["want_item2"].Int32Value;
        ack.m_cNewFriend.m_lstWantGift[2] = item["want_item3"].Int32Value;
        ack.m_cNewFriend.m_strSignature = item["signature"].ToString();
        ack.m_cNewFriend.m_iLike = item["like"].Int32Value;
        ack.m_cNewFriend.m_iLoginTime = item["login_time"].Int64Value;
        ack.m_cNewFriend.m_iAthleticsLevel = item["pvp_point"].Int32Value;
        ack.m_cNewFriend.m_iHeroTableID = item["hero_id"].Int32Value;
        ack.m_cNewFriend.m_iHeroLv = item["hero_lv"].Int32Value;
        ack.m_cNewFriend.m_iHeroHp = (int)item["hero_hp"].SingleValue;
        ack.m_cNewFriend.m_iAttack = (int)item["hero_attack"].SingleValue;
        ack.m_cNewFriend.m_iDefend = (int)item["hero_defend"].SingleValue;
        ack.m_cNewFriend.m_iRecover = (int)item["hero_recover"].SingleValue;
        ack.m_cNewFriend.m_iSendTime = item["give_time"].Int64Value;


        return ack;
    }
}
