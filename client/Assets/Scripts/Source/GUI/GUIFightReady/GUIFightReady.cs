//Micro.Sanvey
//2013.11.12
//sanvey.china@gmail.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;
using Game.Media;

/// <summary>
/// 战斗准备GUI界面
/// </summary>
public class GUIFightReady : GUIBase
{
    private const string RES_FIGHTREADY = "GUI_FightReady";         //战斗准备界面地址
    private const string BTN_CANCEL = "Title_Cancel/Btn_Cancel";               //返回上一层按钮地址
    private const string PAN_CANCEL = "Title_Cancel";                 //返回上一层Pan地址
    private const string LB_TITEL = "Title_Cancel/Lb_Info";  //标题
    private const string BTN_BACKTOMAIN = "PanInfo/Btn_BackToMain";                    //返回主界面按钮地址
    private const string BTN_FIGHT = "PanInfo/Btn_Fight";                              //冒险出战按钮地址
    private const string BTN_MONSTER = "PanInfo/Unit_Frame/Btn_Monster";                          //单位变更按钮地址 
    private const string BTN_GOOD = "PanInfo/Btn_Good";                                //物品栏变更按钮地址
    private const string SPR_ARROWLEFT = "PanInfo/Pan_Navigate/ArrowLeft";      //向左滑动特效
    private const string SPR_ARROWRIGHT = "PanInfo/Pan_Navigate/ArrowRight";   //向右滑动特效
    private const string PAN_INFO = "PanInfo";                        //除了取消按钮的划出panel
    private const string Table = "PanInfo/Unit_Frame/Table";  //滑动table
    private const string MARK_LIGHY = "PanInfo/Pan_Navigate/PositionMarkL";  //选中发光点
    private const string LB_STRENGTH = "PanInfo/StrengthNum";  //消费体力
    private const string RES_ITEMSFRAME = "Good_Frame";  //物品整个Frame
    private const string TOGGLE_AUTO_FULL = "PanInfo/Toggle";  //自动补给

    private const string RES_ITEM0 = "GUI_FightReadyItem0";  //三个拖动项目
    private const string RES_ITEM1 = "GUI_FightReadyItem1";  //三个拖动项目
    private const string RES_ITEM2 = "GUI_FightReadyItem2";  //三个拖动项目

    private const string RES_BattleItem0 = "PanInfo/Good_Frame/GoodItem0";
    private const string RES_BattleItem1 = "PanInfo/Good_Frame/GoodItem1";
    private const string RES_BattleItem2 = "PanInfo/Good_Frame/GoodItem2";
    private const string RES_BattleItem3 = "PanInfo/Good_Frame/GoodItem3";
    private const string RES_BattleItem4 = "PanInfo/Good_Frame/GoodItem4";

    private GameObject m_cBtnCancel;            //取消按钮
    private GameObject m_cPanCancel;            //取消Pan
    private GameObject m_cBtnBackToMain;        //返回主界面按钮
    private GameObject m_cBtnFight;             //冒险出战按钮 
    private GameObject m_cBtnMonster;           //单位变更按钮
    private GameObject m_cBtnGood;              //物品栏变更按钮
    private GameObject m_cBackFrameTop;         //背景菜单栏和状态栏Panel
    private GameObject m_cBackFrameBottom;      //背景菜单栏和状态栏Panel
    private GameObject m_cPanInfo;              //除了取消按钮的划出panel
    private GameObject m_cPiontL;               //选中的放光点
    private GameObject m_cTable;                //滑动table
    private UISprite m_cSprArrowLeft;           //向左滑动特效
    private UISprite m_cSprArrowRight;          //向右滑动特效
    private UILabel m_cLbStrength;              //消费体力
    private UILabel m_cLbTitle;                 //标题关卡
    private UIToggle m_cTogAutoFull;    //自动补给勾选框

    private List<FightReadyItem> m_lstSlideItem = new List<FightReadyItem>();    //显示战斗英雄组 3个拖动项目
    private HeroTeam[] m_vecTeams;                  //队伍列表
    private BattleShowItem[] m_vecBattleItems;      //战斗装备

    private TDAnimation m_cEffectLeft;         //特效类
    private TDAnimation m_cEffectRight;        //特效类

    private bool m_bIsDraging = false;        //是否滑动中
    private bool m_bIsRight = false;          //向右拖动
    private int m_fVIndex = 0;                //偏移量
    private int m_iSelectId = 0;              //滑动中的当前ID
    private int m_iTeamId = 0;                //初始化的时候，第一个TeamId位置
    private bool m_bTweening = false;         //是否动画进行中
    private int m_iSelectBattleIndex = 0;     //战斗装备选中Index

    private string m_strTittle;//标题
    private int m_iHPcost;//消费体力

    private bool m_bHasShow = false;  //加载过showobject

