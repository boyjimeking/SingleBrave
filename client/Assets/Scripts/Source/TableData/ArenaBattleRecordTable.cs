using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//竞技场战绩表
//Author sunyi
//2014-03-06
public class ArenaBattleRecordTable : TableBase
{
    private int m_iId;//id

    public int Id
    {
        get { return m_iId; }
    }

    private string m_strTitle;//标题

    public string Title
    {
        get { return m_strTitle; }
    }

    private int m_iCoperTimes;//铜

    public int CoperTimes
    {
        get { return m_iCoperTimes; }
    }

    private int m_iSilverTimes;//银

    public int SilverTimes
    {
        get { return m_iSilverTimes; }
    }

    private int m_iGoldTimes;//金

    public int GoldTimes
    {
        get { return m_iGoldTimes; }
    }

    /// <summary>
    /// 读取文本
    /// </summary>
    /// <param name="lstInfo"></param>
    /// <param name="index"></param>
    public override void ReadText(List<string> lstInfo, ref int index)
    {
        this.m_iId = INT(lstInfo[index++]);
        this.m_strTitle = STRING(lstInfo[index++]);
        this.m_iCoperTimes = INT(lstInfo[index++]);
        this.m_iSilverTimes = INT(lstInfo[index++]);
        this.m_iGoldTimes = INT(lstInfo[index++]);
    }
}

