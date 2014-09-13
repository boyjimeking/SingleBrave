using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using Game.Resource;

//菜单-战绩
//Author sunyi
//2014-01-02
public class GUIBattleRecord : GUIBase
{
    private const string RES_MAIN = "GUI_BattleRecord";//主资源地址
    private const string RES_ITEM = "GUI_BattleRecordItem";//列表项资源地址

    private const string ITEM_PARENT = "MainPanel/ContentPanel/UITableText";//列表项父对象地址

    private const string TOPPANEL = "TopPanel";//导航栏面板地址
    private const string MAINPANEL = "MainPanel";//主面板地址
    private const string BACKBUTTON = "TopPanel/Button_Back";//返回按钮地址
    private const string LISTVIEW = "MainPanel/ContentPanel";//滚动视图地址
    private const string LABEL_TITLE = "Lab_Title";//标题标签地址
    private const string SPR_LEVEL = "Spr_Level";//等级精灵地址
    private const string LABEL_NO = "Lab_No";//数字标签地址

    private GameObject m_cTopPanel;//导航栏
    private GameObject m_cMainPanel;//主面板
    private GameObject m_cBtnBack;//返回按钮
    private GameObject m_cListView;//滚动视图
    private GameObject m_cItemParent;//item父对象

    private UnityEngine.Object m_cItem;//列表项
    private List<GameObject> m_lstRecordItems = new List<GameObject>();//滑动列表项

    public GUIBattleRecord(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_BATTLERECORD, GUILAYER.GUI_PANEL)
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
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_ITEM);
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

            this.m_cTopPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, TOPPANEL);
            this.m_cTopPanel.transform.localPosition = new Vector3(-420, 0, 0);

            this.m_cListView = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LISTVIEW);

            this.m_cBtnBack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BACKBUTTON);
            GUIComponentEvent backEvent = this.m_cBtnBack.AddComponent<GUIComponentEvent>();
            backEvent.AddIntputDelegate(OnClickBackButton);

            this.m_cItem = (UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_ITEM);

            this.m_cItemParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, ITEM_PARENT);
        }


        this.m_cListView.transform.localPosition = new Vector3(0, 0, 0);
        UIPanel panel = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(this.m_cGUIObject, LISTVIEW);
        float y = -74.1f;
        panel.clipRange = new Vector4(panel.clipRange.x, y, panel.clipRange.z, panel.clipRange.w);
        this.m_cGUIMgr.SetCurGUIID(this.m_iID);

        SetBattleRecordLabels();

        SetLocalPos(Vector3.zero);

        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-420, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, -10, 0), new Vector3(0, -10, 0));
    }
    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //base.Hiden();

        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        ResourcesManager.GetInstance().UnloadUnusedResources();

        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-420, 270, 0));
        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, -10, 0), new Vector3(640, -10, 0), Destory);

        if (this.m_lstRecordItems != null)
        {
            foreach (GameObject obj in this.m_lstRecordItems)
            {
                GameObject.Destroy(obj);
            }
        }
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        if (this.m_lstRecordItems != null)
        {
            foreach (GameObject obj in this.m_lstRecordItems)
            {
                GameObject.Destroy(obj);
            }
        }

        this.m_cTopPanel = null;
        this.m_cMainPanel = null;
        this.m_cBtnBack = null;
        this.m_cListView = null;
        this.m_cItemParent = null;

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

            GUIMenu menu = (GUIMenu)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_MENU);
            menu.Show();
        }
    }

    /// <summary>
    /// 设置标签内容
    /// </summary>
    private void SetBattleRecordLabels()
    {
        List<BattleRecordTable> lstRecords = BattleRecordTableManager.GetInstance().GetAll();

        if (this.m_lstRecordItems != null)
        {
            foreach (GameObject obj in this.m_lstRecordItems)
            {
                GameObject.Destroy(obj);
            }
        }

        List<int> lstNo = Role.role.GetBattleRecordProperty().GetAll();

        for (int i = 0; i < lstRecords.Count; i++)
        {
            GameObject recordItem = GameObject.Instantiate(this.m_cItem) as GameObject;
            recordItem.transform.parent = this.m_cItemParent.transform;
            recordItem.transform.localScale = Vector3.one;
            recordItem.transform.localPosition = new Vector3(60,- 20 - (40 * i),0);

            UILabel labTitle = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(recordItem, LABEL_TITLE);
            labTitle.text = lstRecords[i].Title;

            UILabel labNo = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(recordItem, LABEL_NO);
            labNo.text = lstNo[i].ToString();


            UISprite sprLevel = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(recordItem, SPR_LEVEL);
            if (lstNo[i] >= lstRecords[i].CoperTimes && lstNo[i] < lstRecords[i].SilverTimes)
            {
                sprLevel.spriteName = "coper";
            }
            else if (lstNo[i] >= lstRecords[i].SilverTimes && lstNo[i] < lstRecords[i].GoldTimes)
            {
                sprLevel.spriteName = "silver";
            }
            else if (lstNo[i] >= lstRecords[i].GoldTimes)
            {
                sprLevel.spriteName = "gold";
            }
            else {
                sprLevel.enabled = false ;
            }
            this.m_lstRecordItems.Add(recordItem);
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

