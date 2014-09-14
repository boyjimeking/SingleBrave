//Micro.Sanvey
//2013.12.4
//sanvey.china@gmail.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;
using Game.Media;

/// <summary>
/// 好友菜单GUI
/// </summary>
public class GUIFriendMenu : GUIBase
{

    private const string RES_MAIN = "GUI_FriendMenu";                   //菜单资源地址
    private const string BTN_CANCEL = "Title_Cancel/Btn_Cancel";        //取消按钮地址
    private const string PAN_CANCEL = "Title_Cancel";                   //取消Pan地址
    private const string BTN_FRIENDAPPLY = "PanInfo/BtnFriendApply";    //好友申请按钮地址
    private const string BTN_FRIENDGIFT = "PanInfo/BtnFriendGift";      //好友礼物按钮地址
    private const string BTN_FRIENDLIST = "PanInfo/BtnFriendList";      //好友列表按钮地址
    private const string BTN_FRIENDSEARCH = "PanInfo/BtnFriendSearch";  //好友搜索按钮地址
    private const string LAB_APPLYNUM = "PanInfo/BtnFriendApply/Num/Label"; //好友申请数量//
    private const string LAB_GIFTNUM = "PanInfo/BtnFriendGift/Num/Label";   //礼物数量//
    private const string PAN_RIGHT = "PanInfo";  //滑出Panel地址

    private GameObject m_cPanSlide;   //panel滑动
    private GameObject m_cBtnCancel;  //取消按钮Panel
    private UILabel m_labApplyNum;  //好友申请数量
    private UILabel m_labGiftNum;   //礼物数量


    public GUIFriendMenu(GUIManager mgr)
        : base(mgr, GUI_DEFINE.GUIID_FRIENDMENU, GUILAYER.GUI_PANEL)
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
        this.m_labApplyNum = null;
        this.m_labGiftNum = null;

        base.Destory();
    }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        base.Show();

        if (this.m_cGUIObject == null)
        {
            //实例化GameObject
            //Main主资源
            this.m_cGUIObject = GameObject.Instantiate(ResourceMgr.LoadAsset(GAME_DEFINE.RESOURCE_GUI_CACHE, RES_MAIN) as UnityEngine.Object) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;
            //滑出动画panel
            this.m_cPanSlide = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_RIGHT);
            //取消按钮
            GameObject cancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_CANCEL);
            GUIComponentEvent gui_event = cancel.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnCancel_OnEvent);
            this.m_cBtnCancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_CANCEL);
            //好友申请按钮
            GameObject friendapply = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_FRIENDAPPLY);
            gui_event = friendapply.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnFriendApply_OnEvent);
            //好友列表按钮
            GameObject friendlist = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_FRIENDLIST);
            gui_event = friendlist.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnFriendList_OnEvent);
            //好友检索按钮
            GameObject friendsearch = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_FRIENDSEARCH);
            gui_event = friendsearch.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnFriendSearch_OnEvent);
            //好友礼物按钮
            GameObject help = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_FRIENDGIFT);
            gui_event = help.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnFriendGift_OnEvent);

            m_labApplyNum = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cGUIObject, LAB_APPLYNUM);
            m_labGiftNum = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cGUIObject, LAB_GIFTNUM);

        }

        //播放主背景音乐
		MediaMgr.sInstance.PlayBGM(SOUND_DEFINE.BGM_MAIN);
//        MediaMgr.PlayBGM(SOUND_DEFINE.BGM_MAIN);

        this.m_cGUIMgr.SetCurGUIID(this.ID);

        GUIBackFrameBottom bottom = (GUIBackFrameBottom)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM);
        bottom.SetFriendApllyNumber(Role.role.GetBaseProperty().m_iFriendGiftCount + Role.role.GetBaseProperty().m_iFriendApplyCount);

        if (Role.role.GetBaseProperty().m_iFriendApplyCount <= 0)
        {
            m_labApplyNum.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            m_labApplyNum.transform.parent.gameObject.SetActive(true);
            if (Role.role.GetBaseProperty().m_iFriendApplyCount > 99)
            {
                m_labApplyNum.text = "99";
            }
            else
            {
                m_labApplyNum.text = Role.role.GetBaseProperty().m_iFriendApplyCount.ToString();
            }

        }

        if (Role.role.GetBaseProperty().m_iFriendGiftCount <= 0)
        {
            m_labGiftNum.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            m_labGiftNum.transform.parent.gameObject.SetActive(true);
            m_labGiftNum.text = Role.role.GetBaseProperty().m_iFriendGiftCount.ToString();
        }

        CTween.TweenPosition(this.m_cBtnCancel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-540, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cPanSlide, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);

        SetLocalPos(Vector3.zero);

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_MAIN_MENU));
    }

    /// <summary>
    /// 初始化GUI
    /// </summary>
    protected override void InitGUI()
    {
        
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //base.Hiden();

        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        ResourceMgr.UnloadUnusedResources();

        //CTween.TweenPosition(this.m_cPanSlide, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0));
        //CTween.TweenPosition(this.m_cBtnCancel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-540, 270, 0),Destory);
        Destory();
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

            GUIBackFrameBottom backbottom = (GUIBackFrameBottom)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM);
            backbottom.ShowHalf();
            GUIMain guimain = (GUIMain)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_MAIN);
            guimain.Show();

        }
    }

    /// <summary>
    /// 好友申请点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnFriendApply_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {

            Hiden();
            SendAgent.SendFriendGetApplyListReq(Role.role.GetBaseProperty().m_iPlayerId);
            //GUIFriendApply friapp = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FRIENDAPPLY) as GUIFriendApply;
            //friapp.Show();
        }
    }

    /// <summary>
    /// 好友列表点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnFriendList_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {

            Hiden();

            GUIFriendList frilst = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FRIENDLIST) as GUIFriendList;
            frilst.Show();
        }
    }

    /// <summary>
    /// 好友检索点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnFriendSearch_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            GUIFriendSearch frisearch = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FRIENDSEARCH) as GUIFriendSearch;
            frisearch.Show();
        }
    }

    /// <summary>
    /// 好友礼物点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnFriendGift_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            SendAgent.SendFriendGetGiftList(Role.role.GetBaseProperty().m_iPlayerId);

            //GUIFriendGift frigift = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FRIENDGIFT) as GUIFriendGift;
            //frigift.Show();
        }
    }

}