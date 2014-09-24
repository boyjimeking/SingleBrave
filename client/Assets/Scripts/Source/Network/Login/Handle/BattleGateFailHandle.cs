using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;
using Game.Base;


//  BattleGateFailHandle.cs
//  Author: Lu Zexi
//  2014-03-05




/// <summary>
/// 战斗关卡失败句柄
/// </summary>
public class BattleGateFailHandle
{

    /// <summary>
    /// 获取action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.BATTLE_GATE_FAIL_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketAck packet)
    {
        BattleGateFailPktAck ack = packet as BattleGateFailPktAck;

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.header.code != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.header.desc);
            
        }


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

        //更新物品
        for (int i = 0; i < ack.m_lstUpdateItem.Count; i++)
        {
            BattleGateFailPktAck.ItemData itemData = ack.m_lstUpdateItem[i];
            Item item = new Item(itemData.m_iTableID);
            item.m_iID = itemData.m_iID;
            item.m_iTableID = itemData.m_iTableID;
            item.m_iNum = itemData.m_iNum;
            Role.role.GetItemProperty().UpdateItemByID(item.m_iID, item.m_iNum);
        }

        //删除物品
        for (int i = 0; i < ack.m_lstDelItem.Count; i++)
        {
            int delitem = ack.m_lstDelItem[i];
            Role.role.GetItemProperty().DeleteItem(delitem);
        }

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
		
		int newAreaIndex = FuBen.GetNewAreaIndex(WorldManager.s_iCurrentWorldId);
		int newDungeonIndex = FuBen.GetNewDungeonIndex(WorldManager.s_iCurrentWorldId, newAreaIndex);
        if (newDungeonIndex > 0)
        {
            WorldManager.s_iLastNewDungeonIndex = newDungeonIndex - 1;
        }

        //新手引导战斗失败剧情
        if (WorldManager.s_iCurrentWorldId == 1 && WorldManager.s_iCurrentAreaIndex == 0 && WorldManager.s_iCurrentDungeonIndex == 0)
        {
            GUIDE_FUNCTION.SHOW_STORY(GUIDE_FUNCTION.STORY_SECOND_FIGHT_END, story_callback);
            
        }

        GUIArea area = (GUIArea)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_AREA);
        area.ResetCurrentAreaId();

        area.Show();

        
    }

    /// <summary>
    /// 剧情回调
    /// </summary>
    private static void story_callback()
    {
        GUIArea area = (GUIArea)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_AREA);
        area.ResetCurrentAreaId();
        area.Show();
    }

}
