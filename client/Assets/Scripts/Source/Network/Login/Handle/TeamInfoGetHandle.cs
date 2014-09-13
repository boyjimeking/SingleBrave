using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Network;

//  TeamInfoGetHandle.cs
//  Author: sanvey
//  2013-12-11

/// <summary>
/// 获取玩家队伍信息请求应答句柄
/// </summary>
public class TeamInfoGetHandle : HTTPHandleBase
{
    /// <summary>
    /// 获取Action
    /// </summary>
    /// <returns></returns>
    public override string GetAction()
    {
        return PACKET_DEFINE.GET_TEAMS_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public override bool Excute(HTTPPacketBase packet)
    {
        TeamInfoGetPktAck ack = (TeamInfoGetPktAck)packet;
        GAME_LOG.LOG("code :" + ack.m_iErrorCode);
        GAME_LOG.LOG("desc :" + ack.m_strErrorDes);

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return false;
        }

        for (int i = 0; i < ack.m_lstTeams.Count; i++)
        {
            HeroTeam tmp = new HeroTeam();
            tmp.m_vecTeam = ack.m_lstTeams[i].m_vecTeam;
            tmp.m_iLeadID = ack.m_lstTeams[i].m_vecTeam[ack.m_lstTeams[i].m_iLeaderIndex];

            Role.role.GetTeamProperty().AddTeam(i, tmp);
        }
         SendAgent.SendPlayerTaskInfoGetPktReq(Role.role.GetBaseProperty().m_iPlayerId);
    
        return true;
    }
}