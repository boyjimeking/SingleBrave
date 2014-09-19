
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Gfx;
using Game.Base;
using Game.Resource;
using Game.Media;


//  GUIBattle.cs
//  Author: Lu Zexi
//  2013-11-28



/// <summary>
/// 战场GUI
/// </summary>
public abstract class GUIBattle : IGUIBattle
{
    public delegate void CALL_BACK();   //回调委托

    private const string RES_MAIN = "GUI_Battle"; //主资源
    private const string RES_HURT_NUM = "BATTLE_NUM1"; //伤害数字资源
    private const string RES_RECOVER_NUM = "BATTLE_NUM_EX"; //恢复数字资源
    private const string RES_SPARK_EFFECT = "effect_spark";    //Spark特效资源
    private const string RES_CRITICAL_EFFECT = "effect_critical";  //Critical特效资源
    private const string RES_FARM_MESH = "BATTLE_Farm";    //农场点模型
    private const string RES_XIN_MESH = "BATTLE_Xin";   //心点模型
    private const string RES_SHUIJING_MESH = "BATTLE_Shuijing"; //水晶模型
    private const string RES_JINBI_MESH = "BATTLE_Jinbi";   //金币模型
    private const string RES_SOUL_MESH = "BATTLE_hunpo"; //灵魂模型
    private const string RES_BOX_MESH = "BATTLE_baoxiang";  //宝箱模型
    private const string RES_BOX_MONSTER_EFFECT = "effect_bxgchuxian";   //宝箱怪出现特效
    private const string RES_WIN_EFFECT = "effect_win1"; //胜利特效
    private const string RES_LOSE_EFFECT = "effect_gameover";   //失败特效
    private const string RES_BOX_WARNING_EFFECT = "effect_box1";   //宝箱警告特效
    private const string RES_BOSS_WARNING_EFFECT = "effect_boss1";   //BOSS警告特效
    private const string RES_SKILL_AVATAR = "BATTLE_SkillAvatar"; //技能头像
    private const string RES_DEBUFF_DU_EFFECT = "effect_debuff_du";    //毒DEBUFF特效
    private const string RES_DEBUFF_MA_EFFECT = "effect_debuff_mabi";    //麻痹特效
    private const string RES_DEBUFF_FENGYIN_EFFECT = "effect_debuff_zuzhou";   //封印特效
    private const string RES_DEBUFF_XURUO_EFFECT = "effect_debuff_xuruo"; //虚弱特效
    private const string RES_DEBUFF_POJIA_EFFECT = "effect_debuff_pojia"; //破甲特效
    private const string RES_DEBUFF_POREN_EFFECT = "effect_debuff_poren"; //破刃特效
    private const string RES_CONGRATULATION_EFFECT = "effect_congratulation";  //恭喜特效
    private const string RES_DEFENCE_EFFECT = "effect_fangyu1";   //防御特效
    private const string RES_SKILL_SHOW_BG_EFFECT = "effect_cutin_";    //技能展示属性背景
    private const string RES_COLLECT_HERO_EFFECT1 = "effect_mengjiang1"; //收集英雄特效1
    private const string RES_COLLECT_HERO_EFFECT2 = "effect_mengjiang2"; //收集英雄特效2
    private const string RES_COLLECT_HERO_EFFECT3 = "effect_mengjiang3"; //收集英雄特效3
    private const string RES_COLLECT_HERO_EFFECT4 = "effect_mengjiang4"; //收集英雄特效4
    private const string RES_BOX_OPEN_EFFECT = "effect_baoxiang";   //宝箱打开特效
    private const string RES_BATTLE_FONT1 = "BATTLE_Font_1";    //战斗字体1
    private const string RES_BATTLE_FONT2 = "BATTLE_Font_2";    //战斗字体2
    private const string RES_BATTLE_FONT3 = "BATTLE_Font_3";    //战斗字体3
    private const string RES_BATTLE_FONT4 = "BATTLE_Font_4";    //战斗字体4

    protected BATTLE_STATE m_eBattleState = BATTLE_STATE.BATTLE_STATE_BEGIN_BEGIN; //当前战斗状态
    protected int m_iUIState = 0; //UI状态
    protected int m_iSelectIndex = 0;   //物品选择索引

    protected const int UI_STATE_NONE = 0;    //UI无状态
    protected const int ITEM_STATE_SELECT = 1;    //物品选择状态
    protected const int SKILL_SHOW_STATE = 2;   //技能展示状态

    protected const int BATTLE_WIN_NONE = 0;      //无胜利
    protected const int BATTLE_WIN_SELF = 1;      //己方胜利
    protected const int BATTLE_WIN_TARGET = 2;    //敌方胜利

