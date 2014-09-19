using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using Game.Resource;

//英雄详细页面
//Author:sunyi
//2013-11-28
public class GUIHeroDetail : GUIBase
{
    public delegate void CALLBACK();    //回调委托

    private const string RES_MAIN = "GUI_HeroDetail";//主资源地址

    private const string BUTTON_BACK = "TopPanel/Button_Back";//返回按钮地址
    private const string LABEL_NAME = "TopPanel/Label_Name";//英雄名称地址
    private const string SP_STAR1 = "TopPanel/Sprite_Star1";  //星级1
    private const string SP_STAR2 = "TopPanel/Sprite_Star2";  //星级2
    private const string SP_STAR3 = "TopPanel/Sprite_Star3";  //星级3
    private const string SP_STAR4 = "TopPanel/Sprite_Star4";  //星级4
    private const string SP_STAR5 = "TopPanel/Sprite_Star5";  //星级5
    private const string SP_LIKE = "TopPanel/Sprite_Like";        //喜欢
    private const string LB_UNIT = "TopPanel/Label_Unit";  //英雄tableid编号
    private const string SP_NATURE = "TopPanel/Sprite_Icon";  //英雄属性

    private const string HELP_BOARD = "HeroTypeDescPanel"; //帮助按钮地址
    private const string BUTTON_LOCK = "Btn_Lock";//Lock按钮地址
    private const string BUTTON_ATLAS = "Btn_Atals";//图鉴按钮地址
    private const string ITEM_TYPE = "Item_Type/Label_TypeContent";//类型标签地址
    private const string ITEM_HELP = "Item_Type/Btn_Help";//帮助键地址
    private const string ITEM_MAXLV = "Item_LV/Label_LVMaxContent";//最大等级标签地址
    private const string ITEM_CURRENTLV = "Item_LV/Label_LVCurContent";//当前等级标签地址
    private const string ITEM_LVUP = "Item_LvUp/Label_LvUpContent";//升级标签地址
    private const string ITEM_LVEXP = "Item_LvEXP/Sprite_SlideContent";//升级经验条地址
    private const string ITEM_HP = "Item_HP/Label_HPContent";//血量标签地址
    private const string ITEM_ATTACK = "Item_Attack/Label_AttackContent";//攻击标签地址
    private const string ITEM_DEFENSE = "Item_Defense/Label_DefenseContent";//防御标签地址
    private const string ITEM_RECOVER = "Item_Recover/Label_RecoverContent";//恢复标签地址
    private const string ITEM_EQUIPMENT = "Item_Equipment/Label_EquipmentContent";//装备标签地址
    private const string ITEM_ICON = "Item_Equipment/Sprite_Icon";
    private const string ITEM_BORDER = "Item_Equipment/Sprite_Edge";
    private const string ITEM_EQUIPMENT_ALL = "Item_Equipment";//装备标签地址
    private const string ITEM_COST = "Item_Cost/Label_CostContent";//恢复标签地址
    private const string TEXTURE_HERO = "Tex_Hero";//英雄全身头像地址
    private const string ITEM_LeaderSkillName = "Footer/Label_LeaderJiNeng";
    private const string ITEM_LeaderSkillInfo = "Footer/Label_LeaderJiNengDesc";
    private const string ITEM_BBName = "Footer/Label_BBJiNeng";
    private const string ITEM_BBInfo = "Footer/Label_BBJiNengDesc";
    private const string ITEM_BBLv = "Footer/Lab_BBLv";
    private const string Collider = "Collider";   //整个节目Collider遮罩

