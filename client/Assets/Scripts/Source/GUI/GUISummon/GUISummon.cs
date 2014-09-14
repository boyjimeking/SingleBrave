//Micro.Sanvey
//2013.11.12
//sanvey.china@gmail.com

using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Resource;
using Game.Gfx;
using Game.Base;
using Game.Media;

/// <summary>
/// 召唤GUI类
/// </summary>
public class GUISummon : GUIBase
{
    private const string RES_MAIN = "GUI_HeroSummon";             //召唤主资源地址
    private const string BTN_DIAMONDCALL = "PanInfo/SlidePanel/Diamond";   //砖石召唤地址
    private const string BTN_FRIENDCALL = "PanInfo/SlidePanel/Friend";     //友情召唤地址
    private const string SPR_ARROWLEFT = "PanInfo/ArrowLeft";       //向左滑动特效
    private const string SPR_ARROWRIGHT = "PanInfo/ArrowRight";     //向右滑动特效
    private const string BTN_CANCEL = "Title_Cancel/Btn_Cancel";    //返回按钮地址
    private const string PAN_CANCEL = "Title_Cancel";                 //返回上一层Pan地址
    private const string LB_INFO = "PanInfo/ListItemBg/Lb_Info";     //召唤信息地址
    private const string PAN_INFO = "PanInfo";                        //除了取消按钮的划出panel
    private const string SP_BADGE = "Badge";   //提示显示框
    private const string LB_COUNT = "Count";  //提示显示字符
    private const string SP_BTN = "Btn";           //招募按钮

    private GameObject m_cBtnCancel;       //取消按钮
    private GameObject m_cPanCancel;            //取消Pan
    private GameObject m_cBtnDiamondCall;  //砖石召唤按钮
    private GameObject m_cBtnFriendCall;   //友情召唤按钮
    private UISprite m_cSprArrowLeft;      //向左滑动特效
    private UISprite m_cSprArrowRight;     //向右滑动特效
    private UILabel m_cLb_Info;            //召唤信息显示字符
    private GameObject m_cPanInfo;  //除了取消按钮的划出panel
    private TDAnimation m_cEffectLeft;   //特效类
    private TDAnimation m_cEffectRight;  //特效类
    private UISprite m_cSpFriendSummonBtn; // 好友招募按钮
    private UISprite m_cSpDiamondSummonBtn; //砖石招募按钮
    private UISprite m_cSpFriendSummonBadge; //好友招募提示框
    private UISprite m_cSpDiamondSummonBadge; //砖石招募提示框
    private UILabel m_cLbDiamondSummonCount; //砖石招募字符
    private UILabel m_cLbFriendSummonCount;  //好友招募字符

    private List<GameObject> m_lstSlideItemList = new List<GameObject>();  //滑动对象列表

    private bool m_bIsDraging = false;    //是否滑动中
    private bool m_bIsRight = false;      //向右拖动
    private float m_fDragDistance = 0f;   //累计滑动距离
    private bool m_bIsMoving = false;     //正在ITween动画中
    private bool m_bHasShow = false;  //加载过showobject
    private Hero m_iReusltHero;   //召唤接口返回的英雄ID
    public bool m_bDiamondOrFriend = true; //区分砖石和好友

    public const string PATH_DIAMOND_BTN = "btn_zhaomu2";
    public const string PATH_DIAMOND_BTN_2 = "btn_zhaomu22";
    public const string PATH_FRIEND_BTN = "btn_Zhaomu";
    public const string PATH_FRIEND_BTN2 = "btn_Zhaomu1";

    //稀有召唤

