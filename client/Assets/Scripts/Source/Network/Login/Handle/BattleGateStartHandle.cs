using System;
using System.Collections.Generic;
using Game.Base;
using Game.Network;

//  BattleGateStartHandle.cs
//  Author: Lu Zexi
//  2013-12-19



/// <summary>
/// 战斗关卡开始句柄
/// </summary>
public class BattleGateStartHandle
{
    /// <summary>
    /// 获取action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.BATTLE_GATE_START_REQ;
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketRequest packet)
    {
        //this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Hiden();
        //this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).HidenImmediately();

        //this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Destory();
        //this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Destory();
        //Destory();

        BattleGateStartPktAck ack = (BattleGateStartPktAck)packet;

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return false;
        }

        AreaTable areaTable = WorldManager.GetArea(WorldManager.s_iCurrentWorldId, WorldManager.s_iCurrentAreaIndex);
        DungeonTable dungeonTable = WorldManager.GetDungeonTable(areaTable.ID, WorldManager.s_iCurrentDungeonIndex);
        GateTable gateTable = WorldManager.GetGateTable(dungeonTable.ID, WorldManager.s_iCurrentGateIndex);

        Hero[] heros = new Hero[6];
        GUIGateBattle gatebattleGui = (GUIGateBattle)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_GATE_BATTLE);

        Role.role.GetBaseProperty().m_iBattleID = ack.m_iBattleID;

        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Hiden();
        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Hiden();

        gatebattleGui.m_iGateID = gateTable.ID;
        gatebattleGui.m_iDungeonID = dungeonTable.ID;

        if (gateTable.ID == GUIDE_FUNCTION.GATE_ID)
        {
            GUIDE_FUNCTION.SHOW_STORY(GUIDE_FUNCTION.STORY_SECOND_FIGHT_START, ShowStoryCallBack);
            return true;
        }
        else if (gateTable.ID == GUIDE_FUNCTION.GATE_ID1)
        {
            GUIDE_FUNCTION.SHOW_STORY(GUIDE_FUNCTION.STORY_THIRD_FIGHT_START, ShowStoryCallBack1);
            return true;
        }

        ////新手
        //if ( gateTable.ID == GUIDE_FUNCTION.GATE_ID)
        //{
        //    gatebattleGui.SetSelfLeaderSkill(null);
        //    gatebattleGui.SetFriendLeaderSkill(null);
        //    gatebattleGui.SetLeaderIndex(0);

        //    //1方案
        //    Hero tmpHero = new Hero(4);
        //    tmpHero.m_iLevel = 80;
        //    tmpHero.m_iBBSkillLevel = 1;
        //    tmpHero.m_iHp = 3917;
        //    tmpHero.m_iAttack = 1364;
        //    tmpHero.m_iDefence = 1230;
        //    tmpHero.m_iRevert = 1082;
        //    heros[0] = tmpHero;

        //    tmpHero = new Hero(8);
        //    tmpHero.m_iLevel = 80;
        //    tmpHero.m_iBBSkillLevel = 1;
        //    tmpHero.m_iHp = 3815;
        //    tmpHero.m_iAttack = 1355;
        //    tmpHero.m_iDefence = 1127;
        //    tmpHero.m_iRevert = 1461;
        //    heros[1] = tmpHero;

        //    tmpHero = new Hero(12);
        //    tmpHero.m_iLevel = 80;
        //    tmpHero.m_iBBSkillLevel = 1;
        //    tmpHero.m_iHp = 3894;
        //    tmpHero.m_iAttack = 1373;
        //    tmpHero.m_iDefence = 1378;
        //    tmpHero.m_iRevert = 805;
        //    heros[4] = tmpHero;

        //    tmpHero = new Hero(16);
        //    tmpHero.m_iLevel = 80;
        //    tmpHero.m_iBBSkillLevel = 1;
        //    tmpHero.m_iHp = 4233;
        //    tmpHero.m_iAttack = 1460;
        //    tmpHero.m_iDefence = 1006;
        //    tmpHero.m_iRevert = 770;
        //    heros[3] = tmpHero;

        //    gatebattleGui.SetBattleSelfHero(heros);
        //    gatebattleGui.SetItem(Role.role.GetItemProperty().GetAllBattleItem());
        //    gatebattleGui.Show();
        //    gatebattleGui.m_bLoseShow = false;
        //    gatebattleGui.m_bMenuShow = false;
        //    return true;
        //}
        //else if (gateTable.ID == GUIDE_FUNCTION.GATE_ID1)
        //{
        //    gatebattleGui.SetSelfLeaderSkill(null);
        //    gatebattleGui.SetFriendLeaderSkill(null);
        //    gatebattleGui.SetLeaderIndex(0);

        //    //1方案
        //    Hero tmpHero = new Hero(4);
        //    tmpHero.m_iLevel = 80;
        //    tmpHero.m_iBBSkillLevel = 1;
        //    tmpHero.m_iHp = 3917;
        //    tmpHero.m_iAttack = 1364;
        //    tmpHero.m_iDefence = 1230;
        //    tmpHero.m_iRevert = 1082;
        //    heros[0] = tmpHero;

        //    tmpHero = new Hero(8);
        //    tmpHero.m_iLevel = 80;
        //    tmpHero.m_iBBSkillLevel = 1;
        //    tmpHero.m_iHp = 3815;
        //    tmpHero.m_iAttack = 1355;
        //    tmpHero.m_iDefence = 1127;
        //    tmpHero.m_iRevert = 1461;
        //    heros[1] = tmpHero;

        //    tmpHero = new Hero(12);
        //    tmpHero.m_iLevel = 80;
        //    tmpHero.m_iBBSkillLevel = 1;
        //    tmpHero.m_iHp = 3894;
        //    tmpHero.m_iAttack = 1373;
        //    tmpHero.m_iDefence = 1378;
        //    tmpHero.m_iRevert = 805;
        //    heros[4] = tmpHero;

        //    tmpHero = new Hero(16);
        //    tmpHero.m_iLevel = 80;
        //    tmpHero.m_iBBSkillLevel = 1;
        //    tmpHero.m_iHp = 4233;
        //    tmpHero.m_iAttack = 1460;
        //    tmpHero.m_iDefence = 1006;
        //    tmpHero.m_iRevert = 770;
        //    heros[3] = tmpHero;

        //    gatebattleGui.SetBattleSelfHero(heros);
        //    gatebattleGui.SetItem(Role.role.GetItemProperty().GetAllBattleItem());
        //    gatebattleGui.Show();
        //    gatebattleGui.m_bLoseShow = false;
        //    gatebattleGui.m_bMenuShow = false;
        //    return true;
        //}
        //else
        {
            HeroTeam team = Role.role.GetTeamProperty().GetTeam(Role.role.GetBaseProperty().m_iCurrentTeam);
            for (int i = 0; i < team.m_vecTeam.Length; i++)
            {
                Hero item = Role.role.GetHeroProperty().GetHero(team.m_vecTeam[i]);
                heros[i] = item;
            }

            heros[5] = Role.role.GetBattleFriendProperty().GetSelectFriend().m_cLeaderHero;

            LeaderSkillTable selfLeaderSkill = LeaderSkillTableManager.GetInstance().GetLeaderSkillTable(Role.role.GetHeroProperty().GetHero(team.m_iLeadID).m_iLeaderSkillID);
            LeaderSkillTable friendLeaderSkill = null;
            if (Role.role.GetFriendProperty().IsMyFriend(Role.role.GetBattleFriendProperty().GetSelectFriend().m_iID))
            {
                friendLeaderSkill = LeaderSkillTableManager.GetInstance().GetLeaderSkillTable(Role.role.GetBattleFriendProperty().GetSelectFriend().m_cLeaderHero.m_iLeaderSkillID);
            }

            //获得世界，区域，副本，关卡id
            UnityEngine.Debug.Log(WorldManager.s_iCurrentWorldId.ToString() + " - " + WorldManager.s_iCurrentAreaIndex.ToString() + " - "
                + WorldManager.s_iCurrentDungeonIndex.ToString() + " - " + WorldManager.s_iCurrentGateIndex.ToString());

            gatebattleGui.SetSelfLeaderSkill(selfLeaderSkill);
            gatebattleGui.SetFriendLeaderSkill(friendLeaderSkill);
            gatebattleGui.SetLeaderIndex(team.GetLeaderIndex());
            gatebattleGui.SetBattleSelfHero(heros);
            gatebattleGui.SetItem(Role.role.GetItemProperty().GetAllBattleItem());
            gatebattleGui.Show();
        }

        return true;
    }

    /// <summary>
    /// 剧情回调
    /// </summary>
    private void ShowStoryCallBack()
    {
        AreaTable areaTable = WorldManager.GetArea(WorldManager.s_iCurrentWorldId, WorldManager.s_iCurrentAreaIndex);
        DungeonTable dungeonTable = WorldManager.GetDungeonTable(areaTable.ID, WorldManager.s_iCurrentDungeonIndex);
        GateTable gateTable = WorldManager.GetGateTable(dungeonTable.ID, WorldManager.s_iCurrentGateIndex);

        Hero[] heros = new Hero[6];
        GUIGateBattle gatebattleGui = (GUIGateBattle)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_GATE_BATTLE);

        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Hiden();
        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Hiden();

        gatebattleGui.m_iGateID = gateTable.ID;
        gatebattleGui.m_iDungeonID = dungeonTable.ID;

        gatebattleGui.SetSelfLeaderSkill(null);
        gatebattleGui.SetFriendLeaderSkill(null);
        gatebattleGui.SetLeaderIndex(0);

        //1方案
        Hero tmpHero = new Hero(4);
        tmpHero.m_iLevel = 80;
        tmpHero.m_iBBSkillLevel = 1;
        tmpHero.m_iHp = 3917;
        tmpHero.m_iAttack = 1364;
        tmpHero.m_iDefence = 1230;
        tmpHero.m_iRevert = 1082;
        heros[0] = tmpHero;

        tmpHero = new Hero(8);
        tmpHero.m_iLevel = 80;
        tmpHero.m_iBBSkillLevel = 1;
        tmpHero.m_iHp = 3815;
        tmpHero.m_iAttack = 1355;
        tmpHero.m_iDefence = 1127;
        tmpHero.m_iRevert = 1461;
        heros[1] = tmpHero;

        tmpHero = new Hero(12);
        tmpHero.m_iLevel = 80;
        tmpHero.m_iBBSkillLevel = 1;
        tmpHero.m_iHp = 3894;
        tmpHero.m_iAttack = 1373;
        tmpHero.m_iDefence = 1378;
        tmpHero.m_iRevert = 805;
        heros[4] = tmpHero;

        tmpHero = new Hero(16);
        tmpHero.m_iLevel = 80;
        tmpHero.m_iBBSkillLevel = 1;
        tmpHero.m_iHp = 4233;
        tmpHero.m_iAttack = 1460;
        tmpHero.m_iDefence = 1006;
        tmpHero.m_iRevert = 770;
        heros[3] = tmpHero;

        gatebattleGui.SetBattleSelfHero(heros);
        gatebattleGui.SetItem(Role.role.GetItemProperty().GetAllBattleItem());
        gatebattleGui.Show();
        gatebattleGui.m_bLoseShow = false;
        gatebattleGui.m_bMenuShow = false;
    }

    /// <summary>
    /// 展示剧情回调
    /// </summary>
    private void ShowStoryCallBack1()
    {
        AreaTable areaTable = WorldManager.GetArea(WorldManager.s_iCurrentWorldId, WorldManager.s_iCurrentAreaIndex);
        DungeonTable dungeonTable = WorldManager.GetDungeonTable(areaTable.ID, WorldManager.s_iCurrentDungeonIndex);
        GateTable gateTable = WorldManager.GetGateTable(dungeonTable.ID, WorldManager.s_iCurrentGateIndex);

        Hero[] heros = new Hero[6];
        GUIGateBattle gatebattleGui = (GUIGateBattle)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_GATE_BATTLE);

        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Hiden();
        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Hiden();

        gatebattleGui.m_iGateID = gateTable.ID;
        gatebattleGui.m_iDungeonID = dungeonTable.ID;

        gatebattleGui.SetSelfLeaderSkill(null);
        gatebattleGui.SetFriendLeaderSkill(null);
        gatebattleGui.SetLeaderIndex(0);

        HeroTeam team = Role.role.GetTeamProperty().GetTeam(Role.role.GetBaseProperty().m_iCurrentTeam);
        for (int i = 0; i < team.m_vecTeam.Length; i++)
        {
            Hero item = Role.role.GetHeroProperty().GetHero(team.m_vecTeam[i]);
            heros[i] = item;
        }
        heros[5] = null;

        gatebattleGui.SetBattleSelfHero(heros);
        gatebattleGui.SetItem(Role.role.GetItemProperty().GetAllBattleItem());
        gatebattleGui.Show();
        gatebattleGui.m_bLoseShow = false;
        gatebattleGui.m_bMenuShow = false;
    }

}
