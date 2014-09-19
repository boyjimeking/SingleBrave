//Micro.Sanvey
//2013.11.28
//sanvey.china@gmail.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;

/// <summary>
/// 英雄图鉴GUI
/// </summary>
public class GUIHeroAltas : GUIBase
{
    /// <summary>
    /// 英雄图鉴Item
    /// </summary>
    public class HeroAltasItem
    {
        public GameObject m_cItem;           //item整个显示对象
        public UISprite m_cBorder;           //边框
        public UISprite m_cFrame;            //底色
        public UISprite m_cMonster;          //头像
        public UILabel m_cLbBottom;          //字体底部

        private const string RES_BORDER = "ItemBorder";      //英雄头像资源地址
        private const string RES_FRAME = "ItemFrame";        //英雄头像资源地址
        private const string RES_MONSTER = "ItemMonster";    //英雄头像资源地址
        private const string RES_LBBOTTOM = "LabelBottom";   //英雄头像资源地址

        public HeroAltasItem(UnityEngine.Object item)
        {
            m_cItem = GameObject.Instantiate(item) as GameObject;

            m_cBorder = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, RES_BORDER);
            m_cFrame = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, RES_FRAME);
            m_cMonster = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, RES_MONSTER);
            m_cLbBottom = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cItem, RES_LBBOTTOM);

        }
    }

    private const string RES_MAIN = "GUI_HeroAtlas";                    //菜单资源地址
    private const string BTN_CANCEL = "Title_Cancel/Btn_Cancel";        //取消按钮地址
    private const string PAN_CANCEL = "Title_Cancel";                   //取消Pan地址
    private const string PAN_RIGHT = "PanInfo";                         //滑出Panel地址
    private const string RES_TABLE = "PanInfo/Panel/Table";             //Table地址
    private const string LISTVIEW = "PanInfo/Panel";//滚动视图地址
    private const string RES_HEROITEM = "GUI_HeroAtlasItem";            //显示英雄地址
    private const string RES_SECRET = "secret";                //问号英雄显示

    private GameObject m_cPanSlide;                       //panel滑动
    private GameObject m_cPanCancel;                      //取消按钮Panel
    private UnityEngine.Object m_cHeroAtlasItem;          //英雄显示对象
    private GameObject m_cTable;                             //table
    private GameObject m_cListView;                       //滚动视图

    private List<HeroTable> m_lstHeroTable;               //图鉴中所有英雄列表
    private List<Hero> m_lstMyHeros;                      //当前玩家所有英雄
    private List<HeroAltasItem> m_lstHeroShow;            //游戏中显示的英雄对象列表

    private bool m_bHasShow = false;  //加载过showobject

    private Vector4 m_cOldVec4;  //移动到的当前位置，如果gui还未消除，进来后还是原先老位置
    private Vector3 m_cOldVec3;

    protected int m_iShowOffsetX = 0; //展示X偏移量索引
    protected float m_fClipParentY =26;   //剪裁父节点Y轴坐标
    protected float m_fClipCenterY =-84;   //剪裁中间点Y轴坐标
    protected float m_fClipSizeY = 530; //剪裁Y轴大小

    private const int OFFSET_X = 120;   //X偏移量
    private const int OFFSET_Y = -120;  //Y偏移量


    public GUIHeroAltas(GUIManager mgr)
        : base(mgr, GUI_DEFINE.GUIID_HEROALTAS, UILAYER.GUI_PANEL)
    {
    }



    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        if (m_lstHeroTable != null) m_lstHeroTable.Clear();
        if (m_lstMyHeros != null) m_lstMyHeros.Clear();
        if (m_lstMyHeros != null) m_lstMyHeros.Clear();

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
			ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_HEROITEM);
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
            //m_cTable.enabled = false;
            //英雄Item
            m_cHeroAtlasItem = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_HEROITEM);
        }

        if (m_cOldVec3 != null && m_cOldVec3.y != 0)
        {
            this.m_cListView.transform.localPosition = m_cOldVec3;
            UIPanel panel = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(this.m_cGUIObject, LISTVIEW);
            panel.clipRange = m_cOldVec4;
        }
        else
        {
            this.m_cListView.transform.localPosition = new Vector3(this.m_cListView.transform.localPosition.x, this.m_fClipParentY, this.m_cListView.transform.localPosition.z);
            this.m_cListView.GetComponent<UIPanel>().clipRange = new Vector4(this.m_cListView.GetComponent<UIPanel>().clipRange.x, this.m_fClipCenterY, this.m_cListView.GetComponent<UIPanel>().clipRange.z, this.m_fClipSizeY);

        }

        m_lstHeroTable = HeroTableManager.GetInstance().GetAll(); //加载所有table英雄
        m_lstMyHeros = Role.role.GetHeroProperty().GetAllHero();  //加载玩家所有获得的英雄

        if (m_lstHeroShow == null)
        {
            m_lstHeroShow = new List<HeroAltasItem>();
        }
        else
        {
            m_lstHeroShow.ForEach((item) => { GameObject.DestroyImmediate(item.m_cItem); });
            m_lstHeroShow.Clear();
        }

        UpdateLstShow();  //根据list更新显示

        this.m_cGUIMgr.SetCurGUIID(this.ID);

        CTween.TweenPosition(this.m_cPanCancel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-540, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cPanSlide, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);

        SetLocalPos(Vector3.zero);

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_SCANE_HERO));
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
            foreach (HeroAltasItem item in m_lstHeroShow)
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
                   // if (item.m_cItem.activeSelf)
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

        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        CTween.TweenPosition(this.m_cPanSlide, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0));
        CTween.TweenPosition(this.m_cPanCancel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-540, 270, 0) , Destory);

        ResourceMgr.UnloadUnusedResources();
    }

    /// <summary>
    /// 立即隐藏
    /// </summary>
    public override void HidenImmediately()
    {
        base.HidenImmediately();
        //SetLocalPos(Vector3.one * 0xFFFFF);

        this.m_cPanSlide = null;
        this.m_cPanCancel = null;
        this.m_cHeroAtlasItem = null;
        this.m_cTable = null;
        this.m_cListView = null;

        Destory();
    }

    public override void ShowImmediately()
    {
        base.ShowImmediately();

        this.m_cPanCancel.transform.localPosition = new Vector3(0, 270, 0);
        this.m_cPanSlide.transform.localPosition = Vector3.zero;
        SetLocalPos(Vector3.zero);
        this.m_cGUIMgr.SetCurGUIID(this.ID);
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

            GUIMenu menu = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_MENU) as GUIMenu;
            menu.Show();
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
            

            m_cOldVec3 = this.m_cListView.transform.localPosition;
            UIPanel panel = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(this.m_cGUIObject, LISTVIEW);
            m_cOldVec4 = panel.clipRange;

            int herotableID = (int)args[0];

            HidenImmediately();

            GUIHeroAltasDetail tmp = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERO_ALTAS_DETAIL) as GUIHeroAltasDetail;
            tmp.m_iHeroTableId = herotableID;
            tmp.Show(BackToHeroALtas); //进入下层界面时记录上层
        }
    }

    /// <summary>
    /// 英雄图鉴的回调方法
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void BackToHeroALtas()
    {
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Show();
        this.Show();
    }

    /// <summary>
    /// 根据英雄列表显示所有单位
    /// </summary>
    /// <param name="lstHero"></param>
    private void UpdateLstShow()
    {
        for (int i = 0; i < m_lstHeroTable.Count; i++)
        {
            HeroAltasItem tmp = new HeroAltasItem(m_cHeroAtlasItem);
            tmp.m_cItem.transform.parent = m_cTable.gameObject.transform;
            tmp.m_cItem.transform.localScale = Vector3.one;

            //判断该图鉴的英雄，玩家是否拥有，有则显示，无则用？号图片代替
            bool hashero = Role.role.GetHeroBookProperty().HadHero(m_lstHeroTable[i].ID);

            if (hashero)  //显示头像，添加相应事件
            {
                GUI_FUNCTION.SET_HeroBorderAndBack(tmp.m_cBorder, tmp.m_cFrame, (Nature)m_lstHeroTable[i].Property);
                GUI_FUNCTION.SET_AVATORS(tmp.m_cMonster, m_lstHeroTable[i].AvatorMRes);
                tmp.m_cLbBottom.text = "No." + m_lstHeroTable[i].ID;
                tmp.m_cItem.AddComponent<GUIComponentEvent>().AddIntputDelegate(ItemSelect_OnEvent, m_lstHeroTable[i].ID);
            }
            else  //显示问号，没有响应事件
            {
                tmp.m_cLbBottom.text = "No." + m_lstHeroTable[i].ID;
                //tmp.m_cLbBottom.gameObject.SetActive(false);
                tmp.m_cMonster.gameObject.SetActive(false);
                tmp.m_cFrame.gameObject.SetActive(false);
                tmp.m_cBorder.spriteName = RES_SECRET;
            }
            //OnInvisible temp = tmp.m_cItem.AddComponent<OnInvisible>();
            //temp.OffTop = 240f;
            //temp.OffBottom = 320f;

            m_lstHeroShow.Add(tmp);
        }

        for (int i = 0; i < this.m_lstHeroShow.Count; i++)
        {
            HeroAltasItem item = this.m_lstHeroShow[i];
            int x = ((i + this.m_iShowOffsetX) % 5) * OFFSET_X;
            int y = ((i + this.m_iShowOffsetX) / 5) * OFFSET_Y;
            item.m_cItem.transform.localPosition = new Vector3(x, y, 0);
        }
    }

}