using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using Game.Resource;
using Game.Media;

//特殊副本页面
//Author:Sunyi
//2013-11-25

public class GUIEspDungeon : GUIBase
{
    private const string RES_MAIN = "GUI_EspDungeon"; //主资源地址
    private const string RES_DUNGEONITEM = "GUI_EspDungeonItem";//副本项资源地址

    private const string BUTTON_WORLD = "Btn_World"; //返回世界地图按钮资源
    private const string BUTTON_MAIN = "Btn_Back"; //返回主页面按钮资源
    private const string LISTPARENT = "ListView/ScrollView/ClipView"; //特殊副本项父对象
    private const string LISTVIEW = "ListView/ScrollView";//滚动视图地址
    private const string LABEL_TIME = "Lab_Time";//剩余时间标签地址
    private const string SPR_BG = "Sprite_Bg";//副本背景
    private const string SPR_BLACK_BG = "Spr_Black";//灰色遮罩

    private GameObject m_cBtnWorld; //返回世界地图按钮
    private GameObject m_cBtnMain; //返回世界地图按钮
    private UnityEngine.Object m_cDungeonItem; //特殊副本项
    private GameObject m_cListItemParent;//副本列表父对象
    private GameObject m_cListView;//滚动视图

    private List<ActivityDungeonTable> m_lstActivityDungeon = new List<ActivityDungeonTable>();
    private List<FAV_TYPE> m_lstFavType = new List<FAV_TYPE>();//优惠类型列表
    private FAV_TYPE m_iActivityDungeonFavType;//优惠类型

    private int m_iLastGuiId;//上一个GUIid

