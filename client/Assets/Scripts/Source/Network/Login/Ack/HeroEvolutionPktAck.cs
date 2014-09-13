//  HeroEvolutionPktAck.cs
//  Author: Cheng Xia
//  2013-12-25

using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Base;

using Game.Network;

/// <summary>
/// 英雄升级应答数据
/// </summary>
public class HeroEvolutionPktAck : HTTPPacketAck
{

    public int m_iGold;
    public HeroData m_cAfterHero;
    public List<int> m_lstDeleteHeros = new List<int>();  //强化素材需要删除

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


    // public HeroEvolutionPktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.HERO_EVOLUTION_REQ;
    // }
}


// /// <summary>
// /// 英雄升级应答工厂类
// /// </summary>
// public class HeroEvolutionPktAckFactory : HTTPPacketFactory
// {
//     /// <summary>
//     /// 获取action
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.HERO_EVOLUTION_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(IJSonObject json)
//     {
//         HeroEvolutionPktAck ack = PACKET_HEAD.PACKET_ACK_HEAD<HeroEvolutionPktAck>(json);

//         if (ack.m_iErrorCode != 0)
//         {
//             GAME_LOG.ERROR("Error . code desc " + ack.m_strErrorDes);
//             return ack;
//         }

//         IJSonObject data = json["data"];
//         //金钱获得
//         ack.m_iGold = data["cur_gold"].Int32Value;
//         //需要删除的英雄素材
//         IJSonObject deleteHeros = data["sacrifice_heros"];
//         ack.m_lstDeleteHeros = new List<int>();
//         foreach (IJSonObject hero in deleteHeros.ArrayItems)
//         {
//             ack.m_lstDeleteHeros.Add(hero.Int32Value);
//         }
//         //进化后的英雄
//         IJSonObject heroAfter = data["evo_hero"];
//         ack.m_cAfterHero = new HeroEvolutionPktAck.HeroData();
//         ack.m_cAfterHero.m_iID = heroAfter["id"].Int32Value;
//         ack.m_cAfterHero.m_iTableID = heroAfter["hero_id"].Int32Value;
//         ack.m_cAfterHero.m_iLevel = heroAfter["lv"].Int32Value;
//         ack.m_cAfterHero.m_iCurrenExp = heroAfter["exp"].Int32Value;
//         ack.m_cAfterHero.m_lGetTime = heroAfter["create_time"].Int32Value;
//         ack.m_cAfterHero.m_iHp = (int)(heroAfter["hp"].SingleValue);
//         ack.m_cAfterHero.m_iAttack = (int)(heroAfter["attack"].SingleValue);
//         ack.m_cAfterHero.m_iDefense = (int)(heroAfter["defend"].SingleValue);
//         ack.m_cAfterHero.m_iRevert = (int)(heroAfter["recover"].SingleValue);
//         ack.m_cAfterHero.m_iBBSkillLevel = heroAfter["bb_level"].Int32Value;
//         ack.m_cAfterHero.m_eGrowType = heroAfter["grow_type"].Int32Value;
//         ack.m_cAfterHero.m_iEquipId = heroAfter["equip_id"].Int32Value;
//         ack.m_cAfterHero.m_iLock = heroAfter["lock"].Int32Value;

//         return ack;
//     }
// }