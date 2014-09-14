using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Game.Base;

//  GUIManager.cs
//  Author: Lu Zexi
//  2013-11-12



/// <summary>
/// GUI管理器
/// </summary>
public class GUIManager
{
    private const int RETAIN_GUI = 0;   //保留隐藏的GUI数量
    private const float EXIST_TIME = 0F;   //存留时间
    private const float CHECK_TIME = 0.5F;    //检查时间
    private List<GUIBase> m_lstGUI; //GUI列表

    private int m_iCurGUIID;    //当前中心展示的GUI
    private float m_fDestoryStartTime; //销毁开始时间

    public GUIManager()
    {
        this.m_lstGUI = new List<GUIBase>();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Initialize()
    {
        this.m_fDestoryStartTime = GAME_TIME.TIME_FIXED();

        GUIBackFrameTop guiBackTop = new GUIBackFrameTop(this);
        AddGUI(guiBackTop);

        GUIBackFrameBottom guiBackBottom = new GUIBackFrameBottom(this);
        AddGUI(guiBackBottom);

        GUIWorld guiWord = new GUIWorld(this);
        AddGUI(guiWord);

        GUIArea guiArea = new GUIArea(this);
        AddGUI(guiArea);

        GUIAreaDungeon guiAreanDungeon = new GUIAreaDungeon(this);
        AddGUI(guiAreanDungeon);

        GUIFriendFight guiFriendFight = new GUIFriendFight(this);
        AddGUI(guiFriendFight);

        GUIMain guiMain = new GUIMain(this);
        AddGUI(guiMain);

        GUIFightReady guiFightReady = new GUIFightReady(this);
        AddGUI(guiFightReady);

        GUIEspDungeon guiEspDungeon = new GUIEspDungeon(this);
        AddGUI(guiEspDungeon);

        GUIEspDungeonGate guiEspGate = new GUIEspDungeonGate(this);
        AddGUI(guiEspGate);

        GUIStore guiStore = new GUIStore(this);
        AddGUI(guiStore);

        GUIGem guiGem = new GUIGem(this);
        AddGUI(guiGem);

        GUIFesta guiFesta = new GUIFesta(this);
        AddGUI(guiFesta);

        GUIFestaInvite guiFestaInvite = new GUIFestaInvite(this);
        AddGUI(guiFestaInvite);

        GUISetting guiSetting = new GUISetting(this);
        AddGUI(guiSetting);

        GUIIntelligence guiIntelligence = new GUIIntelligence(this);
        AddGUI(guiIntelligence);

        GUIPropsAtlas guiPropsAtlas = new GUIPropsAtlas(this);
        AddGUI(guiPropsAtlas);

        GUIMail guiGift = new GUIMail(this);
        AddGUI(guiGift);

        GUIHeroDetail guiHeroDetail = new GUIHeroDetail(this);
        AddGUI(guiHeroDetail);

        GUIBodyStrengthRestoration guiBodyStrengthRestoration = new GUIBodyStrengthRestoration(this);
        AddGUI(guiBodyStrengthRestoration);

        GUIPropsSlotExpansion guiPropsSlotExpansion = new GUIPropsSlotExpansion(this);
        AddGUI(guiPropsSlotExpansion);

        GUIUnitSlotExpansion guiUnitSlotExpansion = new GUIUnitSlotExpansion(this);
        AddGUI(guiUnitSlotExpansion);

        GUIFistfightPointRestoration guiFistfightPointRestoration = new GUIFistfightPointRestoration(this);
        AddGUI(guiFistfightPointRestoration);

        GUIResouceLoading guiResourceLoading = new GUIResouceLoading(this);
        AddGUI(guiResourceLoading);

        GUILoading guiLoading = new GUILoading(this);
        AddGUI(guiLoading);

        GUI_Hiden guiHiden = new GUI_Hiden(this);
        AddGUI(guiHiden);

        GUIHelp guiHelp = new GUIHelp(this);
        AddGUI(guiHelp);

        GUIHelpTypeDetail guiHelpDetail = new GUIHelpTypeDetail(this);
        AddGUI(guiHelpDetail);

        GUIHelpProjectDetail guiHelpProjectDetail = new GUIHelpProjectDetail(this);
        AddGUI(guiHelpProjectDetail);

        GUITown guiTown = new GUITown(this);
        AddGUI(guiTown);

        GUIPropsWareHouse propsWareHouse = new GUIPropsWareHouse(this);
        AddGUI(propsWareHouse);

        GUIPropsPreview propsPreview = new GUIPropsPreview(this);
        AddGUI(propsPreview);

        //GUIPropsPreviewDetail guiPropsPreviewDetail = new GUIPropsPreviewDetail(this);
        //AddGUI(guiPropsPreviewDetail);

        GUIPropsSales guiPropsSales = new GUIPropsSales(this);
        AddGUI(guiPropsSales);

        //GUIPropsSalesDetail guiPropsSalesDetail = new GUIPropsSalesDetail(this);
        //AddGUI(guiPropsSalesDetail);

        GUIPropsGroup guiPropsGroup = new GUIPropsGroup(this);
        AddGUI(guiPropsGroup);

        GUIReconceliHouse reconceliHouse = new GUIReconceliHouse(this);
        AddGUI(reconceliHouse);

        GUIEquipmentHouse equipmentHouse = new GUIEquipmentHouse(this);
        AddGUI(equipmentHouse);

        GUISummon guiCall = new GUISummon(this);
        AddGUI(guiCall);

        GUITeamEditor guiTeamEditor = new GUITeamEditor(this);
        AddGUI(guiTeamEditor);


        //GUIAlertView guiAlert = new GUIAlertView(this);
        //AddGUI(guiAlert);

        GUIHeroMenu guiHeroMenu = new GUIHeroMenu(this);
        AddGUI(guiHeroMenu);

        GUIMenu menu = new GUIMenu(this);
        AddGUI(menu);


        GUIUserInfo userinfo = new GUIUserInfo(this);
        AddGUI(userinfo);

        GUIBattleRecord record = new GUIBattleRecord(this);
        AddGUI(record);

        GUIHeroUpgradeMain upgrade = new GUIHeroUpgradeMain(this);
        AddGUI(upgrade);

        GUIGateBattle guiGateBattle = new GUIGateBattle(this);
        AddGUI(guiGateBattle);

        GUIHeroAltas heroatlas = new GUIHeroAltas(this);
        AddGUI(heroatlas);

        GUIHeroEquipment heroEq = new GUIHeroEquipment(this);
        AddGUI(heroEq);

        GUIHeroSell herosell = new GUIHeroSell(this);
        AddGUI(herosell);

        GUIFriendMenu frimenu = new GUIFriendMenu(this);
        AddGUI(frimenu);

        GUIFriendSearch frisearch = new GUIFriendSearch(this);
        AddGUI(frisearch);

        GUIFriendInfo friinfo = new GUIFriendInfo(this);
        AddGUI(friinfo);

        GUIFriendList firlst = new GUIFriendList(this);
        AddGUI(firlst);

        GUIFriendInfoLike friinfol = new GUIFriendInfoLike(this);
        AddGUI(friinfol);

        GUIFriendApply friapp = new GUIFriendApply(this);
        AddGUI(friapp);


        GUITeamHero teamHero = new GUITeamHero(this);
        AddGUI(teamHero);


        GUIFriendGift frigift = new GUIFriendGift(this);
        AddGUI(frigift);

        //GUIFriendGiftB frigiftb = new GUIFriendGiftB(this);
        //AddGUI(frigiftb);

        GUIFriendGiftSelect friselect = new GUIFriendGiftSelect(this);
        AddGUI(friselect);

        GUIEquipUpgrade guiepup = new GUIEquipUpgrade(this);
        AddGUI(guiepup);

        GUIBattleNext gateBattleNext = new GUIBattleNext(this);
        AddGUI(gateBattleNext);

        GUIRoleCreate createRole = new GUIRoleCreate(this);
        AddGUI(createRole);

        GUIHeroChoose heroChoose = new GUIHeroChoose(this);
        AddGUI(heroChoose);

        GUIMessageL guimessage = new GUIMessageL(this);
        AddGUI(guimessage);

        GUIMessageM guimessageM = new GUIMessageM(this);
        AddGUI(guimessageM);

        GUIMessageS guimessageS = new GUIMessageS(this);
        AddGUI(guimessageS);

        GUIMessageSS guimessageSS = new GUIMessageSS(this);
        AddGUI(guimessageSS);

        GUIBattleReward battleReward = new GUIBattleReward(this);
        AddGUI(battleReward);

        GUIBattleMenu battleMenu = new GUIBattleMenu(this);
        AddGUI(battleMenu);

        GUIBackGround guibackground = new GUIBackGround(this);
        AddGUI(guibackground);

        GUIHeroShow guiHeroShow = new GUIHeroShow(this);
        AddGUI(guiHeroShow);

        GUIHeroUpgrade guiUpgradeHero = new GUIHeroUpgrade(this);
        AddGUI(guiUpgradeHero);

        GUIHeroEquipSelect guiEqSelect = new GUIHeroEquipSelect(this);
        AddGUI(guiEqSelect);

        GUIHeroUpgradeSelect guiUpgradeHeroSelect = new GUIHeroUpgradeSelect(this);
        AddGUI(guiUpgradeHeroSelect);

        GUIHeroUpgradeResult guiUpgradeHeroResult = new GUIHeroUpgradeResult(this);
        AddGUI(guiUpgradeHeroResult);

        GUIBattleItemSelect battleselect = new GUIBattleItemSelect(this);
        AddGUI(battleselect);

        GUIReconceCombineDetail comdetail = new GUIReconceCombineDetail(this);
        AddGUI(comdetail);

        GUIHeroEvolutionMain evolution = new GUIHeroEvolutionMain(this);
        AddGUI(evolution);

        GUIHeroEvolution evolutionHero = new GUIHeroEvolution(this);
        AddGUI(evolutionHero);

        GUIHeroEvolutionResult evolutionHeroResult = new GUIHeroEvolutionResult(this);
        AddGUI(evolutionHeroResult);

        GUIActivityBattle activityBattle = new GUIActivityBattle(this);
        AddGUI(activityBattle);

        GUIFriendGiftExpectSelect friendGiftExpectSelect = new GUIFriendGiftExpectSelect(this);
        AddGUI(friendGiftExpectSelect);

        GUIFriendGiftGive friendGiftGive = new GUIFriendGiftGive(this);
        AddGUI(friendGiftGive);

        GUIHeroAltasDetail aldetail = new GUIHeroAltasDetail(this);
        AddGUI(aldetail);

        GUIAnnouncement announcement = new GUIAnnouncement(this);
        AddGUI(announcement);

        GUIFestaInput apiput = new GUIFestaInput(this);
        AddGUI(apiput);

        GUIArena guiArena = new GUIArena(this);
        AddGUI(guiArena);

        GUIArenaFightReady arenaFight = new GUIArenaFightReady(this);
        AddGUI(arenaFight);

        GUIArenaRewardInfo lvIntelligence = new GUIArenaRewardInfo(this);
        AddGUI(lvIntelligence);

        GUIArenaBattleIntelligence battleIntelligence = new GUIArenaBattleIntelligence(this);
        AddGUI(battleIntelligence);

        GUIArenaRankings rankings = new GUIArenaRankings(this);
        AddGUI(rankings);

        GUIBattleArena battleArena = new GUIBattleArena(this);
        AddGUI(battleArena);

        GUISummonResult sumresult = new GUISummonResult(this);
        AddGUI(sumresult);

        GUISummonDetail sumdetail = new GUISummonDetail(this);
        AddGUI(sumdetail);

        GUIAysncLoading aysncLoading = new GUIAysncLoading(this);
        AddGUI(aysncLoading);

        GUIArenaBattleResult arenare = new GUIArenaBattleResult(this);
        AddGUI(arenare);


        GUIArenaBattleAddFriend batfria = new GUIArenaBattleAddFriend(this);
        AddGUI(batfria);

        GUIBattleAddFriend batfrid = new GUIBattleAddFriend(this);
        AddGUI(batfrid);

        //GUIHeroCreate heroCreate = new GUIHeroCreate(this);
        //AddGUI(heroCreate);

        GUIGuide guiGuide = new GUIGuide(this);
        AddGUI(guiGuide);

        GUILockPanel lockPanel = new GUILockPanel(this);
        AddGUI(lockPanel);

        GUIBattleLose battleLose = new GUIBattleLose(this);
        AddGUI(battleLose);

        GUIAccount guiAccount = new GUIAccount(this);
        AddGUI(guiAccount);

        GUIStory guiStory = new GUIStory(this);
        AddGUI(guiStory);

        GUIGuideBattle guiGuideBattle = new GUIGuideBattle(this);
        AddGUI(guiGuideBattle);

        GUILoginReward reward = new GUILoginReward(this);
        AddGUI(reward);

        GUIBattleNewHeroShow newHeroShow = new GUIBattleNewHeroShow(this);
        AddGUI(newHeroShow);

        GUIBattleMenuSetting menuSetting = new GUIBattleMenuSetting(this);
        AddGUI(menuSetting);

        GUIBattleMenuGetHero getHero = new GUIBattleMenuGetHero(this);
        AddGUI(getHero);

        GUIBattleMenuGetItem getItem = new GUIBattleMenuGetItem(this);
        AddGUI(getItem);

        GUIBattleMenuHeroIntelligence heroIntelligence = new GUIBattleMenuHeroIntelligence(this);
        AddGUI(heroIntelligence);

        GUIBattleMenuHelp menuHelp = new GUIBattleMenuHelp(this);
        AddGUI(menuHelp);

        GUIBattleMenuGiveUp giveUp = new GUIBattleMenuGiveUp(this);
        AddGUI(giveUp);

        GUIProductionStaff productionstaff = new GUIProductionStaff(this);
        AddGUI(productionstaff);
    }

    /// <summary>
    /// 获取GUI
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public GUIBase GetGUI(int id)
    {
        foreach (GUIBase item in this.m_lstGUI)
        {
            if (item.ID == id)
            {
                return item;
            }
        }
        return null;
    }

    /// <summary>
    /// 获取所有GUI
    /// </summary>
    /// <returns></returns>
    public List<GUIBase> GetAll()
    {
        return new List<GUIBase>(this.m_lstGUI);
    }

    /// <summary>
    /// 增加GUI
    /// </summary>
    /// <param name="gui"></param>
    public void AddGUI(GUIBase gui)
    {
        if (GetGUI(gui.ID) != null)
        {
            Game.Base.GAME_LOG.ERROR("GUI is ready exist."); 
        }
        this.m_lstGUI.Add(gui);
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public void Update()
    {
        if (GAME_TIME.TIME_FIXED() - this.m_fDestoryStartTime > CHECK_TIME)
        {
            this.m_fDestoryStartTime = GAME_TIME.TIME_FIXED();
            //CheckDestory();
        }

        for (int i = 0; i < this.m_lstGUI.Count; i++)
        {
            GUIBase gui = this.m_lstGUI[i];
            if (gui == null)
            {
                GAME_LOG.ERROR("GUI is null.");
            }
            else
            {
                gui.Update();
            }
        }
    }

    /// <summary>
    /// 渲染更新
    /// </summary>
    public void Render()
    {
        for (int i = 0; i < this.m_lstGUI.Count; i++)
        {
            GUIBase gui = this.m_lstGUI[i];
            if (gui == null)
            {
                GAME_LOG.ERROR("GUI is null.");
            }
            else
            {
                gui.Render();
            }
        }
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public void Destory()
    {
        for (int i = 0; i < this.m_lstGUI.Count; i++)
        {
            if (this.m_lstGUI[i] != null)
            {
                this.m_lstGUI[i].Destory();
            }
        }
    }

    /// <summary>
    /// 检查多时间不用GUI，并销毁
    /// </summary>
    public void CheckDestory()
    {
        //List<GUIBase> lst = new List<GUIBase>();
        //foreach (GUIBase item in this.m_lstGUI)
        //{
        //    if (!item.IsShow() && item.GetGUIObject() != null )
        //        lst.Add(item);
        //}

        //lst.Sort(CompareGUI);

        //if (lst.Count > RETAIN_GUI)
        //{
        //    for (int i = RETAIN_GUI; i < lst.Count; i++)
        //    {
        //        if (GAME_TIME.TIME_FIXED() - lst[i].LastHidenTime > EXIST_TIME)
        //        {
        //            lst[i].Destory();
        //        }
        //    }
        //}

        //Resources.UnloadUnusedAssets();
        //GC.Collect();
    }

    /// <summary>
    /// 比较GUI
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    private int CompareGUI(GUIBase a, GUIBase b)
    {
        return (int)(b.LastHidenTime - a.LastHidenTime);
    }

    /// <summary>
    /// 设置当前展示的中心GUIID
    /// </summary>
    public void SetCurGUIID( int id )
    {
        this.m_iCurGUIID = id;
    }

    /// <summary>
    /// 获取当前GUIID
    /// </summary>
    /// <returns></returns>
    public int GetCurGUIID()
    {
        return this.m_iCurGUIID;
    }

    /// <summary>
    /// 隐藏当前GUI
    /// </summary>
    public void HidenCurGUI()
    {
        if (this.m_iCurGUIID > 0)
        {
            GetGUI(this.m_iCurGUIID).Hiden();
        }
    }

}

