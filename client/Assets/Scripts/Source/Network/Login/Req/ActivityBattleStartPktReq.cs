using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

//  ActivityBattleStartPktReq.cs
//  Author: Lu Zexi
//  2014-01-08



/// <summary>
/// 活动战斗开始
/// </summary>
public class ActivityBattleStartPktReq : HTTPPacketRequest
{
    public int m_iPid;  //玩家ID
    public int m_iFubenID;   //副本ID
    public int m_iGateIndex;    //关卡索引

    public ActivityBattleStartPktReq()
    {
        this.m_strAction = PACKET_DEFINE.ACTIVITY_BATTLE_START_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 发送活动战斗开始请求
	/// </summary>
	/// <param name="pid"></param>
	/// <param name="fubenid"></param>
	/// <param name="gateIndex"></param>
	public static void SendActivityBattleStartReq(int pid, int fubenid, int gateIndex)
	{
		ActivityBattleStartPktReq req = new ActivityBattleStartPktReq();
		req.m_iPid = pid;
		req.m_iFubenID = fubenid;
		req.m_iGateIndex = gateIndex;
		
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}


