//Micro.Sanvey
//2013.11.12
//sanvey.china@gmail.com

using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Resource;
using Game.Gfx;
using Game.Base;
using Game.Media;

/// <summary>
/// 召唤GUI类
/// </summary>
public class GUISummonResult : GUIBase
{
    //----------------------------------------------资源地址--------------------------------------------------

    private const string RES_MAIN = "GUI_HeroSummonResult";             //召唤主资源地址
    private const string LB_INFO = "Label";  //英雄台词

    private const string HERO_ZHAOHUAN_ROOT = "ZHAOHUAN";   //展示根节点
    private const string HERO_STAY_POS = "After/HeroPos";    //3D模型站立位置
    private const string ZhaoHuan_BG = "BACK_GROUND";  //召唤效背景
    private const string DOOR_BG = "Door_black";  //门特效背景

    private const string MODEL_BG = "effect_GUI_choujiang_di1";  //模型展示的大英雄底图
    private const string MATERIALS_HERO_A1 = "GUI_choujiang_di_1";  //底部材质球
    private const string MATERIALS_HERO_A2 = "GUI_choujiang_di_2";  //底部材质球
    private const string MATERIALS_HERO_A3 = "GUI_choujiang_di_3";  //底部材质球
    private const string EFFECT_DOOR1_OPEN = "effect_Door_1";            //门打开特效 3星以下
    private const string EFFECT_DOOR2_OPEN = "effect_Door_2";            //门打开特效 4星
    private const string EFFECT_DOOR2_LIZI = "lizi";   //门打开特效粒子
    private const string EFFECT_DOOR3_OPEN = "effect_Door_3";            //门打开特效 5星
    private const string EFFECT_DOOR3_LIZI = "lizi";
    private const string EFFECT_STAR_2 = "effect_GUI_mengjiang";               //2星及以下特效
    private const string EFFECT_STAR_3 = "effect_GUI_chaomengjiang";        //3星特效
    private const string EFFECT_STAR_4 = "effect_GUI_chaojuemengjiang";    //4星特效
    private const string EFFECT_STAR_5 = "effect_GUI_jueshiwushuang";       //5星特效
    private const string EFFECT_GUI_STAR_2 = "GUI_mengjiang";               //2星及以下特效
    private const string EFFECT_GUI_STAR_3 = "GUI_chaomengjiang";        //3星特效
    private const string EFFECT_GUI_STAR_4 = "GUI_chaojuemengjiang";    //4星特效
    private const string EFFECT_GUI_STAR_5 = "GUI_jueshiwushuang";       //5星特效
    private const string EFFECT_MOFA_SELF = "effect_GUI_HeroJinhuaEffect_mofa_self"; //魔法底盘
    private const string EFFECT_STAR_5_BLACK = "effect_GUI_wuxing_black"; //五星模型进入特效

    //----------------------------------------------游戏对象--------------------------------------------------

    private UILabel m_cLbInfo;  //英雄台词
    private GameObject m_cEffectDoor1Open; //门打开特效  3星以下
    private GameObject m_cEffectDoor2Open; //门打开特效  4星
    private GameObject m_cEffectDoor3Open; //门打开特效  5星
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
    private GameObject m_cDoorBg;  //门特效背景
    private GameObject m_cSceneRoot;    //场景根节点
    private GameObject m_cStayPos;  //站立点
    private GfxObject m_cGfxHero;   //渲染实例
    private AIControl m_cAI;    //AI
    private GameObject m_cModelBg; //3D背景
    private GameObject m_cModelMaterial1;
    private GameObject m_cModelMaterial2;
    private GameObject m_cModelMaterial3;
    private GameObject m_cMofaSelf;  //魔法底盘

    //----------------------------------------------data--------------------------------------------------

    private bool m_bHasShow = false;  //加载过showobject
    private Hero m_iReusltHero;   //召唤接口返回的英雄ID
    private ZhaoHuanState m_eState;   //召唤效果进展状态
    private bool m_bHasKeep = false;  //设置过keep状态
    //private bool m_bHasDoor3 = false;  //门3特效是否播放过

