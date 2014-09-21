using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Media;
using Game.Resource;
using UnityEngine;

//竞技场主界面
public class GUIArenaFightReady : GUIBase
{
    private const string RES_MAIN = "GUI_ArenaFightReady";//主资源地址
    private const string RES_ITEM = "GUI_ArenaFightItem";  //单项资源

    private const string TOPPANEL = "TopPanel";//导航栏地址
    private const string MAINPANEL = "MainPanel";//主面板地址
    private const string BUTTON_BACK = "TopPanel/Btn_Back";//返回按钮地址
    //我的信息
    private const string MY_HERO_FRAME = "Top/ItemFrame";
    private const string MY_BIG_NAME = "Top/Lab_BattleName";
    private const string MY_HERO_LV = "Top/Lab_Lv";
    private const string MY_NEXT_PVP_POINT = "Top/Lab_Next";
    private const string MY_RANK = "Top/Lab_Rank";
    private const string MY_WEEK_POINT = "Top/Lab_week";
    private const string MY_HERO_BORDER = "Top/Spr_HeroFrame";
    private const string MY_HERO_ICON = "Top/Spr_HeroIcon";

    private const string LISTVIEW = "Bottom/ListView";

    public class FightItem
    {
        public GameObject m_cItem;

        private const string ENEMY_HERO_FRAME = "ItemFrame";
        private const string ENEMY_HERO_BORDER = "Spr_HeroFrame";
        private const string ENEMY_HERO_ICON = "Spr_HeroIcon";
        private const string ENEMY_HERO_LV = "Lab_Lv";
        private const string ENEMY_NAME = "Lab_Name";
        private const string EVEMY_BIG_NAME = "Lab_bigName";
        private const string ENEMY_RECORD = "Lab_record";
        private const string ENEMY_WEEK_RANK = "Lab_weekRank";
        private const string BTN_FIGHT = "btn_fight";

        public UISprite m_cHeroFrame;
        public UISprite m_cHeroBorder;
        public UISprite m_cHeroIcon;
        public UILabel m_cLbName;
        public UILabel m_cLbBigName;
        public UILabel m_cHeroLv;
        public UILabel m_cLbRecord;
        public UILabel m_cLbWeekRank;
        public GameObject m_cBtnFight;

        /// <summary>
        /// 挑战对手ID
        /// </summary>
        public int EnemyId;

