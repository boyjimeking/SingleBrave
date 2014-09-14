using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Base;
using Game.Resource;
using Game.Gfx;
using Game.Media;


//  GUIBattlePVP.cs
//  Author: Lu Zexi
//  2014-02-07




/// <summary>
/// 战斗PVPGUI
/// </summary>
public class GUIBattlePVP : IGUIBattle
{
    public delegate void CALL_BACK();   //回调委托

    private const string RES_MAIN = "GUI_ArenaBattle"; //主资源
    private const string RES_HURT_NUM = "BATTLE_NUM1"; //伤害数字资源
    private const string RES_RECOVER_NUM = "BATTLE_NUM_EX"; //恢复数字资源
    private const string RES_SPARK_EFFECT = "effect_spark";    //Spark特效资源
    private const string RES_CRITICAL_EFFECT = "effect_critical";  //Critical特效资源
    private const string RES_XIN_MESH = "BATTLE_Xin";   //心点模型
    private const string RES_SHUIJING_MESH = "BATTLE_Shuijing"; //水晶模型
    private const string RES_FIGHT_BEGIN = "effect_arenafight_begin";  //战斗开始
    private const string RES_WIN_EFFECT = "effect_arenawin"; //胜利特效
    private const string RES_LOSE_EFFECT = "effect_arenalose";   //失败特效
    private const string RES_TIME_OUT_EFFECT = "effect_arenatimeup"; //时间超时
    private const string RES_SKILL_AVATAR = "BATTLE_SkillAvatar"; //技能头像
    private const string RES_DEBUFF_DU_EFFECT = "effect_debuff_du";    //毒DEBUFF特效
    private const string RES_DEBUFF_MA_EFFECT = "effect_debuff_mabi";    //麻痹特效
    private const string RES_DEBUFF_FENGYIN_EFFECT = "effect_debuff_zuzhou";   //封印特效
    private const string RES_DEBUFF_XURUO_EFFECT = "effect_debuff_xuruo"; //虚弱特效
    private const string RES_DEBUFF_POJIA_EFFECT = "effect_debuff_pojia"; //破甲特效
    private const string RES_DEBUFF_POREN_EFFECT = "effect_debuff_poren"; //破刃特效
    private const string RES_SKILL_SHOW_BG_EFFECT = "effect_cutin_";    //技能展示属性背景
    private const string RES_BATTLE_FONT1 = "BATTLE_Font_1";    //战斗字体1
    private const string RES_BATTLE_FONT2 = "BATTLE_Font_2";    //战斗字体2
    private const string RES_BATTLE_FONT3 = "BATTLE_Font_3";    //战斗字体3
    private const string RES_BATTLE_FONT4 = "BATTLE_Font_4";    //战斗字体4

    private const float MAX_BATTLE_TIME = 99;   //最大对战时间

    protected BATTLE_STATE m_eBattleState = BATTLE_STATE.BATTLE_STATE_BEGIN_BEGIN; //当前战斗状态
    protected int m_iUIState = 0; //UI状态

    protected const int UI_STATE_NONE = 0;    //UI无状态
    protected const int SKILL_SHOW_STATE = 2;   //技能展示状态

    protected const int BATTLE_WIN_NONE = 0;      //无胜利
    protected const int BATTLE_WIN_SELF = 1;      //己方胜利
    protected const int BATTLE_WIN_TARGET = 2;    //敌方胜利

    protected const int WIN_TYPE_NONE = 0;  //胜利类型
    protected const int WIN_TYPE_NORMAL = 1;    //普通胜利
    protected const int WIN_TYPE_PERFECT = 2;   //完美胜利
    protected const int WIN_TYPE_TIME = 3;  //时间胜利

    /// <summary>
    /// 战斗状态
    /// </summary>
    protected enum BATTLE_STATE
    { 
        BATTLE_STATE_INIT_BEGIN = 1,    //战斗初始化开始
        BATTLE_STATE_INIT1,  //战斗初始化1
        BATTLE_STATE_INIT2,  //战斗初始化2
        BATTLE_STATE_INIT_END,  //战斗初始化结束
        BATTLE_STATE_BEGIN_BEGIN,   //战斗开始
        BATTLE_STATE_BEGIN_BEGIN1,   //战斗开始1
        BATTLE_STATE_BEGIN_BEGIN2,   //战斗开始2
        BATTLE_STATE_BEGIN, //战斗开始
        BATTLE_STATE_BEGIN_END, //战斗开始
        BATTLE_STATE_SELF_ATTACK_BEGIN, //己方攻击开始
        BATTLE_STATE_SELF_ATTACK,   //己方攻击
        BATTLE_STATE_SELF_ATTACK_END,   //己方攻击结束
        BATTLE_STATE_GET_ITEM_BEGIN,    //获取物品开始
        BATTLE_STATE_GET_ITEM,  //获取物品
        BATTLE_STATE_GET_ITEM_END,  //获取物品结束
        BATTLE_STATE_ENEMY_ATTACK_BEGIN,    //敌人攻击开始
        BATTLE_STATE_ENEMY_ATTACK,  //敌人攻击
        BATTLE_STATE_ENEMY_ATTACK_END,  //敌人攻击结束
        BATTLE_STATE_GET_TARGET_ITEM_BEGIN,    //敌方获取物品开始
        BATTLE_STATE_GET_TARGET_ITEM,  //敌方获取物品
        BATTLE_STATE_GET_TARGET_ITEM_END,  //敌方获取物品结束
        BATTLE_STATE_TIME_OUT_BEGIN,    //时间超时开始
        BATTLE_STATE_TIME_OUT,  //时间超时
        BATTLE_STATE_TIME_OUT_END,  //时间超时END
        BATTLE_STATE_RESULT_BEGIN,  //结果计算开始
        BATTLE_STATE_RESULT,    //结果计算
        BATTLE_STATE_RESULT_END,    //结果计算结束
        BATTLE_STATE_END_BEGIN, //结束开始
        BATTLE_STATE_END1,  //结束
        BATTLE_STATE_END2,  //结束
        BATTLE_STATE_END_END,   //结束结束
    }

    protected const int HERO_MAX_NUM = 5; //英雄最多数量
    protected const int NATURE_NUM = 6; //属性数量

    private const string BATTLE_PARENT = "BATTLE_PVP";    //战场父节点
    private const string BATTLE_BBPOS = "BBPOS";        //BB技能释放点
    private const string BATTLE_GOLD_POS = "GOLDPOS"; //金币位置
    private const string BATTLE_FARM_POS = "FARMPOS"; //农场点位置
    private const string BATTLE_SOUL_POS = "SOULPOS"; //灵魂点位置
    private const string BATTLE_TARGET_POS = "TARGET_POS/pos";    //目标点
    private const string BATTLE_TARGET_ATTACK_POS = "TARGET_ATTACK_POS/pos"; //目标攻击点
    private const string BATTLE_MYSELF_POS = "MYSELF_POS/pos";    //自身点
    private const string BATTLE_MYSQL_ATTACK_POS = "MYSELF_ATTACK_POS/pos";  //自身攻击点

    private const string SKILL_SHOW_HEAD_PATH = "effect_cutin_head"; //头像
    private const string SKILL_SHOW_LABEL_PATH = "effect_cutin_zidiwen/Label";   //文字
    private const string SKILL_SHOW_BG_PATH = "effect_cutin_zidiwen";    //背景

    private Vector3 BLACK_GROUND_START = new Vector3(640f, 260f, 0);    //开幕
    private Vector3 BLACK_GROUND_END = new Vector3(0, 260f, 0);     //关幕

    protected GameObject m_cBattleParent; //战场父节点
    private GameObject m_cBattleBBPos; //战场技能移动位置点
    private GameObject m_cBattleGoldPos;  //金币点位置
    private GameObject m_cBattleFarmPointPos;    //农场点位置
    private GameObject m_cBattleSoulPointPos; //收集点位置
    protected GameObject m_cScene;    //场景
    private GameObject[] m_vecTargetPos;    //目标站点
    private GameObject[] m_vecTargetAttackPos;  //目标攻击点
    private GameObject[] m_vecMyselfPos;    //自身站点
    private GameObject[] m_vecMyselfAttackPos;  //自身攻击点

    private Texture[] m_vecTexSkillShowBG;  //技能展示属性背景

    private UISprite[] m_vecSelfBuf;  //己方BUF
    private UISprite[] m_vecTargetBuf;    //目标BUF

    private BattleHero[] m_vecSelfHero; //己方英雄
    private BattleHero[] m_vecTargetHero;   //目标英雄
    public int m_iTargetLeaderIndex;    //目标英雄队长索引
    private LeaderSkillTable m_cTargetSelfLeaderSkill;  //目标自身队长技能
    public int m_iSelfLeaderIndex;  //自身英雄队长索引
    private LeaderSkillTable m_cSelfLeaderSkill;    //自身队长技能
    public string m_strRoleSelfName;    //自身角色名
    public string m_strRoleTargetName;  //目标角色名
    public int m_iRoleSelfPvpPoint; //自身PVP点
    public int m_iRoleTargetPvpPoint;   //目标PVP点

    //伤害数值
    private const float HURT_NUM_SPEED = 0.04f; //伤害数值上升速度
    private const float HURT_NUM_TIME = 0.7f;    //伤害数字存留时间
    private UnityEngine.Object m_cResHurtNum;  //伤害数字资源
    private UnityEngine.Object m_cResRecoverNum;    //恢复数字资源
    private List<GameObject> m_lstHurtTxt;  //伤害数字
    private List<float> m_lstHurtTxtTime;   //伤害数字开始时间

    //收集品
    private UnityEngine.Object m_cResShuijing;  //水晶点资源
    private UnityEngine.Object m_cResXin;   //心点资源
    private UnityEngine.Object m_cResSkillAvatar;   //技能头像
    private CollectItem m_cCollectItemShuijing; //水晶点
    private CollectItem m_cCollectItemXin;  //心点
    private const float COLLECT_ITEM_DROP_TIME = 1f; //物品掉落时间
    private const float COLLECT_ITEM_SPEED = 10f;  //物品收集速度

    //特效
    private const float SPARK_TIME = 2; //Spark特效时间
    private const float CRITICAL_TIME = 2;   //暴击特效时间
    private const float RESULT_EFFECT_TIME = 2; //结果特效持续时间
    private UnityEngine.Object m_cResSpark; //Spark特效资源
    private UnityEngine.Object m_cResCritical;  //暴击特效资源
    private UnityEngine.Object m_cResFightBegin;    //战斗开始
    private UnityEngine.Object m_cResResultWin; //胜利特效
    private UnityEngine.Object m_cResResultLose;    //失败特效
    private UnityEngine.Object m_cResTimeOut;   //时间超时特效
    private UnityEngine.Object m_cResBattleFont1;   //战斗字体1
    private UnityEngine.Object m_cResBattleFont2;   //战斗字体1
    private UnityEngine.Object m_cResBattleFont3;   //战斗字体1
    private UnityEngine.Object m_cResBattleFont4;   //战斗字体1
    private GameObject m_cFightBegin;   //战斗开始
    private List<GameObject> m_lstSpark;    ///Spark特效列表
    private List<float> m_lstSparkStartTime;   //Spark特效开始时间列表
    private List<GameObject> m_lstCritical;    ///暴击特效列表
    private List<float> m_lstCriticalStartTime;   //暴击特效开始时间列表
    private GameObject m_cResultEffect; //结果特效
    private float m_fResultEffectStartTime; //结果特效开始时间
    public string m_strSceneName;   //场景名字

    //战斗结果数据
    public int m_iRound;    //回合数
    public int m_iBattleResult;    //战斗结果
    protected CALL_BACK m_delFinishCallBack; //正常结束回调
    protected CALL_BACK m_delEndCallBack;    //强制结束回调

    //临时变量
    private const float END_WAIT_TIME = 0.5f;   //结束等待时间
    private const float SKILL_AVATOR_TIME = 1.25f; //秒
    private const float BLACK_GROUND_COST_TIME = 0.5F;    //黑幕移动时间
    private int m_iAutoTargetIndex;   //自动选择目标索引
    private int m_iSelectTargetIndex;    //手动选择索引
    private float m_fEndWaitStartTime;  //结束等待开始时间
    private int m_iBufShowIndex;    //展示BUF索引
    private float m_fBufShowStartTime;  //开始展示BUF时间
    private float m_fSkillAvatarStartTime;  //技能头像开始时间
    private GameObject m_cSkillAvatar;    //技能头像
    private float m_fBattleStartTime;   //战斗开始时间

    //统计
    private int m_iRoundHurt;   //回合内伤害值
    protected int m_iRoundMaxHurt;  //1回合内最大伤害值
    private int m_iRoundSpark;  //回合内SPARK值
    protected int m_iRoundMaxSpark; //1回合内最大SPARK次数
    protected int m_iTotalHurt; //总伤害
    protected int m_iTotalRecover;  //回复总血量
    protected int m_iTotalSpark;    //总SPARK数
    protected int m_iTotalSkill;    //总技能使用次数
    protected int m_iWinType;   //胜利类型

    //GUI
    private const string GUI_SELF_POS = "selfPos/pos"; //己方位置
    private const string GUI_TARGET_POS = "targetPos/pos";   //目标位置
    private const string GUI_SELF_BUFF = "BUF";    //己方BUFF图标
    private const string GUI_TARGET_BUFF = "BUF";  //目标BUFF图标
    private const string GUI_ROLE_SELF_ICON = "TopPanel/Spr_RightIcon";   //己方图标
    private const string GUI_ROLE_TARGET_ICON = "TopPanel/Spr_LeftIcon"; //目标图标
    private const string GUI_ROLE_SELF_NAME = "TopPanel/Lab_RightUserName";   //己方名字
    private const string GUI_ROLE_TARGET_NAME = "TopPanel/Lab_LeftUserName"; //目标名字
    private const string GUI_ROLE_SELF_PVP_POINT = "TopPanel/Lab_RightAbpText";  //己方PVP点
    private const string GUI_ROLE_TARGET_PVP_POINT = "TopPanel/Lab_LeftAbp";    //敌方PVPV点
    private const string GUI_ROLE_SELF_PVP_TITTLE = "TopPanel/Lab_RightBigName"; //己方PVP称谓
    private const string GUI_ROLE_TARGET_PVP_TITTLE = "TopPanel/Lab_LeftBigName";   //目标PVP称谓
    private const string GUI_HERO_SELF_UI = "BottomPanel/RightItem";  //己方人物UI
    private const string GUI_HERO_TARGET_UI = "BottomPanel/LeftItem";    //目标人物UI
    private const string GUI_HERO_HP_TEXT = "Label_Hp"; //HP文字
    private const string GUI_HERO_HP_BAR = "HP_Bg/Sprite_HpContent";  //HP条
    private const string GUI_HERO_NAME = "Label_Name";    //名字
    private const string GUI_HERO_ICON = "Sprite_Icon";    //头像
    private const string GUI_HERO_BBHP_BAR = "WuShuang_Bg/Sprite_HpContent";    //BBHP条
    private const string GUI_HERO_BB_FULL_EFFECT = "WuShuang_Bg/Sprite_HpFull";  //BB满特效
    private const string GUI_HERO_DEAD = "Spr_Down";    //死亡
    private const string GUI_HERO_BLACK = "Spr_BlackBg";   //遮罩
    private const string GUI_HERO_PROPERTY = "Sprite_Property"; //属性
    private const string GUI_BATTLE_TIME = "TopPanel/Spr_Time";  //战斗时间

