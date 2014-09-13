//  FriendGiftItemTableManager.cs
//  Author: ChengXia
//  2013-1-15

using System;
using System.Collections.Generic;
using System.Collections;
using Game.Base;
using UnityEngine;
using Game.Resource;

/// <summary>
/// 好友礼物管理类
/// </summary>
class FriendGiftItemTableManager : Singleton<FriendGiftItemTableManager>
{
    private List<FriendGiftItemTable> m_lstItem = new List<FriendGiftItemTable>();  //好友礼物列表

    public FriendGiftItemTableManager()
    {
#if GAME_TEST_LOAD
        LoadText(ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.FRIEND_GIFTITEM_PATH) as string);
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
                FriendGiftItemTable table = new FriendGiftItemTable();
                table.ReadText(lst, ref index);
                this.m_lstItem.Add(table);
            }
        }
        catch (System.Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "好友礼物配置表错误");
        }
    }

    /// <summary>
    /// 根据index 获取物品
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public FriendGiftItemTable GetItemByIndex(int index)
    {
        return m_lstItem[index];
    }

    /// <summary>
    /// 获取指定物品
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public FriendGiftItemTable GetItem(int id)
    {
        foreach (FriendGiftItemTable item in this.m_lstItem)
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
    public List<FriendGiftItemTable> GetAll()
    {
        return new List<FriendGiftItemTable>(this.m_lstItem);
    }

    /// <summary>
    /// 获取所有item 有金币 农场点 友情点需分开盘点 item传入图片集
    /// </summary>
    /// <returns></returns>
    public List<FriendGiftItem> GetItemAll()
    {
        List<FriendGiftItem> lstItem = new List<FriendGiftItem>();

        foreach (FriendGiftItemTable git in m_lstItem)
        {
            FriendGiftItem i;
            if (git.TypeID != 6)
            {
                i = new FriendGiftItem();
                i.m_strSpiritName = git.SpiritName;
                i.m_iTypeID = git.BorderType;
                i.m_strName = git.Name;
                i.m_strDesc = git.Desc;
                i.m_eType = (GiftType)git.TypeID;
            }
            else
            {
                ItemTable tmp = ItemTableManager.GetInstance().GetItem(git.TableID);
                i = new FriendGiftItem();
                i.m_strSpiritName = tmp.SpiritName;
                i.m_iTypeID = tmp.Type;
                i.m_strName = tmp.Name;
                i.m_strDesc = tmp.Desc;
                i.m_eType = GiftType.Item;
            }

            i.m_iID = git.ID;
            i.m_strNumText = git.NumText;
            i.m_iNum = git.Num;
            lstItem.Add(i);
        }

        return lstItem;
    }

    /// <summary>
    /// 获取单个FriendGiftItem
    /// </summary>
    /// <returns></returns>
    public FriendGiftItem GetGiftItem(int giftID)
    {
        foreach (FriendGiftItemTable git in m_lstItem)
        {
            if (git.ID == giftID)
            {
                FriendGiftItem i;
                if (git.TypeID != 6)
                {
                    i = new FriendGiftItem();
                    i.m_strSpiritName = git.SpiritName;
                    i.m_iTypeID = git.BorderType;
                    i.m_strDesc = git.Desc;
                    i.m_strName = git.Name;
                    i.m_eType = (GiftType)git.TypeID;
                }
                else
                {
                    ItemTable tmp = ItemTableManager.GetInstance().GetItem(git.TableID);
                    i = new FriendGiftItem();
                    i.m_strSpiritName = tmp.SpiritName;
                    i.m_iTypeID = tmp.Type;
                    i.m_strDesc = tmp.Desc;
                    i.m_strName = tmp.Name;
                    i.m_eType = (GiftType)git.TypeID;
                }
                i.m_iID = git.ID;
                i.m_strNumText = git.NumText;
                i.m_iNum = git.Num;
                return i;
            }
        }

        return null;
    }
    
    /// <summary>
    /// 根据索性获取Item;
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public FriendGiftItem GetGiftItemByIndex(int index)
    {
        FriendGiftItemTable git = m_lstItem[index];
        FriendGiftItem i;
        if (git.TypeID != 6)
        {
            i = new FriendGiftItem();
            i.m_strSpiritName = git.SpiritName;
            i.m_iTypeID = git.BorderType;
            i.m_strDesc = git.Desc;
            i.m_strName = git.Name;
            i.m_eType = (GiftType)git.TypeID;
        }
        else
        {
            ItemTable tmp = ItemTableManager.GetInstance().GetItem(git.TableID);
            i = new FriendGiftItem();
            i.m_strSpiritName = tmp.SpiritName;
            i.m_iTypeID = tmp.Type;
            i.m_strDesc = tmp.Desc;
            i.m_strName = tmp.Name;
            i.m_eType = (GiftType)git.TypeID;
        }
        i.m_iID = git.ID;
        i.m_strNumText = git.NumText;
        i.m_iNum = git.Num;
        return i;
    }
}
