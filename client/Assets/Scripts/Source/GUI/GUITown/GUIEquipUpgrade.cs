//Micro.Sanvey
//2013.12.6
//sanvey.china@gmail.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;
using Game.Base;

/// <summary>
/// 设施强化GUI
/// </summary>
public class GUIEquipUpgrade : GUIBase
{
    private const string RES_MAIN = "GUI_TownEquipUpgrade";                   //主资源地址
    private const string BTN_CANCEL = "Title_Cancel/Btn_Cancel";        //取消按钮地址
    private const string PAN_CANCEL = "Title_Cancel";                   //取消Pan地址
    private const string PAN_RIGHT = "PanInfo";                         //滑出Panel地址
    private const string SP_EXPFULL = "PanInfo/Sp_EXPFull";  //升级槽精度条
    private const string BTN_UPGRADE = "PanInfo/BtnUpgrade";  //升级按钮
    private const string LB_NextCount = "PanInfo/Lb_NextCount";  //距离下一级升级经验
    private const string LB_NextInfo = "PanInfo/Lb_NextInfo";  //下一级新的收获介绍
    private const string PAN_Slide = "PanInfo/PanelSlide";  //滑动panel
    private const string RES_TABLE = "PanInfo/PanelSlide/Table";
    private const string RES_ITEM0 = "Item0";
    private const string RES_ITEM1 = "Item1";
    private const string RES_ITEM2 = "Item2";
    private const string RES_BACK = "Back";  //点击无效遮罩

    private const string SPR_ARROWLEFT = "PanInfo/Arrow_left";      //向左滑动特效
    private const string SPR_ARROWRIGHT = "PanInfo/Arrow_right";   //向右滑动特效

    private const string EFFECT_LEVEL_UP = "effect_GUI_BattleRewardLevelUp";  //玩家升级特效
    private const string GUI_EFFECT = "GUI_EFFECT";//3d特效资源地址
    private const string EFFECT_CENTER_ANCHOR = "ANCHOR_CENTER";//3d特效父对象

    private const float LEVEL_UP_SHOW_TIME = 2.5f;

    public class BuildShowItem
    {
        private const string ITEM_Lb = "Label";
        private const string ITEM_Sp = "Sp";

        public UILabel m_cLbLevel;
        public UISprite m_cSpItem;
        public GameObject m_cItem;

        public BuildShowItem(GameObject parent)
        {
            this.m_cItem = parent;
            this.m_cSpItem = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(parent, ITEM_Sp);
            this.m_cLbLevel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(parent, ITEM_Lb);
        }
    }

    List<BuildShowItem> m_lstBuilding = new List<BuildShowItem>();
    List<Building> m_lstBuildItem = new List<Building>();
    private GameObject m_cPanSlide;       //panel滑动
    private GameObject m_cTable;
    private GameObject m_cPanInfo;
    private GameObject m_cBtnCancel;      //取消按钮Panel
    private UISprite m_cSPExpFull;
    private GameObject m_cBtnUpgrade;
    private UILabel m_cLbNextCount;
    private UILabel m_cLbNextInfo;
    private GameObject m_cBack;   //点击无效遮罩

    private UISprite m_cSprArrowLeft;           //向左滑动特效
    private UISprite m_cSprArrowRight;          //向右滑动特效

    //3D特效

    private GameObject m_cGuiEffect;    //3d特效资源
    private GameObject m_cEffectParent; //3d特效父对象
    private GameObject m_cEffectObjLevelUp;
    private UnityEngine.Object m_cEffectLevelUp;  //玩家升级特效

    private bool m_bPressDown = false;        //升级按钮按下
    private bool m_bIsDraging = false;        //是否滑动中
    private bool m_bIsRight = false;          //向右拖动
    private int m_fVIndex = 0;                //偏移量
    private int m_iSelectId = 0;              //选中项
    private bool m_bTweening = false;         //是否动画进行中
    private bool m_bhasMove = false;          //是否已经左右填补过
    private int m_iDragDistance = 0;          //来回滑动的距离
    private float m_fLevelShowTime = 0;  //Level Up 展示时间
    private bool m_bLevelShow = false;  //level up 展示标志
    private bool m_bFirstPress = false;
    private bool m_bCanMove = false;
    private bool m_bIsEquipOrTiaohe = false; //装备屋调和屋是否升级了
    private int m_iEquipNowLevel;//升级前装备屋等级
    private int m_iTiaoheNowLevel;//升级前调和屋等级

