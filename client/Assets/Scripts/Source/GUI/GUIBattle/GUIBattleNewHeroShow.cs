using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Resource;
using Game.Gfx;
using Game.Base;
using Game.Media;

public class GUIBattleNewHeroShow : GUIBase
{
    private const string RES_MAIN = "GUI_HeroSummonResult";             //展示主资源地址
    private const string LB_INFO = "Label";  //英雄台词

    private const string HERO_ZHAOHUAN_ROOT = "ZHAOHUAN";   //展示根节点
    private const string HERO_STAY_POS = "After/HeroPos";    //3D模型站立位置
    private const string ZhaoHuan_BG = "BACK_GROUND";  //召唤效背景
    private const string DOOR_BG = "Door_black";  //门特效背景

    private const string MODEL_BG = "effect_GUI_choujiang_di1";  //模型展示的大英雄底图
    private const string MATERIALS_HERO_A1 = "GUI_choujiang_di_1";  //底部材质球
    private const string MATERIALS_HERO_A2 = "GUI_choujiang_di_2";  //底部材质球
    private const string MATERIALS_HERO_A3 = "GUI_choujiang_di_3";  //底部材质球
    private const string EFFECT_STAR_2 = "effect_GUI_mengjiang";               //2星及以下特效
    private const string EFFECT_STAR_3 = "effect_GUI_chaomengjiang";        //3星特效
    private const string EFFECT_STAR_4 = "effect_GUI_chaojuemengjiang";    //4星特效
    private const string EFFECT_STAR_5 = "effect_GUI_jueshiwushuang";       //5星特效
    private const string EFFECT_GUI_STAR_2 = "GUI_mengjiang";               //2星及以下特效
    private const string EFFECT_GUI_STAR_3 = "GUI_chaomengjiang";        //3星特效
    private const string EFFECT_GUI_STAR_4 = "GUI_chaojuemengjiang";    //4星特效
    private const string EFFECT_GUI_STAR_5 = "GUI_jueshiwushuang";       //5星特效
    private const string EFFECT_MOFA_SELF = "effect_GUI_HeroUpgradeEffect_mofa_self"; //魔法底盘
    private const string EFFECT_STAR_5_BLACK = "effect_GUI_wuxing_black"; //五星模型进入特效

    private UILabel m_cLbInfo;  //英雄台词
    private Hero m_iReusltHero;   //展示接口返回的英雄ID
    private GameObject m_cEffectStar2; //2星及以下特效
    private GameObject m_cEffectStar3; //3星特效
    private GameObject m_cEffectStar4; //4星特效
    private GameObject m_cEffectStar5; //5星特效
    private GameObject m_cEffectStar5Black; //五星模型进入特效
    private GameObject m_cEffectStarStr2; //2星及以下字特效
    private GameObject m_cEffectStarStr3; //3星字特效
    private GameObject m_cEffectStarStr4; //4星字特效
    private GameObject m_cEffectStarStr5; //5星字特效
    private GameObject m_cZhaoHuanBg;  //召唤背景
    private GameObject m_cSceneRoot;    //场景根节点
    private GameObject m_cStayPos;  //站立点
    private GfxObject m_cGfxHero;   //渲染实例
    private GameObject m_cModelBg; //3D背景
    private GameObject m_cModelMaterial1;
    private GameObject m_cModelMaterial2;
    private GameObject m_cModelMaterial3;
    private GameObject m_cMofaSelf;  //魔法底盘
    private GameObject m_cDoorBg;  //门特效背景

    private float m_fModelShowTime = 2f;  //播放模型时间
    private const float m_fHeroDetailShowTime = 2f;  //播放英雄详细信息时间
    private const float m_fStar2 = 1.5f;
    private const float m_fStar3 = 1.5f;
    private const float m_fStar4 = 2.8f;
    private const float m_fStar5 = 3;
    private float m_fdisStar;
    private float m_fdis;  //播放控制

    private ShowState m_eState;   //展示效果进展状态
    private bool m_bHasKeep = false;  //设置过keep状态

    /// <summary>
    /// 展示特效进展状态
    /// </summary>
    enum ShowState
    {
        Nomal = -1,              //普通状态
        Begin = 0,                //网络接口返回得到英雄以后，召唤动画开始

        ModelShowBegin = 1,       //3D模型展示
        ModelShowIng = 2,
        ModelShowEnd = 3,

        End = 4,                          //结束
    }

