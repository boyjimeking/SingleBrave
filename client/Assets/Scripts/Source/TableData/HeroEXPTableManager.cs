//Micro.Sanvey
//2013.12.4
//sanvey.china@gmail.com

using System.Collections.Generic;
using Game.Base;
using UnityEngine;
using Game.Resource;

/// <summary>
/// 英雄成长经验表管理类
/// </summary>
public class HeroEXPTableManager : Singleton<HeroEXPTableManager>
{

    private List<HeroEXPTable> m_lstHero = new List<HeroEXPTable>();  //英雄成长经验列表

    public HeroEXPTableManager()
    {
#if GAME_TEST_LOAD
        //英雄成长经验曲线表
        LoadText(ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.HERO_EXP_PATH) as string);
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
                HeroEXPTable table = new HeroEXPTable();
                table.ReadText(lst, ref index);
                this.m_lstHero.Add(table);
            }
        }
        catch (System.Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "英雄经验配置表错误");
        }
    }

    /// <summary>
    /// 根据成长类型获取所有等级列表 eg: 1,2,3
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public List<int> GetHeroExpByType(int type)
    {
        List<int> re = new List<int>();

        for (int i = 0; i < m_lstHero.Count; i++)
        {
            re.Add(m_lstHero[i].VecEXP[type - 1]);
        }
        return re;
    }

    /// <summary>
    /// 获取当前等级最大经验
    /// </summary>
    /// <param name="type"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    public int GetMaxExp(int type, int level)
    {
        List<int> lst = GetHeroExpByType(type);
        for (int i = 0; i < lst.Count; i++)
        {
            if (i + 1 > level)
            {
                return lst[i];
            }
        }
        return lst[lst.Count - 1];
    }

    /// <summary>
    /// 获取当前等级起始经验
    /// </summary>
    /// <param name="type"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    public int GetMinExp(int type, int level)
    {
        List<int> lst = GetHeroExpByType(type);
        for (int i = 0; i < lst.Count; i++)
        {
            if (i + 1 >= level)
            {
                return lst[i];
            }
        }
        return lst[lst.Count - 1];
    }

    /// <summary>
    /// 根据经验获得等级
    /// </summary>
    /// <param name="type"></param>
    /// <param name="exp"></param>
    /// <returns></returns>
    public int GetLv(int type, int exp)
    {
        List<int> lst = GetHeroExpByType(type);
        int min = 0;
        for (int i = 0; i < lst.Count; i++)
        {
            if (lst[i] <= exp)
            {

                min = i;
            }
        }
        min++;
        return min;
    }
}