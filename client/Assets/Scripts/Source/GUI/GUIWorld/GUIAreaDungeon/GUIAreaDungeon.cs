using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using Game.Resource;
using Game.Media;



/// <summary>
/// 区域副本列表类
/// </summary>
public class GUIAreaDungeon : GUIBase
{

    private const string RES_MAIN = "GUI_AreaDungeon"; //主资源地址
    private const string RES_DUNGEONCELL = "GUI_AreaDungeonListCell";//区域副本入口项资源地址

    private const string LIST_PARENT = "ClipView";  //列表父节点
    private const string BACK_PARENT = "TopPanel";   //返回父节点
    private const string HOMENAMEBUTTON = "TopPanel/Button_HomeName";//房名返回按钮地址
    private const string MAINBUTTON = "TopPanel/Button_MainPanel"; //返回主页面按钮地址
    private const string LABEL_GATE = "TopPanel/Label_DungeonName"; //导航栏标签地址
    private const string GRID = "ClipView/Panel_ListView/UIGrid";//列表地址
    private const string DUNGEON_PANEL = "ClipView/Panel_ListView";    //副本面板地址

    private const string LABEL_BOSSNAME = "Label_BossName";//boss名称标签地址
    private const string LABEL_STRENGTH = "Label_StrengthCount";//体力标签地址
    private const string LABEL_MAXLAVEL = "Label_MaxLevelCount";//最大层数标签地址
    private const string LABEL_DESC = "Label_Descripte";//关卡描述标签地址
    private const string SPR_CLEAR = "Spr_Clear";//已完成关卡
    private const string LABEL_REWARD = "Reward/Lab_Reward";//优惠类型标签地址
    private const string LABEL_TIME = "Reward/Lab_Time";//还剩多少时间
    private const string REWARD_PARENT = "Reward";//优惠类型标签父对象
    private const string SPR_NEW = "New";//new 对象地址

    private GameObject m_cListParent;   //列表父节点
    private GameObject m_cBackParent;   //返回父节点
    private UIPanel m_cDungeonPanel; //副本面板
    private GameObject m_cAreaDungeonItem;   //区域地址入口项
    private GameObject m_cHomeName_Button;    //房名按钮
    private UILabel m_cLabGateName;//导航栏关卡名称标签
    private GameObject m_cMainButton;        //返回主页按钮
    private GameObject m_cGrid;              //列表

    private GameObject m_cBtnAlertOk;//前往商城按钮

    private List<GameObject> m_lstAreaDungeonList = new List<GameObject>();//副本项列表
    private string m_strAreaGateName;//区域关卡名称
    private List<GateTable> m_lstGate = new List<GateTable>();//关卡列表
    private int m_iDungeonID;  //当前副本
    //public int currentGate = 0;//当前选择的关卡
    private bool m_bIsNewDungeon = false;//是否是最新副本
    private bool m_bIsCurrentGui;//判断当前GUI，用在跳转到战友界面
    private int m_iFavTimeType;//优惠类型
    private bool m_bIsNeedToDisplatFav;//是否需要显示优惠类型
    private int m_iFavTimeFromNow;//还剩多少时间

    private string m_strGateName;//关卡名
    private bool m_bIsAlertViewShow;//判断当前消息提示框是否显示
    private bool m_bIsNeedToBuyDiamond;//判断是否需要购买钻石

    private UnityEngine.Object m_cItem;//列表项

    public GUIAreaDungeon(GUIManager guimgr)
        : base(guimgr, GUI_DEFINE.GUIID_AREADUNGEON, UILAYER.GUI_PANEL)
    { 
        
    }

    /// <summary>
    /// 区域ID设置
    /// </summary>
    /// <param name="id"></param>
    public void SetDungeonID(int id)
    {
        this.m_iDungeonID = id;
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
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_DUNGEONCELL);
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

