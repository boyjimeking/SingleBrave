using System;
using System.Collections.Generic;
using Game.Base;
using UnityEngine;
using Game.Resource;


//  HeroGrowTableManager.cs
//  Author: Lu Zexi
//  2013-11-18



/// <summary>
/// 英雄成长表管理类
/// </summary>
public class HeroGrowTableManager : Singleton<HeroGrowTableManager>
{
    private List<HeroGrowTable> m_lstHero = new List<HeroGrowTable>();  //英雄成长列表

    public HeroGrowTableManager()
    {
#if GAME_TEST_LOAD
        //英雄表
        LoadText(ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.HERO_GROW_PATH) as string);
#endif
    }

    /// <summary>
    /// 读取文本数据
    /// </summary>
    public void LoadText(string content)
    {
        try
        {
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;
            this.m_lstHero.Clear();

            for (; index < lst.Count; )
            {
                HeroGrowTable table = new HeroGrowTable();
                table.ReadText(lst, ref index);
                this.m_lstHero.Add(table);
            }
        }
        catch (System.Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "英雄成长配置表错误");
        }
    }


    /// <summary>
    /// 获取所有
    /// </summary>
    /// <returns></returns>
    public List<HeroGrowTable> GetAll()
    {
        return new List<HeroGrowTable>(this.m_lstHero);
    }

    /// <summary>
    /// 获取英雄表
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public HeroGrowTable GetHeroTable(int id)
    {
        foreach (HeroGrowTable item in this.m_lstHero)
        {
            if (item.ID == id)
            {
                return item;
            }
        }

        return null;
    }

    /// <summary>
    /// 根据tableID 和 成长类型获得 英雄基础属性 hp atk def rec
    /// </summary>
    /// <param name="tableId"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public List<int> GetHeroByType(int tableId, GrowType type)
    {
        List<int> re = new List<int>();
        HeroGrowTable table = GetHeroTable(tableId);
        switch (type)
        {
            case GrowType.Balance: re.Add(table.BHP); re.Add(table.BAtk); re.Add(table.BDef); re.Add(table.BRec);
                break;
            case GrowType.Hp: re.Add(table.HHP); re.Add(table.HAtk); re.Add(table.HDef); re.Add(table.HRec);
                break;
            case GrowType.Attack: re.Add(table.AHP); re.Add(table.AAtk); re.Add(table.ADef); re.Add(table.ARec);
                break;
            case GrowType.Defense: re.Add(table.DHP); re.Add(table.DAtk); re.Add(table.DDef); re.Add(table.DRec);
                break;
            case GrowType.Revert: re.Add(table.RHP); re.Add(table.RAtk); re.Add(table.RDef); re.Add(table.RRec);
                break;
            default:
                break;
        }

        return re;
    }
}