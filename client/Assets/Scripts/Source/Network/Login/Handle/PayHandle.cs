using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;


//  PayHandle.cs
//  Author: Lu Zexi
//  2014-04-02


/// <summary>
/// 支付句柄
/// </summary>
class PayHandle
{

    /// <summary>
    /// 获取ACTION
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.PAY_REQ;
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketRequest packet)
    {

        PayPktAck ack = packet as PayPktAck;

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return;
        }

        Role.role.GetPayProperty().m_iPayID = ack.m_iPayID;
        Role.role.GetPayProperty().m_iPayNum = ack.m_iPayNum;
        Role.role.GetPayProperty().m_iPrice = ack.m_iPrice;
        Role.role.GetPayProperty().m_iGoodID = ack.m_iGoodID;
        Role.role.GetPayProperty().m_strTypeID = ack.m_strType_ID;

        Role.role.GetPayProperty().m_iState = 1;

        PlatformManager.GetInstance().ShowPayment(ack.m_iPayID, ack.m_iGoodID , ack.m_iPayNum , ack.m_strType_ID , ack.m_iPrice);

        return;
    }

}
