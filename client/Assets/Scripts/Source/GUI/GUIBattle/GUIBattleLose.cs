using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Resource;
using UnityEngine;
using Game.Media;

//  GUIBattleLose.cs
//  Author: Lu Zexi
//  2014-02-18




/// <summary>
/// 战斗失败面板
/// </summary>
public class GUIBattleLose : GUIBase
{
    private const string RES_MAIN = "GUI_BattleRevive"; //主资源

    private const string GUI_HERO_NUM = "Lab_HeroNum";     //捕捉英雄数量
    private const string GUI_ITEM_NUM = "Lab_ItemPoint";     //物品数量
    private const string GUI_JINBI_NUM = "Lab_Gold";    //金币数量
    private const string GUI_FARM_NUM = "Lab_FarmNum"; //农场点数量
    private const string GUI_BACK_BTN = "TopPanel/Button_Back"; //返回按钮
    private const string GUI_LIVE_BTN = "Btn_Revive"; //复活按钮
    private const string GUI_CANCEL_BTN = "Btn_Cancel";   //取消按钮
    private const string GUI_PAY_CONTENT = "Lab_Content";  //消费信息
    private const string GUI_OWN_CONTENT = "Lab_CurDiamon";  //拥有信息


    private UILabel m_cPayContent;  //消费内容
    private UILabel m_cOwnContent;  //拥有内容
    private UILabel m_cLabelHeroNum;    //英雄数量
    private UILabel m_cLabelItemNum;    //物品数量
    private UILabel m_cLabelJinbiNum;   //金币数量
    private UILabel m_cLabelFarmNum;    //农场点数量
    private GameObject m_cBtnBack;  //返回按钮
    private GameObject m_cBtnLive;  //复活按钮
    private GameObject m_cBtnCancel;    //取消按钮

    private int m_iHeroNum; //英雄数量
    private int m_iJinbiNum;    //金币数量
    private int m_iItemNum; //物品数量
    private int m_iFarmNum; //农场数量
    private int m_iLiveNum; //复活次数

    private GUIBattle.CALL_BACK m_cBackFun; //返回函数
    private IGUIBattle m_cGUIBattle;    //战斗GUI

    public GUIBattleLose(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_BATTLE_LOSE, UILAYER.GUI_PANEL3)
    { 
        //
    }

    /// <summary>
    /// 清除复活次数
    /// </summary>
    public void CleanReliveNum( )
    {
        this.m_iLiveNum = 1;
    }

