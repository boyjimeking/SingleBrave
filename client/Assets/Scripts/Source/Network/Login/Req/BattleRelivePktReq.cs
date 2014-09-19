using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;



//  BattleRelivePktReq.cs
//  Author: Lu Zexi
//  2014-03-04


/// <summary>
/// 战斗复活请求
/// </summary>
public class BattleRelivePktReq : HTTPPacketRequest
{
    public int m_iPID;  //玩家ID
    public int m_iReliveNum;    //复活次数

    public BattleRelivePktReq()
    {
        this.m_strAction = PACKET_DEFINE.BATTLE_RELIVE_REQ;
    }

}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 发送战斗复活
	/// </summary>
	/// <param name="pid"></param>
	/// <param name="num"></param>
	public static void SendBattleRelive(int pid, int num)
	{
		BattleRelivePktReq req = new BattleRelivePktReq();
		req.m_iPID = pid;
		req.m_iReliveNum = num;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}


