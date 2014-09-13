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
public class AccountBoundPktReq : HTTPPacketBase
{
    public string m_strUser;    //用户名
    public string m_strPassword;    //密码

    public string m_strNewUser; //新用户名
    public string m_strNewPassword;     //新密码

    public AccountBoundPktReq()
    {
        this.m_strAction = PACKET_DEFINE.ACCOUNT_BOUND_REQ;
    }

    /// <summary>
    /// 获取请求
    /// </summary>
    /// <returns></returns>
    public override string GetRequire()
    {
        string str = "username=" + this.m_strUser + "&password=" + this.m_strPassword + "&ua=" + this.m_strNewUser + "&pswd=" + this.m_strNewPassword;
        return str;
    }

}