    /// <summary>
    /// 战斗状态
    /// </summary>
    public enum BATTLE_STATE
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
        BATTLE_STATE_WARING_BEGIN,  //警告开始
        BATTLE_STATE_WARING,    //警告
        BATTLE_STATE_WARING_END,    //警告结束
        BATTLE_STATE_WARING_BOSS_BEGIN, //BOSS警告开始
        BATTLE_STATE_WARING_BOSS,   //BOSS警告开始
        BATTLE_STATE_WARING_BOSS_END,   //BOSS警告开始
        BATTLE_STATE_SELF_ATTACK_BEGIN, //己方攻击开始
        BATTLE_STATE_SELF_ATTACK,   //己方攻击
        BATTLE_STATE_SELF_ATTACK_END,   //己方攻击结束
        BATTLE_STATE_GET_ITEM_BEGIN,    //获取物品开始
        BATTLE_STATE_GET_ITEM,  //获取物品
        BATTLE_STATE_GET_ITEM_END,  //获取物品结束
        BATTLE_STATE_ENEMY_ATTACK_BEGIN,    //敌人攻击开始
        BATTLE_STATE_ENEMY_ATTACK,  //敌人攻击
        BATTLE_STATE_ENEMY_ATTACK_END,  //敌人攻击结束
        BATTLE_STATE_RESULT_BEGIN,  //结果计算开始
        BATTLE_STATE_RESULT,    //结果计算
        BATTLE_STATE_RESULT_END,    //结果计算结束
        BATTLE_STATE_GET_BOX_BEGIN, //获取宝箱开始
        BATTLE_STATE_GET_BOX,   //获取宝箱
        BATTLE_STATE_GET_BOX_END,   //获取宝箱结束
        BATTLE_STATE_END_BEGIN, //结束开始
        BATTLE_STATE_END1,  //结束
        BATTLE_STATE_END2,  //结束
        BATTLE_STATE_END_END,   //结束结束
    }

    protected const int ITEM_MAX_NUM = 5; //物品携带最多数量
    protected const int HERO_MAX_NUM = 6; //英雄最多数量

    private const string BATTLE_PARENT = "BATTLE";    //战场父节点
    private const string BATTLE_BBPOS = "BBPOS";        //BB技能释放点
    private const string BATTLE_GOLD_POS = "GOLDPOS"; //金币位置
    private const string BATTLE_FARM_POS = "FARMPOS"; //农场点位置
    private const string BATTLE_SOUL_POS = "SOULPOS"; //灵魂点位置
    private const string BATTLE_TARGET_POS = "TARGET_POS/pos";    //目标点
    private const string BATTLE_TARGET_ATTACK_POS = "TARGET_ATTACK_POS/pos"; //目标攻击点
    private const string BATTLE_MYSELF_POS = "MYSELF_POS/pos";    //自身点
    private const string BATTLE_MYSQL_ATTACK_POS = "MYSELF_ATTACK_POS/pos";  //自身攻击点

    private const string GUI_TARGET_POS = "Battle/Pos/TargetPos";   //UI目标站立点
    private const string GUI_SELF_POS = "Battle/Pos/SelfPos"; //UI自身站立点
    private const string GUI_BLACK_GROUND = "BlackGround"; //黑背景
    private const string GUI_TARGET_NAME = "Battle/Main/Lab_Title"; //目标名字
    private const string GUI_TARGET_HP_BAR = "Battle/Main/Spr_RedBlood";    //目标HP条
    private const string GUI_TARGET_PROPERTY = "Battle/Main/Spr_Property";  //目标属性
    private const string GUI_HERO_BB_IMG = "Battle/Main/BBSkill";  //BB技能图
    private const string GUI_HERO_DEFENCE_IMG = "Battle/Main/Defense";  //BB防御图

    private const string GUI_HERO_BTN = "Battle/Main/Item";   //英雄按钮
    private const string GUI_HERO_BLACK = "Spr_BlackBg";   //黑屏遮罩
    private const string GUI_HERO_PROPERTY = "Sprite_Property";  //英雄属性
    private const string GUI_HERO_HP = "Label_Hp";  //HP
    private const string GUI_HERO_HP_BAR = "HP_Bg/Sprite_HpContent";  //HP BAR
    private const string GUI_HERO_ENERGY = "WuShuang_Bg/Sprite_HpContent";  //能量
    private const string GUI_HERO_NAME = "Label_Name";    //名字
    private const string GUI_HERO_ATTACK_PROPERTY = "Spr_PropertyStatus";   //攻击属性判定
    private const string GUI_HERO_AVATOR = "Sprite_Icon";   //头像
    private const string GUI_HERO_DIE = "Spr_Dead";     //死亡
    private const string GUI_HERO_EMPTY = "Spr_Empty";  //空缺
    private const string GUI_HERO_HP_PARENT = "HP_Bg";       //HP父节点
    private const string GUI_HERO_ENERGY_PARENT = "WuShuang_Bg";   //能量父节点
    private const string GUI_HERO_HP_IMG = "Sprite_TextHp";          //HP图片
    private const string GUI_HERO_ENERGY_IMG = "Sprite_TextWuShuang";      //能量图片
    private const string GUI_HERO_FRAME = "Sprite_Edge";   //英雄框
    private const string GUI_HERO_FRAME_BACK = "Sprite_Bg";  //底框
    private const string GUI_HERO_BB_ANI_IMG = "Spr_BBBack";  //BB特效动画图
    private const string GUI_HERO_BB_ANI_IMG1 = "Spr_BBBack_White";  //BB特效动画图1
    private const string GUI_HERO_BB_ANI_IMG2 = "Spr_BBBack_White2";  //BB特效动画图2
    private const string GUI_HERO_ITEM_GUARD = "SprGuard";  //物品箭头
    private const string GUI_HERO_TARGET_BTN = "Battle/Pos/TargetPos";  //目标按钮
    private const string GUI_HERO_TARGET_SP = "Battle/Pos/TargetSp";   //目标点图
    private const string GUI_BOX_GUIDE = "box_guide";   //宝箱箭头

    private const string GUI_ITEM_BACK_COLLIDER = "ItemCover";   //背景碰撞体
    private const string GUI_ITEM_BTN = "Battle/FooterPanel/Item"; //物品按钮
    private const string GUI_ITEM_SPRITE_BACK = "Battle/FooterPanel/Sprite_Back"; //物品选中以后背景遮罩
    private const string GUI_ITEM_BTN_CANCEL = "Battle/FooterPanel/Btn_cancel"; //物品选中以后返回按钮
    private const string GUI_ITEM_ICON = "Sprite_Content";    //物品ICON
    private const string GUI_ITEM_NUM = "Label_Count"; //物品数量
    private const string GUI_ITEM_NAME = "Label_Name";    //物品名字

    private const string GUI_MENU_BTN = "Battle/TopPanel/Btn_Menu"; //菜单按钮
    private const string GUI_BOX_GIVEUP_BTN = "Battle/Main/BoxBtn"; //宝箱放弃按钮
    private const string GUI_GOLD_POS = "Battle/Pos/GoldPos"; //金币位置
    private const string GUI_FARM_POS = "Battle/Pos/FarmPos"; //农场点位置
    private const string GUI_SOUL_POS = "Battle/Pos/SoulPos"; //灵魂点位置
    private const string GUI_GOLD = "Battle/TopPanel/Label_JinBi"; //金币
    private const string GUI_FARMPOINT = "Battle/TopPanel/Label_NongChangDian";    //农场点
    private const string GUI_COLLECT = "Battle/TopPanel/Label_Zhanshi";  //收集
    private const string GUI_SELF_BUF = "Battle/Pos/SelfPos";  //BUF位置
    private const string GUI_TARGET_BUF = "Battle/Pos/TargetPos"; //目标位置
    private const string GUI_TOP_PANEL = "Battle/TopPanel";//顶部面板

    private const string SKILL_SHOW_HEAD_PATH = "effect_cutin_head"; //头像
    private const string SKILL_SHOW_LABEL_PATH = "effect_cutin_zidiwen/Label";   //文字
    private const string SKILL_SHOW_BG_PATH = "effect_cutin_zidiwen";    //背景

    private const string GUI_ITEM_USE_INFO = "GUI_ReconcileHouseItemCell";  //使用战斗物品提示显示

    private Vector3 BLACK_GROUND_START = new Vector3(-11, 3.679647f, 23.23237f);    //开幕
    private Vector3 BLACK_GROUND_END = new Vector3(0, 3.679647f, 23.23237f);     //关幕

    protected GameObject m_cBattleParent; //战场父节点
    private GameObject m_cBattleBBPos; //战场技能移动位置点
    private GameObject m_cBattleGoldPos;  //金币点位置
    private GameObject m_cBattleFarmPointPos;    //农场点位置
    private GameObject m_cBattleSoulPointPos; //收集点位置
    protected GameObject m_cScene;    //场景
    private GameObject[] m_vecTargetPos;    //目标站点
    private GameObject[] m_vecUITargetPos;  //目标UI站立点
    private GameObject[] m_vecTargetAttackPos;  //目标攻击点
    private GameObject[] m_vecMyselfPos;    //自身站点
    private GameObject[] m_vecUIMyselfPos;   //自身UI站立点
    private GameObject[] m_vecMyselfAttackPos;  //自身攻击点

    private GameObject m_cBlackGround;  //黑背景
    private GameObject[] m_vecHeroBtn;  //英雄按钮
    private UISprite[] m_vecHeroBlack;    //黑屏遮罩
    private UISprite[] m_vecHeroProperty;   //英雄属性
    private UILabel[] m_vecHeroHP;  //英雄HP
    private UISprite[] m_vecHeroHPBar;  //英雄HP条
    private UISprite[] m_vecHeroEnergyBar;  //英雄能量条
    private UILabel[] m_vecHeroName;    //英雄名字
    private UISprite[] m_vecHeroAttackProperty; //英雄攻击属性
    private UISprite[] m_vecHeroAvator;    //英雄头像
    private GameObject[] m_vecHeroDie;  //英雄死亡
    private GameObject[] m_vecHeroEmpty;    //空缺
    private GameObject[] m_vecHeroHPParent; //HP父节点
    private GameObject[] m_vecHeroEnergyParent; //能量父节点
    private GameObject[] m_vecHeroHPIMG;    //HP图片
    private GameObject[] m_vecHeroEnergyIMG;    //能量图片
    private UISprite[] m_vecHeroFrame;  //英雄框
    private UISprite[] m_vecHeroFrameBack;  //底框
    private UISprite[] m_vecHeroBBANI_IMG;   //BB动画图
    private GameObject[] m_vecHeroBBANI_IMG1;   //BB动画图1
    private GameObject[] m_vecHeroBBANI_IMG2;   //BB动画图1
    private GameObject[] m_vecItemGuard;  //物品箭头
    private GameObject[] m_vecHeroTargetBtn;    //目标按钮
    private GameObject[] m_vecBoxGuide; //宝箱箭头
    private GameObject m_cHeroTargetSp; //目标点图
    private GameObject m_cTopPanel;//顶部面板

    private GameObject m_vecHeroBBIMG;    //BB技能图
    private GameObject m_vecHeroDefenceIMG;   //防御图
    private Texture[] m_vecTexSkillShowBG;  //技能展示属性背景
    private GameObject m_cItemBackCollider;   //背景碰撞体
    private GameObject[] m_vecItemBtn;  //物品按钮
    private UISprite[] m_vecItemIcon;   //物品icon
    private UILabel[] m_vecItemName;    //物品名称
    private UILabel[] m_vecItemNum;     //物品数量
    private UISprite m_cSprBlack;  //物品栏使用时候的黑色遮罩
    private GameObject m_cBtnItemBack; //物品栏使用时候出现的返回按钮

    private GameObject m_cMenuBtn;  //菜单按钮
    private GameObject m_cBoxGiveUpBtn; //宝箱放弃按钮
    private UILabel m_cGold;    //金币数量
    private UILabel m_cFarmPoint;   //农场点
    private UILabel m_cCollect;     //收集数

    private UILabel m_cTargetName;  //目标人名
    private UISprite m_cTargetHPBar;   //目标血条
    private UISprite m_cTargetProperty; //目标属性

    private UISprite[] m_vecSelfBuf;  //己方BUF
    private UISprite[] m_vecTargetBuf;    //目标BUF

    private BattleHero[] m_vecSelfHero; //己方英雄
    private BattleHero[] m_vecTargetHero;   //目标英雄
    protected Item[] m_vecItem;   //物品
    private int m_iLeaderIndex;  //队长索引
    private LeaderSkillTable m_cSelfLeaderSkill;    //自身队长技能
    private LeaderSkillTable m_cFriendLaderSkill;   //队友队长技能

    //宝箱
    private GfxObject[] m_lstBox;  //宝箱列表
    private bool[] m_lstBoxOpen;    //宝箱打开状态
    private bool[] m_lstBoxIsMonster;   //是否为怪物

    //伤害数值
    private const float HURT_NUM_SPEED = 0.04f; //伤害数值上升速度
    private const float HURT_NUM_TIME = 0.7f;    //伤害数字存留时间
    private UnityEngine.Object m_cResHurtNum;  //伤害数字资源
    private UnityEngine.Object m_cResRecoverNum;    //恢复数字资源
    private UnityEngine.Object m_cResDefenceEffect;  //防御特效
    private List<GameObject> m_lstHurtTxt;  //伤害数字
    private List<float> m_lstHurtTxtTime;   //伤害数字开始时间

    //特效
    private const float SPARK_TIME = 2; //Spark特效时间
    private const float CRITICAL_TIME = 2;   //暴击特效时间
    private const float RESULT_EFFECT_TIME = 2; //结果特效持续时间
    private UnityEngine.Object m_cResSpark; //Spark特效资源
    private UnityEngine.Object m_cResCritical;  //暴击特效资源
    private UnityEngine.Object m_cResResultWin; //胜利特效
    private UnityEngine.Object m_cResResultLose;    //失败特效
    private UnityEngine.Object m_cResResultCongratulation;  //恭喜特效
    private UnityEngine.Object m_cResBoxWarningEffect;  //宝箱警告特效
    private UnityEngine.Object m_cResBossWarningEffect; //BOSS警告特效
    private UnityEngine.Object m_cResBoxMonsterEffect;  //宝箱怪出现特效
    private UnityEngine.Object m_cResBoxOpenEffect; //宝箱打开特效
    private UnityEngine.Object m_cResCollectHeroEffect1;    //收集英雄特效1
    private UnityEngine.Object m_cResCollectHeroEffect2;    //收集英雄特效2
    private UnityEngine.Object m_cResCollectHeroEffect3;    //收集英雄特效3
    private UnityEngine.Object m_cResCollectHeroEffect4;    //收集英雄特效4
    private UnityEngine.Object m_cResBattleFont1;   //战斗字体1
    private UnityEngine.Object m_cResBattleFont2;   //战斗字体1
    private UnityEngine.Object m_cResBattleFont3;   //战斗字体1
    private UnityEngine.Object m_cResBattleFont4;   //战斗字体1
    private List<GameObject> m_lstSpark;    ///Spark特效列表
    private List<float> m_lstSparkStartTime;   //Spark特效开始时间列表
    private List<GameObject> m_lstCritical;    ///暴击特效列表
    private List<float> m_lstCriticalStartTime;   //暴击特效开始时间列表
    private GameObject m_cResultEffect; //结果特效
    private GameObject m_cWarningEffect;    //警告特效
    public string m_strSceneName;   //场景名字

    //战斗结果数据
    protected int m_iMaxGold;   //最大金币数
    protected int m_iMaxFarm;   //最大农场点
    protected string m_strTittle;   //标题
    protected string m_strContent;  //内容
    protected int m_iCurLayer; //当前层级
    protected int m_iMaxLayer; //最大层级
    protected bool m_bBoss; //是否为BOSS关
    public int m_iRound;    //回合数
    public int m_iBattleResult;    //战斗结果
    public int m_iGoldNum; //收集金币数量
    public int m_iGoldDummyNum; //收集的虚拟金币量
    public int m_iFarmNum; //收集农场点数量
    public int m_iFarmDummyNum; //收集的虚拟农场点
    private bool m_bSoul;    //是否已经得到灵魂
    public List<int> m_lstSoul;    //灵魂英雄配置表ID列表
    public List<int> m_lstItem; //物品
    public List<int> m_lstItemNum;  //物品个数
    protected CALL_BACK m_delFinishCallBack; //正常结束回调
    protected CALL_BACK m_delEndCallBack;    //强制结束回调

    //整体战斗数据
    //protected int m_iCanCatchNum;   //可捕捉的灵魂数
    protected FAV_TYPE m_eFavType;  //优惠类型
    protected float m_fBoxRate;   //宝箱掉落概率
    protected float m_fBoxMonsterRate;  //宝箱怪概率
    protected int m_iBoxMonsterTableID;   //宝箱怪ID
    protected float m_fBoxDropItemRate;  //素材宝箱概率
    protected float m_fBoxBBHpRate;   //宝箱水晶掉落率
    protected int m_iBoxBBHPDropNum;  //宝箱水晶掉落个数
    protected float m_fBoxHeartRate;  //宝箱心掉落率
    protected int m_iBoxHeartDropNum; //宝箱心掉落数量
    protected float m_fBoxFarmRate;   //宝箱农场点掉率
    protected int m_iBoxFarmDropNum;  //宝箱农场点掉落数量
    protected float m_fBoxGoldRate;   //宝箱金币掉落率
    protected int m_iBoxGoldDropNum;  //宝箱金币掉落数量
    protected float m_fDropItemRate;   //战斗素材掉落概率

    //临时变量
    private const float END_WAIT_TIME = 0.5f;   //结束等待时间
    private const float PRESS_DIS_TIME = 0.2f;  //按下超时时间
    private const float PRESS_DIS_TIME_MIN = 0.1f;  //按下超时最小时间
    private const float SKILL_AVATOR_TIME = 1.25f; //秒
    private const float BLACK_GROUND_COST_TIME = 0.5F;    //黑幕移动时间
    public bool m_bLoseShow; //是否展示失败界面
    public bool m_bMenuShow;    //菜单是否可展示
    private int m_iAutoTargetIndex;   //自动选择目标索引
    private int m_iSelectTargetIndex;    //手动选择索引
    private Vector2 m_cHeroPressPos;    //英雄BB技能按钮按下位置
    private float m_fPressBtnStartTime; //按下按钮开始时间
    private bool m_bPressTimeOut = false;  //按下长时间超时无效标志
    private GameObject m_cPressBtnObject;   //按下激活的物体
    private float m_fEndWaitStartTime;  //结束等待开始时间
    private int m_iBufShowIndex;    //展示BUF索引
    private float m_fBufShowStartTime;  //开始展示BUF时间
    private float m_fSkillAvatarStartTime;  //技能头像开始时间
    private GameObject m_cSkillAvatar;    //技能头像

    //统计数据
    protected int m_iRecordShuijingNum;    //单场战斗记录水晶数
    protected int m_iRecordMaxShuijingNum;    //单场战斗记录水晶数
    protected int m_iTotalShuijingNum;    //总水晶数量
    protected int m_iRecordXinNum;    //单场战斗记录心数量
    protected int m_iRecordMaxXinNum;    //单场战斗记录心数量
    protected int m_iTotalXinNum; //总心数量
    protected int m_iRoundHurtSum;    //回合内总伤害
    protected int m_iRoundMaxHurt;    //回合内最大总伤害
    protected int m_iRoundSparkNum;    //回合内Spark次数
    protected int m_iRoundMaxSparkNum;    //回合内最多Spark次数
    protected int m_iTotalSparkNum;   //总Spark次数
    protected int m_iTotalSkillNum;   //总技能使用次数
    protected int m_iTotalBoxMonster; //宝箱怪出现次数

    //收集品
    private UnityEngine.Object m_cResFarm;  //农场点资源
    private UnityEngine.Object m_cResJinbi; //金币点资源
    private UnityEngine.Object m_cResShuijing;  //水晶点资源
    private UnityEngine.Object m_cResXin;   //心点资源
    private UnityEngine.Object m_cResSoul;  //灵魂资源
    private UnityEngine.Object m_cResBox;   //宝箱资源
    private UnityEngine.Object m_cResSkillAvatar;   //技能头像
    private const float COLLECT_ITEM_DROP_TIME = 1f; //物品掉落时间
    private const float COLLECT_ITEM_SPEED = 10f;  //物品收集速度
    private const float COLLECT_ITEM_TIME = 1f; //物品收集消失时间
    private CollectItem m_cCollectItem; //物品收集
    private CollectItem m_cCollectItemFarm;    //农场点
    private CollectItem m_cCollectItemJinbi;   //金币点
    private CollectItem m_cCollectItemShuijing; //水晶点
    private CollectItem m_cCollectItemXin;  //心点
    private CollectItem m_cCollectItemSoul; //灵魂


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

    

    /// <summary>
    ///  使用战斗物品提示
    /// </summary>
    public class ReconceliHouseShowItem
    {
        private const string LB_Count = "Lab_Count";  //装备数量
        private const string LB_Desc = "Lab_Desc";  //描述
        private const string LB_Name = "Lab_Name";  //名称
        private const string SP_Item = "Spr_Icon"; //图像spr
        private const string SP_BACK = "Spr_Back";  //遮罩
        private const string SP_NOITEM = "Spr_NoItem";  //素材不足
        private const string SP_NEW = "Spr_New"; //新物品图标

        public GameObject m_cItem;
        public UILabel m_cLbCount;
        public UILabel m_cLbDesc;
        public UILabel m_cLbName;
        public UISprite m_cItemPath;
        public UISprite m_cSpBack;
        public UISprite m_cSpNoItem;
        public UISprite m_cSpNew;

        public int m_cItemTableId;

        public ReconceliHouseShowItem(UnityEngine.Object parent)
        {
            m_cItem = GameObject.Instantiate(parent) as GameObject;
            m_cItemPath = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, SP_Item);
            m_cLbCount = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cItem, LB_Count);
            m_cLbDesc = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cItem, LB_Desc);
            m_cLbName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cItem, LB_Name);
            m_cSpBack = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, SP_BACK);
            m_cSpNoItem = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, SP_NOITEM);
            m_cSpNew = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, SP_NEW);     

        }  
    }

    private ReconceliHouseShowItem m_cUseItemInfoShow;  //使用战斗物品的提示

    public GUIBattle(GUIManager guiMgr, int guiid, UILAYER layer)
        : base(guiMgr, guiid, layer)
    {
        this.m_cCollectItem = new CollectItem();
        this.m_cCollectItemFarm = new CollectItem();
        this.m_cCollectItemJinbi = new CollectItem();
        this.m_cCollectItemShuijing = new CollectItem();
        this.m_cCollectItemXin = new CollectItem();
        this.m_cCollectItemSoul = new CollectItem();

        this.m_lstBox = new GfxObject[HERO_MAX_NUM];
        this.m_lstBoxOpen = new bool[HERO_MAX_NUM];
        this.m_lstBoxIsMonster = new bool[HERO_MAX_NUM];

        this.m_lstSoul = new List<int>();
        this.m_lstItem = new List<int>();
        this.m_lstItemNum = new List<int>();
        this.m_lstHurtTxt = new List<GameObject>();
        this.m_lstHurtTxtTime = new List<float>();
        this.m_lstSpark = new List<GameObject>();
        this.m_lstSparkStartTime = new List<float>();
        this.m_lstCritical = new List<GameObject>();
        this.m_lstCriticalStartTime = new List<float>();

        this.m_vecTexSkillShowBG = new Texture[HERO_MAX_NUM];
        this.m_vecItem = new Item[ITEM_MAX_NUM];
        this.m_vecSelfBuf = new UISprite[HERO_MAX_NUM];
        this.m_vecTargetBuf = new UISprite[HERO_MAX_NUM];
        this.m_vecSelfHero = new BattleHero[HERO_MAX_NUM];
        this.m_vecTargetHero = new BattleHero[HERO_MAX_NUM];
        this.m_vecTargetPos = new GameObject[HERO_MAX_NUM];
        this.m_vecUITargetPos = new GameObject[HERO_MAX_NUM];
        this.m_vecTargetAttackPos = new GameObject[HERO_MAX_NUM];
        this.m_vecMyselfPos = new GameObject[HERO_MAX_NUM];
        this.m_vecUIMyselfPos = new GameObject[HERO_MAX_NUM];
        this.m_vecMyselfAttackPos = new GameObject[HERO_MAX_NUM];

        this.m_vecBoxGuide = new GameObject[HERO_MAX_NUM];

        this.m_vecHeroBtn = new GameObject[HERO_MAX_NUM];
        this.m_vecHeroBlack = new UISprite[HERO_MAX_NUM];
        this.m_vecHeroProperty = new UISprite[HERO_MAX_NUM];
        this.m_vecHeroHP = new UILabel[HERO_MAX_NUM];
        this.m_vecHeroHPBar = new UISprite[HERO_MAX_NUM];
        this.m_vecHeroEnergyBar = new UISprite[HERO_MAX_NUM];
        this.m_vecHeroName = new UILabel[HERO_MAX_NUM];
        this.m_vecHeroAttackProperty = new UISprite[HERO_MAX_NUM];
        this.m_vecHeroAvator = new UISprite[HERO_MAX_NUM];
        this.m_vecHeroDie = new GameObject[HERO_MAX_NUM];
        this.m_vecHeroEmpty = new GameObject[HERO_MAX_NUM];
        this.m_vecHeroHPParent = new GameObject[HERO_MAX_NUM];
        this.m_vecHeroEnergyParent = new GameObject[HERO_MAX_NUM];
        this.m_vecHeroHPIMG = new GameObject[HERO_MAX_NUM];
        this.m_vecHeroEnergyIMG = new GameObject[HERO_MAX_NUM];
        this.m_vecHeroFrame = new UISprite[HERO_MAX_NUM];
        this.m_vecHeroFrameBack = new UISprite[HERO_MAX_NUM];
        this.m_vecHeroBBANI_IMG = new UISprite[HERO_MAX_NUM];
        this.m_vecHeroBBANI_IMG1 = new GameObject[HERO_MAX_NUM];
        this.m_vecHeroBBANI_IMG2 = new GameObject[HERO_MAX_NUM];
        this.m_vecItemGuard = new GameObject[HERO_MAX_NUM];
        this.m_vecHeroTargetBtn = new GameObject[HERO_MAX_NUM];

        this.m_vecItemBtn = new GameObject[ITEM_MAX_NUM];
        this.m_vecItemIcon = new UISprite[ITEM_MAX_NUM];
        this.m_vecItemName = new UILabel[ITEM_MAX_NUM];
        this.m_vecItemNum = new UILabel[ITEM_MAX_NUM];

        this.m_lstDropItem = new List<int>();
        this.m_lstDropItemRate = new List<float>();
    }

    /// <summary>
    /// 设置战斗状态
    /// </summary>
    /// <param name="state"></param>
    public override void SetBattleState(int state)
    {
        this.m_eBattleState = (BATTLE_STATE)state;
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
                if (this.m_vecSelfHero[i].m_iHp > 0)
                {
                    MIN = this.m_vecSelfHero[i].m_iHp;
                    index = i;
                }
            }
        }
        if (index >= 0 && index < this.m_vecSelfHero.Length)
        {
            return this.m_vecSelfHero[index];
        }
        return null;
    }

    /// <summary>
    /// 获取最小HP己方英雄
    /// </summary>
    /// <returns></returns>
    public override BattleHero GetMaxHPSelf()
    {
        float MAX = float.MinValue;
        int index = -1;
        for (int i = 0; i < this.m_vecSelfHero.Length; i++)
        {
            if (this.m_vecSelfHero[i] != null && this.m_vecSelfHero[i].m_iHp > MAX && !this.m_vecSelfHero[i].m_bDead)
            {
                if (this.m_vecSelfHero[i].m_iHp > 0)
                {
                    MAX = this.m_vecSelfHero[i].m_iHp;
                    index = i;
                }
            }
        }
        if (index >= 0 && index < this.m_vecSelfHero.Length)
        {
            return this.m_vecSelfHero[index];
        }
        return null;
    }

    /// <summary>
    /// 自动获取敌人
    /// </summary>
    /// <returns></returns>
    public override BattleHero GetTargetAuto()
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
                    SetUITargetData(this.m_vecTargetHero[this.m_iSelectTargetIndex]);
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
            if (this.m_vecSelfHero[index] != null && !this.m_vecSelfHero[index].m_bDead && this.m_vecSelfHero[index].m_iHp > 0 )
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
            if (this.m_vecSelfHero[index] != null && !this.m_vecSelfHero[index].m_bDead && this.m_vecSelfHero[index].m_iHp > 0 )
            {
                return this.m_vecSelfHero[index];
            }
            index = (index + 1) % HERO_MAX_NUM;
        }

        return null;
    }

    /// <summary>
    /// 选择目标
    /// </summary>
    /// <param name="index"></param>
    public void SelectTarget(int index)
    {
        if (this.m_iSelectTargetIndex == index)
        {
            this.m_iSelectTargetIndex = -1;
            this.m_cHeroTargetSp.transform.localPosition = Vector3.one * 0xFFFFF;
            for (int i = 0; i<this.m_vecHeroAttackProperty.Length ; i++ )
            {
                this.m_vecHeroAttackProperty[i].enabled = false;
            }
        }
        else
        {
            this.m_iSelectTargetIndex = index;
            this.m_cHeroTargetSp.transform.localPosition = this.m_vecUITargetPos[index].transform.localPosition;
            SetUITargetData(this.m_vecTargetHero[index]);

            for (int i = 0; i < this.m_vecSelfHero.Length; i++)
            {
                if (this.m_vecSelfHero[i] != null && !this.m_vecSelfHero[i].m_bDead && this.m_vecSelfHero[i].m_iHp > 0)
                {
                    int isBane = 0;
                    BattleHero self = this.m_vecSelfHero[i];
                    BattleHero target = this.m_vecTargetHero[index];
                    if ((self.m_eNature == Nature.Fire && target.m_eNature == Nature.Wood) || (self.m_eNature == Nature.Wood && target.m_eNature == Nature.Thunder)
                        || (self.m_eNature == Nature.Thunder && target.m_eNature == Nature.Water) || (self.m_eNature == Nature.Water && target.m_eNature == Nature.Fire)
                        || (self.m_eNature == Nature.Bright && target.m_eNature == Nature.Dark) || (self.m_eNature == Nature.Dark && target.m_eNature == Nature.Bright)
                        )
                    {
                        isBane = 1;
                    }

                    if ((target.m_eNature == Nature.Fire && self.m_eNature == Nature.Wood) || (target.m_eNature == Nature.Wood && self.m_eNature == Nature.Thunder)
                        || (target.m_eNature == Nature.Thunder && self.m_eNature == Nature.Water) || (target.m_eNature == Nature.Water && self.m_eNature == Nature.Fire)
                        )
                    {
                        isBane = 2;
                    }

                    if (isBane == 1)
                    {
                        this.m_vecHeroAttackProperty[i].enabled = true;
                        this.m_vecHeroAttackProperty[i].spriteName = "up";
                    }
                    else if (isBane == 2)
                    {
                        this.m_vecHeroAttackProperty[i].enabled = true;
                        this.m_vecHeroAttackProperty[i].spriteName = "down";
                    }
                    else
                    {
                        this.m_vecHeroAttackProperty[i].enabled = false;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 更换目标
    /// </summary>
    public void ChangeSelectTarget()
    {
        int tmp_index = this.m_iSelectTargetIndex;
        for (int i = 0; i < HERO_MAX_NUM; i++)
        {
            if (this.m_vecTargetHero[tmp_index] != null && !this.m_vecTargetHero[tmp_index].m_bDead)
            {
                SelectTarget(tmp_index);
                return;
            }
            tmp_index = (tmp_index + 1) % HERO_MAX_NUM;
        }
    }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        this.m_bLoseShow = true;
        this.m_bMenuShow = true;
        base.Show();

        this.m_eBattleState = BATTLE_STATE.BATTLE_STATE_INIT_BEGIN;

        //ResourceMgr.ClearAsyncLoad();

        GUI_FUNCTION.AYSNCLOADING_SHOW();

        //battle_next_gui
        ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + GUIBattleNext.RES_MAIN);
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

		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_FARM_MESH);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_JINBI_MESH);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_SHUIJING_MESH);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_XIN_MESH);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_SOUL_MESH);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_BOX_MESH);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_SKILL_AVATAR);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_DEFENCE_EFFECT);

		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_DEBUFF_DU_EFFECT);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_DEBUFF_XURUO_EFFECT);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_DEBUFF_MA_EFFECT);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_DEBUFF_POJIA_EFFECT);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_DEBUFF_POREN_EFFECT);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_DEBUFF_FENGYIN_EFFECT);

		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_WIN_EFFECT);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_LOSE_EFFECT);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_CONGRATULATION_EFFECT);

		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_BOSS_WARNING_EFFECT);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_BOX_WARNING_EFFECT);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_BOX_MONSTER_EFFECT);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_BOX_OPEN_EFFECT);

		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_COLLECT_HERO_EFFECT1);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_COLLECT_HERO_EFFECT2);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_COLLECT_HERO_EFFECT3);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_COLLECT_HERO_EFFECT4);

        for (int i = 0; i < 6; i++)
        {
			ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_SKILL_SHOW_BG_EFFECT + (i + 1));
        }

		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + GUI_ITEM_USE_INFO);

        for( int i = 0 ; i<this.m_vecSelfHero.Length ; i++ )
        {
            BattleHeroGenerator.GeneratorHeroAysnc(this.m_vecSelfHero[i]);
        }

        SetLocalPos(Vector3.one*0xFFFFF);
    }

    /// <summary>
    /// 初始化展示
    /// </summary>
    private void InitShow()
    {
        base.Show();

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

		this.m_cResFarm = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_FARM_MESH);
        ResourceMgr.UnloadResource(RES_FARM_MESH);
		this.m_cResJinbi = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_JINBI_MESH);
        ResourceMgr.UnloadResource(RES_JINBI_MESH);
		this.m_cResShuijing = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_SHUIJING_MESH);
        ResourceMgr.UnloadResource(RES_SHUIJING_MESH);
		this.m_cResXin = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_XIN_MESH);
        ResourceMgr.UnloadResource(RES_XIN_MESH);
		this.m_cResSoul = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_SOUL_MESH);
        ResourceMgr.UnloadResource(RES_SOUL_MESH);
		this.m_cResBox = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_BOX_MESH);
        ResourceMgr.UnloadResource(RES_BOX_MESH);
		this.m_cResSkillAvatar = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_SKILL_AVATAR);
        ResourceMgr.UnloadResource(RES_SKILL_AVATAR);
		this.m_cResDefenceEffect = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_DEFENCE_EFFECT);
        ResourceMgr.UnloadResource(RES_DEFENCE_EFFECT);

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

		this.m_cResResultWin = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_WIN_EFFECT);
        ResourceMgr.UnloadResource(RES_WIN_EFFECT);
		this.m_cResResultLose = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_LOSE_EFFECT);
        ResourceMgr.UnloadResource(RES_LOSE_EFFECT);
		this.m_cResResultCongratulation = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_CONGRATULATION_EFFECT);
        ResourceMgr.UnloadResource(RES_CONGRATULATION_EFFECT);

		this.m_cResBossWarningEffect = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_BOSS_WARNING_EFFECT);
        ResourceMgr.UnloadResource(RES_BOSS_WARNING_EFFECT);
		this.m_cResBoxWarningEffect = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_BOX_WARNING_EFFECT);
        ResourceMgr.UnloadResource(RES_BOX_WARNING_EFFECT);
		this.m_cResBoxMonsterEffect = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_BOX_MONSTER_EFFECT);
        ResourceMgr.UnloadResource(RES_BOX_MONSTER_EFFECT);
		this.m_cResBoxOpenEffect = ResourceMgr.LoadAsset(RES_BOX_OPEN_EFFECT) as UnityEngine.Object;
        ResourceMgr.UnloadResource(RES_BOX_OPEN_EFFECT);

		this.m_cResCollectHeroEffect1 = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_COLLECT_HERO_EFFECT1);
        ResourceMgr.UnloadResource(RES_COLLECT_HERO_EFFECT1);
		this.m_cResCollectHeroEffect2 = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_COLLECT_HERO_EFFECT2);
        ResourceMgr.UnloadResource(RES_COLLECT_HERO_EFFECT2);
		this.m_cResCollectHeroEffect3 = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_COLLECT_HERO_EFFECT3);
        ResourceMgr.UnloadResource(RES_COLLECT_HERO_EFFECT3);
		this.m_cResCollectHeroEffect4 = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_COLLECT_HERO_EFFECT4);
        ResourceMgr.UnloadResource(RES_COLLECT_HERO_EFFECT4);

        for (int i = 0; i < 6; i++)
        {
			this.m_vecTexSkillShowBG[i] = (Texture)ResourceMgr.LoadAsset(RES_SKILL_SHOW_BG_EFFECT + (i + 1));
            ResourceMgr.UnloadResource(RES_SKILL_SHOW_BG_EFFECT + (i + 1));
        }

        this.m_cBattleParent = GUI_FINDATION.FIND_GAME_OBJECT(BATTLE_PARENT);
        this.m_cBattleBBPos = GUI_FINDATION.GET_GAME_OBJECT(this.m_cBattleParent, BATTLE_BBPOS);
        this.m_cBattleGoldPos = GUI_FINDATION.GET_GAME_OBJECT(this.m_cBattleParent, BATTLE_GOLD_POS);
        this.m_cBattleFarmPointPos = GUI_FINDATION.GET_GAME_OBJECT(this.m_cBattleParent, BATTLE_FARM_POS);
        this.m_cBattleSoulPointPos = GUI_FINDATION.GET_GAME_OBJECT(this.m_cBattleParent, BATTLE_SOUL_POS);

        this.m_cMenuBtn = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_MENU_BTN);
        GUIComponentEvent guiEvent = this.m_cMenuBtn.AddComponent<GUIComponentEvent>();
        guiEvent.AddIntputDelegate(OnMenu);
        this.m_cBoxGiveUpBtn = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_BOX_GIVEUP_BTN);
        guiEvent = this.m_cBoxGiveUpBtn.AddComponent<GUIComponentEvent>();
        guiEvent.AddIntputDelegate(OnBoxGiveUpBtn);
        this.m_cBlackGround = GUI_FINDATION.GET_GAME_OBJECT(this.m_cBattleParent, GUI_BLACK_GROUND);
        this.m_cBlackGround.transform.localPosition = BLACK_GROUND_END;
        this.m_cTargetName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, GUI_TARGET_NAME);
        this.m_cTargetHPBar = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, GUI_TARGET_HP_BAR);
        this.m_cTargetProperty = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, GUI_TARGET_PROPERTY);
        this.m_cHeroTargetSp = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_HERO_TARGET_SP);
        this.m_cItemBackCollider = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_ITEM_BACK_COLLIDER);

        this.m_vecHeroDefenceIMG = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_HERO_DEFENCE_IMG);
        this.m_vecHeroDefenceIMG.SetActive(false);
        this.m_vecHeroBBIMG = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_HERO_BB_IMG);
        this.m_vecHeroBBIMG.SetActive(false);

        guiEvent = this.m_cItemBackCollider.AddComponent<GUIComponentEvent>();
        guiEvent.AddIntputDelegate(OnItemBackCollider);

        for (int i = 0; i < HERO_MAX_NUM; i++)
        {
            this.m_vecSelfBuf[i] = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, GUI_SELF_BUF + (i + 1) + "/BUF");
            this.m_vecSelfBuf[i].enabled = false;
            this.m_vecTargetBuf[i] = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, GUI_TARGET_BUF + (i + 1) + "/BUF");
            this.m_vecTargetBuf[i].enabled = false;

            this.m_vecTargetPos[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_cBattleParent, BATTLE_TARGET_POS + (i + 1));
            this.m_vecUITargetPos[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_TARGET_POS + (i + 1));
            this.m_vecTargetAttackPos[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_cBattleParent, BATTLE_TARGET_ATTACK_POS + (i + 1));
            this.m_vecMyselfPos[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_cBattleParent, BATTLE_MYSELF_POS + (i + 1));
            this.m_vecUIMyselfPos[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_SELF_POS + (i + 1));
            this.m_vecMyselfAttackPos[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_cBattleParent, BATTLE_MYSQL_ATTACK_POS + (i + 1));

            this.m_vecBoxGuide[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_vecUITargetPos[i], GUI_BOX_GUIDE);
            this.m_vecBoxGuide[i].SetActive(false);

            this.m_vecHeroBtn[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_HERO_BTN + (i + 1));
            GUIComponentEvent ce = this.m_vecHeroBtn[i].AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnHeroBtn, i);
            this.m_vecHeroBlack[i] = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_vecHeroBtn[i], GUI_HERO_BLACK);
            this.m_vecHeroProperty[i] = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_vecHeroBtn[i], GUI_HERO_PROPERTY);
            this.m_vecHeroHP[i] = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_vecHeroBtn[i], GUI_HERO_HP);
            this.m_vecHeroHPBar[i] = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_vecHeroBtn[i], GUI_HERO_HP_BAR);
            this.m_vecHeroEnergyBar[i] = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_vecHeroBtn[i], GUI_HERO_ENERGY);
            this.m_vecHeroName[i] = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_vecHeroBtn[i], GUI_HERO_NAME);
            this.m_vecHeroAttackProperty[i] = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_vecHeroBtn[i], GUI_HERO_ATTACK_PROPERTY);
            this.m_vecHeroAvator[i] = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_vecHeroBtn[i], GUI_HERO_AVATOR);
            this.m_vecHeroDie[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_vecHeroBtn[i], GUI_HERO_DIE);
            this.m_vecHeroEmpty[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_vecHeroBtn[i], GUI_HERO_EMPTY);
            this.m_vecHeroBBANI_IMG[i] = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_vecHeroBtn[i], GUI_HERO_BB_ANI_IMG);
            this.m_vecHeroBBANI_IMG1[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_vecHeroBtn[i], GUI_HERO_BB_ANI_IMG1);
            this.m_vecHeroBBANI_IMG2[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_vecHeroBtn[i], GUI_HERO_BB_ANI_IMG2);
            this.m_vecHeroTargetBtn[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_HERO_TARGET_BTN + (i + 1) + "/Btn");
            ce = this.m_vecHeroTargetBtn[i].AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnHeroTargetBtn, i);
            this.m_vecHeroHPParent[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_vecHeroBtn[i], GUI_HERO_HP_PARENT);
            this.m_vecHeroEnergyParent[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_vecHeroBtn[i], GUI_HERO_ENERGY_PARENT);
            this.m_vecHeroHPIMG[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_vecHeroBtn[i], GUI_HERO_HP_IMG);
            this.m_vecHeroEnergyIMG[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_vecHeroBtn[i], GUI_HERO_ENERGY_IMG);
            this.m_vecItemGuard[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_vecHeroBtn[i], GUI_HERO_ITEM_GUARD);
            this.m_vecHeroFrame[i] = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_vecHeroBtn[i], GUI_HERO_FRAME);
            this.m_vecHeroFrameBack[i] = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_vecHeroBtn[i], GUI_HERO_FRAME_BACK);

            this.m_cTopPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_TOP_PANEL);
            this.m_cTopPanel.SetActive(true);
        }

        for (int i = 0; i < ITEM_MAX_NUM; i++)
        {
            this.m_vecItemBtn[i] = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_ITEM_BTN + (i + 1));
            guiEvent = this.m_vecItemBtn[i].AddComponent<GUIComponentEvent>();
            guiEvent.AddIntputDelegate(OnItem, i);
            this.m_vecItemIcon[i] = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_vecItemBtn[i], GUI_ITEM_ICON);
            this.m_vecItemName[i] = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_vecItemBtn[i], GUI_ITEM_NAME);
            this.m_vecItemNum[i] = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_vecItemBtn[i], GUI_ITEM_NUM);
        }

        //物品使用时候的遮罩
        this.m_cSprBlack = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, GUI_ITEM_SPRITE_BACK);
        this.m_cSprBlack.enabled = false;

        //物品使用时候的返回按钮
        this.m_cBtnItemBack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_ITEM_BTN_CANCEL);
        this.m_cBtnItemBack.AddComponent<GUIComponentEvent>().AddIntputDelegate(Item_Back);
        this.m_cBtnItemBack.SetActive(false);

        //宝箱放弃按钮和物品遮罩
        this.m_cBoxGiveUpBtn.SetActive(false);
        this.m_cItemBackCollider.SetActive(false);

        this.m_cGold = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, GUI_GOLD);
        this.m_cFarmPoint = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, GUI_FARMPOINT);
        this.m_cCollect = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, GUI_COLLECT);

		UnityEngine.Object itemshow = (UnityEngine.Object)ResourceMgr.LoadAsset(GUI_ITEM_USE_INFO);
        ResourceMgr.UnloadResource(GUI_ITEM_USE_INFO);
        this.m_cUseItemInfoShow = new ReconceliHouseShowItem(itemshow);
        this.m_cUseItemInfoShow.m_cItem.transform.parent = this.m_cGUIObject.transform;
        this.m_cUseItemInfoShow.m_cItem.transform.localScale = Vector3.one;
        this.m_cUseItemInfoShow.m_cItem.transform.localPosition = new Vector3(0, 305, 0);
        this.m_cUseItemInfoShow.m_cItem.SetActive(false);

        this.m_bBoss = false;

        CameraManager.GetInstance().ShowBattle3DCamera();
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Hiden();

        this.m_lstSoul.Clear();
        this.m_lstItem.Clear();
        this.m_lstItemNum.Clear();
        this.m_iGoldNum = 0;
        this.m_iGoldDummyNum = 0;
        this.m_iFarmNum = 0;
        this.m_iFarmDummyNum = 0;
        this.m_iUIState = UI_STATE_NONE;
        this.m_iBattleResult = BATTLE_WIN_NONE;
        this.m_cGUIMgr.SetCurGUIID(this.m_iID);
        GUIBattleLose losegui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BATTLE_LOSE) as GUIBattleLose;
        losegui.CleanReliveNum();

        //统计
        this.m_iTotalShuijingNum = 0;
        this.m_iTotalXinNum = 0;
        this.m_iTotalSparkNum = 0;
        this.m_iTotalSkillNum = 0;
        this.m_iRecordMaxShuijingNum = 0;
        this.m_iRecordMaxXinNum = 0;
        this.m_iRoundMaxHurt = 0;
        this.m_iRoundMaxSparkNum = 0;
        this.m_iTotalBoxMonster = 0;

        //场景
		this.m_cScene = GameObject.Instantiate(ResourceMgr.LoadAsset(this.m_strSceneName) as UnityEngine.Object) as GameObject;
        this.m_cScene.transform.parent = this.m_cBattleParent.transform;
        this.m_cScene.transform.localScale = Vector3.one;
        this.m_cScene.transform.localPosition = Vector3.zero;
        if (!GAME_SETTING.s_bENEffectSwitch)
        {
            SwitchSceneEffect(false);
        }

        //人物
        for (int i = 0; i < this.m_vecSelfHero.Length; i++)
        {
            if (this.m_vecSelfHero[i] != null && this.m_vecSelfHero[i].GetGfxObject() != null)
                this.m_vecSelfHero[i].GetGfxObject().Destory();
            BattleHeroGenerator.GeneratorHeroGfxAysnc(this.m_vecSelfHero[i], this, this.m_cBattleParent, this.m_vecUIMyselfPos[i], this.m_vecMyselfPos[i], this.m_vecMyselfAttackPos[i],true);
        }

        //设置UI
        for (int i = 0; i < this.m_vecSelfHero.Length ; i++)
        {
            if (this.m_vecSelfHero[i] == null)
            {
                SetUIHeroActive(i, false);
            }
            else
            {
                SetUIHeroActive(i, true);
                SetUIHeroData(this.m_vecSelfHero[i]);
            }
        }

        //物品
        for (int i = 0; i < this.m_vecItem.Length; i++)
        {
            Item item = this.m_vecItem[i];
            if (item != null)
            {
                GUI_FUNCTION.SET_ITEMM(this.m_vecItemIcon[i], item.m_strSprName);
                this.m_vecItemName[i].text = item.m_strShortName;
                this.m_vecItemNum[i].text = "X" + item.m_iNum;
            }
            else
            {
                this.m_vecItemIcon[i].atlas = null;
                this.m_vecItemName[i].text = "";
                this.m_vecItemNum[i].text = "";
            }
        }

        GC.Collect();

        SetLocalPos(Vector3.zero);
    }


    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        base.Hiden();

        CameraManager.GetInstance().HidenBattle3DCamera();
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
		ResourceMgr.UnloadUnusedResources();

        //if( this.m_cGUIObject != null )
        //    GameObject.DestroyImmediate(this.m_cGUIObject);

        //ResourceMgr.ClearAsyncLoad();
        //SetLocalPos(Vector3.one * 0xFFFF);

        //this.m_cResHurtNum = null;
        //this.m_cResRecoverNum = null;

        //this.m_cResSpark = null;
        //this.m_cResCritical = null;

        //this.m_cResFarm = null;
        //this.m_cResJinbi = null;
        //this.m_cResShuijing = null;
        //this.m_cResXin = null;
        //this.m_cResSoul = null;
        //this.m_cResBox = null;
        //this.m_cResSkillAvatar = null;
        //this.m_cResDefenceEffect = null;

        //this.m_cResDebuffDu = null;
        //this.m_cResDebuffXuruo = null;
        //this.m_cResDebuffMa = null;
        //this.m_cResDebuffPojia = null;
        //this.m_cResDebuffPoren = null;
        //this.m_cResDebuffFengyin = null;

        //this.m_cResResultWin = null;
        //this.m_cResResultLose = null;
        //this.m_cResResultCongratulation = null;

        //this.m_cResBossWarningEffect = null;
        //this.m_cResBoxWarningEffect = null;

        //for (int i = 0; i < 6; i++)
        //{
        //    this.m_vecTexSkillShowBG[i] = null;
        //}

        ////销毁物体
        //if (this.m_cScene != null)
        //{
        //    GameObject.DestroyImmediate(this.m_cScene);
        //}
        //this.m_cScene = null;

        //for (int i = 0; i < this.m_vecItem.Length; i++)
        //    this.m_vecItem[i] = null;
        //for (int i = 0; i < this.m_vecSelfHero.Length; i++)
        //{
        //    if (this.m_vecSelfHero[i] != null)
        //    {
        //        this.m_vecSelfHero[i].Destory();
        //    }
        //    this.m_vecSelfHero[i] = null;
        //}

        //for (int i = 0; i < this.m_vecTargetHero.Length; i++)
        //{
        //    if (this.m_vecTargetHero[i] != null)
        //    {
        //        this.m_vecTargetHero[i].Destory();
        //    }
        //    this.m_vecTargetHero[i] = null;
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
        //this.m_cCollectItem.Destory();
        //this.m_cCollectItemFarm.Destory();
        //this.m_cCollectItemJinbi.Destory();
        //this.m_cCollectItemShuijing.Destory();
        //this.m_cCollectItemXin.Destory();
        //this.m_cCollectItemSoul.Destory();

        //for (int i = 0; i < this.m_lstBox.Length; i++)
        //{
        //    if (this.m_lstBox[i] != null)
        //    {
        //        this.m_lstBox[i].Destory();
        //    }
        //}

        //Array.Clear(this.m_lstBox, 0, this.m_lstBox.Length);
        //Array.Clear(this.m_lstBoxOpen, 0, this.m_lstBoxOpen.Length);
        //Array.Clear(this.m_lstBoxIsMonster, 0, this.m_lstBoxIsMonster.Length);

        //if (this.m_cResultEffect != null)
        //{
        //    GameObject.Destroy(this.m_cResultEffect);
        //}
        //this.m_cResultEffect = null;
        //if (this.m_cWarningEffect != null)
        //{
        //    GameObject.Destroy(this.m_cWarningEffect);
        //}
        //this.m_cWarningEffect = null;

        Destory();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        //资源销毁
        this.m_cResBattleFont1 = null;
        this.m_cResBattleFont2 = null;
        this.m_cResBattleFont3 = null;
        this.m_cResBattleFont4 = null;

        this.m_cResHurtNum = null;
        this.m_cResRecoverNum = null;

        this.m_cResSpark = null;
        this.m_cResCritical = null;

        this.m_cResFarm = null;
        this.m_cResJinbi = null;
        this.m_cResShuijing = null;
        this.m_cResXin = null;
        this.m_cResSoul = null;
        this.m_cResBox = null;
        this.m_cResSkillAvatar = null;
        this.m_cResDefenceEffect = null;

        this.m_cResDebuffDu = null;
        this.m_cResDebuffXuruo = null;
        this.m_cResDebuffMa = null;
        this.m_cResDebuffPojia = null;
        this.m_cResDebuffPoren = null;
        this.m_cResDebuffFengyin = null;

        this.m_cResResultWin = null;
        this.m_cResResultLose = null;
        this.m_cResResultCongratulation = null;

        this.m_cResBossWarningEffect = null;
        this.m_cResBoxWarningEffect = null;
        this.m_cResBoxMonsterEffect = null;
        this.m_cResBoxOpenEffect = null;

        this.m_cResCollectHeroEffect1 = null;
        this.m_cResCollectHeroEffect2 = null;
        this.m_cResCollectHeroEffect3 = null;
        this.m_cResCollectHeroEffect4 = null;

        for (int i = 0; i < 6; i++)
        {
            this.m_vecTexSkillShowBG[i] = null;
        }

        //引用销毁
        this.m_cBattleParent = null;
        this.m_cBattleBBPos = null;
        this.m_cBattleGoldPos = null;
        this.m_cBattleFarmPointPos = null;
        this.m_cBattleSoulPointPos = null;
        Array.Clear( this.m_vecTargetPos,0,this.m_vecTargetPos.Length);    //目标站点
        Array.Clear( this.m_vecUITargetPos,0,this.m_vecUITargetPos.Length);  //目标UI站立点
        Array.Clear( this.m_vecTargetAttackPos,0,this.m_vecTargetAttackPos.Length);  //目标攻击点
        Array.Clear( this.m_vecMyselfPos,0,this.m_vecMyselfPos.Length);    //自身站点
        Array.Clear( this.m_vecUIMyselfPos,0,this.m_vecUIMyselfPos.Length);   //自身UI站立点
        Array.Clear( this.m_vecMyselfAttackPos,0,this.m_vecMyselfAttackPos.Length);  //自身攻击点

        this.m_cBlackGround = null;
        Array.Clear( this.m_vecHeroBtn,0,this.m_vecHeroBtn.Length);  //英雄按钮
        Array.Clear( this.m_vecHeroBlack , 0 , this.m_vecHeroBlack.Length);    //黑屏遮罩
        Array.Clear( this.m_vecHeroProperty , 0 ,this.m_vecHeroProperty.Length);   //英雄属性
        Array.Clear( this.m_vecHeroHP , 0 , this.m_vecHeroHP.Length);  //英雄HP
        Array.Clear( this.m_vecHeroHPBar , 0 , this.m_vecHeroHPBar.Length);  //英雄HP条
        Array.Clear( this.m_vecHeroEnergyBar , 0 , this.m_vecHeroEnergyBar.Length);  //英雄能量条
        Array.Clear( this.m_vecHeroName , 0 , this.m_vecHeroName.Length);    //英雄名字
        Array.Clear(this.m_vecHeroAttackProperty, 0, this.m_vecHeroAttackProperty.Length);  //攻击属性判定
        Array.Clear( this.m_vecHeroAvator , 0 , this.m_vecHeroAvator.Length);    //英雄头像
        Array.Clear( this.m_vecHeroDie , 0 , this.m_vecHeroDie.Length);  //英雄死亡
        Array.Clear( this.m_vecHeroEmpty , 0 , this.m_vecHeroEmpty.Length);    //空缺
        Array.Clear( this.m_vecHeroHPParent , 0 , this.m_vecHeroHPParent.Length); //HP父节点
        Array.Clear( this.m_vecHeroEnergyParent , 0 , this.m_vecHeroEnergyParent.Length); //能量父节点
        Array.Clear( this.m_vecHeroHPIMG , 0 , this.m_vecHeroHPIMG.Length);    //HP图片
        Array.Clear( this.m_vecHeroEnergyIMG , 0 , this.m_vecHeroEnergyIMG.Length);    //能量图片
        Array.Clear( this.m_vecHeroFrame , 0 ,this.m_vecHeroFrame.Length);  //英雄框
        Array.Clear( this.m_vecHeroFrameBack , 0 , this.m_vecHeroFrameBack.Length) ;  //底框
        Array.Clear( this.m_vecHeroBBANI_IMG , 0 , this.m_vecHeroBBANI_IMG.Length);   //BB动画图
        Array.Clear( this.m_vecHeroBBANI_IMG1 , 0 , this.m_vecHeroBBANI_IMG1.Length);
        Array.Clear( this.m_vecHeroBBANI_IMG2 , 0 , this.m_vecHeroBBANI_IMG2.Length);
        Array.Clear( this.m_vecItemGuard , 0 , this.m_vecItemGuard.Length);
        Array.Clear( this.m_vecHeroTargetBtn , 0 , this.m_vecHeroTargetBtn.Length);
        Array.Clear( this.m_vecBoxGuide , 0 , this.m_vecBoxGuide.Length);
        this.m_cHeroTargetSp = null;

        this.m_vecHeroBBIMG = null;
        this.m_vecHeroDefenceIMG = null;
        Array.Clear(m_vecTexSkillShowBG , 0 , this.m_vecTexSkillShowBG.Length);
        this.m_cItemBackCollider = null;
        Array.Clear( this.m_vecItemBtn , 0 , this.m_vecItemBtn.Length);
        Array.Clear( this.m_vecItemIcon , 0 , this.m_vecItemIcon.Length);
        Array.Clear( this.m_vecItemName , 0 , this.m_vecItemName.Length);
        Array.Clear( this.m_vecItemNum , 0 , this.m_vecItemNum.Length);
        this.m_cSprBlack = null;
        this.m_cBtnItemBack = null;

        this.m_cMenuBtn = null;
        this.m_cBoxGiveUpBtn = null;
        this.m_cGold = null;
        this.m_cFarmPoint = null;
        this.m_cCollect = null;

        this.m_cTargetName = null;
        this.m_cTargetHPBar = null;

        Array.Clear(this.m_vecSelfBuf , 0 , this.m_vecSelfBuf.Length);
        Array.Clear(this.m_vecTargetBuf , 0 , this.m_vecTargetBuf.Length);

        this.m_cUseItemInfoShow = null;

        //销毁物体
        if (this.m_cScene != null)
        {
            GameObject.DestroyImmediate(this.m_cScene);
        }
        this.m_cScene = null;

        for (int i = 0; i < this.m_vecItem.Length; i++)
            this.m_vecItem[i] = null;
        for (int i = 0; i < this.m_vecSelfHero.Length; i++)
        {
            if (this.m_vecSelfHero[i] != null)
            {
                this.m_vecSelfHero[i].Destory();
            }
            this.m_vecSelfHero[i] = null;
        }

        for (int i = 0; i < this.m_vecTargetHero.Length; i++)
        {
            if (this.m_vecTargetHero[i] != null)
            {
                this.m_vecTargetHero[i].Destory();
            }
            this.m_vecTargetHero[i] = null;
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
        this.m_cCollectItem.Destory();
        this.m_cCollectItemFarm.Destory();
        this.m_cCollectItemJinbi.Destory();
        this.m_cCollectItemShuijing.Destory();
        this.m_cCollectItemXin.Destory();
        this.m_cCollectItemSoul.Destory();

        for (int i = 0; i < this.m_lstBox.Length; i++)
        {
            if (this.m_lstBox[i] != null)
            {
                this.m_lstBox[i].Destory();
                this.m_lstBox[i] = null;
            }
        }

        Array.Clear(this.m_lstBox, 0, this.m_lstBox.Length);
        Array.Clear(this.m_lstBoxOpen, 0, this.m_lstBoxOpen.Length);
        Array.Clear(this.m_lstBoxIsMonster, 0, this.m_lstBoxIsMonster.Length);

        if (this.m_cResultEffect != null)
        {
            GameObject.Destroy(this.m_cResultEffect);
        }
        this.m_cResultEffect = null;
        if (this.m_cWarningEffect != null)
        {
            GameObject.Destroy(this.m_cWarningEffect);
        }
        this.m_cWarningEffect = null;

        base.Destory();
    }
    
    /// <summary>
    /// 隐藏顶部面板
    /// </summary>
    public void HidenTopPanel()
    {
        this.m_cTopPanel.SetActive(false);
    }

    /// <summary>
    /// 显示顶部面板
    /// </summary>
    public void ShowTopPanel()
    {
        this.m_cTopPanel.SetActive(true);
    }

    /// <summary>
    /// 设置队长索引
    /// </summary>
    /// <param name="index"></param>
    public void SetLeaderIndex(int index)
    {
        this.m_iLeaderIndex = index;
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
    /// 设置队友队长技能
    /// </summary>
    /// <param name="leaderSkill"></param>
    public void SetFriendLeaderSkill(LeaderSkillTable leaderSkill)
    {
        this.m_cFriendLaderSkill = leaderSkill;
    }

    /// <summary>
    /// 获取敌方队长技能
    /// </summary>
    /// <returns></returns>
    public override LeaderSkillTable GetTargetLeaderSkill()
    {
        return null;
    }

    /// <summary>
    /// 获取队友队长技能
    /// </summary>
    /// <returns></returns>
    public override LeaderSkillTable GetFriendLeaderSkill()
    {
        return this.m_cFriendLaderSkill;
    }

    /// <summary>
    /// 设置物品
    /// </summary>
    /// <param name="vecItem"></param>
    public void SetItem(Item[] vecItem)
    {
        this.m_vecItem = vecItem;
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
                Item item = null;
                if (i != HERO_MAX_NUM - 1)
                {
                    item = Role.role.GetItemProperty().GetItem(heros[i].m_iEquipID);
                }
                else
                {
                    if (heros[i].m_iEquipID > 0)
                    {
                        item = new Item(heros[i].m_iEquipID);
                    }
                }
                this.m_vecSelfHero[i] = BattleHeroGenerator.Generator(i, true, heros[i], this.m_cSelfLeaderSkill, this.m_cFriendLaderSkill, item);
            }
        }
    }

    /// <summary>
    /// 设置对手英雄数据
    /// </summary>
    /// <param name="heros"></param>
    /// <param name="monsters"></param>
    public void SetBattleTargetHero(MonsterTable[] monsters)
    {
        for (int i = 0; i < HERO_MAX_NUM; i++)
        {
            if (this.m_vecTargetHero[i] != null)
            {
                this.m_vecTargetHero[i].Destory();
                this.m_vecTargetHero[i] = null;
            }

            //this.m_vecTargetHero[i] = BattleHeroGenerator.Generator(this.m_cBattleParent, i, this.m_vecUITargetPos[i], this.m_vecTargetPos[i], this.m_vecTargetAttackPos[i], monsters[i], this, this.m_eFavType);
            this.m_vecTargetHero[i] = BattleHeroGenerator.Generator(i , this , monsters[i], this.m_eFavType);
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
    /// 设置英雄空位
    /// </summary>
    /// <param name="index"></param>
    private void SetUIHeroActive(int index, bool active)
    {
        this.m_vecHeroBlack[index].enabled = !active;
        this.m_vecHeroProperty[index].enabled = active;
        this.m_vecHeroHP[index].enabled = active;
        this.m_vecHeroHPBar[index].enabled = active;
        this.m_vecHeroEnergyBar[index].enabled = active;
        this.m_vecHeroName[index].enabled = active;
        this.m_vecHeroAttackProperty[index].enabled = false;
        this.m_vecHeroAvator[index].enabled = active;
        this.m_vecHeroDie[index].SetActive(active);
        this.m_vecHeroEmpty[index].SetActive(!active);
        this.m_vecHeroHPParent[index].SetActive(active);
        this.m_vecHeroEnergyParent[index].SetActive(active);
        this.m_vecHeroHPIMG[index].SetActive(active);
        this.m_vecHeroEnergyIMG[index].SetActive(active);
        if (active)
        {
            this.m_vecHeroFrame[index].spriteName = "Frame";
            this.m_vecHeroFrameBack[index].spriteName = "BG_green";
        }
        else
        {
            this.m_vecHeroFrame[index].spriteName = "Ash-frame";
            this.m_vecHeroFrameBack[index].spriteName = "bg_Ash-frame";
        }

        this.m_vecHeroBBANI_IMG[index].enabled = false;
        this.m_vecHeroBBANI_IMG1[index].SetActive(false);
        this.m_vecHeroBBANI_IMG2[index].SetActive(false);
        this.m_vecItemGuard[index].SetActive(false);
    }

    /// <summary>
    /// 设置英雄数据
    /// </summary>
    /// <param name="hero"></param>
    public void SetUIHeroData(BattleHero hero)
    {
        if (hero == null)
        {
            return;
        }

        if (hero.m_bSelf)
        {
            GUI_FUNCTION.SET_NATURES(this.m_vecHeroProperty[hero.m_iIndex], hero.m_eNature);

            this.m_vecHeroName[hero.m_iIndex].text = "" + hero.m_strName;
            this.m_vecHeroHP[hero.m_iIndex].text = "" + hero.m_iHp + "/" + (int)(hero.m_cMaxHP.GetFinalData());
            this.m_vecHeroHPBar[hero.m_iIndex].transform.localScale = new Vector3(hero.m_iHp * 1f / hero.m_cMaxHP.GetFinalData(), 1, 1);
            //GUI_FUNCTION.SET_AVATORM(this.m_vecHeroAvator[hero.m_iIndex], hero.m_iSpIndex, hero.m_strAvatorM);
            GUI_FUNCTION.SET_AVATORM(this.m_vecHeroAvator[hero.m_iIndex], hero.m_strAvatorM);

            if( hero.m_iBBMaxHP <= 0 )
                this.m_vecHeroEnergyBar[hero.m_iIndex].transform.localScale = new Vector3(0, 1, 1);
            else
                this.m_vecHeroEnergyBar[hero.m_iIndex].transform.localScale = new Vector3(hero.m_fBBHP * 1f / hero.m_iBBMaxHP, 1, 1);

            if (hero.m_iHp <= 0)
            {
                this.m_vecHeroDie[hero.m_iIndex].SetActive(true);
                this.m_vecHeroFrame[hero.m_iIndex].spriteName = "Ash-frame";
                this.m_vecHeroFrameBack[hero.m_iIndex].spriteName = "bg_Ash-frame";
            }
            else
            {
                this.m_vecHeroDie[hero.m_iIndex].SetActive(false);
                this.m_vecHeroFrame[hero.m_iIndex].spriteName = "Frame";
                switch (hero.m_eNature)
                {
                    case Nature.Bright:
                        this.m_vecHeroFrameBack[hero.m_iIndex].spriteName = "BG_white";
                        break;
                    case Nature.Dark:
                        this.m_vecHeroFrameBack[hero.m_iIndex].spriteName = "BG_purple";
                        break;
                    case Nature.Fire:
                        this.m_vecHeroFrameBack[hero.m_iIndex].spriteName = "BG_red";
                        break;
                    case Nature.Thunder:
                        this.m_vecHeroFrameBack[hero.m_iIndex].spriteName = "BG_yellow";
                        break;
                    case Nature.Water:
                        this.m_vecHeroFrameBack[hero.m_iIndex].spriteName = "BG_blue";
                        break;
                    case Nature.Wood:
                        this.m_vecHeroFrameBack[hero.m_iIndex].spriteName = "BG_green";
                        break;
                }
            }

            if (hero.m_iAttackNum <= 0 || hero.m_iHp <= 0)
            {
                this.m_vecHeroBlack[hero.m_iIndex].enabled = true;
            }
            else
            {
                this.m_vecHeroBlack[hero.m_iIndex].enabled = false;
            }

            if ( hero.m_iBBMaxHP > 0 && hero.m_fBBHP >= hero.m_iBBMaxHP)
            {
                this.m_vecHeroBBANI_IMG[hero.m_iIndex].enabled = true;
                this.m_vecHeroBBANI_IMG1[hero.m_iIndex].SetActive(true);
                this.m_vecHeroBBANI_IMG2[hero.m_iIndex].SetActive(true);
            }
            else
            {
                this.m_vecHeroBBANI_IMG[hero.m_iIndex].enabled = false;
                this.m_vecHeroBBANI_IMG1[hero.m_iIndex].SetActive(false);
                this.m_vecHeroBBANI_IMG2[hero.m_iIndex].SetActive(false);
            }
        }

    }

    /// <summary>
    /// 设置英雄Hp
    /// </summary>
    /// <param name="hero"></param>
    public override void SetUIHeroHP(BattleHero hero)
    {
        if (hero != null && hero.m_bSelf)
        {
            this.m_vecHeroHP[hero.m_iIndex].text = "" + hero.m_iHp + "/" + (int)hero.m_cMaxHP.GetFinalData();
            this.m_vecHeroHPBar[hero.m_iIndex].transform.localScale = new Vector3(hero.m_iHp * 1f / hero.m_cMaxHP.GetFinalData(), 1, 1);
            if (hero.m_iHp <= 0)
            {
                if (!this.m_vecHeroDie[hero.m_iIndex].activeSelf)
                {
                    this.m_vecHeroDie[hero.m_iIndex].SetActive(true);
                    this.m_vecHeroFrame[hero.m_iIndex].spriteName = "Ash-frame";
                    this.m_vecHeroFrameBack[hero.m_iIndex].spriteName = "bg_Ash-frame";
                    this.m_vecHeroBBANI_IMG[hero.m_iIndex].enabled = false;
                    this.m_vecHeroBBANI_IMG1[hero.m_iIndex].SetActive(false);
                    this.m_vecHeroBBANI_IMG2[hero.m_iIndex].SetActive(false);
                }
            }
            else
            {
                if (this.m_vecHeroDie[hero.m_iIndex].activeSelf)
                {
                    this.m_vecHeroDie[hero.m_iIndex].SetActive(false);
                    this.m_vecHeroFrame[hero.m_iIndex].spriteName = "Frame";
                    switch (hero.m_eNature)
                    {
                        case Nature.Bright:
                            this.m_vecHeroFrameBack[hero.m_iIndex].spriteName = "BG_white";
                            break;
                        case Nature.Dark:
                            this.m_vecHeroFrameBack[hero.m_iIndex].spriteName = "BG_purple";
                            break;
                        case Nature.Fire:
                            this.m_vecHeroFrameBack[hero.m_iIndex].spriteName = "BG_red";
                            break;
                        case Nature.Thunder:
                            this.m_vecHeroFrameBack[hero.m_iIndex].spriteName = "BG_yellow";
                            break;
                        case Nature.Water:
                            this.m_vecHeroFrameBack[hero.m_iIndex].spriteName = "BG_blue";
                            break;
                        case Nature.Wood:
                            this.m_vecHeroFrameBack[hero.m_iIndex].spriteName = "BG_green";
                            break;
                    }
                    //this.m_vecHeroBBANI_IMG[hero.m_iIndex].enabled = true;
                    //this.m_vecHeroBBANI_IMG1[hero.m_iIndex].SetActive(true);
                    //this.m_vecHeroBBANI_IMG2[hero.m_iIndex].SetActive(true);
                }
            }
        }
    }

    /// <summary>
    /// 设置UI英雄隐藏
    /// </summary>
    public void SetUIHeroAllHiden()
    {
        for (int i = 0; i < this.m_vecSelfHero.Length; i++)
        {
            this.m_vecHeroBlack[i].enabled = true;
            this.m_vecHeroFrame[i].spriteName = "bg_brown-frame";
        }
        this.m_fPressBtnStartTime = 0;
    }

    /// <summary>
    /// 设置英雄是否可攻击
    /// </summary>
    /// <param name="hero"></param>
    public override void SetUIHeroAttackNum(BattleHero hero)
    {
        if (hero != null && hero.m_bSelf)
        {
            if (hero.m_iAttackNum <= 0 || hero.m_iHp <= 0 || hero.BUFExist(BATTLE_BUF.MA))
            {
                if (!this.m_vecHeroBlack[hero.m_iIndex].enabled)
                {
                    this.m_vecHeroBlack[hero.m_iIndex].enabled = true;
                    this.m_vecHeroFrame[hero.m_iIndex].spriteName = "bg_brown-frame";
                }
            }
            else
            {
                if (this.m_vecHeroBlack[hero.m_iIndex].enabled)
                {
                    this.m_vecHeroBlack[hero.m_iIndex].enabled = false;
                    this.m_vecHeroFrame[hero.m_iIndex].spriteName = "Frame";
                }
            }
        }
    }

    /// <summary>
    /// 设置英雄BBHP
    /// </summary>
    /// <param name="hero"></param>
    public override void SetUIHeroBBHP(BattleHero hero)
    {
        if (hero != null && hero.m_bSelf)
        {
            if(hero.m_iBBMaxHP <= 0 )
                this.m_vecHeroEnergyBar[hero.m_iIndex].transform.localScale = new Vector3(0, 1, 1);
            else
                this.m_vecHeroEnergyBar[hero.m_iIndex].transform.localScale = new Vector3(hero.m_fBBHP * 1f / hero.m_iBBMaxHP, 1, 1);

            if (hero.m_iBBMaxHP > 0 && hero.m_fBBHP >= hero.m_iBBMaxHP)
            {
                if (!this.m_vecHeroBBANI_IMG[hero.m_iIndex].enabled)
                {
                    this.m_vecHeroBBANI_IMG[hero.m_iIndex].enabled = true;
                    this.m_vecHeroBBANI_IMG1[hero.m_iIndex].SetActive(true);
                    this.m_vecHeroBBANI_IMG2[hero.m_iIndex].SetActive(true);
                }
            }
            else
            {
                if (this.m_vecHeroBBANI_IMG[hero.m_iIndex].enabled)
                {
                    this.m_vecHeroBBANI_IMG[hero.m_iIndex].enabled = false;
                    this.m_vecHeroBBANI_IMG1[hero.m_iIndex].SetActive(false);
                    this.m_vecHeroBBANI_IMG2[hero.m_iIndex].SetActive(false);
                }
            }
        }
    }

    /// <summary>
    /// 设置数据
    /// </summary>
    public void SetGlobalData()
    {
        this.m_cGold.text = "X" + this.m_iGoldNum;
        this.m_cFarmPoint.text = "X" + this.m_iFarmNum;
        this.m_cCollect.text = "X" + this.m_lstSoul.Count;
    }

    /// <summary>
    /// 设置目标数据
    /// </summary>
    /// <param name="hero"></param>
    public override void SetUITargetData(BattleHero hero)
    {
        if (hero == null || hero.m_bSelf)
        {
            return;
        }

        this.m_cTargetName.text = hero.m_strName;
        this.m_cTargetHPBar.transform.localScale = new Vector3(hero.m_iHp * 1f / hero.m_cMaxHP.GetFinalData(), 1, 1);
        GUI_FUNCTION.SET_NATUREL(this.m_cTargetProperty, hero.m_eNature);
    }

    /// <summary>
    /// 根据索引重置物品数量
    /// </summary>
    /// <param name="index"></param>
    public void SetUIItemNum(int index)
    {
        if (index >= ITEM_MAX_NUM)
            return;
        Item item = this.m_vecItem[index];
        if (item == null)
            return;

        this.m_vecItemNum[index].text = "X" + item.m_iNum;

        //if (item.m_iNum <= 0)
        ////{
        ////    this.m_vecItemIcon[index].atlas = null;
        ////    this.m_vecItemName[index].text = "";
        ////    this.m_vecItemNum[index].text = "";
        ////}
        ////else
        //{
        //    this.m_vecItemNum[index].text = "X" + item.m_iNum;
        //}
    }

    /// <summary>
    /// 隐藏目标BUF标记
    /// </summary>
    /// <param name="index"></param>
    public override void HidenTargetBUF(int index)
    {
        if (this.m_vecTargetBuf[index] != null)
        {
            this.m_vecTargetBuf[index].enabled = false;
        }
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
        obj.transform.localPosition = pos + GAME_FUNCTION.RANDOM_IN_SPHERE()*50f;

        GameObject objNum = GUI_FUNCTION.GENERATOR_NUM(num, (this.m_cResBattleFont2 as GameObject).GetComponent<UIFont>());
        objNum.transform.parent = obj.transform;
        objNum.transform.localPosition = Vector3.zero;
        objNum.transform.localScale = Vector3.one*1f;

        this.m_lstHurtTxt.Add(obj);
        this.m_lstHurtTxtTime.Add(GAME_TIME.TIME_FIXED());

        //统计
        if (!target.m_bSelf)
        {
            this.m_iRoundHurtSum += num;
            if (this.m_iRoundHurtSum > this.m_iRoundMaxHurt)
                this.m_iRoundMaxHurt = this.m_iRoundHurtSum;
        }
    }

    /// <summary>
    /// 克制时的伤害数字
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="num"></param>
    public override void GeneratorHurtBaneNum(Vector3 pos, int num , BattleHero target )
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
            this.m_iRoundHurtSum += num;
            if (this.m_iRoundHurtSum > this.m_iRoundMaxHurt)
                this.m_iRoundMaxHurt = this.m_iRoundHurtSum;
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
            this.m_iRoundHurtSum += num;
            if (this.m_iRoundHurtSum > this.m_iRoundMaxHurt)
                this.m_iRoundMaxHurt = this.m_iRoundHurtSum;
        }
    }

    /// <summary>
    /// 生成回复数值
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="num"></param>
    public override void GeneratorRecoverNum(Vector3 pos, int num , BattleHero target )
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
        obj.transform.localPosition = pos + GAME_FUNCTION.RANDOM_IN_SPHERE() * 0.5f;

        this.m_lstSpark.Add(obj);
        this.m_lstSparkStartTime.Add(GAME_TIME.TIME_FIXED());

        //统计
        if (!target.m_bSelf)
        {
            this.m_iTotalSparkNum++;
            this.m_iRoundSparkNum++;
            if (this.m_iRoundSparkNum > this.m_iRoundMaxSparkNum)
                this.m_iRoundMaxSparkNum = this.m_iRoundSparkNum;
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
    /// 生成物品
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="itemTableID"></param>
    public override void GeneratorItem(Vector3 pos, int itemTableID)
    {
        Vector2 bottom2d = GAME_FUNCTION.RANDOM_IN_CIRCLE();
        Vector3 bottom = new Vector3(bottom2d.x + pos.x, 0.4f, bottom2d.y + pos.z);
        float heigh = GAME_FUNCTION.RANDOM(0.7f, 2.5f);

        GameObject obj = new GameObject("BATTLE_Item");
        obj.transform.parent = this.m_cBattleParent.transform;
        obj.transform.localScale = Vector3.one*0.75f;
        obj.transform.localPosition = pos;
        ItemTable table = ItemTableManager.GetInstance().GetItem(itemTableID);
        if (table != null)
        {
            GameObject itemObj = GUI_FUNCTION.GENERATOR_ITEM_OBJECT(table.SpiritName , GAME_DEFINE.U3D_OBJECT_LAYER_MODEL);
            itemObj.transform.parent = obj.transform;
            itemObj.transform.localPosition = Vector3.zero;
            itemObj.transform.localScale = Vector3.one * 0.012f;
        }

        bool exist = false;
        for (int i = 0; i < this.m_lstItem.Count; i++)
        {
            if (this.m_lstItem[i] == itemTableID)
            {
                this.m_lstItemNum[i]++;
                exist = true;
                break;
            }
        }
        if (!exist)
        {
            this.m_lstItem.Add(itemTableID);
            this.m_lstItemNum.Add(1);
        }

        this.m_cCollectItem.m_lstMesh.Add(obj);
        this.m_cCollectItem.m_lstCurveStart.Add(pos);
        this.m_cCollectItem.m_lstCurveTop.Add(heigh);
        this.m_cCollectItem.m_lstCurveBottom.Add(bottom);
        this.m_cCollectItem.m_lstCurveStartTime.Add(GAME_TIME.TIME_FIXED());
    }

    /// <summary>
    /// 更新物品
    /// </summary>
    public bool UpdateItem()
    {
        bool finish = true;

        for (int i = 0; i < this.m_cCollectItem.m_lstMesh.Count; )
        {
            float disTime = GAME_TIME.TIME_FIXED() - this.m_cCollectItem.m_lstLineStartTime[i];
            if (this.m_cCollectItem.m_lstLineCostTime[i] < disTime)
            {
                if (this.m_cCollectItem.m_lstMesh[i] != null)
                {
                    GameObject.Destroy(this.m_cCollectItem.m_lstMesh[i]);
                    this.m_cCollectItem.m_lstMesh[i] = null;
                }
                this.m_cCollectItem.m_lstMesh.RemoveAt(i);
                this.m_cCollectItem.m_lstLineStartTime.RemoveAt(i);
                this.m_cCollectItem.m_lstLineCostTime.RemoveAt(i);
                continue;
            }
            else
            {
                float rate = CMath.QuadraticIn(disTime / this.m_cCollectItem.m_lstLineCostTime[i], 0, 1, 1);
                GUI_FUNCTION.SET_SPRITE_OBJECT_ALPHA(this.m_cCollectItem.m_lstMesh[i], Mathf.Lerp(1, 0, rate)); 
                i++;
                finish = false;
            }
        }

        return finish;
    }

    /// <summary>
    /// 生成心
    /// </summary>
    public override void GeneratorXin(Vector3 pos)
    {
        Vector2 bottom2d = GAME_FUNCTION.RANDOM_IN_CIRCLE();
        Vector3 bottom = new Vector3(bottom2d.x + pos.x, 0.2f, bottom2d.y + pos.z);
        float heigh = GAME_FUNCTION.RANDOM(0.5f, 2.5f);
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
                if (this.m_cSelfLeaderSkill != null)
                    rate += this.m_cSelfLeaderSkill.HeartRecoverRate;
                if (this.m_cFriendLaderSkill != null)
                    rate += this.m_cFriendLaderSkill.HeartRecoverRate;

                int recover = (int)(this.m_cCollectItemXin.m_lstBattleHero[i].m_cRecover.GetFinalData() * rate);
                this.m_cCollectItemXin.m_lstBattleHero[i].AddHP(recover);
                GeneratorRecoverNum(this.m_cCollectItemXin.m_lstBattleHero[i].m_cUIStartPos, recover, this.m_cCollectItemXin.m_lstBattleHero[i]);
                SetUIHeroHP(this.m_cCollectItemXin.m_lstBattleHero[i]);
                this.m_cCollectItemXin.m_lstBattleHero.RemoveAt(i);

                //统计
                this.m_iRecordXinNum++;
                this.m_iTotalXinNum++;
                if (this.m_iRecordXinNum > this.m_iRecordMaxXinNum)
                    this.m_iRecordMaxXinNum = this.m_iRecordXinNum;

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
        float heigh = GAME_FUNCTION.RANDOM(0.5f, 2.5f);
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
                if (this.m_cSelfLeaderSkill != null)
                    addbbhp += this.m_cSelfLeaderSkill.BBHPIncrease;
                if (this.m_cFriendLaderSkill != null)
                    addbbhp += this.m_cFriendLaderSkill.BBHPIncrease;
                this.m_cCollectItemShuijing.m_lstBattleHero[i].AddBBHP(addbbhp);
                SetUIHeroBBHP(this.m_cCollectItemShuijing.m_lstBattleHero[i]);
                this.m_cCollectItemShuijing.m_lstBattleHero.RemoveAt(i);

                //统计
                this.m_iTotalShuijingNum++;
                this.m_iRecordShuijingNum++;
                if (this.m_iRecordShuijingNum > this.m_iRecordMaxShuijingNum)
                    this.m_iRecordMaxShuijingNum = this.m_iRecordShuijingNum;

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
    /// 生成金币
    /// </summary>
    public override void GeneratorJinbi(Vector3 pos, int val)
    {
        if (this.m_iMaxGold < this.m_iGoldDummyNum + val)
            val = this.m_iMaxGold - this.m_iGoldDummyNum;
        if (val <= 0)
            return;

        this.m_iGoldDummyNum += val;

        Vector2 bottom2d = GAME_FUNCTION.RANDOM_IN_CIRCLE();
        Vector3 bottom = new Vector3(bottom2d.x + pos.x, 0.2f, bottom2d.y + pos.z);
        float heigh = GAME_FUNCTION.RANDOM(0.5f, 2.5f);
        //Vector3 top = (bottom - pos) / 2f;
        //top.y = heigh;

        GameObject obj = GameObject.Instantiate(this.m_cResJinbi) as GameObject;
        obj.transform.parent = this.m_cBattleParent.transform;
        obj.transform.localPosition = pos;

        this.m_cCollectItemJinbi.m_lstMesh.Add(obj);
        this.m_cCollectItemJinbi.m_lstArg.Add(val);
        this.m_cCollectItemJinbi.m_lstCurveStart.Add(pos);
        this.m_cCollectItemJinbi.m_lstCurveTop.Add(heigh);
        this.m_cCollectItemJinbi.m_lstCurveBottom.Add(bottom);
        this.m_cCollectItemJinbi.m_lstCurveStartTime.Add(GAME_TIME.TIME_FIXED());
    }

    /// <summary>
    /// 逻辑更新金币点
    /// </summary>
    /// <returns></returns>
    public bool UpdateJinbi()
    {
        bool finish = true;

        for (int i = 0; i < this.m_cCollectItemJinbi.m_lstMesh.Count; )
        {
            float disTime = GAME_TIME.TIME_FIXED() - this.m_cCollectItemJinbi.m_lstLineStartTime[i];
            if (this.m_cCollectItemJinbi.m_lstLineCostTime[i] < disTime)
            {
                if (this.m_cCollectItemJinbi.m_lstMesh[i] != null)
                {
                    GameObject.Destroy(this.m_cCollectItemJinbi.m_lstMesh[i]);
                    this.m_cCollectItemJinbi.m_lstMesh[i] = null;
                }
                //增加金币
                this.m_iGoldNum += this.m_cCollectItemJinbi.m_lstArg[i];

                this.m_cCollectItemJinbi.m_lstMesh.RemoveAt(i);
                this.m_cCollectItemJinbi.m_lstArg.RemoveAt(i);
                this.m_cCollectItemJinbi.m_lstLineStartTime.RemoveAt(i);
                this.m_cCollectItemJinbi.m_lstLineCostTime.RemoveAt(i);
                this.m_cCollectItemJinbi.m_lstCurveBottom.RemoveAt(i);
                this.m_cCollectItemJinbi.m_lstCurveStart.RemoveAt(i);

				MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_BATTLE_GET_JINBI);
//                MediaMgr.PlaySound(SOUND_DEFINE.SE_BATTLE_GET_JINBI);

                SetGlobalData();
                continue;
            }
            else
            {
                float rate = CMath.QuadraticIn(disTime / this.m_cCollectItemJinbi.m_lstLineCostTime[i], 0, 1, 1);
                this.m_cCollectItemJinbi.m_lstMesh[i].transform.localPosition = Vector3.Lerp(this.m_cCollectItemJinbi.m_lstCurveStart[i], this.m_cCollectItemJinbi.m_lstCurveBottom[i], rate);
                i++;
                finish = false;
            }
        }

        return finish;
    }

    /// <summary>
    /// 生成农场点
    /// </summary>
    public override void GeneratorFarm(Vector3 pos, int val)
    {
        if (this.m_iMaxFarm < this.m_iFarmDummyNum + val)
            val = this.m_iMaxFarm - this.m_iFarmDummyNum;
        if (val <= 0)
            return;

        this.m_iFarmDummyNum += val;

        Vector2 bottom2d = GAME_FUNCTION.RANDOM_IN_CIRCLE();
        Vector3 bottom = new Vector3(bottom2d.x + pos.x, 0.2f, bottom2d.y + pos.z);
        float heigh = GAME_FUNCTION.RANDOM(0.5f, 2.5f);
        //Vector3 top = (bottom - pos) / 2f;
        //top.y = heigh;

        GameObject obj = GameObject.Instantiate(this.m_cResFarm) as GameObject;
        obj.transform.parent = this.m_cBattleParent.transform;
        obj.transform.localPosition = pos;

        this.m_cCollectItemFarm.m_lstMesh.Add(obj);
        this.m_cCollectItemFarm.m_lstArg.Add(val);
        this.m_cCollectItemFarm.m_lstCurveStart.Add(pos);
        this.m_cCollectItemFarm.m_lstCurveTop.Add(heigh);
        this.m_cCollectItemFarm.m_lstCurveBottom.Add(bottom);
        this.m_cCollectItemFarm.m_lstCurveStartTime.Add(GAME_TIME.TIME_FIXED());
    }

    /// <summary>
    /// 逻辑更新农场点
    /// </summary>
    /// <returns></returns>
    public bool UpdateFarm()
    {
        bool finish = true;

        for (int i = 0; i < this.m_cCollectItemFarm.m_lstMesh.Count; )
        {
            float disTime = GAME_TIME.TIME_FIXED() - this.m_cCollectItemFarm.m_lstLineStartTime[i];
            if (this.m_cCollectItemFarm.m_lstLineCostTime[i] < disTime)
            {
                if (this.m_cCollectItemFarm.m_lstMesh[i] != null)
                {
                    GameObject.Destroy(this.m_cCollectItemFarm.m_lstMesh[i]);
                    this.m_cCollectItemFarm.m_lstMesh[i] = null;
                }
                //增加farm值
                this.m_iFarmNum += this.m_cCollectItemFarm.m_lstArg[i];

                this.m_cCollectItemFarm.m_lstMesh.RemoveAt(i);
                this.m_cCollectItemFarm.m_lstArg.RemoveAt(i);
                this.m_cCollectItemFarm.m_lstLineStartTime.RemoveAt(i);
                this.m_cCollectItemFarm.m_lstLineCostTime.RemoveAt(i);
                this.m_cCollectItemFarm.m_lstCurveBottom.RemoveAt(i);
                this.m_cCollectItemFarm.m_lstCurveStart.RemoveAt(i);

				MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_BATTLE_GET_FARM);
//                MediaMgr.PlaySound(SOUND_DEFINE.SE_BATTLE_GET_FARM);

                SetGlobalData();
                continue;
            }
            else
            {
                float rate = CMath.QuadraticIn(disTime / this.m_cCollectItemFarm.m_lstLineCostTime[i], 0, 1, 1);
                this.m_cCollectItemFarm.m_lstMesh[i].transform.localPosition = Vector3.Lerp(this.m_cCollectItemFarm.m_lstCurveStart[i], this.m_cCollectItemFarm.m_lstCurveBottom[i], rate);
                i++;
                finish = false;
            }
        }

        return finish;
    }

    /// <summary>
    /// 生成灵魂
    /// </summary>
    public void GeneratorSoul(Vector3 pos, int val)
    {
        Vector2 bottom2d = GAME_FUNCTION.RANDOM_IN_CIRCLE();
        Vector3 bottom = new Vector3(bottom2d.x + pos.x, 0.43f, bottom2d.y + pos.z);
        float heigh = 2f;
        //Vector3 top = (bottom - pos) / 2f;
        //top.y = heigh;

        GameObject obj = GameObject.Instantiate(this.m_cResSoul) as GameObject;
        obj.transform.parent = this.m_cBattleParent.transform;
        obj.transform.localPosition = pos;

        this.m_cCollectItemSoul.m_lstMesh.Add(obj);
        this.m_cCollectItemSoul.m_lstArg.Add(val);
        this.m_cCollectItemSoul.m_lstCurveStart.Add(pos);
        this.m_cCollectItemSoul.m_lstCurveTop.Add(heigh);
        this.m_cCollectItemSoul.m_lstCurveBottom.Add(bottom);
        this.m_cCollectItemSoul.m_lstCurveStartTime.Add(GAME_TIME.TIME_FIXED());
    }

    /// <summary>
    /// 逻辑更新灵魂
    /// </summary>
    /// <returns></returns>
    public bool UpdateSoul()
    {
        bool finish = true;

        for (int i = 0; i < this.m_cCollectItemSoul.m_lstMesh.Count; )
        {
            float disTime = GAME_TIME.TIME_FIXED() - this.m_cCollectItemSoul.m_lstLineStartTime[i];
            if (this.m_cCollectItemSoul.m_lstLineCostTime[i] < disTime)
            {
                if (this.m_cCollectItemSoul.m_lstMesh[i] != null)
                {
                    GameObject.Destroy(this.m_cCollectItemSoul.m_lstMesh[i]);
                    this.m_cCollectItemSoul.m_lstMesh[i] = null;
                }

                //增加Soul
                this.m_lstSoul.Add(this.m_cCollectItemSoul.m_lstArg[i]);

                HeroTable table = HeroTableManager.GetInstance().GetHeroTable(this.m_cCollectItemSoul.m_lstArg[i]);
                GameObject effect = null;
                if (table.Star <= 2)
                {
                    effect = GameObject.Instantiate(this.m_cResCollectHeroEffect1) as GameObject;
                    effect.transform.parent = this.m_cCollect.transform;
                    effect.transform.localScale = Vector3.one * 85;
                    effect.transform.localPosition = new Vector3(0, -50, 8);
                }
                else if (table.Star == 3)
                {
                    effect = GameObject.Instantiate(this.m_cResCollectHeroEffect2) as GameObject;
                    effect.transform.parent = this.m_cCollect.transform;
                    effect.transform.localScale = Vector3.one * 85;
                    effect.transform.localPosition = new Vector3(12, -52, 8);
                }
                else if (table.Star == 4)
                {
                    effect = GameObject.Instantiate(this.m_cResCollectHeroEffect3) as GameObject;
                    effect.transform.parent = this.m_cCollect.transform;
                    effect.transform.localScale = Vector3.one * 85;
                    effect.transform.localPosition = new Vector3(12, -52, 8);
                }
                else if (table.Star == 5)
                {
                    effect = GameObject.Instantiate(this.m_cResCollectHeroEffect4) as GameObject;
                    effect.transform.parent = this.m_cCollect.transform;
                    effect.transform.localScale = Vector3.one * 85;
                    effect.transform.localPosition = new Vector3(42, -56, 0);
                }

                //effect.transform.parent = this.m_cCollect.transform;
                //effect.transform.localScale = Vector3.one * 100f;
                //effect.transform.localPosition = Vector3.up * -50f;
                //effect.transform.parent = this.m_cBattleParent.transform;
                //effect.transform.localPosition = Vector3.zero;

                this.m_cCollectItemSoul.m_lstMesh.RemoveAt(i);
                this.m_cCollectItemSoul.m_lstArg.RemoveAt(i);
                this.m_cCollectItemSoul.m_lstLineStartTime.RemoveAt(i);
                this.m_cCollectItemSoul.m_lstLineCostTime.RemoveAt(i);
                this.m_cCollectItemSoul.m_lstCurveBottom.RemoveAt(i);
                this.m_cCollectItemSoul.m_lstCurveStart.RemoveAt(i);

				MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_BATTLE_GET_SOUL);
//                MediaMgr.PlaySound(SOUND_DEFINE.SE_BATTLE_GET_SOUL);

                SetGlobalData();
                continue;
            }
            else
            {
                float rate = CMath.QuadraticIn(disTime / this.m_cCollectItemSoul.m_lstLineCostTime[i], 0, 1, 1);
                this.m_cCollectItemSoul.m_lstMesh[i].transform.localPosition = Vector3.Lerp(this.m_cCollectItemSoul.m_lstCurveStart[i], this.m_cCollectItemSoul.m_lstCurveBottom[i], rate);
                i++;
                finish = false;
            }
        }

        return finish;
    }

    /// <summary>
    /// 生成宝箱
    /// </summary>
    /// <param name="pos">位置</param>
    /// <param name="index">索引</param>
    /// <param name="isMonster">是否为宝箱怪</param>
    public void GeneratorBox(Vector3 pos, int index , bool isMonster )
    {
        GameObject obj = GameObject.Instantiate(this.m_cResBox) as GameObject;
        obj.transform.parent = this.m_cBattleParent.transform;
        //obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = pos;

        GfxObject gfx = new GfxObject(obj);
        gfx.Play("state", WrapMode.Loop, 1, PLAY_MODE.PLAY);
        this.m_lstBox[index] = gfx;
        this.m_lstBoxOpen[index] = false;
        this.m_lstBoxIsMonster[index] = isMonster;
        if (isMonster)
        {
            this.m_iTotalBoxMonster++;
        }
    }

    /// <summary>
    /// 生成技能展示
    /// </summary>
    public override void GeneratorSkillShow( BattleHero hero )
    {
        if (hero == null)
            return;

        Time.timeScale = 0;
        this.m_fSkillAvatarStartTime = GAME_TIME.TIME_REAL();
        if (this.m_cSkillAvatar != null)
            GameObject.DestroyImmediate(this.m_cSkillAvatar);
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

        //隐藏目标
        if (this.m_iSelectTargetIndex >= 0)
        {
            this.m_cHeroTargetSp.GetComponent<UISprite>().enabled = false;
        }
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
            this.m_vecTargetBuf[i].enabled = false;
        }
        for (int i = 0; i < this.m_vecSelfBuf.Length; i++)
        {
            this.m_vecSelfBuf[i].enabled = false;
        }

        //隐藏掉落
        for (int i = 0; i < this.m_cCollectItem.m_lstMesh.Count; i++)
        {
            if( this.m_cCollectItem.m_lstMesh[i] != null )
                this.m_cCollectItem.m_lstMesh[i].SetActive(false);
        }
        for (int i = 0; i < this.m_cCollectItemFarm.m_lstMesh.Count; i++)
        {
            if (this.m_cCollectItemFarm.m_lstMesh[i] != null )
                this.m_cCollectItemFarm.m_lstMesh[i].SetActive(false);
        }
        for (int i = 0; i < this.m_cCollectItemJinbi.m_lstMesh.Count; i++)
        {
            if (this.m_cCollectItemJinbi.m_lstMesh[i]!= null )
                this.m_cCollectItemJinbi.m_lstMesh[i].SetActive(false);
        }
        for (int i = 0; i < this.m_cCollectItemShuijing.m_lstMesh.Count; i++)
        {
            if (this.m_cCollectItemShuijing.m_lstMesh[i] != null )
                this.m_cCollectItemShuijing.m_lstMesh[i].SetActive(false);
        }
        for (int i = 0; i < this.m_cCollectItemXin.m_lstMesh.Count; i++)
        {
            if (this.m_cCollectItemXin.m_lstMesh[i] != null )
                this.m_cCollectItemXin.m_lstMesh[i].SetActive(false);
        }
        for (int i = 0; i < this.m_cCollectItemSoul.m_lstMesh.Count; i++)
        {
            if (this.m_cCollectItemSoul.m_lstMesh[i] != null )
                this.m_cCollectItemSoul.m_lstMesh[i].SetActive(false);
        }

        this.m_iUIState = SKILL_SHOW_STATE;

        //统计
        if (hero.m_bSelf)
        {
            this.m_iTotalSkillNum++;
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

        //英雄头像按钮按下操作
        if (this.m_fPressBtnStartTime > 0)
        {
            if (GAME_TIME.TIME_FIXED() - this.m_fPressBtnStartTime > PRESS_DIS_TIME)
            {
                if (this.m_eBattleState == BATTLE_STATE.BATTLE_STATE_SELF_ATTACK && this.m_iUIState == UI_STATE_NONE)
                {
                    //新手引导中不再显示技能和防御图标
                    if (Role.role.GetBaseProperty().m_iModelID < 0 && this.m_cPressBtnObject != null )
                        this.m_cPressBtnObject.SetActive(true);
                }
                m_bPressTimeOut = true;
                this.m_fPressBtnStartTime = 0;
            }
        }

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
                        if (this.m_vecSelfBuf[i].enabled != false)
                            this.m_vecSelfBuf[i].enabled = false;
                    }
                    else
                    {
                        if (this.m_vecSelfHero[i].m_lstBUF.Count <= 0)
                        {
                            if (this.m_vecSelfBuf[i].enabled != false)
                                this.m_vecSelfBuf[i].enabled = false;
                        }
                        else
                        {
                            if (this.m_vecSelfBuf[i].enabled != true && this.m_vecSelfHero[i].GetGfxObject().GetStateControl().GetCurrentState().GetStateType() == STATE_TYPE.STATE_IDLE )
                            {
                                this.m_vecSelfBuf[i].enabled = true;
                            }
                            BATTLE_FUNCTION.BUF_SPRITE(this.m_vecSelfHero[i], this.m_vecSelfBuf[i], this.m_iBufShowIndex);
                        }
                    }
                }
                else
                {
                    if (this.m_vecSelfBuf[i].enabled != false)
                        this.m_vecSelfBuf[i].enabled = false;
                }

                //目标BUF展示
                if (this.m_vecTargetHero[i] != null)
                {
                    if (this.m_vecTargetHero[i].m_bDead)
                    {
                        if (this.m_vecTargetBuf[i].enabled != false)
                            this.m_vecTargetBuf[i].enabled = false;
                    }
                    else
                    {
                        if (this.m_vecTargetHero[i].m_lstBUF.Count <= 0)
                        {
                            if (this.m_vecTargetBuf[i].enabled != false)
                                this.m_vecTargetBuf[i].enabled = false;
                        }
                        else
                        {
                            if (this.m_vecTargetBuf[i].enabled != true && this.m_vecTargetHero[i].GetGfxObject().GetStateControl().GetCurrentState().GetStateType() == STATE_TYPE.STATE_IDLE)
                                this.m_vecTargetBuf[i].enabled = true;
                            BATTLE_FUNCTION.BUF_SPRITE(this.m_vecTargetHero[i], this.m_vecTargetBuf[i], this.m_iBufShowIndex);
                        }
                    }
                }
                else
                {
                    if (this.m_vecTargetBuf[i].enabled != false)
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

        //宝箱更新
        for (int i = 0; i < this.m_lstBox.Length; i++)
        {
            if (this.m_lstBox[i] != null && !this.m_lstBox[i].IsPlay("open") && this.m_lstBoxOpen[i])
            {
                this.m_lstBox[i].Destory();
                this.m_lstBox[i] = null;
            }
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
                this.m_eBattleState++;
                break;
            case BATTLE_STATE.BATTLE_STATE_BEGIN_BEGIN:    //战斗开始准备
                for (int i = 0; i < HERO_MAX_NUM; i++)
                {
                    this.m_vecSelfBuf[i].enabled = false;
                    this.m_vecTargetBuf[i].enabled = false;
                    if (this.m_vecSelfHero[i] != null)
                        this.m_vecSelfHero[i].ClearBUF();
                }
                this.m_bSoul = false;
                this.m_iBufShowIndex = 0;
                this.m_fBufShowStartTime = 0;
                this.m_iRound = 0;
                this.m_iBattleResult = BATTLE_WIN_NONE;
                this.m_iAutoTargetIndex = -1;
                this.m_iSelectTargetIndex = -1;
                this.m_cHeroTargetSp.SetActive(true);
                this.m_cHeroTargetSp.transform.localPosition = Vector3.one * 0xFFFFF;
                for (int i = 0; i < this.m_vecHeroAttackProperty.Length; i++ )
                {
                    this.m_vecHeroAttackProperty[i].enabled = false;
                }
                this.m_cBlackGround.transform.localPosition = BLACK_GROUND_END;
                this.m_cBoxGiveUpBtn.SetActive(false);
                this.m_cItemBackCollider.SetActive(false);
                this.m_iUIState = UI_STATE_NONE;
                //统计
                this.m_iRecordShuijingNum = 0;
                this.m_iRecordXinNum = 0;
                //
                SetGlobalData();

				ResourceMgr.ClearAsyncLoad();
//                ResourceMgr.ClearAsyncLoad();
                //异步加载敌人
                for (int i = 0; i < this.m_vecTargetHero.Length; i++)
                {
                    BattleHeroGenerator.GeneratorTargetHeroAysnc(this.m_vecTargetHero[i]);
                }
                GUI_FUNCTION.LOADING_SHOW();
                //音效
                this.m_eBattleState++;
                break;
            case BATTLE_STATE.BATTLE_STATE_BEGIN_BEGIN1:    //战斗准备开始1 敌人资源加载等待
                //per = ResourceMgr.GetAsyncProcess() * 100f;
                if (ResourceMgr.GetProgress() >=1f && ResourceMgr.IsComplete() )
                {
                    this.m_eBattleState++;
                }
                break;
            case BATTLE_STATE.BATTLE_STATE_BEGIN_BEGIN2:    //战斗准备开始2 实例化敌人
                GUI_FUNCTION.LOADING_HIDEN();
                for (int i = 0; i < this.m_vecTargetHero.Length; i++)
                {
                    if (this.m_vecTargetHero[i] != null && this.m_vecTargetHero[i].GetGfxObject() != null)
                        this.m_vecTargetHero[i].GetGfxObject().Destory();
                    BattleHeroGenerator.GeneratorMonsterHeroGfxAysnc(this.m_vecTargetHero[i], this, this.m_cBattleParent, this.m_vecUITargetPos[i], this.m_vecTargetPos[i], this.m_vecTargetAttackPos[i], false);
                }
                this.m_fEndWaitStartTime = GAME_TIME.TIME_FIXED();
                this.m_eBattleState++;

                //新手引导
                GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_FIRST_FIGHT1);
                GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_BATTLE_SECOND4);
                
                //GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_BATTLE_THIRD4);
                //GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_BATTLE_THIRD5);
                //GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_BATTLE_THIRD6);

                break;
            case BATTLE_STATE.BATTLE_STATE_BEGIN:    //战斗开始
                //黑幕移动
                float disRate = (GAME_TIME.TIME_FIXED() - this.m_fEndWaitStartTime) / BLACK_GROUND_COST_TIME;
                if (disRate > 1f)
                {
                    this.m_cBlackGround.transform.localPosition = BLACK_GROUND_START;
                    this.m_eBattleState++;

                    GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_FIRST_FIGHT2);
                    GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_BATTLE_SECOND5);
                }
                else
                {
                    this.m_cBlackGround.transform.localPosition = Vector3.Lerp(BLACK_GROUND_END, BLACK_GROUND_START, disRate);
                }
                break;
            case BATTLE_STATE.BATTLE_STATE_BEGIN_END:    //战斗开始结束
                //装备影响
                for (int i = 0; i < this.m_vecSelfHero.Length; i++)
                {
                    BattleHero tmpHero = this.m_vecSelfHero[i];
                    BATTLE_FUNCTION.EQUIP_ACTION(BATTLE_TIME_SCENE.BATTLE_START, tmpHero);
                }
                if (this.m_bBoss)
                    this.m_eBattleState = BATTLE_STATE.BATTLE_STATE_WARING_BOSS_BEGIN;
                else
                    this.m_eBattleState = BATTLE_STATE.BATTLE_STATE_SELF_ATTACK_BEGIN;
                break;
            case BATTLE_STATE.BATTLE_STATE_WARING_BEGIN:    //宝箱警告开始
                this.m_cWarningEffect = GameObject.Instantiate(this.m_cResBoxWarningEffect) as GameObject;
                //音效
				MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_BATTLE_BOX_WARNING);
