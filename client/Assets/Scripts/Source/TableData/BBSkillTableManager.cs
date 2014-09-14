using System;
using System.Collections.Generic;
using Game.Base;
using Game.Resource;
using UnityEngine;


//  BBSkillTableManager.cs
//  Author: Lu Zexi
//  2013-12-02



/// <summary>
/// BB技能配置管理
/// </summary>
public class BBSkillTableManager : Singleton<BBSkillTableManager>
{
    private List<BBSkillTable> m_lstSkill = new List<BBSkillTable>();

    public BBSkillTableManager()
    {
#if GAME_TEST_LOAD
        //技能表
        LoadText((string)ResourceMgr.Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.BBSKILL_PATH));
#endif
    }

    /// <summary>
    /// 获取BB技能
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BBSkillTable GetBBSkillTable(int id)
    {
        foreach (BBSkillTable item in this.m_lstSkill)
        {
            if (item.ID == id)
                return item;
        }
        return null;
    }

    /// <summary>
    /// 加载文本数据
    /// </summary>
    /// <param name="content"></param>
    public void LoadText( string content )
    {
        try
        {
            this.m_lstSkill.Clear();

            List<string> lst = TABLE_READER.LOAD_CSV(content);
            lst = TABLE_READER.SPLIT_EMPTY(lst);
            int index = 0;
            for (; index < lst.Count; )
            {
                BBSkillTable table = new BBSkillTable();
                table.ReadText(lst, ref index);
                this.m_lstSkill.Add(table);
            }
        }
        catch (Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "技能配置表错误");
            Debug.LogError(ex.StackTrace);
        }
    }

}