    private const string LABEL_BALANCE = "HeroTypeDescPanel/Lab_Balance"; //平衡型 标签地址
    private const string LABEL_BALANCE_DETAIL = "HeroTypeDescPanel/Lab_Balance_Detail"; //平衡型 描述文字地址
    private const string LABEL_ATTACK = "HeroTypeDescPanel/Lab_Attack"; //攻击型 标签地址
    private const string LABEL_ATTACK_DETAIL = "HeroTypeDescPanel/Lab_Attack_Detail"; //攻击型 描述文字地址
    private const string LABEL_DEFENCE = "HeroTypeDescPanel/Lab_Defence"; //防御型 标签地址
    private const string LABEL_DEFENCE_DETAIL = "HeroTypeDescPanel/Lab_Defence_Detail"; //防御型 描述文字地址
    private const string LABEL_HP = "HeroTypeDescPanel/Lab_HP"; //生命型 标签地址
    private const string LABEL_HP_DETAIL = "HeroTypeDescPanel/Lab_HP_Detail"; //生命型 描述文字地址
    private const string LABEL_RECOVER = "HeroTypeDescPanel/Lab_Recover"; //回复型 标签地址
    private const string LABEL_RECOVER_DETAIL = "HeroTypeDescPanel/Lab_Recover_Detail"; //回复型 描述文字地址
    private const string BUTTON_COMMIT = "HeroTypeDescPanel/Button_Back"; //确认键地址

    private const string BTN_LOCK = "btn_lock";
    private const string BTN_UNLOCK = "btn_unlock";

    private UISprite m_cSpNature;  //英雄属性
    private UILabel m_cLbHeroTableID;  //英雄表id
    private UILabel m_cNameLabel;//英雄名称标签
    private UILabel m_cTypeLabel;//类型标签
    private UILabel m_cMaxLvLabel;//最大等级标签
    private UILabel m_cCurLvLabel;//当前等级标签
    private UILabel m_cLvUpLabel;//升级标签
    private UILabel m_cHpLabel;//血量标签
    private UILabel m_cAttackLabel;//攻击力标签
    private UILabel m_cDefenseLabel;//防御标签
    private UILabel m_cRecoverLabel;//恢复标签
    private UILabel m_cEquipmentLabel;//装备标签
    private UISprite m_cSpItem;
    private UISprite m_cSpItemBorder;
    private UILabel m_cCostLabel;//领导力标签
    private UITexture m_cTexHero;//英雄全身贴图
    private UISprite m_cSprHeroExpSlide;//经验条
    private UILabel m_cLBLeaderSkillName;
    private UILabel m_cLbLeaderSkillInfo;
    private UILabel m_cLBBBName;
    private UILabel m_cLBBBInfo;
    private UILabel m_cLBBBLv;
    private UILabel m_cLabBalance;//平衡型
    private UILabel m_cLabBalanceDetail;//平衡型描述
    private UILabel m_cLabAttck;//攻击型
    private UILabel m_cLabAttckDetail;//攻击型描述
    private UILabel m_cLabHP;//生命型
    private UILabel m_cLabHPDetail;//生命型描述
    private UILabel m_cLabRecover;//回复型
    private UILabel m_cLabRecoverDetail;//回复型描述
    private UILabel m_cLabDefence;//防御型
    private UILabel m_cLabDefenceDetail;//防御型描述
    private UISprite m_cSpStar1;
    private UISprite m_cSpStar2;
    private UISprite m_cSpStar3;
    private UISprite m_cSpStar4;
    private UISprite m_cSpStar5;
    private UISprite m_cSpLike;
    private GameObject m_cCollider;   //整个节目Collider遮罩

    private Hero m_cCurHero;

    private GameObject m_cBtnBack;//返回按钮
    private GameObject m_cItemEquipment;//装备项
    private GameObject m_cBtnLock; //锁定
    private GameObject m_cBtnAltas;//图鉴
    private GameObject m_cBtnCommit;//确认
    private GameObject m_cBtnHelp;//帮助
    private GameObject m_cHelpBoard;//帮助面板

    private UIImageButton m_cImageBtnLock;//锁定键image组件

    private CALLBACK m_delCallBack; //回调方法


    //选中的数据
    public List<int> lstSelectHero = new List<int>();  //选中准备出售的英雄列表
    private bool m_bOpened; //记录是否打开过帮助界面
    public Vector3 m_cLocalposition; //记录panel位置
    public Vector4 m_cClipRange;//记录clipRange


    public GUIHeroDetail(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_HERODETAIL, UILAYER.GUI_PANEL)
    { }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {

        this.m_eLoadingState = LOADING_STATE.NONE;

        ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_TEX_PATH + this.m_cCurHero.m_strAvatarA);

