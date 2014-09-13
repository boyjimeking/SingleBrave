using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  TeamProperty.cs
//  Author: Lu Zexi
//  2013-11-14


/// <summary>
/// 队伍属性
/// </summary>
public class TeamProperty
{
    private int HERO_TEAM_NUM = 10;  //编队最大数量
    private HeroTeam[] m_vecHeroTeam;   //英雄编队
    private int m_iCurrentTeamNum;//当前队伍编号

    public TeamProperty()
    {
        this.m_vecHeroTeam = new HeroTeam[HERO_TEAM_NUM];
    }

    /// <summary>
    /// 获取所有英雄编队
    /// </summary>
    /// <returns></returns>
    public HeroTeam[] GetAllTeam()
    {
        HeroTeam[] vec = new HeroTeam[HERO_TEAM_NUM];

        Array.Copy(this.m_vecHeroTeam, 0, vec, 0, HERO_TEAM_NUM);

        return vec;
    }

    /// <summary>
    /// 获取指定编队
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public HeroTeam GetTeam(int index)
    {
        if (index >= HERO_TEAM_NUM)
        {
            return null;
        }

        return this.m_vecHeroTeam[index];
    }

    /// <summary>
    /// 增加队伍
    /// </summary>
    /// <param name="index"></param>
    /// <param name="team"></param>
    /// <returns></returns>
    public bool AddTeam(int index, HeroTeam team)
    {
        if (index >= HERO_TEAM_NUM)
        {
            return false;
        }

        this.m_vecHeroTeam[index] = team;

        return true;
    }

    /// <summary>
    /// 获取当前编队的cost
    /// </summary>
    /// <returns></returns>
    public int GetCurTeamCost()
    { 
        int cost = 0;
        int[] heroTableId = Role.role.GetTeamProperty().GetTeam(Role.role.GetBaseProperty().m_iCurrentTeam).m_vecTeam;
        for (int i = 0; i < heroTableId.Length; i++)
        {
            Hero hero = Role.role.GetHeroProperty().GetHero(heroTableId[i]);
            if (hero != null)
            {
                cost += hero.m_iCost;
            }

        }
        return cost;
    }

    /// <summary>
    /// 设置当前队伍编号
    /// </summary>
    /// <param name="teamNum"></param>
    public void SetCurrentTeamNum(int teamNum)
    {
        this.m_iCurrentTeamNum = teamNum;
    }

    /// <summary>
    /// 获取当前队伍编号
    /// </summary>
    /// <param name="teamNum"></param>
    public int GetCurrentTeamNum()
    {
        return this.m_iCurrentTeamNum;
    }
}
