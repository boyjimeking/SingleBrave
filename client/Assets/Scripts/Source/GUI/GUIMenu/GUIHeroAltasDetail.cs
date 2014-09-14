//Micro.Sanvey
//2014.1.16
//sanvey.china@gmail.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;
using Game.Gfx;

/// <summary>
/// 英雄介绍详细GUI
/// </summary>
public class GUIHeroAltasDetail : GUIBase
{
    public delegate void CALLBACK();    //回调委托

    private const string RES_MAIN = "GUI_HeroAtlasDetail";  //主资源地址
    private const string BTN_CANCEL = "TopPanel/Button_Back";        //取消按钮地址
    private const string PAN_CANCEL = "TopPanel";        //取消Pan地址
    private const string PAN_RIGHT = "PanInfo";  //滑出Panel地址
    private const string LB_ATTACK = "LB_Attack";  //攻击
    private const string LB_DEFENSE = "LB_Defense";  //防御
    private const string LB_HP = "LB_HP";  //HP
    private const string LB_REVERT = "LB_Revert";  //回复
    private const string SP_NATURE = "SP_Nature";  //英雄属性
    private const string LB_NAME = "LB_Name";  //英雄名称
    private const string LB_INFO = "Lb_Info";  //英雄介绍详细

    private const string HERO_SHOW_ROOT = "HERO_SHOW";   //展示根节点
    private const string HERO_STAY_POS = "BBPOS";    //站立位置

    //图鉴场景
    private GameObject m_cSceneRoot;    //场景根节点
    private GameObject m_cScene;    //场景
    private GameObject m_cStayPos;  //站立点
    private GfxObject m_cGfxHero;   //渲染实例
    private AIControl m_cAI;    //AI

    private GameObject m_cPanSlide;   //panel滑动
    private GameObject m_cPanCancel;  //取消按钮Panel
    private UILabel m_cLbHP;
    private UILabel m_cLbAttack;
    private UILabel m_cLbDefense;
    private UILabel m_cLbRevert;
    private UISprite m_cSpNature;
    private UILabel m_cName;
    private UILabel m_cInfo;

    private CALLBACK m_delCallBack; //回调方法

    public int m_iHeroTableId;  //英雄tableID

    private bool m_bHasShow = false;  //加载过showobject

