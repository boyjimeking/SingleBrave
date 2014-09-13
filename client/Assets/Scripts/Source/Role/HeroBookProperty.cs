using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  HeroBookProperty.cs
//  Author: Lu Zexi
//  2013-12-30


/// <summary>
/// 英雄图鉴
/// </summary>
public class HeroBookProperty
{
    private List<int> m_lstItem = new List<int>();

    public HeroBookProperty()
    { 
        //
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public void Destory()
    {
        this.m_lstItem.Clear();
    }

    /// <summary>
    /// 增加ID
    /// </summary>
    /// <param name="id"></param>
    public void Add(int id)
    {
        if (!HadHero(id))
            this.m_lstItem.Add(id);
    }

    /// <summary>
    /// 是否曾拥有
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool HadHero(int id)
    {
        foreach (int item in this.m_lstItem)
            if (id == item)
                return true;
        return false;
    }

}
