//  HeroEvolutionHandle.cs
//  Author: Cheng Xia
//  2013-12-25

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;
using UnityEngine;

/// <summary>
/// 英雄升级句柄
/// </summary>
public class HeroEvolutionHandle
{
    /// <summary>
    /// 获取action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.HERO_EVOLUTION_REQ;
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketAck packet)
    {
        HeroEvolutionPktAck ack = (HeroEvolutionPktAck)packet;

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.header.code != 0)
        {
            
        }
        //更新金币
        Role.role.GetBaseProperty().m_iGold = ack.m_iGold;
        GUIBackFrameTop guitop = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP) as GUIBackFrameTop;
        guitop.UpdateGold();
        //隐藏进化界面
        GUIHeroEvolution gui_evolutionHero = (GUIHeroEvolution)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_EVOLUTIONHERO);
        gui_evolutionHero.Hiden();
        //进化英雄赋值
        Hero hero = Role.role.GetHeroProperty().GetHero(ack.m_cAfterHero.m_iID);
        int oldTableID = hero.m_iTableID;

        Hero hTable = new Hero(ack.m_cAfterHero.m_iTableID);
        hero.m_iID = ack.m_cAfterHero.m_iID;
        hero.m_iTableID = ack.m_cAfterHero.m_iTableID;
        hero.m_iLevel = ack.m_cAfterHero.m_iLevel;
        hero.m_iCurrenExp = ack.m_cAfterHero.m_iCurrenExp;
        hero.m_iHp = ack.m_cAfterHero.m_iHp;
        hero.m_iAttack = ack.m_cAfterHero.m_iAttack;
        hero.m_iDefence = ack.m_cAfterHero.m_iDefense;
        hero.m_iRevert = ack.m_cAfterHero.m_iRevert;
        hero.m_iBBSkillLevel = ack.m_cAfterHero.m_iBBSkillLevel;
        hero.m_iCost = hTable.m_iCost;
        hero.m_iMaxLevel = hTable.m_iMaxLevel;
        hero.m_iStarLevel = hTable.m_iStarLevel;
        hero.m_strAvatarA = hTable.m_strAvatarA;
        hero.m_strAvatarM = hTable.m_strAvatarM;
        hero.m_strAvatarL = hTable.m_strAvatarL;
        hero.m_iBBSkillTableID = hTable.m_iBBSkillTableID;
        hero.m_strName = hTable.m_strName;
        hero.m_iExpType = hTable.m_iExpType;
        hero.m_iEvolutionID = hTable.m_iEvolutionID;
        hero.m_iStarLevel = hTable.m_iStarLevel;
        hero.m_iMaxLevel = hTable.m_iMaxLevel;
        hero.m_iMaxBBHP = hTable.m_iMaxBBHP;
        hero.m_iTypeID = hTable.m_iTypeID;
        hero.m_strModel = hTable.m_strModel;
        hero.m_iLeaderSkillID = hTable.m_iLeaderSkillID;
        hero.m_fMoveSpeed = hTable.m_fMoveSpeed;
        hero.m_iCombineExp = hTable.m_iCombineExp;
        hero.m_vecEvolution = hTable.m_vecEvolution;
        hero.m_eMoveType = hTable.m_eMoveType;
        hero.m_iSellCost = hTable.m_iSellCost;

        Role.role.GetHeroProperty().UpdateHero(hero);
        //删除进化素材
        foreach (int heroID in ack.m_lstDeleteHeros)
        {
            Role.role.GetHeroProperty().DelHero(heroID);
        }
        //开始进化特效
        GUIHeroEvolutionResult tmp = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_EVOLUTIONRESULT) as GUIHeroEvolutionResult;
        tmp.SetHeroSelectId(ack.m_cAfterHero.m_iID, oldTableID);
        tmp.Show();

        
    }
}
