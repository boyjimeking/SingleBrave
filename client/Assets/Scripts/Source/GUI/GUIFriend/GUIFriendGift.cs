//  GUIFriendGift.cs
//  Author: Cheng Xia
//  2013-1-10

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;

/// <summary>
/// 好友礼物界面GUI
/// </summary>
public class GUIFriendGift : GUIBase
{
    private const string RES_MAIN = "GUI_FriendGift";                   //主资源地址
    private const string BTN_CANCEL = "Title_Cancel/Btn_Cancel";        //取消按钮地址
    private const string PAN_CANCEL = "Title_Cancel";                   //取消Pan地址
    private const string PAN_RIGHT = "PanInfo";                         //滑出Panel地址
    private const string RES_TABLE = "PanInfo/Panel/Table";             //Table地址
    private const string LIST_VIEW = "PanInfo/Panel";             //滚动视图地址
    private const string BTN_GIVE = "PanInfo/BtnGive";                  //赠送按钮地址
    private const string BTN_ALLACCEPT = "PanInfo/PanTitle/BtnAcceptAll";  //全部接受按钮地址

    private GameObject m_cPanSlide;       //panel滑动
    private GameObject m_cBtnCancel;      //取消按钮Panel
    private UITable m_cTable;             //table
    private GameObject m_cListView;//滚动视图

    private List<GUIFriendGiftGetItem> m_clstFriends = new List<GUIFriendGiftGetItem>();

    public bool m_bSend;   //是否有数据发送
    public List<int> m_lstRceiveGift = new List<int>(); //接收礼物
    private FriendGiftItem[] m_clstItems = new FriendGiftItem[3];      //期望礼物位置//
    private PreviewItem[] m_clstPreviewItems = new PreviewItem[3];  //期望礼物对象

    private Vector3 PAN_LOACL_POS = new Vector3(-308f, -10.19995f, 0f);
    private Vector4 PAN_CLIP = new Vector4(320f, -154.8f, 640f, 309.6f);


    public int m_iSelectPos;    //选择期望礼物的位置


