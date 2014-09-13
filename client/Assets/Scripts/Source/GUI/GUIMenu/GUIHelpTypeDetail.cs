using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Resource;
using UnityEngine;

//帮助项目类
//Author Sunyi
//2014-1-16
public class GUIHelpTypeDetail : GUIBase
{
    private const string RES_MAIN = "GUI_HelpProject";//主资源地址
    private const string RES_LISTCELL = "GUI_HelpListItemCell";//列表行

    private const string TOPPANEL = "TopPanel";//导航栏地址
    private const string MAINPANEL = "ClipPanel";//主面板地址
    private const string LISTVIEW = "ClipPanel/ListView";//列表地址
    private const string BACKBUTTON = "TopPanel/Btn_Back";//返回按钮地址
    private const string LABEL_HOMENAME = "TopPanel/Label";//导航栏标签地址
    private const string CONTENTLABEL = "Content";//列表内容地址

    private GameObject m_cTopPanel;//导航栏
    private GameObject m_cMainPanel;//主面板
    private GameObject m_cListView;//列表
    private GameObject m_cBtnBack;//返回按钮
    private UnityEngine.Object m_cListItem;//列表项
    private UILabel m_cLabHomeName;//导航栏标签

    private List<HelpProjectTable> m_lstHelpProjectTable;//帮助项目表
    private int m_iCurrentTypeId;//当前帮助类型id

    public GUIHelpTypeDetail(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_HELPTYPEDETAIL, GUILAYER.GUI_PANEL)
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
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_LISTCELL);
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

            this.m_cMainPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, MAINPANEL);
            this.m_cMainPanel.transform.localPosition = new Vector3(640, 0, 0);

            this.m_cListItem = (UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_LISTCELL);

            this.m_cListView = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LISTVIEW);

            this.m_cLabHomeName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LABEL_HOMENAME);

            this.m_cBtnBack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BACKBUTTON);
            GUIComponentEvent backEvent = this.m_cBtnBack.AddComponent<GUIComponentEvent>();
            backEvent.AddIntputDelegate(OnClickBackButton);
        }

        UIPanel panel = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(this.m_cGUIObject, LISTVIEW);
        float y = -63.0f;
        panel.transform.localPosition = new Vector3(0, 0, 0);
        panel.clipRange = new Vector4(panel.clipRange.x, y, panel.clipRange.z, panel.clipRange.w);

        this.m_cLabHomeName.text = HelpTableManager.GetInstance().GetHelpTypeTable(this.m_iCurrentTypeId).TypeName;

        this.m_lstHelpProjectTable = HelpTableManager.GetInstance().GetAllHelpProjectWithTypeId(this.m_iCurrentTypeId);

        for (int i = 0; i < this.m_lstHelpProjectTable.Count; i++)
        {
            GameObject listCell = GameObject.Instantiate(this.m_cListItem) as GameObject;
            listCell.transform.parent = this.m_cListView.transform;
            listCell.transform.localScale = Vector3.one;
            listCell.transform.localPosition = new Vector3(0, 145 - 113 * i, 0);
            UILabel contentLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(listCell, CONTENTLABEL);
            contentLabel.text = this.m_lstHelpProjectTable[i].ProjectName;
            GUIComponentEvent listCellEvent = listCell.AddComponent<GUIComponentEvent>();
            listCellEvent.AddIntputDelegate(DidSelectedListCell, i);
        }

        this.m_cGUIMgr.SetCurGUIID(this.m_iID);
        SetLocalPos(Vector3.zero);

        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-640, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //base.Hiden();

        if (this.m_lstHelpProjectTable != null)
        {
            this.m_lstHelpProjectTable.Clear();
        }

        foreach (Transform trans in this.m_cListView.transform)
        {
            GameObject.Destroy(trans.gameObject);
        }

        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-640, 270, 0));
        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0) , Destory);

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
        this.m_cListView = null;
        this.m_cBtnBack = null;
        this.m_cListItem = null;
        this.m_cLabHomeName = null;

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

            GUIHelp help = (GUIHelp)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HELP);
            help.Show();
        }
    }

    /// <summary>
    /// 设置当前帮助类型id
    /// </summary>
    /// <param name="id"></param>
    public void SetHelpTypeId(int id)
    {
        this.m_iCurrentTypeId = id;
    }

    /// <summary>
    /// 列表行点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void DidSelectedListCell(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            GUIHelpProjectDetail detail = (GUIHelpProjectDetail)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HELPPROJECTDETAIL);
            detail.SetProjectNameAndContent(this.m_lstHelpProjectTable[(int)args[0]].ProjectName, this.m_lstHelpProjectTable[(int)args[0]].DetailDesc);
            Hiden();
            detail.Show();
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

