using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Base;
using Game.Resource;
using UnityEngine;


//  AITableManager.cs
//  Author: Lu Zexi
//  2014-01-15


/// <summary>
/// AI配置表管理类
/// </summary>
public class AITableManager : Singleton<AITableManager>
{
    private List<AITable> m_lstAI = new List<AITable>();    //AI列表

    public AITableManager()
    {
#if GAME_TEST_LOAD
        //副本表
        Load((string)ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.AI_TABLE_PATH));
#endif
    }

    /// <summary>
    /// 加载
    /// </summary>
    /// <param name="content"></param>
    public void Load(string content)
    {
        try
        {
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;

            this.m_lstAI.Clear();
            for (; index < lst.Count; )
            {
                AITable table = new AITable();
                table.ReadText(lst, ref index);
                this.m_lstAI.Add(table);
            }
        }
        catch (Exception ex)
        {
            GUI_FUNCTION.MESSAGEM(Application.Quit, "人工智能表出错");
        }
    }

    /// <summary>
    /// 获取AI
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public AITable GetAI(int id)
    {
        for (int i = 0; i < this.m_lstAI.Count; i++)
        {
            if (this.m_lstAI[i].ID == id)
                return this.m_lstAI[i];
        }

        return null;
    }

}
