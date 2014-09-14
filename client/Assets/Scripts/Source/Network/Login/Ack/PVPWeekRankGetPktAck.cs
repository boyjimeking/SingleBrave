using System;
using System.Collections.Generic;

using Game.Network;
using UnityEngine;

//  PVPWeekRankGetPktAck.cs
//  Author: sanvey
//  2014-2-8


/// <summary>
/// 竞技场周排名信息获得
/// </summary>
public class PVPWeekRankGetPktAck : HTTPPacketAck
{
    public List<PVPItem> m_lstWeekRank = new List<PVPItem>();  //所有排名
    public int m_iMyWeek;

    /// <summary>
    /// 排名显示单项
    /// </summary>
    public class PVPItem
    {
        public string m_strName;
        public int m_iHeroTableID;
        public int m_iHeroLv;
        public int m_iPoint;
        public int m_iWinNum;
        public int m_iLoseNum;
    }

    // public PVPWeekRankGetPktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.PVP_WEEK_RANK_GET_REQ;
    // }
}


// /// <summary>
// /// 竞技场周排名获得数据包工厂类
// /// </summary>
// public class PVPWeekRankGetPktAckFactory : HTTPPacketFactory
// {

//     /// <summary>
//     /// 获取数据包Action
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.PVP_WEEK_RANK_GET_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(IJSonObject json)
//     {
//         PVPWeekRankGetPktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<PVPWeekRankGetPktAck>(json);

//         if (pkt.header.code != 0)
//         {
//             return pkt;
//         }

//         IJSonObject data = json["data"];

//         IEnumerable<IJSonObject> allrank = data["rank"].ArrayItems;
//         pkt.m_lstWeekRank = new List<PVPWeekRankGetPktAck.PVPItem>();

//         //所有排行
//         foreach (IJSonObject item in allrank)
//         {
//             PVPWeekRankGetPktAck.PVPItem tmp = new PVPWeekRankGetPktAck.PVPItem();
//             tmp.m_strName = item["name"].StringValue;
//             tmp.m_iHeroTableID = item["hero_id"].Int32Value;
//             tmp.m_iHeroLv = item["lv"].Int32Value;
//             tmp.m_iPoint = item["pvp_point"].Int32Value;
//             tmp.m_iWinNum = item["win_num"].Int32Value;
//             tmp.m_iLoseNum = item["lose_num"].Int32Value;

//             pkt.m_lstWeekRank.Add(tmp);
//         }

//         pkt.m_iMyWeek = data["playerRank"].Int32Value;

//         return pkt;
//     }
// }