    public GUIBattleNewHeroShow(GUIManager mgr)
        : base(mgr, GUI_DEFINE.GUIID_BATTLE_NEWHEROSHOW, UILAYER.GUI_PANEL)
    {
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        if (this.m_cGfxHero != null)
            this.m_cGfxHero.Destory();

        this.m_cGfxHero = null;
        base.Destory();
    }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        GUI_FUNCTION.AYSNCLOADING_SHOW();
        
        if (this.m_cGUIObject == null)
        {
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_MAIN);
        }

		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + MODEL_BG);        //加载特效
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + EFFECT_STAR_2);  //加载特效
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + EFFECT_STAR_3);  //加载特效
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + EFFECT_STAR_4);  //加载特效
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + EFFECT_STAR_5);  //加载特效
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + EFFECT_MOFA_SELF);  //加载特效
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + EFFECT_STAR_5_BLACK);  //加载特效

        HeroTable heroTable = HeroTableManager.GetInstance().GetHeroTable(m_iReusltHero.m_iTableID);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_TEX_PATH + heroTable.AvatorARes);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_MODEL_PATH + heroTable.Modle);

        this.m_eLoadingState = LOADING_STATE.START;
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
            //实例化GameObject
            //召唤主资源
            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;
            this.m_cGUIObject.AddComponent<GUIComponentEvent>().AddIntputDelegate(ShowNext);

            this.m_cLbInfo = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LB_INFO);

            this.m_cSceneRoot = GUI_FINDATION.FIND_GAME_OBJECT(HERO_ZHAOHUAN_ROOT);
            this.m_cStayPos = GUI_FINDATION.GET_GAME_OBJECT(this.m_cSceneRoot, HERO_STAY_POS);
            this.m_cDoorBg = GUI_FINDATION.GET_GAME_OBJECT(this.m_cSceneRoot, DOOR_BG);
            this.m_cZhaoHuanBg = GUI_FINDATION.GET_GAME_OBJECT(this.m_cSceneRoot, ZhaoHuan_BG);

			this.m_cModelBg = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(MODEL_BG)) as GameObject;
            this.m_cModelBg.transform.parent = this.m_cSceneRoot.transform;
            this.m_cModelBg.transform.localScale = Vector3.one;
            this.m_cModelBg.transform.localPosition = Vector3.zero;
            this.m_cModelMaterial1 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cModelBg, MATERIALS_HERO_A1);
            this.m_cModelMaterial2 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cModelBg, MATERIALS_HERO_A2);
            this.m_cModelMaterial3 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cModelBg, MATERIALS_HERO_A3);
        }
        //台词显示
        HeroTable heroTable = HeroTableManager.GetInstance().GetHeroTable(m_iReusltHero.m_iTableID);
        this.m_cLbInfo.text = heroTable.Word;
        this.m_cLbInfo.enabled = true;
        this.m_cGUIMgr.SetCurGUIID(this.ID);

        SetLocalPos(Vector3.zero);
    }

    /// <summary>
    /// 隐藏展示
    /// </summary>
    private void ShowNext(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (this.m_eState== ShowState.ModelShowEnd)
            {
                this.m_eState++;
            }
        }
    }

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

        switch (this.m_eState)
        {
            case ShowState.Nomal:
                break;
            case ShowState.Begin:
                //隐藏背景，隐藏结算界面
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Hiden();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Hiden();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Hiden();
                //打开模型展示摄像头
                CameraManager.GetInstance().ShowUIHeroZhaoHuan2Camera();
                //全屏点击无效
                this.m_cGUIObject.collider.enabled = false;
                this.m_cDoorBg.SetActive(false);
                m_eState++;
                break;
            case ShowState.ModelShowBegin:
                //开启模型展示摄像头
                CameraManager.GetInstance().ShowUIHeroZhaoHuan2Camera();
                //3D模型的背景特效
                HeroTable heroTable = HeroTableManager.GetInstance().GetHeroTable(m_iReusltHero.m_iTableID);
                Texture heroBg = (Texture)ResourceMgr.LoadAsset(heroTable.AvatorARes);

                this.m_cModelMaterial1.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", heroBg);
                this.m_cModelMaterial2.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", heroBg);
                this.m_cModelMaterial3.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", heroBg);
                //将内存中的模型加载出来
                GameObject heroObj = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(heroTable.Modle)) as GameObject;
                heroObj.transform.parent = this.m_cStayPos.transform;
                heroObj.transform.localScale = Vector3.one;
                heroObj.transform.localPosition = Vector3.zero;
                this.m_cGfxHero = new GfxObject(heroObj);
                heroObj.SetActive(true);
                //英雄底盘
			this.m_cMofaSelf = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(EFFECT_MOFA_SELF)) as GameObject;
                this.m_cMofaSelf.transform.parent = this.m_cStayPos.transform;
                this.m_cMofaSelf.transform.localScale = Vector3.one;
                this.m_cMofaSelf.transform.localPosition = Vector3.zero;

                //将异步内存中的星级特效加载出来
                switch (heroTable.Star)
                {
                    case 1:
                    case 2:
                        //1星2星显示猛将特效
				this.m_cEffectStar2 = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(EFFECT_STAR_2)) as GameObject;
                        this.m_cEffectStar2.transform.parent = this.m_cSceneRoot.transform;
                        this.m_cEffectStar2.transform.localPosition = Vector3.zero;
                        this.m_cEffectStar2.transform.localScale = Vector3.one;
                        this.m_cEffectStar2.SetActive(true);
                        this.m_cEffectStarStr2 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cEffectStar2, EFFECT_GUI_STAR_2);
                        Animation[] m_vecAnim2 = this.m_cEffectStarStr2.GetComponentsInChildren<Animation>();
                        foreach (Animation item in m_vecAnim2)
                        {
                            m_fModelShowTime = m_fStar2;
                            item.wrapMode = WrapMode.Once;
                            item.Play("start");
                        }

                        //音效
						MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_SUMMON_STAR_2);
