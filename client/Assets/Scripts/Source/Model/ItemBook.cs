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
public class ItemBook : CModel
{
	public int m_iItemID;

    public ItemBook()
    { 
        //
    }

	/// <summary>
	/// Adds the item.
	/// </summary>
	/// <param name="id">Identifier.</param>
	public void AddItem( int id )
	{
		ItemBook itemBook = new ItemBook();
		itemBook.m_iItemID = id;
		Add(itemBook);
	}

    /// <summary>
    /// 是否曾拥有
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool HadItem(int id)
    {
        foreach (ItemBook item in this.s_lstData)
            if (item.m_iItemID == id)
                return true;
        return false;
    }
}
