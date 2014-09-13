using System;
using System.Collections.Generic;


//  AreaTable.cs
//  Author; Lu Zexi
//  2013-11-18




/// <summary>
/// 区域表
/// </summary>
public class AreaTable : TableBase
{
    private int m_iID;  //唯一标识
    public int ID
    {
        get { return m_iID; }
    }
    private string m_strName;   //区域名字
    public string Name
    {
        get { return this.m_strName; }
    }
    private string m_strDesc;   //描述
    public string Desc
    {
        get { return this.m_strDesc; }
    }
    private int m_iStoryID; //剧情ID
    public int StoryID
    {
        get { return this.m_iStoryID; }
    }
    private int m_iMaxNum;  //最大数量
    public int MaxNum
    {
        get { return this.m_iMaxNum; }
    }
    private int[] m_vecDungeon; //副本
    public int[] VecDungeon
    {
        get { return this.m_vecDungeon; }
    }

    private string m_strBgName;//背景图片文件名

    public string BgName
    {
        get { return m_strBgName; }
    }

    private string m_strTitleSprName;

    public string TitleSprName
    {
        get { return m_strTitleSprName; }
    }
    
    public AreaTable()
    {
        this.m_vecDungeon = new int[10];
    }


    /// <summary>
    /// 读取文本数据
    /// </summary>
    /// <param name="lstInfo"></param>
    /// <param name="index"></param>
    public override void ReadText(List<string> lstInfo, ref int index)
    {
        this.m_iID = INT(lstInfo[index++]);
        this.m_strName = STRING(lstInfo[index++]);
        this.m_strDesc = STRING(lstInfo[index++]);
        this.m_iStoryID = INT(lstInfo[index++]);
        this.m_iMaxNum = INT(lstInfo[index++]);
        for (int i = 0; i < this.m_vecDungeon.Length; i++)
        {
            this.m_vecDungeon[i] = INT(lstInfo[index++]);
        }
        this.m_strBgName = STRING(lstInfo[index++]);
        this.m_strTitleSprName = STRING(lstInfo[index++]);
    }

}
