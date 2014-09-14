using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Network;
using UnityEngine;

//  MoneySummonHandle.cs
//  Author: sanvey
//  2013-12-17

//金钱召唤请求应答句柄
public class MoneySummonHandle
{
    /// <summary>
    /// 获得Action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.MONEY_SUMMON_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketAck packet)
    {
        MoneySummonPktAck ack = (MoneySummonPktAck)packet;

        GAME_LOG.LOG("code :" + ack.header.code);
        GAME_LOG.LOG("desc :" + ack.header.desc);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.header.code != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.header.desc);
            return;
        }

        Role.role.GetBaseProperty().m_iDiamond = ack.m_iDiamond;
        GUIBackFrameTop top = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP) as GUIBackFrameTop;
        top.UpdateDiamond(Role.role.GetBaseProperty().m_iDiamond);

        Hero hero = new Hero(ack.m_cHero.m_iTableID);

        //存储英雄信息
        hero.m_iID = ack.m_cHero.m_iID;// 英雄id
        hero.m_iTableID = ack.m_cHero.m_iTableID;// 配置表id
        hero.m_iCurrenExp = ack.m_cHero.m_iCurrenExp;// 英雄经验
        hero.m_lGetTime = ack.m_cHero.m_lGetTime;// 英雄创建时间
        hero.m_iHp = ack.m_cHero.m_iHp;// 英雄血量
        hero.m_iAttack = ack.m_cHero.m_iAttack;// 英雄攻击力
        hero.m_iDefence = ack.m_cHero.m_iDefense;// 英雄恢复力
        hero.m_iRevert = ack.m_cHero.m_iRevert;// 英雄id
        hero.m_iBBSkillLevel = ack.m_cHero.m_iBBSkillLevel;// 英雄BB技能
        hero.m_eGrowType = (GrowType)ack.m_cHero.m_eGrowType;// 英雄成长类型
        hero.m_iLevel = ack.m_cHero.m_iLevel;  // 英雄等级
        hero.m_iEquipID = ack.m_cHero.m_iEquipId;
        hero.m_bLock = (ack.m_cHero.m_iLock == 1);
        hero.m_bNew = true;

        Role.role.GetHeroProperty().AddHero(hero);
        Role.role.GetHeroBookProperty().Add(hero.m_iTableID);

        //GUIHeroDetail tmp = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_HERODETAIL) as GUIHeroDetail;
        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_SUMMON_DETAIL).HidenImmediately();
        GUISummonResult tmp2 = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_SUMMON_RESULT) as GUISummonResult;
        tmp2.SetDiamondOrFriend(true);
        tmp2.ShowEffect(hero);
        //tmp.ShowWithColliderBack(tmp2.Show, hero);


        return;
    }
}


