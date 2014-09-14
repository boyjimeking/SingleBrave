using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;

//Micro.Sanvey
//2013.12.24
//sanvey.china@gmail.com

/// <summary>
/// 物品选择基类
/// </summary>
public class GUIItemSelectBase : GUIBase
{
    public const string RES_MAIN = "GUI_PropsPreview";//主资源地址
    public const string RES_PROPSITEM = "GUI_PropsPreviewItem";//物品项地址

    private const string TOPPANEL = "TopPanel";//导航栏地址
    private const string MAINPANEL = "MainPanel";//主面板地址
    private const string LB_ItemCount = "LBItemCount"; //物品数量
    private const string BACK_BUTTON = "TopPanel/Button_Back";//房名返回按钮地址
    private const string LB_TITLE = "TopPanel/Label";  //标题
    private const string LISTVIEW = "MainPanel/ListView/UITable";//列表地址
    private const string LISTVIEWPANEL = "MainPanel/ListView";  //裁切panel
    private const string SP_BACK = "BG";  //背景遮罩

    private GameObject m_cTopPanel;//导航栏
    protected GameObject m_cMainPanel;//主面板地址
    protected GameObject m_cBtnBack;//返回按钮
    protected GameObject m_cListView;//列表地址
    protected UIPanel m_cClipPanel; //裁切panel
    protected UILabel m_cUITittle;  //标题
    protected UILabel m_cLbCount; //当前数量
    protected UISprite m_cBack;  //背景遮罩

    private const int OFFSET_X = 120;   //X偏移量
    private const int OFFSET_Y = -120;  //Y偏移量

    protected UnityEngine.Object m_cResItem;  //Item资源

    protected int m_iShowOffsetX = 0; //展示X偏移量索引
    protected float m_fClipParentY = 6.523351F;   //剪裁父节点Y轴坐标
    protected float m_fCipParentY2 = 18F;  //第一行没有new的时候，有new的话 会被拉开长度
    protected float m_fClipCenterY = -66.52333F;   //剪裁中间点Y轴坐标
    protected float m_fClipCenterY2 = -77.99999F; //第一行没有new的时候
    protected float m_fClipSizeY = 540; //剪裁Y轴大小

    protected bool m_bIfWithBattle = false;  //是否减去战斗物品显示
    protected bool m_bEqEnable = false;  //是否将装备物品变灰

    //变量
    protected List<ItemShowItem> m_lstItemShow = new List<ItemShowItem>();    //显示英雄对象
    protected List<Item> m_lstItems = new List<Item>();  //英雄列表
    protected List<ShowItemData> m_lstShows;  //根据tableid去更新显示

    /// <summary>
    /// 叠加显示对象
    /// </summary>
    public class ShowItemData
    {
        public int m_iNum;
        public int m_iTable;
        public int m_iId;
        public bool m_bNew;

        /// <summary>
        /// 装备
        /// </summary>
        /// <param name="num"></param>
        /// <param name="table"></param>
        /// <param name="id"></param>
        public ShowItemData(int num, int table, int id, bool bnew)
        {
            m_iNum = num;
            m_iId = id;
            m_iTable = table;
            m_bNew = bnew;
        }

        /// <summary>
        /// 素材和消耗品
        /// </summary>
        /// <param name="num"></param>
        /// <param name="table"></param>
        public ShowItemData(int num, int table, bool bnew)
        {
            m_iNum = num;
            m_iTable = table;
            m_bNew = bnew;
        }
    }

    /// <summary>
    /// 物品显示Item
    /// </summary>
    public class ItemShowItem
    {
        public GameObject m_cRes;  //item整个显示对象
        public Item m_cItem;    //英雄实例

        //变化属性
        public UILabel m_cItemNum;
        public UILabel m_cItemName;
        public UISprite m_cItemSprite;
        public UISprite m_cItemBorder;
        public UISprite m_cItemNew;
        public UISprite m_cItemE;
        public UISprite m_cItemBg;

        private const string LB_Count = "Lab_Count";  //数量
        private const string LB_Name = "Lab_Name";    //名称
        private const string SP_ITEMPATH = "Spr_Icon"; //物品图标
        private const string SP_ITEMBORDER = "Frame";  //物品边框
        private const string SP_ITEMNEW = "Spr_New";   //最新
        private const string SP_E = "Spr_E";  //装备标志
        private const string SP_BG = "Bg";

