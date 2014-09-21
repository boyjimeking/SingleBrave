using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;


//  PVPBattleStartHandle.cs
//  Author: Lu Zexi
//  2014-02-11





/// <summary>
/// PVP战斗开始句柄
/// </summary>
public class PVPBattleStartHandle
{

    /// <summary>
    /// 获取ACTION
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.PVP_BATTLE_START_REQ;
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketAck packet)
    {
        PVPBattleStartPktAck ack = (PVPBattleStartPktAck)packet;

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.header.code != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.header.desc);
            return;
        }

        Role.role.GetBaseProperty().m_iBattleID = ack.m_iBattleID;
        Role.role.GetBaseProperty().m_iSportPoint = ack.m_iSportPoint;
        GUIBackFrameTop top =    GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP) as GUIBackFrameTop;
        top.UpdateSportPoint(Role.role.GetBaseProperty().m_iSportPoint);
        top.Hiden();

        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Hiden();

        GUI_FUNCTION.AYSNCLOADING_SHOW();

        GUIBattleArena gui = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_ARENA_BATTLE) as GUIBattleArena;

        //己方设置
        gui.m_strRoleSelfName = Role.role.GetBaseProperty().m_strUserName;
        gui.m_iRoleSelfPvpPoint = Role.role.GetBaseProperty().m_iPVPExp;
        HeroTeam team = new HeroTeam().Get<HeroTeam>(Role.role.GetBaseProperty().m_iCurrentTeam);
        Hero[] heros = new Hero[5];
        for (int i = 0; i < team.m_vecTeam.Length; i++)
        {
            Hero item = Role.role.GetHeroProperty().GetHero(team.m_vecTeam[i]);
            heros[i] = item;
        }

        //设置领导技能
        gui.m_iSelfLeaderIndex = team.GetLeaderIndex();
        Hero selfLeader = heros[ack.m_iLeaderIndex];
        if (selfLeader != null)
        {
            LeaderSkillTable leaderSkill = LeaderSkillTableManager.GetInstance().GetLeaderSkillTable(selfLeader.m_iLeaderSkillID);
            gui.SetSelfLeaderSkill(leaderSkill);
        }
        gui.SetBattleSelfHero(heros);

        //目标方
        GUIArenaFightReady tmpgui = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_ARENAFIGHTREADY) as GUIArenaFightReady;
        gui.m_strRoleTargetName = tmpgui.m_strSelectName;
        gui.m_iRoleTargetPvpPoint = tmpgui.m_iTagetPvpPoint;
        Item[] vecEquip = new Item[5];
        for( int i = 0 ; i<vecEquip.Length ; i++ )
        {
            vecEquip[i] = null;
            if(ack.m_vecEquips[i] > 0 )
            {
                vecEquip[i] = new Item(ack.m_vecEquips[i]);
            }
        }
        //设置领导技能
        gui.m_iTargetLeaderIndex = ack.m_iLeaderIndex;
        Hero targetLeader = ack.m_vecHeros[ack.m_iLeaderIndex];
        if (targetLeader != null)
        {
            LeaderSkillTable leaderSkill = LeaderSkillTableManager.GetInstance().GetLeaderSkillTable(targetLeader.m_iLeaderSkillID);
            gui.SetTargetLeaderSkill(leaderSkill);
        }
        gui.SetBattleTargetHero(ack.m_vecHeros, vecEquip);
        gui.Show();

        return;
    }
}
