using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Base;
using Game.Gfx;
using Game.Resource;
using Game.Media;



//  GUIActivityBattle.cs
//  Author: Lu Zexi
//  2014-01-08



/// <summary>
/// 活动战斗GUI
/// </summary>
public class GUIActivityBattle : GUIBattle
{
    public int m_iDungeonID;    //副本ID
    public int m_iGateID;   //战斗的关卡ID

    private ActivityDungeonTable m_cDungeonTable;   //副本表
    private ActivityGateTable m_cGateTable; //关卡表

    private bool m_bJingying;    //精英是否出现过
    private float m_fJingyingRate;   //精英怪概率
    private int m_iJingyingOrder;    //精英怪序列

    private List<int> m_lstMonsterOrder = new List<int>();  //曾过去的序列

    public GUIActivityBattle(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_ACTIVITY_BATTLE, UILAYER.GUI_PANEL1)
    { 
        //
    }

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

        this.m_iCurLayer = 0;
        this.m_lstMonsterOrder.Clear();

        this.m_cDungeonTable = WorldManager.GetActivityDungeonTable(this.m_iDungeonID);
        this.m_cGateTable = WorldManager.GetActivityGateTable(this.m_iGateID);
        int orderID = this.m_cGateTable.VecLayerNum[this.m_iCurLayer];

        if (this.m_cScene != null)
        {
            GameObject.DestroyImmediate(this.m_cScene);
        }

        this.m_cScene = null;
        this.m_strSceneName = this.m_cGateTable.Scene;

        //设置当前层级
        this.m_iMaxLayer = this.m_cGateTable.MaxLayer;

        //最大金币数和农场点
        this.m_iMaxGold = this.m_cGateTable.MaxGold;
        this.m_iMaxFarm = this.m_cGateTable.MaxFarm;

        //设置标题
        this.m_strTittle = this.m_cDungeonTable.Name;
        this.m_strContent = this.m_cGateTable.Name;

        //设置精英怪
        this.m_bJingying = false;
        this.m_fJingyingRate = this.m_cGateTable.JingYingRate;
        this.m_iJingyingOrder = this.m_cGateTable.JingYing;

        //设置优惠类型
        this.m_eFavType = (FAV_TYPE)WorldManager.s_eCurActivityDungeonFavType;
        if (this.m_eFavType == FAV_TYPE.GOLD_NUM_2)
        {
            this.m_iMaxGold *= 2;
        }
        if (this.m_eFavType == FAV_TYPE.FARM_NUM_2)
        {
            this.m_iMaxFarm *= 2;
        }

        //设置可捕捉的灵魂数
        //this.m_iCanCatchNum = Role.role.GetBaseProperty().m_iMaxHeroCount - Role.role.GetHeroProperty().GetAllHero().Count;

        //设置对手
        MonsterTable[] monsters = new MonsterTable[6];
        ActivityMonsterTeamTable teamTable = ActivityTableManager.GetInstance().GetMonsterTeamTable(this.m_iGateID, orderID, this.m_lstMonsterOrder);

        //背景音乐
		MediaMgr.sInstance.PlayBGM(this.m_cGateTable.BGSound);
//        MediaMgr.PlayBGM(this.m_cGateTable.BGSound);

        this.m_lstMonsterOrder.Add(teamTable.OrderID);

        for (int i = 0; i < teamTable.VecMonster.Length; i++)
        {
            monsters[i] = ActivityTableManager.GetInstance().GetMonsterTable(this.m_iGateID, teamTable.VecMonster[i]);
        }
        SetBattleTargetHero(monsters);

        //设置胜利回调和失败回调
        this.m_delFinishCallBack = BattleFinish;
        this.m_delEndCallBack = BattleEnd;

        //宝箱掉落率
        this.m_fBoxRate = this.m_cGateTable.DropRateBox;
        this.m_fBoxMonsterRate = this.m_cGateTable.DropRateMonsterL;
        this.m_iBoxMonsterTableID = this.m_cGateTable.BoxID;
        this.m_fBoxDropItemRate = this.m_cGateTable.DropItemRate;
        this.m_fBoxBBHpRate = this.m_cGateTable.DropAgerRate;
        this.m_iBoxBBHPDropNum = this.m_cGateTable.DropAnger;
        this.m_fBoxHeartRate = this.m_cGateTable.DropHeartRate;
        this.m_iBoxHeartDropNum = this.m_cGateTable.DropHeart;
        this.m_fBoxFarmRate = this.m_cGateTable.DropFarmRate;
        this.m_iBoxFarmDropNum = this.m_cGateTable.DropFarm;
        this.m_fBoxGoldRate = this.m_cGateTable.DropGoldRate;
        this.m_iBoxGoldDropNum = this.m_cGateTable.DropGold;

        this.m_lstDropItem.Clear();
        this.m_lstDropItemRate.Clear();
        float sumItemRate = 0;
        for (int i = 0; i < this.m_cGateTable.VecDropItemID.Count; i++)
        {
            if (this.m_cGateTable.VecDropItemID[i] != 0)
            {
                this.m_lstDropItem.Add(this.m_cGateTable.VecDropItemID[i]);
                sumItemRate += this.m_cGateTable.VecDropRate[i];
            }
        }
        for (int i = 0; i < this.m_cGateTable.VecDropItemID.Count; i++)
        {
            if (this.m_cGateTable.VecDropItemID[i] != 0)
            {
                this.m_lstDropItemRate.Add(this.m_cGateTable.VecDropRate[i] / sumItemRate);
            }
        }


