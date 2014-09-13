using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Base;
using Game.Resource;
using UnityEngine;

//战绩信息表管理类
//Author sunyi
//2014-03-04
public class BattleRecordTableManager : Singleton<BattleRecordTableManager>
{
    private List<BattleRecordTable> m_lstBattleRecord = new List<BattleRecordTable>();    //战绩表列表

    public BattleRecordTableManager()
    {
#if GAME_TEST_LOAD
        //加载战绩表
        LoadText(ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.BATTLE_RECORD) as string);
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
            this.m_lstBattleRecord.Clear();

            for (; index < lst.Count; )
            {
                BattleRecordTable battleRecord = new BattleRecordTable();
                battleRecord.ReadText(lst, ref index);
                this.m_lstBattleRecord.Add(battleRecord);
            }
        }
        catch (System.Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "战绩表错误");
        }

    }

    /// <summary>
    /// 获取所有表 
    /// </summary>
    /// <returns></returns>
    public List<BattleRecordTable> GetAll()
    {
        return new List<BattleRecordTable>(this.m_lstBattleRecord);
    }

    /// <summary>
    /// 获取指定战绩表
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BattleRecordTable GetBattleRecordTable(int id)
    {
        foreach (BattleRecordTable item in this.m_lstBattleRecord)
        {
            if (item.Id == id)
            {
                return item;
            }
        }

        return null;
    }
}

