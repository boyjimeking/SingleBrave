//  GUIUpgradeHeroResult.cs
//  Author: Sanvey
//  2013-12-25

using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using Game.Resource;
using Game.Gfx;
using Game.Base;

/// <summary>
/// 英雄强化结果动画GUI
/// </summary>
public class GUIHeroUpgradeResult : GUIBase
{
    public GUIHeroUpgradeResult(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_UPGRADEHERORESULT, GUILAYER.GUI_PANEL)
    {
    }

    #region-----------------------------------------------------------资源地址---------------------------------------------------------------

    //界面主GUI资源
    private const string RES_MAIN = "GUI_HeroUpgradeResult";    //主面板
    private const string BTN_UP = "BtnUp";  //跳过按钮
    private const string BTN_COLLIDER = "Collider"; //全屏碰撞
    private const string OBJ_BBNEW = "Attribute/BBNew"; //技能面板
    private const string OBJ_NEW = "Attribute/New"; //升级后面板
    private const string OBJ_OLD = "Attribute/Old"; //旧属性面板
    private const string RES_TOP = "Top";   //UI TOP面板路径
    private const string LAB_HERONAME = "HeroName"; //英雄名字标题
    private const string SP_HERO_NATURE = "Property"; //英雄属性标题
    private const string LAB_HEROTEXT = "HeroText"; //英雄台词
    private const string OBJ_PROPERTY = "Property"; //英雄属性
    //数值
    private const string LAB_ATTACK = "AttackValue";    //攻击//
    private const string LAB_ATTACKNEW = "AttackValueNew";  //攻击升级后数值//
    private const string LAB_BB = "BBValue";    //BB等级//
    private const string LAB_BBNEW = "BBValueNew";  //BB等级升级后//
    private const string LAB_DEFENSE = "DefenseValue";  //防御//
    private const string LAB_DEFENSENEW = "DefenseValueNew";    //防御升级后数值//
    private const string LAB_HP = "HpValue";    //HP//
    private const string LAB_HPNEW = "HpValueNew";  //HP升级后数值//
    private const string LAB_LV = "LvValue";    //等级//
    private const string LAB_LVNEW = "LvValueNew";  //升级后的等级//
    private const string LAB_REVERT = "RevertValue";    //回复力//
    private const string LAB_REVERTNEW = "RevertValueNew";  //回复力升级后数值//
    //经验条
    private const string LAB_UPEXP = "Exp/UpExpValue";  //经验显示//
    private const string SLIDER_UPEXP = "Exp/ExpBar";   //经验条//
    private const string LAB_UPEXPPROMPT = "Exp/UpExpPrompt";   //强化成功后文字地址//
    //3D进化场景主资源
    private const string HERO_3D_ROOT = "HERO_LEVEL_UP";    //3D模型面板//
    private const string RES_HERO_UPGRADE = "GUI_HERO_UPGRADE";  //3d资源
    private const string HERO_BACKGROUND = "BACK_Base";  //3D模型主面板背景
    private const string HERO_BACKGROUND_BACK = "BACK_Back";
    private const string HERO_RESULT = "Result";    //3D结果面板//
    private const string HERO_Panel = "Team"; //3D模型队伍面板//
    private const string HERO_POS = "HeroPos";  //主英雄的位置//
    private const string HERO_OBJPOS = "Pos_";  //其他英雄升级位置//
    private const string HERO_AFTERPOS = "After/HeroPos";   //成功出来后的位置//
    //成功特效类型
    private const string RES_SUCCEED_P = "effect_GUI_HeroUpgradeEffect_CG";   //成功
    private const string RES_SUCCEED_D = "effect_GUI_HeroUpgradeEffect_DCG";   //大成功
    private const string RES_SUCCEED_C = "effect_GUI_HeroUpgradeEffect_CCG";   //超成功
    //素材上升消失特效
    private const string EFFECT_FIRE_HERO = "effect_GUI_HeroUpgradeEffect_xuanzhong_1";
    private const string EFFECT_WATER_HERO = "effect_GUI_HeroUpgradeEffect_xuanzhong_2";
    private const string EFFECT_WOOD_HERO = "effect_GUI_HeroUpgradeEffect_xuanzhong_3";
    private const string EFFECT_THUNDER_HERO = "effect_GUI_HeroUpgradeEffect_xuanzhong_4";
    private const string EFFECT_BRIGHT_HERO = "effect_GUI_HeroUpgradeEffect_xuanzhong_5";
    private const string EFFECT_DARK_HERO = "effect_GUI_HeroUpgradeEffect_xuanzhong_6";
    //强化英雄特效
    private const string EFFECT_FIRE_SELF = "effect_GUI_HeroUpgradeEffect_1";
    private const string EFFECT_WATER_SELF = "effect_GUI_HeroUpgradeEffect_2";
    private const string EFFECT_WOOD_SELF = "effect_GUI_HeroUpgradeEffect_3";
    private const string EFFECT_THUNDER_SELF = "effect_GUI_HeroUpgradeEffect_4";
    private const string EFFECT_BRIGHT_SELF = "effect_GUI_HeroUpgradeEffect_5";
    private const string EFFECT_DARK_SELF = "effect_GUI_HeroUpgradeEffect_6";
    //魔法底座
    private const string EFFECT_DI_SELF = "effect_GUI_HeroUpgradeEffect_mofa_self";
    //英雄等级提升特效
    private const string RES_LEVEL_UP_EFFECT = "effect_GUI_HeroLevelupEffect";    //英雄升级特效
    //英雄技能提升特效
    private const string RES_SKILL_UP_EFFECT = "effect_GUI_skillup";  //技能升级特效

    #endregion

    #region-----------------------------------------------------------游戏对象---------------------------------------------------------------