    public GUIHeroAltasDetail(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_HERO_ALTAS_DETAIL, GUILAYER.GUI_PANEL)
    { }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        this.m_eLoadingState = LOADING_STATE.START;
        GUI_FUNCTION.AYSNCLOADING_SHOW();
        HeroTable heroTable = HeroTableManager.GetInstance().GetHeroTable(this.m_iHeroTableId);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_MODEL_PATH + heroTable.Modle);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + "BATTLE_Scene9");
        if (this.m_cGUIObject == null)
        {
			ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_MAIN);

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
            //Main主资源
            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;
            //滑出动画panel
            this.m_cPanSlide = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_RIGHT);
            //取消按钮
            var cancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_CANCEL);
            GUIComponentEvent gui_event = cancel.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnCancel_OnEvent);
            this.m_cPanCancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_CANCEL);

            this.m_cInfo = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cPanSlide, LB_INFO);
            this.m_cLbAttack = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cPanSlide, LB_ATTACK);
            this.m_cLbDefense = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cPanSlide, LB_DEFENSE);
            this.m_cLbHP = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cPanSlide, LB_HP);
            this.m_cLbRevert = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cPanSlide, LB_REVERT);
            this.m_cName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cPanSlide, LB_NAME);
            this.m_cSpNature = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cPanSlide, SP_NATURE);

            this.m_cSceneRoot = GUI_FINDATION.FIND_GAME_OBJECT(HERO_SHOW_ROOT);
            this.m_cStayPos = GUI_FINDATION.GET_GAME_OBJECT(this.m_cSceneRoot, HERO_STAY_POS);
        }

        //设置英雄显示
        UpdateHeroInfo();

        //图鉴场景
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Hiden();
        CameraManager.GetInstance().ShowUIHeroShowCamera();
        this.m_cScene = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset("BATTLE_Scene9")) as GameObject;
        this.m_cScene.transform.parent = this.m_cSceneRoot.transform;
        this.m_cScene.transform.localScale = Vector3.one;
        this.m_cScene.transform.localPosition = Vector3.zero;
        if (!GAME_SETTING.s_bENEffectSwitch)
        {
            GameObject ef = GUI_FINDATION.GET_GAME_OBJECT(this.m_cScene, GUI_DEFINE.SCENE_EFFECT_OBJECT);
            ef.SetActive(false);
        }
        HeroTable heroTable = HeroTableManager.GetInstance().GetHeroTable(this.m_iHeroTableId);
        GameObject heroObj = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(heroTable.Modle)) as GameObject;
        heroObj.transform.parent = this.m_cStayPos.transform;
        heroObj.transform.localScale = Vector3.one * 0.75f;
        heroObj.transform.localPosition = Vector3.zero;
        this.m_cGfxHero = new GfxObject(heroObj);
        this.m_cAI = new NormalAIControl(this.m_cGfxHero);

        this.m_cGUIMgr.SetCurGUIID(this.ID);
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Hiden();

        SetLocalPos(Vector3.zero);
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        if (this.m_cGfxHero != null)
            this.m_cGfxHero.Destory();
        this.m_cGfxHero = null;
        if (this.m_cScene != null)
            GameObject.DestroyImmediate(this.m_cScene);
        this.m_cScene = null;

        this.m_cSceneRoot = null;
        this.m_cStayPos = null;
        this.m_cAI = null;

        this.m_cPanSlide = null;
        this.m_cPanCancel = null;
        this.m_cLbHP = null;
        this.m_cLbAttack = null;
        this.m_cLbDefense = null;
        this.m_cLbRevert = null;
        this.m_cSpNature = null;
        this.m_cName = null;
        this.m_cInfo = null;

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

        if (this.m_cGfxHero != null)
            this.m_cGfxHero.Update();

        if (this.m_cAI != null)
            this.m_cAI.Update();

        return true;
    }

   /// <summary>
    /// 设置英雄显示
   /// </summary>
    private void UpdateHeroInfo()
    {
        HeroTable table = HeroTableManager.GetInstance().GetHeroTable(m_iHeroTableId);
        List<int> prop = HeroGrowTableManager.GetInstance().GetHeroByType(table.ID, GrowType.Balance);

        if (table != null)
        {
            this.m_cInfo.text = table.Desc;
            this.m_cLbHP.text = prop[0].ToString();
            this.m_cLbAttack.text = prop[1].ToString();
            this.m_cLbDefense.text = prop[2].ToString();
            this.m_cLbRevert.text = prop[3].ToString();
            this.m_cName.text = table.Name;
            GUI_FUNCTION.SET_NATUREM(this.m_cSpNature, (Nature)table.Property);

        }
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        base.Hiden();

        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
        CameraManager.GetInstance().HidenUIHeroShowCamera();
        if (this.m_cGfxHero != null)
            this.m_cGfxHero.Destory();
        this.m_cGfxHero = null;
        if (this.m_cScene != null)
            GameObject.DestroyImmediate(this.m_cScene);
        this.m_cScene = null;

        //CTween.TweenPosition(this.m_cPanSlide, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0));
        //CTween.TweenPosition(this.m_cPanCancel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-420, 270, 0));
        //CTween.TweenAlpha(this.m_cGUIObject, 0, GAME_DEFINE.FADEOUT_GUI_TIME, 1f, 0f);
        //SetLocalPos(Vector3.one*0xFFFFF);
        Destory();
    }

    /// <summary>
    /// 返回按钮事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void BtnCancel_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();
            if (this.m_delCallBack != null)
                this.m_delCallBack();
       
        }
    }

    /// <summary>
    /// 展示
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void Show(CALLBACK cal)
    {
        this.m_delCallBack = cal;
        this.Show();
    }
}