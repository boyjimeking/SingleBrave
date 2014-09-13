////Micro.Sanvey
////2013.12.6
////sanvey.china@gmail.com

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using UnityEngine;
//using Game.Resource;

///// <summary>
///// 好友礼物界面GUI
///// </summary>
//public class GUIFriendGiftB : GUIBase
//{

//    #region ----------Property---------

//    private const string RES_MAIN = "GUI_FriendGiftB";                     //主资源地址
//    private const string BTN_CANCEL = "Title_Cancel/Btn_Cancel";          //取消按钮地址
//    private const string PAN_CANCEL = "Title_Cancel";                     //取消Pan地址
//    private const string PAN_RIGHT = "PanInfo";                           //滑出Panel地址
//    private const string RES_TABLE = "PanInfo/Panel/Table";               //Table地址
//    private const string BTN_ACCEPT = "PanInfo/BtnAccept";                //收取按钮地址
//    private const string BTN_ALLGIVE = "PanInfo/PanTitle/BtnGiveAll";     //全部接受按钮地址
//    private const string SPR_ARROWLEFT = "PanInfo/PanTitle/ArrowLeft";    //向左滑动特效
//    private const string SPR_ARROWRIGHT = "PanInfo/PanTitle/ArrowRight";  //向右滑动特效
//    private const string LB_FILTER = "PanInfo/PanTitle/LbFilter";         //筛选条件地址
//    private const string LB_GIFTNAME = "PanInfo/PanTitle/LbGiftName";     //礼物名称地址
//    private const string LB_GIFTINFO = "PanInfo/PanTitle/LbGiftInfo";     //礼物信息地址
//    private const string CH_FILTER = "PanInfo/PanTitle/CheckBox";         //筛选按钮地址

//    private GameObject m_cPanSlide;       //panel滑动
//    private GameObject m_cBtnCancel;      //取消按钮Panel
//    private UITable m_cTable;             //table
//    private UISprite m_cSprArrowLeft;     //向左滑动特效
//    private UISprite m_cSprArrowRight;    //向右滑动特效
//    private UILabel m_cLBFilter;          //筛选玩家信息
//    private UILabel m_cLBName;            //礼物名称
//    private UILabel m_cLBInfo;            //礼物信息
//    private UIToggle m_cChFilter;         //筛选按钮

//    private TDAnimation m_cEffectLeft; //特效类
//    private TDAnimation m_cEffectRight; //特效类
//    private List<GUIFriendGiftBItem> m_clstFriends = new List<GUIFriendGiftBItem>();

//    public GUIFriendGiftB(GUIManager mgr)
//        : base(mgr, GUI_DEFINE.GUIID_FRIENDGIFTB, GUILAYER.GUI_PANEL)
//    {
//    }

//    #endregion

//    #region ----------Override----------

//    /// <summary>
//    /// 初始化
//    /// </summary>
//    public override void Initialize()
//    {
//        base.Initialize();
//    }

//    /// <summary>
//    /// 销毁
//    /// </summary>
//    public override void Destory()
//    {
//        m_clstFriends.Clear();
//        base.Destory();
//    }

//    /// <summary>
//    /// 展示
//    /// </summary>
//    public override void Show()
//    {
//        base.Show();

//        if (this.m_cGUIObject == null)
//        {
//            //实例化GameObject
//            //Main主资源
//            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_GUI_PATH, RES_MAIN)) as GameObject;
//            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
//            this.m_cGUIObject.transform.localScale = Vector3.one;
//            //滑出动画panel
//            this.m_cPanSlide = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_RIGHT);
//            //取消按钮
//            var cancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_CANCEL);
//            GUIComponentEvent gui_event = cancel.AddComponent<GUIComponentEvent>();
//            gui_event.AddIntputDelegate(BtnCancel_OnEvent);
//            this.m_cBtnCancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_CANCEL);
//            //table
//            m_cTable = GUI_FINDATION.GET_OBJ_COMPONENT<UITable>(this.m_cGUIObject, RES_TABLE);
//            //收取按钮
//            var btnaccept = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_ACCEPT);
//            gui_event = btnaccept.AddComponent<GUIComponentEvent>();
//            gui_event.AddIntputDelegate(Accept_OnEvent);
//            //全部赠予
//            var btnallgive = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_ALLGIVE);
//            gui_event = btnallgive.AddComponent<GUIComponentEvent>();
//            gui_event.AddIntputDelegate(AllGive_OnEvent);


//            var frilst = Role.role.GetFriendProperty().GetAll();
//            for (int i = 0; i < frilst.Count; i++)
//            {
//                GUIFriendGiftBItem tmp = new GUIFriendGiftBItem();
//                tmp.m_cItem.transform.parent = m_cTable.gameObject.transform;
//                tmp.m_cItem.transform.localScale = Vector3.one;
//                tmp.m_cLbName.text = frilst[i].m_strName;

//                gui_event = tmp.m_cBtnGive.AddComponent<GUIComponentEvent>();
//                gui_event.AddIntputDelegate(ItemSelect_OnEvent,i);

//                m_clstFriends.Add(tmp);
//            }
//            m_cTable.Reposition();

