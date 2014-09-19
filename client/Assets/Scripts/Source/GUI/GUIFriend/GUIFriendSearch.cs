//  GUIFriendSearch.cs
//  Author: Cheng Xia
//  2013-1-7

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;

/// <summary>
/// 好友检索GUI
/// </summary>
public class GUIFriendSearch : GUIBase
{
    private const string RES_MAIN = "GUI_FriendSearch";                 //菜单资源地址
    private const string BTN_CANCEL = "Title_Cancel/Btn_Cancel";        //取消按钮地址
    private const string PAN_CANCEL = "Title_Cancel";                   //取消Pan地址
    private const string BTN_FRIENDSEARCH = "PanInfo/AlertView/Btn_Search";    //search按钮地址
    private const string PAN_RIGHT = "PanInfo";  //滑出Panel地址
    private const string SP_BACK = "Back";  //遮罩地址
    private const string RES_INPUT = "PanInfo/AlertView/Input";  //输入框地址
    private const string LAB_SELFID = "AlertView/LabSelfID";

    private GameObject m_cPanSlide;   //panel滑动
    private GameObject m_cBtnCancel;  //取消按钮Panel
    private GameObject m_cSpBack;       //遮罩背景
    private GameObject m_cInput;      //输入框
    private UILabel m_labSelfID;    //自己的ID
    private UIInput m_input; //输入

    public GUIFriendSearch(GUIManager mgr)
        : base(mgr, GUI_DEFINE.GUIID_FRIENDSEARCH, UILAYER.GUI_PANEL)
    {
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public override void Initialize()
    {
        base.Initialize();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        this.m_cPanSlide = null;
        this.m_cBtnCancel = null;
        this.m_cSpBack = null;
        this.m_cInput = null;
        this.m_labSelfID = null;
        this.m_input = null;

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
            //实例化GameObject
            //Main主资源
            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset( RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;
            //滑出动画panel
            this.m_cPanSlide = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_RIGHT);

            m_labSelfID = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cPanSlide, LAB_SELFID);
            //取消按钮
            var cancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_CANCEL);
            GUIComponentEvent gui_event = cancel.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnCancel_OnEvent);
            this.m_cBtnCancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_CANCEL);
            //好友检索按钮
            var friendsearch = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_FRIENDSEARCH);
            gui_event = friendsearch.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnSearch_OnEvent);
            //遮罩背景
            this.m_cSpBack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, SP_BACK);
            gui_event = m_cSpBack.gameObject.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnBackClick_OnEvent);
            //输入框
            m_cInput = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, RES_INPUT);
            m_input = GUI_FINDATION.GET_OBJ_COMPONENT<UIInput>(this.m_cGUIObject, RES_INPUT);
            gui_event = m_cInput.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(Input_OnEvent);
        }


        this.m_input.value = "";
        this.m_cSpBack.SetActive(false);
        m_labSelfID.text = Role.role.GetBaseProperty().m_iPlayerId.ToString();

        this.m_cGUIMgr.SetCurGUIID(this.ID);

        CTween.TweenPosition(this.m_cBtnCancel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-540, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cPanSlide, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);

        SetLocalPos(Vector3.zero);

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_SEARCH_FRIEND));
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //base.Hiden();

        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        ResourceMgr.UnloadUnusedResources();

        CTween.TweenPosition(this.m_cPanSlide, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0));
        CTween.TweenPosition(this.m_cBtnCancel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-540, 270, 0),Destory);

        if (this.m_cSpBack != null)
        {
            this.m_cSpBack.SetActive(false);
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

    /// <summary>
    /// 取消点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnCancel_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {

            GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

            Hiden();

            GUIFriendMenu frimenu = (GUIFriendMenu)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FRIENDMENU);
            frimenu.Show();

        }
    }

    /// <summary>
    /// 检索点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnSearch_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            int friendID;
            int.TryParse(m_cInput.GetComponent<UIInput>().value, out friendID);

            SendAgent.SendFriendFind(Role.role.GetBaseProperty().m_iPlayerId, friendID);
        }
    }

    /// <summary>
    /// 输入框点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void Input_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            m_input.label.color = Color.gray;
            this.m_cSpBack.SetActive(true);
        }
    }

    /// <summary>
    /// 遮罩背景点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnBackClick_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            m_input.label.color = Color.black;
            this.m_cSpBack.SetActive(false);
        }
    }

}