    public GUIEspDungeon(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_ESPDUNGEON, UILAYER.GUI_PANEL)
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
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_DUNGEONITEM);
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

            this.m_cBtnWorld = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_WORLD);
            GUIComponentEvent guiWorldEvent = this.m_cBtnWorld.AddComponent<GUIComponentEvent>();
            guiWorldEvent.AddIntputDelegate(OnClickWorldButton);

            this.m_cBtnMain = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_MAIN);
            GUIComponentEvent guiMainEvent = this.m_cBtnMain.AddComponent<GUIComponentEvent>();
            guiMainEvent.AddIntputDelegate(OnClickMainButton);

            this.m_cListView = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LISTVIEW);

            this.m_cListItemParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LISTPARENT);

			this.m_cDungeonItem = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_DUNGEONITEM);

        }

        //播放特殊副本界面
        MediaMgr.sInstance.PlayBGM(SOUND_DEFINE.BGM_ACTIVE);

        this.m_cListView.transform.localPosition = new Vector3(0, -13, 0);
        UIPanel panel = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(this.m_cGUIObject, LISTVIEW);
        float y = -52.9f;
        panel.clipRange = new Vector4(panel.clipRange.x, y, panel.clipRange.z, panel.clipRange.w);

        if (this.m_lstActivityDungeon != null)
        {
            this.m_lstActivityDungeon.Clear();
        }

        if (this.m_lstFavType != null)
        {
            this.m_lstFavType.Clear();
        }

        List<ActivityDungeonTable> lstActivityDungeon = ActivityTableManager.GetInstance().GetAllDungeon();

        for (int i = 0; i < lstActivityDungeon.Count; i++)
        {
            int type = lstActivityDungeon[i].TimeType;
            switch (type)
            {
                case 1:
                    this.m_lstActivityDungeon.Add(lstActivityDungeon[i]);
                    this.m_lstFavType.Add(ActivityTableManager.GetInstance().GetActivityDungeonFavType()[i]);
                    break;
                case 2:
                    bool isInThisWeek = GUI_FUNCTION.IsInThisWeek(Convert.ToInt32(lstActivityDungeon[i].StartTime), Convert.ToInt32(lstActivityDungeon[i].EndTime));
                    if (isInThisWeek)
                    {
                        this.m_lstActivityDungeon.Add(lstActivityDungeon[i]);
                        this.m_lstFavType.Add(ActivityTableManager.GetInstance().GetActivityDungeonFavType()[i]);

                    }
                    break;
                case 3:
                    DateTime timeStart = DateTime.Parse(lstActivityDungeon[i].StartTime);
                    DateTime timeEnd = DateTime.Parse(lstActivityDungeon[i].EndTime);
                    bool isInThisDates = GUI_FUNCTION.IsInThisDates(timeStart, timeEnd);
                    if (isInThisDates)
                    {
                        this.m_lstActivityDungeon.Add(lstActivityDungeon[i]);
                        this.m_lstFavType.Add(ActivityTableManager.GetInstance().GetActivityDungeonFavType()[i]);
                    }
                    break;
                default:
                    break;
            }
        }

        

        for (int i = 0; i < this.m_lstActivityDungeon.Count; i++)
        {
            GameObject dungeonItem = GameObject.Instantiate(this.m_cDungeonItem) as GameObject;
            dungeonItem.transform.parent = this.m_cListItemParent.transform;
            dungeonItem.transform.localScale = Vector3.one;
            dungeonItem.transform.localPosition = new Vector3(0, 250 - 215 * i, 0);

            UISprite sprBg = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(dungeonItem, SPR_BG);
            sprBg.spriteName = this.m_lstActivityDungeon[i].SpName;

            UILabel labTime = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(dungeonItem, LABEL_TIME);

            UISprite spr = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(dungeonItem, SPR_BLACK_BG);

            GUIComponentEvent itemEvent = dungeonItem.AddComponent<GUIComponentEvent>();

            if (this.m_lstActivityDungeon[i].ID == 7)
            {
                DateTime d1 = new DateTime();
                d1 = DateTime.Parse(this.m_lstActivityDungeon[i].StartTime);
                int timeStarts = d1.Hour;

                DateTime d2 = new DateTime();
                d2 = DateTime.Parse(this.m_lstActivityDungeon[i].EndTime);
                int timeEnds = d2.Hour;

                bool isInThisTime = GUI_FUNCTION.IsInThisDuration(timeStarts, timeEnds);
                if (isInThisTime)
                {
                    spr.enabled = false;
                    SetIimeLabel(labTime, i);
                    itemEvent.AddIntputDelegate(DidSelectedItem, i);
                }
                else
                {
                    labTime.text = d1.Hour +"点开放";
                    spr.enabled = true;
                }
            }
            else {
                spr.enabled = false;
                SetIimeLabel(labTime, i);
                itemEvent.AddIntputDelegate(DidSelectedItem, i,this.m_lstActivityDungeon[i].TimeType);
            }
        }

        UIDraggablePanel dragPanel = GUI_FINDATION.GET_OBJ_COMPONENT<UIDraggablePanel>(this.m_cGUIObject, LISTVIEW);
        dragPanel.repositionClipping = true;

        SetLocalPos(Vector3.zero);
    }

    /// <summary>
    /// 设置上一个GUI
    /// </summary>
    /// <param name="id"></param>
    public void SetLastGuiId(int id)
    {
        this.m_iLastGuiId = id;
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        //SetLocalPos(Vector3.one * 0XFFFF);

        if (this.m_cListItemParent != null)
        {
            foreach (Transform trans in this.m_cListItemParent.transform)
            {
                GameObject.Destroy(trans.gameObject);
            }
        }

        if (this.m_lstActivityDungeon.Count > 0)
        {
            this.m_lstActivityDungeon.Clear();
        }

        ResourceMgr.UnloadUnusedResources();

        Destory();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        this.m_cBtnWorld = null;
        this.m_cBtnMain = null;
        this.m_cDungeonItem = null;
        this.m_cListItemParent = null;
        this.m_cListView = null;

        base.Destory();
    }
    /// <summary>
    /// 返回世界地图按钮事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void OnClickWorldButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            if (this.m_iLastGuiId == GUI_DEFINE.GUIID_AREA)
            {
                GUIArea area = (GUIArea)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_AREA);
                area.Show();
            }
            else if (this.m_iLastGuiId == GUI_DEFINE.GUIID_WORLD)
            {
                GUIWorld world = (GUIWorld)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_WORLD);
                world.Show();
            }
            else if (this.m_iLastGuiId == GUI_DEFINE.GUIID_MAIN)
            {
                GUIArea area = (GUIArea)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_AREA);
                WorldManager.s_iCurrentWorldId = 1;
				int newAreaIndex = FuBen.GetNewAreaIndex(WorldManager.s_iCurrentWorldId);
				int newDungeonIndex = FuBen.GetNewDungeonIndex(WorldManager.s_iCurrentWorldId, newAreaIndex);
                if (newDungeonIndex >= 0)
                {
                    WorldManager.s_iLastNewDungeonIndex = newDungeonIndex - 1;
                }
                area.ResetCurrentAreaId();
                area.Show();
            }
        }
    }

    /// <summary>
    /// 返回主页面按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void OnClickMainButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            GUIBackFrameBottom backbottom = (GUIBackFrameBottom)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM);
            backbottom.Show();
            this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();

            GUIMain main = (GUIMain)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_MAIN);
            main.Show();

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
            if (false)
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
    /// 关卡点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void DidSelectedItem(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            //最大英雄数量和最大物品数量控制
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

            WorldManager.s_iCurEspDungeonId = this.m_lstActivityDungeon[(int)args[0]].ID;
            WorldManager.s_eCurActivityDungeonFavType = this.m_lstFavType[(int)args[0]];

            Hiden();

            GUIBackFrameTop top = (GUIBackFrameTop)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP);
            top.Show();
            GUIBackFrameBottom backbottom = (GUIBackFrameBottom)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM);
            backbottom.HiddenHalf();

            GUIEspDungeonGate espDungeonGate = (GUIEspDungeonGate)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_ESPDUNGEONGATE);
            espDungeonGate.Show();
        }
    }

    /// <summary>
    /// 设置优惠类型标签内容
    /// </summary>
    /// <param name="FavLabel"></param>
    private void SetFavTimeLabel(UILabel FavLabel, FAV_TYPE favTimeType)
    {
        switch (favTimeType)
        {
            case FAV_TYPE.EXP_1_5:
                FavLabel.text = "经验1.5倍";
                break;
            case FAV_TYPE.EXP_2:
                FavLabel.text = "经验2倍";
                break;
            case FAV_TYPE.STRENGTH_HALF:
                FavLabel.text = "体力消耗减半";
                break;
            case FAV_TYPE.ITEM_DROP_1_5:
                FavLabel.text = "素材掉率1.5倍";
                break;
            case FAV_TYPE.FARM_NUM_2:
                FavLabel.text = "元气2倍";
                break;
            case FAV_TYPE.CATCH_RATE_1_5:
                FavLabel.text = "捕捉率1.5倍";
                break;
            case FAV_TYPE.GOLD_NUM_2:
                FavLabel.text = "获得金币2倍";
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 设置剩余时间
    /// </summary>
    /// <param name="labTime"></param>
    /// <param name="i"></param>
    private void SetIimeLabel(UILabel labTime, int i)
    {
        DateTime dtNow = GAME_FUNCTION.UNIXTimeToCDateTime(GAME_DEFINE.m_lServerTime);
        int endHourFromNow = 0;
        DateTime dt = new DateTime();
        switch (this.m_lstActivityDungeon[i].TimeType)
        { 
            case 1:
                dt = DateTime.Parse(this.m_lstActivityDungeon[i].EndTime);
                endHourFromNow = dt.Hour - dtNow.Hour;
                if (endHourFromNow == 0)
                {
                    int endMinuteFromNow = dt.Minute - dtNow.Minute;
                    labTime.text = "还剩" + endMinuteFromNow + "分钟";
                }else{
                    labTime.text = "还剩" + endHourFromNow + "小时";
                }
                break;
            case 2:
                int dayEnd = 0;
                if (Convert.ToInt32(this.m_lstActivityDungeon[i].EndTime) == 0)
                {
                    dayEnd = 7;
                }
                else {
                    dayEnd = Convert.ToInt32(this.m_lstActivityDungeon[i].EndTime);
                }
                int endDaysFromNow = dayEnd - (int)dtNow.DayOfWeek;
                if(endDaysFromNow == 0)
                {
                    endHourFromNow = 24 - dtNow.Hour;
                    if (endHourFromNow == 0)
                    {
                        int endMinuteFromNow = dt.Minute - dtNow.Minute;
                        labTime.text = "还剩" + endMinuteFromNow + "分钟";
                    }else{
                        labTime.text = "还剩" + endHourFromNow + "小时";
                    }
                }else{
                    labTime.text = "还剩" + endDaysFromNow + "天";
                }
                break;
            case 3:
                DateTime timeEnd = DateTime.Parse(this.m_lstActivityDungeon[i].EndTime);
                Debug.Log("time  " + (timeEnd - DateTime.Now).TotalDays);
                int endDateFromNow = (int)(timeEnd - DateTime.Now).TotalDays;
                Debug.Log("time222  " + (timeEnd - DateTime.Now).TotalDays);
                 if(endDateFromNow == 0)
                {
                    endHourFromNow = 24 - dtNow.Hour;
                    if (endHourFromNow == 0)
                    {
                        int endMinuteFromNow = dt.Minute - dtNow.Minute;
                        labTime.text = "还剩" + endMinuteFromNow + "分钟";
                    }else{
                        labTime.text = "还剩" + endHourFromNow + "小时";
                    }
                }else{
                        labTime.text = "还剩" + endDateFromNow + "天";
                }

                break;
            default:
                break;
        }
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
        return base.Update();
    }
}
