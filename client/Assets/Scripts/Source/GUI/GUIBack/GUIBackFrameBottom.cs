//Micro.Sanvey
//2013.11.12
//sanvey.china@gmail.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using Game.Resource;


/// <summary>
/// 背景层底部菜单栏
/// </summary>
public class GUIBackFrameBottom : GUIBase
{
    private const string RES_BACKFRAMEBOTTOM = "GUI_BackFrameBottom";   //背景菜单栏地址
    private const string LabelInformationBar = "FlootBase/LbSummary";   //信息栏地址
    private const string BTN_CALL = "FlootBase/BtnCall";    //召唤按钮
    private const string BTN_FRIEND = "FlootBase/BtnFriend"; //好友按钮
    private const string BTN_MAIN = "FlootBase/BtnMain";  //主界面按钮
    private const string BTN_MONSTER = "FlootBase/BtnMonster"; //单位按钮
    private const string BTN_STORE = "FlootBase/BtnStore";  //商店按钮
    private const string BTN_TOWN = "FlootBase/BtnTown";  //城镇按钮
    private const string RES_DARKFRAME = "FlootBase/DarkFrame"; //底部遮罩
    private const string RES_FLOOT = "FlootBase";
    private const string RES_BAR = "bg";
    private const string RES_COUNT = "count";
    private const string RES_WARN = "warn";

    private UILabel m_cInfoLabel; //信息栏
    private GameObject m_cBtnCall; //召唤按钮
    private GameObject m_cBtnFriend; //好友按钮
    private GameObject m_cBtnMain; //主界面按钮
    private GameObject m_cBtnMonster;  //单位按钮
    private GameObject m_cBtnStore; //商店按钮
    private GameObject m_cBtnTown; //城镇按钮
    private GameObject m_cDarkFrame;//底部遮罩
    private GameObject m_cFlootBase;
    private UISprite m_cSpBarSummon;
    private UILabel m_cLbCountSummon;
    private UISprite m_cSpBarFriend;
    private UILabel m_cLbCountFriend;
    public UISprite m_cHeroWarn;
    public UISprite m_cTwonWarn;

    public const int m_iFriendPer = 200;  //200友情点招募一次

    public GUIBackFrameBottom(GUIManager mgr)
        : base(mgr, GUI_DEFINE.GUIID_BACKFRAMEBOTTOM, GUILAYER.GUI_MENU)
    {
    }

    /// <summary>
    /// 隐藏一半
    /// </summary>
    public void HiddenHalf()
    {
        if (this.m_cGUIObject != null)
        {
            CTween.TweenPosition(this.m_cFlootBase, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, this.m_cFlootBase.transform.localPosition, new Vector3(0, -105, 0));
        }
    }

    /// <summary>
    /// 显示出全部
    /// </summary>
    public void ShowHalf()
    {
        if (m_cGUIObject != null)
        {
            SetFriendApllyNumber(Role.role.GetBaseProperty().m_iFriendApplyCount + Role.role.GetBaseProperty().m_iFriendGiftCount);
            //友情点招募计算
            int fCount = Role.role.GetBaseProperty().m_iFriendPoint / m_iFriendPer;       //可以招募数量
            if (fCount > 99)
            {
                fCount = 99;
            }
            if (fCount == 0)  //不可招募
            {
                //隐藏招募数量提示
                this.m_cSpBarSummon.enabled = false;
                this.m_cLbCountSummon.enabled = false;
            }
            else  //提示招募数量
            {
                //隐藏招募数量提示
                m_cSpBarSummon.enabled = true;
                m_cLbCountSummon.enabled = true;
                m_cLbCountSummon.text = fCount.ToString();
            }

            CTween.TweenPosition(this.m_cFlootBase, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, this.m_cFlootBase.transform.localPosition, new Vector3(0, 0, 0));

        }

    }

    public void HidenHalfImmediately()
    {
        if (this.m_cGUIObject != null)
        {
            this.m_cFlootBase.transform.localPosition = new Vector3(0, -105, 0);
        }
    }

    public void ShowHalfImmediately()
    {
        if (m_cGUIObject != null)
        {
            SetFriendApllyNumber(Role.role.GetBaseProperty().m_iFriendApplyCount + Role.role.GetBaseProperty().m_iFriendGiftCount);

            //友情点招募计算
            int fCount = Role.role.GetBaseProperty().m_iFriendPoint / m_iFriendPer;       //可以招募数量
            if (fCount > 99)
            {
                fCount = 99;
            }
            if (fCount == 0)  //不可招募
            {
                //隐藏招募数量提示
                this.m_cSpBarSummon.enabled = false;
                this.m_cLbCountSummon.enabled = false;
            }
            else  //提示招募数量
            {
                //隐藏招募数量提示
                m_cSpBarSummon.enabled = true;
                m_cLbCountSummon.enabled = true;
                m_cLbCountSummon.text = fCount.ToString();
            }

            this.m_cFlootBase.transform.localPosition = new Vector3(0, 0, 0);
        }

    }

