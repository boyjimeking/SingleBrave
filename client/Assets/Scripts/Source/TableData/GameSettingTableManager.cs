using System;
using System.Collections.Generic;
using Game.Base;
using UnityEngine;
using Game.Resource;



//  GameSettingTableManager.cs
//  Author: Lu Zexi
//  2013-12-04


/// <summary>
/// 游戏参数表管理类 
/// </summary>
public class GameSettingTableManager : Singleton<GameSettingTableManager>
{
    private Dictionary<string, string> m_mapSet = new Dictionary<string, string>(); //参数

    public GameSettingTableManager()
    {
        //
    }

    /// <summary>
    /// 加载文本数据
    /// </summary>
    /// <param name="content"></param>
    public void LoadText(string content)
    {
        try
        {
            this.m_mapSet.Clear();
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;
            for (; index < lst.Count; )
            {
                string key = TABLE_READER.STRING(lst[index++]);
                string value = TABLE_READER.STRING(lst[index++]);
                string desc = TABLE_READER.STRING(lst[index++]);
                this.m_mapSet.Add(key, value);
            }
        }
        catch (Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "全局配置表错误");
        }
    }

    /// <summary>
    /// 获取字符串参数
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetString(string key)
    {
        return this.m_mapSet[key];
    }

    /// <summary>
    /// 获取整数参数
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public int GetInt(string key)
    {
        return int.Parse(this.m_mapSet[key]);
    }

    /// <summary>
    /// 获取浮点数参数
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public float GetFloat(string key)
    {
        return float.Parse(this.m_mapSet[key]);
    }

}
