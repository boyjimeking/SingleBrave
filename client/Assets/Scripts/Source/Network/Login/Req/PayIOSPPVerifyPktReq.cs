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
public class PayIOSPPVerifyPktReq :HTTPPacketBase
{
    public int m_iPID;  //角色ID
    public int m_iPayID;    //支付订单号
    //public string m_strVerify; //验证字符串

    public PayIOSPPVerifyPktReq()
    {
        this.m_strAction = PACKET_DEFINE.PAY_IOS_PP_VERIFY;
    }

    /// <summary>
    /// 获取请求
    /// </summary>
    /// <returns></returns>
    public override string GetRequire()
    {
        string req = "pid=" + this.m_iPID + "&pay_id=" + this.m_iPayID;

        req = PACKET_HEAD.PACKET_REQ_END(ref req);

        return req;
    }
}
