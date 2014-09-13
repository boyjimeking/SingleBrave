
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

    // /// <summary>
    // /// 获取请求参数
    // /// </summary>
    // /// <returns></returns>
    // public override string GetRequire()
    // {
    //     return "username="+this.m_strUserName+"&password=" + this.m_strPassword;
    // }

}
