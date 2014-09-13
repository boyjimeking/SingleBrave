//  GUIFriendGift.cs
//  Author: Cheng Xia
//  2013-1-10

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;

/// <summary>
/// 好友礼物赠送界面
/// </summary>
class GUIFriendGiftGive : GUIBase
{
    private const string RES_MAIN = "GUI_FriendGiftGive";                   //主资源地址
    private const string RES_PANINFO = "PanInfo";
    private const string RES_PANCANCEL = "Title_Cancel";                   //取消Pan地址
    private const string TAB_TABLE = "Panel/Table";             //Table地址
    private const string PANEL = "PanInfo/Panel";//面板地址
    private const string RES_TABLE = "ItemPanel/Table";         //赠送的Item面板
    private const string TABLE_POS = "FriendGiftItem_";         //赠送的Item面板位置
    private const string ITEM_FRAME = "Frame";  //赠送礼物的FRAME
    private const string ITEM_ITEM = "Item";    //赠送礼物的图标//
    private const string ITEM_LAB_EXPLAIN = "LabExplain";   //赠送礼物的介绍//
    private const string ITEM_LAB_NAME = "LabName"; //赠送礼物的名字//
    private const string DRAGLE_PANEL = "PanInfo/Panel";//滑动面板

    private const string BTN_CANCEL = "Title_Cancel/Btn_Cancel";        //取消按钮地址
    private const string BTN_GET = "BtnGet";                  //赠送按钮地址
    private const string BTN_SELECT = "PanTitle/BtnSelect"; //物品选择//
    private const string BTN_SENDALL = "PanTitle/BtnSendAll";   //一起赠送
    private const string BTN_RIGHT = "PanTitle/ArrowRight";
    private const string BTN_LEFT = "PanTitle/ArrowLeft";

    private GameObject m_cPanInfo;
    private GameObject m_cPanCancel;    //返回面板
    private UITable m_cTable;             //table
    private GameObject m_cItemTable;    //赠送的物品面板节点
    private UIToggle m_tSelect;
    private UISprite m_cSprArrowLeft;           //向左滑动特效
    private UISprite m_cSprArrowRight;          //向右滑动特效

    private List<GUIFriendGiftGiveItem> m_clstFriends = new List<GUIFriendGiftGiveItem>();

    public List<FriendSendData> m_lstFriendSendData;   //发送的数据结构
    private List<Friend> m_lstFriend;  //好友没赠送的礼物列表

    private const float OFFSET_DIS = 450f;
    private bool m_bDrag;   //拖拽状态
    private GameObject[] m_vecTeamPos;  //赠送的物品
    private int m_iIndex;   //当前面板位置
    private int m_cGiftNum; //当前礼物编号//

    private TDAnimation m_cEffectLeft;         //特效类
    private TDAnimation m_cEffectRight;        //特效类

    public GameObject m_cBtnSelect; //物品删选//
    private GameObject m_cBtnCancel;//返回按钮
    private GameObject m_cBtnGet;//收取按钮
    private GameObject m_cBtnSendAll;//全部赠送按钮
    private GameObject m_cPanel;//面板
    private bool m_bSend;   //是否需要发送

