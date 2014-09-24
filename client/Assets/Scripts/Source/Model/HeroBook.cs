using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  HeroBook.cs
//  Author: Lu Zexi
//  2013-12-30


/// <summary>
/// 英雄图鉴
/// </summary>
public class HeroBook : CModel<HeroBook>
{
	public int m_iHeroID;

    public HeroBook()
    { 
        //
    }

    /// <summary>
    /// 增加ID
    /// </summary>
    /// <param name="id"></param>
    public void AddBook(int id)
    {
        if (!HadHero(id))
		{
			HeroBook heroBook = new HeroBook();
			heroBook.m_iHeroID = id;
			Add(heroBook);
		}
    }

    /// <summary>
    /// 是否曾拥有
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool HadHero(int id)
    {
        foreach ( HeroBook item  in s_lstData)
            if (id == item.m_iHeroID)
                return true;
        return false;
    }

}
