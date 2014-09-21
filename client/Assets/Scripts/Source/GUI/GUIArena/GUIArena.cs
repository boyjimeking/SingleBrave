 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;
using Game.Media;

//竞技场GUI
//Author Sunyi
//2014-1-26
public class GUIArena : GUIBase
{
    private const string RES_MAIN = "GUI_Arena";//主资源地址
    private const string BUTTON_BACK = "Btn_Back";//返回按钮地址
    private const string BUTTON_JOIN = "Btn_Join";//参加战斗按钮地址
    private const string BUTTON_LVINTELLIGENCE = "Btn_LvIntelligence";//等级情报按钮地址
    private const string BUTTON_BATTLERECORD = "Btn_BattleRecord";//战绩情报按钮地址
    private const string BUTTON_RANKINGS = "Btn_BattleRankings";//排行榜按钮地址
    private const string BUTTON_TEAMEDITOR = "Btn_TeamEditor";//队伍编成按钮地址

    private const string GOLD = "TopPanel/Gold";
    private const string LB_GOLD_NAME = "TopPanel/Gold/Lab_GoldName";  //第一名名称
    private const string LB_GOLD_BIGNAME = "TopPanel/Gold/Lab_GoldBigName";  //第一名官位
    private const string LB_GOLD_AWARD = "TopPanel/Gold/Lab_GoldAward";  //第一名奖励
    private const string SP_GOLD_HERO = "TopPanel/Gold/Spr_GoldIcon";  //英雄头像
    private const string SP_GOLD_ITEM = "TopPanel/Gold/Spr_GlodIcon1"; //第一名奖励

    private const string SILVER = "TopPanel/Silver";
    private const string LB_SILVER_NAME = "TopPanel/Silver/Lab_SilverName";  //第2名名称
    private const string LB_SILVER_BIGNAME = "TopPanel/Silver/Lab_SilverBigName";  //第2名官位
    private const string LB_SILVER_AWARD = "TopPanel/Silver/Lab_SilverAward";  //第2名奖励
    private const string SP_SILVER_HERO = "TopPanel/Silver/Spr_SilverIcon";  //英雄头像
    private const string SP_SILVER_ITEM = "TopPanel/Silver/Spr_SilverIcon1";

    private const string COPER = "TopPanel/Coper";
    private const string LB_COPER_NAME = "TopPanel/Coper/Lab_CoperName";  //第3名名称
    private const string LB_COPER_BIGNAME = "TopPanel/Coper/Lab_CoperBigName";  //第3名官位
    private const string LB_COPER_AWARD = "TopPanel/Coper/Lab_CoperAward";  //第3名奖励
    private const string SP_COPER_HERO = "TopPanel/Coper/Spr_CoperIcon";  //英雄头像
    private const string SP_COPER_ITEM = "TopPanel/Coper/Spr_CoperIcon1";

    //private const string PAN_NONE_POINT = "NoneSportPointPanel"; //竞技场点不足提示框
    private const string BTN_BUY="Btn_Buy";  //购买竞技点数
    private const string BTN_CANCEL="Btn_Cancel"; //取消
    private const string LB_NONE_TITLE = "Lab_Title";  //竞技场不足提示标题
    private const string LB_NONE_MSG = "Lab_Desc"; //竞技场不足提示详情
    private const string LB_NONE_SPORTPOINT = "Lab_Desc";//竞技点数不足

    private GameObject m_cBtnBack;//返回按钮
    private GameObject m_cBtnJoin;//参加战斗按钮
    private GameObject m_cBtnLvIntelligence;//等级情报按钮
    private GameObject m_cBtnBattleRecord;//战绩情报按钮
    private GameObject m_cBtnRankings;//排行版按钮
    private GameObject m_cBtnTeamEditor;//队伍编成按钮
    private UISprite m_cSpGoldHero;
    private UILabel m_cLbGoldName;
    private UILabel m_cLbGoldBigName;
    private UILabel m_cLbGoldAward;
    private UISprite m_cSpGoldItem;
    private UISprite m_cSpSilverHero;
    private UILabel m_cLbSilverName;
    private UILabel m_cLbSilverBigName;
    private UILabel m_cLbSilverAward;
    private UISprite m_cSpSilverItem;
    private UISprite m_cSpCoperHero;
    private UILabel m_cLbCoperName;
    private UILabel m_cLbCoperBigName;
    private UILabel m_cLbCoperAward;
    private UISprite m_cSpCoperItem;
    private GameObject m_cGold;
    private GameObject m_cSilver;
    private GameObject m_cCoper;
    //private GameObject m_cPanNonePoint;  //竞技场点数不足
    //private GameObject m_cBtnBuy;
    //private GameObject m_cBtnCancel;
    //private UILabel m_cLbTitle;
    //private UILabel m_cLbMsg;

