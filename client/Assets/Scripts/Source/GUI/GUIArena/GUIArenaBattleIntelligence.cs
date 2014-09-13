using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using Game.Resource;

//竞技场-战绩情报类
//Author sunyi
//2014-1-26
public class GUIArenaBattleIntelligence : GUIBase
{
    private const string RES_MAIN = "GUI_ArenaBattleIntelligence";//主资源地址
    private const string RES_ITEM = "GUI_ArenaBattleIntelligenceItem";//列表项资源地址

    private const string MAINPANEL = "MainPanel";//主面板地址
    private const string BUTTON_BACK = "Btn_Back";//返回按钮地址
    private const string LISTVIEW = "MainPanel/ClipView";//滚动视图地址
    private const string LB_PARENT_NUM = "MainPanel/ClipView/Content/No";  //数字父对象
    private const string LB_NAME = "MainPanel/ClipView/Content/Lab_Name";  //玩家称号
    private const string LB_RECORD = "MainPanel/ClipView/Content/Lab_BattleRecord";  //战绩

    private const string ITEM_PARENT = "MainPanel/ClipView/Content/UITableText";//列表项父对象地址

    private const string LABEL_TITLE = "Lab_Title";//标题标签地址
    private const string SPR_LEVEL = "Spr_Level";//等级精灵地址
    private const string LABEL_NO = "Lab_No";//数字标签地址

    private GameObject m_cBtnBack;//返回按钮
    private GameObject m_cMainPanel;//主面板
    private GameObject m_cNumParent;  //数字父对象
    private GameObject m_cItemParent;//item父对象
    private UnityEngine.Object m_cItem;//列表项
    private GameObject m_cListView;//滚动视图

    private UILabel m_cLB_Name;
    private UILabel m_cLB_Record;

    public List<int> m_lstRecord;

    private List<GameObject> m_lstRecordItems = new List<GameObject>();//列表

    public GUIArenaBattleIntelligence(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_ARENABATTLEINTELLIGENCE, GUILAYER.GUI_PANEL)
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
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_MAIN);
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH,RES_ITEM);
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
            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            this.m_cMainPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, MAINPANEL);
            this.m_cMainPanel.transform.localPosition = new Vector3(640, 0, 0);

            this.m_cBtnBack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_BACK);
            this.m_cBtnBack.transform.localPosition = new Vector3(-640, 420, 0);
            GUIComponentEvent backEvent = this.m_cBtnBack.AddComponent<GUIComponentEvent>();
            backEvent.AddIntputDelegate(OnClickBackButton);

            this.m_cLB_Record = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LB_RECORD);
            this.m_cLB_Name = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LB_NAME);

            this.m_cListView = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LISTVIEW);

            this.m_cItem = (UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_ITEM);

            this.m_cItemParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, ITEM_PARENT);


        }


        this.m_cListView.transform.localPosition = new Vector3(0, 0, 0);
        UIPanel panel = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(this.m_cGUIObject, LISTVIEW);
        float y = 0;
        panel.clipRange = new Vector4(panel.clipRange.x, y, panel.clipRange.z, panel.clipRange.w);

        SetLabelNum();

        SetLocalPos(Vector3.zero);
        this.m_cGUIMgr.SetCurGUIID(this.m_iID);

        CTween.TweenPosition(this.m_cBtnBack, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-640, 420, 0), new Vector3(-250, 420, 0));
        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), new Vector3(0, 0, 0));

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_ARENA_RECORD));
    }
    /// <summary>
    /// 刷新显示战绩
    /// </summary>
    private void SetLabelNum()
    {
        this.m_cLB_Name.text = AthleticsExpTableManager.GetInstance().GetAthleticsNameByPoint(Role.role.GetBaseProperty().m_iPVPExp);
        this.m_cLB_Record.text = Role.role.GetBaseProperty().m_iPVPWin + "胜 " + Role.role.GetBaseProperty().m_iPVPLose + "负";

        List<ArenaBattleRecordTable> lstRecords = ArenaBattleRecordTableManager.GetInstance().GetAll();

        if (this.m_lstRecordItems != null)
        {
            foreach (GameObject obj in this.m_lstRecordItems)
            {
                GameObject.Destroy(obj);
            }
        }

        for (int i = 0; i < lstRecords.Count; i++)
        {
            GameObject recordItem = GameObject.Instantiate(this.m_cItem) as GameObject;
            recordItem.transform.parent = this.m_cItemParent.transform;
            recordItem.transform.localScale = Vector3.one;
            recordItem.transform.localPosition = new Vector3(0, 150 - (40 * i), 0);

            UILabel labTitle = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(recordItem, LABEL_TITLE);
            labTitle.text = lstRecords[i].Title;

            UILabel labNo = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(recordItem, LABEL_NO);
            labNo.text = m_lstRecord[i].ToString();


            UISprite sprLevel = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(recordItem, SPR_LEVEL);
            if (m_lstRecord[i] >= lstRecords[i].CoperTimes && m_lstRecord[i] < lstRecords[i].SilverTimes)
            {
                sprLevel.spriteName = "coper";
            }
            else if (m_lstRecord[i] >= lstRecords[i].SilverTimes && m_lstRecord[i] < lstRecords[i].GoldTimes)
            {
                sprLevel.spriteName = "silver";
            }
            else if (m_lstRecord[i] >= lstRecords[i].GoldTimes)
            {
                sprLevel.spriteName = "gold";
            }
            else
            {
                sprLevel.enabled = false;
            }
            this.m_lstRecordItems.Add(recordItem);
        }
    }
    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //base.Hiden();

        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        CTween.TweenPosition(this.m_cBtnBack, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-250, 420, 0), new Vector3(-640, 420, 0));
        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 0, 0), new Vector3(640, 0, 0) , Destory);

        ResourcesManager.GetInstance().UnloadUnusedResources();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        m_cBtnBack = null;//返回按钮
        m_cMainPanel = null;//主面板
        m_cNumParent = null;  //数字父对象
        m_cItemParent = null;//item父对象
        m_cItem = null;//列表项
        m_cListView = null;//滚动视图
        m_cLB_Name = null;
        m_cLB_Record = null;

        if (m_lstRecordItems!=null)
        {
            foreach (GameObject item in m_lstRecordItems)
            {
                GameObject.DestroyImmediate(item);
            }
            m_lstRecordItems.Clear();

        }

        base.Destory();
    }

    /// <summary>
    /// 返回按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickBackButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            GUIArena arena = (GUIArena)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_ARENA);
            arena.Show();
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

        return base.Update();
    }

}

