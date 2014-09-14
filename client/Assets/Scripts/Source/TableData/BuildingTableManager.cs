using System;
using System.Collections.Generic;
using Game.Base;
using UnityEngine;
using Game.Resource;

//  BuildingTableManager.cs
//  Author: Lu Zexi
//  2013-11-26


/// <summary>
/// 建筑配置表管理类
/// </summary>
public class BuildingTableManager : Singleton<BuildingTableManager>
{
    private List<BuildingTable> m_lstBuildingTable = new List<BuildingTable>(); //建筑表
    private List<BuildingEquipTable> m_lstEquipTable = new List<BuildingEquipTable>();  //装备建筑升级表
    private List<BuildingItemTable> m_lstItemTable = new List<BuildingItemTable>(); //物品建筑升级表
    private List<BuildingShanTable> m_lstShanTable = new List<BuildingShanTable>(); //山建筑升级表
    private List<BuildingChuanTable> m_lstChuanTable = new List<BuildingChuanTable>();  //川建筑升级表
    private List<BuildingTianTable> m_lstTianTable = new List<BuildingTianTable>(); //田建筑升级表
    private List<BuildingLinTable> m_lstLinTable = new List<BuildingLinTable>();    //林建筑升级表

    public BuildingTableManager()
    {
#if GAME_TEST_LOAD
        //建筑表
        LoadTextBuilding((string)ResourceMgr.Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.BUILDING_PATH));

        //建筑川表
        LoadTextChuan(ResourceMgr.Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.BUILDING_CHUAN_PATH) as string);

        //建筑装备表
        LoadTextEquip(ResourceMgr.Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.BUILDING_EQUIP_PATH) as string);

        //建筑物品表
        LoadTextItem(ResourceMgr.Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.BUILDING_ITEM_PATH) as string);

        //建筑林表
        LoadTextLin(ResourceMgr.Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.BUILDING_LIN_PATH) as string);

        //建筑山表
        LoadTextShan(ResourceMgr.Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.BUILDING_SHAN_PATH) as string);

        //建筑田表
        LoadTextTian(ResourceMgr.Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.BUILDING_TIAN_PATH) as string);
