using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Base;
using Game.Resource;
using UnityEngine;

//竞技场战绩表管理
//Author sunyi
//2014-03-06
public class ArenaBattleRecordTableManager : Singleton<ArenaBattleRecordTableManager>
{
    private List<ArenaBattleRecordTable> m_lstArenaBattleRecord = new List<ArenaBattleRecordTable>();    //世界配置表列表

    public ArenaBattleRecordTableManager()
    {
#if GAME_TEST_LOAD
        //加载竞技场战绩
        LoadText(ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.ARENA_BATTLE_RECORD) as string);
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
            this.m_lstArenaBattleRecord.Clear();

            for (; index < lst.Count; )
            {
                ArenaBattleRecordTable battleRecord = new ArenaBattleRecordTable();
                battleRecord.ReadText(lst, ref index);
                this.m_lstArenaBattleRecord.Add(battleRecord);
            }
        }
        catch (System.Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "竞技场战绩表错误");
        }

    }

    /// <summary>
    /// 获取所有表 
    /// </summary>
    /// <returns></returns>
    public List<ArenaBattleRecordTable> GetAll()
    {
        return new List<ArenaBattleRecordTable>(this.m_lstArenaBattleRecord);
    }

    /// <summary>
    /// 获取指定战绩表
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ArenaBattleRecordTable GetArenaBattleRecordTable(int id)
    {
        foreach (ArenaBattleRecordTable item in this.m_lstArenaBattleRecord)
        {
            if (item.Id == id)
            {
                return item;
            }
        }

        return null;
    }
}