    private const float m_fDoorKeepTime = 0.1f; //播放门展示状态停留时间
    private float m_fDoorOpenTime = 0f;  //播放门打开特效时间
    private const float DOOR_OPEN_1 = 1.5f;    //第1闪门播放时间
    private const float DOOR_OPNE_2 = 5f;    //第2扇门播放时间
    private const float DOOR_OPNE_3 = 3.7f;    //第3扇门播放时间
    //private const float DOOR_OPEN_3_1 = 2.6667f;  //第三门特效延长播放时间
    private float m_fModelShowTime = 2f;  //播放模型时间
    private const float m_fHeroDetailShowTime = 2f;  //播放英雄详细信息时间
    private const float m_fStar2 = 1.5f;
    private const float m_fStar3 = 1.5f;
    private const float m_fStar4 = 2.8f;
    private const float m_fStar5 = 3;
    private float m_fdisStar;
    private float m_fdis;  //播放控制
    private bool m_bDiamondOrFriend = true;

    /// <summary>
    /// 召唤特效进展状态
    /// </summary>
    enum ZhaoHuanState
    {
        Nomal = -1,              //普通状态
        Begin = 0,                //网络接口返回得到英雄以后，召唤动画开始

        DoorKeepBegin = 1,          //门展示
        DoorKeepIng = 2,
        DoorKeepEnd = 3,

        DoorOpenBegin = 4,       //门打开
        DoorOpenIng = 5,
        DoorOpenEnd = 6,

        ModelShowBegin = 7,       //3D模型展示
        ModelShowIng = 8,
        ModelShowEnd = 9,

        DetailShowBegin = 10,     //2D卡牌展示
        DetailShowIng = 11,
        DetailShowEnd = 12,

        End = 13,                          //结束
    }

    public GUISummonResult(GUIManager mgr)
        : base(mgr, GUI_DEFINE.GUIID_SUMMON_RESULT, GUILAYER.GUI_PANEL)
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
        this.m_eLoadingState = LOADING_STATE.START;
        GUI_FUNCTION.AYSNCLOADING_SHOW();

