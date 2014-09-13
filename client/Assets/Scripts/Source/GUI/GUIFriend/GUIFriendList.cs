//  GUIFriendList.cs
//  Author: Cheng Xia
//  2013-1-6

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;

/// <summary>
/// 好友列表界面GUI
/// </summary>
public class GUIFriendList : GUIBase
{
    private const string RES_MAIN = "GUI_FriendList";                   //菜单资源地址
    private const string BTN_CANCEL = "Title_Cancel/Btn_Cancel";        //取消按钮地址
    private const string PAN_CANCEL = "Title_Cancel";                   //取消Pan地址
    private const string PAN_RIGHT = "PanInfo";                         //滑出Panel地址
    private const string LB_LSTCOUNT = "PanInfo/Lb_LstCount";           //好友数量地址
    private const string RES_TABLE = "PanInfo/Panel/Table";             //Table地址
    private const string PANEL_PARENT = "PanInfo/Panel";    //拖拽的父节点

    private UIPanel m_cPanelParent; //面板拖拽父节点
    private GameObject m_cPanSlide;       //panel滑动
    private GameObject m_cBtnCancel;      //取消按钮Panel
    private UITable m_cTable;             //table
    private UILabel m_cLbLstCount;        //好友数量
    private List<FriendListItem> m_lstFriends=new List<FriendListItem>();

    public GUIFriendList(GUIManager mgr)
        : base(mgr, GUI_DEFINE.GUIID_FRIENDLIST, GUILAYER.GUI_PANEL)
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

        if (this.m_lstFriends != null)
        {
            m_lstFriends.Clear();
        }
        
        if (this.m_lstFriends != null)
        {
            foreach (FriendListItem fli in m_lstFriends)
            {
                fli.DestoryObj();
            }
        }

        this.m_cPanelParent = null;
        this.m_cPanSlide = null;
        this.m_cBtnCancel = null;
        this.m_cTable = null;
        this.m_cLbLstCount = null;

        base.Destory();
    }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        this.m_eLoadingState = LOADING_STATE.START;
        GUI_FUNCTION.AYSNCLOADING_SHOW();
        ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, FriendListItem.RES_ITEM);
        ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, FriendListItem.RES_PREVIEWITEM);
        if (this.m_cGUIObject == null)
        {
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_MAIN);
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
            //取消按钮
            var cancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_CANCEL);
            GUIComponentEvent gui_event = cancel.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnCancel_OnEvent);
            this.m_cBtnCancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_CANCEL);
            //table
            m_cTable = GUI_FINDATION.GET_OBJ_COMPONENT<UITable>(this.m_cGUIObject, RES_TABLE);
            //显示好友数量字符
            this.m_cLbLstCount = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LB_LSTCOUNT);
            this.m_cPanelParent = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(this.m_cGUIObject, PANEL_PARENT);
        }

        CreateGUI();

        this.m_cGUIMgr.SetCurGUIID(this.ID);

        CTween.TweenPosition(this.m_cBtnCancel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-540, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cPanSlide, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);

        SetLocalPos(Vector3.zero);

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_FRIEND_LIST));
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //base.Hiden();

        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        ResourcesManager.GetInstance().UnloadUnusedResources();

        CTween.TweenPosition(this.m_cPanSlide, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0));
        CTween.TweenPosition(this.m_cBtnCancel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-540, 270, 0),Destory);
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
            GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

            Hiden();

            GUIFriendMenu frimenu = (GUIFriendMenu)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FRIENDMENU);
            frimenu.Show();
        }
    }

    /// <summary>
    /// 好友选中事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void ItemSelect_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            List<Friend>  frilst = Role.role.GetFriendProperty().GetAll();
            Friend fri = frilst[(int)args[0]];
            if (fri!=null)
            {

                GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

                Hiden();

                GUIFriendInfoLike frilike = (GUIFriendInfoLike)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FRIENDINFOLIKE);
                frilike.SetFrindInfo(fri);
                frilike.Show();
            }
        }
    }

    /// <summary>
    /// 创建GUI
    /// </summary>
    private void CreateGUI()
    {
        this.m_cPanelParent.transform.localPosition = new Vector3(0, -18, 0);
        this.m_cPanelParent.clipRange = new Vector4(0, -45, this.m_cPanelParent.clipRange.z, this.m_cPanelParent.clipRange.w);

        foreach (FriendListItem fli in m_lstFriends)
        {
            fli.DestoryObj();
        }

        m_lstFriends.Clear();

        List<Friend> frilst = Role.role.GetFriendProperty().GetAll();
        for (int i = 0; i < frilst.Count; i++)
        {
            FriendListItem tmp = new FriendListItem();

            tmp.m_cItem.transform.parent = m_cTable.gameObject.transform;
            tmp.m_cItem.transform.localScale = Vector3.one;
            tmp.SetMonsterInfo(frilst[i]);

            tmp.SetItemInfo(frilst[i]);

            GUIComponentEvent lst_event = tmp.m_cItem.AddComponent<GUIComponentEvent>();
            lst_event.AddIntputDelegate(ItemSelect_OnEvent, i);

            m_lstFriends.Add(tmp);
        }

        m_cTable.repositionNow = true;
        RoleExpTable expTable = RoleExpTableManager.GetInstance().GetRoleExpTable(Role.role.GetBaseProperty().m_iLevel);
        this.m_cLbLstCount.text = frilst.Count + "/" + expTable.MaxFriend;  //显示数量
    }
}

