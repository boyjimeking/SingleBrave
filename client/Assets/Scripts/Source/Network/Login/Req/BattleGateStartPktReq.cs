using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;


//  BattleGateStartPktReq.cs
//  Author: Lu Zexi
//  2013-12-19



/// <summary>
/// 战斗关卡请求数据包
/// </summary>
public class BattleGateStartPktReq : HTTPPacketRequest
{
    public int m_iPid;  //玩家ID
    public int m_iWorldID;  //世界ID
    public int m_iAreaIndex;    //区域索引
    public int m_iFubenIndex;   //副本索引
    public int m_iGateIndex;    //关卡索引

    public BattleGateStartPktReq()
    {
        this.m_strAction = PACKET_DEFINE.BATTLE_GATE_START_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	
	/// <summary>
	/// 发送战斗关卡开始请求
	/// </summary>
	/// <param name="pid"></param>
	/// <param name="worldid"></param>
	/// <param name="areaIndex"></param>
	/// <param name="fubenIndex"></param>
	/// <param name="gateIndex"></param>
	public static void SendBattleGateStartReq(int pid, int worldid, int areaIndex, int fubenIndex, int gateIndex)
	{
		BattleGateStartPktReq req = new BattleGateStartPktReq();
		req.m_iPid = pid;
		req.m_iWorldID = worldid;
		req.m_iAreaIndex = areaIndex;
		req.m_iFubenIndex = fubenIndex;
		req.m_iGateIndex = gateIndex;
		
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}
