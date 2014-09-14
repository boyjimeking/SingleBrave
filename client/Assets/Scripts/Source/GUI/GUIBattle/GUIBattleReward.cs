using System.Collections.Generic;
using System.Collections;
using Game.Base;
using Game.Resource;
using Game.Gfx;
using UnityEngine;
using Game.Media;

//  GUIBattleReward.cs
//  Author: Lu Zexi
//  2013-12-12



/// <summary>
/// 战斗奖励界面
/// </summary>
public class GUIBattleReward : GUIBase
{
    public delegate void CALLBACK();

    //----------------------------------------------资源地址--------------------------------------------------

    private const string RES_MAIN = "GUI_BattleReward"; //主资源
    //英雄和物品图标资源
    private const string RES_HERO_ICON = "GUI_BattleRewardHero";    //英雄图标资源
    private const string RES_ITEM_ICON = "GUI_BattleRewardItem";    //物品图标资源
    //中部数字显示父节点
    private const string GUI_NUM_PARENT = "Panel_MiddleContent";   //数字父节点
    //金币
    private const string GUI_GOLD_NUM_PARENT = "Panel_MiddleContent/Label_Jinbi_Parent";  //金币父节点
    private const string GUI_GOLD_NUM = "Panel_MiddleContent/Label_Jinbi_Parent/Label_JinbiCount"; //金币数
    //农场点
    private const string GUI_FARM_NUM_PARENT = "Panel_MiddleContent/Label_Farm_Parent";  //农场父节点
    private const string GUI_FARM_NUM = "Panel_MiddleContent/Label_Farm_Parent/Label_Farm_Num"; //农场点
    //经验提示父节点
    private const string GUI_EXP_PARENT = "Panel_MiddleContent/Exp_Parent";   //经验父节点
    private const string GUI_EXP_REWARD_NUM = "Panel_MiddleContent/Exp_Parent/Label_EXPRewardNum";   //获取经验数
    private const string GUI_EXP_NUM = "Panel_MiddleContent/Exp_Parent/Label_EXPNowNum";  //经验数
    //经验条对象
    private const string GUI_EXP_OBJ = "Panel_MiddleContent/Exp_Slide";
    private const string GUI_EXP_BAR = "Panel_MiddleContent/Exp_Slide/Sprite_EXPSp";  //经验条
    //英雄获得
    private const string GUI_HERO_LISTPARENT = "Hero_ListParent";
    private const string GUI_HERO_ICON_PARENT = "Hero_ListParent/Hero_ListParent"; //图标父节点
    private const string GUI_HERO_LISTVIEW = "Hero_ListParent/Hero_ListParent/ListView";
    private const string GUI_HERO_GET = "Hero_ListParent/GetHero";  //获得英雄父节点
    //新英雄
    private const string GUI_NEWHERO = "NewHero"; //新英雄地址
    private const string GUI_NEWHERO_INFO = "NewHero/Label";//新英雄台词
    //物品获得
    private const string GUI_ITEM_ICON_PARENT = "Item_ListParent/Item_ListParent"; //物品图标父节点
    private const string GUI_ITEM_LISTVIEW = "Item_ListParent/Item_ListParent/ListView";
    private const string GUI_ITEM_GET = "Item_ListParent/GetItem";  //获得物品父节点
    //背景遮罩
    private const string GUI_BACK_GROUND = "Sprite_BgBlack";    //背景
    //标题
    private const string GUI_TOP_CONTENT = "Panel_TopContent";  //关卡显示父节点
    private const string GUI_DUNGEON_NAME = "Panel_TopContent/Label_FubenName"; //副本名
    private const string GUI_GATE_NAME = "Panel_TopContent/Label_GateName";    //关卡名
    //结算标题
    private const string GUI_TITLE = "PanNavigation"; //标题
    //底部上升显示
    private const string GUI_LEVEL_UP_NUM_PARENT = "Panel_BottomContent";  //升级数值父节点
    //等级提升至LV
    private const string GUI_LEVEL_UP_NUM_OBJ_LV = "LevelUP";  //当前等级滑动对象
    private const string GUI_LEVEL_UP_NUM_NOWLEVE = "LevelUP/LV"; //升级至当前等级数值
    //体力上升1点
    private const string GUI_LEVEL_UP_NUM_OBJ_STRENGTH = "Strength";  //体力上升滑动对象
    private const string GUI_LEVEL_UP_NUM_INT_STRENGTH = "Strength/Lb_Strength";  //体力上升点数
    //Cost上升1点
    private const string GUI_LEVEL_UP_NUM_OBJ_COST = "CostUp";  //Cost滑动对象
    private const string GUI_LEVEL_UP_NUM_NOW_COST = "CostUp/Lb_Cost";  //最大体力增加数量
    //好友上升1点
    private const string GUI_LEVEL_UP_NUM_OBJ_FRIEND="FriendUp";  //好友滑动对象
    private const string GUI_LEVEL_UP_NUM_INT_FRIEND = "FriendUp/Lb_Friend";  //最大好友增加数
    //体力全部恢复
    private const string GUI_LEVEL_UP_NUM_OBJ_FULL = "Full";  //体力回满滑动对象
    //获得Bonus额外奖励展示
    private const string GUI_Diamond_GET = "Panel_DiamondGet";  //获得砖石特效
    private const string GUI_Diamond_GET_INFO = "Panel_DiamondGet/TextInfo";  //滑动钻石提示  初次通关奖励 字符
    private const string GUI_Diamond_GET_OBJ = "Panel_DiamondGet/GetDiamondNum";  //获得砖石数量 提示 滑动对象
    private const string GUI_Diamond_Get_INT = "Panel_DiamondGet/GetDiamondNum/Lb_Count";  //获得钻石数量
    //3D特效
    private const string EFFECT_LEVEL_UP = "effect_GUI_BattleRewardLevelUp";  //玩家升级特效
    private const string EFFECT_BONUS_GET = "effect_GUI_bonusget";  //战斗胜利额外奖励获得
    private const string GUI_EFFECT = "GUI_EFFECT";//3d特效资源地址
    private const string EFFECT_CENTER_ANCHOR = "ANCHOR_CENTER";//3d特效父对象

 //   private const string RES_MAIN = "GUI_HeroSummonResult";             //展示主资源地址
//    private const string LB_INFO = "Label";  //英雄台词

    private const string HERO_ZHAOHUAN_ROOT = "ZHAOHUAN";   //展示根节点
    private const string HERO_STAY_POS = "After/HeroPos";    //3D模型站立位置
    private const string ZhaoHuan_BG = "BACK_GROUND";  //召唤效背景
    private const string DOOR_BG = "Door_black";  //门特效背景

    private const string MODEL_BG = "effect_GUI_choujiang_di1";  //模型展示的大英雄底图
    private const string MATERIALS_HERO_A1 = "GUI_choujiang_di_1";  //底部材质球
    private const string MATERIALS_HERO_A2 = "GUI_choujiang_di_2";  //底部材质球
    private const string MATERIALS_HERO_A3 = "GUI_choujiang_di_3";  //底部材质球
    private const string EFFECT_STAR_2 = "effect_GUI_mengjiang";               //2星及以下特效
    private const string EFFECT_STAR_3 = "effect_GUI_chaomengjiang";        //3星特效
    private const string EFFECT_STAR_4 = "effect_GUI_chaojuemengjiang";    //4星特效
    private const string EFFECT_STAR_5 = "effect_GUI_jueshiwushuang";       //5星特效
    private const string EFFECT_GUI_STAR_2 = "GUI_mengjiang";               //2星及以下特效
    private const string EFFECT_GUI_STAR_3 = "GUI_chaomengjiang";        //3星特效
    private const string EFFECT_GUI_STAR_4 = "GUI_chaojuemengjiang";    //4星特效
    private const string EFFECT_GUI_STAR_5 = "GUI_jueshiwushuang";       //5星特效
    private const string EFFECT_MOFA_SELF = "effect_GUI_HeroUpgradeEffect_mofa_self"; //魔法底盘
    private const string EFFECT_STAR_5_BLACK = "effect_GUI_wuxing_black"; //五星模型进入特效

    //----------------------------------------------游戏对象--------------------------------------------------

    //中部数字父节点
    private GameObject m_cMiddleParent;    //数字父节点
    //金币
    private GameObject m_cGoldNumParent;    //金币数父节点
    private UILabel m_cGoldNum; //金币数
    //农场点
    private GameObject m_cFarmNumParent;    //农场点父节点
    private UILabel m_cFarmNum; //农场点
    //经验字符
    private GameObject m_cExpParent;    //经验数父节点
    private UILabel m_cExpRewardNum;    //获取经验数
    private UILabel m_cExpNum;   //经验数
    //经验条
    private UISprite m_cSpBar;  //经验条
    private GameObject m_cExpBar;   //经验条滑动对象
    //英雄获得
    private GameObject m_cHeroListParent;
    private GameObject m_cHeroGet; //获得英雄提示栏
    private GameObject m_cHeroListView;
    private GameObject m_cHeroIconParent;   //英雄图标父节点
    //新英雄
    private GameObject m_cNewHero; //新英雄
    private UILabel m_cNewHeroInfo;//新英雄台词
    //物品获得
    private GameObject m_cItemGet;  //获得物品提示栏
    private GameObject m_cItemListView;
    private GameObject m_cItemIconParent;   //物品图标父节点
    //背景
    private GameObject m_cBackGroundBtn;    //背景按钮
    //标题
    private GameObject m_cTitleParent;  //标题父节点
    private UILabel m_cDungeonName; //副本名
    private UILabel m_cGateName;    //关卡名
    //结算标题
    private GameObject m_cGUITitle;
    //底部
    private GameObject m_cGUILevelUpNumParent;  //升级数值父节点
    //升级
    private GameObject m_cGUILevelUpNumObj;   //等级滑动对象
    private UILabel m_cGUILevelUpNumNowLevel;   //当前等级
    //体力上升
    private GameObject m_cGUILevelUpNumStrengthObj;  //体力上升滑动对象
    private UILabel m_cGUILevelUpNumNowStrength; //增加体力
    //Cost上升
    private GameObject m_cGUILevelUpNumCostObj;  //Cost上升滑动对象
    private UILabel m_cGUILevelUpNumNowCost; //增加cost
    //好友上升
    private GameObject m_cGUILevelUpNumFriendObj;  //好友上升对象
    private UILabel m_cGUILevelUpNumNowFriend; //增加好友
    //体力全部恢复
    private GameObject m_cGUILevelUpNumNowMaxStrenght; //全部恢复
    //获得Bonus额外奖励
    private GameObject m_cDiamondGet; //获得砖石特效
    private GameObject m_cPanDiamondGet;  //获得砖石提示父节点
    private GameObject m_cDiamondGetInfo;  //钻石获得提示字符
    private UILabel m_cLbDiamdonGetNum; //获得砖石数量提示
    //3D特效
    private UnityEngine.Object m_cEffectLevelUp;  //玩家升级特效
    private UnityEngine.Object m_cEffectBonusGet; //玩家获得额外奖励
    private GameObject m_cGuiEffect;    //3d特效资源
    private GameObject m_cEffectParent; //3d特效父对象
    private GameObject m_cEffectObjLevelUp;
    private GameObject m_cEffectObjBonusGet;
    //英雄和物品图标资源
    private UnityEngine.Object m_cResHeroIcon;  //英雄图标资源
    private UnityEngine.Object m_cResItemIcon;  //物品图标资源
    private List<RewardHero> m_lstHeroIcon = new List<RewardHero>();    //英雄图标列表
    private List<RewardItem> m_lstItemIcon = new List<RewardItem>();    //物品图标列表
    private List<int> m_lstNewHero = new List<int>(); //新英雄TableId列表

