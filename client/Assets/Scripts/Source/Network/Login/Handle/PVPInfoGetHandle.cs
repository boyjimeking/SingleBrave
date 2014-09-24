using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Network;

//  PVPInfoGetHandle.cs
//  Author: sanvey
//  2014-2-8

//竞技场基本信息获取请求应答句柄
public class PVPInfoGetHandle
{
    /// <summary>
    /// 获得Action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.PVP_INFO_GET_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketAck packet)
    {
        PVPInfoGetPktAck ack = (PVPInfoGetPktAck)packet;

        GAME_LOG.LOG("code :" + ack.header.code);
        GAME_LOG.LOG("desc :" + ack.header.desc);

        if (ack.header.code != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.header.desc);
            return;
        }

        Role.role.GetBaseProperty().m_iPVPExp = ack.m_iPVP_point;
        Role.role.GetBaseProperty().m_iMyWeekRank = ack.m_iPVPWeekRank;
        Role.role.GetBaseProperty().m_iMyWeekPoint = ack.m_iPVP_WeekPoint;
        Role.role.GetBaseProperty().m_iPVPWin = ack.m_iPVPwin_num;
        Role.role.GetBaseProperty().m_iPVPLose = ack.m_iPVPlose_num;
        Role.role.GetBaseProperty().m_iPVPRank = ack.m_iPVPRank;
        Role.role.GetBaseProperty().m_iPVPMaxExp = ack.m_iPVPMaxExp;

//        SendAgent.SendFriendFightReq(Role.role.GetBaseProperty().m_iPlayerId);

        return;
    }
}


