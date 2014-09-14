using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using Game.Resource;
using Game.Base;
using Game.Media;



/// <summary>
/// 区域地图GUI
/// </summary>
public class GUIArea : GUIBase
{
    private const string RES_MAIN = "GUI_Area";  //主资源地址
    private const string RES_AREAITEM = "GUI_AreaItem"; //区域地图入口项资源地址
    private const string RES_EFFECT_GUI_NEW_AREA_EXPOSE = "effect_GUI_newarea_chuxian";//new area出现特效
    private const string RES_NEW_AREA = "effect_GUI_Spr_NewArea";//new area特效
    private const string RES_GUI_AREA1 = "GUI_Area1";//第一张地图地址
    private const string RES_GUI_AREA2 = "GUI_Area2";//第二张地图地址
    private const string RES_GUI_AREA3 = "GUI_Area3";//第三张地图地址

    private const string GUI_EFFECT = "GUI_EFFECT";//3d特效资源地址

    private const string LST_PARENT = "lst";  //列表父节点
    private const string BACK_BUTTON = "Button_Back";    //返回按钮资源地址
    private const string FUBEN_BUTTON = "Button_Dungeon";  //特殊副本按钮资源地址
    private const string WORLD_BUTTON = "Button_World";  //特殊副本按钮资源地址
    private const string AREANAME_LABEL = "NameLabel";//副本名称地址
    private const string SPR_NEWAREA = "Spr_New";//新副本地址
    private const string SPR_CLEAR = "Spr_Clear";//已完成副本地址
    private const string LABEL_FAV = "Label_Fav/Lab_Fav";//优惠类型
    private const string LABEL_FAV_PARENT = "Label_Fav";//优惠类型标签父对象
    private const string CENTERPANEL = "AreaPanels/CenterAreaPanel";//中间区域面板
    private const string LEFTPANEL = "AreaPanels/LeftAreaPanel";//左侧区域面板
    private const string RIGHTPANEL = "AreaPanels/RightAreaPanel";//右侧区域面板
    private const string AREA_PANELS = "AreaPanels";//三个移动面板的父对象
    private const string AREA_BG = "Bg";//背景图片
    private const string EFFECT_CENTER_ANCHOR = "ANCHOR_CENTER";//3d特效父对象

    private const string SPR_ARROW_LEFT = "BottomPanel/Left/ArrowLeft";//左边箭头地址
    private const string SPR_ARROW_RIGHT = "BottomPanel/Right/ArrowRight";//右边箭头地址
    private const string SPR_ARROW_LEFT_PARENT = "BottomPanel/Left";//左边箭头父对象地址
    private const string SPR_ARROW_RIGHT_PARENT = "BottomPanel/Right";//右边箭头父对象地址
    private const string SPR_TITLE = "BottomPanel/Spr_Title";//底部标题图片
    
    private GameObject m_cListParent;   //列表父节点
    private GameObject m_cBack_Button;  //返回主页按钮
    private GameObject m_cFuben_Button; //特殊副本按钮
    private GameObject m_cWorld_Button; //返回世界地图按钮
    private GameObject m_cAreaItem;     //副本关卡项
    private GameObject m_cGuiEffect;    //3d特效资源
    private GameObject m_cEffectParent; //3d特效父对象

    private UnityEngine.Object m_cDungeonItem;//副本入口项
    private UnityEngine.Object m_cSprNewArea;//新副本入口特效
    private GameObject m_cCenterPanel;//当前显示的区域面板
    private GameObject m_cLeftPanel;//左边区域面板
    private GameObject m_cRightPanel;//右边区域面板
    private GameObject m_cAreaPanelParent;//三个移动面板的父对象
    private GameObject m_cSprNew;//new area对象
    private GameObject m_cEffectNew;//粒子特效

    private UISprite m_cSprTitle;//底部标题
    private GameObject m_cLeftParent;//左边箭头父对象
    private GameObject m_cRightParent;//右边箭头父对象
    private UISprite m_cSprArrowLeft;//左边箭头
    private UISprite m_cSprArrowRight;//右边箭头
    private TDAnimation m_cArrowLeftAnimation;//左边箭头动画
    private TDAnimation m_cArrowRightAnimation;//左边箭头动画

    private List<GameObject> m_lstDungeon = new List<GameObject>(); //区域副本项列表
    private List<GameObject> m_lstAreaPanel = new List<GameObject>();//区域面板列表
    private List<List<DungeonTable>> m_lstDungeonsAll = new List<List<DungeonTable>>();//该世界区域地图所有关卡项
    private List<DungeonTable> m_lstCurDungeonLst;//当前区域副本列表

    private bool m_bIsDrag = false;//是否滑动
    private bool m_bIsRight = false;//向左或向右滑动
    private int m_iNewAreaIndex = 0;//玩家最新地图
    private List<AreaTable> m_lstArea;//区域表

    private int m_iLastGuiId;//上一个guiid
    private List<GameObject> m_lstMovePanel =  new List<GameObject>();//三个移动的面板
    private int offset;//移动偏移量

    private float m_fDuration;//new出现动画持续时间
    private bool m_bIsNeedToShowNew;//new出现动画是否需要展示

