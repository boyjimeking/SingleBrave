using System;
using System.Collections.Generic;
using Game.Base;
using UnityEngine;
using Game.Resource;

//  PVPWeekRankTableManager.cs
//  Author: Lu Zexi
//  2013-12-02



/// <summary>
/// 竞技场周排名奖励表管理类
/// </summary>
public class PVPWeekRankTableManager : Singleton<PVPWeekRankTableManager>
{
    private List<PVPWeekRankTable> m_lstRanks = new List<PVPWeekRankTable>();  //队长技能表

    public PVPWeekRankTableManager()
    {
#if GAME_TEST_LOAD
        //队长技能表
        LoadText(ResourceMgr.Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.PVP_WEEK_RANK_PATH) as string);
#endif
    }

    /// <summary>
    /// 获取所有排行记录
    /// </summary>
    /// <returns></returns>
    public List<PVPWeekRankTable> GetAll()
    {
        return new List<PVPWeekRankTable>(this.m_lstRanks);
    }

    /// <summary>
    /// 更具排名对应的 获取奖励表
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public PVPWeekRankTable GetWeekRankTable(int rank)
    {
        foreach (PVPWeekRankTable item in this.m_lstRanks)
        {
            if (item.RankFrom<=rank&&item.RankTo>=rank)
            {
                return item;
            }
        }
        return null;
    }

    /// <summary>
    /// 读取文本数据
    /// </summary>
    /// <param name="content"></param>
    public void LoadText(string content)
    {
        try
        {
            this.m_lstRanks.Clear();

            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;
            for (; index < lst.Count; )
            {
                PVPWeekRankTable table = new PVPWeekRankTable();
                table.ReadText(lst, ref index);
                this.m_lstRanks.Add(table);
            }
        }
        catch (System.Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "竞技场周排行配置表错误");
        }
    }

}