    public GUIFriendGiftGive(GUIManager mgr)
        : base(mgr, GUI_DEFINE.GUIID_FRIENDGIFTGIVE, GUILAYER.GUI_PANEL)
    {
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        m_cPanInfo = null;
        m_cPanCancel = null;    //返回面板
        m_cTable = null;             //table
        m_cItemTable = null;    //赠送的物品面板节点
        m_tSelect = null;
        m_cSprArrowLeft = null;           //向左滑动特效
        m_cSprArrowRight = null;          //向右滑动特效

        m_cEffectLeft = null;         //特效类
        m_cEffectRight = null;        //特效类

        m_cBtnSelect = null; //物品删选//
        m_cBtnCancel = null;//返回按钮
        m_cBtnGet = null;//收取按钮
        m_cBtnSendAll = null;//全部赠送按钮
        m_cPanel = null;//面板

        m_vecTeamPos = null;

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
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_MAIN);
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, GUIFriendGiftGiveItem.RES_GIVEITEM);
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
            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            m_cPanInfo = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, RES_PANINFO);
            m_cPanCancel = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, RES_PANCANCEL);

            //table
            m_cTable = GUI_FINDATION.GET_OBJ_COMPONENT<UITable>(this.m_cPanInfo, TAB_TABLE);
            m_cItemTable = GUI_FINDATION.GET_GAME_OBJECT(this.m_cPanInfo, RES_TABLE);

            m_tSelect = GUI_FINDATION.GET_OBJ_COMPONENT<UIToggle>(m_cPanInfo, BTN_SELECT);

            //取消按钮
            this.m_cBtnCancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_CANCEL);
            GUIComponentEvent gui_event = this.m_cBtnCancel.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnCancel_OnEvent);

            //获取按钮
            this.m_cBtnGet = GUI_FINDATION.GET_GAME_OBJECT(m_cPanInfo, BTN_GET);
            gui_event = this.m_cBtnGet.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(Get_OnEvent);

            //物品选择
            m_cBtnSelect = GUI_FINDATION.GET_GAME_OBJECT(m_cPanInfo, BTN_SELECT);
            gui_event = m_cBtnSelect.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(SendSelect_OnEvent);

            this.m_cPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PANEL);

            this.m_cBtnSendAll = GUI_FINDATION.GET_GAME_OBJECT(m_cPanInfo, BTN_SENDALL);
            gui_event = this.m_cBtnSendAll.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(SendAll_OnEvent);

            //左右导航
            this.m_cSprArrowLeft = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cPanInfo, BTN_LEFT);
            this.m_cSprArrowLeft.gameObject.AddComponent<GUIComponentEvent>().AddIntputDelegate(Left_OnEvent);
            this.m_cEffectLeft = new TDAnimation(m_cSprArrowLeft.atlas, m_cSprArrowLeft); //左右导航
            this.m_cSprArrowRight = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cPanInfo, BTN_RIGHT);
            this.m_cSprArrowRight.gameObject.AddComponent<GUIComponentEvent>().AddIntputDelegate(Right_OnEvent);
            this.m_cEffectRight = new TDAnimation(m_cSprArrowRight.atlas, m_cSprArrowRight);

            m_lstFriendSendData = new List<FriendSendData>();

            m_vecTeamPos = new GameObject[3];
            for (int i = 0; i < 3; i++)
            {
                GameObject uiTeam = GUI_FINDATION.GET_GAME_OBJECT(m_cItemTable, TABLE_POS + (i + 1));
                m_vecTeamPos[i] = uiTeam;

                //添加事件//
                gui_event = uiTeam.AddComponent<GUIComponentEvent>();
                gui_event.AddIntputDelegate(OnTeamDrag);
            }

        }

        this.m_cEffectLeft.Play("ArrowLeft", Game.Base.TDAnimationMode.Loop, 0.4F);
        this.m_cEffectRight.Play("ArrowRight", Game.Base.TDAnimationMode.Loop, 0.4F);

        m_iIndex = 0;

        this.m_cPanel.transform.localPosition = new Vector3(-308, -5, 0);
        UIPanel panel = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(this.m_cGUIObject, PANEL);
        float y = -160.0f;
        panel.clipRange = new Vector4(panel.clipRange.x, y, panel.clipRange.z, panel.clipRange.w);

        this.m_lstFriendSendData.Clear();
        this.m_bSend = false;
        m_tSelect.value = false;

        CreateGUI();

        InitThreeItem();

        this.m_cGUIMgr.SetCurGUIID(this.ID);
        SetLocalPos(Vector3.zero);

        CTween.TweenPosition(this.m_cPanCancel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-540, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cPanInfo, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);
    }


    //初始化3项滑动项目属性
    private void InitThreeItem()
    {
        int pre = m_cGiftNum - 1;
        if (pre < 0)
        {
            pre += 10;
        }
        else if (pre > 9)
        {
            pre -= 10;
        }
        int next = m_cGiftNum + 1;
        if (next < 0)
        {
            next += 10;
        }
        else if (next > 9)
        {
            next -= 10;
        }


        RefreshItem(pre, 0);
        RefreshItem(m_cGiftNum, 1);
        RefreshItem(next, 2);
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

        if (!IsShow()) return false;

        if (this.m_cEffectLeft != null)
        {
            this.m_cEffectLeft.Update();
        }
        if (this.m_cEffectRight != null)
        {
            this.m_cEffectRight.Update();
        }

        return base.Update();
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        if (m_bSend)
        {
            SendAgent.SendFriendSendGift(Role.role.GetBaseProperty().m_iPlayerId, m_lstFriendSendData);
            GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();
        }

        base.Hiden();

        CTween.TweenPosition(this.m_cPanCancel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-540, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cPanInfo, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero,Destory);

        SetLocalPos(Vector3.one * 0xFFFF);

        ResourcesManager.GetInstance().UnloadUnusedResources();

    }

    /// <summary>
    /// 导航箭头向左移
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void Left_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            //手动触发两次滑动
            OnTeamDrag(new GUI_INPUT_INFO() { m_eType = GUI_INPUT_INFO.GUI_INPUT_TYPE.DRAG, m_vecDelta = new Vector2(11, 0) }, null);
            OnTeamDrag(new GUI_INPUT_INFO() { m_eType = GUI_INPUT_INFO.GUI_INPUT_TYPE.PRESS }, null);
        }
    }

    /// <summary>
    /// 导航箭头向右移
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void Right_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            //手动触发两次滑动
            OnTeamDrag(new GUI_INPUT_INFO() { m_eType = GUI_INPUT_INFO.GUI_INPUT_TYPE.DRAG, m_vecDelta = new Vector2(-11, 0) }, null);
            OnTeamDrag(new GUI_INPUT_INFO() { m_eType = GUI_INPUT_INFO.GUI_INPUT_TYPE.PRESS }, null);
        }
    }

    /// <summary>
    /// 选择
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void SendSelect_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            ReflashFriendList();
        }
    }

    private void ReflashFriendList()
    {

        //this.m_cPanel.transform.localPosition = new Vector3(-308, -5, 0);
        //UIPanel panel = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(this.m_cGUIObject, PANEL);
        //float y = -160.0f;
        //panel.clipRange = new Vector4(panel.clipRange.x, y, panel.clipRange.z, panel.clipRange.w);

        //m_cTable.repositionNow = true;
        CreateGUI();
    }

    /// <summary>
    /// 一起赠送
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void SendAll_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (this.m_lstFriend.Count != 0)
            {
                foreach (Friend f in m_lstFriend)
                {
                    FriendSendData fsd = new FriendSendData();
                    fsd.m_iGiftID = GetSelectGiftId();
                    fsd.m_iFriendID = f.m_iID;
                    m_lstFriendSendData.Add(fsd);
                }
                foreach (Friend fChild in Role.role.GetFriendProperty().GetAll())
                {
                    foreach (FriendSendData fsdChild in m_lstFriendSendData)
                    {
                        if (fChild.m_iID == fsdChild.m_iFriendID)
                        {
                            fChild.m_bSend = true;
                        }
                    }
                }

                CreateGUI();

                m_bSend = true;
            }
        }
    }

    /// <summary>
    /// 赠送
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void Send_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Friend f = m_lstFriend[(int)args[0]];
            FriendSendData fsd = new FriendSendData();
            fsd.m_iGiftID = GetSelectGiftId();
            fsd.m_iFriendID = f.m_iID;

            this.m_lstFriendSendData.Add(fsd);

            //SendAgent.SendFriendSendGift(Role.role.GetBaseProperty().m_iPlayerId, m_lstFriendSendData);

            foreach (Friend fChild in Role.role.GetFriendProperty().GetAll())
            {
                foreach (FriendSendData fsdChild in m_lstFriendSendData)
                {
                    if (fChild.m_iID == fsdChild.m_iFriendID)
                    {
                        fChild.m_bSend = true;
                    }
                }
            }

            CreateGUI();

            this.m_bSend = true;
        }
    }

    /// <summary>
    /// 收取按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void Get_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            GUIFriendGift friendGiftGet = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FRIENDGIFT) as GUIFriendGift;
            friendGiftGet.Show();
        }
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

            GUIFriendGiftSelect friendGiftSelect = (GUIFriendGiftSelect)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_FRIENDGIFTSELECT);
            friendGiftSelect.SetOldPos(m_cGiftNum);

            friendGiftSelect.Show();
        }
    }

    private int GetSelectGiftId()
    {
        int id = 0;

        FriendGiftItem m_cItem = FriendGiftItemTableManager.GetInstance().GetGiftItemByIndex(m_cGiftNum);
        id = m_cItem.m_iID;

        return id;
    }

    /// <summary>
    /// 创建GUI
    /// </summary>
    public void CreateGUI()
    {

        foreach (GUIFriendGiftGiveItem iChild in m_clstFriends)
        {
            iChild.Destory();
        }
        m_clstFriends.Clear();

        m_lstFriend = new List<Friend>();
        foreach (Friend f in Role.role.GetFriendProperty().GetAll())
        {
            if (!f.m_bSend)
            {
                m_lstFriend.Add(f);
            }
        }

        //如果点击了选择按钮
        if (m_tSelect.value)
        {
            for (int i = 0; i < m_lstFriend.Count; i++)
            {
                if (!m_lstFriend[i].m_lstWantGift.Contains(GetSelectGiftId()))
                {
                    m_lstFriend.Remove(m_lstFriend[i]);
                    i--;
                }
            }
     
        
        }
        else
        {
            if (m_lstFriend != null)
            {
                m_lstFriend.Clear();
            }
            foreach (Friend f in Role.role.GetFriendProperty().GetAll())
            {
                if (!f.m_bSend)
                {
                    m_lstFriend.Add(f);
                }
            }

        }

        for (int i = 0; i < m_lstFriend.Count; i++)
        {
            GUIFriendGiftGiveItem tmp = new GUIFriendGiftGiveItem();
            tmp.m_cItem.transform.parent = m_cTable.gameObject.transform;
            tmp.m_cItem.transform.localScale = Vector3.one;

            tmp.SetInfo(m_lstFriend[i]);

            GUIComponentEvent gui_event = tmp.m_cBtnGive.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(Send_OnEvent, i);

            m_clstFriends.Add(tmp);
        }
        m_cTable.repositionNow = true;

        this.m_cPanel.transform.localPosition = new Vector3(-308, -5, 0);
        UIPanel panel = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(this.m_cGUIObject, PANEL);
        float y = -160.0f;
        panel.clipRange = new Vector4(panel.clipRange.x, y, panel.clipRange.z, panel.clipRange.w);

    }

    /// <summary>
    /// 滚动标签
    /// </summary>
    /// <param name="info"></param>
    /// <param name="arg"></param>
    private void OnTeamDrag(GUI_INPUT_INFO info, object[] arg)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.DRAG)
        {
            m_cItemTable.transform.localPosition = m_cItemTable.transform.localPosition + new Vector3(info.m_vecDelta.x, 0, 0);

            m_bDrag = true;
        }
        else if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.PRESS)
        {
            if (m_bDrag)
            {
                if (Math.Abs(m_iIndex * OFFSET_DIS - m_cItemTable.transform.localPosition.x) > 10)
                {
                    if (m_iIndex * OFFSET_DIS - m_cItemTable.transform.localPosition.x > 0)
                    {
                        this.m_iIndex--;
                        this.m_cGiftNum++;
                        if (m_cGiftNum < 0)
                        {
                            m_cGiftNum += 10;
                        }
                        else if (m_cGiftNum > 9)
                        {
                            m_cGiftNum -= 10;
                        }

                        FriendGiftItem m_cItem = FriendGiftItemTableManager.GetInstance().GetGiftItemByIndex(m_cGiftNum);

                        int tmpIndex = (2 - m_iIndex) % 3;
                        while (tmpIndex < 0)
                        {
                            tmpIndex += 3;
                        }
                        int m_iGiftPanelNum = tmpIndex - 1;

                        if (m_iGiftPanelNum < 0)
                        {
                            m_iGiftPanelNum += 3;
                        }
                        RefreshItem(m_cGiftNum, m_iGiftPanelNum);

                        m_vecTeamPos[tmpIndex].transform.localPosition = new Vector3(OFFSET_DIS * -(m_iIndex - 1), m_vecTeamPos[tmpIndex].transform.localPosition.y, m_vecTeamPos[tmpIndex].transform.localPosition.z);
                        int index = m_cGiftNum + 1;

                        if (index < 0)
                        {
                            index += 10;
                        }
                        else if (index > 9)
                        {
                            index -= 10;
                        }
                        for (int i = 0; i < 5; i++)
                        {
                        }
                    }
                    else
                    {
                        m_iIndex++;
                        m_cGiftNum--;
                        if (m_cGiftNum < 0)
                        {
                            m_cGiftNum += 10;
                        }
                        else if (m_cGiftNum > 9)
                        {
                            m_cGiftNum -= 10;
                        }
                        FriendGiftItem m_cItem = FriendGiftItemTableManager.GetInstance().GetGiftItemByIndex(m_cGiftNum);
    
                        int tmpIndex = (2 - (2 + m_iIndex) % 3) % 3;

                        while (tmpIndex < 0)
                        {
                            tmpIndex += 3;
                        }

                        int m_iGiftPanelNum = tmpIndex + 1;

                        if (m_iGiftPanelNum > 2)
                        {
                            m_iGiftPanelNum -= 3;
                        }
                        RefreshItem(m_cGiftNum, m_iGiftPanelNum);

                        m_vecTeamPos[tmpIndex].transform.localPosition = new Vector3(OFFSET_DIS * -(m_iIndex + 1), m_vecTeamPos[tmpIndex].transform.localPosition.y, m_vecTeamPos[tmpIndex].transform.localPosition.z);
                        int index = m_cGiftNum - 1;

                        if (index < 0)
                        {
                            index += 10;
                        }
                        else if (index > 9)
                        {
                            index -= 10;
                        }
                        for (int i = 0; i < 5; i++)
                        {

                            //RefreshHero(tmpIndex, index, i);
                        }
                    }

                    CTween.TweenPosition(m_cItemTable, 0.4f, new Vector3(m_iIndex * OFFSET_DIS, m_cItemTable.transform.localPosition.y, m_cItemTable.transform.localPosition.z));
                    //翻页特效
                    SoundManager.GetInstance().PlaySound2(SOUND_DEFINE.SE_SLIDE);
                }
                else
                {
                    CTween.TweenPosition(m_cItemTable, 0.2f, new Vector3(m_iIndex * OFFSET_DIS, m_cItemTable.transform.localPosition.y, m_cItemTable.transform.localPosition.z));
                    //翻页特效
                    SoundManager.GetInstance().PlaySound2(SOUND_DEFINE.SE_SLIDE);
                }

 
                ReflashFriendList();
            }
            m_bDrag = false;

        }


    }

    //刷新礼物//
    private void RefreshItem(int fgi, int pos)
    {
        GameObject uiPos = GUI_FINDATION.GET_GAME_OBJECT(m_cItemTable, TABLE_POS + (pos + 1));

        UISprite spFrame = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(uiPos, ITEM_FRAME);
        UISprite spItem = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(uiPos, ITEM_ITEM);
        UILabel labExplain = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(uiPos, ITEM_LAB_EXPLAIN);
        UILabel labName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(uiPos, ITEM_LAB_NAME);

        FriendGiftItem m_cItem = FriendGiftItemTableManager.GetInstance().GetGiftItemByIndex(fgi);
        switch (m_cItem.m_eType)
        {
            case GiftType.Diamond:
                spFrame.spriteName = "frame1";
                spItem.spriteName = m_cItem.m_strSpiritName;
                break;
            case GiftType.Gold:
                spFrame.spriteName = "frame1";
                spItem.spriteName = m_cItem.m_strSpiritName;
                break;
            case GiftType.FriendPoint:
                spFrame.spriteName = "frame1";
                spItem.spriteName = m_cItem.m_strSpiritName;
                break;
            case GiftType.FarmPoint:
                spFrame.spriteName = "frame1";
                spItem.spriteName = m_cItem.m_strSpiritName;
                break;
            case GiftType.Item:
                spFrame.spriteName = "getsucai";
                spItem.spriteName = m_cItem.m_strSpiritName;
                break;
            default:
                break;
        }

        labExplain.text = m_cItem.m_strDesc;
        labName.text = m_cItem.m_strName + m_cItem.m_strNumText;
    }

    /// <summary>
    /// 设置旧值
    /// </summary>
    /// <param name="Index"></param>
    /// <param name="GiftNum"></param>
    public void SetOldPos(int Index, int GiftNum)
    {
        this.m_iIndex = Index;
        this.m_cGiftNum = GiftNum;
    }
}


