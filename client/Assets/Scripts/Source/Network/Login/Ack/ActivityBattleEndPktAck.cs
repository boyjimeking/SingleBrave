using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeTitans.JSon;
using Game.Network;

//  ActivityBattleEndPktAck.cs
//  Author: Lu Zexi
//  2014-01-08




/// <summary>
/// 活动战斗结束应答数据包
/// </summary>
public class ActivityBattleEndPktAck : HTTPPacketBase
{
    public int m_iExp;  //经验
    public int m_iLevel;    //等级
    public int m_iHP;   //体力
    public int m_iFriendPoint;  //友情点
    public int m_iGold; //金币
    public int m_iFarm; //农场点
    public int m_iDiamond;  //砖石
    public int m_iSportPoint;//竞技点
    public List<HeroData> m_lstHero = new List<HeroData>(); //获得的英雄
    public List<ItemData> m_lstItem = new List<ItemData>(); //获得的物品
    public int[] m_vecReadyItem = new int[5];
    public int[] m_vecReadyItemNum = new int[5];
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

        public void Parse(IJSonObject json)
        {
            this.m_iID = json["id"].Int32Value;
            this.m_iTableID = json["item_id"].Int32Value;
            this.m_iNum = json["item_num"].Int32Value;
        }
    }

    /// <summary>
    /// 获得英雄
    /// </summary>
    public class HeroData
    {
        public int m_iID;   //ID
        public int m_iTableID;  //配置表ID
        public int m_iLevel;    //等级
        public int m_iExp;  //经验
        public int m_iHp;   //HP
        public int m_iAttack;   //攻击
        public int m_iDefence;  //防御
        public int m_iRecover;  //回复
        public int m_iBBLevel;  //BB技能
        public int m_iGrowType; //成长类型
        public int m_iEquipID;  //装备ID
        public long m_lCreateTime;   //创建时间

        public void Parse(IJSonObject json)
        {
            this.m_iID = json["id"].Int32Value;
            this.m_iTableID = json["hero_id"].Int32Value;
            this.m_iLevel = json["lv"].Int32Value;
            this.m_iExp = json["exp"].Int32Value;
            this.m_iHp = json["hp"].Int32Value;
            this.m_iAttack = json["attack"].Int32Value;
            this.m_iDefence = json["defend"].Int32Value;
            this.m_iRecover = json["recover"].Int32Value;
            this.m_iBBLevel = json["bb_level"].Int32Value;
            this.m_iGrowType = json["grow_type"].Int32Value;
            this.m_iEquipID = json["equip_id"].Int32Value;
            this.m_lCreateTime = json["create_time"].Int64Value;
        }

    }

    public ActivityBattleEndPktAck()
    {
        this.m_strAction = PACKET_DEFINE.ACTIVITY_BATTLE_END_REQ;
    }

}



/// <summary>
/// 活动战斗结束数据包工厂类
/// </summary>
public class ActivityBattleEndPktFactory : HTTPPacketFactory
{
    /// <summary>
    /// 获取action
    /// </summary>
    /// <returns></returns>
    public override string GetPacketAction()
    {
        return PACKET_DEFINE.ACTIVITY_BATTLE_END_REQ;
    }