    public GUIArea(GUIManager guimgr)
        : base(guimgr, GUI_DEFINE.GUIID_AREA, GUILAYER.GUI_PANEL)
    { 

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
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_MAIN);
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_AREAITEM);
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_EFFECT_GUI_NEW_AREA_EXPOSE);
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_EFFECT_PATH + RES_NEW_AREA);
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_GUI_AREA1);
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_GUI_AREA2);
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_GUI_AREA3);
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

			this.m_cSprNewArea = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_NEW_AREA);

            this.m_cGuiEffect = GUI_FINDATION.FIND_GAME_OBJECT(GUI_EFFECT);

            this.m_cEffectParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGuiEffect, EFFECT_CENTER_ANCHOR);

            this.m_cAreaPanelParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, AREA_PANELS);

            this.m_cBack_Button = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BACK_BUTTON);
            GUIComponentEvent guiBackEvent = this.m_cBack_Button.AddComponent<GUIComponentEvent>();
            guiBackEvent.AddIntputDelegate(OnClickBackButton);

            this.m_cWorld_Button = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, WORLD_BUTTON);
            GUIComponentEvent guiWorldEvent = this.m_cWorld_Button.AddComponent<GUIComponentEvent>();
            guiWorldEvent.AddIntputDelegate(OnClickWorldButton);

            this.m_cFuben_Button = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, FUBEN_BUTTON);
            GUIComponentEvent guiFubenEvent = this.m_cFuben_Button.AddComponent<GUIComponentEvent>();
            guiFubenEvent.AddIntputDelegate(OnClickFubenButton);

			this.m_cDungeonItem = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_AREAITEM);

            this.m_cSprArrowLeft = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SPR_ARROW_LEFT);
            this.m_cSprArrowRight = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SPR_ARROW_RIGHT);

            this.m_cArrowLeftAnimation = new TDAnimation(this.m_cSprArrowLeft.atlas, this.m_cSprArrowLeft);
            this.m_cArrowRightAnimation = new TDAnimation(this.m_cSprArrowRight.atlas, this.m_cSprArrowRight);

            this.m_cLeftParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, SPR_ARROW_LEFT_PARENT);
            GUIComponentEvent leftEvent = this.m_cLeftParent.AddComponent<GUIComponentEvent>();
            leftEvent.AddIntputDelegate(OnClickLeftArrow);

            this.m_cRightParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, SPR_ARROW_RIGHT_PARENT);
            GUIComponentEvent rightEvent = this.m_cRightParent.AddComponent<GUIComponentEvent>();
            rightEvent.AddIntputDelegate(OnClickRightArrow);

            this.m_cSprTitle = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SPR_TITLE);

        }


        //播放主题乐
        MediaMgr.sInstance.PlayBGM(SOUND_DEFINE.BGM_MAIN);

        this.m_bIsNeedToShowNew = true;

        CameraManager.GetInstance().ShowGUIEffectCamera();

        this.m_cArrowLeftAnimation.Play("ArrowLeft", Game.Base.TDAnimationMode.Loop, 0.4F);
        this.m_cArrowRightAnimation.Play("ArrowRight", Game.Base.TDAnimationMode.Loop, 0.4F);

        this.m_cSprNew = GameObject.Instantiate(this.m_cSprNewArea) as GameObject;
        this.m_cSprNew.transform.parent = this.m_cEffectParent.transform;
        this.m_cSprNew.transform.localScale = new Vector3(80, 80, 80);

        if (WorldManager.s_iCurrentAreaIndex == Role.role.GetFubenProperty().GetNewAreaIndex(WorldManager.s_iCurrentWorldId))
        {
            this.m_cSprNew.SetActive(true);
        }
        else
        {
            this.m_cSprNew.SetActive(false);
        }


        if (Role.role.GetFubenProperty().GetAllFuben().Count > 1)
        {
            if (!Role.role.GetFubenProperty().GetAllFuben()[1].m_bActive)
            {
                this.m_cWorld_Button.SetActive(false);
            }
            else
            {
                this.m_cWorld_Button.SetActive(true);
            }
        }

        this.m_cLeftPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LEFTPANEL);
        GUIComponentEvent leftPanelDragEvent = this.m_cLeftPanel.AddComponent<GUIComponentEvent>();
        leftPanelDragEvent.AddIntputDelegate(DragAreaPanel);

        this.m_cCenterPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, CENTERPANEL);
        GUIComponentEvent centerPanelDragEvent = this.m_cCenterPanel.AddComponent<GUIComponentEvent>();
        centerPanelDragEvent.AddIntputDelegate(DragAreaPanel);

        this.m_cRightPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, RIGHTPANEL);
        GUIComponentEvent rightPanelDragEvent = this.m_cRightPanel.AddComponent<GUIComponentEvent>();
        rightPanelDragEvent.AddIntputDelegate(DragAreaPanel);

        this.m_lstArea = WorldManager.GetAllArea(WorldManager.s_iCurrentWorldId);
        this.m_lstDungeonsAll.Clear();

        for (int i = 0; i < this.m_lstArea.Count; i++)
        {
            List<DungeonTable> lstDungeon = new List<DungeonTable>();
            for (int j = 0; j < WorldManager.GetAllDungeon(i + 1).Count; j++)//获取索引为i的区域地图关卡
            {
                lstDungeon.Add(WorldManager.GetAllDungeon(i + 1)[j]);
            }

            this.m_lstDungeonsAll.Add(lstDungeon);
        }

        foreach (GameObject item in this.m_lstDungeon)
        {
            GameObject.DestroyImmediate(item);
        }

        if (this.m_lstDungeon != null)
        {
            this.m_lstDungeon.Clear();
        }

        this.m_lstMovePanel.Clear();
        this.m_cLeftPanel.transform.localPosition = new Vector3(-640, 0, 0);

        this.m_lstMovePanel.Add(this.m_cLeftPanel);
        this.m_lstMovePanel.Add(this.m_cCenterPanel);
        this.m_lstMovePanel.Add(this.m_cRightPanel);

        if (WorldManager.s_iCurrentAreaIndex - 1 < 0)
        {
            SetAreaPanel(this.m_lstMovePanel[0], this.m_iNewAreaIndex);
        }
        else
        {
            SetAreaPanel(this.m_lstMovePanel[0], WorldManager.s_iCurrentAreaIndex - 1);
        }

        SetAreaPanel(this.m_lstMovePanel[1], WorldManager.s_iCurrentAreaIndex);

        if (WorldManager.s_iCurrentAreaIndex + 1 > this.m_iNewAreaIndex)
        {
            SetAreaPanel(this.m_lstMovePanel[2], 0);
        }
        else
        {
            SetAreaPanel(this.m_lstMovePanel[2], WorldManager.s_iCurrentAreaIndex + 1);
        }

        this.m_cGUIMgr.SetCurGUIID(this.m_iID);
        SetLocalPos(Vector3.zero);
        CTween.TweenAlpha(this.m_cListParent, GAME_DEFINE.FADEIN_GUI_TIME, 0, 1);

        //新手引导
        GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_BATTLE_SECOND2);
        GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_TOWN1);
        GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_BATTLE_THIRD2);
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        base.Hiden();
        CameraManager.GetInstance().HidenGUIEffectCamera();
        this.m_lstMovePanel.Clear();
        SetLocalPos(Vector3.one * 0XFFFF);

        ResourceMgr.UnloadUnusedResources();

        Destory();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        this.m_lstMovePanel.Clear();
        this.m_lstDungeon.Clear();
        this.m_lstDungeonsAll.Clear();

        if (this.m_cSprNew != null)
        {
            GameObject.Destroy(this.m_cSprNew);
            this.m_cSprNew = null;
        }

        if (this.m_cEffectNew != null)
        {
            GameObject.Destroy(this.m_cEffectNew);
            this.m_cEffectNew = null;
        }
        this.m_cListParent = null;
        this.m_cBack_Button = null;
        this.m_cFuben_Button = null;
        this.m_cWorld_Button = null;
        this.m_cAreaItem = null;
        this.m_cGuiEffect = null;
        this.m_cEffectParent = null;

        this.m_cDungeonItem = null;
        this.m_cSprNewArea = null;
        this.m_cCenterPanel = null;
        this.m_cLeftPanel = null;
        this.m_cRightPanel = null;
        this.m_cAreaPanelParent = null;

        this.m_cSprTitle = null;
        this.m_cLeftParent = null;
        this.m_cRightParent = null;
        this.m_cSprArrowLeft = null;
        this.m_cSprArrowRight = null;
        this.m_cArrowLeftAnimation = null;
        this.m_cArrowRightAnimation = null;

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

        if (this.m_cArrowLeftAnimation != null)
        {
            this.m_cArrowLeftAnimation.Update();
        }
        if (this.m_cArrowRightAnimation != null)
        {
            this.m_cArrowRightAnimation.Update();
        }

        //if (this.m_bIsNeedToShowNew)
        //{
        //    float dis = GAME_TIME.TIME_FIXED() - this.m_fDuration;
        //    Debug.Log("sssssssssssssssssssssssssssssss222");
        //    if (dis > 1.4)
        //    {
        //        if (this.m_cSprNew != null)
        //        {
        //            this.m_cSprNew.SetActive(true);
        //            Debug.Log("sssssssssssssssssssssssssssssss");
        //            this.m_bIsNeedToShowNew = false;
        //            return false;
        //        }
        //    }
        //}

        return base.Update();
    }

    /// <summary>
    /// 重置最新区域索引
    /// </summary>
    public void ResetNewAreaIndex()
    {
        this.m_iNewAreaIndex = 0;
    }

    /// <summary>
    /// 设置区域面板副本项
    /// </summary>
    /// <param name="areaPanel"></param>
    /// <param name="areaIndex"></param>
    private void SetAreaPanel(GameObject areaPanel,int areaIndex)
    {
        bool isNewDungeon = false;

        UITexture bgTex = GUI_FINDATION.GET_OBJ_COMPONENT<UITexture>(areaPanel, AREA_BG);
		Texture text = (Texture)ResourceMgr.LoadAsset(this.m_lstArea[areaIndex].BgName);
        bgTex.mainTexture = text;
        GameObject lstParent = GUI_FINDATION.GET_GAME_OBJECT(areaPanel, LST_PARENT);
        List<DungeonTable> lstDungeon = this.m_lstDungeonsAll[areaIndex];

        //if (true)
        //{
        //    lstDungeon.Clear();
        //    lstDungeon.Add(WorldManager.GetDungeonTable(100000));
        //}

        int newDungeonIndex = 0;
        if (this.m_iNewAreaIndex == areaIndex)
        {
            newDungeonIndex = Role.role.GetFubenProperty().GetNewDungeonIndex(WorldManager.s_iCurrentWorldId, this.m_iNewAreaIndex);
        }
        else {
            
            newDungeonIndex = lstDungeon.Count-1;
        }

        SetCurAreaBottomTitleSpr();

        for (int i = 0; i < newDungeonIndex + 1; i++)
        {
            //新手引导副本去除
            if (lstDungeon[i].ID == GUIDE_FUNCTION.DUNGEONID_ID && newDungeonIndex > 0)
            {
                continue;
            }

            GameObject dungeonItemCell = GameObject.Instantiate(this.m_cDungeonItem) as GameObject;
            dungeonItemCell.transform.parent = lstParent.transform;
            dungeonItemCell.transform.localScale = Vector3.one;
            dungeonItemCell.transform.localPosition = new Vector3(lstDungeon[i].PoxX, lstDungeon[i].PosY, 0);


            UILabel nameLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(dungeonItemCell, AREANAME_LABEL);
            nameLabel.text = lstDungeon[i].Name;

            GameObject favLabelParent = GUI_FINDATION.GET_GAME_OBJECT(dungeonItemCell, LABEL_FAV_PARENT);

            UILabel favLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(dungeonItemCell, LABEL_FAV);
            string strType = lstDungeon[i].FavTime.Substring(0,1);

            int intType = int.Parse(strType);

            string strDayOfweek = lstDungeon[i].FavTime.Substring(2, 1);
            int dayOfweek = int.Parse(strDayOfweek);
            bool isTodayInWeek = GUI_FUNCTION.IsTodayInWeek(lstDungeon[i].FavTimeDayOfWeek);

            int timeStart = int.Parse(lstDungeon[i].FavTime.Substring(4, 2));
            int timeEnd = int.Parse(lstDungeon[i].FavTime.Substring(10, 2));
            int timeNowFromEnd = timeEnd - DateTime.Now.Hour;
            bool isInThisTime = GUI_FUNCTION.IsInThisDuration(lstDungeon[i].FavTimeStart, lstDungeon[i].FavTimeEnd);
            bool isNeedToDisplay = false;
            if (isTodayInWeek && isInThisTime)
            {
                isNeedToDisplay = true;
                SetFavLabel(favLabel, lstDungeon[i].FavType);
            }
            else
            {
                isNeedToDisplay = false;
                favLabelParent.SetActive(false);
            }

            GameObject sprClaer = GUI_FINDATION.GET_GAME_OBJECT(dungeonItemCell, SPR_CLEAR);
            if (this.m_iNewAreaIndex == areaIndex)
            {
                if (i == newDungeonIndex && GAME_SETTING.s_iIsOver == 0)
                {
                    sprClaer.SetActive(false);
                    //this.m_cSprNew.SetActive(true);
                    isNewDungeon = true;
                        this.m_cSprNew.transform.localPosition = new Vector3(lstDungeon[i].PoxX, lstDungeon[i].PosY + 30, 0);

                        if (WorldManager.s_iLastNewDungeonIndex == newDungeonIndex - 2)
                        {
                            if (this.m_cEffectNew == null)
                            {
							this.m_cEffectNew = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_EFFECT_GUI_NEW_AREA_EXPOSE)) as GameObject;
                            }
                            m_cEffectNew.transform.parent = this.m_cEffectParent.transform;
                            m_cEffectNew.transform.localScale = new Vector3(80, 80, 80);
                            m_cEffectNew.transform.localPosition = this.m_cSprNew.transform.localPosition;
                            WorldManager.s_iLastNewDungeonIndex = newDungeonIndex - 1;
                            GAME_SETTING.SaveNewDungeonOfNewArea(true);
                            this.m_bIsNeedToShowNew = true;
                            this.m_cSprNew.SetActive(false);
                            this.m_fDuration = GAME_TIME.TIME_FIXED();
                            CTween.TweenPosition(this.m_cSprNew.gameObject, 1.4f, this.m_cSprNew.transform.localPosition, this.m_cSprNew.transform.localPosition, ShowNew);
                        }
                        else if (!GAME_SETTING.s_bNewDungeonOfNewArea)
                        {
						this.m_cEffectNew = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_EFFECT_GUI_NEW_AREA_EXPOSE)) as GameObject;
                            m_cEffectNew.transform.parent = this.m_cEffectParent.transform;
                            m_cEffectNew.transform.localScale = new Vector3(80, 80, 80);
                            m_cEffectNew.transform.localPosition = this.m_cSprNew.transform.localPosition;
                            GAME_SETTING.SaveNewDungeonOfNewArea(true);
                            this.m_bIsNeedToShowNew = true;
                            this.m_cSprNew.SetActive(false);
                            this.m_fDuration = GAME_TIME.TIME_FIXED();
                            CTween.TweenPosition(this.m_cSprNew.gameObject, 1.4f, this.m_cSprNew.transform.localPosition, this.m_cSprNew.transform.localPosition, ShowNew);
                        }
                }
                else
                {
                    sprClaer.SetActive(true);
                }
            }
            else {
                sprClaer.SetActive(true);
                this.m_bIsNeedToShowNew = false;
            }

            GUIComponentEvent dungeonItemCellEvent = dungeonItemCell.AddComponent<GUIComponentEvent>();
            dungeonItemCellEvent.AddIntputDelegate(didSelectedDungeonItem, lstDungeon[i].ID, lstDungeon[i].Name, isNewDungeon, lstDungeon[i].FavType, isNeedToDisplay, timeNowFromEnd);
            this.m_lstDungeon.Add(dungeonItemCell);
        }
        
    }

    /// <summary>
    /// 展示‘新’特效
    /// </summary>
    private void ShowNew()
    {
        if( this.m_cSprNew != null)
            this.m_cSprNew.SetActive(true);
    }

    /// <summary>
    /// 返回按钮事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void OnClickBackButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            //GUIBackFrameBottom backbottom = (GUIBackFrameBottom)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM);
            //backbottom.ShowHalf();

            this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
            this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Show();
            this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();


            GUIMain main = (GUIMain)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_MAIN);
            main.SetLastGuiId(GUI_DEFINE.GUIID_AREA);
            main.Show();

        }

    }

    /// <summary>
    /// 返回世界地图按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void OnClickWorldButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            GUIWorld world = (GUIWorld)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_WORLD);
            world.Show();
        }
    }


    /// <summary>
    /// 特殊副本按钮事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void OnClickFubenButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            List<FAV_TYPE> lstActivityFavType = ActivityTableManager.GetInstance().GetActivityDungeonFavType();

            GUIEspDungeon espDungeon = (GUIEspDungeon)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_ESPDUNGEON);
            espDungeon.SetLastGuiId(GUI_DEFINE.GUIID_AREA);
            espDungeon.Show();

        }
    }


    /// <summary>
    /// 区域副本入口点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void didSelectedDungeonItem(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
  
            //最大英雄数量和最大物品数量控制
            if (Role.role.GetHeroProperty().GetAllHero().Count >= Role.role.GetBaseProperty().m_iMaxHeroCount)
            {
                GUI_FUNCTION.MESSAGEM_(MessageCallBack_HeroMax, GAME_FUNCTION.STRING(STRING_DEFINE.WARNING_MAX_HERO), "btn_expand", "btn_expand1", "btn_hero", "btn_hero1");
                if (this.m_cSprNew)
                {
                    float x = this.m_cSprNew.transform.localPosition.x;
                    float y = this.m_cSprNew.transform.localPosition.y;
                    if (-349.0f < x && x < 349.0f && -142.0f < y && y < 142.0f)
                    {
                        this.m_cSprNew.SetActive(false);
                    }
                }
                return;
            }
            if (Role.role.GetItemProperty().GetAllItemCount() >= Role.role.GetBaseProperty().m_iMaxItem)
            {
                GUI_FUNCTION.MESSAGEM_(MessageCallBack_ItemMax, GAME_FUNCTION.STRING(STRING_DEFINE.WARNING_MAX_ITEM), "btn_expand", "btn_expand1", "btn_daoju1", "btn_daoju");
                if (this.m_cSprNew)
                {
                    float x = this.m_cSprNew.transform.localPosition.x;
                    float y = this.m_cSprNew.transform.localPosition.y;
                    if (-349.0f < x && x < 349.0f && -142.0f < y && y < 142.0f)
                    {
                        this.m_cSprNew.SetActive(false);
                    }
                }

                return;
            }


            int id = (int)args[0];//当前被点击的itemID
            GUIAreaDungeon gui = (GUIAreaDungeon)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_AREADUNGEON);
            gui.SetDungeonID(id);
            List<DungeonTable> lstDungeon = WorldManager.GetAllDungeon(WorldManager.s_iCurrentAreaIndex + 1);
            for (int i = 0; i < lstDungeon.Count; i++)
            {
                if (lstDungeon[i].ID == id)
                {
                    WorldManager.s_iCurrentDungeonIndex = i;
                }
            }

            //是否是第一次进
            FuBen fuben = Role.role.GetFubenProperty().GetFubenByWorldID(WorldManager.s_iCurrentWorldId);
            if ( !fuben.m_bDungeonStory && fuben.m_iGateIndex == 0 && fuben.m_iDungeonIndex == WorldManager.s_iCurrentDungeonIndex)
            {
                //GUI_FUNCTION.SHOW_STORY(dungeonTable.StoryID, Show);
                AreaTable area = WorldManager.GetArea(fuben.m_iWorldID, fuben.m_iAreaIndex);
                DungeonTable dungeonTable = WorldManager.GetDungeonTable(area.ID, fuben.m_iDungeonIndex);
                if (dungeonTable.StoryID > 0)
                {
                    fuben.m_bDungeonStory = true;
                    SendAgent.SendFubenStory(Role.role.GetBaseProperty().m_iPlayerId, WorldManager.s_iCurrentWorldId, WorldManager.s_iCurrentAreaIndex,
                        WorldManager.s_iCurrentDungeonIndex, WorldManager.s_iCurrentGateIndex);
                    Hiden();
                    return;
                }
            }

            Hiden();

            if ((bool)args[4])//判断当前点击的副本是否在优惠时间段内
            {
                WorldManager.s_eCurDungeonFavType = WorldManager.GetDungeonTable(id).FavType;
            }
            else {
                WorldManager.s_eCurDungeonFavType = 0;
            }

            this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();

            GUIBackFrameBottom backbottom = (GUIBackFrameBottom)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM);
            backbottom.Show();
            backbottom.HiddenHalf();

            GUIAreaDungeon guiAreaDungeon = (GUIAreaDungeon)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_AREADUNGEON);
            guiAreaDungeon.SetAreaGateName(args[1].ToString());
            bool isNew = (bool)args[2];
            if (GAME_SETTING.s_iIsOver == 1)
                isNew = false;
            guiAreaDungeon.IsNewDungeon(isNew);
            guiAreaDungeon.SetFavTimeType((int)args[3],(bool)args[4],(int)args[5]);

            guiAreaDungeon.Show();

        }
        else {
            DragAreaPanel(info, args);
        }
    }

    /// <summary>
    /// 英雄超限对话框CallBack
    /// </summary>
    /// <param name="reuslt"></param>
    private void MessageCallBack_HeroMax(bool result)
    {
        Hiden();
        if (result)  //扩大单位数量
        {
            this.m_cGUIMgr.HidenCurGUI();

            if (false)
            {
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_UNITSLOTEXPANSION).Show);
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show);
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show);
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Show);

            }
            else
            {
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_UNITSLOTEXPANSION).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Show();
            }
        }
        else  //出售
        {

            this.m_cGUIMgr.HidenCurGUI();
            if (false)
            {
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERO_MENU).Show);
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show);
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show);
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Show);
            }
            else
            {
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERO_MENU).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Show();
            }
        }
    }

    /// <summary>
    /// 物品满了 委托
    /// </summary>
    /// <param name="ok"></param>
    private void MessageCallBack_ItemMax(bool result1)
    {
        Hiden();
        if (result1)  //扩大单位数量
        {
            this.m_cGUIMgr.HidenCurGUI();

            if (false)
            {
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_PROPSSLOTEXPANSION).Show);
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show);
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show);
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Show);
            }
            else
            {
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_PROPSSLOTEXPANSION).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Show();
            }
        }
        else  //出售
        {

            this.m_cGUIMgr.HidenCurGUI();
            if (false)
            {

                SessionManager.GetInstance().SetCallBack(() =>
                {
                    GUITown town = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_TOWN) as GUITown;
                    town.Show();
                    this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_PROPSSALES).Show();
                    this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
                    this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Show();
                });
            }
            else
            {
                GUITown town = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_TOWN) as GUITown;
                town.Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_PROPSSALES).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Show();
            }
        }
    }

    /// <summary>
    /// 左箭头点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickLeftArrow(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (WorldManager.s_iCurrentAreaIndex < this.m_iNewAreaIndex)
            {
                LeftDragPanel();
                WorldManager.s_iCurrentAreaIndex += 1;

                if (WorldManager.s_iCurrentAreaIndex == Role.role.GetFubenProperty().GetNewAreaIndex(WorldManager.s_iCurrentWorldId))
                {
                    this.m_cSprNew.SetActive(true);
                }
                else
                {
                    this.m_cSprNew.SetActive(false);
                }
                SetCurAreaBottomTitleSpr();
            }
        }

        DragAreaPanel(info, args);
    }

    /// <summary>
    /// 右箭头点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickRightArrow(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (WorldManager.s_iCurrentAreaIndex > 0)
            {
                RightDragPanel();
                WorldManager.s_iCurrentAreaIndex -= 1;

                if (WorldManager.s_iCurrentAreaIndex == Role.role.GetFubenProperty().GetNewAreaIndex(WorldManager.s_iCurrentWorldId))
                {
                    this.m_cSprNew.SetActive(true);
                }
                else
                {
                    this.m_cSprNew.SetActive(false);
                }
                SetCurAreaBottomTitleSpr();
            }
        }
        DragAreaPanel(info, args);
    }

    /// <summary>
    /// 设置底部当前区域标题
    /// </summary>
    private void SetCurAreaBottomTitleSpr()
    {
        if (this.m_lstArea != null)
        {
            this.m_cSprTitle.spriteName = this.m_lstArea[WorldManager.s_iCurrentAreaIndex].TitleSprName;

            if (WorldManager.s_iCurrentAreaIndex == 0 && this.m_iNewAreaIndex > 0)
            {
                this.m_cLeftParent.SetActive(false);
                this.m_cRightParent.SetActive(true);
            }
            else if (WorldManager.s_iCurrentAreaIndex == this.m_iNewAreaIndex && this.m_iNewAreaIndex != 0)
            {
                this.m_cRightParent.SetActive(false);
                this.m_cLeftParent.SetActive(true);
            }
            else if (WorldManager.s_iCurrentAreaIndex == this.m_iNewAreaIndex && this.m_iNewAreaIndex == 0)
            {
                this.m_cRightParent.SetActive(false);
                this.m_cLeftParent.SetActive(false);
            }
            else {
                this.m_cRightParent.SetActive(true);
                this.m_cLeftParent.SetActive(true);
            }
        }
    }

    /// <summary>
    /// 滑动区域面板
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void DragAreaPanel(GUI_INPUT_INFO info, object[] args)
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

                if (m_bIsRight)
                {
                    if (WorldManager.s_iCurrentAreaIndex > 0)
                    {
                        RightDragPanel();

                        WorldManager.s_iCurrentAreaIndex -= 1;
                        this.m_bIsDrag = false;
                        
                        if (WorldManager.s_iCurrentAreaIndex == Role.role.GetFubenProperty().GetNewAreaIndex(WorldManager.s_iCurrentWorldId))
                        {
                            this.m_cSprNew.SetActive(true);
                        }
                        else
                        {
                            this.m_cSprNew.SetActive(false);
                        }
                        SetCurAreaBottomTitleSpr();
                    }

                }
                else {
                    if (WorldManager.s_iCurrentAreaIndex < this.m_iNewAreaIndex)
                    {
                        LeftDragPanel();
                        WorldManager.s_iCurrentAreaIndex += 1;
                        this.m_bIsDrag = false;

                        if (WorldManager.s_iCurrentAreaIndex == Role.role.GetFubenProperty().GetNewAreaIndex(WorldManager.s_iCurrentWorldId))
                        {
                            this.m_cSprNew.SetActive(true);
                        }
                        else
                        {
                            this.m_cSprNew.SetActive(false);
                        }
                        SetCurAreaBottomTitleSpr();
                    }

                }
            }
        }
    }

    /// <summary>
    /// 重置当前区域id
    /// </summary>
    /// <param name="id"></param>
    public void ResetCurrentAreaId()
    {
       this.m_iNewAreaIndex = Role.role.GetFubenProperty().GetNewAreaIndex(WorldManager.s_iCurrentWorldId);
       WorldManager.s_iCurrentAreaIndex = this.m_iNewAreaIndex;
    }

    /// <summary>
    /// 右滑
    /// </summary>
    /// <returns></returns>
    private void RightDragPanel()
    {
        for (int i = 0; i < this.m_lstMovePanel.Count; i++)
        {
            Vector3 vec3 = this.m_lstMovePanel[i].transform.localPosition;
            if (this.m_lstMovePanel[i].transform.localPosition.x + this.m_cAreaPanelParent.transform.localPosition.x + 1 > 640)
            {
                MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_SLIDE);

                Vector3 vec = this.m_lstMovePanel[i].transform.localPosition;
                this.m_lstMovePanel[i].transform.localPosition = new Vector3(vec.x - 3 * 640, 0, 0);
                ResetDungeonItem(this.m_lstMovePanel[i]);
                Vector3 vec2 = this.m_cAreaPanelParent.transform.localPosition;
                TweenPosition.Begin(this.m_cAreaPanelParent, 0.2f, new Vector3(vec2.x  + 640, 0, 0));
                this.offset += 640;
            }

        }
        
    }

    /// <summary>
    /// 左滑
    /// </summary>
    /// <returns></returns>
    private void LeftDragPanel()
    {
        for (int i = 0; i < this.m_lstMovePanel.Count; i++)
        {
            Vector3 vec3 = this.m_lstMovePanel[i].transform.localPosition;
            if (this.m_lstMovePanel[i].transform.localPosition.x + this.m_cAreaPanelParent.transform.localPosition.x - 1 < -640)
            {
                MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_SLIDE);

                Vector3 vec = this.m_lstMovePanel[i].transform.localPosition;
                this.m_lstMovePanel[i].transform.localPosition = new Vector3(vec.x + 3 * 640, 0, 0);
                ResetDungeonItem(this.m_lstMovePanel[i]);
                Vector3 vec2 = this.m_cAreaPanelParent.transform.localPosition;
                TweenPosition.Begin(this.m_cAreaPanelParent, 0.2f, new Vector3(vec2.x - 640, 0, 0));
                this.offset -= 640;
            }
        }
    }

    /// <summary>
    /// 重置副本项
    /// </summary>
    /// <param name="areaPanel"></param>
    private void ResetDungeonItem(GameObject areaPanel)
    {
        GameObject lst = null;

        foreach (Transform tran in areaPanel.transform)
        {
            if (tran.gameObject.name == "lst")
            {
                lst = tran.gameObject;
            }
        }
        //移除lst下所有组件
        foreach (Transform tsf in lst.transform)
        {
            GameObject.Destroy(tsf.gameObject);
        }
        int index = -1;
        if (this.m_bIsRight)
        {
            index = WorldManager.s_iCurrentAreaIndex - 2;
            if (index < 0)
            {
                index = this.m_iNewAreaIndex;
            }
        }
        else {
            index = WorldManager.s_iCurrentAreaIndex + 2;
            if (index > this.m_lstDungeonsAll.Count - 1)
            {
                index = 0;
            }
        }
        UITexture bgTex = GUI_FINDATION.GET_OBJ_COMPONENT<UITexture>(areaPanel, AREA_BG);
		Texture text = (Texture)ResourceMgr.LoadAsset(this.m_lstArea[index].BgName);
        bgTex.mainTexture = text;
        List<DungeonTable> lstDungeon = this.m_lstDungeonsAll[index];
        int newDungeonIndex = 0;
        if (this.m_iNewAreaIndex == index)
        {
            newDungeonIndex = Role.role.GetFubenProperty().GetNewDungeonIndex(WorldManager.s_iCurrentWorldId, this.m_iNewAreaIndex);
        }
        else
        {
            newDungeonIndex = lstDungeon.Count - 1;
        }
        //重新加载lst下所有组件
        for (int i = 0; i < newDungeonIndex + 1; i++)
        {
            GameObject dungeonItemCell = GameObject.Instantiate(this.m_cDungeonItem) as GameObject;
            dungeonItemCell.transform.parent = lst.transform;
            dungeonItemCell.transform.localScale = Vector3.one;
            dungeonItemCell.transform.localPosition = new Vector3(lstDungeon[i].PoxX, lstDungeon[i].PosY, 0);

            GameObject favLabelParent = GUI_FINDATION.GET_GAME_OBJECT(dungeonItemCell, LABEL_FAV_PARENT);

            UILabel nameLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(dungeonItemCell, AREANAME_LABEL);
            nameLabel.text = lstDungeon[i].Name;

            UILabel favLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(dungeonItemCell, LABEL_FAV);
            string strType = lstDungeon[i].FavTime.Substring(0, 1);

            int intType = int.Parse(strType);
 
            string strDayOfweek = lstDungeon[i].FavTime.Substring(2, 1);
            int dayOfweek = int.Parse(strDayOfweek);
            bool isTodayInWeek = GUI_FUNCTION.IsTodayInWeek(lstDungeon[i].FavTimeDayOfWeek);

            int timeStart = int.Parse(lstDungeon[i].FavTime.Substring(4, 2));
            int timeEnd = int.Parse(lstDungeon[i].FavTime.Substring(10, 2));
            int timeNowFromEnd = timeEnd - DateTime.Now.Hour;

            bool isInThisTime = GUI_FUNCTION.IsInThisDuration(lstDungeon[i].FavTimeStart, lstDungeon[i].FavTimeEnd);

            bool isNeedToDisplay = false;//用于判断下一个关卡界面是否需要显示优惠条件
            if (isTodayInWeek && isInThisTime)
            {
                SetFavLabel(favLabel, lstDungeon[i].FavType);
                isNeedToDisplay = true;
            }
            else
            {
                isNeedToDisplay = false;
                favLabelParent.SetActive(false);
            }

            GameObject sprClaer = GUI_FINDATION.GET_GAME_OBJECT(dungeonItemCell, SPR_CLEAR);
            bool isNewDungeon = false;
            if (this.m_iNewAreaIndex == index && GAME_SETTING.s_iIsOver == 0)
            {
                isNewDungeon = true;
                if (i == newDungeonIndex)
                {
                    sprClaer.SetActive(false);
                    isNewDungeon = true;
                    this.m_cSprNew.transform.localPosition = new Vector3(lstDungeon[i].PoxX, lstDungeon[i].PosY + 30, 0);
                }
                else
                {
                    sprClaer.SetActive(true);
                    isNewDungeon = false;
                }
            }
            else
            {
                isNewDungeon = false;
                sprClaer.SetActive(true);
            }

            GUIComponentEvent dungeonItemCellEvent = dungeonItemCell.AddComponent<GUIComponentEvent>();
            dungeonItemCellEvent.AddIntputDelegate(didSelectedDungeonItem, lstDungeon[i].ID, lstDungeon[i].Name, isNewDungeon, lstDungeon[i].FavType, isNeedToDisplay, timeNowFromEnd);
            this.m_lstDungeon.Add(dungeonItemCell);
        }
    }

    /// <summary>
    /// 设置优惠类型标签内容
    /// </summary>
    /// <param name="label"></param>
    /// <param name="type"></param>
    private void SetFavLabel(UILabel favLabel, FAV_TYPE type)
    {
        switch (type)
        {
            case FAV_TYPE.EXP_1_5:
                favLabel.text = "经验1.5倍";
                break;
            case FAV_TYPE.EXP_2:
                favLabel.text = "经验2倍";
                break;
            case FAV_TYPE.STRENGTH_HALF:
                favLabel.text = "体力消耗减半";
                break;
            case FAV_TYPE.ITEM_DROP_1_5:
                favLabel.text = "素材掉率1.5倍";
                break;
            case FAV_TYPE.FARM_NUM_2:
                favLabel.text = "元气2倍";
                break;
            case FAV_TYPE.CATCH_RATE_1_5:
                favLabel.text = "捕捉率1.5倍";
                break;
            case FAV_TYPE.GOLD_NUM_2:
                favLabel.text = "获得金币2倍";
                break;
            default:
                break;
        }
    }


}

