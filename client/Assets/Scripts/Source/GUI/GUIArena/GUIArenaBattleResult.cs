//Micro.Sanvey
//2014.2.11
//sanvey.china@gmail.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;
using Game.Base;
using Game.Media;

//竞技场战斗结果GUI
public class GUIArenaBattleResult : GUIBase
{
    private const string RES_MAIN = "GUI_ArenaBattleResult";//主资源地址
    private const string PAN_CANCEL = "TopPanel";                 //返回上一层Pan地址
    private const string PAN_INFO = "MainPanel";                        //除了取消按钮的划出panel
    private const string PAN_VS = "VsPanel";  //vs面板
    private const string PAN_SCORE = "ScorePanel";  //成绩面板
    private const string LAB_LINE_LEFT_NO1 = "Lab_TopLineLeftNo1";
    private const string LAB_LINE_LEFT_NO2 = "Lab_TopLineLeftNo2";
    private const string LAB_LINE_RIGHT_NO1 = "Lab_TopLineRightNo1";
    private const string LAB_LINE_RIGHT_NO2 = "Lab_TopLineRightNo2";
    private const string SP_LEFT_BACK = "Spr_LeftBack";
    private const string SP_RIGHT_BACK = "Spr_RightBack";
    private const string HERO_LEFT_1 = "LeftHero1";
    private const string HERO_LEFT_2 = "LeftHero2";
    private const string HERO_LEFT_3 = "LeftHero3";
    private const string HERO_LEFT_4 = "LeftHero4";
    private const string HERO_RIGHT_1 = "RightHero1";
    private const string HERO_RIGHT_2 = "RightHero2";
    private const string HERO_RIGHT_3 = "RightHero3";
    private const string HERO_RIGHT_4 = "RightHero4";
    private const string HERO_LEFT_LEADER = "PlayerLeft";
    private const string HERO_RIGHT_LEADER = "PlayerRight";
    private const string CROWN_TOP = "Spr_LeftTopCrown";
    private const string CROWN_BOTTOM = "Spr_LeftBottomCrown";
    private const string COLLIDER = "Collider";
    private const string HERO_BORDER = "GUI_HeroSelectItem/ItemBorder";
    private const string HERO_FRAME = "GUI_HeroSelectItem/ItemFrame";
    private const string HERO_ICON = "GUI_HeroSelectItem/ItemMonster";
    private const string LB_NAME = "Lb_Name";
    private const string LB_BIG_NAME = "Lb_BigName";
    private const string LB_NEXT = "Lb_next";
    private const string LB_POINT = "Lb_point";
    private const string SLIDE_EXP = "Slide_exp";
    private const string SP_WIN_FRAME = "SP_Frame";
    private const string PAN_ZHANJI = "zhanji";
    private const string LB_ZHANJI = "Lb_zhanji";
    private const string LB_GET_AWARD = "Lb_Award";

    private const string GUI_EFFECT = "GUI_EFFECT";//3d特效资源地址
    private const string EFFECT_CENTER_ANCHOR = "ANCHOR_CENTER";//3d特效父对象
    private const string RES_EFFECT_WIN = "effect_GUI_arenawin";  //胜利特效
    private const string RES_EFFECT_LOSE = "effect_GUI_arenalose";  //失败特效
    private const string RES_EFFECT_RANK_DOWN = "effect_GUI_lankdown";  //官位下降
    private const string RES_EFFECT_RANK_UP = "effect_GUI_lankup";  //官位上升
    private const string RES_EFFECT_BONUS_GET = "effect_GUI_arenabonus";  //获得奖励特效

    private GameObject m_cPanCancel;            //取消Pan
    private GameObject m_cPanInfo;  //除了取消按钮的划出panel
    private GameObject m_cPanVs;
    private GameObject m_cPanScore;
    private UILabel m_cLbLineLeftNo1;
    private UILabel m_cLbLineLeftNo2;
    private UILabel m_cLbLineRightNo1;
    private UILabel m_cLbLineRightNo2;
    private UISprite m_cSpRgihtBack;
    private UISprite m_cSpLeftBack;
    private UISprite m_cSpCrownTop;
    private UISprite m_cSpCrownBottom;
    private GameObject m_cObjRightLeader;
    private GameObject m_cObjLeftLeader;
    private GameObject m_cObjLeftHero1;
    private GameObject m_cObjLeftHero2;
    private GameObject m_cObjLeftHero3;
    private GameObject m_cObjLeftHero4;
    private GameObject m_cObjRightHero1;
    private GameObject m_cObjRightHero2;
    private GameObject m_cObjRightHero3;
    private GameObject m_cObjRightHero4;
    private GameObject m_cCollider;
    private GameObject m_cPanZhanji;
    private UILabel m_cLbZhanji;
    private UISprite m_cSpWinFrame;
    private UISprite m_cSlideExp;
    private UILabel m_cLbPoint;
    private UILabel m_cLbNext;
    private UILabel m_cLbBigName;
    private UILabel m_cLbName;
    private UISprite m_cHeroIconl;
    private UISprite m_cHeroBorder;
    private UISprite m_cHeroFrame;
    private UILabel m_cLbGetAward;

    private GameObject m_cGuiEffect;    //3d特效资源
    private GameObject m_cEffectParent; //3d特效父对象
    private UnityEngine.Object m_cEffectWin; //胜利
    private UnityEngine.Object m_cEffectLose;  //失败
    private GameObject m_cEffectWinObj;
    private GameObject m_cEffectLoseObj;
    private UnityEngine.Object m_cEffectRankDown;
    private UnityEngine.Object m_cEffectRankUp;
    private GameObject m_cEffectRankDownObj;
    private GameObject m_cEffectRankUpObj;
    private UnityEngine.Object m_cEffectBonusGet;  //获得钻石和物品的奖励特效
    private GameObject m_cEffectBonusGetObj;

    /// <summary>
    /// 动画状态
    /// </summary>
    public enum State
    {
        Nomal,
        Start,
        DeadShowBegin,  //死亡展示
        DeadShowIng,
        DeadShowEnd,
        PointHpNumUpBegin,  //HP数字滚动
        PointHpNumUpIng,
        PointHpNumUpEnd,
        GoldHpShowBegin,  //HP王冠显示
        GoldHpShowIng,
        GoldHpShowEnd,
        PointHurtNumUpBegin,  //伤害数字滚动
        PointHurtNumUpIng,
        PointHurtNumUpEnd,
        GoldHurtShowBegin,  //伤害王冠显示
        GoldHurtShowIng,
        GoldHurtShowEnd,
        WinShowBegin,  //胜利失败展示
        WinShowIng,
        WinShowEnd,
        ScoreShowBegin,
        ScoreShowIng,
        ScoreShowEnd,
        SlideRunBegin,
        SlideRunIng,
        SlideRunEnd,
        PVPNameUpBegin,
        PVPNameUpIng,
        PVPNameUpEnd,
        PVPAwardBegin,
        PVPAwardIng,
        PVPAwardEnd,
        End,
    }
    private State m_eState;  //动画播放状态
    private float m_fDis;  //时间控制
    private bool m_bSkillShow;  //技能显示
    private List<ShowHeroData> alldata = new List<ShowHeroData>();
    private int m_iDeadIndex;  //死亡顺序控制

