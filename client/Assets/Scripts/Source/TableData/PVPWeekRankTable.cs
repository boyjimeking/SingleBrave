using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  PVPWeekRankTable.cs
//  Author: Lu Zexi
//  2013-11-18


/// <summary>
/// 竞技场周排行奖励
/// </summary>
public class PVPWeekRankTable : TableBase
{
    private int m_iRankFrom;  //竞技排名
    public int RankFrom
    {
        get { return this.m_iRankFrom; }
    }
    private int m_iRankTo;  //竞技排名
    public int RankTo
    {
        get { return this.m_iRankTo; }
    }
    private GiftType m_eAwardType;  //奖励类型
    public GiftType AwardType
    {
        get { return this.m_eAwardType; }
    }
    private int m_iNum;  //奖励数量
    public int Num
    {
        get { return this.m_iNum; }
    }
    private int m_iID;  //ID
    public int ID
    {
        get { return this.m_iID; }
    }

    public PVPWeekRankTable()
    {
    }

    /// <summary>
    /// 读取文本
    /// </summary>
    /// <param name="lstInfo"></param>
    /// <param name="index"></param>
    public override void ReadText(List<string> lstInfo, ref int index)
    {
        this.m_iRankFrom = INT(lstInfo[index++]);
        this.m_iRankTo = INT(lstInfo[index++]);
        this.m_eAwardType = (GiftType)INT(lstInfo[index++]);
        this.m_iNum = INT(lstInfo[index++]);
        this.m_iID = INT(lstInfo[index++]);

    }
}

