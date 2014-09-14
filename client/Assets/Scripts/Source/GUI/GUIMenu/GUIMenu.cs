//Micro.Sanvey
//2013.11.26
//sanvey.china@gmail.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;

/// <summary>
/// Menu菜单GUI
/// </summary>
public class GUIMenu : GUIBase
{

    #region ----------Property---------

    private const string RES_MAIN = "GUI_Menu";         //菜单资源地址
    private const string BTN_CANCEL = "Title_Cancel/Btn_Cancel";        //取消按钮地址
    private const string PAN_CANCEL = "Title_Cancel";        //取消Pan地址
    private const string BTN_ABOUT = "PanInfo/Btn_About";   //关于按钮地址
    private const string BTN_FESTA = "PanInfo/Btn_Festa";  //特典按钮地址
    private const string BTN_GOODATLAS = "PanInfo/Btn_GoodAtlas";  //物品图鉴地址
    private const string BTN_HELP = "PanInfo/Btn_Help";  //帮助按钮地址
    private const string BTN_HEROATLAS = "PanInfo/Btn_HeroAtlas"; //英雄图鉴地址
    private const string BTN_EXCHANGECODE = "PanInfo/Btn_ExchangeCode"; //兑换码按钮地址
    private const string BTN_USERINFO = "PanInfo/Btn_UserInfo"; //角色信息地址
    private const string BTN_NOTICE = "PanInfo/Btn_Notice"; //通知按钮地址
    private const string BTN_RECORD = "PanInfo/Btn_Record"; //战绩按钮地址
    private const string BTN_SETTING = "PanInfo/Btn_Setting"; //设定按钮地址
    private const string PAN_RIGHT = "PanInfo";  //滑出Panel地址

    private GameObject m_cPanSlide;   //panel滑动
    private GameObject m_cBtnCancel;  //取消按钮Panel

    private bool m_bHasShow = false;  //加载过showobject

    public GUIMenu(GUIManager mgr)
        : base(mgr, GUI_DEFINE.GUIID_MENU, GUILAYER.GUI_PANEL)
    {
    }

    #endregion

    #region ----------Override----------

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
            var cancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_CANCEL);
            GUIComponentEvent gui_event = cancel.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnCancel_OnEvent);
            this.m_cBtnCancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_CANCEL);
            //关于按钮
            var about = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_ABOUT);
            gui_event = about.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnAbout_OnEvent);
            //特典按钮
            var festa = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_FESTA);
            gui_event = festa.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnFesta_OnEvent);
            //物品图鉴
            var goodaltas = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_GOODATLAS);
            gui_event = goodaltas.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnGoodAltas_OnEvent);
            //帮助按钮
            var help = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_HELP);
            gui_event = help.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnHelp_OnEvent);
            //英雄图鉴
            var heroatlas = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_HEROATLAS);
            gui_event = heroatlas.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnHeroAtlas_OnEvent);
            //情报
            var interlli = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_EXCHANGECODE);
            gui_event = interlli.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnExchangeCode_OnEvent);
            interlli.GetComponent<UIImageButton>().isEnabled = true;
            //最新战绩
            var userinfo = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_USERINFO);
            gui_event = userinfo.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnUserInfo_OnEvent);
            //通知
            var notice = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_NOTICE);
            gui_event = notice.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnNotice_OnEvent);
            //战绩
            var record = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_RECORD);
            gui_event = record.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnRecord_OnEvent);
            //设定
            var setting = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_SETTING);
            gui_event = setting.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnSetting_OnEvent);

        }

        this.m_cGUIMgr.SetCurGUIID(this.ID);

        CTween.TweenPosition(this.m_cBtnCancel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-540, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cPanSlide, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);

        SetLocalPos(Vector3.zero);
        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_MAIN_MENU));

    }

    /// <summary>
    /// 立即隐藏
    /// </summary>
    public override void HidenImmediately()
    {
        //base.Hiden();

        //GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        //this.m_cPanSlide.transform.localPosition = new Vector3(640, 0, 0);
        //this.m_cBtnCancel.transform.localPosition = new Vector3(-540, 270, 0);
        
        base.HidenImmediately();
        Destory();
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //base.Hiden();

        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        CTween.TweenPosition(this.m_cPanSlide, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0));
        CTween.TweenPosition(this.m_cBtnCancel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-540, 270, 0) , Destory);

        ResourceMgr.UnloadUnusedResources();
    }


    #endregion

    #region ----------Event----------

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
    /// 关于点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnAbout_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            GUIProductionStaff gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_PRODUCTION_STAFF) as GUIProductionStaff;
            gui.Show();
        }
    }

    /// <summary>
    /// 特典点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnFesta_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            GUIFesta festa = (GUIFesta)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FESTA);
            festa.Show();
        }
    }

    /// <summary>
    /// 物品图鉴事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnGoodAltas_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            GUIPropsAtlas propsAtlas = (GUIPropsAtlas)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_PROPSATLAS);
            propsAtlas.Show();
        }
    }

    /// <summary>
    /// 帮助点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnHelp_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            GUIHelp help = (GUIHelp)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HELP);
            help.Show();
        }
    }

    /// <summary>
    /// 英雄图鉴点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnHeroAtlas_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            GUIHeroAltas heroaltas = (GUIHeroAltas)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HEROALTAS);
            heroaltas.Show();
        }
    }

    /// <summary>
    /// 兑换码点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnExchangeCode_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            GUI_FUNCTION.MESSAGEM(null, "暂未开通");
           
            //Hiden();

            //GUIIntelligence intelligence = (GUIIntelligence)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_INTELLIGENCE);
            //intelligence.Show();
        }
    }

    /// <summary>
    /// 角色信息点击
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnUserInfo_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            HidenImmediately();

            GUIUserInfo userinfo = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_USERINFO) as GUIUserInfo;
            userinfo.Show();

            //更改为直接显示玩家情报隐藏账号管理
    //        GUIAccount ga = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_ACCOUNT) as GUIAccount;
    //        ga.Show();
    //        ga.Box.enabled = false;
        }
    }

    /// <summary>
    /// 通知点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnNotice_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            GUIAnnouncement announcement = (GUIAnnouncement)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_ANNOUNCEMENT);
            announcement.Show();
        }
    }

    /// <summary>
    /// 战绩点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnRecord_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            SendAgent.SendBattleRecord(Role.role.GetBaseProperty().m_iPlayerId);
        }
    }

    /// <summary>
    /// 设定点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnSetting_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            GUISetting setting = (GUISetting)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_SETTING);
            setting.Show();
        }
    }


    #endregion

}