using System;
using System.Collections.Generic;
using Game.Base;
using UnityEngine;
using Game.Resource;


//  HeroTableManager.cs
//  Author: Lu Zexi
//  2013-11-18



/// <summary>
/// 英雄表管理类
/// </summary>
public class HeroTableManager : Singleton<HeroTableManager>
{
    private List<HeroTable> m_lstHero = new List<HeroTable>();  //英雄列表

    public HeroTableManager()
    {
#if GAME_TEST_LOAD
        //英雄表
        LoadText(ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.HERO_TABLE_PATH) as string);
#endif
    }

    /// <summary>
    /// 读取文本数据
    /// </summary>
    public void LoadText( string content )
    {
        try
        {
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;
            this.m_lstHero.Clear();

            for (; index < lst.Count; )
            {
                HeroTable table = new HeroTable();
                table.ReadText(lst, ref index);
                this.m_lstHero.Add(table);
            }
        }
        catch (System.Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "英雄配置表错误");
        }
    }


    /// <summary>
    /// 获取所有
    /// </summary>
    /// <returns></returns>
    public List<HeroTable> GetAll()
    {
        return new List<HeroTable>(this.m_lstHero);
    }

    /// <summary>
    /// 获取英雄表
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public HeroTable GetHeroTable(int id)
    {
        foreach (HeroTable item in this.m_lstHero)
        {
            if (item.ID == id)
            {
                return item;
            }
        }

        return null;
    }

    /// <summary>
    /// 获取英雄最大星级
    /// </summary>
    /// <param name="tableID"></param>
    /// <returns></returns>
    public int GetMaxStar(int tableID)
    {

        HeroTable ht = GetHeroTable(tableID);
        while (ht.EvolveID != 0)
        {
            ht = GetHeroTable(ht.EvolveID);
        }
        return ht.Star;

    }
}
