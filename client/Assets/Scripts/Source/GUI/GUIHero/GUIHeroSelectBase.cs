using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;


//  GUIHeroSelectBase.cs
//  Author: Lu Zexi
//  2013-12-18



/// <summary>
/// 英雄选择基类
/// </summary>
public class GUIHeroSelectBase : GUIBase
{
    public const string RES_MAIN = "GUI_HeroSelectBase";              //菜单资源地址
    public const string RES_HEROITEM = "GUI_HeroSelectItem";          //英雄头像资源地址

    private const string TOP_RIGHT = "PanInfo";                     //滑出Panel地址
    private const string TOP_CANCEL_PARENT = "Title_Cancel";               //取消Pan地址

    private const string UI_TITTLE = "Title_Cancel/Lb_Info"; //标题文字
    private const string BTN_CANCEL = "Title_Cancel/Btn_Cancel";    //取消按钮地址
    private const string UI_SORT = "Title_Cancel/Lb_Sort";          //显示现在排序规则地址
    private const string CLIP_PANEL = "PanInfo/Panel";  //剪裁panel
    private const string ICON_PARENT = "PanInfo/Panel/Table";         //Table地址
    private const string BTN_SORT = "PanInfo/Btn_Sort";             //排序方式按钮地址
    private const string LB_COUNT = "PanInfo/Lb_Count";             //显示拥有个数地址

    public const string BTN_NULL = "GUI_BtnNull";
    private const string SP_NULL = "SP_Null";
    protected UISprite m_cBtnNull;
    private GameObject m_cResNull; //移除按钮资源

    private const int OFFSET_X = 120;   //X偏移量
    private const int OFFSET_Y = -120;  //Y偏移量

    protected UnityEngine.Object m_cHeroAtlasItem;  //英雄资源

    protected GameObject m_cTopRightParent;   //上右父节点
    protected GameObject m_cTopLeftParent;     //取消按钮Panel
    protected UILabel m_cUITittle;        //标题
    protected UIPanel m_cClipPanel; //裁剪Panel
    protected GameObject m_cIconParent;   //图标父节点
    protected UILabel m_cLbSort;  //排序规则
    protected UILabel m_cLbCount; //当前数量
    protected GameObject m_cCancelBtn;  //取消按钮
    protected GameObject m_cHeroRemove;  //移除按钮

    protected int m_iShowOffsetX = 0; //展示X偏移量索引
    protected float m_fClipParentY = 16;   //剪裁父节点Y轴坐标
    protected float m_fClipCenterY = -73;   //剪裁中间点Y轴坐标
    protected float m_fClipSizeY = 530; //剪裁Y轴大小
    //出框隐藏计算
    private float m_fTop;
    private float m_fBottom;

    //长按数据
    protected float PRESS_TIME = 0.8f;  //长按时间后进入详细界面
    protected float m_fDis;  //长按计时器
    protected bool m_bHasPress=false;  //是否按下
    protected bool m_bHasDrag = false;  //是否被拖动
    protected int m_iSelectHeroIndex = 0;  //按下的英雄index
    protected bool m_bBeLongPress = false;  //已经为长按状态了
    public bool m_bShowNull = false;  //是否显示移除

    //变量
    protected List<HeroShowItem> m_lstHeroShow = new List<HeroShowItem>();    //显示英雄对象
    protected List<Hero> m_lstHero = new List<Hero>();  //英雄列表
    protected Vector3 m_cPanelLocalposition = Vector3.zero; //英雄列表滑动位置
    protected Vector4 m_cPanelClipRange = Vector4.zero;//英雄列表panel记录

    /// <summary>
    /// 英雄显示Item
    /// </summary>
    public class HeroShowItem
    {
        public GameObject m_cItem;  //item整个显示对象
        public Hero m_cHero;    //英雄实例

        public UISprite m_cBorder;  //边框
        public UISprite m_cStar; //英雄星级
        public UISprite m_cStar1; //英雄星级
        public UISprite m_cStar2; //英雄星级
        public UISprite m_cStar3; //英雄星级
        public UISprite m_cStar4; //英雄星级
        public UISprite m_cE;  //装备
        public UISprite m_cFrame; //底色
        public UISprite m_cLike; //喜欢
        public UISprite m_cIcon;  //头像
        public UISprite m_cInTeam;  //当前TEAM标志
        public UISprite m_cItemCover;   //物品遮盖

