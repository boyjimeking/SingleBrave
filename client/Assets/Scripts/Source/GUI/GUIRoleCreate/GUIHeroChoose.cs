using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using Game.Resource;
using Game.Gfx;
using Game.Media;

//角色选择
//Author:Sunyi
//2014-2-14
public class GUIHeroChoose : GUIBase
{

    private const string RES_MAIN = "GUI_HeroChoose";//主资源地址
    private const string RES_HERO_CREATE = "effect_GUI_Hero_Create";//创建角色选择英雄资源地址
    private const string BUTTON_OK = "Btn_Ok";//确定按钮地址
    private const string HERO_CHOOSE = "HERO_CHOOSE";//展示根节点
    private const string HERO_POS = "HeroPos";//3D模型站立位置
    private const string HERO_BACKGROUND = "GUI_Hero_Create_head";//3D模型背景图地址
    private const string LEFT_ARROW_PARENT = "Left";//左边箭头父对象
    private const string RIGHT_ARROW_PARENT = "Right";//右边箭头父对象
    private const string SPRITE_ARROW_LEFT = "Left/Spr_Left";//左边箭头地址
    private const string SPRITE_ARROW_RIGHT = "Right/Spr_Right";//右边箭头地址
    private const string DRAGPANEL = "DragPanel";//滑动面板地址
    private const string LABEL_NAME = "Lab_Name";//英雄名字标签地址
    private const string LABEL_DESC = "Lab_Desc";//英雄描述标签地址

    private GameObject m_cHeroCreate;//英雄选择
    private GameObject m_cBtnOk;//确定按钮
    private GameObject m_cSceneRoot;//展示根节点
    private GameObject m_cStayPos;//英雄位置
    private GfxObject m_cGfxHero;   //渲染实例
    private GameObject m_cLeftParent;//左边箭头父对象
    private GameObject m_cRightParent;//右边箭头父对象
    private UISprite m_cSprArrowLeft;//左边箭头
    private UISprite m_cSprArrowRight;//右边箭头
    private TDAnimation m_cArrowLeftAnimation;//左边箭头动画
    private TDAnimation m_cArrowRightAnimation;//左边箭头动画
    private GameObject m_cDragPanel;//滑动面板
    private UILabel m_cLabName;//英雄名字标签
    private UILabel m_cLabDesc;//英雄描述标签
    private GameObject m_cModelBGMaterial;//3D背景材质球

    private bool m_bIsDrag = false;//是否滑动
    private bool m_bIsRight = false;//向左或向右滑动
    private bool m_bHasShow;  //加载过showobject
    private int m_iSelectHeroIndex;//选择的英雄索引

    private List<GameObject> m_lstModels = new List<GameObject>();