    /// <summary>
    /// 设置好友状态数
    /// </summary>
    /// <param name="count"></param>
    public void SetFriendApllyNumber(int count) 
    {
        if (this.m_cGUIObject != null)
        { 
            int listCount = 0;
            if (count > 99)
            {
                listCount = 99;
                this.m_cSpBarFriend.enabled = true;
                this.m_cLbCountFriend.enabled = true;
                this.m_cLbCountFriend.text = listCount.ToString();
            }
            else if (count == 0)
            {
                this.m_cSpBarFriend.enabled = false;
                this.m_cLbCountFriend.enabled = false;
            }
            else {
                listCount = count;
                this.m_cSpBarFriend.enabled = true;
                this.m_cLbCountFriend.enabled = true;
                this.m_cLbCountFriend.text = listCount.ToString();
            }
        }
    }

    /// <summary>S
    /// 滚动显示
    /// </summary>
    /// <param name="content"></param>
    /// <param name="isNewContent"></param>
    public void InformationBarMoveAnimation(string content, bool isNewContent)
    {
        if (isNewContent)
        {
            this.m_cInfoLabel.text = content;
            this.m_cInfoLabel.transform.localPosition = new Vector3(310, -359, 0);
            TweenPosition.Begin(this.m_cInfoLabel.gameObject, 10f, new Vector3(-660, -359, 0)).style = UITweener.Style.Loop;
        }
        else
        {
            this.m_cInfoLabel.transform.localPosition = new Vector3(310, -359, 0);
            TweenPosition.Begin(this.m_cInfoLabel.gameObject, 10f, new Vector3(-660, -359, 0)).style = UITweener.Style.Loop;
        }
    }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        base.Show();


