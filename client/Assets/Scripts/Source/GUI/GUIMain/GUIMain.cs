//Micro.Sanvey
//2013.11.12
//sanvey.china@gmail.com

using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Resource;

/// <summary>
/// 主界面GUI类
/// </summary>
public class GUIMain : GUIBase
{
    //----------------------------------------------资源地址--------------------------------------------------

    private const string RES_MAIN = "GUI_Main";         //主界面地址
    private const string BTN_MENU = "BtnMenu";  //菜单按钮地址
    private const string BTN_BOX = "BtnBox";    //箱子按钮地址
    private const string BTN_WINARENA = "PanSlide/WinArena";     //副本按钮地址
    private const string BTN_WINGATE = "PanSlide/WinGate";     //Gate按钮地址
    private const string BTN_WINQUEST = "PanSlide/WinQuest";     //Quest按钮地址
    //private const string BTN_WINRAID = "PanSlide/WinRaid";     //Raid按钮地址
    private const string SPR_MARKLIGHT = "PanNavigation/MarkLight";   //导航亮点
    private const string PAN_SLIDE = "PanSlide";   //滑动Panel
    private const string STORY1 = "CharacterFrame/Story1";  //人物头像1
    private const string STORY2 = "CharacterFrame/Story2";  //人物头像2
    private const string STORY3 = "CharacterFrame/Story3";  //人物头像3
    private const string STORY4 = "CharacterFrame/Story4";  //人物头像4
    private const string STORY5 = "CharacterFrame/Story5";  //人物头像5
    private const string LB_LEADER = "CharacterFrame/Leader"; //队长图标
    private const string STORY1_NATURE = "CharacterFrame/Spr_Property1";  //人物属性1
    private const string STORY2_NATURE = "CharacterFrame/Spr_Property2";  //人物属性2
    private const string STORY3_NATURE = "CharacterFrame/Spr_Property3";  //人物属性3
    private const string STORY4_NATURE = "CharacterFrame/Spr_Property4";  //人物属性4
    private const string STORY5_NATURE = "CharacterFrame/Spr_Property5";  //人物属性5
    private const string STORY1_BG = "CharacterFrame/StoryBg1";   //人物长头像背景
    private const string STORY2_BG = "CharacterFrame/StoryBg2";   //人物长头像背景
    private const string STORY3_BG = "CharacterFrame/StoryBg3";   //人物长头像背景
    private const string STORY4_BG = "CharacterFrame/StoryBg4";   //人物长头像背景
    private const string STORY5_BG = "CharacterFrame/StoryBg5";   //人物长头像背景
    private const string STORY1_BACK = "CharacterFrame/Spr_back_1";  //英雄空背景
    private const string STORY2_BACK = "CharacterFrame/Spr_back_2"; //英雄空背景
    private const string STORY3_BACK = "CharacterFrame/Spr_back_3"; //英雄空背景
    private const string STORY4_BACK = "CharacterFrame/Spr_back_4"; //英雄空背景
    private const string STORY5_BACK = "CharacterFrame/Spr_back_5"; //英雄空背景

    private const string LABEL_MAILCOUNT = "Lab_MailCount";//礼物数量标签地址
    private const string SPR_MAILCOUNT = "Spr_MailCount";//礼物数量图片地址

    //3D特效  主将特效
    //private const string EFFECT_LEADER = "effect_GUI_zhujiang";  //主将特效
    //private const string GUI_EFFECT = "GUI_EFFECT";//3d特效资源地址
    //private const string EFFECT_CENTER_ANCHOR = "ANCHOR_CENTER";//3d特效父对象

    //----------------------------------------------游戏对象--------------------------------------------------

    private GameObject m_cBtnMenu;   //菜单按钮
    private GameObject m_cBtnBox;    //箱子按钮
    private GameObject m_cBtnWinArena;  //副本按钮
    private GameObject m_cBtnWinGate;  //gate按钮
    private GameObject m_cBtnWinQuest;  //quest按钮
    //private GameObject m_cBtnWinRaid;  //raid按钮
    private GameObject m_cMarkLight;  //导航亮点
    private GameObject m_cPanSlide;   //panel滑动
    private UITexture m_cTxtureStory1;  //人物头像1
    private UITexture m_cTxtureStory2;  //人物头像2
    private UITexture m_cTxtureStory3;  //人物头像3
    private UITexture m_cTxtureStory4;  //人物头像4
    private UITexture m_cTxtureStory5;  //人物头像5
    private UISprite m_cLbLeader;  //队长图标
    private UISprite m_cSpStory1Nature;
    private UISprite m_cSpStory2Nature;
    private UISprite m_cSpStory3Nature;
    private UISprite m_cSpStory4Nature;
    private UISprite m_cSpStory5Nature;
    private UISprite m_cSpStory1Bg;
    private UISprite m_cSpStory2Bg;
    private UISprite m_cSpStory3Bg;
    private UISprite m_cSpStory4Bg;
    private UISprite m_cSpStory5Bg;
    private UISprite m_cSpStory1Back;
    private UISprite m_cSpStory2Back;
    private UISprite m_cSpStory3Back;
    private UISprite m_cSpStory4Back;
    private UISprite m_cSpStory5Back;

    private UISprite m_cSprMailCount;//礼物数量背景
    private UILabel m_cLabMailCount;//礼物数量标签

    //3d 主将特效
    //private GameObject m_cGuiEffect;    //3d特效资源
    //private GameObject m_cEffectParent; //3d特效父对象
    //private GameObject m_cEffectLeader;  //主将特效

    //----------------------------------------------Data--------------------------------------------------

    private List<GameObject> m_lstSlideItemList = new List<GameObject>();  //滑动对象列表
    private bool m_bIsDraging = false;  //是否滑动中
    private bool m_bIsRight = false;  //向右拖动
    private float m_fDragDistance = 0f;  //累计滑动距离
    private float m_fdistance = 0f;  //剩余滑动距离
    private bool m_bImmediaty = false;
    private int m_iLastGuiId;//上一个gui

    private List<ResourceRequireOwner> m_lstOwners = new List<ResourceRequireOwner>();
    private List<string> m_lstResName = new List<string>();

