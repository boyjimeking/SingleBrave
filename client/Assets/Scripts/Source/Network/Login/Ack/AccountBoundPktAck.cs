using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;



//  AccountBoundPktAck.cs
//  Author: Lu Zexi
//  2014-02-18




/// <summary>
/// 帐号绑定应答数据
/// </summary>
public class AccountBoundPktAck : HTTPPacketAck
{
    public int m_iUid;  //帐号ID

    // public AccountBoundPktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.ACCOUNT_BOUND_REQ;
    // }
}


///// <summary>
///// 帐号绑定数据工厂类
///// </summary>
//public class AccountBoundPktAckFactory : HTTPPacketFactory
//{
//    /// <summary>
//    /// 获取ACTION
//    /// </summary>
//    /// <returns></returns>
//    public override string GetPacketAction()
//    {
//        return PACKET_DEFINE.ACCOUNT_BOUND_REQ;
//    }
//
//    /// <summary>
//    /// 创建数据类
//    /// </summary>
//    /// <param name="json"></param>
//    /// <returns></returns>
//    public override HTTPPacketRequest Create(IJSonObject json)
//    {
//        AccountBoundPktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<AccountBoundPktAck>(json);
//
//        if (pkt.m_iErrorCode != 0)
//        {
//            return pkt;
//        }
//
//        IJSonObject data = json["data"];
//
//        pkt.m_iUid = data["uid"].Int32Value;
//
//        return pkt;
//    }
//}
