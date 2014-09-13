using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Base;
using Game.Resource;
using UnityEngine;

//玩家名称表管理
//Author sunyi
//2014-03-12
public class PlayerNameTableManager : Singleton<PlayerNameTableManager>
{
    private List<PlayerNameTable> m_lstNameTable = new List<PlayerNameTable>();    //玩家推荐名称列表

    public PlayerNameTableManager()
    {
#if GAME_TEST_LOAD
        //加载玩家名称表
        LoadText(ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.PLAYER_NAME) as string);
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
            this.m_lstNameTable.Clear();

            for (; index < lst.Count; )
            {
                PlayerNameTable nameTable = new PlayerNameTable();
                nameTable.ReadText(lst, ref index);
                this.m_lstNameTable.Add(nameTable);
            }
        }
        catch (System.Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "玩家名称表错误");
        }

    }

    /// <summary>
    /// 获取所有表 
    /// </summary>
    /// <returns></returns>
    public List<PlayerNameTable> GetAll()
    {
        return new List<PlayerNameTable>(this.m_lstNameTable);
    }

}