    private GameObject[] m_vecUISelfPos;    //己方位置
    private GameObject[] m_vecUITargetPos;  //目标位置
    private UISprite m_cRoleSelfIcon;   //角色己方头像
    private UISprite m_cRoleTargetIcon; //角色目标头像
    private UILabel m_cRoleSelfName;    //角色己方名字
    private UILabel m_cRoleTargetName;  //角色目标名字
    private UILabel m_cRoleSelfPvpPoint;    //角色己方PVP点
    private UILabel m_cRoleTargetPvpPoint;  //角色目标PVP点
    private UILabel m_cRoleSelfPvpTittle;   //角色己方PVP称号
    private UILabel m_cRoleTargetPvpTittle; //角色目标PVP称号
    private UILabel[] m_vecHeroSelfHpTxt;   //英雄己方HP
    private UILabel[] m_vecHeroTargetHpTxt; //英雄目标HP
    private GameObject[] m_vecHeroSelfHpBar;  //英雄己方HP条
    private GameObject[] m_vecHeroTargetHpBar;    //英雄目标HP条
    private UILabel[] m_vecHeroSelfName;  //英雄己方名字
    private UILabel[] m_vecHeroTargetName;  //英雄目标名字
    private UISprite[] m_vecHeroSelfIcon;   //英雄己方头像
    private UISprite[] m_vecHeroTargetIcon; //英雄目标头像
    private GameObject[] m_vecHeroSelfBBHPBar;  //英雄己方BBHP条
    private GameObject[] m_vecHeroTargetBBHPBar;    //英雄目标BBHP条
    private GameObject[] m_vecHeroSelfBBFullEffect;    //英雄己方BB满特效
    private GameObject[] m_vecHeroTargetBBFullEffect;    //英雄目标BB满特效
    private GameObject[] m_vecHeroSelfDead; //英雄己方死亡
    private GameObject[] m_vecHeroTargetDead;   //英雄目标死亡
    private GameObject[] m_vecHeroSelfBlack;    //英雄己方遮罩
    private GameObject[] m_vecHeroTargetBlack;  //英雄目标遮罩
    private UISprite[] m_vecHeroSelfProperty; //英雄己方属性
    private UISprite[] m_vecHeroTargetProperty; //英雄目标
    private UILabel m_cBattleTime;  //战斗时间

    /// <summary>
    /// 收集品类
    /// </summary>
    private class CollectItem
    {
        public List<GameObject> m_lstMesh; //物体列表
        public List<Vector3> m_lstCurveStart;  //曲线起始点
        public List<int> m_lstArg;    //参数
        public List<float> m_lstCurveTop;    //曲线顶点
        public List<Vector3> m_lstCurveBottom; //曲线底点
        public List<float> m_lstCurveStartTime; //曲线开始时间
        public List<float> m_lstLineStartTime;  //直线开始时间
        public List<float> m_lstLineCostTime;  //消耗时间
        public List<BattleHero> m_lstBattleHero;    //指定英雄

        public CollectItem()
        {
            this.m_lstMesh = new List<GameObject>();
            this.m_lstCurveStart = new List<Vector3>();
            this.m_lstArg = new List<int>();
            this.m_lstCurveTop = new List<float>();
            this.m_lstCurveBottom = new List<Vector3>();
            this.m_lstCurveStartTime = new List<float>();
            this.m_lstLineStartTime = new List<float>();
            this.m_lstLineCostTime = new List<float>();
            this.m_lstBattleHero = new List<BattleHero>();
        }

        /// <summary>
        /// 清除数据
        /// </summary>
        public void Clear()
        {
            this.m_lstCurveBottom.Clear();
            this.m_lstCurveStart.Clear();
            this.m_lstCurveStartTime.Clear();
            this.m_lstCurveTop.Clear();
            this.m_lstLineCostTime.Clear();
            this.m_lstLineStartTime.Clear();
            this.m_lstBattleHero.Clear();
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Destory()
        {
            for (int i = 0; i < this.m_lstMesh.Count; i++)
            {
                if (this.m_lstMesh[i] != null)
                {
                    GameObject.Destroy(this.m_lstMesh[i]);
                }
                this.m_lstMesh[i] = null;
            }
            this.m_lstMesh.Clear();
            this.m_lstArg.Clear();
            this.m_lstCurveStart.Clear();
            this.m_lstCurveBottom.Clear();
            this.m_lstCurveTop.Clear();
            this.m_lstCurveStartTime.Clear();
            this.m_lstLineStartTime.Clear();
            this.m_lstLineCostTime.Clear();
            this.m_lstBattleHero.Clear();
        }

        /// <summary>
        /// 更新曲线
        /// </summary>
        public bool UpdateCurve()
        {
            bool finish = true;
            for (int i = 0; i < this.m_lstCurveStartTime.Count; i++)
            {
                float dis = GAME_TIME.TIME_FIXED() - this.m_lstCurveStartTime[i];
                if (dis > COLLECT_ITEM_DROP_TIME)
                {
                    this.m_lstMesh[i].transform.localPosition = this.m_lstCurveBottom[i];
                }
                else
                {
                    float rate = dis / COLLECT_ITEM_DROP_TIME;
                    this.m_lstMesh[i].transform.localPosition = CMath.Curve(this.m_lstCurveStart[i], this.m_lstCurveBottom[i], this.m_lstCurveTop[i], rate);
                    this.m_lstMesh[i].transform.localEulerAngles = new Vector3(0, 0, 360 * rate);
                    finish = false;
                }
            }
            return finish;
        }
    }


    public GUIBattlePVP(GUIManager guiMgr, int guiid, GUILAYER layer)
        : base(guiMgr, guiid, layer)
    {
        this.m_strSceneName = "BATTLE_Scene9";

        this.m_cCollectItemShuijing = new CollectItem();
        this.m_cCollectItemXin = new CollectItem();

        this.m_lstHurtTxt = new List<GameObject>();
        this.m_lstHurtTxtTime = new List<float>();
        this.m_lstSpark = new List<GameObject>();
        this.m_lstSparkStartTime = new List<float>();
        this.m_lstCritical = new List<GameObject>();
        this.m_lstCriticalStartTime = new List<float>();

        this.m_vecTexSkillShowBG = new Texture[NATURE_NUM];
        this.m_vecUISelfPos = new GameObject[HERO_MAX_NUM];
        this.m_vecUITargetPos = new GameObject[HERO_MAX_NUM];
        this.m_vecSelfBuf = new UISprite[HERO_MAX_NUM];
        this.m_vecTargetBuf = new UISprite[HERO_MAX_NUM];
        this.m_vecSelfHero = new BattleHero[HERO_MAX_NUM];
        this.m_vecTargetHero = new BattleHero[HERO_MAX_NUM];
        this.m_vecTargetPos = new GameObject[HERO_MAX_NUM];
        this.m_vecTargetAttackPos = new GameObject[HERO_MAX_NUM];
        this.m_vecMyselfPos = new GameObject[HERO_MAX_NUM];
        this.m_vecMyselfAttackPos = new GameObject[HERO_MAX_NUM];

        this.m_vecHeroSelfHpTxt = new UILabel[HERO_MAX_NUM];
        this.m_vecHeroTargetHpTxt = new UILabel[HERO_MAX_NUM];
        this.m_vecHeroSelfHpBar = new GameObject[HERO_MAX_NUM];
        this.m_vecHeroTargetHpBar = new GameObject[HERO_MAX_NUM];
        this.m_vecHeroSelfName = new UILabel[HERO_MAX_NUM];
        this.m_vecHeroTargetName = new UILabel[HERO_MAX_NUM];
        this.m_vecHeroSelfIcon = new UISprite[HERO_MAX_NUM];
        this.m_vecHeroTargetIcon = new UISprite[HERO_MAX_NUM];
        this.m_vecHeroSelfBBHPBar = new GameObject[HERO_MAX_NUM];
        this.m_vecHeroTargetBBHPBar = new GameObject[HERO_MAX_NUM];
        this.m_vecHeroSelfBBFullEffect = new GameObject[HERO_MAX_NUM];
        this.m_vecHeroTargetBBFullEffect = new GameObject[HERO_MAX_NUM];
        this.m_vecHeroSelfDead = new GameObject[HERO_MAX_NUM];
        this.m_vecHeroTargetDead = new GameObject[HERO_MAX_NUM];
        this.m_vecHeroSelfBlack = new GameObject[HERO_MAX_NUM];
        this.m_vecHeroTargetBlack = new GameObject[HERO_MAX_NUM];
        this.m_vecHeroSelfProperty = new UISprite[HERO_MAX_NUM];
        this.m_vecHeroTargetProperty = new UISprite[HERO_MAX_NUM];

        this.m_lstDropItem = new List<int>();
        this.m_lstDropItemRate = new List<float>();
    }

    /// <summary>
    /// 获取回合数
    /// </summary>
    /// <returns></returns>
    public override int GetRoundNum()
    {
        return this.m_iRound;
    }

    /// <summary>
    /// 获取敌人列表
    /// </summary>
    /// <returns></returns>
    public override BattleHero[] GetVecEnemy()
    {
        return this.m_vecTargetHero;
    }

    /// <summary>
    /// 获取自身列表
    /// </summary>
    /// <returns></returns>
    public override BattleHero[] GetVecSelf()
    {
        return this.m_vecSelfHero;
    }

    /// <summary>
    /// 获取BB位置
    /// </summary>
    /// <returns></returns>
    public override GameObject GetBattleBBPos()
    {
        return this.m_cBattleBBPos;
    }

    /// <summary>
    /// 获取最小HP己方英雄
    /// </summary>
    /// <returns></returns>
    public override BattleHero GetMinHPSelf()
    {
        float MIN = float.MaxValue;
        int index = -1;
        for (int i = 0; i < this.m_vecSelfHero.Length; i++)
        {
            if (this.m_vecSelfHero[i] != null && this.m_vecSelfHero[i].m_iHp < MIN && !this.m_vecSelfHero[i].m_bDead)
            {
                MIN = this.m_vecSelfHero[i].m_iHp;
                index = i;
            }
        }
        if (index >= 0 && index < this.m_vecSelfHero.Length)
        {
            return this.m_vecSelfHero[index];
        }
        return null;
    }

    /// <summary>
    /// 获取敌方最小HP目标
    /// </summary>
    /// <returns></returns>
    public override BattleHero GetMinHPTarget()
    {
        float MIN = float.MaxValue;
        int index = -1;
        for (int i = 0; i < this.m_vecTargetHero.Length; i++)
        {
            if ( this.m_vecTargetHero[i] != null && this.m_vecTargetHero[i].m_iHp < MIN && !this.m_vecTargetHero[i].m_bDead)
            {
                MIN = this.m_vecTargetHero[i].m_iHp;
                index = i;
            }
        }
        if (index >= 0 && index < this.m_vecTargetHero.Length)
        {
            return this.m_vecTargetHero[index];
        }
        return null;
    }

    /// <summary>
    /// 获取最小HP己方英雄
    /// </summary>
    /// <returns></returns>
    public override BattleHero GetMaxHPSelf()
    {
        return null;
    }

    /// <summary>
    /// 自动获取敌人
    /// </summary>
    /// <returns></returns>
    public BattleHero GetEnemyAuto()
    {
        if (this.m_iSelectTargetIndex < 0)
        {

            if (this.m_iAutoTargetIndex < 0)
            {
                int index = 0;
                for (int i = 0; i < HERO_MAX_NUM; i++)
                {
                    if (this.m_vecTargetHero[index] != null && !this.m_vecTargetHero[index].m_bDead)
                    {
                        this.m_iAutoTargetIndex = index;
                        return this.m_vecTargetHero[index];
                    }
                    index = (index + 1) % HERO_MAX_NUM;
                }
                return null;
            }
            else
            {
                int index = this.m_iAutoTargetIndex;
                int tmpIndex = index;
                for (int i = 0; i < HERO_MAX_NUM; i++)
                {
                    if (this.m_vecTargetHero[index] != null && !this.m_vecTargetHero[index].m_bDead)
                    {
                        tmpIndex = index;
                        if (this.m_vecTargetHero[index].m_iDummyHP > 0)
                        {
                            this.m_iAutoTargetIndex = index;
                            return this.m_vecTargetHero[index];
                        }
                    }
                    index = (index + 1) % HERO_MAX_NUM;
                }

                if (this.m_vecTargetHero[tmpIndex] != null && !this.m_vecTargetHero[tmpIndex].m_bDead)
                {
                    return this.m_vecTargetHero[tmpIndex];
                }

                return null;
            }
        }
        else
        {
            for (int i = 0; i < HERO_MAX_NUM; i++)
            {
                if (this.m_vecTargetHero[this.m_iSelectTargetIndex] != null && !this.m_vecTargetHero[this.m_iSelectTargetIndex].m_bDead)
                {
                    return this.m_vecTargetHero[this.m_iSelectTargetIndex];
                }
                this.m_iSelectTargetIndex = (this.m_iSelectTargetIndex + 1) % HERO_MAX_NUM;
            }
        }

        return null;
    }

    /// <summary>
    /// 自动获取己方
    /// </summary>
    /// <returns></returns>
    public override BattleHero GetSelfAuto()
    {
        int index = GAME_FUNCTION.RANDOM(0, HERO_MAX_NUM);
        for (int i = 0; i < HERO_MAX_NUM; i++)
        {
            if (this.m_vecSelfHero[index] != null && !this.m_vecSelfHero[index].m_bDead)
            {
                return this.m_vecSelfHero[index];
            }
            index = (index + 1) % HERO_MAX_NUM;
        }
        return null;
    }

    /// <summary>
    /// 获取己方HP随机不满
    /// </summary>
    /// <returns></returns>
    public BattleHero GetSelfHPNotFULL()
    {
        int index = GAME_FUNCTION.RANDOM(0, HERO_MAX_NUM);
        //随机血没满的
        for (int i = 0; i < HERO_MAX_NUM; i++)
        {
            if (this.m_vecSelfHero[index] != null && !this.m_vecSelfHero[index].m_bDead && this.m_vecSelfHero[index].m_iHp > 0 && this.m_vecSelfHero[index].m_iHp < this.m_vecSelfHero[index].m_cMaxHP.GetFinalData())
            {
                return this.m_vecSelfHero[index];
            }
            index = (index + 1) % HERO_MAX_NUM;
        }

        //否则，随机一个
        for (int i = 0; i < HERO_MAX_NUM; i++)
        {
            if (this.m_vecSelfHero[index] != null && !this.m_vecSelfHero[index].m_bDead && this.m_vecSelfHero[index].m_iHp > 0)
            {
                return this.m_vecSelfHero[index];
            }
            index = (index + 1) % HERO_MAX_NUM;
        }

        return null;
    }

    /// <summary>
    /// 自动获取敌方人物
    /// </summary>
    /// <returns></returns>
    public override BattleHero GetTargetAuto()
    {
        int index = GAME_FUNCTION.RANDOM(0, HERO_MAX_NUM);
        for (int i = 0; i < HERO_MAX_NUM; i++)
        {
            if (this.m_vecTargetHero[index] != null && !this.m_vecTargetHero[index].m_bDead)
            {
                return this.m_vecTargetHero[index];
            }
            index = (index + 1) % HERO_MAX_NUM;
        }
        return null;
    }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        base.Show();

        this.m_eBattleState = BATTLE_STATE.BATTLE_STATE_INIT_BEGIN;

        //ResourceMgr.ClearAsyncLoad();

        GUI_FUNCTION.AYSNCLOADING_SHOW();

