using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


//  ItemProperty.cs
//  Author: Lu Zexi
//  2013-11-14



/// <summary>
/// 物品属性
/// </summary>
public class ItemProperty
{
    private List<Item> m_lstItem = new List<Item>();    //物品列表
    private Item[] m_vecBattleItem;  //战斗物品

    public ItemProperty()
    {
        this.m_vecBattleItem = new Item[5];
    }

    /// <summary>
    /// 获取战斗物品
    /// </summary>
    /// <returns></returns>
    public Item[] GetAllBattleItem()
    {
        Item[] tmp = new Item[5];
        for (int i = 0; i < 5; i++)
        {
            tmp[i] = m_vecBattleItem[i];
        }
        return tmp;
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public void Destory()
    {
        this.m_lstItem.Clear();
    }

    /// <summary>
    /// 根据tableid获取战斗物品
    /// </summary>
    /// <param name="tableid"></param>
    /// <returns></returns>
    public Item GetBattleItemByTableID(int tableid)
    {
        for (int i = 0; i < this.m_vecBattleItem.Length; i++)
        {
            if (this.m_vecBattleItem[i] != null && this.m_vecBattleItem[i].m_iTableID == tableid)
            {
                Item tmp = new Item(this.m_vecBattleItem[i].m_iTableID);
                tmp.m_iID = this.m_vecBattleItem[i].m_iID;
                tmp.m_iNum = this.m_vecBattleItem[i].m_iNum;
                return tmp;
            }
        }
        return null;
    }

    /// <summary>
    /// 更新战斗物品
    /// </summary>
    /// <param name="item"></param>
    /// <param name="pos"></param>
    public void UpdateBattleItem(int itemId, int num, int pos)
    {
        if (pos <= 4 && pos >= 0)
        {
            if (itemId == -1)
            {
                m_vecBattleItem[pos] = null;
            }
            else
            {
                Item tmp = new Item(itemId);
                tmp.m_iNum = num;
                m_vecBattleItem[pos] = tmp;
            }
        }
        else
        {
            Debug.LogError("Error Battle Item Pos : " + pos.ToString());
        }
    }

    /// <summary>
    /// 获取指定索引战斗物品
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Item GetBattleItemByIndex(int index)
    {
        if (index >= this.m_vecBattleItem.Length)
            return null;
        return this.m_vecBattleItem[index];
    }

    /// <summary>
    /// 获取物品
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Item GetItem(int id)
    {
        for (int i = 0; i < this.m_lstItem.Count; i++)
        {
            if (this.m_lstItem[i].m_iID == id)
            {
                return this.m_lstItem[i];
            }
        }
        return null;
    }

    /// <summary>
    /// 更具tableID获取相关物品
    /// </summary>
    /// <param name="tableId"></param>
    /// <returns></returns>
    public Item GetItemByTableID(int tableId)
    {
        for (int i = 0; i < this.m_lstItem.Count; i++)
        {
            if (this.m_lstItem[i].m_iTableID == tableId)
            {
                return this.m_lstItem[i];
            }
        }
        return null;
    }

    /// <summary>
    /// 根据tableID获取装备，除去英雄已经装备的物品不返回
    /// </summary>
    /// <param name="tableId"></param>
    /// <returns></returns>
    public Item GetEquipByTableIdWithNoHero(int tableId)
    {
        for (int i = 0; i < this.m_lstItem.Count; i++)
        {
            if (this.m_lstItem[i].m_iTableID == tableId)
            {
                //已经装备的英雄不返回
                if (Role.role.GetHeroProperty().GetAllHero().Exists(new Predicate<Hero>((hero) => { return hero.m_iEquipID == m_lstItem[i].m_iID; })))
                {
                    continue;
                }
                return this.m_lstItem[i];
            }
        }
        return null;
    }

    /// <summary>
    /// 根据装备TableID获取被装备的数量
    /// </summary>
    /// <param name="tableId"></param>
    /// <returns></returns>
    public int GetEquipNumByTableId(int TableID)
    {
        List<Hero> lstHero = Role.role.GetHeroProperty().GetAllHero();
        int num = 0;
        foreach (Hero hero in lstHero)
        {
            Item item = Role.role.GetItemProperty().GetItem(hero.m_iEquipID);
            if (item != null && item.m_iTableID == TableID)
                num++;
        }
        return num;
    }

    /// <summary>
    /// 根据装备tableID获取未被装备的装备列表
    /// </summary>
    /// <param name="TableID"></param>
    /// <returns></returns>
    public List<Item> GetItemListByTableId(int TableID)
    {
        List<Item> itemlist = new List<Item>(); //相同tableId的装备列表
        List<Item> allitem = Role.role.GetItemProperty().GetAllItem();
        List<Hero> lstHero = Role.role.GetHeroProperty().GetAllHero();

        foreach (Item item in allitem)
        {
            if (item.m_iTableID == TableID)
            {
                bool flag = true;
                foreach (Hero hero in lstHero)
                {
                    Item _item = Role.role.GetItemProperty().GetItem(hero.m_iEquipID);
                    if (_item != null && _item.m_iTableID == TableID)
                    {
                 
                        if (item.m_iID == _item.m_iID)
                        {
                            flag = false;
                            break;
                        }
                    }                        
                }
                if(flag)
                    itemlist.Add(item);
            }
                
        }
        return itemlist;
    }

    /// <summary>
    /// 获取指定类型物品列表
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public List<Item> GetItemsType(ITEM_TYPE type)
    {
        List<Item> lst = new List<Item>();
        foreach (Item item in this.m_lstItem)
        {
            if (item.m_eType == type)
            {
                lst.Add(item);
            }
        }
        return lst;
    }

    /// <summary>
    /// 增加物品
    /// 1，对于消耗品和素材，如果已经存在该物品，就更新，如果不存在就加入
    /// 2. 对于装备，直接加入
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(Item item)
    {
        if (item.m_eType == ITEM_TYPE.EQUIP)
        {
            this.m_lstItem.Add(item);
        }
        else
        {
            Item it = this.m_lstItem.Find(new Predicate<Item>((q) =>
            {
                return q.m_iTableID == item.m_iTableID;
            }));
            if (it == null)
            {
                this.m_lstItem.Add(item);
            }
            else
            {
                UpdateItem(item);
            }
        }

    }

    /// <summary>
    /// 获取物品列表
    /// </summary>
    /// <returns></returns>
    public List<Item> GetAllItem()
    {
        return new List<Item>(this.m_lstItem);
    }

    /// <summary>
    /// 更新物品
    /// </summary>
    /// <param name="item"></param>
    public void UpdateItem(Item item)
    {
        if (item == null)
        {
            Debug.LogError("Update Item is Null.");
        }

        //更新数据
        for (int i = 0; i < this.m_lstItem.Count; i++)
        {
            if (this.m_lstItem[i].m_iTableID == item.m_iTableID)
            {
                this.m_lstItem[i].m_iNum = item.m_iNum;
                this.m_lstItem[i].m_iDummyNum = item.m_iDummyNum;

                return;
            }
        }

        Debug.LogError("Can not find this Item. Error Item ID : " + item.m_iID);
    }

    /// <summary>
    /// 获取物品数量
    /// </summary>
    /// <param name="tableID"></param>
    /// <returns></returns>
    public int GetItemCountByTableId(int tableID)
    {
        ItemTable tmp = ItemTableManager.GetInstance().GetItem(tableID);
        if (tmp == null)
        {
            return 0;
        }
        //如果是装备，则在item中获取所有是这个tableid的装备数量
        if (ItemTableManager.GetInstance().GetItem(tableID).Type == (int)ITEM_TYPE.EQUIP)
        {
            return m_lstItem.FindAll(new Predicate<Item>((q) =>
            {
                return q.m_iTableID == tableID && q.m_iNum > 0;
            })).Count;
        }
        else  //如果是素材，则直接返回数量字段
        {
            Item it = m_lstItem.Find(new Predicate<Item>((q) =>
            {
                return q.m_iTableID == tableID && q.m_iNum > 0;
            }));
            if (it != null)
            {
                return it.m_iNum;
            }
            return 0;
        }
    }

    /// <summary>
    /// 获取所有物品数量，虚假的临时的
    /// </summary>
    /// <param name="tableid"></param>
    /// <returns></returns>
    public int GetItemCountByTableIdForDummy(int tableID)
    {

        ItemTable tmp = ItemTableManager.GetInstance().GetItem(tableID);
        if (tmp == null)
        {
            return 0;
        }
        //如果是装备，则在item中获取所有是这个tableid的装备数量
        if (tmp.Type == (int)ITEM_TYPE.EQUIP)
        {
            return m_lstItem.FindAll(new Predicate<Item>((q) =>
            {
                return q.m_iTableID == tableID && q.m_iDummyNum > 0; ;
            })).Count;
        }
        else  //如果是素材，则直接返回数量字段
        {
            int re = 0;
            Item it = m_lstItem.Find(new Predicate<Item>((q) =>
            {
                return q.m_iTableID == tableID;
            }));
            if (it != null)
            {
                re = it.m_iDummyNum;
            }
            return re;
        }
    }

    /// <summary>
    /// 删除装备, 需要知道tableID 和 唯一ID
    /// </summary>
    /// <param name="p"></param>
    /// <param name="p_2"></param>
    public void DeleteEquip(int tableID, int ID)
    {
        Item ites = this.m_lstItem.Find(new Predicate<Item>((q) =>
        {
            return q.m_iTableID == tableID && q.m_iID == ID;
        }));

        this.m_lstItem.Remove(ites);

    }

    /// <summary>
    /// 更新物品数量
    /// </summary>
    /// <param name="p"></param>
    /// <param name="p_2"></param>
    public void UpdateItemByID(int id, int num)
    {
        for (int i = 0; i < m_lstItem.Count; i++)
        {
            if (m_lstItem[i].m_iID == id)
            {
                m_lstItem[i].m_iNum = num;
                return;
            }
        }
    }

    /// <summary>
    /// 获取实际仓库占有数量大小 eg 101：100 物品会占用两格挡 101：99 和 101：1
    /// </summary>
    /// <returns></returns>
    public int GetAllItemCount()
    {
        //除去数量是0的Item
        List<Item> m_lstItems = m_lstItem.FindAll(new Predicate<Item>((q) =>
            {
                return q.m_iNum != 0;
            }));
        //获取所有超出最大叠加数量的物品 eg  101： 133 需要变成 101 ：99 ？ 101：34 两个物品，导致数量增加
        List<Item> allMaxItems = m_lstItems.FindAll(new Predicate<Item>((q) => { return q.m_iNum > ItemTableManager.GetInstance().GetItem(q.m_iTableID).MaxNum; }));

        foreach (Item q in allMaxItems)
        {
            int maxNum = ItemTableManager.GetInstance().GetItem(q.m_iTableID).MaxNum;
            List<Item> tmplst = new List<Item>();
            // eg 102 99 / 4 99
            int tmpint = q.m_iNum;
            while (tmpint > 0)
            {
                Item it = new Item(q.m_iTableID);
                it.m_iID = q.m_iID;
                int tmpNum = tmpint - maxNum > 0 ? maxNum : tmpint; //99
                it.m_iNum = tmpNum;  //99
                tmplst.Add(it);

                tmpint -= tmpNum; //3
            }

            int index = m_lstItems.IndexOf(q);
            m_lstItems.Remove(q);
            m_lstItems.InsertRange(index, tmplst);

        }

        if (m_lstItems != null && m_lstItems.Count > 0)
        {
            return m_lstItems.Count;
        }

        return 0;
    }

    /// <summary>
    /// 设置所有物品不是new
    /// </summary>
    public void SetAllItemOld()
    {
        for (int i = 0; i < m_lstItem.Count; i++)
        {
            m_lstItem[i].m_bNew = false;
        }
    }

    /// <summary>
    /// 删除该ID物品
    /// </summary>
    /// <param name="id"></param>
    public void DeleteItem(int id)
    {
        for (int i = 0; i < this.m_lstItem.Count; i++)
        {
            if (m_lstItem[i].m_iID == id)
            {
                m_lstItem.Remove(m_lstItem[i]);
                return;
            }
        }
       
    }

    /// <summary>
    /// 检查最新解锁的装备是否满足
    /// </summary>
    /// <returns></returns>
    public bool CheckNewEquipWarn(int dis)
    {
        bool re = false;

        //最近一次建筑升级解锁的新装备
        List<int> newEquip = BuildingTableManager.GetInstance().GetLastBuildEquipItem(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.EQUIP).m_iLevel,dis);
        if (newEquip.Count == 0)
        {
            return false;
        }

        //遍历所有解锁的装备id
        foreach (int itemTableId in newEquip)
        {
            if (CanCombined(itemTableId))  //有一个符合 就可提示
            {
                return true;
            }
        }

        return re;
    }