        if (this.m_cGUIObject == null)
        {
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_MAIN);
        }

        ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + EFFECT_DOOR1_OPEN);  //加载特效
        ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + EFFECT_DOOR2_OPEN);  //加载特效
        ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + EFFECT_DOOR3_OPEN);  //加载特效
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

    }

    /// <summary>
    /// 立即显示
    /// </summary>
    public override void ShowImmediately()
    {
        base.ShowImmediately();

        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Show();
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();

        this.SetLocalPos(Vector3.one);
        this.m_eState = ZhaoHuanState.Nomal;

        this.m_cGUIMgr.SetCurGUIID(this.ID);
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
            this.m_cGUIObject.AddComponent<GUIComponentEvent>().AddIntputDelegate(BgFull_OnClick);

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

        //m_bHasDoor3 = false;

        this.m_cGUIMgr.SetCurGUIID(this.ID);

        SetLocalPos(Vector3.zero);
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        base.Hiden();

        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
        CameraManager.GetInstance().HidenUIHeroZhaoHuanCamera();
        CameraManager.GetInstance().HidenUIHeroZhaoHuan2Camera();
        if (this.m_cGfxHero != null)
            this.m_cGfxHero.Destory();
        this.m_cGfxHero = null;
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


        switch (this.m_eState)
        {
            case ZhaoHuanState.Nomal:
                break;
            case ZhaoHuanState.Begin:
                //隐藏背景，隐藏召唤GUI
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Hiden();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Hiden();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Hiden();
                //关闭NGUI摄像头 out 新手引导GUI 关闭camera后无法显示
                //CameraManager.GetInstance().HidenGUICamera();
                //开启门特效摄像头
                CameraManager.GetInstance().ShowUIHeroZhaoHuanCamera();
                //关闭模型展示摄像头
                CameraManager.GetInstance().HidenUIHeroZhaoHuan2Camera();
                //设置背景和门特效背景有效
                this.m_cZhaoHuanBg.SetActive(false);
                this.m_cDoorBg.SetActive(true);
                this.m_cModelBg.SetActive(false);
                //全屏点击无效
                this.m_cGUIObject.collider.enabled = false;
                //英雄台词
                this.m_cLbInfo.enabled = false;
                HeroTable heroTable5 = HeroTableManager.GetInstance().GetHeroTable(m_iReusltHero.m_iTableID);
                this.m_cLbInfo.text = heroTable5.Word;
                //音效
                MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_SUMMON_ENTER);
                //状态
                this.m_eState++;
                break;
            case ZhaoHuanState.DoorKeepBegin:
                HeroTable heroTable2 = HeroTableManager.GetInstance().GetHeroTable(m_iReusltHero.m_iTableID);
                switch (heroTable2.Star)
                {
                    case 1:
                    case 2:
                    case 3:
                        //加载异步中的门特效
						this.m_cEffectDoor1Open = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(EFFECT_DOOR1_OPEN)) as GameObject;
                        this.m_cEffectDoor1Open.transform.parent = this.m_cSceneRoot.transform;
                        this.m_cEffectDoor1Open.transform.localPosition = Vector3.zero;
                        this.m_cEffectDoor1Open.transform.localScale = Vector3.one;
                        this.m_cEffectDoor1Open.SetActive(true);
                        Animation[] m_vecAni1 = this.m_cEffectDoor1Open.GetComponentsInChildren<Animation>();
                        foreach (Animation item in m_vecAni1)
                        {
                            item.Play("keep");
                        }
                        break;
                    case 4:
                        //加载异步中的门特效
						this.m_cEffectDoor2Open = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(EFFECT_DOOR2_OPEN)) as GameObject;
                        this.m_cEffectDoor2Open.transform.parent = this.m_cSceneRoot.transform;
                        this.m_cEffectDoor2Open.transform.localPosition = Vector3.zero;
                        this.m_cEffectDoor2Open.transform.localScale = Vector3.one;

                        //for (int i = 0; i <   this.m_cEffectDoor2Open.transform.childCount; i++)
                        //{
                        //    GameObject obj = this.m_cEffectDoor2Open.transform.GetChild(i).gameObject;
                        //    if (obj.name!="Door_2_1")
                        //    {
                        //        obj.SetActive(false);
                        //    }
                        //}
  
                        this.m_cEffectDoor2Open.SetActive(true);
                        Animation[] m_vecAni3 = this.m_cEffectDoor2Open.GetComponentsInChildren<Animation>();
                        foreach (Animation item in m_vecAni3)
                        {
                            item.Play("keep");
                        }

                        GameObject lizi2 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cEffectDoor2Open, EFFECT_DOOR2_LIZI);
                        if (lizi2 != null)
                            lizi2.SetActive(false);
                        break;
                    case 5:
                        //加载异步中的门特效
						this.m_cEffectDoor3Open = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(EFFECT_DOOR3_OPEN)) as GameObject;
                        this.m_cEffectDoor3Open.transform.parent = this.m_cSceneRoot.transform;
                        this.m_cEffectDoor3Open.transform.localPosition = Vector3.zero;
                        this.m_cEffectDoor3Open.transform.localScale = Vector3.one;

                        //for (int i = 0; i < this.m_cEffectDoor3Open.transform.childCount; i++)
                        //{
                        //    GameObject obj = this.m_cEffectDoor3Open.transform.GetChild(i).gameObject;
                        //    if (obj.name != "Door_3_1")
                        //    {
                        //        obj.SetActive(false);
                        //    }
                        //}

                        this.m_cEffectDoor3Open.SetActive(true);
                        Animation[] m_vecAni4 = this.m_cEffectDoor3Open.GetComponentsInChildren<Animation>();
                        foreach (Animation item in m_vecAni4)
                        {
                            item.Play("keep");
                        }
                        GameObject lizi3 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cEffectDoor3Open, EFFECT_DOOR3_LIZI);
                        if (lizi3 != null)
                            lizi3.SetActive(false);
                        break;
                    default: break;
                }
                //下一状态
                m_fdis = GAME_TIME.TIME_FIXED();
                this.m_eState++;
                break;
            case ZhaoHuanState.DoorKeepIng:
                //门keep展示时间
                float dis = GAME_TIME.TIME_FIXED() - m_fdis;
                if (dis > m_fDoorKeepTime)
                {
                    //下一状态
                    this.m_eState++;
                    GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_SUMMON3);
                }
                break;
            case ZhaoHuanState.DoorKeepEnd:
                //等待全屏点击，触发开门特效
                this.m_cGUIObject.collider.enabled = true;
                break;
            case ZhaoHuanState.DoorOpenBegin:
                //全屏点击无效
                this.m_cGUIObject.collider.enabled = false;
                if (m_cEffectDoor1Open != null)
                {
                    Animation[] m_vecAni2 = this.m_cEffectDoor1Open.GetComponentsInChildren<Animation>();
                    foreach (Animation item in m_vecAni2)
                    {
                        item.Play("open");
                    }
                    m_fDoorOpenTime = DOOR_OPEN_1;

                    //音效
                    MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_SUMMON_DOOR_1);
                }
                if (m_cEffectDoor2Open != null)
                {
                    //for (int i = 0; i < this.m_cEffectDoor2Open.transform.childCount; i++)
                    //{
                    //    GameObject obj = this.m_cEffectDoor2Open.transform.GetChild(i).gameObject;
                    //    obj.SetActive(true);
                    //}

                    Animation[] m_vecAni2 = this.m_cEffectDoor2Open.GetComponentsInChildren<Animation>();
                    foreach (Animation item in m_vecAni2)
                    {
                        item.Play("open");
                    }
                    GameObject lizi2 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cEffectDoor2Open, EFFECT_DOOR2_LIZI);
                    if (lizi2 != null)
                        lizi2.SetActive(true);

                    m_fDoorOpenTime = DOOR_OPNE_2;

                    //音效
                    MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_SUMMON_DOOR_2);
                }
                if (m_cEffectDoor3Open != null)
                {
                    //for (int i = 0; i < this.m_cEffectDoor3Open.transform.childCount; i++)
                    //{
                    //    GameObject obj = this.m_cEffectDoor3Open.transform.GetChild(i).gameObject;

                    //    if (obj.name != "Door_3_3")
                    //    {
                    //        obj.SetActive(true);
                    //    }
                    //}
                    

                    Animation[] m_vecAni2 = this.m_cEffectDoor3Open.GetComponentsInChildren<Animation>();
                    foreach (Animation item in m_vecAni2)
                    {
                        item.Play("open");
                    }
                    GameObject lizi3 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cEffectDoor3Open, EFFECT_DOOR3_LIZI);
                    if (lizi3 != null)
                        lizi3.SetActive(true);

                    m_fDoorOpenTime = DOOR_OPNE_3;

                    //音效
                    MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_SUMMON_DOOR_3);
                }

                //下一状态
                m_fdis = GAME_TIME.TIME_FIXED();
                this.m_eState++;
                break;
            case ZhaoHuanState.DoorOpenIng:
                float dis2 = GAME_TIME.TIME_FIXED() - m_fdis;
                //if (dis2 > DOOR_OPEN_3_1)
                //{
                //    if (m_cEffectDoor3Open != null&&!m_bHasDoor3)
                //    {
                //        for (int i = 0; i < this.m_cEffectDoor3Open.transform.childCount; i++)
                //        {
                //            GameObject obj = this.m_cEffectDoor3Open.transform.GetChild(i).gameObject;

                //            if (obj.name == "Door_3_3")
                //            {
                //                obj.SetActive(true);
                //            }
                //        }
                //        m_bHasDoor3 = true;
                //    }
                //}
                if (dis2 > m_fDoorOpenTime)
                {
                    //下一状态
                    this.m_eState++;
                }
                break;
            case ZhaoHuanState.DoorOpenEnd:
                //下一状态
                this.m_eState++;
                break;
            case ZhaoHuanState.ModelShowBegin:
                //开启NGUI摄像头
                CameraManager.GetInstance().ShowGUICamera();
                //关闭门特效摄像头
                CameraManager.GetInstance().HidenUIHeroZhaoHuanCamera();
                //开启模型展示摄像头
                CameraManager.GetInstance().ShowUIHeroZhaoHuan2Camera();
                //设置背景和门特效背景无效
                this.m_cZhaoHuanBg.SetActive(true);
                this.m_cDoorBg.SetActive(false);
                this.m_cModelBg.SetActive(true);
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
                this.m_cMofaSelf.transform.localScale = Vector3.one*0.5f;  //由于英雄被放大2倍，魔法盘加载2倍的位置上，这时候缩小回原来的
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
                //台词显示
                this.m_cLbInfo.enabled = true;
      

                //销毁特效
                GameObject.DestroyImmediate(this.m_cEffectDoor1Open);
                GameObject.DestroyImmediate(this.m_cEffectDoor2Open);
                GameObject.DestroyImmediate(this.m_cEffectDoor3Open);

                //下一状态
                m_fdisStar = GAME_TIME.TIME_FIXED();
                m_fdis = GAME_TIME.TIME_FIXED();
                this.m_eState++;
                break;
            case ZhaoHuanState.ModelShowIng:
                //文字特效start结束
                if (GAME_TIME.TIME_FIXED() - m_fdisStar > m_fModelShowTime)
                {
                    //将猛将字符保持Keep状态
                    SetEffectKeep();
                    this.m_eState++;
                }
                break;
            case ZhaoHuanState.ModelShowEnd:
                //等待全屏点击，关闭3D展示，进入2D-GUI英雄详细界面
                this.m_cGUIObject.collider.enabled = true;
                break;
            case ZhaoHuanState.DetailShowBegin:
                //销毁模型
                if (this.m_cGfxHero != null)
                    this.m_cGfxHero.Destory();
                this.m_cGfxHero = null;
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
                //显示GUI背景
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
                //显示英雄详细GUI
                GUIHeroDetail tmp = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_HERODETAIL) as GUIHeroDetail;
                tmp.Show(HeroDetailBack, m_iReusltHero);
                //关闭台词显示
                this.m_cLbInfo.enabled = false;
                //下一状态
                m_fdis = GAME_TIME.TIME_FIXED();
                this.m_eState++;
                break;
            case ZhaoHuanState.DetailShowIng:
                break;
            case ZhaoHuanState.DetailShowEnd:
                //隐藏英雄详细界面
                GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_HERODETAIL).Hiden();
                break;
            case ZhaoHuanState.End:
                //重新展示召唤
                GUISummon sum= GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_SUMMON) as GUISummon;
                sum.m_bDiamondOrFriend = m_bDiamondOrFriend;
                sum.ShowImmediately();

                break;
            default:
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
    /// 英雄展示完成回调
    /// </summary>
    private void HeroDetailBack()
    {
        this.m_eState = ZhaoHuanState.Nomal;
        this.Destory();
        GUISummon sum = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_SUMMON) as GUISummon;
        sum.m_bDiamondOrFriend = this.m_bDiamondOrFriend;
        sum.ShowImmediately();
    }

    /// <summary>
    /// 展示抽卡特效
    /// </summary>
    public void ShowEffect(Hero hero)
    {
        this.m_iReusltHero = hero;
        //this.m_iReusltHero = Role.role.GetHeroProperty().GetHero(14815);
        this.m_eState = ZhaoHuanState.Begin;  //开始准备播放召唤特效
        Show();
    }

    /// <summary>
    /// 打开门Click
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BgFull_OnClick(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            //门等待 全屏点击后 开启门
            if (this.m_eState == ZhaoHuanState.DoorKeepEnd)
            {
                this.m_eState = ZhaoHuanState.DoorOpenBegin;
            }
            //3D模型展示等待，全屏点击后，进入2D-GUI英雄详细展示
            else if (this.m_eState == ZhaoHuanState.ModelShowEnd)
            {
                this.m_eState = ZhaoHuanState.DetailShowBegin;
            }
        }
    }

    /// <summary>
    /// 设备砖石召唤和好友召唤标志
    /// </summary>
    /// <param name="p"></param>
    public void SetDiamondOrFriend(bool p)
    {
        m_bDiamondOrFriend = p;
    }
}