    private const float DEAD_SHOW_TIME = 0.6F;    //死亡显示时间
    private const float NUM_UP_TIME = 2f;  //数字滚动时间
    private const float GOLD_SHOW_TIME = 1f;  //王冠显示时间
    private const float WIN_SHOW_TIME = 2f;  //胜利失败展示时间
    private const float SCORE_SHOW_TIME = 1f;  //分数展示停留时间
    private const float SLIDE_TIME = 1.2f;  // 经验条滚动时间
    private const float PVP_UP_TIME = 2f;  //官位上升时间
    private const float PVP_AWARD_TIME = 1f; // 奖励特效时间
    private const float SHORTTIME = 0.05f; //展示速度
    private float SHOW_COST_TIME;   //展示花费时间
    private bool m_bQuickShow ; //快速展示结算

    private const string WIN_LEADER_FRAME = "Leader2";
    private const string LOSE_LEADER_FRAME = "leader";

    private Vector3 VEC_LEFT = new Vector3(-160, 170, 0);  //左边特效位置
    private Vector3 VEC_RIGHT = new Vector3(160, 170, 0); //右边特效位置
    private Vector3 VEC_HP_RIGHT = new Vector3(142, -17, 0);
    private Vector3 VEC_HP_LEFT = new Vector3(-142, -17, 0);
    private Vector3 VEC_HURT_RIGHT = new Vector3(142, -142, 0);
    private Vector3 VEC_HURT_LEFT = new Vector3(-142, -142, 0);
    private Vector3 VEC_MIDDLE = new Vector3(0, 115, 0); //中间特效位置


    //滚动条显示数据
    private class ShowData
    {
        public int m_iMax;
        public int m_iMin;
        public int m_iNow;
        public int m_iEnd;
        public int m_iMin2;

        private ShowData() { }  //私有化 防止被实例化 空实例化

        public ShowData(int max, int min, int now, int end, int min2)
        {
            m_iMax = max;
            m_iMin = min;
            m_iNow = now;
            m_iEnd = end;
            m_iMin2 = min2;
        }
    }
    private List<ShowData> m_lstSlideDatas;

    //英雄显示
    public HeroDown m_cMyHero1;
    public HeroDown m_cMyHero2;
    public HeroDown m_cMyHero3;
    public HeroDown m_cMyHero4;
    public HeroDown m_cEnemyHero1;
    public HeroDown m_cEnemyHero2;
    public HeroDown m_cEnemyHero3;
    public HeroDown m_cEnemyHero4;
    public HeroLeader m_cMyHeroLeader;
    public HeroLeader m_cEnemyLeader;

    //Data数据
    public List<ShowHeroData> m_lstMyShowHeroDatas = new List<ShowHeroData>();          //用于显示的英雄数据列表
    public List<HeroDown> m_lstMyShowHeroObjs = new List<HeroDown>();                  //用于显示的英雄对象列表
    public List<ShowHeroData> m_lstEnemyShowHeroDatas = new List<ShowHeroData>();          //用于显示的英雄数据列表
    public List<HeroDown> m_lstEnemyShowHeroObjs = new List<HeroDown>();                  //用于显示的英雄对象列表

    public BattleHero[] m_cSelfHeros;  //我方战斗英雄
    public BattleHero[] m_cEnemyHeros; //对方战斗英雄
    public int m_cEnemyLeaderIndex; //对方英雄队长位置
    public int m_iMyHPSum;  //我方剩余HP总和
    public int m_iEnemyHPSum; //敌方剩余HP总和
    public int m_iMyDamageSum; //我方伤害对手总和
    public int m_iEnemyDamageSum; //敌方伤害对手总和
    public int m_iWinLost;  //  0 无胜利; 1 己方胜利; 2 敌方胜利
    private int m_iOldExp;  //原先的EXP
    private int m_iNext = 0; //升降级进程
    private bool m_bIfShowAward; // 是否需要显示官位升级奖励
    private int m_iDiamond;
    private int m_iItemTableID;


    /// <summary>
    /// 用于英雄显示数据
    /// </summary>         
    public class ShowHeroData
    {
        public int m_iHeroTableId;  //英雄TableID
        public float m_fHeroDeadTime;  //英雄死亡时间
        public HeroDown m_cObj;
    }

    /// <summary>
    /// 英雄显示单项， 带有Donw图标
    /// </summary>
    public class HeroDown
    {
        public GameObject m_cItem;
        public UISprite m_cSpHero;
        public UISprite m_cSpBg;
        public UISprite m_cSpHeroBorder;
        public UISprite m_cSpDown;
        public UISprite m_cSpCover;

        private const string SP_BG = "Spr_Bg";
        private const string SP_HERO = "Spr_Icon";
        private const string SP_HERO_BORDER = "Spr_Frame";
        private const string SP_DOWN = "Spr_Result";
        private const string SP_COVER = "ItemCover";

