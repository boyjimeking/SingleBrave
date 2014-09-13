using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

//  HeroBookHandle.cs
//  Author: Lu Zexi
//  2013-12-30




/// <summary>
/// 英雄图鉴句柄
/// </summary>
public class HeroBookHandle
{
    /// <summary>
    /// 获取action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.HERO_BOOK_REQ;
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketRequest packet)
    {
        HeroBookPktAck ack = (HeroBookPktAck)packet;

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return false;
        }

        foreach (int item in ack.m_lstHero)
        {
            Role.role.GetHeroBookProperty().Add(item);
        }

        SendAgent.SendItemBookReq(Role.role.GetBaseProperty().m_iPlayerId);

        return true;
    }

}
