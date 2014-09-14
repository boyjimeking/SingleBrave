//  GUIHeroEvolutionResult.cs
//  Author: Cheng Xia
//  2013-12-31

using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using Game.Resource;
using Game.Gfx;
using Game.Base;
using Game.Media;

/// <summary>
/// 进化结果界面
/// </summary>
public class GUIHeroEvolutionResult : GUIBase
{
    /// <summary>
    /// RES为GameObject节点 Hero为3D界面 
    /// </summary>
    private const string RES_MAIN = "GUI_HeroEvolutionResult";
    private const string RES_GET = "Get";
    private const string RES_TEXT = "Text/Label";
    private const string HERO_EVOLUTION = "HERO_JINHUA";
    private const string RES_HERO_JINHUA = "GUI_HERO_JINHUA";

    private const string HERO_RESULT = "Result";
    private const string HERO_BEFORE = "Before";
    private const string HERO_ENDPOS = "EndPos";
    private const string HERO_TEAM_POS = "Team/Pos_";
    private const string HERO_AFTER = "After";
    private const string HERO_AFTERPOS = "After/HeroPos";   //出来后的位置//

    private const string BTN_COLLIDER = "Collider"; //进化结果碰撞地址//

    //特效路径//
    private const string EFFECT_FIRE_HERO = "effect_GUI_HeroUpgradeEffect_xuanzhong_1";
    private const string EFFECT_WATER_HERO = "effect_GUI_HeroUpgradeEffect_xuanzhong_2";
    private const string EFFECT_WOOD_HERO = "effect_GUI_HeroUpgradeEffect_xuanzhong_3";
    private const string EFFECT_THUNDER_HERO = "effect_GUI_HeroUpgradeEffect_xuanzhong_4";
    private const string EFFECT_BRIGHT_HERO = "effect_GUI_HeroUpgradeEffect_xuanzhong_5";
    private const string EFFECT_DARK_HERO = "effect_GUI_HeroUpgradeEffect_xuanzhong_6";

    private const string EFFECT_FIRE_SELF = "effect_GUI_HeroUpgradeEffect_1";
    private const string EFFECT_WATER_SELF = "effect_GUI_HeroUpgradeEffect_2";
    private const string EFFECT_WOOD_SELF = "effect_GUI_HeroUpgradeEffect_3";
    private const string EFFECT_THUNDER_SELF = "effect_GUI_HeroUpgradeEffect_4";
    private const string EFFECT_BRIGHT_SELF = "effect_GUI_HeroUpgradeEffect_5";
    private const string EFFECT_DARK_SELF = "effect_GUI_HeroUpgradeEffect_6";

    private const string EFFECT_SELF = "effect_GUI_HeroJinhuaEffect_mofa_self";

    private const string EFFECT_STAR_2 = "effect_GUI_mengjiang";               //2星及以下特效
    private const string EFFECT_STAR_3 = "effect_GUI_chaomengjiang";        //3星特效
    private const string EFFECT_STAR_4 = "effect_GUI_chaojuemengjiang";    //4星特效
    private const string EFFECT_STAR_5 = "effect_GUI_jueshiwushuang";       //5星特效
    private const string EFFECT_GUI_STAR_2 = "GUI_mengjiang";               //2星及以下特效
    private const string EFFECT_GUI_STAR_3 = "GUI_chaomengjiang";        //3星特效
    private const string EFFECT_GUI_STAR_4 = "GUI_chaojuemengjiang";    //4星特效
    private const string EFFECT_GUI_STAR_5 = "GUI_jueshiwushuang";       //5星特效

    private const string HERO_BACKGROUND = "BACK_Base";  //3D模型主面板背景
    private const string HERO_BACKGROUND_BACK = "BACK_Back";


    private GameObject m_cText;
    private GameObject m_cHeroJinHua;

    private GameObject m_cBack;
    private GameObject m_cBase;

    private GameObject m_cHeroPlane;
    private GameObject m_cHeroResult;
    private GameObject m_cHeroBefore;
    private GameObject m_cHeroEndPos;
    private GameObject m_cHeroAfter;
    private GameObject m_cHeroAfterPos; //成功出来的英雄位置//