        public HeroDown(GameObject parent)
        {
            m_cItem = parent;

            m_cSpHero = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, SP_HERO);
            m_cSpBg = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, SP_BG);
            m_cSpHeroBorder = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, SP_HERO_BORDER);
            m_cSpDown = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, SP_DOWN);
            m_cSpCover = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, SP_COVER);
        }
    }

    /// <summary>
    /// 队长显示单项
    /// </summary>
    public class HeroLeader
    {
        public GameObject m_cItem;
        public UILabel m_cLbName;
        public UILabel m_cLbBigName;
        public UILabel m_cLbABP;
        public UISprite m_cSpLeaderFrame;
        public UISprite m_cSpLeaderBack;

        public HeroDown heroDown;

        private const string LB_NAME = "Lab_Name";
        private const string LB_BIGNAME = "Lab_BigName";
        private const string LB_ABP = "Lab_ABP";
        private const string SP_WIN_FRAME = "Spr_win_bg";
        private const string SP_BACK = "Leader_Back";

        public HeroLeader(GameObject obj)
        {
            m_cItem = obj;
            heroDown = new HeroDown(m_cItem);
            m_cLbABP = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cItem, LB_ABP);
            m_cLbBigName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cItem, LB_BIGNAME);
            m_cLbName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cItem, LB_NAME);
            m_cSpLeaderFrame = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, SP_WIN_FRAME);
            m_cSpLeaderBack = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, SP_BACK);
        }
    }

    public GUIArenaBattleResult(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_ANRENA_BATTLE_RESULT, UILAYER.GUI_PANEL)
    { }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        this.m_eLoadingState = LOADING_STATE.NONE;
        if (this.m_cGUIObject == null)
        {
            this.m_eLoadingState = LOADING_STATE.START;
            GUI_FUNCTION.AYSNCLOADING_SHOW();
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_MAIN);
			ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_EFFECT_BONUS_GET);
			ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_EFFECT_LOSE);
			ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_EFFECT_RANK_DOWN);
			ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_EFFECT_RANK_UP);
			ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_EFFECT_WIN);
        }
        else
        {
            InitGUI();
        }
        m_bQuickShow = false;
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

            this.m_cPanCancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_CANCEL);
            this.m_cPanInfo = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_INFO);
            this.m_cCollider = GUI_FINDATION.GET_GAME_OBJECT(this.m_cPanInfo, COLLIDER);
            this.m_cCollider.AddComponent<GUIComponentEvent>().AddIntputDelegate(FullClick_OnEvent);

            this.m_cPanVs = GUI_FINDATION.GET_GAME_OBJECT(this.m_cPanInfo, PAN_VS);
            this.m_cPanScore = GUI_FINDATION.GET_GAME_OBJECT(this.m_cPanInfo, PAN_SCORE);

            this.m_cLbLineLeftNo1 = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cPanVs, LAB_LINE_LEFT_NO1);
            this.m_cLbLineLeftNo2 = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cPanVs, LAB_LINE_LEFT_NO2);
            this.m_cLbLineRightNo1 = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cPanVs, LAB_LINE_RIGHT_NO1);
            this.m_cLbLineRightNo2 = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cPanVs, LAB_LINE_RIGHT_NO2);

            this.m_cSpCrownTop = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cPanVs, CROWN_TOP);
            this.m_cSpCrownBottom = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cPanVs, CROWN_BOTTOM);

            this.m_cObjRightLeader = GUI_FINDATION.GET_GAME_OBJECT(this.m_cPanVs, HERO_RIGHT_LEADER);
            this.m_cObjLeftLeader = GUI_FINDATION.GET_GAME_OBJECT(this.m_cPanVs, HERO_LEFT_LEADER);
            this.m_cObjLeftHero1 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cPanVs, HERO_LEFT_1);
            this.m_cObjLeftHero2 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cPanVs, HERO_LEFT_2);
            this.m_cObjLeftHero3 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cPanVs, HERO_LEFT_3);
            this.m_cObjLeftHero4 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cPanVs, HERO_LEFT_4);
            this.m_cObjRightHero1 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cPanVs, HERO_RIGHT_1);
            this.m_cObjRightHero2 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cPanVs, HERO_RIGHT_2);
            this.m_cObjRightHero3 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cPanVs, HERO_RIGHT_3);
            this.m_cObjRightHero4 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cPanVs, HERO_RIGHT_4);
            this.m_cSpLeftBack = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cPanVs, SP_LEFT_BACK);
            this.m_cSpRgihtBack = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cPanVs, SP_RIGHT_BACK);

            m_cEnemyHero1 = new HeroDown(m_cObjLeftHero1);
            m_cEnemyHero2 = new HeroDown(m_cObjLeftHero2);
            m_cEnemyHero3 = new HeroDown(m_cObjLeftHero3);
            m_cEnemyHero4 = new HeroDown(m_cObjLeftHero4);
            m_cEnemyHero1.m_cItem.SetActive(false);
            m_cEnemyHero2.m_cItem.SetActive(false);
            m_cEnemyHero3.m_cItem.SetActive(false);
            m_cEnemyHero4.m_cItem.SetActive(false);

            m_cMyHero1 = new HeroDown(m_cObjRightHero1);
            m_cMyHero2 = new HeroDown(m_cObjRightHero2);
            m_cMyHero3 = new HeroDown(m_cObjRightHero3);
            m_cMyHero4 = new HeroDown(m_cObjRightHero4);
            m_cMyHero1.m_cItem.SetActive(false);
            m_cMyHero2.m_cItem.SetActive(false);
            m_cMyHero3.m_cItem.SetActive(false);
            m_cMyHero4.m_cItem.SetActive(false);

            m_cEnemyLeader = new HeroLeader(m_cObjLeftLeader);
            m_cMyHeroLeader = new HeroLeader(m_cObjRightLeader);

            this.m_cGuiEffect = GUI_FINDATION.FIND_GAME_OBJECT(GUI_EFFECT);

            this.m_cEffectParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGuiEffect, EFFECT_CENTER_ANCHOR);

            this.m_cEffectWin = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_EFFECT_WIN);
			this.m_cEffectLose = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_EFFECT_LOSE);
			this.m_cEffectRankDown = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_EFFECT_RANK_DOWN);
			this.m_cEffectRankUp = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_EFFECT_RANK_UP);
			this.m_cEffectBonusGet = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_EFFECT_BONUS_GET);

            m_cPanZhanji = GUI_FINDATION.GET_GAME_OBJECT(this.m_cPanScore, PAN_ZHANJI);
            m_cLbZhanji = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cPanZhanji, LB_ZHANJI);
            m_cSpWinFrame = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cPanScore, SP_WIN_FRAME);
            m_cSlideExp = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cPanScore, SLIDE_EXP);
            m_cLbPoint = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cPanScore, LB_POINT);
            m_cLbNext = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cPanScore, LB_NEXT);
            m_cLbBigName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cPanScore, LB_BIG_NAME);
            m_cLbName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cPanScore, LB_NAME);
            m_cHeroIconl = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cPanScore, HERO_ICON);
            m_cHeroBorder = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cPanScore, HERO_BORDER);
            m_cHeroFrame = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cPanScore, HERO_FRAME);
            m_cLbGetAward = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cPanScore, LB_GET_AWARD);
        }

        //生成除了动画以外的初始显示数据
        InitData();

        CTween.TweenPosition(this.m_cPanInfo, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, -125, 0), new Vector3(0, -125, 0));
        CTween.TweenPosition(this.m_cPanCancel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-430, 275, 0), new Vector3(0, 275, 0));

        this.m_cGUIMgr.SetCurGUIID(this.m_iID);
        SetLocalPos(Vector3.zero);

        this.m_eState = State.Nomal;
    }
    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //base.Hiden();

        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        CTween.TweenPosition(this.m_cPanInfo, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, -125, 0), new Vector3(640, -125, 0));
        CTween.TweenPosition(this.m_cPanCancel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 275, 0), new Vector3(-430, 275, 0), Destory);

		ResourceMgr.UnloadUnusedResources();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        this.m_cPanCancel = null;
        this.m_cPanInfo = null;
        this.m_cPanVs = null;
        this.m_cPanScore = null;
        this.m_cLbLineLeftNo1 = null;
        this.m_cLbLineLeftNo2 = null;
        this.m_cLbLineRightNo1 = null;
        this.m_cLbLineRightNo2 = null;
        this.m_cSpRgihtBack = null;
        this.m_cSpLeftBack = null;
        this.m_cSpCrownTop = null;
        this.m_cSpCrownBottom = null;
        this.m_cObjRightLeader = null;
        this.m_cObjLeftLeader = null;
        this.m_cObjLeftHero1 = null;
        this.m_cObjLeftHero2 = null;
        this.m_cObjLeftHero3 = null;
        this.m_cObjLeftHero4 = null;
        this.m_cObjRightHero1 = null;
        this.m_cObjRightHero2 = null;
        this.m_cObjRightHero3 = null;
        this.m_cObjRightHero4 = null;
        this.m_cCollider = null;
        this.m_cPanZhanji = null;
        this.m_cLbZhanji = null;
        this.m_cSpWinFrame = null;
        this.m_cSlideExp = null;
        this.m_cLbPoint = null;
        this.m_cLbNext = null;
        this.m_cLbBigName = null;
        this.m_cLbName = null;
        this.m_cHeroIconl = null;
        this.m_cHeroBorder = null;
        this.m_cHeroFrame = null;
        this.m_cLbGetAward = null;

        this.m_cGuiEffect = null;
        this.m_cEffectParent = null;
        this.m_cEffectWinObj = null;
        this.m_cEffectLoseObj = null;
        this.m_cEffectRankDownObj = null;
        this.m_cEffectRankUpObj = null;
        this.m_cEffectBonusGetObj = null;

        this.m_cEffectWin = null;
        this.m_cEffectLose = null;
        this.m_cEffectRankDown = null;
        this.m_cEffectRankUp = null;
        this.m_cEffectBonusGet = null;

        alldata.Clear();

        //英雄显示
        this.m_cMyHero1 = null;
        this.m_cMyHero2 = null;
        this.m_cMyHero3 = null;
        this.m_cMyHero4 = null;
        this.m_cEnemyHero1 = null;
        this.m_cEnemyHero2 = null;
        this.m_cEnemyHero3 = null;
        this.m_cEnemyHero4 = null;
        this.m_cMyHeroLeader = null;
        this.m_cEnemyLeader = null;

        //Data数据
        this.m_lstMyShowHeroDatas.Clear();
        this.m_lstMyShowHeroObjs.Clear();
        this.m_lstEnemyShowHeroDatas.Clear();
        this.m_lstEnemyShowHeroObjs.Clear();

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

        if (!this.IsShow())
            return false;

        switch (this.m_eState)
        {
            case State.Nomal:
                this.m_fDis = GAME_TIME.TIME_FIXED();
                this.m_eState++;
                break;
            case State.Start:
                this.m_cPanScore.SetActive(false);
                this.m_cPanVs.SetActive(true);
                this.m_cSpLeftBack.enabled = false;
                this.m_cSpRgihtBack.enabled = false;
                this.m_cEnemyLeader.m_cSpLeaderBack.enabled = false;
                this.m_cMyHeroLeader.m_cSpLeaderBack.enabled = false;
                this.m_cLbGetAward.enabled = false;
                this.m_cEnemyLeader.m_cSpLeaderFrame.spriteName = LOSE_LEADER_FRAME;
                this.m_cMyHeroLeader.m_cSpLeaderFrame.spriteName = LOSE_LEADER_FRAME;
                this.m_cMyHeroLeader.m_cSpLeaderFrame.MakePixelPerfect();
                this.m_cEnemyLeader.m_cSpLeaderFrame.MakePixelPerfect();

                float fdis = GAME_TIME.TIME_FIXED() - m_fDis;
                if (fdis > 1)
                {
                    //初始化dead依次显示数据
                    alldata = new List<ShowHeroData>();
                    alldata.AddRange(this.m_lstEnemyShowHeroDatas);
                    alldata.AddRange(this.m_lstMyShowHeroDatas);
                    alldata.RemoveAll((q) => { return q.m_fHeroDeadTime == 0; });
                    alldata.Sort((q1, q2) => { return q1.m_fHeroDeadTime.CompareTo(q2.m_fHeroDeadTime); });
                    m_iDeadIndex = 0;
                    this.m_eState++;
                }
                break;
            case State.DeadShowBegin:
                if (m_iDeadIndex == alldata.Count)
                {
                    this.m_eState = State.PointHpNumUpBegin;
                }
                else
                {
                    alldata[m_iDeadIndex].m_cObj.m_cSpCover.enabled = true;
                    alldata[m_iDeadIndex].m_cObj.m_cSpDown.enabled = true;
                    //设置时间
                    this.m_fDis = GAME_TIME.TIME_FIXED();
                    this.SHOW_COST_TIME = DEAD_SHOW_TIME;
                    this.m_eState++;
                }
                break;
            case State.DeadShowIng:
                //快速展示
                if (m_bQuickShow)
                    this.SHOW_COST_TIME = SHORTTIME;

                float dis = GAME_TIME.TIME_FIXED() - m_fDis;
                if (dis > SHOW_COST_TIME)
                {
                    this.m_eState++;
                }
                break;
            case State.DeadShowEnd:
                this.m_iDeadIndex++;
                this.m_eState = State.DeadShowBegin;
                break;
            case State.PointHpNumUpBegin:
                //设置hp初始
                this.m_cLbLineLeftNo1.text = "0";
                this.m_cLbLineRightNo1.text = "0";
                this.m_cLbLineLeftNo1.enabled = true;
                this.m_cLbLineRightNo1.enabled = true;
                //设置时间
                this.m_fDis = GAME_TIME.TIME_FIXED();
                this.SHOW_COST_TIME = NUM_UP_TIME;
                this.m_eState++;
                break;
            case State.PointHpNumUpIng:
                //快速展示
                if (m_bQuickShow)
                    this.SHOW_COST_TIME = SHORTTIME;

                //数字跳动音效
				MediaMgr.sInstance.PlayENV(SOUND_DEFINE.SE_NUM_JUMP);
                //MediaMgr.PlaySoundContinue(SOUND_DEFINE.SE_NUM_JUMP);

                float disNum = GAME_TIME.TIME_FIXED() - m_fDis;
                this.m_cLbLineLeftNo1.text = ((int)(disNum / NUM_UP_TIME * m_iEnemyHPSum)).ToString();
                this.m_cLbLineRightNo1.text = ((int)(disNum / NUM_UP_TIME * m_iMyHPSum)).ToString();
                if (disNum > SHOW_COST_TIME)
                {
                    //数字跳动音效关闭
					MediaMgr.sInstance.StopENV();
//                    MediaMgr.StopSoundContinue();

                    this.m_cLbLineLeftNo1.text = ((int)m_iEnemyHPSum).ToString();
                    this.m_cLbLineRightNo1.text = ((int)m_iMyHPSum).ToString();
                    this.m_eState++;
                }
                break;
            case State.PointHpNumUpEnd:
                this.m_eState++;
                break;
            case State.GoldHpShowBegin:

                if (m_iEnemyHPSum > m_iMyHPSum)
                {
                    m_cSpCrownTop.transform.localPosition = VEC_HP_LEFT;
                }
                else
                {
                    m_cSpCrownTop.transform.localPosition = VEC_HP_RIGHT;
                }
                this.m_cSpCrownTop.enabled = true;

                //设置时间
                this.m_fDis = GAME_TIME.TIME_FIXED();
                this.SHOW_COST_TIME = GOLD_SHOW_TIME;
                this.m_eState++;
                break;
            case State.GoldHpShowIng:
                //快速展示
                if (m_bQuickShow)
                    this.SHOW_COST_TIME = SHORTTIME;

                float disg1 = GAME_TIME.TIME_FIXED() - m_fDis;
                if (disg1 > SHOW_COST_TIME)
                {
                    this.m_eState++;
                }
                break;
            case State.GoldHpShowEnd:
                this.m_eState++;
                break;
            case State.PointHurtNumUpBegin:
                //设置hp初始
                this.m_cLbLineLeftNo2.text = "0";
                this.m_cLbLineRightNo2.text = "0";
                this.m_cLbLineLeftNo2.enabled = true;
                this.m_cLbLineRightNo2.enabled = true;
                //设置时间
                this.m_fDis = GAME_TIME.TIME_FIXED();
                this.SHOW_COST_TIME = NUM_UP_TIME;
                this.m_eState++;
                break;
            case State.PointHurtNumUpIng:
                //快速展示
                if (m_bQuickShow)
                    this.SHOW_COST_TIME = SHORTTIME;

                //数字跳动音效
				MediaMgr.sInstance.PlayENV(SOUND_DEFINE.SE_NUM_JUMP);
//                MediaMgr.PlaySoundContinue(SOUND_DEFINE.SE_NUM_JUMP);

                float disNum2 = GAME_TIME.TIME_FIXED() - m_fDis;
                this.m_cLbLineLeftNo2.text = ((int)(disNum2 / NUM_UP_TIME * m_iEnemyDamageSum)).ToString();
                this.m_cLbLineRightNo2.text = ((int)(disNum2 / NUM_UP_TIME * m_iMyDamageSum)).ToString();
                if (disNum2 > SHOW_COST_TIME)
                {
                    //数字跳动音效关闭
                    //MediaMgr.StopSoundContinue();
					MediaMgr.sInstance.StopENV();

                    this.m_cLbLineLeftNo2.text = ((int)m_iEnemyDamageSum).ToString();
                    this.m_cLbLineRightNo2.text = ((int)m_iMyDamageSum).ToString();
                    this.m_eState++;
                }
                break;
            case State.PointHurtNumUpEnd:
                this.m_eState++;
                break;
            case State.GoldHurtShowBegin:

                if (m_iEnemyDamageSum > m_iMyDamageSum)
                {
                    this.m_cSpCrownBottom.transform.localPosition = VEC_HURT_LEFT;
                }
                else
                {
                    this.m_cSpCrownBottom.transform.localPosition = VEC_HURT_RIGHT;
                }
                this.m_cSpCrownBottom.enabled = true;
                //设置时间
                this.m_fDis = GAME_TIME.TIME_FIXED();
                this.SHOW_COST_TIME = GOLD_SHOW_TIME;
                this.m_eState++;
                break;
            case State.GoldHurtShowIng:
                //快速展示
                if (m_bQuickShow)
                    this.SHOW_COST_TIME = SHORTTIME;

                float disg2 = GAME_TIME.TIME_FIXED() - m_fDis;
                if (disg2 > SHOW_COST_TIME)
                {
                    this.m_eState++;
                }
                break;
            case State.GoldHurtShowEnd:
                this.m_eState++;
                break;
            case State.WinShowBegin:
                CameraManager.GetInstance().ShowGUIEffectCamera();
                this.m_cEffectLoseObj = GameObject.Instantiate(this.m_cEffectLose) as GameObject;
                this.m_cEffectWinObj = GameObject.Instantiate(this.m_cEffectWin) as GameObject;
                if (m_iWinLost == 1)  //胜利
                {
                    //胜利 失败特效
                    m_cEffectLoseObj.transform.parent = this.m_cEffectParent.transform;
                    m_cEffectLoseObj.transform.localScale = Vector3.one;
                    m_cEffectLoseObj.transform.localPosition = VEC_LEFT;

                    m_cEffectWinObj.transform.parent = this.m_cEffectParent.transform;
                    m_cEffectWinObj.transform.localScale = Vector3.one;
                    m_cEffectWinObj.transform.localPosition = VEC_RIGHT;

                    this.m_cSpLeftBack.enabled = true;
                    this.m_cSpRgihtBack.enabled = false;
                    this.m_cEnemyLeader.m_cSpLeaderBack.enabled = true;
                    this.m_cMyHeroLeader.m_cSpLeaderBack.enabled = false;
                    this.m_cEnemyLeader.m_cSpLeaderFrame.spriteName = LOSE_LEADER_FRAME;
                    this.m_cMyHeroLeader.m_cSpLeaderFrame.spriteName = WIN_LEADER_FRAME;
                    this.m_cMyHeroLeader.m_cSpLeaderFrame.MakePixelPerfect();
                    this.m_cEnemyLeader.m_cSpLeaderFrame.MakePixelPerfect();
                }
                else if (m_iWinLost == 2)  //失败
                {
                    //胜利失败特效
                    m_cEffectLoseObj.transform.parent = this.m_cEffectParent.transform;
                    m_cEffectLoseObj.transform.localScale = Vector3.one;
                    m_cEffectLoseObj.transform.localPosition = VEC_RIGHT;

                    m_cEffectWinObj.transform.parent = this.m_cEffectParent.transform;
                    m_cEffectWinObj.transform.localScale = Vector3.one;
                    m_cEffectWinObj.transform.localPosition = VEC_LEFT;

                    this.m_cSpLeftBack.enabled = false;
                    this.m_cSpRgihtBack.enabled = true;
                    this.m_cEnemyLeader.m_cSpLeaderBack.enabled = false;
                    this.m_cMyHeroLeader.m_cSpLeaderBack.enabled = true;
                    this.m_cEnemyLeader.m_cSpLeaderFrame.spriteName = WIN_LEADER_FRAME;
                    this.m_cMyHeroLeader.m_cSpLeaderFrame.spriteName = LOSE_LEADER_FRAME;
                    this.m_cMyHeroLeader.m_cSpLeaderFrame.MakePixelPerfect();
                    this.m_cEnemyLeader.m_cSpLeaderFrame.MakePixelPerfect();

                }
                this.m_cEffectLoseObj.SetActive(true);
                this.m_cEffectWinObj.SetActive(true);

                this.m_eState++;
                break;
            case State.WinShowIng:
                //等待点击
                break;
            case State.WinShowEnd:
                this.m_cEffectLoseObj.SetActive(false);
                this.m_cEffectWinObj.SetActive(false);
                this.m_eState++;
                break;
            case State.ScoreShowBegin:
                this.m_cPanVs.SetActive(false);
                this.m_cPanScore.SetActive(true);
                this.m_cPanZhanji.SetActive(true);

                if (m_iWinLost == 1)  //胜利
                {
                    GameObject.DestroyImmediate(this.m_cEffectLoseObj);
                    this.m_cEffectWinObj.SetActive(true);
                    this.m_cEffectWinObj.transform.localPosition = VEC_MIDDLE;
                }
                else if (m_iWinLost == 2)  //失败
                {
                    GameObject.DestroyImmediate(this.m_cEffectWinObj);
                    this.m_cEffectLoseObj.SetActive(true);
                    this.m_cEffectLoseObj.transform.localPosition = VEC_MIDDLE;
                }
                else  //和局
                {
                    GameObject.DestroyImmediate(this.m_cEffectLoseObj);
                    GameObject.DestroyImmediate(this.m_cEffectWinObj);
                }

                //设置初始字段
				int leaderID = HeroTeam.Get(Role.role.GetBaseProperty().m_iCurrentTeam).m_iLeadID;
                Hero lead = Role.role.GetHeroProperty().GetHero(leaderID);
                GUI_FUNCTION.SET_AVATORS(m_cHeroIconl, lead.m_strAvatarM);
                GUI_FUNCTION.SET_HeroBorderAndBack(m_cHeroBorder, m_cHeroFrame, (Nature)lead.m_eNature);
                this.m_cLbName.text = Role.role.GetBaseProperty().m_strUserName;
                this.m_cLbBigName.text = AthleticsExpTableManager.GetInstance().GetAthleticsNameByPoint(m_iOldExp);
                this.m_cLbZhanji.text = Role.role.GetBaseProperty().m_iPVPWin + "胜" + Role.role.GetBaseProperty().m_iPVPLose + "败";
                if (m_lstSlideDatas != null && m_lstSlideDatas.Count > 0)
                {
                    //滚动条
                    this.m_lstSlideDatas[m_iNext].m_iMax -= this.m_lstSlideDatas[m_iNext].m_iMin;
                    this.m_lstSlideDatas[m_iNext].m_iNow -= this.m_lstSlideDatas[m_iNext].m_iMin;
                    this.m_lstSlideDatas[m_iNext].m_iEnd -= this.m_lstSlideDatas[m_iNext].m_iMin;
                    this.m_lstSlideDatas[m_iNext].m_iMin -= this.m_lstSlideDatas[m_iNext].m_iMin;
                    float distan2 = this.m_lstSlideDatas[m_iNext].m_iMax - this.m_lstSlideDatas[m_iNext].m_iMin;

                    this.m_cSlideExp.fillAmount = (float)this.m_lstSlideDatas[m_iNext].m_iNow / distan2;
                    this.m_cLbPoint.text = this.m_lstSlideDatas[m_iNext].m_iMin2 + this.m_cSlideExp.fillAmount * distan2 + " #f4af21]积分";
                    this.m_cLbNext.text = (int)(distan2 - this.m_cSlideExp.fillAmount * distan2) + " #f4af21]积分";
                }
                else
                {
                    this.m_cSlideExp.fillAmount = 0;
                    this.m_cLbPoint.text = 0 + " #f4af21]积分";
                    int max = AthleticsExpTableManager.GetInstance().GetAhtleticsMaxExp(0);
                    int min = AthleticsExpTableManager.GetInstance().GetAhtleticsMinExp(0);
                    this.m_cLbNext.text = (int)(max - min) + " #f4af21]积分";
                }

                //设置时间
                this.m_fDis = GAME_TIME.TIME_FIXED();
                this.SHOW_COST_TIME = SCORE_SHOW_TIME;
                this.m_eState++;
                break;
            case State.ScoreShowIng:                
                float scdis = GAME_TIME.TIME_FIXED() - m_fDis;
                if (scdis > SHOW_COST_TIME)
                {
                    this.m_eState++;
                }
                break;
            case State.ScoreShowEnd:
                this.m_eState++;
                break;
            case State.SlideRunBegin:
                if (m_lstSlideDatas == null || m_lstSlideDatas.Count == m_iNext)
                {
                    this.m_eState = State.PVPAwardBegin;
                }
                else
                {
                    //设置时间
                    this.m_fDis = GAME_TIME.TIME_FIXED();
                    this.SHOW_COST_TIME = SLIDE_TIME;
                    this.m_eState++;
                }
                break;
            case State.SlideRunIng:

                //数字跳动音效
				MediaMgr.sInstance.PlayENV(SOUND_DEFINE.SE_NUM_JUMP);
//                MediaMgr.PlaySoundContinue(SOUND_DEFINE.SE_NUM_JUMP);

                float ffdis = GAME_TIME.TIME_FIXED() - m_fDis;
                float pp = (ffdis / SLIDE_TIME);
                if ((this.m_lstSlideDatas[m_iNext].m_iNow == this.m_lstSlideDatas[m_iNext].m_iMin && m_iWinLost == 2) || (this.m_lstSlideDatas[m_iNext].m_iMin == Role.role.GetBaseProperty().m_iPVPExp && m_iWinLost == 1))
                {
                    SHOW_COST_TIME = 0;
                }
                
                this.m_lstSlideDatas[m_iNext].m_iMax -= this.m_lstSlideDatas[m_iNext].m_iMin;
                this.m_lstSlideDatas[m_iNext].m_iNow -= this.m_lstSlideDatas[m_iNext].m_iMin;
                this.m_lstSlideDatas[m_iNext].m_iEnd -= this.m_lstSlideDatas[m_iNext].m_iMin;
                this.m_lstSlideDatas[m_iNext].m_iMin -= this.m_lstSlideDatas[m_iNext].m_iMin;
                float distan = this.m_lstSlideDatas[m_iNext].m_iMax - this.m_lstSlideDatas[m_iNext].m_iMin;

                this.m_cSlideExp.fillAmount = (float)this.m_lstSlideDatas[m_iNext].m_iNow / distan + (float)(this.m_lstSlideDatas[m_iNext].m_iEnd - this.m_lstSlideDatas[m_iNext].m_iNow) / distan * pp;
                this.m_cLbPoint.text = this.m_lstSlideDatas[m_iNext].m_iMin2 + Math.Ceiling(this.m_cSlideExp.fillAmount * distan) + " #f4af21]积分";
                this.m_cLbNext.text = Math.Floor(distan - this.m_cSlideExp.fillAmount * distan) + " #f4af21]积分";

                if (ffdis > SHOW_COST_TIME)
                {
                    //数字跳动音效关闭
					MediaMgr.sInstance.StopENV();
//                    MediaMgr.StopSoundContinue();

                    this.m_cSlideExp.fillAmount = this.m_lstSlideDatas[m_iNext].m_iEnd / distan;
                    this.m_cLbPoint.text = (int)(this.m_lstSlideDatas[m_iNext].m_iMin2 + this.m_cSlideExp.fillAmount * distan + 0.5f) + " #f4af21]积分";
                    this.m_cLbNext.text = (int)(distan - this.m_cSlideExp.fillAmount * distan + 0.5f) + " #f4af21]积分";
                    this.m_eState++;
                }
                break;
            case State.SlideRunEnd:
                this.m_iNext++;
                this.m_eState++;
                break;
            case State.PVPNameUpBegin:
                this.m_cPanZhanji.SetActive(false);

                if (this.m_cSlideExp.fillAmount == 0 && m_iNext <= m_lstSlideDatas.Count - 1)
                {
                    CameraManager.GetInstance().ShowGUIEffectCamera();
                    //官位上升音效
					MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_NAME_DOWN);
//                    MediaMgr.PlaySound2(SOUND_DEFINE.SE_NAME_DOWN);
                    //官位降级
                    this.m_cEffectRankDownObj = GameObject.Instantiate(this.m_cEffectRankDown) as GameObject;
                    this.m_cEffectRankDownObj.transform.parent = this.m_cEffectParent.transform;
                    this.m_cEffectRankDownObj.transform.localScale = Vector3.one;
                    this.m_cEffectRankDownObj.transform.localPosition = Vector3.zero;
                    //设置时间
                    this.m_fDis = GAME_TIME.TIME_FIXED();
                    this.SHOW_COST_TIME = PVP_UP_TIME;
                    this.m_eState++;
                }
                else if (this.m_cSlideExp.fillAmount == 1)
                {
                    CameraManager.GetInstance().ShowGUIEffectCamera();
                    //官位上升音效
					MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_NAME_UP);