//            //左右导航
//            this.m_cSprArrowLeft = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SPR_ARROWLEFT);
//            gui_event = this.m_cSprArrowLeft.gameObject.AddComponent<GUIComponentEvent>();
//            gui_event.AddIntputDelegate(Left_OnEvent);
//            this.m_cEffectLeft = new TDAnimation(m_cSprArrowLeft.atlas, m_cSprArrowLeft); //左右导航
//            this.m_cSprArrowRight = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SPR_ARROWRIGHT);
//            gui_event = this.m_cSprArrowRight.gameObject.AddComponent<GUIComponentEvent>();
//            gui_event.AddIntputDelegate(Right_OnEvent);
//            this.m_cEffectRight = new TDAnimation(m_cSprArrowRight.atlas, m_cSprArrowRight);
//            //筛选
//            this.m_cLBFilter = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LB_FILTER);
//            //名称
//            this.m_cLBName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LB_GIFTNAME);
//            //信息
//            this.m_cLBInfo = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LB_GIFTINFO);
//            //筛选按钮
//            this.m_cChFilter = GUI_FINDATION.GET_OBJ_COMPONENT<UIToggle>(this.m_cGUIObject, CH_FILTER);
//            EventDelegate.Add(this.m_cChFilter.onChange, delegate()
//            {
//                Debug.Log(this.m_cChFilter.value ? "选中" : "没有选中");
//            });
//        }

//        this.m_cEffectLeft.Play("ArrowLeft", Game.Base.TDAnimationMode.Loop, 0.4F);
//        this.m_cEffectRight.Play("ArrowRight", Game.Base.TDAnimationMode.Loop, 0.4F);

//        this.m_cGUIMgr.SetCurGUIID(this.ID);

//        CTween.TweenPosition(this.m_cBtnCancel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-540, 270, 0), new Vector3(0, 270, 0));
//        CTween.TweenPosition(this.m_cPanSlide, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);

//        SetLocalPos(Vector3.zero);
//    }

//    /// <summary>
//    /// 隐藏
//    /// </summary>
//    public override void Hiden()
//    {
//        base.Hiden();

//        CTween.TweenPosition(this.m_cPanSlide, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0));
//        CTween.TweenPosition(this.m_cBtnCancel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-540, 270, 0));
//    }

//    /// <summary>
//    /// 逻辑更新
//    /// </summary>
//    /// <returns></returns>
//    public override bool Update()
//    {
//        base.Update();

//        if (!IsShow()) return false;

//        if (this.m_cEffectLeft != null)
//        {
//            this.m_cEffectLeft.Update();
//        }
//        if (this.m_cEffectRight != null)
//        {
//            this.m_cEffectRight.Update();
//        }

//        return true;
//    }

//    #endregion

//    #region ----------Event----------

//    /// <summary>
//    /// 取消点击事件
//    /// </summary>
//    /// <param name="info"></param>
//    /// <param name="args"></param>
//    public void BtnCancel_OnEvent(GUI_INPUT_INFO info, object[] args)
//    {
//        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
//        {
//            GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

//            Hiden();

//            GUIFriendGiftSelect friselect = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FRIENDGIFTSELECT) as GUIFriendGiftSelect;
//            friselect.Show();
//        }
//    }

//    /// <summary>
//    /// 赠送按钮点击事件
//    /// </summary>
//    /// <param name="info"></param>
//    /// <param name="args"></param>
//    public void ItemSelect_OnEvent(GUI_INPUT_INFO info, object[] args)
//    {
//        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
//        {
//            var frilst = Role.role.GetFriendProperty().GetAll();
//            Friend fri = frilst[(int)args[0]];
//            if (fri != null)
//            {
//                Debug.Log("赠送好友" + fri.m_strName + "的礼物");
//            }
//        }
//    }

//    /// <summary>
//    /// 接受按钮点击事件
//    /// </summary>
//    /// <param name="info"></param>
//    /// <param name="args"></param>
//    public void Accept_OnEvent(GUI_INPUT_INFO info, object[] args)
//    {
//        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
//        {
//            Hiden();

//            GUIFriendGift frigift = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FRIENDGIFT) as GUIFriendGift;
//            frigift.Show();
//        }
//    }

//    /// <summary>
//    /// 全部给予点击事件
//    /// </summary>
//    /// <param name="info"></param>
//    /// <param name="args"></param>
//    public void AllGive_OnEvent(GUI_INPUT_INFO info, object[] args)
//    {
//        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
//        {

//        }
//    }

//    /// <summary>
//    /// 向左点击事件
//    /// </summary>
//    /// <param name="info"></param>
//    /// <param name="args"></param>
//    public void Left_OnEvent(GUI_INPUT_INFO info, object[] args)
//    {
//        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
//        {

//        }
//    }

//    /// <summary>
//    /// 向右点击事件
//    /// </summary>
//    /// <param name="info"></param>
//    /// <param name="args"></param>
//    public void Right_OnEvent(GUI_INPUT_INFO info, object[] args)
//    {
//        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
//        {

//        }
//    }

//    #endregion



//}

///// <summary>
///// 好友接受礼物列表项
///// </summary>
//public class GUIFriendGiftBItem
//{
//    private const string RES_ITEM = "GUI_FriendGiftBItem";   //Item主资源
//    private const string BTN_GIVE = "BtnGive";               //接受按钮
//    private const string LB_NAME = "LbGiftName";             //礼物名称

//    public GameObject m_cItem;
//    public GameObject m_cBtnGive;
//    public UILabel m_cLbName;

//    public GUIFriendGiftBItem()
//    {
//        m_cItem = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_GUI_PATH, RES_ITEM)) as GameObject;
//        m_cBtnGive = GUI_FINDATION.GET_GAME_OBJECT(this.m_cItem, BTN_GIVE);
//        m_cLbName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cItem, LB_NAME);

//    }
//}