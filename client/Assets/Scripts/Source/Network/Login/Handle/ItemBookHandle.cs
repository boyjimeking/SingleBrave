using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Network;

//  ItemBookHandle.cs
//  Author: Lu Zexi
//  2013-12-30




/// <summary>
/// 物品图鉴句柄
/// </summary>
public class ItemBookHandle
{
    public ItemBookHandle()
    { 
        //
    }

    /// <summary>
    /// 获取action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.ITEM_BOOK_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketRequest packet)
    {
        ItemBookPktAck ack = (ItemBookPktAck)packet;

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return;
        }

        foreach (int item in ack.m_lstItem)
        {
            Role.role.GetItemBookProperty().AddItem(item);
        }

        SendAgent.SendActivityFubenFavourableEeq(Role.role.GetBaseProperty().m_iPlayerId);

        return;
    }
}