/// <summary>
/// 好友期望礼物列表项
/// </summary>
public class GUIFriendGiftGiveItem
{
    public const string RES_GIVEITEM = "GUI_FriendGiftGiveItem";   //Item主资源地址//
    private const string BTN_GIVE = "Btn_Give"; //赠送按钮地址//
    private const string MONSTER_BORDER = "GUI_MonsterItem/ItemBorder";
    private const string MONSTER_FRAME = "GUI_MonsterItem/ItemFrame";
    private const string MONSTER_MONSTER = "GUI_MonsterItem/ItemMonster";
    private const string LAB_NAME = "LabName"; //名字标签//
    private const string RES_PREVIEWITEM = "GUI_PreviewItem_"; //礼物节点
    private const string ITEM_BORDER = "ItemBorder";    //礼物边框
    private const string ITEM_FRAME = "ItemFrame";  //礼物背景
    private const string ITEM_ICON = "ItemIcon";    //礼物图标

    public GameObject m_cItem;  //Item主资源//
    public GameObject m_cBtnGive;   //赠送按钮//
    public UISprite m_spMonsterBorder;  //好友英雄头像边框//
    public UISprite m_spMonsterFrame;   //好友英雄头像背景//
    public UISprite m_spMonsterIcon;    //好友英雄头像图标//
    public UILabel m_labName;   //好友名字//

    public GUIFriendGiftGiveItem()
    {
        m_cItem = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_GIVEITEM)) as GameObject;
        m_cBtnGive = GUI_FINDATION.GET_GAME_OBJECT(m_cItem, BTN_GIVE);
        m_spMonsterBorder = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, MONSTER_BORDER);
        m_spMonsterFrame = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, MONSTER_FRAME);
        m_spMonsterIcon = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cItem, MONSTER_MONSTER);
        m_labName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cItem, LAB_NAME);
    }

    public void SetInfo(Friend friend)
    {
        m_labName.text = friend.m_strName;

        //好友单位信息//
        Hero hero = friend.m_cHero;
        HeroTable table = HeroTableManager.GetInstance().GetHeroTable(hero.m_iTableID);

        GUI_FUNCTION.SET_AVATORS(m_spMonsterIcon, table.AvatorMRes);
        GUI_FUNCTION.SET_HeroBorderAndBack(m_spMonsterBorder, m_spMonsterFrame, (Nature)table.Property);
    }

    //销毁对象//
    public void Destory()
    {
        GameObject.Destroy(m_cItem);
    }
}

//好友礼物送出数据//
public class FriendSendData
{
    public int m_iFriendID;
    public int m_iGiftID;
}
