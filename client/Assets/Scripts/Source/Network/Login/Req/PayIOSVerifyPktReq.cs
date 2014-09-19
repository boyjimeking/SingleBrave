using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

//  PayVerifyPktReq.cs
//  Author: Lu Zexi
//  2014-04-02



/// <summary>
/// 支付验证请求
/// </summary>
public class PayIOSVerifyPktReq : HTTPPacketRequest
{
    public int m_iPID;  //角色ID
    public int m_iPayID;    //支付订单号
    public string m_strVerify; //验证字符串

    public PayIOSVerifyPktReq()
    {
        this.m_strAction = PACKET_DEFINE.PAY_IOS_VERIFY;
    }

}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// IOS支付验证
	/// </summary>
	/// <param name="payid"></param>
	/// <param name="verify"></param>
	public static void SendPayIOSVerify(int pid ,int payid, string verify)
	{
		PayIOSVerifyPktReq req = new PayIOSVerifyPktReq();
		req.m_iPID = pid;
		req.m_iPayID = payid;
		req.m_strVerify = verify;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}