    private GameObject m_cEffectStar2; //2星及以下特效
    private GameObject m_cEffectStar3; //3星特效
    private GameObject m_cEffectStar4; //4星特效
    private GameObject m_cEffectStar5; //5星特效

    private GameObject m_cEffectStarStr2; //2星及以下字特效
    private GameObject m_cEffectStarStr3; //3星字特效
    private GameObject m_cEffectStarStr4; //4星字特效
    private GameObject m_cEffectStarStr5; //5星字特效

    private List<GameObject> m_lstEffectObj;    //其他人身上特效//
    private GameObject m_cEffectSelf;

    private GameObject m_cBtnCollider;  //面板碰撞//

    private GfxObject m_gfxSelf;    //英雄实例
    private List<GfxObject> m_gfxObjHeros;  //进化素材实例//

    private float m_fStateStartTime;    //状态开始时间
    private bool m_bHasKeep;
    private const float m_fStar2 = 1.5f;
    private const float m_fStar3 = 1.5f;
    private const float m_fStar4 = 2.8f;
    private const float m_fStar5 = 3;
    private bool m_bSoundPlayed = false;

    public int m_selectID; //选择进化的英雄ID
    public int m_selectTableID; //旧英雄tableID

    /// <summary>
    /// 升级动画过程枚举
    /// </summary>
    private enum AniState
    {
        Effect = 0,
        Succeed = 1,
        Show = 2
    }

    private AniState m_aniState;

    enum LOAD_STATE
    {
        START = 0,  //开始
        LOAD = 1,   //加载过程
        END = 2,  //加载结束
        OUT = 3     //不再加载
    }

    private LOAD_STATE m_eState;    //状态

    public GUIHeroEvolutionResult(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_EVOLUTIONRESULT, GUILAYER.GUI_PANEL)
    {
    }

    public void SetHeroSelectId(int selectID, int oldId)
    {
        m_selectID = selectID;
        m_selectTableID = oldId;
    }

    public override void Show()
    {

        this.m_eLoadingState = LOADING_STATE.START;
        this.m_eState = LOAD_STATE.START;
        this.m_aniState = AniState.Effect;

        GUI_FUNCTION.AYSNCLOADING_SHOW();

		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_MAIN);
        Hero hero = Role.role.GetHeroProperty().GetHero(m_selectID);
        HeroTable herotable = HeroTableManager.GetInstance().GetHeroTable(m_selectTableID);

		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_HERO_JINHUA);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_MODEL_PATH + hero.m_strModel);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_MODEL_PATH + herotable.Modle);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + GetEffectSelf(herotable));

		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + EFFECT_STAR_2);  //加载特效
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + EFFECT_STAR_3);  //加载特效
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + EFFECT_STAR_4);  //加载特效
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + EFFECT_STAR_5);  //加载特效

        for (int i = 0; i < herotable.VecEvolveMat.Length; i++)
        {
            if (herotable.VecEvolveMat[i] != 0)
            {

                HeroTable evolutionTable = HeroTableManager.GetInstance().GetHeroTable(herotable.VecEvolveMat[i]);

				ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_MODEL_PATH + evolutionTable.Modle);
				ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + GetEffectHero(evolutionTable));
            }
        }

		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + EFFECT_SELF);
    }

    public override void Hiden()
    {
        ResourceMgr.UnloadUnusedResources();

        m_gfxSelf.Destory();

        foreach (GfxObject gfx in m_gfxObjHeros)
        {
            gfx.Destory();
        }

        foreach (GameObject go in m_lstEffectObj)
        {
            GameObject.Destroy(go);
        }
        m_lstEffectObj.Clear();

        GameObject.DestroyImmediate(m_cEffectStar2);
        GameObject.DestroyImmediate(m_cEffectStar3);
        GameObject.DestroyImmediate(m_cEffectStar4);
        GameObject.DestroyImmediate(m_cEffectStar5);

        GameObject.DestroyImmediate(m_cEffectSelf);

        base.Hiden();

        CameraManager.GetInstance().HidenUIHeroEvolutionCamera();

        Destory();
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Show();
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
        m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
        //SetLocalPos(Vector3.one * 0xFFFF);
        
    }

    public override void Destory()
    {
        m_cBase = null;
        m_cBack = null;

        if (m_cHeroJinHua != null)
        {
            GameObject.DestroyImmediate(m_cHeroJinHua);
        }
        m_cHeroJinHua = null;


        m_cText = null;
        m_cHeroPlane = null;
        m_cHeroResult = null;
        m_cHeroBefore = null;
        m_cHeroEndPos = null;
        m_cHeroAfter = null;
        m_cHeroAfterPos = null; //成功出来的英雄位置//

        m_cEffectStar2 = null; //2星及以下特效
        m_cEffectStar3 = null; //3星特效
        m_cEffectStar4 = null; //4星特效
        m_cEffectStar5 = null; //5星特效

        m_cEffectStarStr2 = null; //2星及以下字特效
        m_cEffectStarStr3 = null; //3星字特效
        m_cEffectStarStr4 = null; //4星字特效
        m_cEffectStarStr5 = null; //5星字特效

        m_cEffectSelf = null;
        m_cBtnCollider = null;



        base.Hiden();
        base.Destory();
    }

    private void OnDetail(GUI_INPUT_INFO info, object[] arg)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
			MediaMgr.sInstance.StopENV();