        //宝箱怪加载
        MonsterTable boxtable = MonsterTableManager.GetInstance().GetMonsterBaoxiangTable(this.m_iBoxMonsterTableID);
        BattleHero boxhero = BattleHeroGenerator.Generator(0, this, boxtable, this.m_eFavType);
        BattleHeroGenerator.GeneratorTargetHeroAysnc(boxhero);
        
        base.Show();
    }

    /// <summary>
    /// 战斗结束回调
    /// </summary>
    private void BattleFinish()
    {
        this.m_iCurLayer++;
        if (this.m_cGateTable.MaxLayer <= this.m_iCurLayer)
        {
            int[] vecItemTmp = new int[ITEM_MAX_NUM];
            for (int i = 0; i < this.m_vecItem.Length; i++)
            {
                vecItemTmp[i] = 0;
                if(this.m_vecItem[i] != null )
                    vecItemTmp[i] = this.m_vecItem[i].m_iNum;
            }
            int friendBattle_id = GLOBAL_DEFINE.m_cSelectBattleFriend.m_iID;
            SendAgent.SendActivityBattleEndReq(Role.role.GetBaseProperty().m_iPlayerId, Role.role.GetBaseProperty().m_iBattleID,
                WorldManager.s_iCurEspDungeonId, WorldManager.s_iCurEspDungeonGateIndex, this.m_iGoldNum + this.m_cGateTable.RewardJinbi, this.m_iFarmNum + this.m_cGateTable.RewardFram, friendBattle_id, this.m_lstSoul, this.m_lstItem, this.m_lstItemNum,vecItemTmp,
                this.m_iRecordMaxShuijingNum, this.m_iTotalShuijingNum, this.m_iRecordMaxXinNum, this.m_iTotalXinNum, this.m_iRoundMaxHurt, this.m_iRoundMaxSparkNum, this.m_iTotalSparkNum, this.m_iTotalSkillNum, this.m_iTotalBoxMonster
                );

            Hiden();
            this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
            GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BATTLEMENU).Hiden();
            return;
        }

        GUIBattleNext nextgui = (GUIBattleNext)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BATTLE_NEXT);
        HidenTopPanel();
        nextgui.ShowLayer(this.m_iCurLayer + 1, this.m_cGateTable.MaxLayer, this.m_cDungeonTable.Name, this.m_cGateTable.Name, BattleNextFinish);

    }
    
        
    /// <summary>
    /// 下一个战斗动画结束
    /// </summary>
    /// <param name="args"></param>
    private void BattleNextFinish(object[] args)
    {
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BATTLE_NEXT).Hiden();

        if (this.m_cGateTable.MaxLayer <= this.m_iCurLayer)
        {
            Debug.LogError("关卡结束");
            return;
        }

        if (this.m_iCurLayer + 1 >= this.m_cGateTable.MaxLayer)
        {
            this.m_bBoss = true;
        }

        int orderID = this.m_cGateTable.VecLayerNum[this.m_iCurLayer];
        MonsterTable[] monsters = new MonsterTable[6];
        if (!this.m_bJingying && !this.m_bBoss)
        {
            if (GAME_FUNCTION.RANDOM_ONE() <= this.m_fJingyingRate)
            {
                this.m_bJingying = true;
                orderID = this.m_iJingyingOrder;
            }
        }
        ActivityMonsterTeamTable teamTable = ActivityTableManager.GetInstance().GetMonsterTeamTable(this.m_iGateID, orderID , this.m_lstMonsterOrder);

        this.m_lstMonsterOrder.Add(teamTable.OrderID);

        for (int i = 0; i < teamTable.VecMonster.Length; i++)
        {
            monsters[i] = ActivityTableManager.GetInstance().GetMonsterTable(this.m_iGateID, teamTable.VecMonster[i]);
        }
        SetBattleTargetHero(monsters);
        this.m_eBattleState = BATTLE_STATE.BATTLE_STATE_BEGIN_BEGIN;

        if (this.m_bBoss)
        {
			MediaMgr.sInstance.PlayBGM(teamTable.BGSound);
//            MediaMgr.PlayBGM(teamTable.BGSound);
        }
        Debug.Log("下一层");

        ShowTopPanel();
    }

    /// <summary>
    /// 战斗失败结束
    /// </summary>
    private void BattleEnd()
    {
        int[] vecItemTmp = new int[ITEM_MAX_NUM];
        for (int i = 0; i < this.m_vecItem.Length; i++)
        {
            vecItemTmp[i] = 0;
            if (this.m_vecItem[i] != null)
                vecItemTmp[i] = this.m_vecItem[i].m_iNum;
        }

        for (int i = 0; i < this.m_vecItem.Length; i++)
        {
            if(this.m_vecItem[i] != null )
            {
                Role.role.GetItemProperty().UpdateBattleItem(this.m_vecItem[i].m_iTableID, this.m_vecItem[i].m_iNum, i);
            }
        }

        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();

        Role.role.GetItemProperty();

        SendAgent.SendActivityBattleFail(Role.role.GetBaseProperty().m_iPlayerId, Role.role.GetBaseProperty().m_iBattleID, vecItemTmp);

        Hiden();
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        base.Hiden();
        //背景音乐
		MediaMgr.sInstance.PlayBGM(SOUND_DEFINE.BGM_ACTIVE);
//        MediaMgr.PlayBGM(SOUND_DEFINE.BGM_ACTIVE);
    }
}
