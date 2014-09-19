using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;


//  AccountLoginIOSPPPktReq.cs
//  Author: Lu Zexi
//  2014-04-04



/// <summary>
/// PP助手登录
/// </summary>
public class AccountLoginIOSPPPktReq : HTTPPacketRequest
{
    public int m_iPPUid;    //PP助手帐号ID
    public string m_strToken;   //Token;

    public AccountLoginIOSPPPktReq()
    {
        this.m_strAction = PACKET_DEFINE.ACCOUNT_IOS_PP_LOGIN;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 发送PP助手登录
	/// </summary>
	public static void SendAccountIOSPPLogin( int uid , string token )
	{
		AccountLoginIOSPPPktReq req = new AccountLoginIOSPPPktReq();
		req.m_iPPUid = uid;
		req.m_strToken = token;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}

