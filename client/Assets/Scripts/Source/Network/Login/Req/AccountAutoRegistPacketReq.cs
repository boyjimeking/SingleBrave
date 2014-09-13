using UnityEngine;
using System.Collections;
using Game.Network;

//  AccountAutoRegistPacketReq.cs
//  Author: Lu Zexi
//  2013-11-14



/// <summary>
/// 自动注册请求包
/// </summary>
public class AccountAutoRegistPacketReq : HTTPPacketBase
{

    public AccountAutoRegistPacketReq()
    {
        this.m_strAction = PACKET_DEFINE.AUTO_REGIST_REQ;
    }

    /// <summary>
    /// 获取请求参数
    /// </summary>
    /// <returns></returns>
    public override string GetRequire()
    {
        return "";
    }

}
