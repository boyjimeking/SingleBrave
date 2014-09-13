using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Resource;

/// <summary>
/// 制作人员GUI类
/// </summary>
public class GUIProductionStaff : GUIBase
{
    //主资源
    public const string RES_PRODUCTIONSTAFF = "GUI_ProductionStaff";//制作人员资源地址
    public const string RES_STAFFITEM = "GUI_ProductionStaffItem"; //制作人员名单地址

    private const string LIST = "SliderPanel/Panel/list"; //名单父节点
    private const string BACKBUTTON = "TopPanel/Button_Back";//返回按钮地址
    private const string TOPPANEL = "TopPanel";//导航栏地址
    private const string PANEL = "SliderPanel"; //滑动Panel地址

    private List<GameObject> m_lstStaffList = new List<GameObject>(); //制作人员列表

    private GameObject m_cTopPanel;
    private GameObject m_cBtnBack;
    private Transform m_cList;
    private GameObject m_cPanSlide;   //panel滑动

    private UnityEngine.Object m_cItem;//名单项

    public GUIProductionStaff(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_PRODUCTION_STAFF, GUILAYER.GUI_PANEL)
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
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_PRODUCTIONSTAFF);
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_STAFFITEM);
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
            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_PRODUCTIONSTAFF)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            this.m_cPanSlide = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PANEL);
            this.m_cList = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LIST).transform;
            this.m_cItem = (UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_STAFFITEM);

            this.m_cTopPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, TOPPANEL);
            this.m_cTopPanel.transform.localPosition = new Vector3(-420, 270, 0);

            this.m_cBtnBack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BACKBUTTON);
            GUIComponentEvent backEvent = this.m_cBtnBack.AddComponent<GUIComponentEvent>();
            backEvent.AddIntputDelegate(OnClickBackButton);
        }
        SetData();

        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-540, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cPanSlide, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);

        this.m_cGUIMgr.SetCurGUIID(this.m_iID);
        SetLocalPos(Vector3.zero);
    }

    /// <summary>
    /// 资源加载
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

    /// <summary>
    /// 初始化人员名单
    /// </summary>
    private void SetData()
    {
        List<ProductionStaffTable> list = ProductionStaffTableManager.GetInstance().GetAll();
        int num = 0;
        int count = 0; //相同职位数
        int positionID = 0;
        int AllTransform = 0; //总位移
        foreach (ProductionStaffTable ps in list)
        {
            GameObject obj = GameObject.Instantiate(this.m_cItem) as GameObject;
            obj.transform.parent = m_cList;
            obj.transform.localScale = Vector3.one;

            UILabel position = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(obj, "Lab_Position");         
            UILabel name = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(obj, "Lab_Name");
            name.text = ps.StaffName;


            if (num != 0)
            {
                if (positionID == ps.PositionID)
                {
                    count++;
                    obj.transform.localPosition = new Vector3(0, AllTransform + 70 * count - num * 100, 0);
                    position.enabled = false;
                }
                else
                {
                    AllTransform += 70 * count;
                    obj.transform.localPosition = new Vector3(0, AllTransform - num * 100, 0);
                    count = 0;
                    position.text = "[FFE399]" + ps.PositionName;
                }
            }
            else
            {
                obj.transform.localPosition = new Vector3(0, -num * 100, 0);
                position.text = "[FFE399]" + ps.PositionName;
            }
              

            m_lstStaffList.Add(obj);
            positionID = ps.PositionID;
            num++;
        }
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
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        m_cBtnBack = null;
        m_cItem = null;
        m_cList = null;
        m_cTopPanel = null;
        m_cPanSlide = null;

        base.Hiden();
        m_lstStaffList.Clear();
        base.Destory();
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //base.Hiden();

        GAME_SETTING.SaveSetting();
        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        CTween.TweenPosition(this.m_cPanSlide, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0));
        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-540, 270, 0), Destory);

        ResourcesManager.GetInstance().UnloadUnusedResources();
    }
}