    public GUIFriendGift(GUIManager mgr)
        : base(mgr, GUI_DEFINE.GUIID_FRIENDGIFT, GUILAYER.GUI_PANEL)
    {
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();
        if (this.m_clstFriends != null)
        {
            m_clstFriends.Clear();
        }
        
        this.m_cPanSlide = null;
        this.m_cBtnCancel = null;
        this.m_cTable = null;
        this.m_cListView = null;

        base.Destory();
    }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        this.m_eLoadingState = LOADING_STATE.START;
        GUI_FUNCTION.AYSNCLOADING_SHOW();
        ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + GUIFriendGiftGetItem.RES_ITEM);

        if (this.m_cGUIObject == null)
        {
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_MAIN);
        }
    }

    protected override void InitGUI()
    {
        base.Show();
        GUI_FUNCTION.AYSNCLOADING_HIDEN();
        if (this.m_cGUIObject == null)
        {
            //实例化GameObject
            //Main主资源
            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;
            //滑出动画panel
            this.m_cPanSlide = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_RIGHT);
            //取消按钮
            GameObject cancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_CANCEL);
            GUIComponentEvent gui_event = cancel.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnCancel_OnEvent);
            this.m_cBtnCancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_CANCEL);
            //table
            m_cTable = GUI_FINDATION.GET_OBJ_COMPONENT<UITable>(this.m_cGUIObject, RES_TABLE);

            m_cListView = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LIST_VIEW);

            //赠送按钮
            GameObject btngive = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_GIVE);
            gui_event = btngive.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(Give_OnEvent);
            //全部收取
            GameObject btnallacc = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_ALLACCEPT);
            gui_event = btnallacc.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(AllAccept_OnEvent);

            for (int i = 0; i < m_clstPreviewItems.Length; i++)
            {
                PreviewItem pi = new PreviewItem(m_cPanSlide, i);
                gui_event = pi.m_cBtnCancel.AddComponent<GUIComponentEvent>();
                gui_event.AddIntputDelegate(PreviewCancel_OnEvent, i);
                gui_event = pi.m_cBtnLogin.AddComponent<GUIComponentEvent>();
                gui_event.AddIntputDelegate(PreviewLogin_OnEvent, i);
                m_clstPreviewItems[i] = pi;
            }
        }

        this.m_cListView.transform.localPosition = PAN_LOACL_POS;
        UIPanel panel = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(this.m_cGUIObject, LIST_VIEW);
        panel.clipRange = PAN_CLIP;

        CreateGUI();

        this.m_cGUIMgr.SetCurGUIID(this.ID);

        CTween.TweenPosition(this.m_cBtnCancel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-540, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cPanSlide, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);

        SetLocalPos(Vector3.zero);

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_RECEIVE_GIFT));
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        if (m_bSend)
        {
            m_bSend = false;
            SendAgent.SendFriendAcceptGift(Role.role.GetBaseProperty().m_iPlayerId, Role.role.GetBaseProperty().m_vecWantItems, this.m_lstRceiveGift);
            foreach (int gid in m_lstRceiveGift)
            {
                Role.role.GetFriendProperty().RemoveFriendGift(gid);
            }
        }

        if (this.m_clstFriends != null)
        {
            Role.role.GetBaseProperty().m_iFriendGiftCount = this.m_clstFriends.Count;
        }

        SendAgent.SendFriendWantGift(Role.role.GetBaseProperty().m_iPlayerId, Role.role.GetBaseProperty().m_vecWantItems);

        //base.Hiden();
        if (!m_bSend)
            GUI_FUNCTION.AYSNCLOADING_HIDEN();

        CTween.TweenPosition(this.m_cPanSlide, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0));
        CTween.TweenPosition(this.m_cBtnCancel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-540, 270, 0),Destory);

		ResourceMgr.UnloadUnusedResources();
        base.Hiden();
        Destory();
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

            if (false)
            {
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FRIENDMENU).Show);
            }
            else
            {
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FRIENDMENU).Show();
            }
        }
    }

    /// <summary>
    /// 赠送按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void Give_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (m_bSend)
            {
                m_bSend = false;
                SendAgent.SendFriendAcceptGift(Role.role.GetBaseProperty().m_iPlayerId, Role.role.GetBaseProperty().m_vecWantItems, this.m_lstRceiveGift);
                foreach (int gid in m_lstRceiveGift)
                {
                    Role.role.GetFriendProperty().RemoveFriendGift(gid);
                }
            }

            Hiden();

            GUIFriendGiftSelect friselect = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FRIENDGIFTSELECT) as GUIFriendGiftSelect;

            //friselect.SetOldPos(0, 1, 0);

            friselect.Show();

            //下方提示
            GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
            gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_GIVE_GIFT));
        }
    }

    /// <summary>
    /// 全部接受点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void AllAccept_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.m_bSend = true;
            //全部接收
            for (int i = 0; i < this.m_clstFriends.Count; i++)
            {
                this.m_lstRceiveGift.Add(this.m_clstFriends[i].m_iID);
            }

            //清除
            for (int i = 0; i < this.m_clstFriends.Count; i++)
            {
                this.m_clstFriends[i].Destroy();
            }
            this.m_clstFriends.Clear();
        }
    }

    /// <summary>
    /// 接受点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void Accept_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.m_bSend = true;

            int id = (int)args[0];
            this.m_lstRceiveGift.Add(id);
            for (int i = 0; i < this.m_clstFriends.Count; i++)
            {
                if (this.m_clstFriends[i].m_iID == id)
                {
                    this.m_clstFriends[i].Destroy();
                    this.m_clstFriends.RemoveAt(i);
                    break;
                }
            }
            this.m_cTable.repositionNow = true;
        }
    }

    /// <summary>
    /// 取消期望礼物
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void PreviewCancel_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            m_iSelectPos = (int)args[0];

            this.m_bSend = true;
            m_clstItems[m_iSelectPos] = null;
            Role.role.GetBaseProperty().m_vecWantItems[m_iSelectPos] = 0;
            m_clstPreviewItems[m_iSelectPos].SetPreview(null);
        }
    }

    /// <summary>
    /// 选择期望礼物
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void PreviewLogin_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            m_iSelectPos = (int)args[0];

            Hiden();

            GUIFriendGiftExpectSelect friendGiftExpectSelect = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FRIENDGIFTEXPECTSELECT) as GUIFriendGiftExpectSelect;
            friendGiftExpectSelect.Show();
        }
    }


    /// <summary>
    /// 创建GUI
    /// </summary>
    private void CreateGUI()
    {
        this.m_bSend = false;

        this.m_lstRceiveGift.Clear();

        for (int i = 0; i < 3; i++)
        {
            m_clstItems[i] = FriendGiftItemTableManager.GetInstance().GetGiftItem(Role.role.GetBaseProperty().m_vecWantItems[i]);
        }

        foreach (GUIFriendGiftGetItem iChild in m_clstFriends)
        {
            iChild.Destroy();
        }

        m_clstFriends.Clear();

        List<FriendGift> fiendGiftList = Role.role.GetFriendProperty().GetAllGift();
        List<Friend> friendList = Role.role.GetFriendProperty().GetAll();
        for (int i = 0; i < fiendGiftList.Count; i++)
        {
            //判断该礼物的赠送者是否在当前好友列表中 如果存在则显示礼物 不存在不显示
            foreach (Friend f in friendList)
            {

                if (f.m_iID == fiendGiftList[i].m_iFID)
                {
                    GUIFriendGiftGetItem tmp = new GUIFriendGiftGetItem();
                    tmp.m_cItem.transform.parent = m_cTable.gameObject.transform;
                    tmp.m_cItem.transform.localScale = Vector3.one;
                    tmp.SetInfo(fiendGiftList[i]);

                    GUIComponentEvent gui_event = tmp.m_cBtnGet.AddComponent<GUIComponentEvent>();
                    gui_event.AddIntputDelegate(Accept_OnEvent, fiendGiftList[i].m_iID);

                    m_clstFriends.Add(tmp);
                }
            }
        }
        m_cTable.repositionNow = true;

        for (int j = 0; j < m_clstPreviewItems.Length; j++)
        {
            m_clstPreviewItems[j].SetPreview(m_clstItems[j]);
        }

        //UIDraggablePanel panel = GUI_FINDATION.GET_OBJ_COMPONENT<UIDraggablePanel>(this.m_cGUIObject, LIST_VIEW);
        //panel.repositionClipping = true;
    }


}