        if (this.m_cGUIObject == null)
        {
            this.m_cGUIObject = GameObject.Instantiate(ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_GUI_CACHE ,RES_BACKFRAMEBOTTOM) as UnityEngine.Object) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            this.m_cInfoLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LabelInformationBar);
            InformationBarMoveAnimation("", false);

            //召唤按钮
            this.m_cBtnCall = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_CALL);
            var gui_event = this.m_cBtnCall.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnCall_OnEvent);
            this.m_cSpBarSummon = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cBtnCall, RES_BAR);
            this.m_cLbCountSummon = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cBtnCall, RES_COUNT);
            //好友按钮
            this.m_cBtnFriend = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_FRIEND);
            gui_event = this.m_cBtnFriend.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnFriend_OnEvent);
            this.m_cSpBarFriend = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cBtnFriend, RES_BAR);
            this.m_cLbCountFriend = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cBtnFriend, RES_COUNT);
            //主界面按钮
            this.m_cBtnMain = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_MAIN);
            gui_event = this.m_cBtnMain.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnMain_OnEvent);
            //单位按钮
            this.m_cBtnMonster = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_MONSTER);
            gui_event = this.m_cBtnMonster.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnMonster_OnEvent);
            this.m_cHeroWarn = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cBtnMonster, RES_WARN);
            //商店按钮
            this.m_cBtnStore = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_STORE);
            gui_event = this.m_cBtnStore.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnStore_OnEvent);
            //城镇按钮
            this.m_cBtnTown = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_TOWN);
            gui_event = this.m_cBtnTown.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnTwon_OnEvent);
            this.m_cTwonWarn = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cBtnTown, RES_WARN);
            //底部遮罩
            this.m_cDarkFrame = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, RES_DARKFRAME);

            this.m_cFlootBase = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, RES_FLOOT);
        }

        SetFriendApllyNumber(Role.role.GetBaseProperty().m_iFriendApplyCount + Role.role.GetBaseProperty().m_iFriendGiftCount);

        //友情点招募计算
        int fCount = Role.role.GetBaseProperty().m_iFriendPoint / m_iFriendPer;       //可以招募数量
        if (fCount > 99)
        {
            fCount = 99;
        }
        if (fCount == 0)  //不可招募
        {
            //隐藏招募数量提示
            this.m_cSpBarSummon.enabled = false;
            this.m_cLbCountSummon.enabled = false;
        }
        else  //提示招募数量
        {
            //隐藏招募数量提示
            m_cSpBarSummon.enabled = true;
            m_cLbCountSummon.enabled = true;
            m_cLbCountSummon.text = fCount.ToString();
        }

        //英雄界面提示
        if (GAME_SETTING.s_iWarnHeroEquip == 1 || GAME_SETTING.s_iWarnHeroEquip == 1)
        {
            m_cHeroWarn.enabled = true;
        }
        else
        {
            bool flag = false;
            List<Hero> list = Role.role.GetHeroProperty().GetAllHero();
            foreach (Hero hero in list)
            {
                if (hero.m_iEvolutionID != 0)
                {
                    float spentGold;
                    if (GAME_DEFINE.m_vecEvolutionHeroID.Contains(hero.m_iTableID))
                    {
                        spentGold = GAME_DEFINE.m_iEvolutionSpent[hero.m_iStarLevel - 1];
                    }
                    else
                    {
                        spentGold = GAME_DEFINE.m_iOtherEvolutionSpent[hero.m_iStarLevel - 1];
                    }

                    bool canEvo = true;  //素材是否足够
                    for (int i = 0; i < hero.m_vecEvolution.Length; i++)
                    {
                        if (hero.m_vecEvolution[i] != 0)
                        {
                            int deleIndex = list.FindIndex(q => { return q.m_iTableID == hero.m_vecEvolution[i]; });
                            if (deleIndex < 0)
                            {
                                canEvo = false;
                            }
                        }
                    }
                    //判断进化条件是否全部满足
                    if (spentGold <= Role.role.GetBaseProperty().m_iGold && canEvo && hero.m_iLevel == hero.m_iMaxLevel)
                    {
                        if (GAME_SETTING.s_dicWarnHeroJinhua.ContainsKey(hero.m_iTableID))
                        {
                            if (GAME_SETTING.s_dicWarnHeroJinhua[hero.m_iTableID] == 1)
                            {
                                flag = true;
                                break;
                            }
                        }
                        else
                        {
                            flag = true;
                            break;
                        }
                    }
                }
            }
            if (flag)
                this.m_cHeroWarn.enabled = true;
            else
                this.m_cHeroWarn.enabled = false;
        }
        //桃园提示
        if (GAME_SETTING.s_iWarnHouseEquip == 1 || GAME_SETTING.s_iWarnHouseTiaohe == 1)
        {
            this.m_cTwonWarn.enabled = true;
        }
        else
        {
            bool flag = false;
            if (GAME_SETTING.s_bEquipLevelUp)
            {
                if (Role.role.GetItemProperty().CheckNewEquipWarn(GAME_SETTING.s_iEquipLevelAdd))
                {
                    flag = true;
                    GAME_SETTING.s_iWarnHouseEquip = 1;
                    GAME_SETTING.SaveWarnHouseEquip();
                }
            }
            if (flag)
                this.m_cTwonWarn.enabled = true;
            else
            {
                if (GAME_SETTING.s_bItemLevelUp)
                {
                    if (Role.role.GetItemProperty().CheckNewItemWarn(GAME_SETTING.s_iItemLevelAdd))
                    {
                        m_cTwonWarn.enabled = true;
                        GAME_SETTING.s_iWarnHouseTiaohe = 1;
                        GAME_SETTING.SaveWarnHouseTiaohe();
                    }
                    else
                        m_cTwonWarn.enabled = false;
                }
                else
                    m_cTwonWarn.enabled = false;
            }

        }

        SetLocalPos(Vector3.zero);
        ShowHalf();

    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
       base.Hiden();
       //SetLocalPos(Vector3.one * 0xFFFF);
       Destory();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        this.m_cInfoLabel = null;
        this.m_cBtnCall = null;
        this.m_cBtnFriend = null;
        this.m_cBtnMain = null;
        this.m_cBtnMonster = null;
        this.m_cBtnStore = null;
        this.m_cBtnTown = null;
        this.m_cDarkFrame = null;
        this.m_cFlootBase = null;
        this.m_cSpBarSummon = null;
        this.m_cLbCountSummon = null;
        this.m_cSpBarFriend = null;
        this.m_cLbCountFriend = null;
        this.m_cHeroWarn = null;
        this.m_cTwonWarn = null;

        base.Destory();
    }

    /// <summary>
    /// 召唤界面
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnCall_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (Role.role.GetHeroProperty().GetAllHero().Count >= Role.role.GetBaseProperty().m_iMaxHeroCount)
            {
                GUI_FUNCTION.MESSAGEM_(MessageCallBack_HeroMax, GAME_FUNCTION.STRING(STRING_DEFINE.WARNING_MAX_HERO), "btn_expand", "btn_expand1", "btn_hero", "btn_hero1");
                return;
            }
            this.m_cGUIMgr.HidenCurGUI();
            if (SessionManager.GetInstance().Refresh())
            {
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_SUMMON).Show);
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show);
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show);
            }
            else
            {
                //召唤
                GUISummon guicall = (GUISummon)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_SUMMON);
                guicall.Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
            }
        }
    }

    /// <summary>
    /// 好友按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnFriend_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.m_cGUIMgr.HidenCurGUI();
            if (SessionManager.GetInstance().Refresh())
            {
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FRIENDMENU).Show);
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show);
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show);
            }
            else
            {
                GUIFriendMenu friendmenu = (GUIFriendMenu)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FRIENDMENU);
                friendmenu.Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
            }
        }
    }

    /// <summary>
    /// 主界面
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnMain_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.m_cGUIMgr.HidenCurGUI();

            if (SessionManager.GetInstance().Refresh())
            {
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_MAIN).Show);
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show);
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show);
            }
            else
            {
                GUIMain guimain = (GUIMain)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_MAIN);
                guimain.Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
            }
        }
    }

    /// <summary>
    /// 单位界面
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnMonster_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.m_cGUIMgr.HidenCurGUI();
            if (SessionManager.GetInstance().Refresh())
            {
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERO_MENU).Show);
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show);
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show);
            }
            else
            {
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERO_MENU).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
            }
        }
    }

    /// <summary>
    /// 商城界面
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnStore_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.m_cGUIMgr.HidenCurGUI();

            if (SessionManager.GetInstance().Refresh())
            {
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_STORE).Show);
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show);
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show);
            }
            else
            {
                GUIStore store = (GUIStore)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_STORE);
                store.Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
            }
        }
    }

    /// <summary>
    /// 城镇界面
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnTwon_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.m_cGUIMgr.HidenCurGUI();

            if (SessionManager.GetInstance().Refresh())
            {
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_TOWN).Show);
            }
            else
            {
                GUITown town = (GUITown)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_TOWN);
                town.Show();
            }
        }
    }

    /// <summary>
    /// 英雄超限对话框CallBack
    /// </summary>
    /// <param name="reuslt"></param>
    private void MessageCallBack_HeroMax(bool result)
    {
        if (result)  //扩大单位数量
        {
            this.m_cGUIMgr.HidenCurGUI();

            if (SessionManager.GetInstance().Refresh())
            {
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_UNITSLOTEXPANSION).Show);
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show);
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show);
            }
            else
            {
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_UNITSLOTEXPANSION).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
            }
        }
        else  //出售
        {

            this.m_cGUIMgr.HidenCurGUI();
            if (SessionManager.GetInstance().Refresh())
            {
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERO_MENU).Show);
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show);
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show);
            }
            else
            {
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERO_MENU).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
            }
        }
    }

    /// <summary>
    /// 设置Bottom层级 eg: 招募界面，招募大图需要在bottom下面遮挡
    /// </summary>
    /// <param name="p"></param>
    public void SetPanelDepth(int p)
    {
        this.m_cGUIObject.GetComponent<UIPanel>().depth = p;
    }

    /// <summary>
    /// 更新底部提示信息
    /// </summary>
    /// <param name="str"></param>
    public void ChangeBottomLabel(string str)
    {
        if (this.IsShow())
        {
            if (str != this.m_cInfoLabel.text)
            {
                this.m_cInfoLabel.text = str;
                this.m_cInfoLabel.transform.localPosition = new Vector3(310, -359, 0);
                TweenPosition.Begin(this.m_cInfoLabel.gameObject, 10f, new Vector3(-660, -359, 0)).style = UITweener.Style.Loop;
            }
            else
            {
                this.m_cInfoLabel.transform.localPosition = new Vector3(310, -359, 0);
                TweenPosition.Begin(this.m_cInfoLabel.gameObject, 10f, new Vector3(-660, -359, 0)).style = UITweener.Style.Loop;
            }
        }
    }

    /// <summary>
    /// 打开或关闭底部遮罩
    /// </summary>
    public void OpenOrCloseDarkFrame(bool flag)
    {
        this.m_cDarkFrame.SetActive(flag);
    }

    /// <summary>
    /// 英雄界面提示
    /// </summary>
    public void HeroWarn()
    {
        if (GAME_SETTING.s_iWarnHeroEquip == 1 || GAME_SETTING.s_iWarnHeroEquip == 1)
        { 
            m_cHeroWarn.enabled = true;
        }
        else
            m_cHeroWarn.enabled = false;
    }

    /// <summary>
    /// 桃园提示
    /// </summary>
    public void TownWarn()
    {
        if (GAME_SETTING.s_iWarnHouseEquip == 1 || GAME_SETTING.s_iWarnHouseTiaohe == 1)
        {
            this.m_cTwonWarn.enabled = true;
        }
        else
            this.m_cTwonWarn.enabled = false;
    }
}