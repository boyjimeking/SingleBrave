using System;
using System.Collections.Generic;
using System.Collections;
using Game.Base;
using UnityEngine;
using Game.Resource;

//  ItemTableManager.cs
//  Author: Lu Zexi
//  2013-11-20



/// <summary>
/// 物品表管理类
/// </summary>
public class ItemTableManager : Singleton<ItemTableManager>
{
    private List<ItemTable> m_lstItem = new List<ItemTable>();  //物品列表

    public ItemTableManager()
    {
#if GAME_TEST_LOAD
        //物品表
        LoadText(ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.ITEM_TABLE_PATH) as string);
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
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;
            this.m_lstItem.Clear();

            for (; index < lst.Count; )
            {
                ItemTable table = new ItemTable();
                table.ReadText(lst, ref index);
                this.m_lstItem.Add(table);
            }
        }
        catch (System.Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "物品配置表错误");
        }
    }

    /// <summary>
    /// 获取指定物品
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ItemTable GetItem(int id)
    {
        foreach (ItemTable item in this.m_lstItem)
        {
            if (item.ID == id)
            {
                return item;
            }
        }

        return null;
    }

    /// <summary>
    /// 获取所有数据
    /// </summary>
    /// <returns></returns>
    public List<ItemTable> GetAll()
    {
        return new List<ItemTable>(this.m_lstItem);
    }

    /// <summary>
    /// 获得战斗物品最大数量
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns></returns>
    public int GetBattleMaxNum(int itemId)
    {
        foreach (ItemTable item in this.m_lstItem)
        {
            if (item.ID == itemId)
            {
                return item.BattleMaxNum;
            }
        }


        return 0;
    }

}
