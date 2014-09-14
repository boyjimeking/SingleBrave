//Micro.Sanvey
//2013.12.2
//sanvey.china@gmail.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;

/// <summary>
/// 英雄装备
/// </summary>
public class GUIHeroEquipment : GUIBase
{

    /// <summary>
    /// 装备展示Item
    /// </summary>
    public class HeroEquipItemShow
    {
        public GameObject m_cItem;  //item整个显示对象
        public UISprite m_cBorder;  //边框
        public UISprite m_cFrame; //底色
        public UISprite m_cMonster;  //头像
        public UILabel m_cLbBottom; //字体底部
        public UISprite m_cENew; //装备new
        public UISprite m_cEName; //装备精灵
        public GameObject m_cBtnEq;  //装备按钮
        public GameObject m_cMonsterItem; //头像Item
        public UISprite m_cMonsterTop;  //party字样闪烁
        public GameObject m_cEqItem;// 装备Item
        public UILabel m_cLbEName; //装备名称
        public UILabel m_cLbEInfo; //装备描述
        public UISprite m_cEFrame; //装备外框

        private const string RES_MONSTERITEM = "GUI_MonsterItem"; //英雄头像
        private const string RES_BORDER = "ItemBorder";  //英雄头像资源地址
        private const string RES_FRAME = "ItemFrame";  //英雄头像资源地址
        private const string RES_MONSTER = "ItemMonster";  //英雄头像资源地址
        private const string RES_LBBOTTOM = "LabelBottom";  //英雄头像资源地址
        private const string RES_EITEM = "GUI_EItem";  //装备地址
        private const string RES_EQ = "Eq";  //装备图片地址
        private const string RES_NES = "New";  //装备是否新
        private const string RES_BG = "Bg";  //装备外框
        private const string LB_EINFO = "LbEInfo";  //装备描述信息
        private const string LB_EName = "LbEName";  //装备名称
        private const string BTN_EITEM = "BtnEq";  //装备按钮
        private const string ITEM_TOP = "ItemTop";  //party字样

        public Hero m_cItemHero; //该装备的英雄
        public Item m_cItemId;   //装备

