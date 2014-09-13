using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//战绩属性
//Author sunyi
//2014-02-28
public class BattleRecordProperty
{
    public List<int> m_lstRecords = new List<int>();//列表

    /// <summary>
    /// 获取战绩列表
    /// </summary>
    /// <returns></returns>
    public List<int> GetAll()
    {
        return new List<int>(this.m_lstRecords);
    }

    /// <summary>
    /// 添加战绩记录
    /// </summary>
    /// <param name="record"></param>
    public void AddRecord(int record)
    {
        this.m_lstRecords.Add(record);
    }

    /// <summary>
    /// 清空
    /// </summary>
    public void Clear()
    {
        if (this.m_lstRecords != null)
        {
            this.m_lstRecords.Clear();
        }
        
    }
}

