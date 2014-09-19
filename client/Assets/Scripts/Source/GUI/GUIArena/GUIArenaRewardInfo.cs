using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Resource;
using UnityEngine;

//竞技场-等级情报类
//Author Sunyi
//2014-1-26
public class GUIArenaRewardInfo : GUIBase
{
    private const string RES_MAIN = "GUI_ArenaRewardInfo";//主资源地址
    private const string RES_GUANWEI_ITEM = "GUI_ArenaRewardGuanweiItem";  //官位一览Item资源
    private const string RES_WEEK_RANK_ITEM = "GUI_ArenaWeekRewardItem";  //周排行奖励Item资源

    private const string MAINPANEL = "MainPanel";//主面板地址
    private const string BUTTON_BACK = "Btn_Back";//返回按钮地址
    private const string BUTTON_PLAYERRANKING = "Btn_PlayerRanking";//排行榜按钮地址
    private const string LIST_VIEW = "MainPanel/ClipView/ListView";  //Item父对象
    private const string CLIP_VIEW = "MainPanel/ClipView";  //裁切Panel
    private const string GUANWEI_OBJECTS_PARENT = "MainPanel/GuanweiObjects";//官位一栏父对象地址
    private const string WEEKRANK_OBJECTS_PARENT = "MainPanel/WeekRankObjecs";//周排行父对象地址

    private const string WEEK_REWARD_ICON = "Spr_Icon";//周排行奖励图标标签地址
    private const string WEEK_REWARD_NAME = "Lab_Name";//周排行奖励名称标签地址
    private const string WEEK_REWARD_COUNT = "Lab_Count";//周排行奖励数量标签地址
    private const string WEEK_REWARD_RANK = "Lab_Rank";//周排行奖励排位标签地址
    private const string WEEK_REWARD_FRAME = "Spr_IconFrame";//周排行奖励图标边框地址Spr_IconBg
    private const string WEEK_REWARD_BG = "Spr_IconBg";//周排行奖励图标边框地址
    private const string WEEK_REWARD_CUR_RANK = "Spr_Frame";//当前玩家排名显示框地址

    private const string ARROW_LEFT_PARENT = "MainPanel/ArrowLeft";//左边箭头父对象
    private const string ARROW_LEFT = "MainPanel/ArrowLeft/Spr_ArrowLeft";//左边箭头
    private const string ARROW_RIGHT_PARENT = "MainPanel/ArrowRight";//右边箭头父对象
    private const string ARROW_RIGHT = "MainPanel/ArrowRight/Spr_ArrowRight";//右边箭头

    private UnityEngine.Object m_cGuanweiItem;  //官位一栏单项资源
    private UnityEngine.Object m_cWeekRankItem;  //周排行单项资源
    private GameObject m_cBtnBack;//返回按钮
    private GameObject m_cBtnPlayerRanking;//排行榜按钮
    private GameObject m_cMainPanel;//主面板
    private GameObject m_cListView; //Item父对象
    private GameObject m_cPanClip; //裁切Panel
    private GameObject m_cGuanweiObjectsParent;//官位一栏父对象
    private GameObject m_cWeekRankObjectsParent;//周排行父对象父对象

    private GameObject m_cArrowLeft;//左边箭头父对象
    private GameObject m_cArrowRight;//右边箭头父对象

    private UISprite m_cSprArrowLeft;//左边箭头
    private UISprite m_cSprArrowRight;//右边箭头
    private TDAnimation m_cEffectLeft;//左边箭头动画
    private TDAnimation m_cEffectRight;//右边箭头动画

    private bool m_bIsNeedToFirstDisplayGuanweiPanel;//是否需要显示官位一栏
    private bool m_bIsCurGuanweiPanel;//判断当前面板是否为官位面板

    public List<ArenaGuanweiItem> m_lstArenaGameObjs = new List<ArenaGuanweiItem>(); //游戏展示数据列表
    private List<GameObject> m_lstArenaWeekRewardItem = new List<GameObject>();//周排行奖励列表
    List<PVPWeekRankTable> m_lstWeekRankTable = new List<PVPWeekRankTable>();//pvp排行表

    /// <summary>
    /// 报酬单项资源
    /// </summary>
    public class ArenaGuanweiItem
    {
        public GameObject m_cItem;

