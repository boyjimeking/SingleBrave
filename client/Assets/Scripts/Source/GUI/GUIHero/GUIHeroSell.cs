//Micro.Sanvey
//2013.12.3
//sanvey.china@gmail.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;
using Game.Base;


/// <summary>
/// 英雄出售GUI
/// </summary>
public class GUIHeroSell : GUIHeroSelectBase
{
    //----------------------------------------------资源地址--------------------------------------------------

    private const string RES_HEROSELL = "GUI_HeroSell";  //出售信息资源地址
    private const string LB_SELLCOUNT = "SellInfo/LbSellCount";  //出售金额
    private const string LB_SELLNUM = "SellInfo/LbSellNum";  //出售数量
    private const string BTN_ABANDON = "SellInfo/BtnAbandon";  //放弃全部选择按钮地址
    private const string BTN_SELL = "SellInfo/BtnSell";  //出售按钮地址

    //----------------------------------------------游戏对象--------------------------------------------------

    private GameObject m_cSellInfo; //出售信息
    private GameObject m_cBtnSell;  //出售按钮
    private UILabel m_cSellCount;   //出售金额
    private UILabel m_cSellNum;     //出售数量

    //----------------------------------------------data--------------------------------------------------

    private List<HeroShowItem> m_lstSelectHero=new List<HeroShowItem>();  //选中准备出售的英雄列表
    private int m_iSelectMax = 10;       //最多可以一次选中10个英雄
    public List<int> lstSelectHeroID = new List<int>(); //记录准备出售的英雄的ID记录
    private bool m_bShowNowChange = false;

    public GUIHeroSell(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_HEROSELL, UILAYER.GUI_PANEL)
    {
        //
    }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        this.m_eLoadingState = LOADING_STATE.START;
        GUI_FUNCTION.AYSNCLOADING_SHOW();