        //变化属性
        public UILabel m_cLbBottom; //当前等级，攻击，防御，回复，数值
        public UISprite m_cSelectBar;      //英雄选中条
        public UILabel m_cSelectLb;  //英雄选中数量

        private const string RES_BORDER = "ItemBorder";  //属性框
        private const string RES_BOTTOM = "ItemBottom";  //底部
        private const string RES_BOTTOM1 = "ItemBottom1";  //底部
        private const string RES_BOTTOM2 = "ItemBottom2";  //底部
        private const string RES_BOTTOM3 = "ItemBottom3";  //底部
        private const string RES_BOTTOM4 = "ItemBottom4";  //底部
        private const string RES_E = "ItemE";  //装备
        private const string RES_FRAME = "ItemFrame";  //英雄头像资源地址
        private const string RES_LIKE = "ItemLike";  //英雄头像资源地址
        private const string RES_MONSTER = "ItemMonster";  //英雄头像资源地址
        private const string RES_TOP = "ItemTop";  //英雄头像资源地址
        private const string RES_LBBOTTOM = "LabelBottom";  //英雄头像资源地址
        private const string RES_HEROBAR = "ItemBar";  //英雄选中数量
        private const string RES_LBSELECT = "LbSelect"; //英雄选中数量
        private const string RES_ITEM_COVER = "ItemCover";  //物品遮盖

        private const string SP_NAME_TEAM_USE = "pack_used";    //使用中
        private const string SP_NAME_AT_TEAM = "pack_atTeam";   //在队伍中
        private const string SP_NEW = "new_icon";  //最新获得

