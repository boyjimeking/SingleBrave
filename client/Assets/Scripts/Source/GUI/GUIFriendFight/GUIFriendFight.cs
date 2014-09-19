using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using Game.Resource;


//战友栏页面
//Author:sunyi
//2013-11-25

public class GUIFriendFight : GUIBase
{

    private const string RES_MAIN = "GUI_FriendFight";//主资源地址
    private const string RES_FRIENDITEM = "GUI_FriendItem";//战友栏资源地址
    private const string UIGRID = "ClipView/FriendListView/UIGrid";//列表资源地址
    private const string LISTVIEW = "ClipView/FriendListView";//滚动视图地址
    private const string CLIPVIEW = "ClipView";//列表资源地址
    private const string TOPPANEL = "Panel_Title";//导航栏

    private const string BUTTON_HOME = "Panel_Title/Button_HomeName"; //房名按钮地址
    private const string BUTTON_MAIN = "Panel_Title/Button_MainPanel"; //返回主页面按钮地址
    private const string LABEL_HOMENAME = "Panel_Title/Label_HomeName"; //关卡名地址

    private const string LABEL_FRIENDNAME = "Label_Name";//战友名称标签地址
    private const string LABEL_LV = "Label_LVNo";//战友等级
    private const string LABEL_EQUIPT = "Equipment/Label_Equipt";//装备名称标签地址
    private const string EQUIPT_PARENT = "Equipment";//装备父对象地址
    //private const string EQUIPT_FRAME_SPR = "Equipment/Icon/Sprite_Bg";//装备属性框地址
    private const string EQUIPT_ICON_SPR = "Equipment/Icon/Sprite_Content";//装备图标地址
    private const string LABEL_FRIENDPOINT = "Label_FpNo";//友情点数

    private const string MONSTER_LABEL_HEROLV = "MonsterItem/LabelBottom";//英雄等级标签
    private const string MONSTER_SPR_ICON = "MonsterItem/ItemIcon";//英雄头像
    private const string MONSTER_SPR_FRAME = "MonsterItem/ItemFrame";//英雄头像边框
    private const string MONSTER_SPR_BG = "MonsterItem/ItemBg";//英雄背景
    private const string MONSTER_SPR_FRIEND = "MonsterItem/Spr_Friend";//Friend图片地址

    private UnityEngine.Object m_cFrienIitem; //战友栏
    private GameObject m_cButton_HomeName; //房名按钮
    private UILabel m_cHomeName;//房名
    private GameObject m_cButton_Main; //返回主页面按钮
    private GameObject m_cGrid;//列表
    private GameObject m_cClipView;//列表面板
    private GameObject m_cTopPanel;//导航栏
    private GameObject m_cListView;//滚动视图

    private UISprite m_cSprBg;//英雄头像背景
    private UISprite m_cSprFrame;//英雄头像边框
    private UISprite m_cSprIcon;//英雄头像
    private UILabel m_cLabHeroLv;//英雄等级标签

    private List<GameObject> m_lstFriend = new List<GameObject>(); //战友列表
    private List<BattleFriend> m_lstBattleFriend = new List<BattleFriend>();//获取的战友列表
    private int m_iLastGUIId;//上一个guiid

    private string m_strGateName;//关卡名
    private bool m_bHasShow = false;  //加载过showobject

