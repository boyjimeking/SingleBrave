using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Resource;
using UnityEngine;
//竞技场-排行榜
//Author Sunyi
//2014-1-26

/// <summary>
/// 竞技场排行
/// </summary>
public class GUIArenaRankings : GUIBase
{
    //----------------------------------------------资源地址--------------------------------------------------

    //主资源
    private const string RES_MAIN = "GUI_ArenaRankings";//主资源地址
    //单项列表资源
    private const string RES_ITEM = "GUI_ArenaRankingsItem";  //排行单项资源
    //返回按钮
    private const string BUTTON_BACK = "Btn_Back";//返回按钮地址
    //奖励信息按钮
    private const string BUTTON_REWARDINFO = "Btn_RewardInfo";//奖励信息按钮
    //滑动进入主面板
    private const string MAINPANEL = "MainPanel";//主面板地址
    //列表父节点
    private const string LIST_VIEW = "MainPanel/ClipView/ListView"; //单项父节点
    private const string CLIP_VIEW = "MainPanel/ClipView";  //滑动显示Panel
    private const string SPR_ARROWLEFT = "Spr_ArrowLeft";      //向左滑动特效
    private const string SPR_ARROWRIGHT = "Spr_ArrowRight";   //向右滑动特效
    private const string SPR_TITLE = "Spr_Title";  //标题

    /// <summary>
    /// 排行单项资源
    /// </summary>
    public class ArenaItem
    {
        public GameObject m_cItem;

        private const string SP_HERO = "Spr_Icon";      //英雄
        private const string SP_HERO_BORDER = "Spr_IconFrame";
        private const string SP_HERO_BG = "ItemFrame";
        private const string LB_LV = "Lab_LV";      //英雄等级
        private const string LB_NAME = "Lab_Name";   //名称
        private const string LB_TITLE = "Lab_BigName"; //官位
        private const string LB_RECORD = "Lab_BattleRecord";  //战绩
        private const string LB_RANK = "Lab_No";  //排行
        private const string LB_POINT = "Lab_Exp";       //官位积分
        private const string SP_RANK_LEVEL = "Spr_Level";        //金银铜
        private const string SP_JIFEN = "Spr_JiFen";  //积分
        private const string SP_ZHANJI = "Spr_ZhanJi";  //战绩
        private const string SP_LINE = "Spr_Line"; //黄线

        public UISprite m_cSpHero;
        public UISprite m_cSpHeroBorder;
        public UISprite m_cSpHeroBg;
        public UISprite m_cSpLine;
        public UILabel m_cLbHeroLv;
        public UILabel m_cLbName;
        public UILabel m_cLbTitile;
        public UILabel m_cLbRecord;
        public UILabel m_cLbRank;
        public UILabel m_cLbPoint;
        public UISprite m_cSpRankLv;
        public UISprite m_cSpJifen;
        public UISprite m_cSpZhanji;