    public GUIHeroChoose(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_HERO_CHOOSE, GUILAYER.GUI_PANEL)
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
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_HERO_CREATE);
            for (int i = 0; i < GAME_DEFINE.m_vecSelectHero.Length; i++)
            {
                HeroTable heroTable = HeroTableManager.GetInstance().GetHeroTable(GAME_DEFINE.m_vecSelectHero[i]);
                ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_TEX_PATH + heroTable.AvatorARes);
                ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_MODEL_PATH + heroTable.Modle);
            }
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
        CameraManager.GetInstance().ShowUIHeroChooseCamera();
        //隐藏GUI背景
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Hiden();
        if (this.m_cGUIObject == null)
        {
            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            this.m_cDragPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, DRAGPANEL);
            GUIComponentEvent dragEvent = this.m_cDragPanel.AddComponent<GUIComponentEvent>();
            dragEvent.AddIntputDelegate(DragPanelEvent);

            this.m_cBtnOk = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_OK);
            GUIComponentEvent okEvent = this.m_cBtnOk.AddComponent<GUIComponentEvent>();
            okEvent.AddIntputDelegate(OnClickOkButton);

            this.m_cSceneRoot = GUI_FINDATION.FIND_GAME_OBJECT(HERO_CHOOSE);
            this.m_cStayPos = GUI_FINDATION.GET_GAME_OBJECT(this.m_cSceneRoot, HERO_POS);

            this.m_cHeroCreate = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_HERO_CREATE)) as GameObject;
            this.m_cHeroCreate.transform.parent = this.m_cSceneRoot.transform;
            this.m_cHeroCreate.transform.localPosition = Vector3.zero;
            this.m_cHeroCreate.transform.localScale = Vector3.one;

            this.m_cSprArrowLeft = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SPRITE_ARROW_LEFT);
            this.m_cSprArrowRight = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SPRITE_ARROW_RIGHT);

            this.m_cArrowLeftAnimation = new TDAnimation(this.m_cSprArrowLeft.atlas, this.m_cSprArrowLeft);
            this.m_cArrowRightAnimation = new TDAnimation(this.m_cSprArrowRight.atlas, this.m_cSprArrowRight);

            this.m_cLabName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LABEL_NAME);
            this.m_cLabDesc = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LABEL_DESC);

            this.m_cModelBGMaterial = GUI_FINDATION.GET_GAME_OBJECT(this.m_cHeroCreate, HERO_BACKGROUND);

            this.m_cLeftParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LEFT_ARROW_PARENT);
            GUIComponentEvent leftEvent = this.m_cLeftParent.AddComponent<GUIComponentEvent>();
            leftEvent.AddIntputDelegate(OnClickLeftArrow);

            this.m_cRightParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, RIGHT_ARROW_PARENT);
            GUIComponentEvent rightEvent = this.m_cRightParent.AddComponent<GUIComponentEvent>();
            rightEvent.AddIntputDelegate(OnClickRightArrow);
        }

        this.m_cArrowLeftAnimation.Play("ArrowLeft", Game.Base.TDAnimationMode.Loop, 0.4F);
        this.m_cArrowRightAnimation.Play("ArrowRight", Game.Base.TDAnimationMode.Loop, 0.4F);

        for (int i = 0; i < GAME_DEFINE.m_vecSelectHero.Length; i++)
        {
            HeroTable heroTable = HeroTableManager.GetInstance().GetHeroTable(GAME_DEFINE.m_vecSelectHero[i]);

            //将内存中的模型加载出来
            GameObject heroObj = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(heroTable.Modle)) as GameObject;
            heroObj.transform.parent = this.m_cStayPos.transform;
            heroObj.transform.localScale = Vector3.one;
            heroObj.transform.localPosition = new Vector3(-4 + 4 * i, 0, 0);
            this.m_cGfxHero = new GfxObject(heroObj);
            this.m_lstModels.Add(heroObj);
        }

        this.m_iSelectHeroIndex = 1;

        HeroTable curHeroTable = HeroTableManager.GetInstance().GetHeroTable(GAME_DEFINE.m_vecSelectHero[this.m_iSelectHeroIndex]);
        this.m_cLabName.text = curHeroTable.Name;
        switch (curHeroTable.Property)
        {
            case 1:
                this.m_cLabDesc.text = "我的#ff0000]炎火#ffffff]之力必将助我守护一切事物";
                break;
            case 2:
                this.m_cLabDesc.text = "我的#0090ff]冰水#ffffff]之力在这乱世之中必将大有可为";
                break;
            case 3:
                this.m_cLabDesc.text = "我的#7fff00]树木#ffffff]之力是为了拯救苍生而存在的";
                break;
            case 4:
                this.m_cLabDesc.text = "我的#fffe400]奔雷#ffffff]之力可助你将世界揽入手中";
                break;
            case 5:
                this.m_cLabDesc.text = "我的#0090ff]冰水#ffffff]之力在这乱世之中必将大有可为";
                break;
            case 6:
                this.m_cLabDesc.text = "我的#0090ff]冰水#ffffff]之力在这乱世之中必将大有可为";
                break;
            default:
                break;
        }

        Texture heroBg = (Texture)ResourceMgr.LoadAsset(curHeroTable.AvatorARes);

        this.m_cModelBGMaterial.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", heroBg);

        SetLocalPos(Vector3.zero);
    }



    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        base.Hiden();

        CameraManager.GetInstance().HidenUIHeroChooseCamera();
        ResourceMgr.UnloadUnusedResources();

        //SetLocalPos(Vector3.one * 0XFFFF);
        Destory();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        if (this.m_lstModels != null)
        {
            foreach (GameObject obj in this.m_lstModels)
            {
                GameObject.Destroy(obj);
            }
        }

        if (this.m_cGfxHero != null)
        {
            this.m_cGfxHero.Destory();
            this.m_cGfxHero = null;
        }

        this.m_cHeroCreate = null;
        this.m_cBtnOk = null;
        this.m_cSceneRoot = null;
        this.m_cStayPos = null;
        this.m_cGfxHero = null;
        this.m_cLeftParent = null;
        this.m_cRightParent = null;
        this.m_cSprArrowLeft = null;
        this.m_cSprArrowRight = null;
        this.m_cArrowLeftAnimation = null;
        this.m_cArrowRightAnimation = null;
        this.m_cDragPanel = null;
        this.m_cLabName = null;
        this.m_cLabDesc = null;
        if (this.m_cModelBGMaterial != null)
        {
            GameObject.Destroy(this.m_cModelBGMaterial);
            this.m_cModelBGMaterial = null;
        }
        
        this.m_lstModels.Clear();

        base.Destory();
    }

    /// <summary>
    /// 是否正在展示
    /// </summary>
    /// <returns></returns>
    public virtual bool IsShow()
    {
        return this.m_bShow;
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

        if (!IsShow())
            return false;
        if (this.m_cArrowLeftAnimation != null)
        {
            this.m_cArrowLeftAnimation.Update();
        }
        if (this.m_cArrowRightAnimation != null)
        {
            this.m_cArrowRightAnimation.Update();
        }

        return true;

    }
    /// <summary>
    /// 确定按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickOkButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            //显示GUI背景
            this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();

            GUIRoleCreate gui = (GUIRoleCreate)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_ROLE_CREATE);
            gui.SetSelectHeroIndex(this.m_iSelectHeroIndex);
            gui.Show();
        }
    }

    /// <summary>
    /// 滑动面板事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void DragPanelEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.DRAG)
        {
            this.m_bIsDrag = true;
            if (info.m_vecDelta.x > 2)
            {
                m_bIsRight = true;
            }
            else if (info.m_vecDelta.x < -2)
            {
                m_bIsRight = false;
            }
        }
        else if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.PRESS)
        {
            if (this.m_bIsDrag)
            {
                //翻页音效
				MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_SLIDE);
//                MediaMgr.PlaySound(SOUND_DEFINE.SE_SLIDE);

                if (m_bIsRight)
                {
                    int nextIndex = 0;
                    if (this.m_iSelectHeroIndex == GAME_DEFINE.m_vecSelectHero.Length - 1)
                    {
                        nextIndex = 0;
                    }
                    else {
                        nextIndex = this.m_iSelectHeroIndex + 1;
                    }
                    RightDragPanel(nextIndex);
                    this.m_bIsDrag = false;
                    Debug.Log("right");
                }
                else
                {
                    int nextIndex = 0;
                    if (this.m_iSelectHeroIndex == 0)
                    {
                        nextIndex = GAME_DEFINE.m_vecSelectHero.Length - 1;
                    }
                    else
                    {
                        nextIndex = this.m_iSelectHeroIndex - 1;
                    }
                    LeftDragPanel(nextIndex);
                    this.m_bIsDrag = false;
                }
            }
        }
    }

    /// <summary>
    /// 点击左边箭头事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickLeftArrow(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            int nextIndex = 0;
            if (this.m_iSelectHeroIndex == 0)
            {
                nextIndex = GAME_DEFINE.m_vecSelectHero.Length - 1;
            }
            else
            {
                nextIndex = this.m_iSelectHeroIndex - 1;
            }
            LeftDragPanel(nextIndex);
            this.m_bIsDrag = false;
        }

        DragPanelEvent(info, args);
    }

    /// <summary>
    /// 点击右边箭头事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickRightArrow(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            int nextIndex = 0;
            if (this.m_iSelectHeroIndex == GAME_DEFINE.m_vecSelectHero.Length - 1)
            {
                nextIndex = 0;
            }
            else
            {
                nextIndex = this.m_iSelectHeroIndex + 1;
            }
            RightDragPanel(nextIndex);
            this.m_bIsDrag = false;
        }
        DragPanelEvent(info, args);
    }

    /// <summary>
    /// 右滑
    /// </summary>
    private void RightDragPanel(int nextIndex)
    {
        TweenPosition.Begin(this.m_lstModels[nextIndex], 0.4f, new Vector3(4, 0, 0), new Vector3(0, 0, 0));
        TweenPosition.Begin(this.m_lstModels[this.m_iSelectHeroIndex], 0.4f, new Vector3(0, 0, 0), new Vector3(-4, 0, 0));
        this.m_iSelectHeroIndex = nextIndex;
        HeroTable heroTable = HeroTableManager.GetInstance().GetHeroTable(GAME_DEFINE.m_vecSelectHero[this.m_iSelectHeroIndex]);
        Texture heroBg = (Texture)ResourceMgr.LoadAsset(heroTable.AvatorARes);
        this.m_cModelBGMaterial.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", heroBg);
        ResetNameAndDesc();
    }

    /// <summary>
    /// 左滑
    /// </summary>
    private void LeftDragPanel(int nextIndex)
    {
        TweenPosition.Begin(this.m_lstModels[nextIndex], 0.4f, new Vector3(-4, 0, 0), new Vector3(0, 0, 0));
        TweenPosition.Begin(this.m_lstModels[this.m_iSelectHeroIndex], 0.4f, new Vector3(0, 0, 0), new Vector3(4, 0, 0));
        this.m_iSelectHeroIndex = nextIndex;
        HeroTable heroTable = HeroTableManager.GetInstance().GetHeroTable(GAME_DEFINE.m_vecSelectHero[this.m_iSelectHeroIndex]);
        Texture heroBg = (Texture)ResourceMgr.LoadAsset(heroTable.AvatorARes);
        this.m_cModelBGMaterial.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", heroBg);
        ResetNameAndDesc();
    }

    /// <summary>
    /// 更新当前英雄签名和描述
    /// </summary>
    private void ResetNameAndDesc()
    {
        HeroTable table = HeroTableManager.GetInstance().GetHeroTable(GAME_DEFINE.m_vecSelectHero[this.m_iSelectHeroIndex]);
        this.m_cLabName.text = table.Name;
        switch (table.Property)
        {
            case 1:
                this.m_cLabDesc.text = "我的#ff0000]炎火#ffffff]之力必将助我守护一切事物";
                break;
            case 2:
                this.m_cLabDesc.text = "我的#0090ff]冰水#ffffff]之力在这乱世之中必可大有可为";
                break;
            case 3:
                this.m_cLabDesc.text = "我的#7fff00]树木#ffffff]之力是为了拯救苍生而存在的";
                break;
            case 4:
                this.m_cLabDesc.text = "我的#ffe400]奔雷#ffffff]之力可助你将世界揽入手中";
                break;
            case 5:
                this.m_cLabDesc.text = "我的#0090ff]冰水#ffffff]之力在这乱世之中必可大有可为";
                break;
            case 6:
                this.m_cLabDesc.text = "我的#0090ff]冰水#ffffff]之力在这乱世之中必可大有可为";
                break;
            default:
                break;
        }
    }
}

