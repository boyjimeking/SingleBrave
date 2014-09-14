using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using Game.Resource;

public class GUIPropsAtlas : GUIBase
{
    private GUIPropsPreviewDetail m_cItemDetail;

    /// <summary>
    /// 物品图鉴Item
    /// </summary>
    public class ItemAltasItem
    {
        public GameObject m_cItem;           //item整个显示对象
        public UISprite m_cBG;               //底色
        public UISprite m_cBorder;            //边框
        public UISprite m_cIcon;             //图像
        public UILabel m_cLbBottom;          //字体底部

        private const string RES_BORDER = "Frame";
        private const string RES_BG = "Bg";
        private const string RES_ITEM = "Icon";
        private const string RES_LBBOTTOM = "Lab_Name";

        public ItemAltasItem(UnityEngine.Object item)
        {
            m_cItem = GameObject.Instantiate(item) as GameObject;

            m_cBorder = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, RES_BORDER);
            m_cBG = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, RES_BG);
            m_cIcon = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, RES_ITEM);
            m_cLbBottom = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cItem, RES_LBBOTTOM);

        }
    }

    private const string RES_MAIN = "GUI_PropsAtlas";                   //主资源地址
    private const string RES_ITEM = "GUI_PropsAtlasListItem";           //道具item资源地址
    private const string BTN_CANCEL = "Title_Cancel/Btn_Cancel";        //取消按钮地址
    private const string PAN_CANCEL = "Title_Cancel";                   //取消Pan地址
    private const string PAN_RIGHT = "PanInfo";                         //滑出Panel地址
    private const string RES_TABLE = "PanInfo/Panel/Table";             //Table地址
    private const string LISTVIEW = "PanInfo/Panel";//滚动视图地址
    private const string RES_SECRET = "secret";                         //问号道具显示
    private const string RES_BLACK = "BG";   //物品图鉴遮罩

    private GameObject m_cPanSlide;                       //panel滑动
    private GameObject m_cPanCancel;                      //取消按钮Panel
    private UnityEngine.Object m_cItemAtlasItem;          //英雄显示对象
    private GameObject m_cTable;                          //table
    private GameObject m_cBack;       //背景遮罩
    private GameObject m_cListView;//滚动视图

    private List<ItemTable> m_lstItemTable;               //图鉴中所有物品列表
    private List<Item> m_lstMyItems;                      //当前玩家所有物品
    private List<ItemAltasItem> m_lstItemShow;            //游戏中显示的物品对象列表

    private const int OFFSET_X = 120;   //X偏移量
    private const int OFFSET_Y = -120;  //Y偏移量

    protected float m_fClipParentY = -4;   //剪裁父节点Y轴坐标
    protected float m_fClipCenterY = -54;   //剪裁中间点Y轴坐标
    protected float m_fClipSizeY = 530; //剪裁Y轴大小

    private bool m_bHasShow = false;  //加载过showobject

    public GUIPropsAtlas(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_PROPSATLAS, GUILAYER.GUI_PANEL)
    { }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        if (m_lstItemTable != null) m_lstItemTable.Clear();
        if (m_lstItemShow != null) m_lstItemShow.Clear();

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
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_ITEM);
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + GUIPropsPreviewDetail.RES_MAIN);
           
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
            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;
            //滑出动画panel
            this.m_cPanSlide = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_RIGHT);

            this.m_cListView = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LISTVIEW);

            //取消按钮
            var cancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_CANCEL);
            GUIComponentEvent gui_event = cancel.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnCancel_OnEvent);
            this.m_cPanCancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_CANCEL);
            //table
            m_cTable = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, RES_TABLE);
            //英雄Item
            m_cItemAtlasItem = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_ITEM);
            //背景遮罩
            m_cBack = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, RES_BLACK);
            m_cBack.AddComponent<GUIComponentEvent>().AddIntputDelegate(BlackClick);

        }

        this.m_cListView.transform.localPosition = new Vector3(0, 0, 0);
        UIPanel panel = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(this.m_cGUIObject, LISTVIEW);
        panel.transform.localPosition = new Vector3(panel.transform.localPosition.x, this.m_fClipParentY, panel.transform.localPosition.z);
        panel.clipRange = new Vector4(panel.clipRange.x, this.m_fClipCenterY, panel.clipRange.z, this.m_fClipSizeY);

        this.m_cBack.SetActive(false);
        if (m_cItemDetail != null)
        {
            GameObject.DestroyImmediate(m_cItemDetail.m_cMain);
            m_cItemDetail = null;
        }

        m_lstItemTable = ItemTableManager.GetInstance().GetAll(); //加载所有table物品
        m_lstMyItems = Role.role.GetItemProperty().GetAllItem();  //加载玩家所有获得的物品

        if (m_lstItemShow == null)
        {
            m_lstItemShow = new List<ItemAltasItem>();
        }
        else
        {
            m_lstItemShow.ForEach((item) => { GameObject.DestroyImmediate(item.m_cItem); });
            m_lstItemShow.Clear();
        }

        UpdateLstShow();  //根据list更新显示

        this.m_cGUIMgr.SetCurGUIID(this.ID);

        CTween.TweenPosition(this.m_cPanCancel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-540, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cPanSlide, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);

        SetLocalPos(Vector3.zero);

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_SCANE_ITEM));
    }

    /// <summary>
    /// 更新
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


        if (IsShow())
        {
            foreach (ItemAltasItem item in m_lstItemShow)
            {
                if ((this.m_cListView.transform.localPosition.y - m_fClipParentY + OFFSET_Y) <= (-item.m_cItem.transform.localPosition.y + 240)
                     && (this.m_cListView.transform.localPosition.y - m_fClipParentY + m_fClipSizeY + 50) >= (-item.m_cItem.transform.localPosition.y - 480))
                {
                    //if (item.m_cItem.activeSelf)
                    {
                        item.m_cItem.SetActive(true);
                    }

                }
                else
                {
                    //if (item.m_cItem.activeSelf)
                    {
                        item.m_cItem.SetActive(false);
                    }
                }
            }
        }

        return base.Update();
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //base.Hiden();

        this.m_cBack.gameObject.SetActive(false);

        if (m_cItemDetail != null)
        {
            GameObject.DestroyImmediate(m_cItemDetail.m_cMain);
            m_cItemDetail = null;
        }

        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        CTween.TweenPosition(this.m_cPanSlide, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0));
        CTween.TweenPosition(this.m_cPanCancel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-540, 270, 0) , Destory);

        ResourceMgr.UnloadUnusedResources();
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
            if (m_cItemDetail != null)
            {
                this.m_cBack.gameObject.SetActive(false);

                GameObject.DestroyImmediate(m_cItemDetail.m_cMain);
                m_cItemDetail = null;

            }
            else
            {
                Hiden();

                GUIMenu menu = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_MENU) as GUIMenu;
                menu.Show();
            }
        }
    }

    /// <summary>
    /// Item选中事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void ItemSelect_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            int itemIndex = (int)args[0];
            Item tmp = new Item(m_lstItemTable[itemIndex].ID);

            this.m_cBack.gameObject.SetActive(true);
            if (m_cItemDetail != null)
            {
                GameObject.DestroyImmediate(m_cItemDetail.m_cMain);
                m_cItemDetail = null;
            }

            m_cItemDetail = new GUIPropsPreviewDetail(this.m_cGUIObject, tmp);
        }
    }

    /// <summary>
    /// 根据英雄列表显示所有单位
    /// </summary>
    /// <param name="lstHero"></param>
    private void UpdateLstShow()
    {
        for (int i = 0; i < m_lstItemTable.Count; i++)
        {
            ItemAltasItem tmp = new ItemAltasItem(m_cItemAtlasItem);
            tmp.m_cItem.transform.parent = m_cTable.gameObject.transform;
            tmp.m_cItem.transform.localScale = Vector3.one;

            int x = ((i) % 5) * OFFSET_X;
            int y = ((i) / 5) * OFFSET_Y;
            tmp.m_cItem.transform.localPosition = new Vector3(x, y, 0);

            //判断该图鉴的物品，玩家是否拥有，有则显示，无则用？号图片代替
            bool hasItem = Role.role.GetItemBookProperty().HadItem(m_lstItemTable[i].ID);

            if (hasItem)  //显示头像，添加相应事件
            {
                GUI_FUNCTION.SET_ITEMM(tmp.m_cIcon, m_lstItemTable[i].SpiritName);
                GUI_FUNCTION.SET_ITEM_BORDER(tmp.m_cBorder, (ITEM_TYPE)m_lstItemTable[i].Type);
                tmp.m_cLbBottom.text = m_lstItemTable[i].ShortName;
                tmp.m_cItem.AddComponent<GUIComponentEvent>().AddIntputDelegate(ItemSelect_OnEvent, i);
            }
            else  //显示问号，没有响应事件
            {
                tmp.m_cLbBottom.enabled = false;
                tmp.m_cBorder.enabled = false;
                tmp.m_cIcon.enabled = false;
                tmp.m_cBG.spriteName = RES_SECRET;
            }
            //OnInvisible temp = tmp.m_cItem.AddComponent<OnInvisible>();
            //temp.OffTop = 240f;
            //temp.OffBottom = 320f;

            m_lstItemShow.Add(tmp);
        }
    }

    /// <summary>
    /// 背景遮罩点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BlackClick(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.m_cBack.gameObject.SetActive(false);

            if (m_cItemDetail != null)
            {
                GameObject.DestroyImmediate(m_cItemDetail.m_cMain);
                m_cItemDetail = null;
            }
        }
    }
}