    private UILabel m_cLbInfo;  //英雄台词
    private Hero m_iReusltHero;   //展示接口返回的英雄ID
    private GameObject m_cEffectStar2; //2星及以下特效
    private GameObject m_cEffectStar3; //3星特效
    private GameObject m_cEffectStar4; //4星特效
    private GameObject m_cEffectStar5; //5星特效
    private GameObject m_cEffectStar5Black; //五星模型进入特效
    private GameObject m_cEffectStarStr2; //2星及以下字特效
    private GameObject m_cEffectStarStr3; //3星字特效
    private GameObject m_cEffectStarStr4; //4星字特效
    private GameObject m_cEffectStarStr5; //5星字特效
    private GameObject m_cZhaoHuanBg;  //召唤背景
    private GameObject m_cSceneRoot;    //场景根节点
    private GameObject m_cStayPos;  //站立点
    private GfxObject m_cGfxHero;   //渲染实例
    private GameObject m_cModelBg; //3D背景
    private GameObject m_cModelMaterial1;
    private GameObject m_cModelMaterial2;
    private GameObject m_cModelMaterial3;
    private GameObject m_cMofaSelf;  //魔法底盘
    private GameObject m_cDoorBg;  //门特效背景

    /// <summary>
    /// 奖励英雄
    /// </summary>
    private class RewardHero
    {
        public GameObject m_cHero; //英雄实体

        private string HERO_BORDRE = "ItemBorder";    //框
        private string HERO_FARME = "ItemFrame"; //底框
        private string HERO_ICON = "ItemMonster";  //头像
        private string HERO_COVER = "ItemCover"; //遮盖
        private string HERO_UNKNOW = "ItemUnKnow";    //未知

        public UISprite m_cHeroBorder; //英雄框
        public UISprite m_cHeroFarme;  //英雄底框
        public UISprite m_cHeroIcon;   //英雄头像
        public UISprite m_cHeroCover;  //英雄遮盖
        public UISprite m_cHeroUnknow; //英雄未知
        public Hero m_cHero_; //英雄实例

