using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;



//  PVPBattleEndPktAck.cs
//  Author: Lu Zexi
//  2014-02-11




/// <summary>
/// PVP战斗结束
/// </summary>
public class PVPBattleEndPktAck : HTTPPacketAck
{
    public int m_iPvpPoint; //PVP点
    public int m_iPvpWeekPoint;  //pvp周积分
    public int m_iWinNum; //胜利数
    public int m_iLoseNum;  //失败数
    public int m_iMyWeekRank;  //我的周排行
    public int m_iPVPMaxPoint;
    public List<PVPItem> m_lstWeekRank = new List<PVPItem>();  //所有排名
    public bool m_bHasNewRecord;    //是否有奖励
    public int m_iRewardDiamond;    //砖石奖励
    public ItemData m_cRewardItem;  //奖励物品

    public List<BattleFriend> m_lstBattleFriend = new List<BattleFriend>();//战友列表
    public List<BattleFriend> m_lstBattleFriendEx = new List<BattleFriend>();//好友战友列表

    public PlayerValidater m_cPlayerValidate;

    /// <summary>
    /// 物品数量
    /// </summary>
    public class ItemData
    {
        public int m_iID;   //ID
        public int m_iTableID;  //配置ID
        public int m_iNum;  //数量

//        public void Parse(IJSonObject json)
//        {
//            this.m_iID = json["id"].Int32Value;
//            this.m_iTableID = json["item_id"].Int32Value;
//            this.m_iNum = json["item_num"].Int32Value;
//        }
    }

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


    // public PVPBattleEndPktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.PVP_BATTLE_END_REQ;
    // }
}


// /// <summary>
// /// PVP战斗结束数据工厂
// /// </summary>
// public class PVPBattleEndPktAckFactory : HTTPPacketFactory
// {
//     /// <summary>
//     /// 获取ACTION
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.PVP_BATTLE_END_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(IJSonObject json)
//     {
//         PVPBattleEndPktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<PVPBattleEndPktAck>(json);

//         if (pkt.header.code != 0)
//         {
//             return pkt;
//         }

//         IJSonObject data = json["data"];

//         pkt.m_iPvpPoint = data["pvp_point"].Int32Value;
//         pkt.m_iWinNum = data["win_num"].Int32Value;
//         pkt.m_iLoseNum = data["lose_num"].Int32Value;
//         pkt.m_bHasNewRecord = data["is_reward"].BooleanValue;
//         pkt.m_iPVPMaxPoint = data["pvp_max_point"].Int32Value;
//         pkt.m_iPvpWeekPoint = data["pvp_weekpoint"].Int32Value;

//         pkt.m_cPlayerValidate = new PlayerValidater(data["player_validater"]);

//         IJSonObject rewardData = data["reward_res"];
//         pkt.m_iRewardDiamond = rewardData["diamond"].Int32Value;
//         pkt.m_cRewardItem = null;
//         if (rewardData["reward_item"].Count > 0)
//         {
//             PVPBattleEndPktAck.ItemData itemData = new PVPBattleEndPktAck.ItemData();
//             itemData.Parse(rewardData["reward_item"][0]);
//             pkt.m_cRewardItem = itemData;
//         }

//         IJSonObject rank = data["week_rank"];

//         IEnumerable<IJSonObject> allrank = rank["rank"].ArrayItems;
//         pkt.m_lstWeekRank = new List<PVPBattleEndPktAck.PVPItem>();

//         //所有排行
//         foreach (IJSonObject item in allrank)
//         {
//             PVPBattleEndPktAck.PVPItem tmp = new PVPBattleEndPktAck.PVPItem();
//             tmp.m_strName = item["name"].StringValue;
//             tmp.m_iHeroTableID = item["hero_id"].Int32Value;
//             tmp.m_iHeroLv = item["lv"].Int32Value;
//             tmp.m_iPoint = item["pvp_point"].Int32Value;
//             tmp.m_iWinNum = item["win_num"].Int32Value;
//             tmp.m_iLoseNum = item["lose_num"].Int32Value;

//             pkt.m_lstWeekRank.Add(tmp);
//         }

//         pkt.m_iMyWeekRank = rank["playerRank"].Int32Value;