    /// <summary>
    /// 设置参数
    /// </summary>
    /// <param name="heroNum"></param>
    /// <param name="itemNum"></param>
    /// <param name="jinbiNum"></param>
    /// <param name="farmNum"></param>
    public void Set(int heroNum, int itemNum, int jinbiNum, int farmNum, GUIBattle.CALL_BACK call , IGUIBattle guibattle)
    {
        this.m_iHeroNum = heroNum;
        this.m_iJinbiNum = jinbiNum;
        this.m_iItemNum = itemNum;
        this.m_iFarmNum = farmNum;
        this.m_cBackFun = call;
        this.m_cGUIBattle = guibattle;
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
    /// 初始化
    /// </summary>
    protected override void InitGUI()
    {
        base.Show();
        GUI_FUNCTION.AYSNCLOADING_HIDEN();
        if (this.m_cGUIObject == null)
        {
            this.m_cGUIObject = GameObject.Instantiate(ResourceMgr.LoadAsset(RES_MAIN) as GameObject) as GameObject;
            this.m_cGUIObject.transform.parent = GUI_FINDATION.FIND_GAME_OBJECT(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;


            this.m_cPayContent = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, GUI_PAY_CONTENT);
            this.m_cOwnContent = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, GUI_OWN_CONTENT);
            this.m_cLabelFarmNum = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, GUI_FARM_NUM);
            this.m_cLabelHeroNum = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, GUI_HERO_NUM);
            this.m_cLabelItemNum = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, GUI_ITEM_NUM);
            this.m_cLabelJinbiNum = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, GUI_JINBI_NUM);


            this.m_cBtnBack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_BACK_BTN);
            GUIComponentEvent ce = this.m_cBtnBack.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnBack);
            this.m_cBtnCancel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_CANCEL_BTN);
            ce = this.m_cBtnCancel.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnCancel);
            this.m_cBtnLive = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_LIVE_BTN);
            ce = this.m_cBtnLive.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnLive);
        }

        this.m_cLabelFarmNum.text = "" + this.m_iFarmNum;
        this.m_cLabelHeroNum.text = "" + this.m_iHeroNum;
        this.m_cLabelItemNum.text = "" + this.m_iItemNum;
        this.m_cLabelJinbiNum.text = "" + this.m_iJinbiNum;
        this.m_cPayContent.text = "花费: " + GAME_DEFINE.DiamondRelive + "钻石";
        this.m_cOwnContent.text = "拥有: " + Role.role.GetBaseProperty().m_iDiamond+"钻石";

        SetLocalPos(Vector3.zero);
    }


    /// <summary>
    /// 更新
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

        //SetLocalPos(Vector3.one*0xFFFFF);

        Destory();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        this.m_cPayContent = null;
        this.m_cOwnContent = null;
        this.m_cLabelHeroNum = null;
        this.m_cLabelItemNum = null;
        this.m_cLabelJinbiNum = null;
        this.m_cLabelFarmNum = null;
        this.m_cBtnBack = null;
        this.m_cBtnLive = null;
        this.m_cBtnCancel = null;

        base.Destory();
    }

    /// <summary>
    /// 返回按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnBack(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();
            this.m_cBackFun();
        }
    }

    /// <summary>
    /// 复活按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnLive(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (GAME_DEFINE.DiamondRelive > Role.role.GetBaseProperty().m_iDiamond)
            //if (20 > Role.role.GetBaseProperty().m_iDiamond)
            {
                GUI_FUNCTION.MESSAGEL(null, "您的钻石不足");
                return;
            }

            SendAgent.SendBattleRelive(Role.role.GetBaseProperty().m_iPlayerId, this.m_iLiveNum);
            this.m_iLiveNum++;
            //if (this.m_iLiveNum > 5)
            //    this.m_iLiveNum = 5;
        }
    }

    /// <summary>
    /// 复活接口
    /// </summary>
    public void Relive()
    {
        for (int i = 0; i < this.m_cGUIBattle.GetVecSelf().Length; i++)
        {
            BattleHero hero = this.m_cGUIBattle.GetVecSelf()[i];

            if (hero != null)
            {
                hero.m_iHp = (int)hero.m_cMaxHP.GetFinalData();
                hero.m_bDead = false;
                hero.ClearBUF();
                hero.GetGfxObject().SetLocalScale(Vector3.one);
                this.m_cGUIBattle.SetUIHeroHP(hero);
                hero.m_fBBHP = hero.m_iBBMaxHP;
                this.m_cGUIBattle.SetUIHeroBBHP(hero);
            }
        }
		MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_BATTLE_RELIVE);
//        MediaMgr.PlaySound(SOUND_DEFINE.SE_BATTLE_RELIVE);

        this.m_cGUIBattle.SetBattleState((int)GUIBattle.BATTLE_STATE.BATTLE_STATE_SELF_ATTACK_BEGIN);
    }

    /// <summary>
    /// 取消按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnCancel(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            GUI_FUNCTION.MESSAGEL_(GiveUpCallBack, "如果放弃当前任务，本次任务中获得的#ED4312]角色、素材、金币、元气点#FFFFFF]都将消失，你确定放弃吗？");
        }
    }

    /// <summary>
    /// 消息框回调
    /// </summary>
    /// <param name="yes"></param>
    private void GiveUpCallBack(bool yes)
    {
        if (yes)
        {
            Hiden();
            this.m_cBackFun();
        }
    }
}
