using System;
using System.Collections.Generic;
using CodeTitans.JSon;
using Game.Network;

//  GuestZhaoDaiPktAck.cs
//  Author: sanvey
//  2013-12-17


/// <summary>
/// 招待应答数据包
/// </summary>
public class GuestZhaoDaiPktAck : HTTPPacketBase
{
    public bool m_bOk;
    //public int m_iFarm;
    //public int m_iGold;
    //public int m_iDiamond;
    //public int m_iFriendPoint;

    //public ItemData m_cItem;
    //public HeroData m_cHero;

    ///// <summary>
    ///// 物品数据类
    ///// </summary>
    //public class ItemData
    //{
    //    public int m_iId;
    //    public int m_iPid;
    //    public int m_iItem_id;
    //    public int m_iItem_num;
    //}

    ///// <summary>
    ///// 玩家英雄数据类
    ///// </summary>
    //public class HeroData
    //{
    //    public int m_iID;    // 英雄id
    //    public int m_iTableID;    // 配置表id
    //    public int m_iLevel;    // 英雄等级
    //    public int m_iCurrenExp;    // 英雄经验
    //    public int m_lGetTime;    // 英雄创建时间
    //    public int m_iHp;    // 英雄血量
    //    public int m_iAttack;    // 英雄攻击力
    //    public int m_iDefense;    // 英雄防御力
    //    public int m_iRevert;    // 英雄恢复力
    //    public int m_iBBSkillLevel;    //英雄BB技能
    //    public int m_eGrowType;    // 英雄成长类型
    //    public int m_iEquipId;
    //    public int m_iLock;  //锁定

    //}

    public GuestZhaoDaiPktAck()
    {
        this.m_strAction = PACKET_DEFINE.GUEST_ZHAODAI_REQ;
    }
}


/// <summary>
/// 招待应答数据包工厂类
/// </summary>
public class GuestZhaoDaiPktAckFactory : HTTPPacketFactory
{

    /// <summary>
    /// 获取数据包Action
    /// </summary>
    /// <returns></returns>
    public override string GetPacketAction()
    {
        return PACKET_DEFINE.GUEST_ZHAODAI_REQ;
    }

    /// <summary>
    /// 创建数据包
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public override HTTPPacketBase Create(IJSonObject json)
    {
        GuestZhaoDaiPktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<GuestZhaoDaiPktAck>(json);

        if (pkt.m_iErrorCode != 0)
        {
            return pkt;
        }

        IJSonObject data = json["data"];
        pkt.m_bOk = data["ok"].BooleanValue;
        //if (data.Contains("gold"))
        //    pkt.m_iGold = data["gold"].Int32Value;
        //if (data.Contains("farm"))
        //    pkt.m_iFarm = data["farm"].Int32Value;
        //if (data.Contains("diamond"))
        //    pkt.m_iDiamond = data["diamond"].Int32Value;
        //if (data.Contains("friendPoint"))
        //    pkt.m_iFriendPoint = data["friendPoint"].Int32Value;

        //if (data.Contains("item"))
        //{
        //    if (data["item"] != null)
        //    {
        //        foreach (IJSonObject tmp in data["item"].ArrayItems)
        //        {
        //            pkt.m_cItem = new GuestZhaoDaiPktAck.ItemData();
        //            pkt.m_cItem.m_iId = tmp["id"].Int32Value;
        //            pkt.m_cItem.m_iItem_id = tmp["item_id"].Int32Value;
        //            pkt.m_cItem.m_iItem_num = tmp["item_num"].Int32Value;
        //        }
        //    }
        //}

        //if (data.Contains("hero"))
        //{
        //    if (data["hero"] != null)
        //    {
        //        foreach (IJSonObject tmp in data["hero"].ArrayItems)
        //        {
        //            pkt.m_cHero = new GuestZhaoDaiPktAck.HeroData();
        //            pkt.m_cHero.m_iID = tmp["id"].Int32Value;
        //            pkt.m_cHero.m_iTableID = tmp["hero_id"].Int32Value;
        //            pkt.m_cHero.m_iLevel = tmp["lv"].Int32Value;
        //            pkt.m_cHero.m_iCurrenExp = tmp["exp"].Int32Value;
        //            pkt.m_cHero.m_lGetTime = tmp["create_time"].Int32Value;
        //            pkt.m_cHero.m_iHp = (int)(tmp["hp"].SingleValue);
        //            pkt.m_cHero.m_iAttack = (int)(tmp["attack"].SingleValue);
        //            pkt.m_cHero.m_iDefense = (int)(tmp["defend"].SingleValue);
        //            pkt.m_cHero.m_iRevert = (int)(tmp["recover"].SingleValue);
        //            pkt.m_cHero.m_iBBSkillLevel = tmp["bb_level"].Int32Value;
        //            pkt.m_cHero.m_eGrowType = tmp["grow_type"].Int32Value;
        //            pkt.m_cHero.m_iEquipId = tmp["equip_id"].Int32Value;
        //            pkt.m_cHero.m_iLock = tmp["lock"].Int32Value;
        //        }
        //    }
        //}

        return pkt;
    }
}
