using UnityEngine;
using System.Collections;
using Game.Network;

//  BattleItemGetPktReq.cs
//  Author: sanvey
//  2013-12-25

/// <summary>
/// 英雄出售请求
/// </summary>
public class BattleItemGetPktReq : HTTPPacketRequest
{
    public int m_iPid;  //Pid

    public BattleItemGetPktReq()
    {
        this.m_strAction = PACKET_DEFINE.BATTLE_ITEM_GET_REQ;
    }
}



/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	
	/// <summary>
	/// 发送物品获取数据
	/// </summary>
	/// <param name="pid"></param>
	public static void SendBattleItemGet(int pid)
	{
		BattleItemGetPktReq req = new BattleItemGetPktReq();
		req.m_iPid = pid;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}