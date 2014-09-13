using System;
using System.Collections.Generic;
using UnityEngine;


//  Item.cs
//  Author: Lu Zexi
//  2013-11-14


/// <summary>
/// 物品类型
/// </summary>
public enum ITEM_TYPE
{
    CONSUME = 1,    //消耗
    MATERIAL = 2,   //材料
    EQUIP = 3,      //装备
}


/// <summary>
/// 物品
/// </summary>
public class Item
{
    public int m_iID;   //唯一标识
    public int m_iTableID;  //配置表ID
    public int m_iNum = 0;  //数量
    public int m_iDummyNum; //虚拟数量 临时添加或删除时显示用
    public bool m_bNew = false; //物品是否最新获得

    //配置表
    public string m_strName;    //名字
    public string m_strShortName; //缩略名
    public string m_strSprName;//图集名 sunyi 2013-12-2 add
    public string m_strDesc; //描述
    public string m_strDetail; //详细简介
    public ITEM_TYPE m_eType; //类型
    public int m_iEvent;    //事件
    public float m_fAttackInc;  //攻击增加比率
    public float m_fDefenceInc; //防御增加比率
    public float m_fRecoverInc; //回复增加比率
    public float m_fMaxHpInc;   //MAXHP 增加比率


    public Item(int tableID)
    {
        ItemTable table = ItemTableManager.GetInstance().GetItem(tableID);

        if (table == null)
        {
            Debug.LogError("Item table is null. tableid : " + tableID);
            return;
        }

        this.m_iTableID = tableID;
        this.m_eType = (ITEM_TYPE)table.Type;
        this.m_strName = table.Name;
        this.m_strShortName = table.ShortName;
        this.m_strSprName = table.SpiritName;
        this.m_strDesc = table.Desc;
        this.m_strDetail = table.Detail;
        this.m_iEvent = table.EventID;

        this.m_fAttackInc = table.Attack;
        this.m_fDefenceInc = table.Defence;
        this.m_fRecoverInc = table.Recover;
        this.m_fMaxHpInc = table.MaxHP;
    }
}
