//  HeroUpgradePktAck.cs
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
public class HeroUpgradePktAck : HTTPPacketAck
{
    public UpgradeSucessType m_iSuccessType;  //成功类型
    public int m_iGold;  //金钱

    public List<int> m_lstDeleteHeros = new List<int>();  //强化素材需要删除

    public HeroData m_cAfterHero;  //进化后的英雄

    public List<UpgradeAttribute> m_upgradeProcess = new List<UpgradeAttribute>();  //强化过程

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

    // public HeroUpgradePktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.HERO_UPGRADE_REQ;
    // }
}


// /// <summary>
// /// 英雄升级应答工厂类
// /// </summary>
// public class HeroUpgradePktAckFactory : HTTPPacketFactory
// {
//     /// <summary>
//     /// 获取action
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.HERO_UPGRADE_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(IJSonObject json)
//     {
//         HeroUpgradePktAck ack = PACKET_HEAD.PACKET_ACK_HEAD<HeroUpgradePktAck>(json);

//         if (ack.header.code != 0)
//         {
//             return ack;
//         }

//         IJSonObject data = json["data"];

//         //强化成功类型
//         ack.m_iSuccessType = (UpgradeSucessType)data["success_type"].Int32Value;
//         //金钱
//         ack.m_iGold = data["gold"].Int32Value;
//         //需要删除的英雄素材
//         IJSonObject deleteHeros = data["sacrifice_heros"];
//         ack.m_lstDeleteHeros = new List<int>();
//         foreach (IJSonObject hero in deleteHeros.ArrayItems)
//         {
//             ack.m_lstDeleteHeros.Add(hero.Int32Value);
//         }
//         //强化后的英雄
//         IJSonObject heroAfter = data["after_strength_hero"];
//         ack.m_cAfterHero = new HeroUpgradePktAck.HeroData();
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
//         //强化过程
//         IJSonObject strengthProcess = data["strength_process"];
//         ack.m_upgradeProcess = new List<UpgradeAttribute>();
//         foreach (IJSonObject item in strengthProcess.ArrayItems)
//         {
//             UpgradeAttribute upgradeAttribute = new UpgradeAttribute();
//             upgradeAttribute.m_iLv = item["lv"].Int32Value;
//             upgradeAttribute.m_iExp = item["exp"].Int32Value;
//             upgradeAttribute.m_iHp = (int)item["hp"].SingleValue;
//             upgradeAttribute.m_iAttack = (int)item["attack"].SingleValue;
//             upgradeAttribute.m_iDefend = (int)item["defend"].SingleValue;
//             upgradeAttribute.m_iRecover = (int)item["recover"].SingleValue;
//             upgradeAttribute.m_iBBLv = item["bb_level"].Int32Value;

//             ack.m_upgradeProcess.Add(upgradeAttribute);
//         }

//         return ack;
//     }
// }

/// <summary>
/// 英雄强化过程属性
/// </summary>
public class UpgradeAttribute
{
    public int m_iLv;
    public int m_iHp;
    public int m_iBBLv;
    public int m_iAttack;
    public int m_iDefend;
    public int m_iRecover;
    public int m_iExp;
}

/// <summary>
/// 英雄强化成功类型
/// </summary>
public enum UpgradeSucessType
{
    /// <summary>
    /// 成功
    /// </summary>
    Nomal = 0,
    /// <summary>
    /// 大成功
    /// </summary>
    BigSuccess = 1,
    /// <summary>
    /// 超成功
    /// </summary>
    ExtraSuccess = 2,
}