        public FightItem(UnityEngine.Object obj)
        {
            m_cItem = GameObject.Instantiate(obj) as GameObject;

            m_cHeroFrame = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, ENEMY_HERO_FRAME);
            m_cHeroBorder = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, ENEMY_HERO_BORDER);
            m_cHeroIcon = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, ENEMY_HERO_ICON);
            m_cLbName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cItem, ENEMY_NAME);
            m_cLbBigName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cItem, EVEMY_BIG_NAME);
            m_cHeroLv = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cItem, ENEMY_HERO_LV);
            m_cLbRecord = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cItem, ENEMY_RECORD);
            m_cLbWeekRank = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cItem, ENEMY_WEEK_RANK);
            m_cBtnFight = GUI_FINDATION.GET_GAME_OBJECT(this.m_cItem, BTN_FIGHT);
        }

        public void Destory()
        {
            m_cItem = null;
            m_cHeroFrame = null;
            m_cHeroBorder = null;
            m_cHeroIcon = null;
            m_cLbName = null;
            m_cLbBigName = null;
            m_cHeroLv = null;
            m_cLbRecord = null;
            m_cLbWeekRank = null;
            m_cBtnFight = null;
        }
    }

    private GameObject m_cTopPanel;//导航栏
    private GameObject m_cMainPanel;//主面板
    private GameObject m_cBtnBack;//返回按钮

    //我方
    private UISprite m_cSpHeroFrame;
    private UISprite m_cSpHeroIcon;
    private UILabel m_cLbRank;
    private UILabel m_cLbLv;
    private UILabel m_cLbBigName;
    private UILabel m_cLbWeekPoint;
    private UILabel m_cLbNextPvpPoint;
    private UISprite m_cSpHeroBorder;

    private GameObject m_cListView;

    private List<FightItem> m_lstEnemyShows;

    private UnityEngine.Object Res_Item;

    private List<PVPEnemyData> m_lstEnemyDatas;

    public string m_strSelectName;
    public int m_iTagetPvpPoint;
    public int m_iSelectId;

    public GUIArenaFightReady(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_ARENAFIGHTREADY, UILAYER.GUI_PANEL)
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
            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            this.m_cTopPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, TOPPANEL);
            this.m_cTopPanel.transform.localPosition = new Vector3(-420, 270, 0);

            this.m_cMainPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, MAINPANEL);
            this.m_cMainPanel.transform.localPosition = new Vector3(640, 33, 0);

            this.m_cBtnBack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_BACK);
            GUIComponentEvent backEvent = this.m_cBtnBack.AddComponent<GUIComponentEvent>();
            backEvent.AddIntputDelegate(OnClickBackButton);

            m_cLbBigName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cMainPanel, MY_BIG_NAME);
            m_cLbLv = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cMainPanel, MY_HERO_LV);
            m_cLbNextPvpPoint = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cMainPanel, MY_NEXT_PVP_POINT);
            m_cLbWeekPoint = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cMainPanel, MY_WEEK_POINT);
            m_cSpHeroIcon = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cMainPanel, MY_HERO_ICON);
            m_cSpHeroFrame = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cMainPanel, MY_HERO_FRAME);
            m_cSpHeroBorder = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cMainPanel, MY_HERO_BORDER);
            m_cLbRank = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cMainPanel, MY_RANK);

            m_cListView = GUI_FINDATION.GET_GAME_OBJECT(this.m_cMainPanel, LISTVIEW);

            this.Res_Item = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_ITEM);

        }

        if (m_lstEnemyShows != null)
        {
            foreach (FightItem item in m_lstEnemyShows)
            {
                GameObject.DestroyImmediate(item.m_cItem);
            }
        }

        m_lstEnemyShows = new List<FightItem>();
        for (int i = 0; i < m_lstEnemyDatas.Count; i++)
        {
            FightItem tmp = new FightItem(Res_Item);
            tmp.m_cItem.transform.parent = m_cListView.transform;
            tmp.m_cItem.transform.localScale = Vector3.one;
            tmp.m_cItem.transform.localPosition = new Vector3(0, 60 - 130 * i, 0);

            HeroTable table = HeroTableManager.GetInstance().GetHeroTable(m_lstEnemyDatas[i].m_iHeroTableID);
            GUI_FUNCTION.SET_HeroBorderAndBack(tmp.m_cHeroBorder, tmp.m_cHeroFrame, (Nature)table.Property);
            GUI_FUNCTION.SET_AVATORS(tmp.m_cHeroIcon, table.AvatorMRes);
            tmp.m_cHeroLv.text = "Lv." + m_lstEnemyDatas[i].m_iLv;
            tmp.m_cLbBigName.text = AthleticsExpTableManager.GetInstance().GetAthleticsNameByPoint(m_lstEnemyDatas[i].m_iPVP_point);
            tmp.m_cLbName.text = m_lstEnemyDatas[i].m_strName;
            tmp.m_cLbRecord.text = m_lstEnemyDatas[i].m_iPVPwin_num + "胜" + m_lstEnemyDatas[i].m_iPVPlose_num + "负";
            tmp.m_cLbWeekRank.text = m_lstEnemyDatas[i].m_iPVP_point.ToString();
            tmp.EnemyId = m_lstEnemyDatas[i].m_iTpid;

            tmp.m_cBtnFight.AddComponent<GUIComponentEvent>().AddIntputDelegate(Fight_OnEvent, i);

            m_lstEnemyShows.Add(tmp);
        }

        //我方
		HeroTeam heroTeam = CModelMgr.sInstance.GetModel<HeroTeam>();
        int leaderID = heroTeam.Get<HeroTeam>(Role.role.GetBaseProperty().m_iCurrentTeam).m_iLeadID;
        Hero heroLeader = Role.role.GetHeroProperty().GetHero(leaderID);
        GUI_FUNCTION.SET_HeroBorderAndBack(m_cSpHeroBorder, m_cSpHeroFrame, heroLeader.m_eNature);
        GUI_FUNCTION.SET_AVATORS(m_cSpHeroIcon, heroLeader.m_strAvatarM);
        this.m_cLbBigName.text = AthleticsExpTableManager.GetInstance().GetAthleticsNameByPoint(Role.role.GetBaseProperty().m_iPVPExp);
        m_cLbLv.text = "Lv." + heroLeader.m_iLevel;
        m_cLbWeekPoint.text = Role.role.GetBaseProperty().m_iMyWeekPoint.ToString();
        this.m_cLbRank.text = Role.role.GetBaseProperty().m_iPVPRank.ToString();
        m_cLbNextPvpPoint.text = (AthleticsExpTableManager.GetInstance().GetAhtleticsMaxExp(Role.role.GetBaseProperty().m_iPVPExp) - Role.role.GetBaseProperty().m_iPVPExp).ToString();


        SetLocalPos(Vector3.zero);
        this.m_cGUIMgr.SetCurGUIID(this.m_iID);

        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 33, 0), new Vector3(0, 33, 0));
        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-420, 270, 0), new Vector3(0, 270, 0));

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_ARENA_CHOOSE_OPPONENT));
    }
    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //base.Hiden();

        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 33, 0), new Vector3(640, 33, 0));
        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-420, 270, 0), Destory);

		ResourceMgr.UnloadUnusedResources();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        m_cTopPanel = null;//导航栏
        m_cMainPanel = null;//主面板
        m_cBtnBack = null;//返回按钮
        m_cSpHeroFrame = null;
        m_cSpHeroIcon = null;
        m_cLbRank = null;
        m_cLbLv = null;
        m_cLbBigName = null;
        m_cLbWeekPoint = null;
        m_cLbNextPvpPoint = null;
        m_cSpHeroBorder = null;
        m_cListView = null;
        Res_Item = null;

        if (m_lstEnemyShows != null)
        {
            foreach (FightItem q in m_lstEnemyShows)
            {
                q.Destory();
            }
            m_lstEnemyShows.Clear();
        }
  


        base.Destory();
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

            this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Hiden();

            GUIArena arena = (GUIArena)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_ARENA);
            arena.Show();
        }
    }

    /// <summary>
    /// 竞技场战斗事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void Fight_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            int index = (int)args[0];

            this.m_iSelectId = m_lstEnemyShows[index].EnemyId;
            this.m_iTagetPvpPoint = this.m_lstEnemyDatas[index].m_iPVP_point;
            this.m_strSelectName = this.m_lstEnemyDatas[index].m_strName;

            Role.role.GetBaseProperty().m_iEnemyLevel = m_lstEnemyDatas[index].m_iPlayLv;  //挑战的对手玩家等级
            Role.role.GetBaseProperty().m_iEnemyPid = m_lstEnemyDatas[index].m_iTpid;  //挑战的对手玩家ID
            Role.role.GetBaseProperty().m_strEnemySignture = m_lstEnemyDatas[index].m_strSignure;  //挑战的对手玩家签名
            Role.role.GetBaseProperty().m_strEnemyName = m_lstEnemyDatas[index].m_strName; //挑战的对手玩家用户名
            Role.role.GetBaseProperty().m_iEnemyPVPEXP = m_lstEnemyDatas[index].m_iPVP_point;  //t挑战对手玩家竞技点

            MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_BATTLE_PVP_JOIN);

            SendAgent.SendPVPBattleStart(Role.role.GetBaseProperty().m_iPlayerId, m_iSelectId);

            Hiden();

            //this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Hiden();
            //this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Hiden();

            //GUI_FUNCTION.AYSNCLOADING_SHOW();

            //GUIBattleArena gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_ARENA_BATTLE) as GUIBattleArena;

            //HeroTeam team = new HeroTeam().GetTeam(Role.role.GetBaseProperty().m_iCurrentTeam);
            //Hero[] heros = new Hero[5];
            //for (int i = 0; i < team.m_vecTeam.Length; i++)
            //{
            //    Hero item = Role.role.GetHeroProperty().GetHero(team.m_vecTeam[i]);
            //    heros[i] = item;
            //}
            //gui.SetBattleSelfHero(heros);
            //gui.SetBattleTargetHero(heros);
            //gui.Show();

        }
    }

    /// <summary>
    /// 设置本页面数据
    /// </summary>
    /// <param name="enemys"></param>
    public void SetPVPData(List<PVPEnemyData> enemys)
    {
        this.m_lstEnemyDatas = enemys;
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
}