        public HeroShowItem(GameObject iconObj, Hero hero)
        {
            if (iconObj == null || hero == null) return;

            this.m_cItem = iconObj;
            this.m_cHero = hero;
            this.m_cBorder = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, RES_BORDER);
            this.m_cStar = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, RES_BOTTOM);
            this.m_cStar1 = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, RES_BOTTOM1);
            this.m_cStar2 = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, RES_BOTTOM2);
            this.m_cStar3 = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, RES_BOTTOM3);
            this.m_cStar4 = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, RES_BOTTOM4);
            this.m_cE = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, RES_E);
            this.m_cFrame = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, RES_FRAME);
            this.m_cLike = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, RES_LIKE);
            this.m_cIcon = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, RES_MONSTER);
            this.m_cInTeam = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, RES_TOP);
            this.m_cLbBottom = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cItem, RES_LBBOTTOM);
            this.m_cSelectBar = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, RES_HEROBAR);
            this.m_cSelectLb = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cItem, RES_LBSELECT);
            this.m_cItemCover = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cItem, RES_ITEM_COVER);

            GUI_FUNCTION.SET_HeroBorderAndBack(this.m_cBorder, this.m_cFrame, this.m_cHero.m_eNature);
            GUI_FUNCTION.SET_AVATORS(this.m_cIcon, this.m_cHero.m_strAvatarM);
            if (hero.m_iEquipID > 0)
                this.m_cE.enabled = true;
            else
                this.m_cE.enabled = false;
            if (hero.m_bLock)
                this.m_cLike.enabled = true;
            else
                this.m_cLike.enabled = false;

            if (hero.m_bNew)
            {
                this.m_cInTeam.enabled = true;
                this.m_cInTeam.spriteName = SP_NEW;
                this.m_cInTeam.MakePixelPerfect();
                TweenAlpha.Begin(this.m_cInTeam.gameObject, 1, 0).style = UITweener.Style.PingPong;
            }


            int currentTeam = Role.role.GetBaseProperty().m_iCurrentTeam;
            if (Role.role.GetTeamProperty().GetTeam(currentTeam).GetHeroIndex(hero) >= 0)
            {
                this.m_cInTeam.enabled = true;
                this.m_cInTeam.spriteName = SP_NAME_TEAM_USE;
                this.m_cInTeam.MakePixelPerfect();
                TweenAlpha.Begin(this.m_cInTeam.gameObject, 1, 0).style = UITweener.Style.PingPong;
            }
            else
            {
                bool exist = false;
                foreach (HeroTeam item in Role.role.GetTeamProperty().GetAllTeam())
                {
                    if (item.GetHeroIndex(hero) >= 0)
                    {
                        exist = true;
                        break;
                    }
                }
                if (exist)
                {
                    this.m_cInTeam.enabled = true;
                    this.m_cInTeam.spriteName = SP_NAME_AT_TEAM;
                    this.m_cInTeam.MakePixelPerfect();
                    TweenAlpha.Begin(this.m_cInTeam.gameObject, 1, 0).style = UITweener.Style.PingPong;
                }
                else
                {
                    if (hero.m_bNew)
                    {
                        this.m_cInTeam.enabled = true;
                    }
                    else
                    {
                        this.m_cInTeam.enabled = false;
                    }
                }
            }

            this.m_cSelectBar.alpha = 0f; // 用透明度解决图片下滑的问题
            this.m_cSelectLb.enabled = false;
            this.m_cItemCover.enabled = false;
        }

        /// <summary>
        /// 展示属性
        /// </summary>
        public void ShowNature()
        {
            this.m_cStar.enabled = false;
            this.m_cStar1.enabled = false;
            this.m_cStar2.enabled = false;
            this.m_cStar3.enabled = false;
            this.m_cStar4.enabled = false;
            this.m_cLbBottom.enabled = true;
            if (this.m_cHero.m_iLevel < this.m_cHero.m_iMaxLevel)
            {
                this.m_cLbBottom.text = "Lv." + this.m_cHero.m_iLevel;
                this.m_cLbBottom.color = Color.white;
            }
            else
            {
                this.m_cLbBottom.text = "Lv.MAX";
                this.m_cLbBottom.color = Color.yellow;
            }
        }

        /// <summary>
        /// 展示等级
        /// </summary>
        public void ShowLevel()
        {
            this.m_cStar.enabled = false;
            this.m_cStar1.enabled = false;
            this.m_cStar2.enabled = false;
            this.m_cStar3.enabled = false;
            this.m_cStar4.enabled = false;
            this.m_cLbBottom.enabled = true;
            if (this.m_cHero.m_iLevel < this.m_cHero.m_iMaxLevel)
            {
                this.m_cLbBottom.text = "Lv." + this.m_cHero.m_iLevel;
                this.m_cLbBottom.color = Color.white;
            }
            else
            {
                this.m_cLbBottom.text = "Lv.MAX";
                this.m_cLbBottom.color = Color.yellow;
            }
        }

        /// <summary>
        /// 展示HP
        /// </summary>
        public void ShowHP()
        {
            this.m_cStar.enabled = false;
            this.m_cStar1.enabled = false;
            this.m_cStar2.enabled = false;
            this.m_cStar3.enabled = false;
            this.m_cStar4.enabled = false;
            this.m_cLbBottom.enabled = true;
            this.m_cLbBottom.text = "" + this.m_cHero.GetMaxHP();
        }

        /// <summary>
        /// 展示攻击
        /// </summary>
        public void ShowAttack()
        {
            this.m_cStar.enabled = false;
            this.m_cStar1.enabled = false;
            this.m_cStar2.enabled = false;
            this.m_cStar3.enabled = false;
            this.m_cStar4.enabled = false;
            this.m_cLbBottom.enabled = true;
            this.m_cLbBottom.text = "" + this.m_cHero.GetAttack();
        }

        /// <summary>
        /// 展示防御
        /// </summary>
        public void ShowDefence()
        {
            this.m_cStar.enabled = false;
            this.m_cStar1.enabled = false;
            this.m_cStar2.enabled = false;
            this.m_cStar3.enabled = false;
            this.m_cStar4.enabled = false;
            this.m_cLbBottom.enabled = true;
            this.m_cLbBottom.text = "" + this.m_cHero.GetDefence();
        }

        /// <summary>
        /// 展示恢复
        /// </summary>
        public void ShowRecover()
        {
            this.m_cStar.enabled = false;
            this.m_cStar1.enabled = false;
            this.m_cStar2.enabled = false;
            this.m_cStar3.enabled = false;
            this.m_cStar4.enabled = false;
            this.m_cLbBottom.enabled = true;
            this.m_cLbBottom.text = "" + this.m_cHero.GetRecover();
        }

        /// <summary>
        /// 展示星级
        /// </summary>
        public void ShowStar()
        {

            switch (this.m_cHero.m_iStarLevel)
            {
                case 1:
                    this.m_cStar.enabled = true;
                    this.m_cStar.transform.localPosition = new Vector3(0, -45, 0);
                    this.m_cStar1.enabled = false;
                    this.m_cStar2.enabled = false;
                    this.m_cStar3.enabled = false;
                    this.m_cStar4.enabled = false;
                    break;
                case 2:
                    this.m_cStar.enabled = true;
                    this.m_cStar.transform.localPosition = new Vector3(-10, -45, 0);
                    this.m_cStar1.enabled = true;
                    this.m_cStar1.transform.localPosition = new Vector3(10, -45, 0);
                    this.m_cStar2.enabled = false;
                    this.m_cStar3.enabled = false;
                    this.m_cStar4.enabled = false;
                    break;
                case 3:
                    this.m_cStar.enabled = true;
                    this.m_cStar.transform.localPosition = new Vector3(-20, -45, 0);
                    this.m_cStar1.enabled = true;
                    this.m_cStar1.transform.localPosition = new Vector3(0, -45, 0);
                    this.m_cStar2.enabled = true;
                    this.m_cStar2.transform.localPosition = new Vector3(20, -45, 0);
                    this.m_cStar3.enabled = false;
                    this.m_cStar4.enabled = false;
                    break;
                case 4:
                    this.m_cStar.enabled = true;
                    this.m_cStar.transform.localPosition = new Vector3(-30, -45, 0);
                    this.m_cStar1.enabled = true;
                    this.m_cStar1.transform.localPosition = new Vector3(-10, -45, 0);
                    this.m_cStar2.enabled = true;
                    this.m_cStar2.transform.localPosition = new Vector3(10, -45, 0);
                    this.m_cStar3.enabled = true;
                    this.m_cStar3.transform.localPosition = new Vector3(30, -45, 0);
                    this.m_cStar4.enabled = false;
                    break;
                case 5:
                    this.m_cStar.enabled = true;
                    this.m_cStar.transform.localPosition = new Vector3(-40, -45, 0);
                    this.m_cStar1.enabled = true;
                    this.m_cStar1.transform.localPosition = new Vector3(-20, -45, 0);
                    this.m_cStar2.enabled = true;
                    this.m_cStar2.transform.localPosition = new Vector3(0, -45, 0);
                    this.m_cStar3.enabled = true;
                    this.m_cStar3.transform.localPosition = new Vector3(20, -45, 0);
                    this.m_cStar4.enabled = true;
                    this.m_cStar4.transform.localPosition = new Vector3(40, -45, 0);
                    break;
                default:
                    break;
            }
            this.m_cLbBottom.enabled = false;
        }

        /// <summary>
        /// 展示新旧
        /// </summary>
        public void ShowNewOld()
        {
            this.m_cStar.enabled = false;
            this.m_cStar1.enabled = false;
            this.m_cStar2.enabled = false;
            this.m_cStar3.enabled = false;
            this.m_cStar4.enabled = false;
            this.m_cLbBottom.enabled = true;
            if (this.m_cHero.m_iLevel < this.m_cHero.m_iMaxLevel)
            {
                this.m_cLbBottom.text = "Lv." + this.m_cHero.m_iLevel;
                this.m_cLbBottom.color = Color.white;
            }
            else
            {
                this.m_cLbBottom.text = "Lv.MAX";
                this.m_cLbBottom.color = Color.yellow;
            }
        }

        /// <summary>
        /// 展示领导力
        /// </summary>
        public void ShowCost()
        {
            this.m_cStar.enabled = false;
            this.m_cStar1.enabled = false;
            this.m_cStar2.enabled = false;
            this.m_cStar3.enabled = false;
            this.m_cStar4.enabled = false;
            this.m_cLbBottom.enabled = true;
            this.m_cLbBottom.text = "" + this.m_cHero.m_iCost;
        }
    }

    public GUIHeroSelectBase(GUIManager mgr, int guiid, GUILAYER layer)
        : base(mgr, guiid, layer)
    {
    }


    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {

        m_cBtnNull = null;
        m_cResNull = null; //移除按钮资源

        m_cHeroAtlasItem = null;  //英雄资源

        m_cTopRightParent = null;   //上右父节点
        m_cTopLeftParent = null;     //取消按钮Panel
        m_cUITittle = null;        //标题
    
        m_cIconParent = null;   //图标父节点
        m_cLbSort = null;  //排序规则
        m_cLbCount = null; //当前数量
        m_cCancelBtn = null;  //取消按钮
        m_cHeroRemove = null;  //移除按钮



        base.Hiden();

        if (null != m_lstHeroShow) m_lstHeroShow.Clear();

        base.Destory();
    }

    public override bool Update()
    {
        if (IsShow())
        {
            foreach (HeroShowItem item in m_lstHeroShow)
            {
                if ((this.m_cClipPanel.transform.localPosition.y - m_fClipParentY + OFFSET_Y) <= (-item.m_cItem.transform.localPosition.y + 240)
                    && (this.m_cClipPanel.transform.localPosition.y - m_fClipParentY + m_fClipSizeY) >= (-item.m_cItem.transform.localPosition.y - 480))
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
    /// 初始化
    /// </summary>
    protected override void InitGUI()
    {

        GUI_FUNCTION.AYSNCLOADING_HIDEN();

        m_bBeLongPress = false;
        m_bHasPress = false;

        base.Show();

        if (this.m_cGUIObject == null)
        {
            //Main主资源
            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;


            this.m_cTopRightParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, TOP_RIGHT);
            this.m_cTopLeftParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, TOP_CANCEL_PARENT);

            this.m_cUITittle = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, UI_TITTLE);
            this.m_cIconParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, ICON_PARENT);
            this.m_cClipPanel = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(this.m_cGUIObject, CLIP_PANEL);
            this.m_cLbSort = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, UI_SORT);
            this.m_cLbCount = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LB_COUNT);

            this.m_cCancelBtn = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_CANCEL);

            GameObject btnsort = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_SORT);
            btnsort.AddComponent<GUIComponentEvent>().AddIntputDelegate(BtnSort_OnEvent);

            this.m_cHeroAtlasItem = (UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_HEROITEM);
        }

        if (m_bShowNull)
        {
            //加入置空按钮
            m_cResNull = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(BTN_NULL)) as GameObject;
            m_cBtnNull = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cResNull, SP_NULL);
            m_cResNull.transform.parent = this.m_cIconParent.transform;
            m_cResNull.transform.localPosition = Vector3.zero;
            m_cResNull.transform.localScale = Vector3.one;
        }

        if (this.m_cPanelClipRange == Vector4.zero)
        {
            this.m_cClipPanel.transform.localPosition = new Vector3(this.m_cClipPanel.transform.localPosition.x, this.m_fClipParentY, this.m_cClipPanel.transform.localPosition.z);
            this.m_cClipPanel.clipRange = new Vector4(this.m_cClipPanel.clipRange.x, this.m_fClipCenterY, this.m_cClipPanel.clipRange.z, this.m_fClipSizeY);
        }
        else
        {
            this.m_cClipPanel.transform.localPosition = this.m_cPanelLocalposition;
            this.m_cClipPanel.clipRange = this.m_cPanelClipRange;
        }
        m_fTop = m_cClipPanel.clipRange.w / 2f + m_cClipPanel.clipRange.y + m_cClipPanel.transform.localPosition.y;
        m_fBottom = m_cClipPanel.clipRange.y + m_cClipPanel.transform.localPosition.y - m_cClipPanel.clipRange.w / 2f;

 
        if (this.m_lstHeroShow != null)
        {
            this.m_lstHeroShow.ForEach((item) => { GameObject.DestroyImmediate(item.m_cItem); });
        }
        this.m_lstHeroShow.Clear();

        for (int i = 0; i < this.m_lstHero.Count; i++)
        {
            GameObject obj = GameObject.Instantiate(this.m_cHeroAtlasItem) as GameObject;
            obj.transform.parent = this.m_cIconParent.transform;
            obj.transform.localScale = Vector3.one;
            //OnInvisible temp = obj.AddComponent<OnInvisible>();
            //temp.OffTop = 240f;
           //temp.OffBottom = 320f;

            HeroShowItem tmp = new HeroShowItem(obj, this.m_lstHero[i]);
            m_lstHeroShow.Add(tmp);
        }

        //显示英雄数量
        this.m_cLbCount.text = Role.role.GetHeroProperty().GetAllHero().Count + "/" + Role.role.GetBaseProperty().m_iMaxHeroCount;

        SortLst();

        CTween.TweenPosition(this.m_cTopLeftParent, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-540, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cTopRightParent, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);

        SetLocalPos(Vector3.zero);

        //设置整体GUI点击GUIID
        this.m_cGUIMgr.SetCurGUIID(this.ID);

    //    this.m_cClipPanel.GetComponent<UIDraggablePanel>().repositionClipping = true;

    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        m_bBeLongPress = false;

        if (m_cBtnNull != null)
        {
            GameObject.DestroyImmediate(m_cBtnNull);
        }
        if (m_cResNull != null)
        {
            GameObject.DestroyImmediate(m_cResNull);
        }


        ResourcesManager.GetInstance().UnloadUnusedResources();

        //base.Hiden();
        GUI_FUNCTION.AYSNCLOADING_HIDEN();

        CTween.TweenPosition(this.m_cTopRightParent, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0));
        CTween.TweenPosition(this.m_cTopLeftParent, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-540, 270, 0),Destory);
    }

    /// <summary>
    /// 排序按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnSort_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            GAME_SETTING.m_eSortType = (SORT_TYPE)(((int)GAME_SETTING.m_eSortType + 1) % ((int)SORT_TYPE.MAX));

            GAME_SETTING.SetHeroSort(GAME_SETTING.m_eSortType);

            SortLst();
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
            //
        }
    }

    /// <summary>
    /// 重新排列英雄列表,先对是team的排序，在对非team的排序
    /// </summary>
    /// <param name="tmplst"></param>
    private void SortLst()
    {
        SetSortLabel();

        //新排序规则 ---- 策划时云确认 date:2014.1.26 
        //先对稀有度，领导力这两个排序中，相同的稀有度和领导力 再排序
        //      第一档     第二档      第三档         第四档    
        //      稀有度     属性         TableID        等级
        //      领导力
        //其中属性按照 树--水--火--雷--光--暗 排序
        //因此当用户选择稀有度排序时， 先对稀有度排序  在对都是4星的英雄 进行属性排序，属性中相同的在对 tableID排序，在对同tableID的英雄进行等级排序

        //优先显示在team中的先排序
        List<HeroShowItem> partyHeros = this.m_lstHeroShow.FindAll(new Predicate<HeroShowItem>((item) => { return CheckExistInTeams(Role.role.GetTeamProperty().GetAllTeam(), item.m_cHero.m_iID); }));
        //剩下不在team中的再排序
        List<HeroShowItem> nextHeros = this.m_lstHeroShow.Except(partyHeros).ToList();



        SortPartItem(ref partyHeros, GAME_SETTING.m_eSortType);
        SortPartItem(ref nextHeros, GAME_SETTING.m_eSortType);

        this.m_lstHeroShow.Clear();
        this.m_lstHeroShow.AddRange(partyHeros);
        this.m_lstHeroShow.AddRange(nextHeros);

        foreach (HeroShowItem item in this.m_lstHeroShow)
        {
            switch (GAME_SETTING.m_eSortType)
            {
                case SORT_TYPE.NATURE://属性排序
                    item.ShowNature();
                    break;
                case SORT_TYPE.LEVEL://等级排序
                    item.ShowLevel();
                    break;
                case SORT_TYPE.HP://HP排序
                    item.ShowHP();
                    break;
                case SORT_TYPE.ATTACK://攻击排序
                    item.ShowAttack();
                    break;
                case SORT_TYPE.DEFENCE://防御排序
                    item.ShowDefence();
                    break;
                case SORT_TYPE.REVERT://回复排序
                    item.ShowRecover();
                    break;
                case SORT_TYPE.STAR://星级排序
                    item.ShowStar();
                    break;
                case SORT_TYPE.COST://领导力排序
                    item.ShowCost();
                    break;
                case SORT_TYPE.NEW_OLD://新旧排序
                    item.ShowNewOld();
                    break;
                default:
                    Debug.LogError("排序错误");
                    break;
            }
        }

        for (int i = 0; i < this.m_lstHeroShow.Count; i++)
        {
            HeroShowItem item = this.m_lstHeroShow[i];
            int x = ((i + this.m_iShowOffsetX) % 5) * OFFSET_X;
            int y = ((i + this.m_iShowOffsetX) / 5) * OFFSET_Y;
            item.m_cItem.transform.localPosition = new Vector3(x, y, 0);
        }
    }

    /// <summary>
    /// 对一列开始排序
    /// </summary>
    /// <param name="tmplst"></param>
    private void SortPartItem(ref List<HeroShowItem> tmplst, SORT_TYPE type)
    {
        switch (type)
        {
            case SORT_TYPE.NATURE://属性排序
                tmplst.Sort(new Comparison<HeroShowItem>((i2, i1) => { return GetWeight(i1.m_cHero).CompareTo(GetWeight(i2.m_cHero)); }));
                break;
            case SORT_TYPE.LEVEL://等级排序
                tmplst.Sort(new Comparison<HeroShowItem>((i2, i1) => { return (i1.m_cHero.m_iLevel + GetMinWeight(i1.m_cHero)).CompareTo((i2.m_cHero.m_iLevel + GetMinWeight(i2.m_cHero))); }));
                break;
            case SORT_TYPE.HP://HP排序
                tmplst.Sort(new Comparison<HeroShowItem>((i2, i1) => { return (i1.m_cHero.GetMaxHP() + GetMinWeight(i1.m_cHero)).CompareTo((i2.m_cHero.GetMaxHP() + GetMinWeight(i2.m_cHero))); }));
                break;
            case SORT_TYPE.ATTACK://攻击排序
                tmplst.Sort(new Comparison<HeroShowItem>((i2, i1) => { return (i1.m_cHero.GetAttack() + GetMinWeight(i1.m_cHero)).CompareTo((i2.m_cHero.GetAttack() + GetMinWeight(i2.m_cHero))); }));
                break;
            case SORT_TYPE.DEFENCE://防御排序
                tmplst.Sort(new Comparison<HeroShowItem>((i2, i1) => { return (i1.m_cHero.GetDefence() + GetMinWeight(i1.m_cHero)).CompareTo((i2.m_cHero.GetDefence() + GetMinWeight(i2.m_cHero))); }));
                break;
            case SORT_TYPE.REVERT://回复排序
                tmplst.Sort(new Comparison<HeroShowItem>((i2, i1) => { return (i1.m_cHero.GetRecover()+ GetMinWeight(i1.m_cHero)).CompareTo((i2.m_cHero.GetRecover()+ GetMinWeight(i2.m_cHero))); }));
                break;
            case SORT_TYPE.STAR://星级排序
                tmplst.Sort(new Comparison<HeroShowItem>((i2, i1) => { return (i1.m_cHero.m_iStarLevel+ GetMinWeight(i1.m_cHero)).CompareTo((i2.m_cHero.m_iStarLevel+ GetMinWeight(i2.m_cHero))); }));
                break;
            case SORT_TYPE.COST://领导力排序
                tmplst.Sort(new Comparison<HeroShowItem>((i2, i1) => { return (i1.m_cHero.m_iCost+ GetMinWeight(i1.m_cHero)).CompareTo((i2.m_cHero.m_iCost+ GetMinWeight(i2.m_cHero))); }));
                break;
            case SORT_TYPE.NEW_OLD://新旧排序
                tmplst.Sort(new Comparison<HeroShowItem>((i2, i1) => { return i1.m_cHero.m_lGetTime.CompareTo(i2.m_cHero.m_lGetTime); }));
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 设置排序文字
    /// </summary>
    private void SetSortLabel()
    {
        switch (GAME_SETTING.m_eSortType)
        {
            case SORT_TYPE.NATURE:
                this.m_cLbSort.text = GAME_FUNCTION.STRING(STRING_DEFINE.NATURE);
                break;
            case SORT_TYPE.LEVEL:
                this.m_cLbSort.text = GAME_FUNCTION.STRING(STRING_DEFINE.LEVEL);
                break;
            case SORT_TYPE.HP:
                this.m_cLbSort.text = GAME_FUNCTION.STRING(STRING_DEFINE.HP);
                break;
            case SORT_TYPE.ATTACK:
                this.m_cLbSort.text = GAME_FUNCTION.STRING(STRING_DEFINE.ATTACK);
                break;
            case SORT_TYPE.DEFENCE:
                this.m_cLbSort.text = GAME_FUNCTION.STRING(STRING_DEFINE.DEFENCE);
                break;
            case SORT_TYPE.REVERT:
                this.m_cLbSort.text = GAME_FUNCTION.STRING(STRING_DEFINE.REVERT);
                break;
            case SORT_TYPE.STAR:
                this.m_cLbSort.text = GAME_FUNCTION.STRING(STRING_DEFINE.STAR);
                break;
            case SORT_TYPE.COST:
                this.m_cLbSort.text = GAME_FUNCTION.STRING(STRING_DEFINE.COST);
                break;
            case SORT_TYPE.NEW_OLD:
                this.m_cLbSort.text = GAME_FUNCTION.STRING(STRING_DEFINE.NEW_OLD);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 判断所有队伍列表中存在该英雄ID与否
    /// </summary>
    /// <param name="heroTeam"></param>
    /// <param name="heroId"></param>
    /// <returns></returns>
    protected bool CheckExistInTeams(HeroTeam[] heroTeam, int heroId)
    {
        return heroTeam.Any<HeroTeam>(new Func<HeroTeam, bool>((item) =>
        {
            return item.m_vecTeam.Contains(heroId);
        }));
    }

    /// <summary>
    /// 用于等级，hp attack defense revert cost star的排序
    /// eg  ：
    /// hp排序                    英雄1      英雄2
    ///                   hp值    560          560
    ///    剩余排序影响力    0.1243     0.9548    由剩下3个权重  ==》  属性权重+table权重+Lv权重 / 最大值200000 获得
    ///  </summary>
    /// <param name="hero"></param>
    /// <returns></returns>
    public float GetMinWeight(Hero hero)
    {
        float wei = GetNatureWeight(hero) + GetTableIdWeight(hero) + GetLevelWeight(hero);
        return wei / 200000;
    }

    /// <summary>
    /// 剩余基本排序
    /// </summary>
    /// <param name="hero"></param>
    /// <returns></returns>
    public float GetWeight(Hero hero)
    {
        float wei = GetNatureWeight(hero) + GetTableIdWeight(hero) + GetLevelWeight(hero);
        return wei;
    }

    /// <summary>
    /// 获得属性排序权重，保证下一级权重最大差值小于这一级的最小差值 
    /// eg                   英雄属性优先排序      英雄表ID再排序                                 英雄等级再排序                    最终排序值 保证上下级留有足够差值
    ///                       50000（水）      +    1 * 100  （tableid第一的英雄）       +1（英雄等级）                     =50101
    ///                       25000（树）     +     250 * 100 (tableid最后一个英雄)      +100（英雄等级）                =50100
    /// </summary>
    /// <param name="nature"></param>
    /// <returns></returns>
    public int GetNatureWeight(Hero hero)
    {
        int weight = 0;
        switch (hero.m_eNature)
        {
            case Nature.Fire: weight = 100000;
                break;
            case Nature.Water: weight = 125000;
                break;
            case Nature.Wood: weight = 150000;
                break;
            case Nature.Thunder: weight = 75000;
                break;
            case Nature.Bright: weight = 50000;
                break;
            case Nature.Dark: weight = 25000;
                break;
            default:
                break;
        }
        return weight;
    }

    /// <summary>
    /// 获得英雄tableID排序权重
    /// </summary>
    /// <param name="tableID"></param>
    /// <returns></returns>
    public int GetTableIdWeight(Hero hero)
    {
        return (250-hero.m_iTableID) * 100;
    }

    /// <summary>
    /// 获取英雄等级排序
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public int GetLevelWeight(Hero hero)
    {
        return (100 - hero.m_iLevel);
    }
}

public enum SORT_TYPE
{
    NATURE = 0, //属性排序
    LEVEL = 1,  //等级排序
    HP = 2, //HP排序
    ATTACK = 3, //攻击力排序
    DEFENCE = 4,    //防御力排序
    REVERT = 5, //恢复力排序
    STAR = 6,   //星级排序
    COST = 7,   //领导力排序
    NEW_OLD = 8,    //新旧排序
    MAX = 9,    //最大值
}