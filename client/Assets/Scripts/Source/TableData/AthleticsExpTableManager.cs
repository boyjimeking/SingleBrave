//Micro.Sanvey
//2013.12.9
//sanvey.china@gmail.com

using System;
using System.Collections.Generic;
using Game.Base;
using UnityEngine;
using Game.Resource;

/// <summary>
/// 竞技场经验列表管理类
/// </summary>
public class AthleticsExpTableManager : Singleton<AthleticsExpTableManager>
{
    private List<AthleticsExpTable> m_lstAreaTable = new List<AthleticsExpTable>(); //竞技场经验列表

    public AthleticsExpTableManager()
    {
#if GAME_TEST_LOAD
        //竞技场经验列表
        LoadText((string)ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.ALTHLETICSEXP_PATH));
#endif
    }

    /// <summary>
    /// 读取文本数据
    /// </summary>
    public void LoadText(string content)
    {
        try
        {
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;
            this.m_lstAreaTable.Clear();
            for (; index < lst.Count; )
            {
                AthleticsExpTable area = new AthleticsExpTable();
                area.ReadText(lst, ref index);
                this.m_lstAreaTable.Add(area);
            }
        }
        catch (Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "竞技场经验配置表错误");
        }
    }

    /// <summary>
    /// 获取所有竞技场经验
    /// </summary>
    /// <returns></returns>
    public List<AthleticsExpTable> GetAll()
    {
        return new List<AthleticsExpTable>(this.m_lstAreaTable);
    }

    /// <summary>
    /// 根据等级获取竞技场经验
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public AthleticsExpTable GetAthExp(int level)
    {
        for (int i = 0; i < this.m_lstAreaTable.Count; i++)
        {
            if (this.m_lstAreaTable[i].Level == level)
            {
                return this.m_lstAreaTable[i];
            }
        }
        return null;
    }

    /// <summary>
    /// 根据用户竞技场等级获得对应称号
    /// </summary>
    /// <param name="athLevel"></param>
    /// <returns></returns>
    public string GetAthleticsName(int athLevel)
    {
        return GetAthExp(athLevel).Name;
    }

    /// <summary>
    /// 根据用户竞技点获得对应的最大经验
    /// </summary>
    /// <param name="athLevel"></param>
    /// <returns></returns>
    public int GetAhtleticsMaxExp(int point)
    {
        int re = 0;
        for (int i = 0; i < this.m_lstAreaTable.Count; i++)
        {
            if (point < this.m_lstAreaTable[i].EXP)
            {
               return this.m_lstAreaTable[i].EXP;
            }
        }
        return re;
    }

    /// <summary>
    /// 根据用户竞技点获得对应的最小经验
    /// </summary>
    /// <param name="athLevel"></param>
    /// <returns></returns>
    public int GetAhtleticsMinExp(int point)
    {
        int re = 0;
        for (int i = this.m_lstAreaTable.Count - 1; i > 0; i--)
        {
            if (point >= this.m_lstAreaTable[i].EXP)
            {
                return this.m_lstAreaTable[i].EXP;
            }
        }
        return re;
    }

    /// <summary>
    /// 用户竞技点获得对应的称号
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public string GetAthleticsNameByPoint(int point)
    {
       string re=string.Empty;
        for (int i = 0; i < this.m_lstAreaTable.Count; i++)
        {
            if (this.m_lstAreaTable[i].EXP <= point)
            {
                re = this.m_lstAreaTable[i].Name; 
            }
            else
            {
                return re;
            }
        }
        return re;
    }
}