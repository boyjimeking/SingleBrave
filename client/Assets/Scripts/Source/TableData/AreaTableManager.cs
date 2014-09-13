using System;
using System.Collections.Generic;
using Game.Base;
using UnityEngine;
using Game.Resource;

//  AreaTableManager.cs
//  Author: Lu Zexi
//  2013-11-18

/// <summary>
/// 区域表管理类
/// </summary>
public class AreaTableManager : Singleton<AreaTableManager>
{
    private List<AreaTable> m_lstAreaTable = new List<AreaTable>(); //区域表列表

    public AreaTableManager()
    {
#if GAME_TEST_LOAD
        //加载区域表
        LoadText((string)ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.AREA_TABLE_PATH));
#endif
    }

    /// <summary>
    /// 读取文本数据
    /// </summary>
    public void LoadText( string content )
    {
        try
        {
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;
            this.m_lstAreaTable.Clear();
            for (; index < lst.Count; )
            {
                AreaTable area = new AreaTable();
                area.ReadText(lst, ref index);
                this.m_lstAreaTable.Add(area);
            }
        }
        catch (Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "区域配置表错误");
        }
    }

    /// <summary>
    /// 获取所有区域类
    /// </summary>
    /// <returns></returns>
    public List<AreaTable> GetAll()
    {
        return new List<AreaTable>(this.m_lstAreaTable);
    }

    /// <summary>
    /// 获取区域表
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public AreaTable GetArea(int id)
    {
        for (int i = 0; i < this.m_lstAreaTable.Count; i++)
        {
            if (this.m_lstAreaTable[i].ID == id)
            {
                return this.m_lstAreaTable[i];
            }
        }
        return null;
    }

}

