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
public class AccountLoginIOSPPPktReq : HTTPPacketBase
{
    public int m_iPPUid;    //PP助手帐号ID
    public string m_strToken;   //Token;

    public AccountLoginIOSPPPktReq()
    {
        this.m_strAction = PACKET_DEFINE.ACCOUNT_IOS_PP_LOGIN;
    }

    /// <summary>
    /// 获取需求
    /// </summary>
    /// <returns></returns>
    public override string GetRequire()
    {
        string req = "";
        req += "pp_uid=" + this.m_iPPUid + "&token=" + this.m_strToken;
        return req;
    }

}
