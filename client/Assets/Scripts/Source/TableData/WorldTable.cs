using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  WorldTable.cs
//  Author: Lu Zexi
//  2013-11-18

/// <summary>
/// 世界数据表
/// </summary>
public class WorldTable : TableBase
{
    private int m_iID;  //唯一标识
    public int ID
    {
        get { return this.m_iID; }
    }
    private string m_strName;   //名字
    public string Name
    {
        get { return this.m_strName; }
    }
    private string m_strDesc;   //描述
    public string Desc
    {
        get { return this.m_strDesc; }
    }
    private string m_strCondition;  //条件
    public string Condition
    {
        get { return this.m_strCondition; }
    }
    private int m_iAreaNum; //区域数量
    public int AreaNum
    {
        get { return this.m_iAreaNum; }
    }
    private int[] m_vecArea;    //区域
    public int[] VecArea
    {
        get { return this.m_vecArea; }
    }

    public WorldTable()
    {
        this.m_vecArea = new int[10];
    }

    /// <summary>
    /// 读取文本
    /// </summary>
    /// <param name="lstInfo"></param>
    /// <param name="index"></param>
    public override void ReadText(List<string> lstInfo, ref int index)
    {
        this.m_iID = INT(lstInfo[index++]);
        this.m_strName = STRING(lstInfo[index++]);
        this.m_strDesc = STRING(lstInfo[index++]);
        this.m_strCondition = STRING(lstInfo[index++]);
        this.m_iAreaNum = INT(lstInfo[index++]);
        for (int i = 0; i < this.m_vecArea.Length; i++)
        {
            this.m_vecArea[i] = INT(lstInfo[index++]);
        }
    }

    public override string ToString()
    {
        return String.Format("id:{0} name:{1} Desc: {2} Conditon:{3}", this.m_iID, this.m_strName, this.m_strDesc, this.m_strCondition);
    }
}

