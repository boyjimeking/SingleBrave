using System;
using System.Collections.Generic;
using Game.Resource;
using UnityEngine;
using System.Linq;
using Game.Media;

//  GUIHeroMenu.cs
//  Author: Lu Zexi
//  2013-11-25




/// <summary>
/// 英雄菜单
/// </summary>
public class GUIHeroMenu : GUIBase
{
    private const string RES_MAIN = "GUI_HeroMenu"; //主资源

    private const string TOP_PANEL = "TopPanel";    //定部面板
    private const string MAIN_PANEL = "MainPanel";  //主面板
    private const string CLOSE_BTN = "TopPanel/Image Button";    //关闭按钮
    private const string HERO_BTN = "MainPanel/Btn_DanweiYiLan"; //英雄浏览按钮
    private const string TEAM_BTN = "MainPanel/Btn_DuiWuBianCheng"; //团队编辑按钮
    private const string UPLEVEL_BTN = "MainPanel/Btn_QiangHuaHeCheng";  //英雄升级按钮
    private const string EVOLVE_BTN = "MainPanel/Btn_JinHuaHeCheng";   //英雄进化按钮
    private const string HERO_EQUIP_BTN = "MainPanel/Btn_DanweiZhuangBei";    //装备按钮
    private const string HERO_SELL_BTN = "MainPanel/Btn_DanweiChuShou";    //英雄出售按钮
    private const string SP_EQUIP = "MainPanel/SP_Equip";   //有最新装备提示
    private const string SP_JINHUA_WARN = "MainPanel/SP_JinHua"; //有英雄可以进化

    private GameObject m_cTopPanel; //顶部面板
    private GameObject m_cMainPanel;    //主面板
    private UISprite m_cSpEquipWarn;
    private UISprite m_cSpJinhuaWarn;

    public GUIHeroMenu(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_HERO_MENU, GUILAYER.GUI_PANEL)
    { 
    }

    /// <summary>
    /// 初始化
    /// </summary>
    protected override void InitGUI()
    {
        

    }

    /// <summary>
    /// 提示是否有新可用
    /// </summary>
    private void CheckIfNeedShowWarn()
    {
        //新装备可装备英雄
        if (GAME_SETTING.s_iWarnHeroEquip == 1)
        {
           this.m_cSpEquipWarn.enabled = true;
        }
        
        //英雄进化
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
                            this.m_cSpJinhuaWarn.enabled = true;
                    }
                    else
                    {
                        GAME_SETTING.s_dicWarnHeroJinhua.Add(hero.m_iTableID, 1);
                        GAME_SETTING.SaveWarnHeroJinhua();
                        this.m_cSpJinhuaWarn.enabled = true;
                    }
                }
            }
        }
        
    }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        base.Show();

        GUI_FUNCTION.AYSNCLOADING_HIDEN();

        if (this.m_cGUIObject == null)
        {
            this.m_cGUIObject = GameObject.Instantiate(ResourceMgr.LoadAsset(GAME_DEFINE.RESOURCE_GUI_CACHE, RES_MAIN) as UnityEngine.Object) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            GameObject closebtn = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, CLOSE_BTN);
            GUIComponentEvent ce = closebtn.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnCloseBtn);
            GameObject herobtn = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, HERO_BTN);
            ce = herobtn.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnHeroBtn);
            GameObject teambtn = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, TEAM_BTN);
            ce = teambtn.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnTeamBtn);
            GameObject uplevelbtn = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, UPLEVEL_BTN);
            ce = uplevelbtn.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnUplevelBtn);
            GameObject evolvebtn = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, EVOLVE_BTN);
            ce = evolvebtn.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnEvolveBtn);
            GameObject heroEquipbtn = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, HERO_EQUIP_BTN);
            ce = heroEquipbtn.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnHeroEquipBtn);
            GameObject heroSellbtn = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, HERO_SELL_BTN);
            ce = heroSellbtn.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnHeroSellBtn);

            m_cSpEquipWarn = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SP_EQUIP);
            m_cSpJinhuaWarn = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SP_JINHUA_WARN);

            this.m_cTopPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, TOP_PANEL);
            this.m_cMainPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, MAIN_PANEL);
        }

        //播放主背景音乐
		MediaMgr.sInstance.PlayBGM(SOUND_DEFINE.BGM_MAIN);
//        MediaMgr.PlayBGM(SOUND_DEFINE.BGM_MAIN);

        this.m_cGUIMgr.SetCurGUIID(this.m_iID);
        SetLocalPos(Vector3.zero);

        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-640, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_MAIN_MENU));

        m_cSpJinhuaWarn.enabled = false;
        m_cSpEquipWarn.enabled = false;

        CheckIfNeedShowWarn();  //是否需要提示用户有新可用
        if (m_cSpEquipWarn.enabled || m_cSpJinhuaWarn.enabled)
        {
            if (gui.m_cHeroWarn)
            {
                gui.m_cHeroWarn.enabled = true;
            }

        }
        else
            if (gui.m_cHeroWarn)
            {
                gui.m_cHeroWarn.enabled = false;
            }

        //Role.role.GetBaseProperty().m_iModelID = GUIDE_FUNCTION.MODEL_HERO_UP1;

        //新手引导
        GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_HERO_UP1);
        GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_HERO_EDITOR1);
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {

        ResourceMgr.UnloadUnusedResources();

        //base.Hiden();
        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();
        //CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-640, 270, 0));
        //CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0),Destory);
        Destory();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        m_cTopPanel = null;
        m_cMainPanel = null;
        m_cSpEquipWarn = null;
        m_cSpJinhuaWarn = null;

        base.Hiden();
        base.Destory();
    }

    /// <summary>
    /// 关闭按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnCloseBtn(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            ShowLockPanel();

            Hiden();
            this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_MAIN).Show();
        }
    }

    /// <summary>
    /// 英雄浏览按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnHeroBtn(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            ShowLockPanel();

            Hiden();
            GUIHeroShow gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERO_SHOW) as GUIHeroShow;
            gui.Show();
        }
    }

    /// <summary>
    /// 团队按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnTeamBtn(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            ShowLockPanel();

            Hiden();
            //GUI_FUNCTION.HIDEN_SHOW();

            GUITeamEditor tmpediter = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_TEAM_EDITOR) as GUITeamEditor;
            tmpediter.Show(this.Show);
        }
    }

    /// <summary>
    /// 英雄升级按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnUplevelBtn(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            ShowLockPanel();

            Hiden();
            this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_UPGRADE).Show();
        }
    }

    /// <summary>
    /// 英雄进化按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnEvolveBtn(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            ShowLockPanel();

            Hiden();
            this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_EVOLUTION).Show();
        }
    }

    /// <summary>
    /// 英雄装备按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnHeroEquipBtn(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            //已经查看过提示信息
            GAME_SETTING.s_iWarnHeroEquip= 0;
            GAME_SETTING.SaveWarnHeroEquip();

            ShowLockPanel();

            Hiden();
            //装备和英雄反过来选择
            this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HEROEQUIPMENT).Show();
            //this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HEROEQUIPSELECT).Show();
        }
    }

    /// <summary>
    /// 英雄出售按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnHeroSellBtn(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            ShowLockPanel();

            Hiden();
            this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HEROSELL).Show();
        }
    }

    /// <summary>
    /// 显示lock面板
    /// </summary>
    private void ShowLockPanel()
    {

        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();
    }

}

