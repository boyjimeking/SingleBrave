using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using Game.Resource;


//特殊副本关卡页面
//Author:Sunyi
//2013-11-25

public class GUIEspDungeonGate : GUIBase
{
    private const string RES_MAIN = "GUI_EspDungeonGate";//主资源地址
    private const string RES_ITEMCELL = "GUI_AreaDungeonListCell"; //关卡列表资源地址

    private const string GRID = "ClipView/Panel_ListView/UIGrid";//uigrid资源地址
    private const string LISTVIEW = "ClipView/Panel_ListView";//滚动视图
    private const string CLIPVIEW = "ClipView";//列表面板地址
    private const string TITLEPANEL = "TopPanel";//导航栏地址
    private const string HOMENAMEBUTTON = "TopPanel/Button_HomeName";//房名返回按钮
    private const string LABEL_DUNGEONNAME = "TopPanel/Label_DungeonName";//副本名称
    private const string MAINBUTTON = "TopPanel/Button_MainPanel"; //返回主页面按钮地址

    private const string LABEL_BOSSNAME = "Label_BossName";//boss名称标签地址
    private const string LABEL_STRENGTH = "Label_StrengthCount";//体力标签地址
    private const string LABEL_MAXLAVEL = "Label_MaxLevelCount";//最大层数标签地址
    private const string LABEL_DESC = "Label_Descripte";//关卡描述标签地址
    private const string SPR_NEW = "New";//新关卡地址
    private const string SPR_CLEAR = "Spr_Clear";//已完成关卡
    private const string REWARD_PARENT = "Reward";//优惠类型标签父对象

    private GameObject m_cItemCell; //关卡列表项
    private GameObject m_cGrid;          //列表
    private GameObject m_cListView;//滚动视图
    private GameObject m_cHomeName_Button;    //房名按钮
    private GameObject m_cMainButton;        //返回主页按钮
    private GameObject m_cPanelTitle;//导航栏
    private GameObject m_ClipView;
    private UILabel m_cLabDungeonName;//副本名
    private UnityEngine.Object m_cGateItem;//关卡item

    //private GameObject m_cAlertView;//体力不足消息弹出框
    //private GameObject m_cBtnAlertOk;//前往商城按钮

    private List<GameObject> m_lstGate = new List<GameObject>();//关卡列表
    private List<ActivityGateTable> m_lstGateTable = new List<ActivityGateTable>();//当前副本关卡表
    private bool m_bIsCurrentGui;//判断是否为当前GUI，用在跳转到好友界面的时候

    private string m_strGateName;//关卡名
    private bool m_bIsAlertViewShow;//判断当前消息提示框是否在显示
    private bool m_bIsNeedToBuyDiamond;//判断是否需要购买钻石

    private bool m_IsBackToEspDungeon;//是否返回

