using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Network;
//战绩记录数据请求类
//Author sunyi
//2014-02-28
public class PlayerBattleRecordGetPktReq : HTTPPacketRequest
{
    public int m_iPid;//玩家id

    public PlayerBattleRecordGetPktReq()
    {
        this.m_strAction = PACKET_DEFINE.BATTLE_RECORD_GET_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 发送战绩请求
	/// </summary>
	/// <param name="pid"></param>
	public static void SendBattleRecord(int pid)
	{
		PlayerBattleRecordGetPktReq req = new PlayerBattleRecordGetPktReq();
		req.m_iPid = pid;
		
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}