/// <summary>
/// 好友接受礼物列表项
/// </summary>
public class GUIFriendGiftGetItem
{
    public const string RES_ITEM = "GUI_FriendGiftGetItem";   //Item主资源
    private const string BTN_GET = "Btn_Get";

    private const string MONSTER_SPBORDER = "GUI_MonsterItem/ItemBorder";
    private const string MONSTER_SPFRAME = "GUI_MonsterItem/ItemFrame";
    private const string MONSTER_SPMONSTER = "GUI_MonsterItem/ItemMonster";
    private const string MONSTER_LABFROM = "GUI_MonsterItem/LabFrom";
    private const string MONSTER_LABNAME = "GUI_MonsterItem/LabName";

    private const string ITEM_SPBORDER = "GUI_PreviewItem/ItemBorder";
    private const string ITEM_SPFRAME = "GUI_PreviewItem/ItemFrame";
    private const string ITEM_SPICON = "GUI_PreviewItem/ItemIcon";

    public GameObject m_cItem;
    public GameObject m_cBtnGet;
    private UISprite m_spMonsterBorder;
    private UISprite m_spMonsterFrame;
    private UISprite m_spMonster;
    private UILabel m_labFrom;
    private UILabel m_labName;
    private UISprite m_spItemBorder;
    private UISprite m_spItemFrame;
    private UISprite m_spItemIcon;

    //数据
    public int m_iID;   //礼物ID

