//  FriendFindPktAck.cs
//  Author: Cheng Xia
//  2013-1-13

using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Base;

using Game.Network;


/// <summary>
/// 好友查找应答数据
/// </summary>
public class FriendFindPktAck : HTTPPacketAck
{
    public FriendData m_friendData = new FriendData();

//     public FriendFindPktAck()
//     {
//         this.m_strAction = PACKET_DEFINE.FRIEND_FIND_REQ;
//     }
}

// /// <summary>
// /// 好友查找应答工厂类
// /// </summary>
// public class FriendFindPktAckFactory : HTTPPacketFactory
// {
//     /// <summary>
//     /// 获取action
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.FRIEND_FIND_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(IJSonObject json)
//     {
//         FriendFindPktAck ack = PACKET_HEAD.PACKET_ACK_HEAD<FriendFindPktAck>(json);

//         Debug.Log("FriendFindPktAck");

//         if (ack.header.code != 0)
//         {
//             //GAME_LOG.ERROR("Error . code desc " + ack.header.desc);
//             return ack;
//         }

//         IJSonObject data = json["data"];

//         FriendData fd = new FriendData();
//         fd.m_iPID = data["pid"].Int32Value;
//         fd.m_strNickName = data["nickname"].ToString();
//         fd.m_iRoloLevel = data["lv"].Int32Value;
//         fd.m_lstWantGift[0] = data["want_item1"].Int32Value;
//         fd.m_lstWantGift[1] = data["want_item2"].Int32Value;
//         fd.m_lstWantGift[2] = data["want_item3"].Int32Value;
//         fd.m_strSignature = data["signature"].ToString();
//         //fd.m_iLike = data["like"].Int32Value;
//         //fd.m_iLoginTime = data["login_time"].Int64Value;
//         //fd.m_iAthleticsLevel = data["sport_lv"].Int32Value;
//         fd.m_iHeroTableID = data["hero_id"].Int32Value;
//         fd.m_iHeroLv = data["hero_lv"].Int32Value;
//         fd.m_iHeroHp = (int)data["hero_hp"].SingleValue;
//         fd.m_iAttack = (int)data["hero_attack"].SingleValue;
//         fd.m_iDefend = (int)data["hero_defend"].SingleValue;
//         fd.m_iRecover = (int)data["hero_recover"].SingleValue;

//         ack.m_friendData = fd;

//         return ack;
//     }
// }