        private const string LB_PT = "Lab_Pt"; //竞技场点
        private const string LB_TITLE = "Lab_Title";  //竞技场称号
        private const string LB_NAME = "Lab_Name"; //奖励名称
        private const string SP_DIAMOND = "Spr_Diamond"; //砖石图标
        private const string SP_UNKONW = "Spr_Unkonw"; //未知图标
        private const string SP_ITEM = "Spr_Item";  //物品图标
        private const string SP_ITEM_BORDER = "Spr_Item_Border"; // 物品图标边框
        private const string LB_REWARD = "Lab_Reward";  //报酬字符
        private const string SPR_PT = "Spr_Pt";//PT图片
        private const string SPR_TITLE = "Spr_Title";//title图片
        private const string SPR_SELECT = "Spr_Select";//选择框

        public UILabel m_cLbPt; //竞技场点
        public UILabel m_cLbTitle; //竞技场称号
        public UILabel m_cLbName;  //奖励名称
        public UISprite m_cSpDiamond; //砖石图标
        public UISprite m_cSpUnKonw;  //未知图标
        public UISprite m_cSpItem; //物品图标
        public UISprite m_cSpItemBorder; //物品边框图标
        public UILabel m_cLbReward;  //报酬字符
        public UISprite m_cSprPt;//pt图片
        public UISprite m_cSprTitle;//title图片
        public UISprite m_cSprSelect;//选择框