#endif
    }


    /// <summary>
    /// 获得建筑最大等级
    /// </summary>
    /// <param name="buildType"></param>
    /// <returns></returns>
    public int GetBuildingMaxLevel(BUILDING_TYPE buildType)
    {
        int lv = -1;
        switch (buildType)
        {
            case BUILDING_TYPE.EQUIP: lv = m_lstEquipTable.Count;
                break;
            case BUILDING_TYPE.ITEM: lv = m_lstItemTable.Count;
                break;
            case BUILDING_TYPE.SHAN: lv = m_lstShanTable.Count;
                break;
            case BUILDING_TYPE.CHUAN: lv = m_lstChuanTable.Count;
                break;
            case BUILDING_TYPE.TIAN: lv = m_lstTianTable.Count;
                break;
            case BUILDING_TYPE.LIN: lv = m_lstLinTable.Count;
                break;
            default:
                break;
        }

        return lv;
    }

    /// <summary>
    /// 获取当前等级升级到下一等级需要的经验值
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public int GetBuildingExp(BUILDING_TYPE buildType, int level)
    {
        if (level >= GetBuildingMaxLevel(buildType))
        {
            return -1;
        }
        switch (buildType)
        {
            case BUILDING_TYPE.EQUIP:
                for (int i = 0; i < this.m_lstEquipTable.Count; i++)
                {
                    if (this.m_lstEquipTable[i].Level == level)
                    {
                        return this.m_lstEquipTable[i + 1].Exp;
                    }
                }
                break;
            case BUILDING_TYPE.ITEM:
                for (int i = 0; i < this.m_lstItemTable.Count; i++)
                {
                    if (this.m_lstItemTable[i].Level == level)
                    {
                        return this.m_lstItemTable[i + 1].Exp;
                    }
                }
                break;
            case BUILDING_TYPE.SHAN:
                for (int i = 0; i < this.m_lstShanTable.Count; i++)
                {
                    if (this.m_lstShanTable[i].Level == level)
                    {
                        return this.m_lstShanTable[i + 1].Exp;
                    }
                }
                break;
            case BUILDING_TYPE.CHUAN:
                for (int i = 0; i < this.m_lstChuanTable.Count; i++)
                {
                    if (this.m_lstChuanTable[i].Level == level)
                    {
                        return this.m_lstChuanTable[i + 1].Exp;
                    }
                }
                break;
            case BUILDING_TYPE.TIAN:
                for (int i = 0; i < this.m_lstTianTable.Count; i++)
                {
                    if (this.m_lstTianTable[i].Level == level)
                    {
                        return this.m_lstTianTable[i + 1].Exp;
                    }
                }
                break;
            case BUILDING_TYPE.LIN:
                for (int i = 0; i < this.m_lstLinTable.Count; i++)
                {
                    if (this.m_lstLinTable[i].Level == level)
                    {
                        return this.m_lstLinTable[i + 1].Exp;
                    }
                }
                break;
            default:
                break;
        }
        return -1;
    }


    /// <summary>
    /// 获取当前等级升级到下一等级的提示信息
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public string GetBuildingNextInfo(BUILDING_TYPE buildType, int level)
    {
        if (level >= GetBuildingMaxLevel(buildType))
        {
            return "已满级";
        }
        switch (buildType)
        {
            case BUILDING_TYPE.EQUIP:
                for (int i = 0; i < this.m_lstEquipTable.Count; i++)
                {
                    if (this.m_lstEquipTable[i].Level == level)
                    {
                        return this.m_lstEquipTable[i + 1].Desc;
                    }
                }
                break;
            case BUILDING_TYPE.ITEM:
                for (int i = 0; i < this.m_lstItemTable.Count; i++)
                {
                    if (this.m_lstItemTable[i].Level == level)
                    {
                        return this.m_lstItemTable[i + 1].Desc;
                    }
                }
                break;
            case BUILDING_TYPE.SHAN:
                for (int i = 0; i < this.m_lstShanTable.Count; i++)
                {
                    if (this.m_lstShanTable[i].Level == level)
                    {
                        return this.m_lstShanTable[i + 1].Desc;
                    }
                }
                break;
            case BUILDING_TYPE.CHUAN:
                for (int i = 0; i < this.m_lstChuanTable.Count; i++)
                {
                    if (this.m_lstChuanTable[i].Level == level)
                    {
                        return this.m_lstChuanTable[i + 1].Desc;
                    }
                }
                break;
            case BUILDING_TYPE.TIAN:
                for (int i = 0; i < this.m_lstTianTable.Count; i++)
                {
                    if (this.m_lstTianTable[i].Level == level)
                    {
                        return this.m_lstTianTable[i + 1].Desc;
                    }
                }
                break;
            case BUILDING_TYPE.LIN:
                for (int i = 0; i < this.m_lstLinTable.Count; i++)
                {
                    if (this.m_lstLinTable[i].Level == level)
                    {
                        return this.m_lstLinTable[i + 1].Desc;
                    }
                }
                break;
            default:
                break;
        }
        return "已满级";
    }

    /// <summary>
    /// 获取建筑表
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BuildingTable GetBuildingTable(int type)
    {
        foreach (BuildingTable item in this.m_lstBuildingTable)
        {
            if (item.Type == type)
            {
                return item;
            }
        }
        return null;
    }

    /// <summary>
    /// 用等级获取需要的配置表
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BuildingEquipTable GetBuildingEquipTable(int level)
    {
        if (level > this.m_lstEquipTable.Count || level < 1)
        {
            return null;
        }
        return this.m_lstEquipTable[level-1];
    }

    /// <summary>
    /// 根据等级获取装备合成物品ID
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public List<int> GetBuildingEquipItem(int level)
    {
        List<int> lst = new List<int>();
        level--;
        if (level >= this.m_lstEquipTable.Count)
        {
            level = this.m_lstEquipTable.Count - 1;
        }
        if (level < 0)
        {
            level = 0;
        }


        for (int i = 0; i <= level; i++)
        {
            BuildingEquipTable table = this.m_lstEquipTable[i];
            for (int j = 0; j < table.VecItemTableID.Length; j++)
            {
                if (table.VecItemTableID[j] > 0)
                {
                    lst.Add(table.VecItemTableID[j]);
                }
            }
        }

        return lst;
    }

    /// <summary>
    /// 获取建筑升级解锁的新物品
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public List<int> GetLastBuildEquipItem(int level, int dis)
    {
        if (level < dis)
            return new List<int>();
        List<int> lst = new List<int>();
        for (int i = 0; i < dis; ++i)
        {
            level -= i + 1;
            if (level >= this.m_lstEquipTable.Count)
            {
                level = this.m_lstEquipTable.Count - 1;
            }
            if (level < 0)
            {
                level = 0;
            }

            BuildingEquipTable table = this.m_lstEquipTable[level];
            for (int j = 0; j < table.VecItemTableID.Length; j++)
            {
                if (table.VecItemTableID[j] > 0)
                {
                    lst.Add(table.VecItemTableID[j]);
                }
            }
        }
            
        

        return lst;
    }

    /// <summary>
    /// 获取指定等级的物品建筑升级表
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public BuildingItemTable GetBuildingItemTable(int level)
    {
        if (level > this.m_lstItemTable.Count || level < 1)
        {
            return null;
        }
        return this.m_lstItemTable[level - 1];
    }

    /// <summary>
    /// 获取指定等级的物品建筑物品量
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public List<int> GetBuildingItemItem(int level)
    {
        List<int> lst = new List<int>();
        level--;
        if (level >= this.m_lstItemTable.Count)
        {
            level = this.m_lstItemTable.Count - 1;
        }
        if (level < 0)
        {
            level = 0;
        }


        for (int i = 0; i <= level; i++)
        {
            BuildingItemTable table = this.m_lstItemTable[i];
            for (int j = 0; j < table.VecItemTableID.Length; j++)
            {
                if (table.VecItemTableID[j] > 0)
                {
                    lst.Add(table.VecItemTableID[j]);
                }
            }
        }

        return lst;
    }

    /// <summary>
    /// 获取建筑升级解锁的消耗品
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public List<int> GetLastBuildItem(int level, int dis)
    {
        if (level < dis)
            return new List<int>();
        List<int> lst = new List<int>();
        for (int i = 0; i < dis; ++i)
        {
            level -= i + 1;
            if (level >= this.m_lstItemTable.Count)
            {
                level = this.m_lstItemTable.Count - 1;
            }
            if (level < 0)
            {
                level = 0;
            }


            BuildingItemTable table = this.m_lstItemTable[level];
            for (int j = 0; j < table.VecItemTableID.Length; j++)
            {
                if (table.VecItemTableID[j] > 0)
                {
                    lst.Add(table.VecItemTableID[j]);
                }
            }
        }
        


        return lst;
    }

    /// <summary>
    /// 获取指定的建筑山配置表
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public BuildingShanTable GetShanTable(int level)
    {
        if (level > this.m_lstShanTable.Count || level < 1)
        {
            return null;
        }
        return this.m_lstShanTable[level - 1];
    }

    /// <summary>
    /// 获取指定等级的建筑山物品列表
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public List<int> GetShanItem(int level)
    {
        List<int> lst = new List<int>();
        level--;
        if (level >= this.m_lstShanTable.Count)
        {
            level = this.m_lstShanTable.Count - 1;
        }
        if (level<0)
        {
            level = 0;
        }

        for (int i = 0; i <= level; i++)
        {
            BuildingShanTable table = this.m_lstShanTable[i];
            for (int j = 0; j < table.VecItemTableID.Length; j++)
            {
                if (table.VecItemTableID[j] > 0)
                {
                    lst.Add(table.VecItemTableID[j]);
                }
            }
        }

        return lst;
    }

    /// <summary>
    /// 获取指定等级的建筑山物品列表，列表序列完全与物品 一 一 对 应 ；
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public List<int> GetShanItemWeight(int level)
    {
        List<int> lst = new List<int>();
        level--;
        if (level >= this.m_lstShanTable.Count)
        {
            level = this.m_lstShanTable.Count - 1;
        }
        if (level < 0)
        {
            level = 0;
        }

        for (int i = 0; i <= level; i++)
        {
            BuildingShanTable table = this.m_lstShanTable[i];
            for (int j = 0; j < table.VecItemTableID.Length; j++)
            {
                if (table.VecItemTableID[j] > 0)
                {
                    lst.Add(table.VecItemWeight[j]);
                }
            }
        }

        return lst;
    }

    /// <summary>
    /// 获取指定等级的建筑山的金币农场点砖石数量
    /// </summary>
    /// <returns></returns>
    public List<int> GetShanGoldFarmDiamond(int level)
    {
        List<int> lst = new List<int>();
        level--;
        if (level >= this.m_lstShanTable.Count)
        {
            level = this.m_lstShanTable.Count - 1;
        }
        if (level < 0)
        {
            level = 0;
        }

        BuildingShanTable table = this.m_lstShanTable[level];
        lst.Add(table.Gold);
        lst.Add(table.FarmPoint);
        lst.Add(table.Diamond);

        return lst;
    }

    /// <summary>
    /// 获取指定等级的建筑山的金币农场点砖石权重
    /// </summary>
    /// <returns></returns>
    public List<int> GetShanGoldFarmDiamondWeight(int level)
    {
        List<int> lst = new List<int>();
        level--;
        if (level >= this.m_lstShanTable.Count)
        {
            level = this.m_lstShanTable.Count - 1;
        }
        if (level < 0)
        {
            level = 0;
        }

        BuildingShanTable table = this.m_lstShanTable[level];
        lst.Add(table.GoldWeight);
        lst.Add(table.FarmPointWeight);
        lst.Add(table.DiamondWeight);

        return lst;
    }

    /// <summary>
    /// 获取指定等级的建筑林升级表
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public BuildingLinTable GetLinTable(int level)
    {
        if (level > this.m_lstLinTable.Count || level < 1)
        {
            return null;
        }
        return this.m_lstLinTable[level - 1];
    }

    /// <summary>
    /// 获取指定等级的建筑林物品列表
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public List<int> GetLinItem(int level)
    {
        List<int> lst = new List<int>();
        level--;
        if (level >= this.m_lstLinTable.Count)
        {
            level = this.m_lstLinTable.Count - 1;
        }
        if (level < 0)
        {
            level = 0;
        }

        for (int i = 0; i <= level; i++)
        {
            BuildingLinTable table = this.m_lstLinTable[i];
            for (int j = 0; j < table.VecItemTableID.Length; j++)
            {
                if (table.VecItemTableID[j] > 0)
                {
                    lst.Add(table.VecItemTableID[j]);
                }
            }
        }
        return lst;
    }

    /// <summary>
    /// 获取指定等级的建筑林物品列表权重，列表序列完全与物品 一 一 对 应 ；
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public List<int> GetLinItemWeight(int level)
    {
        List<int> lst = new List<int>();
        level--;
        if (level >= this.m_lstLinTable.Count)
        {
            level = this.m_lstLinTable.Count - 1;
        }
        if (level < 0)
        {
            level = 0;
        }

        for (int i = 0; i <= level; i++)
        {
            BuildingLinTable table = this.m_lstLinTable[i];
            for (int j = 0; j < table.VecItemTableID.Length; j++)
            {
                if (table.VecItemTableID[j] > 0)
                {
                    lst.Add(table.VecItemWeight[j]);
                }
            }
        }
        return lst;
    }

    /// <summary>
    /// 获取指定等级的建筑林的金币农场点砖石数量
    /// </summary>
    /// <returns></returns>
    public List<int> GetLinGoldFarmDiamond(int level)
    {
        List<int> lst = new List<int>();
        level--;
        if (level >= this.m_lstLinTable.Count)
        {
            level = this.m_lstLinTable.Count - 1;
        }
        if (level < 0)
        {
            level = 0;
        }

        BuildingLinTable table = this.m_lstLinTable[level];
        lst.Add(table.Gold);
        lst.Add(table.FarmPoint);
        lst.Add(table.Diamond);

        return lst;
    }

    /// <summary>
    /// 获取指定等级的建筑林的金币农场点砖石权重
    /// </summary>
    /// <returns></returns>
    public List<int> GetLinGoldFarmDiamondWeight(int level)
    {
        List<int> lst = new List<int>();
        level--;
        if (level >= this.m_lstLinTable.Count)
        {
            level = this.m_lstLinTable.Count - 1;
        }
        if (level < 0)
        {
            level = 0;
        }

        BuildingLinTable table = this.m_lstLinTable[level];
        lst.Add(table.GoldWeight);
        lst.Add(table.FarmPointWeight);
        lst.Add(table.DiamondWeight);

        return lst;
    }

    /// <summary>
    /// 获取指定等级的建筑川升级表
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public BuildingChuanTable GetChuanTable(int level)
    {
        if (level > this.m_lstChuanTable.Count || level < 1)
        {
            return null;
        }
        return this.m_lstChuanTable[level - 1];
    }

    /// <summary>
    /// 获取指定等级的建筑川物品列表
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public List<int> GetChuanItem(int level)
    {
        List<int> lst = new List<int>();
        level--;
        if (level >= this.m_lstChuanTable.Count)
        {
            level = this.m_lstChuanTable.Count - 1;
        }
        if (level < 0)
        {
            level = 0;
        }

        for (int i = 0; i <= level; i++)
        {
            BuildingChuanTable table = this.m_lstChuanTable[i];
            for (int j = 0; j < table.VecItemTableID.Length; j++)
            {
                if (table.VecItemTableID[j] > 0)
                {
                    lst.Add(table.VecItemTableID[j]);
                }
            }
        }

        return lst;
    }

    /// <summary>
    /// 获取指定等级的建筑川物品列表权重，序列完全与物品 一 一 对 应 ；
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public List<int> GetChuanItemWeight(int level)
    {
        List<int> lst = new List<int>();
        level--;
        if (level >= this.m_lstChuanTable.Count)
        {
            level = this.m_lstChuanTable.Count - 1;
        }
        if (level < 0)
        {
            level = 0;
        }

        for (int i = 0; i <= level; i++)
        {
            BuildingChuanTable table = this.m_lstChuanTable[i];

            for (int j = 0; j < table.VecItemTableID.Length; j++)
            {
                if (table.VecItemTableID[j] > 0)
                {
                    lst.Add(table.VecItemWeight[j]);
                }
            }

        }

        return lst;
    }

    /// <summary>
    /// 获取指定等级的建筑川的金币农场点砖石数量
    /// </summary>
    /// <returns></returns>
    public List<int> GetChuanGoldFarmDiamond(int level)
    {
        List<int> lst = new List<int>();
        level--;
        if (level >= this.m_lstChuanTable.Count)
        {
            level = this.m_lstChuanTable.Count - 1;
        }
        if (level < 0)
        {
            level = 0;
        }

        BuildingChuanTable table = this.m_lstChuanTable[level];
        lst.Add(table.Gold);
        lst.Add(table.FarmPoint);
        lst.Add(table.Diamond);

        return lst;
    }

    /// <summary>
    /// 获取指定等级的建筑川的金币农场点砖石权重
    /// </summary>
    /// <returns></returns>
    public List<int> GetChuanGoldFarmDiamondWeight(int level)
    {
        List<int> lst = new List<int>();
        level--;
        if (level >= this.m_lstChuanTable.Count)
        {
            level = this.m_lstChuanTable.Count - 1;
        }
        if (level < 0)
        {
            level = 0;
        }

        BuildingChuanTable table = this.m_lstChuanTable[level];
        lst.Add(table.GoldWeight);
        lst.Add(table.FarmPointWeight);
        lst.Add(table.DiamondWeight);

        return lst;
    }

    /// <summary>
    /// 获取指定等级的建筑田升级表
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public BuildingTianTable GetTianTable(int level)
    {
        if (level > this.m_lstTianTable.Count || level < 1)
        {
            return null;
        }
        return this.m_lstTianTable[level - 1];
    }

    /// <summary>
    /// 获取指定等级建筑田物品列表
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public List<int> GetTianItem(int level)
    {
        List<int> lst = new List<int>();
        level--;
        if (level >= this.m_lstTianTable.Count)
        {
            level = this.m_lstTianTable.Count - 1;
        }
        if (level < 0)
        {
            level = 0;
        }

        for (int i = 0; i <= level; i++)
        {
            BuildingTianTable table = this.m_lstTianTable[i];
            for (int j = 0; j < table.VecItemTableID.Length; j++)
            {
                if (table.VecItemTableID[j] > 0)
                {
                    lst.Add(table.VecItemTableID[j]);
                }
            }
        }

        return lst;
    }

    /// <summary>
    /// 获取指定等级建筑田物品列表权重，列表序列与物品完全 一 一 对 应 ；
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public List<int> GetTianItemWeight(int level)
    {
        List<int> lst = new List<int>();
        level--;
        if (level >= this.m_lstTianTable.Count)
        {
            level = this.m_lstTianTable.Count - 1;
        }
        if (level < 0)
        {
            level = 0;
        }

        for (int i = 0; i <= level; i++)
        {
            BuildingTianTable table = this.m_lstTianTable[i];
            for (int j = 0; j < table.VecItemTableID.Length; j++)
            {
                if (table.VecItemTableID[j] > 0)
                {
                    lst.Add(table.VecItemWeight[j]);
                }
            }
        }

        return lst;
    }

    /// <summary>
    /// 获取指定等级的建筑田的金币农场点砖石数量
    /// </summary>
    /// <returns></returns>
    public List<int> GetTianGoldFarmDiamond(int level)
    {
        List<int> lst = new List<int>();
        level--;
        if (level >= this.m_lstTianTable.Count)
        {
            level = this.m_lstTianTable.Count - 1;
        }
        if (level < 0)
        {
            level = 0;
        }

        BuildingTianTable table = this.m_lstTianTable[level];
        lst.Add(table.Gold);
        lst.Add(table.FarmPoint);
        lst.Add(table.Diamond);

        return lst;
    }

    /// <summary>
    /// 获取指定等级的建筑田的金币农场点砖石权重
    /// </summary>
    /// <returns></returns>
    public List<int> GetTianGoldFarmDiamondWeight(int level)
    {
        List<int> lst = new List<int>();
        level--;
        if (level >= this.m_lstTianTable.Count)
        {
            level = this.m_lstTianTable.Count - 1;
        }
        if (level < 0)
        {
            level = 0;
        }

        BuildingTianTable table = this.m_lstTianTable[level];
        lst.Add(table.GoldWeight);
        lst.Add(table.FarmPointWeight);
        lst.Add(table.DiamondWeight);

        return lst;
    }

    /// <summary>
    /// 加载文本数据
    /// </summary>
    public void LoadTextBuilding( string content )
    {
        try
        {
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;
            this.m_lstBuildingTable.Clear();
            for (; index < lst.Count; )
            {
                BuildingTable table = new BuildingTable();
                table.ReadText(lst, ref index);
                m_lstBuildingTable.Add(table);
            }
        }
        catch (Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "建筑配置表错误");
        }
    }

    /// <summary>
    /// 加载装备建筑升级表
    /// </summary>
    /// <param name="content"></param>
    public void LoadTextEquip( string content )
    {
        try
        {
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;
            this.m_lstEquipTable.Clear();
            for (; index < lst.Count; )
            {
                BuildingEquipTable table = new BuildingEquipTable();
                table.ReadText(lst, ref index);
                m_lstEquipTable.Add(table);
            }
        }
        catch (Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "装备建筑配置表错误");
        }
    }

    /// <summary>
    /// 加载物品建筑升级表
    /// </summary>
    /// <param name="content"></param>
    public void LoadTextItem(string content)
    {
        try
        {
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;
            this.m_lstItemTable.Clear();
            for (; index < lst.Count; )
            {
                BuildingItemTable table = new BuildingItemTable();
                table.ReadText(lst, ref index);
                m_lstItemTable.Add(table);
            }
        }
        catch (Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "物品建筑配置表错误");
        }
    }

    /// <summary>
    /// 加载山建筑升级表
    /// </summary>
    /// <param name="content"></param>
    public void LoadTextShan(string content)
    {
        try
        {
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;
            this.m_lstShanTable.Clear();
            for (; index < lst.Count; )
            {
                BuildingShanTable table = new BuildingShanTable();
                table.ReadText(lst, ref index);
                m_lstShanTable.Add(table);
            }
        }
        catch (Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "建筑山配置表错误");
        }
    }

    /// <summary>
    /// 加载川建筑升级表
    /// </summary>
    /// <param name="content"></param>
    public void LoadTextChuan(string content)
    {
        try
        {
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;
            this.m_lstChuanTable.Clear();
            for (; index < lst.Count; )
            {
                BuildingChuanTable table = new BuildingChuanTable();
                table.ReadText(lst, ref index);
                m_lstChuanTable.Add(table);
            }
        }
        catch (Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "建筑川配置表错误");
        }
    }

    /// <summary>
    /// 加载田建筑升级表
    /// </summary>
    /// <param name="content"></param>
    public void LoadTextTian(string content)
    {
        try
        {
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;
            this.m_lstTianTable.Clear();
            for (; index < lst.Count; )
            {
                BuildingTianTable table = new BuildingTianTable();
                table.ReadText(lst, ref index);
                m_lstTianTable.Add(table);
            }
        }
        catch (Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "建筑田配置表错误");
        }
    }

    /// <summary>
    /// 加载林建筑升级表
    /// </summary>
    /// <param name="content"></param>
    public void LoadTextLin(string content)
    {
        try
        {
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;
            this.m_lstLinTable.Clear();
            for (; index < lst.Count; )
            {
                BuildingLinTable table = new BuildingLinTable();
                table.ReadText(lst, ref index);
                m_lstLinTable.Add(table);
            }
        }
        catch (Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "建筑林配置表错误");
        }
    }

}

