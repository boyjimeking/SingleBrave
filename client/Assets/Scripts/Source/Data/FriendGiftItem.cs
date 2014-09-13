using System;
using System.Collections.Generic;
using UnityEngine;


//  FriendGiftItem.cs
//  Author: Cheng Xia
//  2013-11-14

/// <summary>
/// 物品
/// </summary>
public class FriendGiftItem
{
    public int m_iID;
    public int m_iTypeID;
    public int m_iNum;  //数量
    public string m_strSpiritName;  //图片集名称
    public string m_strNumText; //礼物数量显示文字
    public GiftType m_eType; //礼物边框类型
    public string m_strName;    //礼物名字
    public string m_strDesc;    //礼物描述

    public FriendGiftItem()
    {
    }
}
