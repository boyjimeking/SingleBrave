using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;

//帮助项目详细描述类
//Author sunyi
//2014-1-16
public class GUIHelpProjectDetail : GUIBase
{
    private const string RES_MAIN = "GUI_HelpProjectDetail";//主资源地址

    private const string MAINPANEL = "MainPanel";//主面板地址
    private const string TOPPANEL = "TopPanel";//导航栏面板地址
    private const string LABEL_HOMENAME = "TopPanel/Label";//导航栏标签地址
    private const string LABEL_CONTENT = "MainPanel/ClipView/ListView/Lab_Content";//主面板标签地址
    private const string BACKBUTTON = "TopPanel/Btn_Back";//返回按钮地址
    private const string LISTVIEW = "MainPanel/ClipView/ListView";//滚动视图地址

    private GameObject m_cTopPanel;//导航栏
    private GameObject m_cMainPanel;//主面板
    private UILabel m_cLabHomeName;//导航栏标签
    private UILabel m_cLabContent;//主面板标签内容
    private GameObject m_cBtnBack;//返回按钮地址
    private GameObject m_cListView;//滚动视图

    private string m_strProjectName;//帮助项目名称
    private string m_strContent;//详细内容

    public GUIHelpProjectDetail(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_HELPPROJECTDETAIL, GUILAYER.GUI_PANEL)
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

            this.m_cTopPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, TOPPANEL);
            this.m_cTopPanel.transform.localPosition = new Vector3(-420, 270, 0);

            this.m_cListView = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LISTVIEW);

            this.m_cMainPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, MAINPANEL);
            this.m_cMainPanel.transform.localPosition = new Vector3(640, 0, 0);

            this.m_cLabHomeName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LABEL_HOMENAME);

            this.m_cLabContent = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LABEL_CONTENT);

            this.m_cBtnBack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BACKBUTTON);
            GUIComponentEvent backEvent = this.m_cBtnBack.AddComponent<GUIComponentEvent>();
            backEvent.AddIntputDelegate(OnClickBackButton);
            ;
        }

        this.m_cListView.transform.localPosition = new Vector3(0, 0, 0);
        UIPanel panel = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(this.m_cGUIObject, LISTVIEW);
        float y = -66.75f;
        panel.clipRange = new Vector4(panel.clipRange.x, y, panel.clipRange.z, panel.clipRange.w);

        this.m_cLabHomeName.text = this.m_strProjectName;
        this.m_cLabContent.text = this.m_strContent.Replace("\\n", "\n");

        this.m_cGUIMgr.SetCurGUIID(this.m_iID);
        SetLocalPos(Vector3.zero);

        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-640, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 10, 0), new Vector3(0, 10, 0));
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //base.Hiden();

        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-640, 270, 0));
        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0,10,0), new Vector3(640, 10, 0) , Destory);

        ResourcesManager.GetInstance().UnloadUnusedResources();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        this.m_cTopPanel = null;
        this.m_cMainPanel = null;
        this.m_cLabHomeName = null;
        this.m_cLabContent = null;
        this.m_cBtnBack = null;
        this.m_cListView = null;

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

            GUIHelpTypeDetail detail = (GUIHelpTypeDetail)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HELPTYPEDETAIL);
            detail.Show();
        }
    }

    /// <summary>
    /// 设置标签内容
    /// </summary>
    /// <param name="projectName"></param>
    /// <param name="content"></param>
    public void SetProjectNameAndContent(string projectName, string content)
    {
        this.m_strProjectName = projectName;
        this.m_strContent = content;
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