    public GUIArena(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_ARENA, UILAYER.GUI_PANEL)
    { }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        //播放村音效
        MediaMgr.sInstance.PlayBGM(SOUND_DEFINE.BGM_ARENA);

        this.m_eLoadingState = LOADING_STATE.NONE;
        if (this.m_cGUIObject == null)
        {
            this.m_eLoadingState = LOADING_STATE.START;
            GUI_FUNCTION.AYSNCLOADING_SHOW();
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_MAIN);
        }
        else
        {
            InitGUI();
        }
    }

    /// <summary>
    /// 初始化GUI
    /// </summary>
    protected override void InitGUI()
    {
        base.Show();

        GUI_FUNCTION.AYSNCLOADING_HIDEN();

        if (this.m_cGUIObject == null)
        {
            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            this.m_cBtnBack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_BACK);
            GUIComponentEvent backEvent = this.m_cBtnBack.AddComponent<GUIComponentEvent>();
            backEvent.AddIntputDelegate(OnClickBackButton);

            this.m_cBtnJoin = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_JOIN);
            GUIComponentEvent joinEvent = this.m_cBtnJoin.AddComponent<GUIComponentEvent>();
            joinEvent.AddIntputDelegate(OnClickJoinButton);

            this.m_cBtnLvIntelligence = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_LVINTELLIGENCE);
            GUIComponentEvent lvIntelligenceEvent = this.m_cBtnLvIntelligence.AddComponent<GUIComponentEvent>();
            lvIntelligenceEvent.AddIntputDelegate(OnClickLvIntelligenceButton);

            this.m_cBtnBattleRecord = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_BATTLERECORD);
            GUIComponentEvent battleRecordEvent = this.m_cBtnBattleRecord.AddComponent<GUIComponentEvent>();
            battleRecordEvent.AddIntputDelegate(OnClickBattleRecordButton);

            this.m_cBtnRankings = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_RANKINGS);
            GUIComponentEvent rankingsEvent = this.m_cBtnRankings.AddComponent<GUIComponentEvent>();
            rankingsEvent.AddIntputDelegate(OnClickRankingsButton);

            this.m_cBtnTeamEditor = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_TEAMEDITOR);
            GUIComponentEvent teamEditorEvent = this.m_cBtnTeamEditor.AddComponent<GUIComponentEvent>();
            teamEditorEvent.AddIntputDelegate(OnClickTeamEditorEventButton);

            this.m_cGold = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GOLD);
            this.m_cLbGoldName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LB_GOLD_NAME);
            this.m_cLbGoldBigName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LB_GOLD_BIGNAME);
            this.m_cLbGoldAward = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LB_GOLD_AWARD);
            this.m_cSpGoldHero = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SP_GOLD_HERO);
            this.m_cSpGoldItem = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SP_GOLD_ITEM);

            this.m_cSilver = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, SILVER);
            this.m_cLbSilverName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LB_SILVER_NAME);
            this.m_cLbSilverBigName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LB_SILVER_BIGNAME);
            this.m_cLbSilverAward = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LB_SILVER_AWARD);
            this.m_cSpSilverHero = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SP_SILVER_HERO);
            this.m_cSpSilverItem = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SP_SILVER_ITEM);

            this.m_cCoper = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, COPER);
            this.m_cLbCoperName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LB_COPER_NAME);
            this.m_cLbCoperBigName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LB_COPER_BIGNAME);
            this.m_cLbCoperAward = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LB_COPER_AWARD);
            this.m_cSpCoperHero = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SP_COPER_HERO);
            this.m_cSpCoperItem = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SP_COPER_ITEM);
        }

        UpdateData();
        this.m_cGUIMgr.SetCurGUIID(this.m_iID);

        CTween.TweenAlpha(this.m_cGUIObject, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, 0f, 1f);

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_ARENA));

    }

    /// <summary>
    /// 更新数据显示
    /// </summary>
    private void UpdateData()
    {
        PVPWeekRankTable gold = PVPWeekRankTableManager.GetInstance().GetWeekRankTable(1);
        PVPWeekRankTable silver = PVPWeekRankTableManager.GetInstance().GetWeekRankTable(2);
        PVPWeekRankTable coper = PVPWeekRankTableManager.GetInstance().GetWeekRankTable(3);

		PVPItemInfo pvpItem = CModelMgr.sInstance.GetModel<PVPItemInfo>();
        if ( pvpItem.Count > 0)
        {
			PVPItemInfo tmpItem = pvpItem[0] as PVPItemInfo;
			this.m_cLbGoldName.text = tmpItem.m_strName;
			this.m_cLbGoldBigName.text = AthleticsExpTableManager.GetInstance().GetAthleticsNameByPoint(tmpItem.m_iPoint);
            this.m_cLbGoldAward.text = gold.Num.ToString();
			GUI_FUNCTION.SET_AVATORS(this.m_cSpGoldHero, HeroTableManager.GetInstance().GetHeroTable(tmpItem.m_iHeroTableID).AvatorMRes);
            SetAwardSprite(m_cSpGoldItem, gold.AwardType, gold.ID);
        }

		if (pvpItem.Count > 1)
        {
			PVPItemInfo tmpItem = pvpItem[1] as PVPItemInfo;
			this.m_cLbSilverName.text = tmpItem.m_strName;
			this.m_cLbSilverBigName.text = AthleticsExpTableManager.GetInstance().GetAthleticsNameByPoint(tmpItem.m_iPoint);
            this.m_cLbSilverAward.text = silver.Num.ToString();
			GUI_FUNCTION.SET_AVATORS(this.m_cSpSilverHero, HeroTableManager.GetInstance().GetHeroTable(tmpItem.m_iHeroTableID).AvatorMRes);
            SetAwardSprite(m_cSpSilverItem, silver.AwardType, silver.ID);
        }

		if (pvpItem.Count > 2)
        {
			PVPItemInfo tmpItem = pvpItem[2] as PVPItemInfo;
			this.m_cLbCoperName.text = tmpItem.m_strName;
			this.m_cLbCoperBigName.text = AthleticsExpTableManager.GetInstance().GetAthleticsNameByPoint(tmpItem.m_iPoint);
            this.m_cLbCoperAward.text = coper.Num.ToString();
			GUI_FUNCTION.SET_AVATORS(this.m_cSpCoperHero, HeroTableManager.GetInstance().GetHeroTable(tmpItem.m_iHeroTableID).AvatorMRes);
            SetAwardSprite(m_cSpCoperItem, coper.AwardType, coper.ID);
        }

		switch (pvpItem.Count)
        {
            case 0:
                this.m_cGold.SetActive(false);
                this.m_cSilver.SetActive(false);
                this.m_cCoper.SetActive(false);
                break;
            case 1:
                this.m_cGold.SetActive(true);
                this.m_cSilver.SetActive(false);
                this.m_cCoper.SetActive(false);
                break;
            case 2:
                this.m_cGold.SetActive(true);
                this.m_cSilver.SetActive(true);
                this.m_cCoper.SetActive(false);
                break;
            case 3:
                this.m_cGold.SetActive(true);
                this.m_cSilver.SetActive(true);
                this.m_cCoper.SetActive(true);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 设置奖励图片
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="type"></param>
    /// <param name="tableID"></param>
    private void SetAwardSprite(UISprite sp,GiftType type,int tableID)
    {
        switch (type)
        {
            case GiftType.Gold:
                sp.spriteName = "zell_thum";
                break;
            case GiftType.FarmPoint:
                sp.spriteName = "karma_thum";
                break;
            case GiftType.Diamond:
                sp.spriteName = "gem";
                break;
            case GiftType.FriendPoint:
                sp.spriteName = "friend_p_thum";
                break;
            case GiftType.Hero:
                HeroTable table2 = HeroTableManager.GetInstance().GetHeroTable(tableID);
                GUI_FUNCTION.SET_AVATORS(sp, table2.AvatorMRes);
                break;
            case GiftType.Item:
                ItemTable table = ItemTableManager.GetInstance().GetItem(tableID);
                GUI_FUNCTION.SET_ITEMS(sp, table.SpiritName);
                break;
            default:
                break;
        }
    }


    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //base.Hiden();

        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        CTween.TweenAlpha(this.m_cGUIObject, 0, GAME_DEFINE.FADEOUT_GUI_TIME, 1f, 0f , Destory);

		ResourceMgr.UnloadUnusedResources();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        this.m_cBtnBack = null;
        this.m_cBtnJoin = null;
        this.m_cBtnLvIntelligence = null;
        this.m_cBtnBattleRecord = null;
        this.m_cBtnRankings = null;
        this.m_cBtnTeamEditor = null;
        this.m_cSpGoldHero = null;
        this.m_cLbGoldName = null;
        this.m_cLbGoldBigName = null;
        this.m_cLbGoldAward = null;
        this.m_cSpGoldItem = null;
        this.m_cSpSilverHero = null;
        this.m_cLbSilverName = null;
        this.m_cLbSilverBigName = null;
        this.m_cLbSilverAward = null;
        this.m_cSpSilverItem = null;
        this.m_cSpCoperHero = null;
        this.m_cLbCoperName = null;
        this.m_cLbCoperBigName = null;
        this.m_cLbCoperAward = null;
        this.m_cSpCoperItem = null;
        this.m_cGold = null;
        this.m_cSilver = null;
        this.m_cCoper = null;

        base.Destory();
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    /// <returns></returns>
    public override bool Update()
    {
        //资源加载等待
        switch (this.m_eLoadingState)
        {
            case LOADING_STATE.START:
                this.m_eLoadingState++;
                return false;
            case LOADING_STATE.LOADING:
                if (ResourceMgr.GetProgress() >= 1f && ResourceMgr.IsComplete())
                {
                    this.m_eLoadingState++;
                }
                return false;
            case LOADING_STATE.END:
                InitGUI();
                this.m_eLoadingState++;
                break;
        }

        return base.Update();
    }

    /// <summary>
    /// 返回按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickBackButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            GUIMain main = (GUIMain)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_MAIN);
            main.Show();
            this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
        }
    }

    /// <summary>
    /// 参加战斗按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickJoinButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            //Cost超限控制
			HeroTeam heroTeam = CModelMgr.sInstance.GetModel<HeroTeam>();
            if (heroTeam.GetCurTeamCost() > RoleExpTableManager.GetInstance().GetMaxCost(Role.role.GetBaseProperty().m_iLevel))
            {
                GUI_FUNCTION.MESSAGEM(null, GAME_FUNCTION.STRING(STRING_DEFINE.INFO_COST_OVER_MAX));
                return;
            }

            //检查是否有竞技点
            if (Role.role.GetBaseProperty().sportpoint == 0)
            {
                GUI_FUNCTION.MESSAGEL_(BugSport_OnEvent, "花费" + GAME_DEFINE.DiamondSportPointCost + "个钻石可将#FF0000]竞技点全回复", "btn_buy", "btn_buy1");
                return;
            }

            SendAgent.SendPVPEnemyGet5Req(Role.role.GetBaseProperty().m_iPlayerId);
        }
    }

    /// <summary>
    /// 等级情报按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickLvIntelligenceButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            GUIArenaRewardInfo rewordInfo = (GUIArenaRewardInfo)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_ARENAREWORDINFO);
            rewordInfo.Show();
        }
    }

    /// <summary>
    /// 战绩情报按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickBattleRecordButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            SendAgent.SendPVPDetailGetReq(Role.role.GetBaseProperty().m_iPlayerId);


            //Hiden();
            //GUIArenaBattleIntelligence battleIntelligence = (GUIArenaBattleIntelligence)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_ARENABATTLEINTELLIGENCE);
            //battleIntelligence.Show();
        }
    }

    /// <summary>
    /// 排行榜按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickRankingsButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            SendAgent.SendPVPBattleRankReq(Role.role.GetBaseProperty().m_iPlayerId);
        }
    }

    /// <summary>
    /// 队伍编成按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickTeamEditorEventButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();

            GUITeamEditor tmpeditor = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_TEAM_EDITOR) as GUITeamEditor;
            tmpeditor.Show(TeamEditCallBack);
        }
    }

    /// <summary>
    /// 购买竞技点
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void BugSport_OnEvent(bool ok)
    {
        if(ok)
        {
            this.Hiden();
            this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Show();
            this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
            this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FISTFIGHTPOINTRESTORATION).Show();
        }
    }

    ///// <summary>
    ///// 取消购买竞技点
    ///// </summary>
    ///// <param name="info"></param>
    ///// <param name="args"></param>
    //private void CancelSport_OnEvent(GUI_INPUT_INFO info, object[] args)
    //{
    //    if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
    //    {
    //        this.m_cPanNonePoint.SetActive(false);
    //    }
    //}

    /// <summary>
    /// 队伍编辑界面返回回调函数
    /// </summary>
    private void TeamEditCallBack()
    {
        this.Show();

        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Hiden();
    }
}