    /// <summary>
    /// 素材和农场点是否足够合成所需
    /// </summary>
    /// <param name="tableID"></param>
    /// <returns></returns>
    public bool CanCombined(int tableID)
    {
        ItemCompositeTable m_clstTable = ItemCompositeTableManager.GetInstance().GetItemCompositeTable(tableID);
        bool canCombined = true;
        //遍历所有需要合成的素材数量满足
        for (int i = 0; i < m_clstTable.LstNeedID.Count; i++)
        {
            int nedID = m_clstTable.LstNeedID[i];
            int nedCount = m_clstTable.LstNeedNum[i];

            ItemTable ned = ItemTableManager.GetInstance().GetItem(nedID);

            int nowCount = 0;
            if (ned.Type == (int)ITEM_TYPE.EQUIP)
            {
                nowCount = Role.role.GetItemProperty().GetItemCountByTableId(ned.ID);
                nowCount -= Role.role.GetItemProperty().GetEquipNumByTableId(ned.ID);
            }
            else if (ned.Type == (int)ITEM_TYPE.CONSUME)
            {
                nowCount = Role.role.GetItemProperty().GetItemCountByTableId(ned.ID);
                Item batitem = Role.role.GetItemProperty().GetBattleItemByTableID(ned.ID);
                if (batitem != null)
                {
                    nowCount -= batitem.m_iNum;
                }
            }
            else
            {
                nowCount = Role.role.GetItemProperty().GetItemCountByTableId(ned.ID);
            }

            if (nedCount > nowCount)
            {
                return false;
            }
        }

        ItemTable tab = ItemTableManager.GetInstance().GetItem(tableID);
        if (tab.Type == (int)ITEM_TYPE.EQUIP)
        {
            if (m_clstTable.NeedFarmPoint > Role.role.GetBaseProperty().m_iFarmPoint)
            {
                return  false;
            }
        }

        return canCombined;
    }