        public ArenaItem(UnityEngine.Object obj)
        {
            m_cItem = GameObject.Instantiate(obj) as GameObject;

            m_cSpHero = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, SP_HERO);
            m_cSpHeroBg = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, SP_HERO_BG);
            m_cSpHeroBorder = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, SP_HERO_BORDER);
            m_cSpLine = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, SP_LINE);
            m_cLbName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cItem, LB_NAME);
            m_cLbPoint = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cItem, LB_POINT);
            m_cLbRank = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cItem, LB_RANK);
            m_cLbRecord = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cItem, LB_RECORD);
            m_cLbTitile = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cItem, LB_TITLE);
            m_cLbHeroLv = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cItem, LB_LV);
            m_cSpRankLv = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, SP_RANK_LEVEL);
            m_cSpJifen = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, SP_JIFEN);
            m_cSpZhanji = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, SP_ZHANJI);
        }

        public void Destory()
        {
            m_cItem = null;
            m_cSpHero = null;
            m_cSpHeroBorder = null;
            m_cSpHeroBg = null;
            m_cSpLine = null;
            m_cLbHeroLv = null;
            m_cLbName = null;
            m_cLbTitile = null;
            m_cLbRecord = null;
            m_cLbRank = null;
            m_cLbPoint = null;
            m_cSpRankLv = null;
            m_cSpJifen = null;
            m_cSpZhanji = null;
        }
    }

    //----------------------------------------------游戏对象--------------------------------------------------

    //单项列表对象
    private UnityEngine.Object m_cItem;  //排行单项资源
    //返回按钮
    private GameObject m_cBtnBack;     //返回按钮
    //奖励信息按钮
    private GameObject m_cBtn_RewardInfo; 
    //滑动进入主面板
    private GameObject m_cMainPanel;  //主面板
    //列表父节点
    private GameObject m_cListView;//单项父节点
    private GameObject m_cPanCilpView; //滑动显示Panel
    private UISprite m_cSprArrowLeft;           //向左滑动特效
    private UISprite m_cSprArrowRight;          //向右滑动特效
    private UISprite m_cSpTitle;   //排行类型标题
    private TDAnimation m_cEffectLeft;         //特效类
    private TDAnimation m_cEffectRight;        //特效类

    //----------------------------------------------Data--------------------------------------------------

    //图集地址名称
    private const string PATH_WEEK_RANK = "text_weekRanking";  //周排行
    private const string PATH_ALL_RANK = "text_guanweiranking";  //官位排行
    private const string PATH_FRIEND_RANK = "text_FriendRanking"; //好友排行
    private const string PATH_ALL_SCORE = "text_totleScore";  //总战绩
    private const string PATH_WEEK_SCORE = "text_WeekRecode";  //周战绩
    private const string PATH_ALL_JIFEN = "textP_guanweiScore";  //官位积分
    private const string PATH_WEEK_JIFEN = "text_weekScore";  //周积分

    public List<PVPItem> m_lstRanksAll = new List<PVPItem>();   //全服排行数据
    public List<PVPItem> m_lstRanksFriend = new List<PVPItem>();  //好友排行数据
    public List<PVPItem> m_lstRanksWeek = new List<PVPItem>();  //周排行数据
    public List<ArenaItem> m_lstArenaGameObjs = new List<ArenaItem>(); //游戏展示数据列表
    public RankType m_eRankType = RankType.ALL;  //排行类型切换标志
    public int m_iMyRankAll;  //我在全服排行位置
    public int m_iMyRankFriend; //我在好友排行位置
    public int m_iMyRankWeek;  //我在周排行位置
    private int m_iShowOffsetX = 0; //展示X偏移量索引
    private float m_fClipParentY = 4.5f;   //剪裁父节点Y轴坐标
    private float m_fClipCenterY = -1.5f;   //剪裁中间点Y轴坐标
    private float m_fClipSizeY = 570; //剪裁Y轴大小
    private const int OFFSET_Y = -150;  //Y偏移量
    private const int TOP_Y = 200;  //Y初始


    /// <summary>
    /// 排行类型
    /// </summary>
    public enum RankType
    {
        /// <summary>
        /// 全部官位排行
        /// </summary>
        ALL = 0,
        /// <summary>
        /// 好友排行
        /// </summary>
        Friend = 1,
        /// <summary>
        /// 周排行
        /// </summary>
        Week = 2,
    }

    /// 排名显示单项
    /// </summary>
    public class PVPItem
    {
        public string m_strName;
        public int m_iHeroTableID;
        public int m_iHeroLv;
        public int m_iPoint;
        public int m_iWinNum;
        public int m_iLoseNum;

        public int m_iPointForWeek;
        public int m_iWinNumForWeek;
        public int m_iLoseNumForWeek;
    }

    public GUIArenaRankings(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_ARENARANKINGS, UILAYER.GUI_PANEL)
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
			ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_ITEM);
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
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;
            //单项资源
            this.m_cItem = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_ITEM);
            //返回
            this.m_cBtnBack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_BACK);
            this.m_cBtnBack.transform.localPosition = new Vector3(-640, 420, 0);
            GUIComponentEvent backEvent = this.m_cBtnBack.AddComponent<GUIComponentEvent>();
            backEvent.AddIntputDelegate(OnClickBackButton);
            //奖励信息按钮
            this.m_cBtn_RewardInfo = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_REWARDINFO);
            this.m_cBtn_RewardInfo.transform.localPosition = new Vector3(1000, 430, 0);
            this.m_cBtn_RewardInfo.AddComponent<GUIComponentEvent>().AddIntputDelegate(OnClickRewardInfoButton);
            //滑动panel
            this.m_cMainPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, MAINPANEL);
            this.m_cMainPanel.transform.localPosition = new Vector3(640, 0, 0);
            //列表父节点
            this.m_cListView = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LIST_VIEW);
            this.m_cPanCilpView = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, CLIP_VIEW);
            //左右导航
            this.m_cSprArrowLeft = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cMainPanel, SPR_ARROWLEFT);
            this.m_cSprArrowLeft.gameObject.AddComponent<GUIComponentEvent>().AddIntputDelegate(Left_OnEvent);
            this.m_cEffectLeft = new TDAnimation(m_cSprArrowLeft.atlas, m_cSprArrowLeft);
            this.m_cSprArrowRight = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cMainPanel, SPR_ARROWRIGHT);
            this.m_cSprArrowRight.gameObject.AddComponent<GUIComponentEvent>().AddIntputDelegate(Right_OnEvent);
            this.m_cEffectRight = new TDAnimation(m_cSprArrowRight.atlas, m_cSprArrowRight);
            //排行标题
            this.m_cSpTitle = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cMainPanel, SPR_TITLE);
        }

        //初始为全部排行
        m_eRankType = RankType.Week;

        SetRank();

        this.m_cEffectLeft.Play("ArrowLeft", Game.Base.TDAnimationMode.Loop, 0.4F);
        this.m_cEffectRight.Play("ArrowRight", Game.Base.TDAnimationMode.Loop, 0.4F);

        SetLocalPos(Vector3.zero);
        this.m_cGUIMgr.SetCurGUIID(this.m_iID);

        CTween.TweenPosition(this.m_cBtnBack, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-640, 420, 0), new Vector3(-250, 420, 0));
        CTween.TweenPosition(this.m_cBtn_RewardInfo, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(1000, 430, 0), new Vector3(220, 430, 0));
        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), new Vector3(0, 0, 0));

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_ARENA_RANKING));
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //base.Hiden();

        //SetLocalPos(Vector3.one * 0XFFFF);
        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        CTween.TweenPosition(this.m_cBtnBack, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-250, 420, 0), new Vector3(-640, 420, 0));
        CTween.TweenPosition(this.m_cBtn_RewardInfo, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(220, 430, 0), new Vector3(1000, 430, 0));
        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 0, 0), new Vector3(640, 0, 0) , Destory);

		ResourceMgr.UnloadUnusedResources();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        m_cItem = null;  //排行单项资源
        m_cBtnBack = null;     //返回按钮
        m_cBtn_RewardInfo = null;
        m_cMainPanel = null;  //主面板
        m_cListView = null;//单项父节点
        m_cPanCilpView = null; //滑动显示Panel
        m_cSprArrowLeft = null;           //向左滑动特效
        m_cSprArrowRight = null;          //向右滑动特效
        m_cSpTitle = null;   //排行类型标题
        m_cEffectLeft = null;         //特效类
        m_cEffectRight = null;        //特效类

        if (m_lstArenaGameObjs != null)
        {
            foreach (ArenaItem item in m_lstArenaGameObjs)
            {
                item.Destory();
            }

            this.m_lstArenaGameObjs.Clear();
        }

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

        if (this.m_cEffectLeft != null)
        {
            this.m_cEffectLeft.Update();
        }
        if (this.m_cEffectRight != null)
        {
            this.m_cEffectRight.Update();
        }

        return base.Update(); ;
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

            GUIArena arena = (GUIArena)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_ARENA);
            arena.Show();
        }
    }
    /// <summary>
    /// 奖励信息按钮点击事件
    /// </summary>
    private void OnClickRewardInfoButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            GUIArenaRewardInfo ar = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_ARENAREWORDINFO) as GUIArenaRewardInfo;
            ar.Show();
        }
    }

    /// <summary>
    /// 左滑动
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void Left_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            int tmp = (int)m_eRankType;
            tmp--;
            if (tmp < 0)
            {
                tmp = 2;
            }

            this.m_eRankType = (RankType)tmp;

            SetRank();
        }
    }

    /// <summary>
    /// 右滑动
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void Right_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            int tmp = (int)m_eRankType;
            tmp++;
            if (tmp > 2)
            {
                tmp = 0;
            }

            this.m_eRankType = (RankType)tmp;

            SetRank();
        }
    }

    /// <summary>
    /// 根据排行类型 刷新显示
    /// </summary>
    private void SetRank()
    {
        //在切换排行的时候，将滑动重置，回到原来顶点
        UIPanel pan = this.m_cPanCilpView.GetComponent<UIPanel>();
        pan.transform.localPosition = new Vector3(pan.transform.localPosition.x, m_fClipParentY, pan.transform.localPosition.z);
        pan.clipRange = new Vector4(pan.clipRange.x, m_fClipCenterY, pan.clipRange.z, m_fClipSizeY);
        //清空原来的排行
        foreach (ArenaItem item in m_lstArenaGameObjs)
        {
            GameObject.DestroyImmediate(item.m_cItem);
        }

        List<PVPItem> ShowDataList = new List<PVPItem>();  //排行
        int myRank = 0;  //我的位置
        string tmpZhanji = string.Empty;  //战绩图集
        string tmpJifen = string.Empty;  //积分图集
        switch (m_eRankType)
        {
            case RankType.ALL:
                ShowDataList = m_lstRanksAll;
                myRank = m_iMyRankAll;
                m_cSpTitle.spriteName = PATH_ALL_RANK;
                m_cSpTitle.MakePixelPerfect();
                tmpZhanji = PATH_ALL_SCORE;
                tmpJifen = PATH_ALL_JIFEN;

                break;
            case RankType.Friend:
                ShowDataList = m_lstRanksFriend;
                myRank = m_iMyRankFriend;
                m_cSpTitle.spriteName = PATH_FRIEND_RANK;
                m_cSpTitle.MakePixelPerfect();
                tmpZhanji = PATH_ALL_SCORE;
                tmpJifen = PATH_ALL_JIFEN;
                break;
            case RankType.Week:
                ShowDataList = m_lstRanksWeek;
                myRank = m_iMyRankWeek;
                m_cSpTitle.spriteName = PATH_WEEK_RANK;
                m_cSpTitle.MakePixelPerfect();
                tmpZhanji = PATH_WEEK_SCORE;
                tmpJifen = PATH_WEEK_JIFEN;
                break;
            default:
                break;
        }

        //将内存中的好友排行和全服排行数据刷新显示
        //好友排行
        for (int i = 0; i < ShowDataList.Count; i++)
        {
            ArenaItem tmp = new ArenaItem(m_cItem);
            tmp.m_cItem.transform.parent = m_cListView.transform;
            tmp.m_cItem.transform.localScale = Vector3.one;
            tmp.m_cItem.transform.localPosition = new Vector3(m_iShowOffsetX, TOP_Y + OFFSET_Y * i, 0);

            //英雄
            HeroTable herotable = HeroTableManager.GetInstance().GetHeroTable(ShowDataList[i].m_iHeroTableID);
            GUI_FUNCTION.SET_AVATORS(tmp.m_cSpHero, herotable.AvatorMRes);  //英雄图标
            GUI_FUNCTION.SET_HeroBorderAndBack(tmp.m_cSpHeroBorder, tmp.m_cSpHeroBg, (Nature)herotable.Property);  //英雄边框
            tmp.m_cLbHeroLv.text = "Lv." + ShowDataList[i].m_iHeroLv.ToString();  //英雄等级
            tmp.m_cLbName.text = ShowDataList[i].m_strName;  //名称
            if (m_eRankType == RankType.Week)
            {
                tmp.m_cLbTitile.text = AthleticsExpTableManager.GetInstance().GetAthleticsNameByPoint(ShowDataList[i].m_iPointForWeek);  //官位
            }
            else
            {
                tmp.m_cLbTitile.text = AthleticsExpTableManager.GetInstance().GetAthleticsNameByPoint(ShowDataList[i].m_iPoint);  //官位
            }
            tmp.m_cLbPoint.text = ShowDataList[i].m_iPoint.ToString();  //竞技点
            tmp.m_cLbRank.text = (i + 1).ToString() + "位";  //排位
            if (m_eRankType == RankType.Week)
            {
                tmp.m_cLbRecord.text = ShowDataList[i].m_iWinNumForWeek + "胜 " + ShowDataList[i].m_iLoseNumForWeek + "败";  //战绩
            }
            else
            {
                tmp.m_cLbRecord.text = ShowDataList[i].m_iWinNum + "胜 " + ShowDataList[i].m_iLoseNum + "败";  //战绩
            }
            tmp.m_cSpZhanji.spriteName = tmpZhanji;
            tmp.m_cSpZhanji.MakePixelPerfect();
            tmp.m_cSpJifen.spriteName = tmpJifen;
            tmp.m_cSpJifen.MakePixelPerfect();

            if (i == 0)
                tmp.m_cSpRankLv.spriteName = "icon_Gold";
            else if (i == 1)
                tmp.m_cSpRankLv.spriteName = "icon_silver";
            else if (i == 2)
                tmp.m_cSpRankLv.spriteName = "icon_coper";
            else
                tmp.m_cSpRankLv.enabled = false;

            m_lstArenaGameObjs.Add(tmp);
        }

        //我在排行中的位置
        if (myRank > 0)
        {
            ArenaItem tmp = new ArenaItem(m_cItem);
            tmp.m_cItem.transform.parent = m_cListView.transform;
            tmp.m_cItem.transform.localScale = Vector3.one;
            tmp.m_cItem.transform.localPosition = new Vector3(m_iShowOffsetX, TOP_Y + OFFSET_Y * ShowDataList.Count, 0);

            //英雄
			HeroTeam heroTeam = CModelMgr.sInstance.GetModel<HeroTeam>();
            int leaderID = heroTeam.Get<HeroTeam>(Role.role.GetBaseProperty().m_iCurrentTeam).m_iLeadID;
            Hero heroLeader = Role.role.GetHeroProperty().GetHero(leaderID);
            GUI_FUNCTION.SET_AVATORS(tmp.m_cSpHero, heroLeader.m_strAvatarM);
            GUI_FUNCTION.SET_HeroBorderAndBack(tmp.m_cSpHeroBorder, tmp.m_cSpHeroBg, heroLeader.m_eNature);
            tmp.m_cLbHeroLv.text = "Lv." + heroLeader.m_iLevel.ToString();
            tmp.m_cLbName.text = Role.role.GetBaseProperty().m_strUserName;
            tmp.m_cLbTitile.text = AthleticsExpTableManager.GetInstance().GetAthleticsNameByPoint(Role.role.GetBaseProperty().m_iPVPExp);
            if (m_eRankType == RankType.Week)
            {
                tmp.m_cLbPoint.text = Role.role.GetBaseProperty().m_iMyWeekPoint.ToString();
                tmp.m_cLbRank.text = Role.role.GetBaseProperty().m_iMyWeekRank.ToString() + "位";
            }
            else
            {
                tmp.m_cLbPoint.text = Role.role.GetBaseProperty().m_iPVPExp.ToString();
                tmp.m_cLbRank.text = myRank.ToString() + "位";
            }
            
            tmp.m_cLbRecord.text = Role.role.GetBaseProperty().m_iPVPWin + "胜 " + Role.role.GetBaseProperty().m_iPVPLose + "败";
            tmp.m_cSpZhanji.spriteName = tmpZhanji;
            tmp.m_cSpZhanji.MakePixelPerfect();
            tmp.m_cSpJifen.spriteName = tmpJifen;
            tmp.m_cSpJifen.MakePixelPerfect();

            if (myRank == 1)
                tmp.m_cSpRankLv.spriteName = "icon_Gold";
            else if (myRank == 2)
                tmp.m_cSpRankLv.spriteName = "icon_silver";
            else if (myRank == 3)
                tmp.m_cSpRankLv.spriteName = "icon_coper";
            else if (myRank >= 30)
            {
                tmp.m_cSpRankLv.enabled = false;
                tmp.m_cSpLine.enabled = true;
            }
                

            m_lstArenaGameObjs.Add(tmp);
        }

    }
}