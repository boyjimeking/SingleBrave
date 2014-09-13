using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeTitans.JSon;
using Game.Network;
using UnityEngine;

//获取战友信息应答类
//Author Sunyi
//2013-12-24
public class FriendFightPktAck : HTTPPacketBase
{
    public List<BattleFriend> m_lstBattleFriend = new List<BattleFriend>();//战友列表
    public List<BattleFriend> m_lstBattleFriendEx = new List<BattleFriend>();//好友战友列表

    public FriendFightPktAck() 
    {
        this.m_strAction = PACKET_DEFINE.FRIEND_FIGHT_REQ;
    }
}

public class FriendFightPktAckFactory : HTTPPacketFactory
{
    /// <summary>
    /// 获取数据包Action
    /// </summary>
    /// <returns></returns>
    public override string GetPacketAction()
    {
        return PACKET_DEFINE.FRIEND_FIGHT_REQ;
    }

    /// <summary>
    /// 创建数据包
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public override HTTPPacketBase Create(IJSonObject json)
    {
        FriendFightPktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<FriendFightPktAck>(json);
        if (pkt.m_iErrorCode != 0)
        {
            return pkt;
        }

        IJSonObject data = json["data"];

        if (!data["friend"].IsNull)
        {
            List<IJSonObject> friend = new List<IJSonObject>(data["friend"].ArrayItems);
            if (friend!=null&&friend.Count > 0)
            {
                foreach (var item in friend)
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
                        tmpHero.m_iEquipID = item["equip_item_id"].Int32Value;
                        battleFriend.m_cLeaderHero = tmpHero;
                    }
                    battleFriend.m_bIsFriend = true;
                    battleFriend.m_iEquipItemTableID = item["equip_item_id"].Int32Value;
                    pkt.m_lstBattleFriendEx.Add(battleFriend);
                }
            }
        }

        if (!data["player"].IsNull)
        {
            List<IJSonObject> player = new List<IJSonObject>(data["player"].ArrayItems);
            if (player != null && player.Count > 0)
            {
                foreach (var item in player)
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
                        tmpHero.m_iEquipID = item["equip_item_id"].Int32Value;
                        battleFriend.m_cLeaderHero = tmpHero;
                    }
                    battleFriend.m_bIsFriend = false;
                    battleFriend.m_iEquipItemTableID = item["equip_item_id"].Int32Value;
                    pkt.m_lstBattleFriend.Add(battleFriend);
                }
            }
        }
        return pkt;
    }
}