        if (this.m_cGUIObject == null)
        {
            this.m_eLoadingState = LOADING_STATE.START;
            GUI_FUNCTION.AYSNCLOADING_SHOW();
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_MAIN);
        }
        else
        {
            this.m_eLoadingState = LOADING_STATE.START;
            GUI_FUNCTION.AYSNCLOADING_SHOW();
            //InitGUI();
        }
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {

        ResourceMgr.UnloadResource(this.m_cCurHero.m_strAvatarA);
        ResourceMgr.UnloadResource(RES_MAIN);

        //base.Hiden();

        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Show();

        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        CTween.TweenAlpha(this.m_cGUIObject, 0, GAME_DEFINE.FADEOUT_GUI_TIME, 1f, 0f , Destory);
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {

        m_cSpNature = null;  //英雄属性
        m_cLbHeroTableID = null;  //英雄表id
        m_cNameLabel = null;//英雄名称标签
        m_cTypeLabel = null;//类型标签
        m_cMaxLvLabel = null;//最大等级标签
        m_cCurLvLabel = null;//当前等级标签
        m_cLvUpLabel = null;//升级标签
        m_cHpLabel = null;//血量标签
        m_cAttackLabel = null;//攻击力标签
        m_cDefenseLabel = null;//防御标签
        m_cRecoverLabel = null;//恢复标签
        m_cEquipmentLabel = null;//装备标签
        m_cSpItem = null;
        m_cSpItemBorder = null;
        m_cCostLabel = null;//领导力标签
        m_cTexHero = null;//英雄全身贴图
        m_cSprHeroExpSlide = null;//经验条
        m_cLBLeaderSkillName = null;
        m_cLbLeaderSkillInfo = null;
        m_cLBBBName = null;
        m_cLBBBInfo = null;
        m_cLBBBLv = null;
        m_cLabBalance = null;//平衡型
        m_cLabBalanceDetail = null;//平衡型描述
        m_cLabAttck = null;//攻击型
        m_cLabAttckDetail = null;//攻击型描述
        m_cLabHP = null;//生命型
        m_cLabHPDetail = null;//生命型描述
        m_cLabRecover = null;//回复型
        m_cLabRecoverDetail = null;//回复型描述
        m_cLabDefence = null;//防御型
        m_cLabDefenceDetail = null;//防御型描述
        m_cSpStar1 = null;
        m_cSpStar2 = null;
        m_cSpStar3 = null;
        m_cSpStar4 = null;
        m_cSpStar5 = null;
        m_cSpLike = null;
        m_cCollider = null;   //整个节目Collider遮罩

        m_cBtnBack = null;//返回按钮
        m_cItemEquipment = null;//装备项
        m_cBtnLock = null; //锁定
        m_cBtnAltas = null;//图鉴
        m_cBtnCommit = null;//确认
        m_cBtnHelp = null;//帮助
        m_cHelpBoard = null;//帮助面板

        m_cImageBtnLock = null;//锁定键image组件



        base.Destory();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    protected override void InitGUI()
    {
        GUI_FUNCTION.AYSNCLOADING_HIDEN();

        base.Show();

        if (this.m_cGUIObject == null)
        {
            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            this.m_cBtnBack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_BACK);
            GUIComponentEvent backEvent = this.m_cBtnBack.AddComponent<GUIComponentEvent>();
            backEvent.AddIntputDelegate(OnClickBackButton);

            this.m_cBtnLock = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_LOCK);
            this.m_cBtnLock.AddComponent<GUIComponentEvent>().AddIntputDelegate(HeroLockClick);
            this.m_cImageBtnLock = this.m_cBtnLock.GetComponent<UIImageButton>();

            this.m_cBtnAltas = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_ATLAS);
            this.m_cBtnAltas.AddComponent<GUIComponentEvent>().AddIntputDelegate(HeroAtlasClick);

            this.m_cBtnCommit = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject,BUTTON_COMMIT);
            this.m_cBtnCommit.AddComponent<GUIComponentEvent>().AddIntputDelegate(BackToDetail);

            this.m_cBtnHelp = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, ITEM_HELP);
            this.m_cBtnHelp.AddComponent<GUIComponentEvent>().AddIntputDelegate(ShowItemTypeHelp);

            this.m_cItemEquipment = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, ITEM_EQUIPMENT_ALL);

            this.m_cHelpBoard = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, HELP_BOARD);

            this.m_cCollider = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, Collider);

            m_bOpened = false;
        }

        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();


        this.m_cCollider.SetActive(true);
        this.m_cCollider.transform.localPosition = Vector3.zero;
        if (this.m_cCollider.GetComponent<GUIComponentEvent>() == null)
            this.m_cCollider.AddComponent<GUIComponentEvent>().AddIntputDelegate(Collider_OnEvent);

        //设置标签数值
        setItemLabels();


        this.m_cTexHero = GUI_FINDATION.GET_OBJ_COMPONENT<UITexture>(this.m_cGUIObject, TEXTURE_HERO);

        Texture tex = ResourceMgr.LoadAsset(this.m_cCurHero.m_strAvatarA) as Texture;
        m_cTexHero.mainTexture = tex;
      

        CTween.TweenAlpha(this.m_cGUIObject, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, 0f, 1f);

        this.m_cGUIMgr.SetCurGUIID(this.ID);
        SetLocalPos(Vector3.zero);

        //设置Panel在top框下面 , 避免人物大图超出 ， 遮挡顶部
        this.m_cGUIObject.GetComponent<UIPanel>().depth = 201;
        GUIBackFrameTop top = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP) as GUIBackFrameTop;
        top.SetPanelDepth(202);
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Hiden();

        this.m_cHelpBoard.SetActive(false);

        //新手引导去除图鉴按钮
        if (Role.role.GetBaseProperty().m_iModelID <= 0)
        {
            this.m_cBtnAltas.SetActive(true);
            this.m_cBtnLock.SetActive(true);
            this.m_cBtnHelp.SetActive(true);
        }
        else
        {
            this.m_cBtnAltas.SetActive(false);
            this.m_cBtnLock.SetActive(false);
            this.m_cBtnHelp.SetActive(false);
        }
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
    /// 返回按钮事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickBackButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            //this.LocalpositionOld = Vector3.zero;
            //this.ClipRangeOld = Vector4.zero;
           // this.lstSelectHero.Clear();

            GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();
            
            if (this.m_delCallBack != null)
            {
                this.m_delCallBack();
            }
        }
    }

    /// <summary>
    /// 全屏点击退出
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void Collider_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            //this.LocalpositionOld = Vector3.zero;
            //this.ClipRangeOld = Vector4.zero;
            //this.lstSelectHero.Clear();

            GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

            if (this.m_delCallBack != null)
            {
                this.m_delCallBack();
            }
        }
    }

    /// <summary>
    /// 英雄图鉴 
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void HeroAtlasClick(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            GUIHeroAltasDetail tmp = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERO_ALTAS_DETAIL) as GUIHeroAltasDetail;
            tmp.m_iHeroTableId = m_cCurHero.m_iTableID;
            tmp.Show(BackToHeroDetail);


        }
    }

    /// <summary>
    /// 英雄图鉴的返回按钮回调方法
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void BackToHeroDetail()
    {
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Show();
        this.Show();

        //this.m_cCollider.SetActive(true);
        //this.m_cCollider.transform.localPosition = Vector3.zero;
        //if (this.m_cCollider.GetComponent<GUIComponentEvent>() == null)
        //    this.m_cCollider.AddComponent<GUIComponentEvent>().AddIntputDelegate(Collider_OnEvent);
            
    }

    /// <summary>
    /// 英雄锁定
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void HeroLockClick(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            m_cCurHero.m_bLock = !m_cCurHero.m_bLock;
            UISprite lab = this.m_cBtnLock.GetComponentInChildren<UISprite>();
            if (m_cCurHero.m_bLock)
            {
                lab.spriteName = BTN_UNLOCK;
                this.m_cImageBtnLock.normalSprite = BTN_UNLOCK;
                this.m_cImageBtnLock.pressedSprite = "btn_unlock1";
                this.m_cImageBtnLock.hoverSprite = "btn_unlock1";

                this.m_cSpLike.enabled = true;
                Role.role.GetHeroProperty().UpdateHero(m_cCurHero);

                SendAgent.SendHeroLockReq(
                    Role.role.GetBaseProperty().m_iPlayerId,
                    new List<int>() { m_cCurHero.m_iID });
            }
            else
            {
                this.m_cSpLike.enabled = false;
                Role.role.GetHeroProperty().UpdateHero(m_cCurHero);
                lab.spriteName = BTN_LOCK;
                this.m_cImageBtnLock.normalSprite = BTN_LOCK;
                this.m_cImageBtnLock.pressedSprite = "btn_lock1";
                this.m_cImageBtnLock.hoverSprite = "btn_lock1";

                SendAgent.SendHeroUnlockReq(
                    Role.role.GetBaseProperty().m_iPlayerId,
                    new List<int>() { m_cCurHero.m_iID });
            }
        }
    }

    /// <summary>
    /// 展示
    /// </summary>
    /// <param name="cal"></param>
    /// <param name="hero"></param>
    public void Show(CALLBACK cal, Hero hero)
    {
        ShowWithColliderBack(cal, hero);
    }

    /// <summary>
    /// 点击全屏返回的展示
    /// </summary>
    /// <param name="colliderCalBack"></param>
    /// <param name="hero"></param>
    public void ShowWithColliderBack(CALLBACK colliderCalBack, Hero hero)
    {
        this.m_delCallBack = colliderCalBack;
        this.m_cCurHero = hero;
        Show();
    }

    /// <summary>
    /// 设置标签数值
    /// </summary>
    private void setItemLabels()
    {
        UISprite lab = this.m_cBtnLock.GetComponentInChildren<UISprite>();
        this.m_cSpLike = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cGUIObject, SP_LIKE);

        if (m_cCurHero.m_bLock)
        {
            lab.spriteName = BTN_UNLOCK;
            this.m_cImageBtnLock.normalSprite = BTN_UNLOCK;
            this.m_cImageBtnLock.pressedSprite = "btn_unlock1";
            this.m_cImageBtnLock.hoverSprite = "btn_unlock1";
            this.m_cSpLike.enabled = true;
        }
        else
        {
            lab.spriteName = BTN_LOCK;
            this.m_cImageBtnLock.pressedSprite = "btn_lock1";
            this.m_cImageBtnLock.hoverSprite = "btn_lock1";
            this.m_cSpLike.enabled = false;
        }

        this.m_cNameLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LABEL_NAME);
        this.m_cNameLabel.text = this.m_cCurHero.m_strName;

        this.m_cTypeLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, ITEM_TYPE);//类型
        if (this.m_cCurHero.m_iTypeID > 0)
        {
            this.m_cTypeLabel.text = GUI_FUNCTION.GET_HERO_TYPE_STR(this.m_cCurHero.m_iTypeID);
        }
        else
        {
            this.m_cTypeLabel.text = GUI_FUNCTION.GetHeroGrowTypeName(m_cCurHero.m_eGrowType);
        }

        this.m_cMaxLvLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, ITEM_MAXLV);//最大等级
        this.m_cMaxLvLabel.text = "/" + this.m_cCurHero.m_iMaxLevel.ToString();

        this.m_cCurLvLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, ITEM_CURRENTLV);//当前等级
        this.m_cCurLvLabel.text = this.m_cCurHero.m_iLevel.ToString();

        this.m_cLvUpLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, ITEM_LVUP);//下次升级所需经验
        this.m_cSprHeroExpSlide = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, ITEM_LVEXP);  //经验条

        if (this.m_cCurHero.m_iMaxLevel == m_cCurHero.m_iLevel)  //已经满级
        {
            this.m_cLvUpLabel.text = "----";
            this.m_cSprHeroExpSlide.fillAmount = 1;
        }
        else
        {
            int currentExp = m_cCurHero.m_iCurrenExp;
            int maxExp = HeroEXPTableManager.GetInstance().GetMaxExp(m_cCurHero.m_iExpType, m_cCurHero.m_iLevel) - HeroEXPTableManager.GetInstance().GetMinExp(m_cCurHero.m_iExpType, m_cCurHero.m_iLevel);
            this.m_cLvUpLabel.text = (maxExp - currentExp).ToString();

            float expPercent = (float)(currentExp * 10 / maxExp) / 10.0f;
            this.m_cSprHeroExpSlide.fillAmount = expPercent;
        }

        //装备
        float ItemHP = 1;
        float ItemAttack = 1;
        float ItemDefense = 1;
        float ItemRecover = 1;
        this.m_cEquipmentLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, ITEM_EQUIPMENT);//装备
        ItemProperty itemProperty = Role.role.GetItemProperty();
        if (this.m_cCurHero.m_iEquipID > 0)
        {
            this.m_cItemEquipment.SetActive(true);
            this.m_cEquipmentLabel.text = itemProperty.GetItem((int)this.m_cCurHero.m_iEquipID).m_strShortName;
            //ItemHP += itemProperty.GetItem((int)this.m_cCurHero.m_iEquipID).m_fMaxHpInc;
            //ItemAttack += itemProperty.GetItem((int)this.m_cCurHero.m_iEquipID).m_fAttackInc;
            //ItemDefense += itemProperty.GetItem((int)this.m_cCurHero.m_iEquipID).m_fDefenceInc;
            //ItemRecover += itemProperty.GetItem((int)this.m_cCurHero.m_iEquipID).m_fRecoverInc;
            this.m_cSpItem = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, ITEM_ICON);
            this.m_cSpItemBorder = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, ITEM_BORDER);
            GUI_FUNCTION.SET_ITEM_HERO_DETAIL(m_cSpItem, itemProperty.GetItem(this.m_cCurHero.m_iEquipID).m_strSprName);
            GUI_FUNCTION.SET_ITEM_BORDER(m_cSpItemBorder, itemProperty.GetItem(this.m_cCurHero.m_iEquipID).m_eType);

            this.m_cHpLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, ITEM_HP);//血量
            if (itemProperty.GetItem((int)this.m_cCurHero.m_iEquipID).m_fMaxHpInc > 0)
            {
                this.m_cHpLabel.text = "[06ff00]" + ((int)(this.m_cCurHero.GetMaxHP() * ItemHP)).ToString();
                this.m_cHpLabel.effectStyle = UILabel.Effect.Outline;
                this.m_cHpLabel.effectDistance = new Vector2(2, 2);
            }
            else
            {
                this.m_cHpLabel.text = ((int)(this.m_cCurHero.GetMaxHP() * ItemHP)).ToString();
                this.m_cHpLabel.effectStyle = UILabel.Effect.None;
            }
            this.m_cAttackLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, ITEM_ATTACK);//攻击力
            if (itemProperty.GetItem((int)this.m_cCurHero.m_iEquipID).m_fAttackInc > 0)
            {
                this.m_cAttackLabel.text = "[06ff00]" + ((int)(this.m_cCurHero.GetAttack() * ItemAttack)).ToString();
                this.m_cAttackLabel.effectStyle = UILabel.Effect.Outline;
                this.m_cAttackLabel.effectDistance = new Vector2(2, 2);
            }
            else
            {
                this.m_cAttackLabel.text = ((int)(this.m_cCurHero.GetAttack() * ItemAttack)).ToString();
                this.m_cAttackLabel.effectStyle = UILabel.Effect.None;
            }
            this.m_cDefenseLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, ITEM_DEFENSE);//防御力
            if (itemProperty.GetItem((int)this.m_cCurHero.m_iEquipID).m_fDefenceInc > 0)
            {
                this.m_cDefenseLabel.text = "[06ff00]" + ((int)(this.m_cCurHero.GetDefence() * ItemDefense)).ToString();
                this.m_cDefenseLabel.effectStyle = UILabel.Effect.Outline;
                this.m_cDefenseLabel.effectDistance = new Vector2(2, 2);
            }
            else
            {
                this.m_cDefenseLabel.text = ((int)(this.m_cCurHero.GetDefence() * ItemDefense)).ToString();
                this.m_cDefenseLabel.effectStyle = UILabel.Effect.None;
            }
            this.m_cRecoverLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, ITEM_RECOVER);//恢复力
            if (itemProperty.GetItem((int)this.m_cCurHero.m_iEquipID).m_fRecoverInc > 0)
            {
                this.m_cRecoverLabel.text = "[06ff00]" + ((int)(this.m_cCurHero.GetRecover() * ItemRecover)).ToString();
                this.m_cRecoverLabel.effectStyle = UILabel.Effect.Outline;
                this.m_cRecoverLabel.effectDistance = new Vector2(2, 2);
            }
            else
            {
                this.m_cRecoverLabel.text = ((int)(this.m_cCurHero.GetRecover() * ItemRecover)).ToString();
                this.m_cRecoverLabel.effectStyle = UILabel.Effect.None;
            }
        }
        else
        {
            this.m_cItemEquipment.SetActive(false);

            //hp attack defense recover
            this.m_cHpLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, ITEM_HP);//血量
            this.m_cHpLabel.text = ((int)(this.m_cCurHero.GetMaxHP() * ItemHP)).ToString();
            this.m_cHpLabel.effectStyle = UILabel.Effect.None;
            this.m_cAttackLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, ITEM_ATTACK);//攻击力
            this.m_cAttackLabel.text = ((int)(this.m_cCurHero.GetAttack() * ItemAttack)).ToString();
            this.m_cAttackLabel.effectStyle = UILabel.Effect.None;
            this.m_cDefenseLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, ITEM_DEFENSE);//防御力
            this.m_cDefenseLabel.text = ((int)(this.m_cCurHero.GetDefence() * ItemDefense)).ToString();
            this.m_cDefenseLabel.effectStyle = UILabel.Effect.None;
            this.m_cRecoverLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, ITEM_RECOVER);//恢复力
            this.m_cRecoverLabel.text = ((int)(this.m_cCurHero.GetRecover() * ItemRecover)).ToString();
            this.m_cRecoverLabel.effectStyle = UILabel.Effect.None;
        }
        
        //Cost
        this.m_cCostLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, ITEM_COST);//领导力
        this.m_cCostLabel.text = this.m_cCurHero.m_iCost.ToString();
        //队长技能
        this.m_cLBBBName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, ITEM_BBName);
        this.m_cLBBBInfo = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, ITEM_BBInfo);
        this.m_cLBBBLv = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, ITEM_BBLv);
        this.m_cLbLeaderSkillInfo = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, ITEM_LeaderSkillInfo);
        this.m_cLBLeaderSkillName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, ITEM_LeaderSkillName);
        BBSkillTable bbskill = BBSkillTableManager.GetInstance().GetBBSkillTable(m_cCurHero.m_iBBSkillTableID);
        if (bbskill != null)
        {
            this.m_cLBBBName.text = bbskill.Name;
            this.m_cLBBBInfo.text = bbskill.Desc;
            this.m_cLBBBLv.text = "Lv." + m_cCurHero.m_iBBSkillLevel.ToString();
        }
        else
        {
            this.m_cLBBBName.text = "";
            this.m_cLBBBInfo.text = "";
            this.m_cLBBBLv.text = "";   
        }
        LeaderSkillTable leaderskill = LeaderSkillTableManager.GetInstance().GetLeaderSkillTable(m_cCurHero.m_iLeaderSkillID);
        if (null != leaderskill)
        {
            this.m_cLBLeaderSkillName.text = leaderskill.Name;
            this.m_cLbLeaderSkillInfo.text = leaderskill.Desc;
        }
        else
        {
            this.m_cLBLeaderSkillName.text = "";
            this.m_cLbLeaderSkillInfo.text = "";
        }

        //星级
        this.m_cSpStar1 = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cGUIObject, SP_STAR1);
        this.m_cSpStar2 = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cGUIObject, SP_STAR2);
        this.m_cSpStar3 = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cGUIObject, SP_STAR3);
        this.m_cSpStar4 = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cGUIObject, SP_STAR4);
        this.m_cSpStar5 = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cGUIObject, SP_STAR5);
        UISprite[] vecStars = new UISprite[]{this.m_cSpStar1,this.m_cSpStar2,this.m_cSpStar3,this.m_cSpStar4,this.m_cSpStar5};

        int maxstar = HeroTableManager.GetInstance().GetMaxStar(this.m_cCurHero.m_iTableID);
        for (int i = 0; i < 5; ++i)
        {
            if (m_cCurHero.m_iStarLevel >= i + 1)
            {
                vecStars[i].enabled = true;
                vecStars[i].spriteName = "star_rare";
                vecStars[i].MakePixelPerfect();
            }
            else if (i + 1 > m_cCurHero.m_iStarLevel && i + 1<= maxstar)
            {
                vecStars[i].enabled = true;
                vecStars[i].spriteName = "star_dark";
                vecStars[i].MakePixelPerfect();
            }
            else
                vecStars[i].enabled = false;
        }
        //英雄属性
        m_cSpNature = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cGUIObject, SP_NATURE);
        GUI_FUNCTION.SET_NATURES(m_cSpNature, m_cCurHero.m_eNature);
        //英雄表ID
        m_cLbHeroTableID = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cGUIObject, LB_UNIT);
        m_cLbHeroTableID.text = "英雄 No." + m_cCurHero.m_iTableID;
        //英雄成长类型
    }

    /// <summary>
    /// 点击帮助键展示
    /// </summary>
    /// <param name="colliderCalBack"></param>
    /// <param name="hero"></param>
    private void ShowItemTypeHelp(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.m_cHelpBoard.SetActive(true);
            if (!this.m_bOpened)
            {
                //显示各种类型描述
                this.m_cLabAttck = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LABEL_ATTACK).GetComponent<UILabel>();
                this.m_cLabAttck.text = GAME_FUNCTION.STRING(STRING_DEFINE.INFO_HERO_TYPR_ATTACK);
                this.m_cLabAttckDetail = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LABEL_ATTACK_DETAIL).GetComponent<UILabel>();
                this.m_cLabAttckDetail.text = GAME_FUNCTION.STRING(STRING_DEFINE.INFO_HERO_TYPE_ATTACK_DETAIL);
                this.m_cLabBalance = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LABEL_BALANCE).GetComponent<UILabel>();
                this.m_cLabBalance.text = GAME_FUNCTION.STRING(STRING_DEFINE.INFO_HERO_TYPE_BALANCE);
                this.m_cLabBalanceDetail = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LABEL_BALANCE_DETAIL).GetComponent<UILabel>();
                this.m_cLabBalanceDetail.text = GAME_FUNCTION.STRING(STRING_DEFINE.INFO_HERO_TYPE_BALANCE_DETAIL);
                this.m_cLabDefence = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LABEL_DEFENCE_DETAIL).GetComponent<UILabel>();
                this.m_cLabDefence.text = GAME_FUNCTION.STRING(STRING_DEFINE.INFO_HERO_TYPE_DEFENCE_DETAIL);
                this.m_cLabDefenceDetail = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LABEL_DEFENCE).GetComponent<UILabel>();
                this.m_cLabDefenceDetail.text = GAME_FUNCTION.STRING(STRING_DEFINE.INFO_HERO_TYPE_DEFENCE);
                this.m_cLabHP = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LABEL_HP).GetComponent<UILabel>();
                this.m_cLabHP.text = GAME_FUNCTION.STRING(STRING_DEFINE.INFO_HERO_TYPR_HP);
                this.m_cLabHPDetail = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LABEL_HP_DETAIL).GetComponent<UILabel>();
                this.m_cLabHPDetail.text = GAME_FUNCTION.STRING(STRING_DEFINE.INFO_HERO_TYPE_HP_DETAIL);
                this.m_cLabRecover = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LABEL_RECOVER).GetComponent<UILabel>();
                this.m_cLabRecover.text = GAME_FUNCTION.STRING(STRING_DEFINE.INFO_HERO_TYPE_RECOVER);
                this.m_cLabRecoverDetail = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LABEL_RECOVER_DETAIL).GetComponent<UILabel>();
                this.m_cLabRecoverDetail.text = GAME_FUNCTION.STRING(STRING_DEFINE.INFO_HERO_TYPE_RECOVER_DETAIL);
                this.m_bOpened = true;
            }
            
        }
        
    }

    /// <summary>
    /// 点击确认键
    /// </summary>
    /// <param name="colliderCalBack"></param>
    /// <param name="hero"></param>
    private void BackToDetail(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.m_cHelpBoard.SetActive(false);
        }
        
    }
}