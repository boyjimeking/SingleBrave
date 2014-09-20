using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Network;

//获取战友信息请求句柄
//Author sunyi
//2013-12-23
public class FriendFightHandle
{
    /// <summary>
    /// 获得Action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.FRIEND_FIGHT_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketAck packet)
    {
        FriendFightPktAck ack = (FriendFightPktAck)packet;

        GAME_LOG.LOG("code :" + ack.header.code);
        GAME_LOG.LOG("desc :" + ack.header.desc);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.header.code != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.header.desc);
            
        }

		BattleFriend battleFriend = CModelMgr.sInstance.GetModel<BattleFriend>();
		battleFriend.Clear();

        for (int i = 0; i < ack.m_lstBattleFriendEx.Count; i++)
        {
            battleFriend.Add(ack.m_lstBattleFriendEx[i]);
        }

        for (int i = 0; i < ack.m_lstBattleFriend.Count; i++)
        {
            battleFriend.Add(ack.m_lstBattleFriend[i]);
        }

        SendAgent.SendPVPWeekRankReq(Role.role.GetBaseProperty().m_iPlayerId);

        
    }
}

