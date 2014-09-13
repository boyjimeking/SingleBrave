using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using UnityEngine;

//副本属性
//Author sunyi
//2013-12-12


/// <summary>
/// PVE副本世界数据属性类
/// </summary>
public class FubenProperty
{
    private List<FuBen> m_lstFuben = new List<FuBen>();//副本任务列表

    public FubenProperty()
    { 
        
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public void Destory()
    {
        this.m_lstFuben.Clear();
    }

    /// <summary>
    /// 获取副本任务
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public List<FuBen> GetAllFuben()
    {
        return this.m_lstFuben;
    }

    /// <summary>
    /// 增加副本
    /// </summary>
    /// <param name="fuben"></param>
    public void AddFuben(FuBen fuben)
    {
        this.m_lstFuben.Add(fuben);
    }

    /// <summary>
    /// 删除副本
    /// </summary>
    /// <param name="fuben"></param>
    public void DeleteFuben(FuBen fuben)
    {
        this.m_lstFuben.Remove(fuben);
    }

    /// <summary>
    /// 根据世界ID获取副本信息
    /// </summary>
    /// <param name="worldID"></param>
    /// <returns></returns>
    public FuBen GetFubenByWorldID(int worldID)
    {
        foreach (FuBen item in this.m_lstFuben)
        {
            if (item.m_iWorldID == worldID)
                return item;
        }
        return null;
    }

    /// <summary>
    /// 获取玩家开启的最新区域
    /// </summary>
    /// <param name="SelectWorld"></param>
    /// <returns></returns>
    public int GetNewAreaIndex(int SelectWorld)
    {
        int newAreaIndex = 0;

        foreach (FuBen fuben in this.m_lstFuben)
        {
            if (fuben.m_iWorldID == SelectWorld)
            {
                if (fuben.m_bActive)
                {
                    newAreaIndex = fuben.m_iAreaIndex;
                    continue;
                }
            }
        }

        return newAreaIndex;
    }

    /// <summary>
    /// 获取玩家开启的最新副本
    /// </summary>
    /// <param name="selectWorld"></param>
    /// <returns></returns> 
    public int GetNewDungeonIndex(int selectWorld, int curAreaIndex)
    {
        int newDungeonIndex = -1;

        foreach (FuBen fuben in this.m_lstFuben)
        {
            if (fuben.m_iWorldID == selectWorld)
            {
                if (fuben.m_iAreaIndex == curAreaIndex)
                {
                    if (newDungeonIndex < fuben.m_iDungeonIndex)
                    {
                        newDungeonIndex = fuben.m_iDungeonIndex;
                    }
                }
                
                continue;
            }
        }

        return newDungeonIndex;
    }

    /// <summary>
    /// 获取玩家开启的最新关卡
    /// </summary>
    /// <param name="SelectWorld"></param>
    /// <returns></returns>
    public int GetNewGateIndex(int SelectWorld)
    {
        int newGateIndex = 0;

        foreach (FuBen fuben in this.m_lstFuben)
        {
            if (fuben.m_iWorldID == SelectWorld)
            {
                newGateIndex = fuben.m_iGateIndex;
                continue;
            }
        }

        return newGateIndex;
    }
}

