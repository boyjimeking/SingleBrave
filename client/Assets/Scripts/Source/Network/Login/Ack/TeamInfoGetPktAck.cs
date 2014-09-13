using System;
using System.Collections.Generic;
using CodeTitans.JSon;
using Game.Network;

//  TeamInfoGetPktAck.cs
//  Author: sanvey
//  2013-12-11


/// <summary>
/// 获取玩家队伍信息应答数据包
/// </summary>
public class TeamInfoGetPktAck : HTTPPacketBase
{
    public List<Team> m_lstTeams = new List<Team>();

    public class Team
    {
        public int[] m_vecTeam;    //卡片队伍
        public int m_iLeaderIndex; //队长在队伍中的位置
    }

    public TeamInfoGetPktAck()
    {
        this.m_strAction = PACKET_DEFINE.GET_TEAMS_REQ;
    }
}


/// <summary>
/// 获取玩家队伍信息应答数据包工厂类
/// </summary>
public class TeamInfoGetPktAckFactory : HTTPPacketFactory
{

    /// <summary>
    /// 获取数据包Action
    /// </summary>
    /// <returns></returns>
    public override string GetPacketAction()
    {
        return PACKET_DEFINE.GET_TEAMS_REQ;
    }

    /// <summary>
    /// 创建数据包
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public override HTTPPacketBase Create(IJSonObject json)
    {
        TeamInfoGetPktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<TeamInfoGetPktAck>(json);

        if (pkt.m_iErrorCode != 0)
        {
            return pkt;
        }

        IJSonObject data = json["data"];

        TeamInfoGetPktAck.Team tmp0 = StringToIntVec(data["t0"].StringValue);
        TeamInfoGetPktAck.Team tmp1 = StringToIntVec(data["t1"].StringValue);
        TeamInfoGetPktAck.Team tmp2 = StringToIntVec(data["t2"].StringValue);
        TeamInfoGetPktAck.Team tmp3 = StringToIntVec(data["t3"].StringValue);
        TeamInfoGetPktAck.Team tmp4 = StringToIntVec(data["t4"].StringValue);
        TeamInfoGetPktAck.Team tmp5 = StringToIntVec(data["t5"].StringValue);
        TeamInfoGetPktAck.Team tmp6 = StringToIntVec(data["t6"].StringValue);
        TeamInfoGetPktAck.Team tmp7 = StringToIntVec(data["t7"].StringValue);
        TeamInfoGetPktAck.Team tmp8 = StringToIntVec(data["t8"].StringValue);
        TeamInfoGetPktAck.Team tmp9 = StringToIntVec(data["t9"].StringValue);

        pkt.m_lstTeams.Add(tmp0);
        pkt.m_lstTeams.Add(tmp1);
        pkt.m_lstTeams.Add(tmp2);
        pkt.m_lstTeams.Add(tmp3);
        pkt.m_lstTeams.Add(tmp4);
        pkt.m_lstTeams.Add(tmp5);
        pkt.m_lstTeams.Add(tmp6);
        pkt.m_lstTeams.Add(tmp7);
        pkt.m_lstTeams.Add(tmp8);
        pkt.m_lstTeams.Add(tmp9);


        return pkt;
    }

    /// <summary>
    /// 将0|0|0|0|0|0 转换为 int[] 和 队长位置
    /// </summary>
    /// <param name="str"></param>
    private TeamInfoGetPktAck.Team StringToIntVec(string str)
    {
        TeamInfoGetPktAck.Team tmp = new TeamInfoGetPktAck.Team();
        var arr = str.Split('|');
        int[] iarr = new int[5];
        for (int i = 0; i < 5; i++)
        {
            iarr[i] = int.Parse(arr[i]);
        }
        tmp.m_vecTeam = iarr;
        tmp.m_iLeaderIndex = int.Parse(arr[5]);

        return tmp;
    }
}