
using System.Collections.Generic;
using Game.Base;
using UnityEngine;
using Game.Resource;


//  ItemCompositeTableManager.cs
//  Author: Lu Zexi
//  2013-12-11



/// <summary>
/// 物品合成配置表管理类
/// </summary>
public class ItemCompositeTableManager : Singleton<ItemCompositeTableManager>
{
    private List<ItemCompositeTable> m_lstComposite = new List<ItemCompositeTable>();

    public ItemCompositeTableManager()
    {
#if GAME_TEST_LOAD
        //物品合成表
        LoadText(ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.ITEM_COMPOSITE_PATH) as string);
#endif
    }

    /// <summary>
    /// 加载
    /// </summary>
    public void LoadText( string content )
    {
        try
        {
            this.m_lstComposite.Clear();
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;

            for (; index < lst.Count; )
            {
                ItemCompositeTable table = new ItemCompositeTable();
                table.ReadText(lst, ref index);
                this.m_lstComposite.Add(table);
            }
        }
        catch (System.Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "物品合成配置表错误");
        }
    }

    /// <summary>
    /// 获取全部
    /// </summary>
    /// <returns></returns>
    public List<ItemCompositeTable> GetAll()
    {
        return new List<ItemCompositeTable>(this.m_lstComposite);
    }

    /// <summary>
    /// 获取某物品合成配置
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ItemCompositeTable GetItemCompositeTable(int id)
    {
        foreach (ItemCompositeTable item in this.m_lstComposite)
        {
            if (item.ID == id)
            {
                return item;
            }
        }
        return null;
    }

    /// <summary>
    /// 获取合成类型列表
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public List<ItemCompositeTable> GetByType(int type)
    {
        List<ItemCompositeTable> lst = new List<ItemCompositeTable>();
        foreach (ItemCompositeTable item in this.m_lstComposite)
        {
            if (item.Type == type)
            {
                lst.Add(item);
            }
        }
        return lst;
    }

}
