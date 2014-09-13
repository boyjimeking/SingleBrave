//  GUITeamEdiorPktReq.cs
//  Author: Cheng Xia
//  2013-12-19

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;


/// <summary>
/// 团队编辑数据包
/// </summary>
public class TeamEditorPktReq : HTTPPacketRequest
{
    public int m_iPid;  //玩家ID
    public List<int[]> m_teams; //队伍数组//
    public int m_selectTeam;    //选择的队伍//
    
    public TeamEditorPktReq()
    {
        this.m_strAction = PACKET_DEFINE.TEAM_EDITOR_REQ;
    }

    // /// <summary>
    // /// 获取数据
    // /// </summary>
    // /// <returns></returns>
    // public override string GetRequire()
    // {
    //     string reqStr = "pid=" + this.m_iPid + "&";

    //     for (int i = 0; i < m_teams.Count; i++)
    //     {
    //         reqStr += "t" + i.ToString() + "=";

    //         for (int j = 0; j < m_teams[i].Length; j++)
    //         {
    //             reqStr += m_teams[i][j].ToString();

    //             if (j < (m_teams[i].Length - 1))
    //             {
    //                 reqStr += "|";
    //             }
    //         }

    //         reqStr += "&";
    //     }

    //     reqStr += "select_team=" + m_selectTeam;

    //     PACKET_HEAD.PACKET_REQ_END(ref reqStr);

    //     return reqStr;
    // }
}
