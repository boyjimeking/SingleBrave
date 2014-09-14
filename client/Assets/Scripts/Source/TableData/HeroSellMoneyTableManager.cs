//Micro.Sanvey
//2013.12.20
//sanvey.china@gmail.com

using System.Collections.Generic;
using Game.Base;
using UnityEngine;
using Game.Resource;

/// <summary>
/// 英雄出售价格表管理类
/// </summary>
public class HeroSellMoneyTableManager : Singleton<HeroSellMoneyTableManager>
{

    private List<HeroSellMoneyTable> m_lstHero = new List<HeroSellMoneyTable>();  //英雄成长经验列表

    public HeroSellMoneyTableManager()
    {
#if GAME_TEST_LOAD
        LoadText(ResourceMgr.Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.HERO_SELL_MONEY_PATH) as string);
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
                HeroSellMoneyTable table = new HeroSellMoneyTable();
                table.ReadText(lst, ref index);
                this.m_lstHero.Add(table);
            }
        }
        catch (System.Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "英雄出售配置表错误");
        }
    }

    /// <summary>
    /// 根据 英雄等级和英雄星级，获取英雄出售价格
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetHeroMoey(int level, int star)
    {
        if (star>5||star<1)
        {
            Debug.LogError("Error Star :" + level);
            return 0;
        }
        foreach (HeroSellMoneyTable item in m_lstHero)
        {
            if (item.LV == level)
            {
                return item.Money[star - 1];
            }
        }

        Debug.LogError(
            string.Format("Not Find Money For This Data."
            + " Error Lever :{0};Error Star :{1}", level, star));

        return 0;
    }
}