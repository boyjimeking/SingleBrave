using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  BuildingTable.cs
//  Author: Lu Zexi
//  2013-11-26




/// <summary>
/// 建筑表
/// </summary>
public class BuildingTable : TableBase
{
    private string m_strName;   //名字
    public string Name
    {
        get { return this.m_strName; }
    }
    private string m_strDesc; //描述
    public String Desc
    {
        get { return this.m_strDesc; }
    }
    private int m_iType; //类型(1:装备合成建筑,2:消耗品合成建筑,3:建筑强化建筑,4:仓库建筑,5:收集体山，6:收集体川,7:收集体田,8:收集体森林)
    public int Type
    {
        get { return this.m_iType; }
    }

    /// <summary>
    /// 读取文本数据
    /// </summary>
    /// <param name="lstInfo"></param>
    /// <param name="index"></param>
    public override void ReadText(List<string> lstInfo, ref int index)
    {
        this.m_iType = INT(lstInfo[index++]);
        this.m_strName = STRING(lstInfo[index++]);
        this.m_strDesc = STRING(lstInfo[index++]);
    }
}