        public RewardHero(GameObject obj, Hero hero)
        {
            this.m_cHero = obj;

            this.m_cHeroBorder = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cHero, HERO_BORDRE);
            this.m_cHeroFarme = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cHero, HERO_FARME);
            this.m_cHeroIcon = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cHero, HERO_ICON);
            this.m_cHeroCover = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cHero, HERO_COVER);
            this.m_cHeroUnknow = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cHero, HERO_UNKNOW);

            GUI_FUNCTION.SET_HeroBorderAndBack(this.m_cHeroBorder, this.m_cHeroFarme, hero.m_eNature);
            GUI_FUNCTION.SET_AVATORS(this.m_cHeroIcon, hero.m_strAvatarM);
            this.m_cHero_ = hero;
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Destory()
        {
            GameObject.Destroy(this.m_cHero);
            this.m_cHero = null;
            this.m_cHeroBorder = null;
            this.m_cHeroCover = null;
            this.m_cHeroFarme = null;
            this.m_cHeroIcon = null;
            this.m_cHeroUnknow = null;
        }
    }

    /// <summary>
    /// 奖励物品
    /// </summary>
    private class RewardItem
    {
        private const string ICON = "Eq"; //图标
        private const string UNKNOW = "UnKnow";   //未知图标
        private const string COVER = "Cover";    //遮盖
        private const string NUM = "Num";
        private const string BORDER = "Bg";

        public GameObject m_cRewardItem;    //奖励物品
        public UISprite m_cItemIcon;  //物品图标
        public UISprite m_cItemCover;   //物品遮盖
        public UISprite m_cItemUnknow; //未知图标
        public UILabel m_cItemNum;  //物品数量
        public UISprite m_cItemBorder;


        public RewardItem(GameObject obj, int tableID, int num)
        {
            this.m_cRewardItem = obj;
            ItemTable table = ItemTableManager.GetInstance().GetItem(tableID);
            this.m_cItemIcon = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cRewardItem, ICON);
            this.m_cItemCover = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cRewardItem, COVER);
            this.m_cItemUnknow = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cRewardItem, UNKNOW);
            this.m_cItemNum = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cRewardItem, NUM);
            this.m_cItemBorder = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cRewardItem, BORDER);

            GUI_FUNCTION.SET_ITEMM(this.m_cItemIcon, table.SpiritName);
            GUI_FUNCTION.SET_ITEM_BORDER(this.m_cItemBorder, (ITEM_TYPE)table.Type);
            this.m_cItemNum.text = "X" + num;
        }

        public void Destory()
        {
            GameObject.Destroy(this.m_cRewardItem);
            this.m_cRewardItem = null;
            this.m_cItemIcon = null;
            this.m_cItemUnknow = null;
            this.m_cItemCover = null;
        }
    }

    //----------------------------------------------Data--------------------------------------------------

    private string m_strTittle; //标题
    private string m_strContent;    //内容
    private int m_iGoldNum; //获取金币数
    private int m_iFarmNum; //获取农场点数
    private int m_iExpRewardNum;    //获取经验数
    private int m_iExpNowNum;   //当前经验值
    private int m_iLevelNow; //当前等级
    private int m_iExpTargetNum;    //目标经验
    private int m_iExpTargetTotalNum;   //目标总经验
    private RoleExpTable m_cNowRoleTable;    //当前数据表
    private int m_iDiamondGet = 0;  //获得砖石
    private List<int> m_lstItem = new List<int>();  //获得物品列表
    private List<int> m_lstItemNum = new List<int>();   //获得物品数量
    private List<Hero> m_lstHero = new List<Hero>();  //获取英雄列表
    private int m_iHeroShowIndex;   //英雄展示索引
    private int m_iItemShowIndex;   //物品展示索引

    private SHOW_STATE m_eState;    //当前状态

    //临时变量
    private Vector3 OUT_RIGHT_POS = new Vector3(640f, 0, 0);    //外移位置
    private Vector3 OUT_LEFT_POS = new Vector3(-640f, 0, 0);    //外移位置
    private const float TWEEN_POS_TIME = 1F;    //位置位移时间
    private const float SHOW_NUM_TIME = 2F; //展示数字时间
    private const float SHOW_NUM_SPEED = 20F;   //展示数字速度
    private const float SHOW_ITEM_TIME = 1f;    //展示物品速度
    private const float SHOW_ITEM_SHORTTIME = 0.05f;    //展示物品速度
    private const float SHOW_LEVEL_UP_TIME = 4f;  //展示玩家升级时间
    private const float SHOW_BONUS_GET_TIME = 0.8f; //展示bonus时间
    private const int HERO_CURRENT_Y = -10;  //英雄展示起始位置Y
    private const int HERO_OFFSET_Y = 120; //英雄行间距
    private const int ITEM_CURRENT_Y = -10;  //物品展示起始位置Y
    private const int ITEM_OFFSET_Y = 120; //物品展示间距
    private const float UP_OFFSET = 38f; //上升提醒

    protected float m_fClipParentY = 0;   //剪裁父节点Y轴坐标
    protected float m_fClipCenterY = -158;   //剪裁中间点Y轴坐标
    protected float m_fClipSizeY = 430; //剪裁Y轴大小

    private float m_fStateStartTime;    //状态开始时间
    private float m_fCostTime;  //花费时间
    private float SHOW_COST_TIME;   //展示花费时间
    private CALLBACK m_delCallBack; //回调
    private int m_iMaxExpTmp;   //临时最大经验值
    private int m_iMinExpTmp;   //临时最小经验值

    private float m_fModelShowTime = 2f;  //播放模型时间
    private const float m_fHeroDetailShowTime = 2f;  //播放英雄详细信息时间
    private const float m_fStar2 = 1.5f;
    private const float m_fStar3 = 1.5f;
    private const float m_fStar4 = 2.8f;
    private const float m_fStar5 = 3;
    private float m_fdisStar;
    private float m_fdis;  //播放控制
    private bool m_bHasKeep = false;  //设置过keep状态

    private enum SHOW_STATE
    { 
        START_BEGIN = 0,    //开始
        START = 1,
        START_END,
        GOLD_SHOW1_BEGIN,   //金币移入
        GOLD_SHOW1,
        GOLD_SHOW1_END,
        GOLD_SHOW2_BEGIN,   //金币数字滚动
        GOLD_SHOW2,
        GOLD_SHOW2_END,
        FARM_SHOW1_BEGIN,   //农场点移入
        FARM_SHOW1,
        FARM_SHOW1_END,
        FARM_SHOW2_BEGIN,   //农场点数字滚动
        FARM_SHOW2,
        FARM_SHOW2_END,
        EXP_SHOW1_BEGIN,    //经验移入
        EXP_SHOW1,
        EXP_SHOW1_END,
        EXP_SHOW2_BEGIN,    //经验数字滚动
        EXP_SHOW2,
        EXP_SHOW2_END,
        LEVEL_SHOW_BEGIN,   //升级特效开始
        LEVEL_SHOW,
        LEVEL_SHOW_END,
        WAIT_FOR_CLICK, //等待点击
        NUM_SHOW_END_BEGIN, //数字移出
        NUM_SHOW_END,
        NUM_SHOW_END_END,
        HERO_SHOW_BEGIN,    //英雄展示
        HERO_SHOW_SLIDE_END , //英雄滑入结束
        HERO_SHOW1,
        HERO_SHOW2,
        Begin,                //网络接口返回得到英雄以后，召唤动画开始
        ModelShowBegin,       //3D模型展示
        ModelShowIng,
        ModelShowEnd,
        End,                  //结束
        HERO_DETAIL_SHOW,
        HERO_SHOW_END1,
        HERO_SHOW_END2,
        ITEM_SHOW_BEGIN,    //物品展示
        ITEM_SHOW_SLIDE_END,  //物品滑入结束
        ITEM_SHOW1,
        ITEM_SHOW2,
        ITEM_SHOW_END_WAIT,
        ITEM_SHOW_END,
        DIAMOND_SHOW_BEGIN,  //获得砖石特效
        DIAMOND_SHOW,
        DIAMOND_NUM_SHOW_BEGIN, //获得砖石数字提示滑入
        DIAMOND_NUM_SHOW,
        DIAMOND_NUM_SHOW_END,
        DIAMOND_SHOW_END,
        END_BEGIN,  //结束处理
        END,
        END_END,
    }

    public GUIBattleReward(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_BATTLE_REWARD, GUILAYER.GUI_PANEL1)
    { 
        //
    }


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
			ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_HERO_ICON);
			ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_ITEM_ICON);
			ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + EFFECT_BONUS_GET);
			ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + EFFECT_LEVEL_UP);

            if (m_lstNewHero.Count != 0)
            {
				ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + MODEL_BG);        //加载特效
				ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + EFFECT_STAR_2);  //加载特效
				ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + EFFECT_STAR_3);  //加载特效
				ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + EFFECT_STAR_4);  //加载特效
				ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + EFFECT_STAR_5);  //加载特效
				ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + EFFECT_MOFA_SELF);  //加载特效
				ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + EFFECT_STAR_5_BLACK);  //加载特效

                foreach (int heroID in m_lstNewHero)
                {
                    HeroTable heroTable1 = HeroTableManager.GetInstance().GetHeroTable(heroID);
					ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_TEX_PATH + heroTable1.AvatorARes);
					ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_MODEL_PATH + heroTable1.Modle);
                }

            }
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
            //主资源
            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GUI_FINDATION.FIND_GAME_OBJECT(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;
            //英雄和物品图标资源
			this.m_cResHeroIcon = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_HERO_ICON);
			this.m_cResItemIcon = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_ITEM_ICON);
            //中部数字显示父节点
            this.m_cMiddleParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_NUM_PARENT);
            //金币
            this.m_cGoldNumParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_GOLD_NUM_PARENT);
            this.m_cGoldNum = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, GUI_GOLD_NUM);
            //农场点
            this.m_cFarmNumParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_FARM_NUM_PARENT);
            this.m_cFarmNum = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, GUI_FARM_NUM);
            //经验提示父节点
            this.m_cExpParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_EXP_PARENT);
            this.m_cExpRewardNum = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, GUI_EXP_REWARD_NUM);
            this.m_cExpNum = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, GUI_EXP_NUM);
            //经验条对象
            this.m_cExpBar = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_EXP_OBJ);
            this.m_cSpBar = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, GUI_EXP_BAR);
            //获得英雄
            this.m_cHeroListParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_HERO_LISTPARENT);
            this.m_cHeroIconParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_HERO_ICON_PARENT);
            this.m_cHeroListView = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_HERO_LISTVIEW);
            this.m_cHeroListView.AddComponent<GUIComponentEvent>().AddIntputDelegate(OnBackGround);
            this.m_cHeroGet = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_HERO_GET);
            //新英雄
            this.m_cNewHero = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_NEWHERO);
            this.m_cNewHero.AddComponent<GUIComponentEvent>().AddIntputDelegate(ShowNext);
            this.m_cNewHeroInfo = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, GUI_NEWHERO_INFO);
            this.m_cNewHero.SetActive(false);
            //获得物品
            this.m_cItemIconParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_ITEM_ICON_PARENT);
            this.m_cItemListView = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_ITEM_LISTVIEW);
            this.m_cItemListView.AddComponent<GUIComponentEvent>().AddIntputDelegate(OnBackGround);
            this.m_cItemGet = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_ITEM_GET);
            //背景遮罩
            this.m_cBackGroundBtn = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_BACK_GROUND);
            GUIComponentEvent ce = this.m_cBackGroundBtn.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnBackGround);
            //标题
            this.m_cTitleParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_TOP_CONTENT);
            this.m_cDungeonName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, GUI_DUNGEON_NAME);
            this.m_cGateName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, GUI_GATE_NAME);
            //结算标题
            this.m_cGUITitle = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject , GUI_TITLE);
            //底部上升
            this.m_cGUILevelUpNumParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_LEVEL_UP_NUM_PARENT);
            //等级上升
            this.m_cGUILevelUpNumNowLevel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUILevelUpNumParent, GUI_LEVEL_UP_NUM_NOWLEVE);
            this.m_cGUILevelUpNumObj = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUILevelUpNumParent, GUI_LEVEL_UP_NUM_OBJ_LV);
            //体力上升
            this.m_cGUILevelUpNumStrengthObj = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUILevelUpNumParent, GUI_LEVEL_UP_NUM_OBJ_STRENGTH);
            this.m_cGUILevelUpNumNowStrength = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUILevelUpNumParent, GUI_LEVEL_UP_NUM_INT_STRENGTH);
            //Cost上升
            this.m_cGUILevelUpNumCostObj = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUILevelUpNumParent, GUI_LEVEL_UP_NUM_OBJ_COST);
            this.m_cGUILevelUpNumNowCost = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUILevelUpNumParent, GUI_LEVEL_UP_NUM_NOW_COST);
            //好友上升
            this.m_cGUILevelUpNumNowFriend = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUILevelUpNumParent, GUI_LEVEL_UP_NUM_INT_FRIEND);
            this.m_cGUILevelUpNumFriendObj = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUILevelUpNumParent, GUI_LEVEL_UP_NUM_OBJ_FRIEND);
            //体力全部恢复
            this.m_cGUILevelUpNumNowMaxStrenght = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUILevelUpNumParent, GUI_LEVEL_UP_NUM_OBJ_FULL);
            //获得Bonus额外奖励
            this.m_cDiamondGet = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_Diamond_GET);
            this.m_cPanDiamondGet = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_Diamond_GET_OBJ);
            this.m_cLbDiamdonGetNum = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cGUIObject, GUI_Diamond_Get_INT);
            this.m_cDiamondGetInfo = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_Diamond_GET_INFO);
            //3D特效
            this.m_cGuiEffect = GUI_FINDATION.FIND_GAME_OBJECT(GUI_EFFECT);
            this.m_cEffectParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGuiEffect, EFFECT_CENTER_ANCHOR);
			this.m_cEffectBonusGet = (UnityEngine.Object)ResourceMgr.LoadAsset(EFFECT_BONUS_GET);
			this.m_cEffectLevelUp = (UnityEngine.Object)ResourceMgr.LoadAsset(EFFECT_LEVEL_UP);

            if (m_lstNewHero.Count != 0)
            {
                this.m_cSceneRoot = GUI_FINDATION.FIND_GAME_OBJECT(HERO_ZHAOHUAN_ROOT);
                this.m_cStayPos = GUI_FINDATION.GET_GAME_OBJECT(this.m_cSceneRoot, HERO_STAY_POS);
                this.m_cDoorBg = GUI_FINDATION.GET_GAME_OBJECT(this.m_cSceneRoot, DOOR_BG);
                this.m_cZhaoHuanBg = GUI_FINDATION.GET_GAME_OBJECT(this.m_cSceneRoot, ZhaoHuan_BG);
            }
        }

        this.m_cLbDiamdonGetNum.text = "#35d966]" + this.m_iDiamondGet.ToString();
        this.m_cDungeonName.text = this.m_strTittle;
        this.m_cGateName.text = this.m_strContent;

        this.m_eState = SHOW_STATE.START_BEGIN;

        this.m_cMiddleParent.transform.localPosition = Vector3.one * 0xFFFF;
        this.m_cGoldNumParent.transform.localPosition = Vector3.one * 0xFFFF;
        this.m_cFarmNumParent.transform.localPosition = Vector3.one * 0xFFFF;
        this.m_cExpParent.transform.localPosition = Vector3.one * 0xFFFF;
        this.m_cExpBar.transform.localPosition = Vector3.one * 0xFFFF;
        this.m_cHeroIconParent.transform.localPosition = Vector3.one * 0xFFFF;
        this.m_cItemIconParent.transform.localPosition = Vector3.one * 0xFFFF;
        this.m_cDiamondGet.transform.localPosition = Vector3.one * 0xFFFF;
        this.m_cHeroGet.transform.localPosition = Vector3.one * 0xFFFF;
        this.m_cItemGet.transform.localPosition = Vector3.one * 0xFFFF;
        this.m_cPanDiamondGet.transform.localPosition = Vector3.one * 0xFFFF;
        this.m_cGUILevelUpNumParent.SetActive(false);
        SetLocalPos(Vector3.zero);
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        base.Hiden();
        for (int i = 0; i < this.m_lstHeroIcon.Count; i++)
        {
            this.m_lstHeroIcon[i].Destory();
        }
        this.m_lstHero.Clear();
        this.m_lstHeroIcon.Clear();
        for (int i = 0; i < this.m_lstItemIcon.Count; i++)
        {
            this.m_lstItemIcon[i].Destory();
        }
        this.m_lstItem.Clear();
        this.m_lstItemNum.Clear();
        this.m_lstItemIcon.Clear();

        CameraManager.GetInstance().HidenGUIEffectCamera();  //关闭特效摄像头

        //SetLocalPos(Vector3.one * 0xFFFF);

        Destory();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        for (int i = 0; i < this.m_lstHeroIcon.Count; i++)
        {
            this.m_lstHeroIcon[i].Destory();
        }
        this.m_lstHero.Clear();
        this.m_lstHeroIcon.Clear();
        for (int i = 0; i < this.m_lstItemIcon.Count; i++)
        {
            this.m_lstItemIcon[i].Destory();
        }
        this.m_lstItem.Clear();
        this.m_lstItemNum.Clear();
        this.m_lstItemIcon.Clear();
        this.m_lstNewHero.Clear();

        //销毁资源
        this.m_cEffectLevelUp = null;
        this.m_cEffectBonusGet = null;
        this.m_cResHeroIcon = null;
        this.m_cResItemIcon = null;

        //销毁引用
        //中部数字父节点
        this.m_cMiddleParent = null;
        //金币
        this.m_cGoldNumParent = null;
        this.m_cGoldNum = null;
        //农场点
        this.m_cFarmNumParent = null;
        this.m_cFarmNum = null;
        //经验字符
        this.m_cExpParent = null;
        this.m_cExpRewardNum = null;
        this.m_cExpNum = null;
        //经验条
        this.m_cSpBar = null;
        this.m_cExpBar = null;
        //英雄获得
        this.m_cHeroGet = null;
        this.m_cHeroListView = null;
        this.m_cHeroIconParent = null;
        //物品获得
        this.m_cItemGet = null;
        this.m_cItemListView = null;
        this.m_cItemIconParent = null;
        //背景
        this.m_cBackGroundBtn = null;
        //标题
        this.m_cTitleParent = null;
        this.m_cDungeonName = null;
        this.m_cGateName = null;
        //底部
        this.m_cGUILevelUpNumParent = null;
        //升级
        this.m_cGUILevelUpNumObj = null;
        this.m_cGUILevelUpNumNowLevel = null;
        //体力上升
        this.m_cGUILevelUpNumStrengthObj = null;
        this.m_cGUILevelUpNumNowStrength = null;
        //Cost上升
        this.m_cGUILevelUpNumCostObj = null;
        this.m_cGUILevelUpNumNowCost = null;
        //好友上升
        this.m_cGUILevelUpNumFriendObj = null;
        this.m_cGUILevelUpNumNowFriend = null;
        //体力全部恢复
        this.m_cGUILevelUpNumNowMaxStrenght = null;
        //获得Bonus额外奖励
        this.m_cDiamondGet = null;
        this.m_cPanDiamondGet = null;
        this.m_cDiamondGetInfo = null;
        this.m_cLbDiamdonGetNum = null;

        this.m_cGuiEffect = null;
        this.m_cEffectParent = null;
        this.m_cEffectObjLevelUp = null;
        this.m_cEffectObjBonusGet = null;

        //销毁特效
        GameObject.DestroyImmediate(m_cModelBg);
        GameObject.DestroyImmediate(m_cMofaSelf);
        GameObject.DestroyImmediate(this.m_cEffectStar2);
        GameObject.DestroyImmediate(this.m_cEffectStar3);
        GameObject.DestroyImmediate(this.m_cEffectStar4);
        GameObject.DestroyImmediate(this.m_cEffectStar5);
        GameObject.DestroyImmediate(this.m_cEffectStar5Black);
        if (this.m_cGfxHero != null)
            this.m_cGfxHero.Destory();

        m_cLbInfo = null;  //英雄台词
        m_iReusltHero = null;   //展示接口返回的英雄ID
        m_cEffectStar2 = null; //2星及以下特效
        m_cEffectStar3 = null; //3星特效
        m_cEffectStar4 = null; //4星特效
        m_cEffectStar5 = null; //5星特效
        m_cEffectStar5Black = null; //五星模型进入特效
        m_cEffectStarStr2 = null; //2星及以下字特效
        m_cEffectStarStr3 = null; //3星字特效
        m_cEffectStarStr4 = null; //4星字特效
        m_cEffectStarStr5 = null; //5星字特效
        m_cZhaoHuanBg = null;  //召唤背景
        m_cSceneRoot = null;    //场景根节点
        m_cStayPos = null;  //站立点
        m_cGfxHero = null;   //渲染实例
        m_cModelBg = null; //3D背景
        m_cModelMaterial1 = null;
        m_cModelMaterial2 = null;
        m_cModelMaterial3 = null;
        m_cMofaSelf = null;  //魔法底盘
        m_cDoorBg = null;  //门特效背景

        base.Destory();
    }

    /// <summary>
    /// 更新
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

        base.Update();

        if (!IsShow()) return false;

        //Debug.Log(this.m_eState);
        switch (this.m_eState)
        {
            case SHOW_STATE.START_BEGIN:    //开始
                this.m_cMiddleParent.transform.localPosition = Vector3.zero;
                this.m_cGoldNumParent.transform.localPosition = Vector3.one * 0xFFFF;
                this.m_cFarmNumParent.transform.localPosition = Vector3.one * 0xFFFF;
                this.m_cExpParent.transform.localPosition = Vector3.one * 0xFFFF;
                this.m_cExpBar.transform.localPosition = Vector3.one * 0xFFFF;
                this.m_cHeroIconParent.transform.localPosition = Vector3.one * 0xFFFF;
                this.m_cItemIconParent.transform.localPosition = Vector3.one * 0xFFFF;
                this.m_cDiamondGet.transform.localPosition = Vector3.one * 0xFFFF;
                this.m_cHeroGet.transform.localPosition = Vector3.one * 0xFFFF;
                this.m_cItemGet.transform.localPosition = Vector3.one * 0xFFFF;
                this.m_cPanDiamondGet.transform.localPosition = Vector3.one * 0xFFFF;
                this.m_cGUILevelUpNumParent.SetActive(false);
                this.m_eState++;
                break;
            case SHOW_STATE.START:
                this.m_eState++;
                break;
            case SHOW_STATE.START_END:
                this.m_eState++;
                break;
            case SHOW_STATE.GOLD_SHOW1_BEGIN:   //金币移入
                this.m_cGoldNum.text = "" + 0;
                CTween.TweenPosition(this.m_cGoldNumParent, TWEEN_POS_TIME, OUT_RIGHT_POS, Vector3.zero);
                this.m_fStateStartTime = GAME_TIME.TIME_FIXED();
                this.m_fCostTime = TWEEN_POS_TIME;
                this.m_eState++;
                break;
            case SHOW_STATE.GOLD_SHOW1:
                float disTime = GAME_TIME.TIME_FIXED() - this.m_fStateStartTime;
                if (disTime > this.m_fCostTime)
                    this.m_eState++;
                break;
            case SHOW_STATE.GOLD_SHOW1_END:
                this.m_eState++;
                break;
            case SHOW_STATE.GOLD_SHOW2_BEGIN:   //金币数字滚动
                this.m_fStateStartTime = GAME_TIME.TIME_FIXED();
                this.m_fCostTime = SHOW_NUM_TIME;
                if (this.m_iGoldNum / SHOW_NUM_SPEED < SHOW_NUM_TIME)
                    this.m_fCostTime = this.m_iGoldNum / SHOW_NUM_SPEED;
                this.m_eState++;
                break;
            case SHOW_STATE.GOLD_SHOW2:
                disTime = GAME_TIME.TIME_FIXED() - this.m_fStateStartTime;
                if (disTime > this.m_fCostTime)
                {
					MediaMgr.sInstance.StopENV();
//                    MediaMgr.StopSoundContinue();
                    this.m_cGoldNum.text = "" + this.m_iGoldNum;
                    this.m_eState++;
                }
                else
                {
                    //数字跳动音效
					MediaMgr.sInstance.PlayENV(SOUND_DEFINE.SE_NUM_JUMP);
//                    MediaMgr.PlaySoundContinue(SOUND_DEFINE.SE_NUM_JUMP);
                    this.m_cGoldNum.text = "" + (int)(Mathf.Lerp(0, this.m_iGoldNum, disTime / this.m_fCostTime));
                }
                break;
            case SHOW_STATE.GOLD_SHOW2_END:
                this.m_eState++;
                break;
            case SHOW_STATE.FARM_SHOW1_BEGIN:   //农场点移入
                this.m_cFarmNum.text = "" + 0;
                CTween.TweenPosition(this.m_cFarmNumParent, TWEEN_POS_TIME, OUT_RIGHT_POS, Vector3.zero);
                this.m_fStateStartTime = GAME_TIME.TIME_FIXED();
                this.m_fCostTime = TWEEN_POS_TIME;
                this.m_eState++;
                break;
            case SHOW_STATE.FARM_SHOW1:
                disTime = GAME_TIME.TIME_FIXED() - this.m_fStateStartTime;
                if (disTime > this.m_fCostTime)
                    this.m_eState++;
                break;
            case SHOW_STATE.FARM_SHOW1_END:
                this.m_eState++;
                break;
            case SHOW_STATE.FARM_SHOW2_BEGIN:   //农场点数字滚动
                this.m_fStateStartTime = GAME_TIME.TIME_FIXED();
                this.m_fCostTime = SHOW_NUM_TIME;
                if (this.m_iFarmNum / SHOW_NUM_SPEED < SHOW_NUM_TIME)
                    this.m_fCostTime = this.m_iFarmNum / SHOW_NUM_SPEED;
                this.m_eState++;
                break;
            case SHOW_STATE.FARM_SHOW2:
                disTime = GAME_TIME.TIME_FIXED() - this.m_fStateStartTime;
                if (disTime > this.m_fCostTime)
                {
					MediaMgr.sInstance.StopENV();
//                    MediaMgr.StopSoundContinue();
                    this.m_cFarmNum.text = "" + this.m_iFarmNum;
                    this.m_eState++;
                }
                else
                {
                    //数字跳动音效
					MediaMgr.sInstance.PlayENV(SOUND_DEFINE.SE_NUM_JUMP);
//                    MediaMgr.PlaySoundContinue(SOUND_DEFINE.SE_NUM_JUMP);
                    this.m_cFarmNum.text = "" + (int)(Mathf.Lerp(0, this.m_iFarmNum, disTime / this.m_fCostTime));
                }
                break;
            case SHOW_STATE.FARM_SHOW2_END:
                this.m_eState++;
                break;
            case SHOW_STATE.EXP_SHOW1_BEGIN:    //经验移入
                this.m_iMaxExpTmp = RoleExpTableManager.GetInstance().GetMaxExp(this.m_cNowRoleTable.Level);
                this.m_iMinExpTmp = RoleExpTableManager.GetInstance().GetMinExp(this.m_cNowRoleTable.Level);
                this.m_cExpRewardNum.text = this.m_iExpRewardNum.ToString();
                this.m_cExpNum.text = (this.m_iMaxExpTmp - this.m_iExpNowNum).ToString();
                this.m_iExpTargetNum = this.m_iExpTargetTotalNum;
                if (this.m_iExpTargetNum > this.m_iMaxExpTmp)
                    this.m_iExpTargetNum = this.m_iMaxExpTmp;
                this.m_cExpBar.transform.localPosition = Vector3.one;
                this.m_cSpBar.fillAmount = (this.m_iExpNowNum - this.m_iMinExpTmp) * 1f / (this.m_iMaxExpTmp - this.m_iMinExpTmp);
                CTween.TweenPosition(this.m_cExpParent, TWEEN_POS_TIME, OUT_RIGHT_POS, Vector3.zero);
                this.m_fStateStartTime = GAME_TIME.TIME_FIXED();
                this.m_fCostTime = TWEEN_POS_TIME;
                this.m_eState++;
                break;
            case SHOW_STATE.EXP_SHOW1:
                disTime = GAME_TIME.TIME_FIXED() - this.m_fStateStartTime;
                if (disTime > this.m_fCostTime)
                    this.m_eState++;
                break;
            case SHOW_STATE.EXP_SHOW1_END:
                this.m_eState++;
                break;
            case SHOW_STATE.EXP_SHOW2_BEGIN:    //经验数字滚动
                this.m_iMaxExpTmp = RoleExpTableManager.GetInstance().GetMaxExp(this.m_cNowRoleTable.Level);
                this.m_iMinExpTmp = RoleExpTableManager.GetInstance().GetMinExp(this.m_cNowRoleTable.Level);

                this.m_iExpTargetNum = this.m_iMaxExpTmp;
                if (this.m_iExpTargetNum > this.m_iExpTargetTotalNum)
                    this.m_iExpTargetNum = this.m_iExpTargetTotalNum;
                this.m_cSpBar.fillAmount = (this.m_iExpNowNum - this.m_iMinExpTmp) * 1f / (this.m_iMaxExpTmp - this.m_iMinExpTmp);
                this.m_fStateStartTime = GAME_TIME.TIME_FIXED();
                this.m_fCostTime = SHOW_NUM_TIME;
                if ((this.m_iExpTargetNum - this.m_iExpNowNum) / SHOW_NUM_SPEED < SHOW_NUM_TIME)
                    this.m_fCostTime = (this.m_iExpTargetNum - this.m_iExpNowNum) / SHOW_NUM_SPEED;
                this.m_eState++;
                break;
            case SHOW_STATE.EXP_SHOW2:
                //数字跳动音效
				MediaMgr.sInstance.PlayENV(SOUND_DEFINE.SE_NUM_JUMP);
//                MediaMgr.PlaySoundContinue(SOUND_DEFINE.SE_NUM_JUMP);
                disTime = GAME_TIME.TIME_FIXED() - this.m_fStateStartTime;
                if (disTime > this.m_fCostTime)
                {
                    //数字跳动音效关闭
					MediaMgr.sInstance.StopENV();
//                    MediaMgr.StopSoundContinue();
                    this.m_cExpNum.text = "" + (this.m_iMaxExpTmp - this.m_iExpTargetNum);
                    this.m_cSpBar.fillAmount = (this.m_iExpTargetNum - this.m_iMinExpTmp) * 1f / (this.m_iMaxExpTmp - this.m_iMinExpTmp);
                    this.m_eState++;
                }
                else
                {
                    float tmpExp = Mathf.Lerp(this.m_iExpNowNum, this.m_iExpTargetNum, disTime / this.m_fCostTime);
                    this.m_cExpNum.text = "" + (this.m_iMaxExpTmp - (int)tmpExp);
                    this.m_cSpBar.fillAmount = (tmpExp - this.m_iMinExpTmp) / (this.m_iMaxExpTmp - this.m_iMinExpTmp);
                }
                break;
            case SHOW_STATE.EXP_SHOW2_END:
                this.m_iExpNowNum = this.m_iExpTargetNum;
                this.m_eState++;
                break;
            case SHOW_STATE.LEVEL_SHOW_BEGIN:   //升级特效开始
                if (this.m_iExpNowNum >= this.m_iMaxExpTmp)
                {
                    this.m_iLevelNow++;
                    //最大等级控制
                    if (this.m_iLevelNow >= RoleExpTableManager.GetInstance().GetMaxLevel())
                    {
                        this.m_eState = SHOW_STATE.WAIT_FOR_CLICK;
                        break;
                    }

                    this.m_cGUILevelUpNumParent.SetActive(false);
                    //提示字符偏移量，如果cost是0，那么下面的提示字符会往上移动对齐
                    int offest = 0;
                    //升级等级
                    this.m_cGUILevelUpNumNowLevel.text = "Lv." + this.m_iLevelNow;
                    //最大体力增量
                    int reStrength = (RoleExpTableManager.GetInstance().GetMaxStrength(this.m_iLevelNow) - RoleExpTableManager.GetInstance().GetMaxStrength(this.m_iLevelNow - 1));
                    if (reStrength > 0)
                    {
                        this.m_cGUILevelUpNumStrengthObj.transform.localPosition += new Vector3(0, UP_OFFSET * offest, 0);
                        this.m_cGUILevelUpNumNowStrength.text = @"#35d966]" + reStrength;
                        this.m_cGUILevelUpNumStrengthObj.SetActive(true);
                    }
                    else
                    {
                        this.m_cGUILevelUpNumStrengthObj.SetActive(false);
                        offest++;
                    }
                    //最大好友数增量增量
                    int reFriend = (RoleExpTableManager.GetInstance().GetMaxFriend(this.m_iLevelNow) - RoleExpTableManager.GetInstance().GetMaxFriend(this.m_iLevelNow - 1));
                    if (reFriend > 0)
                    {
                        this.m_cGUILevelUpNumFriendObj.transform.localPosition += new Vector3(0, UP_OFFSET * offest, 0);
                        this.m_cGUILevelUpNumNowFriend.text = @"#35d966]" + reFriend;
                        this.m_cGUILevelUpNumFriendObj.SetActive(true);
                    }
                    else
                    {
                        this.m_cGUILevelUpNumFriendObj.SetActive(false);
                        offest++;
                    }
                    //cost增量
                    int reCost = (RoleExpTableManager.GetInstance().GetMaxCost(this.m_iLevelNow) - RoleExpTableManager.GetInstance().GetMaxCost(this.m_iLevelNow - 1));
                    if (reCost > 0)
                    {

                        this.m_cGUILevelUpNumCostObj.transform.localPosition += new Vector3(0, UP_OFFSET * offest, 0);
                        this.m_cGUILevelUpNumNowCost.text = @"#35d966]" + reCost;
                        this.m_cGUILevelUpNumCostObj.SetActive(true);
                    }
                    else
                    {
                        this.m_cGUILevelUpNumCostObj.SetActive(false);
                        offest++;
                    }
                    //全部恢复
                    this.m_cGUILevelUpNumNowMaxStrenght.transform.localPosition += new Vector3(0, UP_OFFSET * offest, 0);
                    this.m_cGUILevelUpNumNowMaxStrenght.SetActive(true);
                    //显示上升父节点
                    this.m_cGUILevelUpNumParent.SetActive(true);
                    //展示升级特效
                    CameraManager.GetInstance().ShowGUIEffectCamera();  //开启特效摄像头
                    m_cEffectObjLevelUp = GameObject.Instantiate(this.m_cEffectLevelUp) as GameObject;
                    m_cEffectObjLevelUp.transform.parent = this.m_cEffectParent.transform;
                    m_cEffectObjLevelUp.transform.localScale = Vector3.one;
                    m_cEffectObjLevelUp.transform.localPosition = Vector3.zero;
                    //升级音效
					MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_UPGRADE);
//                    MediaMgr.PlaySound(SOUND_DEFINE.SE_UPGRADE);
                    //设置时间
                    this.m_eState++;
                }
                else
                {
                    this.m_eState = SHOW_STATE.WAIT_FOR_CLICK;
                }
                break;
            case SHOW_STATE.LEVEL_SHOW:
     
                //等待点击
                
                break;
            case SHOW_STATE.LEVEL_SHOW_END:
                //关闭升级特效
                GameObject.DestroyImmediate(m_cEffectObjLevelUp);
                CameraManager.GetInstance().HidenGUIEffectCamera();

                this.m_cGUILevelUpNumStrengthObj.transform.localPosition = Vector3.zero;
                this.m_cGUILevelUpNumCostObj.transform.localPosition = Vector3.zero;
                this.m_cGUILevelUpNumFriendObj.transform.localPosition = Vector3.zero;
                this.m_cGUILevelUpNumNowMaxStrenght.transform.localPosition = Vector3.zero;

                this.m_cGUILevelUpNumStrengthObj.SetActive(false);
                this.m_cGUILevelUpNumCostObj.SetActive(false);
                this.m_cGUILevelUpNumFriendObj.SetActive(false);
                this.m_cGUILevelUpNumNowMaxStrenght.SetActive(false);

                this.m_cGUILevelUpNumParent.SetActive(false);
                if (this.m_iExpNowNum < this.m_iExpTargetTotalNum)
                {
                    this.m_cNowRoleTable = RoleExpTableManager.GetInstance().GetRoleLevelByExp(this.m_iExpNowNum);
                    this.m_eState = SHOW_STATE.EXP_SHOW2_BEGIN;
                }
                else
                {
                    this.m_eState++;
                }
                break;
            case SHOW_STATE.WAIT_FOR_CLICK ://等待点击
                break;
            case SHOW_STATE.NUM_SHOW_END_BEGIN://数字移出                
                CTween.TweenPosition(this.m_cMiddleParent, TWEEN_POS_TIME, OUT_LEFT_POS);
                this.m_fStateStartTime = GAME_TIME.TIME_FIXED();
                this.m_fCostTime = TWEEN_POS_TIME;
                //翻页音效
				MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_SLIDE);
