using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Base;
using Game.Resource;
using UnityEngine;

//玩家名称敏感词汇表管理
//Author sunyi
//2014-03-12
public class PlayerNameSensitiveWordTableManager : Singleton<PlayerNameSensitiveWordTableManager>
{
    private List<PlayerNameSensitiveWordTable> m_lstWordTable = new List<PlayerNameSensitiveWordTable>();    //玩家推荐名称列表

    public PlayerNameSensitiveWordTableManager()
    {
#if GAME_TEST_LOAD
        //加载玩家名称敏感词汇表
        LoadText(ResourceMgr.Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.SENSITIVE_WORD) as string);
#endif
    }

    /// <summary>
    /// 读取文本
    /// </summary>
    public void LoadText(string content)
    {
        try
        {
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;
            this.m_lstWordTable.Clear();

            for (; index < lst.Count; )
            {
                PlayerNameSensitiveWordTable wordTable = new PlayerNameSensitiveWordTable();
                wordTable.ReadText(lst, ref index);
                this.m_lstWordTable.Add(wordTable);
            }
        }
        catch (System.Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "玩家名称敏感词汇表错误");
        }

    }

    /// <summary>
    /// 获取所有表 
    /// </summary>
    /// <returns></returns>
    public List<PlayerNameSensitiveWordTable> GetAll()
    {
        return new List<PlayerNameSensitiveWordTable>(this.m_lstWordTable);
    }
}