    public GUIFriendFight(GUIManager guimgr)
        : base(guimgr, GUI_DEFINE.GUIID_FRIENDFIGHT, UILAYER.GUI_PANEL)
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
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_MAIN);
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_FRIENDITEM);
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

            this.m_cButton_HomeName = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_HOME);
            GUIComponentEvent guiHomeNameEvent = this.m_cButton_HomeName.AddComponent<GUIComponentEvent>();
            guiHomeNameEvent.AddIntputDelegate(OnClickHomeNameButton);

            this.m_cButton_Main = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_MAIN);
            GUIComponentEvent guiMainEvent = this.m_cButton_Main.AddComponent<GUIComponentEvent>();
            guiMainEvent.AddIntputDelegate(OnClickMainButton);

            this.m_cHomeName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LABEL_HOMENAME);

            this.m_cListView = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LISTVIEW);

            this.m_cTopPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, TOPPANEL);
            this.m_cTopPanel.transform.localPosition = new Vector3(-640, 270, 0);

            this.m_cClipView = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, CLIPVIEW);
            this.m_cClipView.transform.localPosition = new Vector3(640, 0, 0);

            this.m_cGrid = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, UIGRID);

            this.m_cFrienIitem = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_FRIENDITEM);

        }

        this.m_cListView.transform.localPosition = new Vector3(0, 0, 0);
        UIPanel panel = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(this.m_cGUIObject, LISTVIEW);
        float y = -107.0f;
        panel.clipRange = new Vector4(panel.clipRange.x, y, panel.clipRange.z, panel.clipRange.w);

        this.m_cHomeName.text = this.m_strGateName;

        foreach (GameObject item in this.m_lstFriend)
        {
            GameObject.DestroyImmediate(item);
        }

        this.m_lstFriend.Clear();

        this.m_lstBattleFriend.Clear();

        for (int i = 0; i < Role.role.GetBattleFriendProperty().GetAll().Count; i++)
        {
            this.m_lstBattleFriend.Add(Role.role.GetBattleFriendProperty().GetAll()[i]);
        }

        for (int i = 0; i < m_lstBattleFriend.Count; i++)
        {
            GameObject friendCell = GameObject.Instantiate(m_cFrienIitem) as GameObject;
            friendCell.transform.parent = this.m_cGrid.transform;
            friendCell.transform.localScale = Vector3.one;
            friendCell.transform.localPosition = new Vector3(0, 155 - i * 130, 0);
            UILabel friendNameLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(friendCell, LABEL_FRIENDNAME);//战友名称标签
            friendNameLabel.text = m_lstBattleFriend[i].m_strName;

            UILabel friendLvLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(friendCell, LABEL_LV);//战友等级
            friendLvLabel.text = m_lstBattleFriend[i].m_iLevel.ToString();

            UILabel friendPointLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(friendCell, LABEL_FRIENDPOINT);
            friendPointLabel.text = "+" + m_lstBattleFriend[i].m_iFriendPoint.ToString();

            GameObject sprFriend = GUI_FINDATION.GET_GAME_OBJECT(friendCell, MONSTER_SPR_FRIEND);
            if (m_lstBattleFriend[i].m_bIsFriend)
            {
                sprFriend.SetActive(true);
                TweenAlpha.Begin(sprFriend, 1, 0).style = UITweener.Style.PingPong;
            }
            else
            {
                sprFriend.SetActive(false);
            }

            UILabel equiptLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(friendCell, LABEL_EQUIPT);//装备名称
            UISprite sprEquipIcon = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(friendCell, EQUIPT_ICON_SPR);//英雄图标地址
            UILabel labHeroLv = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(friendCell, MONSTER_LABEL_HEROLV);//英雄等级标签
            GameObject equipParent = GUI_FINDATION.GET_GAME_OBJECT(friendCell, EQUIPT_PARENT);
            UISprite sprBg = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(friendCell, MONSTER_SPR_BG);//英雄头像背景
            UISprite sprFrame = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(friendCell, MONSTER_SPR_FRAME);//英雄属性框地址
            UISprite sprIcon = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(friendCell, MONSTER_SPR_ICON);//英雄头像图标
            GUI_FUNCTION.SET_AVATORS(sprIcon, this.m_lstBattleFriend[i].m_cLeaderHero.m_strAvatarM);
            GUI_FUNCTION.SET_HeroBorderAndBack(sprFrame, sprBg, this.m_lstBattleFriend[i].m_cLeaderHero.m_eNature);

            labHeroLv.text = "LV." + m_lstBattleFriend[i].m_cLeaderHero.m_iLevel.ToString();

            if (m_lstBattleFriend[i].m_iEquipItemTableID == -1)
            {
                equipParent.SetActive(false);
            }
            else
            {
                equipParent.SetActive(true);
                ItemTable tmpItemTable = ItemTableManager.GetInstance().GetItem(this.m_lstBattleFriend[i].m_iEquipItemTableID);
                GUI_FUNCTION.SET_ITEMS(sprEquipIcon, tmpItemTable.SpiritName);
                equiptLabel.text = tmpItemTable.Name;
            }

            int nature = (int)m_lstBattleFriend[i].m_cLeaderHero.m_eNature;

            GUIComponentEvent guiFriendCellEvent = friendCell.AddComponent<GUIComponentEvent>();
            guiFriendCellEvent.AddIntputDelegate(DidSelectedFriendItemEvent, i);
            this.m_lstFriend.Add(friendCell);
        }

        this.m_cGUIMgr.SetCurGUIID(this.m_iID);
        SetLocalPos(Vector3.zero);

        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-640, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cClipView, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);

        List<BattleFriend> lst = Role.role.GetBattleFriendProperty().GetAll();
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

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        base.Hiden();

        //SetLocalPos(Vector3.one * 0XFFFF);

        ResourceMgr.UnloadUnusedResources();

        Destory();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {

        if (m_lstFriend != null)
        {
            foreach (GameObject obj in this.m_lstFriend)
            {
                GameObject.Destroy(obj);
            }
        }

        this.m_lstFriend.Clear();
        if (this.m_lstBattleFriend != null)
        {
            this.m_lstBattleFriend.Clear();
        }

        this.m_cFrienIitem = null;
        this.m_cButton_HomeName = null;
        this.m_cHomeName = null;
        this.m_cButton_Main = null;
        this.m_cGrid = null;
        this.m_cClipView = null;
        this.m_cTopPanel = null;
        this.m_cListView = null;

        this.m_cSprBg = null;
        this.m_cSprFrame = null;
        this.m_cSprIcon = null;
        this.m_cLabHeroLv = null;

        base.Destory();
    }

    /// <summary>
    /// 战友栏入口项点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void DidSelectedFriendItemEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            BattleFriendProperty battleFriend = new BattleFriendProperty();
            List<BattleFriend> lstBattleFriend = battleFriend.GetAll();

            BattleFriend tmp = m_lstBattleFriend[(int)args[0]];
            if (tmp!=null)
            {
                Hiden();

                Role.role.GetBattleFriendProperty().SetSelectFriendID(tmp.m_iID);
                GUIFightReady fightready = (GUIFightReady)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FIGHTREADY);
                fightready.Show();
            }

        }
    }

    /// <summary>
    /// 设置关卡名
    /// </summary>
    /// <param name="tittle"></param>
    public void SetTitle(string tittle)
    {
        this.m_strGateName = tittle;
    }

    /// <summary>
    /// 设置上一个GUIID
    /// </summary>
    /// <param name="id"></param>
    public void SetLastGuiId(int id)
    {
        this.m_iLastGUIId = id;
    }

    /// <summary>
    /// 获取上一个GUIID
    /// </summary>
    /// <returns></returns>
    public int GetLastGuiId()
    {
        return this.m_iLastGUIId;
    }

    /// <summary>
    /// 房名返回事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void OnClickHomeNameButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            if (this.m_iLastGUIId == GUI_DEFINE.GUIID_AREADUNGEON)
            {
                GUIAreaDungeon areaDungeon = (GUIAreaDungeon)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_AREADUNGEON);
                areaDungeon.Show();
            }
            else if (this.m_iLastGUIId == GUI_DEFINE.GUIID_ESPDUNGEONGATE)
            {
                GUIEspDungeonGate espDungeonGate = (GUIEspDungeonGate)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_ESPDUNGEONGATE);
                espDungeonGate.Show();
            }
        }
    }

    /// <summary>
    /// 返回主页面按钮事件
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


}