    private GameObject m_cBtnUp;    //一键升级按钮
    private GameObject m_cBtnCollider;  //结果背面碰撞
    private GameObject m_cPanTop;  //滑入对象
    private GameObject m_cOld;  //老属性面板节点
    private GameObject m_cNew;  //新属性面板节点
    private GameObject m_cBBNew;    //BB升级面板
    private GameObject m_cProperty; //英雄属性
    private UILabel m_cLbHeroName;  //英雄名字
    private UISprite m_cSpHeroNature;   //英雄属性图标
    private UISprite m_cSprProperty; //英雄属性图标
    private UILabel m_cLbHeroText;  //英雄台词
    //属性
    private UILabel m_labAttack;
    private UILabel m_labAttackNew;
    private UILabel m_labBB;
    private UILabel m_labBBNew;
    private UILabel m_labDefense;
    private UILabel m_labDefenseNew;
    private UILabel m_labHp;
    private UILabel m_labHpNew;
    private UILabel m_labLv;
    private UILabel m_labLvNew;
    private UILabel m_labRevert;
    private UILabel m_labRevertNew;
    //经验条
    private UILabel m_labUpExp; //升级经验文本//
    private UISlider m_sliderUpExp; //升级经验条//
    private UILabel m_cLbSuccessInfo;   //成功文本//
    //3D对象
    private GameObject m_cSceneRoot; //主界面//
    private GameObject m_cHeroUpgrade; //3d资源
    private GameObject m_cBack;  //背景
    private GameObject m_cBase;//背景
    private GameObject m_cHeroResult;   //结果面板//
    private GameObject m_cHeroPanel;    //英雄面板//
    private GameObject m_cHeroPos;  //英雄位置//
    private GameObject m_cHeroResultTeam;   //结果面板队伍//
    private GameObject m_cHeroAfterPos; //成功出来的英雄位置//

    //临时存放对象
    private GfxObject m_cGfxSelf;    //强化英雄
    private List<GfxObject> m_lstGfxHeros;    //牺牲英雄素材模型
    private List<GameObject> m_lstEffectHeros;    //牺牲英雄特效
    private GameObject m_cEffectSelfHero;  //强化英雄特效
    private GameObject m_cEffectModelPan;   //强化后模型展示底盘
    private GameObject m_cEffectLevelupEffect; //等级提升特效
    private GameObject m_cEffectSkillUpEffect;  //技能提升特效
    private GameObject m_cEffectSuccee;   //成功特效

    #endregion

    #region-----------------------------------------------------------游戏数据---------------------------------------------------------------

    //需要外面传值设置的Data
    private Hero m_cHeroSelf;      //强化英雄
    private List<Hero> m_lstDeleteHeros;  //强化牺牲掉的素材英雄
    private UpgradeSucessType m_eSuccedType;   //成功 大成功 or 超成功
    private List<UpgradeAttribute> m_lstUpgradeProcess;  //升级过程数据列表

    //临时控制
    private bool m_bHasSyncLoad = false;  //异步加载完成标志
    /// <summary>
    /// 进行状态
    /// </summary>
    private enum State
    {
        Start = 0,  //开始
        ModelShowBegin,  //强化模型展示
        ModelShowIng,
        ModelShowEnd,
        EffectUpBegin,   //强化特效展示
        EffectUpIng,
        EffectUpEnd,
        AfterModelShowBegin,  //强化后英雄展示
        AfterModelShowIng,
        AfterModelShowEnd,
        PropertyPanelInBegin,  //英雄属性面板移入
        PropertyPanelInIng,
        PropteryPanelInEnd,
        ExpSlideBegin,    //经验条前进
        ExpSlideIng,
        ExpSlideEnd,
        LevelUpShowBegin,  //英雄升级特效展示
        LevelUpShowIng,
        LevelUpShowEnd,
        SkillUpShowBegin,  //英雄技能升级特效展示
        SkillUpShowIng,
        SkillUpShowEnd,
        ShowLast,  //显示剩余全部数值
        ShowLastEnd,
        End,       //结束
    }
    private State m_eState;  //进行状态
    private float m_fDis;  //时间控制
    private bool m_bSkillShow;  //技能显示
    private int m_iLevelRunNum = 0;  //升级进度

    //常量  

    //private const float MODEL_SHOW_TIME = 1f;  //强化前 模型展示 停留等待时间
    private const float EFFECT_UP_HERO_DESTORY_TIME = 1.8f;   //强化牺牲英雄消失时间
    private const float EFFECT_UP_TIME = 5f;  //强化特效结束
    private const float TWEEN_POS_TIME = 1F;    //位置位移时间
    private const float SLIDE_RUN_TIME = 1f;  //滑动条时间
    private const float LEVEL_UP_TIME = 2;  //等级提升时间
    private const float SKILL_UP_TIME = 2;  //技能提升时间
    private const float END_SHOW_TIME = 1;  //结束显示时间
    //成功特效展示时间
    private const float SUCCESS_1_TIME = 2.5f;
    private const float SUCCESS_2_TIME = 3f;
    private const float SUCCESS_3_TIME = 4.8f;
    //成功特效展示字符      //Todo : put to string table
    private const string SUCCESS_1_TEXT = "";
    private const string SUCCESS_2_TEXT = "大成功！经验值1.5倍！";
    private const string SUCCESS_3_TEXT = "超成功！经验值2倍！";
    //位置控制
    private Vector3 OUT_RIGHT_POS = new Vector3(640f, 0, 0);    //外移位置
    private Vector3 OUT_LEFT_POS = new Vector3(-640f, 0, 0);    //外移位置
    //属性颜色控制
    private const string COLOR_AFTER = "#0096DC]";   //升级后颜色
    private const string COLOR_NOMAL = "#FFFFFF]";   //升级前颜色

    #endregion

    #region-----------------------------------------------------------重写方法---------------------------------------------------------------