//                MediaMgr.PlaySound(SOUND_DEFINE.SE_SLIDE);
                this.m_eState++;
                break;
            case SHOW_STATE.NUM_SHOW_END:                
                disTime = GAME_TIME.TIME_FIXED() - this.m_fStateStartTime;
                if (disTime > this.m_fCostTime)
                {
                    this.m_eState++;
                }
                break;
            case SHOW_STATE.NUM_SHOW_END_END:
                this.m_eState++;
                break;
            case SHOW_STATE.HERO_SHOW_BEGIN:    //英雄展示
                this.SHOW_COST_TIME = SHOW_ITEM_TIME;
                if (this.m_lstHero.Count <= 0)
                {
                    this.m_eState = SHOW_STATE.ITEM_SHOW_BEGIN;
                    break;
                }

                for (int i = 0; i < this.m_lstHero.Count; i++)
                {
                    GameObject obj = GameObject.Instantiate(this.m_cResHeroIcon) as GameObject;
                    obj.transform.parent = this.m_cHeroListView.transform;
                    obj.transform.localPosition = new Vector3(i % 5 * 120, HERO_CURRENT_Y + i / 5 * -HERO_OFFSET_Y, 0);
                    obj.transform.localScale = Vector3.one;
                    RewardHero rewardHero = new RewardHero(obj, this.m_lstHero[i]);

                    rewardHero.m_cHeroBorder.enabled = false;
                    rewardHero.m_cHeroCover.enabled = false;
                    rewardHero.m_cHeroFarme.enabled = false;
                    rewardHero.m_cHeroIcon.enabled = false;

                    this.m_lstHeroIcon.Add(rewardHero);
                }
                UIDraggablePanel dpan2 = m_cHeroIconParent.GetComponent<UIDraggablePanel>();
                dpan2.repositionClipping = true;
                this.m_iHeroShowIndex = 0;
                this.m_cHeroIconParent.transform.localPosition = new Vector3(640, 0, 0);
                this.m_cHeroGet.transform.localPosition = new Vector3(-640, 0, 0);
                CTween.TweenPosition(this.m_cHeroIconParent, TWEEN_POS_TIME, Vector3.zero);
                CTween.TweenPosition(this.m_cHeroGet, TWEEN_POS_TIME, Vector3.zero);
                this.m_fStateStartTime = GAME_TIME.TIME_FIXED();
                this.m_fCostTime = TWEEN_POS_TIME;
                this.m_eState++;
                break;
            case SHOW_STATE.HERO_SHOW_SLIDE_END:
                disTime = GAME_TIME.TIME_FIXED() - this.m_fStateStartTime;
                if (disTime > this.m_fCostTime)
                {
                    this.m_eState++;
                }
                break;
            case SHOW_STATE.HERO_SHOW1:
                this.m_lstHeroIcon[this.m_iHeroShowIndex].m_cHeroUnknow.enabled = false;
                this.m_lstHeroIcon[this.m_iHeroShowIndex].m_cHeroBorder.enabled = true;
                this.m_lstHeroIcon[this.m_iHeroShowIndex].m_cHeroCover.enabled = true;
                this.m_lstHeroIcon[this.m_iHeroShowIndex].m_cHeroFarme.enabled = true;
                this.m_lstHeroIcon[this.m_iHeroShowIndex].m_cHeroIcon.enabled = true;

                this.m_fCostTime = this.SHOW_COST_TIME;
                this.m_fStateStartTime = GAME_TIME.TIME_FIXED();
                this.m_eState++;
                break;
            case SHOW_STATE.HERO_SHOW2:
                disTime = GAME_TIME.TIME_FIXED() - this.m_fStateStartTime;
                if (disTime >= this.m_fCostTime)
                {
                    this.m_lstHeroIcon[this.m_iHeroShowIndex].m_cHeroCover.alpha = 0;
                    //判断是否为新英雄
                    if (m_lstNewHero.Contains(this.m_lstHeroIcon[this.m_iHeroShowIndex].m_cHero_.m_iTableID))
                    {
                        this.m_cHeroListParent.SetActive(false);
                        this.m_cTitleParent.SetActive(false);
                        this.m_cBackGroundBtn.SetActive(false);
                        this.m_cGUITitle.SetActive(false);
                        this.m_iReusltHero = this.m_lstHeroIcon[this.m_iHeroShowIndex].m_cHero_;
                        m_lstNewHero.Remove(this.m_lstHeroIcon[this.m_iHeroShowIndex].m_cHero_.m_iTableID);
                        this.m_eState++;
                        break;
                    }
       
                    this.m_iHeroShowIndex++;
                    if (this.m_iHeroShowIndex >= this.m_lstHeroIcon.Count)
                    {
                        this.m_eState = SHOW_STATE.HERO_SHOW_END1;
                        break;
                    }

                    int index = this.m_iHeroShowIndex / 5;
                    if (index >= 3)
                    {
                        UIPanel pan = m_cHeroIconParent.GetComponent<UIPanel>();
                        pan.transform.localPosition = new Vector3(pan.transform.localPosition.x, this.m_fClipParentY + (index - 2) * HERO_OFFSET_Y, pan.transform.localPosition.z);
                        pan.clipRange = new Vector4(pan.clipRange.x, this.m_fClipCenterY - (index - 2) * HERO_OFFSET_Y, pan.clipRange.z, this.m_fClipSizeY);
                    }

                    this.m_eState--;
                    
                }
                else
                {
                    this.m_lstHeroIcon[this.m_iHeroShowIndex].m_cHeroCover.alpha = Mathf.Lerp(1, 0, disTime / this.m_fCostTime);
                }
                break;
            case SHOW_STATE.Begin:
                //隐藏背景，隐藏结算界面
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Hiden();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Hiden();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Hiden();

                //打开模型展示摄像头
                CameraManager.GetInstance().ShowUIHeroZhaoHuan2Camera();
                //全屏点击无效
                this.m_cNewHero.collider.enabled = false;
                this.m_cDoorBg.SetActive(false);
                m_eState++;
                break;
            case SHOW_STATE.ModelShowBegin:
                //开启模型展示摄像头
                CameraManager.GetInstance().ShowUIHeroZhaoHuan2Camera();
                //3D模型的背景特效
                HeroTable heroTable = HeroTableManager.GetInstance().GetHeroTable(m_iReusltHero.m_iTableID);
                Texture heroBg = (Texture)ResourceMgr.LoadAsset(heroTable.AvatorARes);
                //台词显示
                this.m_cNewHero.SetActive(true);
                this.m_cNewHeroInfo.text = heroTable.Word;                

                this.m_cModelBg = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(MODEL_BG)) as GameObject;
                this.m_cModelBg.transform.parent = this.m_cSceneRoot.transform;
                this.m_cModelBg.transform.localScale = Vector3.one;
                this.m_cModelBg.transform.localPosition = Vector3.zero;
                this.m_cModelMaterial1 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cModelBg, MATERIALS_HERO_A1);
                this.m_cModelMaterial2 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cModelBg, MATERIALS_HERO_A2);
                this.m_cModelMaterial3 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cModelBg, MATERIALS_HERO_A3);
                this.m_cModelMaterial1.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", heroBg);
                this.m_cModelMaterial2.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", heroBg);
                this.m_cModelMaterial3.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", heroBg);
                //将内存中的模型加载出来
                GameObject heroObj = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(heroTable.Modle)) as GameObject;
                heroObj.transform.parent = this.m_cStayPos.transform;
                heroObj.transform.localScale = Vector3.one;
                heroObj.transform.localPosition = Vector3.zero;
                this.m_cGfxHero = new GfxObject(heroObj);
                heroObj.SetActive(true);
                //英雄底盘
                this.m_cMofaSelf = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(EFFECT_MOFA_SELF)) as GameObject;
                this.m_cMofaSelf.transform.parent = this.m_cStayPos.transform;
                this.m_cMofaSelf.transform.localScale = Vector3.one;
                this.m_cMofaSelf.transform.localPosition = Vector3.zero;

                //将异步内存中的星级特效加载出来
                switch (heroTable.Star)
                {
                    case 1:
                    case 2:
                        //1星2星显示猛将特效
                        this.m_cEffectStar2 = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(EFFECT_STAR_2)) as GameObject;
                        this.m_cEffectStar2.transform.parent = this.m_cSceneRoot.transform;
                        this.m_cEffectStar2.transform.localPosition = Vector3.zero;
                        this.m_cEffectStar2.transform.localScale = Vector3.one;
                        this.m_cEffectStar2.SetActive(true);
                        this.m_cEffectStarStr2 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cEffectStar2, EFFECT_GUI_STAR_2);
                        Animation[] m_vecAnim2 = this.m_cEffectStarStr2.GetComponentsInChildren<Animation>();
                        foreach (Animation item in m_vecAnim2)
                        {
                            m_fModelShowTime = m_fStar2;
                            item.wrapMode = WrapMode.Once;
                            item.Play("start");
                        }

                        //音效
						MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_SUMMON_STAR_2);
