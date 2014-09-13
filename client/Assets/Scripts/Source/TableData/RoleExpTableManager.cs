using System;
using System.Collections.Generic;
using Game.Base;
using UnityEngine;
using Game.Resource;

//  RoleExpTableManager.cs
//  Author: Lu Zexi
//  2013-11-18


/// <summary>
/// 角色经验管理
/// </summary>
public class RoleExpTableManager : Singleton<RoleExpTableManager>
{
    private List<RoleExpTable> m_lstRoleExp = new List<RoleExpTable>(); //角色经验列表

    public int m_iCurrentCost;//当前队伍cost

    public RoleExpTableManager()
    {
#if GAME_TEST_LOAD
        //角色经验表
        LoadText(ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.ROLE_EXP_TABLE_PATH) as string);
#endif
    }

    /// <summary>
    /// 加载数据
    /// </summary>
    public void LoadText(string content)
    {
        try
        {
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;
            this.m_lstRoleExp.Clear();

            for (; index < lst.Count; )
            {
                RoleExpTable table = new RoleExpTable();
                table.ReadText(lst, ref index);
                this.m_lstRoleExp.Add(table);
            }
        }
        catch (System.Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, " 角色经验配置表错误");
        }
    }


    /// <summary>
    /// 获取所有角色经验表
    /// </summary>
    /// <returns></returns>
    public List<RoleExpTable> GetAll()
    {
        return new List<RoleExpTable>(this.m_lstRoleExp);
    }

    /// <summary>
    /// 获取指定角色表
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public RoleExpTable GetRoleExpTable(int level)
    {
        foreach (RoleExpTable item in this.m_lstRoleExp)
        {
            if (item.Level == level)
            {
                return item;
            }
        }

        return null;

    }

    public int GetMaxLevel()
    {
        return this.m_lstRoleExp.Count;
    }

    /// <summary>
    /// 由经验获取角色经验表
    /// </summary>
    /// <param name="exp"></param>
    /// <returns></returns>
    public RoleExpTable GetRoleLevelByExp(int exp)
    {
        for (int i = 0; i < this.m_lstRoleExp.Count; i++)
        {
            if (this.m_lstRoleExp[i].Exp > exp)
            {
                return this.m_lstRoleExp[i - 1];
            }
        }
        return this.m_lstRoleExp[this.m_lstRoleExp.Count - 1];
    }

    /// <summary>
    /// 获取角色当前等级的最大经验
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public int GetMaxExp(int level)
    {
        for (int i = 0; i < this.m_lstRoleExp.Count; i++)
        {
            if (this.m_lstRoleExp[i].Level > level)
            {
                return this.m_lstRoleExp[i].Exp;
            }
        }
        return this.m_lstRoleExp[this.m_lstRoleExp.Count - 1].Exp;
    }

    /// <summary>
    /// 获取当前起始经验
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public int GetMinExp(int level)
    {
        for (int i = 0; i < this.m_lstRoleExp.Count; i++)
        {
            if (this.m_lstRoleExp[i].Level >= level)
            {
                return this.m_lstRoleExp[i].Exp;
            }
        }
        return this.m_lstRoleExp[this.m_lstRoleExp.Count - 1].Exp;
    }

    /// <summary>
    /// 根据用户等级获取最大Cost
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public int GetMaxCost(int level)
    {
        return GetRoleExpTable(level).Cost;
    }

    /// <summary>
    /// 根据用户等级获取最大体力
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public int GetMaxStrength(int level)
    {
        return GetRoleExpTable(level).HP;
    }

    /// <summary>
    /// 获取用户等级的最大好友数量
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public int GetMaxFriend(int level)
    {
        return GetRoleExpTable(level).MaxFriend;
    }
}