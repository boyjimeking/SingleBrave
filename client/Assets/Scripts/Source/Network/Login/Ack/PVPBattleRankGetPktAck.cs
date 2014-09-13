using System;
using System.Collections.Generic;
using CodeTitans.JSon;
using Game.Network;
using UnityEngine;

//  PVPBattleRankGetPktAck.cs
//  Author: sanvey
//  2014-2-8


/// <summary>
/// 竞技场排名信息获得
/// </summary>
public class PVPBattleRankGetPktAck : HTTPPacketBase
{
    public List<PVPItem> AllRank = new List<PVPItem>();  //所有排名
    public List<PVPItem> FriendRank = new List<PVPItem>();//好友排名
    public List<PVPItem> WeekRank = new List<PVPItem>();  //周排名
    public int m_iMyRankAll;  //我的全服排行
    public int m_iMyRankFriend;  //我的好友排行
    public int m_iMyRankWeek;  //我的周排行

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

        public int m_iPointForWeek;
        public int m_iWinNumForWeek;
        public int m_iLoseNumForWeek;
    }

    public PVPBattleRankGetPktAck()
    {
        this.m_strAction = PACKET_DEFINE.PVP_BATTLE_RANK_REQ;
    }
}


/// <summary>
/// 竞技场基本信息获得数据包工厂类
/// </summary>
public class PVPBattleRankGetPktAckAckFactory : HTTPPacketFactory
{

    /// <summary>
    /// 获取数据包Action
    /// </summary>
    /// <returns></returns>
    public override string GetPacketAction()
    {
        return PACKET_DEFINE.PVP_BATTLE_RANK_REQ;
    }

    /// <summary>
    /// 创建数据包
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public override HTTPPacketBase Create(IJSonObject json)
    {
        PVPBattleRankGetPktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<PVPBattleRankGetPktAck>(json);

        if (pkt.m_iErrorCode != 0)
        {
            return pkt;
        }

        IJSonObject data = json["data"];

        IEnumerable<IJSonObject> allrank = data["rank"].ArrayItems;
        IEnumerable<IJSonObject> friendrank = data["rankFriend"].ArrayItems;
        IEnumerable<IJSonObject> weekrank = data["rankWeek"].ArrayItems;
        pkt.m_iMyRankAll = data["playerRank"].Int32Value;
        pkt.m_iMyRankFriend = data["friendRank"].Int32Value;
        pkt.m_iMyRankWeek = data["weekRank"].Int32Value;

        //所有排行
        foreach (IJSonObject item in allrank)
        {
            PVPBattleRankGetPktAck.PVPItem tmp = new PVPBattleRankGetPktAck.PVPItem();
            tmp.m_strName = item["name"].StringValue;
            tmp.m_iHeroTableID = item["hero_id"].Int32Value;
            tmp.m_iHeroLv = item["lv"].Int32Value;
            tmp.m_iPoint = item["pvp_point"].Int32Value;
            tmp.m_iWinNum = item["win_num"].Int32Value;
            tmp.m_iLoseNum = item["lose_num"].Int32Value;

            pkt.AllRank.Add(tmp);
        }

        //好友排行
        foreach (IJSonObject item in friendrank)
        {
            PVPBattleRankGetPktAck.PVPItem tmp = new PVPBattleRankGetPktAck.PVPItem();
            tmp.m_strName = item["name"].StringValue;
            tmp.m_iHeroTableID = item["hero_id"].Int32Value;
            tmp.m_iHeroLv = item["lv"].Int32Value;
            tmp.m_iPoint = item["pvp_point"].Int32Value;
            tmp.m_iWinNum = item["win_num"].Int32Value;
            tmp.m_iLoseNum = item["lose_num"].Int32Value;

            pkt.FriendRank.Add(tmp);
        }

        //周排行
        foreach (IJSonObject item in weekrank)
        {
            PVPBattleRankGetPktAck.PVPItem tmp = new PVPBattleRankGetPktAck.PVPItem();
            tmp.m_strName = item["name"].StringValue;
            tmp.m_iHeroTableID = item["hero_id"].Int32Value;
            tmp.m_iHeroLv = item["lv"].Int32Value;
            tmp.m_iPoint = item["pvp_weekpoint"].Int32Value;
            tmp.m_iWinNum = item["win_num"].Int32Value;
            tmp.m_iLoseNum = item["lose_num"].Int32Value;
            tmp.m_iPointForWeek = item["pvp_point"].Int32Value;
            tmp.m_iWinNumForWeek = item["week_win_num"].Int32Value;
            tmp.m_iLoseNumForWeek = item["week_lose_num"].Int32Value;

            pkt.WeekRank.Add(tmp);
        }

        return pkt;
    }
}