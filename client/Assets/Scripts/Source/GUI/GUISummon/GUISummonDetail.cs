//Micro.Sanvey
//2013.11.12
//sanvey.china@gmail.com

using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Resource;
using Game.Base;

/// <summary>
/// 召唤详细提示GUI类
/// </summary>
public class GUISummonDetail : GUIBase
{
    private const string RES_MAIN = "GUI_HeroSummonDetail";             //召唤主资源地址
    private const string BTN_CANCEL = "Title_Cancel/Btn_Cancel";    //返回按钮地址
    private const string PAN_CANCEL = "Title_Cancel";                 //返回上一层Pan地址
    private const string PAN_INFO = "PanInfo";                        //除了取消按钮的划出panel
    private const string MSG_IFNO = "PanInfo/MsgBox_Info";  //弹出的招募内容消息提示对象
    private const string MSG_TITLE = "Lb_Title";  //内容提示标题
    private const string MSG_LINE1 = "Lb_Line1";  //内容提示第一行
    private const string MSG_LINE2 = "Lb_Line2";  //内容提示第二行
    private const string MSG_LINE3 = "Lb_Line3"; //内容提示第三行
    private const string MSG_LINE4 = "Lb_Line4"; //内容提示第四行
    private const string MSG_LINE5 = "Lb_Line5"; //内容提示第五行
    private const string MSG_BTN = "Sp_Btn"; //内容提示按钮

    private GameObject m_cBtnCancel;       //取消按钮
    private GameObject m_cPanCancel;            //取消Pan
    private GameObject m_cPanInfo;  //除了取消按钮的划出panel
    private GameObject m_cMsgInfo;  //内容提示对象
    private UILabel m_cMsgTitle;  //内容提示标题
    private UILabel m_cMsgLine1; //内容提示第一行
    private UILabel m_cMsgLine2; //内容提示第二行
    private UILabel m_cMsgLine3; //内容提示第三行
    private UILabel m_cMsgLine4;  //内容提示第四行
    private UILabel m_cMsgLine5; //内容提示第五行
    private UISprite m_cSpBtn; //内容提示按钮
    private bool m_bHasShow = false;  //加载过showobject

    public const string PATH_DIAMOND_BTN = "btn_zhaomu2";
    public const string PATH_DIAMOND_BTN_2 = "btn_zhaomu22";
    public const string PATH_FRIEND_BTN = "btn_Zhaomu";
    public const string PATH_FRIEND_BTN2 = "btn_Zhaomu1";

    private bool m_bDiamondOrFriend = true; //区分砖石和好友


    public GUISummonDetail(GUIManager mgr)
        : base(mgr, GUI_DEFINE.GUIID_SUMMON_DETAIL, UILAYER.GUI_PANEL)
    {
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();
        base.Destory();
    }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        //this.m_eLoadingState = LOADING_STATE.START;
        //GUI_FUNCTION.AYSNCLOADING_SHOW();

        //if (this.m_cGUIObject == null)
        //{
        //    ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH, RES_MAIN);
        //}

