using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  ActiveDungeonTable.cs
//  Author: Lu Zexi
//  2013-01-02
/// <summary>
/// 活动副本配置表
/// </summary>
public class ActivityDungeonTable :TableBase
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
    private string m_strDesc;   //说明
    public string Desc
    {
        get { return this.m_strDesc; }
    }
    private string m_strSpName; //图集名
    public string SpName
    {
        get { return this.m_strSpName; }
    }
    private int m_iTimeType;    //时间类型
    public int TimeType
    {
        get { return this.m_iTimeType; }
    }
    private string m_strStartTime;  //开始时间
    public string StartTime
    {
        get { return this.m_strStartTime; } 
    }
    private string m_strEndTime;    //结束时间
    public string EndTime
    {
        get { return this.m_strEndTime; }
    }
    private int m_iGateNum; //关卡数量
    public int GateNum
    {
        get { return this.m_iGateNum; }
    }
    private int[] m_vecGate;    //关卡
    public int[] VecGate
    {
        get { return this.m_vecGate; }
    }

    public ActivityDungeonTable()
    { 
        this.m_vecGate = new int[10];
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
        this.m_strSpName = STRING(lstInfo[index++]);
        this.m_iTimeType = INT(lstInfo[index++]);
        this.m_strStartTime = STRING(lstInfo[index++]);
        this.m_strEndTime = STRING(lstInfo[index++]);
        this.m_iGateNum = INT(lstInfo[index++]);
        for (int i = 0; i < this.m_vecGate.Length; i++)
        {
            this.m_vecGate[i] = INT(lstInfo[index++]);
        }
    }
}
