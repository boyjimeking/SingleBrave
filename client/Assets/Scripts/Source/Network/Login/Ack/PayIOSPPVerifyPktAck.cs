using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;
using CodeTitans.JSon;

//  PayIOSPPVerifyPktAck.cs
//  Author: Lu Zexi
//  2014-04-04




/// <summary>
/// 支付PP助手验证数据包
/// </summary>
public class PayIOSPPVerifyPktAck : HTTPPacketBase
{
    public int m_iResult;   //结果
    public int m_iDiamond;  //砖石

    public PayIOSPPVerifyPktAck()
    {
        this.m_strAction = PACKET_DEFINE.PAY_IOS_PP_VERIFY;
    }
}


/// <summary>
/// 支付验证应答数据包工厂类
/// </summary>
public class PayIOSPPVerifyPktFactory : HTTPPacketFactory
{
    public PayIOSPPVerifyPktFactory()
    {
        //
    }

    /// <summary>
    /// 获取ACTION
    /// </summary>
    /// <returns></returns>
    public override string GetPacketAction()
    {
        return PACKET_DEFINE.PAY_IOS_PP_VERIFY;
    }


    /// <summary>
    /// 创建数据包
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public override HTTPPacketBase Create(IJSonObject json)
    {
        PayIOSPPVerifyPktAck ack = PACKET_HEAD.PACKET_ACK_HEAD<PayIOSPPVerifyPktAck>(json);

        if (ack.m_iErrorCode != 0)
            return ack;

        IJSonObject data = json["data"];

        ack.m_iResult = data["state"].Int32Value;
        ack.m_iDiamond = data["diamond"].Int32Value;

        return ack;
    }

}