        if (this.m_cGUIObject == null)
        {
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_HEROSELL);
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_MAIN);
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_HEROITEM);
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + BTN_NULL);
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    protected override void InitGUI()
    {
        base.Show();
        //优化每次进入都读取最新所有英雄，否则，如果界面没有被销毁，进入界面选择英雄是老数据
        //所有英雄列表
        List<Hero> allheros = Role.role.GetHeroProperty().GetAllHero();
		HeroTeam heroTeam = CModelMgr.sInstance.GetModel<HeroTeam>();
        //取出使用中的英雄 和 锁定的英雄
		List<Hero> partyHeros = allheros.FindAll(new Predicate<Hero>((item) => { return (item.m_iEquipID == -1) && !(CheckExistInTeams(heroTeam.ToArray<HeroTeam>(), item.m_iID)) && (!item.m_bLock); }));
        this.m_lstHero = partyHeros;

        //设置主资源中panel位置大小调整
        this.m_fClipParentY = 11f;
        this.m_fClipCenterY = -25.5f;
        this.m_fClipSizeY = 435f;

        if (this.m_cGUIObject == null)
        {
            //生成画面
            base.InitGUI();
            //设置页面头
            this.m_cUITittle.text = "英雄出售";

            //出售信息
            this.m_cSellInfo = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_HEROSELL)) as GameObject;
            //出售数量
            this.m_cSellNum = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cSellInfo, LB_SELLNUM);
            //出售金额
            this.m_cSellCount = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cSellInfo, LB_SELLCOUNT);
            //放弃选择按钮
            GameObject btnabaton = GUI_FINDATION.GET_GAME_OBJECT(this.m_cSellInfo, BTN_ABANDON);
            GUIComponentEvent gui_event = btnabaton.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(Abadon_OnEvent);
            //出售按钮
            m_cBtnSell = GUI_FINDATION.GET_GAME_OBJECT(this.m_cSellInfo, BTN_SELL);
            gui_event = m_cBtnSell.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(Sell_OnEvent);
            //加入主资源中
            this.m_cSellInfo.transform.parent = this.m_cTopRightParent.transform;
            this.m_cSellInfo.transform.localPosition = Vector3.zero;
            this.m_cSellInfo.transform.localScale = Vector3.one;

            //设置返回输入接口
            GUIComponentEvent ce = this.m_cCancelBtn.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnCancel);

        }
        else
        {
            base.InitGUI();
        }

        if (m_bShowNowChange)
        {
            m_bBeLongPress = false;
            //返回时记录之前操作
            GUIHeroDetail herodetail = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERODETAIL) as GUIHeroDetail;

            lstSelectHeroID = herodetail.lstSelectHero;

            m_lstSelectHero.Clear();

            foreach (HeroShowItem item in this.m_lstHeroShow)
            {
                int index = lstSelectHeroID.IndexOf(item.m_cHero.m_iID);
                if (index != -1)
                {
                    m_lstSelectHero.Add(item);
                    item.m_cSelectLb.text = (index + 1).ToString();

                    item.m_cSelectBar.alpha = 1f;
                    item.m_cSelectBar.MakePixelPerfect();
                    item.m_cSelectLb.enabled = true;
                    item.m_cItemCover.enabled = false;
                }
                else
                {
                    item.m_cSelectBar.alpha = 0f;
                    item.m_cSelectBar.MakePixelPerfect();
                    item.m_cSelectLb.enabled = false;
                }
            }
        }


        //设置点击英雄接口
        foreach (HeroShowItem item in this.m_lstHeroShow)
        {
            GUIComponentEvent tmp = item.m_cItem.GetComponent<GUIComponentEvent>();
            if (tmp == null)
                tmp = item.m_cItem.AddComponent<GUIComponentEvent>();
            tmp.AddIntputDelegate(OnHero, item.m_cHero.m_iID);
        }

        //刷新显示
        m_cSellNum.text = (m_lstSelectHero.Count).ToString();  //出售数量
        m_cSellCount.text = GetSellCount().ToString();         //出售金额

        this.m_cGUIMgr.SetCurGUIID(this.ID);

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_SALE_HERO));

        //刷新保存的出售列表
        if (lstSelectHeroID.Count != 0)
            this.UpdateSelectState();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        if (null != m_lstSelectHero) m_lstSelectHero.Clear();
        m_cSellInfo = null; //出售信息
        m_cBtnSell = null;  //出售按钮
        m_cSellCount = null;   //出售金额
        m_cSellNum = null;     //出售数量

        base.Destory();
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


        if (this.IsShow())
        {
            if (m_bHasDrag)  //如果是拖动
            {
                return true;
            }
            if (this.m_bHasPress)  //如果是按下,开始计时
            {
                float dis = GAME_TIME.TIME_FIXED() - m_fDis;
                if (dis >= PRESS_TIME)
                {
                    m_bBeLongPress = true;
                    m_bHasPress = false;


                    int heroid = this.m_iSelectHeroIndex;
                    GUIHeroDetail herodetail = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERODETAIL) as GUIHeroDetail;
          
                    herodetail.m_cClipRange = this.m_cClipPanel.clipRange;
                    herodetail.m_cLocalposition = this.m_cClipPanel.transform.localPosition;

                    HidenNotClear();


                    herodetail.Show(this.ShowNotChange, Role.role.GetHeroProperty().GetHero(heroid));
                    //记录英雄选中信息
                    this.lstSelectHeroID.Clear();
                    foreach (HeroShowItem hsi in m_lstSelectHero)
                    {
                        this.lstSelectHeroID.Add(hsi.m_cHero.m_iID);
                    }

                    herodetail.lstSelectHero = this.lstSelectHeroID;
                }
            }
        }
        return base.Update();
    }

    /// <summary>
    /// 进入英雄信息时回调方法
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void ShowNotChange()
    {
        //保存记录的位置
        GUIHeroDetail herodetail = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERODETAIL) as GUIHeroDetail;
        this.m_cPanelLocalposition = herodetail.m_cLocalposition;
        this.m_cPanelClipRange = herodetail.m_cClipRange;

        m_lstSelectHero.Clear();
        m_bShowNowChange = true;
        Show();

        

    }
    /// <summary>
    /// 取消按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnCancel(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
            {

                GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

                this.m_cPanelClipRange = Vector4.zero;
                this.m_cPanelLocalposition = Vector3.zero;
                this.Hiden();

                m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERO_MENU).Show();
            }
        }
    }

    /// <summary>
    /// 点击英雄
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnHero(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.PRESS)
        {
            if (!this.m_bHasPress)  
            {
                this.m_bHasPress = true;
                //开始长按钮计时
                this.m_fDis = GAME_TIME.TIME_FIXED();
                this.m_iSelectHeroIndex = (int)args[0];
            }
            else
            {
                //弹起
                this.m_bHasPress = false;
                this.m_bHasDrag = false;
            }
        }
        else if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.DRAG)
        {
            this.m_bHasDrag = true;
        }
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (m_bBeLongPress)
            {
                return;
            }

            int heroID = (int)args[0];
            HeroShowItem selectItem = this.m_lstHeroShow.Find(new Predicate<HeroShowItem>((item) =>
           {
               return item.m_cHero.m_iID == heroID;
           }));

            //已经选择的再次点击，变为非选中
            bool exist = m_lstSelectHero.Exists(new Predicate<HeroShowItem>((item) =>
            {
                return item.m_cHero.m_iID == selectItem.m_cHero.m_iID;
            }));
            if (exist)  //存在则移除选中
            {
                m_lstSelectHero.RemoveAll(new Predicate<HeroShowItem>((item) =>
                {
                    return item.m_cHero.m_iID == selectItem.m_cHero.m_iID;
                }));
                selectItem.m_cSelectBar.alpha = 0f;
                selectItem.m_cSelectLb.enabled = false;
            }
            else
            {
                //大于一次性最大出售数量，剩余项目变灰，不可点击
                if (m_lstSelectHero.Count >= m_iSelectMax)
                {
                    //刷新选中图标
                    UpdateSelectState();
                    return;
                }
                m_lstSelectHero.Add(selectItem);
            }

            //刷新选中图标
            UpdateSelectState();
        }
    }

    /// <summary>
    /// 放弃选择事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void Abadon_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            //将已有状态去除
            m_lstSelectHero.ForEach((item =>
            {
                item.m_cSelectBar.alpha = 0f;
                item.m_cSelectLb.enabled = false;
            }));
            m_lstSelectHero.Clear();
            //刷新选中图标
            UpdateSelectState();
        }
    }

    /// <summary>
    /// 出售点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void Sell_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            int pid = Role.role.GetBaseProperty().m_iPlayerId;
            int[] sells = GetSellHeroIDs();
            if (sells == null || sells.Length == 0)
            {
                GUI_FUNCTION.MESSAGEM(null, "请选择要出售的英雄");
            }
            else
            {
                //出售列表中是否含有3星以上武将
                if (m_lstSelectHero.Exists(q => { return q.m_cHero.m_iStarLevel >= 3; }))
                {
                    GUI_FUNCTION.MESSAGEL_(new GUIMessageL.CALL_BACK1((ok) =>
                    {
                        if (ok)
                        {
                            Hiden();
                            SendAgent.SendHeroSellPktReq(pid, sells);
                        }
                    }), "出售单位包含3星以上角色\n是否确认出售");
                }
                else
                {
                    GUI_FUNCTION.MESSAGEL_(new GUIMessageL.CALL_BACK1((ok) =>
                    {
                        if (ok)
                        {
                            Hiden();
                            SendAgent.SendHeroSellPktReq(pid, sells);
                        }
                    }), "确定要出售？");
                }

            }
        }
    }

    /// <summary>
    /// 获取所有出售英雄ID数组
    /// </summary>
    /// <returns></returns>
    public int[] GetSellHeroIDs()
    {
        int[] sells = new int[m_lstSelectHero.Count];
        for (int i = 0; i < sells.Length; i++)
        {
            sells[i] = m_lstSelectHero[i].m_cHero.m_iID;
        }
        return sells;
    }

    /// <summary>
    /// 计算当前选中的英雄可以出售的金额
    /// </summary>
    /// <returns></returns>
    private long GetSellCount()
    {
        long allmoney = 0;
        if (m_lstSelectHero != null)
        {
            m_lstSelectHero.ForEach((next) =>
            {
                if (next.m_cHero.m_iSellCost == 0)
                {
                    allmoney += HeroSellMoneyTableManager.GetInstance().GetHeroMoey(next.m_cHero.m_iLevel, next.m_cHero.m_iStarLevel);
                }
                else
                {
                    allmoney += next.m_cHero.m_iSellCost;
                }
            });
        }
        return allmoney;
    }

    /// <summary>
    /// 刷新选中图标
    /// </summary>
    private void UpdateSelectState()
    {
        m_cBtnSell.GetComponent<UIImageButton>().isEnabled = true;

        //大于一次性最大出售数量，剩余项目变灰，不可点击
        for (int i = 0; i < m_lstHeroShow.Count; i++)
        {
            m_lstHeroShow[i].m_cItemCover.enabled = (m_lstSelectHero.Count >= m_iSelectMax);
        }

        for (int i = 0; i < m_lstSelectHero.Count; i++)
        {
            m_lstSelectHero[i].m_cSelectLb.text = (i + 1).ToString();

            m_lstSelectHero[i].m_cSelectBar.alpha = 1f;
            m_lstSelectHero[i].m_cSelectBar.gameObject.SetActive(false);
            m_lstSelectHero[i].m_cSelectBar.gameObject.SetActive(true);
            m_lstSelectHero[i].m_cSelectLb.enabled = true;
            m_lstSelectHero[i].m_cItemCover.enabled = false;
            
        }

        //刷新显示
        m_cSellNum.text = (m_lstSelectHero.Count).ToString();  //出售数量
        m_cSellCount.text = GetSellCount().ToString();         //出售金额

    }

    /// <summary>
    /// 出售完成，更新英雄列表，即使刷新
    /// </summary>
    public void Reflash()
    {
        //所有英雄列表
        List<Hero> allheros = Role.role.GetHeroProperty().GetAllHero();
		HeroTeam heroTeam = CModelMgr.sInstance.GetModel<HeroTeam>();
        //除去 使用中的英雄 和 锁定的英雄
		List<Hero> partyHeros = allheros.FindAll(new Predicate<Hero>((item) => { return (item.m_iEquipID == -1) && !(CheckExistInTeams(heroTeam.ToArray<HeroTeam>(), item.m_iID)) && (!item.m_bLock); }));
        this.m_lstHero = partyHeros;

        base.InitGUI();

        //设置点击英雄接口
        foreach (HeroShowItem item in this.m_lstHeroShow)
        {
            GUIComponentEvent tmp = item.m_cItem.GetComponent<GUIComponentEvent>();
            if (tmp == null)
                tmp = item.m_cItem.AddComponent<GUIComponentEvent>();
            tmp.AddIntputDelegate(OnHero, item.m_cHero.m_iID);
        }

        m_lstSelectHero = new List<HeroShowItem>();
        UpdateSelectState();
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        m_lstSelectHero.Clear();
        m_bShowNowChange = false;
        base.Hiden();
    }

    /// <summary>
    /// 隐藏 不清空选中列表
    /// </summary>
    public void HidenNotClear()
    {
        m_bShowNowChange = false;

        base.Hiden();
    }
}