//Micro.Sanvey
//2013.11.27
//sanvey.china@gmail.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;

/// <summary>
/// 竞技场结束添加好友申请GUI
/// </summary>
public class GUIBattleAddFriend : GUIBase
{
    private const string RES_MAIN = "GUI_DungeonBattleFriend";         //主资源地址
    private const string PAN_CANCEL = "TopPanel";        //取消Pan地址
    private const string PAN_RIGHT = "MainPanel";         //滑出Panel地址
    private const string Pan_Info = "MainPanel/PlayInfo";  //显示角色信息
    private const string BTN_ADD_FRIEND = "Btn_Apply";  //申请好友按钮地址
    private const string BTN_CANCEL_FRIEND = "Btn_NoApply";  //不申请好友按钮地址

    private GameObject m_cPanSlide;   //panel滑动
    private GameObject m_cPanTop;  //取消panel
    private BriefInfoShow m_cUserInfo; //用户信息显示块
    private GameObject m_cBtnAddFriend;
    private GameObject m_cBtnCancelFriend;

    public GUIBattleAddFriend(GUIManager mgr)
        : base(mgr, GUI_DEFINE.GUIID_DUNGEON_BATTLE_FRIEND, GUILAYER.GUI_PANEL)
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
            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;
            //滑出动画panel
            this.m_cPanSlide = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_RIGHT);
            this.m_cPanTop = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_CANCEL);
            //申请好友
            this.m_cBtnAddFriend = GUI_FINDATION.GET_GAME_OBJECT(this.m_cPanSlide, BTN_ADD_FRIEND);
            m_cBtnAddFriend.AddComponent<GUIComponentEvent>().AddIntputDelegate(AddFriend_OnEvent);
            //不申请好友按钮
            this.m_cBtnCancelFriend = GUI_FINDATION.GET_GAME_OBJECT(this.m_cPanSlide, BTN_CANCEL_FRIEND);
            this.m_cBtnCancelFriend.AddComponent<GUIComponentEvent>().AddIntputDelegate(CancelFriend_OnEvent);

            m_cUserInfo = new BriefInfoShow(this.m_cPanSlide);

            m_cUserInfo.m_cUiInput.label.text = "";
            this.m_cUserInfo.m_cSign.overflowMethod = UILabel.Overflow.ResizeHeight;
            this.m_cUserInfo.m_cSign.width = 480;
        }

        this.m_cUserInfo.m_cInput.GetComponentInChildren<UIInput>().enabled = false;

        UpdateData();

        this.m_cGUIMgr.SetCurGUIID(this.ID);

        CTween.TweenPosition(this.m_cPanTop, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-540, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cPanSlide, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);

        SetLocalPos(Vector3.zero);
    }

    /// <summary>
    /// 更新显示数据
    /// </summary>
    private void UpdateData()
    {
        BattleFriend leader = Role.role.GetBattleFriendProperty().GetSelectFriend();
        //头像
        GUI_FUNCTION.SET_HeroBorderAndBack(m_cUserInfo.m_cSpBorder, m_cUserInfo.m_cSpFrame, leader.m_cLeaderHero.m_eNature);
        GUI_FUNCTION.SET_AVATORS(m_cUserInfo.m_cSpHero, leader.m_cLeaderHero.m_strAvatarM);
        GUI_FUNCTION.SET_NATURES(m_cUserInfo.m_cSpNature, leader.m_cLeaderHero.m_eNature);
        //英雄数据
        m_cUserInfo.m_cLbHeroLv.text = "Lv." + leader.m_cLeaderHero.m_iLevel.ToString();
        m_cUserInfo.m_cLbAttack.text = leader.m_cLeaderHero.m_iAttack.ToString();
        m_cUserInfo.m_cLbDefense.text = leader.m_cLeaderHero.m_iDefence.ToString();
        m_cUserInfo.m_cLbHP.text = leader.m_cLeaderHero.m_iHp.ToString();
        m_cUserInfo.m_cLbRecovery.text = leader.m_cLeaderHero.m_iRevert.ToString();
        m_cUserInfo.m_cLbLV.text = leader.m_cLeaderHero.m_iLevel.ToString();
        m_cUserInfo.m_cLbName.text = leader.m_cLeaderHero.m_strName;
        //玩家数据
        m_cUserInfo.m_cLbTitleId.text = leader.m_iID.ToString();  //playid
        m_cUserInfo.m_cLbTitleLV.text = leader.m_iLevel.ToString(); //play lv
        m_cUserInfo.m_cLbTitleName.text = leader.m_strName; //play name
        m_cUserInfo.m_cLbInfo.text = AthleticsExpTableManager.GetInstance().GetAthleticsNameByPoint(leader.m_PvpExp);  //pvp name
        m_cUserInfo.m_cUiInput.value = leader.m_strSign;         //signture
        m_cUserInfo.m_cUiInput.label.text = leader.m_strSign; //signture
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
        //base.Hiden();

        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        CTween.TweenPosition(this.m_cPanSlide, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0));
        CTween.TweenPosition(this.m_cPanTop, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-540, 270, 0), Destory);
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        this.m_cPanSlide = null;
        this.m_cPanTop = null;
        this.m_cUserInfo = null;
        this.m_cBtnAddFriend = null;
        this.m_cBtnCancelFriend = null;

        base.Destory();
    }

    /// <summary>
    /// 好友申请
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void AddFriend_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            //如果好友数量已满，弹框提示
            if (Role.role.GetFriendProperty().GetAll().Count >= RoleExpTableManager.GetInstance().GetMaxFriend(Role.role.GetBaseProperty().m_iLevel))
            {
                GUI_FUNCTION.MESSAGEM(OnSure, "好友数量已满");
                return;
            }
            FriendApplyHandle.CallBack = (ok =>
            {
                GUI_FUNCTION.LOADING_HIDEN();
                if (ok)
                {
                    GUI_FUNCTION.MESSAGEM(OnSure, "好友申请已发送！");
                }
                else
                {
                    GUI_FUNCTION.MESSAGEM(OnSure, "申请失败！");
                }
            });
            SendAgent.SendFriendApply(Role.role.GetBaseProperty().m_iPlayerId, Role.role.GetBattleFriendProperty().GetSelectFriend().m_iID);
        }
    }


    //提示确定//
    private void OnSure()
    {
        this.Hiden();

        if (Role.role.GetBaseProperty().m_bIsNeedShowStory)
        {
            GAME_SETTING.SaveNewDungeonOfNewArea(false);
            GUI_FUNCTION.SHOW_STORY(Role.role.GetBaseProperty().m_iStoryID, GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_AREA).Show);
            Role.role.GetBaseProperty().m_bIsNeedShowStory = false;
        }
        GUIArea area = (GUIArea)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_AREA);
        area.ResetCurrentAreaId();
        area.Show();
    }

    /// <summary>
    /// 好友不申请
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void CancelFriend_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.Hiden();

            if (Role.role.GetBaseProperty().m_bIsNeedShowStory)
            {
                GAME_SETTING.SaveNewDungeonOfNewArea(false);
                GUI_FUNCTION.SHOW_STORY(Role.role.GetBaseProperty().m_iStoryID, GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_AREA).Show);
                Role.role.GetBaseProperty().m_bIsNeedShowStory = false;
            }

            GUIArea area = (GUIArea)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_AREA);
            area.ResetCurrentAreaId();
            area.Show();
        }
    }
}