//            MediaMgr.StopSoundContinue();
            Hiden();

            GUIHeroDetail herodetail = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERODETAIL) as GUIHeroDetail;

            GUIHeroEvolutionMain guiEvolution = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_EVOLUTION) as GUIHeroEvolutionMain;

            Hero hero = Role.role.GetHeroProperty().GetHero(m_selectID);
            if (hero != null)
            {
                herodetail.Show(guiEvolution.Show, hero);
            }
        }
    }

    /// <summary>
    /// 用于特效路径返回
    /// </summary>
    /// <param name="hero"></param>
    /// <returns></returns>
    public string GetEffectSelf(HeroTable hero)
    {
        Nature natu = (Nature)hero.Property;
        switch (natu)
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

    public string GetEffectHero(HeroTable hero)
    {
        Nature natu = (Nature)hero.Property;
        switch (natu)
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

    protected override void InitGUI()
    {
        base.Show();

        GUI_FUNCTION.AYSNCLOADING_HIDEN();

        CameraManager.GetInstance().ShowUIHeroEvolutionCamera();
        if (m_cGUIObject == null)
        {
            m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_MAIN)) as GameObject;
            m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            m_cGUIObject.transform.localScale = Vector3.one;

            m_cText = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, RES_TEXT);

            m_cBtnCollider = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, BTN_COLLIDER);
            GUIComponentEvent ceCollider = m_cBtnCollider.AddComponent<GUIComponentEvent>();
            ceCollider.AddIntputDelegate(OnDetail);
            m_cBtnCollider.SetActive(false);

            m_cHeroPlane = GUI_FINDATION.FIND_GAME_OBJECT(HERO_EVOLUTION);
            m_cHeroJinHua = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_HERO_JINHUA)) as GameObject;
            m_cHeroJinHua.transform.parent = m_cHeroPlane.transform;
            m_cHeroJinHua.transform.localPosition = Vector3.zero;
            m_cHeroJinHua.transform.localScale = Vector3.one;
            m_cHeroResult = GUI_FINDATION.GET_GAME_OBJECT(m_cHeroJinHua, HERO_RESULT);
            m_cHeroBefore = GUI_FINDATION.GET_GAME_OBJECT(m_cHeroJinHua, HERO_BEFORE);
            m_cHeroEndPos = GUI_FINDATION.GET_GAME_OBJECT(m_cHeroResult, HERO_ENDPOS);
            m_cHeroAfter = GUI_FINDATION.GET_GAME_OBJECT(m_cHeroJinHua, HERO_AFTER);
            m_cHeroAfterPos = GUI_FINDATION.GET_GAME_OBJECT(m_cHeroJinHua, HERO_AFTERPOS);

            m_cBack = GUI_FINDATION.GET_GAME_OBJECT(m_cHeroJinHua, HERO_BACKGROUND_BACK);
            m_cBase = GUI_FINDATION.GET_GAME_OBJECT(m_cHeroJinHua, HERO_BACKGROUND);

            m_gfxObjHeros = new List<GfxObject>();
            m_lstEffectObj = new List<GameObject>();
        }

        m_cBase.SetActive(false);
        m_cBack.SetActive(true);

        m_cHeroBefore.SetActive(false);
        m_cHeroResult.SetActive(true);
        m_cHeroAfter.SetActive(true);

        m_cText.SetActive(false);

        if (m_gfxSelf != null)
        {
            m_gfxSelf.Destory();
        }

        HeroTable herotable = HeroTableManager.GetInstance().GetHeroTable(m_selectTableID);

        GameObject objSelf = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(herotable.Modle)) as GameObject;
        objSelf.transform.parent = m_cHeroEndPos.transform;
        objSelf.transform.localPosition = Vector3.zero;
        objSelf.transform.localScale = Vector3.one * 1.5f;
        objSelf.name = herotable.Modle;
        m_gfxSelf = new GfxObject(objSelf);

        foreach (GfxObject gfxObj in m_gfxObjHeros)
        {
            gfxObj.Destory();
        }

        m_gfxObjHeros.Clear();

        for (int i = 0; i < herotable.VecEvolveMat.Length; i++)
        {
            if (herotable.VecEvolveMat[i] != 0)
            {
                GameObject objPos = GUI_FINDATION.GET_GAME_OBJECT(m_cHeroResult, HERO_TEAM_POS + (i + 1));
                //Debug.Log("Hero_" + hero.m_vecEvolution[i]);

                HeroTable evolutionTable = HeroTableManager.GetInstance().GetHeroTable(herotable.VecEvolveMat[i]);
                //Hero evolutionHero = Role.role.GetHeroProperty().GetHero(hero.m_vecEvolution[i]);

                GameObject objHero = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(evolutionTable.Modle)) as GameObject;
                GameObject objEffect = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(GetEffectHero(herotable))) as GameObject;
                objHero.transform.parent = objPos.transform;
                objHero.transform.localPosition = Vector3.zero;
                objHero.transform.localScale = Vector3.one;
                objHero.name = evolutionTable.Modle;
                m_gfxObjHeros.Add(new GfxObject(objHero));

                objEffect.transform.parent = objPos.transform;
                objEffect.transform.localPosition = Vector3.zero;
                objEffect.transform.localScale = Vector3.one;
                m_lstEffectObj.Add(objEffect);
            }
        }

        GameObject objEffectSelf = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(GetEffectSelf(herotable))) as GameObject;
        objEffectSelf.transform.parent = m_cHeroResult.transform;
        objEffectSelf.transform.localPosition = Vector3.zero;
        objEffectSelf.transform.localScale = Vector3.one;
        m_lstEffectObj.Add(objEffectSelf);

        m_fStateStartTime = Time.time;
        m_bSoundPlayed = false;

        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Hiden();
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Hiden();
        m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Hiden();
        this.m_cGUIMgr.SetCurGUIID(this.ID);
        SetLocalPos(Vector3.zero);

    }

    //update 刷新
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

        if (IsShow())
        {

            switch (this.m_eState)
            {
                case LOAD_STATE.START:
                    this.m_eState++;
                    break;
                case LOAD_STATE.LOAD:
                        this.m_eState++;
                    break;
                case LOAD_STATE.END:
                    this.m_eState++;
                    break;
                case LOAD_STATE.OUT:
                    switch (this.m_aniState)
                    {
                        case AniState.Effect:

                            if (!m_bSoundPlayed)
                            {
                                //音效
								MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_EVO_HERO);
//                                MediaMgr.PlaySound2(SOUND_DEFINE.SE_EVO_HERO);
                                m_bSoundPlayed = true;
                            }

                            if ((Time.time - m_fStateStartTime) > 1.8f)
                            {
                                foreach (GfxObject gfx in m_gfxObjHeros)
                                {
                                    gfx.Destory();
                                }
                            }
                            //Debug.Log(Time.time);
                            if ((Time.time - m_fStateStartTime) > 5f)
                            {
                                foreach (GameObject go in m_lstEffectObj)
                                {
                                    GameObject.Destroy(go);
                                }
                                m_lstEffectObj.Clear();

                                m_gfxSelf.Destory();

                                m_fStateStartTime = Time.fixedTime;
                                //m_cSucceed = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.Load(GAME_DEFINE.RESOURCE_GUI_PATH, m_strSucced)) as GameObject;
                                //m_cSucceed.transform.parent = m_cHeroMain.transform;
                                //m_cSucceed.transform.localPosition = Vector3.zero;


                                m_cEffectSelf = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(EFFECT_SELF)) as GameObject;
                                m_cEffectSelf.transform.parent = m_cHeroAfterPos.transform;
                                m_cEffectSelf.transform.localPosition = Vector3.zero;
                                m_cEffectSelf.transform.localScale = Vector3.one;

                                Hero hero2 = Role.role.GetHeroProperty().GetHero(m_selectID);
                                GameObject objSelf2 = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(hero2.m_strModel)) as GameObject;
                                objSelf2.transform.parent = m_cHeroAfterPos.transform;
                                objSelf2.transform.localPosition = Vector3.zero;
                                objSelf2.transform.localScale = Vector3.one * 2f;
                                objSelf2.name = hero2.m_strModel;
                                m_gfxSelf = new GfxObject(objSelf2);


                                ShowMenJiangEffect(hero2);


                                m_cText.GetComponent<UILabel>().text = HeroTableManager.GetInstance().GetHeroTable(hero2.m_iTableID).Word;
                                m_cText.SetActive(true);
                                
                                //CTween.TweenPosition(m_cTop, 2f + m_fSuccedTime, 1f, new Vector3(-640f, 0, 0), Vector3.zero, TWEEN_LINE_TYPE.ElasticInOut, SetState);
                                m_aniState++;
                            }
                            break;
                        case AniState.Succeed:
                            Hero hero3 = Role.role.GetHeroProperty().GetHero(m_selectID);
                            if ((Time.time - m_fStateStartTime) > GetEffectTime(hero3))
                            {
                                SetEffectKeep(hero3);
                                m_aniState++;
                                m_cBtnCollider.SetActive(true);

                            }
                            break;
                        case AniState.Show:
                            m_aniState++;
                            break;
                    }
                    break;
            }
        }

        return true;
    }

    private float GetEffectTime(Hero hero)
    {
        float re=2;
        switch (hero.m_iStarLevel)
        {
            case 1:
            case 2:
                //1星2星显示猛将特效
                re = m_fStar2;
                break;
            case 3:
                //3星显示超猛将特效
                re = m_fStar3;
                break;
            case 4:
                //4星显示超绝猛将特效
                re = m_fStar4;
                break;
            case 5:
                //5星显示无双猛将特效
                re = m_fStar5;
                break;
            default:
                break;
        }

        return re;
    }

    private void ShowMenJiangEffect(Hero hero)
    {
        //将异步内存中的星级特效加载出来
        switch (hero.m_iStarLevel)
        {
            case 1:
            case 2:
                //1星2星显示猛将特效
                this.m_cEffectStar2 = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(EFFECT_STAR_2)) as GameObject;
                this.m_cEffectStar2.transform.parent = this.m_cHeroPlane.transform;
                this.m_cEffectStar2.transform.localPosition = Vector3.zero;
                this.m_cEffectStar2.transform.localScale = Vector3.one;
                this.m_cEffectStar2.SetActive(true);
                this.m_cEffectStarStr2 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cEffectStar2, EFFECT_GUI_STAR_2);
                Animation[] m_vecAnim2 = this.m_cEffectStarStr2.GetComponentsInChildren<Animation>();
                foreach (Animation item in m_vecAnim2)
                {
                    item.wrapMode = WrapMode.Once;
                    item.Play("start");
                }

                //音效
				MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_SUMMON_STAR_2);
