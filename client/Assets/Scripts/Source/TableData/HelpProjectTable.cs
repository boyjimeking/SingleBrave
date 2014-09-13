using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//帮助项目数据表
//Author Sunyi
//2014-1-15
public class HelpProjectTable : TableBase
{
    private int m_iID;//唯一标识·

    public int ID
    {
        get { return m_iID; }
    }

    private string m_strProjectName;//项目名称

    public string ProjectName
    {
        get { return m_strProjectName; }
    }

    private string m_strDetailDesc;//详细描述

    public string DetailDesc
    {
        get { return m_strDetailDesc; }
    }

    /// <summary>
    /// 读取文本
    /// </summary>
    /// <param name="lstInfo"></param>
    /// <param name="index"></param>
    public override void ReadText(List<string> lstInfo, ref int index)
    {
        this.m_iID = INT(lstInfo[index++]);
        this.m_strProjectName = STRING(lstInfo[index++]);
        this.m_strDetailDesc = STRING(lstInfo[index++]);
    }
    
}

