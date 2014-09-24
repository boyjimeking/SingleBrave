using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Network;

//  ActivityBattleEndHandle.cs
//  Author: Lu Zexi
//  2014-01-08



/// <summary>
/// 活动战斗结束句柄
/// </summary>
public class ActivityBattleEndHandle
{
    private static List<int> m_lstNewHero = new List<int>();//新英雄列表

    /// <summary>
    /// 获取action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.ACTIVITY_BATTLE_END_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketAck packet)
    {
        ActivityBattleEndPktAck ack = (ActivityBattleEndPktAck)packet;

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.header.code != 0)
        {
            GAME_LOG.ERROR("error code desc :" + ack.header.desc);
            
        }

        int tmpLevel = Role.role.GetBaseProperty().m_iLevel;
        int tmpexp = Role.role.GetBaseProperty().m_iCurrentExp;
        int tmpDiamond = ack.m_iDiamond;


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

        List<Hero> lstHero = new List<Hero>();

        foreach (ActivityBattleEndPktAck.HeroData item in ack.m_lstHero)
        {
            Hero hero = new Hero(item.m_iTableID);
            hero.m_bLock = false;
            hero.m_iID = item.m_iID;
            hero.m_iLevel = item.m_iLevel;
            hero.m_iHp = item.m_iHp;
            hero.m_iAttack = item.m_iAttack;
            hero.m_iDefence = item.m_iDefence;
            hero.m_iRevert = item.m_iRecover;
            hero.m_iBBSkillLevel = item.m_iBBLevel;
            hero.m_iEquipID = item.m_iEquipID;
            hero.m_lGetTime = item.m_lCreateTime;
            hero.m_iCurrenExp = item.m_iExp;
            hero.m_eGrowType = (GrowType)item.m_iGrowType;
            hero.m_bNew = true;

			if (!new HeroBook().HadHero(item.m_iTableID))
                m_lstNewHero.Add(item.m_iTableID);
            Role.role.GetHeroProperty().AddHero(hero);
			new HeroBook().AddBook(hero.m_iTableID);
            lstHero.Add(hero);
        }

        for (int i = 0; i < ack.m_lstItem.Count; i++)
        {
            ActivityBattleEndPktAck.ItemData itemData = ack.m_lstItem[i];
            Item item = new Item(itemData.m_iTableID);
            item.m_iID = itemData.m_iID;
            item.m_iNum = itemData.m_iNum;
            item.m_iDummyNum = item.m_iNum;

            Role.role.GetItemProperty().AddItem(item);  //加入客户端物品数据
            new ItemBook().AddItem(item.m_iTableID); //物品图鉴更新
        }

        //更新物品
        for (int i = 0; i < ack.m_lstUpdateItem.Count; i++)
        {
            ActivityBattleEndPktAck.ItemData itemData = ack.m_lstUpdateItem[i];
            Item item = new Item(itemData.m_iTableID);
            item.m_iID = itemData.m_iID;
            item.m_iTableID = itemData.m_iTableID;
            item.m_iNum = itemData.m_iNum;
            Role.role.GetItemProperty().UpdateItemByID(item.m_iID, item.m_iNum);
        }

        //删除物品
        for (int i = 0; i < ack.m_lstDelItem.Count; i++)
        {
            int delItem = ack.m_lstDelItem[i];
            Role.role.GetItemProperty().DeleteItem(delItem);
        }

        ActivityDungeonTable dungeonTable = WorldManager.GetActivityDungeonTable(WorldManager.s_iCurEspDungeonId);
        ActivityGateTable gateTable = WorldManager.GetActivityGateTable(dungeonTable.ID, WorldManager.s_iCurEspDungeonGateIndex);

        //刷新战斗好友列表
		BattleFriend battleFriend = CModelMgr.sInstance.GetModel<BattleFriend>();
		battleFriend.Clear();

        for (int i = 0; i < ack.m_lstBattleFriendEx.Count; i++)
        {
            battleFriend.Add(ack.m_lstBattleFriendEx[i]);
        }

        for (int i = 0; i < ack.m_lstBattleFriend.Count; i++)
        {
            battleFriend.Add(ack.m_lstBattleFriend[i]);
        }

        GUIBattleReward gui = (GUIBattleReward)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BATTLE_REWARD);
        GUIActivityBattle battlegui = (GUIActivityBattle)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_ACTIVITY_BATTLE);
        int newtotalExp = RoleExpTableManager.GetInstance().GetMinExp(tmpLevel) + tmpexp;
        int rewardExp = RoleExpTableManager.GetInstance().GetMinExp(ack.m_iLevel) + ack.m_iExp - newtotalExp;
        gui.SetReward(dungeonTable.Name, gateTable.Name, ack.m_iGold, ack.m_iFarm, rewardExp, newtotalExp, tmpLevel, tmpDiamond, lstHero, battlegui.m_lstItem, battlegui.m_lstItemNum, m_lstNewHero, GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_ESPDUNGEON).Show);

        //if (gateTable.ID == GUIDE_FUNCTION.GATE_ID1)
        //{
        //    GUIDE_FUNCTION.SHOW_STORY(1, story_callback);
        //    
        //}
        
        gui.Show();
        m_lstNewHero.Clear();

        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();

        
    }


    /// <summary>
    /// 剧情回调
    /// </summary>
    private void story_callback()
    {
        GUIBattleReward gui = (GUIBattleReward)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BATTLE_REWARD);
        gui.Show();
        m_lstNewHero.Clear();

        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
    }
}
