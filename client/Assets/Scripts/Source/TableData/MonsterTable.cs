using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  MonsterTable.cs
//  Author: Lu Zexi
//  2013-11-18




/// <summary>
/// 怪物表
/// </summary>
public class MonsterTable : TableBase
{
    private string m_strName;   //名字
    public string Name
    {
        get { return this.m_strName; }
    }
    private int m_iHeroTableID; //英雄配置表
    public int HeroTableID
    {
        get { return this.m_iHeroTableID; }
    }
    private int m_iAIType;  //AI类型
    public int AIType
    {
        get { return this.m_iAIType; }
    }
    private int m_iHP;  //生命值
    public int HP
    {
        get { return this.m_iHP; }
    }
    private int m_iAttack;  //攻击力
    public int Attack
    {
        get { return this.m_iAttack; }
    }
    private int m_iDefence; //防御力
    public int Defence
    {
        get { return this.m_iDefence; }
    }
    private int m_iBBSkillID;   //BB技能ID
    public int BBSkillID
    {
        get { return this.m_iBBSkillID; }
    }
    private int m_iBBLevel; //BB技能等级
    public int BBLevel
    {
        get { return this.m_iBBLevel; }
    }
    private int m_iDEBUF;   //减益状态
    public int DEBUF
    {
        get { return this.m_iDEBUF; }
    }
    private float m_fDEBUFRate; //减益状态比率
    public float DEBUFRate
    {
        get { return this.m_fDEBUFRate; }
    }
    private int m_iDEBUFRound;  //减益状态回合数
    public int DEBUFRound
    {
        get { return this.m_iDEBUFRound; }
    }
    private int m_iDropGold;    //掉落金币
    public int DropGold
    {
        get { return this.m_iDropGold; }
    }
    private int m_iDropGoldMax; //掉落金币最大数
    public int DropGoldMax
    {
        get { return this.m_iDropGoldMax; }
    }
    private float m_fDropGoldRate;  //金币掉率率
    public float DropGoldRate
    {
        get { return this.m_fDropGoldRate; }
    }
    private int m_iDropFarm;    //掉落农场点
    public int DropFarm
    {
        get { return this.m_iDropFarm; }
    }
    private int m_iDropFarmMax; //掉落农场点最大数
    public int DropFarmMax
    {
        get { return this.m_iDropFarmMax; }
    }
    private float m_fDropFarmRate;    //掉落农场点几率
    public float DropFarmRate
    {
        get { return this.m_fDropFarmRate; }
    }
    private float m_fDropItemRate;  //素材掉落概率
    public float DropItemRate
    {
        get { return this.m_fDropItemRate; }
    }
    private float m_fCatchRate; //捕捉率
    public float CatchRate
    {
        get { return this.m_fCatchRate; }
    }
    private int m_iCatchID; //捕捉ID
    public int CatchID
    {
        get { return this.m_iCatchID; }
    }
    private float m_fOpposeDu;   //毒抗性
    public float OpposeDu
    {
        get { return this.m_fOpposeDu; }
    }
    private float m_fOpposeXuruo;    //虚弱抗性
    public float OpposeXuruo
    {
        get { return this.m_fOpposeXuruo; }
    }
    private float m_fOpposePojia;    //破甲抗性
    public float OpposePojia
    {
        get { return this.m_fOpposePojia; }
    }
    private float m_fOpposePoren;    //破刃抗性
    public float OpposePoren
    {
        get { return this.m_fOpposePoren; }
    }
    private float m_fOpposeFenying;  //封印抗性
    public float OpposeFenying
    {
        get { return this.m_fOpposeFenying; }
    }
    private float m_fOpposeMa;   //麻痹抗性
    public float OpposeMa
    {
        get { return this.m_fOpposeMa; }
    }

    public MonsterTable()
    { 
        //
    }

    /// <summary>
    /// 读取文本
    /// </summary>
    /// <param name="lstInfo"></param>
    /// <param name="index"></param>
    public override void ReadText(List<string> lstInfo, ref int index)
    {
        this.m_strName = STRING(lstInfo[index++]);
        this.m_iHeroTableID = INT(lstInfo[index++]);
        this.m_iAIType = INT(lstInfo[index++]);
        this.m_iHP = INT(lstInfo[index++]);
        this.m_iAttack = INT(lstInfo[index++]);
        this.m_iDefence = INT(lstInfo[index++]);
        this.m_iBBSkillID = INT(lstInfo[index++]);
        this.m_iBBLevel = INT(lstInfo[index++]);
        this.m_iDEBUF = INT(lstInfo[index++]);
        this.m_fDEBUFRate = FLOAT(lstInfo[index++]);
        this.m_iDEBUFRound = INT(lstInfo[index++]);
        this.m_iDropGold = INT(lstInfo[index++]);
        this.m_iDropGoldMax = INT(lstInfo[index++]);
        this.m_fDropGoldRate = FLOAT(lstInfo[index++]);
        this.m_iDropFarm = INT(lstInfo[index++]);
        this.m_iDropFarmMax = INT(lstInfo[index++]);
        this.m_fDropFarmRate = FLOAT(lstInfo[index++]);
        this.m_fDropItemRate = FLOAT(lstInfo[index++]);
        this.m_fCatchRate = FLOAT(lstInfo[index++]);
        this.m_iCatchID = INT(lstInfo[index++]);
        this.m_fOpposeDu = FLOAT(lstInfo[index++]);
        this.m_fOpposeXuruo = FLOAT(lstInfo[index++]);
        this.m_fOpposePojia = FLOAT(lstInfo[index++]);
        this.m_fOpposePoren = FLOAT(lstInfo[index++]);
        this.m_fOpposeMa = FLOAT(lstInfo[index++]);
        this.m_fOpposeFenying = FLOAT(lstInfo[index++]);
    }

}

