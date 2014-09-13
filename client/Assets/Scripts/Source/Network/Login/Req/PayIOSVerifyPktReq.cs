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


    // /// <summary>
    // /// 获取请求
    // /// </summary>
    // /// <returns></returns>
    // public override string GetRequire()
    // {
    //     string req = "pid=" + this.m_iPID + "&pay_id=" + this.m_iPayID + "&verify=" + this.m_strVerify;

    //     req = PACKET_HEAD.PACKET_REQ_END(ref req);

    //     return req;
    // }

}