//                MediaMgr.PlaySound2(SOUND_DEFINE.SE_SUMMON_STAR_2);
                break;
            case 3:
                //3星显示超猛将特效
                this.m_cEffectStar3 = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(EFFECT_STAR_3)) as GameObject;
                this.m_cEffectStar3.transform.parent = this.m_cHeroPlane.transform;
                this.m_cEffectStar3.transform.localPosition = Vector3.zero;
                this.m_cEffectStar3.transform.localScale = Vector3.one;
                this.m_cEffectStar3.SetActive(true);
                this.m_cEffectStarStr3 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cEffectStar3, EFFECT_GUI_STAR_3);
                Animation[] m_vecAnim3 = this.m_cEffectStarStr3.GetComponentsInChildren<Animation>();
                foreach (Animation item in m_vecAnim3)
                {
                    item.wrapMode = WrapMode.Once;
                    item.Play("start");
                }
                //音效
				MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_SUMMON_STAR_3);
//                MediaMgr.PlaySound2(SOUND_DEFINE.SE_SUMMON_STAR_3);
                break;
            case 4:
                //4星显示超绝猛将特效
                this.m_cEffectStar4 = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(EFFECT_STAR_4)) as GameObject;
                this.m_cEffectStar4.transform.parent = this.m_cHeroPlane.transform;
                this.m_cEffectStar4.transform.localPosition = Vector3.zero;
                this.m_cEffectStar4.transform.localScale = Vector3.one;
                this.m_cEffectStar4.SetActive(true);
                this.m_cEffectStarStr4 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cEffectStar4, EFFECT_GUI_STAR_4);
                Animation[] m_vecAnim4 = this.m_cEffectStarStr4.GetComponentsInChildren<Animation>();
                foreach (Animation item in m_vecAnim4)
                {
                    item.wrapMode = WrapMode.Once;
                    item.Play("start");
                }
                //音效
				MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_SUMMON_STAR_4);
