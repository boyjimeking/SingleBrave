using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using Game.Resource;

//登陆奖励
//Author sunyi
//2014-03-6
public class GUILoginReward : GUIBase
{
    private const string RES_MAIN = "GUI_LoginReward";//主资源地址
    private const string RES_ITEM = "GUI_LoginRewardItem";//奖励项资源地址

    private const string ITEM_PARENT = "MainPanel/RewardItems";//奖励项父对象

    private const string SPR_BLACK = "Spr_Black";//黑色遮罩
    private const string SPR_FRAME = "Spr_Frame";//黑色遮罩
    private const string SPR_RECEIVE = "Spr_Receive";//黑色遮罩
    private const string SPR_ICON = "Spr_Icon";//图标地址
    private const string LABEL_COUNT = "Lab_Count";//数量标签地址

    //private bool m_bIsShowed;//判断是否当前显示

    private UnityEngine.Object m_cItem;//奖励项
    private GameObject m_cItemParent;//父对象
    private List<GameObject> m_lstRewardItems = new List<GameObject>();//列表

    public GUILoginReward(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_LOGINREWARD, GUILAYER.GUI_MESSAGE)
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
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_ITEM);
        }
        else
        {
            InitGUI();
        }
    }

    /// <summary>
    /// 初始化
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

            this.m_cItemParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, ITEM_PARENT);
            this.m_cItem = (UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_ITEM);

            GUIComponentEvent clickEvent = this.m_cGUIObject.AddComponent<GUIComponentEvent>();
            clickEvent.AddIntputDelegate(ClickEvent);
        }

        GAME_SETTING.SaveLastLoginDays(Role.role.GetBaseProperty().m_iLoginTimes);

        SetRewardItem();

        SetLocalPos(Vector3.zero);
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        base.Hiden();

        ResourcesManager.GetInstance().UnloadUnusedResources();

        //SetLocalPos(Vector3.one * 0xFFFF);
        Destory();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        this.m_cItem = null;
        this.m_cItemParent = null;
        this.m_lstRewardItems.Clear();

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
    /// 设置奖励项
    /// </summary>
    private void SetRewardItem()
    {
        List<LoginRewardTable> lstReardTable = LoginRewardTableManager.GetInstance().GetAll();

        for (int i = 0; i < lstReardTable.Count; i++)
        {
            GameObject rewardItem = GameObject.Instantiate(this.m_cItem) as GameObject;
            rewardItem.transform.parent = this.m_cItemParent.transform;
            rewardItem.transform.localScale = Vector3.one;

            int k = 0;
            if (i >= 0 && i < 5)
                k = 0;
            else if (i >= 5 && i < 10)
                k = 1;
            else if (i >= 10 && i < 15)
                k = 2;
            else if (i >= 15 && i < 20)
                k = 3;
            else if (i >= 20 && i < 25)
                k = 4;

            rewardItem.transform.localPosition = new Vector3(-215 + 108 * (i - k * 5), 200 - k * 103, 0);

            UISprite sprFrame = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(rewardItem, SPR_FRAME);
            UISprite sprReceive = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(rewardItem, SPR_RECEIVE);
            UISprite sprBlack = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(rewardItem, SPR_BLACK);
            UISprite sprIcon = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(rewardItem, SPR_ICON);
            UILabel labCount = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(rewardItem, LABEL_COUNT);

            LoginRewardTable rewardTable = lstReardTable[i];

            switch (rewardTable.RewardType)
            { 
                case GiftType.Diamond:
                    sprIcon.spriteName = "diamond88";
                    break;
                case GiftType.Gold:
                    sprIcon.spriteName = "gold88";
                    break;
                case GiftType.FriendPoint:
                    sprIcon.spriteName = "friendpoint88";
                    break;
                case GiftType.FarmPoint:
                    sprIcon.spriteName = "yuanqi88";
                    break;
                case GiftType.Hero:
                    HeroTable hero = HeroTableManager.GetInstance().GetHeroTable(rewardTable.RewardId);
                    if (hero != null)
                    {
                        GUI_FUNCTION.SET_AVATORS(sprIcon, hero.AvatorMRes);
                    }
                    break;
                case GiftType.Item:
                    ItemTable item = ItemTableManager.GetInstance().GetItem(rewardTable.RewardId);
                    if (item != null)
                    {
                        GUI_FUNCTION.SET_ITEMM(sprIcon, item.SpiritName);
                    }
                    break;
                default:
                    break;
            }

            if (i == Role.role.GetBaseProperty().m_iLoginTimes - 1)
            {
                sprReceive.enabled = false;
                sprBlack.enabled = false;
            }
            else if (i < Role.role.GetBaseProperty().m_iLoginTimes - 1)
            {
                sprFrame.enabled = false;
            }
            else if (i > Role.role.GetBaseProperty().m_iLoginTimes - 1)
            {
                sprReceive.enabled = false;
                sprBlack.enabled = false;
                sprFrame.enabled = false;
            }

            labCount.text = "x" + rewardTable.Count;

            this.m_lstRewardItems.Add(rewardItem);
        }
    }

    ///// <summary>
    ///// 设置是否当前显示
    ///// </summary>
    ///// <param name="showed"></param>
    //public void SetIsShow(bool showed)
    //{
    //    this.m_bIsShowed = showed;
    //}

    /// <summary>
    /// 点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void ClickEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();
        }
    }
}

