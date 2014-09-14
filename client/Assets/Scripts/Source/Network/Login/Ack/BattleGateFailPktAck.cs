using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;


//  BattleGateFailPktAck.cs
//  Author: Lu Zexi
//  2014-03-05



/// <summary>
/// 战斗关卡失败应答
/// </summary>
public class BattleGateFailPktAck : HTTPPacketAck
{
    public List<ItemData> m_lstUpdateItem = new List<ItemData>();   //更新物品
    public List<int> m_lstDelItem = new List<int>();  //删除物品

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

    // public BattleGateFailPktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.BATTLE_GATE_FAIL_REQ;
    // }
}


// /// <summary>
// /// 战斗关卡应答工厂类
// /// </summary>
// public class BattleGateFailPktAckFactory : HTTPPacketFactory
// {
//     public BattleGateFailPktAckFactory()
//     { 
//         //
//     }

//     /// <summary>
//     /// 获取数据包action
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.BATTLE_GATE_FAIL_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(IJSonObject json)
//     {
//         BattleGateFailPktAck ack = PACKET_HEAD.PACKET_ACK_HEAD<BattleGateFailPktAck>(json);

//         if (ack.header.code != 0)
//         {
//             return ack;
//         }

//         //
//         IJSonObject data = json["data"];

//         ack.m_cPlayerValidate = new PlayerValidater(data["player_validater"]);

//         IJSonObject ready_update_item = data["update_item"];
//         IJSonObject update_item = ready_update_item["updateItem"];
//         IJSonObject del_item = ready_update_item["delItem"];

//         for (int i = 0; i < update_item.Count; i++)
//         {
//             IJSonObject item = update_item[i];
//             BattleGateFailPktAck.ItemData itemData = new BattleGateFailPktAck.ItemData();
//             itemData.Parse(item);
//             ack.m_lstUpdateItem.Add(itemData);
//         }
//         for (int i = 0; i < del_item.Count; i++)
//         {
//             ack.m_lstDelItem.Add(del_item[i].Int32Value);
//         }

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
//                 ack.m_lstBattleFriendEx.Add(battleFriend);
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
//             ack.m_lstBattleFriend.Add(battleFriend);
//         }

//         return ack;
//     }
// }