//                    MediaMgr.PlaySound2(SOUND_DEFINE.SE_NAME_UP);
                    //官位上升
                    this.m_cEffectRankUpObj = GameObject.Instantiate(this.m_cEffectRankUp) as GameObject;
                    this.m_cEffectRankUpObj.transform.parent = this.m_cEffectParent.transform;
                    this.m_cEffectRankUpObj.transform.localScale = Vector3.one;
                    this.m_cEffectRankUpObj.transform.localPosition = Vector3.zero;
                    //设置时间
                    this.m_fDis = GAME_TIME.TIME_FIXED();
                    this.SHOW_COST_TIME = PVP_UP_TIME;
                    this.m_eState++;
                }
                else
                {
                    this.m_eState = State.PVPAwardBegin;
                }
                break;
            case State.PVPNameUpIng:
                float dis1 = GAME_TIME.TIME_FIXED() - m_fDis;
                if (dis1 > 0.5)
                {
                    if (m_lstSlideDatas == null || m_lstSlideDatas.Count == m_iNext)
                    {
                        this.m_eState = State.PVPAwardBegin;
                        break;
                    }
                    int endd = this.m_lstSlideDatas[this.m_iNext].m_iEnd - 1;
                    if (endd < 0)
                    {
                        endd = 0;
                    }
                    if (this.m_cSlideExp.fillAmount == 0)
                        this.m_cLbBigName.text = AthleticsExpTableManager.GetInstance().GetAthleticsNameByPoint(endd);
                    else
                        this.m_cLbBigName.text = AthleticsExpTableManager.GetInstance().GetAthleticsNameByPoint(this.m_lstSlideDatas[this.m_iNext].m_iEnd);
                }
                if (dis1 > SHOW_COST_TIME + 0.5)
                {
                    this.m_eState++;
                }
                break;
            case State.PVPNameUpEnd:
                CameraManager.GetInstance().HidenGUIEffectCamera();
                GameObject.DestroyImmediate(this.m_cEffectRankDownObj);
                GameObject.DestroyImmediate(this.m_cEffectRankUpObj);
                this.m_eState = State.SlideRunBegin;
                break;
            case State.PVPAwardBegin:

                //TweenAlpha talpa = this.m_cLbBigName.GetComponent<TweenAlpha>();
                //if (talpa != null)
                //{
                //    talpa.enabled = false;
                //}
                //m_cLbBigName.alpha = 1f;

                if (this.m_bIfShowAward)
                {
                    //设置奖励播放
                    if (m_iDiamond != 0)
                    {
                        this.m_cLbGetAward.text = "获得钻石：" + m_iDiamond;
                    }
                    else
                    {
                        ItemTable ittable = ItemTableManager.GetInstance().GetItem(m_iItemTableID);
                        if (ittable != null)
                        {
                            this.m_cLbGetAward.text = "获得物品：" + ittable.Name;
                        }
                    }
                    this.m_cLbGetAward.enabled = true;

                    CameraManager.GetInstance().ShowGUIEffectCamera();
                    //官位降级
                    this.m_cEffectBonusGetObj = GameObject.Instantiate(this.m_cEffectBonusGet) as GameObject;
                    this.m_cEffectBonusGetObj.transform.parent = this.m_cEffectParent.transform;
                    this.m_cEffectBonusGetObj.transform.localScale = Vector3.one;
                    this.m_cEffectBonusGetObj.transform.localPosition = Vector3.zero;

                    //设置时间
                    this.m_fDis = GAME_TIME.TIME_FIXED();
                    this.SHOW_COST_TIME = PVP_AWARD_TIME;
                    this.m_eState++;
                }
                else
                {
                    this.m_eState = State.End;
                }
                break;
            case State.PVPAwardIng:
                float dis2 = GAME_TIME.TIME_FIXED() - m_fDis;
                if (dis2 > SHOW_COST_TIME)
                {
                    this.m_eState++;
                }
                break;
            case State.PVPAwardEnd:
                this.m_eState++;
                break;
            case State.End:
                
                break;
            default:
                break;
        }
        return base.Update();
    }

    /// <summary>
    /// 生成除了动画以外的初始显示数据
    /// </summary>
    private void InitData()
    {
        //将列表队长提到显示数据第一列，将空英雄出去，后面的英雄往上排
        //对敌方英雄排序
        m_lstEnemyShowHeroDatas = new List<ShowHeroData>();
        ShowHeroData tmp1 = new ShowHeroData();
        tmp1.m_iHeroTableId = m_cEnemyHeros[m_cEnemyLeaderIndex].m_iTableID;
        tmp1.m_fHeroDeadTime = m_cEnemyHeros[m_cEnemyLeaderIndex].m_fDeadTime;
        m_lstEnemyShowHeroDatas.Add(tmp1);

        for (int i = 0; i < m_cEnemyHeros.Length; i++)
        {
            if (i == m_cEnemyLeaderIndex)  //跳过队长，因为已经加入队列首位了
            {
                continue;
            }
            if (m_cEnemyHeros[i] != null)
            {
                ShowHeroData tmp2 = new ShowHeroData();

                tmp2.m_fHeroDeadTime = m_cEnemyHeros[i].m_fDeadTime;
                tmp2.m_iHeroTableId = m_cEnemyHeros[i].m_iTableID;

                m_lstEnemyShowHeroDatas.Add(tmp2);
            }
        }
        //对我方英雄排序
        m_lstMyShowHeroDatas = new List<ShowHeroData>();
		int leaderID = HeroTeam.Get(Role.role.GetBaseProperty().m_iCurrentTeam).m_iLeadID;
        int leaderIndex = m_cSelfHeros.ToList().FindIndex(q => { return q != null && q.m_iID == leaderID; });
        ShowHeroData tmp11 = new ShowHeroData();
        tmp11.m_iHeroTableId = m_cSelfHeros[leaderIndex].m_iTableID;
        tmp11.m_fHeroDeadTime = m_cSelfHeros[leaderIndex].m_fDeadTime;
        m_lstMyShowHeroDatas.Add(tmp11);

        for (int i = 0; i < m_cSelfHeros.Length; i++)
        {
            if (i == leaderIndex)  //跳过队长，因为已经加入队列首位了
            {
                continue;
            }
            if (m_cSelfHeros[i] != null)
            {
                ShowHeroData tmp2 = new ShowHeroData();

                tmp2.m_fHeroDeadTime = m_cSelfHeros[i].m_fDeadTime;
                tmp2.m_iHeroTableId = m_cSelfHeros[i].m_iTableID;

                m_lstMyShowHeroDatas.Add(tmp2);
            }
        }

        //计算我方总伤害
        m_iMyDamageSum = 0;
        m_cSelfHeros.ToList().ForEach(q => { if (q != null) m_iMyDamageSum += q.m_iTotalDamage; });
        //计算我方剩余总HP
        m_iMyHPSum = 0;
        m_cSelfHeros.ToList().ForEach(q => { if (q != null) m_iMyHPSum += q.m_iHp; });
        //计算敌方总伤害
        m_iEnemyDamageSum = 0;
        m_cEnemyHeros.ToList().ForEach(q => { if (q != null)  m_iEnemyDamageSum += q.m_iTotalDamage; });
        //计算敌方剩余总HP
        m_iEnemyHPSum = 0;
        m_cEnemyHeros.ToList().ForEach(q => { if (q != null)  m_iEnemyHPSum += q.m_iHp; });

        //显示敌我名称
        m_cMyHeroLeader.m_cLbABP.text = this.m_iOldExp.ToString();
        m_cMyHeroLeader.m_cLbName.text = Role.role.GetBaseProperty().m_strUserName;
        m_cMyHeroLeader.m_cLbBigName.text = AthleticsExpTableManager.GetInstance().GetAthleticsNameByPoint(Role.role.GetBaseProperty().m_iPVPExp);
        m_cEnemyLeader.m_cLbABP.text = Role.role.GetBaseProperty().m_iEnemyPVPEXP.ToString();
        m_cEnemyLeader.m_cLbName.text = Role.role.GetBaseProperty().m_strEnemyName;
        m_cEnemyLeader.m_cLbBigName.text = AthleticsExpTableManager.GetInstance().GetAthleticsNameByPoint(Role.role.GetBaseProperty().m_iEnemyPVPEXP);

        //根据实际排序对游戏对象排序
        m_lstMyShowHeroObjs = new List<HeroDown>();
        m_lstMyShowHeroObjs.Add(m_cMyHeroLeader.heroDown);  //我方队长
        m_lstMyShowHeroDatas[0].m_cObj = m_cMyHeroLeader.heroDown;
        for (int i = 0; i < m_lstMyShowHeroDatas.Count - 1; i++)  //装入剩余英雄
        {
            if (i == 0) m_lstMyShowHeroObjs.Add(m_cMyHero1);
            if (i == 1) m_lstMyShowHeroObjs.Add(m_cMyHero2);
            if (i == 2) m_lstMyShowHeroObjs.Add(m_cMyHero3);
            if (i == 3) m_lstMyShowHeroObjs.Add(m_cMyHero4);
            m_lstMyShowHeroDatas[i + 1].m_cObj = m_lstMyShowHeroObjs[i + 1];
        }
        m_lstEnemyShowHeroObjs = new List<HeroDown>();
        m_lstEnemyShowHeroObjs.Add(m_cEnemyLeader.heroDown);
        m_lstEnemyShowHeroDatas[0].m_cObj = m_cEnemyLeader.heroDown;
        for (int i = 0; i < m_lstEnemyShowHeroDatas.Count - 1; i++)
        {
            if (i == 0) m_lstEnemyShowHeroObjs.Add(m_cEnemyHero1);
            if (i == 1) m_lstEnemyShowHeroObjs.Add(m_cEnemyHero2);
            if (i == 2) m_lstEnemyShowHeroObjs.Add(m_cEnemyHero3);
            if (i == 3) m_lstEnemyShowHeroObjs.Add(m_cEnemyHero4);
            m_lstEnemyShowHeroDatas[i + 1].m_cObj = m_lstEnemyShowHeroObjs[i + 1];
        }

        //填充数据 右边是我方，左边是敌方
        for (int i = 0; i < m_lstMyShowHeroDatas.Count; i++)
        {
            HeroTable tmp = HeroTableManager.GetInstance().GetHeroTable(m_lstMyShowHeroDatas[i].m_iHeroTableId);
            GUI_FUNCTION.SET_AVATORS(m_lstMyShowHeroObjs[i].m_cSpHero, tmp.AvatorMRes);
            GUI_FUNCTION.SET_HeroBorderAndBack(m_lstMyShowHeroObjs[i].m_cSpHeroBorder, m_lstMyShowHeroObjs[i].m_cSpBg, (Nature)tmp.Property);
            m_lstMyShowHeroObjs[i].m_cItem.SetActive(true);
        }
        for (int i = 0; i < m_lstEnemyShowHeroDatas.Count; i++)
        {
            HeroTable tmp = HeroTableManager.GetInstance().GetHeroTable(m_lstEnemyShowHeroDatas[i].m_iHeroTableId);
            GUI_FUNCTION.SET_AVATORS(m_lstEnemyShowHeroObjs[i].m_cSpHero, tmp.AvatorMRes);
            GUI_FUNCTION.SET_HeroBorderAndBack(m_lstEnemyShowHeroObjs[i].m_cSpHeroBorder, m_lstEnemyShowHeroObjs[i].m_cSpBg, (Nature)tmp.Property);
            m_lstEnemyShowHeroObjs[i].m_cItem.SetActive(true);
        }

        //隐藏所有Down图标，隐藏win和lose，隐藏总伤害和总剩余HP
        m_lstMyShowHeroObjs.ForEach(q => { q.m_cSpDown.enabled = false; q.m_cSpCover.enabled = false; });
        m_lstEnemyShowHeroObjs.ForEach(q => { q.m_cSpDown.enabled = false; q.m_cSpCover.enabled = false; });

        m_cLbLineLeftNo1.enabled = false;
        m_cLbLineLeftNo2.enabled = false;
        m_cLbLineRightNo1.enabled = false;
        m_cLbLineRightNo2.enabled = false;
        m_cSpCrownTop.enabled = false;
        m_cSpCrownBottom.enabled = false;
    }

    /// <summary>
    /// 全屏点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void FullClick_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (this.m_eState == State.End)
            {

                CameraManager.GetInstance().HidenGUIEffectCamera();
                GameObject.DestroyImmediate(this.m_cEffectBonusGetObj);

                if (m_iWinLost == 1)  //胜利
                {
                    GameObject.DestroyImmediate(this.m_cEffectWinObj);
                }
                else if (m_iWinLost == 2)  //失败
                {
                    GameObject.DestroyImmediate(this.m_cEffectLoseObj);
                }

                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Hiden();


                Hiden();
                GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

                //是否为我的好友 如果是，就不弹出好友申请提示
//                if (Role.role.GetFriendProperty().IsMyFriend(Role.role.GetBaseProperty().m_iEnemyPid))
				if(false)
                {
                    this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Hiden();
                    this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Show();
                    GUIArena arena = (GUIArena)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_ARENA);
                    arena.Show();
                }
                else
                {
                    GUIArenaBattleAddFriend tmp = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_ANRENA_BATTLE_FRIEND) as GUIArenaBattleAddFriend;
                    tmp.m_cEnemyLeader = m_cEnemyHeros[m_cEnemyLeaderIndex];
                    tmp.Show();
                }
            }

            if (m_eState == State.WinShowIng)
            {
                m_eState++;
            }

            if (m_eState == State.ScoreShowIng || m_eState == State.SlideRunIng
                || m_eState == State.PVPNameUpIng || m_eState == State.PVPAwardIng)
            {
                this.SHOW_COST_TIME = SHORTTIME;
            }

            if(m_eState == State.DeadShowIng || m_eState == State.PointHpNumUpIng || m_eState == State.GoldHpShowIng || m_eState == State.PointHurtNumUpIng|| m_eState == State.GoldHurtShowIng)
            {
                this.m_bQuickShow = true;
            }
        }
    }


    /// <summary>
    /// 设置显示数据
    /// </summary>
    /// <param name="selfHeros"></param>
    /// <param name="enemyHeros"></param>
    public void SetData(BattleHero[] selfHeros, BattleHero[] enemyHeros, int enemyLeaderIndex, int WinLose, int oldExp, bool ifAwardShow, int Diamond, int ItemID)
    {
       // oldExp = 0;
       // Role.role.GetBaseProperty().m_iPVPExp = 2000;

        this.m_iDiamond = Diamond;
        this.m_iItemTableID = ItemID;
        this.m_cSelfHeros = selfHeros;
        this.m_cEnemyHeros = enemyHeros;
        this.m_cEnemyLeaderIndex = enemyLeaderIndex;
        this.m_iWinLost = WinLose;
        this.m_iOldExp = oldExp;
        this.m_bIfShowAward = ifAwardShow;
        this.m_iNext = 0;
        this.m_lstSlideDatas = new List<ShowData>();

        int nowExp = Role.role.GetBaseProperty().m_iPVPExp;  //现在的exp

        if (nowExp > oldExp)  //exp增长
        {
            while (oldExp != nowExp)
            {
                int max1 = AthleticsExpTableManager.GetInstance().GetAhtleticsMaxExp(oldExp);
                int min1 = AthleticsExpTableManager.GetInstance().GetAhtleticsMinExp(oldExp);

                ShowData tmp;
                int tonum = 0;
                if (max1 < nowExp)
                {
                    tonum = max1;
                }
                else
                {
                    tonum = nowExp;
                }
                //     tmp = new ShowData(max1, min1, oldExp, tonum, AthleticsExpTableManager.GetInstance().GetAhtleticsMinExp(oldExp));
                tmp = new ShowData(max1, min1, oldExp, tonum, min1);
                oldExp = tonum;
                this.m_lstSlideDatas.Add(tmp);

                if (max1 == oldExp)  //需要进入下一级
                {
                    max1 = AthleticsExpTableManager.GetInstance().GetAhtleticsMaxExp(oldExp);
                    min1 = AthleticsExpTableManager.GetInstance().GetAhtleticsMinExp(oldExp);

                    if (nowExp == min1)
                        this.m_lstSlideDatas.Add(new ShowData(max1, min1, oldExp, tonum, min1));
                }
            }
        }
        else if (nowExp < oldExp)  //exp 下降
        {
            while (oldExp != nowExp)
            {
                int max = AthleticsExpTableManager.GetInstance().GetAhtleticsMaxExp(oldExp);
                int min = AthleticsExpTableManager.GetInstance().GetAhtleticsMinExp(oldExp);

                if (min == this.m_iOldExp)
                    this.m_lstSlideDatas.Add(new ShowData(max, min, this.m_iOldExp, min, min));

                if (min == oldExp)  //需要进入下一级
                {
                    max = AthleticsExpTableManager.GetInstance().GetAhtleticsMaxExp(oldExp - 1);
                    min = AthleticsExpTableManager.GetInstance().GetAhtleticsMinExp(oldExp - 1);
                }


                int tonum = 0;
                if (min > nowExp)
                {
                    tonum = min;
                }
                else
                {
                    tonum = nowExp;
                }


                //ShowData tmp = new ShowData(max, min, oldExp, tonum, AthleticsExpTableManager.GetInstance().GetAhtleticsMinExp(oldExp));
                ShowData tmp = new ShowData(max, min, oldExp, tonum, min);
                this.m_lstSlideDatas.Add(tmp);

                oldExp = tonum;
            }
        }
        else
        {
            m_lstSlideDatas = null;
        }

        //for (int i = 0; i < m_lstSlideDatas.Count; i++)
        //{
        //    Debug.LogError(m_lstSlideDatas[i].m_iMax + " - " + m_lstSlideDatas[i].m_iMin + " - " + m_lstSlideDatas[i].m_iNow + " - " + m_lstSlideDatas[i].m_iEnd);
        //}

        //test
        //m_bIfShowAward = true;
        //m_iDiamond = 2;
        //m_iItemTableID = 1015;

    }
}