    /// <summary>
    /// 战斗队伍Item
    /// </summary>
    public class FightReadyItem
    {
        private const string LB_TEAMNUM = "Lb_Index";        //队伍1
        private const string LB_BOTTOM1 = "Lb_Bottom1";      //底部1
        private const string LB_BOTTOM2 = "Lb_Bottom2";      //底部2
        private const string SP_BOTTOM = "FriendSkill";  //好友技能
        private const string LB_TOP1 = "Lb_Top1";            //顶部1
        private const string LB_TOP2 = "Lb_Top2";            //顶部2
        private const string SP_NOT_FRIEND = "SP_NotFriend";  //好友不能使用队长技能
        //private const string SP_NOT_FRIEND_BG = "SP_NotFriendBg";//好友不能使用队长技能黑底
        private const string Item0 = "GUI_MonsterItem0";
        private const string Item1 = "GUI_MonsterItem1";
        private const string Item2 = "GUI_MonsterItem2";
        private const string Item3 = "GUI_MonsterItem3";
        private const string Item4 = "GUI_MonsterItem4";
        private const string Item5 = "GUI_MonsterItem5";


        private GameObject m_cItem0;
        private GameObject m_cItem1;
        private GameObject m_cItem2;
        private GameObject m_cItem3;
        private GameObject m_cItem4;
        private GameObject m_cItem5;
        public GameObject m_cItem;
        public UILabel m_cLbTeamNum;
        public UILabel m_cLbBottom1;
        public UILabel m_cLbBottom2;
        public UISprite m_cSpBottom;
        public UILabel m_cLbTop1;
        public UILabel m_cLbTop2;
        public UITexture m_cSpNotFriend;
        //public UISprite m_cSpNotFriendBg;

        public List<HeroShowItem> m_lstFightTeam;

