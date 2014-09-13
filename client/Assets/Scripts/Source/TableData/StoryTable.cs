using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  StoryTable.cs
//  Author: Lu Zexi
//  2014-02-26




/// <summary>
/// 剧情表
/// </summary>
public class StoryTable : TableBase
{
    private int m_iID;  //剧情ID
    public int ID
    {
        get { return this.m_iID; }
    }
    private string m_strSceneName;  //场景名
    public string SceneName
    {
        get { return this.m_strSceneName; }
    }
    private string m_strTittle; //标题
    public string Tittle
    {
        get { return this.m_strTittle; }
    }
    private string m_strFaceName;   //表情名字
    public string FaceName
    {
        get { return this.m_strFaceName; }
    }
    private int m_iNum; //对话数量
    public int Num
    {
        get { return this.m_iNum; }
    }
    private List<string> m_lstDialog = new List<string>();  //对话数量
    public List<string> LstDialog
    {
        get { return this.m_lstDialog; }
    }
    public StoryTable()
    { 
        //
    }

    /// <summary>
    /// 读取文本数据
    /// </summary>
    /// <param name="lstInfo"></param>
    /// <param name="index"></param>
    public override void ReadText(List<string> lstInfo, ref int index)
    {
        this.m_iID = INT(lstInfo[index++]);
        this.m_strSceneName = STRING(lstInfo[index++]);
        this.m_strTittle = STRING(lstInfo[index++]);
        this.m_strFaceName = STRING(lstInfo[index++]);
        this.m_iNum = INT(lstInfo[index++]);
        for (int i = 0; i < this.m_iNum; i++)
        {
            string str = STRING(lstInfo[index++]);
            str = str.Replace("\\n", "\n");
            this.m_lstDialog.Add(str);
        }
    }
}