    public GUIEspDungeonGate(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_ESPDUNGEONGATE, GUILAYER.GUI_PANEL)
    { 
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
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_MAIN);
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_ITEMCELL);
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
            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            this.m_cHomeName_Button = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, HOMENAMEBUTTON);
            GUIComponentEvent guiHNButton = this.m_cHomeName_Button.AddComponent<GUIComponentEvent>();
            guiHNButton.AddIntputDelegate(OnClickHomeButton);

            this.m_cMainButton = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, MAINBUTTON);
            GUIComponentEvent guiMainEvent = this.m_cMainButton.AddComponent<GUIComponentEvent>();
            guiMainEvent.AddIntputDelegate(OnClickMainButton);

            this.m_cPanelTitle = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, TITLEPANEL);
            this.m_cPanelTitle.transform.localPosition = new Vector3(-640, 0, 0);

            this.m_ClipView = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, CLIPVIEW);
            this.m_ClipView.transform.localPosition = new Vector3(640, 0, 0);
            this.m_cLabDungeonName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LABEL_DUNGEONNAME);

            this.m_cGateItem = (UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_ITEMCELL);

            this.m_cListView = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LISTVIEW);

            this.m_cGrid = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GRID);
        }

        this.m_cListView.transform.localPosition = new Vector3(0, -5, 0);
        UIPanel panel = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(this.m_cGUIObject, LISTVIEW);
        float y = -105f;
        panel.clipRange = new Vector4(panel.clipRange.x, y, panel.clipRange.z, panel.clipRange.w);

        this.m_bIsAlertViewShow = false;

        foreach (GameObject item in this.m_lstGate)
        {
            GameObject.DestroyImmediate(item);
        }

        if (this.m_lstGate.Count > 0)
        {
            this.m_lstGate.Clear();
        }

        this.m_lstGate.Clear();

        List<ActivityGateTable> lstTable = ActivityTableManager.GetInstance().GetAllGate(WorldManager.s_iCurEspDungeonId);

        if (this.m_lstGateTable != null)
        {
            this.m_lstGateTable.Clear();
        }

        for (int i = 0; i < lstTable.Count; i++)
        {
            this.m_lstGateTable.Add(lstTable[i]);
        }

        this.m_cLabDungeonName.text = ActivityTableManager.GetInstance().GetDungeonTable(WorldManager.s_iCurEspDungeonId).Name;
        for (int i = 0; i < this.m_lstGateTable.Count; i++)
        {
            Debug.Log("i ==  " + i.ToString());
            GameObject gateItem = GameObject.Instantiate(this.m_cGateItem) as GameObject;
            gateItem.transform.parent = this.m_cGrid.transform;
            gateItem.transform.localScale = Vector3.one;
            gateItem.transform.localPosition = new Vector3(0, 120 - i * 155, 0);
            GUIComponentEvent guiGateItem = gateItem.AddComponent<GUIComponentEvent>();
            guiGateItem.AddIntputDelegate(DidSelectedAreaDungeonItem, i);

            UILabel bossNameLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(gateItem, LABEL_BOSSNAME);
            bossNameLabel.text = this.m_lstGateTable[i].Name;

            UILabel strengthLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(gateItem, LABEL_STRENGTH);
            strengthLabel.text = this.m_lstGateTable[i].CostHP.ToString();

            UILabel maxLevelLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(gateItem, LABEL_MAXLAVEL);
            maxLevelLabel.text = this.m_lstGateTable[i].MaxLayer.ToString();

            UILabel descriptionLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(gateItem, LABEL_DESC);
            descriptionLabel.text = this.m_lstGateTable[i].Desc;

            GameObject rewardParent = GUI_FINDATION.GET_GAME_OBJECT(gateItem, REWARD_PARENT);
            rewardParent.SetActive(false);

            GameObject sprNew = GUI_FINDATION.GET_GAME_OBJECT(gateItem, SPR_NEW);
            GameObject sprClear = GUI_FINDATION.GET_GAME_OBJECT(gateItem, SPR_CLEAR);
            sprClear.SetActive(false);
            sprNew.SetActive(true);

            this.m_lstGate.Add(gateItem);
        }

        this.m_bIsCurrentGui = false;

        SetLocalPos(Vector3.zero);
        this.m_cGUIMgr.SetCurGUIID(this.m_iID);

        CTween.TweenPosition(this.m_ClipView, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), new Vector3(0, 0, 0));
        CTween.TweenPosition(this.m_cPanelTitle, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-640, 270, 0), new Vector3(0, 270, 0));
    }
    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //base.Hiden();
        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        this.m_bIsCurrentGui = false;
        CTween.TweenPosition(this.m_cPanelTitle, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-640, 270, 0));
        CTween.TweenPosition(this.m_ClipView, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 0, 0), new Vector3(640, 0, 0) , Destory);

        ResourcesManager.GetInstance().UnloadUnusedResources();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        this.m_cItemCell = null;
        this.m_cGrid = null;
        this.m_cListView = null;
        this.m_cHomeName_Button = null;
        this.m_cMainButton = null;
        this.m_cPanelTitle = null;
        this.m_ClipView = null;
        this.m_cLabDungeonName = null;
        this.m_cGateItem = null;

        if (m_lstGate != null)
        {
            foreach (GameObject obj in m_lstGate)
            {
                GameObject.Destroy(obj);
            }
        }

        if (this.m_lstGate != null)
        {
            this.m_lstGate.Clear();
        }

        base.Destory();
    }

    public void SetEnable(bool enable)
    {
        this.m_cGUIObject.SetActive(enable);
    }

    /// <summary>
    /// 返回按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void OnClickHomeButton(GUI_INPUT_INFO info, object[] args)
    {

        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (this.m_bIsAlertViewShow)
            {
                this.m_bIsAlertViewShow = false;
            }
            else {
                Hiden();

                GUIEspDungeon espDungeon = (GUIEspDungeon)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_ESPDUNGEON);
                espDungeon.Show();
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
                if (ResourcesManager.GetInstance().GetProgress() >= 1f && ResourcesManager.GetInstance().IsComplete())
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
    /// 返回主页面按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void OnClickMainButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();
            GUIBackFrameBottom backbottom = (GUIBackFrameBottom)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM);
            backbottom.Show();
            GUIMain guiMain = (GUIMain)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_MAIN);
            guiMain.Show();
        }
    }

    /// <summary>
    /// 监听消息框上的按钮事件回调方法
    /// </summary>
    /// <param name="isOkButton"></param>
    private void OnClickAlertViewButtons(bool isOkButton)
    {
        if (isOkButton)//点击了确定按钮
        {
            if (this.m_bIsNeedToBuyDiamond)
            {
                Hiden();

                GUIGem gem = (GUIGem)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_GEM);
                gem.SetLastGuiId(this.m_iID);
                SendAgent.SendStoreDiamondPrice();
                gem.Show();
            }
            else
            {
                SendAgent.SendStrengthRecoverReq(Role.role.GetBaseProperty().m_iPlayerId);
            }
        }
    }

    /// <summary>
    /// 关卡点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void DidSelectedAreaDungeonItem(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            WorldManager.s_iCurEspDungeonGateIndex = (int)args[0];

            RoleBaseProperty baseProperty = Role.role.GetBaseProperty();

            ActivityGateTable gateTable = this.m_lstGateTable[(int)args[0]];

            if (WorldManager.s_eCurActivityDungeonFavType == FAV_TYPE.STRENGTH_HALF)
            {
                if (gateTable.CostHP / 2 > RoleExpTableManager.GetInstance().GetMaxStrength(Role.role.GetBaseProperty().m_iLevel))
                {
                    GUI_FUNCTION.MESSAGEL(null, "您当前的体力值上限不足以进入该副本！");
                    return;
                }
                else
                {
                    if (gateTable.CostHP/2 > Role.role.GetBaseProperty().m_iStrength)
                    {
                        if (Role.role.GetBaseProperty().m_iDiamond >= GAME_DEFINE.DiamondStrenthCost)
                        {
                            this.m_bIsNeedToBuyDiamond = false;
                            GUI_FUNCTION.MESSAGEL_(OnClickAlertViewButtons, "您的体力不足，\n花费" + GAME_DEFINE.DiamondStrenthCost + "个钻石可以恢复体力，是否恢复？");
                            this.m_bIsAlertViewShow = true;
                        }
                        else
                        {
                            this.m_bIsNeedToBuyDiamond = true;
                            GUI_FUNCTION.MESSAGEL_(OnClickAlertViewButtons, "您的体力不足，\n花费" + GAME_DEFINE.DiamondStrenthCost + "个钻石可以恢复体力，是否购买钻石？");
                            this.m_bIsAlertViewShow = true;
                        }
                        return;
                    }
                }
            }
            else {
                if (gateTable.CostHP > RoleExpTableManager.GetInstance().GetMaxStrength(Role.role.GetBaseProperty().m_iLevel))
                {
                    GUI_FUNCTION.MESSAGEL(null, "您当前的体力值上限不足以进入该副本！");
                    return;
                }
                else
                {
                    if (gateTable.CostHP > Role.role.GetBaseProperty().m_iStrength)
                    {
                        if (Role.role.GetBaseProperty().m_iDiamond >= GAME_DEFINE.DiamondStrenthCost)
                        {
                            this.m_bIsNeedToBuyDiamond = false;
                            GUI_FUNCTION.MESSAGEL_(OnClickAlertViewButtons, "您的体力不足，\n花费" + GAME_DEFINE.DiamondStrenthCost + "个钻石可以恢复体力，是否恢复？");
                            this.m_bIsAlertViewShow = true;
                        }
                        else
                        {
                            this.m_bIsNeedToBuyDiamond = true;
                            GUI_FUNCTION.MESSAGEL_(OnClickAlertViewButtons, "您的体力不足，\n花费" + GAME_DEFINE.DiamondStrenthCost + "个钻石可以恢复体力，是否购买钻石？");
                            this.m_bIsAlertViewShow = true;
                        }
                        return;
                    }
                }
            }

            
            this.m_bIsCurrentGui = true;

            GUIBackFrameBottom backbottom = (GUIBackFrameBottom)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM);
            backbottom.HiddenHalf();

            GUIFightReady ready = (GUIFightReady)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FIGHTREADY);
            ready.SetTitle(gateTable.Name);
            if (WorldManager.s_eCurActivityDungeonFavType == FAV_TYPE.STRENGTH_HALF)
            {
                ready.SetHpCost(gateTable.CostHP / 2);
            }
            else {
                ready.SetHpCost(gateTable.CostHP);
            }

            this.m_strGateName = gateTable.Name;

            ShowGUIFriendFight();
        }
    }

    /// <summary>
    /// 设置当前的GUIID
    /// </summary>
    public bool IsCurrentGUI()
    {
        return this.m_bIsCurrentGui;
    }

    /// <summary>
    /// 在加载好友结束后跳转
    /// </summary>
    public void ShowGUIFriendFight()
    {
        Hiden();

        GUIFriendFight guiFriendFight = (GUIFriendFight)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FRIENDFIGHT);
        guiFriendFight.SetLastGuiId(this.m_iID);
        guiFriendFight.SetTitle(this.m_strGateName);
        guiFriendFight.Show();
    }

}