//                        MediaMgr.PlaySound(SOUND_DEFINE.SE_SUMMON_STAR_2);
                        break;
                    case 3:
                        //3星显示超猛将特效
                        this.m_cEffectStar3 = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(EFFECT_STAR_3)) as GameObject;
                        this.m_cEffectStar3.transform.parent = this.m_cSceneRoot.transform;
                        this.m_cEffectStar3.transform.localPosition = Vector3.zero;
                        this.m_cEffectStar3.transform.localScale = Vector3.one;
                        this.m_cEffectStar3.SetActive(true);
                        this.m_cEffectStarStr3 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cEffectStar3, EFFECT_GUI_STAR_3);
                        Animation[] m_vecAnim3 = this.m_cEffectStarStr3.GetComponentsInChildren<Animation>();
                        foreach (Animation item in m_vecAnim3)
                        {
                            m_fModelShowTime = m_fStar3;
                            item.wrapMode = WrapMode.Once;
                            item.Play("start");
                        }

                        //音效
						MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_SUMMON_STAR_3);
//                        MediaMgr.PlaySound(SOUND_DEFINE.SE_SUMMON_STAR_3);
                        break;
                    case 4:
                        //4星显示超绝猛将特效
                        this.m_cEffectStar4 = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(EFFECT_STAR_4)) as GameObject;
                        this.m_cEffectStar4.transform.parent = this.m_cSceneRoot.transform;
                        this.m_cEffectStar4.transform.localPosition = Vector3.zero;
                        this.m_cEffectStar4.transform.localScale = Vector3.one;
                        this.m_cEffectStar4.SetActive(true);
                        this.m_cEffectStarStr4 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cEffectStar4, EFFECT_GUI_STAR_4);
                        Animation[] m_vecAnim4 = this.m_cEffectStarStr4.GetComponentsInChildren<Animation>();
                        foreach (Animation item in m_vecAnim4)
                        {
                            m_fModelShowTime = m_fStar4;
                            item.wrapMode = WrapMode.Once;
                            item.Play("start");
                        }

                        //音效
						MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_SUMMON_STAR_4);