    public GUIMain(GUIManager mgr)
        : base(mgr, GUI_DEFINE.GUIID_MAIN, GUILAYER.GUI_MENU)
    {
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        //删除旧加载
        for (int i = 0; i < m_lstResName.Count; i++)
        {
            ResourcesManager.GetInstance().UnloadResource(m_lstResName[i], m_lstOwners[i]);
        }
        this.m_lstResName.Clear();
        this.m_lstOwners.Clear();

        base.Hiden();
        if (null != m_lstSlideItemList) m_lstSlideItemList.Clear();
        
        this.m_cBtnMenu = null;
        this.m_cBtnBox = null;
        this.m_cBtnWinArena = null;
        this.m_cBtnWinGate = null;
        this.m_cBtnWinQuest = null;
        this.m_cMarkLight = null;
        this.m_cPanSlide = null;
        this.m_cTxtureStory1 = null;
        this.m_cTxtureStory2 = null;
        this.m_cTxtureStory3 = null;
        this.m_cTxtureStory4 = null;
        this.m_cTxtureStory5 = null;
        this.m_cLbLeader = null;
        this.m_cSpStory1Nature = null;
        this.m_cSpStory2Nature = null;
        this.m_cSpStory3Nature = null;
        this.m_cSpStory4Nature = null;
        this.m_cSpStory5Nature = null;
        this.m_cSpStory1Bg = null;
        this.m_cSpStory2Bg = null;
        this.m_cSpStory3Bg = null;
        this.m_cSpStory4Bg = null;
        this.m_cSpStory5Bg = null;
        this.m_cSpStory1Back = null;
        this.m_cSpStory2Back = null;
        this.m_cSpStory3Back = null;
        this.m_cSpStory4Back = null;
        this.m_cSpStory5Back = null;
        this.m_cSprMailCount = null;
        this.m_cLabMailCount = null;

        base.Destory();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    protected override void InitGUI()
    {
        base.Show();

        //GUI_FUNCTION.AYSNCLOADING_HIDEN();

        if (this.m_cGUIObject == null)
        {
            //实例化GameObject
            //Main主资源
            this.m_cGUIObject = GameObject.Instantiate(ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_GUI_CACHE, RES_MAIN) as UnityEngine.Object) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;
            //箱子按钮
            this.m_cBtnBox = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_BOX);
            GUIComponentEvent gui_event = this.m_cBtnBox.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnBox_OnEvent);
            //菜单按钮
            this.m_cBtnMenu = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_MENU);
            gui_event = this.m_cBtnMenu.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnMenu_OnEvent);
            //副本按钮
            this.m_cBtnWinArena = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_WINARENA);
            gui_event = this.m_cBtnWinArena.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnWinArena_OnEvent);
            //gate按钮
            this.m_cBtnWinGate = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_WINGATE);
            gui_event = this.m_cBtnWinGate.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnWinGate_OnEvent);
            //quest按钮
            this.m_cBtnWinQuest = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_WINQUEST);
            gui_event = this.m_cBtnWinQuest.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnWinQuest_OnEvent);
            //Raid按钮
           //this.m_cBtnWinRaid = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_WINRAID);
           //gui_event = this.m_cBtnWinRaid.AddComponent<GUIComponentEvent>();
           //gui_event.AddIntputDelegate(BtnWinRaid_OnEvent);
            //导航亮点
            this.m_cMarkLight = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, SPR_MARKLIGHT);
            this.m_cPanSlide = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_SLIDE);
            //长头像
            this.m_cTxtureStory1 = GUI_FINDATION.GET_OBJ_COMPONENT<UITexture>(this.m_cGUIObject, STORY1);
            gui_event = this.m_cTxtureStory1.gameObject.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(Story1_OnEvent, 0);
            this.m_cTxtureStory2 = GUI_FINDATION.GET_OBJ_COMPONENT<UITexture>(this.m_cGUIObject, STORY2);
            gui_event = this.m_cTxtureStory2.gameObject.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(Story2_OnEvent, 1);
            this.m_cTxtureStory3 = GUI_FINDATION.GET_OBJ_COMPONENT<UITexture>(this.m_cGUIObject, STORY3);
            gui_event = this.m_cTxtureStory3.gameObject.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(Story3_OnEvent, 2);
            this.m_cTxtureStory4 = GUI_FINDATION.GET_OBJ_COMPONENT<UITexture>(this.m_cGUIObject, STORY4);
            gui_event = this.m_cTxtureStory4.gameObject.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(Story4_OnEvent, 3);
            this.m_cTxtureStory5 = GUI_FINDATION.GET_OBJ_COMPONENT<UITexture>(this.m_cGUIObject, STORY5);
            gui_event = this.m_cTxtureStory5.gameObject.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(Story5_OnEvent, 4);

            this.m_cSpStory1Bg = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, STORY1_BG);
            this.m_cSpStory2Bg = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, STORY2_BG);
            this.m_cSpStory3Bg = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, STORY3_BG);
            this.m_cSpStory4Bg = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, STORY4_BG);
            this.m_cSpStory5Bg = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, STORY5_BG);

            this.m_cSpStory1Nature = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, STORY1_NATURE);
            this.m_cSpStory2Nature = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, STORY2_NATURE);
            this.m_cSpStory3Nature = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, STORY3_NATURE);
            this.m_cSpStory4Nature = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, STORY4_NATURE);
            this.m_cSpStory5Nature = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, STORY5_NATURE);

            this.m_cSpStory1Back = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, STORY1_BACK);
            this.m_cSpStory2Back = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, STORY2_BACK);
            this.m_cSpStory3Back = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, STORY3_BACK);
            this.m_cSpStory4Back = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, STORY4_BACK);
            this.m_cSpStory5Back = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, STORY5_BACK);

            this.m_cLbLeader = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, LB_LEADER);

            //3D特效
            //this.m_cGuiEffect = GUI_FINDATION.FIND_GAME_OBJECT(GUI_EFFECT);
            //this.m_cEffectParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGuiEffect, EFFECT_CENTER_ANCHOR);

            this.m_cSprMailCount = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SPR_MAILCOUNT);//礼物数量背景
            this.m_cLabMailCount = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LABEL_MAILCOUNT);//礼物数量标签

            //下方提示
            GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
            gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_MAIN_MENU));
        }

        //播放主背景音乐
        SoundManager.GetInstance().PlayBGM(SOUND_DEFINE.BGM_MAIN);

        UpdateData();

        //初始化数据
        m_bIsDraging = false;  //是否滑动中
        m_bIsRight = false;  //向右拖动
        m_fDragDistance = 0f;  //累计滑动距离
        m_fdistance = 0f;  //剩余滑动距离

        SetMailCountLabel(Role.role.GetBaseProperty().m_iMailCounts);

        this.m_cGUIMgr.SetCurGUIID(this.m_iID);

        SetLocalPos(Vector3.zero);

        m_cGUIObject.GetComponent<UIPanel>().alpha = 0;
        //m_cPanSlide.GetComponent<UIPanel>().alpha = 0;
        //m_cPanSlide.GetComponent<UIPanel>().depth += 10;
        CTween.TweenAlpha(this.m_cGUIObject, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, 0f, 1f);
        CTween.TweenAlpha(this.m_cPanSlide, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, 0f, 1f);


        //Role.role.GetBaseProperty().m_iModelID = GUIDE_FUNCTION.MODEL_SUMMON1;

        //Role.role.GetBaseProperty().m_iModelID = GUIDE_FUNCTION.MODEL_BATTLE_SECOND1;

        //新手引导
        GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_BATTLE_SECOND1);
        GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_TOWN2);
        GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_BATTLE_THIRD1);
        GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_SUMMON1);
    }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        //Debug.Log("main show ");
        m_bImmediaty = false;

        //this.m_eLoadingState = LOADING_STATE.START;
        //GUI_FUNCTION.AYSNCLOADING_SHOW();


        //人物长头像
        //int[] arrHeros = Role.role.GetTeamProperty().GetTeam(Role.role.GetBaseProperty().m_iCurrentTeam).m_vecTeam;
        //foreach (int heroid in arrHeros)
        //{
        //    if (heroid != null && heroid != -1)
        //    {
        //        Hero tmphero = Role.role.GetHeroProperty().GetHero(heroid);
        //        if (tmphero != null)
        //        {
        //            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_TEX_PATH, GAME_DEFINE.RES_VERSION, tmphero.m_strAvatarL);
        //        }
        //    }
        //}


        //if (this.m_cGUIObject == null)
        //{
        //    //Debug.Log("main show 1");
        //    ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_MAIN);
        //}

        InitGUI();


    }

    /// <summary>
    /// 立即Show
    /// </summary>
    public override void ShowImmediately()
    {
        m_bImmediaty = true;

        //this.m_eLoadingState = LOADING_STATE.START;
        //GUI_FUNCTION.AYSNCLOADING_SHOW();


        //人物长头像
        //int[] arrHeros = Role.role.GetTeamProperty().GetTeam(Role.role.GetBaseProperty().m_iCurrentTeam).m_vecTeam;
        //foreach (int heroid in arrHeros)
        //{
        //    if (heroid != null && heroid != -1)
        //    {
        //        Hero tmphero = Role.role.GetHeroProperty().GetHero(heroid);
        //        ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_TEX_PATH, GAME_DEFINE.RES_VERSION, tmphero.m_strAvatarL);
        //    }
        //}
        //if (this.m_cGUIObject == null)
        //{
        //    ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_MAIN);
        //}

        InitGUIImmediately();

    }

    public void InitGUIImmediately()
    {
        GUI_FUNCTION.AYSNCLOADING_HIDEN();

        base.ShowImmediately();

        if (this.m_cGUIObject == null)
        {
            //实例化GameObject
            //Main主资源
            this.m_cGUIObject = GameObject.Instantiate(ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_GUI_CACHE, RES_MAIN) as UnityEngine.Object) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;
            //箱子按钮
            this.m_cBtnBox = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_BOX);
            GUIComponentEvent gui_event = this.m_cBtnBox.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnBox_OnEvent);
            //菜单按钮
            this.m_cBtnMenu = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_MENU);
            gui_event = this.m_cBtnMenu.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnMenu_OnEvent);
            //副本按钮
            this.m_cBtnWinArena = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_WINARENA);
            gui_event = this.m_cBtnWinArena.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnWinArena_OnEvent);
            //gate按钮
            this.m_cBtnWinGate = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_WINGATE);
            gui_event = this.m_cBtnWinGate.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnWinGate_OnEvent);
            //quest按钮
            this.m_cBtnWinQuest = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_WINQUEST);
            gui_event = this.m_cBtnWinQuest.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnWinQuest_OnEvent);
            //Raid按钮
            //this.m_cBtnWinRaid = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_WINRAID);
            //gui_event = this.m_cBtnWinRaid.AddComponent<GUIComponentEvent>();
            //gui_event.AddIntputDelegate(BtnWinRaid_OnEvent);
            //导航亮点
            this.m_cMarkLight = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, SPR_MARKLIGHT);
            this.m_cPanSlide = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_SLIDE);

            //长头像
            this.m_cTxtureStory1 = GUI_FINDATION.GET_OBJ_COMPONENT<UITexture>(this.m_cGUIObject, STORY1);
            gui_event = this.m_cTxtureStory1.gameObject.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(Story1_OnEvent, 0);
            this.m_cTxtureStory2 = GUI_FINDATION.GET_OBJ_COMPONENT<UITexture>(this.m_cGUIObject, STORY2);
            gui_event = this.m_cTxtureStory2.gameObject.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(Story2_OnEvent, 1);
            this.m_cTxtureStory3 = GUI_FINDATION.GET_OBJ_COMPONENT<UITexture>(this.m_cGUIObject, STORY3);
            gui_event = this.m_cTxtureStory3.gameObject.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(Story3_OnEvent, 2);
            this.m_cTxtureStory4 = GUI_FINDATION.GET_OBJ_COMPONENT<UITexture>(this.m_cGUIObject, STORY4);
            gui_event = this.m_cTxtureStory4.gameObject.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(Story4_OnEvent, 3);
            this.m_cTxtureStory5 = GUI_FINDATION.GET_OBJ_COMPONENT<UITexture>(this.m_cGUIObject, STORY5);
            gui_event = this.m_cTxtureStory5.gameObject.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(Story5_OnEvent, 4);

            this.m_cSpStory1Bg = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, STORY1_BG);
            this.m_cSpStory2Bg = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, STORY2_BG);
            this.m_cSpStory3Bg = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, STORY3_BG);
            this.m_cSpStory4Bg = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, STORY4_BG);
            this.m_cSpStory5Bg = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, STORY5_BG);

            this.m_cSpStory1Nature = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, STORY1_NATURE);
            this.m_cSpStory2Nature = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, STORY2_NATURE);
            this.m_cSpStory3Nature = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, STORY3_NATURE);
            this.m_cSpStory4Nature = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, STORY4_NATURE);
            this.m_cSpStory5Nature = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, STORY5_NATURE);


            this.m_cLbLeader = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, LB_LEADER);

            //3D特效
            //this.m_cGuiEffect = GUI_FINDATION.FIND_GAME_OBJECT(GUI_EFFECT);
            //this.m_cEffectParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGuiEffect, EFFECT_CENTER_ANCHOR);
        }

        UpdateData();

        m_bIsDraging = false;  //是否滑动中
        m_bIsRight = false;  //向右拖动
        m_fDragDistance = 0f;  //累计滑动距离
        m_fdistance = 0f;  //剩余滑动距离

        this.m_cGUIMgr.SetCurGUIID(this.m_iID);

        SetLocalPos(Vector3.zero);

        //this.m_cPanSlide.GetComponent<UIPanel>().depth += 10;
    }

    /// <summary>
    /// 立即Hidden
    /// </summary>
    public override void HidenImmediately()
    {
        //CameraManager.GetInstance().HidenGUIEffectCamera();
        //if (null != this.m_cEffectLeader) GameObject.DestroyImmediate(this.m_cEffectLeader);

        ResourcesManager.GetInstance().UnloadUnusedResources();

        base.HidenImmediately();
        //SetLocalPos(Vector3.one * 0xFFFF);
        Destory();
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //CameraManager.GetInstance().HidenGUIEffectCamera();
        //if (null != this.m_cEffectLeader) GameObject.DestroyImmediate(this.m_cEffectLeader);
        //base.Hiden();

        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        ResourcesManager.GetInstance().UnloadUnusedResources();

        //CTween.TweenAlpha(this.m_cGUIObject, 0, GAME_DEFINE.FADEOUT_GUI_TIME, 1f, 0f);
        //CTween.TweenAlpha(this.m_cPanSlide, 0, GAME_DEFINE.FADEOUT_GUI_TIME, 1f, 0f, Destory);
        //SetLocalPos(Vector3.one * 0xFFFF);
        Destory();
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    /// <returns></returns>
    public override bool Update()
    {
        ////资源加载等待
        //switch (this.m_eLoadingState)
        //{
        //    case LOADING_STATE.START:
        //        this.m_eLoadingState++;
        //        return false;
        //    case LOADING_STATE.LOADING:
        //        if (ResourcesManager.GetInstance().GetProgress() >= 1f && ResourcesManager.GetInstance().IsComplete())
        //        {
        //            this.m_eLoadingState++;
        //        }
        //        return false;
        //    case LOADING_STATE.END:
        //        if (m_bImmediaty)
        //        {
        //            InitGUIImmediately();
        //        }
        //        else
        //        {
        //            InitGUI();
        //        }

        //        this.m_eLoadingState++;
        //        break;
        //}

        if (IsShow())
        {

            if (!m_bIsDraging)
            {
                ChangePosion(m_bIsRight);
            }

            float maxSize = 0;
            int maxSizeIndex = 0;
            int index = 0;
            //更新位置大小
            m_lstSlideItemList.ForEach(item =>
            {

                //到达边界时，显示一半大小 (1/2)*(1/320)=0.0015625
                var scaleSize = (1 - 0.0015625F * (Mathf.Abs(item.transform.localPosition.x)));
                item.transform.localScale = Vector3.one * scaleSize;
                //item.transform.localPosition = new Vector3(item.transform.localPosition.x, (1 - scaleSize) * -50, item.transform.localPosition.z);
                if (maxSize < scaleSize)
                {
                    maxSizeIndex = index;
                    maxSize = scaleSize;
                }
                index++;
            });

            //更新导航亮点
            maxSizeIndex = (maxSizeIndex + 2) % 3;
            this.m_cMarkLight.transform.localPosition = new Vector3((maxSizeIndex - 1) * 50, -310, 0);
        }

        return base.Update();
    }

    /// <summary>
    /// 设置邮件数量标签
    /// </summary>
    /// <param name="count"></param>
    public void SetMailCountLabel(int count)
    {
        if (this.m_cGUIObject != null)
        {
            if (count == 0)
            {
                this.m_cSprMailCount.enabled = false;
                this.m_cLabMailCount.enabled = false;
            }
            else
            {
                this.m_cSprMailCount.enabled = true;
                this.m_cLabMailCount.text = count.ToString();
            }
        }

    }

    /// <summary>
    /// 箱子点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnBox_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            ////抽奖测试  3星 29  ； 4星 228  ； 5星
            //this.Hiden();
            //string testStr = "{ \"code\": \"0\", \"desc\": \"\", \"data\": {\"diamond\": 5860, \"hero\": { \"pid\": \"383\",\"hero_id\": \" 4\",            \"lv\": \"1\",            \"exp\": \"0\",            \"create_time\": \"1393575919\",            \"hp\": \"2656\",            \"attack\": \"850\",            \"defend\": \"607\",            \"recover\": \"837\",            \"bb_level\": \"1\",            \"grow_type\": \"4\",            \"equip_id\": \"-1\",            \"lock\": \"0\",            \"id\": \"7866\"        }    },    \"time\": 1393575919}";
            //CodeTitans.JSon.JSonReader reader = new CodeTitans.JSon.JSonReader();
            //CodeTitans.JSon.IJSonObject json = reader.ReadAsJSonObject(testStr);
            //MoneySummonHandle handle = new MoneySummonHandle();
            //MoneySummonPktAckFactory creater = new MoneySummonPktAckFactory();
            //handle.Excute(creater.Create(json));
            //return;

            ////英雄强化测试
            //this.Hiden();
            //string testStr = "{    \"code\": \"0\",    \"desc\": \"\",    \"data\": {        \"success_type\": 0,        \"gold\": 20100,        \"sacrifice_heros\": [],        \"after_strength_hero\": {            \"id\": \"10404\",            \"pid\": \"667\",            \"hero_id\": \"1\",            \"lv\": \"12\",            \"exp\": \"0\",            \"create_time\": \"1393919782\",            \"hp\": \"1947.999999\",            \"attack\": \"632.542966\",            \"defend\": \"640.446605\",            \"recover\": \"448.000000\",            \"bb_level\": 6,            \"grow_type\": \"4\",            \"equip_id\": \"-1\",            \"lock\": \"0\"        },        \"strength_process\": [            {                \"lv\": \"12\",                \"exp\": \"0\",                \"hp\": \"1947.999999\",                \"attack\": \"632.542966\",                \"defend\": \"640.446605\",                \"recover\": \"448.000000\",                \"bb_level\": \"5\"            },            {                \"lv\": 12,                \"exp\": 0,                \"hp\": \"1947.999999\",                \"attack\": \"632.542966\",                \"defend\": \"640.446605\",                \"recover\": \"448.000000\",                \"bb_level\": 6            }        ]    },    \"time\": 1393919989}";
            //CodeTitans.JSon.JSonReader reader = new CodeTitans.JSon.JSonReader();
            //CodeTitans.JSon.IJSonObject json = reader.ReadAsJSonObject(testStr);
            //HeroUpgradeHandle handle = new HeroUpgradeHandle();
            //HeroUpgradePktAckFactory creater = new HeroUpgradePktAckFactory();
            //handle.Excute(creater.Create(json));

            ////英雄进化
            //HidenImmediately();
            //GUIHeroEvolutionResult re = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_EVOLUTIONRESULT) as GUIHeroEvolutionResult;
            //re.SetHeroSelectId(50873, 7);
            //re.Show();
            //return;


            //测试战斗结束奖励入口 
            //this.HidenImmediately();
            //GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Hiden();
            //GUIBattleReward gui = (GUIBattleReward)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BATTLE_REWARD);
            //List<Hero> lstHero = new List<Hero>();
            //for (int i = 1; i < 8; i++)
            //{
            //    lstHero.Add(new Hero(i));
            //}
            //List<int> m_lstItem = new List<int>();
            //for (int i = 0; i < 7; i++)
            //{
            //    m_lstItem.Add((1001));
            //}
            //List<int> m_lstItemNum = new List<int>();
            //for (int i = 0; i < 7; i++)
            //{
            //    m_lstItemNum.Add(1);
            //}
            //gui.SetReward("副本", "具体内容", 111, 111, 111, 1, 1, 14, lstHero, m_lstItem, m_lstItemNum, new List<int>(), GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_ESPDUNGEON).Show);
            //gui.Show();

            SendAgent.SendPlayerGetSystemMail(Role.role.GetBaseProperty().m_iPlayerId);
        }
    }

    /// <summary>
    /// 菜单点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnMenu_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            HidenImmediately();
            //进入菜单GUI
            GUIMenu menu = (GUIMenu)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_MENU);
            menu.Show();
        }
    }

    /// <summary>
    /// 竞技场点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnWinArena_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (Role.role.GetTeamProperty().GetCurTeamCost() <= RoleExpTableManager.GetInstance().GetMaxCost(Role.role.GetBaseProperty().m_iLevel))
            {
                HidenImmediately();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Hiden();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Show();
                //进入竞技场
                GUIArena arena = (GUIArena)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_ARENA);
                arena.Show();
            }
            else
            {
                GUI_FUNCTION.MESSAGEM(CostOverMax, GAME_FUNCTION.STRING(STRING_DEFINE.INFO_COST_OVER_MAX));
            }

        }
        else
        {
            WinItem_OnEvent(info, args);
        }
    }

    /// <summary>
    /// Gate点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnWinGate_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (Role.role.GetTeamProperty().GetCurTeamCost() <= RoleExpTableManager.GetInstance().GetMaxCost(Role.role.GetBaseProperty().m_iLevel))
            {
                if (Role.role.GetHeroProperty().GetAllHero().Count >= Role.role.GetBaseProperty().m_iMaxHeroCount)
                {
                    GUI_FUNCTION.MESSAGEM_(MessageCallBack_HeroMax, GAME_FUNCTION.STRING(STRING_DEFINE.WARNING_MAX_HERO), "btn_expand", "btn_expand1", "btn_hero", "btn_hero1");
                    return;
                }
                if (Role.role.GetItemProperty().GetAllItemCount() >= Role.role.GetBaseProperty().m_iMaxItem)
                {
                    GUI_FUNCTION.MESSAGEM_(MessageCallBack_ItemMax, GAME_FUNCTION.STRING(STRING_DEFINE.WARNING_MAX_ITEM), "btn_expand", "btn_expand1", "btn_daoju1", "btn_daoju");
                    return;
                }

                Role.role.GetHeroProperty().SetAllHeroOld(); //所有英雄不再是new
                Role.role.GetItemProperty().SetAllItemOld();  //所有物品不再是new

                HidenImmediately();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).HidenImmediately();

                //进入特殊副本呢
                GUIEspDungeon espDungeon = (GUIEspDungeon)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_ESPDUNGEON);
                espDungeon.SetLastGuiId(GUI_DEFINE.GUIID_MAIN);
                espDungeon.Show();
            }
            else
            {
                GUI_FUNCTION.MESSAGEM(CostOverMax, GAME_FUNCTION.STRING(STRING_DEFINE.INFO_COST_OVER_MAX));
            }
        }
        else
        {
            WinItem_OnEvent(info, args);
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
    /// 物品满了 委托
    /// </summary>
    /// <param name="ok"></param>
    private void MessageCallBack_ItemMax(bool result1)
    {
        if (result1)  //扩大单位数量
        {
            this.m_cGUIMgr.HidenCurGUI();

            if (SessionManager.GetInstance().Refresh())
            {
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_PROPSSLOTEXPANSION).Show);
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show);
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show);
            }
            else
            {
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_PROPSSLOTEXPANSION).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
            }
        }
        else  //出售
        {

            this.m_cGUIMgr.HidenCurGUI();
            if (SessionManager.GetInstance().Refresh())
            {

                SessionManager.GetInstance().SetCallBack(() =>
                {
                    GUITown town = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_TOWN) as GUITown;
                    town.Show();
                    this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_PROPSSALES).Show();
                    this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
                });
            }
            else
            {
                GUITown town = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_TOWN) as GUITown;
                town.Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_PROPSSALES).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
            }
        }
    }

    /// <summary>
    /// Quest点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnWinQuest_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (Role.role.GetTeamProperty().GetCurTeamCost() <= RoleExpTableManager.GetInstance().GetMaxCost(Role.role.GetBaseProperty().m_iLevel))
            {
                if (Role.role.GetHeroProperty().GetAllHero().Count >= Role.role.GetBaseProperty().m_iMaxHeroCount)
                {
                    //GUI_FUNCTION.MESSAGEM_(MessageCallBack_HeroMax, GAME_FUNCTION.STRING(STRING_DEFINE.WARNING_MAX_HERO), "btn_hero", "btn_hero1");
                    GUI_FUNCTION.MESSAGEM_(MessageCallBack_HeroMax, GAME_FUNCTION.STRING(STRING_DEFINE.WARNING_MAX_HERO), "btn_expand", "btn_expand1", "btn_hero", "btn_hero1");
                    return;
                }
                if (Role.role.GetItemProperty().GetAllItemCount() >= Role.role.GetBaseProperty().m_iMaxItem)
                {
                    GUI_FUNCTION.MESSAGEM_(MessageCallBack_ItemMax, GAME_FUNCTION.STRING(STRING_DEFINE.WARNING_MAX_ITEM), "btn_expand", "btn_expand1", "btn_daoju1", "btn_daoju");
                    return;
                }

                Role.role.GetHeroProperty().SetAllHeroOld(); //所有英雄不再是new
                Role.role.GetItemProperty().SetAllItemOld();  //所有物品不再是new

                HidenImmediately();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).HidenImmediately();
                List<FuBen> lstFuben = new List<FuBen>();
                for (int i = 0; i < Role.role.GetFubenProperty().GetAllFuben().Count; i++)
                {
                    lstFuben.Add(Role.role.GetFubenProperty().GetAllFuben()[i]);
                }
                if(lstFuben.Count > 1)
                {
                    if(!lstFuben[1].m_bActive)
                    {
                        GUIArea area = (GUIArea)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_AREA);
                        WorldManager.s_iCurrentWorldId = 1;
                        int newAreaIndex = Role.role.GetFubenProperty().GetNewAreaIndex(WorldManager.s_iCurrentWorldId);
                        int newDungeonIndex = Role.role.GetFubenProperty().GetNewDungeonIndex(WorldManager.s_iCurrentWorldId, newAreaIndex);
                        if (newDungeonIndex >= 0)
                        {
                            WorldManager.s_iLastNewDungeonIndex = newDungeonIndex - 1;
                        }
                        if (newAreaIndex == WorldManager.s_iCurrentAreaIndex && WorldManager.s_iCurrentAreaIndex == 0)
                        {
                            area.ResetCurrentAreaId();
                        }
                        else if(newAreaIndex > 0 && this.m_iLastGuiId != GUI_DEFINE.GUIID_AREA){
                            area.ResetCurrentAreaId();
                        }
                        
                        area.Show();
                    }else{
                        //进入世界
                        GUIWorld world = (GUIWorld)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_WORLD);
                        world.Show();
                    }
                }

            }
            else
            {
                GUI_FUNCTION.MESSAGEM(CostOverMax, GAME_FUNCTION.STRING(STRING_DEFINE.INFO_COST_OVER_MAX));
            }
        }
        else
        {
            WinItem_OnEvent(info, args);
        }
    }

    /// <summary>
    /// Raid点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnWinRaid_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            //Hiden();
            ////进入世界
            //GUIWorld world = (GUIWorld)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUI_WORLD_ID);
            //world.Show();
        }
        else
        {
            WinItem_OnEvent(info, args);
        }
    }

    /// <summary>
    /// Cost超出上限跳转事件
    /// </summary>
    private void CostOverMax()
    {
        Hiden();

        GUITeamEditor tmpeditor = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_TEAM_EDITOR) as GUITeamEditor;
        tmpeditor.Show(TeamEditCallBack);
    }

    /// <summary>
    /// 编队回调
    /// </summary>
    private void TeamEditCallBack()
    {
        this.Show();
    }

    /// <summary>
    /// 设置上一个GUIID
    /// </summary>
    /// <param name="id"></param>
    public void SetLastGuiId(int id)
    {
        this.m_iLastGuiId = id;
    }

    /// <summary>
    /// Drag处理
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void WinItem_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.DRAG)
        {
            m_bIsDraging = true;
            m_bIsRight = (info.m_vecDelta.x > 0);
            m_fDragDistance += info.m_vecDelta.x;
            ChangePosion(m_bIsRight);

            m_lstSlideItemList.ForEach(item =>
            {
                //跟随位移
                item.transform.localPosition += new Vector3(info.m_vecDelta.x, 0, 0);
            });
        }
        else if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.PRESS)  // drag结束
        {
            if (m_bIsDraging)  // drag结束事件只执行一次
            {

                m_lstSlideItemList.ForEach(item =>
                {
                    var yu = item.transform.localPosition.x % 350;
                    m_fdistance = yu > 0 ? yu : 350 - Math.Abs(yu);

                    if (m_bIsRight)  //滑动超过30单位 就算有效滑动到下一项，否则回到原始位置
                    {
                        m_fdistance = m_fdistance > 1 ? item.transform.localPosition.x + 350 - m_fdistance : item.transform.localPosition.x - m_fdistance;
                    }
                    else
                    {
                        m_fdistance = 350 - m_fdistance > 1 ? item.transform.localPosition.x - m_fdistance : item.transform.localPosition.x + 350 - m_fdistance;
                    }

                    //tween 的 xyz位移动画导致 位移对上下的补偿失效 只能自己在update里面实现只单独移动x
                    CTween.TweenPosition(item, 0.3F, new Vector3(m_fdistance, 0F, 0F));  //结束剩余动画
                    //翻页特效
                    SoundManager.GetInstance().PlaySound2(SOUND_DEFINE.SE_SLIDE);

                });

                m_fDragDistance = 0f;
                m_bIsDraging = false;
            }
        }
    }

    /// <summary>
    /// 长头像点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void Story1_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hero selectHero = Role.role.GetHeroProperty().GetHero(Role.role.GetTeamProperty().GetTeam(Role.role.GetBaseProperty().m_iCurrentTeam).m_vecTeam[(int)args[0]]);
            if (null != selectHero)
            {
                Hiden();
                GUIHeroDetail herodetail = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERODETAIL) as GUIHeroDetail;
                herodetail.Show(Show, selectHero);
            }
        }
    }


    /// <summary>
    /// 长头像点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void Story2_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hero selectHero = Role.role.GetHeroProperty().GetHero(Role.role.GetTeamProperty().GetTeam(Role.role.GetBaseProperty().m_iCurrentTeam).m_vecTeam[(int)args[0]]);
            if (null != selectHero)
            {
                Hiden();
                GUIHeroDetail herodetail = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERODETAIL) as GUIHeroDetail;
                herodetail.Show(Show, selectHero);
            }
        }
    }

    /// <summary>
    /// 长头像点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void Story3_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hero selectHero = Role.role.GetHeroProperty().GetHero(Role.role.GetTeamProperty().GetTeam(Role.role.GetBaseProperty().m_iCurrentTeam).m_vecTeam[(int)args[0]]);
            if (null != selectHero)
            {
                Hiden();
                GUIHeroDetail herodetail = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERODETAIL) as GUIHeroDetail;
                herodetail.Show(Show, selectHero);
            }
        }
    }

    /// <summary>
    /// 长头像点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void Story4_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hero selectHero = Role.role.GetHeroProperty().GetHero(Role.role.GetTeamProperty().GetTeam(Role.role.GetBaseProperty().m_iCurrentTeam).m_vecTeam[(int)args[0]]);
            if (null != selectHero)
            {
                Hiden();
                GUIHeroDetail herodetail = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERODETAIL) as GUIHeroDetail;
                herodetail.Show(Show, selectHero);

            }
        }
    }

    /// <summary>
    /// 长头像点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void Story5_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hero selectHero = Role.role.GetHeroProperty().GetHero(Role.role.GetTeamProperty().GetTeam(Role.role.GetBaseProperty().m_iCurrentTeam).m_vecTeam[(int)args[0]]);
            if (null != selectHero)
            {
                Hiden();
                GUIHeroDetail herodetail = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERODETAIL) as GUIHeroDetail;
                herodetail.Show(Show, selectHero);
            }
        }
    }

    /// <summary>
    /// 检查位置，如果向右边滑，而右边又没有多的项，则将列表最后面一项移动过来顶替
    /// </summary>
    /// <param name="isRight"></param>
    private void ChangePosion(bool isRight)
    {
        if (!isRight)
        {
            var maxLeft = m_lstSlideItemList.Find(new Predicate<GameObject>(item =>
             {
                 return item.transform.localPosition.x < -350;
             }));
            if (maxLeft != null)  //向左滑动时，将最左边项目变换到最右边项目
            {
                maxLeft.transform.localPosition += new Vector3(1050, 0, 0);
            }
        }
        else
        {
            var maxRight = m_lstSlideItemList.Find(new Predicate<GameObject>(item =>
             {
                 return item.transform.localPosition.x > 350;
             }));
            if (maxRight != null)  //向右滑动时，将最右边项目变换到最左边项目
            {
                maxRight.transform.localPosition -= new Vector3(1050, 0, 0);
            }
        }
    }

    /// <summary>
    /// 重新排序显示
    /// </summary>
    private void SortSlide()
    {
        for (int i = 0; i < m_lstSlideItemList.Count; i++)
        {
            m_lstSlideItemList[i].transform.localPosition = new Vector3(350 * (i - 1), 0, 0);
        }
    }

    /// <summary>
    /// 刷新数据
    /// </summary>
    private void UpdateData()
    {
        //GAME_FUNCTION.EXCUTE_DELAY(GAME_DEFINE.FADEOUT_GUI_TIME + GAME_DEFINE.FADEIN_GUI_TIME, this.DelayToShowGUIEffect);

        //人物长图标1-5
        int[] arrHeros = Role.role.GetTeamProperty().GetTeam(Role.role.GetBaseProperty().m_iCurrentTeam).m_vecTeam;


        Hero item1 = Role.role.GetHeroProperty().GetHero(arrHeros[0]);
        if (item1 != null)
        {
            ResourceRequireOwner owner = ResourcesManager.GetInstance().LoadResouce(GAME_DEFINE.RESOURCE_TEX_PATH + item1.m_strAvatarL + ".res", 0, GAME_DEFINE.RES_VERSION, item1.m_strAvatarL, null, RESOURCE_TYPE.WEB_OBJECT, ENCRYPT_TYPE.NORMAL, new DownLoadCallBack((str, obj, arr) =>
            {
                //Debug.Log("load " + str);
                if (IsShow())
                {
                    if (obj != null && obj is Texture)
                    {
                        this.m_cTxtureStory1.mainTexture = obj as Texture;

                        this.m_cTxtureStory1.enabled = true;
                        this.m_cSpStory1Bg.enabled = true;
                        this.m_cSpStory1Nature.enabled = true;
                        this.m_cSpStory1Back.enabled = false;

                        GUI_FUNCTION.SET_AVATORL_BG(this.m_cSpStory1Bg, item1.m_eNature);
                        GUI_FUNCTION.SET_NATUREM(this.m_cSpStory1Nature, item1.m_eNature);
                    }
                    else
                    {
                        this.m_cTxtureStory1.enabled = false;
                        this.m_cSpStory1Bg.enabled = false;
                        this.m_cSpStory1Nature.enabled = false;
                        this.m_cSpStory1Back.enabled = true;
                        Debug.LogError("none  " + str);
                    }
                }
                else
                {
                    if (obj != null)
                    {
                        ResourcesManager.GetInstance().UnloadResource(str);
                    }
                }

            }));

            m_lstOwners.Add(owner);
            m_lstResName.Add(item1.m_strAvatarL);
        }
        else
        {
            this.m_cTxtureStory1.enabled = false;
            this.m_cSpStory1Bg.enabled = false;
            this.m_cSpStory1Nature.enabled = false;
            this.m_cSpStory1Back.enabled = true;
        }

        Hero item2 = Role.role.GetHeroProperty().GetHero(arrHeros[1]);
        if (item2 != null)
        {
            ResourceRequireOwner owner = ResourcesManager.GetInstance().LoadResouce(GAME_DEFINE.RESOURCE_TEX_PATH + item2.m_strAvatarL + ".res", 0, GAME_DEFINE.RES_VERSION, item2.m_strAvatarL, null, RESOURCE_TYPE.WEB_OBJECT, ENCRYPT_TYPE.NORMAL, new DownLoadCallBack((str, obj, arr) =>
            {
                //Debug.Log("load " + str);
                if (IsShow())
                {
                    if (obj != null && obj is Texture)
                    {
                        this.m_cTxtureStory2.mainTexture = obj as Texture;

                        this.m_cTxtureStory2.enabled = true;
                        this.m_cSpStory2Bg.enabled = true;
                        this.m_cSpStory2Nature.enabled = true;
                        this.m_cSpStory2Back.enabled = false;

                        GUI_FUNCTION.SET_AVATORL_BG(this.m_cSpStory2Bg, item2.m_eNature);
                        GUI_FUNCTION.SET_NATUREM(this.m_cSpStory2Nature, item2.m_eNature);
                    }
                    else
                    {
                        this.m_cTxtureStory2.enabled = false;
                        this.m_cSpStory2Bg.enabled = false;
                        this.m_cSpStory2Nature.enabled = false;
                        this.m_cSpStory2Back.enabled = true;
                        Debug.LogError("none  " + str);
                    }
                }
                else
                {
                    if (obj != null)
                    {
                        ResourcesManager.GetInstance().UnloadResource(str);
                    }
                }

            }));

            m_lstOwners.Add(owner);
            m_lstResName.Add(item2.m_strAvatarL);

        }
        else
        {
            this.m_cTxtureStory2.enabled = false;
            this.m_cSpStory2Bg.enabled = false;
            this.m_cSpStory2Nature.enabled = false;
            this.m_cSpStory2Back.enabled = true;
        }


        Hero item3 = Role.role.GetHeroProperty().GetHero(arrHeros[2]);
        if (item3 != null)
        {
            ResourceRequireOwner owner = ResourcesManager.GetInstance().LoadResouce(GAME_DEFINE.RESOURCE_TEX_PATH + item3.m_strAvatarL + ".res", 0, GAME_DEFINE.RES_VERSION, item3.m_strAvatarL, null, RESOURCE_TYPE.WEB_OBJECT, ENCRYPT_TYPE.NORMAL, new DownLoadCallBack((str, obj, arr) =>
            {
                //Debug.Log("load " + str);
                if (IsShow())
                {
                    if (obj != null && obj is Texture)
                    {
                        this.m_cTxtureStory3.mainTexture = obj as Texture;

                        this.m_cTxtureStory3.enabled = true;
                        this.m_cSpStory3Bg.enabled = true;
                        this.m_cSpStory3Nature.enabled = true;
                        this.m_cSpStory3Back.enabled = false;

                        GUI_FUNCTION.SET_AVATORL_BG(this.m_cSpStory3Bg, item3.m_eNature);
                        GUI_FUNCTION.SET_NATUREM(this.m_cSpStory3Nature, item3.m_eNature);
                    }
                    else
                    {
                        this.m_cSpStory3Back.enabled = true;
                        this.m_cTxtureStory3.enabled = false;
                        this.m_cSpStory3Bg.enabled = false;
                        this.m_cSpStory3Nature.enabled = false;
                        Debug.LogError("none  " + str);
                    }
                }
                else
                {
                    if (obj != null)
                    {
                        ResourcesManager.GetInstance().UnloadResource(str);
                    }
                }

            }));

            m_lstOwners.Add(owner);
            m_lstResName.Add(item3.m_strAvatarL);
        }
        else
        {
            this.m_cSpStory3Back.enabled = true;
            this.m_cTxtureStory3.enabled = false;
            this.m_cSpStory3Bg.enabled = false;
            this.m_cSpStory3Nature.enabled = false;
        }


        Hero item4 = Role.role.GetHeroProperty().GetHero(arrHeros[3]);
        if (item4 != null)
        {

            ResourceRequireOwner owner = ResourcesManager.GetInstance().LoadResouce(GAME_DEFINE.RESOURCE_TEX_PATH + item4.m_strAvatarL + ".res", 0, GAME_DEFINE.RES_VERSION, item4.m_strAvatarL, null, RESOURCE_TYPE.WEB_OBJECT, ENCRYPT_TYPE.NORMAL, new DownLoadCallBack((str, obj, arr) =>
            {
                //Debug.Log("load " + str);
                if (IsShow())
                {
                    if (obj != null && obj is Texture)
                    {
                        this.m_cTxtureStory4.mainTexture = obj as Texture;

                        this.m_cTxtureStory4.enabled = true;
                        this.m_cSpStory4Bg.enabled = true;
                        this.m_cSpStory4Nature.enabled = true;
                        this.m_cSpStory4Back.enabled = false;

                        GUI_FUNCTION.SET_AVATORL_BG(this.m_cSpStory4Bg, item4.m_eNature);
                        GUI_FUNCTION.SET_NATUREM(this.m_cSpStory4Nature, item4.m_eNature);
                    }
                    else
                    {
                        this.m_cSpStory4Back.enabled = true;
                        this.m_cTxtureStory4.enabled = false;
                        this.m_cSpStory4Bg.enabled = false;
                        this.m_cSpStory4Nature.enabled = false;
                        Debug.LogError("none  " + str);
                    }
                }
                else
                {
                    if (obj != null)
                    {
                        ResourcesManager.GetInstance().UnloadResource(str);
                    }
                }

            }));

            m_lstOwners.Add(owner);
            m_lstResName.Add(item4.m_strAvatarL);

        }
        else
        {
            this.m_cSpStory4Back.enabled = true;
            this.m_cTxtureStory4.enabled = false;
            this.m_cSpStory4Bg.enabled = false;
            this.m_cSpStory4Nature.enabled = false;
        }


        Hero item5 = Role.role.GetHeroProperty().GetHero(arrHeros[4]);
        if (item5 != null)
        {
            ResourceRequireOwner owner = ResourcesManager.GetInstance().LoadResouce(GAME_DEFINE.RESOURCE_TEX_PATH + item5.m_strAvatarL + ".res", 0, GAME_DEFINE.RES_VERSION, item5.m_strAvatarL, null, RESOURCE_TYPE.WEB_OBJECT, ENCRYPT_TYPE.NORMAL, new DownLoadCallBack((str, obj, arr) =>
            {
                //Debug.Log("load " + str);
                if (IsShow())
                {
                    if (obj != null&&obj is Texture)
                    {
                        this.m_cTxtureStory5.mainTexture = obj as Texture;

                        this.m_cTxtureStory5.enabled = true;
                        this.m_cSpStory5Bg.enabled = true;
                        this.m_cSpStory5Nature.enabled = true;
                        this.m_cSpStory5Back.enabled = false;

                        GUI_FUNCTION.SET_AVATORL_BG(this.m_cSpStory5Bg, item5.m_eNature);
                        GUI_FUNCTION.SET_NATUREM(this.m_cSpStory5Nature, item5.m_eNature);
                    }
                    else
                    {
                        this.m_cSpStory5Back.enabled = true;
                        this.m_cTxtureStory5.enabled = false;
                        this.m_cSpStory5Bg.enabled = false;
                        this.m_cSpStory5Nature.enabled = false;
                        Debug.LogError("none  " + str);
                    }
                }
                else
                {
                    if (obj != null)
                    {
                        ResourcesManager.GetInstance().UnloadResource(str);
                    }
                }

            }));

            m_lstOwners.Add(owner);
            m_lstResName.Add(item5.m_strAvatarL);

        }
        else
        {
            this.m_cSpStory5Back.enabled = true;
            this.m_cTxtureStory5.enabled = false;
            this.m_cSpStory5Bg.enabled = false;
            this.m_cSpStory5Nature.enabled = false;
        }

        m_cLbLeader.MakePixelPerfect();
        m_cLbLeader.gameObject.SetActive(true);
        int leadId = Role.role.GetTeamProperty().GetTeam(Role.role.GetBaseProperty().m_iCurrentTeam).GetLeaderIndex();
        switch (leadId)
        {
            case 0: this.m_cLbLeader.gameObject.transform.localPosition = new Vector3(-230, 145, 0); break;
            case 1: this.m_cLbLeader.gameObject.transform.localPosition = new Vector3(-115, 145, 0); break;
            case 2: this.m_cLbLeader.gameObject.transform.localPosition = new Vector3(15, 145, 0); break;
            case 3: this.m_cLbLeader.gameObject.transform.localPosition = new Vector3(145, 145, 0); break;
            case 4: this.m_cLbLeader.gameObject.transform.localPosition = new Vector3(290, 145, 0); break;
            default:
                break;
        }

        this.m_lstSlideItemList.Clear();
        m_lstSlideItemList.Add(m_cBtnWinArena);
        m_lstSlideItemList.Add(m_cBtnWinQuest);
        m_lstSlideItemList.Add(m_cBtnWinGate);
        //m_lstSlideItemList.Add(m_cBtnWinRaid);

        SortSlide();
    }

    /// <summary>
    /// 延时执行3d特效
    /// </summary>
    private void DelayToShowGUIEffect()
    {
        if (this.IsShow())
        {
            //CameraManager.GetInstance().ShowGUIEffectCamera();

            //if (this.m_cEffectLeader==null)
            //{
            //    this.m_cEffectLeader = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_EFFECT_PATH, EFFECT_LEADER)) as GameObject;
            //}

            //this.m_cEffectLeader.transform.parent = this.m_cEffectParent.transform;
            //this.m_cEffectLeader.transform.localPosition = Vector3.zero;
            //this.m_cEffectLeader.transform.localScale = Vector3.one;

            //m_cLbLeader.MakePixelPerfect();
            //m_cLbLeader.gameObject.SetActive(true);
            //int leadId = Role.role.GetTeamProperty().GetTeam(Role.role.GetBaseProperty().m_iCurrentTeam).GetLeaderIndex();
            //switch (leadId)
            //{
            //    case 0: this.m_cLbLeader.gameObject.transform.localPosition = new Vector3(-230, 145, 0); break;
            //    case 1: this.m_cLbLeader.gameObject.transform.localPosition = new Vector3(-115, 145, 0); break;
            //    case 2: this.m_cLbLeader.gameObject.transform.localPosition = new Vector3(15, 145, 0); break;
            //    case 3: this.m_cLbLeader.gameObject.transform.localPosition = new Vector3(145, 145, 0); break;
            //    case 4: this.m_cLbLeader.gameObject.transform.localPosition = new Vector3(290, 145, 0); break;
            //    default:
            //        break;
            //}
        }
  
    }
}