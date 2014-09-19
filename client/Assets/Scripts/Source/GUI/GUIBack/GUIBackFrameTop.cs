//Micro.Sanvey
//2013.11.12
//sanvey.china@gmail.com

using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Resource;
using Game.Base;

/// <summary>
/// 背景层顶部状态栏
/// </summary>
public class GUIBackFrameTop : GUIBase
{
    #region ----------Property---------

    private const string RES_BACKFRAMETOP = "GUI_BackFrameTop";         //背景状态栏地址
    private const string LB_NAME = "Lb_Name";  //用户名
    private const string LB_INFO = "Lb_Info";  //Info
    private const string LB_DIAMOND = "Lb_Diamond"; //砖石
    private const string LB_GOLD = "Lb_Gold";  //金币
    private const string LB_EXP = "Lb_EXP"; //经验显示
    private const string LB_FARMPOINT = "Lb_FarmPoint"; //农场点
    private const string LB_STRENGTH = "Lb_Strength"; //体力恢复计时器
    private const string LB_STRENGTHTIME = "Lb_StrengthTime"; //体力恢复计时器
    private const string SPR_STRENGTHFULL = "Strength_Full"; //体力条
    private const string SPR_EXPFULL = "EXP_Full"; //体力条
    private const string SPR_ART1 = "AthleticsPoint/Art1";  //竞技场点数高亮1
    private const string SPR_ART2 = "AthleticsPoint/Art2";  //竞技场点数高亮2
    private const string SPR_ART3 = "AthleticsPoint/Art3";  //竞技场点数高亮3
    private const string SPR_LVR = "LevelBase/Sp_R";   //等级显示
    private const string SPR_LVL = "LevelBase/Sp_L";   //等级显示  

    private UILabel m_cLbName;  //名称
    private UILabel m_cLbInfo;  //Info
    private UILabel m_cLbDiamond;  //砖石
    private UILabel m_cLbGold;  //金币
    private UILabel m_cLbEXP; //经验
    private UILabel m_cLbFarmPoint; //农场点
    private UILabel m_cLbStrength; //体力百分比
    private UILabel m_cLbStrengthTime; //体力恢复计数器
    private UISprite m_cSprEXP;   //exp条
    private UISprite m_cSprStrength;  //体力条
    private GameObject m_cArt1;  //竞技场点数高亮1
    private GameObject m_cArt2;  //竞技场点数高亮2
    private GameObject m_cArt3;  //竞技场点数高亮3
    private UISprite m_cLvR;    //等级显示
    private UISprite m_cLvL;    //等级显示

    public const float STRENGTH_PER = 600;    // 10*60s  10分钟恢复一点体力
    public const float PVP_PER = 3600;             // 60*60s  1小时恢复一点体力
    public const string STRING_MAX_STRENGTH = "当前体力已满";

    private float m_fTargetStrengthRate;    //目标体力比例
    private float m_fStartStrengthRate;    //开始体力比例
    private float m_fTargetStrengthStartTime;   //目标体力比例开始时间
    private const float TARGET_STRENGTH_TIME = 0.3f;  //目标体力花费时间
    public bool m_bIsUpdateIng;


    public GUIBackFrameTop(GUIManager mgr)
        : base(mgr, GUI_DEFINE.GUIID_BACKFRAMETOP, UILAYER.GUI_MENU)
    { }

