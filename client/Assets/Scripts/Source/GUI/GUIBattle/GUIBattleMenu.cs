using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using Game.Resource;

//战斗-菜单
//Author:sunyi
//2013-12-18

public class GUIBattleMenu : GUIBase
{
    private const string RES_MAIN = "GUI_BattleMenu";//主资源地址

    private const string MENU_PANEL = "MenuPanel";//菜单按钮面板地址
    private const string GETHERO_PANEL = "GetHero";//获得英雄面板地址
    private const string GETIITEM_PANEL = "GetItem";//获得物品面板地址
    private const string HOMENAME_LABEL = "TopPanel/Lab_HomeName";//导航栏标签地址
    private const string HEROINTELLIGENCE_PANEL = "HeroIntelligence";//英雄情报面板地址
    private const string GIVEUP_PANEL = "GiveUp";//放弃战斗面板地址
    private const string GATE_PANEL = "GatePanel";//当前关卡面板地址

    private const string GETHERO_BUTTON = "MenuPanel/Btn_GetHero";//获得英雄按钮地址
    private const string GETITEM_BUTTON = "MenuPanel/Btn_GetItem";//获得素材按钮地址
    private const string HEROINTELLIGENCE_BUTTON = "MenuPanel/Btn_HeroIntelligence";//英雄情报按钮地址
    private const string GIVEUP_BUTTON = "MenuPanel/Btn_GiveUp";//放弃战斗按钮地址

    private const string BACK_BUTTON = "TopPanel/Button_Back";//返回按钮地址
    private const string BTN_SETTING = "Btn_Setting";//设定按钮地址
    private const string BTN_HELP = "Btn_Help";//帮助按钮地址

    private const string LABEL_DUNGEONNAME = "GatePanel/Lab_BattleDungeonName";//副本名标签地址
    private const string LABEL_GATENAME = "GatePanel/Lab_BattleGateName";//副本名标签地址
    private const string SPR_CURLAYER1 = "GatePanel/Spr_CurLevel1";//当前层级数字精灵地址1
    private const string SPR_CURLAYER2 = "GatePanel/Spr_CurLevel2";//当前层级数字精灵地址2
    private const string SPR_MAXLAYER1 = "GatePanel/Spr_MaxLevel1";//最大层级数字精灵地址1
    private const string SPR_MAXLAYER2 = "GatePanel/Spr_MaxLevel2";//最大层级数字精灵地址2
    private const string SLIDE_LAYRER = "GatePanel/GateSlider";//最大层级数字精灵地址


    private GameObject m_cMenuPanel;//菜单按钮面板
    private GameObject m_cGatePanel;//关卡面板

    private GameObject m_cBtnSetting;//设定按钮
    private GameObject m_cBtnHelp;//帮助按钮
    private GameObject m_cBtnGetHero;//获取英雄按钮
    private GameObject m_cBtnGetItem;//获取素材按钮
    private GameObject m_cBtnGetHeroIntelligence;//英雄情报按钮
    private GameObject m_cBtnGiveUp;//放弃战斗按钮

    private GameObject m_cBtnBack;//返回按钮

    private UILabel m_cLabDungeonName;//副本名
    private UILabel m_cLabGateName;//关卡名
    private UISprite m_cSprCurLayer1;//当前层级1
    private UISprite m_cSprCurLayer2;//当前层级2
    private UISprite m_cSprMaxLayer1;//最大层级1
    private UISprite m_cSprMaxLayer2;//最大层级2
    private UISlider m_cSldLayer;//进度条

    private UILabel m_cLabHomeName;//导航栏标签

    private int m_iCurPanel = 0;//当前页面是菜单index
    private List<Hero> m_lstHeros = new List<Hero>();//当前队伍中的英雄列表
    private List<GameObject> m_lstHeroItems = new List<GameObject>();//英雄对象列表

    private string m_strDungeonName;//当前副本名
    private string m_strGateName;//当前关卡名
    private int m_iCurrentLayer;//当前层级--打到第几层了
    private int m_iMaxLayer;//最大层级
    private List<int> m_lstTeamHeroTabelId = new List<int>();//当前队伍所有英雄tableid列表
    private List<int> m_lstTeamHeroLv = new List<int>();//当前队伍所有英雄等级列表
    private List<int> m_lstSoul = new List<int>();//当前获得到的英雄列表
    private List<int> m_lstItemId = new List<int>();//当前战斗中已经获得的物品id
    private List<int> m_lstItemCount = new List<int>();//当前战斗中已经获得的物品个数

