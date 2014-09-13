using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;


//  GUIGuideBattle.cs
//  Author: Lu zexi
//  2014-02-27




/// <summary>
/// 新手引导战斗
/// </summary>
public class GUIGuideBattle : GUIBattle
{
    public GUIGuideBattle(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_GUIDE_BATTLE, GUILAYER.GUI_PANEL)
    { 
        //
    }

    private const int GATE_ID = 100001;

    private bool m_bJingying;    //精英是否出现过
    private float m_fJingyingRate;   //精英怪概率
    private int m_iJingyingOrder;    //精英怪序列

    private List<int> m_lstMonsterOrderID = new List<int>();    //已经有过的怪物序列ID

    /// <summary>
    /// 初始化
    /// </summary>
    public override void Initialize()
    {
        base.Initialize();
    }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        //清除数据
        this.m_iCurLayer = 0;
        this.m_lstMonsterOrderID.Clear();

        if (this.m_cScene != null)
        {
            GameObject.Destroy(this.m_cScene);
        }

        this.m_cScene = null;
        this.m_strSceneName = "BATTLE_Scene1";

        GateTable gateTable = GateTableManager.GetInstance().GetGateTable(GATE_ID);
        int orderID = gateTable.VecLayerNum[this.m_iCurLayer];

        //设置当前层级
        this.m_iMaxLayer = gateTable.MaxLayer;

        //最大金币数和农场点
        this.m_iMaxGold = gateTable.MaxGold;
        this.m_iMaxFarm = gateTable.MaxFarm;

        //设置标题
        this.m_strTittle = "新手引导";
        this.m_strContent = "新手引导";

        //设置精英怪
        this.m_bJingying = false;
        this.m_fJingyingRate = 0;
        this.m_iJingyingOrder = 0;

        //设置优惠类型
        this.m_eFavType = FAV_TYPE.NONE;

        //设置可捕捉的灵魂数
        //this.m_iCanCatchNum = Role.role.GetBaseProperty().m_iMaxHeroCount - Role.role.GetHeroProperty().GetAllHero().Count;

        //设置对手
        MonsterTable[] monsters = new MonsterTable[6];
        MonsterTeamTable teamTable = MonsterTableManager.GetInstance().GetMonsterTeamTable(gateTable.ID, orderID, this.m_lstMonsterOrderID);

        //背景音乐
        SoundManager.GetInstance().PlayBGM("SD_BGM15混");

        this.m_lstMonsterOrderID.Add(teamTable.OrderID);

        for (int i = 0; i < teamTable.VecMonster.Length; i++)
        {
            monsters[i] = MonsterTableManager.GetInstance().GetMonsterTable(gateTable.ID, teamTable.VecMonster[i]);
        }
        SetBattleTargetHero(monsters);

        //己方
        Hero[] heros = new Hero[6];
        if (gateTable.ID == GATE_ID)
        {
            //1方案
            //HeroTeam team = Role.role.GetTeamProperty().GetTeam(Role.role.GetBaseProperty().m_iCurrentTeam);
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
        }

        SetBattleSelfHero(heros);

        //设置物品
        SetItem(Role.role.GetItemProperty().GetAllBattleItem());

        //设置胜利回调和失败回调
        this.m_delFinishCallBack = BattleFinish;
        this.m_delEndCallBack = BattleEnd;

        //宝箱掉落率
        this.m_fBoxRate = 0;
        this.m_fBoxMonsterRate = 0;
        this.m_iBoxMonsterTableID = 0;
        this.m_fBoxDropItemRate = 0;
        this.m_fBoxBBHpRate = 0;
        this.m_iBoxBBHPDropNum = 0;
        this.m_fBoxHeartRate = 0;
        this.m_iBoxHeartDropNum = 0;
        this.m_fBoxFarmRate = 0;
        this.m_iBoxFarmDropNum = 0;
        this.m_fBoxGoldRate = 0;
        this.m_iBoxGoldDropNum = 0;

        this.m_lstDropItem.Clear();
        this.m_lstDropItemRate.Clear();
        //float sumItemRate = 0;
        //for (int i = 0; i < this.m_cGateTable.VecDropItemID.Count; i++)
        //{
        //    if (this.m_cGateTable.VecDropItemID[i] > 0)
        //    {
        //        this.m_lstDropItem.Add(this.m_cGateTable.VecDropItemID[i]);
        //        sumItemRate += this.m_cGateTable.VecDropRate[i];
        //    }
        //}
        //for (int i = 0; i < this.m_cGateTable.VecDropItemID.Count; i++)
        //{
        //    if (this.m_cGateTable.VecDropItemID[i] > 0)
        //    {
        //        this.m_lstDropItemRate.Add(this.m_cGateTable.VecDropRate[i] / sumItemRate);
        //    }
            
        //}

