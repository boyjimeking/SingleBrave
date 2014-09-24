using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  FuBen.cs
//  Author: Lu Zexi
//  2013-12-12






/// <summary>
/// PVE副本世界数据类
/// </summary>
public class FuBen : CModel<FuBen>
{
    public bool m_bActive;  //是否被激活
    public int m_iWorldID;  //世界ID
    public int m_iAreaIndex;    //区域索引
    public int m_iDungeonIndex; //副本索引
    public int m_iGateIndex;    //关卡索引

    //剧情
    public bool m_bDungeonStory; //story


	
	/// <summary>
	/// 根据世界ID获取副本信息
	/// </summary>
	/// <param name="worldID"></param>
	/// <returns></returns>
	public static FuBen GetFubenByWorldID(int worldID)
	{
		foreach (FuBen item in s_lstData)
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
	public static int GetNewAreaIndex(int SelectWorld)
	{
		int newAreaIndex = 0;
		
		foreach (FuBen fuben in s_lstData)
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
	public static int GetNewDungeonIndex(int selectWorld, int curAreaIndex)
	{
		int newDungeonIndex = -1;
		
		foreach (FuBen fuben in s_lstData)
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
	public static int GetNewGateIndex(int SelectWorld)
	{
		int newGateIndex = 0;
		
		foreach (FuBen fuben in s_lstData)
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
