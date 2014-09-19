using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using Game.Resource;

//GUIGem.cs 宝石购入界面
//Author:SunYi
//2013-11-22

public class GUIGem :GUIBase
{

    private const string RES_MAIN = "GUI_Gem";//主资源地址
    private const string RES_LISTITEM = "GUI_GemListItemCell";//列表项资源地址

    private const string PANEL_LIST = "ClipPanel";//宝石列表资源地址
    private const string PANEL_TOPPANEL = "TopPanel";//导航栏地址
    private const string LISTVIEW = "ClipPanel/ListView";//滚动视图地址
    private const string BUTTON_BACK = "TopPanel/Btn_Back";//返回按钮地址

    private const string LABEL_COUNT = "Lab_Count";//钻石数量地址
    private const string LABEL_TOTAL = "Lab_Price";//总价标签地址
    private const string BUTTON_BUY = "Btn_Buy";//购买按钮地址

    private const string DIAMOND_ICON = "Icon";//钻石地址

    private GameObject m_cClipView;//列表项
    private GameObject m_cTopPanel;//导航栏
    private GameObject m_cBtnBack; //返回按钮
    private GameObject m_cListView;//滚动视图

    private UnityEngine.Object m_cListItem;//列表项

    private List<GameObject> m_lstItems = new List<GameObject>();//列表
    private List<StoreDiamondPrice> m_lstPrices = new List<StoreDiamondPrice>();
    private List<TDAnimation> m_lstAnimations = new List<TDAnimation>();//动画列表

    private int m_iLastGuiId;//上一个guiid

    public GUIGem(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_GEM, UILAYER.GUI_PANEL)
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

