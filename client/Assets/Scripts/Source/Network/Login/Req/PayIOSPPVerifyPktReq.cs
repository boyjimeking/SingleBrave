using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

//  PayIOSVerifyPktReq.cs
//  Author: Lu Zexi
//  2014-04-04



/// <summary>
/// 支付IOS验证数据包
/// </summary>
public class PayIOSPPVerifyPktReq :HTTPPacketRequest
{
    public int m_iPID;  //角色ID
    public int m_iPayID;    //支付订单号
    //public string m_strVerify; //验证字符串

    public PayIOSPPVerifyPktReq()
    {
        this.m_strAction = PACKET_DEFINE.PAY_IOS_PP_VERIFY;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// IOS PP助手验证
	/// </summary>
	/// <param name="pid"></param>
	/// <param name="payid"></param>
	/// <param name="verify"></param>
	public static void SendPayIOSPPVerify(int pid, int payid)
	{
		PayIOSPPVerifyPktReq req = new PayIOSPPVerifyPktReq();
		req.m_iPID = pid;
		req.m_iPayID = payid;
		//req.m_strVerify = verify;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}


