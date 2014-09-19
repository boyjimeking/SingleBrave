using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;


//  BattleGateFailReq.cs
//  Author: Lu Zexi
//  2014-03-05



/// <summary>
/// 关卡战斗失败请求
/// </summary>
public class BattleGateFailPktReq : HTTPPacketRequest
{
    public int m_iPID;  //玩家ID
    public int m_iBattleID; //战斗ID
    public int[] m_vecReadyItemNum; //战斗物品数量

    public BattleGateFailPktReq()
    {
        this.m_strAction = PACKET_DEFINE.BATTLE_GATE_FAIL_REQ;
    }

}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 发送战斗关卡失败
	/// </summary>
	/// <param name="pid"></param>
	/// <param name="vecItemNum"></param>
	public static void SendBattleGateFail(int pid , int battle_id , int[] vecItemNum)
	{
		BattleGateFailPktReq req = new BattleGateFailPktReq();
		req.m_iPID = pid;
		req.m_iBattleID = battle_id;
		req.m_vecReadyItemNum = vecItemNum;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}

