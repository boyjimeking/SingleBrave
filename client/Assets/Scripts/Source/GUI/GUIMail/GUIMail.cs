using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using Game.Resource;

public class GUIMail : GUIBase
{
    private const string RES_MAIN = "GUI_Mail";//主资源地址
    private const string RES_LISTITEM = "GUI_MailListItem";//列表行资源地址

    private const string MAINPANEL = "MainPanel";//主面板地址
    private const string LISTVIEW = "MainPanel/ClipView/ListView";//列表地址
    private const string LISTITEM_PARENT = "MainPanel/ClipView/ListView/Table";//列表父对象
    private const string TOPPANEL = "TopPanel";//导航栏地址
    private const string BUTTON_BACK = "TopPanel/Button_Back";//返回按钮地址
    private const string LABEL_CONTENT = "Lab_Content";//邮件内容标签地址
    private const string LABEL_CONTENTDATE = "Lab_ContentDate";//邮件内容标签地址
    private const string LABEL_DATE = "Lab_Date";//邮件获得时间标签地址
    private const string LABEL_TITLE = "Lab_Title";//邮件标题标签地址
    private const string SPR_ICON = "IconContent";//图标地址
    private const string SPR_ICONFRAME = "IconFrame";//图标边框地址
    private const string SPR_ICONBG = "Spr_IconBg";//图标边框地址
    private const string BTN_RECEIVE = "Btn_Receive";//接收按钮地址
    private const string BTN_GETALL = "MainPanel/Btn_GetAll";//全部接收按钮地址

    private GameObject m_cMainPanel;//主面板
    private GameObject m_cTopPanel;//导航栏
    private GameObject m_cBtnBack;//返回按钮地址
    private GameObject m_cListView;//列表
    private GameObject m_cListItemParent;//列表父对象
    private UnityEngine.Object m_cListItem;//列表项
    private List<Mail> m_lstMail;//邮件列表
    private GameObject m_cBtnGetAll;//全部接收按钮

    private int m_iSelectMailIndex;//单独接收的礼物index
    private List<GameObject> m_lstMailItem = new List<GameObject>();//邮件列表