//         IJSonObject player = json["data"]["helpers"]["player"];
//         IJSonObject friend = json["data"]["helpers"]["friend"];

//         if (friend.Count > 0)
//         {
//             foreach (var item in friend.ArrayItems)
//             {
//                 BattleFriend battleFriend = new BattleFriend();
//                 battleFriend.m_iID = item["pid"].Int32Value;
//                 battleFriend.m_strName = item["nickname"].StringValue;
//                 battleFriend.m_iLevel = item["lv"].Int32Value;
//                 battleFriend.m_iFriendPoint = item["friendPoint"].Int32Value;
//                 battleFriend.m_strSign = item["signature"].StringValue;
//                 battleFriend.m_PvpExp = item["pvp_point"].Int32Value;
//                 var heroData = item["hero"];
//                 if (heroData != null)
//                 {
//                     Hero tmpHero = new Hero(heroData["hero_id"].Int32Value);
//                     tmpHero.m_iID = heroData["id"].Int32Value;
//                     tmpHero.m_iTableID = heroData["hero_id"].Int32Value;
//                     tmpHero.m_iLevel = heroData["lv"].Int32Value;
//                     tmpHero.m_iCurrenExp = heroData["exp"].Int32Value;
//                     tmpHero.m_lGetTime = heroData["create_time"].Int32Value;
//                     tmpHero.m_iHp = (int)heroData["hp"].SingleValue;
//                     tmpHero.m_iAttack = (int)heroData["attack"].SingleValue;
//                     tmpHero.m_iDefence = (int)heroData["defend"].SingleValue;
//                     tmpHero.m_iRevert = (int)heroData["recover"].SingleValue;
//                     tmpHero.m_iBBSkillLevel = heroData["bb_level"].Int32Value;
//                     tmpHero.m_eGrowType = (GrowType)heroData["grow_type"].Int32Value;
//                     tmpHero.m_iEquipID = heroData["equip_id"].Int32Value;
//                     battleFriend.m_cLeaderHero = tmpHero;
//                 }
//                 battleFriend.m_bIsFriend = true;
//                 battleFriend.m_iEquipItemTableID = item["equip_item_id"].Int32Value;
//                 pkt.m_lstBattleFriendEx.Add(battleFriend);
//             }
//         }

//         foreach (var item in player.ArrayItems)
//         {
//             BattleFriend battleFriend = new BattleFriend();
//             battleFriend.m_iID = item["pid"].Int32Value;
//             battleFriend.m_strName = item["nickname"].StringValue;
//             battleFriend.m_iLevel = item["lv"].Int32Value;
//             battleFriend.m_iFriendPoint = item["friendPoint"].Int32Value;
//             battleFriend.m_strSign = item["signature"].StringValue;
//             battleFriend.m_PvpExp = item["pvp_point"].Int32Value;
//             var heroData = item["hero"];
//             if (heroData != null)
//             {
//                 Hero tmpHero = new Hero(heroData["hero_id"].Int32Value);
//                 tmpHero.m_iID = heroData["id"].Int32Value;
//                 tmpHero.m_iTableID = heroData["hero_id"].Int32Value;
//                 tmpHero.m_iLevel = heroData["lv"].Int32Value;
//                 tmpHero.m_iCurrenExp = heroData["exp"].Int32Value;
//                 tmpHero.m_lGetTime = heroData["create_time"].Int32Value;
//                 tmpHero.m_iHp = (int)heroData["hp"].SingleValue;
//                 tmpHero.m_iAttack = (int)heroData["attack"].SingleValue;
//                 tmpHero.m_iDefence = (int)heroData["defend"].SingleValue;
//                 tmpHero.m_iRevert = (int)heroData["recover"].SingleValue;
//                 tmpHero.m_iBBSkillLevel = heroData["bb_level"].Int32Value;
//                 tmpHero.m_eGrowType = (GrowType)heroData["grow_type"].Int32Value;
//                 tmpHero.m_iEquipID = heroData["equip_id"].Int32Value;
//                 battleFriend.m_cLeaderHero = tmpHero;
//             }
//             battleFriend.m_bIsFriend = false;
//             battleFriend.m_iEquipItemTableID = item["equip_item_id"].Int32Value;
//             pkt.m_lstBattleFriend.Add(battleFriend);
//         }

//         return pkt;
//     }
// }