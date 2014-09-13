using System;
using System.Collections.Generic;
using System.Collections;
using Game.Base;
using UnityEngine;
using Game.Resource;

//  MonsterTableManager.cs
//  Author: Lu Zexi
//  2013-11-18




/// <summary>
/// 怪物配置表管理类
/// </summary>
public class MonsterTableManager : Singleton<MonsterTableManager>
{
    private List<MonsterGateTable> m_lstMonsterGate = new List<MonsterGateTable>(); //怪物列表
    private List<MonsterTeamTable> m_lstMonsterTeamTable = new List<MonsterTeamTable>();    //怪物队伍列表
    private List<MonsterBaoxiangTable> m_lstMonsterBaoxiang = new List<MonsterBaoxiangTable>(); //宝箱怪列表

    public MonsterTableManager()
    {
#if GAME_TEST_LOAD
        //怪物表
        LoadMonsterGateTable(ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.MONSTER_TABLE_PATH) as string);

        //怪物编队表
        LoadMonsterTeamTable(ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.MONSTER_TEAM_TABLE_PATH) as string);

        //特殊怪物表
        LoadMonsterBaoxiangTable(ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.SP_MONSTER_TABLE_PATH) as string);
#endif
    }

    /// <summary>
    /// 读取怪物表文本
    /// </summary>
    public void LoadMonsterGateTable(string content )
    {
        try
        {
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;
            this.m_lstMonsterGate.Clear();

            for (; index < lst.Count; )
            {
                MonsterGateTable table = new MonsterGateTable();
                table.ReadText(lst, ref index);
                this.m_lstMonsterGate.Add(table);
            }
        }
        catch (System.Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "敌人配置表错误");
        }
    }

    /// <summary>
    /// 读取怪物团文本
    /// </summary>
    /// <param name="lst"></param>
    public void LoadMonsterTeamTable(string content )
    {
        try
        {
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;
            this.m_lstMonsterTeamTable.Clear();

            for (; index < lst.Count; )
            {
                MonsterTeamTable table = new MonsterTeamTable();
                table.ReadText(lst, ref index);
                this.m_lstMonsterTeamTable.Add(table);
            }
        }
        catch (System.Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "敌人队列配置表错误");
        }
    }

    /// <summary>
    /// 读取特殊怪物
    /// </summary>
    /// <param name="content"></param>
    public void LoadMonsterBaoxiangTable( string content )
    {
        try
        {
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;
            this.m_lstMonsterBaoxiang.Clear();

            for (; index < lst.Count; )
            {
                MonsterBaoxiangTable table = new MonsterBaoxiangTable();
                table.ReadText(lst, ref index);
                this.m_lstMonsterBaoxiang.Add(table);
            }
        }
        catch (System.Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, " 宝箱怪配置表错误");
        }
    }

    /// <summary>
    /// 获取特殊怪物
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public MonsterBaoxiangTable GetMonsterBaoxiangTable(int id)
    {
        foreach (MonsterBaoxiangTable item in this.m_lstMonsterBaoxiang)
        {
            if (item.ID == id)
                return item;
        }
        return null;
    }

    /// <summary>
    /// 获取所有怪物配置表
    /// </summary>
    /// <returns></returns>
    public List<MonsterGateTable> GetAllMonsterGateTable()
    {
        return new List<MonsterGateTable>(this.m_lstMonsterGate);
    }

    /// <summary>
    /// 获取所有怪物队伍表
    /// </summary>
    /// <returns></returns>
    public List<MonsterTeamTable> GetAllMonsterTeamTable()
    {
        return new List<MonsterTeamTable>(this.m_lstMonsterTeamTable);
    }


    /// <summary>
    /// 获取怪物
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public MonsterGateTable GetMonsterTable(int gateid, int orderid)
    {
        foreach (MonsterGateTable item in this.m_lstMonsterGate)
        {
            if (item.GateID == gateid && item.OrderID == orderid)
            {
                return item;
            }
        }

        return null;
    }

    /// <summary>
    /// 获取怪物队伍表
    /// </summary>
    /// <param name="gateid"></param>
    /// <param name="orderid"></param>
    /// <returns></returns>
    public MonsterTeamTable GetMonsterTeamTable( int gateid , int orderid , List<int> order )
    {
        if (orderid <= 0)
        {
            List<MonsterTeamTable> lst = new List<MonsterTeamTable>();
            foreach (MonsterTeamTable item in this.m_lstMonsterTeamTable)
            {
                if (item.GateID == gateid && !item.Fix )
                {
                    if (!order.Contains(orderid))
                        lst.Add(item);
                }
            }
            int index = GAME_FUNCTION.RANDOM(0, lst.Count);
            return lst[index];
        }
        else
        {
            foreach (MonsterTeamTable item in this.m_lstMonsterTeamTable)
            {
                if (item.GateID == gateid && item.OrderID == orderid)
                {
                    return item;
                }
            }
        }
        return null;
    }
}