        //battle font
        ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_BATTLE_FONT1);
        ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_BATTLE_FONT2);
        ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_BATTLE_FONT3);
        ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_BATTLE_FONT4);

        ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_MAIN);

        ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + this.m_strSceneName);
        ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_HURT_NUM);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_RECOVER_NUM);

		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_SPARK_EFFECT);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_CRITICAL_EFFECT);

		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_SHUIJING_MESH);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_XIN_MESH);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_SKILL_AVATAR);

		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_DEBUFF_DU_EFFECT);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_DEBUFF_XURUO_EFFECT);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_DEBUFF_MA_EFFECT);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_DEBUFF_POJIA_EFFECT);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_DEBUFF_POREN_EFFECT);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_DEBUFF_FENGYIN_EFFECT);

		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_FIGHT_BEGIN);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_WIN_EFFECT);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_LOSE_EFFECT);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_TIME_OUT_EFFECT);

        for (int i = 0; i < 6; i++)
        {
			ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_SKILL_SHOW_BG_EFFECT + (i + 1));
        }

        for( int i = 0 ; i<this.m_vecSelfHero.Length ; i++ )
        {
            BattleHeroGenerator.GeneratorHeroAysnc(this.m_vecSelfHero[i]);
        }

        for (int i = 0; i < this.m_vecTargetHero.Length; i++)
        {
            BattleHeroGenerator.GeneratorHeroAysnc(this.m_vecTargetHero[i]);
        }

        SetLocalPos(Vector3.one*0xFFFFF);
    }

    /// <summary>
    /// 初始化展示
    /// </summary>
    private void InitShow()
    {
        this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_MAIN)) as GameObject;
        ResourceMgr.UnloadResource(RES_MAIN);
        this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
        this.m_cGUIObject.transform.localScale = Vector3.one;

		this.m_cResBattleFont1 = ResourceMgr.LoadAsset(RES_BATTLE_FONT1) as UnityEngine.Object;
        ResourceMgr.UnloadResource(RES_BATTLE_FONT1);
		this.m_cResBattleFont2 = ResourceMgr.LoadAsset(RES_BATTLE_FONT2) as UnityEngine.Object;
        ResourceMgr.UnloadResource(RES_BATTLE_FONT2);
		this.m_cResBattleFont3 = ResourceMgr.LoadAsset(RES_BATTLE_FONT3) as UnityEngine.Object;
        ResourceMgr.UnloadResource(RES_BATTLE_FONT3);
		this.m_cResBattleFont4 = ResourceMgr.LoadAsset(RES_BATTLE_FONT4) as UnityEngine.Object;
        ResourceMgr.UnloadResource(RES_BATTLE_FONT4);

		this.m_cResHurtNum = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_HURT_NUM);
        ResourceMgr.UnloadResource(RES_HURT_NUM);
		this.m_cResRecoverNum = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_RECOVER_NUM);
        ResourceMgr.UnloadResource(RES_RECOVER_NUM);

		this.m_cResSpark = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_SPARK_EFFECT);
        ResourceMgr.UnloadResource(RES_SPARK_EFFECT);
		this.m_cResCritical = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_CRITICAL_EFFECT);
        ResourceMgr.UnloadResource(RES_CRITICAL_EFFECT);

		this.m_cResShuijing = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_SHUIJING_MESH);
        ResourceMgr.UnloadResource(RES_SHUIJING_MESH);
		this.m_cResXin = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_XIN_MESH);
        ResourceMgr.UnloadResource(RES_XIN_MESH);
		this.m_cResSkillAvatar = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_SKILL_AVATAR);
        ResourceMgr.UnloadResource(RES_SKILL_AVATAR);

		this.m_cResDebuffDu = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_DEBUFF_DU_EFFECT);
        ResourceMgr.UnloadResource(RES_DEBUFF_DU_EFFECT);
		this.m_cResDebuffXuruo = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_DEBUFF_XURUO_EFFECT);
        ResourceMgr.UnloadResource(RES_DEBUFF_XURUO_EFFECT);
		this.m_cResDebuffMa = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_DEBUFF_MA_EFFECT);
        ResourceMgr.UnloadResource(RES_DEBUFF_MA_EFFECT);
		this.m_cResDebuffPojia = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_DEBUFF_POJIA_EFFECT);
        ResourceMgr.UnloadResource(RES_DEBUFF_POJIA_EFFECT);
		this.m_cResDebuffPoren = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_DEBUFF_POREN_EFFECT);
        ResourceMgr.UnloadResource(RES_DEBUFF_POREN_EFFECT);
		this.m_cResDebuffFengyin = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_DEBUFF_FENGYIN_EFFECT);
        ResourceMgr.UnloadResource(RES_DEBUFF_FENGYIN_EFFECT);

		this.m_cResFightBegin = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_FIGHT_BEGIN);
        ResourceMgr.UnloadResource(RES_FIGHT_BEGIN);
		this.m_cResResultWin = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_WIN_EFFECT);
        ResourceMgr.UnloadResource(RES_WIN_EFFECT);
		this.m_cResResultLose = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_LOSE_EFFECT);
        ResourceMgr.UnloadResource(RES_LOSE_EFFECT);
		this.m_cResTimeOut = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_TIME_OUT_EFFECT);
        ResourceMgr.UnloadResource(RES_TIME_OUT_EFFECT);

        for (int i = 0; i < NATURE_NUM; i++)
        {
			this.m_vecTexSkillShowBG[i] = (Texture)ResourceMgr.LoadAsset(RES_SKILL_SHOW_BG_EFFECT + (i + 1));
            ResourceMgr.UnloadResource(RES_SKILL_SHOW_BG_EFFECT + (i + 1));
        }

        this.m_cBattleParent = GUI_FINDATION.FIND_GAME_OBJECT(BATTLE_PARENT);
        this.m_cBattleBBPos = GUI_FINDATION.GET_GAME_OBJECT(this.m_cBattleParent, BATTLE_BBPOS);
        this.m_cBattleGoldPos = GUI_FINDATION.GET_GAME_OBJECT(this.m_cBattleParent, BATTLE_GOLD_POS);
        this.m_cBattleFarmPointPos = GUI_FINDATION.GET_GAME_OBJECT(this.m_cBattleParent, BATTLE_FARM_POS);
        this.m_cBattleSoulPointPos = GUI_FINDATION.GET_GAME_OBJECT(this.m_cBattleParent, BATTLE_SOUL_POS);


        for (int i = 0; i < HERO_MAX_NUM; i++)
        {
            this.m_vecTargetPos[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_cBattleParent, BATTLE_TARGET_POS + (i + 1));
            this.m_vecTargetAttackPos[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_cBattleParent, BATTLE_TARGET_ATTACK_POS + (i + 1));
            this.m_vecMyselfPos[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_cBattleParent, BATTLE_MYSELF_POS + (i + 1));
            this.m_vecMyselfAttackPos[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_cBattleParent, BATTLE_MYSQL_ATTACK_POS + (i + 1));
        }

        //GUI
        for (int i = 0; i<HERO_MAX_NUM; i++)
        {
            this.m_vecUISelfPos[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_SELF_POS + (i + 1));
            this.m_vecUITargetPos[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_TARGET_POS + (i + 1));
            this.m_vecSelfBuf[i] = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_vecUISelfPos[i], GUI_SELF_BUFF);
            this.m_vecSelfBuf[i].enabled = false;
            this.m_vecTargetBuf[i] = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_vecUITargetPos[i], GUI_TARGET_BUFF);
            this.m_vecTargetBuf[i].enabled = false;

            //
            this.m_vecHeroSelfHpTxt[i] = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, GUI_HERO_SELF_UI + (i + 1) + "/" + GUI_HERO_HP_TEXT);
            this.m_vecHeroTargetHpTxt[i] = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, GUI_HERO_TARGET_UI + (i + 1) + "/" + GUI_HERO_HP_TEXT);
            this.m_vecHeroSelfHpBar[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_HERO_SELF_UI + (i + 1) + "/" + GUI_HERO_HP_BAR);
            this.m_vecHeroTargetHpBar[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_HERO_TARGET_UI + (i + 1) + "/" + GUI_HERO_HP_BAR);
            this.m_vecHeroSelfName[i] = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, GUI_HERO_SELF_UI + (i + 1) + "/" + GUI_HERO_NAME);
            this.m_vecHeroTargetName[i] = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, GUI_HERO_TARGET_UI + (i + 1) + "/" + GUI_HERO_NAME);
            this.m_vecHeroSelfIcon[i] = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, GUI_HERO_SELF_UI + (i + 1) + "/" + GUI_HERO_ICON);
            this.m_vecHeroTargetIcon[i] = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, GUI_HERO_TARGET_UI + (i + 1) + "/" + GUI_HERO_ICON);
            this.m_vecHeroSelfBBHPBar[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_HERO_SELF_UI + (i + 1) + "/" + GUI_HERO_BBHP_BAR);
            this.m_vecHeroTargetBBHPBar[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_HERO_TARGET_UI + (i + 1) + "/" + GUI_HERO_BBHP_BAR);
            this.m_vecHeroSelfBBFullEffect[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_HERO_SELF_UI + (i + 1) + "/" + GUI_HERO_BB_FULL_EFFECT);
            this.m_vecHeroTargetBBFullEffect[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_HERO_TARGET_UI + (i + 1) + "/" + GUI_HERO_BB_FULL_EFFECT);
            this.m_vecHeroSelfDead[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_HERO_SELF_UI + (i + 1) + "/" + GUI_HERO_DEAD);
            this.m_vecHeroTargetDead[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_HERO_TARGET_UI + (i + 1) + "/" + GUI_HERO_DEAD);
            this.m_vecHeroSelfBlack[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_HERO_SELF_UI + (i + 1) + "/" + GUI_HERO_BLACK);
            this.m_vecHeroTargetBlack[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_HERO_TARGET_UI + (i + 1) + "/" + GUI_HERO_BLACK);
            this.m_vecHeroSelfProperty[i] = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, GUI_HERO_SELF_UI + (i + 1) + "/" + GUI_HERO_PROPERTY);
            this.m_vecHeroTargetProperty[i] = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, GUI_HERO_TARGET_UI + (i + 1) + "/" + GUI_HERO_PROPERTY);
        }
        this.m_cRoleSelfIcon = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject , GUI_ROLE_SELF_ICON);
        this.m_cRoleTargetIcon = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject , GUI_ROLE_TARGET_ICON);
        this.m_cRoleSelfName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject , GUI_ROLE_SELF_NAME);
        this.m_cRoleTargetName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject , GUI_ROLE_TARGET_NAME);
        this.m_cRoleSelfPvpPoint = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject , GUI_ROLE_SELF_PVP_POINT);
        this.m_cRoleTargetPvpPoint = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject , GUI_ROLE_TARGET_PVP_POINT);
        this.m_cRoleSelfPvpTittle = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject , GUI_ROLE_SELF_PVP_TITTLE);
        this.m_cRoleTargetPvpTittle = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, GUI_ROLE_TARGET_PVP_TITTLE);
        this.m_cBattleTime = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, GUI_BATTLE_TIME);


        CameraManager.GetInstance().ShowBattlePVPCamera();
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Hiden();

        this.m_iUIState = UI_STATE_NONE;
        this.m_iBattleResult = BATTLE_WIN_NONE;
        this.m_cGUIMgr.SetCurGUIID(this.m_iID);

        //场景
		this.m_cScene = GameObject.Instantiate(ResourceMgr.LoadAsset(this.m_strSceneName) as UnityEngine.Object) as GameObject;
        this.m_cScene.transform.parent = this.m_cBattleParent.transform;
        this.m_cScene.transform.localScale = Vector3.one;
        this.m_cScene.transform.localPosition = Vector3.zero;
        if (!GAME_SETTING.s_bENEffectSwitch)
        {
            SwitchSceneEffect(false);
        }

        //己方英雄
        for (int i = 0; i < this.m_vecSelfHero.Length; i++)
        {
            if (this.m_vecSelfHero[i] != null)
            {
                if (this.m_vecSelfHero[i] != null && this.m_vecSelfHero[i].GetGfxObject() != null)
                    this.m_vecSelfHero[i].GetGfxObject().Destory();
                BattleHeroGenerator.GeneratorHeroGfxAysnc(this.m_vecSelfHero[i], this, this.m_cBattleParent, this.m_vecUISelfPos[i], this.m_vecMyselfPos[i], this.m_vecMyselfAttackPos[i], true);
                SetUIHeroData(this.m_vecSelfHero[i]);
            }
            else
            {
                SetUISelfHeroActive(i, false);
            }
        }

        //目标英雄
        for (int i = 0; i < this.m_vecTargetHero.Length; i++)
        {
            if (this.m_vecTargetHero[i] != null)
            {
                if (this.m_vecTargetHero[i] != null && this.m_vecTargetHero[i].GetGfxObject() != null)
                    this.m_vecTargetHero[i].GetGfxObject().Destory();
                BattleHeroGenerator.GeneratorHeroGfxAysnc(this.m_vecTargetHero[i], this, this.m_cBattleParent, this.m_vecUITargetPos[i], this.m_vecTargetPos[i], this.m_vecTargetAttackPos[i], true);
                SetUIHeroData(this.m_vecTargetHero[i]);
            }
            else
            {
                SetUITargetHeroActive(i, false);
            }

        }

        SetUIRoleSelf();
        SetUIRoleTarget();

        //统计
        this.m_iRoundMaxHurt = 0;
        this.m_iRoundMaxSpark = 0;
        this.m_iTotalHurt = 0;
        this.m_iTotalRecover = 0;
        this.m_iTotalSpark = 0;
        this.m_iTotalSkill = 0;

        GC.Collect();

        SetLocalPos(Vector3.zero);
    }


    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        base.Hiden();

        CameraManager.GetInstance().HidenBattlePVPCamera();
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
		ResourceMgr.UnloadUnusedResources();

        //if( this.m_cGUIObject != null )
        //    GameObject.DestroyImmediate(this.m_cGUIObject);

        //SetLocalPos(Vector3.one * 0xFFFF);

        //this.m_cResHurtNum = null;
        //this.m_cResRecoverNum = null;

        //this.m_cResSpark = null;
        //this.m_cResCritical = null;

        //this.m_cResShuijing = null;
        //this.m_cResXin = null;
        //this.m_cResSkillAvatar = null;

        //this.m_cResDebuffDu = null;
        //this.m_cResDebuffXuruo = null;
        //this.m_cResDebuffMa = null;
        //this.m_cResDebuffPojia = null;
        //this.m_cResDebuffPoren = null;
        //this.m_cResDebuffFengyin = null;

        //this.m_cResFightBegin = null;
        //this.m_cResResultWin = null;
        //this.m_cResResultLose = null;
        //this.m_cResTimeOut = null;

        //for (int i = 0; i < NATURE_NUM; i++)
        //{
        //    this.m_vecTexSkillShowBG[i] = null;
        //}

        ////销毁物体
        //if (this.m_cScene != null)
        //{
        //    GameObject.DestroyImmediate(this.m_cScene);
        //}
        //this.m_cScene = null;

        //for (int i = 0; i < this.m_vecSelfHero.Length; i++)
        //{
        //    if (this.m_vecSelfHero[i] != null)
        //    {
        //        this.m_vecSelfHero[i].Destory();
        //    }
        //}

        //for (int i = 0; i < this.m_vecTargetHero.Length; i++)
        //{
        //    if (this.m_vecTargetHero[i] != null)
        //    {
        //        this.m_vecTargetHero[i].Destory();
        //    }
        //}

        //for (int i = 0; i < this.m_lstHurtTxt.Count; i++)
        //{
        //    if (this.m_lstHurtTxt[i] != null)
        //    {
        //        GameObject.Destroy(this.m_lstHurtTxt[i]);
        //    }
        //    this.m_lstHurtTxt[i] = null;
        //}
        //this.m_lstHurtTxt.Clear();
        //this.m_lstHurtTxtTime.Clear();

        //for (int i = 0; i < this.m_lstSpark.Count; i++)
        //{
        //    if (this.m_lstSpark[i] != null)
        //    {
        //        GameObject.Destroy(this.m_lstSpark[i]);
        //    }
        //    this.m_lstSpark[i] = null;
        //}
        //this.m_lstSpark.Clear();
        //this.m_lstSparkStartTime.Clear();

        //for (int i = 0; i < this.m_lstCritical.Count; i++)
        //{
        //    if (this.m_lstCritical[i] != null)
        //    {
        //        GameObject.Destroy(this.m_lstCritical[i]);
        //    }
        //    this.m_lstCritical[i] = null;
        //}
        //this.m_lstCritical.Clear();
        //this.m_lstCriticalStartTime.Clear();

        ////收集物体销毁
        //this.m_cCollectItemShuijing.Destory();
        //this.m_cCollectItemXin.Destory();

        //if (this.m_cResultEffect != null)
        //{
        //    GameObject.Destroy(this.m_cResultEffect);
        //}
        //this.m_cResultEffect = null;

        Destory();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        //销毁资源
        this.m_cResBattleFont1 = null;
        this.m_cResBattleFont2 = null;
        this.m_cResBattleFont3 = null;
        this.m_cResBattleFont4 = null;

        this.m_cResHurtNum = null;
        this.m_cResRecoverNum = null;

        this.m_cResSpark = null;
        this.m_cResCritical = null;

        this.m_cResShuijing = null;
        this.m_cResXin = null;
        this.m_cResSkillAvatar = null;

        this.m_cResDebuffDu = null;
        this.m_cResDebuffXuruo = null;
        this.m_cResDebuffMa = null;
        this.m_cResDebuffPojia = null;
        this.m_cResDebuffPoren = null;
        this.m_cResDebuffFengyin = null;

        this.m_cResFightBegin = null;
        this.m_cResResultWin = null;
        this.m_cResResultLose = null;
        this.m_cResTimeOut = null;

        for (int i = 0; i < NATURE_NUM; i++)
        {
            this.m_vecTexSkillShowBG[i] = null;
        }

        //销毁引用
        Array.Clear(this.m_vecSelfBuf , 0 , this.m_vecSelfBuf.Length);
        Array.Clear(this.m_vecTargetBuf, 0, this.m_vecTargetBuf.Length);

        Array.Clear(this.m_vecUISelfPos , 0 , this.m_vecUISelfPos.Length);
        Array.Clear(this.m_vecUITargetPos , 0 , this.m_vecUITargetPos.Length);
        this.m_cRoleSelfIcon = null;
        this.m_cRoleTargetIcon = null;
        this.m_cRoleSelfName = null;
        this.m_cRoleTargetName = null;
        this.m_cRoleSelfPvpPoint = null;
        this.m_cRoleTargetPvpPoint = null;
        this.m_cRoleSelfPvpTittle = null;
        this.m_cRoleTargetPvpTittle = null;
        Array.Clear(this.m_vecHeroSelfHpTxt , 0 , this.m_vecHeroSelfHpTxt.Length);
        Array.Clear(this.m_vecHeroTargetHpTxt , 0 , this.m_vecHeroTargetHpTxt.Length);
        Array.Clear(this.m_vecHeroSelfHpBar , 0 , this.m_vecHeroSelfHpBar.Length);
        Array.Clear(this.m_vecHeroTargetHpBar , 0 , this.m_vecHeroTargetHpBar.Length);
        Array.Clear(this.m_vecHeroSelfName , 0 , this.m_vecHeroSelfName.Length);
        Array.Clear(this.m_vecHeroTargetName , 0 , this.m_vecHeroTargetName.Length);
        Array.Clear(this.m_vecHeroSelfIcon , 0 , this.m_vecHeroSelfIcon.Length);
        Array.Clear(this.m_vecHeroTargetIcon , 0 , this.m_vecHeroTargetIcon.Length);
        Array.Clear(this.m_vecHeroSelfBBHPBar , 0 , this.m_vecHeroSelfBBHPBar.Length);
        Array.Clear(this.m_vecHeroTargetBBHPBar , 0 , this.m_vecHeroTargetBBHPBar.Length);
        Array.Clear(this.m_vecHeroSelfBBFullEffect , 0 , this.m_vecHeroSelfBBFullEffect.Length);
        Array.Clear(this.m_vecHeroTargetBBFullEffect , 0 ,this.m_vecHeroTargetBBFullEffect.Length);
        Array.Clear(this.m_vecHeroSelfDead , 0 , this.m_vecHeroSelfDead.Length);
        Array.Clear(this.m_vecHeroTargetDead , 0 , this.m_vecHeroTargetDead.Length);
        Array.Clear(this.m_vecHeroSelfBlack , 0 , this.m_vecHeroSelfBlack.Length);
        Array.Clear(this.m_vecHeroTargetBlack , 0 , this.m_vecHeroTargetBlack.Length);
        Array.Clear(this.m_vecHeroSelfProperty , 0 , this.m_vecHeroSelfProperty.Length);
        Array.Clear(this.m_vecHeroTargetProperty , 0 , this.m_vecHeroTargetProperty.Length);
        this.m_cBattleTime = null;

        //销毁实体
        if (this.m_cScene != null)
        {
            GameObject.Destroy(this.m_cScene);
        }
        this.m_cScene = null;

        for (int i = 0; i < this.m_vecSelfHero.Length; i++)
        {
            if (this.m_vecSelfHero[i] != null)
            {
                this.m_vecSelfHero[i].Destory();
            }
            //this.m_vecSelfHero[i] = null;
        }

        for (int i = 0; i < this.m_vecTargetHero.Length; i++)
        {
            if (this.m_vecTargetHero[i] != null)
            {
                this.m_vecTargetHero[i].Destory();
            }
            //this.m_vecTargetHero[i] = null;
        }

        for (int i = 0; i < this.m_lstHurtTxt.Count; i++)
        {
            if (this.m_lstHurtTxt[i] != null)
            {
                GameObject.Destroy(this.m_lstHurtTxt[i]);
            }
            this.m_lstHurtTxt[i] = null;
        }
        this.m_lstHurtTxt.Clear();
        this.m_lstHurtTxtTime.Clear();

        for (int i = 0; i < this.m_lstSpark.Count; i++)
        {
            if (this.m_lstSpark[i] != null)
            {
                GameObject.Destroy(this.m_lstSpark[i]);
            }
            this.m_lstSpark[i] = null;
        }
        this.m_lstSpark.Clear();
        this.m_lstSparkStartTime.Clear();

        for (int i = 0; i < this.m_lstCritical.Count; i++)
        {
            if (this.m_lstCritical[i] != null)
            {
                GameObject.Destroy(this.m_lstCritical[i]);
            }
            this.m_lstCritical[i] = null;
        }
        this.m_lstCritical.Clear();
        this.m_lstCriticalStartTime.Clear();

        //收集物体销毁
        
        this.m_cCollectItemShuijing.Destory();
        this.m_cCollectItemXin.Destory();

        if (this.m_cResultEffect != null)
        {
            GameObject.Destroy(this.m_cResultEffect);
        }
        this.m_cResultEffect = null;

        base.Destory();
    }

    /// <summary>
    /// 设置自身队长技能
    /// </summary>
    /// <param name="leaderSkill"></param>
    public void SetSelfLeaderSkill(LeaderSkillTable leaderSkill)
    {
        this.m_cSelfLeaderSkill = leaderSkill;
    }

    /// <summary>
    /// 获取自身队长技能
    /// </summary>
    /// <returns></returns>
    public override LeaderSkillTable GetSelfLeaderSkill()
    {
        return this.m_cSelfLeaderSkill;
    }

    /// <summary>
    /// 设置目标队长技能
    /// </summary>
    /// <param name="leaderSkill"></param>
    public void SetTargetLeaderSkill(LeaderSkillTable leaderSkill)
    {
        this.m_cTargetSelfLeaderSkill = leaderSkill;
    }

    /// <summary>
    /// 获取敌方队长技能
    /// </summary>
    /// <returns></returns>
    public override LeaderSkillTable GetTargetLeaderSkill()
    {
        return this.m_cTargetSelfLeaderSkill;
    }

    /// <summary>
    /// 获取队友队长技能
    /// </summary>
    /// <returns></returns>
    public override LeaderSkillTable GetFriendLeaderSkill()
    {
        return null;
    }

    /// <summary>
    /// 生成己方战斗英雄
    /// </summary>
    public void SetBattleSelfHero(Hero[] heros)
    {
        for (int i = 0; i < HERO_MAX_NUM; i++)
        {
            if (this.m_vecSelfHero[i] != null)
            {
                this.m_vecSelfHero[i].Destory();
                this.m_vecSelfHero[i] = null;
            }

            if (heros[i] != null)
            {
                Item item = Role.role.GetItemProperty().GetItem(heros[i].m_iEquipID);
                this.m_vecSelfHero[i] = BattleHeroGenerator.Generator(i, true, heros[i], this.m_cSelfLeaderSkill, null , item);
            }
            if (m_vecSelfHero[i] != null)
            {
                PvpAIControl ai = new PvpAIControl(this.m_vecSelfHero[i], this);
                this.m_vecSelfHero[i].SetAI(ai);
                this.m_vecSelfHero[i].m_cHeartDropRate = new CPropertyValue(GAME_DEFINE.PVPHeartRate);
                this.m_vecSelfHero[i].m_iHeartDropNum = GAME_DEFINE.PVPHeartNum;
                this.m_vecSelfHero[i].m_cShuijingDropRate = new CPropertyValue(GAME_DEFINE.PVPShuijingRate);
                this.m_vecSelfHero[i].m_iShuijingDropMinNum = GAME_DEFINE.PVPShuijingMinNum;
                this.m_vecSelfHero[i].m_iShuijingDropNum = GAME_DEFINE.PVPShuijingMaxNum;
                this.m_vecSelfHero[i].m_fBBHP = this.m_vecSelfHero[i].m_iBBMaxHP / 2;
            }
        }
    }

    /// <summary>
    /// 设置对手英雄数据
    /// </summary>
    /// <param name="heros"></param>
    /// <param name="monsters"></param>
    public void SetBattleTargetHero( Hero[] heros , Item[] items )
    {
        for (int i = 0; i < HERO_MAX_NUM; i++)
        {
            if (this.m_vecTargetHero[i] != null)
            {
                this.m_vecTargetHero[i].Destory();
                this.m_vecTargetHero[i] = null;
            }

            this.m_vecTargetHero[i] = BattleHeroGenerator.Generator(i, false, heros[i], this.m_cTargetSelfLeaderSkill, null, items[i]);
            if (this.m_vecTargetHero[i] != null)
            {
                PvpAIControl ai = new PvpAIControl(this.m_vecTargetHero[i], this);
                this.m_vecTargetHero[i].SetAI(ai);
                this.m_vecTargetHero[i].m_cHeartDropRate = new CPropertyValue(GAME_DEFINE.PVPHeartRate);
                this.m_vecTargetHero[i].m_iHeartDropNum = GAME_DEFINE.PVPHeartNum;
                this.m_vecTargetHero[i].m_cShuijingDropRate = new CPropertyValue(GAME_DEFINE.PVPShuijingRate);
                this.m_vecTargetHero[i].m_iShuijingDropMinNum = GAME_DEFINE.PVPShuijingMinNum;
                this.m_vecTargetHero[i].m_iShuijingDropNum = GAME_DEFINE.PVPShuijingMaxNum;
                this.m_vecTargetHero[i].m_fBBHP = this.m_vecTargetHero[i].m_iBBMaxHP/2;
            }
        }
    }

    /// <summary>
    /// 场景特效开关
    /// </summary>
    /// <param name="sw"></param>
    public void SwitchSceneEffect(bool sw)
    {
        GameObject ef = GUI_FINDATION.GET_GAME_OBJECT(this.m_cScene, GUI_DEFINE.SCENE_EFFECT_OBJECT);
        ef.SetActive(sw);
    }

    /// <summary>
    /// 设置UI自身角色信息
    /// </summary>
    private void SetUIRoleSelf()
    {
        this.m_cRoleSelfName.text = this.m_strRoleSelfName;
        this.m_cRoleSelfPvpPoint.text = "" + this.m_iRoleSelfPvpPoint;
        this.m_cRoleSelfPvpTittle.text = "" + AthleticsExpTableManager.GetInstance().GetAthleticsNameByPoint(this.m_iRoleSelfPvpPoint);
        BattleHero hero = this.m_vecSelfHero[this.m_iSelfLeaderIndex];
        GUI_FUNCTION.SET_AVATORSS(this.m_cRoleSelfIcon, hero.m_strAvatorM);
    }

    /// <summary>
    /// 设置UI目标角色信息
    /// </summary>
    private void SetUIRoleTarget()
    {
        this.m_cRoleTargetName.text = this.m_strRoleTargetName;
        this.m_cRoleTargetPvpPoint.text = "" + this.m_iRoleTargetPvpPoint;
        this.m_cRoleTargetPvpTittle.text = "" + AthleticsExpTableManager.GetInstance().GetAthleticsNameByPoint(this.m_iRoleTargetPvpPoint);
        BattleHero hero = this.m_vecTargetHero[this.m_iTargetLeaderIndex];
        GUI_FUNCTION.SET_AVATORSS(this.m_cRoleTargetIcon, hero.m_strAvatorM);
    }

    /// <summary>
    /// 设置英雄空位
    /// </summary>
    /// <param name="index"></param>
    private void SetUISelfHeroActive(int index , bool active)
    {
        GameObject dead = this.m_vecHeroSelfDead[index];
        if (active)
        {
            dead.SetActive(false);
        }
        else
        {
            UILabel hplab = null;
            GameObject hpBar = null;
            GameObject bbhpBar = null;
            UISprite icon = null;
            GameObject black = null;
            UISprite property = null;
            UILabel namelab = null;
            GameObject bbeffect = null;

            hplab = this.m_vecHeroSelfHpTxt[index];
            hpBar = this.m_vecHeroSelfHpBar[index];
            bbhpBar = this.m_vecHeroSelfBBHPBar[index];
            icon = this.m_vecHeroSelfIcon[index];
            dead = this.m_vecHeroSelfDead[index];
            black = this.m_vecHeroSelfBlack[index];
            property = this.m_vecHeroSelfProperty[index];
            namelab = this.m_vecHeroSelfName[index];
            bbeffect = this.m_vecHeroSelfBBFullEffect[index];

            hplab.text = "-/-";
            hpBar.transform.localScale = Vector3.zero;
            bbhpBar.transform.localScale = Vector3.zero;
            icon.atlas = null;
            namelab.text = "--";
            dead.SetActive(false);
            property.enabled = false;
            black.SetActive(true);
            bbeffect.SetActive(false);
        }
    }

    /// <summary>
    /// 设置英雄空位
    /// </summary>
    /// <param name="index"></param>
    private void SetUITargetHeroActive(int index, bool active)
    {
        GameObject dead = this.m_vecHeroTargetDead[index];
        if (active)
        {
            dead.SetActive(false);
        }
        else
        {
            UILabel hplab = null;
            GameObject hpBar = null;
            GameObject bbhpBar = null;
            UISprite icon = null;
            GameObject black = null;
            UISprite property = null;
            UILabel namelab = null;
            GameObject bbeffect = null;

            hplab = this.m_vecHeroTargetHpTxt[index];
            hpBar = this.m_vecHeroTargetHpBar[index];
            bbhpBar = this.m_vecHeroTargetBBHPBar[index];
            icon = this.m_vecHeroTargetIcon[index];
            dead = this.m_vecHeroTargetDead[index];
            black = this.m_vecHeroTargetBlack[index];
            property = this.m_vecHeroTargetProperty[index];
            namelab = this.m_vecHeroTargetName[index];
            bbeffect = this.m_vecHeroTargetBBFullEffect[index];

            hplab.text = "-/-";
            hpBar.transform.localScale = Vector3.zero;
            bbhpBar.transform.localScale = Vector3.zero;
            icon.atlas = null;
            namelab.text = "--";
            dead.SetActive(false);
            black.SetActive(true);
            bbeffect.SetActive(false);
            property.enabled = false;
        }
    }

    /// <summary>
    /// 设置英雄数据
    /// </summary>
    /// <param name="hero"></param>
    public void SetUIHeroData(BattleHero hero)
    {
        UILabel hplab = null;
        GameObject hpBar = null;
        GameObject bbhpBar = null;
        UISprite icon = null;
        GameObject dead = null;
        GameObject black = null;
        UISprite property = null;
        UILabel namelab = null;
        GameObject bbeffect = null;

        if (hero.m_bSelf)
        {
            hplab = this.m_vecHeroSelfHpTxt[hero.m_iIndex];
            hpBar = this.m_vecHeroSelfHpBar[hero.m_iIndex];
            bbhpBar = this.m_vecHeroSelfBBHPBar[hero.m_iIndex];
            icon = this.m_vecHeroSelfIcon[hero.m_iIndex];
            dead = this.m_vecHeroSelfDead[hero.m_iIndex];
            black = this.m_vecHeroSelfBlack[hero.m_iIndex];
            property = this.m_vecHeroSelfProperty[hero.m_iIndex];
            namelab = this.m_vecHeroSelfName[hero.m_iIndex];
            bbeffect = this.m_vecHeroSelfBBFullEffect[hero.m_iIndex];
        }
        else
        {
            hplab = this.m_vecHeroTargetHpTxt[hero.m_iIndex];
            hpBar = this.m_vecHeroTargetHpBar[hero.m_iIndex];
            bbhpBar = this.m_vecHeroTargetBBHPBar[hero.m_iIndex];
            icon = this.m_vecHeroTargetIcon[hero.m_iIndex];
            dead = this.m_vecHeroTargetDead[hero.m_iIndex];
            black = this.m_vecHeroTargetBlack[hero.m_iIndex];
            property = this.m_vecHeroTargetProperty[hero.m_iIndex];
            namelab = this.m_vecHeroTargetName[hero.m_iIndex];
            bbeffect = this.m_vecHeroTargetBBFullEffect[hero.m_iIndex];
        }

        if (hero == null)
        {
            hplab.text = "-/-";
            hpBar.transform.localScale = Vector3.zero;
            bbhpBar.transform.localScale = Vector3.zero;
            icon.atlas = null;
            namelab.text = "--";
            dead.SetActive(false);
            black.SetActive(true);
            return;
        }

        bbeffect.SetActive(false);
        GUI_FUNCTION.SET_NATURES(property, hero.m_eNature);
        namelab.text = hero.m_strName;
        hplab.text = hero.m_iHp + "/" + (int)hero.m_cMaxHP.GetFinalData();
        float rate = hero.m_iHp / hero.m_cMaxHP.GetFinalData();
        hpBar.transform.localScale = new Vector3(rate,1,1);
        if (hero.m_iBBMaxHP <= 0)
            rate = 0;
        else
            rate = hero.m_fBBHP / hero.m_iBBMaxHP;
        //Debug.Log(rate + " bb bar " + hero.m_fBBHP + " - - " + hero.m_iBBMaxHP);
        bbhpBar.transform.localScale = new Vector3(rate, 1, 1);
        GUI_FUNCTION.SET_AVATORSS(icon, hero.m_strAvatorM);
        if (hero.m_iHp > 0)
        {
            dead.SetActive(false);
            black.SetActive(false);
        }
        else
        {
            dead.SetActive(true);
            if (hero.m_iAttackNum <= 0)
                black.SetActive(true);
            else
                black.SetActive(false);
        }
    }

    /// <summary>
    /// 设置英雄Hp
    /// </summary>
    /// <param name="hero"></param>
    public override void SetUIHeroHP(BattleHero hero)
    {
        UILabel lab = null;
        GameObject obj = null;
        GameObject black = null;
        GameObject dead = null;
        if (hero.m_bSelf)
        {
            lab = this.m_vecHeroSelfHpTxt[hero.m_iIndex];
            obj = this.m_vecHeroSelfHpBar[hero.m_iIndex];
            black = this.m_vecHeroSelfBlack[hero.m_iIndex];
            dead = this.m_vecHeroSelfDead[hero.m_iIndex];
        }
        else
        {
            lab = this.m_vecHeroTargetHpTxt[hero.m_iIndex];
            obj = this.m_vecHeroTargetHpBar[hero.m_iIndex];
            black = this.m_vecHeroTargetBlack[hero.m_iIndex];
            dead = this.m_vecHeroTargetDead[hero.m_iIndex];
        }

        lab.text = hero.m_iHp + "/" + (int)hero.m_cMaxHP.GetFinalData();
        float rate = hero.m_iHp / hero.m_cMaxHP.GetFinalData();
        obj.transform.localScale = new Vector3(rate, 1, 1);

        if (hero.m_iHp <= 0)
        {
            if (!dead.activeSelf)
                dead.SetActive(true);
            if (!black.activeSelf)
                black.SetActive(true);
        }
        else
        {
            if (dead.activeSelf)
                dead.SetActive(false);
        }

    }

    /// <summary>
    /// 设置英雄是否可攻击
    /// </summary>
    /// <param name="hero"></param>
    public override void SetUIHeroAttackNum(BattleHero hero)
    {
        GameObject black = null;
        if (hero.m_bSelf)
        {
            black = this.m_vecHeroSelfBlack[hero.m_iIndex];
        }
        else
        {
            black = this.m_vecHeroTargetBlack[hero.m_iIndex];
        }

        if (hero.m_iAttackNum <= 0 || hero.m_bDead )
        {
            if (!black.activeSelf)
                black.SetActive(true);
        }
        else
        {
            if (black.activeSelf)
                black.SetActive(false);
        }
    }

    /// <summary>
    /// 设置英雄BBHP
    /// </summary>
    /// <param name="hero"></param>
    public override void SetUIHeroBBHP(BattleHero hero)
    {
        GameObject obj = null;
        GameObject bbeffect = null;
        if (hero.m_bSelf)
        {
            obj = this.m_vecHeroSelfBBHPBar[hero.m_iIndex];
            bbeffect = this.m_vecHeroSelfBBFullEffect[hero.m_iIndex];
        }
        else
        {
            obj = this.m_vecHeroTargetBBHPBar[hero.m_iIndex];
            bbeffect = this.m_vecHeroTargetBBFullEffect[hero.m_iIndex];
        }

        float rate = 0;
        if(hero.m_iBBMaxHP > 0 )
            rate = hero.m_fBBHP * 1f / hero.m_iBBMaxHP;
        obj.transform.localScale = new Vector3(rate, 1, 1);
        if (rate >= 1f)
        {
            if(!bbeffect.activeSelf)
                bbeffect.SetActive(true);
        }
        else
        {
            if(bbeffect.activeSelf)
                bbeffect.SetActive(false);
        }
    }

    /// <summary>
    /// 设置目标数据
    /// </summary>
    /// <param name="hero"></param>
    public override void SetUITargetData(BattleHero hero)
    {
        //
    }

    /// <summary>
    /// 隐藏目标BUF标记
    /// </summary>
    /// <param name="index"></param>
    public override void HidenTargetBUF(int index)
    {
        //
    }

    /// <summary>
    /// 生成伤害数值
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="num"></param>
    public override void GeneratorHurtNum(Vector3 pos, int num , BattleHero target )
    {
        GameObject obj = GameObject.Instantiate(this.m_cResHurtNum) as GameObject;
        obj.transform.parent = this.m_cGUIObject.transform;
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = pos + GAME_FUNCTION.RANDOM_IN_SPHERE() * 50f;

        GameObject objNum = GUI_FUNCTION.GENERATOR_NUM(num, (this.m_cResBattleFont2 as GameObject).GetComponent<UIFont>());
        objNum.transform.parent = obj.transform;
        objNum.transform.localPosition = Vector3.zero;
        objNum.transform.localScale = Vector3.one * 1f;

        this.m_lstHurtTxt.Add(obj);
        this.m_lstHurtTxtTime.Add(GAME_TIME.TIME_FIXED());

        //统计
        if (!target.m_bSelf)
        {
            this.m_iRoundHurt += num;
            this.m_iTotalHurt += num;
            if (this.m_iRoundHurt > this.m_iRoundMaxHurt)
                this.m_iRoundMaxHurt = this.m_iRoundHurt;
        }
    }

    /// <summary>
    /// 克制时的伤害数字
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="num"></param>
    public override void GeneratorHurtBaneNum(Vector3 pos, int num , BattleHero target)
    {
        GameObject obj = GameObject.Instantiate(this.m_cResHurtNum) as GameObject;
        obj.transform.parent = this.m_cGUIObject.transform;
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = pos + GAME_FUNCTION.RANDOM_IN_SPHERE() * 50f;

        GameObject objNum = GUI_FUNCTION.GENERATOR_NUM(num, (this.m_cResBattleFont4 as GameObject).GetComponent<UIFont>());
        objNum.transform.parent = obj.transform;
        objNum.transform.localPosition = Vector3.zero;
        objNum.transform.localScale = Vector3.one * 1f;

        this.m_lstHurtTxt.Add(obj);
        this.m_lstHurtTxtTime.Add(GAME_TIME.TIME_FIXED());

        //统计
        if (!target.m_bSelf)
        {
            this.m_iRoundHurt += num;
            this.m_iTotalHurt += num;
            if (this.m_iRoundHurt > this.m_iRoundMaxHurt)
                this.m_iRoundMaxHurt = this.m_iRoundHurt;
        }
    }

    /// <summary>
    /// 被克制时的伤害数字
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="num"></param>
    public override void GeneratorHurtBeBaneNum(Vector3 pos, int num , BattleHero target )
    {
        GameObject obj = GameObject.Instantiate(this.m_cResHurtNum) as GameObject;
        obj.transform.parent = this.m_cGUIObject.transform;
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = pos + GAME_FUNCTION.RANDOM_IN_SPHERE() * 50f;

        GameObject objNum = GUI_FUNCTION.GENERATOR_NUM(num, (this.m_cResBattleFont3 as GameObject).GetComponent<UIFont>());
        objNum.transform.parent = obj.transform;
        objNum.transform.localPosition = Vector3.zero;
        objNum.transform.localScale = Vector3.one * 1f;

        this.m_lstHurtTxt.Add(obj);
        this.m_lstHurtTxtTime.Add(GAME_TIME.TIME_FIXED());

        //统计
        if (!target.m_bSelf)
        {
            this.m_iRoundHurt += num;
            this.m_iTotalHurt += num;
            if (this.m_iRoundHurt > this.m_iRoundMaxHurt)
                this.m_iRoundMaxHurt = this.m_iRoundHurt;
        }
    }

    /// <summary>
    /// 生成回复数值
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="num"></param>
    public override void GeneratorRecoverNum(Vector3 pos, int num , BattleHero target)
    {
        GameObject obj = GameObject.Instantiate(this.m_cResRecoverNum) as GameObject;
        obj.transform.parent = this.m_cGUIObject.transform;
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = pos + GAME_FUNCTION.RANDOM_IN_SPHERE() * 10f;

        GameObject objNum = GUI_FUNCTION.GENERATOR_NUM(num, (this.m_cResBattleFont1 as GameObject).GetComponent<UIFont>());
        objNum.transform.parent = obj.transform;
        objNum.transform.localPosition = Vector3.zero;
        objNum.transform.localScale = Vector3.one * 1f;

        this.m_lstHurtTxt.Add(obj);
        this.m_lstHurtTxtTime.Add(GAME_TIME.TIME_FIXED());

        if (target.m_bSelf)
        {
            this.m_iTotalRecover += num;
        }
    }

    /// <summary>
    /// 生成Spark
    /// </summary>
    /// <param name="pos"></param>
    public override void GeneratorSpark(Vector3 pos , BattleHero target )
    {
        GameObject obj = GameObject.Instantiate(this.m_cResSpark) as GameObject;
        obj.transform.parent = this.m_cBattleParent.transform;
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = pos + GAME_FUNCTION.RANDOM_IN_SPHERE()*0.5f;

        this.m_lstSpark.Add(obj);
        this.m_lstSparkStartTime.Add(GAME_TIME.TIME_FIXED());

        if (!target.m_bSelf)
        {
            this.m_iTotalSpark++;
            this.m_iRoundSpark++;
            if (this.m_iRoundSpark > this.m_iRoundMaxSpark)
            {
                this.m_iRoundMaxSpark = this.m_iRoundSpark;
            }
        }
    }

    /// <summary>
    /// 生成暴击特效
    /// </summary>
    /// <param name="pos"></param>
    public override void GeneratorCritical(Vector3 pos)
    {
        GameObject obj = GameObject.Instantiate(this.m_cResCritical) as GameObject;
        obj.transform.parent = this.m_cBattleParent.transform;
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = pos;

        this.m_lstCritical.Add(obj);
        this.m_lstCriticalStartTime.Add(GAME_TIME.TIME_FIXED());
    }


    /// <summary>
    /// 生成心
    /// </summary>
    public override void GeneratorXin(Vector3 pos)
    {
        Vector2 bottom2d = GAME_FUNCTION.RANDOM_IN_CIRCLE();
        Vector3 bottom = new Vector3(bottom2d.x + pos.x, 0.2f, bottom2d.y + pos.z);
        float heigh = GAME_FUNCTION.RANDOM(0.5f, 1.5f);
        pos += Vector3.up * 0.35f;

        GameObject obj = GameObject.Instantiate(this.m_cResXin) as GameObject;
        obj.transform.parent = this.m_cBattleParent.transform;
        obj.transform.localPosition = pos;

        this.m_cCollectItemXin.m_lstMesh.Add(obj);
        this.m_cCollectItemXin.m_lstCurveStart.Add(pos);
        this.m_cCollectItemXin.m_lstCurveTop.Add(heigh);
        this.m_cCollectItemXin.m_lstCurveBottom.Add(bottom);
        this.m_cCollectItemXin.m_lstCurveStartTime.Add(GAME_TIME.TIME_FIXED());
    }

    /// <summary>
    /// 逻辑更新心点
    /// </summary>
    /// <returns></returns>
    public bool UpdateXin()
    {
        bool finish = true;

        for (int i = 0; i < this.m_cCollectItemXin.m_lstMesh.Count; )
        {
            float disTime = GAME_TIME.TIME_FIXED() - this.m_cCollectItemXin.m_lstLineStartTime[i];
            if (this.m_cCollectItemXin.m_lstLineCostTime[i] < disTime)
            {
                if (this.m_cCollectItemXin.m_lstMesh[i] != null)
                {
                    GameObject.Destroy(this.m_cCollectItemXin.m_lstMesh[i]);
                    this.m_cCollectItemXin.m_lstMesh[i] = null;
                }
                this.m_cCollectItemXin.m_lstMesh.RemoveAt(i);
                this.m_cCollectItemXin.m_lstLineStartTime.RemoveAt(i);
                this.m_cCollectItemXin.m_lstLineCostTime.RemoveAt(i);
                this.m_cCollectItemXin.m_lstCurveBottom.RemoveAt(i);
                this.m_cCollectItemXin.m_lstCurveStart.RemoveAt(i);
                //增加生命
                float rate = GAME_DEFINE.XinRecoverRate;

                if ( this.m_cCollectItemXin.m_lstBattleHero[i].m_bSelf && this.m_cSelfLeaderSkill != null)
                    rate += this.m_cSelfLeaderSkill.HeartRecoverRate;

                if (!this.m_cCollectItemXin.m_lstBattleHero[i].m_bSelf && this.m_cTargetSelfLeaderSkill != null)
                    rate += this.m_cTargetSelfLeaderSkill.HeartRecoverRate;

                int recover = (int)(this.m_cCollectItemXin.m_lstBattleHero[i].m_cRecover.GetFinalData() * rate);
                this.m_cCollectItemXin.m_lstBattleHero[i].AddHP(recover);
                GeneratorRecoverNum(this.m_cCollectItemXin.m_lstBattleHero[i].m_cUIStartPos, recover, this.m_cCollectItemXin.m_lstBattleHero[i]);
                SetUIHeroHP(this.m_cCollectItemXin.m_lstBattleHero[i]);
                this.m_cCollectItemXin.m_lstBattleHero.RemoveAt(i);

				MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_BATTLE_GET_XIN);
//                MediaMgr.PlaySound(SOUND_DEFINE.SE_BATTLE_GET_XIN);

                continue;
            }
            else
            {
                float rate = CMath.QuadraticIn(disTime / this.m_cCollectItemXin.m_lstLineCostTime[i], 0, 1, 1);
                this.m_cCollectItemXin.m_lstMesh[i].transform.localPosition = Vector3.Lerp(this.m_cCollectItemXin.m_lstCurveStart[i], this.m_cCollectItemXin.m_lstCurveBottom[i], rate);
                i++;
                finish = false;
            }
        }

        return finish;
    }

    /// <summary>
    /// 生成水晶
    /// </summary>
    public override void GeneratorShuijing(Vector3 pos)
    {
        Vector2 bottom2d = GAME_FUNCTION.RANDOM_IN_CIRCLE();
        Vector3 bottom = new Vector3(bottom2d.x + pos.x, 0.2f, bottom2d.y + pos.z);
        float heigh = GAME_FUNCTION.RANDOM(0.5f, 1.5f);
        pos += Vector3.up * 0.35f;

        GameObject obj = GameObject.Instantiate(this.m_cResShuijing) as GameObject;
        obj.transform.parent = this.m_cBattleParent.transform;
        obj.transform.localPosition = pos;

        this.m_cCollectItemShuijing.m_lstMesh.Add(obj);
        this.m_cCollectItemShuijing.m_lstCurveStart.Add(pos);
        this.m_cCollectItemShuijing.m_lstCurveTop.Add(heigh);
        this.m_cCollectItemShuijing.m_lstCurveBottom.Add(bottom);
        this.m_cCollectItemShuijing.m_lstCurveStartTime.Add(GAME_TIME.TIME_FIXED());
    }

    /// <summary>
    /// 更新战斗时间
    /// </summary>
    public void UpdateBattleTime()
    {
        //战斗时间是否已经结束了
        if (this.m_iBattleResult != BATTLE_WIN_NONE || this.m_eBattleState <= BATTLE_STATE.BATTLE_STATE_BEGIN || this.m_eBattleState >= BATTLE_STATE.BATTLE_STATE_TIME_OUT_BEGIN)
            return;

        float battledisTime = GAME_TIME.TIME_FIXED() - this.m_fBattleStartTime;
        if (battledisTime >= MAX_BATTLE_TIME)
        {
            this.m_eBattleState = BATTLE_STATE.BATTLE_STATE_TIME_OUT_BEGIN;
        }
        int showTime = (int)(MAX_BATTLE_TIME - battledisTime);
        if (showTime < 0)
            showTime = 0;
        string str_time = "" + showTime;
        if (showTime < 10)
            str_time = "0" + str_time;
        if (this.m_cBattleTime != null)
            this.m_cBattleTime.text = str_time;
    }

    /// <summary>
    /// 逻辑更新水晶点
    /// </summary>
    /// <returns></returns>
    public bool UpdateShuijing()
    {
        bool finish = true;

        for (int i = 0; i < this.m_cCollectItemShuijing.m_lstMesh.Count; )
        {
            float disTime = GAME_TIME.TIME_FIXED() - this.m_cCollectItemShuijing.m_lstLineStartTime[i];
            if (this.m_cCollectItemShuijing.m_lstLineCostTime[i] < disTime)
            {
                if (this.m_cCollectItemShuijing.m_lstMesh[i] != null)
                {
                    GameObject.Destroy(this.m_cCollectItemShuijing.m_lstMesh[i]);
                    this.m_cCollectItemShuijing.m_lstMesh[i] = null;
                }
                this.m_cCollectItemShuijing.m_lstMesh.RemoveAt(i);
                this.m_cCollectItemShuijing.m_lstLineStartTime.RemoveAt(i);
                this.m_cCollectItemShuijing.m_lstLineCostTime.RemoveAt(i);
                this.m_cCollectItemShuijing.m_lstCurveBottom.RemoveAt(i);
                this.m_cCollectItemShuijing.m_lstCurveStart.RemoveAt(i);
                //增加BB水晶值
                float addbbhp = GAME_DEFINE.ShuiJingRecover;
                if ( this.m_cCollectItemShuijing.m_lstBattleHero[i].m_bSelf && this.m_cSelfLeaderSkill != null)
                    addbbhp += this.m_cSelfLeaderSkill.BBHPIncrease;
                if (!this.m_cCollectItemShuijing.m_lstBattleHero[i].m_bSelf && this.m_cTargetSelfLeaderSkill != null)
                    addbbhp += this.m_cTargetSelfLeaderSkill.BBHPIncrease;
                this.m_cCollectItemShuijing.m_lstBattleHero[i].AddBBHP(addbbhp);
                SetUIHeroBBHP(this.m_cCollectItemShuijing.m_lstBattleHero[i]);
                this.m_cCollectItemShuijing.m_lstBattleHero.RemoveAt(i);

				MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_BATTLE_GET_SHUIJING);
//                MediaMgr.PlaySound(SOUND_DEFINE.SE_BATTLE_GET_SHUIJING);

                continue;
            }
            else
            {
                float rate = CMath.QuadraticIn(disTime / this.m_cCollectItemShuijing.m_lstLineCostTime[i], 0, 1, 1);
                this.m_cCollectItemShuijing.m_lstMesh[i].transform.localPosition = Vector3.Lerp(this.m_cCollectItemShuijing.m_lstCurveStart[i], this.m_cCollectItemShuijing.m_lstCurveBottom[i], rate);
                i++;
                finish = false;
            }
        }

        return finish;
    }

    /// <summary>
    /// 生成技能展示
    /// </summary>
    public override void GeneratorSkillShow(BattleHero hero)
    {
        Time.timeScale = 0;
        this.m_fSkillAvatarStartTime = GAME_TIME.TIME_REAL();
        if (this.m_cSkillAvatar != null)
            GameObject.Destroy(this.m_cSkillAvatar);
        this.m_cSkillAvatar = GameObject.Instantiate(this.m_cResSkillAvatar) as GameObject;
        this.m_cSkillAvatar.transform.parent = this.m_cBattleParent.transform;
        this.m_cSkillAvatar.transform.localPosition = Vector3.zero;
        this.m_cSkillAvatar.transform.localScale = Vector3.one;
        UILabel label = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cSkillAvatar, SKILL_SHOW_LABEL_PATH);
        label.text = hero.m_strSkillName;
        Material mat1 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cSkillAvatar, SKILL_SHOW_HEAD_PATH).GetComponent<MeshRenderer>().material;
        mat1.SetTexture("_MainTex", hero.m_cResAvator);
        Material mat2 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cSkillAvatar, SKILL_SHOW_BG_PATH).GetComponent<MeshRenderer>().material;
        mat2.SetTexture("_MainTex", this.m_vecTexSkillShowBG[(int)hero.m_eNature - 1]);

		MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_BATTLE_SKILL_CUTIN);
