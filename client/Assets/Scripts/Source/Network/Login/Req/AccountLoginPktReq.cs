
using System.Collections.Generic;
using Game.Network;

//  AccountLoginPktReq.cs
//  Author: Lu Zexi
//  2013-12-11



/// <summary>
/// 帐号登录请求包
/// </summary>
public class AccountLoginPktReq : HTTPPacketRequest
{
    public string m_strUserName;    //帐号
    public string m_strPassword;    //密码

    public AccountLoginPktReq()
    {
        this.m_strAction = PACKET_DEFINE.ACCOUNT_LOGIN_REQ;
    }

}



/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	
	/// <summary>
	/// 帐号登录
	/// </summary>
	/// <param name="userName"></param>
	/// <param name="password"></param>
	public static void SendAccountLogin(string userName, string password)
	{
		AccountLoginPktReq req = new AccountLoginPktReq();
		req.m_strUserName = userName;
		req.m_strPassword = password;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}


