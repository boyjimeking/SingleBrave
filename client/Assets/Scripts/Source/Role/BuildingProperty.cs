using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  BuildingProperty.cs
//  Author: Lu Zexi
//  2013-11-14



/// <summary>
/// 建筑属性类
/// </summary>
public class BuildingProperty
{
    private List<Building> m_lstBuilding = new List<Building>(); //建筑列表

    public BuildingProperty()
    {
        //Building build1 = new Building(BUILDING_TYPE.BUILD, 1, 1);
        //Building build2 = new Building(BUILDING_TYPE.CHUAN, 1, 1);
        //Building build3 = new Building(BUILDING_TYPE.EQUIP, 1, 1);
        //Building build4 = new Building(BUILDING_TYPE.ITEM, 1, 1);
        //Building build5 = new Building(BUILDING_TYPE.LIN, 1, 1);
        //Building build6 = new Building(BUILDING_TYPE.SHAN, 1, 1);
        //Building build7 = new Building(BUILDING_TYPE.STORAGE, 1, 1);
        //Building build8 = new Building(BUILDING_TYPE.TIAN, 1, 1);


        //this.m_lstBuilding.Add(build1);
        //this.m_lstBuilding.Add(build2);
        //this.m_lstBuilding.Add(build3);
        //this.m_lstBuilding.Add(build4);
        //this.m_lstBuilding.Add(build5);
        //this.m_lstBuilding.Add(build6);
        //this.m_lstBuilding.Add(build7);
        //this.m_lstBuilding.Add(build8);
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public void Destory()
    {
        this.m_lstBuilding.Clear();
    }

    /// <summary>
    /// 增加建筑
    /// </summary>
    /// <param name="building"></param>
    public void AddBuild(Building building)
    {
        this.m_lstBuilding.Add(building);
    }

    /// <summary>
    /// 获取指定建筑
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Building GetBuilding(BUILDING_TYPE type)
    {
        for (int i = 0; i < this.m_lstBuilding.Count; i++)
        {
            if (this.m_lstBuilding[i].m_eType == type)
            {
                return this.m_lstBuilding[i];
            }
        }
        return null;
    }
}

