using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using Game.Resource;

//帮助类
//Author sunyi
//2014-1-16
public class GUIHelp : GUIBase
{
    private const string RES_MAIN = "GUI_Help";//主资源地址
    private const string RES_LISTCELL = "GUI_HelpListItemCell";//列表行

    private const string TOPPANEL = "TopPanel";//导航栏地址
    private const string MAINPANEL = "ClipPanel";//主面板地址
    private const string LISTVIEW = "ClipPanel/ListView";//列表地址
    private const string BACKBUTTON = "TopPanel/Btn_Back";//返回按钮地址
    private const string CONTENTLABEL = "Content";//列表内容地址
    private const string BTN_HELP = "ClipPanel/Btn_Help";//帮助按钮

    private GameObject m_cTopPanel;//导航栏
    private GameObject m_cMainPanel;//主面板
    private GameObject m_cListView;//列表
    private GameObject m_cBtnBack;//返回按钮
    private UnityEngine.Object m_cListItem;//列表项
    private GameObject m_cBtn_Help;

    private List<HelpTypeTable> m_lstHelpTypeTable;//帮助类型表

    protected float m_fClipParentY = 0;   //剪裁父节点Y轴坐标
    protected float m_fClipCenterY = -63;   //剪裁中间点Y轴坐标
    protected float m_fClipSizeY = 530; //剪裁Y轴大小

    private bool m_bHasShow = false;  //加载过showobject

    public GUIHelp(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_HELP, GUILAYER.GUI_PANEL)
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

            this.m_cBtnBack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BACKBUTTON);
            GUIComponentEvent backEvent = this.m_cBtnBack.AddComponent<GUIComponentEvent>();
            backEvent.AddIntputDelegate(OnClickBackButton);

            this.m_cBtn_Help = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_HELP);
            this.m_cBtn_Help.SetActive(false);
        }

        if (m_cListView != null)
        {
            foreach (Transform trans in this.m_cListView.transform)
            {
                GameObject.Destroy(trans.gameObject);
            }
        }
        if (m_lstHelpTypeTable != null)
        {
            m_lstHelpTypeTable.Clear();
        }

        this.m_cListView.transform.localPosition = new Vector3(0, 0, 0);
        UIPanel panel = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(this.m_cGUIObject, LISTVIEW);
        float y = -63.0f;
        panel.clipRange = new Vector4(panel.clipRange.x, y, panel.clipRange.z, panel.clipRange.w);

        this.m_lstHelpTypeTable = HelpTableManager.GetInstance().GetAllHelpTypeTable();

        for (int i = 0; i < this.m_lstHelpTypeTable.Count; i++)
        {
            GameObject listCell = GameObject.Instantiate(this.m_cListItem) as GameObject;
            listCell.transform.parent = this.m_cListView.transform;
            listCell.transform.localScale = Vector3.one;
            listCell.transform.localPosition = new Vector3(0, 145 - 113 * i, 0);
            UILabel contentLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(listCell, CONTENTLABEL);
            contentLabel.text = this.m_lstHelpTypeTable[i].TypeName;
            GUIComponentEvent listCellEvent = listCell.AddComponent<GUIComponentEvent>();
            listCellEvent.AddIntputDelegate(DidSelectedListCell, i);
        }

        this.m_cGUIMgr.SetCurGUIID(this.m_iID);
        SetLocalPos(Vector3.zero);

        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-640, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);
        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_HELP));
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

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //base.Hiden();

        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-640, 270, 0));
        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0) , Destory);

        ResourcesManager.GetInstance().UnloadUnusedResources();
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
    /// 列表行点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void DidSelectedListCell(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            GUIHelpTypeDetail detail = (GUIHelpTypeDetail)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HELPTYPEDETAIL);
            detail.SetHelpTypeId(this.m_lstHelpTypeTable[(int)args[0]].ID);
            detail.Show();
        }
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        if (m_cListView != null)
        {
            foreach (Transform trans in this.m_cListView.transform)
            {
                GameObject.Destroy(trans.gameObject);
            }
        }
        if (m_lstHelpTypeTable != null)
        {
            m_lstHelpTypeTable.Clear();
        }

        this.m_cTopPanel = null;
        this.m_cMainPanel = null;
        this.m_cListView = null;
        this.m_cBtnBack = null;
        this.m_cListItem = null;

        base.Destory();
    }
}