/// <summary>
/// 好友显示列表项
/// </summary>
public class FriendListItem
{
    public const string RES_ITEM = "GUI_FriendListItem";   //Item主资源
    public const string RES_MONSTERITEM = "GUI_MonsterItem";   //好友怪物属性
    public const string RES_PREVIEWITEM = "GUI_FriendPreviewItem";//期望礼物资源地址

    private const string LAB_LASTTIME = "LabLastTime";       //最后登录时间
    private const string LAB_SIGN = "LabSign";               //签名
    private const string LAB_LV = "LabLv";             //好友等级//
    private const string LAB_NAME = "LabName";  //名字//
    private const string LAB_BOTTOM = "LabBottom";  //怪物等级//
    private const string OBJ_LIKE = "ItemLike"; //是否喜欢//
    private const string SP_BORDER = "ItemBorder"; //边框//
    private const string SP_MONSTER = "ItemMonster";   //怪物头像//
    private const string SP_FRAME = "ItemFrame";   //背景//
    private const string SP_ICON = "ItemIcon";  //物体图标//

    private const string SPR_BORDER = "ItemBorder"; //边框//
    private const string SPR_FRAME = "ItemFrame";   //背景//
    private const string SPR_ICON = "ItemIcon";  //物体图标//
    

    public GameObject m_cItem;
    private GameObject m_cMonster;
    private UILabel m_labLastTime;
    private UILabel m_labSign;
    private UILabel m_labLv;
    private UILabel m_labBottom;
    private UILabel m_labName;
    private GameObject m_cLike;
    private UISprite m_spBorder;
    private UISprite m_spMonster;
    private UISprite m_spFrame;
    private UnityEngine.Object m_cPreviewItem;//期望礼物

    private List<GameObject> m_lstPreviewItem = new List<GameObject>();//期望礼物列表

