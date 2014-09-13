using System;
using System.Collections.Generic;
using UnityEngine;

//  Building.cs
//  Author: Lu Zexi
//  2013-11-14


/// <summary>
/// 建筑类型
/// </summary>
public enum BUILDING_TYPE
{ 
    EQUIP = 1,  //装备合成建筑
    ITEM = 2,   //消耗品合成建筑
    BUILD = 3,  //建筑升级建筑
    STORAGE = 4,    //仓库
    SHAN = 5,   //山建筑
    CHUAN = 6,  //川建筑
    TIAN = 7,   //田建筑
    LIN = 8 //林建筑
}


/// <summary>
/// 建筑
/// </summary>
public class Building
{
    public BUILDING_TYPE m_eType; //类型(1:装备合成建筑,2:消耗品合成建筑,3:建筑强化建筑,4:仓库建筑,5:收集体山，6:收集体川,7:收集体田,8:收集体森林)
    public string m_strName;    //名字
    public string m_strDesc; //描述
    public int m_iLevel;    //当前等级
    public int m_iExp;  //经验
    public int m_iCollectNum;   //剩余数量
    public long m_lCollectTime; //上一次时间，服务器返回时间

    public Building(BUILDING_TYPE type, int level, int exp)
    {
        this.m_eType = type;
        BuildingTable table = BuildingTableManager.GetInstance().GetBuildingTable((int)type);

        if (table == null)
        {
            Debug.LogError("Building table is null");
            return;
        }

        this.m_strName = table.Name;
        this.m_strDesc = table.Desc;

        this.m_iExp = exp;
        this.m_iLevel = level;
    }
}

