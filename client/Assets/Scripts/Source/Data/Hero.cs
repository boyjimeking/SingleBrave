
using System.Collections;
using UnityEngine;


//  Hero.cs
//  Author: Lu Zexi
//  2013-11-14



/// <summary>
/// 英雄类
/// </summary>
public class Hero : CModel
{
    public int m_iID;   //英雄唯一标识
    public int m_iTableID;  //配置表的对应ID
    public int m_iLevel;   //等级//
    public int m_iHp;  //血量//
    public int m_iAttack;  //攻击//
    public int m_iDefence; //防御//
    public int m_iRevert;  //恢复//
    public int m_iBBSkillLevel; //BB技能等级//
    public int m_iEquipID; //装备ID//
    public bool m_bLock = false; //是否锁定//
    public bool m_bNew = false;  //最新获得//
    public long m_lGetTime;  //获得该英雄时间
    public int m_iCurrenExp;  //英雄当前经验
    public float m_fOpposeDu;   //毒抗性
    public float m_fOpposeXuruo;    //虚弱抗性
    public float m_fOpposePojia;    //破甲抗性
    public float m_fOpposePoren;    //破刃抗性
    public float m_fOpposeFenying;  //封印抗性
    public float m_fOpposeMa;   //麻痹抗性
    public GrowType m_eGrowType; //成长类型 平衡 攻击 HP 防御 回复

    //数据表固定数据
    public string m_strName; //姓名//
    public int m_iCost;    //领导力//
    public int m_iStarLevel;   //星级//
    public int m_iMaxLevel; //最大等级
    public int m_iMaxBBHP;  //最大BB槽
    public Nature m_eNature;   //属性//
    public int m_iExpType;  //成长曲线类型 1，2，3
    public int m_iTypeID;   //英雄类型0.随机行，1.经验型，2.进化素材，3.金钱素材
    public string m_strModel;    //模型名字//
    public string m_strAvatarM; //中头像
    public string m_strAvatarL; //大头像
    public string m_strAvatarA; //全身像
    public int m_iBBSkillTableID;   //BB技能配置ID//
    public int m_iLeaderSkillID;    //队长技能配置ID
    public float m_fMoveSpeed; //速度//
    public int m_iCombineExp; //怪物吃掉的基本经验//
    public MoveType m_eMoveType;   //移动类型//
    public int[] m_vecEvolution;    //进化素材
    public int m_iEvolutionID;      //进化ID
    public int m_iSellCost; //出售价格


    public Hero(int tableID)
    {
        this.m_fOpposeDu = 0;
        this.m_fOpposeXuruo = 0;
        this.m_fOpposePojia = 0;
        this.m_fOpposePoren = 0;
        this.m_fOpposeFenying = 0;
        this.m_fOpposeMa = 0;

        HeroTable table = HeroTableManager.GetInstance().GetHeroTable(tableID);

        if (tableID == null)
        {
            Debug.LogError("Hero Table is null.");
            return;
        }

        this.m_iTableID = tableID;
        this.m_strName = table.Name;
        this.m_iCost = table.Cost;
        this.m_iStarLevel = table.Star;
        this.m_iMaxLevel = table.MaxLevel;
        this.m_iMaxBBHP = table.MaxBBHP;
        this.m_eNature = (Nature)table.Property;
        this.m_iExpType = table.ExpType;
        this.m_iTypeID = table.TypeID;
        this.m_strModel = table.Modle;
        this.m_strAvatarM = table.AvatorMRes;
        this.m_strAvatarL = table.AvatorLRes;
        this.m_strAvatarA = table.AvatorARes;
        this.m_iBBSkillTableID = table.BBSkill;
        this.m_iLeaderSkillID = table.LeaderSkill;
        this.m_fMoveSpeed = table.Speed;
        this.m_iCombineExp = table.CombineExp;
        this.m_eMoveType = (MoveType)table.MoveType;
        this.m_vecEvolution = table.VecEvolveMat;
        this.m_iEvolutionID = table.EvolveID;
        this.m_iSellCost = table.SellCost;
    }

    /// <summary>
    /// 获取攻击力
    /// </summary>
    /// <returns></returns>
    public int GetAttack()
    {
        if (this.m_iEquipID > 0)
        {
            Item item = Role.role.GetItemProperty().GetItem(this.m_iEquipID);     //获取好友的攻击力时，好友的装备不应该从本地存在的装备中读取，而是表中读取
            //ItemTable table = ItemTableManager.GetInstance().GetItem(this.m_iEquipID);
            return (int)(this.m_iAttack + this.m_iAttack * item.m_fAttackInc);
        }
        return this.m_iAttack;
    }

    /// <summary>
    /// 获取防御终值
    /// </summary>
    /// <returns></returns>
    public int GetDefence()
    {
        if (this.m_iEquipID > 0)
        {
            Item item = Role.role.GetItemProperty().GetItem(this.m_iEquipID);
            //ItemTable table = ItemTableManager.GetInstance().GetItem(this.m_iEquipID);
            return (int)(this.m_iDefence + this.m_iDefence * item.m_fDefenceInc);
        }
        return this.m_iDefence;
    }

    /// <summary>
    /// 获取回复终值
    /// </summary>
    /// <returns></returns>
    public int GetRecover()
    {
        if (this.m_iEquipID > 0)
        {
            Item item = Role.role.GetItemProperty().GetItem(this.m_iEquipID);
           //ItemTable table = ItemTableManager.GetInstance().GetItem(this.m_iEquipID);
            return (int)(this.m_iRevert + this.m_iRevert * item.m_fRecoverInc);
        }
        return this.m_iRevert;
    }

    /// <summary>
    /// 获取MAXHP终值
    /// </summary>
    /// <returns></returns>
    public int GetMaxHP()
    {
        if (this.m_iEquipID > 0)
        {
            Item item = Role.role.GetItemProperty().GetItem(this.m_iEquipID);
            //ItemTable table = ItemTableManager.GetInstance().GetItem(this.m_iEquipID);
            return (int)(this.m_iHp + this.m_iHp * item.m_fMaxHpInc);
        }
        return this.m_iHp;
    }
}

//属性  火 水等等//
public enum Nature
{
    None = 0,   //无
    Fire = 1,   //火
    Water = 2,  //水
    Wood = 3,   //木
    Thunder = 4,    //雷
    Bright = 5, //光
    Dark = 6    //暗
}

//成长类型//
public enum GrowType
{
    Balance = 1,
    Hp = 2,
    Attack = 3,
    Defense = 4,
    Revert = 5
}

//普通攻击移动类型//
public enum MoveType
{
    None = 0,   //不移动
    Normal = 1, //移动
}

/// <summary>
/// BB技能类型
/// </summary>
public enum BBType
{ 
    NONE = 0, //无
    ATTACK = 1, //攻击
    RECOVER = 2,    //回复
    BUFF = 3,   //BUFF
}

//BB技能类型//
public enum BBTargetType
{
    TargetAll = 1,  //敌方全体
    TargetOne = 2,  //敌方单体
    TargetRandom = 3, //敌方随机
    SelfAll = 4, //我方全体
    SelfOne = 5, //我方单体
    SelfRandom = 6,    //我方随机
    Self =7,  //自身
}
