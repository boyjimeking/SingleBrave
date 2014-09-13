using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Network;

//获取战友信息请求句柄
//Author sunyi
//2013-12-23
public class FriendFightHandle : HTTPHandleBase
{
    /// <summary>
    /// 获得Action
    /// </summary>
    /// <returns></returns>
    public override string GetAction()
    {
        return PACKET_DEFINE.FRIEND_FIGHT_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public override bool Excute(HTTPPacketBase packet)
    {
        FriendFightPktAck ack = (FriendFightPktAck)packet;

        GAME_LOG.LOG("code :" + ack.m_iErrorCode);
        GAME_LOG.LOG("desc :" + ack.m_strErrorDes);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return false;
        }

        Role.role.GetBattleFriendProperty().RemoveAll();

        for (int i = 0; i < ack.m_lstBattleFriendEx.Count; i++)
        {
            Role.role.GetBattleFriendProperty().AddBattleFriend(ack.m_lstBattleFriendEx[i]);
        }

        for (int i = 0; i < ack.m_lstBattleFriend.Count; i++)
        {
            Role.role.GetBattleFriendProperty().AddBattleFriend(ack.m_lstBattleFriend[i]);
        }

        SendAgent.SendPVPWeekRankReq(Role.role.GetBaseProperty().m_iPlayerId);

        return true;
    }
}