    #endregion

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {

        if (this.m_cGUIObject == null)
        {

            //主资源
            this.m_cGUIObject = GameObject.Instantiate(ResourceMgr.LoadAsset(GAME_DEFINE.RESOURCE_GUI_CACHE, RES_BACKFRAMETOP) as UnityEngine.Object) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;
            //名称
            this.m_cLbName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LB_NAME);
            //Info
            this.m_cLbInfo = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LB_INFO);
            //砖石
            this.m_cLbDiamond = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LB_DIAMOND);
            //经验
            this.m_cLbEXP = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LB_EXP);
            //金币
            this.m_cLbGold = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LB_GOLD);
            //农场点
            this.m_cLbFarmPoint = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LB_FARMPOINT);
            //exp
            this.m_cLbStrength = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LB_STRENGTH);
            //体力恢复计数器
            this.m_cLbStrengthTime = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LB_STRENGTHTIME);
            //体力条
            this.m_cSprStrength = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SPR_STRENGTHFULL);
            //exp条
            this.m_cSprEXP = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SPR_EXPFULL);
            //竞技场点数高亮
            this.m_cArt1 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, SPR_ART1);
            this.m_cArt2 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, SPR_ART2);
            this.m_cArt3 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, SPR_ART3);
            m_cArt1.SetActive(false); m_cArt2.SetActive(false); m_cArt3.SetActive(false);
            //等级显示
            this.m_cLvL = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SPR_LVL);
            this.m_cLvR = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SPR_LVR);

        }

        if (Role.role.GetBaseProperty().m_fTopTime == -1)
        {
            Role.role.GetBaseProperty().m_fTopTime = GAME_TIME.TIME_REAL();
            Role.role.GetBaseProperty().m_fTopTimeSport = GAME_TIME.TIME_REAL();

            long secnd = (GAME_DEFINE.m_lServerTime - Role.role.GetBaseProperty().m_iStrengthTime);

            int maxStrength = RoleExpTableManager.GetInstance().GetMaxStrength(Role.role.GetBaseProperty().m_iLevel);
            int nowStrength = Role.role.GetBaseProperty().strength;
            Role.role.GetBaseProperty().m_fStrengthNext = (maxStrength - nowStrength) * STRENGTH_PER - (float)secnd;

            long secnd2 = (GAME_DEFINE.m_lServerTime - Role.role.GetBaseProperty().m_iSportTime);
            int maxPVP = 3;
            int nowPVP = Role.role.GetBaseProperty().sportpoint;
            Role.role.GetBaseProperty().m_fSportNext = (maxPVP - nowPVP) * PVP_PER - (float)secnd2;
        }


        this.m_fTargetStrengthRate = -1;
        this.m_fTargetStrengthStartTime = 0;

        UpdateData();

        SetLocalPos(Vector3.zero);

        base.Show();
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {

        base.Hiden();
        Destory();
    }

    public override void HidenImmediately()
    {

        base.Hiden();
        Destory();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        this.m_cLbName = null;
        this.m_cLbInfo = null;
        this.m_cLbDiamond = null;
        this.m_cLbGold = null;
        this.m_cLbEXP = null;
        this.m_cLbFarmPoint = null;
        this.m_cLbStrength = null;
        this.m_cLbStrengthTime = null;
        this.m_cSprEXP = null;
        this.m_cSprStrength = null;
        this.m_cArt1 = null;
        this.m_cArt2 = null;
        this.m_cArt3 = null;
        this.m_cLvR = null;
        this.m_cLvL = null;

        base.Destory();
    }

    /// <summary>
    /// 读取用户数据，更新UI
    /// </summary>
    public void UpdateData()
    {


        RoleBaseProperty baseProperty = Role.role.GetBaseProperty();

        this.m_cLbName.text = baseProperty.m_strUserName;
        this.m_cLbDiamond.text = baseProperty.m_iDiamond.ToString();
        this.m_cLbGold.text = baseProperty.m_iGold.ToString();
        this.m_cLbFarmPoint.text = baseProperty.m_iFarmPoint.ToString();
        if (baseProperty.m_iLevel == 1)
            this.m_cLbEXP.text = string.Format("{0}/{1}", Role.role.GetBaseProperty().m_iCurrentExp, RoleExpTableManager.GetInstance().GetMaxExp(baseProperty.m_iLevel));
        else
            this.m_cLbEXP.text = string.Format("{0}/{1}", Role.role.GetBaseProperty().m_iCurrentExp, RoleExpTableManager.GetInstance().GetMaxExp(baseProperty.m_iLevel) - RoleExpTableManager.GetInstance().GetMaxExp(baseProperty.m_iLevel - 1));
        this.m_cLbInfo.text = AthleticsExpTableManager.GetInstance().GetAthleticsNameByPoint(baseProperty.m_iPVPExp);
        int maxExp = RoleExpTableManager.GetInstance().GetMaxExp(baseProperty.m_iLevel) - RoleExpTableManager.GetInstance().GetMinExp(baseProperty.m_iLevel);
        this.m_cSprEXP.fillAmount = (float)baseProperty.m_iCurrentExp / (float)maxExp;

        int maxStrength = RoleExpTableManager.GetInstance().GetMaxStrength(baseProperty.m_iLevel);
        this.m_cLbStrength.text = string.Format("{0}/{1}", baseProperty.strength, maxStrength);
        this.m_cSprStrength.fillAmount = (float)baseProperty.strength / (float)maxStrength;

        if (baseProperty.m_iLevel < 10)
        {
            this.m_cLvL.spriteName = baseProperty.m_iLevel.ToString();
            this.m_cLvL.transform.localPosition = Vector3.one;
            this.m_cLvL.gameObject.SetActive(true);
            this.m_cLvR.gameObject.SetActive(false);
        }
        else
        {
            this.m_cLvL.spriteName = (baseProperty.m_iLevel / 10).ToString();
            this.m_cLvR.spriteName = (baseProperty.m_iLevel % 10).ToString();
            this.m_cLvR.gameObject.transform.localPosition = new Vector3(14f, 0, 0);
            this.m_cLvL.gameObject.transform.localPosition = new Vector3(-19f, 0, 0);
            this.m_cLvL.gameObject.SetActive(true);
            this.m_cLvR.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 减少体力动画
    /// </summary>
    /// <param name="num"></param>
    public void DecStrength(int num)
    {
        RoleBaseProperty baseProperty = Role.role.GetBaseProperty();

        this.m_fStartStrengthRate = Role.role.GetBaseProperty().strength * 1f / RoleExpTableManager.GetInstance().GetMaxStrength(baseProperty.m_iLevel);


        Role.role.GetBaseProperty().m_iStrength -= num;
        if (Role.role.GetBaseProperty().m_iStrength < 0)
            Role.role.GetBaseProperty().m_iStrength = 0;
        //Role.role.GetBaseProperty().strength -= num;
        //if (Role.role.GetBaseProperty().strength < 0)
        //    Role.role.GetBaseProperty().strength = 0;

        this.m_fTargetStrengthRate = Role.role.GetBaseProperty().strength * 1f / RoleExpTableManager.GetInstance().GetMaxStrength(baseProperty.m_iLevel);
        this.m_fTargetStrengthStartTime = GAME_TIME.TIME_FIXED();

        this.m_cLbStrength.text = string.Format("{0}/{1}", Role.role.GetBaseProperty().strength, RoleExpTableManager.GetInstance().GetMaxStrength(baseProperty.m_iLevel));
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <returns></returns>
    public override bool Update()
    {
        if (this.IsShow())
        {
            if (m_bIsUpdateIng)  //在程序重新激活的时候，更新全部初始数据，暂停刷新，避免赋值不安全
            {
                return base.Update();
            }

            //削减体力动画
            if (this.m_fTargetStrengthStartTime > 0)
            {
                float dis = GAME_TIME.TIME_FIXED() - this.m_fTargetStrengthStartTime;
                if (dis >= TARGET_STRENGTH_TIME)
                {
                    this.m_fTargetStrengthStartTime = 0;
                    this.m_cSprStrength.fillAmount = this.m_fTargetStrengthRate;
                }
                else
                {
                    this.m_cSprStrength.fillAmount = Mathf.Lerp(this.m_fStartStrengthRate, this.m_fTargetStrengthRate, dis / TARGET_STRENGTH_TIME);
                }
            }
            else
            {
                RoleBaseProperty rp = Role.role.GetBaseProperty();

                //体力控制
                int maxStrength = RoleExpTableManager.GetInstance().GetMaxStrength(rp.m_iLevel);
                int nowStrength = rp.strength;
                if (nowStrength >= maxStrength)  //已经最大体力
                {
                    this.m_cSprStrength.fillAmount = 1;
                    this.m_cLbStrength.text = string.Format("{0}/{1}", rp.strength, maxStrength);
                    this.m_cLbStrengthTime.text = STRING_MAX_STRENGTH;
                    rp.m_fTopTime = GAME_TIME.TIME_REAL();
                    rp.m_fStrengthNext = 0;
                }
                else
                {
                    float pass = GAME_TIME.TIME_REAL() - rp.m_fTopTime;  //过去时间
                    //Debug.Log("pass:   " + pass);
                    float next = rp.m_fStrengthNext - pass;  //eg  700s 回满  现在pass过去 4s 剩下 696s
                    //Debug.Log("next:   " + next);
                    int passnum = (int)Math.Ceiling(next / STRENGTH_PER);
                    //Debug.Log("passnum:   " + passnum);
                    rp.strength = maxStrength - passnum;
                    this.m_cLbStrength.text = string.Format("{0}/{1}", rp.strength, maxStrength);
                    this.m_cSprStrength.fillAmount = (float)rp.strength / (float)maxStrength;

                    float yu = next % STRENGTH_PER;
                    int minute = (int)yu / 60;
                    int second = (int)yu % 60;
                    this.m_cLbStrengthTime.text = string.Format("回复还剩 {0:00}:{1:00}", (int)minute, (int)second);
                }

                //更新战斗点数
                int maxPVP = 3;
                int nowPVP = rp.sportpoint;
                if (nowPVP >= maxPVP)  //已经最大体力
                {
                    m_cArt1.SetActive(true); m_cArt2.SetActive(true); m_cArt3.SetActive(true);
                    rp.m_fTopTimeSport = GAME_TIME.TIME_FIXED();
                    rp.m_fSportNext = 0;
                }
                else
                {
                    float pass = GAME_TIME.TIME_FIXED() - rp.m_fTopTimeSport;  //过去时间
                    float next = rp.m_fSportNext - pass;  //eg  700s 回满  现在pass过去 4s 剩下 696s
                    int passnum = (int)Math.Ceiling(next / PVP_PER);
                    rp.sportpoint = maxPVP - passnum;

                    switch (rp.sportpoint)
                    {
                        case 1: m_cArt1.SetActive(true); m_cArt2.SetActive(false); m_cArt3.SetActive(false);
                            break;
                        case 2: m_cArt1.SetActive(true); m_cArt2.SetActive(true); m_cArt3.SetActive(false);
                            break;
                        case 3: m_cArt1.SetActive(true); m_cArt2.SetActive(true); m_cArt3.SetActive(true);
                            break;
                        case 0:
                        default: m_cArt1.SetActive(false); m_cArt2.SetActive(false); m_cArt3.SetActive(false);
                            break;
                    }
                }
            }
        }

        return base.Update();
    }


    /// <summary>
    /// 更新金币
    /// </summary>
    public void UpdateGold()
    {
        if (IsShow())
        {
            this.m_cLbGold.text = Role.role.GetBaseProperty().m_iGold.ToString();
        }
    }    

    /// <summary>
    /// 更新农场点
    /// </summary>
    public void UpdateFarmPiont()
    {
        if (IsShow())
        {
            this.m_cLbFarmPoint.text = Role.role.GetBaseProperty().m_iFarmPoint.ToString();
        }
    }

    /// <summary>
    /// 更新宝石
    /// </summary>
    public void UpdateDiamond(int diamond)
    {
        if (IsShow())
        {

            this.m_cLbDiamond.text = Role.role.GetBaseProperty().m_iDiamond.ToString();
        }

    }

    /// <summary>
    /// 更新竞技点
    /// </summary>
    /// <param name="sportPoint"></param>
    public void UpdateSportPoint(int sportPoint)
    {
        if (IsShow())
        {
            switch (sportPoint)
            {
                case 1: m_cArt1.SetActive(true); m_cArt2.SetActive(false); m_cArt3.SetActive(false);
                    break;
                case 2: m_cArt1.SetActive(true); m_cArt2.SetActive(true); m_cArt3.SetActive(false);
                    break;
                case 3: m_cArt1.SetActive(true); m_cArt2.SetActive(true); m_cArt3.SetActive(true);
                    break;
                case 0:
                default: m_cArt1.SetActive(false); m_cArt2.SetActive(false); m_cArt3.SetActive(false);
                    break;
            }
        }
    }

    ///// <summary>
    ///// 体力全满
    ///// </summary> 
    //public void UpdateStrength()
    //{
    //    //int maxStrength = RoleExpTableManager.GetInstance().GetMaxStrength(Role.role.GetBaseProperty().m_iLevel);
    //    //Role.role.GetBaseProperty().m_iStrength = maxStrength;
    //}

    /// <summary>
    /// 判断当前体力是否为满
    /// </summary>
    /// <returns></returns>
    public bool IsStrengthFull()
    {
        bool isFull = false;
        RoleBaseProperty baseProperty = Role.role.GetBaseProperty();
        if (Role.role.GetBaseProperty().strength < RoleExpTableManager.GetInstance().GetMaxStrength(baseProperty.m_iLevel))
        {
            isFull = false; ;
        }
        else
        {
            isFull = true;
        }

        return isFull;
    }

    /// <summary>
    /// 判断当前格斗点是否为满
    /// </summary>
    /// <returns></returns>
    public bool IsSportPpointhFull()
    {
        bool isFull = false;
        if (Role.role.GetBaseProperty().sportpoint < 3)
        {
            isFull = false; ;
        }
        else
        {
            isFull = true;
        }

        return isFull;
    }

    /// <summary>
    /// 设置top层级 eg 英雄详细界面人物需要在top下面
    /// </summary>
    /// <param name="p"></param>
    public void SetPanelDepth(int p)
    {
        this.m_cGUIObject.GetComponent<UIPanel>().depth = p;
    }
}