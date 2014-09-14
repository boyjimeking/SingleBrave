using System;
using System.Collections.Generic;
using Game.Base;
using UnityEngine;
using Game.Resource;

//  DungeonTableManager.cs
//  Author: Lu Zexi
//  2013-11-18




/// <summary>
/// 副本表管理类
/// </summary>
public class DungeonTableManager : Singleton<DungeonTableManager>
{
    private List<DungeonTable> m_lstDungen = new List<DungeonTable>();

    public DungeonTableManager()
    {
#if GAME_TEST_LOAD
        //副本表
        LoadText(ResourceMgr.Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.DUNGEON_TABLE_PATH) as string);
#endif
    }

    /// <summary>
    /// 加载文本数据
    /// </summary>
    public void LoadText( string content )
    {
        try
        {
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;
            this.m_lstDungen.Clear();

            for (; index < lst.Count; )
            {
                DungeonTable table = new DungeonTable();
                table.ReadText(lst, ref index);
                this.m_lstDungen.Add(table);
            }
        }
        catch (Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "副本配置表错误");
            Debug.LogError(ex.StackTrace);
        }
    }

    /// <summary>
    /// 获取所有副本表
    /// </summary>
    /// <returns></returns>
    public List<DungeonTable> GetAll()
    {
        return new List<DungeonTable>(this.m_lstDungen);
    }

    /// <summary>
    /// 获取副本
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public DungeonTable GetDungeon(int id)
    {
        foreach (DungeonTable item in this.m_lstDungen)
        {
            if (item.ID == id)
            {
                return item;
            }
        }

        return null;
    }
}
