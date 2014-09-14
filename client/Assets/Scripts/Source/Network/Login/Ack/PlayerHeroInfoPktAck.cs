using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Network;

//获取玩家英雄列表应答数据包

public class PlayerHeroInfoPktAck : HTTPPacketAck
{
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


    public List<HeroData> heros = new List<HeroData>();//获取的英雄列表

    // public PlayerHeroInfoPktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.GET_PALYERHEROINFO_REQ;
    // }
}

// /// <summary>
// /// 获取玩家英雄列表信息工厂类
// /// </summary>
// public class PlayerHeroInfoPktAckFactory : HTTPPacketFactory
// {
//     /// <summary>
//     /// 获取数据包Action
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.GET_PALYERHEROINFO_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(IJSonObject json)
//     {
//         PlayerHeroInfoPktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<PlayerHeroInfoPktAck>(json);

//         if (pkt.header.code != 0)
//         {
//             return pkt;
//         }

//         var data = json["data"].ArrayItems;
//         foreach (var item in data)
//         {
//             PlayerHeroInfoPktAck.HeroData tmpHero = new PlayerHeroInfoPktAck.HeroData();
//             tmpHero.m_iID = item["id"].Int32Value;
//             tmpHero.m_iTableID = item["hero_id"].Int32Value;
//             tmpHero.m_iLevel = item["lv"].Int32Value;
//             tmpHero.m_iCurrenExp = item["exp"].Int32Value;
//             tmpHero.m_lGetTime = item["create_time"].Int32Value;
//             tmpHero.m_iHp = (int)(item["hp"].SingleValue);
//             tmpHero.m_iAttack = (int)(item["attack"].SingleValue);
//             tmpHero.m_iDefense = (int)(item["defend"].SingleValue);
//             tmpHero.m_iRevert = (int)(item["recover"].SingleValue);
//             tmpHero.m_iBBSkillLevel = item["bb_level"].Int32Value;
//             tmpHero.m_eGrowType = item["grow_type"].Int32Value;
//             tmpHero.m_iEquipId = item["equip_id"].Int32Value;
//             tmpHero.m_iLock = item["lock"].Int32Value;
//             pkt.heros.Add(tmpHero);
//         }

//         return pkt;
//     }
// }
