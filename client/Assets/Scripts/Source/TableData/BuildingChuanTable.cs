using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  BuildingChuanTable.cs
//  Author: Lu Zexi
//  2013-11-26


/// <summary>
/// 建筑川升级表
/// </summary>
public class BuildingChuanTable : TableBase
{
    private int m_iLevel;   //等级
    public int Level
    {
        get { return this.m_iLevel; }
    }
    private int m_iExp; //需要的经验
    public int Exp
    {
        get { return this.m_iExp; }
    }
    private string m_strDesc;   //升级描述
    public string Desc
    {
        get { return this.m_strDesc; }
    }
    private int m_iHITNum;  //增加的点击次数
    public int HITNum
    {
        get { return this.m_iHITNum; }
    }

    private int[] m_vecItemTableID; //增加的获得物品表
    public int[] VecItemTableID
    {
        get { return this.m_vecItemTableID; }
    }
    private int[] m_vecItemWeight;  //物品权重
    public int[] VecItemWeight
    {
        get { return this.m_vecItemWeight; }
    }
    private int m_iGold;  //金币数量
    public int Gold
    {
        get { return this.m_iGold; }
    }
    private int m_iGoldWeight;  //金币权重
    public int GoldWeight
    {
        get { return this.m_iGoldWeight; }
    }
    private int m_iFarmPoint;  //农场数量
    public int FarmPoint
    {
        get { return this.m_iFarmPoint; }
    }
    private int m_iFarmPointWeight;  //农场权重
    public int FarmPointWeight
    {
        get { return this.m_iFarmPointWeight; }
    }
    private int m_iDiamond;  //砖石数量
    public int Diamond
    {
        get { return this.m_iDiamond; }
    }
    private int m_iDiamondWeight;  //砖石权重
    public int DiamondWeight
    {
        get { return this.m_iDiamondWeight; }
    }

    private int m_iRecoveryTime; //恢复时间
    public int RecoveryTime
    {
        get { return this.m_iRecoveryTime; }
    }


    private const int MAX_ITEM = 5;    //最多可增加的物品

    public BuildingChuanTable()
    {
        this.m_vecItemTableID = new int[MAX_ITEM];
        this.m_vecItemWeight = new int[MAX_ITEM];
    }

    /// <summary>
    /// 读取文本数据
    /// </summary>
    /// <param name="lstInfo"></param>
    /// <param name="index"></param>
    public override void ReadText(List<string> lstInfo, ref int index)
    {
        this.m_iLevel = INT(lstInfo[index++]);
        this.m_iExp = INT(lstInfo[index++]);
        this.m_strDesc = STRING(lstInfo[index++]);
        this.m_iRecoveryTime = INT(lstInfo[index++]);
        this.m_iHITNum = INT(lstInfo[index++]);
        for (int i = 0; i < MAX_ITEM; i++)
        {
            this.m_vecItemTableID[i] = INT(lstInfo[index++]);
            this.m_vecItemWeight[i] = INT(lstInfo[index++]);
        }
        this.m_iGold = INT(lstInfo[index++]);
        this.m_iGoldWeight = INT(lstInfo[index++]);
        this.m_iFarmPoint = INT(lstInfo[index++]);
        this.m_iFarmPointWeight = INT(lstInfo[index++]);
        this.m_iDiamond = INT(lstInfo[index++]);
        this.m_iDiamondWeight = INT(lstInfo[index++]);
    }
}