    List<int> m_lstBuildType=new List<int>(); //更新过的建筑类型
    private int m_iUpSpeed = 0; //连续按升级建筑按钮时候的加速度上升
    private float m_fDis = 0;

    private TDAnimation m_cEffectLeft;         //特效类
    private TDAnimation m_cEffectRight;        //特效类

    public GUIEquipUpgrade(GUIManager mgr)
        : base(mgr, GUI_DEFINE.GUIID_EQUIPUPGRADE, GUILAYER.GUI_PANEL)
    {
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        m_cPanSlide = null;       //panel滑动
        m_cTable = null;
        m_cPanInfo = null;
        m_cBtnCancel = null;      //取消按钮Panel
        m_cSPExpFull = null;
        m_cBtnUpgrade = null;
        m_cLbNextCount = null;
        m_cLbNextInfo = null;
        m_cBack = null;   //点击无效遮罩

        m_cSprArrowLeft = null;           //向左滑动特效
        m_cSprArrowRight = null;          //向右滑动特效

        //3D特效

        m_cGuiEffect = null;    //3d特效资源
        m_cEffectParent = null; //3d特效父对象
        m_cEffectObjLevelUp = null;
        m_cEffectLevelUp = null;  //玩家升级特效

        m_lstBuilding.Clear();
        m_lstBuildItem.Clear();
        m_lstBuildType.Clear();

        base.Destory();
    }

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
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_MAIN);
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_EFFECT_PATH, GAME_DEFINE.RES_VERSION, EFFECT_LEVEL_UP);
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
            //实例化GameObject
            //Main主资源
            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;
            //滑出动画panel
            this.m_cPanInfo = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_RIGHT);
            //取消按钮
            var cancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_CANCEL);
            GUIComponentEvent gui_event = cancel.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnCancel_OnEvent);
            this.m_cBtnCancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_CANCEL);
            //升级按钮
            this.m_cBtnUpgrade = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_UPGRADE);
            this.m_cBtnUpgrade.gameObject.AddComponent<GUIComponentEvent>().AddIntputDelegate(Upgrade_OnEvent);
            //升级槽精度条
            this.m_cSPExpFull = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SP_EXPFULL);
            //距离下一级升级经验
            this.m_cLbNextCount = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LB_NextCount);
            //下一级新的收获介绍
            this.m_cLbNextInfo = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LB_NextInfo);
            //panel slide
            this.m_cPanSlide = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_Slide);
            //滑动的table
            this.m_cTable = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, RES_TABLE);
            //点击无效遮罩
            this.m_cBack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, RES_BACK);

            //3D特效
            this.m_cGuiEffect = GUI_FINDATION.FIND_GAME_OBJECT(GUI_EFFECT);
            this.m_cEffectParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGuiEffect, EFFECT_CENTER_ANCHOR);
            this.m_cEffectLevelUp = (UnityEngine.Object)ResourcesManager.GetInstance().Load(EFFECT_LEVEL_UP);

            //左右导航
            this.m_cSprArrowLeft = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SPR_ARROWLEFT);
            this.m_cSprArrowLeft.gameObject.AddComponent<GUIComponentEvent>().AddIntputDelegate(Left_OnEvent);
            this.m_cEffectLeft = new TDAnimation(m_cSprArrowLeft.atlas, m_cSprArrowLeft); //左右导航
            this.m_cSprArrowRight = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SPR_ARROWRIGHT);
            this.m_cSprArrowRight.gameObject.AddComponent<GUIComponentEvent>().AddIntputDelegate(Right_OnEvent);
            this.m_cEffectRight = new TDAnimation(m_cSprArrowRight.atlas, m_cSprArrowRight);
        }

        this.m_cEffectLeft.Play("ArrowLeft", Game.Base.TDAnimationMode.Loop, 0.4F);
        this.m_cEffectRight.Play("ArrowRight", Game.Base.TDAnimationMode.Loop, 0.4F);

        this.m_cBack.SetActive(false);

        if (this.m_cTable != null) this.m_cTable.transform.localPosition = Vector3.zero;

        GameObject obj0 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cTable, RES_ITEM0);
        obj0.AddComponent<GUIComponentEvent>().AddIntputDelegate(Drag_OnEvent);
        obj0.transform.localPosition = new Vector3(-240, -60, 0);
        GameObject obj1 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cTable, RES_ITEM1);
        obj1.AddComponent<GUIComponentEvent>().AddIntputDelegate(Drag_OnEvent);
        obj1.transform.localPosition = new Vector3(0, -60, 0);
        GameObject obj2 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cTable, RES_ITEM2);
        obj2.AddComponent<GUIComponentEvent>().AddIntputDelegate(Drag_OnEvent);
        obj2.transform.localPosition = new Vector3(240, -60, 0);

        m_lstBuilding.Clear();
        m_lstBuilding.Add(new BuildShowItem(obj0));
        m_lstBuilding.Add(new BuildShowItem(obj1));
        m_lstBuilding.Add(new BuildShowItem(obj2));

        m_lstBuildItem.Clear();
        m_lstBuildItem.Add(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.ITEM));
        m_lstBuildItem.Add(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.EQUIP));
        m_lstBuildItem.Add(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.TIAN));
        m_lstBuildItem.Add(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.LIN));
        m_lstBuildItem.Add(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.CHUAN));
        m_lstBuildItem.Add(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.SHAN));

        m_iEquipNowLevel = Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.EQUIP).m_iLevel;
        m_iTiaoheNowLevel = Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.ITEM).m_iLevel;

        m_bPressDown = false;
        m_bIsDraging = false;
        m_bIsRight = false;
        m_fVIndex = 0;
        m_iSelectId = 0;
        m_bTweening = false;
        m_bhasMove = false;
        m_iDragDistance = 0;
        m_bFirstPress = false;
        m_bCanMove = false;

        UpdateBuilding();

        //设置整体GUI点击GUIID
        GUITown town = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_TOWN) as GUITown;
        town.SetTownChildId(this.ID);
        town.SetTownBlack(false);
        GUIBackFrameTop backtop = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP) as GUIBackFrameTop;
        backtop.Show();
        //this.m_cGUIMgr.SetCurGUIID(this.ID);

        CTween.TweenPosition(this.m_cBtnCancel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-420, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cPanInfo, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);

        SetLocalPos(Vector3.zero);

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_ENHANCE_HOUSE));

        //新手引导
        GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_TOWN4);
    }

    /// <summary>
    /// 更新升级数据
    /// </summary>
    private void UpdateBuilding()
    {
        Building currentBuilding = m_lstBuildItem[m_iSelectId];

        int preIndex = m_iSelectId - 1;
        if (preIndex < 0)
        {
            preIndex = m_lstBuildItem.Count - 1;
        }
        int nextIndex = m_iSelectId + 1;
        if (nextIndex > m_lstBuildItem.Count - 1)
        {
            nextIndex = 0;
        }

        m_lstBuilding.Sort(new Comparison<BuildShowItem>((i1, i2) =>
        {
            return i1.m_cItem.transform.localPosition.x.CompareTo(i2.m_cItem.transform.localPosition.x);
        }));

        ReflashSlideItem(m_lstBuilding[0], preIndex);
        ReflashSlideItem(m_lstBuilding[1], m_iSelectId);
        ReflashSlideItem(m_lstBuilding[2], nextIndex);


        int NextDistance = BuildingTableManager.GetInstance().GetBuildingExp(currentBuilding.m_eType, currentBuilding.m_iLevel);
        if (NextDistance == -1)
        {
            m_cSPExpFull.fillAmount = 0;
            m_cLbNextCount.text = "";   //距离下次升级的经验
            m_cLbNextInfo.text = "已满级";  //下次升级的描述
        }
        else
        {
            m_cLbNextCount.text = (NextDistance - currentBuilding.m_iExp).ToString();   //距离下次升级的经验
            m_cLbNextInfo.text = BuildingTableManager.GetInstance().GetBuildingNextInfo(currentBuilding.m_eType, currentBuilding.m_iLevel).Replace("\\n", "\n"); //下次升级的描述
            m_cSPExpFull.fillAmount = (float)currentBuilding.m_iExp / (float)NextDistance;
        }

        GUIBackFrameTop guitop = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP) as GUIBackFrameTop;
        guitop.UpdateFarmPiont();

    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        if (m_lstBuildType.Count > 0)
        {
            List<int> buildingLevel = new List<int>();
            List<int> buildingExp = new List<int>();
            for (int i = 0; i < m_lstBuildType.Count; i++)
            {
                buildingLevel.Add(Role.role.GetBuildingProperty().GetBuilding((BUILDING_TYPE)m_lstBuildType[i]).m_iLevel);
                buildingExp.Add(Role.role.GetBuildingProperty().GetBuilding((BUILDING_TYPE)m_lstBuildType[i]).m_iExp);
            }
            SendAgent.SendBuildingUpdateReq(Role.role.GetBaseProperty().m_iPlayerId,
                m_lstBuildType, buildingLevel, buildingExp, Role.role.GetBaseProperty().m_iFarmPoint);
        }

        GameObject.DestroyImmediate(m_cEffectObjLevelUp);
        CameraManager.GetInstance().HidenGUIEffectCamera();

        if (this.m_cBack != null)
        {
            this.m_cBack.SetActive(false);
        }

        m_bLevelShow = false;

        CTween.TweenPosition(this.m_cPanInfo, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0));
        CTween.TweenPosition(this.m_cBtnCancel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-420, 270, 0), Destory);

        base.Hiden();
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

        if (IsShow())
        {
            //左右特效
            if (this.m_cEffectLeft != null)
            {
                this.m_cEffectLeft.Update();
            }
            if (this.m_cEffectRight != null)
            {
                this.m_cEffectRight.Update();
            }

            //移动项目大小
            m_lstBuilding.ForEach(ac =>
            {
                float x = Math.Abs(ac.m_cItem.transform.localPosition.x + m_cTable.transform.localPosition.x);
                if (x > 240)
                {
                    x = 240;
                }

                ac.m_cItem.transform.localScale = Vector3.one * (0.8f + 0.2f * (240 - x) / 240);


            });

            if (m_bLevelShow)  // Level Up 展示控制
            {
                float dis = GAME_TIME.TIME_FIXED() - m_fLevelShowTime;

                if (dis > LEVEL_UP_SHOW_TIME)
                {
                    GameObject.DestroyImmediate(m_cEffectObjLevelUp);
                    CameraManager.GetInstance().HidenGUIEffectCamera();
                    this.m_cBack.SetActive(false);
                    m_bLevelShow = false;
                }
            }

            if (m_bPressDown)  //点一次按下 第二次弹起
            {
                if (Role.role.GetBaseProperty().m_iFarmPoint > 0)
                {
                    Building currentBuilding = m_lstBuildItem[m_iSelectId];

                    if (!m_lstBuildType.Contains(((int)currentBuilding.m_eType)))
                    {
                        m_lstBuildType.Add((int)currentBuilding.m_eType);
                    }

                    //距离下一级经验
                    int NextDistance = BuildingTableManager.GetInstance().GetBuildingExp(currentBuilding.m_eType, currentBuilding.m_iLevel);
                    if (NextDistance == -1)
                    {
                        m_bPressDown = false;
                        return false;
                    }

                    //加速度上升
                    //float fl=GAME_TIME.TIME_FIXED()-m_fDis;
                    //m_iUpSpeed = (int)(fl * NextDistance / 400);

                    float fl = (GAME_TIME.TIME_FIXED() - m_fDis) / 2.5f;
                    if (fl > 1)
                    {
                        fl = 1;
                    }
                    fl = CMath.ElasticOut(fl, 0.2F, 0.7f, 1f, 1f, 1f); //二次内曲线上升
                    m_iUpSpeed = (int)(fl * NextDistance / 50);

                    int tmpup = m_iUpSpeed;
                    if (tmpup < 1)
                    {
                        tmpup = 1;
                    }
                    //下一级升级需要
                    int expNow = NextDistance - currentBuilding.m_iExp;
                    if (expNow - tmpup < 0)
                    {
                        tmpup = expNow;
                    }
                    //如果加速度的量超出了农场点，就按照农场点的值
                    int farmNow = Role.role.GetBaseProperty().m_iFarmPoint - tmpup;
                    if (farmNow < 0)
                    {
                        tmpup = Role.role.GetBaseProperty().m_iFarmPoint;
                    }

                    currentBuilding.m_iExp += tmpup;
                    Role.role.GetBaseProperty().m_iFarmPoint -= tmpup;

                    if (Role.role.GetBaseProperty().m_iFarmPoint <= 0)
                    {
                        SoundManager.GetInstance().StopSoundContinue();
                    }
                    else
                    {
                        //数字跳动音效
                        SoundManager.GetInstance().PlaySoundContinue(SOUND_DEFINE.SE_NUM_JUMP);
                    }

                    if (currentBuilding.m_iExp >= NextDistance)
                    {
                        //记录是否为装备屋调和屋
                        if (currentBuilding.m_eType == BUILDING_TYPE.EQUIP || currentBuilding.m_eType == BUILDING_TYPE.ITEM)
                        {
                            m_bIsEquipOrTiaohe = true;
                        }

                        //展示升级特效
                        CameraManager.GetInstance().ShowGUIEffectCamera();  //开启特效摄像头
                        m_cEffectObjLevelUp = GameObject.Instantiate(this.m_cEffectLevelUp) as GameObject;
                        m_cEffectObjLevelUp.transform.parent = this.m_cEffectParent.transform;
                        m_cEffectObjLevelUp.transform.localScale = Vector3.one;
                        m_cEffectObjLevelUp.transform.localPosition = new Vector3(0, 160, 0);


                        this.m_cBack.SetActive(true);

                        m_fLevelShowTime = GAME_TIME.TIME_FIXED();
                        m_bLevelShow = true;

                        //数字跳动音效
                        SoundManager.GetInstance().StopSoundContinue();
                        //升级音效
                        SoundManager.GetInstance().PlaySound(SOUND_DEFINE.SE_UPGRADE);


                        currentBuilding.m_iExp = 0;
                        int maxLevel = BuildingTableManager.GetInstance().GetBuildingMaxLevel(currentBuilding.m_eType);
                        if (currentBuilding.m_iLevel < maxLevel)
                        {
                            currentBuilding.m_iLevel++;
                            //建筑等级提升时，点击回满
                            currentBuilding.m_lCollectTime = 0;
                        }
                        m_bPressDown = false;
                    }
                    UpdateBuilding();
                }
            }
        }
        return true;
    }

    /// <summary>
    /// 取消点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnCancel_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            if (SessionManager.GetInstance().Refresh())
            {
                if (this.m_bIsEquipOrTiaohe)
                {
                    SessionManager.GetInstance().SetCallBack(EquipOrItemBuildingUpgradeCallBack);
                }
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_TOWN).Show);
            }
            else
            {
                //this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_TOWN).Show();
                GUIBackFrameTop backtop = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP) as GUIBackFrameTop;
                backtop.Hiden();

                //设置整体GUI点击GUIID
                GUITown town = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_TOWN) as GUITown;
                town.SetTownChildId(0);
                town.SetTownBlack(true);
            }

        }
    }

    /// <summary>
    /// 装备屋调和屋升级回调
    /// </summary>
    private void EquipOrItemBuildingUpgradeCallBack()
    {
        //装备屋状态更新
        int equipLevel = Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.EQUIP).m_iLevel;
        if (equipLevel > this.m_iEquipNowLevel)
        {
            GAME_SETTING.s_iEquipLevelAdd += equipLevel - this.m_iEquipNowLevel;
            GAME_SETTING.SaveEquipLevelAdd();
            if(GAME_SETTING.s_iWarnHouseEquip == 0)
            {
                for (; equipLevel > this.m_iEquipNowLevel; ++this.m_iEquipNowLevel)
                {
                    List<int> list = BuildingTableManager.GetInstance().GetLastBuildEquipItem(m_iEquipNowLevel, 1);
                    foreach (int i in list)
                    {
                        if (Role.role.GetItemProperty().CanCombined(i))
                        {
                            GAME_SETTING.s_iWarnHouseEquip = 1;
                            GAME_SETTING.SaveWarnHouseEquip();
                            break;
                        }
                    }

                }
            }
            GAME_SETTING.s_bEquipLevelUp = true;
            GAME_SETTING.SaveEquipScane();
        }
        //调和屋状态更新
        int itemLevel = Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.ITEM).m_iLevel;
        if (itemLevel > this.m_iTiaoheNowLevel )
        {
            GAME_SETTING.s_iItemLevelAdd += itemLevel - this.m_iTiaoheNowLevel;
            GAME_SETTING.SaveItemLevelAdd();
            if (GAME_SETTING.s_iWarnHouseTiaohe == 0)
            {
                for (; itemLevel - 1 >= this.m_iTiaoheNowLevel; ++this.m_iTiaoheNowLevel)
                {
                    List<int> list = BuildingTableManager.GetInstance().GetLastBuildItem(this.m_iTiaoheNowLevel + 1,1);
                    foreach (int i in list)
                    {
                        if (Role.role.GetItemProperty().CanCombined(i))
                        {
                            GAME_SETTING.s_iWarnHouseTiaohe = 1;
                            GAME_SETTING.SaveWarnHouseTiaohe();
                            break;
                        }
                    }
                }
            }
            
            GAME_SETTING.s_bItemLevelUp = true;
            GAME_SETTING.SaveItemScane();
        }
    }


    /// <summary>
    /// 升级按钮事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void Upgrade_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        //if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        //{
        //    m_iUpSpeed = 0;

        //    m_bPressDown = false;
        //}
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.PRESS)
        {
            if (m_bFirstPress)
            {
                m_bPressDown = false;
                m_iUpSpeed = 0;

                m_bFirstPress = false;

                //数字跳动音效关闭
                SoundManager.GetInstance().StopSoundContinue();
            }
            else
            {

                m_fDis = GAME_TIME.TIME_FIXED();
                m_bPressDown = true;
                m_bFirstPress = true;
            }
        }
    }

    /// <summary>
    /// 单个物体滑动中
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void Drag_OnEvent(GUI_INPUT_INFO info, object[] args)
    {

        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.DRAG)
        {
            if (m_bTweening)
            {
                return;
            }

            if (m_bCanMove)
            {
                return;
            }

            m_bIsDraging = true;
            m_iDragDistance += (int)info.m_vecDelta.x / 5;
            if (m_bIsRight != (m_iDragDistance > 0))
            {
                m_bhasMove = false;
            }
            m_bIsRight = m_iDragDistance > 0;

            if (Math.Abs(m_iDragDistance) > 240)
            {
                if (m_bIsRight)
                    m_iDragDistance = 240;
                else
                    m_iDragDistance = -240;
            }
            //滑动中，找到最右边或最左边缺少的项目，将另一边多余的回填过来，并将对应的team和英雄数据填写新的项目 
            //eg： 10，1，2 --》  9 10 1 （10和1的gameobject不变，2的obj位置改变，数据从Team重新读取lst的next那个）
            if (!m_bhasMove)
            {
                var left = m_lstBuilding.Find(new Predicate<BuildShowItem>((item) => { return item.m_cItem.transform.localPosition.x + m_cTable.transform.localPosition.x > 320; }));
                if (left != null)
                {
                    left.m_cItem.transform.localPosition -= new Vector3(240 * 3, 0, 0);
                    int NextIndex = m_iSelectId - 2;
                    if (NextIndex < 0)
                    {
                        NextIndex = m_lstBuildItem.Count + NextIndex;
                    }

                    m_bhasMove = true;

                    ReflashSlideItem(left, NextIndex);
                    m_bCanMove = true;
                }
                else
                {
                    var right = m_lstBuilding.Find(new Predicate<BuildShowItem>((item) => { return item.m_cItem.transform.localPosition.x + m_cTable.transform.localPosition.x < -320; }));
                    if (right != null)
                    {
                        right.m_cItem.transform.localPosition += new Vector3(240 * 3, 0, 0);
                        int NextIndex = m_iSelectId + 2;
                        if (NextIndex > m_lstBuildItem.Count - 1)
                        {
                            NextIndex = NextIndex - m_lstBuildItem.Count;
                        }

                        m_bhasMove = true;

                        ReflashSlideItem(right, NextIndex);
                        m_bCanMove = true;
                    }
                }
            }

            //跟随drag移动
            this.m_cTable.transform.localPosition += new Vector3(info.m_vecDelta.x / 5, 0, 0);
        }
        else if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.PRESS && !info.m_bDone)  //drag 结束
        {
            m_bCanMove = false;

            if (m_bIsDraging)  //drag 结束事件只执行一次
            {
                m_bIsDraging = false;

                m_bTweening = true;

                //翻页特效
                SoundManager.GetInstance().PlaySound2(SOUND_DEFINE.SE_SLIDE);

                CTween.TweenPosition(this.m_cTable, 0, 0.3F, m_cTable.transform.localPosition, new Vector3(m_fVIndex * 240 + (m_bIsRight ? 240 : -240), 0, 0), TWEEN_LINE_TYPE.ElasticInOut, TweenFinish);  //结束剩余动画
                m_fVIndex += m_bIsRight ? 1 : -1;


                //根据位置偏移offest 余10得到当前选中的项目
                if (m_fVIndex < 0)
                {
                    m_iSelectId = -m_fVIndex % m_lstBuildItem.Count;
                }
                else
                {
                    m_iSelectId = m_lstBuildItem.Count - m_fVIndex % m_lstBuildItem.Count;
                    m_iSelectId = m_iSelectId == m_lstBuildItem.Count ? 0 : m_iSelectId;
                }

                int index = (m_iSelectId) % m_lstBuildItem.Count;

                if (!m_bhasMove)
                {
                    var left = m_lstBuilding.Find(new Predicate<BuildShowItem>((item) => { return item.m_cItem.transform.localPosition.x + m_cTable.transform.localPosition.x > 240; }));
                    if (left != null)
                    {
                        left.m_cItem.transform.localPosition -= new Vector3(240 * 3, 0, 0);
                        int NextIndex = m_iSelectId - 1;
                        if (NextIndex < 0)
                        {
                            NextIndex = m_lstBuildItem.Count + NextIndex;
                        }

                        ReflashSlideItem(left, NextIndex);
                    }
                    else
                    {
                        var right = m_lstBuilding.Find(new Predicate<BuildShowItem>((item) => { return item.m_cItem.transform.localPosition.x + m_cTable.transform.localPosition.x < -240; }));
                        if (right != null)
                        {
                            right.m_cItem.transform.localPosition += new Vector3(240 * 3, 0, 0);
                            int NextIndex = m_iSelectId + 1;
                            if (NextIndex > m_lstBuildItem.Count - 1)
                            {
                                NextIndex = NextIndex - m_lstBuildItem.Count;
                            }

                            ReflashSlideItem(right, NextIndex);
                        }
                    }
                }
            }
        }
    }

    public void Left_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType== GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (m_bTweening)
            {
                return;
            }

            if (m_bIsDraging)
            {
                return;
            }

            //手动触发两次滑动
            Drag_OnEvent(new GUI_INPUT_INFO() { m_eType = GUI_INPUT_INFO.GUI_INPUT_TYPE.DRAG, m_vecDelta = new Vector2(-10, 0) }, null);
            Drag_OnEvent(new GUI_INPUT_INFO() { m_eType = GUI_INPUT_INFO.GUI_INPUT_TYPE.PRESS }, null);
        }

    }

    public void Right_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (m_bTweening)
            {
                return;
            }

            if (m_bIsDraging)
            {
                return;
            }

            //手动触发两次滑动
            Drag_OnEvent(new GUI_INPUT_INFO() { m_eType = GUI_INPUT_INFO.GUI_INPUT_TYPE.DRAG, m_vecDelta = new Vector2(10, 0) }, null);
            Drag_OnEvent(new GUI_INPUT_INFO() { m_eType = GUI_INPUT_INFO.GUI_INPUT_TYPE.PRESS }, null);
        }

    }
    

    /// <summary>
    /// 动画结束回调
    /// </summary>
    private void TweenFinish()
    {
        m_bTweening = false;
        m_bhasMove = false;
        m_iDragDistance = 0;

        UpdateBuilding();
    }

    /// <summary>
    /// 刷新新进入的项目显示
    /// </summary>
    /// <param name="left"></param>
    /// <param name="NextIndex"></param>
    private void ReflashSlideItem(BuildShowItem buildItem, int Index)
    {
        int maxLevel = BuildingTableManager.GetInstance().GetBuildingMaxLevel(m_lstBuildItem[Index].m_eType);
        if (m_lstBuildItem[Index].m_iLevel >= maxLevel)
        {
            buildItem.m_cLbLevel.text = "Lv.Max";
        }
        else
        {
            buildItem.m_cLbLevel.text = "Lv." + m_lstBuildItem[Index].m_iLevel.ToString();
        }
        SetBuildSprite(buildItem.m_cSpItem, m_lstBuildItem[Index]);
    }

    /// <summary>
    /// 设置建筑图片
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="build"></param>
    private void SetBuildSprite(UISprite sp, Building build)
    {
        switch (build.m_eType)
        {
            case BUILDING_TYPE.EQUIP: sp.spriteName = "facility_sphere";
                break;
            case BUILDING_TYPE.ITEM: sp.spriteName = "facility_item";
                break;
            case BUILDING_TYPE.SHAN: sp.spriteName = "locate_mount";
                break;
            case BUILDING_TYPE.CHUAN: sp.spriteName = "locate_lake";
                break;
            case BUILDING_TYPE.TIAN: sp.spriteName = "locate_field";
                break;
            case BUILDING_TYPE.LIN: sp.spriteName = "locate_forest";
                break;
            default:
                break;
        }
    }
}