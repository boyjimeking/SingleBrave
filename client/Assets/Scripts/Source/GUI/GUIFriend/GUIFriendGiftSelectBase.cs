//  GUIFriendGiftSelectBase.cs
//  Author: Cheng Xia
//  2013-1-10

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;

/// <summary>
/// 物品选择基类
/// </summary>
public class GUIFriendGiftSelectBase : GUIBase
{
    protected const string RES_MAIN_BASE = "GUI_PropsPreview";//主资源地址
    protected const string RES_PROPSITEM = "GUI_FriendGiftPreviewItem";//物品项地址

    private const string TOPPANEL = "TopPanel";//导航栏地址
    private const string MAINPANEL = "MainPanel";//主面板地址
    private const string LB_ItemCount = "LBItemCount"; //物品数量
    private const string BACK_BUTTON = "TopPanel/Button_Back";//房名返回按钮地址
    private const string LB_TITLE = "TopPanel/Label";  //标题
    private const string LISTVIEW = "MainPanel/ListView/UITable";//列表地址
    private const string SP_BACK = "BG";  //背景遮罩

    private GameObject m_cTopPanel;//导航栏
    protected GameObject m_cMainPanel;//主面板地址
    protected GameObject m_cBtnBack;//返回按钮
    protected GameObject m_cListView;//列表地址
    protected UILabel m_cUITittle;  //标题
    protected UILabel m_cLbCount; //当前数量
    protected UISprite m_cBack;  //背景遮罩

    private const int OFFSET_X = 120;   //X偏移量
    private const int OFFSET_Y = -120;  //Y偏移量

    protected UnityEngine.Object m_cResItem;  //Item资源

    protected int m_iShowOffsetX = 0; //展示X偏移量索引
    protected float m_fClipParentY = 21;   //剪裁父节点Y轴坐标
    protected float m_fClipCenterY = -66;   //剪裁中间点Y轴坐标
    protected float m_fClipSizeY = 540; //剪裁Y轴大小

    protected bool m_bIfWithBattle = false;  //是否减去战斗物品显示

    //变量
    protected List<ItemShowItem> m_lstItemShow = new List<ItemShowItem>();    //显示英雄对象
    //protected List<FriendGiftItem> m_lstItems = new List<FriendGiftItem>();  //英雄列表
    protected List<FriendGiftItem> m_lstShows;  //根据tableid去更新显示

    /// <summary>
    /// 物品显示Item
    /// </summary>
    public class ItemShowItem
    {
        public GameObject m_cRes;  //item整个显示对象
        public FriendGiftItem m_cItem;    //英雄实例

        //变化属性
        public UILabel m_cItemNum;
        public UILabel m_cItemName;
        public UISprite m_cItemSprite;
        public UISprite m_cItemBorder;

        private const string LB_Count = "Lab_Count";  //数量
        private const string LB_Name = "Lab_Name";    //名称
        private const string SP_ITEMPATH = "Spr_Icon"; //物品图标
        private const string SP_ITEMBORDER = "Frame";  //物品边框

        public ItemShowItem(GameObject iconObj, FriendGiftItem item)
        {
            if (iconObj == null || item == null) return;

            this.m_cRes = iconObj;
            this.m_cItem = item;

            this.m_cItemNum = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cRes, LB_Count);
            this.m_cItemName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cRes, LB_Name);
            this.m_cItemSprite = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cRes, SP_ITEMPATH);
            this.m_cItemBorder = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cRes, SP_ITEMBORDER);
        }

        public void Destory()
        {
            m_cRes = null;  //item整个显示对象
            m_cItem = null;    //英雄实例


            m_cItemNum = null;
            m_cItemName = null;
            m_cItemSprite = null;
            m_cItemBorder = null;

        }
    }

    public GUIFriendGiftSelectBase(GUIManager mgr, int guiid, GUILAYER layer)
        : base(mgr, guiid, layer)
    {
    }


    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        if (null != m_lstItemShow)
        {
            foreach (ItemShowItem q in m_lstItemShow)
            {
                q.Destory();
            }

            m_lstItemShow.Clear();
        }

        m_cTopPanel = null;//导航栏
        m_cMainPanel = null;//主面板地址
        m_cBtnBack = null;//返回按钮
        m_cListView = null;//列表地址
        m_cUITittle = null;  //标题
        m_cLbCount = null; //当前数量
        m_cBack = null;  //背景遮罩
        m_cResItem = null;  //Item资源

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
            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_MAIN_BASE)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            this.m_cMainPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, MAINPANEL);

            this.m_cTopPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, TOPPANEL);

            this.m_cBtnBack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BACK_BUTTON);

            this.m_cListView = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LISTVIEW);

            this.m_cResItem = (UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_PROPSITEM);

            this.m_cUITittle = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LB_TITLE);

            this.m_cLbCount = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cMainPanel, LB_ItemCount);

            this.m_cBack = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SP_BACK);
        }

        this.m_cBack.enabled = false;

        if (this.m_lstItemShow != null)
        {
            this.m_lstItemShow.ForEach((item) => { GameObject.DestroyImmediate(item.m_cRes); });
        }
        this.m_lstItemShow.Clear();

        for (int i = 0; i < this.m_lstShows.Count; i++)
        {
            GameObject obj = GameObject.Instantiate(this.m_cResItem) as GameObject;
            obj.transform.parent = this.m_cListView.transform;
            obj.transform.localScale = Vector3.one;

            ItemShowItem tmp = new ItemShowItem(obj, this.m_lstShows[i]);
            m_lstItemShow.Add(tmp);
        }

        //显示Item数量
        this.m_cLbCount.text = this.m_lstShows.Count + "/" + Role.role.GetBaseProperty().m_iMaxItem;

        UpdateShowList();

        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-420, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);

        SetLocalPos(Vector3.zero);
    }

    /// <summary>
    /// 根据列表刷新显示
    /// </summary>
    private void UpdateShowList()
    {
        for (int i = 0; i < this.m_lstShows.Count; i++)
        {
            ItemShowItem item = m_lstItemShow[i];
            FriendGiftItem data = this.m_lstShows[i];

            item.m_cItemNum.text = data.m_strNumText;
            item.m_cItemName.text = data.m_strName;

            item.m_cItemSprite.spriteName = data.m_strSpiritName;

            switch ((GiftType)data.m_eType)
            {
                case GiftType.Diamond:
                    item.m_cItemBorder.spriteName = "frame1";
                    break;
                case GiftType.Gold:
                    item.m_cItemBorder.spriteName = "frame1";
                    break;
                case GiftType.FarmPoint:
                    item.m_cItemBorder.spriteName = "frame1";
                    break;
                case GiftType.FriendPoint:
                    item.m_cItemBorder.spriteName = "frame1";
                    break;
                case GiftType.Item:
                    item.m_cItemBorder.spriteName = "item_frame_2";
                    break;
                default:
                    break;
            }

            //GUI_FUNCTION.SET_ITEMM(item.m_cItemSprite, data.m_strSpiritName);
            //GUI_FUNCTION.SET_ITEM_BORDER(item.m_cItemBorder, (ITEM_TYPE)data.m_iTypeID);

            int x = ((i + this.m_iShowOffsetX) % 5) * OFFSET_X;
            int y = ((i + this.m_iShowOffsetX) / 5) * OFFSET_Y;
            item.m_cRes.transform.localPosition = new Vector3(x, y, 0);
        }
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        if (this.m_cBack != null)
        {
            this.m_cBack.enabled = false;
        }

        //base.Hiden();
        GUI_FUNCTION.AYSNCLOADING_HIDEN();

        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-420, 270, 0));
        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0),Destory);
    }
}