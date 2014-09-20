using UnityEngine;
using System.Collections;
using Game.Network;

//  AccountAutoRegistPacketReq.cs
//  Author: Lu Zexi
//  2013-11-14



/// <summary>
/// 自动注册请求包
/// </summary>
public class AccountAutoRegistPacketReq : HTTPPacketRequest
{

    public AccountAutoRegistPacketReq()
    {
        this.m_strAction = PACKET_DEFINE.AUTO_REGIST_REQ;
    }

}



/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 角色信息请求
	/// </summary>
	public static void SendAccountAutoRegistReq()
	{
		AccountAutoRegistPacketReq req = new AccountAutoRegistPacketReq();
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION,
		                                  req,AccountAutoRegistHandle.Excute,
		                                  account_auto_regist_handle);
	}

	/// <summary>
	/// Process_s the handle.
	/// </summary>
	/// <param name="req">Req.</param>
	private static HTTPPacketAck account_auto_regist_handle( HTTPPacketRequest req )
	{
		AccountAutoRegistPktAck ack = new AccountAutoRegistPktAck();
		ack.m_iUid = 1;
		ack.m_strToken = "1";
		ack.m_strUsrName = "dummy";
		ack.m_strPassword = "dummy";
		return ack;
	}
}