//                        MediaMgr.PlaySound(SOUND_DEFINE.SE_SUMMON_STAR_2);
                        break;
                    case 3:
                        //3星显示超猛将特效
                        this.m_cEffectStar3 = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(EFFECT_STAR_3)) as GameObject;
                        this.m_cEffectStar3.transform.parent = this.m_cSceneRoot.transform;
                        this.m_cEffectStar3.transform.localPosition = Vector3.zero;
                        this.m_cEffectStar3.transform.localScale = Vector3.one;
                        this.m_cEffectStar3.SetActive(true);
                        this.m_cEffectStarStr3 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cEffectStar3, EFFECT_GUI_STAR_3);
                        Animation[] m_vecAnim3 = this.m_cEffectStarStr3.GetComponentsInChildren<Animation>();
                        foreach (Animation item in m_vecAnim3)
                        {
                            m_fModelShowTime = m_fStar3;
                            item.wrapMode = WrapMode.Once;
                            item.Play("start");
                        }

                        //音效
						MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_SUMMON_STAR_3);
//                        MediaMgr.PlaySound(SOUND_DEFINE.SE_SUMMON_STAR_3);
                        break;
                    case 4:
                        //4星显示超绝猛将特效
                        this.m_cEffectStar4 = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(EFFECT_STAR_4)) as GameObject;
                        this.m_cEffectStar4.transform.parent = this.m_cSceneRoot.transform;
                        this.m_cEffectStar4.transform.localPosition = Vector3.zero;
                        this.m_cEffectStar4.transform.localScale = Vector3.one;
                        this.m_cEffectStar4.SetActive(true);
                        this.m_cEffectStarStr4 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cEffectStar4, EFFECT_GUI_STAR_4);
                        Animation[] m_vecAnim4 = this.m_cEffectStarStr4.GetComponentsInChildren<Animation>();
                        foreach (Animation item in m_vecAnim4)
                        {
                            m_fModelShowTime = m_fStar4;
                            item.wrapMode = WrapMode.Once;
                            item.Play("start");
                        }

                        //音效
						MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_SUMMON_STAR_4);
//                        MediaMgr.PlaySound(SOUND_DEFINE.SE_SUMMON_STAR_4);
                        break;
                    case 5:
                        //5星显示无双猛将特效
                        this.m_cEffectStar5 = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(EFFECT_STAR_5)) as GameObject;
                        this.m_cEffectStar5.transform.parent = this.m_cSceneRoot.transform;
                        this.m_cEffectStar5.transform.localPosition = Vector3.zero;
                        this.m_cEffectStar5.transform.localScale = Vector3.one;
                        this.m_cEffectStar5.SetActive(true);
                        this.m_cEffectStarStr5 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cEffectStar5, EFFECT_GUI_STAR_5);
                        Animation[] m_vecAnim5 = this.m_cEffectStarStr5.GetComponentsInChildren<Animation>();
                        foreach (Animation item in m_vecAnim5)
                        {
                            m_fModelShowTime = m_fStar5;
                            item.wrapMode = WrapMode.Once;
                            item.Play("start");
                        }

                        //音效
						MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_SUMMON_STAR_5);
