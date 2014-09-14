using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Resource;
using UnityEngine;

//  ActivityTableManager.cs
//  Author: Lu Zexi
//  2014-01-02

/// <summary>
/// 活动表管理类
/// </summary>
public class ActivityTableManager : Singleton<ActivityTableManager>
{
    private List<ActivityDungeonTable> m_lstDungeonTable = new List<ActivityDungeonTable>();    //活动副本
    private List<ActivityGateTable> m_lstGateTable = new List<ActivityGateTable>(); //活动关卡
    private List<ActivityMonsterTable> m_lstMonsterTable = new List<ActivityMonsterTable>();    //活动怪物
    private List<ActivityMonsterTeamTable> m_lstMonsterTeam = new List<ActivityMonsterTeamTable>(); //活动怪物组

    private List<FAV_TYPE> m_lstDungeonFavType = new List<FAV_TYPE>();//活动副本优惠类型

    public ActivityTableManager()
    {
#if GAME_TEST_LOAD
        //副本表
        LoadDungeonTable((string)ResourceMgr.Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.ACTIVITY_DUNGEON_PATH));

        //关卡表
        LoadGateTable((string)ResourceMgr.Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.ACTIVITY_GATE_PATH));

        //怪物表
        LoadMonsterTable((string)ResourceMgr.Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.ACTIVITY_MONSTER_PATH));

        //怪物编队表
        LoadMonsterTeamTable((string)ResourceMgr.Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.ACTIVITY_MONSTER_TEAM_PATH));
#endif

    }

    /// <summary>
    /// 获取所有活动副本
    /// </summary>
    /// <returns></returns>
    public List<ActivityDungeonTable> GetAllDungeon()
    {
        return new List<ActivityDungeonTable>(this.m_lstDungeonTable);
    }

    /// <summary>
    /// 获取副本表
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ActivityDungeonTable GetDungeonTable(int id)
    {
        foreach (ActivityDungeonTable item in this.m_lstDungeonTable)
        {
            if (item.ID == id)
            {
                return item;
            }
        }
        return null;
    }

    /// <summary>
    /// 获取关卡数据表
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ActivityGateTable GetGateTable(int id)
    {
        foreach (ActivityGateTable item in this.m_lstGateTable)
        {
            if (item.ID == id)
            {
                return item;
            }
        }
        return null;
    }

    /// <summary>
    /// 清除副本优惠列表
    /// </summary>
    public void ClearActivityDungeonFavType()
    {
        this.m_lstDungeonFavType.Clear();
    }

    /// <summary>
    /// 添加活动副本优惠类型
    /// </summary>
    /// <param name="type"></param>
    public void AddActivityDungeonFavType(FAV_TYPE type)
    {
        this.m_lstDungeonFavType.Add(type);
    }

    /// <summary>
    /// 获取活动副本优惠类型
    /// </summary>
    /// <returns></returns>
    public List<FAV_TYPE> GetActivityDungeonFavType()
    {
        return new List<FAV_TYPE>(this.m_lstDungeonFavType);
    }

    /// <summary>
    /// 获取所有关卡
    /// </summary>
    /// <param name="dungeonid"></param>
    /// <returns></returns>
    public List<ActivityGateTable> GetAllGate(int dungeonid)
    {
        ActivityDungeonTable table = ActivityTableManager.GetInstance().GetDungeonTable(dungeonid);
        List<ActivityGateTable> lst = new List<ActivityGateTable>();
        for (int i = 0; i < table.VecGate.Length; i++)
        {
            if (table.VecGate[i] > 0)
            {
                ActivityGateTable gateTable = ActivityTableManager.GetInstance().GetGateTable(table.VecGate[i]);
                if (gateTable == null)
                {
                    GAME_LOG.ERROR("The ActivityGateTable is null.");
                    continue;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   
                }

                lst.Add(gateTable);
            }
        }
        return lst;
    }
    /// <summary>
    /// 获取怪物
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ActivityMonsterTable GetMonsterTable(int gateid, int orderid)
    {
        foreach (ActivityMonsterTable item in this.m_lstMonsterTable)
        {
            if (item.GateID == gateid && item.OrderID == orderid)
            {
                return item;
            }
        }

        return null;
    }

    /// <summary>
    /// 获取指定怪物组
    /// </summary>
    /// <param name="gateid"></param>
    /// <param name="order"></param>
    /// <returns></returns>
    public ActivityMonsterTeamTable GetMonsterTeamTable(int gateid, int orderid , List<int> order )
    {
        if (orderid <= 0)
        {
            List<ActivityMonsterTeamTable> lst = new List<ActivityMonsterTeamTable>();
            foreach (ActivityMonsterTeamTable item in this.m_lstMonsterTeam)
            {
                if (item.GateID == gateid && !item.Fix)
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
            foreach (ActivityMonsterTeamTable item in this.m_lstMonsterTeam)
            {
                if (item.GateID == gateid && item.OrderID == orderid)
                {
                    return item;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// 加载副本表
    /// </summary>
    /// <param name="content"></param>
    public void LoadDungeonTable(string content)
    {
        try
        {
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;
            this.m_lstDungeonTable.Clear();

            for (; index < lst.Count; )
            {
                ActivityDungeonTable table = new ActivityDungeonTable();
                table.ReadText(lst, ref index);
                this.m_lstDungeonTable.Add(table);
            }
        }
        catch (Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "活动副本配置表错误");
            Debug.LogError(ex.StackTrace);
        }
    }

    /// <summary>
    /// 加载关卡表
    /// </summary>
    /// <param name="content"></param>
    public void LoadGateTable(string content)
    {
        try
        {
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;
            this.m_lstGateTable.Clear();

            for (; index < lst.Count; )
            {
                ActivityGateTable table = new ActivityGateTable();
                table.ReadText(lst, ref index);
                this.m_lstGateTable.Add(table);
            }
        }
        catch (Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "活动关卡配置表错误");
            Debug.LogError(ex.StackTrace);
        }
    }

    /// <summary>
    /// 加载怪物表
    /// </summary>
    /// <param name="content"></param>
    public void LoadMonsterTable(string content)
    {
        try
        {
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;
            this.m_lstMonsterTable.Clear();

            for (; index < lst.Count; )
            {
                ActivityMonsterTable table = new ActivityMonsterTable();
                table.ReadText(lst, ref index);
                this.m_lstMonsterTable.Add(table);
            }
        }
        catch (Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "活动怪物配置表错误");
            Debug.LogError(ex.StackTrace);
        }
    }

    /// <summary>
    /// 加载活动怪物组
    /// </summary>
    /// <param name="content"></param>
    public void LoadMonsterTeamTable( string content )
    {
        try
        {
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;
            this.m_lstMonsterTeam.Clear();

            for (; index < lst.Count; )
            {
                ActivityMonsterTeamTable table = new ActivityMonsterTeamTable();
                table.ReadText(lst, ref index);
                this.m_lstMonsterTeam.Add(table);
            }
        }
        catch (Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "活动怪物队列配置表错误");
            Debug.LogError(ex.StackTrace);
        }
    }
}