//                MediaMgr.PlaySound2(SOUND_DEFINE.SE_SUMMON_STAR_4);
                break;
            case 5:
                //5星显示无双猛将特效
                this.m_cEffectStar5 = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(EFFECT_STAR_5)) as GameObject;
                this.m_cEffectStar5.transform.parent = this.m_cHeroPlane.transform;
                this.m_cEffectStar5.transform.localPosition = Vector3.zero;
                this.m_cEffectStar5.transform.localScale = Vector3.one;
                this.m_cEffectStar5.SetActive(true);
                this.m_cEffectStarStr5 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cEffectStar5, EFFECT_GUI_STAR_5);
                Animation[] m_vecAnim5 = this.m_cEffectStarStr5.GetComponentsInChildren<Animation>();
                foreach (Animation item in m_vecAnim5)
                {
                    item.wrapMode = WrapMode.Once;
                    item.Play("start");
                }

                //音效
				MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_SUMMON_STAR_5);
//                MediaMgr.PlaySound2(SOUND_DEFINE.SE_SUMMON_STAR_5);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 设置字符Keep状态
    /// </summary>
    private void SetEffectKeep(Hero hero)
    {
        if (!m_bHasKeep)
        {
            switch (hero.m_iStarLevel)
            {
                case 1:
                case 2:
                    //1星2星显示猛将特效
                    Animation[] m_vecAnim2 = this.m_cEffectStarStr2.GetComponentsInChildren<Animation>();
                    foreach (Animation item in m_vecAnim2)
                    {
                        item.wrapMode = WrapMode.Loop;
                        item.Play("keep");
                    }
                    break;
                case 3:
                    //3星显示超猛将特效
                    Animation[] m_vecAnim3 = this.m_cEffectStarStr3.GetComponentsInChildren<Animation>();
                    foreach (Animation item in m_vecAnim3)
                    {
                        item.wrapMode = WrapMode.Loop;
                        item.Play("keep");
                    }
                    break;
                case 4:
                    //4星显示超绝猛将特效
                    Animation[] m_vecAnim4 = this.m_cEffectStarStr4.GetComponentsInChildren<Animation>();
                    foreach (Animation item in m_vecAnim4)
                    {
                        item.wrapMode = WrapMode.Loop;
                        item.Play("keep");
                    }
                    break;
                case 5:
                    //5星显示无双猛将特效
                    Animation[] m_vecAnim5 = this.m_cEffectStarStr5.GetComponentsInChildren<Animation>();
                    foreach (Animation item in m_vecAnim5)
                    {
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
}