        public ArenaGuanweiItem(UnityEngine.Object obj)
        {
            m_cItem = GameObject.Instantiate(obj) as GameObject;

            m_cLbTitle = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cItem, LB_TITLE);
            m_cLbPt = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cItem, LB_PT);
            m_cLbName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cItem, LB_NAME);
            m_cSpDiamond = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, SP_DIAMOND);
            m_cSpUnKonw = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, SP_UNKONW);
            m_cSpItem = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, SP_ITEM);
            m_cSpItemBorder = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, SP_ITEM_BORDER);
            m_cLbReward = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cItem, LB_REWARD);
            m_cSprPt = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, SPR_PT);
            m_cSprTitle = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, SPR_TITLE);
            m_cSprSelect = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, SPR_SELECT);
        }

        public void Destroy()
        {
            m_cItem = null;
            m_cLbPt = null; //竞技场点
            m_cLbTitle = null; //竞技场称号
            m_cLbName = null;  //奖励名称
            m_cSpDiamond = null; //砖石图标
            m_cSpUnKonw = null;  //未知图标
            m_cSpItem = null; //物品图标
            m_cSpItemBorder = null; //物品边框图标
            m_cLbReward = null;  //报酬字符
            m_cSprPt = null;//pt图片
            m_cSprTitle = null;//title图片
            m_cSprSelect = null;//选择框

        }
    }

    public GUIArenaRewardInfo(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_ARENAREWORDINFO, UILAYER.GUI_PANEL)
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
			ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_GUANWEI_ITEM);
			ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_WEEK_RANK_ITEM);
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
            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset( RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            this.m_cMainPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, MAINPANEL);
            this.m_cMainPanel.transform.localPosition = new Vector3(640, 0, 0);

            this.m_cBtnBack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_BACK);
            this.m_cBtnBack.transform.localPosition = new Vector3(-640, 420, 0);
            GUIComponentEvent backEvent = this.m_cBtnBack.AddComponent<GUIComponentEvent>();
            backEvent.AddIntputDelegate(OnClickBackButton);

            this.m_cBtnPlayerRanking = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, BUTTON_PLAYERRANKING);
            this.m_cBtnPlayerRanking.transform.localPosition = new Vector3(1000, 430, 0);
            this.m_cBtnPlayerRanking.AddComponent<GUIComponentEvent>().AddIntputDelegate(OnClickPlayerRankingButton); 

            this.m_cListView = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LIST_VIEW);
            this.m_cPanClip = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, CLIP_VIEW);

            this.m_cGuanweiObjectsParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUANWEI_OBJECTS_PARENT);
            this.m_cWeekRankObjectsParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, WEEKRANK_OBJECTS_PARENT);

            this.m_cGuanweiItem = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_GUANWEI_ITEM);

            this.m_cWeekRankItem = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_WEEK_RANK_ITEM);

            this.m_cArrowLeft = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, ARROW_LEFT_PARENT);
            GUIComponentEvent dragLeftEvent = this.m_cArrowLeft.AddComponent<GUIComponentEvent>();
            dragLeftEvent.AddIntputDelegate(DragPanelEvent);

            this.m_cArrowRight = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, ARROW_RIGHT_PARENT);
            GUIComponentEvent dragRightEvent = this.m_cArrowRight.AddComponent<GUIComponentEvent>();
            dragRightEvent.AddIntputDelegate(DragPanelEvent);

            //左右导航
            this.m_cSprArrowLeft = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, ARROW_LEFT);
            this.m_cEffectLeft = new TDAnimation(m_cSprArrowLeft.atlas, m_cSprArrowLeft); //左右导航
            this.m_cEffectLeft.Play("ArrowLeft_", Game.Base.TDAnimationMode.Loop, 0.4f);
            this.m_cSprArrowRight = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, ARROW_RIGHT);
            this.m_cEffectRight = new TDAnimation(m_cSprArrowRight.atlas, m_cSprArrowRight);
            this.m_cEffectRight.Play("ArrowRight_", Game.Base.TDAnimationMode.Loop, 0.4f);
        }

        this.m_bIsNeedToFirstDisplayGuanweiPanel = false;

        if (this.m_bIsNeedToFirstDisplayGuanweiPanel)
        {
            this.m_cWeekRankObjectsParent.SetActive(false);
            this.m_cGuanweiObjectsParent.SetActive(true);
            UpdateGuanweiShow();
            this.m_bIsCurGuanweiPanel = true;
        }
        else
        {
            this.m_cGuanweiObjectsParent.SetActive(false);
            this.m_cWeekRankObjectsParent.SetActive(true);
            UpdateWeekRankShow();
            this.m_bIsCurGuanweiPanel = false;
        }


        SetLocalPos(Vector3.zero);
        this.m_cGUIMgr.SetCurGUIID(this.m_iID);

        CTween.TweenPosition(this.m_cBtnBack, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-640, 420, 0), new Vector3(-250, 420, 0));
        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), new Vector3(0, 0, 0));
        CTween.TweenPosition(this.m_cBtnPlayerRanking, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(1000, 430, 0), new Vector3(200, 430, 0));

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_ARENA_REWARD));
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
        CTween.TweenPosition(this.m_cBtnPlayerRanking, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(200, 430, 0), new Vector3(1000, 430, 0));
        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 0, 0), new Vector3(640, 0, 0) , Destory);

		ResourceMgr.UnloadUnusedResources();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        if (m_lstArenaWeekRewardItem != null)
        {
            foreach (GameObject obj in m_lstArenaWeekRewardItem)
            {
                GameObject.Destroy(obj);
            }
        }

        if (this.m_lstWeekRankTable != null)
        {
            this.m_lstWeekRankTable.Clear();
        }

        m_cGuanweiItem = null;  //官位一栏单项资源
        m_cWeekRankItem = null;  //周排行单项资源
        m_cBtnBack = null;//返回按钮
        m_cBtnPlayerRanking = null;//排行榜按钮
        m_cMainPanel = null;//主面板
        m_cListView = null; //Item父对象
        m_cPanClip = null; //裁切Panel
        m_cGuanweiObjectsParent = null;//官位一栏父对象
        m_cWeekRankObjectsParent = null;//周排行父对象父对象

        m_cArrowLeft = null;//左边箭头父对象
        m_cArrowRight = null;//右边箭头父对象

        m_cSprArrowLeft = null;//左边箭头
        m_cSprArrowRight = null;//右边箭头
        m_cEffectLeft = null;//左边箭头动画
        m_cEffectRight = null;//右边箭头动画

        if (m_lstArenaGameObjs!=null)
        {
            foreach (ArenaGuanweiItem q in m_lstArenaGameObjs)
            {
                q.Destroy();
            }

            this.m_lstArenaGameObjs.Clear();

        }

        base.Destory();
    }

    /// <summary>
    /// 点击排行榜按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickPlayerRankingButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            SendAgent.SendPVPBattleRankReq(Role.role.GetBaseProperty().m_iPlayerId);
        }
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
    /// 根据table读表，刷新显示
    /// </summary>
    private void UpdateWeekRankShow()
    {
        this.m_cPanClip.transform.localPosition = new Vector3(0, 0, 0);
        UIPanel panel = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(this.m_cGUIObject, CLIP_VIEW);
        float y = -19.5f;
        panel.clipRange = new Vector4(panel.clipRange.x, y, panel.clipRange.z, panel.clipRange.w);

        if (this.m_lstWeekRankTable != null)
        {
            this.m_lstWeekRankTable.Clear();
        }

        for (int i = 0; i < PVPWeekRankTableManager.GetInstance().GetAll().Count; i++)
        {
            this.m_lstWeekRankTable.Add(PVPWeekRankTableManager.GetInstance().GetAll()[i]);
        }

        for (int i = 0; i < m_lstWeekRankTable.Count; i++)
        {
            GameObject weekItem = GameObject.Instantiate(this.m_cWeekRankItem) as GameObject;
            weekItem.transform.parent = this.m_cListView.transform;
            weekItem.transform.localScale = Vector3.one;
            weekItem.transform.localPosition = new Vector3(0, 195 - 115 * i, 0);

            UISprite sprIcon = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(weekItem, WEEK_REWARD_ICON);
            UISprite sprFrame = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(weekItem, WEEK_REWARD_FRAME);
            UISprite sprBg = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(weekItem, WEEK_REWARD_BG);
            UILabel labName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(weekItem, WEEK_REWARD_NAME);
            UILabel labCount = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(weekItem, WEEK_REWARD_COUNT);
            UILabel labRank = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(weekItem, WEEK_REWARD_RANK);
            GameObject sprCurrentRank = GUI_FINDATION.GET_GAME_OBJECT(weekItem, WEEK_REWARD_CUR_RANK);


            if (m_lstWeekRankTable[i].RankFrom <= Role.role.GetBaseProperty().m_iMyWeekRank && m_lstWeekRankTable[i].RankTo >= Role.role.GetBaseProperty().m_iMyWeekRank)
            {
                sprCurrentRank.SetActive(true);
            }
            else {
                sprCurrentRank.SetActive(false);
            }

            labCount.text = "x" + m_lstWeekRankTable[i].Num.ToString();

            GiftType rewardType = m_lstWeekRankTable[i].AwardType;
            switch (rewardType)
            {
                case GiftType.Diamond:
                    sprIcon.spriteName = "gem";
                    labName.text = "钻石";
                    break;
                case GiftType.Gold:
                    sprIcon.spriteName = "zell_thum";
                    labName.text = "金币";
                    break;
                case GiftType.FriendPoint:
                    sprIcon.spriteName = "friend_p_thum";
                    labName.text = "友情点";
                    break;
                case GiftType.FarmPoint:
                    sprIcon.spriteName = "karma_thum";
                    labName.text = "农场点";
                    break;
                case GiftType.Hero:
                    HeroTable hero = HeroTableManager.GetInstance().GetHeroTable(m_lstWeekRankTable[i].ID);
                    GUI_FUNCTION.SET_AVATORS(sprIcon, hero.AvatorMRes);
                    GUI_FUNCTION.SET_HeroBorderAndBack(sprFrame, sprBg, (Nature)hero.Property);
                    labName.text = hero.Name;
                    break;
                case GiftType.Item:
                    ItemTable item = ItemTableManager.GetInstance().GetItem(m_lstWeekRankTable[i].ID);
                    if (item != null)
                    {
                        labName.text = item.ShortName;
                        GUI_FUNCTION.SET_ITEMM(sprIcon, item.SpiritName);
                        GUI_FUNCTION.SET_ITEM_BORDER(sprFrame, (ITEM_TYPE)item.Type);
                    }
                    break;
                default:
                    break;
            }

            if (m_lstWeekRankTable[i].RankFrom == m_lstWeekRankTable[i].RankTo)
            {
                labRank.text = m_lstWeekRankTable[i].RankFrom + "位";
            }
            else {
                labRank.text = m_lstWeekRankTable[i].RankFrom + "~" + m_lstWeekRankTable[i].RankTo + "位";
            }

            this.m_lstArenaWeekRewardItem.Add(weekItem);
        }


    }

    /// <summary>
    /// 根据Table读表， 刷新显示
    /// </summary>
    private void UpdateGuanweiShow()
    {
        this.m_cPanClip.transform.localPosition = new Vector3(0, 0, 0);
        UIPanel panel = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(this.m_cGUIObject, CLIP_VIEW);
        float y = -19.5f;
        panel.clipRange = new Vector4(panel.clipRange.x, y, panel.clipRange.z, panel.clipRange.w);


        //清空原来的排行
        foreach (ArenaGuanweiItem item in m_lstArenaGameObjs)
        {
            GameObject.DestroyImmediate(item.m_cItem);
        }

        //读取竞技表 刷新展示
        List<AthleticsExpTable> tables = AthleticsExpTableManager.GetInstance().GetAll();
        for (int i = 0; i < tables.Count; i++)
        {
            int index = tables.Count - i - 1;
            AthleticsExpTable athTable = tables[index];

            ArenaGuanweiItem tmp = new ArenaGuanweiItem(this.m_cGuanweiItem);
            tmp.m_cItem.transform.parent = m_cListView.transform;
            tmp.m_cItem.transform.localScale = Vector3.one;
            tmp.m_cItem.transform.localPosition = new Vector3(0, 125 - 115 * i, 0);
           
            if (Role.role.GetBaseProperty().m_iPVPMaxExp >= athTable.EXP)  //小于该等级的全部显示
            {
                tmp.m_cLbTitle.enabled = true;
                tmp.m_cSprTitle.enabled = false;
                tmp.m_cLbPt.enabled = true;
                tmp.m_cSprPt.enabled = false;

                tmp.m_cLbTitle.text = athTable.Name;
                tmp.m_cLbPt.text = athTable.EXP.ToString() + "积分";

                //判断是否显示选中框
                if (index == tables.Count - 1 && Role.role.GetBaseProperty().m_iPVPExp >= athTable.EXP)
                {
                    tmp.m_cSprSelect.enabled = true;
                }
                else if (Role.role.GetBaseProperty().m_iPVPExp <= tables[index + 1].EXP && Role.role.GetBaseProperty().m_iPVPExp >= athTable.EXP)
                {
                    tmp.m_cSprSelect.enabled = true;
                }
               
                if (athTable.Num == 0 && athTable.Item == 0)  //第一等级 没有奖励
                {
                    tmp.m_cLbName.text = "";

                    tmp.m_cLbName.enabled = false;
                    tmp.m_cSpDiamond.enabled = false;
                    tmp.m_cSpItem.enabled = false;
                    tmp.m_cSpItemBorder.enabled = false;
                    tmp.m_cSpUnKonw.enabled = false;
                }
                else
                {
                    if (athTable.Num == 0)  //显示装备
                    {
                        ItemTable table = ItemTableManager.GetInstance().GetItem(athTable.Item);
                        tmp.m_cLbName.text = table.Name;
                        GUI_FUNCTION.SET_ITEMM(tmp.m_cSpItem, table.SpiritName);
                        GUI_FUNCTION.SET_ITEM_BORDER(tmp.m_cSpItemBorder, (ITEM_TYPE)table.Type);

                        tmp.m_cSpItemBorder.MakePixelPerfect();
                        tmp.m_cSpItem.MakePixelPerfect();

                        tmp.m_cSpItemBorder.transform.localScale = Vector3.one * 0.5f;
                        tmp.m_cSpItem.transform.localScale = Vector3.one * 0.5f;

                        tmp.m_cSpDiamond.enabled = false;
                        tmp.m_cSpItem.enabled = true;
                        tmp.m_cSpItemBorder.enabled = true;
                        tmp.m_cSpUnKonw.enabled = false;
                        tmp.m_cLbName.enabled = true;
                    }
                    else  //显示钻石
                    {
                        tmp.m_cLbName.text = athTable.Num + "钻石";

                        tmp.m_cLbName.enabled = true;
                        tmp.m_cSpDiamond.enabled = true;
                        tmp.m_cSpItem.enabled = false;
                        tmp.m_cSpItemBorder.enabled = false;
                        tmp.m_cSpUnKonw.enabled = false;
                    }
                }
            }
            else  //超过该等级的未知
            {
                //上一级显示
                index = index == 0 ? 0 : index - 1;
                AthleticsExpTable nextTable = tables[index];
                if (Role.role.GetBaseProperty().m_iPVPMaxExp >= nextTable.EXP)
                {
                    //导航到该项目位置
                    UIPanel pan = this.m_cPanClip.GetComponent<UIPanel>();
                    pan.transform.localPosition = new Vector3(pan.transform.localPosition.x, -(125 - 115 * i), pan.transform.localPosition.z);
                    pan.clipRange = new Vector4(pan.clipRange.x, 125 - 115 * i - 19, pan.clipRange.z, pan.clipRange.w);
                    //设置最后边界
                    if (i >= tables.Count - 3)
                    {
                        int yy = (tables.Count - 3) * 115 - 9 - 125;
                        pan.transform.localPosition = new Vector3(pan.transform.localPosition.x, yy, pan.transform.localPosition.z);
                        pan.clipRange = new Vector4(pan.clipRange.x, -yy - 19, pan.clipRange.z, pan.clipRange.w);
                    }

                    tmp.m_cLbTitle.enabled = true;
                    tmp.m_cSprTitle.enabled = false;
                    tmp.m_cLbPt.enabled = true;
                    tmp.m_cSprPt.enabled = false;
                    tmp.m_cLbTitle.text = athTable.Name;
                    tmp.m_cLbPt.text = athTable.EXP.ToString() + "积分";

                    if (athTable.Num == 0)  //显示装备
                    {
                        ItemTable table = ItemTableManager.GetInstance().GetItem(athTable.Item);
                        tmp.m_cLbName.text = table.Name;
                        GUI_FUNCTION.SET_ITEMM(tmp.m_cSpItem, table.SpiritName);
                        GUI_FUNCTION.SET_ITEM_BORDER(tmp.m_cSpItemBorder, (ITEM_TYPE)table.Type);

                        tmp.m_cSpItemBorder.MakePixelPerfect();
                        tmp.m_cSpItem.MakePixelPerfect();

                        tmp.m_cSpItemBorder.transform.localScale = Vector3.one * 0.5f;
                        tmp.m_cSpItem.transform.localScale = Vector3.one * 0.5f;


                        tmp.m_cSpDiamond.enabled = false;
                        tmp.m_cSpItem.enabled = true;
                        tmp.m_cSpItemBorder.enabled = true;
                        tmp.m_cSpUnKonw.enabled = false;
                        tmp.m_cLbName.enabled = true;
                    }
                    else  //显示钻石
                    {
                        tmp.m_cLbName.text = athTable.Num + "钻石";

                        tmp.m_cLbName.enabled = true;
                        tmp.m_cSpDiamond.enabled = true;
                        tmp.m_cSpItem.enabled = false;
                        tmp.m_cSpItemBorder.enabled = false;
                        tmp.m_cSpUnKonw.enabled = false;
                    }

                    tmp.m_cLbName.color = Color.grey;
                    tmp.m_cLbReward.color = Color.grey;
                    tmp.m_cLbPt.color = Color.grey;
                    tmp.m_cLbTitle.color = Color.grey;
                }
                else
                {
                    tmp.m_cLbTitle.enabled = false;
                    tmp.m_cSprTitle.enabled = true;
                    tmp.m_cLbPt.enabled = false;
                    tmp.m_cSprPt.enabled = true;
                    tmp.m_cLbReward.color = Color.grey;
                    tmp.m_cLbPt.color = Color.grey;
                    tmp.m_cLbTitle.color = Color.grey;
                    tmp.m_cLbName.enabled = false;
                    tmp.m_cSpDiamond.enabled = false;
                    tmp.m_cSpItem.enabled = false;
                    tmp.m_cSpItemBorder.enabled = false;
                    tmp.m_cSpUnKonw.enabled = true;
                }
            }

            m_lstArenaGameObjs.Add(tmp);
        }
    }

    /// <summary>
    /// 滑动事件
    /// </summary>
    private void DragPanelEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        { 
            //左滑
            if (this.m_bIsCurGuanweiPanel)
            {
                this.m_cGuanweiObjectsParent.SetActive(false);
                foreach (Transform trans in this.m_cListView.transform)
                {
                    GameObject.Destroy(trans.gameObject);
                }

                this.m_cWeekRankObjectsParent.SetActive(true);

                UpdateWeekRankShow();

                this.m_bIsCurGuanweiPanel = false;

            }
            else
            {
                this.m_cWeekRankObjectsParent.SetActive(false);
                foreach (Transform trans in this.m_cListView.transform)
                {
                    GameObject.Destroy(trans.gameObject);
                }

                this.m_cGuanweiObjectsParent.SetActive(true);

                UpdateGuanweiShow();

                this.m_bIsCurGuanweiPanel = true;
            }
        }
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

        if (this.m_cEffectLeft != null)
        {
            this.m_cEffectLeft.Update();
        }
        if (this.m_cEffectRight != null)
        {
            this.m_cEffectRight.Update();
        }

        return base.Update();
    }
}