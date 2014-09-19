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
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}
}
