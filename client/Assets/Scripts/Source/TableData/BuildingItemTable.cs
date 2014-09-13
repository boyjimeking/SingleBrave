using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  BuildingItemTable.cs
//  Author: Lu Zexi
//  2013-11-26



/// <summary>
/// 物品建筑表
/// </summary>
public class BuildingItemTable : TableBase
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
    private string m_strDesc;   //描述
    public string Desc
    {
        get { return this.m_strDesc; }
    }
    private int[] m_vecItemTableID; //可合成的物品ID
    public int[] VecItemTableID
    {
        get { return this.m_vecItemTableID; }
    }

    private const int MAX_ITEM = 10;    //最多可增加的物品

    public BuildingItemTable()
    {
        this.m_vecItemTableID = new int[MAX_ITEM];
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
        for (int i = 0; i < MAX_ITEM; i++)
        {
            this.m_vecItemTableID[i] = INT(lstInfo[index++]);
        }
    }
}
