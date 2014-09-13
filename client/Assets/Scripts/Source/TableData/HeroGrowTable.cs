using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  HeroGrowTable.cs
//  Author: Sanvey
//  2013-11-18


/// <summary>
/// 英雄配置表
/// </summary>
public class HeroGrowTable : TableBase
{
    private int m_iID;  //唯一标识
    public int ID
    {
        get { return this.m_iID; }
    }
    private int m_iLvMax; //最大等级
    public int LvMax
    {
        get { return this.m_iLvMax; }
    }
    private int m_iMinHP; //最小HP
    public int MinHP
    {
        get { return this.m_iMinHP; }
    }
    private int m_iMinAtk; //最小攻击
    public int MinAtK
    {
        get { return this.m_iMinAtk; }
    }
    private int m_iMinDef; //最小防御
    public int MinDef
    {
        get { return this.m_iMinDef; }
    }
    private int m_iMinRec; //最小回复
    public int MinRec
    {
        get { return this.m_iMinRec; }
    }
    private int m_iBHP;  //平衡型HP
    public int BHP
    {
        get { return this.m_iBHP; }
    }
    private int m_iBAtk;   //平衡型攻击
    public int BAtk
    {
        get { return this.m_iBAtk; }
    }
    private int m_iBDef;    //平衡型防御
    public int BDef
    {
        get { return this.m_iBDef; }
    }
    private int m_iBRec;    //平衡型回复
    public int BRec
    {
        get { return this.m_iBRec; }
    }
    private int m_iHHP;  //HP型HP
    public int HHP
    {
        get { return this.m_iHHP; }
    }
    private int m_iHAtk;   //HP型攻击
    public int HAtk
    {
        get { return this.m_iHAtk; }
    }
    private int m_iHDef;    //HP型防御
    public int HDef
    {
        get { return this.m_iHDef; }
    }
    private int m_iHRec;    //HP型回复
    public int HRec
    {
        get { return this.m_iHRec; }
    }
    private int m_iAHP;  //攻击型HP
    public int AHP
    {
        get { return this.m_iAHP; }
    }
    private int m_iAAtk;   //攻击型攻击
    public int AAtk
    {
        get { return this.m_iAAtk; }
    }
    private int m_iADef;    //攻击型防御
    public int ADef
    {
        get { return this.m_iADef; }
    }
    private int m_iARec;    //攻击型回复
    public int ARec
    {
        get { return this.m_iARec; }
    }
    private int m_iDHP;  //防御型HP
    public int DHP
    {
        get { return this.m_iDHP; }
    }
    private int m_iDAtk;   //防御型攻击
    public int DAtk
    {
        get { return this.m_iDAtk; }
    }
    private int m_iDDef;    //防御型防御
    public int DDef
    {
        get { return this.m_iDDef; }
    }
    private int m_iDRec;    //防御型回复
    public int DRec
    {
        get { return this.m_iDRec; }
    }
    private int m_iRHP;  //回复型HP
    public int RHP
    {
        get { return this.m_iRHP; }
    }
    private int m_iRAtk;   //回复型攻击
    public int RAtk
    {
        get { return this.m_iRAtk; }
    }
    private int m_iRDef;    //回复型防御
    public int RDef
    {
        get { return this.m_iRDef; }
    }
    private int m_iRRec;    //回复型回复
    public int RRec
    {
        get { return this.m_iRRec; }
    }

    public HeroGrowTable()
    {

    }

    /// <summary>
    /// 读取文本
    /// </summary>
    /// <param name="lstInfo"></param>
    /// <param name="index"></param>
    public override void ReadText(List<string> lstInfo, ref int index)
    {
        this.m_iID = INT(lstInfo[index++]);
        this.m_iLvMax = INT(lstInfo[index++]);
        this.m_iMinHP = INT(lstInfo[index++]);
        this.m_iMinAtk = INT(lstInfo[index++]);
        this.m_iMinDef = INT(lstInfo[index++]);
        this.m_iMinRec = INT(lstInfo[index++]);
        this.m_iBHP = INT(lstInfo[index++]);
        this.m_iBAtk = INT(lstInfo[index++]);
        this.m_iBDef = INT(lstInfo[index++]);
        this.m_iBRec = INT(lstInfo[index++]);
        this.m_iHHP = INT(lstInfo[index++]);
        this.m_iHAtk = INT(lstInfo[index++]);
        this.m_iHDef = INT(lstInfo[index++]);
        this.m_iHRec = INT(lstInfo[index++]);
        this.m_iAHP = INT(lstInfo[index++]);
        this.m_iAAtk = INT(lstInfo[index++]);
        this.m_iADef = INT(lstInfo[index++]);
        this.m_iARec = INT(lstInfo[index++]);
        this.m_iDHP = INT(lstInfo[index++]);
        this.m_iDAtk = INT(lstInfo[index++]);
        this.m_iDDef = INT(lstInfo[index++]);
        this.m_iDRec = INT(lstInfo[index++]);
        this.m_iRHP = INT(lstInfo[index++]);
        this.m_iRAtk = INT(lstInfo[index++]);
        this.m_iRDef = INT(lstInfo[index++]);
        this.m_iRRec = INT(lstInfo[index++]);
    }

}