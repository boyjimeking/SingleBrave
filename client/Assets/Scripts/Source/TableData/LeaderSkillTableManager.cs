using System;
using System.Collections.Generic;
using Game.Base;
using UnityEngine;
using Game.Resource;

//  LeaderSkillTableManager.cs
//  Author: Lu Zexi
//  2013-12-02



/// <summary>
/// 队长技能表管理类
/// </summary>
public class LeaderSkillTableManager : Singleton<LeaderSkillTableManager>
{
    private List<LeaderSkillTable> m_lstLeader = new List<LeaderSkillTable>();  //队长技能表

    public LeaderSkillTableManager()
    {
#if GAME_TEST_LOAD
        //队长技能表
        LoadText(ResourceMgr.Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.LEADER_SKILL_PATH) as string);
#endif
    }

    /// <summary>
    /// 获取队长技能表
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public LeaderSkillTable GetLeaderSkillTable(int id)
    {
        foreach (LeaderSkillTable item in this.m_lstLeader)
        {
            if (item.ID == id)
                return item;
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
            this.m_lstLeader.Clear();

            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;
            for (; index < lst.Count; )
            {
                LeaderSkillTable table = new LeaderSkillTable();
                table.ReadText(lst, ref index);
                this.m_lstLeader.Add(table);
            }
        }
        catch (System.Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "队长技能配置表错误");
        }
    }

}

