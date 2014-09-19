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
}



/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	
	/// <summary>
	/// 发送队伍编辑数据
	/// </summary>
	/// <param name="pid"></param>
	public static void SendTeamEditor(int pid, List<int[]> teams, int teamID)
	{
		TeamEditorPktReq req = new TeamEditorPktReq();
		req.m_iPid = pid;
		req.m_teams = teams;
		req.m_selectTeam = teamID;
		
		SessionManager.GetInstance().SendReady(SESSION_DEFINE.LOGIN_SESSION, req);
	}
}

