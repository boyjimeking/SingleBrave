using System;
using System.Collections.Generic;

using Game.Network;


//  AccountLoginPktAck.cs
//  Author: Lu Zexi
//  2013-11-14



/// <summary>
/// 帐号登录应答数据包
/// </summary>
public class AccountLoginPktAck : HTTPPacketAck
{
    public int m_iUid; //账号ID
    public bool m_bBound;   //是否已绑定
    public string m_strToken;   //token

    // public AccountLoginPktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.ACCOUNT_LOGIN_REQ;
    // }
}


///// <summary>
///// 帐号登录应答数据包工厂类
///// </summary>
//public class AccountLoginPktAckFactory : HTTPPacketFactory
//{
//    /// <summary>
//    /// 获取数据包Action
//    /// </summary>
//    /// <returns></returns>
//    public override string GetPacketAction()
//    {
//        return PACKET_DEFINE.ACCOUNT_LOGIN_REQ;
//    }
//
//    /// <summary>
//    /// 创建数据包
//    /// </summary>
//    /// <param name="json"></param>
//    /// <returns></returns>
//    public override HTTPPacketRequest Create(IJSonObject json)
//    {
//        AccountLoginPktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<AccountLoginPktAck>(json);
//
//        if (pkt.header.code != 0)
//        {
//            return pkt;
//        }
//
//        IJSonObject data = json["data"];
//        pkt.m_iUid = data["uid"].Int32Value;
//        pkt.m_bBound = data["bind"].Int32Value > 0;
//        pkt.m_strToken = data["token"].StringValue;
//
//        return pkt;
//    }
//}
