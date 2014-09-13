using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  ItemBookProperty.cs
//  Author: Lu Zexi
//  2013-12-30



/// <summary>
/// 物品图鉴属性
/// </summary>
public class ItemBookProperty
{
    private List<int> m_lstItem = new List<int>();

    public ItemBookProperty()
    { 
        //
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public void Destory()
    {
        this.m_lstItem.Clear();
    }

    /// <summary>
    /// 增加物品图鉴
    /// </summary>
    /// <param name="id"></param>
    public void AddItem(int id)
    {
        if (!HadItem(id))
            this.m_lstItem.Add(id);
    }

    /// <summary>
    /// 是否曾拥有
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool HadItem(int id)
    {
        foreach (int item in this.m_lstItem)
            if (item == id)
                return true;
        return false;
    }
}
