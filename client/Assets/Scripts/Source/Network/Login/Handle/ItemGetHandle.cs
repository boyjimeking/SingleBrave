using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Network;

//  ItemGetHandle.cs
//  Author: sanvey
//  2013-12-23

//获取物品请求应答句柄
public class ItemGetHandle : HTTPHandleBase
{
    /// <summary>
    /// 获得Action
    /// </summary>
    /// <returns></returns>
    public override string GetAction()
    {
        return PACKET_DEFINE.ITEM_GET_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public override bool Excute(HTTPPacketBase packet)
    {
        ItemGetPktAck ack = (ItemGetPktAck)packet;

        GAME_LOG.LOG("code :" + ack.m_iErrorCode);
        GAME_LOG.LOG("desc :" + ack.m_strErrorDes);

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return false;
        }

        foreach (var q in ack.m_lstItems)
        {
            Item tmp = new Item(q.m_iItem_id);
            tmp.m_iID = q.m_iId;
            tmp.m_iNum = q.m_iItem_num;
            Role.role.GetItemProperty().AddItem(tmp);
        }

        SendAgent.SendBattleItemGet(Role.role.GetBaseProperty().m_iPlayerId);

        return true;
    }
}


