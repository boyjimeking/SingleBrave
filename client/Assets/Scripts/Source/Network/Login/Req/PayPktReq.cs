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
public class PayPktReq : HTTPPacketBase
{
    public int m_iPID;  //角色ID
    public int m_iGoodID;    //商品ID

    public PayPktReq()
    {
        this.m_strAction = PACKET_DEFINE.PAY_REQ;
    }


    public override string GetRequire()
    {
        string req = "pid=" + this.m_iPID + "&good_id=" + this.m_iGoodID;

        req = PACKET_HEAD.PACKET_REQ_END(ref req);

        return req;
    }
}
