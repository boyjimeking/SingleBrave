using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;



//  PayPktAck.cs
//  Author: Lu Zexi
//  2014-04-02


/// <summary>
/// 支付应答
/// </summary>
public class PayPktAck : HTTPPacketAck
{
    public int m_iPayID;    //支付ID
    public int m_iGoodID;   //商品ID
    public int m_iPayNum;   //支付数量
    public int m_iPrice;    //价格
    public string m_strType_ID;    //支付ID

    // public PayPktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.PAY_REQ;
    // }
}


// /// <summary>
// /// 支付应答数据包工厂类
// /// </summary>
// public class PayPktAckFactory : HTTPPacketFactory
// {
//     public PayPktAckFactory()
//     { 
//         //
//     }

//     /// <summary>
//     /// 获取ACTION
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.PAY_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(IJSonObject json)
//     {
//         PayPktAck ack = PACKET_HEAD.PACKET_ACK_HEAD<PayPktAck>(json);
//         if (ack.header.code != 0)
//             return ack;

//         IJSonObject data = json["data"];

//         ack.m_iPayID = data["pay_id"].Int32Value;
//         ack.m_iGoodID = data["good_id"].Int32Value;
//         ack.m_iPayNum = data["pay_num"].Int32Value;
//         ack.m_strType_ID = data["type_id"].StringValue;
//         ack.m_iPrice = data["price"].Int32Value;

//         return ack;
//     }
// }