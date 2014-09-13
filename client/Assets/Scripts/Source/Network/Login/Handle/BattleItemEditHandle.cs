using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Network;

//  BattleItemEditHandle.cs
//  Author: sanvey
//  2013-12-17

//物品编辑请求应答句柄
public class BattleItemEditHandle
{
    /// <summary>
    /// 获得Action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.BATTLE_ITEM_EDIT_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketRequest packet)
    {
        BattleItemEditPktAck ack = (BattleItemEditPktAck)packet;

        GAME_LOG.LOG("code :" + ack.m_iErrorCode);
        GAME_LOG.LOG("desc :" + ack.m_strErrorDes);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return false;
        }

        //成功
        if (ack.m_iErrorCode == 0)
        {
            //Item[] m_vecBattleItem = Role.role.GetItemProperty().GetAllBattleItem();
            //for (int i = 0; i < m_vecBattleItem.Length; i++)
            //{
            //    if (m_vecBattleItem[i] != null)
            //    {
            //        Item tmp = new Item(m_vecBattleItem[i].m_iTableID);
            //        tmp.m_iNum = Role.role.GetItemProperty().GetItemCountByTableIdWithBattle(m_vecBattleItem[i].m_iTableID) - m_vecBattleItem[i].m_iNum;
            //        Role.role.GetItemProperty().UpdateItem(tmp);
            //    }
            //}
        }

        return true;
    }
}