    private Action m_cGiveUpAction;
    private bool m_bIsFirstGetHero;
    private bool m_bIsFirstGetItem;
    private GUIBattle m_cGUIBattle; //战斗UI

    public GUIBattleMenu(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_BATTLEMENU, UILAYER.GUI_PANEL2)
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

            this.m_cMenuPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, MENU_PANEL);
            this.m_cMenuPanel.SetActive(true);

            this.m_cLabHomeName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, HOMENAME_LABEL);
            this.m_cLabHomeName.text = "菜单";

            this.m_cGatePanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GATE_PANEL);
            this.m_cGatePanel.SetActive(true);

            this.m_cBtnSetting = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_SETTING);
            GUIComponentEvent settingEvent = this.m_cBtnSetting.AddComponent<GUIComponentEvent>();
            settingEvent.AddIntputDelegate(ShowBattleSettingGui);

            this.m_cBtnHelp = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_HELP);
            GUIComponentEvent helpEvent = this.m_cBtnHelp.AddComponent<GUIComponentEvent>();
            helpEvent.AddIntputDelegate(ShowBattleHelpGui);

            this.m_cBtnGetHero = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GETHERO_BUTTON);
            GUIComponentEvent getHeroEvent = this.m_cBtnGetHero.AddComponent<GUIComponentEvent>();
            getHeroEvent.AddIntputDelegate(ShowGetHero);

            this.m_cBtnGetItem = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GETITEM_BUTTON);
            GUIComponentEvent getItemEvent = this.m_cBtnGetItem.AddComponent<GUIComponentEvent>();
            getItemEvent.AddIntputDelegate(ShowGetItem);

            this.m_cBtnGetHeroIntelligence = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, HEROINTELLIGENCE_BUTTON);
            GUIComponentEvent heroIntelligenceEvent = this.m_cBtnGetHeroIntelligence.AddComponent<GUIComponentEvent>();
            heroIntelligenceEvent.AddIntputDelegate(ShowHeroIntelligence);

            this.m_cBtnGiveUp = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GIVEUP_BUTTON);
            GUIComponentEvent giveUpEvent = this.m_cBtnGiveUp.AddComponent<GUIComponentEvent>();
            giveUpEvent.AddIntputDelegate(ShowGiveUp);

            this.m_cBtnBack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BACK_BUTTON);
            GUIComponentEvent backEvent = this.m_cBtnBack.AddComponent<GUIComponentEvent>();
            backEvent.AddIntputDelegate(BackEvent);

            this.m_cLabDungeonName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LABEL_DUNGEONNAME);

            this.m_cLabGateName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LABEL_GATENAME);

            this.m_cSprCurLayer1 = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SPR_CURLAYER1);
            this.m_cSprCurLayer2 = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SPR_CURLAYER2);

            this.m_cSprMaxLayer1 = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SPR_MAXLAYER1);
            this.m_cSprMaxLayer2 = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SPR_MAXLAYER2);

            this.m_cSldLayer = GUI_FINDATION.GET_OBJ_COMPONENT<UISlider>(this.m_cGUIObject, SLIDE_LAYRER);
        }

        this.m_cLabDungeonName.text = this.m_strDungeonName;
        this.m_cLabGateName.text = this.m_strGateName;
        //设置当前进行到的层级
        if (this.m_iCurrentLayer < 10)
        {
            this.m_cSprCurLayer2.spriteName = this.m_iCurrentLayer.ToString();
            this.m_cSprCurLayer1.spriteName = "0";
        }
        else
        {
            this.m_cSprCurLayer1.spriteName = this.m_iCurrentLayer.ToString().Substring(0, 1);
            this.m_cSprCurLayer2.spriteName = this.m_iCurrentLayer.ToString().Substring(1, 1);
        }
        this.m_cSprCurLayer1.MakePixelPerfect();
        this.m_cSprCurLayer2.MakePixelPerfect();
        if (this.m_iMaxLayer < 10)
        {
            this.m_cSprMaxLayer1.spriteName = "0";
            this.m_cSprMaxLayer2.spriteName = this.m_iMaxLayer.ToString();
        }
        else
        {
            this.m_cSprMaxLayer1.spriteName = this.m_iMaxLayer.ToString().Substring(0, 1);
            this.m_cSprMaxLayer2.spriteName = this.m_iMaxLayer.ToString().Substring(1, 1);
        }
        this.m_cSprMaxLayer1.MakePixelPerfect();
        this.m_cSprMaxLayer2.MakePixelPerfect();

        this.m_cSldLayer.value = (float)this.m_iCurrentLayer / (float)this.m_iMaxLayer;

        SetLocalPos(Vector3.zero);
        //this.m_cGUIMgr.SetCurGUIID(this.m_iID);
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        ResourceMgr.UnloadResource(RES_MAIN);

        if (this.m_lstSoul != null)
        {
            this.m_lstSoul.Clear();
        };

        if (this.m_lstHeros != null)
        {
            this.m_lstHeros.Clear();
        }

        if (this.m_lstTeamHeroTabelId != null)
        {
            this.m_lstTeamHeroTabelId.Clear();
        }

        if (this.m_lstTeamHeroLv != null)
        {
            this.m_lstTeamHeroLv.Clear();
        }

        if (this.m_lstItemId != null)
        {
            this.m_lstItemId.Clear();
        }

        Destory();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        if (m_lstHeroItems != null)
        {
            foreach (GameObject obj in m_lstHeroItems)
            {
                GameObject.Destroy(obj);
            }
            this.m_lstHeroItems.Clear();
        }

        if (this.m_lstSoul != null)
        {
            this.m_lstSoul.Clear();
        };

        if (this.m_lstHeros != null)
        {
            this.m_lstHeros.Clear();
        }

        if (this.m_lstTeamHeroTabelId != null)
        {
            this.m_lstTeamHeroTabelId.Clear();
        }

        if (this.m_lstTeamHeroLv != null)
        {
            this.m_lstTeamHeroLv.Clear();
        }

        if (this.m_lstItemId != null)
        {
            this.m_lstItemId.Clear();
        }

        this.m_cMenuPanel = null;
        this.m_cGatePanel = null;

        this.m_cBtnSetting = null;
        this.m_cBtnHelp = null;
        this.m_cBtnGetHero = null;
        this.m_cBtnGetItem = null;
        this.m_cBtnGetHeroIntelligence = null;
        this.m_cBtnGiveUp = null;

        this.m_cBtnBack = null;

        this.m_cLabDungeonName = null;
        this.m_cLabGateName = null;
        this.m_cSprCurLayer1 = null;
        this.m_cSprCurLayer2 = null;
        this.m_cSprMaxLayer1 = null;
        this.m_cSprMaxLayer2 = null;
        this.m_cSldLayer = null;

        this.m_cLabHomeName = null;

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

        return base.Update();
    }

    /// <summary>
    /// 设置战斗GUI
    /// </summary>
    /// <param name="gui"></param>
    public void SetBattleGUI(GUIBattle gui)
    {
        this.m_cGUIBattle = gui;
    }

    /// <summary>
    /// 展示获得英雄面板
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void ShowGetHero(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.m_cLabHomeName.text = "获得英雄";
            this.m_iCurPanel = (int)CURRENT_PANEL.CURRENT_PANEL_INDEX.GETHERO_PANEL;
            this.m_cMenuPanel.SetActive(false);
            this.m_cBtnSetting.SetActive(false);
            this.m_cBtnHelp.SetActive(false);
            //this.m_cGetHeroPanel.SetActive(true);
            GUIBattleMenuGetHero getHero = (GUIBattleMenuGetHero)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BATTLE_MENU_GETHERO);
            getHero.GetHero(this.m_lstSoul);
            getHero.SetIsFirstGetHero(this.m_bIsFirstGetHero);
            getHero.Show();
        }

    }

    /// <summary>
    /// 展示获得素材面板
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void ShowGetItem(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
      
            this.m_cLabHomeName.text = "获得素材";
            this.m_iCurPanel = (int)CURRENT_PANEL.CURRENT_PANEL_INDEX.GETITEM_PANEL;
            this.m_cMenuPanel.SetActive(false);
            this.m_cBtnSetting.SetActive(false);
            this.m_cBtnHelp.SetActive(false);
            //this.m_cGetItemPanel.SetActive(true);
            GUIBattleMenuGetItem getItem = (GUIBattleMenuGetItem)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BATTLE_MENU_GETITEM);
            getItem.SetGetItem(this.m_lstItemId, this.m_lstItemCount);
            getItem.SetIsFirstGetItem(this.m_bIsFirstGetItem);
            getItem.Show();

            
        }
    }

    /// <summary>
    /// 展示英雄情报面板
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void ShowHeroIntelligence(GUI_INPUT_INFO info, object[] args)
    {

        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            GUIBattleMenuHeroIntelligence heroIntelligence = (GUIBattleMenuHeroIntelligence)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BATTLE_MENU_HERO_INTELLIGENCE);
            heroIntelligence.SetListHeros(this.m_lstTeamHeroTabelId, this.m_lstTeamHeroLv);
            heroIntelligence.Show();

            this.m_iCurPanel = (int)CURRENT_PANEL.CURRENT_PANEL_INDEX.HEROINTELLIGENCE_PANEL;
            this.m_cMenuPanel.SetActive(false);
            //this.m_cHeroIntelligencePanel.SetActive(true);
            this.m_cBtnSetting.SetActive(false);
            this.m_cBtnHelp.SetActive(false);
        }
    }

    /// <summary>
    /// 展示放弃战斗面板
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void ShowGiveUp(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.m_iCurPanel = (int)CURRENT_PANEL.CURRENT_PANEL_INDEX.GIVEUP_PANEL;
            this.m_cMenuPanel.SetActive(false);
            //this.m_cGiveUpPanel.SetActive(true);
            GUIBattleMenuGiveUp giveup = (GUIBattleMenuGiveUp)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BATTLE_MENU_GIVE_UP);
            giveup.Show();
            this.m_cBtnSetting.SetActive(false);
            this.m_cBtnHelp.SetActive(false);
        }
    }

    /// <summary>
    /// 返回按钮事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void BackEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            switch (this.m_iCurPanel)
            {
                case (int)CURRENT_PANEL.CURRENT_PANEL_INDEX.MENU_PANEL:
                    this.m_lstSoul.Clear();
                    Hiden();
                    break;
                case (int)CURRENT_PANEL.CURRENT_PANEL_INDEX.GETHERO_PANEL:
                    //this.m_cGetHeroPanel.SetActive(false);
                    GUIBattleMenuGetHero getHero = (GUIBattleMenuGetHero)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BATTLE_MENU_GETHERO);
                    getHero.Hiden();

                    this.m_cMenuPanel.SetActive(true);
                    this.m_iCurPanel = (int)CURRENT_PANEL.CURRENT_PANEL_INDEX.MENU_PANEL;
                    this.m_cLabHomeName.text = "菜单";
                    this.m_cBtnSetting.SetActive(true);
                    this.m_cBtnHelp.SetActive(true);
                    break;
                case (int)CURRENT_PANEL.CURRENT_PANEL_INDEX.GETITEM_PANEL:
                    //this.m_cGetItemPanel.SetActive(false);
                    GUIBattleMenuGetItem getItem = (GUIBattleMenuGetItem)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BATTLE_MENU_GETITEM);
                    getItem.Hiden();
                    this.m_cMenuPanel.SetActive(true);
                    this.m_iCurPanel = (int)CURRENT_PANEL.CURRENT_PANEL_INDEX.MENU_PANEL;
                    this.m_cLabHomeName.text = "菜单";
                    this.m_cBtnSetting.SetActive(true);
                    this.m_cBtnHelp.SetActive(true);
                    break;
                case (int)CURRENT_PANEL.CURRENT_PANEL_INDEX.HEROINTELLIGENCE_PANEL:
                    //this.m_cHeroIntelligencePanel.SetActive(false);
                    GUIBattleMenuHeroIntelligence heroIntelligence = (GUIBattleMenuHeroIntelligence)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BATTLE_MENU_HERO_INTELLIGENCE);
                    heroIntelligence.Hiden();
                    this.m_cMenuPanel.SetActive(true);
                    this.m_iCurPanel = (int)CURRENT_PANEL.CURRENT_PANEL_INDEX.MENU_PANEL;
                    this.m_cBtnSetting.SetActive(true);
                    this.m_cBtnHelp.SetActive(true);
                    break;
                case (int)CURRENT_PANEL.CURRENT_PANEL_INDEX.GIVEUP_PANEL:
                    //this.m_cGiveUpPanel.SetActive(false);
                    GUIBattleMenuGiveUp giveup = (GUIBattleMenuGiveUp)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BATTLE_MENU_GIVE_UP);
                    giveup.Hiden();
                    this.m_cMenuPanel.SetActive(true);
                    this.m_iCurPanel = (int)CURRENT_PANEL.CURRENT_PANEL_INDEX.MENU_PANEL;
                    this.m_cBtnSetting.SetActive(true);
                    this.m_cBtnHelp.SetActive(true);
                    break;
                case (int)CURRENT_PANEL.CURRENT_PANEL_INDEX.SETTING_PANEL:
                    //this.m_cBattleSettingPanel.SetActive(false);
                    GUIBattleMenuSetting setting = (GUIBattleMenuSetting)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BATTLE_MENU_SETTING);
                    setting.Hiden();
                    this.m_cMenuPanel.SetActive(true);
                    this.m_iCurPanel = (int)CURRENT_PANEL.CURRENT_PANEL_INDEX.MENU_PANEL;
                    GAME_SETTING.SaveSetting();
                    this.m_cBtnSetting.SetActive(true);
                    this.m_cBtnHelp.SetActive(true);
                    this.m_cLabHomeName.text = "菜单";
                    break;
                case (int)CURRENT_PANEL.CURRENT_PANEL_INDEX.HELP_PANEL:
                    //this.m_cHelpPanel.SetActive(false);
                    GUIBattleMenuHelp help = (GUIBattleMenuHelp)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BATTLE_MENU_HELP);
                    help.Hiden();
                    this.m_cMenuPanel.SetActive(true);
                    this.m_cGatePanel.SetActive(true);
                    this.m_iCurPanel = (int)CURRENT_PANEL.CURRENT_PANEL_INDEX.MENU_PANEL;
                    this.m_cBtnSetting.SetActive(true);
                    this.m_cBtnHelp.SetActive(true);
                    this.m_cLabHomeName.text = "菜单";
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// 展示战斗设定界面
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void ShowBattleSettingGui(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.m_cLabHomeName.text = "设定";

            //this.m_cBattleSettingPanel.SetActive(true);
            this.m_cMenuPanel.SetActive(false);
            this.m_cGatePanel.SetActive(false);
            this.m_cBtnSetting.SetActive(false);
            this.m_cBtnHelp.SetActive(false);
            this.m_iCurPanel = (int)CURRENT_PANEL.CURRENT_PANEL_INDEX.SETTING_PANEL;

            GUIBattleMenuSetting setting = (GUIBattleMenuSetting)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BATTLE_MENU_SETTING);
            setting.SetBattleGUI(this.m_cGUIBattle);
            setting.Show();
        }
    }

    /// <summary>
    /// 展示战斗帮助界面
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void ShowBattleHelpGui(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.m_cLabHomeName.text = "帮助";
            //this.m_cHelpPanel.SetActive(true);
            GUIBattleMenuHelp help = (GUIBattleMenuHelp)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BATTLE_MENU_HELP);
            help.Show();
            this.m_cMenuPanel.SetActive(false);
            this.m_cGatePanel.SetActive(false);
            this.m_cBtnSetting.SetActive(false);
            this.m_cBtnHelp.SetActive(false);
            this.m_iCurPanel = (int)CURRENT_PANEL.CURRENT_PANEL_INDEX.HELP_PANEL;
        }
    }

    /// <summary>
    /// 隐藏战斗帮助界面
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void HidenBattleHelpGui()
    {
        this.m_cLabHomeName.text = "菜单";
        //this.m_cHelpPanel.SetActive(false);
        this.m_cMenuPanel.SetActive(true);
        this.m_cGatePanel.SetActive(true);
        this.m_cBtnSetting.SetActive(true);
        this.m_cBtnHelp.SetActive(true);
        this.m_iCurPanel = (int)CURRENT_PANEL.CURRENT_PANEL_INDEX.MENU_PANEL;
        
    }

    /// <summary>
    /// 放弃战斗确定按钮事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void OnClickGiveUpOkButton()
    {
        if (m_cGiveUpAction != null)
        {
            m_cGiveUpAction();
            Hiden();
        }
    }

    /// <summary>
    /// 设置放弃战斗回调函数
    /// </summary>
    /// <param name="giveupCallback"></param>
    public void SetGiveUpCallBack(Action giveupCallback)
    {
        m_cGiveUpAction = giveupCallback;
    }

    /// <summary>
    /// 放弃战斗取消按钮事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void OnClickGiveUpCancelButton()
    {
        this.m_cMenuPanel.SetActive(true);
        //this.m_cGiveUpPanel.SetActive(false);
        this.m_cBtnSetting.SetActive(true);
        this.m_cBtnHelp.SetActive(true);
        this.m_iCurPanel = (int)CURRENT_PANEL.CURRENT_PANEL_INDEX.MENU_PANEL;
    }

    /// <summary>
    /// 设置副本名和关卡名
    /// </summary>
    /// <param name="dungeonName"></param>
    /// <param name="gateName"></param>
    public void SetDungeonNameAndGateName(string dungeonName, string gateName)
    {
        this.m_strDungeonName = dungeonName;
        this.m_strGateName = gateName;
    }

    /// <summary>
    /// 设置战斗中获得的英雄列表
    /// </summary>
    /// <param name="lst"></param>
    public void SetListSoul(List<int> lst)
    {
        if (this.m_lstSoul != null)
        {
            if (lst.Count != this.m_lstSoul.Count)
            {
                this.m_lstSoul.Clear();
                for (int i = 0; i < lst.Count; i++)
                {
                    this.m_lstSoul.Add(lst[i]);
                }
                this.m_bIsFirstGetHero = true;
            }
        }
        else
        {
            this.m_lstSoul = lst;
        }
    }

    /// <summary>
    /// 设置当前层级以及最大层级
    /// </summary>
    /// <param name="curLayer"></param>
    /// <param name="maxLayer"></param>
    public void SetCurrentAndMaxLayer(int curLayer, int maxLayer)
    {
        this.m_iCurrentLayer = curLayer + 1;
        this.m_iMaxLayer = maxLayer;
    }

    /// <summary>
    /// 设置当前队伍英雄列表
    /// </summary>
    /// <param name="lstTeamHeroTableId"></param>
    /// <param name="lstTeamHeroLv"></param>
    public void SetListTeamHero(int[] lstTeamHeroTableId, int[] lstTeamHeroLv)
    {
        this.m_lstTeamHeroLv.Clear();
        this.m_lstTeamHeroTabelId.Clear();
        for (int i = 0; i < lstTeamHeroTableId.Length; i++)
        {
            this.m_lstTeamHeroTabelId.Add(lstTeamHeroTableId[i]);
        }

        for (int i = 0; i < lstTeamHeroLv.Length; i++)
        {
            this.m_lstTeamHeroLv.Add(lstTeamHeroLv[i]);
        }
    }

    /// <summary>
    /// 设置获取的物品列表
    /// </summary>
    /// <param name="lstItemId"></param>
    public void SetListItem(List<int> listItemId, List<int> lstItemCount)
    {
        if (this.m_lstItemId != null)
        {
            if (listItemId.Count != this.m_lstItemId.Count)
            {
                this.m_lstItemId.Clear();
                for (int i = 0; i < listItemId.Count; i++)
                {
                    this.m_lstItemId.Add(listItemId[i]);
                }
                this.m_lstItemCount.Clear();
                for (int j = 0; j < lstItemCount.Count; j++)
                {
                    this.m_lstItemCount.Add(lstItemCount[j]);
                }
                this.m_bIsFirstGetItem = true;
            }
        }
        else
        {
            this.m_lstItemId = listItemId;
        }
    }
}

/// <summary>
/// 当前页面
/// </summary>
public struct CURRENT_PANEL {
    public enum CURRENT_PANEL_INDEX
    { 
        MENU_PANEL = 0,
        GETHERO_PANEL = 1,
        GETITEM_PANEL = 2,
        HEROINTELLIGENCE_PANEL = 3,
        GIVEUP_PANEL = 4,
        SETTING_PANEL = 5,
        HELP_PANEL = 6,
    }
}