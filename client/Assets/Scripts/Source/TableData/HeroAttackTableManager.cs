using System;
using System.Collections.Generic;
using Game.Base;
using UnityEngine;
using Game.Resource;


//  AttackTableManager.cs
//  Author: Lu zexi
//  2013-12-02


/// <summary>
/// 攻击表管理类
/// </summary>
public class HeroAttackTableManager : Singleton<HeroAttackTableManager>
{
    private List<HeroAttackTable> m_lstAttack;  //攻击表

    public HeroAttackTableManager()
    {
        this.m_lstAttack = new List<HeroAttackTable>();
#if GAME_TEST_LOAD
        //英雄攻击表
        LoadText(ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.HERO_ATTACK_PATH) as string);
#endif
    }

    /// <summary>
    /// 加载文本数据
    /// </summary>
    /// <param name="content"></param>
    public void LoadText( string content )
    {
        try
        {
            this.m_lstAttack.Clear();
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            lst = TABLE_READER.SPLIT_EMPTY(lst);
            int index = 0;
            for (; index < lst.Count; )
            {
                HeroAttackTable table = new HeroAttackTable();
                table.ReadText(lst, ref index);
                this.m_lstAttack.Add(table);
            }
        }
        catch (Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "英雄攻击配置表错误");
        }
    }

    /// <summary>
    /// 获取英雄攻击表
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public HeroAttackTable GetAttackTable(int id)
    {
        foreach (HeroAttackTable item in this.m_lstAttack)
        {
            if (item.ID == id)
            {
                return item;
            }
        }
        return null;
    }

}
