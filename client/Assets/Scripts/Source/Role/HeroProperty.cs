using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;


//  HeroProperty.cs
//  Author: Lu Zexi
//  2013-11-14




/// <summary>
/// 英雄属性
/// </summary>
public class HeroProperty
{
    private List<Hero> m_lstHero;   //英雄列表

    public HeroProperty()
    {
        this.m_lstHero = new List<Hero>();
    }

    /// <summary>
    /// 获取所有英雄列表
    /// </summary>
    /// <returns></returns>
    public List<Hero> GetAllHero()
    {
        List<Hero> lst = new List<Hero>(this.m_lstHero);
        return lst;
    }


    /// <summary>
    /// 获取指定英雄
    /// </summary>
    /// <returns></returns>
    public Hero GetHero(int id)
    {
        for (int i = 0; i < this.m_lstHero.Count; i++)
        {
            if (this.m_lstHero[i].m_iID == id)
            {
                return this.m_lstHero[i];
            }
        }

        return null;
    }

    /// <summary>
    /// 根据tableId获得hero相关信息
    /// </summary>
    /// <param name="tableId"></param>
    /// <returns></returns>
    public Hero GetHeroByTableId(int tableId)
    {
        return m_lstHero.Find(new Predicate<Hero>((item) =>
        {
            return item.m_iTableID == tableId;
        }));
    }


    /// <summary>
    /// 增加英雄
    /// </summary>
    /// <param name="hero"></param>
    public void AddHero(Hero hero)
    {
        this.m_lstHero.Add(hero);
    }


    /// <summary>
    /// 删除英雄
    /// </summary>
    /// <param name="id"></param>
    public void DelHero(int id)
    {
        for (int i = 0; i < this.m_lstHero.Count; i++)
        {
            if (this.m_lstHero[i].m_iID == id)
            {
                this.m_lstHero.RemoveAt(i);
                return;
            }
        }
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public void Destory()
    {
        this.m_lstHero.Clear();
    }

    /// <summary>
    /// 更新英雄 For Test
    /// </summary>
    /// <param name="hero"></param>
    public void UpdateHero(Hero hero)
    {
        if (hero==null)
        {
            Debug.LogError("Update hero null");
            return;
        }
        for (int i = 0; i < this.m_lstHero.Count; i++)
        {
            if (this.m_lstHero[i].m_iID == hero.m_iID)
            {
                this.m_lstHero[i] = hero;
                return;
            }
        }
    }

    /// <summary>
    /// 设置所有英雄不再是new
    /// </summary>
    public void SetAllHeroOld()
    {
        for (int i = 0; i < this.m_lstHero.Count; i++)
        {
            this.m_lstHero[i].m_bNew = false;
        }
    }
}