//                        MediaMgr.PlaySound(SOUND_DEFINE.SE_SUMMON_STAR_4);
                        break;
                    case 5:
                        //5星显示无双猛将特效
                        this.m_cEffectStar5 = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(EFFECT_STAR_5)) as GameObject;
                        this.m_cEffectStar5.transform.parent = this.m_cSceneRoot.transform;
                        this.m_cEffectStar5.transform.localPosition = Vector3.zero;
                        this.m_cEffectStar5.transform.localScale = Vector3.one;
                        this.m_cEffectStar5.SetActive(true);
                        this.m_cEffectStarStr5 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cEffectStar5, EFFECT_GUI_STAR_5);
                        Animation[] m_vecAnim5 = this.m_cEffectStarStr5.GetComponentsInChildren<Animation>();
                        foreach (Animation item in m_vecAnim5)
                        {
                            m_fModelShowTime = m_fStar5;
                            item.wrapMode = WrapMode.Once;
                            item.Play("start");
                        }

                        //音效
						MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_SUMMON_STAR_5);
//                        MediaMgr.PlaySound(SOUND_DEFINE.SE_SUMMON_STAR_5);

                        //5星背景特效
                        this.m_cEffectStar5Black = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(EFFECT_STAR_5_BLACK)) as GameObject;
                        this.m_cEffectStar5Black.transform.parent = this.m_cSceneRoot.transform;
                        this.m_cEffectStar5Black.transform.localPosition = Vector3.zero;
                        this.m_cEffectStar5Black.transform.localScale = Vector3.one;
                        this.m_cEffectStar5.SetActive(true);

                        break;
                    default:
                        break;
                }


                //下一状态
                m_fdisStar = GAME_TIME.TIME_FIXED();
                m_fdis = GAME_TIME.TIME_FIXED();
                this.m_eState++;
                break;
            case SHOW_STATE.ModelShowIng:
                //文字特效start结束
                if (GAME_TIME.TIME_FIXED() - m_fdisStar > m_fModelShowTime)
                {
                    //将猛将字符保持Keep状态
                    SetEffectKeep();
                    this.m_eState++;
                }
                break;
            case SHOW_STATE.ModelShowEnd:
                //等待全屏点击，关闭3D展示，进入英雄详细界面
                this.m_cNewHero.collider.enabled = true;
                break;
            case SHOW_STATE.End:
                //销毁特效
                GameObject.DestroyImmediate(m_cModelBg);
                GameObject.DestroyImmediate(m_cMofaSelf);
                GameObject.DestroyImmediate(this.m_cEffectStar2);
                GameObject.DestroyImmediate(this.m_cEffectStar3);
                GameObject.DestroyImmediate(this.m_cEffectStar4);
                GameObject.DestroyImmediate(this.m_cEffectStar5);
                GameObject.DestroyImmediate(this.m_cEffectStar5Black);
                m_bHasKeep = false;
                //关闭3D模型摄像头
                CameraManager.GetInstance().HidenUIHeroZhaoHuan2Camera();
                m_bHasKeep = false;
                if (this.m_cGfxHero != null)
                    this.m_cGfxHero.Destory();
               
                this.m_cNewHero.SetActive(false);
                this.SetLocalPos(Vector3.one * 0xFFFFFF);

                //显示英雄详细
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
                GUIHeroDetail gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERODETAIL) as GUIHeroDetail;
                gui.Show(HeroDetailShow, this.m_iReusltHero);
                this.m_eState++;
                break;
            case SHOW_STATE.HERO_DETAIL_SHOW:
                //等待点击关闭英雄详细界面，返回结算
                break;
            case SHOW_STATE.HERO_SHOW_END1:
                //等待点击
                break;
            case SHOW_STATE.HERO_SHOW_END2:
                this.m_cHeroIconParent.transform.localPosition = Vector3.one * 0xFFFFF;
                this.m_cHeroGet.transform.localPosition = Vector3.one * 0xFFFFF;

                //翻页音效
				MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_SLIDE);
