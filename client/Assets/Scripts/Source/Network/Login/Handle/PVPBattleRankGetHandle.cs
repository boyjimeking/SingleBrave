using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Network;

//  PVPBattleRankGetHandle.cs
//  Author: sanvey
//  2014-2-8

//竞技场排行获取请求应答句柄
public class PVPBattleRankGetHandle
{
    /// <summary>
    /// 获得Action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.PVP_BATTLE_RANK_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketRequest packet)
    {
        PVPBattleRankGetPktAck ack = (PVPBattleRankGetPktAck)packet;

        GAME_LOG.LOG("code :" + ack.m_iErrorCode);
        GAME_LOG.LOG("desc :" + ack.m_strErrorDes);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return;
        }

        GUIArenaRankings tmp = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_ARENARANKINGS) as GUIArenaRankings;

        tmp.m_iMyRankAll = ack.m_iMyRankAll;
        tmp.m_iMyRankFriend = ack.m_iMyRankFriend;
        tmp.m_iMyRankWeek = ack.m_iMyRankWeek;
        
        //全排行
        tmp.m_lstRanksAll = new List<GUIArenaRankings.PVPItem>();
        foreach (PVPBattleRankGetPktAck.PVPItem item in ack.AllRank)
        {
            GUIArenaRankings.PVPItem ct = new GUIArenaRankings.PVPItem();
            ct.m_iHeroLv = item.m_iHeroLv;
            ct.m_iHeroTableID = item.m_iHeroTableID;
            ct.m_iLoseNum = item.m_iLoseNum;
            ct.m_iPoint = item.m_iPoint;
            ct.m_iWinNum = item.m_iWinNum;
            ct.m_strName = item.m_strName;

            tmp.m_lstRanksAll.Add(ct);
        }
        //好友排行
        tmp.m_lstRanksFriend = new List<GUIArenaRankings.PVPItem>();
        foreach (PVPBattleRankGetPktAck.PVPItem item in ack.FriendRank)
        {
            GUIArenaRankings.PVPItem ct = new GUIArenaRankings.PVPItem();
            ct.m_iHeroLv = item.m_iHeroLv;
            ct.m_iHeroTableID = item.m_iHeroTableID;
            ct.m_iLoseNum = item.m_iLoseNum;
            ct.m_iPoint = item.m_iPoint;
            ct.m_iWinNum = item.m_iWinNum;
            ct.m_strName = item.m_strName;

            tmp.m_lstRanksFriend.Add(ct);
        }
        //周排行
        tmp.m_lstRanksWeek = new List<GUIArenaRankings.PVPItem>();
        foreach (PVPBattleRankGetPktAck.PVPItem item in ack.WeekRank)
        {
            GUIArenaRankings.PVPItem ct = new GUIArenaRankings.PVPItem();
            ct.m_iHeroLv = item.m_iHeroLv;
            ct.m_iHeroTableID = item.m_iHeroTableID;
            ct.m_iLoseNum = item.m_iLoseNum;
            ct.m_iPoint = item.m_iPoint;
            ct.m_iWinNum = item.m_iWinNum;
            ct.m_strName = item.m_strName;
            ct.m_iLoseNumForWeek = item.m_iLoseNumForWeek;
            ct.m_iWinNumForWeek = item.m_iWinNumForWeek;
            ct.m_iPointForWeek = item.m_iPointForWeek;

            tmp.m_lstRanksWeek.Add(ct);
        }
        //即使的周排行刷新 竞技场界面前3名
        Role.role.GetBaseProperty().m_lstWeekRank = new List<RoleBaseProperty.PVPItem>();
        for (int i = 0; i < tmp.m_lstRanksWeek.Count; i++)
        {
            if (Role.role.GetBaseProperty().m_lstWeekRank.Count > 3)  //取前3名
            {
                break;
            }

            RoleBaseProperty.PVPItem ct = new RoleBaseProperty.PVPItem();
            ct.m_iHeroLv = tmp.m_lstRanksWeek[i].m_iHeroLv;
            ct.m_iHeroTableID = tmp.m_lstRanksWeek[i].m_iHeroTableID;
            ct.m_iLoseNum = tmp.m_lstRanksWeek[i].m_iLoseNum;
            ct.m_iPoint = tmp.m_lstRanksWeek[i].m_iPoint;
            ct.m_iWinNum = tmp.m_lstRanksWeek[i].m_iWinNum;
            ct.m_strName = tmp.m_lstRanksWeek[i].m_strName;

            Role.role.GetBaseProperty().m_lstWeekRank.Add(ct);
        }


        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_ARENA).Hiden();
        tmp.Show();

        return;
    }
}