        public HeroEquipItemShow(UnityEngine.Object item)
        {
            m_cItem = GameObject.Instantiate(item) as GameObject;
            m_cMonsterItem = GUI_FINDATION.GET_GAME_OBJECT(this.m_cItem, RES_MONSTERITEM);
            m_cBorder = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cMonsterItem, RES_BORDER);
            m_cFrame = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cMonsterItem, RES_FRAME);
            m_cMonster = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cMonsterItem, RES_MONSTER);
            m_cLbBottom = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cMonsterItem, RES_LBBOTTOM);
            m_cEqItem = GUI_FINDATION.GET_GAME_OBJECT(this.m_cItem, RES_EITEM);
            m_cENew = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cEqItem, RES_NES);
            m_cEName = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cEqItem, RES_EQ);
            m_cBtnEq = GUI_FINDATION.GET_GAME_OBJECT(this.m_cItem, BTN_EITEM);
            m_cLbEName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cItem, LB_EName);
            m_cLbEInfo = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cItem, LB_EINFO);
            m_cEFrame = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cEqItem, RES_BG);
            m_cMonsterTop = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cMonsterItem, ITEM_TOP);
        }
    }

    private const string RES_MAIN = "GUI_HeroEquipment";         //菜单资源地址
    private const string BTN_CANCEL = "Title_Cancel/Btn_Cancel";        //取消按钮地址
    private const string PAN_CANCEL = "Title_Cancel";        //取消Pan地址
    private const string LB_SORT = "Title_Cancel/Lb_Sort";   //显示现在排序规则地址
    private const string PAN_RIGHT = "PanInfo";  //滑出Panel地址
    private const string PAN_CLIP = "PanInfo/Panel";
    private const string RES_TABLE = "PanInfo/Panel/Table";  //Table地址
    private const string RES_EquipmentItem = "GUI_EquipmentItem";  //装备列表Item

    private GameObject m_cPanSlide;   //panel滑动
    private GameObject m_cPanCancel;  //取消按钮Panel
    private GameObject m_cTable;  //table
    private UIPanel m_cClipPanel; //裁剪Panel
    private UnityEngine.Object m_cItem;  //item地址

    private List<HeroEquipItemShow> m_lstItemShow; //显示装备对象
    public Dictionary<int,int> m_lstOldEquips;  //初始玩家装备，用于对比状态上传最新更换装备
    private List<Item> m_lstEquips;   //所有玩家装备
    private int m_iSelectedItemId = 0;   //当前选中的装备
    private int m_iSelectedHeroId = 0;   //当前选中的英雄

    private int m_iYOffset = 130;  //每个行上下间距
    private int m_iXOffset = -12;

    protected int m_iShowOffsetX = 0; //展示X偏移量索引
    protected float m_fClipParentY =31;   //剪裁父节点Y轴坐标
    protected float m_fClipCenterY = -88;   //剪裁中间点Y轴坐标
    protected float m_fClipSizeY = 542; //剪裁Y轴大小

    private const string SP_NAME_TEAM_USE = "pack_used";    //使用中
    private const string SP_NAME_AT_TEAM = "pack_atTeam";   //在队伍中
    private const string SP_NEW = "new_icon";  //最新获得

    public GUIHeroEquipment(GUIManager mgr)
        : base(mgr, GUI_DEFINE.GUIID_HEROEQUIPMENT, GUILAYER.GUI_PANEL)
    {
    }

    /// <summary>
    /// 初始化
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
            //取消按钮
            var cancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_CANCEL);
            GUIComponentEvent gui_event = cancel.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnCancel_OnEvent);
            this.m_cPanCancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_CANCEL);
            //table
            m_cClipPanel = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(this.m_cGUIObject, PAN_CLIP);
            m_cTable = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, RES_TABLE);
            m_cItem = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_EquipmentItem);
        }


        this.m_cClipPanel.transform.localPosition = new Vector3(this.m_cClipPanel.transform.localPosition.x, this.m_fClipParentY, this.m_cClipPanel.transform.localPosition.z);
        this.m_cClipPanel.clipRange = new Vector4(this.m_cClipPanel.clipRange.x, this.m_fClipCenterY, this.m_cClipPanel.clipRange.z, this.m_fClipSizeY);


        //获取所有是装备的物品
        List<Item> m_lstEquips = Role.role.GetItemProperty().GetItemsType(ITEM_TYPE.EQUIP);
        m_lstEquips.RemoveAll(new Predicate<Item>((q) => { return q.m_iNum == 0; }));

        m_lstEquips.Sort((a, b) => { return a.m_iTableID.CompareTo(b.m_iTableID); });

        if (m_lstItemShow == null)
        {
            m_lstItemShow = new List<HeroEquipItemShow>();
        }
        else
        {
            m_lstItemShow.ForEach((item) => { GameObject.DestroyImmediate(item.m_cItem); });
            m_lstItemShow.Clear();
        }

        for (int i = 0; i < m_lstEquips.Count; i++)
        {
            HeroEquipItemShow tmp = new HeroEquipItemShow(m_cItem);
            tmp.m_cItem.transform.parent = m_cTable.gameObject.transform;
            tmp.m_cItem.transform.localScale = Vector3.one;
            tmp.m_cItemId = m_lstEquips[i];
            GUIComponentEvent gui_event = tmp.m_cBtnEq.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(Equip_OnEvent, i);
            gui_event = tmp.m_cItem.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(Item_OnEvent, i);
            tmp.m_cMonsterItem.AddComponent<GUIComponentEvent>().AddIntputDelegate(Item_OnEvent, i);
            m_lstItemShow.Add(tmp);
        }

        SetSelectHeroItem(this.m_iSelectedHeroId);

        //更新显示
        UpdateEquipShow();

        this.m_cGUIMgr.SetCurGUIID(this.ID);

        CTween.TweenPosition(this.m_cPanCancel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-430, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cPanSlide, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);

        SetLocalPos(Vector3.zero);

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_SELECT_EPUIP));
        gui.HeroWarn();
    }

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
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_EquipmentItem);
        }
        else
        {
            InitGUI();
        }
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        List<int> heros = new List<int>();
        List<int> items = new List<int>();

        Dictionary<int, int> newdic = new Dictionary<int, int>();

        Debug.Log(m_lstItemShow.Count);

        foreach (var i in m_lstItemShow)
        {
            newdic.Add(i.m_cItemId.m_iID, i.m_cItemHero == null ? -1 : i.m_cItemHero.m_iID);
        }

        foreach (var q in newdic)
        {
            if (q.Value != m_lstOldEquips[q.Key])
            {
                if (q.Value == -1) //说明该物品被英雄卸载了，则更新旧的英雄id的装备为-1
                {
                    heros.Add(m_lstOldEquips[q.Key]);
                    items.Add(-1);
                }
                else  //不一样就说明是重新装备了物品
                {
                    heros.Add(q.Value);
                    items.Add(q.Key);
                }
            }
        }

        // 原始状态   -->  改变后状态   -->  更新列表      -->  优化更新列表
        // 刀  关羽   -->  刀  -1       -->  关羽 :  -1    -->   
        // 剑  刘备   -->  剑  关羽     -->  关羽 ： 剑    -->  关羽 ： 剑
        // 枪  赵云   -->  抢  -1       -->  赵云 ： -1    -->  赵云 ： -1
        // 弓  -1     -->  弓  黄忠     -->  黄忠 ： 弓    -->  黄忠 ： 弓

        //找到是重新装备的 关羽：刀|刘备：剑 --> |关羽：剑|关羽：-1 
        List<int> needdo = new List<int>();
        for (int i = 0; i < heros.Count; i++)
        {
            List<int> re = heros.FindAll(new Predicate<int>((q) =>
             {
                 return q == heros[i];
             }));
            if (re.Count>1)
            {
                if (items[i]==-1)
                {
                    needdo.Add(i);
                }
            }
        }
        for (int i = 0; i < needdo.Count; i++)  //优化做更改操作的，增和删不用优化
        {
            heros.RemoveAt(needdo[needdo.Count - 1 - i]);
            items.RemoveAt(needdo[needdo.Count - 1 - i]);
        }

        if (heros.Count > 0)
        {
            SendAgent.SendHeroEquipUpdateReq(
              Role.role.GetBaseProperty().m_iPlayerId, heros.ToArray<int>(), items.ToArray<int>());
        }

        CTween.TweenPosition(this.m_cPanSlide, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0));
        CTween.TweenPosition(this.m_cPanCancel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-430, 270, 0),Destory);

        ResourceMgr.UnloadUnusedResources();
        base.Hiden();
    }

    public override void HidenImmediately()
    {

        List<int> heros = new List<int>();
        List<int> items = new List<int>();

        Dictionary<int, int> newdic = new Dictionary<int, int>();

        if (m_lstItemShow != null)
        {
            foreach (var i in m_lstItemShow)
            {
                newdic.Add(i.m_cItemId.m_iID, i.m_cItemHero == null ? -1 : i.m_cItemHero.m_iID);
            }
        }
        
        foreach (var q in newdic)
        {
            if (q.Value != m_lstOldEquips[q.Key])
            {
                if (q.Value == -1) //说明该物品被英雄卸载了，则更新旧的英雄id的装备为-1
                {
                    heros.Add(m_lstOldEquips[q.Key]);
                    items.Add(-1);
                }
                else  //不一样就说明是重新装备了物品
                {
                    heros.Add(q.Value);
                    items.Add(q.Key);
                }
            }
        }

        // 原始状态   -->  改变后状态   -->  更新列表      -->  优化更新列表
        // 刀  关羽   -->  刀  -1       -->  关羽 :  -1    -->   
        // 剑  刘备   -->  剑  关羽     -->  关羽 ： 剑    -->  关羽 ： 剑
        // 枪  赵云   -->  抢  -1       -->  赵云 ： -1    -->  赵云 ： -1
        // 弓  -1     -->  弓  黄忠     -->  黄忠 ： 弓    -->  黄忠 ： 弓

        //找到是重新装备的 关羽：刀|刘备：剑 --> |关羽：剑|关羽：-1 
        List<int> needdo = new List<int>();
        for (int i = 0; i < heros.Count; i++)
        {
            List<int> re = heros.FindAll(new Predicate<int>((q) =>
            {
                return q == heros[i];
            }));
            if (re.Count > 1)
            {
                if (items[i] == -1)
                {
                    needdo.Add(i);
                }
            }
        }
        for (int i = 0; i < needdo.Count; i++)  //优化做更改操作的，增和删不用优化
        {
            heros.RemoveAt(needdo[needdo.Count - 1 - i]);
            items.RemoveAt(needdo[needdo.Count - 1 - i]);
        }


        if (heros.Count > 0)
        {
            SendAgent.SendHeroEquipUpdateReq(
              Role.role.GetBaseProperty().m_iPlayerId, heros.ToArray<int>(), items.ToArray<int>());
        }

        ResourceMgr.UnloadUnusedResources();
        base.HidenImmediately();
        Destory();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        m_lstEquips = null;
        m_lstItemShow = null;
        m_lstOldEquips = null;

        m_cPanSlide = null;   //panel滑动
        m_cPanCancel = null;  //取消按钮Panel
        m_cTable = null;  //table
        m_cClipPanel = null; //裁剪Panel
        m_cItem = null;  //item地址

        base.Destory();
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

            //GUIHeroEquipSelect heropre = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HEROEQUIPSELECT) as GUIHeroEquipSelect;
            //heropre.Show();

            if (false)
            {
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERO_MENU).Show);
            }
            else
            {
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERO_MENU).Show();
            }
        }
    }

    /// <summary>
    /// 装备按钮点击
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void Equip_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            //HidenNotMove();
            HidenNotSend();

            m_iSelectedItemId = (int)args[0];

            //SetSelectHeroItem(m_iSelectedHeroId);

            GUIHeroEquipSelect heropre = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HEROEQUIPSELECT) as GUIHeroEquipSelect;
            heropre.m_bShowNull = false;
            heropre.m_lstOldEquip = this.m_lstOldEquips;
            heropre.Show();
        }
    }

    /// <summary>
    /// 装备Item行点击
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void Item_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            //HidenNotMove();
            HidenNotSend();

            m_iSelectedItemId = (int)args[0];

            //SetSelectHeroItem(m_iSelectedHeroId);

            GUIHeroEquipSelect heropre = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HEROEQUIPSELECT) as GUIHeroEquipSelect;
            heropre.m_bShowNull = (m_lstItemShow[m_iSelectedItemId].m_cItemHero != null);
            heropre.m_lstOldEquip  =this.m_lstOldEquips;
            heropre.Show();

        }
    }

    /// <summary>
    /// 根据装备，获得拥有它的Hero
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private Hero IfHeroHasEq(Item item)
    {
        return Role.role.GetHeroProperty().GetAllHero().Find(new Predicate<Hero>((tmp) =>
        {
            return tmp.m_iEquipID == item.m_iID;

        }));
    }

    /// <summary>
    /// 刷新装备显示
    /// </summary>
    private void UpdateEquipShow()
    {
        this.m_cGUIObject.SetActive(true);

        for (int i = 0; i < m_lstItemShow.Count; i++)
        {
            HeroEquipItemShow tmpshow = m_lstItemShow[i];
            Item tmpitem = tmpshow.m_cItemId;

            //物品
            GUI_FUNCTION.SET_ITEMM(tmpshow.m_cEName, tmpitem.m_strSprName);
            GUI_FUNCTION.SET_ITEM_BORDER(tmpshow.m_cEFrame, tmpitem.m_eType);
            if (tmpitem.m_bNew)
            {
                tmpshow.m_cENew.enabled = true;
                TweenAlpha.Begin(tmpshow.m_cENew.gameObject, 1, 0).style = UITweener.Style.PingPong;
            }
            else
            {
                tmpshow.m_cENew.enabled = false;
            }

            //装备名称
            tmpshow.m_cLbEName.text = tmpitem.m_strName;
            //装备描述
            tmpshow.m_cLbEInfo.text = tmpitem.m_strDesc;
            //获取拥有该物品的英雄
            Hero ehero = IfHeroHasEq(tmpitem);
            if (ehero == null)
            {
                tmpshow.m_cMonsterItem.SetActive(false);
                tmpshow.m_cBtnEq.SetActive(true);
                tmpshow.m_cItemHero = null;
            }
            else
            { 
                //当前使用队伍
                int currentTeam = Role.role.GetBaseProperty().m_iCurrentTeam;
                if (Role.role.GetTeamProperty().GetTeam(currentTeam).GetHeroIndex(ehero) >= 0)
                {
                    tmpshow.m_cMonsterTop.enabled = true;
                    tmpshow.m_cMonsterTop.spriteName = SP_NAME_TEAM_USE;
                    tmpshow.m_cMonsterTop.MakePixelPerfect();
                    TweenAlpha.Begin(tmpshow.m_cMonsterTop.gameObject, 1, 0).style = UITweener.Style.PingPong;
                }
                else
                {
                    bool exist = false;
                    foreach (HeroTeam item in Role.role.GetTeamProperty().GetAllTeam())
                    {
                        if (item.GetHeroIndex(ehero) > 0)
                        {
                            exist = true;
                            break;
                        }
                    }
                    //队伍中
                    if (exist)
                    {
                        tmpshow.m_cMonsterTop.enabled = true;
                        tmpshow.m_cMonsterTop.spriteName = SP_NAME_AT_TEAM;
                        tmpshow.m_cMonsterTop.MakePixelPerfect();
                        TweenAlpha.Begin(tmpshow.m_cMonsterTop.gameObject, 1, 0).style = UITweener.Style.PingPong;
                    }
                    else
                    {
                        if (ehero.m_bNew)
                        {
                            tmpshow.m_cMonsterTop.enabled = true;
                            tmpshow.m_cMonsterTop.spriteName = SP_NEW;
                            tmpshow.m_cMonsterTop.MakePixelPerfect();
                            TweenAlpha.Begin(tmpshow.m_cMonsterTop.gameObject, 1, 0).style = UITweener.Style.PingPong;
                        }
                        else
                        {
                            tmpshow.m_cMonsterTop.enabled = false;
                        }
                    }
                }

                tmpshow.m_cMonsterItem.SetActive(true);
                tmpshow.m_cBtnEq.SetActive(false);
                //设置边框
                GUI_FUNCTION.SET_HeroBorderAndBack(tmpshow.m_cBorder, tmpshow.m_cFrame, ehero.m_eNature);
                //更新英雄头像
                GUI_FUNCTION.SET_AVATORS(tmpshow.m_cMonster, ehero.m_strAvatarM);
                //更新英雄等级
                tmpshow.m_cLbBottom.text = "Lv." + ehero.m_iLevel;
                tmpshow.m_cItemHero = ehero;
            }
        }

        if (null == m_lstOldEquips)
        {
            m_lstOldEquips = new Dictionary<int, int>();
            for (int i = 0; i < m_lstItemShow.Count; i++)
            {
                m_lstItemShow[i].m_cItem.transform.localPosition = new Vector3(m_iXOffset, (1 - i) * m_iYOffset, 0);

                m_lstOldEquips.Add(m_lstItemShow[i].m_cItemId.m_iID, m_lstItemShow[i].m_cItemHero == null ? -1 : m_lstItemShow[i].m_cItemHero.m_iID);
            }
        }
        else
        {
            for (int i = 0; i < m_lstItemShow.Count; i++)
            {
                m_lstItemShow[i].m_cItem.transform.localPosition = new Vector3(m_iXOffset, (1 - i) * m_iYOffset, 0);
            }
        }
    }

    /// <summary>
    /// 设置英雄
    /// </summary>
    public void SetSelectHeroItem(int HeroId)
    {
        Debug.Log("heroID  "+HeroId);

        //置空，移除
        if (HeroId == -1)
        {
            Item Equip = m_lstItemShow[m_iSelectedItemId].m_cItemId;
            //已经装备了 卸掉 给新英雄装备
            Hero tmp = IfHeroHasEq(Equip);
            if (tmp != null)
            {
                tmp.m_iEquipID = -1;
                Role.role.GetHeroProperty().UpdateHero(tmp);
            }
        }
        else if(HeroId>0)
        {
            Debug.Log("m_lstItemShow  " + m_lstItemShow.Count);
            Debug.Log("m_iSelectedItemId  " + m_iSelectedItemId);

            Item Equip = m_lstItemShow[m_iSelectedItemId].m_cItemId;
            //已经装备了 卸掉 给新英雄装备
            Hero tmp = IfHeroHasEq(Equip);
            if (tmp != null)
            {
                tmp.m_iEquipID = -1;
                Role.role.GetHeroProperty().UpdateHero(tmp);
            }

            //卸掉原有物品的主人，在赋给新英雄
            Hero selectHero = Role.role.GetHeroProperty().GetHero(HeroId);
            selectHero.m_iEquipID = Equip.m_iID;
            Role.role.GetHeroProperty().UpdateHero(selectHero);
        }

        //UpdateEquipShow();
    }

    /// <summary>
    /// 设置选中的英雄ID
    /// </summary>
    /// <param name="heroId"></param>
    public void SetSelectHeroId(int heroId)
    {
        m_iSelectedHeroId = heroId;
    }

    /// <summary>
    /// 跳转到选择英雄界面的时候 hiden不发送数据，默认hiden是在点击下面图标切换的时候发生数据
    /// </summary>
    private void HidenNotSend()
    {
        ResourceMgr.UnloadUnusedResources();

        //base.Hiden();
        GUI_FUNCTION.AYSNCLOADING_HIDEN();

        CTween.TweenPosition(this.m_cPanSlide, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0));
        CTween.TweenPosition(this.m_cPanCancel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-540, 270, 0),Destory);
    }
}