//                MediaMgr.PlaySound(SOUND_DEFINE.SE_SLIDE);

                this.m_eState++;
                break;
            case SHOW_STATE.ITEM_SHOW_BEGIN:    //物品展示
                this.SHOW_COST_TIME = SHOW_ITEM_TIME;
                if (this.m_lstItem.Count <= 0)
                {
                    this.m_eState = SHOW_STATE.END_BEGIN;
                    break;
                }

                for (int i = 0; i < this.m_lstItem.Count; i++)
                {
                    GameObject obj = GameObject.Instantiate(this.m_cResItemIcon) as GameObject;
                    obj.transform.parent = this.m_cItemListView.transform;
                    obj.transform.localPosition = new Vector3(i % 5 * 120, ITEM_CURRENT_Y + i / 5 * -ITEM_OFFSET_Y, 0);
                    obj.transform.localScale = Vector3.one;
                    RewardItem rewardItem = new RewardItem(obj, this.m_lstItem[i], this.m_lstItemNum[i]);

                    rewardItem.m_cItemIcon.enabled = false;
                    rewardItem.m_cItemCover.enabled = false;
                    rewardItem.m_cItemNum.enabled = false;
                    rewardItem.m_cItemBorder.enabled = false;

                    this.m_lstItemIcon.Add(rewardItem);
                }
                UIDraggablePanel dpan = m_cItemIconParent.GetComponent<UIDraggablePanel>();
                dpan.repositionClipping = true;
                this.m_iItemShowIndex = 0;
                this.m_cItemIconParent.transform.localPosition = new Vector3(640, 0, 0);
                this.m_cItemGet.transform.localPosition = new Vector3(-640, 0, 0);
                //物品滑入
                CTween.TweenPosition(this.m_cItemIconParent, TWEEN_POS_TIME, Vector3.zero);
                CTween.TweenPosition(this.m_cItemGet, TWEEN_POS_TIME, Vector3.zero);
                this.m_fStateStartTime = GAME_TIME.TIME_FIXED();
                this.m_fCostTime = TWEEN_POS_TIME;
                this.m_eState++;
                break;
            case SHOW_STATE.ITEM_SHOW_SLIDE_END:
                disTime = GAME_TIME.TIME_FIXED() - this.m_fStateStartTime;
                if (disTime > this.m_fCostTime)
                {
                    this.m_eState++;
                }
                break;
            case SHOW_STATE.ITEM_SHOW1:
                this.m_fStateStartTime = GAME_TIME.TIME_FIXED();
                this.m_fCostTime = this.SHOW_COST_TIME;
                this.m_lstItemIcon[this.m_iItemShowIndex].m_cItemUnknow.enabled = false;
                this.m_lstItemIcon[this.m_iItemShowIndex].m_cItemIcon.enabled = true;
                this.m_lstItemIcon[this.m_iItemShowIndex].m_cItemCover.enabled = true;
                this.m_lstItemIcon[this.m_iItemShowIndex].m_cItemNum.enabled = true;
                this.m_lstItemIcon[this.m_iItemShowIndex].m_cItemBorder.enabled = true;

                this.m_eState++;
                break;
            case SHOW_STATE.ITEM_SHOW2:
                disTime = GAME_TIME.TIME_FIXED() - this.m_fStateStartTime;
                if (disTime >= this.m_fCostTime)
                {
                    this.m_lstItemIcon[this.m_iItemShowIndex].m_cItemCover.alpha = 0;

                    this.m_iItemShowIndex++;
                    if (this.m_iItemShowIndex >= this.m_lstItemIcon.Count)
                    {
                        this.m_eState++;
                        break;
                    }

                    int index = this.m_iItemShowIndex / 5;
                    if (index >= 3)
                    {
                        UIPanel pan = m_cItemIconParent.GetComponent<UIPanel>();
                        pan.transform.localPosition = new Vector3(pan.transform.localPosition.x, this.m_fClipParentY + (index - 2) * ITEM_OFFSET_Y, pan.transform.localPosition.z);
                        pan.clipRange = new Vector4(pan.clipRange.x, this.m_fClipCenterY - (index - 2) * ITEM_OFFSET_Y, pan.clipRange.z, this.m_fClipSizeY);
                    }

                    this.m_eState--;
                }
                else
                {
                    this.m_lstItemIcon[this.m_iItemShowIndex].m_cItemCover.alpha = Mathf.Lerp(1, 0, disTime / this.m_fCostTime);
                }
                break;
            case SHOW_STATE.ITEM_SHOW_END_WAIT:
                //等待点击
                break;
            case SHOW_STATE.ITEM_SHOW_END:
                this.m_cItemIconParent.transform.localPosition = Vector3.one * 0xFFFF;
                this.m_cItemGet.transform.localPosition = Vector3.one * 0xFFFFF;
                this.m_eState++;
                break;
            case SHOW_STATE.DIAMOND_SHOW_BEGIN:  //获得砖石
                if (m_iDiamondGet > 0)
                {

                    //翻页音效
					MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_SLIDE);