//        MediaMgr.PlaySound(SOUND_DEFINE.SE_BATTLE_SKILL_CUTIN);

        //隐藏数字
        for (int i = 0; i < this.m_lstHurtTxt.Count; i++)
        {
            if (this.m_lstHurtTxt[i] != null)
            {
                this.m_lstHurtTxt[i].GetComponentInChildren<MeshRenderer>().enabled = false;
            }
        }

        //隐藏BUFF
        for (int i = 0; i < this.m_vecTargetBuf.Length; i++)
        {
            HidenTargetBUF(i);
        }

        //隐藏掉落
        for (int i = 0; i < this.m_cCollectItemShuijing.m_lstMesh.Count; i++)
        {
            this.m_cCollectItemShuijing.m_lstMesh[i].SetActive(false);
        }
        for (int i = 0; i < this.m_cCollectItemXin.m_lstMesh.Count; i++)
        {
            this.m_cCollectItemXin.m_lstMesh[i].SetActive(false);
        }

        this.m_iUIState = SKILL_SHOW_STATE;

        //统计
        if (hero.m_bSelf)
        {
            this.m_iTotalSkill++;
        }
    }


    /// <summary>
    /// 逻辑更新
    /// </summary>
    /// <returns></returns>
    public override bool Update()
    {
        base.Update();

        if (!this.m_bShow)
        {
            return false;
        }

        //战斗时间是否已经结束了
        UpdateBattleTime();

        //英雄渲染实体更新 以及 BUF 展示与更新
        float bufDis = GAME_TIME.TIME_FIXED() - this.m_fBufShowStartTime;
        if (bufDis > 2f)
        {
            this.m_iBufShowIndex++;
            this.m_iBufShowIndex = this.m_iBufShowIndex % 100;
            this.m_fBufShowStartTime = GAME_TIME.TIME_FIXED();
        }

        for (int i = 0; i < HERO_MAX_NUM; i++)
        {
            if (this.m_vecSelfHero[i] != null && this.m_vecSelfHero[i].GetGfxObject() != null )
            {
                this.m_vecSelfHero[i].GetGfxObject().Update();
            }
            if (this.m_vecTargetHero[i] != null && this.m_vecTargetHero[i].GetGfxObject() != null )
            {
                this.m_vecTargetHero[i].GetGfxObject().Update();
            }

            if (this.m_eBattleState >= BATTLE_STATE.BATTLE_STATE_INIT_END && this.m_iUIState != SKILL_SHOW_STATE)
            {
                //己方BUF展示
                if (this.m_vecSelfHero[i] != null)
                {
                    if (this.m_vecSelfHero[i].m_bDead)
                    {
                        if (this.m_vecSelfBuf[i] != null && this.m_vecSelfBuf[i].enabled != false)
                            this.m_vecSelfBuf[i].enabled = false;
                    }
                    else
                    {
                        if (this.m_vecSelfHero[i].m_lstBUF.Count <= 0)
                        {
                            if (this.m_vecSelfBuf[i] != null && this.m_vecSelfBuf[i].enabled != false)
                                this.m_vecSelfBuf[i].enabled = false;
                        }
                        else
                        {
                            if (this.m_vecSelfBuf[i] != null && this.m_vecSelfBuf[i].enabled != true && this.m_vecSelfHero[i].GetGfxObject().GetStateControl().GetCurrentState().GetStateType() == STATE_TYPE.STATE_IDLE)
                            {
                                this.m_vecSelfBuf[i].enabled = true;
                                if (this.m_vecSelfBuf[i] != null)
                                    BATTLE_FUNCTION.BUF_SPRITE(this.m_vecSelfHero[i], this.m_vecSelfBuf[i], this.m_iBufShowIndex);
                            }
                        }
                    }
                }
                else
                {
                    if (this.m_vecSelfBuf[i] != null && this.m_vecSelfBuf[i].enabled != false)
                        this.m_vecSelfBuf[i].enabled = false;
                }

                //目标BUF展示
                if (this.m_vecTargetHero[i] != null)
                {
                    if (this.m_vecTargetHero[i].m_bDead)
                    {
                        if ( this.m_vecTargetBuf[i] != null && this.m_vecTargetBuf[i].enabled != false)
                            this.m_vecTargetBuf[i].enabled = false;
                    }
                    else
                    {
                        if (this.m_vecTargetHero[i].m_lstBUF.Count <= 0)
                        {
                            if ( this.m_vecTargetBuf[i] != null && this.m_vecTargetBuf[i].enabled != false)
                                this.m_vecTargetBuf[i].enabled = false;
                        }
                        else
                        {
                            if (this.m_vecTargetBuf[i] != null && this.m_vecTargetBuf[i].enabled != true && this.m_vecTargetHero[i].GetGfxObject().GetStateControl().GetCurrentState().GetStateType() == STATE_TYPE.STATE_IDLE)
                            {
                                this.m_vecTargetBuf[i].enabled = true;
                                if (this.m_vecTargetBuf[i] != null)
                                    BATTLE_FUNCTION.BUF_SPRITE(this.m_vecTargetHero[i], this.m_vecTargetBuf[i], this.m_iBufShowIndex);
                            }
                        }
                    }
                }
                else
                {
                    if (this.m_vecTargetBuf[i] != null && this.m_vecTargetBuf[i].enabled != false)
                        this.m_vecTargetBuf[i].enabled = false;
                }
            }
        }

        //更新伤害数值位置,以及判定销毁
        for (int i = 0; i < this.m_lstHurtTxt.Count; )
        {
            //float dis = GAME_TIME.TIME_FIXED() - this.m_lstHurtTxtTime[i];
            //if (dis > HURT_NUM_TIME)
            if(this.m_lstHurtTxt[i] == null)
            {
                //GameObject.Destroy(this.m_lstHurtTxt[i]);
                this.m_lstHurtTxt.RemoveAt(i);
                this.m_lstHurtTxtTime.RemoveAt(i);
                continue;
            }
            i++;
        }

        //Spark特效判定销毁
        for (int i = 0; i < this.m_lstSpark.Count; )
        {
            if (GAME_TIME.TIME_FIXED() - this.m_lstSparkStartTime[i] > SPARK_TIME)
            {
                GameObject.Destroy(this.m_lstSpark[i]);
                this.m_lstSpark.RemoveAt(i);
                this.m_lstSparkStartTime.RemoveAt(i);
                continue;
            }
            i++;
        }

        //暴击特效判定销毁
        for (int i = 0; i < this.m_lstCritical.Count; )
        {
            if (GAME_TIME.TIME_FIXED() - this.m_lstCriticalStartTime[i] > CRITICAL_TIME)
            {
                GameObject.Destroy(this.m_lstCritical[i]);
                this.m_lstCritical.RemoveAt(i);
                this.m_lstCriticalStartTime.RemoveAt(i);
                continue;
            }
            i++;
        }

        //状态逻辑更新
        //Debug.Log(" state " + this.m_eBattleState);
        switch (this.m_eBattleState)
        {
            case BATTLE_STATE.BATTLE_STATE_INIT_BEGIN:
                this.m_eBattleState++;
                break;
            case BATTLE_STATE.BATTLE_STATE_INIT1:
                //float per = ResourceMgr.GetAsyncProcess() * 100f;
                if (ResourceMgr.GetProgress() >= 1f && ResourceMgr.IsComplete() )
                {
                    this.m_eBattleState++;
                }
                break;
            case BATTLE_STATE.BATTLE_STATE_INIT2:
                InitShow();
                GUI_FUNCTION.AYSNCLOADING_HIDEN();
                this.m_eBattleState++;
                break;
            case BATTLE_STATE.BATTLE_STATE_INIT_END:
                //全局装备影响
                for (int i = 0; i < this.m_vecSelfHero.Length; i++)
                {
                    BattleHero tmpHero = this.m_vecSelfHero[i];
                    BATTLE_FUNCTION.EQUIP_ACTION(BATTLE_TIME_SCENE.BATTLE_INIT, tmpHero);
                }
                for (int i = 0; i < this.m_vecTargetHero.Length; i++)
                {
                    BattleHero tmpHero = this.m_vecTargetHero[i];
                    BATTLE_FUNCTION.EQUIP_ACTION(BATTLE_TIME_SCENE.BATTLE_INIT, tmpHero);
                }
                this.m_eBattleState++;
                break;
            case BATTLE_STATE.BATTLE_STATE_BEGIN_BEGIN:    //战斗开始准备
                this.m_iRound = 0;
                this.m_iBattleResult = BATTLE_WIN_NONE;
                this.m_iWinType = WIN_TYPE_NONE;
                this.m_iAutoTargetIndex = -1;
                this.m_iSelectTargetIndex = -1;
                this.m_iUIState = UI_STATE_NONE;
                this.m_eBattleState++;
                break;
            case BATTLE_STATE.BATTLE_STATE_BEGIN_BEGIN1:    //战斗准备开始1
                this.m_cFightBegin = GameObject.Instantiate(this.m_cResFightBegin) as GameObject;
                this.m_cFightBegin.transform.parent = this.m_cBattleParent.transform;
                this.m_cFightBegin.transform.localPosition = Vector3.zero;
                this.m_eBattleState++;
                break;
            case BATTLE_STATE.BATTLE_STATE_BEGIN_BEGIN2:    //战斗准备开始2
                if( this.m_cFightBegin == null)
                    this.m_eBattleState++;
                break;
            case BATTLE_STATE.BATTLE_STATE_BEGIN:    //战斗开始
                this.m_fBattleStartTime = GAME_TIME.TIME_FIXED();
                this.m_eBattleState++;
                break;
            case BATTLE_STATE.BATTLE_STATE_BEGIN_END:    //战斗开始结束
                //单场战斗装备影响
                for (int i = 0; i < this.m_vecSelfHero.Length; i++)
                {
                    BattleHero tmpHero = this.m_vecSelfHero[i];
                    BATTLE_FUNCTION.EQUIP_ACTION(BATTLE_TIME_SCENE.BATTLE_START, tmpHero);
                }
                for (int i = 0; i < this.m_vecTargetHero.Length; i++)
                {
                    BattleHero tmpHero = this.m_vecTargetHero[i];
                    BATTLE_FUNCTION.EQUIP_ACTION(BATTLE_TIME_SCENE.BATTLE_START, tmpHero);
                }
                this.m_eBattleState++;
                break;
            case BATTLE_STATE.BATTLE_STATE_SELF_ATTACK_BEGIN:    //己方攻击开始

                //统计
                this.m_iRoundHurt = 0;
                this.m_iRoundSpark = 0;

                //己方回合攻击开始装备影响
                for (int i = 0; i < this.m_vecSelfHero.Length; i++)
                {
                    BattleHero tmpHero = this.m_vecSelfHero[i];
                    BATTLE_FUNCTION.EQUIP_ACTION(BATTLE_TIME_SCENE.BATTLE_ROUND_START, tmpHero);
                }

                this.m_iRound++;    //回合数增加

                //更新BUF状态
                for (int i = 0; i < this.m_vecTargetHero.Length; i++)
                {
                    if (this.m_vecTargetHero[i] != null && !this.m_vecTargetHero[i].m_bDead)
                    {
                        BATTLE_FUNCTION.BUF_UPDATE(this.m_vecTargetHero[i], this);
                        this.m_vecTargetHero[i].BUFDec();
                    }
                }
                for (int i = 0; i < this.m_vecSelfHero.Length; i++)
                {
                    if (this.m_vecSelfHero[i] != null && !this.m_vecSelfHero[i].m_bDead)
                    {
                        BATTLE_FUNCTION.BUF_UPDATE(this.m_vecSelfHero[i], this);
                        this.m_vecSelfHero[i].BUFDec();
                    }
                }

                //初始化回合前数据
                for (int i = 0; i < this.m_vecSelfHero.Length; i++)
                {
                    if (this.m_vecSelfHero[i] != null && !this.m_vecSelfHero[i].m_bDead)
                    {
                        //this.m_vecSelfHero[i].m_iAttackNum = this.m_vecSelfHero[i].m_iAttackMaxNum;
                        this.m_vecSelfHero[i].GetAIControl().Initialize();
                        this.m_vecSelfHero[i].m_bDefence = false;
                        this.m_vecSelfHero[i].m_iDummyHP = this.m_vecSelfHero[i].m_iHp;
                        Array.Clear(this.m_vecSelfHero[i].m_vecHitStartTime, 0, this.m_vecSelfHero[i].m_vecHitStartTime.Length);
                        Array.Clear(this.m_vecSelfHero[i].m_vecHitEndTime, 0, this.m_vecSelfHero[i].m_vecHitEndTime.Length);

                        SetUIHeroAttackNum(this.m_vecSelfHero[i]);
                    }
                }
                this.m_eBattleState++;
                break;
            case BATTLE_STATE.BATTLE_STATE_SELF_ATTACK:  //攻击
                bool cmdFinish = true;

                //收集品曲线
                if (!this.m_cCollectItemShuijing.UpdateCurve())
                {
                    cmdFinish = false;
                }
                if (!this.m_cCollectItemXin.UpdateCurve())
                {
                    cmdFinish = false;
                }

                for (int i = 0; i < this.m_vecSelfHero.Length; i++)
                {
                    if (this.m_vecSelfHero[i] != null)
                    {
                        //判断AI
                        if (this.m_vecSelfHero[i].GetAIControl().Update())
                        {
                            cmdFinish = false;
                        }
                    }
                }

                //攻击指令暂时结束,判断怪物是否死亡
                if (cmdFinish)
                {
                    for (int i = 0; i < this.m_vecTargetHero.Length; i++)
                    {
                        if (this.m_vecTargetHero[i] != null && this.m_vecTargetHero[i].m_iHp <= 0 && !this.m_vecTargetHero[i].m_bDead)
                        {
                            cmdFinish = false;
                            this.m_vecTargetHero[i].m_bDead = true;
                            this.m_vecTargetHero[i].GetCmdControl().CmdDie();
                        }
                    }
                }

                //判定敌方是否指令结束
                for (int i = 0; i < this.m_vecTargetHero.Length; i++)
                {
                    if (this.m_vecTargetHero[i] != null)
                    {
                        if (this.m_vecTargetHero[i].GetCmdControl().Update())
                        {
                            cmdFinish = false;
                        }
                    }
                }

                if (cmdFinish)
                {
                    bool tmpFinish = true;
                    //检查是否无敌人可打
                    for (int i = 0; i < this.m_vecTargetHero.Length; i++)
                    {
                        if (this.m_vecTargetHero[i] != null && !this.m_vecTargetHero[i].m_bDead)
                        {
                            tmpFinish = false;
                        }
                    }
                    if (tmpFinish)
                    {
                        this.m_iBattleResult = BATTLE_WIN_SELF;
                        this.m_eBattleState++;
                        break;
                    }
                }

                if (cmdFinish )
                {
                    for (int i = 0; i < this.m_vecSelfHero.Length; i++)
                    {
                        if (this.m_vecSelfHero[i] != null)
                        {
                            if ( !this.m_vecSelfHero[i].m_bDead && this.m_vecSelfHero[i].m_iAttackNum > 0)
                            {
                                cmdFinish = false;
                            }
                        }
                    }
                }

                if (cmdFinish)
                {
                    this.m_eBattleState++;
                }

                break;
            case BATTLE_STATE.BATTLE_STATE_SELF_ATTACK_END:  //己方攻击结束
                //己方攻击结束装备影响
                for (int i = 0; i < this.m_vecSelfHero.Length; i++)
                {
                    BattleHero tmpHero = this.m_vecSelfHero[i];
                    BATTLE_FUNCTION.EQUIP_ACTION(BATTLE_TIME_SCENE.BATTLE_ROUND_END, tmpHero);
                }

                //销毁
                for (int i = 0; i < this.m_vecTargetHero.Length; i++)
                {
                    if (this.m_vecTargetHero[i] != null && this.m_vecTargetHero[i].m_iHp <= 0 && this.m_vecTargetHero[i].m_bDead)
                    {
                        this.m_vecTargetHero[i].GetGfxObject().Destory();
                    }
                }
                this.m_eBattleState++;
                break;
            case BATTLE_STATE.BATTLE_STATE_GET_ITEM_BEGIN:   //物品收集开始
                this.m_cCollectItemShuijing.Clear();
                for (int i = 0; i < this.m_cCollectItemShuijing.m_lstMesh.Count; i++)
                {
                    BattleHero target = GetSelfAuto();
                    if (target != null)
                    {
                        this.m_cCollectItemShuijing.m_lstCurveStart.Add(this.m_cCollectItemShuijing.m_lstMesh[i].transform.localPosition);
                        this.m_cCollectItemShuijing.m_lstCurveBottom.Add(target.m_cStartPos + Vector3.up * 0.45f);
                        float disTime = (target.m_cStartPos - this.m_cCollectItemShuijing.m_lstMesh[i].transform.localPosition).magnitude / COLLECT_ITEM_SPEED;
                        this.m_cCollectItemShuijing.m_lstLineCostTime.Add(disTime);
                        this.m_cCollectItemShuijing.m_lstLineStartTime.Add(GAME_TIME.TIME_FIXED());
                        this.m_cCollectItemShuijing.m_lstBattleHero.Add(target);
                    }
                }
                this.m_cCollectItemXin.Clear();
                for (int i = 0; i < this.m_cCollectItemXin.m_lstMesh.Count; i++)
                {
                    BattleHero target = GetSelfHPNotFULL();
                    if (target != null)
                    {
                        this.m_cCollectItemXin.m_lstCurveStart.Add(this.m_cCollectItemXin.m_lstMesh[i].transform.localPosition);
                        this.m_cCollectItemXin.m_lstCurveBottom.Add(target.m_cStartPos + Vector3.up * 0.45f);
                        float disTime = (target.m_cStartPos - this.m_cCollectItemXin.m_lstMesh[i].transform.localPosition).magnitude / COLLECT_ITEM_SPEED;
                        this.m_cCollectItemXin.m_lstLineCostTime.Add(disTime);
                        this.m_cCollectItemXin.m_lstLineStartTime.Add(GAME_TIME.TIME_FIXED());
                        this.m_cCollectItemXin.m_lstBattleHero.Add(target);
                    }
                }
                this.m_eBattleState++;
                break;
            case BATTLE_STATE.BATTLE_STATE_GET_ITEM: //物品收集
                cmdFinish = true;
                if (!UpdateShuijing())
                {
                    cmdFinish = false;
                }
                if (!UpdateXin())
                {
                    cmdFinish = false;
                }
                if (cmdFinish)
                {
                    this.m_eBattleState++;
                }
                break;
            case BATTLE_STATE.BATTLE_STATE_GET_ITEM_END: //物品收集结束
                this.m_cCollectItemShuijing.Destory();
                this.m_cCollectItemXin.Destory();

                for (int i = 0; i < this.m_vecSelfHero.Length; i++)
                {
                    if (this.m_vecSelfHero[i] != null && !this.m_vecSelfHero[i].m_bDead)
                    {
                        float addbbhp = 0;
                        if (this.m_cSelfLeaderSkill != null)
                            addbbhp += this.m_cSelfLeaderSkill.RoundBBIncrease;

                        this.m_vecSelfHero[i].AddBBHP(addbbhp);
                        SetUIHeroBBHP(this.m_vecSelfHero[i]);
                    }
                }

                this.m_eBattleState++;
                break;
            case BATTLE_STATE.BATTLE_STATE_ENEMY_ATTACK_BEGIN:   //敌方攻击开始
                //敌方攻击开始装备影响
                for (int i = 0; i < this.m_vecTargetHero.Length; i++)
                {
                    BattleHero tmpHero = this.m_vecTargetHero[i];
                    BATTLE_FUNCTION.EQUIP_ACTION(BATTLE_TIME_SCENE.BATTLE_ROUND_START, tmpHero);
                }

                if (this.m_iBattleResult == BATTLE_WIN_NONE)
                {
                    //更新BUF状态
                    for (int i = 0; i < this.m_vecTargetHero.Length; i++)
                    {
                        if (this.m_vecTargetHero[i] != null && !this.m_vecTargetHero[i].m_bDead)
                        {
                            BATTLE_FUNCTION.BUF_UPDATE(this.m_vecTargetHero[i], this);
                            this.m_vecTargetHero[i].BUFDec();
                        }
                    }
                    for (int i = 0; i < this.m_vecSelfHero.Length; i++)
                    {
                        if (this.m_vecSelfHero[i] != null && !this.m_vecSelfHero[i].m_bDead)
                        {
                            BATTLE_FUNCTION.BUF_UPDATE(this.m_vecSelfHero[i], this);
                            this.m_vecSelfHero[i].BUFDec();
                        }
                    }
                }

                //初始化回合前数据
                for (int i = 0; i < this.m_vecTargetHero.Length; i++)
                {
                    if (this.m_vecTargetHero[i] != null && !this.m_vecTargetHero[i].m_bDead)
                    {
                        //this.m_vecTargetHero[i].m_iAttackNum = this.m_vecTargetHero[i].m_iAttackMaxNum;
                        this.m_vecTargetHero[i].GetAIControl().Initialize();
                        this.m_vecTargetHero[i].m_bDefence = false;
                        this.m_vecTargetHero[i].m_iDummyHP = this.m_vecTargetHero[i].m_iHp;
                        Array.Clear(this.m_vecTargetHero[i].m_vecHitStartTime, 0, this.m_vecTargetHero[i].m_vecHitStartTime.Length);
                        Array.Clear(this.m_vecTargetHero[i].m_vecHitEndTime, 0, this.m_vecTargetHero[i].m_vecHitEndTime.Length);
                    }
                }
                this.m_eBattleState++;
                break;
            case BATTLE_STATE.BATTLE_STATE_ENEMY_ATTACK: //敌方攻击
                cmdFinish = true;

                //收集品曲线
                if (!this.m_cCollectItemShuijing.UpdateCurve())
                {
                    cmdFinish = false;
                }
                if (!this.m_cCollectItemXin.UpdateCurve())
                {
                    cmdFinish = false;
                }

                for (int i = 0; i < this.m_vecTargetHero.Length; i++)
                {
                    if (this.m_vecTargetHero[i] != null)
                    {
                        //判断AI
                        if (this.m_vecTargetHero[i].GetAIControl().Update())
                        {
                            cmdFinish = false;
                        }
                    }
                }

                //攻击指令暂时结束,判断怪物是否死亡
                if (cmdFinish)
                {
                    for (int i = 0; i < this.m_vecSelfHero.Length; i++)
                    {
                        if (this.m_vecSelfHero[i] != null && this.m_vecSelfHero[i].m_iHp <= 0 && !this.m_vecSelfHero[i].m_bDead)
                        {
                            this.m_vecSelfHero[i].m_bDead = true;
                            this.m_vecSelfHero[i].GetCmdControl().CmdDie();
                        }
                    }
                }

                //判定己方是否指令结束
                for (int i = 0; i < this.m_vecSelfHero.Length; i++)
                {
                    if (this.m_vecSelfHero[i] != null)
                    {
                        if (this.m_vecSelfHero[i].GetCmdControl().Update())
                        {
                            cmdFinish = false;
                        }
                    }
                }

                if (cmdFinish)
                {
                    bool tmpFinish = true;
                    //检查是否无敌人可打
                    for (int i = 0; i < this.m_vecSelfHero.Length; i++)
                    {
                        if (this.m_vecSelfHero[i] != null && !this.m_vecSelfHero[i].m_bDead)
                        {
                            tmpFinish = false;
                        }
                    }
                    if (tmpFinish)
                    {
                        this.m_iBattleResult = BATTLE_WIN_TARGET;
                        this.m_eBattleState++;
                        break;
                    }
                }

                if (cmdFinish)
                {
                    for (int i = 0; i < this.m_vecTargetHero.Length; i++)
                    {
                        if (this.m_vecTargetHero[i] != null)
                        {
                            if (!this.m_vecTargetHero[i].m_bDead && this.m_vecTargetHero[i].m_iAttackNum > 0)
                            {
                                cmdFinish = false;
                            }
                        }
                    }
                }

                if (cmdFinish)
                {
                    this.m_eBattleState++;
                }
                break;
            case BATTLE_STATE.BATTLE_STATE_ENEMY_ATTACK_END: //敌方攻击结束
                //敌方攻击结束装备影响
                for (int i = 0; i < this.m_vecTargetHero.Length; i++)
                {
                    BattleHero tmpHero = this.m_vecTargetHero[i];
                    BATTLE_FUNCTION.EQUIP_ACTION(BATTLE_TIME_SCENE.BATTLE_ROUND_END, tmpHero);
                }

                //销毁
                for (int i = 0; i < this.m_vecSelfHero.Length; i++)
                {
                    if (this.m_vecSelfHero[i] != null && this.m_vecSelfHero[i].m_iHp <= 0 && this.m_vecSelfHero[i].m_bDead)
                    {
                        this.m_vecSelfHero[i].GetGfxObject().Destory();
                    }
                }
                this.m_eBattleState++;
                break;
            case BATTLE_STATE.BATTLE_STATE_GET_TARGET_ITEM_BEGIN:   //敌方物品收集开始
                this.m_cCollectItemShuijing.Clear();
                for (int i = 0; i < this.m_cCollectItemShuijing.m_lstMesh.Count; i++)
                {
                    BattleHero target = GetTargetAuto();
                    if (target != null)
                    {
                        this.m_cCollectItemShuijing.m_lstCurveStart.Add(this.m_cCollectItemShuijing.m_lstMesh[i].transform.localPosition);
                        this.m_cCollectItemShuijing.m_lstCurveBottom.Add(target.m_cStartPos + Vector3.up * 0.45f);
                        float disTime = (target.m_cStartPos - this.m_cCollectItemShuijing.m_lstMesh[i].transform.localPosition).magnitude / COLLECT_ITEM_SPEED;
                        this.m_cCollectItemShuijing.m_lstLineCostTime.Add(disTime);
                        this.m_cCollectItemShuijing.m_lstLineStartTime.Add(GAME_TIME.TIME_FIXED());
                        this.m_cCollectItemShuijing.m_lstBattleHero.Add(target);
                    }
                }
                this.m_cCollectItemXin.Clear();
                for (int i = 0; i < this.m_cCollectItemXin.m_lstMesh.Count; i++)
                {
                    BattleHero target = GetTargetAuto();
                    if (target != null)
                    {
                        this.m_cCollectItemXin.m_lstCurveStart.Add(this.m_cCollectItemXin.m_lstMesh[i].transform.localPosition);
                        this.m_cCollectItemXin.m_lstCurveBottom.Add(target.m_cStartPos + Vector3.up * 0.45f);
                        float disTime = (target.m_cStartPos - this.m_cCollectItemXin.m_lstMesh[i].transform.localPosition).magnitude / COLLECT_ITEM_SPEED;
                        this.m_cCollectItemXin.m_lstLineCostTime.Add(disTime);
                        this.m_cCollectItemXin.m_lstLineStartTime.Add(GAME_TIME.TIME_FIXED());
                        this.m_cCollectItemXin.m_lstBattleHero.Add(target);
                    }
                }
                this.m_eBattleState++;
                break;
            case BATTLE_STATE.BATTLE_STATE_GET_TARGET_ITEM: //敌方物品收集
                cmdFinish = true;
                if (!UpdateShuijing())
                {
                    cmdFinish = false;
                }
                if (!UpdateXin())
                {
                    cmdFinish = false;
                }
                if (cmdFinish)
                {
                    this.m_eBattleState++;
                }
                break;
            case BATTLE_STATE.BATTLE_STATE_GET_TARGET_ITEM_END: //敌方物品收集结束
                this.m_cCollectItemShuijing.Destory();
                this.m_cCollectItemXin.Destory();

                for (int i = 0; i < this.m_vecTargetHero.Length; i++)
                {
                    if (this.m_vecTargetHero[i] != null && !this.m_vecTargetHero[i].m_bDead)
                    {
                        float addbbhp = 0;
                        if (this.m_cTargetSelfLeaderSkill != null)
                            addbbhp += this.m_cTargetSelfLeaderSkill.RoundBBIncrease;

                        this.m_vecTargetHero[i].AddBBHP(addbbhp);
                        SetUIHeroBBHP(this.m_vecTargetHero[i]);
                    }
                }
                this.m_eBattleState++;
                break;
            case BATTLE_STATE.BATTLE_STATE_TIME_OUT_BEGIN:
                if (GAME_TIME.TIME_FIXED() - this.m_fBattleStartTime >= MAX_BATTLE_TIME)
                {
                    this.m_cResultEffect = GameObject.Instantiate(this.m_cResTimeOut) as GameObject;
                    this.m_cResultEffect.transform.parent = this.m_cBattleParent.transform;
                    this.m_cResultEffect.transform.localPosition = Vector3.zero;
                    this.m_cResultEffect.transform.localScale = Vector3.one;
                    this.m_eBattleState++;
                }
                else
                {
                    this.m_eBattleState = BATTLE_STATE.BATTLE_STATE_RESULT_BEGIN;
                }
                break;
            case BATTLE_STATE.BATTLE_STATE_TIME_OUT:
                if(this.m_cResultEffect == null )
                    this.m_eBattleState++;
                break;
            case BATTLE_STATE.BATTLE_STATE_TIME_OUT_END:
                this.m_eBattleState++;
                break;
            case BATTLE_STATE.BATTLE_STATE_RESULT_BEGIN: //结束计算开始
                int battlefinish = BATTLE_WIN_TARGET;
                for (int i = 0; i < HERO_MAX_NUM; i++)
                {
                    if (this.m_vecSelfHero[i] != null && !this.m_vecSelfHero[i].m_bDead)
                    {
                        battlefinish = BATTLE_WIN_NONE;
                    }
                }
                if (battlefinish <= 0)
                {
                    battlefinish = BATTLE_WIN_SELF;
                    for (int i = 0; i < HERO_MAX_NUM; i++)
                    {
                        if (this.m_vecTargetHero[i] != null && !this.m_vecTargetHero[i].m_bDead)
                        {
                            battlefinish = BATTLE_WIN_NONE;
                        }
                    }
                }
                if (battlefinish <= 0)
                {
                    float tmpdisTime = GAME_TIME.TIME_FIXED() - this.m_fBattleStartTime;
                    if (tmpdisTime >= MAX_BATTLE_TIME)
                    {
                        battlefinish = BATTLE_WIN_NONE;
                        this.m_iWinType = WIN_TYPE_TIME;
                        //比较参数
                        int selflive = 0;
                        int targetlive = 0;
                        int selfhp = 0;
                        int targethp = 0;
                        int selfDamage = 0;
                        int targetDamage = 0;
                        for (int i = 0; i<this.m_vecSelfHero.Length ; i++ )
                        {
                            if (this.m_vecSelfHero[i] != null)
                            {
                                if (!this.m_vecSelfHero[i].m_bDead)
                                {
                                    selflive++;
                                    selfhp += this.m_vecSelfHero[i].m_iHp;
                                }
                                selfDamage += this.m_vecSelfHero[i].m_iTotalDamage;
                            }
                            if (this.m_vecTargetHero[i] != null)
                            {
                                if (!this.m_vecTargetHero[i].m_bDead)
                                {
                                    targetlive++;
                                    targethp += this.m_vecTargetHero[i].m_iHp;
                                }
                                targetDamage += this.m_vecTargetHero[i].m_iTotalDamage;
                            }
                        }

                        //比较存活数
                        if (targetlive != selflive)
                        {
                            if (targetlive > selflive)
                                battlefinish = BATTLE_WIN_TARGET;
                            else
                                battlefinish = BATTLE_WIN_SELF;
                        }

                        //比较HP总数
                        if (battlefinish == BATTLE_WIN_NONE)
                        {
                            if (targethp > selfhp)
                                battlefinish = BATTLE_WIN_TARGET;
                            else if( targethp < selfhp )
                                battlefinish = BATTLE_WIN_SELF;
                        }

                        //比较伤害数
                        if (battlefinish == BATTLE_WIN_NONE)
                        {
                            if (targetDamage > selfDamage)
                                battlefinish = BATTLE_WIN_TARGET;
                            else if( targetDamage < selfDamage )
                                battlefinish = BATTLE_WIN_SELF;
                        }

                        if (battlefinish == BATTLE_WIN_NONE)
                        {
                            battlefinish = BATTLE_WIN_SELF;
                        }
                    }
                }
                this.m_iBattleResult = battlefinish;
                if (battlefinish > 0)
                {
                    for (int i = 0; i < HERO_MAX_NUM; i++)
                    {
                        this.m_vecSelfBuf[i].enabled = false;
                        this.m_vecTargetBuf[i].enabled = false;
                        if (this.m_vecSelfHero[i] != null)
                            this.m_vecSelfHero[i].ClearBUF();
                        if (this.m_vecTargetHero[i] != null)
                            this.m_vecTargetHero[i].ClearBUF();
                    }
                    if (battlefinish == BATTLE_WIN_SELF)
                    {
						MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_BATTLE_CONGRATULATE);
//                        MediaMgr.PlaySound(SOUND_DEFINE.SE_BATTLE_CONGRATULATE);

                        this.m_cResultEffect = GameObject.Instantiate(this.m_cResResultWin) as GameObject;

                        float tmpdisTime = GAME_TIME.TIME_FIXED() - this.m_fBattleStartTime;
                        if (tmpdisTime <= 0)
                        {
                            this.m_iWinType = WIN_TYPE_TIME;
                        }
                        else
                        {
                            this.m_iWinType = WIN_TYPE_PERFECT;
                            for (int i = 0; i < HERO_MAX_NUM; i++)
                            {
                                if (this.m_vecSelfHero[i] != null && this.m_vecSelfHero[i].m_bDead)
                                {
                                    this.m_iWinType = WIN_TYPE_NORMAL;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        this.m_cResultEffect = GameObject.Instantiate(this.m_cResResultLose) as GameObject;
                    }
                    if (this.m_cResultEffect != null)
                    {
                        this.m_cResultEffect.transform.parent = this.m_cBattleParent.transform;
                        this.m_cResultEffect.transform.localPosition = Vector3.zero;
                        this.m_cResultEffect.transform.localScale = Vector3.one;
                    }
                    this.m_eBattleState++;
                }
                else
                {
                    this.m_eBattleState = BATTLE_STATE.BATTLE_STATE_SELF_ATTACK_BEGIN;
                }
                break;
            case BATTLE_STATE.BATTLE_STATE_RESULT:   //结果计算
                if (this.m_cResultEffect == null)
                {
                    this.m_eBattleState++;
                }
                break;
            case BATTLE_STATE.BATTLE_STATE_RESULT_END:   //结果计算结束
                this.m_eBattleState++;
                break;
            case BATTLE_STATE.BATTLE_STATE_END_BEGIN:   //结束开始
                this.m_eBattleState++;
                break;
            case BATTLE_STATE.BATTLE_STATE_END1:  //结束
                this.m_fEndWaitStartTime = GAME_TIME.TIME_FIXED();
                this.m_eBattleState++;
                break;
            case BATTLE_STATE.BATTLE_STATE_END2:
                if (GAME_TIME.TIME_FIXED() - this.m_fEndWaitStartTime > END_WAIT_TIME)
                {
                    this.m_fEndWaitStartTime = 0;
                    this.m_eBattleState++;
                }
                break;
            case BATTLE_STATE.BATTLE_STATE_END_END:  //结束结束
                if (this.m_iBattleResult == BATTLE_WIN_SELF)
                {
                    if (this.m_delFinishCallBack != null)
                    {
                        this.m_delFinishCallBack();
                    }
                }
                else
                {
                    BattleEnd();
                }
                this.m_eBattleState++;
                break;
        }

        return true;
    }

    /// <summary>
    /// 渲染更新
    /// </summary>
    /// <returns></returns>
    public override bool Render()
    {
        if (!this.m_bShow)
            return false;

        //技能头像
        if (this.m_fSkillAvatarStartTime > 0)
        {
            float disskillTime = GAME_TIME.TIME_REAL() - this.m_fSkillAvatarStartTime;
            if (disskillTime > SKILL_AVATOR_TIME)
            {
                this.m_fSkillAvatarStartTime = 0;
                GameObject.Destroy(this.m_cSkillAvatar);
                this.m_cSkillAvatar = null;
                Time.timeScale = 1;
                //恢复数字
                for (int i = 0; i < this.m_lstHurtTxt.Count; i++)
                {
                    if (this.m_lstHurtTxt[i] != null)
                    {
                        this.m_lstHurtTxt[i].GetComponentInChildren<MeshRenderer>().enabled = true;
                    }
                }
                //恢复掉落
                for (int i = 0; i < this.m_cCollectItemShuijing.m_lstMesh.Count; i++)
                {
                    this.m_cCollectItemShuijing.m_lstMesh[i].SetActive(true);
                }
                for (int i = 0; i < this.m_cCollectItemXin.m_lstMesh.Count; i++)
                {
                    this.m_cCollectItemXin.m_lstMesh[i].SetActive(true);
                }

                this.m_iUIState = UI_STATE_NONE;
            }
        }

        return base.Render();
    }

    /// <summary>
    /// 战斗结束
    /// </summary>
    private void BattleEnd()
    {
        this.m_eBattleState = 0;
        if (this.m_delEndCallBack != null)
        {
            this.m_delEndCallBack();
        }
    }


    /// <summary>
    /// 生成金币
    /// </summary>
    public override void GeneratorJinbi(Vector3 pos, int val)
    { 
    }

    /// <summary>
    /// 生成农场点
    /// </summary>
    public override void GeneratorFarm(Vector3 pos, int val)
    { 
    }

    /// <summary>
    /// 生成物品
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="itemTableID"></param>
    public override void GeneratorItem(Vector3 pos, int itemTableID)
    { 
    }
}
