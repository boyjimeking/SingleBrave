using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

//  ActivityBattleFailPktReq.cs
//  Author: Lu Zexi
//  2014-03-05




/// <summary>
/// 活动战斗失败请求
/// </summary>
public class ActivityBattleFailPktReq : HTTPPacketRequest
{
    public int m_iPID;  //玩家ID
    public int m_iBattleID; //战斗ID
    public int[] m_vecReadyItemNum; //战斗物品数量

    public ActivityBattleFailPktReq()
    {
        this.m_strAction = PACKET_DEFINE.ACTIVITY_BATTLE_FAIL_REQ;
    }

}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 发送活动战斗失败
	/// </summary>
	/// <param name="pid"></param>
	/// <param name="vecItemNum"></param>
	public static void SendActivityBattleFail(int pid , int battle_id , int[] vecItemNum)
	{
		ActivityBattleFailPktReq req = new ActivityBattleFailPktReq();
		req.m_iPID = pid;
		req.m_iBattleID = battle_id;
		req.m_vecReadyItemNum = vecItemNum;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}
}