    /// <summary>
    /// 检查最新解锁的物品是否满足
    /// </summary>
    /// <returns></returns>
    public bool CheckNewItemWarn(int dis)
    {
        bool re = false;

        //最近一次建筑升级解锁的新消耗品
        List<int> newItem = BuildingTableManager.GetInstance().GetLastBuildItem(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.ITEM).m_iLevel,dis);
        if (newItem.Count == 0)
        {
            return false;
        }

        //遍历所有解锁的装备id
        foreach (int itemTableId in newItem)
        {
            if (CanCombined(itemTableId))  //有一个符合 就可提示
            {
                return true;
            }
        }

        return re;
    }

    //用于采集零时计算的公开接口
    /// <summary>
    /// 增加物品
    /// 1，对于消耗品和素材，如果已经存在该物品，就更新，如果不存在就加入
    /// 2. 对于装备，直接加入
    /// </summary>
    /// <param name="item"></param>
    public List<Item> AddItem(List<Item> allitem, Item item)
    {
        if (item.m_eType == ITEM_TYPE.EQUIP)
        {
            allitem.Add(item);
        }
        else
        {
            Item it = allitem.Find(new Predicate<Item>((q) =>
            {
                return q.m_iTableID == item.m_iTableID;
            }));
            if (it == null)
            {
                allitem.Add(item);
            }
            else
            {
                UpdateItem(allitem, item);
            }
        }

        return allitem;
    }

    /// <summary>
    /// 虚拟更新物品
    /// </summary>
    /// <param name="item"></param>
    public void UpdateItem(List<Item> allitem, Item item)
    {
        for (int i = 0; i < allitem.Count; i++)
        {
            if (allitem[i].m_iTableID == item.m_iTableID)
            {
                allitem[i].m_iNum += item.m_iNum;
                //allitem[i] = item;
                return;
            }
        }
    }

    /// <summary>
    /// 获取实际仓库占有数量大小 eg 101：100 物品会占用两格挡 101：99 和 101：1
    /// </summary>
    /// <returns></returns>
    public int GetAllItemCount(List<Item> allitem)
    {
        //除去数量是0的Item
        List<Item> m_lstItems = allitem.FindAll(new Predicate<Item>((q) =>
        {
            return q.m_iNum != 0;
        }));
        //获取所有超出最大叠加数量的物品 eg  101： 133 需要变成 101 ：99 ？ 101：34 两个物品，导致数量增加
        List<Item> allMaxItems = m_lstItems.FindAll(new Predicate<Item>((q) => { return q.m_iNum > ItemTableManager.GetInstance().GetItem(q.m_iTableID).MaxNum; }));

        foreach (Item q in allMaxItems)
        {
            int maxNum = ItemTableManager.GetInstance().GetItem(q.m_iTableID).MaxNum;
            List<Item> tmplst = new List<Item>();
            // eg 102 99 / 4 99
            int tmpint = q.m_iNum;
            while (tmpint > 0)
            {
                Item it = new Item(q.m_iTableID);
                it.m_iID = q.m_iID;
                int tmpNum = tmpint - maxNum > 0 ? maxNum : tmpint; //99
                it.m_iNum = tmpNum;  //99
                tmplst.Add(it);

                tmpint -= tmpNum; //3
            }

            int index = m_lstItems.IndexOf(q);
            m_lstItems.Remove(q);
            m_lstItems.InsertRange(index, tmplst);

        }

        if (m_lstItems != null && m_lstItems.Count > 0)
        {
            //Debug.LogError(m_lstItems.Count);
            return m_lstItems.Count;
        }

        return 0;
    }

    /// <summary>
    /// 清空零时数据 itemid=-1的那些
    /// </summary>
    public void RemoveTmpItem()
    {
        this.m_lstItem.RemoveAll(q => { return q.m_iID == -2; });
    }

    public void UpdateItemByID(Item item)
    {
        if (item == null)
        {
            Debug.LogError("Update Item is Null.");
        }

        //更新数据
        for (int i = 0; i < this.m_lstItem.Count; i++)
        {
            if (this.m_lstItem[i].m_iID == item.m_iID)
            {
                this.m_lstItem[i].m_iNum = item.m_iNum;
                this.m_lstItem[i].m_iDummyNum = item.m_iDummyNum;

                return;
            }
        }

        Debug.LogError("Can not find this Item. Error Item ID : " + item.m_iID);
    }
}