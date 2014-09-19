using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;


//  AccountBoundPktReq.cs
//  Author: Lu Zexi
//  2014-02-18




/// <summary>
/// 帐号绑定请求
/// </summary>
public class AccountBoundPktReq : HTTPPacketRequest
{
    public string m_strUser;    //用户名
    public string m_strPassword;    //密码

    public string m_strNewUser; //新用户名
    public string m_strNewPassword;     //新密码

    public AccountBoundPktReq()
    {
        this.m_strAction = PACKET_DEFINE.ACCOUNT_BOUND_REQ;
    }
}



/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 发送帐号绑定请求
	/// </summary>
	/// <param name="userName"></param>
	/// <param name="passWord"></param>
	/// <param name="newUsername"></param>
	/// <param name="newPassword"></param>
	public static void SendAccountBoundReq(string userName, string passWord, string newUsername, string newPassword)
	{
		AccountBoundPktReq req = new AccountBoundPktReq();
		req.m_strUser = userName;
		req.m_strPassword = passWord;
		req.m_strNewUser = newUsername;
		req.m_strNewPassword = newPassword;
		
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}
}
