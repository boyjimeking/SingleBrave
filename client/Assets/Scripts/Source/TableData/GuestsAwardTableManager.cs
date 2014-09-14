using System;
using System.Collections.Generic;
using System.Collections;
using Game.Base;
using UnityEngine;
using Game.Resource;

//  GuestsAwardTableManager.cs
//  Author: Sanvey
//  2013-11-20


/// <summary>
/// 物品表管理类
/// </summary>
public class GuestsAwardTableManager : Singleton<GuestsAwardTableManager>
{
    private List<GuestsAwardTable> m_lstItem = new List<GuestsAwardTable>();  //招待列表

    public GuestsAwardTableManager()
    {
#if GAME_TEST_LOAD
        //招待列表
        LoadText(ResourceMgr.Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.GUEST_GIFT_PATH) as string);
#endif
    }

    /// <summary>
    /// 加载文本数据
    /// </summary>
    /// <param name="content"></param>
    public void LoadText(string content)
    {
        try
        {
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;
            this.m_lstItem.Clear();

            for (; index < lst.Count; )
            {
                GuestsAwardTable table = new GuestsAwardTable();
                table.ReadText(lst, ref index);
                this.m_lstItem.Add(table);
            }
        }
        catch (System.Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "招待配置表错误");
        }
    }

    /// <summary>
    /// 获得所有招待礼物配置表
    /// </summary>
    public List<GuestsAwardTable> GetAllGuestAward()
    {
        return new List<GuestsAwardTable>(m_lstItem);
    }
}