        InitGUI();
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
            //召唤主资源
			this.m_cGUIObject = GameObject.Instantiate(ResourceMgr.LoadAsset(GAME_DEFINE.RESOURCE_GUI_CACHE, RES_MAIN) as UnityEngine.Object) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;
            //返回按钮
            this.m_cBtnCancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_CANCEL);
            GUIComponentEvent gui_event = this.m_cBtnCancel.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnCancel_OnEvent);
            this.m_cPanCancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_CANCEL);
            //除了取消按钮的划出panel
            this.m_cPanInfo = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_INFO);
            //内容提示对象
            this.m_cMsgInfo = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, MSG_IFNO);
            this.m_cMsgTitle = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cMsgInfo, MSG_TITLE);
            this.m_cMsgLine1 = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cMsgInfo, MSG_LINE1);
            this.m_cMsgLine2 = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cMsgInfo, MSG_LINE2);
            this.m_cMsgLine3 = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cMsgInfo, MSG_LINE3);
            this.m_cMsgLine4 = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cMsgInfo, MSG_LINE4);
            this.m_cMsgLine5 = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cMsgInfo, MSG_LINE5);
            this.m_cSpBtn = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cMsgInfo, MSG_BTN);
            this.m_cSpBtn.gameObject.AddComponent<GUIComponentEvent>().AddIntputDelegate(BtnSummon_OnEvent);
        }

        if (m_bDiamondOrFriend)
        {
            //砖石展示title
            this.m_cMsgTitle.text =GAME_FUNCTION.STRING(STRING_DEFINE.SUMMON_DIAMOND_MSG_TITLE);
            this.m_cMsgLine1.text =GAME_FUNCTION.STRING(STRING_DEFINE.SUMMON_DIAMOND_MSG_LINE1);
            this.m_cMsgLine2.text =GAME_FUNCTION.STRING(STRING_DEFINE.SUMMON_DIAMOND_MSG_LINE2);
            this.m_cMsgLine3.text = string.Format(GAME_FUNCTION.STRING(STRING_DEFINE.SUMMON_DIAMOND_MSG_LINE3), GAME_DEFINE.DiamondSummon);

            //砖石招募计算
            int dCount = Role.role.GetBaseProperty().m_iDiamond / GAME_DEFINE.DiamondSummon;       //可以招募数量
            if (dCount == 0)  //不可招募
            {
                this.m_cMsgLine5.text = "#FF0000]水晶不足";
                this.m_cMsgLine4.text = "所持水晶:#FF0000] " + Role.role.GetBaseProperty().m_iDiamond;
                //招募按钮变灰
                //m_cSpBtn.spriteName = PATH_DIAMOND_BTN_2;
            }
            else
            {
                this.m_cMsgLine5.text = "还能招募#FFFC00]" + dCount + "次";
                this.m_cMsgLine4.text = "所持水晶:#FFFC00] " + Role.role.GetBaseProperty().m_iDiamond;
            }
            //招募按钮变亮
            m_cSpBtn.spriteName = PATH_DIAMOND_BTN;
        }
        else
        {
            //友情展示Title
            this.m_cMsgTitle.text = GAME_FUNCTION.STRING(STRING_DEFINE.SUMMON_FIREND_MSG_TITLE);
            this.m_cMsgLine1.text =  GAME_FUNCTION.STRING(STRING_DEFINE.SUMMON_FRIEND_MSG_LINE1);
            this.m_cMsgLine2.text = GAME_FUNCTION.STRING(STRING_DEFINE.SUMMON_FRIEND_MSG_LINE2);
            this.m_cMsgLine3.text = string.Format(GAME_FUNCTION.STRING(STRING_DEFINE.SUMMON_FRIEND_MSG_LINE3), GAME_DEFINE.FriendSummon);

            //友情点招募计算
            int fCount = Role.role.GetBaseProperty().m_iFriendPoint / GAME_DEFINE.FriendSummon;       //可以招募数量
            if (fCount == 0)  //不可招募
            {
                this.m_cMsgLine5.text = "#FF0000]友情点不足";
                this.m_cMsgLine4.text = "所持友情点:#FF0000] " + Role.role.GetBaseProperty().m_iFriendPoint;
                //招募按钮变灰
                //m_cSpBtn.spriteName = PATH_FRIEND_BTN2;
            }
            else
            {
                this.m_cMsgLine5.text = "还能招募#FFFC00]" + fCount + "次";
                this.m_cMsgLine4.text = "所持友情点:#FFFC00] " + Role.role.GetBaseProperty().m_iFriendPoint;

            }
            //招募按钮变亮
            m_cSpBtn.spriteName = PATH_FRIEND_BTN;
        }


        CTween.TweenPosition(this.m_cPanInfo, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(1280, 0, 0), Vector3.one);
        CTween.TweenPosition(this.m_cPanCancel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-430, 270, 0), new Vector3(0, 270, 0));

        this.m_cGUIMgr.SetCurGUIID(this.ID);


        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_CLICK_RECUIT));

        SetLocalPos(Vector3.zero);
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {

        ResourceMgr.UnloadUnusedResources();

        //base.Hiden();

        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        CTween.TweenPosition(this.m_cPanInfo, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(1280, 0, 0));
        CTween.TweenPosition(this.m_cPanCancel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-430, 270, 0),Destory);

    }

    /// <summary>
    /// 立即隐藏
    /// </summary>
    public override void HidenImmediately()
    {
        ResourceMgr.UnloadUnusedResources();

        //SetLocalPos(Vector3.one * 0xFFFF);
        Destory();
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    /// <returns></returns>
    public override bool Update()
    {
        //资源加载等待
        //switch (this.m_eLoadingState)
        //{
        //    case LOADING_STATE.START:
        //        this.m_eLoadingState++;
        //        return false;
        //    case LOADING_STATE.LOADING:
        //        if (ResourceMgr.GetProgress() >= 1f && ResourceMgr.IsComplete())
        //        {
        //            this.m_eLoadingState++;
        //        }
        //        return false;
        //    case LOADING_STATE.END:
        //        InitGUI();
        //        this.m_eLoadingState++;
        //        break;
        //}

        return true;
    }

    /// <summary>
    /// 取消事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnCancel_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.Hiden();

            GUIBackFrameBottom backbottom = (GUIBackFrameBottom)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM);
            backbottom.ShowHalf();
            this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_SUMMON).ShowImmediately();

        }
    }

    /// <summary>
    /// 招募按钮响应
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void BtnSummon_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (m_bDiamondOrFriend)
            {
                if (Role.role.GetHeroProperty().GetAllHero().Count >= Role.role.GetBaseProperty().m_iMaxHeroCount)
                {
                    GUI_FUNCTION.MESSAGEM_(MessageCallBack_HeroMax, GAME_FUNCTION.STRING(STRING_DEFINE.WARNING_MAX_HERO), "btn_expand", "btn_expand1", "btn_hero", "btn_hero1");
                    return;
                }
                //砖石招募数量不够提示，不可招募
                //砖石招募计算
                int dCount = Role.role.GetBaseProperty().m_iDiamond / GAME_DEFINE.DiamondSummon;       //可以招募数量
                if (dCount == 0)  //不可招募
                {
                    GUI_FUNCTION.MESSAGEM_(DiamondBugCallBack, GAME_FUNCTION.STRING(STRING_DEFINE.SUMMON_DIAMOND_MSG_NOT_ENOUGH));
                    return;
                }

                SendAgent.SendMoneySummonPktReq(Role.role.GetBaseProperty().m_iPlayerId);
            }
            else
            {
                if (Role.role.GetHeroProperty().GetAllHero().Count >= Role.role.GetBaseProperty().m_iMaxHeroCount)
                {
                    GUI_FUNCTION.MESSAGEM_(MessageCallBack_HeroMax, GAME_FUNCTION.STRING(STRING_DEFINE.WARNING_MAX_HERO), "btn_hero", "btn_hero1", "btn_expand", "btn_expand1");
                    return;
                }
                //友情招募数量不够提示，不可招募
                //友情点招募计算
                int fCount = Role.role.GetBaseProperty().m_iFriendPoint / GAME_DEFINE.FriendSummon;       //可以招募数量
                if (fCount == 0)  //不可招募
                {
                    GUI_FUNCTION.MESSAGEM(null, GAME_FUNCTION.STRING(STRING_DEFINE.SUMMON_FRIEND_MSG_NOT_ENOUGH));
                    return;
                }

//                SendAgent.SendFriendPointSummonPktReq(Role.role.GetBaseProperty().m_iPlayerId);
            }
        }
    }

    /// <summary>
    /// 英雄超限对话框CallBack
    /// </summary>
    /// <param name="reuslt"></param>
    private void MessageCallBack_HeroMax(bool result)
    {
        if (result)  //扩大单位数量
        {
            this.m_cGUIMgr.HidenCurGUI();

            if (false)
            {
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_UNITSLOTEXPANSION).Show);
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show);
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show);
            }
            else
            {
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_UNITSLOTEXPANSION).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
            }
        }
        else  //出售
        {

            this.m_cGUIMgr.HidenCurGUI();
            if (false)
            {
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERO_MENU).Show);
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show);
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show);
            }
            else
            {
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERO_MENU).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
            }
        }
    }

    /// <summary>
    /// 是否购买水晶
    /// </summary>
    /// <param name="result"></param>
    private void DiamondBugCallBack(bool result)
    {
        if (result)
        {
            this.Hiden();

            GUIGem gem = (GUIGem)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_GEM);
            gem.SetLastGuiId(this.m_iID);
            SendAgent.SendStoreDiamondPrice();
            GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();
        }
    }

    /// <summary>
    /// 设置友情点召唤还是砖石召唤
    /// </summary>
    /// <param name="p"></param>
    public void SetTag(bool p)
    {
        this.m_bDiamondOrFriend = p;
    }
}