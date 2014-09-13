using System;
using System.Collections.Generic;
using CodeTitans.JSon;
using Game.Network;

//  FriendPointSummonPktAck.cs
//  Author: sanvey
//  2013-12-17


/// <summary>
/// 友情点召唤应答数据包
/// </summary>
public class FriendPointSummonPktAck : HTTPPacketBase
{
    public int m_iFriendPiont;  //友情点
    public HeroData m_cHero;

    /// <summary>
    /// 玩家英雄数据类
    /// </summary>
    public class HeroData
    {
        public int m_iID;    // 英雄id
        public int m_iTableID;    // 配置表id
        public int m_iLevel;    // 英雄等级
        public int m_iCurrenExp;    // 英雄经验
        public int m_lGetTime;    // 英雄创建时间
        public int m_iHp;    // 英雄血量
        public int m_iAttack;    // 英雄攻击力
        public int m_iDefense;    // 英雄防御力
        public int m_iRevert;    // 英雄恢复力
        public int m_iBBSkillLevel;    //英雄BB技能
        public int m_eGrowType;    // 英雄成长类型
        public int m_iEquipId;
        public int m_iLock;  //锁定
    }

    public FriendPointSummonPktAck()
    {
        this.m_strAction = PACKET_DEFINE.FRIENDPOINT_SUMMON_REQ;
    }
}


/// <summary>
/// 友情点召唤应答数据包工厂类
/// </summary>
public class FriendPointSummonPktAckFactory : HTTPPacketFactory
{

    /// <summary>
    /// 获取数据包Action
    /// </summary>
    /// <returns></returns>
    public override string GetPacketAction()
    {
        return PACKET_DEFINE.FRIENDPOINT_SUMMON_REQ;
    }

    /// <summary>
    /// 创建数据包
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public override HTTPPacketBase Create(IJSonObject json)
    {
        FriendPointSummonPktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<FriendPointSummonPktAck>(json);

        if (pkt.m_iErrorCode != 0)
        {
            return pkt;
        }

        IJSonObject data = json["data"];

        pkt.m_iFriendPiont = data["friend_point"].Int32Value;
        IJSonObject item = data["hero"];
        pkt.m_cHero = new FriendPointSummonPktAck.HeroData();
        pkt.m_cHero.m_iID = item["id"].Int32Value;
        pkt.m_cHero.m_iTableID = item["hero_id"].Int32Value;
        pkt.m_cHero.m_iLevel = item["lv"].Int32Value;
        pkt.m_cHero.m_iCurrenExp = item["exp"].Int32Value;
        pkt.m_cHero.m_lGetTime = item["create_time"].Int32Value;
        pkt.m_cHero.m_iHp = (int)(item["hp"].SingleValue);
        pkt.m_cHero.m_iAttack = (int)(item["attack"].SingleValue);
        pkt.m_cHero.m_iDefense = (int)(item["defend"].SingleValue);
        pkt.m_cHero.m_iRevert = (int)(item["recover"].SingleValue);
        pkt.m_cHero.m_iBBSkillLevel = item["bb_level"].Int32Value;
        pkt.m_cHero.m_eGrowType = item["grow_type"].Int32Value; ;
        pkt.m_cHero.m_iEquipId = item["equip_id"].Int32Value;
        pkt.m_cHero.m_iLock = item["lock"].Int32Value;

        return pkt;
    }
}
