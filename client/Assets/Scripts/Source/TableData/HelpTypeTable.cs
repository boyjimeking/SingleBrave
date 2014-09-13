using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//帮助类型类
//Author:sunyi
//2014-1-15
public class HelpTypeTable : TableBase
{
    private int m_iID;

    public int ID//唯一标识
    {
        get { return this.m_iID; }
    }

    private string m_strTypeName;//类型名称

    public string TypeName
    {
        get { return this.m_strTypeName; }
    }

    private int m_iProjectNum;//项目总数

    public int ProjectNum
    {   
        get { return this.m_iProjectNum; }
    }

    private int[] vecProject;

    public int[] VecProject
    {
        get { return this.vecProject; }
    }

    public HelpTypeTable()
    {
        this.vecProject = new int[12];
    }

    /// <summary>
    /// 读取文本
    /// </summary>
    /// <param name="lstInfo"></param>
    /// <param name="index"></param>
    public override void ReadText(List<string> lstInfo, ref int index)
    {
        this.m_iID = INT(lstInfo[index++]);
        this.m_strTypeName = STRING(lstInfo[index++]);
        this.m_iProjectNum = INT(lstInfo[index++]);
        for (int i = 0; i < this.vecProject.Length; i++)
        {
            this.vecProject[i] = INT(lstInfo[index++]);
        }
    }
}

