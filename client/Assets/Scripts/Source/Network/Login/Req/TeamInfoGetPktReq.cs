using UnityEngine;
using System.Collections;
using Game.Network;

//  TeamInfoGetPktReq.cs
//  Author: sanvey
//  2013-12-11

/// <summary>
/// 获取玩家队伍信息请求包
/// </summary>
public class TeamInfoGetPktReq : HTTPPacketRequest
{
    public int m_iPid;  //玩家Pid

    public TeamInfoGetPktReq()
    {
        this.m_strAction = PACKET_DEFINE.GET_TEAMS_REQ;
    }
}



/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	
	/// <summary>
	/// 获取玩家队伍信息
	/// </summary>
	/// <param name="pid">玩家Pid</param>
	public static void SendTeamInfoGetPktReq(int pid)
	{
		TeamInfoGetPktReq req = new TeamInfoGetPktReq();
		req.m_iPid = pid;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}