        public FightReadyItem(List<Hero> heros, GameObject parent)
        {
            m_cItem = parent;
            m_cLbTeamNum = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cItem, LB_TEAMNUM);
            m_cLbBottom1 = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cItem, LB_BOTTOM1);
            m_cLbBottom2 = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cItem, LB_BOTTOM2);
            m_cSpBottom = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, SP_BOTTOM);
            m_cLbTop1 = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cItem, LB_TOP1);
            m_cLbTop2 = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cItem, LB_TOP2);
            m_cSpNotFriend = GUI_FINDATION.GET_OBJ_COMPONENT<UITexture>(this.m_cItem, SP_NOT_FRIEND);
            //m_cSpNotFriendBg = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, SP_NOT_FRIEND_BG);
            m_cItem0 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cItem, Item0);
            m_cItem1 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cItem, Item1);
            m_cItem2 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cItem, Item2);
            m_cItem3 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cItem, Item3);
            m_cItem4 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cItem, Item4);
            m_cItem5 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cItem, Item5);


            m_lstFightTeam = new List<HeroShowItem>();

            HeroShowItem hero1 = new HeroShowItem(m_cItem0,false);
            HeroShowItem hero2 = new HeroShowItem(m_cItem1, false);
            HeroShowItem hero3 = new HeroShowItem(m_cItem2, false);
            HeroShowItem hero4 = new HeroShowItem(m_cItem3, false);
            HeroShowItem hero5 = new HeroShowItem(m_cItem4, false);
            HeroShowItem hero6 = new HeroShowItem(m_cItem5, true);

            //战友是好友的 闪烁显示, 并且显示好友队长技，非好友不闪烁显示，并且提示不能使用队长技能
            if (GLOBAL_DEFINE.m_cSelectBattleFriend.m_bIsFriend)
            {
                //闪烁好友
                hero6.m_cSPFriend.enabled = true;
                TweenAlpha.Begin(hero6.m_cSPFriend.gameObject, 1, 0).style = UITweener.Style.PingPong;
                //隐藏不能使用队长技能提示
                m_cSpNotFriend.enabled = false;
                //m_cSpNotFriendBg.enabled = false;
                m_cSpBottom.enabled = true;
                m_cLbBottom1.enabled = true;
                m_cLbBottom2.enabled = true;
            }
            else
            {
                //不闪烁好友
                hero6.m_cSPFriend.enabled = false;
                //显示不能使用队长技能提示
                m_cSpNotFriend.enabled = true;
                //m_cSpNotFriendBg.enabled = false;
                m_cSpBottom.enabled = false;
                m_cLbBottom1.enabled = false;
                m_cLbBottom2.enabled = false;
            }

            m_lstFightTeam.Add(hero1);
            m_lstFightTeam.Add(hero2);
            m_lstFightTeam.Add(hero3);
            m_lstFightTeam.Add(hero4);
            m_lstFightTeam.Add(hero5);
            m_lstFightTeam.Add(hero6);

            for (int i = 0; i < heros.Count; i++)  //对空的item进行向前排序
            {
                if (heros[i] == null)
                {
                    heros.RemoveAt(i);
                    m_lstFightTeam[m_lstFightTeam.Count - 2].m_cItem.SetActive(false);
                    m_lstFightTeam.RemoveAt(m_lstFightTeam.Count - 2);
                    i--;
                }
            }

            for (int i = 0; i < m_lstFightTeam.Count; i++)
            {
                var item = heros[i];
                m_lstFightTeam[i].m_cItem.SetActive(true);
                GUI_FUNCTION.SET_HeroBorderAndBack(m_lstFightTeam[i].m_cBorder, m_lstFightTeam[i].m_cFrame, item.m_eNature);
                //更新英雄头像
                UISprite avatar = m_lstFightTeam[i].m_cMonster.GetComponent<UISprite>();
                GUI_FUNCTION.SET_AVATORS(avatar, item.m_strAvatarM);
                //更新英雄图鉴ID
                var lbbottom = m_lstFightTeam[i].m_cLbBottom.GetComponent<UILabel>();
                lbbottom.text = "Lv." + item.m_iLevel;
                lbbottom.MakePixelPerfect();
            }

            //队长技能
            LeaderSkillTable ldSkill = LeaderSkillTableManager.GetInstance().GetLeaderSkillTable(heros[0].m_iLeaderSkillID);
            if (ldSkill == null)
            {
                m_cLbTop1.text = "";
                m_cLbTop2.text = "";
            }
            else
            {
                m_cLbTop1.text = ldSkill.Name;
                m_cLbTop2.text = ldSkill.Desc;
            }

            //好友技能
            LeaderSkillTable ldSkill2 = LeaderSkillTableManager.GetInstance().GetLeaderSkillTable(heros[heros.Count - 1].m_iLeaderSkillID);
            if (ldSkill2 == null)
            {
                m_cLbBottom1.text = "";
                m_cLbBottom2.text = "";
            }
            else
            {
                m_cLbBottom1.text = ldSkill2.Name;
                m_cLbBottom2.text = ldSkill2.Desc;
            }
        }

        public void UpdateShowItem(List<Hero> heros)
        {
            m_lstFightTeam = new List<HeroShowItem>();

            HeroShowItem hero1 = new HeroShowItem(m_cItem0,false);
            HeroShowItem hero2 = new HeroShowItem(m_cItem1, false);
            HeroShowItem hero3 = new HeroShowItem(m_cItem2, false);
            HeroShowItem hero4 = new HeroShowItem(m_cItem3, false);
            HeroShowItem hero5 = new HeroShowItem(m_cItem4, false);
            HeroShowItem hero6 = new HeroShowItem(m_cItem5, true);

            //战友是好友的 闪烁显示, 并且显示好友队长技，非好友不闪烁显示，并且提示不能使用队长技能
            if (GLOBAL_DEFINE.m_cSelectBattleFriend.m_bIsFriend)
            {
                //闪烁好友
                hero6.m_cSPFriend.enabled = true;
                TweenAlpha.Begin(hero6.m_cSPFriend.gameObject, 1, 0).style = UITweener.Style.PingPong;
                //隐藏不能使用队长技能提示
                m_cSpNotFriend.enabled = false;
                //m_cSpNotFriendBg.enabled = false;
                m_cSpBottom.enabled = true;
                m_cLbBottom1.enabled = true;
                m_cLbBottom2.enabled = true;
            }
            else
            {
                //不闪烁好友
                hero6.m_cSPFriend.enabled = false;
                //显示不能使用队长技能提示
                m_cSpNotFriend.enabled = true;
                //m_cSpNotFriendBg.enabled = false;
                m_cSpBottom.enabled = false;
                m_cLbBottom1.enabled = false;
                m_cLbBottom2.enabled = false;
            }

            m_lstFightTeam.Add(hero1);
            m_lstFightTeam.Add(hero2);
            m_lstFightTeam.Add(hero3);
            m_lstFightTeam.Add(hero4);
            m_lstFightTeam.Add(hero5);
            m_lstFightTeam.Add(hero6);

            for (int i = 0; i < heros.Count; i++)  //对空的item进行向前排序
            {
                if (heros[i] == null)
                {
                    heros.RemoveAt(i);
                    m_lstFightTeam[m_lstFightTeam.Count - 2].m_cItem.SetActive(false);
                    m_lstFightTeam.RemoveAt(m_lstFightTeam.Count - 2);
                    i--;
                }
            }

            for (int i = 0; i < m_lstFightTeam.Count; i++)
            {
                var item = heros[i];
                m_lstFightTeam[i].m_cItem.SetActive(true);
                GUI_FUNCTION.SET_HeroBorderAndBack(m_lstFightTeam[i].m_cBorder, m_lstFightTeam[i].m_cFrame, item.m_eNature);
                //更新英雄头像
                UISprite avatar = m_lstFightTeam[i].m_cMonster.GetComponent<UISprite>();
                GUI_FUNCTION.SET_AVATORS(avatar, item.m_strAvatarM);
                //更新英雄图鉴ID
                var lbbottom = m_lstFightTeam[i].m_cLbBottom.GetComponent<UILabel>();
                lbbottom.text = "Lv." + item.m_iLevel;
                lbbottom.MakePixelPerfect();
        
            }


            //队长技能
            LeaderSkillTable ldSkill = LeaderSkillTableManager.GetInstance().GetLeaderSkillTable(heros[0].m_iLeaderSkillID);
            if (ldSkill == null)
            {
                m_cLbTop1.text = "";
                m_cLbTop2.text = "";
            }
            else
            {
                m_cLbTop1.text = ldSkill.Name;
                m_cLbTop2.text = ldSkill.Desc;
            }

            //好友技能
            LeaderSkillTable ldSkill2 = LeaderSkillTableManager.GetInstance().GetLeaderSkillTable(heros[heros.Count - 1].m_iLeaderSkillID);
            if (ldSkill2 == null)
            {
                m_cLbBottom1.text = "";
                m_cLbBottom2.text = "";
            }
            else
            {
                m_cLbBottom1.text = ldSkill2.Name;
                m_cLbBottom2.text = ldSkill2.Desc;
            }
        }

    }

    /// <summary>
    /// 英雄显示Item
    /// </summary>
    public class HeroShowItem
    {
        public GameObject m_cItem;  //item整个显示对象
        public UISprite m_cBorder;  //边框
        public UISprite m_cFrame; //底色
        public GameObject m_cMonster;  //头像
        public UILabel m_cLbBottom; //字体底部
        public UISprite m_cSPFriend; //好友精灵

        private const string RES_BORDER = "ItemBorder";  //英雄头像资源地址
        private const string RES_FRAME = "ItemFrame";  //英雄头像资源地址
        private const string RES_MONSTER = "ItemMonster";  //英雄头像资源地址
        private const string RES_LBBOTTOM = "LabelBottom";  //英雄头像资源地址
        private const string RES_FRIEND = "Spr_Friend";  //好友精灵

        public HeroShowItem(GameObject parent,bool isFriend)
        {
            m_cItem = parent;
            m_cBorder = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, RES_BORDER);
            m_cFrame = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, RES_FRAME);
            m_cMonster = GUI_FINDATION.GET_GAME_OBJECT(this.m_cItem, RES_MONSTER);
            m_cLbBottom = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cItem, RES_LBBOTTOM);
            if (isFriend)
            {
                m_cSPFriend = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, RES_FRIEND);
            }
        }
    }

    /// <summary>
    /// 战斗
    /// </summary>
    public class BattleShowItem
    {
        public GameObject m_cItem;
        public UILabel m_cLbCount;
        public UILabel m_cLbName;
        public UISprite m_cSpItem;

        private const string LB_Count = "Lb_Count";
        private const string LB_Name = "Lb_Info";
        private const string SP_ITEM = "Spr_Good";

        public BattleShowItem(GameObject parent)
        {
            this.m_cItem = parent;

            this.m_cLbCount = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(parent, LB_Count);
            this.m_cLbName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(parent, LB_Name);
            this.m_cSpItem = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(parent, SP_ITEM);

        }
    }

    public GUIFightReady(GUIManager mgr)
        : base(mgr, GUI_DEFINE.GUIID_FIGHTREADY, UILAYER.GUI_PANEL)
    {
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
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_FIGHTREADY);
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
            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_FIGHTREADY)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;
            //返回按钮
            this.m_cBtnCancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_CANCEL);
            GUIComponentEvent gui_event = this.m_cBtnCancel.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnCancel_OnEvent);
            this.m_cPanCancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_CANCEL);
            //返回主界面按钮
            this.m_cBtnBackToMain = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_BACKTOMAIN);
            gui_event = this.m_cBtnBackToMain.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnBackToMain_OnEvent);
            //冒险出战按钮
            this.m_cBtnFight = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_FIGHT);
            gui_event = this.m_cBtnFight.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnFight_OnEvent);
            //队伍变更按钮
            this.m_cBtnMonster = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_MONSTER);
            gui_event = this.m_cBtnMonster.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnMonster_OnEvent);
            //物品变更按钮
            this.m_cBtnGood = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_GOOD);
            gui_event = this.m_cBtnGood.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnGood_OnEvent);
            //除了取消按钮的划出panel
            this.m_cPanInfo = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_INFO);
            //对整个物品一览添加事件，然后进入道具编程界面
            GameObject itemFrame = GUI_FINDATION.GET_GAME_OBJECT(this.m_cPanInfo, RES_ITEMSFRAME);
            itemFrame.AddComponent<GUIComponentEvent>().AddIntputDelegate(BtnGood_OnEvent);
            //左右导航
            this.m_cSprArrowLeft = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SPR_ARROWLEFT);
            this.m_cSprArrowLeft.gameObject.AddComponent<GUIComponentEvent>().AddIntputDelegate(Left_OnEvent);
            this.m_cEffectLeft = new TDAnimation(m_cSprArrowLeft.atlas, m_cSprArrowLeft); //左右导航
            this.m_cSprArrowRight = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SPR_ARROWRIGHT);
            this.m_cSprArrowRight.gameObject.AddComponent<GUIComponentEvent>().AddIntputDelegate(Right_OnEvent);
            this.m_cEffectRight = new TDAnimation(m_cSprArrowRight.atlas, m_cSprArrowRight);
            //选中发光点
            this.m_cPiontL = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, MARK_LIGHY);
            //table
            this.m_cTable = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, Table);
            //消费体力
            this.m_cLbStrength = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LB_STRENGTH);
            //标题关卡
            this.m_cLbTitle = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LB_TITEL);
            //自动填满勾选框
            this.m_cTogAutoFull = GUI_FINDATION.GET_OBJ_COMPONENT<UIToggle>(this.m_cGUIObject, TOGGLE_AUTO_FULL);
            EventDelegate.Add(this.m_cTogAutoFull.onChange, Toggle_OnChange);

        }

        this.m_cTogAutoFull.value = GAME_SETTING.s_bBattleAutoFull;

        this.m_cLbStrength.text = this.m_iHPcost.ToString();
        this.m_cLbTitle.text = this.m_strTittle;

        m_lstSlideItem = new List<FightReadyItem>();
		HeroTeam heroTeam = CModelMgr.sInstance.GetModel<HeroTeam>();

        GameObject obj0 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cTable, RES_ITEM0);
        obj0.AddComponent<GUIComponentEvent>().AddIntputDelegate(Drag_OnEvent);
        obj0.transform.localPosition = new Vector3(-640, 0, 0);
        GameObject obj1 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cTable, RES_ITEM1);
        obj1.AddComponent<GUIComponentEvent>().AddIntputDelegate(Drag_OnEvent);
        obj1.transform.localPosition = new Vector3(0, 0, 0);
        GameObject obj2 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cTable, RES_ITEM2);
        obj2.AddComponent<GUIComponentEvent>().AddIntputDelegate(Drag_OnEvent);
        obj2.transform.localPosition = new Vector3(640, 0, 0);

        m_bIsDraging = false;
        m_bIsRight = false;
        m_fVIndex = 0;
        m_iSelectId = 0;
        m_bTweening = false;
        m_iTeamId = Role.role.GetBaseProperty().m_iCurrentTeam;

		int index = (0 + m_iTeamId) % heroTeam.Count;
        this.m_cPiontL.transform.localPosition = new Vector3(-180 + index * 40, m_cPiontL.transform.localPosition.y, 0);

        SetShow(obj0, 9);
        SetShow(obj1, 0);
        SetShow(obj2, 1);
        this.m_cTable.transform.localPosition = Vector3.zero;

        GameObject bat0 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, RES_BattleItem0);
        GameObject bat1 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, RES_BattleItem1);
        GameObject bat2 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, RES_BattleItem2);
        GameObject bat3 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, RES_BattleItem3);
        GameObject bat4 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, RES_BattleItem4);

        m_vecBattleItems = new BattleShowItem[5];
        m_vecBattleItems[0] = new BattleShowItem(bat0);
        m_vecBattleItems[1] = new BattleShowItem(bat1);
        m_vecBattleItems[2] = new BattleShowItem(bat2);
        m_vecBattleItems[3] = new BattleShowItem(bat3);
        m_vecBattleItems[4] = new BattleShowItem(bat4);
        UpdateBattleItemShow();

        this.m_cEffectLeft.Play("ArrowLeft", Game.Base.TDAnimationMode.Loop, 0.4F);
        this.m_cEffectRight.Play("ArrowRight", Game.Base.TDAnimationMode.Loop, 0.4F);

        CTween.TweenPosition(this.m_cPanInfo, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.one);
        CTween.TweenPosition(this.m_cPanCancel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-540, 270, 0), new Vector3(0, 270, 0));

        SetLocalPos(Vector3.zero);
   
    }

    /// <summary>
    /// 刷新战斗准备道具
    /// </summary>
    private void UpdateBattleItemShow()
    {
        Item[] battleItems = Role.role.GetItemProperty().GetAllBattleItem();

        if (this.m_cTogAutoFull.value)
        {
            //进入战斗前 自动装满
            for (int i = 0; i < 5; i++)
            {
                if (battleItems[i] != null)
                {
                    int tableId = battleItems[i].m_iTableID;
                    //最大消耗品可装备数量  eg 10
                    int maxBat = ItemTableManager.GetInstance().GetBattleMaxNum(tableId);

                    //现在所有的数量  eg 2
                    int nowCount = Role.role.GetItemProperty().GetItemCountByTableId(tableId);
                    //最终可以最大化数量  eg 2
                    int maxNum = nowCount > maxBat ? maxBat : nowCount;

                    Role.role.GetItemProperty().UpdateBattleItem(tableId, maxNum, i);
                }
            }
            //装满后新的战斗道具
            battleItems = Role.role.GetItemProperty().GetAllBattleItem();

            Item[] bitem = Role.role.GetItemProperty().GetAllBattleItem();
            int[] tids = new int[5];
            int[] tnums = new int[5];
            for (int i = 0; i < 5; i++)
            {
                if (bitem[i] == null)
                {
                    tids[i] = -1;
                    tnums[i] = 0;
                }
                else
                {
                    tids[i] = bitem[i].m_iTableID;
                    tnums[i] = bitem[i].m_iNum;
                }
            }
            //发送服务器数据
            SendAgent.SendBattleItemEdit(Role.role.GetBaseProperty().m_iPlayerId, tids, tnums);
        }

        for (int i = 0; i < 5; i++)
        {
            if (null == battleItems[i])
            {
                m_vecBattleItems[i].m_cSpItem.enabled = false;
                m_vecBattleItems[i].m_cLbCount.enabled = false;
                m_vecBattleItems[i].m_cLbName.enabled = false;
            }
            else
            {
                m_vecBattleItems[i].m_cSpItem.enabled = true;
                m_vecBattleItems[i].m_cLbCount.enabled = true;
                m_vecBattleItems[i].m_cLbName.enabled = true;

                m_vecBattleItems[i].m_cLbName.text = battleItems[i].m_strShortName;
                m_vecBattleItems[i].m_cLbCount.text = "×" + battleItems[i].m_iNum.ToString();
                GUI_FUNCTION.SET_ITEMM(m_vecBattleItems[i].m_cSpItem, battleItems[i].m_strSprName);
            }
        }
    }

    /// <summary>
    /// 根据当前队伍，重排队伍数组
    /// </summary>
    /// <param name="heroTeam"></param>
    /// <param name="currentTeam"></param>
    /// <returns></returns>
    private HeroTeam[] Reverse(HeroTeam[] heroTeam, int currentTeam)
    {
        HeroTeam[] re = new HeroTeam[heroTeam.Length];
        for (int i = 0; i < heroTeam.Length; i++)
        {
            int index = (i + currentTeam) % heroTeam.Length;
            re[i] = heroTeam[index];
        }
        return re;
    }

    /// <summary>
    /// 第一次显示填充3个滑动项目
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="SelectTeamIndex"></param>
    private void SetShow(GameObject parent, int SelectTeamIndex)
    {
        HeroTeam arrtmp = m_vecTeams[SelectTeamIndex];

        List<Hero> hrs = new List<Hero>();   
        //本方队长

        hrs.Add(Role.role.GetHeroProperty().GetHero(arrtmp.m_iLeadID));
        //其他4人
        for (int i = 0; i < 5; i++)
        {
            if (arrtmp.m_vecTeam[i] == arrtmp.m_iLeadID)
            {
                continue;
            }
            hrs.Add(Role.role.GetHeroProperty().GetHero(arrtmp.m_vecTeam[i]));
        }
        //友方队长
        var friend = GLOBAL_DEFINE.m_cSelectBattleFriend;  
        hrs.Add(friend.m_cLeaderHero);

        FightReadyItem tmp = new FightReadyItem(hrs, parent);

        int index = (SelectTeamIndex + 1 + m_iTeamId) % m_vecTeams.Length;
        tmp.m_cLbTeamNum.text = "队伍" + (index == 0 ? 10 : index).ToString();
        m_lstSlideItem.Add(tmp);
    }

    /// <summary>
    /// 滑动中刷新移动项目
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="SelectTeamIndex"></param>
    private void ReflashSlideItem(FightReadyItem tmp, int SelectTeamIndex)
    {
        int[] arrtmp = m_vecTeams[SelectTeamIndex].m_vecTeam;

        List<Hero> hrs = new List<Hero>();   //本方队长
        hrs.Add(Role.role.GetHeroProperty().GetHero(arrtmp[2]));

        hrs.Add(Role.role.GetHeroProperty().GetHero(arrtmp[0]));
        hrs.Add(Role.role.GetHeroProperty().GetHero(arrtmp[1]));
        hrs.Add(Role.role.GetHeroProperty().GetHero(arrtmp[3]));
        hrs.Add(Role.role.GetHeroProperty().GetHero(arrtmp[4]));

        List<Hero> tes = hrs.ToList();

        BattleFriend friend = GLOBAL_DEFINE.m_cSelectBattleFriend;  //友方队长
        hrs.Add(friend.m_cLeaderHero);

        tmp.UpdateShowItem(hrs);

        int index = (SelectTeamIndex + 1 + m_iTeamId) % m_vecTeams.Length;
        tmp.m_cLbTeamNum.text = "队伍" + (index == 0 ? 10 : index).ToString();
    }

    /// <summary>
    /// 设置标题
    /// </summary>
    /// <param name="title"></param>
    public void SetTitle(string tittle)
    {
        this.m_strTittle = tittle;
    }

    /// <summary>
    /// 设置体力消费值
    /// </summary>
    /// <param name="hpCost"></param>
    public void SetHpCost(int hpCost)
    {
        this.m_iHPcost = hpCost;
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        ResourceMgr.UnloadUnusedResources();

        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        CTween.TweenPosition(this.m_cPanInfo, GAME_DEFINE.FADEOUT_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0));
        CTween.TweenPosition(this.m_cPanCancel, GAME_DEFINE.FADEOUT_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-540, 270, 0) , Destory);
        //SetLocalPos(Vector3.one * 0xFFFFF);
        //base.Hiden();
    }

    /// <summary>
    /// 立即隐藏
    /// </summary>
    public override void HidenImmediately()
    {
        base.HidenImmediately();

        ResourceMgr.UnloadUnusedResources();

        //SetLocalPos(Vector3.one * 0xFFFF);
        Destory();
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

        if (!IsShow()) return false;

        if (this.m_cEffectLeft != null)
        {
            this.m_cEffectLeft.Update();
        }
        if (this.m_cEffectRight != null)
        {
            this.m_cEffectRight.Update();
        }

        return true;
    }

    /// <summary>
    /// 销毁对象
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        this.m_cBtnCancel = null;
        this.m_cPanCancel = null;
        this.m_cBtnBackToMain = null;
        this.m_cBtnFight = null;
        this.m_cBtnMonster = null;
        this.m_cBtnGood = null;
        this.m_cBackFrameTop = null;
        this.m_cBackFrameBottom = null;
        this.m_cPanInfo = null;
        this.m_cPiontL = null;
        this.m_cTable = null;
        this.m_cSprArrowLeft = null;
        this.m_cSprArrowRight = null;
        this.m_cLbStrength = null;
        this.m_cLbTitle = null;
        this.m_cTogAutoFull = null;

        this.m_lstSlideItem.Clear();
        this.m_vecBattleItems = null;

        this.m_cEffectLeft = null;
        this.m_cEffectRight = null;

        base.Destory();
    }

    /// <summary>
    /// 取消事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnCancel_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.Hiden();
            GUIFriendFight friendfight = (GUIFriendFight)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FRIENDFIGHT);
            friendfight.Show();
        }
    }

    /// <summary>
    /// 返回主界面事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnBackToMain_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.Hiden();


            GUIBackFrameBottom backbottom = (GUIBackFrameBottom)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM);
            backbottom.ShowHalf();
            GUIMain guimain = (GUIMain)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_MAIN);
            guimain.Show();

        }
    }

    /// <summary>
    /// 战斗事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnFight_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            //Cost超限控制
			HeroTeam heroTeam = CModelMgr.sInstance.GetModel<HeroTeam>();
            if (heroTeam.GetCurTeamCost() > RoleExpTableManager.GetInstance().GetMaxCost(Role.role.GetBaseProperty().m_iLevel))
            {
                GUI_FUNCTION.MESSAGEM(CostOverMax, GAME_FUNCTION.STRING(STRING_DEFINE.INFO_COST_OVER_MAX));
                return;
            }


            //减少相应体力
            if (this.m_iHPcost > Role.role.GetBaseProperty().m_iStrength)
            {
                GUI_FUNCTION.MESSAGEL(null, GAME_FUNCTION.STRING(STRING_DEFINE.STRENGTH_NOT_ENOUGH));
                return;
            }

            Hiden();

            GUIBackFrameTop guitop = (GUIBackFrameTop)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP);
            int cost = this.m_iHPcost;

            GUIBackFrameBottom guibot = (GUIBackFrameBottom)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM);
            guibot.HidenImmediately();

            GUIFriendFight gui = (GUIFriendFight)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FRIENDFIGHT);
            guitop.DecStrength(cost);
			MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_BATTLE_JOIN);
