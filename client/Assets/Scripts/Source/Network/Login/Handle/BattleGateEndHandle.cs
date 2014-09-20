
using System.Collections.Generic;
using System.Collections;
using Game.Base;
using Game.Network;

//  BattleGateendHandle.cs
//  Author: Lu Zexi
//  2013-12-19



/// <summary>
/// 战斗关卡结束句柄
/// </summary>
public class BattleGateEndHandle
{
    private static List<int> m_lstNewHero = new List<int>();//新英雄列表

    /// <summary>
    /// 获取action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.BATTLE_GATE_END_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketAck packet)
    {
        BattleGateEndPktAck ack = (BattleGateEndPktAck)packet;
        
        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.header.code != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.header.desc);
            
        }

        AreaTable areaTable = WorldManager.GetArea(WorldManager.s_iCurrentWorldId, WorldManager.s_iCurrentAreaIndex);
        int last_dungeonid = areaTable.VecDungeon[areaTable.MaxNum - 1];
        DungeonTable dungeonTable = WorldManager.GetDungeonTable(areaTable.ID, WorldManager.s_iCurrentDungeonIndex);
        int last_gateid = dungeonTable.VecGate[dungeonTable.GateNum - 1];
        GateTable gateTable = WorldManager.GetGateTable(dungeonTable.ID, WorldManager.s_iCurrentGateIndex);

        int tmpLevel = Role.role.GetBaseProperty().m_iLevel;
        int tmpexp = Role.role.GetBaseProperty().m_iCurrentExp;
        int tmpDiamond = ack.m_iDiamond;


        FuBen fuben = CModelMgr.sInstance.GetModel<FuBen>().GetFubenByWorldID(ack.m_iWorldID);
        int old_areindex = fuben.m_iAreaIndex;
        int old_dungeonindex = fuben.m_iDungeonIndex;
        int old_gateindex = fuben.m_iGateIndex;
        fuben.m_iAreaIndex = ack.m_iAreaIndex;
        fuben.m_iDungeonIndex = ack.m_iFubenIndex;
        fuben.m_iGateIndex = ack.m_iGateIndex;

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

        foreach( BattleGateEndPktAck.HeroData item in ack.m_lstHero )
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

            if (!Role.role.GetHeroBookProperty().HadHero(item.m_iTableID))
                m_lstNewHero.Add(item.m_iTableID);
            Role.role.GetHeroProperty().AddHero(hero);
            Role.role.GetHeroBookProperty().Add(hero.m_iTableID);
            lstHero.Add(hero);
        }

        for (int i = 0; i < ack.m_lstItem.Count; i++)
        {
            BattleGateEndPktAck.ItemData itemData = ack.m_lstItem[i];

            Item item = new Item(itemData.m_iTableID);
            item.m_iID = itemData.m_iID;
            item.m_iNum = itemData.m_iNum;
            item.m_iDummyNum = item.m_iNum;

            Role.role.GetItemProperty().AddItem(item);  //加入客户端物品数据
            Role.role.GetItemBookProperty().AddItem(item.m_iTableID); //物品图鉴更新
        }

        //更新物品
        for (int i = 0; i <ack.m_lstUpdateItem.Count; i++ )
        {
            BattleGateEndPktAck.ItemData itemData = ack.m_lstUpdateItem[i];
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

        GUIBattleReward gui = (GUIBattleReward)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BATTLE_REWARD);
        GUIGateBattle guibattle = (GUIGateBattle)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_GATE_BATTLE);

        int newtotalExp = RoleExpTableManager.GetInstance().GetMinExp(tmpLevel) + tmpexp;
        int rewardExp = RoleExpTableManager.GetInstance().GetMinExp(ack.m_iLevel) + ack.m_iExp - newtotalExp;

        //是否为副本最后一个关卡,并重置剧情
        if (last_gateid == gateTable.ID && old_gateindex == WorldManager.s_iCurrentGateIndex)
        {
            fuben.m_bDungeonStory = false;
        }

        //是否是区域最后一个关卡
        if (last_dungeonid == dungeonTable.ID && last_gateid == gateTable.ID && old_areindex == WorldManager.s_iCurrentAreaIndex && old_dungeonindex == WorldManager.s_iCurrentDungeonIndex && old_gateindex == WorldManager.s_iCurrentGateIndex && GAME_SETTING.s_iIsOver == 0)
        {
            if (ack.m_iIsover == 1)
            {
                GAME_SETTING.s_iIsOver = 1;
                GAME_SETTING.SaveGuanKa();
            }
            GUIArea area = (GUIArea)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_AREA);
            area.ResetCurrentAreaId();

            Role.role.GetBaseProperty().m_bIsNeedShowStory = true;

            Role.role.GetBaseProperty().m_iStoryID=areaTable.StoryID;
            //
            
        }

        if (GLOBAL_DEFINE.m_cSelectBattleFriend != null && GLOBAL_DEFINE.m_cSelectBattleFriend.m_bIsFriend)  //如果是好友，不需要在提示申请加好友
        {
            //GAME_SETTING.SaveNewDungeonOfNewArea(false);
            gui.SetReward(dungeonTable.Name, gateTable.Name, ack.m_iGold, ack.m_iFarm, rewardExp, newtotalExp, tmpLevel, tmpDiamond, lstHero, guibattle.m_lstItem, guibattle.m_lstItemNum, m_lstNewHero,
                new GUIBattleReward.CALLBACK(() =>
                {
                    if (Role.role.GetBaseProperty().m_bIsNeedShowStory)
                    {
                        GAME_SETTING.SaveNewDungeonOfNewArea(false);
                        GUI_FUNCTION.SHOW_STORY(Role.role.GetBaseProperty().m_iStoryID, GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_AREA).Show);
                        Role.role.GetBaseProperty().m_bIsNeedShowStory = false;
                    }
                    else
                        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_AREA).Show();
                }));
            gui.Show();
        }
        else
        {
            //GAME_SETTING.SaveNewDungeonOfNewArea(false);
            if (gateTable.ID != GUIDE_FUNCTION.GATE_ID1)
            {
                gui.SetReward(dungeonTable.Name, gateTable.Name, ack.m_iGold, ack.m_iFarm, rewardExp, newtotalExp, tmpLevel, tmpDiamond, lstHero, guibattle.m_lstItem, guibattle.m_lstItemNum, m_lstNewHero,
                    GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_DUNGEON_BATTLE_FRIEND).Show);
            }
            else
            {
                gui.SetReward(dungeonTable.Name, gateTable.Name, ack.m_iGold, ack.m_iFarm, rewardExp, newtotalExp, tmpLevel, tmpDiamond, lstHero, guibattle.m_lstItem, guibattle.m_lstItemNum, m_lstNewHero,
                    gui_main_show);
            }
            gui.Show();
        }
        m_lstNewHero.Clear();

        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();

        //刷新战斗好友列表
		BattleFriend battleFriend = CModelMgr.sInstance.GetModel<BattleFriend>();

        for (int i = 0; i < ack.m_lstBattleFriendEx.Count; i++)
        {
			battleFriend.Add(ack.m_lstBattleFriendEx[i]);
        }

        for (int i = 0; i < ack.m_lstBattleFriend.Count; i++)
        {
			battleFriend.Add(ack.m_lstBattleFriend[i]);
        }

        
    }

    /// <summary>
    /// 新手引导后展示GUI
    /// </summary>
    private static void gui_main_show()
    {
        GUIDE_FUNCTION.SHOW_STORY(GUIDE_FUNCTION.STORY_THIRD_FIGHT_END, gui_story);
    }
    
    /// <summary>
    /// 第三场战斗结束剧情
    /// </summary>
    private static void gui_story()
    {
        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_MAIN).Show();
        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Show();
    }

}