    public FriendListItem()
    {
        m_cItem = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_ITEM)) as GameObject;
        m_cMonster = GUI_FINDATION.GET_GAME_OBJECT(m_cItem,RES_MONSTERITEM);
        m_labLastTime = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cMonster, LAB_LASTTIME);
        m_labSign = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cMonster, LAB_SIGN);
        m_labLv = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cMonster, LAB_LV);
        m_labName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cMonster, LAB_NAME);
        m_labBottom = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cMonster, LAB_BOTTOM);
        m_cLike = GUI_FINDATION.GET_GAME_OBJECT(m_cMonster, OBJ_LIKE);
        m_spBorder = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cMonster, SP_BORDER);
        m_spMonster = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cMonster, SP_MONSTER);
        m_spFrame = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cMonster, SP_FRAME);
        m_cPreviewItem = (UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_PREVIEWITEM);
    }

    //设置人物属性//
    public void SetMonsterInfo(Friend friend)
    {
        m_labLastTime.text = GetLastTimeString( GAME_FUNCTION.UNIXTimeToCDateTime(friend.m_lLastLoginTime) );
        m_labSign.text = AthleticsExpTableManager.GetInstance().GetAthleticsNameByPoint(friend.m_iAthleticPoint);
        m_labLv.text = "Lv." + friend.m_iLevel;
        m_labName.text = friend.m_strName;

        if (friend.m_bLike)
        {
            m_cLike.SetActive(true);
        }
        else
        {
            m_cLike.SetActive(false);
        }

        Hero hero = friend.m_cHero;
        HeroTable table = HeroTableManager.GetInstance().GetHeroTable(hero.m_iTableID);
        m_labBottom.text = "Lv." + hero.m_iLevel;
        GUI_FUNCTION.SET_AVATORS(m_spMonster, table.AvatorMRes);
        GUI_FUNCTION.SET_HeroBorderAndBack(m_spBorder, m_spFrame, (Nature)table.Property);
    }

    //设置物品//
    public void SetItemInfo(Friend friend)
    {
        for (int i = 0; i < friend.m_lstWantGift.Length; i++)
        {
            if (friend.m_lstWantGift[i] != 0)
            {
            GameObject previewItem = GameObject.Instantiate(this.m_cPreviewItem) as GameObject;
            previewItem.transform.parent = m_cItem.transform;
            previewItem.transform.localScale = Vector3.one;
            previewItem.transform.localPosition = new Vector3(-153 + i * 50,-26,0);

            UISprite sprBorder = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(previewItem, SPR_BORDER);
            UISprite sprFrame = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(previewItem, SPR_FRAME);
            UISprite sprIcon = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(previewItem, SPR_ICON);

            FriendGiftItem itemTable = FriendGiftItemTableManager.GetInstance().GetGiftItem(friend.m_lstWantGift[i]);

            switch (itemTable.m_eType)
            {
                case GiftType.Diamond:
                    sprFrame.spriteName = "frame1";
                    sprIcon.spriteName = itemTable.m_strSpiritName;
                    break;
                case GiftType.Gold:
                    sprFrame.spriteName = "frame1";
                    sprIcon.spriteName = itemTable.m_strSpiritName;
                    break;
                case GiftType.FarmPoint:
                    sprFrame.spriteName = "frame1";
                    sprIcon.spriteName = itemTable.m_strSpiritName;
                    break;
                case GiftType.FriendPoint:
                    sprFrame.spriteName = "frame1";
                    sprIcon.spriteName = itemTable.m_strSpiritName;
                    break;
                case GiftType.Item:
                    sprFrame.spriteName = "getsucai";
                    sprIcon.spriteName = itemTable.m_strSpiritName;
                    break;
                default:
                    break;
            }
            //GUI_FUNCTION.SET_ITEM_BORDER(sprBorder, (ITEM_TYPE)itemTable.m_eType);
            //GUI_FUNCTION.SET_ITEMS(sprIcon, itemTable.m_strSpiritName);

            this.m_lstPreviewItem.Add(previewItem);
           }
        }
    }

    //销毁//
    public void DestoryObj()
    {
        m_cItem = null;
        m_cMonster = null;
        m_labLastTime = null;
        m_labSign = null;
        m_labLv = null;
        m_labBottom = null;
        m_labName = null;
        m_cLike = null;
        m_spBorder = null;
        m_spMonster = null;
        m_spFrame = null;
        m_cPreviewItem = null;//期望礼物

        if (this.m_lstPreviewItem != null)
        {
            foreach (GameObject obj in this.m_lstPreviewItem)
            {
                GameObject.Destroy(obj);
            }

            this.m_lstPreviewItem.Clear();
        }
    }

    public string GetLastTimeString(DateTime dateTime)
    {
        var years = DateTime.Now.Year - dateTime.Year;
        if (years > 0)
        {
            if (DateTime.Now.Month - dateTime.Month >= 0)
                return "最后登录时间：" + years + "年前";
            else if(DateTime.Now.Month - dateTime.Month < 0 && years > 1)
                return "最后登录时间：" + (years - 1) + "年前";
            else if (DateTime.Now.Day - dateTime.Day >= 0 && DateTime.Now.Month - dateTime.Month < 0 && years == 1)
                return "最后登录时间：" + (DateTime.Now.Month - dateTime.Month + 12) + "月前";
            else if(DateTime.Now.Day - dateTime.Day < 0 && DateTime.Now.Month - dateTime.Month < 0 && years == 1)
                return "最后登录时间：" + (DateTime.DaysInMonth(dateTime.Year, dateTime.Month) + DateTime.Now.Day - dateTime.Day) + "天前";
        }
        var months = DateTime.Now.Month - dateTime.Month;
        if (months > 0)
        {
            if (DateTime.Now.Day - dateTime.Day >= 0)
                return "最后登录时间：" + months + "月前";
            else
                return "最后登录时间: " + (DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) + DateTime.Now.Day - dateTime.Day) + "天前";
        }
        var days = DateTime.Now.Day - dateTime.Day;
        if (days > 0)
        {
            return "最后登录时间：" + days + "天前";
        }
        var hours = DateTime.Now.Hour - dateTime.Hour;
        if (hours > 0)
        {
            return "最后登录时间：" + hours + "小时前";
        }
        var mins = DateTime.Now.Minute - dateTime.Minute;
        if (mins > 0)
        {
            return "最后登录时间：" + mins + "分钟前";
        }
        var second = DateTime.Now.Second - dateTime.Second;
        if (second < 0)
            second = 0;
        return "最后登录时间：" + second + "秒钟前";
    }
}