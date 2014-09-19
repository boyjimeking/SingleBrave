using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

//  PVPBattleStartPktReq.cs
//  Author: Lu Zexi
//  2014-02-11



/// <summary>
/// PVP战斗开始
/// </summary>
public class PVPBattleStartPktReq : HTTPPacketRequest
{
    public int m_iPid;  //Pid
    public int m_iTpid; //目标ID

    public PVPBattleStartPktReq()
    {
        this.m_strAction = PACKET_DEFINE.PVP_BATTLE_START_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 发送PVP战斗开始
	/// </summary>
	/// <param name="pid"></param>
	/// <param name="tpid"></param>
	public static void SendPVPBattleStart(int pid, int tpid)
	{
		PVPBattleStartPktReq req = new PVPBattleStartPktReq();
		req.m_iPid = pid;
		req.m_iTpid = tpid;
		
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}