        public ItemShowItem(GameObject iconObj, Item item)
        {
            if (iconObj == null || item == null) return;

            this.m_cRes = iconObj;
            this.m_cItem = item;

            this.m_cItemNum = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cRes, LB_Count);
            this.m_cItemName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cRes, LB_Name);
            this.m_cItemSprite = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cRes, SP_ITEMPATH);
            this.m_cItemBorder = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cRes, SP_ITEMBORDER);
            this.m_cItemNew = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cRes, SP_ITEMNEW);
            this.m_cItemBg = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cRes, SP_BG);
            this.m_cItemE = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cRes, SP_E);
        
        }
    }

    public GUIItemSelectBase(GUIManager mgr, int guiid, GUILAYER layer)
        : base(mgr, guiid, layer)
    {
    }


    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        m_cTopPanel = null;//导航栏
        m_cMainPanel = null;//主面板地址
        m_cBtnBack = null;//返回按钮
        m_cListView = null;//列表地址
        m_cClipPanel = null; //裁切panel
        m_cUITittle = null;  //标题
        m_cLbCount = null; //当前数量
        m_cBack = null;  //背景遮罩
        m_cResItem = null;


        if (null != m_lstItemShow) m_lstItemShow.Clear();

        base.Destory();
    }

    /// <summary>
    /// 展示
    /// </summary>
    protected override void  InitGUI()
    {
        base.Show();

        GUI_FUNCTION.AYSNCLOADING_HIDEN();

        if (this.m_cGUIObject == null)
        {
			this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            this.m_cMainPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, MAINPANEL);

            this.m_cTopPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, TOPPANEL);

            this.m_cBtnBack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BACK_BUTTON);

            this.m_cListView = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LISTVIEW);

            this.m_cClipPanel=GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(this.m_cGUIObject,LISTVIEWPANEL);

			this.m_cResItem = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_PROPSITEM);

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


        this.m_cClipPanel.transform.localPosition = new Vector3(this.m_cClipPanel.transform.localPosition.x, this.m_fClipParentY, this.m_cClipPanel.transform.localPosition.z);
        this.m_cClipPanel.clipRange = new Vector4(this.m_cClipPanel.clipRange.x, this.m_fClipCenterY, this.m_cClipPanel.clipRange.z, this.m_fClipSizeY);


        m_lstShows = ChangeToShows();


        for (int i = 0; i < this.m_lstShows.Count; i++)
        {
            GameObject obj = GameObject.Instantiate(this.m_cResItem) as GameObject;
            obj.transform.parent = this.m_cListView.transform;
            obj.transform.localScale = Vector3.one;
            //OnInvisible temp = obj.AddComponent<OnInvisible>();
            //temp.OffTop = 240f;
            //temp.OffBottom = 340f;

            ItemShowItem tmp = new ItemShowItem(obj, this.m_lstItems[i]);
            m_lstItemShow.Add(tmp);
        }

        //显示Item数量
        this.m_cLbCount.text =Role.role.GetItemProperty().GetAllItemCount() + "/" + Role.role.GetBaseProperty().m_iMaxItem;

        UpdateShowList();

        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-420, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);

        SetLocalPos(Vector3.zero);
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        ResourceMgr.UnloadUnusedResources();

        this.m_cBack.enabled = false;

        //base.Hiden();
        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-420, 270, 0));
        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0),Destory);
    }

    /// <summary>
    /// 将素材，消耗品，装备分别转换到指定显示格式
    /// </summary>
    /// <param name="m_lstItems"></param>
    /// <returns></returns>
    private List<ShowItemData> ChangeToShows()
    {
        if (m_bIfWithBattle)
        {
            m_lstItems.ForEach((q) => {
                int tmpnum = q.m_iDummyNum;
                //出售界面 减去战斗装备物品
                Item tmpp = Role.role.GetItemProperty().GetBattleItemByTableID(q.m_iTableID);
                if (tmpp != null)
                {
                    tmpnum -= tmpp.m_iNum;
                }
                q.m_iDummyNum = tmpnum;
            });
        }

        //除去数量是0的Item
        m_lstItems = m_lstItems.FindAll(new Predicate<Item>((q) =>
            {
                //int tmpnum=q.m_iNum;
                //if (m_bIfWithBattle)  //有些界面需要展示所有，有些界面需要展示除去战斗装备的物品
                //{
                //    //出售界面 减去战斗装备物品
                //    Item tmpp = Role.role.GetItemProperty().GetBattleItemByTableID(q.m_iTableID);
                //    if (tmpp != null)
                //    {
                //        tmpnum -= tmpp.m_iNum;
                //    }
                //}
                return q.m_iDummyNum > 0;
            }));

        //拆分超过最大叠加数量的
        List<Item> allMaxItems = m_lstItems.FindAll(new Predicate<Item>((q) => { return q.m_iDummyNum > ItemTableManager.GetInstance().GetItem(q.m_iTableID).MaxNum; }));

        foreach (Item q in allMaxItems)
        {
            int maxNum = ItemTableManager.GetInstance().GetItem(q.m_iTableID).MaxNum;
            List<Item> tmplst = new List<Item>();
            // eg 102 99 / 4 99
            int tmpint = q.m_iDummyNum;
            //if (m_bIfWithBattle)  //有些界面需要展示所有，有些界面需要展示除去战斗装备的物品
            //{
            //    //出售界面 减去战斗装备物品
            //    Item tmpp = Role.role.GetItemProperty().GetBattleItemByTableID(q.m_iTableID);
            //    if (tmpp != null)
            //    {
            //        tmpint -= tmpp.m_iNum;
            //    }
            //}

            while (tmpint > 0)
            {
                Item it = new Item(q.m_iTableID);
                it.m_iID = q.m_iID;
                it.m_bNew = q.m_bNew;
                int tmpNum = tmpint - maxNum > 0 ? maxNum : tmpint; //99
                it.m_iNum = tmpNum;  //99
                it.m_iDummyNum = tmpNum;  //99
                tmplst.Add(it);

                tmpint -= tmpNum; //3
            }

            int index = m_lstItems.IndexOf(q);
            m_lstItems.Remove(q);
            m_lstItems.InsertRange(index, tmplst);

        }

        m_lstItems.Sort(new Comparison<Item>((q1, q2) => { return q1.m_iTableID.CompareTo(q2.m_iTableID); }));
        List<ShowItemData> re = new List<ShowItemData>();

        foreach (var q in m_lstItems)
        {
            if (q.m_eType == ITEM_TYPE.EQUIP)
            {
                ShowItemData tmp = new ShowItemData(1, q.m_iTableID, q.m_iID,q.m_bNew);
                re.Add(tmp);
            }
            else
            {
                ShowItemData tmp = new ShowItemData(q.m_iDummyNum, q.m_iTableID,q.m_bNew);
                re.Add(tmp);
            }
        }

        return re;
    }

    public override bool Update()
    {
        if (IsShow())
        {
            foreach (ItemShowItem item in m_lstItemShow)
            {
                if ((this.m_cClipPanel.transform.localPosition.y - m_fClipParentY + OFFSET_Y) <= (-item.m_cRes.transform.localPosition.y + 240)
                    && (this.m_cClipPanel.transform.localPosition.y - m_fClipParentY + m_fClipSizeY) >= (-item.m_cRes.transform.localPosition.y - 480))
                {
                    //if (item.m_cRes.activeSelf)
                    {
                        item.m_cRes.SetActive(true);
                    }

                }
                else
                {
                    //if (item.m_cRes.activeSelf)
                    {
                        item.m_cRes.SetActive(false);
                    }
                }
            }
        }

        return base.Update();
    }

    /// <summary>
    /// 根据列表刷新显示
    /// </summary>
    private void UpdateShowList()
    {
        bool FirstLineHasNew = false;

        for (int i = 0; i < this.m_lstShows.Count; i++)
        {
            ItemShowItem item = m_lstItemShow[i];
            ShowItemData data = this.m_lstShows[i];
            //物品短名称
            ItemTable tmp = ItemTableManager.GetInstance().GetItem(data.m_iTable);
            item.m_cItemName.text = tmp.ShortName;
            //物品显示数量
            int showNum = data.m_iNum;
            //if (m_bIfWithBattle)  //有些界面需要展示所有，有些界面需要展示除去战斗装备的物品
            //{
            //    //出售界面 减去战斗装备物品
            //    Item tmpp = Role.role.GetItemProperty().GetBattleItemByTableID(data.m_iTable);
            //    if (tmpp != null)
            //    {
            //        showNum -= tmpp.m_iNum;
            //    }
            //}
            item.m_cItemNum.text = "×" + showNum.ToString();
            //物品图标
            GUI_FUNCTION.SET_ITEMM(item.m_cItemSprite, tmp.SpiritName);
            GUI_FUNCTION.SET_ITEM_BORDER(item.m_cItemBorder, (ITEM_TYPE)tmp.Type);
            //是否new
            if (data.m_bNew)
            {
                if (i<5)  //第一行是否存在new
                {
                    FirstLineHasNew = true;
                }
                item.m_cItemNew.enabled = true;
                TweenAlpha.Begin(item.m_cItemNew.gameObject, 1, 0).style = UITweener.Style.PingPong;
            }
            else
            {
                item.m_cItemNew.enabled = false;
            }
            //物品装备
            item.m_cItemE.enabled = false;
            if (tmp.Type == (int)ITEM_TYPE.EQUIP)  //检查物品是否被装备
            {
                if (data.m_iId != -1)
                {
                    if (IfHeroHasItem(data.m_iId))
                    {
                        if (m_bEqEnable)
                        {
                            item.m_cItemBg.depth = item.m_cItemBg.depth + 2;
                            item.m_cRes.collider.enabled = false;
                        }
                        item.m_cItemE.enabled = true;
                    }
                }
            }

            int x = ((i + this.m_iShowOffsetX) % 5) * OFFSET_X;
            int y = ((i + this.m_iShowOffsetX) / 5) * OFFSET_Y;
            item.m_cRes.transform.localPosition = new Vector3(x, y, 0);
        }

        //第一行有new字样 重新调整clip,new的精灵会扩大item的大小
        if (!FirstLineHasNew)
        {
            this.m_cClipPanel.transform.localPosition = new Vector3(this.m_cClipPanel.transform.localPosition.x, this.m_fCipParentY2, this.m_cClipPanel.transform.localPosition.z);
            this.m_cClipPanel.clipRange = new Vector4(this.m_cClipPanel.clipRange.x, this.m_fClipCenterY2, this.m_cClipPanel.clipRange.z, this.m_fClipSizeY);
        }
    }

    private bool IfHeroHasItem(int ItemId)
    {
        return Role.role.GetHeroProperty().GetAllHero().Exists(new Predicate<Hero>((hero) => { return hero.m_iEquipID == ItemId; }));
    }
}