            this.m_cBtnBack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_BACK);
            GUIComponentEvent backEvent = this.m_cBtnBack.AddComponent<GUIComponentEvent>();
            backEvent.AddIntputDelegate(OnClickBackButton);

            this.m_cClipView = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PANEL_LIST);
            this.m_cClipView.transform.localPosition = new Vector3(640, 0, 0);

            this.m_cTopPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PANEL_TOPPANEL);
            this.m_cTopPanel.transform.localPosition = new Vector3(-420, 270, 0);

            this.m_cListItem = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_LISTITEM);

            this.m_cListView = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LISTVIEW);

        }

        this.m_cListView.transform.localPosition = new Vector3(0, 0, 0);
        UIPanel panel = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(this.m_cGUIObject, LISTVIEW);
        float y = -110.0f;
        panel.clipRange = new Vector4(panel.clipRange.x, y, panel.clipRange.z, panel.clipRange.w);

        if (this.m_lstItems != null)
        {
            this.m_lstItems.Clear();
        }

        if (this.m_lstPrices != null)
        {
            this.m_lstPrices.Clear();
        }

        for (int i = 0; i < Role.role.GetStoreDiamondProperty().GetAll().Count; i++)
        {
            this.m_lstPrices.Add(Role.role.GetStoreDiamondProperty().GetAll()[i]);
        }

        for (int i = 0; i < this.m_lstPrices.Count; i++)
        {
            GameObject listItem = GameObject.Instantiate(this.m_cListItem) as GameObject;
            listItem.transform.parent = this.m_cListView.transform;
            listItem.transform.localScale = Vector3.one;
            listItem.transform.localPosition = new Vector3(0, 150 - i * 125, 0);

            UILabel countLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(listItem, LABEL_COUNT);
            UILabel totalLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(listItem, LABEL_TOTAL);

            UISprite icon = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(listItem, DIAMOND_ICON);
            TDAnimation iconEffect = new TDAnimation(icon.atlas, icon); //左右导航
            iconEffect.Play("diamond", Game.Base.TDAnimationMode.Loop);

            this.m_lstAnimations.Add(iconEffect);

            countLabel.text = this.m_lstPrices[i].m_iCount.ToString() + "个";
            totalLabel.text = "￥" + this.m_lstPrices[i].m_iTotal.ToString();

            GameObject btnBuy = GUI_FINDATION.GET_GAME_OBJECT(listItem, BUTTON_BUY);
            GUIComponentEvent buyEvent = btnBuy.AddComponent<GUIComponentEvent>();
            buyEvent.AddIntputDelegate(OnClickBuyButton, this.m_lstPrices[i].m_iId , this.m_lstPrices[i].m_iCount, this.m_lstPrices[i].m_iTotal , this.m_lstPrices[i].m_strTypeID);

            this.m_lstItems.Add(listItem);
        }

        this.m_cGUIMgr.SetCurGUIID(this.m_iID);
        SetLocalPos(Vector3.zero);

        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-420, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cClipView, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_BUY_DIAMOND));
    }
    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //base.Hiden();

        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        CTween.TweenPosition(this.m_cClipView, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 0, 0), new Vector3(640, 0, 0));
        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-420, 270, 0) ,Destory);

        if (this.m_lstItems != null)
        {
            foreach (GameObject obj in this.m_lstItems)
            {
                GameObject.Destroy(obj);
            }
        }

        if (this.m_lstPrices != null)
        {
            this.m_lstPrices.Clear();
        }

        if (this.m_lstAnimations!= null)
        {
            this.m_lstAnimations.Clear();
        }

        ResourceMgr.UnloadUnusedResources();
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

        foreach (TDAnimation amt in this.m_lstAnimations)
        {
            if (amt != null)
            {
                amt.Update();
            }
        }

        if (this.m_bShow)
        {
            PlatformManager.GetInstance().Update();
        }

        return true;
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();
        if (this.m_lstItems != null)
        {
            foreach (GameObject obj in this.m_lstItems)
            {
                GameObject.Destroy(obj);
            }
        }

        if (this.m_lstPrices != null)
        {
            this.m_lstPrices.Clear();
        }

        this.m_cClipView = null;
        this.m_cTopPanel = null;
        this.m_cBtnBack = null;
        this.m_cListView = null;

        m_cListItem = null;

        base.Destory();
    }

    /// <summary>
    /// 淡进
    /// </summary>
    private void FadeIn()
    {
        SetLocalPos(Vector3.zero);

        CTween.TweenPosition(this.m_cClipView, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), new Vector3(0, 0, 0));
        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-420, 270, 0), new Vector3(0, 270, 0));

        
    }

    /// <summary>
    /// 设置上一个GUIID
    /// </summary>
    /// <param name="guiId"></param>
    public void SetLastGuiId(int guiId)
    {
        this.m_iLastGuiId = guiId;
    }

    /// <summary>
    /// 返回按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickBackButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();
            switch (this.m_iLastGuiId)
            {
                case GUI_DEFINE.GUIID_STORE:
                    ShowStoreGui();
                    break;
                case GUI_DEFINE.GUIID_BODYSTRENGTHRESTORATION:
                    ShowStoreGui();
                    break;
                case GUI_DEFINE.GUIID_FISTFIGHTPOINTRESTORATION:
                    ShowStoreGui();
                    break;
                case GUI_DEFINE.GUIID_PROPSSLOTEXPANSION:
                    ShowStoreGui();
                    break;
                case GUI_DEFINE.GUIID_UNITSLOTEXPANSION:
                    ShowStoreGui();
                    break;
                case GUI_DEFINE.GUIID_AREADUNGEON:
                    GUIAreaDungeon areaDungeon = (GUIAreaDungeon)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_AREADUNGEON);
                    areaDungeon.Show();
                    break;
                case GUI_DEFINE.GUIID_ESPDUNGEONGATE:
                    GUIEspDungeonGate gate = (GUIEspDungeonGate)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_ESPDUNGEONGATE);
                    gate.Show();
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// 显示商城GUI界面
    /// </summary>
    private void ShowStoreGui()
    {
        GUIStore store = (GUIStore)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_STORE);
        store.Show();
        GUIBackFrameBottom bottom = (GUIBackFrameBottom)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM);
        bottom.ShowHalf();
    }

    /// <summary>
    /// 购买事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickBuyButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        { 
            //GUI_FUNCTION.MESSAGEM(null,"购买数量：" + args[0] + "\n" + "总价：" + args[1]);
            SendAgent.SendPay( Role.role.GetBaseProperty().m_iPlayerId , (int)args[0]);
            //PlatformManager.GetInstance().ShowPayment(args[3]);
        }
    }
}

