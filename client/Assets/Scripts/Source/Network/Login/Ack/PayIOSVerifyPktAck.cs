using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;


//  PayIOSAverifyPktAck.cs
//  Author: Lu Zexi
//  2014-04-02



/// <summary>
/// 支付IOS验证应答
/// </summary>
public class PayIOSVerifyPktAck : HTTPPacketAck
{
    public int m_iResult;   //结果
    public int m_iDiamond;  //砖石

    // public PayIOSVerifyPktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.PAY_IOS_VERIFY;
    // }
}


// /// <summary>
// /// 支付验证应答数据包工厂类
// /// </summary>
// public class PayIOSVerifyPktAckFactory : HTTPPacketFactory
// {
//     public PayIOSVerifyPktAckFactory()
//     { 
//         //
//     }

//     /// <summary>
//     /// 获取ACTION
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.PAY_IOS_VERIFY;
//     }


//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(IJSonObject json)
//     {
//         PayIOSVerifyPktAck ack = PACKET_HEAD.PACKET_ACK_HEAD<PayIOSVerifyPktAck>(json);

//         if (ack.m_iErrorCode != 0)
//             return ack;

//         IJSonObject data = json["data"];

//         ack.m_iResult = data["state"].Int32Value;
//         ack.m_iDiamond = data["diamond"].Int32Value;

//         return ack;
//     }

// }