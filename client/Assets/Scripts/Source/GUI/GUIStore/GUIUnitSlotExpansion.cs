using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;

//单位槽扩张类 GUIUnitSlotExpansion
//Author: sunyi
//2013-11-26

public class GUIUnitSlotExpansion : GUIBase
{
    private const string RES_MAIN = "GUI_AlertView"; //主资源地址
    private const string ALERTVIEW = "AlertView"; //对话框资源地址
    private const string CONTENT = "Content"; //对话框内容资源地址
    private const string TOPPANEL = "TopPanel";//导航栏资源地址
    private const string BACK_BUTTON = "TopPanel/Button_Back";//房名返回按钮地址
    private const string HOME_NAME = "TopPanel/Label";//导航栏标签地址
    private const string OK_BUTTON = "AlertView/Btn_Ok";//确定按钮地址
    private const string REVERT_BUTTON = "AlertView/Btn_Revert";//恢复按钮地址
    private const string EXPANSION_BUTTON = "AlertView/Btn_Expand";//扩张按钮地址
    private const string CANCEL_BUTTON = "AlertView/Btn_Cancel";//恢复按钮地址

    private GameObject m_cAlertView;//对话框
    private GameObject m_cTopPanel;//导航栏
    private GameObject m_cBtnBack;//返回按钮
    private UILabel m_cLabelContent;//内容标签
    private UILabel m_cLabHomeName;//导航栏标签
    private GameObject m_cBtnExpansion;//扩张按钮
    private GameObject m_cBtnOk;//确定按钮
    private GameObject m_cBtnRevert;//恢复按钮地址
    private GameObject m_cBtnCancel;//取消按钮

    private bool m_bIsSecondAlert = false;//是否是第一次提示，若钻石不足则进行第二次提示玩家购买钻石

    public GUIUnitSlotExpansion(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_UNITSLOTEXPANSION, GUILAYER.GUI_PANEL)
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

            this.m_cTopPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, TOPPANEL);
            this.m_cTopPanel.SetActive(true);
            this.m_cTopPanel.transform.localPosition = new Vector3(-420, 270, 0);

            this.m_cLabHomeName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, HOME_NAME);
            this.m_cLabHomeName.text = "英雄栏扩张";

            this.m_cAlertView = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, ALERTVIEW);
            this.m_cAlertView.transform.position = new Vector3(640, 0, 0);

            this.m_cBtnBack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BACK_BUTTON);
            GUIComponentEvent backEvent = this.m_cBtnBack.AddComponent<GUIComponentEvent>();
            backEvent.AddIntputDelegate(OnClickBackCutton);

            this.m_cBtnExpansion = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, EXPANSION_BUTTON);
            GUIComponentEvent recoverEvent = this.m_cBtnExpansion.AddComponent<GUIComponentEvent>();
            recoverEvent.AddIntputDelegate(ExpansionUnitSlot);

            this.m_cBtnOk = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, OK_BUTTON);
            GUIComponentEvent okEvent = this.m_cBtnOk.AddComponent<GUIComponentEvent>();
            okEvent.AddIntputDelegate(ExpansionUnitSlot);

            this.m_cBtnRevert = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, REVERT_BUTTON);

            this.m_cBtnCancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, CANCEL_BUTTON);
            GUIComponentEvent cancelEvent = this.m_cBtnCancel.AddComponent<GUIComponentEvent>();
            cancelEvent.AddIntputDelegate(OnClickBackCutton);

            this.m_cLabelContent = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cAlertView, CONTENT);

        }

        this.m_cBtnRevert.SetActive(false);
        this.m_cBtnOk.SetActive(false);
        this.m_cBtnExpansion.SetActive(true);

        StringBuilder sb = new StringBuilder();
        sb.Append("花费" + GAME_DEFINE.DiamondUnitSlotCost + "个钻石可以将英雄持有槽\n增加" + GAME_DEFINE.UnitSlotAddPerDiamond + "个\n\n#00FF00]现在的英雄持有槽为");
        this.m_cLabelContent.text = sb.ToString() + Role.role.GetBaseProperty().m_iMaxHeroCount + "个";

        this.m_cGUIMgr.SetCurGUIID(this.m_iID);
        SetLocalPos(Vector3.zero);

        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-420, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cAlertView, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_INCREASE_HERO_MAX_NUMBR));
    }
    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //base.Hiden();

        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        CTween.TweenPosition(this.m_cAlertView, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 0, 0), new Vector3(640, 0, 0));

        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-420, 270, 0),Destory);

        this.m_bIsSecondAlert = false;

        ResourceMgr.UnloadUnusedResources();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        this.m_cAlertView = null;
        this.m_cTopPanel = null;
        this.m_cBtnBack = null;
        this.m_cLabelContent = null;
        this.m_cLabHomeName = null;
        this.m_cBtnExpansion = null;
        this.m_cBtnOk = null;
        this.m_cBtnRevert = null;
        this.m_cBtnCancel = null;

        base.Destory();
    }

    /// <summary>
    /// 返回按钮事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickBackCutton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            GUIStore store = (GUIStore)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_STORE);
            store.Show();
        }
    }

    /// <summary>
    /// 单位槽恢复按钮事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void ExpansionUnitSlot(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (!this.m_bIsSecondAlert)
            {
                if (Role.role.GetBaseProperty().m_iDiamond > 0)
                {
                    //扩张单位槽
                    SendAgent.SendUnitSlotExpansionReq(Role.role.GetBaseProperty().m_iPlayerId);
                    this.m_bIsSecondAlert = false;
                }
                else
                {
                    this.m_bIsSecondAlert = true;
                    StringBuilder sb = new StringBuilder();
                    sb.Append("水晶不足，是否购买水晶？");
                    this.m_cLabelContent.text = sb.ToString();
                    this.m_cBtnOk.SetActive(true);
                    this.m_cBtnExpansion.SetActive(false);
                }
            }
            else
            {
                Hiden();

                GUIBackFrameBottom bottom = (GUIBackFrameBottom)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM);
                bottom.HiddenHalf();

                GUIGem gem = (GUIGem)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_GEM);
                gem.SetLastGuiId(this.m_iID);
                SendAgent.SendStoreDiamondPrice();

                GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();
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
