using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  HeroTeam.cs
//  Author: Lu Zexi
//  2013-11-14



/// <summary>
/// 卡片队伍
/// </summary>
public class HeroTeam
{
    public const int TEAM_MAX_NUM = 5;  //队伍成员最大数量
    public int m_iID;   //队伍ID
    public int[] m_vecTeam;    //卡片队伍
    public int m_iLeadID;   //领导者ID

    public HeroTeam()
    {
        this.m_vecTeam = new int[TEAM_MAX_NUM];
    }

    /// <summary>
    /// 获取队长索引
    /// </summary>
    /// <returns></returns>
    public int GetLeaderIndex()
    {
        for (int i = 0; i < this.m_vecTeam.Length; i++)
        {
            if (this.m_vecTeam[i] == this.m_iLeadID)
                return i;
        }
        return -1;
    }

    //获取cost总值//
    public int GetCostValue()
    {
        int cost = 0;
        for (int i = 0; i < this.m_vecTeam.Length; i++)
        {
            Hero hero = Role.role.GetHeroProperty().GetHero(m_vecTeam[i]);
            if (hero != null)
            {
                cost += hero.m_iCost;
            }
            
        }
        return cost;
    }

    //获取hero在队伍里面的位置//
    public int GetHeroIndex(Hero hero)
    {
        for (int i = 0; i < this.m_vecTeam.Length; i++)
        {
            if (this.m_vecTeam[i] == hero.m_iID)
                return i;
        }
        return -1;
    }

}
