//  GUIFRiendInfo.cs
//  Author: Cheng Xia
//  2013-1-7

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;

/// <summary>
/// 好友信息界面GUI
/// </summary>
public class GUIFriendInfoLike : GUIBase
{
    private const string RES_MAIN = "GUI_FriendInfoLike";                   //菜单资源地址
    private const string BTN_CANCEL = "Title_Cancel/Btn_Cancel";        //取消按钮地址
    private const string PAN_CANCEL = "Title_Cancel";                   //取消Pan地址
    private const string BTN_RELIVEVE = "BtnRelieve";         //好友解除
    private const string BTN_LIKE = "BtnLike";       //好友喜欢
    private const string BTN_Unlock = "BtnUnlock";  //解除锁定

    private const string RES_BLACK = "Black";
    private const string PAN_RIGHT = "PanInfo";  //滑出Panel地址

    private GameObject m_cPanSlide;   //panel滑动
    private GameObject m_cBtnCancel;  //取消按钮Panel
    private GameObject m_cPlayInfo;

    private GameObject m_cBlack;
    private GameObject m_cLike;
    private GameObject m_cUnlock;
    public Friend m_cFriend;

    private FriendPanel m_cFriendPanel;


    public GUIFriendInfoLike(GUIManager mgr)
        : base(mgr, GUI_DEFINE.GUIID_FRIENDINFOLIKE, GUILAYER.GUI_PANEL)
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
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_MAIN);
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, FriendPanel.RES_FRIENDPANEL);
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
            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            //滑出动画panel
            this.m_cPanSlide = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_RIGHT);
            m_cBlack = GUI_FINDATION.GET_GAME_OBJECT(m_cPanSlide, RES_BLACK);

            //取消按钮//
            GameObject cancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_CANCEL);
            GUIComponentEvent gui_event = cancel.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnCancel_OnEvent);
            this.m_cBtnCancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_CANCEL);
            //好友解除//
            GameObject friendReliveve = GUI_FINDATION.GET_GAME_OBJECT(m_cPanSlide, BTN_RELIVEVE);
            gui_event = friendReliveve.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnFriendReliveve_OnEvent);

            //好友喜欢//
            m_cLike = GUI_FINDATION.GET_GAME_OBJECT(m_cPanSlide, BTN_LIKE);
            gui_event = m_cLike.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnFriendLike_OnEvent);

            m_cUnlock = GUI_FINDATION.GET_GAME_OBJECT(m_cPanSlide, BTN_Unlock);
            gui_event = m_cUnlock.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnFriendLike_OnEvent);
            //m_cLike = GUI_FINDATION.GET_GAME_OBJECT(friendLike, OBJ_LIKE);
            //m_cUnlock = GUI_FINDATION.GET_GAME_OBJECT(friendLike, OBJ_UNLOCK);

            m_cFriendPanel = new FriendPanel();
            m_cFriendPanel.SetParent(m_cPanSlide.transform);
        }
        ReflashBtn();

        m_cFriendPanel.ReflashInfo(m_cFriend);

        this.m_cGUIMgr.SetCurGUIID(this.ID);

        CTween.TweenPosition(this.m_cBtnCancel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-540, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cPanSlide, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);

        SetLocalPos(Vector3.zero);

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_OPERATE_FRIEND));
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //base.Hiden();

        ResourcesManager.GetInstance().UnloadUnusedResources();

        GUI_FUNCTION.AYSNCLOADING_HIDEN();

        CTween.TweenPosition(this.m_cPanSlide, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0));
        CTween.TweenPosition(this.m_cBtnCancel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-540, 270, 0),Destory);

        //SetLocalPos(Vector3.one * 0xFFFF);
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        this.m_cPanSlide = null;
        this.m_cBtnCancel = null;
        this.m_cPlayInfo = null;

        this.m_cBlack = null;
        this.m_cLike = null;
        this.m_cUnlock = null;

        if (m_cFriendPanel != null)
        {
            m_cFriendPanel.Destory();
            m_cFriendPanel = null;
        }

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
    /// 取消点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnCancel_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            GUIFriendList frilst = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FRIENDLIST) as GUIFriendList;
            frilst.Show();
        }
    }

    /// <summary>
    /// 好友解除
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnFriendReliveve_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Debug.Log("Reliveve");

            GUI_FUNCTION.MESSAGEM_(SelectReliveveFriend, "是否真要解除好友!");
        }
    }

    /// <summary>
    /// 好友喜欢绑定/取消
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnFriendLike_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            List<int> lstFriendPID = new List<int>();
            lstFriendPID.Add(m_cFriend.m_iID);
            if (m_cFriend.m_bLike)
            {
                SendAgent.SendFriendUnlockLikeReq(Role.role.GetBaseProperty().m_iPlayerId, lstFriendPID);

            }
            else
            {
                SendAgent.SendFriendLockLikeReq(Role.role.GetBaseProperty().m_iPlayerId, lstFriendPID);
            }   
        }
    }

    /// <summary>
    /// 设置要显示的好友信息
    /// </summary>
    /// <param name="friend"></param>
    public void SetFrindInfo(Friend friend)
    {
        m_cFriend = friend;
    }

    //是否取消好友弹出框//
    public void SelectReliveveFriend(bool isSure)
    {
        if (isSure)
        {
            SendAgent.SendFriendDel(Role.role.GetBaseProperty().m_iPlayerId, m_cFriend.m_iID);
            Debug.Log("isSure_" + isSure);
        }
        else
        {
            Debug.Log("isSure_" + isSure);
        }
        
    }

    //刷新//
    public void ReflashBtn()
    {
        //设置按钮状态//
        if (m_cFriend.m_bLike)
        {
            m_cBlack.SetActive(true);
            m_cLike.SetActive(false);
            m_cUnlock.SetActive(true);
        }
        else
        {
            m_cBlack.SetActive(false);
            m_cLike.SetActive(true);
            m_cUnlock.SetActive(false);
        }
    }
}