    public GUIMail(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_MAIL, UILAYER.GUI_PANEL)
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
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_LISTITEM);
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

            this.m_cTopPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, TOPPANEL);
            this.m_cTopPanel.transform.localPosition = new Vector3(-420, 270, 0);

            this.m_cMainPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, MAINPANEL);
            this.m_cMainPanel.transform.localPosition = new Vector3(640, 0, 0);

            this.m_cListView = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LISTVIEW);



            this.m_cBtnBack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_BACK);
            GUIComponentEvent backEvent = this.m_cBtnBack.AddComponent<GUIComponentEvent>();
            backEvent.AddIntputDelegate(OnClickBackButton);

            this.m_cBtnGetAll = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_GETALL);
            GUIComponentEvent getAllEvent = this.m_cBtnGetAll.AddComponent<GUIComponentEvent>();
            getAllEvent.AddIntputDelegate(OnClickGetAllButton);
        }

        this.m_cListItem = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_LISTITEM);
        this.m_cListItemParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LISTITEM_PARENT);

        this.m_cListView.transform.localPosition = new Vector3(0, 0, 0);
        UIPanel panel = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(this.m_cGUIObject, LISTVIEW);
        float y = -56.3f;
        panel.clipRange = new Vector4(panel.clipRange.x, y, panel.clipRange.z, panel.clipRange.w);


		Mail mail = CModelMgr.sInstance.GetModel<Mail>();
		for (int i = 0; i < mail.Count; i++)
        {
            GameObject listItem = GameObject.Instantiate(this.m_cListItem) as GameObject;
            listItem.transform.parent = this.m_cListItemParent.transform;
            listItem.transform.localScale = Vector3.one;
            listItem.transform.localPosition = new Vector3(0, 150 - i * 125, 0);

            UISprite sprIcon = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(listItem, SPR_ICON);
            sprIcon.MakePixelPerfect();
            UISprite sprFrame = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(listItem, SPR_ICONFRAME);
            UISprite sprBg = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(listItem, SPR_ICONBG);

            UILabel labTitle = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(listItem, LABEL_TITLE);
			Mail tmpMail = mail[i] as Mail;
			switch (tmpMail.m_cType)
            {
                case GiftType.Diamond:
					labTitle.text = tmpMail.m_strTittle + " x " + tmpMail.m_iCount;
                    sprIcon.spriteName = "gem";
                    break;
                case GiftType.Gold:
					labTitle.text = tmpMail.m_strTittle + " x " + tmpMail.m_iCount;
                    sprIcon.spriteName = "zell_thum";
                    break;
                case GiftType.FriendPoint:
					labTitle.text = tmpMail.m_strTittle + " x " + tmpMail.m_iCount;
                    sprIcon.spriteName = "friend_p_thum";
                    break;
                case GiftType.FarmPoint:
					labTitle.text = tmpMail.m_strTittle + " x " + tmpMail.m_iCount;
                    sprIcon.spriteName = "karma_thum";
                    break;
                case GiftType.Hero:
					labTitle.text = tmpMail.m_strTittle + " x " + tmpMail.m_iCount;
					HeroTable hero = HeroTableManager.GetInstance().GetHeroTable(tmpMail.m_iHeroTableID);
                    GUI_FUNCTION.SET_AVATORS(sprIcon, hero.AvatorMRes);
                    GUI_FUNCTION.SET_HeroBorderAndBack(sprFrame, sprBg, (Nature)hero.Property);
                    break;
                case GiftType.Item:
					labTitle.text = tmpMail.m_strTittle + " x " + tmpMail.m_iCount;
					ItemTable item = ItemTableManager.GetInstance().GetItem(tmpMail.m_iItemTableID);
                    GUI_FUNCTION.SET_ITEMM(sprIcon, item.SpiritName);
                    GUI_FUNCTION.SET_ITEM_BORDER(sprFrame, (ITEM_TYPE)item.Type);
                    break;
            }

            UILabel labDate = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(listItem, LABEL_DATE);
            DateTime dt = DateTime.Parse("1970-1-1 8:00:00");
            DateTime dt1 = dt.AddSeconds(m_lstMail[i].m_lDate);
            labDate.text = dt1.Year + "-" + dt1.Month + "-" + dt1.Day;

            UILabel labContent = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(listItem, LABEL_CONTENT);
            labContent.text = m_lstMail[i].m_strContent;

            UILabel labContentDate = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(listItem, LABEL_CONTENTDATE);
            labContentDate.text = dt1.Month + "月" + dt1.Day + "日赠予";

            GameObject btnReceive = GUI_FINDATION.GET_GAME_OBJECT(listItem, BTN_RECEIVE);
            GUIComponentEvent receiveEvent = btnReceive.AddComponent<GUIComponentEvent>();
            receiveEvent.AddIntputDelegate(OnClickReceiveButton, i);

            this.m_lstMailItem.Add(listItem);
        }

        UIDraggablePanel dragPanel = GUI_FINDATION.GET_OBJ_COMPONENT<UIDraggablePanel>(this.m_cGUIObject, LISTVIEW);
        dragPanel.repositionClipping = true;

        this.m_cGUIMgr.SetCurGUIID(this.m_iID);
        SetLocalPos(Vector3.zero);

        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-420, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //base.Hiden();

        foreach (Transform trans in this.m_cListItemParent.transform)
        {
            GameObject.Destroy(trans.gameObject);
        }

        if (this.m_lstMail != null)
        {
            Role.role.GetBaseProperty().m_iMailCounts = this.m_lstMail.Count;
        }

        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-420, 270, 0));
        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0) , Destory);

        ResourceMgr.UnloadUnusedResources();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        this.m_cListItem = null;

        if (this.m_lstMailItem != null)
        {
            foreach (GameObject obj in this.m_lstMailItem)
            {
                if (obj != null)
                {
                    GameObject.Destroy(obj);
                }
            }
        }

        this.m_cTopPanel = null;
        this.m_cBtnBack = null;
        this.m_cListView = null;
        this.m_cListItemParent = null;
        this.m_cListItem = null;
        this.m_cBtnGetAll = null;

        base.Destory();
    }

    /// <summary>
    /// 刷新列表
    /// </summary>
    public void ReFlashListView()
    {
        foreach (Transform trans in this.m_cListItemParent.transform)
        {
            GameObject.Destroy(trans.gameObject);
        }

        this.m_cListView.transform.localPosition = new Vector3(0, 0, 0);
        UIPanel panel = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(this.m_cGUIObject, LISTVIEW);
        float y = -56.3f;
        panel.clipRange = new Vector4(panel.clipRange.x, y, panel.clipRange.z, panel.clipRange.w);

        for (int i = 0; i < m_lstMail.Count; i++)
        {
            GameObject listItem = GameObject.Instantiate(this.m_cListItem) as GameObject;
            listItem.transform.parent = this.m_cListItemParent.transform;
            listItem.transform.localScale = Vector3.one;
            listItem.transform.localPosition = new Vector3(0, 160 - i * 120, 0);

            UILabel labTitle = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(listItem, LABEL_TITLE);
            labTitle.text = m_lstMail[i].m_strTittle + " x " + m_lstMail[i].m_iCount;

            UISprite sprIcon = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(listItem, SPR_ICON);
            sprIcon.MakePixelPerfect();
            UISprite sprFrame = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(listItem, SPR_ICONFRAME);
            UISprite sprBg = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(listItem, SPR_ICONBG);

            switch (m_lstMail[i].m_cType)
            {
                case GiftType.Diamond:
                    sprIcon.spriteName = "gem";
                    break;
                case GiftType.Gold:
                    sprIcon.spriteName = "zell_thum";
                    break;
                case GiftType.FriendPoint:
                    sprIcon.spriteName = "friend_p_thum";
                    break;
                case GiftType.FarmPoint:
                    sprIcon.spriteName = "karma_thum";
                    break;
                case GiftType.Hero:
                    HeroTable hero = HeroTableManager.GetInstance().GetHeroTable(m_lstMail[i].m_iHeroTableID);
                    GUI_FUNCTION.SET_AVATORM(sprIcon, hero.AvatorMRes);
                    GUI_FUNCTION.SET_HeroBorderAndBack(sprFrame, sprBg, (Nature)hero.Property);
                    break;
                case GiftType.Item:
                    ItemTable item = ItemTableManager.GetInstance().GetItem(m_lstMail[i].m_iItemTableID);
                    GUI_FUNCTION.SET_ITEMM(sprIcon, item.SpiritName);
                    GUI_FUNCTION.SET_ITEM_BORDER(sprFrame, (ITEM_TYPE)item.Type);
                    break;
            }

            UILabel labDate = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(listItem, LABEL_DATE);
            DateTime dt = DateTime.Parse("1970-1-1 8:00:00");
            DateTime dt1 = dt.AddSeconds(m_lstMail[i].m_lDate);
            labDate.text = dt1.Year + "-" + dt1.Month + "-" + dt1.Day;

            UILabel labContent = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(listItem, LABEL_CONTENT);
            labContent.text = m_lstMail[i].m_strContent;

            UILabel labContentDate = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(listItem, LABEL_CONTENTDATE);
            labContentDate.text = dt1.Month + "月" + dt1.Day + "日赠予";

            GameObject btnReceive = GUI_FINDATION.GET_GAME_OBJECT(listItem, BTN_RECEIVE);
            GUIComponentEvent receiveEvent = btnReceive.AddComponent<GUIComponentEvent>();
            receiveEvent.AddIntputDelegate(OnClickReceiveButton, i);
        }

        if (this.m_lstMail.Count == 0)
        {
            Hiden();

            GUIMain main = (GUIMain)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_MAIN);
            main.Show();
        }

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

            GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

            Hiden();

            GUIMain menu = (GUIMain)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_MAIN);
            menu.Show();
        }
    }

    /// <summary>
    /// 接收按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickReceiveButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            SendAgent.SendPlayerReceiveSystemGift(Role.role.GetBaseProperty().m_iPlayerId,this.m_lstMail[(int)args[0]].m_iID.ToString());
            this.m_lstMail.RemoveAt((int)args[0]);
        }
    }

    /// <summary>
    /// 全部接收按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickGetAllButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (this.m_lstMail.Count != 0)
            {
                string mailIds = "";

                for (int i = 0; i < this.m_lstMail.Count; i++)
                {
                    int id = this.m_lstMail[i].m_iID;
                    if (i != this.m_lstMail.Count - 1)
                    {
                        mailIds = mailIds + id.ToString() + "|";
                    }
                    else
                    {
                        mailIds = mailIds + id.ToString();
                    }
                }

                SendAgent.SendPlayerReceiveSystemGift(Role.role.GetBaseProperty().m_iPlayerId, mailIds);
                this.m_lstMail.Clear();
            }
            
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
}

