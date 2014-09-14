//  FriendGetListPktAck.cs
//  Author: Cheng Xia
//  2013-1-13

using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Base;

using Game.Network;


/// <summary>
/// 好友列表应答数据
/// </summary>
public class FriendGetListPktAck : HTTPPacketAck
{
    public List<FriendData> m_lstFriendData = new List<FriendData>();

//     public FriendGetListPktAck()
//     {
//         this.m_strAction = PACKET_DEFINE.FRIEND_GETLIST_REQ;
//     }
}

// /// <summary>
// /// 好友列表应答工厂类
// /// </summary>
// public class FriendGetListPktAckFactory : HTTPPacketFactory
// {
//     /// <summary>
//     /// 获取action
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.FRIEND_GETLIST_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(IJSonObject json)
//     {
//         FriendGetListPktAck ack = PACKET_HEAD.PACKET_ACK_HEAD<FriendGetListPktAck>(json);

//         if (ack.header.code != 0)
//         {
//             GAME_LOG.ERROR("Error . code desc " + ack.header.desc);
//             return ack;
//         }

//         IJSonObject data = json["data"];
//         foreach (IJSonObject item in data.ArrayItems)
//         {
//             FriendData fd = new FriendData();
//             fd.m_iPID = item["pid"].Int32Value;
//             fd.m_strNickName = item["nickname"].ToString();
//             fd.m_iRoloLevel = item["lv"].Int32Value;
//             fd.m_lstWantGift[0] = item["want_item1"].Int32Value;
//             fd.m_lstWantGift[1] = item["want_item2"].Int32Value;
//             fd.m_lstWantGift[2] = item["want_item3"].Int32Value;
//             fd.m_strSignature = item["signature"].ToString();
//             fd.m_iLike = item["like"].Int32Value;
//             fd.m_iLoginTime = item["login_time"].Int64Value;
//             fd.m_iAthleticsLevel = item["pvp_point"].Int32Value;
//             fd.m_iHeroTableID = item["hero_id"].Int32Value;
//             fd.m_iHeroLv = item["hero_lv"].Int32Value;
//             fd.m_iHeroHp = (int)item["hero_hp"].SingleValue; 
//             fd.m_iAttack = (int)item["hero_attack"].SingleValue;
//             fd.m_iDefend = (int)item["hero_defend"].SingleValue;
//             fd.m_iRecover = (int)item["hero_recover"].SingleValue;
//             fd.m_iSendTime = item["give_time"].Int64Value;
//             ack.m_lstFriendData.Add(fd);
//         }

//         return ack;
//     }
// }