        base.Show();
        this.m_bMenuShow = false;
    }

    /// <summary>
    /// 战斗结束回调
    /// </summary>
    private void BattleFinish()
    {
        this.m_iCurLayer++;
        if ( this.m_iMaxLayer <= this.m_iCurLayer)
        {
            Hiden();

            GUIDE_FUNCTION.SHOW_STORY(GUIDE_FUNCTION.STORY_FIRST_FIGHT_END1, StoryFinishCallBack2);

            //SendAgent.SendGuideStep(Role.role.GetBaseProperty().m_iPlayerId, -1);

            //this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).ShowImmediately();
            //GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BATTLEMENU).Hiden();
            //int[] vecItemTmp = new int[ITEM_MAX_NUM];
            //for (int i = 0; i<this.m_vecItem.Length ;i++ )
            //{
            //    vecItemTmp[i] = 0;
            //    if (this.m_vecItem[i] != null)
            //        vecItemTmp[i] = this.m_vecItem[i].m_iNum;
            //}
            //int friendBattle_id = Role.role.GetBattleFriendProperty().GetSelectFriend().m_iID;
            //SendAgent.SendBattleGateEndReq(Role.role.GetBaseProperty().m_iPlayerId, WorldManager.s_iCurrentWorldId, WorldManager.s_iCurrentAreaIndex,
            //    WorldManager.s_iCurrentDungeonIndex, WorldManager.s_iCurrentGateIndex, this.m_iGoldNum + this.m_cGateTable.RewardJinbi, this.m_iFarmNum + this.m_cGateTable.RewardFram,friendBattle_id, this.m_lstSoul, this.m_lstItem, this.m_lstItemNum , vecItemTmp,
            //    this.m_iRecordMaxShuijingNum , this.m_iTotalShuijingNum , this.m_iRecordMaxXinNum , this.m_iTotalXinNum , this.m_iRoundMaxHurt , this.m_iRoundMaxSparkNum , this.m_iTotalSparkNum , this.m_iTotalSkillNum
            //    );
            return;
        }

        HidenTopPanel();
        GUIBattleNext nextgui = (GUIBattleNext)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BATTLE_NEXT);
        nextgui.ShowLayer(this.m_iCurLayer + 1, this.m_iMaxLayer, "新手引导", "新手引导", BattleNextFinish);
    }

    /// <summary>
    /// 下一个战斗动画结束
    /// </summary>
    /// <param name="args"></param>
    private void BattleNextFinish(object[] args)
    {
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BATTLE_NEXT).Hiden();

        if (this.m_iMaxLayer <= this.m_iCurLayer)
        {
            Debug.LogError("关卡结束");
            return;
        }

        if (this.m_iCurLayer + 1 >= this.m_iMaxLayer)
        {
            this.m_bBoss = true;
        }

        GateTable gateTable = GateTableManager.GetInstance().GetGateTable(GATE_ID);
        int orderID = gateTable.VecLayerNum[this.m_iCurLayer];

        MonsterTable[] monsters = new MonsterTable[6];
        //if (!this.m_bJingying && !this.m_bBoss)
        //{
        //    if (GAME_FUNCTION.RANDOM_ONE() <= this.m_fJingyingRate)
        //    {
        //        this.m_bJingying = true;
        //        orderID = this.m_iJingyingOrder;
        //    }
        //}
        MonsterTeamTable teamTable = MonsterTableManager.GetInstance().GetMonsterTeamTable(gateTable.ID, orderID, this.m_lstMonsterOrderID);
        this.m_lstMonsterOrderID.Add(teamTable.OrderID);

        for (int i = 0; i < teamTable.VecMonster.Length; i++)
        {
            monsters[i] = MonsterTableManager.GetInstance().GetMonsterTable(gateTable.ID, teamTable.VecMonster[i]);
        }
        SetBattleTargetHero(monsters);
        this.m_eBattleState = BATTLE_STATE.BATTLE_STATE_BEGIN_BEGIN;

        if (this.m_bBoss)
        {
            SoundManager.GetInstance().PlayBGM(teamTable.BGSound);
        }
        Debug.Log("下一层");
        ShowTopPanel();
    }

    /// <summary>
    /// 战斗失败结束
    /// </summary>
    private void BattleEnd()
    {
        Hiden();

        GUIDE_FUNCTION.SHOW_STORY(GUIDE_FUNCTION.STORY_FIRST_FIGHT_END1, StoryFinishCallBack2);

        //GUIBackFrameTop backtop = (GUIBackFrameTop)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP);
        //backtop.Show();

        //GUIBackFrameBottom backbottom = (GUIBackFrameBottom)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM);
        //backbottom.Show();

        //GUIMain wordmain = (GUIMain)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_MAIN);
        //wordmain.Show();

        //GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();

        //SendAgent.SendGuideStep(Role.role.GetBaseProperty().m_iPlayerId, -1);
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        base.Hiden();
        //背景音乐
        SoundManager.GetInstance().PlayBGM(SOUND_DEFINE.BGM_MAIN);
    }


    ///// <summary>
    ///// 剧情结束回调
    ///// </summary>
    //private void StoryFinishCallBack()
    //{
    //    GUIDE_FUNCTION.SHOW_STORY(GUIDE_FUNCTION.STORY_FIRST_FIGHT_END2, StoryFinishCallBack2);
    //}

    /// <summary>
    /// 剧情结束回调2
    /// </summary>
    private void StoryFinishCallBack2()
    {
        GUIBackFrameTop backtop = (GUIBackFrameTop)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP);
        backtop.Show();

        GUIBackFrameBottom backbottom = (GUIBackFrameBottom)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM);
        backbottom.Show();

        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_HERO_MENU).Show();

        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
    }
}
