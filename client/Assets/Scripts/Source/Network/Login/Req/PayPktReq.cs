using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

//  PayPktReq.cs
//  Author: Lu Zexi
//  2014-04-02


/// <summary>
/// 支付请求数据包
/// </summary>
public class PayPktReq : HTTPPacketRequest
{
    public int m_iPID;  //角色ID
    public int m_iGoodID;    //商品ID

    public PayPktReq()
    {
        this.m_strAction = PACKET_DEFINE.PAY_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	
	/// <summary>
	/// 支付请求
	/// </summary>
	/// <param name="payNum"></param>
	/// <param name="payid"></param>
	public static void SendPay( int pid , int good_id)
	{
		PayPktReq req = new PayPktReq();
		req.m_iGoodID = good_id;
		req.m_iPID = pid;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}


