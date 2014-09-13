using System;
using System.Collections.Generic;


using Game.Network;


//  AccountAutoRegistPktAck.cs
//  Author: Lu Zexi
//  2013-11-14



/// <summary>
/// 自动注册应答数据包
/// </summary>
public class AccountAutoRegistPktAck : HTTPPacketAck
{
    public int m_iUid; //账号ID
    public string m_strUsrName; //账号
    public string m_strPassword;    //密码
    public string m_strToken;   //Token

    // public AccountAutoRegistPktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.AUTO_REGIST_REQ;
    // }
}

//
///// <summary>
///// 自动注册应答数据包工厂类
///// </summary>
//public class AccountAutoRegistPktAckFactory : HTTPPacketFactory
//{
//    /// <summary>
//    /// 获取数据包Action
//    /// </summary>
//    /// <returns></returns>
//    public override string GetPacketAction()
//    {
//        return PACKET_DEFINE.AUTO_REGIST_REQ;
//    }
//
//    /// <summary>
//    /// 创建数据包
//    /// </summary>
//    /// <param name="json"></param>
//    /// <returns></returns>
//    public override HTTPPacketRequest Create(IJSonObject json)
//    {
//        AccountAutoRegistPktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<AccountAutoRegistPktAck>(json);
//
//        if (pkt.m_iErrorCode != 0)
//        {
//            return pkt;
//        }
//
//        IJSonObject data = json["data"];
//        pkt.m_iUid = data["uid"].Int32Value;
//        pkt.m_strUsrName = data["username"].StringValue;
//        pkt.m_strPassword = data["password"].StringValue;
//        pkt.m_strToken = data["token"].StringValue;
//
//        return pkt;
//    }
//}