//                MediaMgr.PlaySound(SOUND_DEFINE.SE_BATTLE_BOX_WARNING);
                this.m_eBattleState++;
                break;
            case BATTLE_STATE.BATTLE_STATE_WARING:   //宝箱警告
                if (this.m_cWarningEffect == null)
                {
                    this.m_eBattleState++;
                }
                break;
            case BATTLE_STATE.BATTLE_STATE_WARING_END:   //宝箱警告结束
                //装备影响
                for (int i = 0; i < this.m_vecSelfHero.Length; i++)
                {
                    BattleHero tmpHero = this.m_vecSelfHero[i];
                    BATTLE_FUNCTION.EQUIP_ACTION(BATTLE_TIME_SCENE.BATTLE_START, tmpHero);
                }
                this.m_eBattleState = BATTLE_STATE.BATTLE_STATE_SELF_ATTACK_BEGIN;
                break;
            case BATTLE_STATE.BATTLE_STATE_WARING_BOSS_BEGIN:    //BOSS警告开始
                this.m_cWarningEffect = GameObject.Instantiate(this.m_cResBossWarningEffect) as GameObject;
                //音效
				MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_BATTLE_BOSS_WARNING);
//                MediaMgr.PlaySound(SOUND_DEFINE.SE_BATTLE_BOSS_WARNING);
                this.m_eBattleState++;
                break;
            case BATTLE_STATE.BATTLE_STATE_WARING_BOSS:   //BOSS警告
                if (this.m_cWarningEffect == null)
                {
                    this.m_eBattleState++;
                }
                break;
            case BATTLE_STATE.BATTLE_STATE_WARING_BOSS_END:   //BOSS警告结束
                this.m_eBattleState = BATTLE_STATE.BATTLE_STATE_SELF_ATTACK_BEGIN;

                GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_FIRST_FIGHT3);
                GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_BATTLE_SECOND6);

                break;
            case BATTLE_STATE.BATTLE_STATE_SELF_ATTACK_BEGIN:    //己方攻击开始
                //装备影响
                for (int i = 0; i < this.m_vecSelfHero.Length; i++)
                {
                    BattleHero tmpHero = this.m_vecSelfHero[i];
                    BATTLE_FUNCTION.EQUIP_ACTION(BATTLE_TIME_SCENE.BATTLE_ROUND_START, tmpHero);
                }

                this.m_iRound++;    //回合数增加
                this.m_cMenuBtn.SetActive(true);    //开启按钮开关
                this.m_cHeroTargetSp.SetActive(true);  //己方攻击时选择目标图开启

                //统计
                this.m_iRoundHurtSum = 0;
                this.m_iRoundSparkNum = 0;

                //展示目标血条
                BattleHero seltar = GetTargetAuto();
                SetUITargetData(seltar);

                //开启目标选择Collider
                for (int i = 0; i < this.m_vecHeroTargetBtn.Length; i++)
                {
                    this.m_vecHeroTargetBtn[i].collider.enabled = true;
                }

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
                    //&& !this.m_vecSelfHero[i].m_bDead
                    if (this.m_vecSelfHero[i] != null )
                    {
                        this.m_vecSelfHero[i].m_iAttackNum = this.m_vecSelfHero[i].m_iAttackMaxNum;
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

                //收集品逻辑更新
                if (!this.m_cCollectItemSoul.UpdateCurve())
                {
                    cmdFinish = false;
                }
                if (!this.m_cCollectItemFarm.UpdateCurve())
                {
                    cmdFinish = false;
                }
                if (!this.m_cCollectItemJinbi.UpdateCurve())
                {
                    cmdFinish = false;
                }
                if (!this.m_cCollectItemShuijing.UpdateCurve())
                {
                    cmdFinish = false;
                }
                if (!this.m_cCollectItemXin.UpdateCurve())
                {
                    cmdFinish = false;
                }
                if (!this.m_cCollectItem.UpdateCurve())
                {
                    cmdFinish = false;
                }

                //判定己方命令
                for (int i = 0; i < this.m_vecSelfHero.Length; i++)
                {
                    if (this.m_vecSelfHero[i] != null)
                    {
                        //检查是否还有指令在身
                        if (this.m_vecSelfHero[i].GetCmdControl() != null && this.m_vecSelfHero[i].GetCmdControl().Update())
                        {
                            cmdFinish = false;
                            if (this.m_vecSelfBuf[i].enabled != false)
                                this.m_vecSelfBuf[i].enabled = false;
                        }
                    }
                }

                //判定怪物是否指令结束
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

                    for (int i = 0; i < this.m_vecTargetHero.Length; i++)
                    {
                        if ( this.m_vecTargetHero[i] != null && this.m_vecTargetHero[i].m_iHp <= 0 && this.m_vecTargetHero[i].m_bDead && !this.m_vecTargetHero[i].m_bDropJudge)
                        {
                            cmdFinish = false;
                            if (this.m_vecTargetHero[i].GetCmdControl().GetCmdType() != CMD_TYPE.STATE_DIE)
                            {
                                this.m_vecTargetHero[i].m_bDropJudge = true;

                                //判断是否重新选择目标
                                if (this.m_iSelectTargetIndex == i)
                                {
                                    ChangeSelectTarget();
                                }
                                //捕捉判断
                                if ((!this.m_bSoul || this.m_lstBoxIsMonster[i]) && this.m_vecTargetHero[i].m_fCatchRate > GAME_FUNCTION.RANDOM_ONE())
                                {
                                    //this.m_iCanCatchNum--;
                                    this.m_bSoul = true;
                                    int catchID = this.m_vecTargetHero[i].m_iTableID;
                                    if (this.m_vecTargetHero[i].m_iCatchID > 0)
                                        catchID = this.m_vecTargetHero[i].m_iCatchID;
                                    GeneratorSoul(this.m_vecTargetHero[i].m_cStartPos, this.m_vecTargetHero[i].m_iTableID);
                                }
                                //宝箱怪不能再开出宝箱，以及BOSS不掉宝箱
                                if (!this.m_lstBoxIsMonster[i] && !this.m_bBoss && this.m_fBoxRate > GAME_FUNCTION.RANDOM_ONE())
                                {
                                    if ( GAME_FUNCTION.RANDOM_ONE() < this.m_fBoxMonsterRate)
                                    {
                                        GeneratorBox(this.m_vecTargetHero[i].m_cStartPos, i , true);
                                    }
                                    else
                                    {
                                        GeneratorBox(this.m_vecTargetHero[i].m_cStartPos, i , false);
                                    }
                                }
                            }
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

                //检查是否有多余的攻击次数
                if (cmdFinish)
                {
                    for (int i = 0; i < this.m_vecSelfHero.Length; i++)
                    {
                        if ( this.m_vecSelfHero[i] != null)
                        {
                            if ( !this.m_vecSelfHero[i].m_bDead && this.m_vecSelfHero[i].m_iAttackNum > 0 && !this.m_vecSelfHero[i].BUFExist(BATTLE_BUF.MA))
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
                //装备影响
                for (int i = 0; i < this.m_vecSelfHero.Length; i++)
                {
                    BattleHero tmpHero = this.m_vecSelfHero[i];
                    BATTLE_FUNCTION.EQUIP_ACTION(BATTLE_TIME_SCENE.BATTLE_ROUND_END, tmpHero);
                }

                //展示目标血条
                seltar = GetTargetAuto();
                SetUITargetData(seltar);

                SetUIHeroAllHiden();

                //关闭目标选择Collider
                //for (int i = 0; i < this.m_vecHeroTargetBtn.Length; i++)
                //{
                //    this.m_vecHeroTargetBtn[i].collider.enabled = false;
                //}

                this.m_cHeroTargetSp.SetActive(false);  //攻击结束消除目标图标

                for (int i = 0; i < this.m_vecTargetHero.Length; i++)
                {
                    if (this.m_vecTargetHero[i] != null && this.m_vecTargetHero[i].m_iHp <= 0 && this.m_vecTargetHero[i].m_bDead)
                    {
                        this.m_vecTargetHero[i].GetGfxObject().Destory();
                        this.m_vecTargetHero[i] = null;
                    }
                }
                this.m_eBattleState++;
                break;
            case BATTLE_STATE.BATTLE_STATE_GET_ITEM_BEGIN:   //物品收集开始
                this.m_cCollectItemSoul.Clear();
                for (int i = 0; i < this.m_cCollectItemSoul.m_lstMesh.Count; i++)
                {
                    this.m_cCollectItemSoul.m_lstCurveStart.Add(this.m_cCollectItemSoul.m_lstMesh[i].transform.localPosition);
                    this.m_cCollectItemSoul.m_lstCurveBottom.Add(this.m_cBattleSoulPointPos.transform.localPosition);
                    float disTime = (this.m_cBattleSoulPointPos.transform.localPosition - this.m_cCollectItemSoul.m_lstMesh[i].transform.localPosition).magnitude / COLLECT_ITEM_SPEED;
                    this.m_cCollectItemSoul.m_lstLineCostTime.Add(disTime);
                    this.m_cCollectItemSoul.m_lstLineStartTime.Add(GAME_TIME.TIME_FIXED());
                }
                this.m_cCollectItem.Clear();
                for (int i = 0; i < this.m_cCollectItem.m_lstMesh.Count; i++)
                {
                    this.m_cCollectItem.m_lstLineCostTime.Add(COLLECT_ITEM_TIME);
                    this.m_cCollectItem.m_lstLineStartTime.Add(GAME_TIME.TIME_FIXED());
                }
                this.m_cCollectItemFarm.Clear();
                for (int i = 0; i < this.m_cCollectItemFarm.m_lstMesh.Count; i++)
                {
                    this.m_cCollectItemFarm.m_lstCurveStart.Add(this.m_cCollectItemFarm.m_lstMesh[i].transform.localPosition);
                    this.m_cCollectItemFarm.m_lstCurveBottom.Add(this.m_cBattleFarmPointPos.transform.localPosition);
                    float disTime = (this.m_cBattleFarmPointPos.transform.localPosition - this.m_cCollectItemFarm.m_lstMesh[i].transform.localPosition).magnitude / COLLECT_ITEM_SPEED;
                    this.m_cCollectItemFarm.m_lstLineCostTime.Add(disTime);
                    this.m_cCollectItemFarm.m_lstLineStartTime.Add(GAME_TIME.TIME_FIXED());
                }
                this.m_cCollectItemJinbi.Clear();
                for (int i = 0; i < this.m_cCollectItemJinbi.m_lstMesh.Count; i++)
                {
                    this.m_cCollectItemJinbi.m_lstCurveStart.Add(this.m_cCollectItemJinbi.m_lstMesh[i].transform.localPosition);
                    this.m_cCollectItemJinbi.m_lstCurveBottom.Add(this.m_cBattleGoldPos.transform.localPosition);
                    float disTime = (this.m_cBattleGoldPos.transform.localPosition - this.m_cCollectItemJinbi.m_lstMesh[i].transform.localPosition).magnitude / COLLECT_ITEM_SPEED;
                    this.m_cCollectItemJinbi.m_lstLineCostTime.Add(disTime);
                    this.m_cCollectItemJinbi.m_lstLineStartTime.Add(GAME_TIME.TIME_FIXED());
                }
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
                if (!UpdateSoul())
                {
                    cmdFinish = false;
                }
                if (!UpdateFarm())
                {
                    cmdFinish = false;
                }
                if (!UpdateJinbi())
                {
                    cmdFinish = false;
                }
                if (!UpdateShuijing())
                {
                    cmdFinish = false;
                }
                if (!UpdateXin())
                {
                    cmdFinish = false;
                }
                if (!UpdateSoul())
                {
                    cmdFinish = false;
                }
                if (!UpdateItem())
                {
                    cmdFinish = false;
                }
                if (cmdFinish)
                {
                    this.m_eBattleState++;
                }
                break;
            case BATTLE_STATE.BATTLE_STATE_GET_ITEM_END: //物品收集结束
                this.m_cCollectItem.Destory();
                this.m_cCollectItemFarm.Destory();
                this.m_cCollectItemJinbi.Destory();
                this.m_cCollectItemShuijing.Destory();
                this.m_cCollectItemXin.Destory();
                this.m_cCollectItemSoul.Destory();

                for (int i = 0; i < this.m_vecSelfHero.Length; i++)
                {
                    if (this.m_vecSelfHero[i] != null && !this.m_vecSelfHero[i].m_bDead)
                    {
                        float addbbhp = 0;
                        if (this.m_cSelfLeaderSkill != null)
                            addbbhp += this.m_cSelfLeaderSkill.RoundBBIncrease;
                        if (this.m_cFriendLaderSkill != null)
                            addbbhp += this.m_cFriendLaderSkill.RoundBBIncrease;

                        this.m_vecSelfHero[i].AddBBHP(addbbhp);
                        SetUIHeroBBHP(this.m_vecSelfHero[i]);
                    }
                }

                this.m_eBattleState++;
                break;
            case BATTLE_STATE.BATTLE_STATE_ENEMY_ATTACK_BEGIN:   //敌方攻击开始

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

                for (int i = 0; i < this.m_vecTargetHero.Length; i++)
                {
                    if (this.m_vecTargetHero[i] != null)
                    {
                        //判断AI
                        if (this.m_vecTargetHero[i].GetAIControl().Update())
                        {
                            //Debug.Log("ssssss ai false");
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
                        if (this.m_vecSelfHero[i].GetCmdControl() != null && this.m_vecSelfHero[i].GetCmdControl().Update())
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
                            if ( !this.m_vecTargetHero[i].m_bDead && this.m_vecTargetHero[i].m_iAttackNum > 0)
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
                this.m_eBattleState = BATTLE_STATE.BATTLE_STATE_RESULT_BEGIN;
                break;
            case BATTLE_STATE.BATTLE_STATE_RESULT_BEGIN: //结束计算开始
                int battlefinish = BATTLE_WIN_TARGET;
                for (int i = 0; i < HERO_MAX_NUM; i++)
                {
                    if (this.m_vecSelfHero[i] != null && !this.m_vecSelfHero[i].m_bDead)
                    {
                        battlefinish = 0;
                    }
                }
                if (battlefinish <= 0)
                {
                    battlefinish = BATTLE_WIN_SELF;
                    for (int i = 0; i < HERO_MAX_NUM; i++)
                    {
                        if (this.m_vecTargetHero[i] != null && !this.m_vecTargetHero[i].m_bDead)
                        {
                            battlefinish = 0;
                        }
                    }
                }
                this.m_iBattleResult = battlefinish;
                if (battlefinish > 0)
                {
                    this.m_cTargetName.text = "";  //胜利或失败后  将攻击目标的名称清空，不带入下一场初始显示

                    for (int i = 0; i < HERO_MAX_NUM; i++)
                    {
                        this.m_vecSelfBuf[i].enabled = false;
                        this.m_vecTargetBuf[i].enabled = false;
                        if (this.m_vecSelfHero[i] != null)
                            this.m_vecSelfHero[i].ClearBUF();
                    }

                    if (battlefinish == BATTLE_WIN_SELF)
                    {
                        if (!this.m_bBoss)
                        {
                            this.m_cResultEffect = GameObject.Instantiate(this.m_cResResultWin) as GameObject;
                            //音效
							MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_BATTLE_WIN);
//                            MediaMgr.PlaySound(SOUND_DEFINE.SE_BATTLE_WIN);
                        }
                        else
                        {
                            this.m_cResultEffect = GameObject.Instantiate(this.m_cResResultCongratulation) as GameObject;
                            //音效
							MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_BATTLE_CONGRATULATE);
//                            MediaMgr.PlaySound(SOUND_DEFINE.SE_BATTLE_CONGRATULATE);
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
                if (this.m_iBattleResult == BATTLE_WIN_SELF)
                {
                    this.m_eBattleState = BATTLE_STATE.BATTLE_STATE_GET_BOX_BEGIN;
                }
                else
                {
                    this.m_eBattleState = BATTLE_STATE.BATTLE_STATE_END_END;
                }
                break;
            case BATTLE_STATE.BATTLE_STATE_GET_BOX_BEGIN:    //开宝箱开始
                cmdFinish = false;
                for (int i = 0; i < this.m_lstBox.Length; i++)
                {
                    if (this.m_lstBox[i] != null)
                    {
                        this.m_vecBoxGuide[i].SetActive(true);
                        cmdFinish = true;
                    }
                }
                if (cmdFinish)
                    this.m_cBoxGiveUpBtn.SetActive(true);
                this.m_eBattleState++;
                break;
            case BATTLE_STATE.BATTLE_STATE_GET_BOX:  //开宝箱
                //this.m_iBattleState++;
                cmdFinish = true;

                for (int i = 0; i < this.m_lstBox.Length; i++)
                {
                    if ((this.m_lstBox[i] != null && this.m_lstBox[i].IsPlay("open")) || (this.m_lstBox[i] != null && !this.m_lstBoxOpen[i]))
                    {
                        cmdFinish = false;
                    }
                }

                //收集品逻辑更新
                if (!this.m_cCollectItemSoul.UpdateCurve())
                {
                    cmdFinish = false;
                }
                if (!this.m_cCollectItemFarm.UpdateCurve())
                {
                    cmdFinish = false;
                }
                if (!this.m_cCollectItemJinbi.UpdateCurve())
                {
                    cmdFinish = false;
                }
                if (!this.m_cCollectItemShuijing.UpdateCurve())
                {
                    cmdFinish = false;
                }
                if (!this.m_cCollectItemXin.UpdateCurve())
                {
                    cmdFinish = false;
                }
                if (!this.m_cCollectItem.UpdateCurve())
                {
                    cmdFinish = false;
                }

                if (cmdFinish)
                {
                    //设置收集物品参数
                    this.m_cCollectItemSoul.Clear();
                    for (int i = 0; i < this.m_cCollectItemSoul.m_lstMesh.Count; i++)
                    {
                        this.m_cCollectItemSoul.m_lstCurveStart.Add(this.m_cCollectItemSoul.m_lstMesh[i].transform.localPosition);
                        this.m_cCollectItemSoul.m_lstCurveBottom.Add(this.m_cBattleSoulPointPos.transform.localPosition);
                        float disTime = (this.m_cBattleSoulPointPos.transform.localPosition - this.m_cCollectItemSoul.m_lstMesh[i].transform.localPosition).magnitude / COLLECT_ITEM_SPEED;
                        this.m_cCollectItemSoul.m_lstLineCostTime.Add(disTime);
                        this.m_cCollectItemSoul.m_lstLineStartTime.Add(GAME_TIME.TIME_FIXED());
                    }
                    this.m_cCollectItem.Clear();
                    for (int i = 0; i < this.m_cCollectItem.m_lstMesh.Count; i++)
                    {
                        this.m_cCollectItem.m_lstLineCostTime.Add(COLLECT_ITEM_TIME);
                        this.m_cCollectItem.m_lstLineStartTime.Add(GAME_TIME.TIME_FIXED());
                    }
                    this.m_cCollectItemFarm.Clear();
                    for (int i = 0; i < this.m_cCollectItemFarm.m_lstMesh.Count; i++)
                    {
                        this.m_cCollectItemFarm.m_lstCurveStart.Add(this.m_cCollectItemFarm.m_lstMesh[i].transform.localPosition);
                        this.m_cCollectItemFarm.m_lstCurveBottom.Add(this.m_cBattleFarmPointPos.transform.localPosition);
                        float disTime = (this.m_cBattleFarmPointPos.transform.localPosition - this.m_cCollectItemFarm.m_lstMesh[i].transform.localPosition).magnitude / COLLECT_ITEM_SPEED;
                        this.m_cCollectItemFarm.m_lstLineCostTime.Add(disTime);
                        this.m_cCollectItemFarm.m_lstLineStartTime.Add(GAME_TIME.TIME_FIXED());
                    }
                    this.m_cCollectItemJinbi.Clear();
                    for (int i = 0; i < this.m_cCollectItemJinbi.m_lstMesh.Count; i++)
                    {
                        this.m_cCollectItemJinbi.m_lstCurveStart.Add(this.m_cCollectItemJinbi.m_lstMesh[i].transform.localPosition);
                        this.m_cCollectItemJinbi.m_lstCurveBottom.Add(this.m_cBattleGoldPos.transform.localPosition);
                        float disTime = (this.m_cBattleGoldPos.transform.localPosition - this.m_cCollectItemJinbi.m_lstMesh[i].transform.localPosition).magnitude / COLLECT_ITEM_SPEED;
                        this.m_cCollectItemJinbi.m_lstLineCostTime.Add(disTime);
                        this.m_cCollectItemJinbi.m_lstLineStartTime.Add(GAME_TIME.TIME_FIXED());
                    }
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

                    for (int i = 0; i < this.m_vecBoxGuide.Length; i++ )
                    {
                        this.m_vecBoxGuide[i].SetActive(false);
                    }
                    this.m_eBattleState++;
                }
                break;
            case BATTLE_STATE.BATTLE_STATE_GET_BOX_END:  //开宝箱结束

                if (this.m_cBoxGiveUpBtn.activeSelf)
                    this.m_cBoxGiveUpBtn.SetActive(false);

                cmdFinish = true;
                if (!UpdateSoul())
                {
                    cmdFinish = false;
                }
                if (!UpdateFarm())
                {
                    cmdFinish = false;
                }
                if (!UpdateJinbi())
                {
                    cmdFinish = false;
                }
                if (!UpdateShuijing())
                {
                    cmdFinish = false;
                }
                if (!UpdateSoul())
                {
                    cmdFinish = false;
                }
                if (!UpdateXin())
                {
                    cmdFinish = false;
                }
                if (!UpdateItem())
                {
                    cmdFinish = false;
                }

                if (cmdFinish)
                {
                    //宝箱销毁
                    for (int i = 0; i < this.m_lstBox.Length; i++)
                    {
                        if (this.m_lstBox[i] != null)
                        {
                            this.m_lstBox[i].Destory();
                        }
                        this.m_lstBox[i] = null;
                    }
                    Array.Clear(this.m_lstBox, 0, this.m_lstBox.Length);
                    Array.Clear(this.m_lstBoxOpen, 0, this.m_lstBoxOpen.Length);
                    Array.Clear(this.m_lstBoxIsMonster, 0, this.m_lstBoxIsMonster.Length);

                    //收集销毁
                    this.m_cCollectItem.Destory();
                    this.m_cCollectItemFarm.Destory();
                    this.m_cCollectItemJinbi.Destory();
                    this.m_cCollectItemShuijing.Destory();
                    this.m_cCollectItemXin.Destory();
                    this.m_cCollectItemSoul.Destory();

                    this.m_eBattleState++;
                }
                break;
            case BATTLE_STATE.BATTLE_STATE_END_BEGIN:   //结束开始
                for (int i = 0; i < this.m_vecSelfHero.Length; i++)
                {
                    if (this.m_vecSelfHero[i] != null)
                        this.m_vecSelfHero[i].Clear();
                }
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BATTLEMENU).Hiden();
                this.m_cBlackGround.transform.localPosition = new Vector3(640f, this.m_cBlackGround.transform.localPosition.y, this.m_cBlackGround.transform.localPosition.z);
                this.m_fEndWaitStartTime = GAME_TIME.TIME_FIXED();

                //装备影响
                for (int i = 0; i < this.m_vecSelfHero.Length; i++)
                {
                    BattleHero tmpHero = this.m_vecSelfHero[i];
                    BATTLE_FUNCTION.EQUIP_ACTION(BATTLE_TIME_SCENE.BATTLE_END, tmpHero);
                }

                this.m_eBattleState++;
                break;
            case BATTLE_STATE.BATTLE_STATE_END1:  //结束
                disRate = (GAME_TIME.TIME_FIXED() - this.m_fEndWaitStartTime) / BLACK_GROUND_COST_TIME;
                if (disRate >= 1f)
                {
                    this.m_cBlackGround.transform.localPosition = BLACK_GROUND_END;
                    this.m_eBattleState++;
                }
                else
                {
                    this.m_cBlackGround.transform.localPosition = Vector3.Lerp(BLACK_GROUND_START, BLACK_GROUND_END, disRate);
                }
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
                    if (m_bLoseShow)
                    {
                        GUIBattleLose battleLose = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BATTLE_LOSE) as GUIBattleLose;
                        battleLose.Set(this.m_lstSoul.Count, this.m_lstItem.Count, this.m_iGoldNum, this.m_iFarmNum, BattleEnd, this);
                        battleLose.Show();
                    }
                    else
                    {
                        BattleEnd();
                    }
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
                //恢复目标
                if (this.m_iSelectTargetIndex >= 0)
                {
                    this.m_cHeroTargetSp.GetComponent<UISprite>().enabled = true;
                }
                //恢复数字
                for (int i = 0; i < this.m_lstHurtTxt.Count; i++)
                {
                    if (this.m_lstHurtTxt[i] != null)
                    {
                        this.m_lstHurtTxt[i].GetComponentInChildren<MeshRenderer>().enabled = true;
                    }
                }
                //恢复掉落
                for (int i = 0; i < this.m_cCollectItem.m_lstMesh.Count; i++)
                {
                    this.m_cCollectItem.m_lstMesh[i].SetActive(true);
                }
                for (int i = 0; i < this.m_cCollectItemFarm.m_lstMesh.Count; i++)
                {
                    this.m_cCollectItemFarm.m_lstMesh[i].SetActive(true);
                }
                for (int i = 0; i < this.m_cCollectItemJinbi.m_lstMesh.Count; i++)
                {
                    this.m_cCollectItemJinbi.m_lstMesh[i].SetActive(true);
                }
                for (int i = 0; i < this.m_cCollectItemShuijing.m_lstMesh.Count; i++)
                {
                    this.m_cCollectItemShuijing.m_lstMesh[i].SetActive(true);
                }
                for (int i = 0; i < this.m_cCollectItemXin.m_lstMesh.Count; i++)
                {
                    this.m_cCollectItemXin.m_lstMesh[i].SetActive(true);
                }
                for (int i = 0; i < this.m_cCollectItemSoul.m_lstMesh.Count; i++)
                {
                    this.m_cCollectItemSoul.m_lstMesh[i].SetActive(true);
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
    /// 点击宝箱
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnBox(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            int boxindex = (int)args[0];

            if (this.m_lstBoxOpen[boxindex] || this.m_lstBox[boxindex] == null )
                return;

            if (!this.m_lstBoxIsMonster[boxindex])
            {
                //音效
				MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_BATTLE_OPEN_BOX);
//                MediaMgr.PlaySound(SOUND_DEFINE.SE_BATTLE_OPEN_BOX);

                int res = GAME_FUNCTION.BET(this.m_fBoxHeartRate, this.m_fBoxBBHpRate, this.m_fBoxGoldRate, this.m_fBoxFarmRate, this.m_fBoxDropItemRate);
                int num = 0;

                switch (res)
                {
                    case 0:
                        num = (int)(this.m_iBoxHeartDropNum * GAME_FUNCTION.RANDOM(0.8f, 1.2f));
                        for (int i = 0; i < num; i++)
                        {
                            GeneratorXin(this.m_lstBox[boxindex].GetLocalPos());
                        }
                        break;
                    case 1:
                        num = (int)(this.m_iBoxBBHPDropNum * GAME_FUNCTION.RANDOM(0.8f, 1.2f));
                        for (int i = 0; i < num; i++)
                        {
                            GeneratorShuijing(this.m_lstBox[boxindex].GetLocalPos());
                        }
                        break;
                    case 2:
                        num = 20;
                        for (int i = 0; i < num; i++)
                        {
                            GeneratorJinbi(this.m_lstBox[boxindex].GetLocalPos(), this.m_iBoxGoldDropNum);
                        }
                        break;
                    case 3:
                        num = 20;
                        for (int i = 0; i < num; i++)
                        {
                            GeneratorFarm(this.m_lstBox[boxindex].GetLocalPos(), this.m_iBoxFarmDropNum);
                        }
                        break;
                    case 4:
                        if (this.m_lstDropItem.Count > 0)
                        {
                            num = GAME_FUNCTION.RANDOM_BET(1, this.m_lstDropItemRate.ToArray())[0];
                            GeneratorItem(this.m_lstBox[boxindex].GetLocalPos(), this.m_lstDropItem[num]);
                        }
                        break;
                }
            }
            else
            {
                MonsterTable table = MonsterTableManager.GetInstance().GetMonsterBaoxiangTable(this.m_iBoxMonsterTableID);
                //BattleHero hero = BattleHeroGenerator.Generator(this.m_cBattleParent, posindex, this.m_vecUITargetPos[posindex], this.m_vecTargetPos[posindex], this.m_vecTargetAttackPos[posindex], table, this, this.m_eFavType);
                BattleHero hero = BattleHeroGenerator.Generator(boxindex, this, table, this.m_eFavType);
                BattleHeroGenerator.GeneratorGfx(hero, this, this.m_cBattleParent, this.m_vecUITargetPos[boxindex], this.m_vecTargetPos[boxindex], this.m_vecTargetAttackPos[boxindex]);
                this.m_vecTargetHero[boxindex] = hero;
                this.m_eBattleState = BATTLE_STATE.BATTLE_STATE_WARING_BEGIN;
                for (int i = 0; i < this.m_vecBoxGuide.Length; i++)
                {
                    this.m_vecBoxGuide[i].SetActive(false);
                }
                if (this.m_cBoxGiveUpBtn.activeSelf)
                    this.m_cBoxGiveUpBtn.SetActive(false);
            }

            this.m_vecBoxGuide[boxindex].SetActive(false);

            this.m_lstBox[boxindex].Play("open", WrapMode.Once, 1, PLAY_MODE.PLAY);

            GameObject boxEffect = GameObject.Instantiate(this.m_cResBoxOpenEffect) as GameObject;
            boxEffect.transform.parent = this.m_cBattleParent.transform;
            boxEffect.transform.localPosition = this.m_lstBox[boxindex].GetLocalPos();

            boxEffect = GameObject.Instantiate(this.m_cResBoxMonsterEffect) as GameObject;
            boxEffect.transform.parent = this.m_cBattleParent.transform;
            boxEffect.transform.localPosition = this.m_lstBox[boxindex].GetLocalPos();

            this.m_lstBoxOpen[boxindex] = true;
        }
    }

    /// <summary>
    /// 英雄按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnHeroBtn(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {

            

            int index = (int)args[0];

            //如果为空或攻击结束者无法继续
            if (this.m_vecSelfHero[index] == null || this.m_vecSelfHero[index].m_iAttackNum <= 0)
            {
                return;
            }
            //攻击状态不是己方不继续
            if (this.m_eBattleState != BATTLE_STATE.BATTLE_STATE_SELF_ATTACK)
            {
                return;
            }

            if (this.m_iUIState == ITEM_STATE_SELECT)
            {
                BattleHeroActionInput input = new BattleHeroActionInput();
                input.SetBattleHero(this.m_vecSelfHero[index]);
                input.SetBattleAllHero(this.m_vecSelfHero);
                Item item = this.m_vecItem[this.m_iSelectIndex];

                if (EventActionManager.GetInstance().ExcuteReq(item.m_iEvent, input) != ACTION_ERROR_CODE.NONE)
                {
                    return;
                }

                //音效
				MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_BATTLE_CLICK_HERO);
//                MediaMgr.PlaySound(SOUND_DEFINE.SE_BATTLE_CLICK_HERO);

                if (EventActionManager.GetInstance().Excute(item.m_iEvent, input) == ACTION_ERROR_CODE.NONE)
                {
                    item.m_iNum--;
                    SetUIItemNum(this.m_iSelectIndex);

                    //物品仍有剩余
                    if (item.m_iNum > 0)
                    {
                        for (int i = 0; i < this.m_vecSelfBuf.Length; i++)
                        {
                            BattleHeroActionInput bInput = new BattleHeroActionInput();
                            bInput.SetBattleHero(this.m_vecSelfHero[i]);
                            bInput.SetBattleAllHero(this.m_vecSelfHero);
                            if (EventActionManager.GetInstance().ExcuteReq(item.m_iEvent, bInput) == ACTION_ERROR_CODE.NONE)
                            {
                                this.m_vecItemGuard[i].SetActive(true);
                            }
                            else
                            {
                                this.m_vecItemGuard[i].SetActive(false);
                            }
                        }
                    }
                    else
                    {
                        this.m_iUIState = UI_STATE_NONE;
                        this.m_cItemBackCollider.SetActive(false);
                        for (int i = 0; i < this.m_vecItemGuard.Length; i++)
                        {
                            this.m_vecItemGuard[i].SetActive(false);
                        }

                        this.m_cSprBlack.enabled = false;
                        this.m_cUseItemInfoShow.m_cItem.SetActive(false);
                        this.m_cBtnItemBack.SetActive(false);
                    }
                }
                else
                {
                    Debug.LogError("Use ERROR");
                }
                return;
            }

            //死亡英雄不可行动
            if (this.m_vecSelfHero[index] == null || this.m_vecSelfHero[index].m_bDead)
            {
                return;
            }

            if (this.m_iUIState != UI_STATE_NONE)
            {
                return;
            }

            //麻痹无法战斗
            if (this.m_vecSelfHero[index].BUFExist(BATTLE_BUF.MA))
            {
                return;
            }

            //按下长时间不动不可行动
            if (m_bPressTimeOut)
            {
                m_bPressTimeOut = false;
                return;
            }

            BattleHero target = GetTargetAuto();
            if (target == null)
            {
                return;
            }

            //音效
			MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_BATTLE_CLICK_HERO);
//            MediaMgr.PlaySound(SOUND_DEFINE.SE_BATTLE_CLICK_HERO);

            switch (this.m_vecSelfHero[index].m_eMoveType)
            {
                case MoveType.None:
                    this.m_vecSelfHero[index].GetCmdControl().CmdAttackState(target);
                    break;
                case MoveType.Normal:
                    this.m_vecSelfHero[index].GetCmdControl().CmdMoveAttack(target, this.m_vecMyselfPos[index].transform.localPosition, target.m_cAttackPos);
                    break;
            }

            //this.m_vecSelfHero[index].GetCmdControl().CmdSkill(target);
            //this.m_vecSelfHero[index].GetCmdControl().CmdMoveSkill(target, this.m_cMyselfPos[index].transform.localPosition, target.m_cAttackPos);
            //this.m_vecSelfHero[index].GetCmdControl().CmdAllSkill(this.m_vecTargetHero);
            //this.m_vecSelfHero[index].GetCmdControl().CmdMoveAllSkill(this.m_vecTargetHero, this.m_cMyselfPos[index].transform.localPosition, this.m_cBattleBBSkillPos.transform.localPosition);
            //this.m_vecSelfHero[index].GetCmdControl().CmdAttackState(target);
            //this.m_vecSelfHero[index].GetCmdControl().CmdMoveAttack(target, this.m_cMyselfPos[index].transform.localPosition, target.m_cAttackPos);

            if (this.m_cMenuBtn.activeSelf != false)
                this.m_cMenuBtn.SetActive(false);
            this.m_vecSelfHero[index].m_iAttackNum--;
            SetUIHeroAttackNum(this.m_vecSelfHero[index]);
        }
        else if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.PRESS)  //划指判断 BB技能 还是 防御
        {
            if (this.m_iUIState != UI_STATE_NONE)
            {
                return;
            }

            int index = (int)args[0];
            if (this.m_vecSelfHero[index] == null || this.m_vecSelfHero[index].m_bDead || this.m_vecSelfHero[index].m_iAttackNum <= 0 || this.m_vecSelfHero[index].BUFExist(BATTLE_BUF.MA))
            {
                return;
            }

            if (this.m_vecSelfHero[index] != null && this.m_vecSelfHero[index].BUFExist(BATTLE_BUF.FENGYIN))
            {
                return;
            }

            if (this.m_eBattleState != BATTLE_STATE.BATTLE_STATE_SELF_ATTACK)
            {
                return;
            }

            BattleHero target = GetTargetAuto();

            if (target == null)
            {
                return;
            }

            if (!info.m_bDone)
            {
                Vector2 dis = UICamera.currentTouch.pos - this.m_cHeroPressPos;

                if ((dis.y > 70f) && (GuideManager.GetInstance().GetModelID() > GUIDE_FUNCTION.MODEL_BATTLE_SECOND4 || GuideManager.GetInstance().GetModelID() <= 0 || Role.role.GetBaseProperty().m_iModelID < 0))   //BB判定
                {
                    if (this.m_vecSelfHero[index].m_fBBHP >= this.m_vecSelfHero[index].m_iBBMaxHP && this.m_vecSelfHero[index].m_iBBMaxHP > 0)
                    {
                        GeneratorSkillShow(this.m_vecSelfHero[index]);

                        if (this.m_vecSelfHero[index].m_eBBType == BBType.ATTACK)//攻击
                        {

                            switch (this.m_vecSelfHero[index].m_eBBMoveType)
                            {
                                case MoveType.None:
                                    switch (this.m_vecSelfHero[index].m_eBBTargetType)
                                    {
                                        case BBTargetType.TargetOne:
                                            this.m_vecSelfHero[index].GetCmdControl().CmdSkill(target);
                                            break;
                                        case BBTargetType.TargetRandom:
                                        case BBTargetType.TargetAll:
                                            this.m_vecSelfHero[index].GetCmdControl().CmdAllSkill(this.m_vecTargetHero);
                                            break;
                                    }
                                    break;
                                case MoveType.Normal:
                                    switch (this.m_vecSelfHero[index].m_eBBTargetType)
                                    {
                                        case BBTargetType.TargetOne:
                                            this.m_vecSelfHero[index].GetCmdControl().CmdMoveSkill(target, this.m_vecSelfHero[index].m_cStartPos, target.m_cAttackPos);
                                            break;
                                        case BBTargetType.TargetRandom:
                                        case BBTargetType.TargetAll:
                                            this.m_vecSelfHero[index].GetCmdControl().CmdMoveAllSkill(this.m_vecTargetHero, this.m_vecSelfHero[index].m_cStartPos, this.m_cBattleBBPos.transform.localPosition);
                                            break;
                                    }
                                    break;
                            }
                        }
                        else if (this.m_vecSelfHero[index].m_eBBType == BBType.RECOVER)  //恢复
                        {
                            this.m_vecSelfHero[index].GetCmdControl().CmdAllSkillRecover(this.m_vecSelfHero);
                        }
                        else if (this.m_vecSelfHero[index].m_eBBType == BBType.BUFF)  //BUFF
                        {
                            this.m_vecSelfHero[index].GetCmdControl().CmdAllSkillBuff(this.m_vecSelfHero);
                        }
                        if (this.m_cMenuBtn.activeSelf != false)
                            this.m_cMenuBtn.SetActive(false);
                        this.m_vecSelfHero[index].ClearBBHP();
                        this.m_vecSelfHero[index].m_iAttackNum--;
                        SetUIHeroAttackNum(this.m_vecSelfHero[index]);
                        SetUIHeroBBHP(this.m_vecSelfHero[index]);
                    }
                }
                else if (dis.y < -70f && (dis.x / dis.y > -0.5f && dis.x / dis.y < 0.5f) )    //防御判定
                {
                    //if (this.m_vecHeroDefenceIMG[index].activeSelf || (this.m_fPressBtnStartTime > 0 && GAME_TIME.TIME_FIXED() - this.m_fPressBtnStartTime > PRESS_DIS_TIME_MIN))
                    {
                        //防御特效
                        GameObject defEffect = GameObject.Instantiate(this.m_cResDefenceEffect) as GameObject;
                        defEffect.transform.parent = this.m_cBattleParent.transform;
                        defEffect.transform.localScale = Vector3.one;
                        defEffect.transform.localPosition = this.m_vecSelfHero[index].m_cStartPos;

                        if (this.m_cMenuBtn.activeSelf != false)
                            this.m_cMenuBtn.SetActive(false);
                        this.m_vecSelfHero[index].m_bDefence = true;
                        this.m_vecSelfHero[index].m_iAttackNum--;
                        SetUIHeroAttackNum(this.m_vecSelfHero[index]);
                    }
                }
                this.m_vecHeroBBIMG.SetActive(false);
                this.m_vecHeroDefenceIMG.SetActive(false);
                this.m_fPressBtnStartTime = 0;
            }
            else
            {
                this.m_fPressBtnStartTime = GAME_TIME.TIME_FIXED();

                if (this.m_vecSelfHero[index].m_fBBHP >= this.m_vecSelfHero[index].m_iBBMaxHP && this.m_vecSelfHero[index].m_iBBMaxHP > 0 )
                {
                    this.m_vecHeroBBIMG.transform.localPosition = this.m_vecHeroBtn[index].transform.localPosition;
                    this.m_cPressBtnObject = this.m_vecHeroBBIMG;
                }
                else
                {
                    this.m_vecHeroDefenceIMG.transform.localPosition = this.m_vecHeroBtn[index].transform.localPosition;
                    this.m_cPressBtnObject = this.m_vecHeroDefenceIMG;
                }

                this.m_cHeroPressPos = UICamera.currentTouch.pos;
            }
        }
    }

    /// <summary>
    /// 英雄目标按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnHeroTargetBtn(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (this.m_iUIState != UI_STATE_NONE)
            {
                return;
            }

            int index = (int)args[0];
            //if (this.m_vecTargetHero[index] == null || this.m_vecTargetHero[index].m_bDead)
            //{
            //    return;
            //}

            if (this.m_eBattleState == BATTLE_STATE.BATTLE_STATE_SELF_ATTACK && this.m_vecTargetHero[index] != null && !this.m_vecTargetHero[index].m_bDead)
            {
                SelectTarget(index);
            }

            if (this.m_eBattleState == BATTLE_STATE.BATTLE_STATE_GET_BOX)
            {
                Debug.Log(index + " box index ");
                OnBox(info, args);
            }
        }
    }

    /// <summary>
    /// 菜单按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnMenu(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (this.m_iUIState != UI_STATE_NONE)
            {
                return;
            }

            if (!this.m_bMenuShow)
            {
                return;
            }

            //菜单GUI展示
            GUIBattleMenu battleMenu = (GUIBattleMenu)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BATTLEMENU);

            //this.m_lstSoul
            //itemlst 
            //tittle1
            //tittle2
            //curlayer maxlayer
            //int[] teamheroID
            //int[] teamherolevel

            battleMenu.SetBattleGUI(this);
            battleMenu.SetDungeonNameAndGateName(this.m_strTittle, this.m_strContent);
            battleMenu.SetCurrentAndMaxLayer(this.m_iCurLayer, this.m_iMaxLayer);
            battleMenu.SetListSoul(this.m_lstSoul);

            battleMenu.SetGiveUpCallBack(BattleEnd);
            int[] heroid = new int[6];
            int[] herolevel = new int[6];
            for (int i = 0; i < heroid.Length; i++)
            {
                heroid[i] = -1;
                if (this.m_vecSelfHero[i] != null)
                {
                    heroid[i] = this.m_vecSelfHero[i].m_iTableID;
                    herolevel[i] = this.m_vecSelfHero[i].m_iLevel;
                }
            }

            //更换队长位置
            int tmp = heroid[this.m_iLeaderIndex];
            heroid[this.m_iLeaderIndex] = heroid[0];
            heroid[0] = tmp;
            tmp = herolevel[this.m_iLeaderIndex];
            herolevel[this.m_iLeaderIndex] = herolevel[0];
            herolevel[0] = tmp;

            battleMenu.SetListTeamHero(heroid, herolevel);

            battleMenu.SetListItem(this.m_lstItem, this.m_lstItemNum);

            UIPanel panel = this.m_cGUIObject.GetComponent<UIPanel>();
            panel.depth = 500;

            battleMenu.Show();
        }
    }

    /// <summary>
    /// 宝箱放弃按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnBoxGiveUpBtn(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            for (int i = 0; i < this.m_lstBoxOpen.Length;i++ )
            {
                this.m_lstBoxOpen[i] = true;
            }
            //宝箱箭头
            for (int i = 0; i < this.m_vecBoxGuide.Length; i++)
            {
                this.m_vecBoxGuide[i].SetActive(false);
            }
            this.m_cBoxGiveUpBtn.SetActive(false);
        }
    }

    /// <summary>
    /// 点击物品
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnItem(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            int index = (int)args[0];

            if (this.m_eBattleState != BATTLE_STATE.BATTLE_STATE_SELF_ATTACK || this.m_iUIState != UI_STATE_NONE)
            {
                return;
            }

            if (this.m_vecItem[index] == null || this.m_vecItem[index].m_iNum <= 0)
            {
                return;
            }

            for (int i = 0; i < this.m_vecSelfHero.Length; i++)
            {
                if (this.m_vecSelfHero[i] != null && !this.m_vecSelfHero[i].m_bDead && this.m_vecSelfHero[i].m_iAttackNum <= 0)
                {
                    return;
                }
            }

            Item item = this.m_vecItem[index];

            for (int i = 0; i < this.m_vecSelfBuf.Length; i++)
            {
                BattleHeroActionInput input = new BattleHeroActionInput();
                input.SetBattleHero(this.m_vecSelfHero[i]);
                input.SetBattleAllHero(this.m_vecSelfHero);
                if (EventActionManager.GetInstance().ExcuteReq(item.m_iEvent, input) == ACTION_ERROR_CODE.NONE)
                {
                    this.m_vecItemGuard[i].SetActive(true);
                }
                else
                {
                    this.m_vecItemGuard[i].SetActive(false);
                }
            }

            //显示物品提示
            if (m_cUseItemInfoShow != null)
            {
                Item tmpItem = this.m_vecItem[index];
                GUI_FUNCTION.SET_ITEMM(m_cUseItemInfoShow.m_cItemPath, tmpItem.m_strSprName);
                m_cUseItemInfoShow.m_cLbName.text = tmpItem.m_strName;
                m_cUseItemInfoShow.m_cLbDesc.text = tmpItem.m_strDesc;
                m_cUseItemInfoShow.m_cLbCount.text = tmpItem.m_iNum.ToString();
                m_cUseItemInfoShow.m_cSpBack.enabled = false;
                m_cUseItemInfoShow.m_cSpNoItem.enabled = false;
                m_cUseItemInfoShow.m_cSpNew.enabled = false;
                UIPanel tmppanel = m_cUseItemInfoShow.m_cItem.GetComponent<UIPanel>();
                if (tmppanel == null)
                {
                    tmppanel = m_cUseItemInfoShow.m_cItem.AddComponent<UIPanel>();
                }
                tmppanel.depth = 500;
                m_cUseItemInfoShow.m_cItem.SetActive(true);
            }

            //物品返回按钮和遮罩开启
            this.m_cBtnItemBack.SetActive(true);
            this.m_cSprBlack.enabled = true;

            this.m_iUIState = ITEM_STATE_SELECT;
            this.m_iSelectIndex = index;
            this.m_cItemBackCollider.SetActive(true);

        }
    }

    /// <summary>
    /// 物品背景
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnItemBackCollider(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            //物品返回按钮和遮罩关闭
            this.m_cBtnItemBack.SetActive(false);
            this.m_cSprBlack.enabled = false;

            this.m_cUseItemInfoShow.m_cItem.SetActive(false);
            this.m_iUIState = UI_STATE_NONE;
            this.m_cItemBackCollider.SetActive(false);
            for (int i = 0; i < this.m_vecItemGuard.Length; i++)
            {
                this.m_vecItemGuard[i].SetActive(false);
            }
        }
    }

    /// <summary>
    /// 返回按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void Item_Back(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            //物品返回按钮和遮罩关闭
            this.m_cBtnItemBack.SetActive(false);
            this.m_cSprBlack.enabled = false;

            this.m_cUseItemInfoShow.m_cItem.SetActive(false);
            this.m_iUIState = UI_STATE_NONE;
            this.m_cItemBackCollider.SetActive(false);

            for (int i = 0; i < this.m_vecItemGuard.Length; i++)
            {
                this.m_vecItemGuard[i].SetActive(false);
            }
        }
    }
}