using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  HeroTable.cs
//  Author: Lu Zexi
//  2013-11-18



/// <summary>
/// 英雄配置表
/// </summary>
public class HeroTable : TableBase
{
    private int m_iID;  //唯一标识
    public int ID
    {
        get { return this.m_iID; }
    }
    private string m_strAvatorMRes; //中头像资源
    public string AvatorMRes
    {
        get { return this.m_strAvatorMRes; }
    }
    private string m_strAvatorLRes; //大头像资源
    public string AvatorLRes
    {
        get { return this.m_strAvatorLRes; }
    }
    private string m_strAvatorARes; //全身像资源
    public string AvatorARes
    {
        get { return this.m_strAvatorARes; }
    }
    private string m_strModle;  //模型
    public string Modle
    {
        get { return this.m_strModle; }
    }
    private string m_strName;   //名字
    public string Name
    {
        get { return this.m_strName; }
    }
    private int m_iProperty;    //属性
    public int Property
    {
        get { return this.m_iProperty; }
    }
    private int m_iMoveType;    //移动类型
    public int MoveType
    {
        get { return this.m_iMoveType; }
    }
    private int m_iStar;    //星级
    public int Star
    {
        get { return this.m_iStar; }
    }
    private string m_strSEHit1; //击打音效1
    public string SEHit1
    {
        get { return this.m_strSEHit1; }
    }
    private string m_strSEHit2; //击打音效2
    public string SEHit2
    {
        get { return this.m_strSEHit2; }
    }
    private int m_iMaxLevel;   //最大等级
    public int MaxLevel
    {
        get { return this.m_iMaxLevel; }
    }
    private int m_iMaxBBHP; //最大BB槽
    public int MaxBBHP
    {
        get { return this.m_iMaxBBHP; }
    }
    private int m_iCombineExp;    //合成经验
    public int CombineExp
    {
        get { return this.m_iCombineExp; }
    }
    private int m_iExpType; //经验成长曲线类型
    public int ExpType
    {
        get { return this.m_iExpType; }
    }
    private int m_iTypeID;  //类型ID
    public int TypeID
    {
        get { return this.m_iTypeID; }
    }
    private int m_iSellCost;  //出售花费
    public int SellCost
    {
        get { return this.m_iSellCost; }
    }
    private int m_iEvolveID;    //进化后的ID
    public int EvolveID
    {
        get { return this.m_iEvolveID; }
    }
    private int[] m_vecEvolveMat;   //进化素材
    public int[] VecEvolveMat
    {
        get { return this.m_vecEvolveMat; }
    }
    private int m_iSpeed;   //移动速度
    public int Speed
    {
        get { return this.m_iSpeed; }
    }
    private int m_iCost;    //花费的领导力
    public int Cost
    {
        get { return this.m_iCost; }
    }
    private int m_iBBSkill; //BB技能
    public int BBSkill
    {
        get { return this.m_iBBSkill; }
    }
    private int m_iLeaderSkill; //队长技能
    public int LeaderSkill
    {
        get { return this.m_iLeaderSkill; }
    }
    private string m_strDesc;   //描述
    public string Desc
    {
        get { return this.m_strDesc; }
    }
    private string m_strWord;   //台词
    public string Word
    {
        get { return this.m_strWord.Replace("\\n", "\n"); }
    }

    public HeroTable()
    {
        this.m_vecEvolveMat = new int[5];
    }

    /// <summary>
    /// 读取文本
    /// </summary>
    /// <param name="lstInfo"></param>
    /// <param name="index"></param>
    public override void ReadText(List<string> lstInfo, ref int index)
    {
        this.m_iID = INT(lstInfo[index++]);
        this.m_strAvatorMRes = STRING(lstInfo[index++]);
        this.m_strAvatorLRes = STRING(lstInfo[index++]);
        this.m_strAvatorARes = STRING(lstInfo[index++]);
        this.m_strModle = STRING(lstInfo[index++]);
        this.m_strName = STRING(lstInfo[index++]);
        this.m_iProperty = INT(lstInfo[index++]);
        this.m_iMoveType = INT(lstInfo[index++]);
        this.m_iStar = INT(lstInfo[index++]);
        this.m_strSEHit1 = STRING(lstInfo[index++]);
        this.m_strSEHit2 = STRING(lstInfo[index++]);
        this.m_iMaxLevel = INT(lstInfo[index++]);
        this.m_iMaxBBHP = INT(lstInfo[index++]);
        this.m_iCombineExp = INT(lstInfo[index++]);
        this.m_iExpType = INT(lstInfo[index++]);
        this.m_iTypeID = INT(lstInfo[index++]);
        this.m_iSellCost = INT(lstInfo[index++]);
        this.m_iEvolveID = INT(lstInfo[index++]);
        for( int i = 0 ; i<this.m_vecEvolveMat.Length ; i++ )
        {
            this.m_vecEvolveMat[i] = INT(lstInfo[index++]);
        }
        this.m_iSpeed = INT(lstInfo[index++]);
        this.m_iCost = INT(lstInfo[index++]);
        this.m_iBBSkill = INT(lstInfo[index++]);
        this.m_iLeaderSkill = INT(lstInfo[index++]);
        this.m_strDesc = STRING(lstInfo[index++]);
        this.m_strWord = STRING(lstInfo[index++]);
    }

}

