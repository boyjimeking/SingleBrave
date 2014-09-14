using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Network;

//  BattleItemGetHandle.cs
//  Author: sanvey
//  2013-12-17

//物品获取请求应答句柄
public class BattleItemGetHandle
{
    /// <summary>
    /// 获得Action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.BATTLE_ITEM_GET_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketAck packet)
    {
        BattleItemGetPktAck ack = (BattleItemGetPktAck)packet;

        GAME_LOG.LOG("code :" + ack.header.code);
        GAME_LOG.LOG("desc :" + ack.header.desc);

        if (ack.header.code != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.header.desc);
            
        }

        Role.role.GetItemProperty().UpdateBattleItem(ack.m_iPos0, ack.m_iPos0_n, 0);
        Role.role.GetItemProperty().UpdateBattleItem(ack.m_iPos1, ack.m_iPos1_n, 1);
        Role.role.GetItemProperty().UpdateBattleItem(ack.m_iPos2, ack.m_iPos2_n, 2);
        Role.role.GetItemProperty().UpdateBattleItem(ack.m_iPos3, ack.m_iPos3_n, 3);
        Role.role.GetItemProperty().UpdateBattleItem(ack.m_iPos4, ack.m_iPos4_n, 4);


        //GUI_FUNCTION.LOADING_HIDEN();
        SendAgent.SendHeroBookReq(Role.role.GetBaseProperty().m_iPlayerId);
        
    }
}