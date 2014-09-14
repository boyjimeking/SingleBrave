using System.Collections.Generic;
using System.Text;
using Game.Base;
using Game.Resource;
using UnityEngine;

//  StoryTableManager.cs
//  Author: Lu Zexi
//  2014-02-26



/// <summary>
/// 剧情表管理类
/// </summary>
public class StoryTableManager : Singleton<StoryTableManager>
{
    private Dictionary<int, StoryTable> m_mapStory = new Dictionary<int, StoryTable>();

    public StoryTableManager()
    {
#if GAME_TEST_LOAD
        //剧情
        Load(ResourceMgr.Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.STORY_PATH) as string);
#endif
    }

    /// <summary>
    /// 获取剧情表
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public StoryTable GetStory(int id)
    {
        if (this.m_mapStory.ContainsKey(id))
            return this.m_mapStory[id];
        return null;
    }


    /// <summary>
    /// 加载数据
    /// </summary>
    /// <param name="content"></param>
    public void Load(string content)
    {
        try
        {
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            lst = TABLE_READER.SPLIT_EMPTY(lst);
            int index = 0;
            this.m_mapStory.Clear();
            for (; index < lst.Count; )
            {
                StoryTable table = new StoryTable();
                table.ReadText(lst, ref index);
                m_mapStory.Add(table.ID, table);
            }
        }
        catch (System.Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, " 剧情表错误");
        }
    }
}
