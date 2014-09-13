using System;
using System.Collections.Generic;
using Game.Base;


//  WorldManager.cs
//  Author: Lu Zexi
//  2013-11-14



/// <summary>
/// 世界副本管理类
/// </summary>
public class WorldManager
{
    //当前世界
    public static int s_iCurrentWorldId;

    //当前区域
    public static int s_iCurrentAreaIndex;

    //当前副本
    public static int s_iCurrentDungeonIndex;

    //当前关卡
    public static int s_iCurrentGateIndex;

    //当前特殊副本id
    public static int s_iCurEspDungeonId;

    //当前特殊副本关卡id
    public static int s_iCurEspDungeonGateIndex;//当前特殊副本关卡索引

    public static FAV_TYPE s_eCurDungeonFavType;//当前普通副本优惠类型id

    public static FAV_TYPE s_eCurActivityDungeonFavType;//当前活动副本优惠类型id

    public static int s_iLastNewDungeonIndex;//上一次最新副本索引

    public static int s_iLastNewAreaIndex;//上一次最新区域索引

    public WorldManager()
    { 
        //
    }

    /// <summary>
    /// 获取整个世界数据
    /// </summary>
    /// <returns></returns>
    public static List<WorldTable> GetAllWorld()
    {
        return WorldTableManager.GetInstance().GetAll();
    }

    /// <summary>
    /// 获取指定世界数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static WorldTable GetWorld(int worldid)
    {
        return WorldTableManager.GetInstance().GetWorldTable(worldid);
    }

    /// <summary>
    /// 获取某个世界所有区域
    /// </summary>
    /// <returns></returns>
    public static List<AreaTable> GetAllArea( int worldid )
    {
        WorldTable table = WorldTableManager.GetInstance().GetWorldTable(worldid);
        List<AreaTable> lst = new List<AreaTable>();
        for (int i = 0; i < table.VecArea.Length; i++)
        {
            if (table.VecArea[i] > 0)
            {
                AreaTable areaTable = AreaTableManager.GetInstance().GetArea(table.VecArea[i]);
                if (areaTable == null)
                {
                    GAME_LOG.ERROR("The AreaTable is error.");
                    continue;
                }
                lst.Add(areaTable);
            }
        }
        return lst;
    }

    /// <summary>
    /// 获取指定区域
    /// </summary>
    /// <param name="areaid"></param>
    /// <returns></returns>
    public static AreaTable GetArea(int areaid)
    {
        return AreaTableManager.GetInstance().GetArea(areaid);
    }

    /// <summary>
    /// 获取指定世界中的区域
    /// </summary>
    /// <param name="areaid"></param>
    /// <returns></returns>
    public static AreaTable GetArea( int worldID , int areaIndex )
    {
        WorldTable worldTable = GetWorld(worldID);
        return GetArea(worldTable.VecArea[areaIndex]);
    }

    /// <summary>
    /// 获取指定区域的所有副本
    /// </summary>
    /// <param name="areaid"></param>
    /// <returns></returns>
    public static List<DungeonTable> GetAllDungeon(int areaid)
    {
        AreaTable table = AreaTableManager.GetInstance().GetArea(areaid);
        List<DungeonTable> lst = new List<DungeonTable>();
        for (int i = 0; i < table.VecDungeon.Length; i++)
        {
            if (table.VecDungeon[i] > 0)
            {
                DungeonTable dungeonTable = DungeonTableManager.GetInstance().GetDungeon(table.VecDungeon[i]);
                if (dungeonTable == null)
                {
                    GAME_LOG.ERROR("The dungeonTable is error.");
                    continue;
                }
                lst.Add(dungeonTable);
            }
        }
        return lst;
    }

    /// <summary>
    /// 获取指定副本表
    /// </summary>
    /// <param name="dungeonid"></param>
    /// <returns></returns>
    public static DungeonTable GetDungeonTable(int dungeonid)
    {
        return DungeonTableManager.GetInstance().GetDungeon(dungeonid);
    }

    /// <summary>
    /// 获取指定区域索引中的副本
    /// </summary>
    /// <param name="areaID"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static DungeonTable GetDungeonTable(int areaID, int index)
    {
        AreaTable areaTable = GetArea(areaID);
        return GetDungeonTable(areaTable.VecDungeon[index]);
    }

    /// <summary>
    /// 获取所有关卡
    /// </summary>
    /// <param name="dungeonid"></param>
    /// <returns></returns>
    public static List<GateTable> GetAllGate(int dungeonid)
    {
        DungeonTable table = DungeonTableManager.GetInstance().GetDungeon(dungeonid);
        List<GateTable> lst = new List<GateTable>();
        for (int i = 0; i < table.VecGate.Length; i++)
        {
            if (table.VecGate[i] > 0)
            {
                GateTable gateTable = GateTableManager.GetInstance().GetGateTable(table.VecGate[i]);
                if (gateTable == null)
                {
                    GAME_LOG.ERROR("The gateTable is null.");
                    continue;
                }
                lst.Add(gateTable);
            }
        }

        return lst;
    }

    /// <summary>
    /// 获取指定关卡数据
    /// </summary>
    /// <param name="gateid"></param>
    /// <returns></returns>
    public static GateTable GetGateTable(int gateid)
    {
        return GateTableManager.GetInstance().GetGateTable(gateid);
    }

    /// <summary>
    /// 获取指定副本中索引的关卡
    /// </summary>
    /// <param name="gateid"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static GateTable GetGateTable(int dungeonid, int index)
    {
        DungeonTable dungeonTable = GetDungeonTable(dungeonid);
        return GetGateTable(dungeonTable.VecGate[index]);
    }

    /// <summary>
    /// 获取活动副本表
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static ActivityDungeonTable GetActivityDungeonTable(int id)
    {
        return ActivityTableManager.GetInstance().GetDungeonTable(id);
    }

    /// <summary>
    /// 获取活动关卡表
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static ActivityGateTable GetActivityGateTable(int id)
    {
        return ActivityTableManager.GetInstance().GetGateTable(id);
    }

    /// <summary>
    /// 从活动副本索引获取活动关卡表
    /// </summary>
    /// <param name="id"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static ActivityGateTable GetActivityGateTable(int id, int index)
    {
        ActivityDungeonTable fuben = GetActivityDungeonTable(id);
        int gateid = fuben.VecGate[index];
        return GetActivityGateTable(gateid);
    }

}