    /// <summary>
    /// 创建数据包
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public override HTTPPacketBase Create(IJSonObject json)
    {
        ActivityBattleEndPktAck ack = PACKET_HEAD.PACKET_ACK_HEAD<ActivityBattleEndPktAck>(json);

        if (ack.m_iErrorCode != 0)
        {
            return ack;
        }

        IJSonObject data = json["data"];
        ack.m_iExp = data["cur_exp"].Int32Value;
        ack.m_iLevel = data["cur_lv"].Int32Value;
        ack.m_iHP = data["strength"].Int32Value;
        ack.m_iFriendPoint = data["cur_friend_point"].Int32Value;
        ack.m_iGold = data["get_gold"].Int32Value;
        ack.m_iFarm = data["get_farm"].Int32Value;
        ack.m_iDiamond = data["get_diamond"].Int32Value;
        ack.m_iSportPoint = data["sport_point"].Int32Value;

        ack.m_cPlayerValidate = new PlayerValidater(data["player_validater"]);

        IJSonObject hero = data["get_heros"];

        ack.m_lstHero.Clear();
        foreach ( IJSonObject item in hero.ArrayItems)
        {
            if (item != null)
            {
                ActivityBattleEndPktAck.HeroData heroData = new ActivityBattleEndPktAck.HeroData();
                heroData.Parse(item);
                ack.m_lstHero.Add(heroData);
            }
        }

        IJSonObject getitem = data["get_items"];
        ack.m_lstItem.Clear();

        foreach (IJSonObject item in getitem.ArrayItems)
        {
            if (item != null)
            {
                ActivityBattleEndPktAck.ItemData itemData = new ActivityBattleEndPktAck.ItemData();
                itemData.Parse(item);
                ack.m_lstItem.Add(itemData);
            }
        }
        for (int i = 0; i < 5; i++)
        {
            ack.m_vecReadyItem[i] = data["ready_items"]["pos" + i].Int32Value;
            ack.m_vecReadyItemNum[i] = data["ready_items"]["pos" + i + "_n"].Int32Value;
        }

        IJSonObject ready_update_item = data["update_item"];
        IJSonObject update_item = ready_update_item["updateItem"];
        IJSonObject del_item = ready_update_item["delItem"];

        for (int i = 0; i < update_item.Count; i++)
        {
            IJSonObject item = update_item[i];
            ActivityBattleEndPktAck.ItemData itemData = new ActivityBattleEndPktAck.ItemData();
            itemData.Parse(item);
            ack.m_lstUpdateItem.Add(itemData);
        }
        for (int i = 0; i < del_item.Count; i++)
        {
            ack.m_lstDelItem.Add(del_item[i].Int32Value);
        }

        IJSonObject player = data["helpers"]["player"];
        IJSonObject friend = data["helpers"]["friend"];

        if (friend.Count > 0)
        {
            foreach (var item in friend.ArrayItems)
            {
                BattleFriend battleFriend = new BattleFriend();
                battleFriend.m_iID = item["pid"].Int32Value;
                battleFriend.m_strName = item["nickname"].StringValue;
                battleFriend.m_iLevel = item["lv"].Int32Value;
                battleFriend.m_iFriendPoint = item["friendPoint"].Int32Value;
                battleFriend.m_strSign = item["signature"].StringValue;
                battleFriend.m_PvpExp = item["pvp_point"].Int32Value;
                var heroData = item["hero"];
                if (heroData != null)
                {
                    Hero tmpHero = new Hero(heroData["hero_id"].Int32Value);
                    tmpHero.m_iID = heroData["id"].Int32Value;
                    tmpHero.m_iTableID = heroData["hero_id"].Int32Value;
                    tmpHero.m_iLevel = heroData["lv"].Int32Value;
                    tmpHero.m_iCurrenExp = heroData["exp"].Int32Value;
                    tmpHero.m_lGetTime = heroData["create_time"].Int32Value;
                    tmpHero.m_iHp = (int)heroData["hp"].SingleValue;
                    tmpHero.m_iAttack = (int)heroData["attack"].SingleValue;
                    tmpHero.m_iDefence = (int)heroData["defend"].SingleValue;
                    tmpHero.m_iRevert = (int)heroData["recover"].SingleValue;
                    tmpHero.m_iBBSkillLevel = heroData["bb_level"].Int32Value;
                    tmpHero.m_eGrowType = (GrowType)heroData["grow_type"].Int32Value;
                    tmpHero.m_iEquipID = heroData["equip_id"].Int32Value;
                    battleFriend.m_cLeaderHero = tmpHero;
                }
                battleFriend.m_bIsFriend = true;
                battleFriend.m_iEquipItemTableID = item["equip_item_id"].Int32Value;
                ack.m_lstBattleFriendEx.Add(battleFriend);
            }
        }

        foreach (var item in player.ArrayItems)
        {
            BattleFriend battleFriend = new BattleFriend();
            battleFriend.m_iID = item["pid"].Int32Value;
            battleFriend.m_strName = item["nickname"].StringValue;
            battleFriend.m_iLevel = item["lv"].Int32Value;
            battleFriend.m_iFriendPoint = item["friendPoint"].Int32Value;
            battleFriend.m_strSign = item["signature"].StringValue;
            battleFriend.m_PvpExp = item["pvp_point"].Int32Value;
            var heroData = item["hero"];
            if (heroData != null)
            {
                Hero tmpHero = new Hero(heroData["hero_id"].Int32Value);
                tmpHero.m_iID = heroData["id"].Int32Value;
                tmpHero.m_iTableID = heroData["hero_id"].Int32Value;
                tmpHero.m_iLevel = heroData["lv"].Int32Value;
                tmpHero.m_iCurrenExp = heroData["exp"].Int32Value;
                tmpHero.m_lGetTime = heroData["create_time"].Int32Value;
                tmpHero.m_iHp = (int)heroData["hp"].SingleValue;
                tmpHero.m_iAttack = (int)heroData["attack"].SingleValue;
                tmpHero.m_iDefence = (int)heroData["defend"].SingleValue;
                tmpHero.m_iRevert = (int)heroData["recover"].SingleValue;
                tmpHero.m_iBBSkillLevel = heroData["bb_level"].Int32Value;
                tmpHero.m_eGrowType = (GrowType)heroData["grow_type"].Int32Value;
                tmpHero.m_iEquipID = heroData["equip_id"].Int32Value;
                battleFriend.m_cLeaderHero = tmpHero;
            }
            battleFriend.m_bIsFriend = false;
            battleFriend.m_iEquipItemTableID = item["equip_item_id"].Int32Value;
            ack.m_lstBattleFriend.Add(battleFriend);
        }

        return ack;
    }
}