    public GUIFriendGiftGetItem()
    {
		m_cItem = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_ITEM)) as GameObject;

        m_cBtnGet = GUI_FINDATION.GET_GAME_OBJECT(m_cItem, BTN_GET);
        m_spMonsterBorder = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, MONSTER_SPBORDER);
        m_spMonsterFrame = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, MONSTER_SPFRAME);
        m_spMonster = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, MONSTER_SPMONSTER);
        m_labFrom = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cItem, MONSTER_LABFROM);
        m_labName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cItem, MONSTER_LABNAME);

        m_spItemBorder = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, ITEM_SPBORDER);
        m_spItemFrame = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, ITEM_SPFRAME);
        m_spItemIcon = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, ITEM_SPICON);
    }

    //设置panel位置//
    public void SetParent(Transform parent)
    {
        m_cItem.transform.parent = parent;
        m_cItem.transform.localPosition = Vector3.zero;
        m_cItem.transform.localScale = Vector3.one;
    }

    //设置信息//
    public void SetInfo(FriendGift fg)
    {
        if(fg.m_cFriend != null)
        {
            this.m_iID = fg.m_iID;
            m_labFrom.text = "来自好友" + "\"" + fg.m_cFriend.m_strName + "\"" + "的礼物";

            //好友单位信息//
            Hero hero = fg.m_cFriend.m_cHero;
            HeroTable table = HeroTableManager.GetInstance().GetHeroTable(hero.m_iTableID);

            if (fg.m_cItem == null)
            {
                //m_cBtnCancel.SetActive(false);
                //m_cBtnLogin.SetActive(true);
            }
            else
            {
                //m_cBtnCancel.SetActive(true);
                //m_cBtnLogin.SetActive(false);

                switch (fg.m_eType)
                {
                    case GiftType.Diamond:
                        m_labName.text = fg.m_strName + " ×" + fg.m_iNum;
                        m_spItemFrame.spriteName = "frame1";
                        m_spItemIcon.spriteName = fg.m_strSprName;
                        break;
                    case GiftType.Gold:
                        m_labName.text = fg.m_strName + " ×" + fg.m_iNum;
                        m_spItemFrame.spriteName = "frame1";
                        m_spItemIcon.spriteName = fg.m_strSprName;
                        break;
                    case GiftType.FarmPoint:
                        m_labName.text = fg.m_strName + " ×" + fg.m_iNum;
                        m_spItemFrame.spriteName = "frame1";
                        m_spItemIcon.spriteName = fg.m_strSprName;
                        break;
                    case GiftType.FriendPoint:
                        m_labName.text = fg.m_strName + " ×" + fg.m_iNum;
                        m_spItemFrame.spriteName = "frame1";
                        m_spItemIcon.spriteName = fg.m_strSprName;
                        break;
                    case GiftType.Item:
                        m_labName.text = fg.m_strName + " ×" + fg.m_iNum;
                        m_spItemFrame.spriteName = "item_frame_2";
                        m_spItemIcon.spriteName = fg.m_cItem.m_strSpiritName;
                        break;
                    default:
                        break;
                }
            }

            GUI_FUNCTION.SET_AVATORS(m_spMonster, table.AvatorMRes);
            GUI_FUNCTION.SET_HeroBorderAndBack(m_spMonsterBorder, m_spMonsterFrame, (Nature)table.Property);
        }
        

    }

    //销毁//
    public void Destroy()
    {
        GameObject.Destroy(m_cItem);
    }
}

/// <summary>
/// 好友期望礼物类
/// </summary>
public class PreviewItem
{
    private const string RES_ITEM = "GUI_PreviewItem_";
    private const string SP_ITEMBORDER = "ItemBorder";
    private const string SP_ITEMFRAME = "ItemFrame";
    private const string SP_ITEMICON = "ItemIcon";
    private const string BTN_CANCEL = "BtnCancel";
    private const string BTN_LOGIN = "BtnLogin";
    private const string LAB_NUM = "Lab_Num";

    public GameObject m_cItem;
    private UISprite m_spBorder;
    private UISprite m_spFrame;
    private UISprite m_spIcon;
    private UILabel m_cLabNum;
    public GameObject m_cBtnCancel;
    public GameObject m_cBtnLogin;

    public PreviewItem(GameObject main,int i)
    {
        m_cItem = GUI_FINDATION.GET_GAME_OBJECT(main, RES_ITEM + (i + 1));
        m_spBorder = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, SP_ITEMBORDER);
        m_spFrame = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, SP_ITEMFRAME);
        m_spIcon = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, SP_ITEMICON);
        m_cLabNum = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cItem, LAB_NUM);
        m_cBtnCancel = GUI_FINDATION.GET_GAME_OBJECT(m_cItem, BTN_CANCEL);
        m_cBtnLogin = GUI_FINDATION.GET_GAME_OBJECT(m_cItem, BTN_LOGIN); 
    }

    //设置//
    public void SetPreview(FriendGiftItem item)
    {
        if (item == null)
        {
            m_cBtnCancel.SetActive(false);
            m_cBtnLogin.SetActive(true);
        }
        else
        {
            m_cBtnCancel.SetActive(true);
            m_cBtnLogin.SetActive(false);

            switch (item.m_eType)
            {
                case GiftType.Diamond:
                    m_spFrame.spriteName = "frame1";
                    break;
                case GiftType.Gold:
                    m_spFrame.spriteName = "frame1";
                    break;
                case GiftType.FarmPoint:
                    m_spFrame.spriteName = "frame1";
                    break;
                case GiftType.FriendPoint:
                    m_spFrame.spriteName = "frame1";
                    break;
                case GiftType.Item:
                    m_spFrame.spriteName = "getsucai";
                    break;
                default:
                    break;
            }

            m_spIcon.spriteName = item.m_strSpiritName;
            m_cLabNum.text = item.m_strNumText;
        }
    }
}