    public GUISummon(GUIManager mgr)
        : base(mgr, GUI_DEFINE.GUIID_SUMMON, GUILAYER.GUI_PANEL)
    {
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        m_lstSlideItemList.Clear();

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
    /// 立即展示
    /// </summary>
    public override void ShowImmediately()
    {
        base.ShowImmediately();


        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Show();
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();

        Show();

    }

    /// <summary>
    /// 立即隐藏
    /// </summary>
    public override void HidenImmediately()
    {
        //base.HidenImmediately();

        ResourceMgr.UnloadUnusedResources();

        SetLocalPos(Vector3.one * 0xFFFF);

        Destory();
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
            //砖石召唤按钮
            this.m_cBtnDiamondCall = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_DIAMONDCALL);
            GUIComponentEvent gui_event = this.m_cBtnDiamondCall.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnDiamondCall_OnEvent);
            //友情召唤按钮
            this.m_cBtnFriendCall = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_FRIENDCALL);
            gui_event = this.m_cBtnFriendCall.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnFriendCall_OnEvent);
            //砖石内容
            this.m_cSpDiamondSummonBadge = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cBtnDiamondCall, SP_BADGE);
            this.m_cSpDiamondSummonBtn = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cBtnDiamondCall, SP_BTN);
            this.m_cLbDiamondSummonCount = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cBtnDiamondCall, LB_COUNT);
            //好友内容
            this.m_cSpFriendSummonBadge = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cBtnFriendCall, SP_BADGE);
            this.m_cSpFriendSummonBtn = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cBtnFriendCall, SP_BTN);
            this.m_cLbFriendSummonCount = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cBtnFriendCall, LB_COUNT);
            //返回按钮
            this.m_cBtnCancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_CANCEL);
            gui_event = this.m_cBtnCancel.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnCancel_OnEvent);
            this.m_cPanCancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_CANCEL);
            //左右导航
            this.m_cSprArrowLeft = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SPR_ARROWLEFT);
            this.m_cEffectLeft = new TDAnimation(m_cSprArrowLeft.atlas, m_cSprArrowLeft); //左右导航
            this.m_cSprArrowRight = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SPR_ARROWRIGHT);
            this.m_cEffectRight = new TDAnimation(m_cSprArrowRight.atlas, m_cSprArrowRight);
            gui_event = this.m_cSprArrowLeft.gameObject.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnLeft_OnEvent);
            gui_event = this.m_cSprArrowRight.gameObject.AddComponent<GUIComponentEvent>();
            gui_event.AddIntputDelegate(BtnRight_OnEvent);
            //召唤信息显示字符
            this.m_cLb_Info = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LB_INFO);
            //除了取消按钮的划出panel
            this.m_cPanInfo = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, PAN_INFO);

            m_lstSlideItemList.Add(m_cBtnDiamondCall);
            m_lstSlideItemList.Add(m_cBtnFriendCall);

            //初始化数据
            m_bIsDraging = false;    //是否滑动中
            m_bIsRight = false;      //向右拖动
            m_fDragDistance = 0f;   //累计滑动距离
            m_bIsMoving = false;     //正在ITween动画中
        }

        SetSummonData();

        //友情召唤回来以后，初始还是友情
        if (!m_bDiamondOrFriend)
        {
            if (this.m_cBtnDiamondCall != null)
            {

                this.m_cBtnDiamondCall.transform.localPosition = new Vector3(640, 0, 0);
            }
            if (this.m_cBtnFriendCall != null)
            {
                this.m_cBtnFriendCall.transform.localPosition = new Vector3(0, 0, 0);
            }

        }

        SetSummonInfo();

        //播放主背景音乐
        MediaMgr.sInstance.PlayBGM(SOUND_DEFINE.BGM_MAIN);

        this.m_cEffectLeft.Play("ArrowLeft", Game.Base.TDAnimationMode.Loop, 0.4F);
        this.m_cEffectRight.Play("ArrowRight", Game.Base.TDAnimationMode.Loop, 0.4F);

        CTween.TweenPosition(this.m_cPanInfo, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(1280, 0, 0), Vector3.one);
        CTween.TweenPosition(this.m_cPanCancel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-430, 270, 0), new Vector3(0, 270, 0));

        this.m_cGUIMgr.SetCurGUIID(this.ID);

        SetLocalPos(Vector3.zero);

        //设置Panel在top框下面 , 避免人物大图超出 ， 遮挡顶部
        this.m_cGUIObject.GetComponent<UIPanel>().depth = 201;
        GUIBackFrameBottom bottom = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        bottom.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_RECRUIT_HERO));
        bottom.SetPanelDepth(202);

        //新手引导
        GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_SUMMON2);
    }

    /// <summary>
    /// 设置可以招募的提示
    /// </summary>
    private void SetSummonData()
    {
        //砖石招募计算
        int dCount = Role.role.GetBaseProperty().m_iDiamond / GAME_DEFINE.DiamondSummon;       //可以招募数量

        if (dCount>99)
        {
            dCount = 99;
        }
        if (dCount == 0)  //不可招募
        {
            //隐藏招募数量提示
            m_cSpDiamondSummonBadge.enabled = false;
            m_cLbDiamondSummonCount.enabled = false;
            //招募按钮变灰
            //m_cSpDiamondSummonBtn.spriteName = PATH_DIAMOND_BTN_2;
        }
        else  //提示招募数量
        {
            //隐藏招募数量提示
            m_cSpDiamondSummonBadge.enabled = true;
            m_cLbDiamondSummonCount.enabled = true;
            m_cLbDiamondSummonCount.text = dCount.ToString();
            //招募按钮变亮
            m_cSpDiamondSummonBtn.spriteName = PATH_DIAMOND_BTN;
        }

        //友情点招募计算
        int fCount = Role.role.GetBaseProperty().m_iFriendPoint / GAME_DEFINE.FriendSummon;       //可以招募数量
        if (fCount>99)
        {
            fCount = 99;
        }
        if (fCount == 0)  //不可招募
        {
            //隐藏招募数量提示
            m_cSpFriendSummonBadge.enabled = false;
            m_cLbFriendSummonCount.enabled = false;
            //招募按钮变灰
            //m_cSpFriendSummonBtn.spriteName = PATH_FRIEND_BTN2;
        }
        else  //提示招募数量
        {
            //隐藏招募数量提示
            m_cSpFriendSummonBadge.enabled = true;
            m_cLbFriendSummonCount.enabled = true;
            m_cLbFriendSummonCount.text = fCount.ToString();
            //招募按钮变亮
            m_cSpFriendSummonBtn.spriteName = PATH_FRIEND_BTN;
        }
    }

    /// <summary>
    /// 设置召唤提示信息
    /// </summary>
    private void SetSummonInfo()
    {
        if (m_cBtnDiamondCall==null)
        {
            return;
        }
        if (m_cLb_Info==null)
        {
            return;
        }
        if (this.m_cBtnDiamondCall.transform.localPosition.x >= 0 && this.m_cBtnDiamondCall.transform.localPosition.x < 640)
        {
            this.m_cLb_Info.text = string.Format(GAME_FUNCTION.STRING(STRING_DEFINE.SUMMON_DIAMOND_INFO) , GAME_DEFINE.DiamondSummon);
        }
        else
        {
            this.m_cLb_Info.text = string.Format(GAME_FUNCTION.STRING(STRING_DEFINE.SUMMON_FRIEND_INFO), GAME_DEFINE.FriendSummon);
        }
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        ResourceMgr.UnloadUnusedResources();

        m_bDiamondOrFriend = true;

        //base.Hiden();
        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

       // CTween.TweenPosition(this.m_cPanInfo, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(1280, 0, 0));
       // CTween.TweenPosition(this.m_cPanCancel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-430, 270, 0),Destory);

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

        if (!IsShow()) return false;

        //左右闪烁
        if (this.m_cEffectLeft != null)
        {
            this.m_cEffectLeft.Update();
        }
        if (this.m_cEffectRight != null)
        {
            this.m_cEffectRight.Update();
        }

        ////更新label信息透明度，当该项远离时，该项目的显示描述信息渐渐隐藏
        //m_lstSlideItemList.ForEach(item =>
        //{
        //    //到达边界时，显示半透明 
        //    var scaleSize = (1 - 0.0015625F * (Mathf.Abs(item.transform.localPosition.x)));
        //    this.m_cLb_Info.alpha = scaleSize;

        //});

        return true;
    }

    /// <summary>
    /// 金币召唤点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnDiamondCall_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.Hiden();

            GUISummonDetail sumDetail = m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_SUMMON_DETAIL) as GUISummonDetail;
            sumDetail.SetTag(true);
            sumDetail.Show();
        }
        else
        {
            WinItem_OnEvent(info, args);
        }
    }

    /// <summary>
    /// 友情点召唤点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void BtnFriendCall_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.Hiden();

            GUISummonDetail sumDetail = m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_SUMMON_DETAIL) as GUISummonDetail;
            sumDetail.SetTag(false);
            sumDetail.Show();
        }
        else
        {
            WinItem_OnEvent(info, args);
        }
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
            GUIMain guimain = (GUIMain)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_MAIN);
            guimain.Show();

        }
    }

    /// <summary>
    /// Drag处理
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void WinItem_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (m_bIsMoving)
        {
            return;
        }
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.DRAG)
        {

            m_bIsDraging = true;
            m_bIsRight = (info.m_vecDelta.x > 0);
            m_fDragDistance += info.m_vecDelta.x;
            ChangePosion(m_bIsRight);

            if (m_fDragDistance > 640)
            {
                m_fDragDistance = 640;
                return;
            }

            m_lstSlideItemList.ForEach(item =>
            {
                //跟随位移
                item.transform.localPosition += new Vector3(info.m_vecDelta.x, 0, 0);
            });
        }
        else if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.PRESS)  //drag 结束
        {
            if (m_bIsDraging)  //drag 结束事件只执行一次
            {
                m_bIsMoving = true;
                m_lstSlideItemList.ForEach(item =>
                {
                    var yu = item.transform.localPosition.x % 640;
                    var distance = yu > 0 ? yu : 640 - Math.Abs(yu);

                    if (m_bIsRight)  //滑动超过30单位 就算有效滑动到下一项，否则回到原始位置
                    {
                        distance = distance > 30 ? item.transform.localPosition.x + 640 - distance : item.transform.localPosition.x - distance;
                    }
                    else
                    {
                        distance = 640 - distance > 30 ? item.transform.localPosition.x - distance : item.transform.localPosition.x + 640 - distance;
                    }

                    distance = distance > 640 ? distance % 640 : distance;
                    distance = distance < -640 ? distance % 640 : distance;

                    CTween.TweenPosition(item, 0, 0.3F, item.transform.localPosition, new Vector3(distance, 0F, 0F), TWEEN_LINE_TYPE.Line, MoveFinish);  //结束剩余动画
                });

                m_fDragDistance = 0f;
                m_bIsDraging = false;
            }
        }
    }

    /// <summary>
    /// 向左滑动按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void BtnLeft_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (m_bIsMoving)
        {
            return;
        }
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (!m_bIsDraging)  // 没有在Drag
            {
                m_bIsMoving = true;
                ChangePosionByBtn(false);
                m_lstSlideItemList.ForEach(item =>
                {
                    CTween.TweenPosition(item, 0, 0.3F, item.transform.localPosition, item.transform.localPosition - new Vector3(640, 0F, 0F), TWEEN_LINE_TYPE.Line, MoveFinish);  //结束剩余动画
                });
            }
        }
    }

    /// <summary>
    /// 向右滑动按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void BtnRight_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (m_bIsMoving)
        {
            return;
        }
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (!m_bIsDraging)  // 没有在Drag
            {
                m_bIsMoving = true;
                ChangePosionByBtn(true);
                m_lstSlideItemList.ForEach(item =>
                {
                    CTween.TweenPosition(item, 0, 0.3f, item.transform.localPosition, item.transform.localPosition + new Vector3(640, 0F, 0F), TWEEN_LINE_TYPE.Line, MoveFinish);  //结束剩余动画
                });
            }
        }
    }

    /// <summary>
    /// 检查位置，如果向右边滑，而右边又没有多的项，则将列表最后面一项移动过来顶替
    /// </summary>
    /// <param name="isRight"></param>
    private void ChangePosion(bool isRight)
    {
        var outItem = m_lstSlideItemList.Find(new Predicate<GameObject>(item =>
        {
            return item.transform.localPosition.x < -640 || item.transform.localPosition.x > 640;
        }));
        if (outItem != null)  //向左滑动时，将最左边项目变换到最右边项目
        {
            outItem.transform.localPosition += new Vector3(!isRight ? 1280 : -1280, 0, 0);
        }
    }

    /// <summary>
    /// 检查位置，如果向右边滑，而右边又没有多的项，则将列表最后面一项移动过来顶替
    /// </summary>
    /// <param name="isRight"></param>
    private void ChangePosionByBtn(bool isRight)
    {
        if (isRight)
        {
            var leftItem = m_lstSlideItemList.Find(new Predicate<GameObject>(item =>
            {
                return item.transform.localPosition.x > 0;
            }));
            if (leftItem != null)
            {
                leftItem.transform.localPosition -= new Vector3(1280, 0, 0);
            }
        }
        else
        {
            var rightItem = m_lstSlideItemList.Find(new Predicate<GameObject>(item =>
            {
                return item.transform.localPosition.x < 0;
            }));
            if (rightItem != null)
            {
                rightItem.transform.localPosition += new Vector3(1280, 0, 0);
            }
        }
    }

    /// <summary>
    /// 滑动结束
    /// </summary>
    private void MoveFinish()
    {
        m_bIsMoving = false;

        SetSummonInfo();
    }

}