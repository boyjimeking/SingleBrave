using System;
using System.Collections.Generic;
using Game.Base;
using UnityEngine;
using Game.Resource;

//  WorldTableManager
//  Author: Lu Zexi
//  2013-11-18

/// <summary>
/// 世界表管理类
/// </summary>
public class WorldTableManager : Singleton<WorldTableManager>
{
    private List<WorldTable> m_lstWorld = new List<WorldTable>();    //世界配置表列表

    public WorldTableManager()
    {
#if GAME_TEST_LOAD
        //加载世界表
        LoadText(ResourceMgr.Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.WORLD_TABLE_PATH) as string);
#endif
    }

    /// <summary>
    /// 读取文本
    /// </summary>
    public void LoadText(string content)
    {
        try
        {
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;
            this.m_lstWorld.Clear();

            for (; index < lst.Count; )
            {
                WorldTable world = new WorldTable();
                world.ReadText(lst, ref index);
                this.m_lstWorld.Add(world);
            }
        }
        catch (System.Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, " 世界配置表错误");
        }

    }


    /// <summary>
    /// 获取所有表 
    /// </summary>
    /// <returns></returns>
    public List<WorldTable> GetAll()
    {
        return new List<WorldTable>(this.m_lstWorld);
    }

    /// <summary>
    /// 获取指定世界表
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public WorldTable GetWorldTable(int id)
    {
        foreach (WorldTable item in this.m_lstWorld)
        {
            if (item.ID == id)
            {
                return item;
            }
        }

        return null;
    }

}