//                    MediaMgr.PlaySound(SOUND_DEFINE.SE_SLIDE);

                    //展示升级特效
                    CameraManager.GetInstance().ShowGUIEffectCamera();  //开启特效摄像头
                    m_cEffectObjBonusGet = GameObject.Instantiate(this.m_cEffectBonusGet) as GameObject;
                    m_cEffectObjBonusGet.transform.parent = this.m_cEffectParent.transform;
                    m_cEffectObjBonusGet.transform.localScale = Vector3.one;
                    m_cEffectObjBonusGet.transform.localPosition = Vector3.zero;

                    this.m_cDiamondGet.transform.localPosition = Vector3.zero;
                    this.m_cDiamondGetInfo.transform.localPosition = Vector3.zero;

                    this.m_fStateStartTime = GAME_TIME.TIME_FIXED();
                    this.m_fCostTime = SHOW_BONUS_GET_TIME;
                    this.m_eState++;
                }
                else
                {
                    this.m_eState = SHOW_STATE.END_BEGIN;
                }
                break;
            case SHOW_STATE.DIAMOND_SHOW:
                disTime = GAME_TIME.TIME_FIXED() - this.m_fStateStartTime;
                if (disTime > this.m_fCostTime)
                {
                    this.m_eState++;
                }
                break;
            case SHOW_STATE.DIAMOND_NUM_SHOW_BEGIN:
                CTween.TweenPosition(this.m_cPanDiamondGet, TWEEN_POS_TIME, OUT_RIGHT_POS, Vector3.zero);
                this.m_fStateStartTime = GAME_TIME.TIME_FIXED();
                this.m_fCostTime = TWEEN_POS_TIME;
                this.m_eState++;
                break;
            case SHOW_STATE.DIAMOND_NUM_SHOW:
                disTime = GAME_TIME.TIME_FIXED() - this.m_fStateStartTime;
                if (disTime > this.m_fCostTime)
                {
                    this.m_fStateStartTime = GAME_TIME.TIME_FIXED();
                    this.m_fCostTime = 1;
                    this.m_eState++;
                }
                break;
            case SHOW_STATE.DIAMOND_NUM_SHOW_END:
                disTime = GAME_TIME.TIME_FIXED() - this.m_fStateStartTime;
                if (disTime > this.m_fCostTime)
                {

                    //this.m_eState++;
                }
                break;
            case SHOW_STATE.DIAMOND_SHOW_END:
                GameObject.DestroyImmediate(m_cEffectObjBonusGet);
                CameraManager.GetInstance().HidenGUIEffectCamera();
                this.m_cDiamondGet.transform.localPosition = Vector3.one * 0xFFFF;
                this.m_cPanDiamondGet.transform.localPosition = Vector3.one * 0xFFFF;
                this.m_eState++;
                break;
            case SHOW_STATE.END_BEGIN:  //结束处理
                this.m_fStateStartTime = GAME_TIME.TIME_FIXED();
                this.m_fCostTime = TWEEN_POS_TIME;
                this.m_eState++;
                break;
            case SHOW_STATE.END:
                disTime = GAME_TIME.TIME_FIXED() - this.m_fStateStartTime;
                if (disTime >= this.m_fCostTime)
                {
                    this.m_eState++;
                }
                break;
            case SHOW_STATE.END_END:
                Hiden();
                if (this.m_delCallBack != null)
                    this.m_delCallBack();
                this.m_eState++;
                break;
        }

        return true;
    }

    /// <summary>
    /// 点击背景
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnBackGround(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (this.m_eState == SHOW_STATE.HERO_SHOW_SLIDE_END || this.m_eState == SHOW_STATE.ITEM_SHOW_SLIDE_END || this.m_eState == SHOW_STATE.ITEM_SHOW_BEGIN || this.m_eState == SHOW_STATE.HERO_SHOW_BEGIN)
            {
                return;
            }

            if (this.m_eState == SHOW_STATE.HERO_SHOW_BEGIN || this.m_eState == SHOW_STATE.HERO_SHOW1 || this.m_eState == SHOW_STATE.HERO_SHOW2 || this.m_eState == SHOW_STATE.ITEM_SHOW1 || this.m_eState == SHOW_STATE.ITEM_SHOW2)
            {
                this.SHOW_COST_TIME = SHOW_ITEM_SHORTTIME;
            }

            if (this.m_eState == SHOW_STATE.DIAMOND_NUM_SHOW_END || this.m_eState == SHOW_STATE.LEVEL_SHOW || this.m_eState == SHOW_STATE.WAIT_FOR_CLICK || this.m_eState == SHOW_STATE.HERO_SHOW_END1 || this.m_eState == SHOW_STATE.ITEM_SHOW_END_WAIT)
            {
                this.m_eState++;
            }
            this.m_fStateStartTime = 0;
        }
    }

    /// <summary>
    /// 结束英雄展示
    /// </summary>
    private void HeroShowEnd()
    {
        this.m_iHeroShowIndex++;
        if (this.m_iHeroShowIndex >= this.m_lstHeroIcon.Count)
        {
            this.m_eState = SHOW_STATE.HERO_SHOW_END1;
        }
        else
        {
            this.m_eState = SHOW_STATE.HERO_SHOW1;
            this.SHOW_COST_TIME = SHOW_ITEM_TIME;
        }
    }

    /// <summary>
    /// 设置字符Keep状态
    /// </summary>
    private void SetEffectKeep()
    {
        if (!m_bHasKeep)
        {
            HeroTable heroTable = HeroTableManager.GetInstance().GetHeroTable(m_iReusltHero.m_iTableID);

            switch (heroTable.Star)
            {
                case 1:
                case 2:
                    //1星2星显示猛将特效
                    Animation[] m_vecAnim2 = this.m_cEffectStarStr2.GetComponentsInChildren<Animation>();
                    foreach (Animation item in m_vecAnim2)
                    {
                        Debug.Log(item.name);
                        item.wrapMode = WrapMode.Loop;
                        item.Play("keep");
                    }
                    break;
                case 3:
                    //3星显示超猛将特效
                    Animation[] m_vecAnim3 = this.m_cEffectStarStr3.GetComponentsInChildren<Animation>();
                    foreach (Animation item in m_vecAnim3)
                    {
                        Debug.Log(item.name);

                        item.wrapMode = WrapMode.Loop;
                        item.Play("keep");
                    }
                    break;
                case 4:
                    //4星显示超绝猛将特效
                    Animation[] m_vecAnim4 = this.m_cEffectStarStr4.GetComponentsInChildren<Animation>();
                    foreach (Animation item in m_vecAnim4)
                    {
                        Debug.Log(item.name);

                        item.wrapMode = WrapMode.Loop;
                        item.Play("keep");
                    }
                    break;
                case 5:
                    //5星显示无双猛将特效
                    Animation[] m_vecAnim5 = this.m_cEffectStarStr5.GetComponentsInChildren<Animation>();
                    foreach (Animation item in m_vecAnim5)
                    {
                        Debug.Log(item.name);

                        item.wrapMode = WrapMode.Loop;
                        item.Play("keep");
                    }
                    break;
                default:
                    break;
            }

            m_bHasKeep = true;
        }
    }

    /// <summary>
    /// 隐藏展示
    /// </summary>
    private void ShowNext(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (this.m_eState == SHOW_STATE.ModelShowEnd)
            {
                this.m_eState++;
            }
        }
    }

    /// <summary>
    /// 展示新英雄详细信息回调
    /// </summary>
    private void HeroDetailShow()
    {
        this.SetLocalPos(Vector3.zero);
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Hiden();

        this.m_cHeroListParent.SetActive(true);
        this.m_cTitleParent.SetActive(true);
        this.m_cBackGroundBtn.SetActive(true);
        this.m_cGUITitle.SetActive(true);
        HeroShowEnd();
    }

    /// <summary>
    /// 设置奖励
    /// </summary>
    /// <param name="goldNum"></param>
    /// <param name="farmNum"></param>
    /// <param name="expRewardNum"></param>
    /// <param name="expNow"></param>
    /// <param name="heroLst"></param>
    /// <param name="itemLst"></param>
    /// <param name="cal"></param>
    public void SetReward(string tittle, string content, int goldNum, int farmNum, int expRewardNum, int expNow, int nowLevel, int tmpDiamond, List<Hero> heroLst, List<int> itemLst, List<int> itemNumlst, List<int> newHero, CALLBACK cal)
    {
        this.m_strTittle = tittle;
        this.m_strContent = content;
        this.m_delCallBack = cal;
        this.m_iGoldNum = goldNum;
        this.m_iFarmNum = farmNum;
        this.m_iExpRewardNum = expRewardNum;
        this.m_iExpNowNum = expNow;
        this.m_iLevelNow = nowLevel;
        this.m_lstNewHero = new List<int>(newHero);
        this.m_lstHero = new List<Hero>(heroLst);
        this.m_lstItem = new List<int>(itemLst);
        this.m_lstItemNum = new List<int>(itemNumlst);
        this.m_cNowRoleTable = RoleExpTableManager.GetInstance().GetRoleLevelByExp(expNow);
        this.m_iExpTargetTotalNum = this.m_iExpNowNum + this.m_iExpRewardNum;
        this.m_iDiamondGet = tmpDiamond;
    }
}
