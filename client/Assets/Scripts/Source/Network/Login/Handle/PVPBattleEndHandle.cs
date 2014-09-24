using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;
using Game.Base;



//  PVPBattleEndHandle.cs
//  Author: Lu Zexi
//  2014-02-11





/// <summary>
/// PVP战斗结束
/// </summary>
public class PVPBattleEndHandle
{
    /// <summary>
    /// 获取ACTION
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.PVP_BATTLE_END_REQ;
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketAck packet)
    {
        PVPBattleEndPktAck ack = (PVPBattleEndPktAck)packet;

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.header.code != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.header.desc);
            return;
        }

        int expOld = Role.role.GetBaseProperty().m_iPVPExp;
        Role.role.GetBaseProperty().m_iPVPExp = ack.m_iPvpPoint;
        Role.role.GetBaseProperty().m_iPVPWin = ack.m_iWinNum;
        Role.role.GetBaseProperty().m_iPVPLose = ack.m_iLoseNum;
        Role.role.GetBaseProperty().m_iPVPMaxExp = ack.m_iPVPMaxPoint;
        Role.role.GetBaseProperty().m_iMyWeekRank = ack.m_iMyWeekRank;
        Role.role.GetBaseProperty().m_iMyWeekPoint = ack.m_iPvpWeekPoint;


        //更新服务器验证信息
        Role.role.GetBaseProperty().m_iLevel = ack.m_cPlayerValidate.m_iLv;
        Role.role.GetBaseProperty().m_iCurrentExp = ack.m_cPlayerValidate.m_iExp;
        Role.role.GetBaseProperty().m_iFriendPoint = ack.m_cPlayerValidate.m_iFriendPoint;
        Role.role.GetBaseProperty().m_iGold = ack.m_cPlayerValidate.m_iGold;
        Role.role.GetBaseProperty().m_iFarmPoint = ack.m_cPlayerValidate.m_iFarmPoint;
        Role.role.GetBaseProperty().m_iDiamond = ack.m_cPlayerValidate.m_iDiamond;
        Role.role.GetBaseProperty().m_iMaxHeroCount = ack.m_cPlayerValidate.m_iMaxHero;
        Role.role.GetBaseProperty().m_iMaxItem = ack.m_cPlayerValidate.m_iMaxItem;
        Role.role.GetBaseProperty().m_iPVPExp = ack.m_cPlayerValidate.m_iPvpPoint;
        Role.role.GetBaseProperty().m_iMyWeekPoint = ack.m_cPlayerValidate.m_iPvpWeekPoint;

        GUIBackFrameTop top = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP) as GUIBackFrameTop;
        //初始化体力和竞技点数据  
        //所有状态都只有m_fSportNext和m_fStrengthNext两个字段对体力和竞技点进行维护
        //m_fSportNext  到达满体力剩余的时间
        //m_fStrengthNext  到达满竞技点剩余的时间
        //在重新和服务器对体力和体力10分钟剩余时间 进行同步的时候，相当于重走一遍登陆接口，对唯一计算剩余值重新更新一次
        top.m_bIsUpdateIng = true;
        Role.role.GetBaseProperty().m_iSportPoint = ack.m_cPlayerValidate.m_iSport;
        Role.role.GetBaseProperty().m_iStrength = ack.m_cPlayerValidate.m_iStrength;
        Role.role.GetBaseProperty().m_iStrengthTime = ack.m_cPlayerValidate.m_iStrengthTime;
        Role.role.GetBaseProperty().m_iSportTime = ack.m_cPlayerValidate.m_iSportTime;

        Role.role.GetBaseProperty().m_fTopTime = GAME_TIME.TIME_REAL();
        Role.role.GetBaseProperty().m_fTopTimeSport = GAME_TIME.TIME_REAL();
        long secnd = (GAME_DEFINE.m_lServerTime - Role.role.GetBaseProperty().m_iStrengthTime);
        int maxStrength = RoleExpTableManager.GetInstance().GetMaxStrength(Role.role.GetBaseProperty().m_iLevel);
        int nowStrength = Role.role.GetBaseProperty().strength;
        Role.role.GetBaseProperty().m_fStrengthNext = (maxStrength - nowStrength) * GUIBackFrameTop.STRENGTH_PER - (float)secnd;
        long secnd2 = (GAME_DEFINE.m_lServerTime - Role.role.GetBaseProperty().m_iSportTime);
        int maxPVP = 3;
        int nowPVP = Role.role.GetBaseProperty().sportpoint;
        Role.role.GetBaseProperty().m_fSportNext = (maxPVP - nowPVP) * GUIBackFrameTop.PVP_PER - (float)secnd2;
        top.m_bIsUpdateIng = false;

        //刷新战斗好友列表
		BattleFriend.Clear();

        for (int i = 0; i < ack.m_lstBattleFriendEx.Count; i++)
        {
			BattleFriend.Add(ack.m_lstBattleFriendEx[i]);
        }
        for (int i = 0; i < ack.m_lstBattleFriend.Count; i++)
        {
			BattleFriend.Add(ack.m_lstBattleFriend[i]);
        }
		
		PVPItemInfo.Clear();
		for (int i = 0; i < PVPItemInfo.Count; i++)
        {
            PVPItemInfo tmp = new PVPItemInfo();
            tmp.m_iHeroLv = ack.m_lstWeekRank[i].m_iHeroLv;
            tmp.m_iHeroTableID = ack.m_lstWeekRank[i].m_iHeroTableID;
            tmp.m_iLoseNum = ack.m_lstWeekRank[i].m_iLoseNum;
            tmp.m_iPoint = ack.m_lstWeekRank[i].m_iPoint;
            tmp.m_iWinNum = ack.m_lstWeekRank[i].m_iWinNum;
            tmp.m_strName = ack.m_lstWeekRank[i].m_strName;

			PVPItemInfo.Add(tmp);
        }

        if (ack.m_bHasNewRecord)  //获得奖励
        {
            if (ack.m_iRewardDiamond != 0)  //获得奖励
            {
                Role.role.GetBaseProperty().m_iDiamond += ack.m_iRewardDiamond;
            }
            else
            {
                //获得物品
                Item tmp = new Item(ack.m_cRewardItem.m_iTableID);
                tmp.m_iID = ack.m_cRewardItem.m_iID;
                tmp.m_iNum = ack.m_cRewardItem.m_iNum;

                Role.role.GetItemProperty().AddItem(tmp);  //加入本地物品
                new ItemBook().AddItem(tmp.m_iTableID);  //更新本地图鉴
            }
        }


        GUIBattleArena gui = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_ARENA_BATTLE) as GUIBattleArena;
        BattleHero[] self = gui.GetVecSelf();
        BattleHero[] target = gui.GetVecEnemy();
        int enemyLeaderIndex = gui.m_iTargetLeaderIndex;


        GUIArenaBattleResult arenaResult = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_ANRENA_BATTLE_RESULT) as GUIArenaBattleResult;
        int itemID = ack.m_cRewardItem == null ? 0 : ack.m_cRewardItem.m_iTableID;
        arenaResult.SetData(self, target, enemyLeaderIndex, gui.m_iBattleResult, expOld, ack.m_bHasNewRecord, ack.m_iRewardDiamond, itemID);

        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Hiden();
        arenaResult.Show();

        return;
    }
}