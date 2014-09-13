using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



//  BuildingEquipTable.cs
//  Author: Lu Zexi
//  2013-11-26



/// <summary>
/// 装备建筑数据配置表
/// </summary>
public class BuildingEquipTable : TableBase
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
    private int[] m_vecItemTableID; //所拥有的装备物品配置ID
    public int[] VecItemTableID
    {
        get { return this.m_vecItemTableID; }
    }
    private string m_strDesc;   //描述
    public string Desc
    {
        get { return this.m_strDesc; }
    }

    private const int MAX_ITEMTABLE = 10;

    public BuildingEquipTable()
    {
        this.m_vecItemTableID = new int[MAX_ITEMTABLE];
    }

    public override void ReadText(List<string> lstInfo, ref int index)
    {
        this.m_iLevel = INT(lstInfo[index++]);
        this.m_iExp = INT(lstInfo[index++]);
        this.m_strDesc = STRING(lstInfo[index++]);
        for (int i = 0; i < MAX_ITEMTABLE; i++)
        {
            this.m_vecItemTableID[i] = INT(lstInfo[index++]);
        }
        
    }
}

