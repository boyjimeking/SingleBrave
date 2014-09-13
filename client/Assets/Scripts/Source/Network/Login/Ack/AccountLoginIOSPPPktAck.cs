using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;


//  AccountLoginIOSPPPktAck.cs
//  Author: Lu Zexi
//  2014-04-04


/// <summary>
/// 帐号登录IOS应答数据包
/// </summary>
public class AccountLoginIOSPPPktAck : HTTPPacketAck
{
    public int m_iUid; //账号ID
    public string m_strToken=string.Empty;   //token
    public int m_iResult;  //state

    // public AccountLoginIOSPPPktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.ACCOUNT_IOS_PP_LOGIN;
    // }
}


///// <summary>
///// PP助手登录应答数据包工厂类
///// </summary>
//public class AccountLoginIOSPPPktFactory : HTTPPacketFactory
//{
//    public AccountLoginIOSPPPktFactory()
//    {
//        //
//    }
//
//    /// <summary>
//    /// 获取ACTION
//    /// </summary>
//    /// <returns></returns>
//    public override string GetPacketAction()
//    {
//        return PACKET_DEFINE.ACCOUNT_IOS_PP_LOGIN;
//    }
//
//    /// <summary>
//    /// 创建数据包
//    /// </summary>
//    /// <param name="json"></param>
//    /// <returns></returns>
//    public override HTTPPacketRequest Create(IJSonObject json)
//    {
//        AccountLoginIOSPPPktAck ack = PACKET_HEAD.PACKET_ACK_HEAD<AccountLoginIOSPPPktAck>(json);
//
//        if (ack.m_iErrorCode != 0)
//        {
//            return ack;
//        }
//
//        IJSonObject data = json["data"];
//
//        ack.m_iUid = data["uid"].Int32Value;
//        ack.m_iResult = data["status"].Int32Value;
//        if (data.Contains("token"))
//        {
//            ack.m_strToken = data["token"].StringValue;
//        }
//
//        return ack;
//    }
//}