//                        MediaMgr.PlaySound(SOUND_DEFINE.SE_SUMMON_STAR_5);

                        //5星背景特效
                        this.m_cEffectStar5Black = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(EFFECT_STAR_5_BLACK)) as GameObject;
                        this.m_cEffectStar5Black.transform.parent = this.m_cSceneRoot.transform;
                        this.m_cEffectStar5Black.transform.localPosition = Vector3.zero;
                        this.m_cEffectStar5Black.transform.localScale = Vector3.one;
                        this.m_cEffectStar5.SetActive(true);

                        break;
                    default:
                        break;
                }


                //下一状态
                m_fdisStar = GAME_TIME.TIME_FIXED();
                m_fdis = GAME_TIME.TIME_FIXED();
                this.m_eState++;
                break;
            case ShowState.ModelShowIng:
                //文字特效start结束
                if (GAME_TIME.TIME_FIXED() - m_fdisStar > m_fModelShowTime)
                {
                    //将猛将字符保持Keep状态
                    SetEffectKeep();
                    this.m_eState++;
                }
                break;
            case ShowState.ModelShowEnd:
                //等待全屏点击，关闭3D展示，进入战斗结算界面
                this.m_cGUIObject.collider.enabled = true;
                //this.m_eState++;
                break;
            case ShowState.End:
                Destory();
                //销毁特效
                GameObject.DestroyImmediate(m_cModelBg);
                GameObject.DestroyImmediate(m_cMofaSelf);
                GameObject.DestroyImmediate(this.m_cEffectStar2);
                GameObject.DestroyImmediate(this.m_cEffectStar3);
                GameObject.DestroyImmediate(this.m_cEffectStar4);
                GameObject.DestroyImmediate(this.m_cEffectStar5);
                GameObject.DestroyImmediate(this.m_cEffectStar5Black);
                m_bHasKeep = false;
                //关闭3D模型摄像头
                CameraManager.GetInstance().HidenUIHeroZhaoHuan2Camera();
                m_bHasKeep = false;

                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
                GUIBattleReward gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BATTLE_REWARD) as GUIBattleReward;
       //         gui.HeroShowEnd();
                gui.SetLocalPos( Vector3.one);
                break;
        }

        return true;
    }

    /// <summary>
    /// 设置字符Keep状态
    /// </summary>
    private void SetEffectKeep()
    {
        if (!m_bHasKeep)
        {
            HeroTable heroTable = HeroTableManager.GetInstance().GetHeroTable(m_iReusltHero.m_iTableID);

            switch (heroTable.Star)
            {
                case 1:
                case 2:
                    //1星2星显示猛将特效
                    Animation[] m_vecAnim2 = this.m_cEffectStarStr2.GetComponentsInChildren<Animation>();
                    foreach (Animation item in m_vecAnim2)
                    {
                        Debug.Log(item.name);
                        item.wrapMode = WrapMode.Loop;
                        item.Play("keep");
                    }
                    break;
                case 3:
                    //3星显示超猛将特效
                    Animation[] m_vecAnim3 = this.m_cEffectStarStr3.GetComponentsInChildren<Animation>();
                    foreach (Animation item in m_vecAnim3)
                    {
                        Debug.Log(item.name);

                        item.wrapMode = WrapMode.Loop;
                        item.Play("keep");
                    }
                    break;
                case 4:
                    //4星显示超绝猛将特效
                    Animation[] m_vecAnim4 = this.m_cEffectStarStr4.GetComponentsInChildren<Animation>();
                    foreach (Animation item in m_vecAnim4)
                    {
                        Debug.Log(item.name);

                        item.wrapMode = WrapMode.Loop;
                        item.Play("keep");
                    }
                    break;
                case 5:
                    //5星显示无双猛将特效
                    Animation[] m_vecAnim5 = this.m_cEffectStarStr5.GetComponentsInChildren<Animation>();
                    foreach (Animation item in m_vecAnim5)
                    {
                        Debug.Log(item.name);

                        item.wrapMode = WrapMode.Loop;
                        item.Play("keep");
                    }
                    break;
                default:
                    break;
            }

            m_bHasKeep = true;
        }
    }

    /// <summary>
    /// 展示抽卡特效
    /// </summary>
    public void ShowEffect(Hero hero)
    {
        this.m_iReusltHero = hero;
        //this.m_iReusltHero = Role.role.GetHeroProperty().GetHero(14815);
        this.m_eState =  ShowState.Begin;  //开始准备播放展示特效
        Show();
    }
}
