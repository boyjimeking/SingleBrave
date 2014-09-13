using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//  HeroSellMoney.cs
//  Sanvey
//  2013-12-20

/// <summary>
/// 英雄出售价格表
/// </summary>
public class HeroSellMoneyTable : TableBase
{
    private int m_iLv;  //等级
    public int LV
    {
        get { return this.m_iLv; }
    }

    private int[] m_vecMoney;  //星级对应价钱
    public int[] Money
    {
        get { return this.m_vecMoney; }
    }

    public HeroSellMoneyTable()
        : base()
    {
        this.m_vecMoney = new int[5];
    }

    /// <summary>
    /// 读取数据
    /// </summary>
    /// <param name="lstInfo"></param>
    /// <param name="index"></param>
    public override void ReadText(List<string> lstInfo, ref int index)
    {
        this.m_iLv = INT(lstInfo[index++]);
        for (int i = 0; i < this.m_vecMoney.Length; i++)
        {
            this.m_vecMoney[i] = INT(lstInfo[index++]);
        }

    }

}