//            MediaMgr.PlaySound2(SOUND_DEFINE.SE_BATTLE_JOIN);
            GAME_FUNCTION.EXCUTE_DELAY(GAME_DEFINE.FADEOUT_GUI_TIME, BeginGateBattle);

			int index = (m_iSelectId + m_iTeamId) % heroTeam.Count + 1;
			Role.role.GetBaseProperty().m_iCurrentTeam = index == 0 ? 10 : index;
        }
    }

    /// <summary>
    /// 变更队伍事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnMonster_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.Hiden();

            GUIBackFrameBottom tmpbottom = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
            tmpbottom.ShowHalfImmediately();
            tmpbottom.OpenOrCloseDarkFrame(true);

            GUITeamEditor tmpediter = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_TEAM_EDITOR) as GUITeamEditor;
            tmpediter.Show(TeamAndItemEditBack);
        }
    }

    /// <summary>
    /// 队伍编辑和物品编辑回掉
    /// </summary>
    private void TeamAndItemEditBack()
    {
        GUIBackFrameBottom tmpbottom = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        tmpbottom.HiddenHalf();
        tmpbottom.OpenOrCloseDarkFrame(false);

        this.Show();
    }

    /// <summary>
    /// 变更物品栏界面
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnGood_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();


            GUIBackFrameBottom tmpbottom = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
            tmpbottom.ShowHalfImmediately();
            tmpbottom.OpenOrCloseDarkFrame(true);

            this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_TOWN).Show();
            GUIPropsGroup tmp = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_PROPSGROUP) as GUIPropsGroup;
            tmp.Show();
            tmp.m_bBackToFightReady = true;
        }
    }

    /// <summary>
    /// 单个物体滑动中
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void Drag_OnEvent(GUI_INPUT_INFO info, object[] args)
    {

        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.DRAG)
        {
            if (m_bTweening)
            {
                return;
            }
            m_bIsDraging = true;
            m_bIsRight = (info.m_vecDelta.x > 0);
            //滑动中，找到最右边或最左边缺少的项目，将另一边多余的回填过来，并将对应的team和英雄数据填写新的项目 
            //eg： 10，1，2 --》  9 10 1 （10和1的gameobject不变，2的obj位置改变，数据从Team重新读取lst的next那个）
            var left = m_lstSlideItem.Find(new Predicate<FightReadyItem>((item) => { return item.m_cItem.transform.localPosition.x + m_cTable.transform.localPosition.x >= 640 * (m_lstSlideItem.Count - 1); }));
            if (left != null)
            {
                left.m_cItem.transform.localPosition -= new Vector3(640 * m_lstSlideItem.Count, 0, 0);
                int NextIndex = m_iSelectId - 1;
                if (NextIndex < 0)
                {
                    NextIndex = 9;
                }
                ReflashSlideItem(left, NextIndex);
            }
            var right = m_lstSlideItem.Find(new Predicate<FightReadyItem>((item) => { return item.m_cItem.transform.localPosition.x + m_cTable.transform.localPosition.x <= -640 * (m_lstSlideItem.Count - 1); }));
            if (right != null)
            {
                right.m_cItem.transform.localPosition += new Vector3(640 * m_lstSlideItem.Count, 0, 0);
                int NextIndex = m_iSelectId + 1;
                if (NextIndex > 9)
                {
                    NextIndex = 0;
                }

                ReflashSlideItem(right, NextIndex);
            }
            //跟随drag移动
            this.m_cTable.transform.localPosition += new Vector3(info.m_vecDelta.x, 0, 0);
        }
        else if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.PRESS
            && !info.m_bDone)  //drag 结束
        {
            if (m_bIsDraging)  //drag 结束事件只执行一次
            {
                m_bIsDraging = false;

                m_bTweening = true;
                CTween.TweenPosition(this.m_cTable, 0, 0.3F, m_cTable.transform.localPosition, new Vector3(m_fVIndex * 640 + (m_bIsRight ? 640 : -640), 0, 0), TWEEN_LINE_TYPE.ElasticInOut, TweenFinish);  //结束剩余动画
                m_fVIndex += m_bIsRight ? 1 : -1;


                //根据位置偏移offest 余10得到当前选中的项目
                if (m_fVIndex < 0)
                {
                    m_iSelectId = -m_fVIndex % 10;
                }
                else
                {
                    m_iSelectId = 10 - m_fVIndex % 10;
                    m_iSelectId = m_iSelectId == 10 ? 0 : m_iSelectId;
                }

                int index = (m_iSelectId + m_iTeamId) % m_vecTeams.Length;

                //更新当前选中TeamId
                Role.role.GetBaseProperty().m_iCurrentTeam = index;  //赋值滑动选中的TeamID

                //更新导航选中亮点覆盖灰点
                this.m_cPiontL.transform.localPosition = new Vector3(-180 + index * 40, m_cPiontL.transform.localPosition.y, 0);


            }
        }
    }

    /// <summary>
    /// 导航箭头向左移
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void Left_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            //手动触发两次滑动
            Drag_OnEvent(new GUI_INPUT_INFO() { m_eType = GUI_INPUT_INFO.GUI_INPUT_TYPE.DRAG, m_vecDelta = new Vector2(10, 0) }, null);
            Drag_OnEvent(new GUI_INPUT_INFO() { m_eType = GUI_INPUT_INFO.GUI_INPUT_TYPE.PRESS }, null);
        }
    }

    /// <summary>
    /// 导航箭头向右移
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void Right_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            //手动触发两次滑动
            Drag_OnEvent(new GUI_INPUT_INFO() { m_eType = GUI_INPUT_INFO.GUI_INPUT_TYPE.DRAG, m_vecDelta = new Vector2(-10, 0) }, null);
            Drag_OnEvent(new GUI_INPUT_INFO() { m_eType = GUI_INPUT_INFO.GUI_INPUT_TYPE.PRESS }, null);
        }
    }

    /// <summary>
    /// 动画结束回调
    /// </summary>
    private void TweenFinish()
    {
        m_bTweening = false;
    }

    /// <summary>
    /// 开始关卡战斗
    /// </summary>
    /// <param name="arg"></param>
    private void BeginGateBattle()
    {
        GUIFriendFight gui = (GUIFriendFight)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FRIENDFIGHT);
        if (gui.GetLastGuiId() == GUI_DEFINE.GUIID_AREADUNGEON)
        {
            SendAgent.SendBattleGateStartReq(Role.role.GetBaseProperty().m_iPlayerId, WorldManager.s_iCurrentWorldId, WorldManager.s_iCurrentAreaIndex, WorldManager.s_iCurrentDungeonIndex, WorldManager.s_iCurrentGateIndex);
        }
        else if (gui.GetLastGuiId() == GUI_DEFINE.GUIID_ESPDUNGEONGATE)
        {
            SendAgent.SendActivityBattleStartReq(Role.role.GetBaseProperty().m_iPlayerId, WorldManager.s_iCurEspDungeonId, WorldManager.s_iCurEspDungeonGateIndex);
        }
    }

    /// <summary>
    /// 自动填满勾选框 状态改变
    /// </summary>
    public void Toggle_OnChange()
    {
        GAME_SETTING.SaveBattelAutoFull(m_cTogAutoFull.value);
        UpdateBattleItemShow();
    }

    /// Cost超出上限跳转事件
    /// </summary>
    private void CostOverMax()
    {

    }
}