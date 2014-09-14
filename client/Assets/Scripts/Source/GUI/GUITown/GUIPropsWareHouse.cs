using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using Game.Resource;

//道具仓库
//Author:Sunyi
//2013-12-5

public class GUIPropsWareHouse : GUIBase
{
    private const string RES_MAIN = "GUI_PropsWareHouse";//主资源地址

    private const string TOPPANEL = "TopPanel";//导航栏地址
    private const string MAINPANEL = "MainPanel";//主面板地址
    private const string BACK_BUTTON = "TopPanel/Button_Back";//房名返回按钮地址
    private const string BUTTON_PROPSPREVIEW = "MainPanel/Btn_PropsPreview";//道具一览按钮地址
    private const string BUTTON_PROPSGROUP = "MainPanel/Btn_PropsGroup";//道具编成按钮地址
    private const string BUTTON_PROPSSALE = "MainPanel/Btn_PropsSale";//道具出售按钮地址

    private GameObject m_cTopPanel;//导航栏
    private GameObject m_cMainPanel;//主面板地址
    private GameObject m_cBtnBack;//返回按钮
    private GameObject m_cBtnPropsPreview;//道具一览按钮
    private GameObject m_cBtnPropsGroup;//道具编成按钮
    private GameObject m_cBtnPropsPropsSale;//道具出售按钮

    public GUIPropsWareHouse(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_PROPSWAREHOUSE, GUILAYER.GUI_PANEL)
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

            this.m_cMainPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, MAINPANEL);
            this.m_cMainPanel.transform.localPosition = new Vector3(0, 640, 0);

            this.m_cTopPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, TOPPANEL);
            this.m_cTopPanel.transform.localPosition = new Vector3(0, 270, 0);

            this.m_cBtnBack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BACK_BUTTON);
            GUIComponentEvent backEvent = this.m_cBtnBack.AddComponent<GUIComponentEvent>();
            backEvent.AddIntputDelegate(OnClickBackCutton);

            this.m_cBtnPropsPreview = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_PROPSPREVIEW);
            GUIComponentEvent propsPreviewEvent = this.m_cBtnPropsPreview.AddComponent<GUIComponentEvent>();
            propsPreviewEvent.AddIntputDelegate(OnCiickPropsPreviewButton);

            this.m_cBtnPropsGroup = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_PROPSGROUP);
            GUIComponentEvent propsGroupEvent = this.m_cBtnPropsGroup.AddComponent<GUIComponentEvent>();
            propsGroupEvent.AddIntputDelegate(OnCiicknPropsGroupButton);

            this.m_cBtnPropsPropsSale = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_PROPSSALE);
            GUIComponentEvent propsPropsSaleEvent = this.m_cBtnPropsPropsSale.AddComponent<GUIComponentEvent>();
            propsPropsSaleEvent.AddIntputDelegate(OnCiickPropsSaleButton);
        }

        //设置整体GUI点击GUIID
        GUITown town = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_TOWN) as GUITown;
        town.SetTownChildId(this.ID);
        town.SetTownBlack(false);
        GUIBackFrameTop backtop = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP) as GUIBackFrameTop;
        backtop.Show();
        //this.m_cGUIMgr.SetCurGUIID(this.ID);

        SetLocalPos(Vector3.zero);

        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-420, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_MAIN_MENU));

        //新手引导
        GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_TOWN10);
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //base.Hiden();

        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-420, 270, 0));
        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0),Destory);

        ResourceMgr.UnloadUnusedResources();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        m_cTopPanel = null;//导航栏
        m_cMainPanel = null;//主面板地址
        m_cBtnBack = null;//返回按钮
        m_cBtnPropsPreview = null;//道具一览按钮
        m_cBtnPropsGroup = null;//道具编成按钮
        m_cBtnPropsPropsSale = null;//道具出售按钮

        base.Hiden();
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

        return base.Update();
    }

    /// <summary>
    /// 返回按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickBackCutton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            //this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_TOWN).Show();
            GUIBackFrameTop backtop = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP) as GUIBackFrameTop;
            backtop.Hiden();

            //设置整体GUI点击GUIID
            GUITown town = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_TOWN) as GUITown;
            town.SetTownChildId(0);
            town.SetTownBlack(true);
        }
    }

    /// <summary>
    /// 道具一览按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnCiickPropsPreviewButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            GUIPropsPreview propsPreview = (GUIPropsPreview)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_PROPSPREVIEW);
            propsPreview.Show();
        }
    }

    /// <summary>
    /// 道具编成按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnCiicknPropsGroupButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            GUIPropsGroup propsGroup = (GUIPropsGroup)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_PROPSGROUP);
         
            propsGroup.Show();
            propsGroup.SetOldID(this.ID);
        }
    }

    /// <summary>
    /// 道具出售按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnCiickPropsSaleButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            GUIPropsSales propsSales = (GUIPropsSales)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_PROPSSALES);
            propsSales.Show();
        }
    }
}

