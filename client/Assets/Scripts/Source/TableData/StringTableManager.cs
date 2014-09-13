using System;
using System.Collections.Generic;
using System.Collections;
using Game.Base;
using UnityEngine;
using Game.Resource;

//  StringTableManager.cs
//  Author: Lu Zexi
//  2013-11-14


/// <summary>
/// 字符串管理
/// </summary>
public class StringTableManager : Singleton<StringTableManager>
{
    private Dictionary<int, string> m_mapStr;   //字符串集合

    public StringTableManager()
    {
        this.m_mapStr = new Dictionary<int, string>();

#if GAME_TEST_LOAD
        //字符串表
        Load(ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.STRING_TABLE_PATH) as string);
#endif
    }

    /// <summary>
    /// 获取指定字符串
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public string GetString(int id)
    {
        if (this.m_mapStr.ContainsKey(id))
        {
            return this.m_mapStr[id];
        }

        return "";
    }

    /// <summary>
    /// 临时加载数据
    /// </summary>
    public void Load( string content )
    {
        try
        {
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            this.m_mapStr.Clear();
            for (int i = 0; i < lst.Count; )
            {
                int id = TABLE_READER.INT(lst[i++]);
                string str = TABLE_READER.STRING(lst[i++]);
                str = str.Replace("\\n", "\n");
                string desc = TABLE_READER.STRING(lst[i++]);

                this.m_mapStr.Add(id, str);
            }
        }
        catch (System.Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, " 字符串配置表错误");
        }
    }

}

