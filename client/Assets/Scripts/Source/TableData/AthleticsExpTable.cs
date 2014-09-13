//Micro.Sanvey
//2013.12.9
//sanvey.china@gmail.com

using System;
using System.Collections.Generic;


/// <summary>
/// 竞技场经验表
/// </summary>
public class AthleticsExpTable : TableBase
{
    private int m_iLevel;  //用户等级
    public int Level
    {
        get { return m_iLevel; }
    }
    private int m_iExp;   //竞技点
    public int EXP
    {
        get { return this.m_iExp; }
    }
    private string m_strName;   //竞技称号
    public string Name
    {
        get { return this.m_strName; }
    }
    private int m_iNum;  //奖励砖石
    public int Num
    {
        get { return this.m_iNum; }
    }
    private int m_iItem; //奖励装备
    public int Item
    {
        get { return this.m_iItem; }
    }

    public AthleticsExpTable()
    {

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
        this.m_strName = STRING(lstInfo[index++]);
        this.m_iNum = INT(lstInfo[index++]);
        this.m_iItem = INT(lstInfo[index++]);
    }

}