    // 显示
    public override void Show()
    {
        this.m_eLoadingState = LOADING_STATE.START;
        GUI_FUNCTION.AYSNCLOADING_SHOW();

        //加载GUI主资源
        ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_MAIN);
        //3d对象资源
        ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_HERO_UPGRADE);
        //加载强化英雄模型和特效
        ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_MODEL_PATH, GAME_DEFINE.RES_VERSION, m_cHeroSelf.m_strModel);
        ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_EFFECT_PATH, GAME_DEFINE.RES_VERSION, GetEffectSelf(m_cHeroSelf));
        //加载强化牺牲素材英雄模型和特效
        foreach (Hero hero in m_lstDeleteHeros)
        {
            if (hero == null)
            {
                Debug.LogError("Null");
                continue;
            }
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_MODEL_PATH, GAME_DEFINE.RES_VERSION, hero.m_strModel);
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_EFFECT_PATH, GAME_DEFINE.RES_VERSION, GetEffectHero(hero));
        }
        //升级英雄魔法底盘
        ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_EFFECT_PATH, GAME_DEFINE.RES_VERSION, EFFECT_DI_SELF);
        ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_EFFECT_PATH, GAME_DEFINE.RES_VERSION, GetEffectSuccessType(m_eSuccedType));
        //加载等级提升特效
        ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_EFFECT_PATH, GAME_DEFINE.RES_VERSION, RES_LEVEL_UP_EFFECT);
        //加载技能提升特效
        ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_EFFECT_PATH, GAME_DEFINE.RES_VERSION, RES_SKILL_UP_EFFECT);

    }

    // 隐藏
    public override void Hiden()
    {
        ResourcesManager.GetInstance().UnloadUnusedResources();

        base.Hiden();
        CameraManager.GetInstance().HidenUIHeroUpgradeCamera();
        SetLocalPos(Vector3.one * 0xFFFF);

        Destory();
    }

    //销毁
    public override void Destory()
    {
        m_cBase = null;
        m_cBack = null;

        if (m_cHeroUpgrade!=null)
        {
            GameObject.DestroyImmediate(m_cHeroUpgrade);
        }
        m_cHeroUpgrade = null;


        m_cBtnUp = null;    //一键升级按钮
        m_cBtnCollider = null;  //结果背面碰撞
        m_cPanTop = null;  //滑入对象
        m_cOld = null;  //老属性面板节点
        m_cNew = null;  //新属性面板节点
        m_cBBNew = null;    //BB升级面板
        m_cProperty = null; //英雄属性
        m_cLbHeroName = null;  //英雄名字
        m_cSpHeroNature = null;   //英雄属性图标
        m_cSprProperty = null; //英雄属性图标
        m_cLbHeroText = null;  //英雄台词
        //属性
        m_labAttack = null;
        m_labAttackNew = null;
        m_labBB = null;
        m_labBBNew = null;
        m_labDefense = null;
        m_labDefenseNew = null;
        m_labHp = null;
        m_labHpNew = null;
        m_labLv = null;
        m_labLvNew = null;
        m_labRevert = null;
        m_labRevertNew = null;
        //经验条
        m_labUpExp = null; //升级经验文本//
        m_sliderUpExp = null; //升级经验条//
        m_cLbSuccessInfo = null;   //成功文本//
        //3D对象
        m_cSceneRoot = null; //主界面//
        m_cHeroResult = null;   //结果面板//
        m_cHeroPanel = null;    //英雄面板//
        m_cHeroPos = null;  //英雄位置//
        m_cHeroResultTeam = null;   //结果面板队伍//
        m_cHeroAfterPos = null; //成功出来的英雄位置//

        m_cEffectSelfHero = null;  //强化英雄特效
        m_cEffectModelPan = null;   //强化后模型展示底盘
        m_cEffectLevelupEffect = null; //等级提升特效
        m_cEffectSkillUpEffect = null;  //技能提升特效
        m_cEffectSuccee = null;   //成功特效

        //临时存放对象
        m_cGfxSelf = null;    //强化英雄
        if (m_lstGfxHeros != null) m_lstGfxHeros.Clear();    //牺牲英雄素材模型
        if (m_lstEffectHeros != null) m_lstEffectHeros.Clear();    //牺牲英雄特效

        base.Hiden();
        base.Destory();
    }

    // 显示内存中对象
    protected override void InitGUI()
    {
        base.Show();

        GUI_FUNCTION.AYSNCLOADING_HIDEN();

        if (m_cGUIObject == null)
        {
            //GUI资源
            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;
            //跳过按钮
            this.m_cBtnUp = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, BTN_UP);
            this.m_cBtnUp.AddComponent<GUIComponentEvent>().AddIntputDelegate(OnUp);
            //全屏触控
            this.m_cBtnCollider = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, BTN_COLLIDER);
            this.m_cBtnCollider.AddComponent<GUIComponentEvent>().AddIntputDelegate(OnCollider);
            //显示属性滑入面板
            m_cPanTop = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, RES_TOP);
            m_cLbHeroName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cPanTop, LAB_HERONAME);
            m_cSpHeroNature = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cPanTop, SP_HERO_NATURE);
            //属性
            this.m_cProperty = GUI_FINDATION.GET_GAME_OBJECT(m_cPanTop, OBJ_PROPERTY);
            this.m_cSprProperty = this.m_cProperty.GetComponent<UISprite>();
            this.m_cNew = GUI_FINDATION.GET_GAME_OBJECT(m_cPanTop, OBJ_NEW);
            this.m_cOld = GUI_FINDATION.GET_GAME_OBJECT(m_cPanTop, OBJ_OLD);
            this.m_labAttack = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cOld, LAB_ATTACK);
            this.m_labAttackNew = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cNew, LAB_ATTACKNEW);
            this.m_labBB = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cOld, LAB_BB);
            this.m_cBBNew = GUI_FINDATION.GET_GAME_OBJECT(m_cPanTop, OBJ_BBNEW);
            this.m_labBBNew = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cBBNew, LAB_BBNEW);
            this.m_labDefense = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cOld, LAB_DEFENSE);
            this.m_labDefenseNew = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cNew, LAB_DEFENSENEW);
            this.m_labHp = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cOld, LAB_HP);
            this.m_labHpNew = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cNew, LAB_HPNEW);
            this.m_labLv = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cOld, LAB_LV);
            this.m_labLvNew = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cNew, LAB_LVNEW);
            this.m_labRevert = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cOld, LAB_REVERT);
            this.m_labRevertNew = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cNew, LAB_REVERTNEW);
            //滑动条
            this.m_labUpExp = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cPanTop, LAB_UPEXP);
            this.m_sliderUpExp = GUI_FINDATION.GET_OBJ_COMPONENT<UISlider>(m_cPanTop, SLIDER_UPEXP);
            this.m_cLbSuccessInfo = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cPanTop, LAB_UPEXPPROMPT);
            //台词
            this.m_cLbHeroText = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cGUIObject, LAB_HEROTEXT);
            //3D场景
            this.m_cSceneRoot = GUI_FINDATION.FIND_GAME_OBJECT(HERO_3D_ROOT);
            this.m_cHeroUpgrade = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_HERO_UPGRADE)) as GameObject;
            this.m_cHeroUpgrade.transform.parent = this.m_cSceneRoot.transform;
            this.m_cHeroUpgrade.transform.localScale = Vector3.one;
            this.m_cHeroUpgrade.transform.localPosition = Vector3.zero;
            this.m_cHeroResult = GUI_FINDATION.GET_GAME_OBJECT(m_cHeroUpgrade, HERO_RESULT);
            this.m_cHeroPanel = GUI_FINDATION.GET_GAME_OBJECT(m_cHeroUpgrade, HERO_Panel);
            this.m_cHeroPos = GUI_FINDATION.GET_GAME_OBJECT(m_cHeroResult, HERO_POS);
            this.m_cHeroResultTeam = GUI_FINDATION.GET_GAME_OBJECT(m_cHeroResult, HERO_Panel);
            this.m_cHeroAfterPos = GUI_FINDATION.GET_GAME_OBJECT(m_cHeroUpgrade, HERO_AFTERPOS);
            m_cBack = GUI_FINDATION.GET_GAME_OBJECT(m_cHeroUpgrade, HERO_BACKGROUND_BACK);
            m_cBase = GUI_FINDATION.GET_GAME_OBJECT(m_cHeroUpgrade, HERO_BACKGROUND);
        }

        m_cBase.SetActive(false);
        m_cBack.SetActive(true);

        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Hiden();
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Hiden();
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Hiden();

        this.m_cGUIMgr.SetCurGUIID(this.ID);
        SetLocalPos(Vector3.one);

        this.m_eState = State.Start;

        GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_HERO_UP6);
    }

    //逻辑更新
    public override bool Update()
    {
        //资源加载等待
        switch (this.m_eLoadingState)
        {
            case LOADING_STATE.START:
                this.m_eLoadingState++;
                return false;
            case LOADING_STATE.LOADING:
                if (ResourcesManager.GetInstance().GetProgress() >= 1f && ResourcesManager.GetInstance().IsComplete())
                {
                    this.m_eLoadingState++;
                }
                return false;
            case LOADING_STATE.END:
                InitGUI();
                this.m_eLoadingState++;
                break;
        }

        //界面显示控制
        if (!IsShow()) return false;

        float dis = 0;  //时间量
        switch (m_eState)
        {
            case State.Start:
                //开启英雄强化摄像头
                CameraManager.GetInstance().ShowUIHeroUpgradeCamera();
                //设置面板初始状态
                this.m_cHeroPanel.SetActive(false);    //英雄面板
                this.m_cHeroResult.SetActive(false);   //强化面板
                this.m_cBtnUp.SetActive(false);          //关闭跳过按钮显示
                this.m_cPanTop.SetActive(false);        //关闭属性面板显示
                this.m_cLbHeroText.enabled = false;
                this.m_eState++;
                break;
            case State.ModelShowBegin:
                this.m_eState++;
                break;
            case State.ModelShowIng:
                this.m_eState++;
                break;
            case State.ModelShowEnd:
                this.m_eState++;
                break;
            case State.EffectUpBegin:
                //开启强化面板
                this.m_cHeroResult.SetActive(true);   //强化面板
                //音效
                SoundManager.GetInstance().PlaySound2(SOUND_DEFINE.SE_EVO_HERO);
                //加载强化英雄模型
                GameObject objSelf = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(m_cHeroSelf.m_strModel)) as GameObject;
                objSelf.transform.parent = m_cHeroPos.transform;
                objSelf.transform.localPosition = Vector3.zero;
                objSelf.transform.localScale = Vector3.one;
                objSelf.name = m_cHeroSelf.m_strModel;
                m_cGfxSelf = new GfxObject(objSelf);
                //加载牺牲英雄素材和特效
                m_lstGfxHeros = new List<GfxObject>();
                m_lstEffectHeros = new List<GameObject>();
                for (int i = 0; i < m_lstDeleteHeros.Count; i++)
                {
                    Hero hero = m_lstDeleteHeros[i];
                    //加载
                    GameObject objPos = GUI_FINDATION.GET_GAME_OBJECT(m_cHeroResultTeam, HERO_OBJPOS + (i + 1));
                    GameObject objHero = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(hero.m_strModel)) as GameObject;
                    GameObject objEffect = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(GetEffectHero(hero))) as GameObject;
                    //模型
                    objHero.transform.parent = objPos.transform;
                    objHero.transform.localPosition = Vector3.zero;
                    objHero.transform.localScale = Vector3.one;
                    objHero.name = hero.m_strModel;
                    m_lstGfxHeros.Add(new GfxObject(objHero));
                    //特效
                    objEffect.transform.parent = objPos.transform;
                    objEffect.transform.localPosition = Vector3.zero;
                    objEffect.transform.localScale = Vector3.one;
                    m_lstEffectHeros.Add(objEffect);
                }
                //强化英雄特效
                m_cEffectSelfHero = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(GetEffectSelf(m_cHeroSelf))) as GameObject;
                m_cEffectSelfHero.transform.parent = m_cHeroResult.transform;
                m_cEffectSelfHero.transform.localPosition = Vector3.zero;
                m_cEffectSelfHero.transform.localScale = Vector3.one;
                //设置状态进度
                this.m_fDis = GAME_TIME.TIME_FIXED();
                this.m_eState++;
                break;
            case State.EffectUpIng:
                dis = GAME_TIME.TIME_FIXED() - m_fDis;
                if (dis > EFFECT_UP_HERO_DESTORY_TIME)
                {
                    //销毁牺牲对象
                    foreach (GfxObject gfx in m_lstGfxHeros)
                    {
                        gfx.Destory();
                    }
                    m_lstGfxHeros.Clear();
                }
                if (dis > EFFECT_UP_TIME)
                {
                    this.m_eState++;
                }
                break;
            case State.EffectUpEnd:
                this.m_eState++;
                break;
            case State.AfterModelShowBegin:
                //成功类型特效
                m_cEffectSuccee = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(GetEffectSuccessType(m_eSuccedType))) as GameObject;
                m_cEffectSuccee.transform.parent = m_cSceneRoot.transform;
                m_cEffectSuccee.transform.localPosition = Vector3.zero;
                //魔法底盘
                m_cEffectModelPan = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(EFFECT_DI_SELF)) as GameObject;
                m_cEffectModelPan.transform.parent = m_cHeroAfterPos.transform;
                m_cEffectModelPan.transform.position = m_cGfxSelf.GetPos();
                m_cEffectModelPan.transform.localScale = Vector3.one;
                //设置升级后模型位置和缩放
                m_cGfxSelf.SetParent(m_cHeroAfterPos.transform);
                m_cGfxSelf.SetLocalScale(Vector3.one);
                //台词显示
                m_cLbHeroText.enabled = true;
                m_cLbHeroText.text = HeroTableManager.GetInstance().GetHeroTable(m_cHeroSelf.m_iTableID).Word;

                //销毁强化和被强化特效
                GameObject.Destroy(m_cEffectSelfHero);  //强化的特效
                foreach (GameObject item in m_lstEffectHeros)  //被强化特效
                {
                    GameObject.Destroy(item);
                }
                m_lstEffectHeros.Clear();

                //设置状态
                this.m_fDis = GAME_TIME.TIME_FIXED();
                this.m_eState++;
                break;
            case State.AfterModelShowIng:
                dis = GAME_TIME.TIME_FIXED() - m_fDis;
                if (dis > GetEffectSuccessTime(this.m_eSuccedType))
                {
                    this.m_eState++;
                }
                break;
            case State.AfterModelShowEnd:
                GameObject.DestroyImmediate(m_cEffectSuccee);
                this.m_eState++;
                break;
            case State.PropertyPanelInBegin:
                //填充显示面板
                m_cLbHeroName.text = m_cHeroSelf.m_strName;  //英雄名称
                //英雄属性
                GUI_FUNCTION.SET_NATUREM(m_cSpHeroNature, m_cHeroSelf.m_eNature);  //m_cSpHeroNature.transform.localPosition = new Vector3(-70f - 18f * (m_cHeroSelf.m_strName.Length - 2), m_cSpHeroNature.transform.localPosition.y, m_cSpHeroNature.transform.localPosition.z);
                //设置英雄名字与属性的位置
                m_cProperty.transform.localPosition = new Vector3(m_cSprProperty.localSize.x / 2 - (m_cSprProperty.localSize.x + m_cLbHeroName.localSize.x + 20) / 2, m_cProperty.transform.localPosition.y, m_cProperty.transform.localPosition.z);
                m_cLbHeroName.transform.localPosition = new Vector3((m_cSprProperty.localSize.x + m_cLbHeroName.localSize.x + 20) / 2 - m_cLbHeroName.localSize.x / 2, m_cLbHeroName.transform.localPosition.y, m_cLbHeroName.transform.localPosition.z);
                //设置显示数字
                SetFirstProcessText(false, false);  //显示初始，隐藏 New字段 和 Skill字段
                //设置滑动条
                SetExpBar(0);
                //设置英雄台词
                this.m_cLbSuccessInfo.text = GetSuccessText(this.m_eSuccedType);
                m_cPanTop.transform.localPosition = OUT_LEFT_POS;
                m_cPanTop.SetActive(true);
                CTween.TweenPosition(m_cPanTop, 0, TWEEN_POS_TIME, OUT_LEFT_POS, Vector3.zero, TWEEN_LINE_TYPE.ElasticInOut, null);
                //设置状态
                this.m_fDis = GAME_TIME.TIME_FIXED();
                this.m_eState++;
                break;
            case State.PropertyPanelInIng:
                dis = GAME_TIME.TIME_FIXED() - m_fDis;
                if (dis > TWEEN_POS_TIME + 0.3)
                {
                    this.m_eState++;
                }
                break;
            case State.PropteryPanelInEnd:
                this.m_eState++;
                break;
            case State.ExpSlideBegin:
                //设置显示数字
                SetFirstProcessText(false, false);  //显示初始，隐藏 New字段 和 Skill字段
                      this.m_cBtnUp.SetActive(true);          //开启跳过按钮显示
                //英雄已经满级
                if (this.m_lstUpgradeProcess[this.m_iLevelRunNum].m_iLv == m_cHeroSelf.m_iMaxLevel)
                {
                    this.m_sliderUpExp.value = 0;
                    this.m_labUpExp.text = "---";
                    this.m_eState = State.SkillUpShowBegin;
                }
                else
                {
                    //设置进度条 和 剩余经验
                    int ExpDistance = (HeroEXPTableManager.GetInstance().GetMaxExp(m_cHeroSelf.m_iExpType, m_lstUpgradeProcess[m_iLevelRunNum].m_iLv) - HeroEXPTableManager.GetInstance().GetMinExp(m_cHeroSelf.m_iExpType, m_lstUpgradeProcess[m_iLevelRunNum].m_iLv));
                    this.m_sliderUpExp.value = this.m_lstUpgradeProcess[this.m_iLevelRunNum].m_iExp / ExpDistance;
                    this.m_labUpExp.text = ((int)(ExpDistance - this.m_lstUpgradeProcess[this.m_iLevelRunNum].m_iExp)).ToString();

                    //设置状态
                    this.m_fDis = GAME_TIME.TIME_FIXED();
                    this.m_eState++;
                }
                break;
            case State.ExpSlideIng:
                //数字跳动音效
                SoundManager.GetInstance().PlaySoundContinue(SOUND_DEFINE.SE_NUM_JUMP);
                dis = GAME_TIME.TIME_FIXED() - m_fDis;
                SetExpBar(dis / SLIDE_RUN_TIME);
                if (dis >= SLIDE_RUN_TIME)
                {
                    //数字跳动音效关闭
                    SoundManager.GetInstance().StopSoundContinue();
                    SetExpBar(1);
                    this.m_eState++;
                }
                break;
            case State.ExpSlideEnd:
                this.m_eState++;
                break;
            case State.LevelUpShowBegin:
                //等级是否提升
                if (m_lstUpgradeProcess[m_iLevelRunNum].m_iLv != m_lstUpgradeProcess[m_iLevelRunNum + 1].m_iLv)
                {
                    //升级音效
                    SoundManager.GetInstance().PlaySound2(SOUND_DEFINE.SE_UPGRADE);
                    //设置显示数字
                    SetFirstProcessText(true, false);
                    //加载等级提升特效
                    this.m_cEffectLevelupEffect = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_LEVEL_UP_EFFECT)) as GameObject;
                    this.m_cEffectLevelupEffect.transform.parent = this.m_cSceneRoot.transform;
                    this.m_cEffectLevelupEffect.transform.localScale = Vector3.one;
                    this.m_cEffectLevelupEffect.transform.localPosition = Vector3.zero;
                    //设置状态
                    this.m_fDis = GAME_TIME.TIME_FIXED();
                    this.m_eState++;
                }
                else
                {
                    this.m_eState = State.SkillUpShowBegin;
                }
                break;
            case State.LevelUpShowIng:
                dis = GAME_TIME.TIME_FIXED() - m_fDis;
                if (dis > LEVEL_UP_TIME)
                {
                    //this.m_eState++;  //自动下一页
                }
                break;
            case State.LevelUpShowEnd:
                GameObject.DestroyImmediate(m_cEffectLevelupEffect);
                this.m_eState++;
                break;
            case State.SkillUpShowBegin:
                //最后一行数据展示
                if (this.m_iLevelRunNum == this.m_lstUpgradeProcess.Count - 2)
                {
                    // 技能提升
                    if (this.m_bSkillShow)
                    {
                        //升级音效
                        SoundManager.GetInstance().PlaySound2(SOUND_DEFINE.SE_UPGRADE);
                        //设置显示数字
                        SetFirstProcessText(true, true);
                        //加载技能提升特效
                        this.m_cEffectSkillUpEffect = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_SKILL_UP_EFFECT)) as GameObject;
                        this.m_cEffectSkillUpEffect.transform.parent = this.m_cSceneRoot.transform;
                        this.m_cEffectSkillUpEffect.transform.localScale = Vector3.one;
                        this.m_cEffectSkillUpEffect.transform.localPosition = Vector3.zero;
                        //设置状态
                        this.m_fDis = GAME_TIME.TIME_FIXED();
                        this.m_eState++;
                    }
                    else
                    {
                        //设置状态
                        this.m_fDis = GAME_TIME.TIME_FIXED();
                        this.m_eState = State.ShowLast;
                    }
                }
                else
                {
                    //不是最后一行继续
                    this.m_iLevelRunNum++;
                    this.m_eState = State.ExpSlideBegin;
                }
                break;
            case State.SkillUpShowIng:
                dis = GAME_TIME.TIME_FIXED() - m_fDis;
                if (dis > SKILL_UP_TIME)
                {
                    this.m_eState++;
                }
                break;
            case State.SkillUpShowEnd:
                GameObject.DestroyImmediate(m_cEffectSkillUpEffect);
                //设置状态
                this.m_fDis = GAME_TIME.TIME_FIXED();
                this.m_eState++;
                break;
            case State.ShowLast:
                dis = GAME_TIME.TIME_FIXED() - m_fDis;
                if (dis > END_SHOW_TIME)
                {
                    // this.m_eState++;  //自动跳转
                }
                break;
            case State.ShowLastEnd:
                break;
            case State.End:
                UpgradeEnd();
                break;
            default:
                break;
        }
        return base.Update();
    }
    
    #endregion

    #region-----------------------------------------------------------事件执行---------------------------------------------------------------

    //全屏点击事件
    private void OnCollider(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {

            if (this.m_eState == State.LevelUpShowIng)
            {
                this.m_eState = State.LevelUpShowEnd;
            }
            else if (this.m_eState== State.ShowLast)
            {
                if (this.m_bSkillShow)
                {
                    this.m_eState = State.End;
                }
                else
                {
                    SetLastProcessText();
                    this.m_eState = State.ShowLastEnd;
                }
            }
            else if (this.m_eState== State.ShowLastEnd)
            {
                this.m_eState = State.End;
            }
        }
    }

    //跳过升级动画
    private void OnUp(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (this.m_eState == State.ShowLastEnd)
            {
                this.m_eState = State.End;
            }
            else
            {
                this.m_eState = State.ShowLastEnd;

                GameObject.DestroyImmediate(m_cEffectLevelupEffect);
                GameObject.DestroyImmediate(m_cEffectSkillUpEffect);

                SetLastProcessText(this.m_iLevelRunNum);
                this.m_iLevelRunNum = m_lstUpgradeProcess.Count - 2;
                SetExpBar(1);
            }

            SoundManager.GetInstance().StopSoundContinue();
        }
    }

    #endregion

    #region -----------------------------------------------------------私有方法---------------------------------------------------------------

    // 获得强化英雄属性对应特效
    private string GetEffectSelf(Hero hero)
    {
        switch (hero.m_eNature)
        {
            case Nature.Fire:
                return EFFECT_FIRE_SELF;
            case Nature.Water:
                return EFFECT_WATER_SELF;
            case Nature.Wood:
                return EFFECT_WOOD_SELF;
            case Nature.Thunder:
                return EFFECT_THUNDER_SELF;
            case Nature.Bright:
                return EFFECT_BRIGHT_SELF;
            case Nature.Dark:
                return EFFECT_DARK_SELF;
        }
        return null;
    }

    // 获得英雄对应的素材上升消失特效
    private string GetEffectHero(Hero hero)
    {
        switch (hero.m_eNature)
        {
            case Nature.Fire:
                return EFFECT_FIRE_HERO;
            case Nature.Water:
                return EFFECT_WATER_HERO;
            case Nature.Wood:
                return EFFECT_WOOD_HERO;
            case Nature.Thunder:
                return EFFECT_THUNDER_HERO;
            case Nature.Bright:
                return EFFECT_BRIGHT_HERO;
            case Nature.Dark:
                return EFFECT_DARK_HERO;
        }
        return null;
    }

    // 获得成功，大成功，超成功特效路径
    private string GetEffectSuccessType(UpgradeSucessType type)
    {
        switch (type)
        {
            case UpgradeSucessType.Nomal: return RES_SUCCEED_P;
            case UpgradeSucessType.BigSuccess: return RES_SUCCEED_D;
            case UpgradeSucessType.ExtraSuccess: return RES_SUCCEED_C;
            default:
                break;
        }
        Debug.LogError("Error Upgrade success type :" + type.ToString());
        return RES_SUCCEED_P;
    }

    // 获得成功特效展示时间
    private float GetEffectSuccessTime(UpgradeSucessType type)
    {
        switch (type)
        {
            case UpgradeSucessType.Nomal: return SUCCESS_1_TIME;
            case UpgradeSucessType.BigSuccess: return SUCCESS_2_TIME; ;
            case UpgradeSucessType.ExtraSuccess: return SUCCESS_3_TIME; ;
            default:
                break;
        }
        Debug.LogError("Error Upgrade success type :" + type.ToString());
        return SUCCESS_1_TIME;
    }

    // 获得成功字符提示
    private string GetSuccessText(UpgradeSucessType type)
    {
        switch (type)
        {
            case UpgradeSucessType.Nomal: return SUCCESS_1_TEXT;
            case UpgradeSucessType.BigSuccess: return SUCCESS_2_TEXT;
            case UpgradeSucessType.ExtraSuccess: return SUCCESS_3_TEXT;
            default:
                break;
        }
        Debug.LogError("Error Upgrade success type :" + type.ToString());
        return SUCCESS_1_TEXT;
    }

    // 设置显示字符
    private void SetFirstProcessText(bool showNew, bool showSkill)
    {
        //old
        m_labLv.text = m_lstUpgradeProcess[m_iLevelRunNum].m_iLv.ToString();
        m_labAttack.text = m_lstUpgradeProcess[m_iLevelRunNum].m_iAttack.ToString();
        m_labDefense.text = m_lstUpgradeProcess[m_iLevelRunNum].m_iDefend.ToString();
        m_labHp.text = m_lstUpgradeProcess[m_iLevelRunNum].m_iHp.ToString();
        m_labRevert.text = m_lstUpgradeProcess[m_iLevelRunNum].m_iRecover.ToString();
        //new
        m_labAttackNew.text = COLOR_AFTER + m_lstUpgradeProcess[m_iLevelRunNum + 1].m_iAttack.ToString();
        m_labDefenseNew.text = COLOR_AFTER + m_lstUpgradeProcess[m_iLevelRunNum + 1].m_iDefend.ToString();
        m_labHpNew.text = COLOR_AFTER + m_lstUpgradeProcess[m_iLevelRunNum + 1].m_iHp.ToString();
        m_labLvNew.text = COLOR_AFTER + m_lstUpgradeProcess[m_iLevelRunNum + 1].m_iLv.ToString() + COLOR_NOMAL + "/" + m_cHeroSelf.m_iMaxLevel.ToString();
        m_labRevertNew.text = COLOR_AFTER + m_lstUpgradeProcess[m_iLevelRunNum + 1].m_iRecover.ToString();
        //bb
        m_labBB.text = m_lstUpgradeProcess[0].m_iBBLv.ToString();
        m_labBBNew.text = COLOR_AFTER + m_lstUpgradeProcess[m_lstUpgradeProcess.Count + -1].m_iBBLv.ToString() + COLOR_NOMAL + "/" + "10";

        m_cNew.SetActive(showNew);
        m_cBBNew.SetActive(showSkill);
    }

    // 设置最后显示字符
    private void SetLastProcessText()
    {
        int lastCount = m_lstUpgradeProcess.Count - 1;
        m_labLv.text = m_lstUpgradeProcess[lastCount].m_iLv.ToString();
        m_labAttack.text = m_lstUpgradeProcess[lastCount].m_iAttack.ToString();
        m_labDefense.text = m_lstUpgradeProcess[lastCount].m_iDefend.ToString();
        m_labHp.text = m_lstUpgradeProcess[lastCount].m_iHp.ToString();
        m_labRevert.text = m_lstUpgradeProcess[lastCount].m_iRecover.ToString();

        m_labAttackNew.text = COLOR_AFTER + m_lstUpgradeProcess[lastCount].m_iAttack.ToString();
        m_labDefenseNew.text = COLOR_AFTER + m_lstUpgradeProcess[lastCount].m_iDefend.ToString();
        m_labHpNew.text = COLOR_AFTER + m_lstUpgradeProcess[lastCount].m_iHp.ToString();
        m_labLvNew.text = COLOR_AFTER + m_lstUpgradeProcess[lastCount].m_iLv.ToString() + COLOR_NOMAL + "/" + m_cHeroSelf.m_iMaxLevel.ToString();
        m_labRevertNew.text = COLOR_AFTER + m_lstUpgradeProcess[lastCount].m_iRecover.ToString();


        m_labBB.text = m_lstUpgradeProcess[0].m_iBBLv.ToString();
        m_labBBNew.text = COLOR_AFTER + m_lstUpgradeProcess[lastCount].m_iBBLv.ToString() + COLOR_NOMAL + "/" + "10";

        m_cNew.SetActive(true);
        m_cBBNew.SetActive(true);
    }

    // 设置最后显示字符
    private void SetLastProcessText(int index)
    {
        int lastCount = m_lstUpgradeProcess.Count - 1;
        m_labLv.text = m_lstUpgradeProcess[index].m_iLv.ToString();
        m_labAttack.text = m_lstUpgradeProcess[index].m_iAttack.ToString();
        m_labDefense.text = m_lstUpgradeProcess[index].m_iDefend.ToString();
        m_labHp.text = m_lstUpgradeProcess[index].m_iHp.ToString();
        m_labRevert.text = m_lstUpgradeProcess[index].m_iRecover.ToString();

        m_labAttackNew.text = COLOR_AFTER + m_lstUpgradeProcess[lastCount].m_iAttack.ToString();
        m_labDefenseNew.text = COLOR_AFTER + m_lstUpgradeProcess[lastCount].m_iDefend.ToString();
        m_labHpNew.text = COLOR_AFTER + m_lstUpgradeProcess[lastCount].m_iHp.ToString();
        m_labLvNew.text = COLOR_AFTER + m_lstUpgradeProcess[lastCount].m_iLv.ToString() + COLOR_NOMAL + "/" + m_cHeroSelf.m_iMaxLevel.ToString();
        m_labRevertNew.text = COLOR_AFTER + m_lstUpgradeProcess[lastCount].m_iRecover.ToString();


        m_labBB.text = m_lstUpgradeProcess[0].m_iBBLv.ToString();
        m_labBBNew.text = COLOR_AFTER + m_lstUpgradeProcess[lastCount].m_iBBLv.ToString() + COLOR_NOMAL + "/" + "10";

        m_cNew.SetActive(true);
        m_cBBNew.SetActive(true);
    }

    //设置进度条
    private void SetExpBar(float p)
    {
        float ExpDistance = 0;
        float maxDistance = (HeroEXPTableManager.GetInstance().GetMaxExp(m_cHeroSelf.m_iExpType, m_lstUpgradeProcess[m_iLevelRunNum].m_iLv) - HeroEXPTableManager.GetInstance().GetMinExp(m_cHeroSelf.m_iExpType, m_lstUpgradeProcess[m_iLevelRunNum].m_iLv));
        if (this.m_lstUpgradeProcess[this.m_iLevelRunNum + 1].m_iExp == 0)
        {
            ExpDistance = (float)maxDistance - (float)this.m_lstUpgradeProcess[this.m_iLevelRunNum].m_iExp;
        }
        else
        {
            ExpDistance = this.m_lstUpgradeProcess[this.m_iLevelRunNum + 1].m_iExp - this.m_lstUpgradeProcess[this.m_iLevelRunNum].m_iExp;
        }
        this.m_sliderUpExp.value = m_lstUpgradeProcess[m_iLevelRunNum].m_iExp / maxDistance + p * (ExpDistance) / maxDistance;
        this.m_labUpExp.text = ((int)(maxDistance - ExpDistance * p - this.m_lstUpgradeProcess[this.m_iLevelRunNum].m_iExp)).ToString();

        //英雄已经满级
        if (this.m_lstUpgradeProcess[this.m_iLevelRunNum].m_iLv == m_cHeroSelf.m_iMaxLevel)
        {
            this.m_sliderUpExp.value = 0;
            this.m_labUpExp.text = "---";
        }
    }

    //结束升级
    private void UpgradeEnd()
    {
        m_cGfxSelf.Destory();
        GameObject.DestroyImmediate(this.m_cEffectModelPan);
        Hiden();

        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Show();
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();

        GUIHeroUpgrade upgra = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_UPGRADEHERO) as GUIHeroUpgrade;
        upgra.SetUpgradeData(this.m_cHeroSelf.m_iID,new List<int>());
        upgra.Show();
    }

    #endregion

    #region -----------------------------------------------------------暴露方法---------------------------------------------------------------

    /// <summary>
    /// 设置本界面展示必要数据
    /// </summary>
    /// <param name="afterHero">强化英雄</param>
    /// <param name="deleteHeros">强化牺牲掉的素材英雄</param>
    /// <param name="successType">成功 or 大成功 or 超成功</param>
    /// <param name="UpProcesses">升级过程数据列表</param>
    public void SetShowData(Hero afterHero, List<Hero> deleteHeros, UpgradeSucessType successType, List<UpgradeAttribute> UpProcesses)
    {
        this.m_cHeroSelf = afterHero;
        this.m_lstDeleteHeros = deleteHeros;
        this.m_eSuccedType = successType;
        this.m_lstUpgradeProcess = UpProcesses;
        this.m_bSkillShow = false;
        for (int i = 0; i < UpProcesses.Count - 1; i++)
        {
            if (UpProcesses[i].m_iBBLv != UpProcesses[i + 1].m_iBBLv)
            {
                this.m_bSkillShow = true;
            }
        }
        this.m_iLevelRunNum = 0;
    }

    #endregion
}