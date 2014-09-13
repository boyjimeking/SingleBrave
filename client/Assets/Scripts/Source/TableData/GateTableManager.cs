using System;
using System.Collections.Generic;
using Game.Base;
using UnityEngine;
using Game.Resource;

//  GateTableManager.cs
//  Author: Lu Zexi
//  2013-11-18



/// <summary>
/// 关卡配置表管理类
/// </summary>
public class GateTableManager : Singleton<GateTableManager>
{
    private List<GateTable> m_lstGate = new List<GateTable>();  //关卡列表
    
    public GateTableManager()
    {
#if GAME_TEST_LOAD
        //关卡表
        LoadText(ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.GATE_TABLE_PATH) as string);
#endif
    }

    /// <summary>
    /// 加载文本数据
    /// </summary>
    public void LoadText(string content )
    {
        try
        {
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;
            this.m_lstGate.Clear();

            for (; index < lst.Count; )
            {
                GateTable table = new GateTable();
                table.ReadText(lst, ref index);
                this.m_lstGate.Add(table);
            }
        }
        catch (Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "关卡配置表错误");
        }
    }

    /// <summary>
    /// 获取所有数据表
    /// </summary>
    /// <returns></returns>
    public List<GateTable> GetAll()
    {
        return new List<GateTable>(this.m_lstGate);
    }

    /// <summary>
    /// 获取关卡数据表
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public GateTable GetGateTable(int id)
    {
        foreach (GateTable item in this.m_lstGate)
        {
            if (item.ID == id)
            {
                return item;
            }
        }
        return null;
    }

}