            this.m_cListParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LIST_PARENT);
            this.m_cBackParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BACK_PARENT);

            this.m_cDungeonPanel = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(this.m_cGUIObject, DUNGEON_PANEL);

            this.m_cHomeName_Button = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, HOMENAMEBUTTON);
            GUIComponentEvent guiHNButton = this.m_cHomeName_Button.AddComponent<GUIComponentEvent>();
            guiHNButton.AddIntputDelegate(OnClickHomeButton);

            this.m_cMainButton = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, MAINBUTTON);
            GUIComponentEvent guiMainEvent = this.m_cMainButton.AddComponent<GUIComponentEvent>();
            guiMainEvent.AddIntputDelegate(OnClickMainButton);

			this.m_cItem = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_DUNGEONCELL);

            this.m_cLabGateName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LABEL_GATE);

            this.m_cGrid = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GRID);
        }

        //播放活动本音效
        MediaMgr.sInstance.PlayBGM(SOUND_DEFINE.BGM_ACTIVE);

        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();

        this.m_cDungeonPanel.transform.localPosition = new Vector3(0, 0, 0);
        UIPanel panel = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(this.m_cGUIObject, DUNGEON_PANEL);
        float y = -114.85f;
        panel.clipRange = new Vector4(panel.clipRange.x, y, panel.clipRange.z, panel.clipRange.w);

        this.m_bIsAlertViewShow = false;

        this.m_cLabGateName.text = this.m_strAreaGateName;

        foreach (GameObject item in this.m_lstAreaDungeonList)
        {
            if (item != null)
            {
                GameObject.DestroyImmediate(item);
            }
        }

        m_lstAreaDungeonList.Clear();
        m_lstGate.Clear();

        GUIArea area = (GUIArea)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_AREA);

        List<GateTable> listGateTabel = WorldManager.GetAllGate(this.m_iDungeonID);

        int newGateIndex = 0;

		int newDungeonIndex = CModelMgr.sInstance.GetModel<FuBen>().GetNewDungeonIndex(WorldManager.s_iCurrentWorldId, WorldManager.s_iCurrentAreaIndex);
        if (newDungeonIndex == WorldManager.s_iCurrentDungeonIndex)
        {
			newGateIndex = CModelMgr.sInstance.GetModel<FuBen>().GetNewGateIndex(WorldManager.s_iCurrentWorldId);
        }
        else
        {
            newGateIndex = listGateTabel.Count - 1;
        }
        for (int i = 0; i < newGateIndex + 1; i++)
        {
            GameObject gateItem = GameObject.Instantiate(this.m_cItem) as GameObject;
            gateItem.transform.parent = this.m_cGrid.transform;
            gateItem.transform.localScale = Vector3.one;
            gateItem.transform.localPosition = new Vector3(0, (i - newGateIndex) * 155 + 120, 0);

            UILabel bossNameLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(gateItem, LABEL_BOSSNAME);
            bossNameLabel.text = listGateTabel[i].Name;

            UILabel strengthLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(gateItem, LABEL_STRENGTH);
            strengthLabel.text = listGateTabel[i].CostHP.ToString();

            UILabel maxLevelLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(gateItem, LABEL_MAXLAVEL);
            maxLevelLabel.text = listGateTabel[i].MaxLayer.ToString();

            UILabel descriptionLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(gateItem, LABEL_DESC);
            descriptionLabel.text = listGateTabel[i].Desc;

            GameObject sprNew = GUI_FINDATION.GET_GAME_OBJECT(gateItem, SPR_NEW);
            GameObject sprClear = GUI_FINDATION.GET_GAME_OBJECT(gateItem, SPR_CLEAR);

            GameObject rewardParent = GUI_FINDATION.GET_GAME_OBJECT(gateItem, REWARD_PARENT);
            UILabel rewardLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(gateItem, LABEL_REWARD);
            UILabel rewardTimeLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(gateItem, LABEL_TIME);


            if (this.m_bIsNeedToDisplatFav)
            {
                rewardParent.SetActive(true);
                SetFavTimeLabel(rewardLabel);
                rewardTimeLabel.text = "还剩" + this.m_iFavTimeFromNow.ToString() + "小时";
            }
            else
            {
                rewardParent.SetActive(false);
            }
            if (i == newGateIndex && this.m_bIsNewDungeon)
            {
                sprNew.SetActive(true);
                sprClear.SetActive(false);
            }
            else
            {
                sprClear.SetActive(true);
                sprNew.SetActive(false);
            }

            GUIComponentEvent guiGateItem = gateItem.AddComponent<GUIComponentEvent>();
            guiGateItem.AddIntputDelegate(DidSelectedAreaDungeonItem, i);

            this.m_lstAreaDungeonList.Add(gateItem);
            this.m_lstGate.Add(listGateTabel[i]);

            gateItem = null;
            guiGateItem = null;
        }

        this.m_bIsCurrentGui = false;

        UIDraggablePanel draglePanel = GUI_FINDATION.GET_OBJ_COMPONENT<UIDraggablePanel>(this.m_cGUIObject, DUNGEON_PANEL);
        draglePanel.repositionClipping = true;

        SetLocalPos(Vector3.zero);
        this.m_cGUIMgr.SetCurGUIID(this.m_iID);

        CTween.TweenPosition(this.m_cListParent, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), new Vector3(0, 0, 0));
        CTween.TweenPosition(this.m_cBackParent, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-640, 270, 0), new Vector3(0, 270, 0));

        //新手引导
        GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_BATTLE_SECOND3);
        GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_BATTLE_THIRD3);
    }
    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //base.Hiden();

        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        CTween.TweenPosition(this.m_cListParent, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 0, 0), new Vector3(640, 0, 0));
        CTween.TweenPosition(this.m_cBackParent, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-640, 270, 0) , Destory);
        this.m_bIsCurrentGui = false;

        ResourceMgr.UnloadUnusedResources();

    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        this.m_cItem = null;

        if (m_lstAreaDungeonList != null)
        {
            foreach (GameObject item in this.m_lstAreaDungeonList)
            {
                if (item != null)
                {
                    GameObject.DestroyImmediate(item);
                }
            }
        }

        m_lstAreaDungeonList.Clear();

        if (m_lstGate != null)
        {
            m_lstGate.Clear();
        }

        this.m_cListParent = null;
        this.m_cBackParent = null; 
        this.m_cDungeonPanel = null;
        this.m_cAreaDungeonItem = null;
        this.m_cHomeName_Button = null; 
        this.m_cLabGateName = null;
        this.m_cMainButton = null;
        this.m_cGrid = null;

        this.m_cBtnAlertOk = null;

        base.Destory();
    }

    /// <summary>
    /// 设置当前副本名称
    /// </summary>
    /// <param name="gateName"></param>
    public void SetAreaGateName(string gateName)
    {
        this.m_strAreaGateName = gateName;
    }

    /// <summary>
    /// 判断当前副本是不是最新副本
    /// </summary>
    /// <param name="isNew"></param>
    /// <returns></returns>
    public void IsNewDungeon(bool isNew)
    {
        this.m_bIsNewDungeon = isNew;
    }

    /// <summary>
    /// 设置优惠类型标签内容
    /// </summary>
    /// <param name="FavLabel"></param>
    private void SetFavTimeLabel(UILabel FavLabel)
    {
        switch (this.m_iFavTimeType)
        {
            case 1:
                FavLabel.text = "经验1.5倍";
                break;
            case 2:
                FavLabel.text = "经验2倍";
                break;
            case 3:
                FavLabel.text = "体力消耗减半";
                break;
            case 4:
                FavLabel.text = "素材掉率1.5倍";
                break;
            case 5:
                FavLabel.text = "元气2倍";
                break;
            case 6:
                FavLabel.text = "捕捉率1.5倍";
                break;
            case 7:
                FavLabel.text = "获得金币2倍";
                break;
            default:
                break;
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

                GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();
            }
            else {
                SendAgent.SendStrengthRecoverReq(Role.role.GetBaseProperty().m_iPlayerId);
            }
        }
    }
    
    /// <summary>
    /// 区域副本入口项点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void DidSelectedAreaDungeonItem(GUI_INPUT_INFO info, object[] args)
    {

        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            int index = (int)args[0];
            WorldManager.s_iCurrentGateIndex = index;

            RoleBaseProperty baseProperty = Role.role.GetBaseProperty();
            
            GateTable gateTable = this.m_lstGate[index];

            if (this.m_iFavTimeType == 3 && this.m_bIsNeedToDisplatFav)
            {
                if (gateTable.CostHP / 2 > Role.role.GetBaseProperty().m_iStrength)
                {
                    if (Role.role.GetBaseProperty().m_iDiamond > 0)
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
                    //this.m_cAlertView.SetActive(true);
                    //this.m_bIsAlertViewShow = true; 
                    return;
                }
            }
            else {
                if (gateTable.CostHP > Role.role.GetBaseProperty().m_iStrength)
                {
                    if (Role.role.GetBaseProperty().m_iDiamond > 0)
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
                    //this.m_cAlertView.SetActive(true);
                    //this.m_bIsAlertViewShow = true; 
                    return;
                }
            }

            this.m_bIsCurrentGui = true;

            GUIFightReady ready = (GUIFightReady)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FIGHTREADY);
            ready.SetTitle(gateTable.Name);
            if (this.m_iFavTimeType == 3 && this.m_bIsNeedToDisplatFav)
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

        //新手
        if (this.m_iDungeonID == GUIDE_FUNCTION.DUNGEONID_ID)
        { 
            SendAgent.SendBattleGateStartReq(Role.role.GetBaseProperty().m_iPlayerId, WorldManager.s_iCurrentWorldId, WorldManager.s_iCurrentAreaIndex, WorldManager.s_iCurrentDungeonIndex, WorldManager.s_iCurrentGateIndex);
            return;
        }

        GUIFriendFight guiFriendFight = (GUIFriendFight)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FRIENDFIGHT);
        guiFriendFight.SetLastGuiId(this.m_iID);
        guiFriendFight.SetTitle(this.m_strGateName);
        guiFriendFight.Show();
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
                    GUIArea guiArea = (GUIArea)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_AREA);
                    guiArea.Show();
                }
            }
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
            backbottom.ShowHalf();
            GUIMain guiMain = (GUIMain)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_MAIN);
            guiMain.Show();

        }
    }

    /// <summary>
    /// 设置优惠类型
    /// </summary>
    /// <param name="type"></param>
    public void SetFavTimeType(int type,bool isNeedToDisplay,int timeFromNow)
    {
        this.m_iFavTimeType = type;
        this.m_bIsNeedToDisplatFav = isNeedToDisplay;
        this.m_iFavTimeFromNow = timeFromNow;
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
