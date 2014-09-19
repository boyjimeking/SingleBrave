using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Network;

//获取商城钻石购买数据请求类
//Author sunyi
//2014-02-28
public class StoreDiamondGetPktReq : HTTPPacketRequest
{
    public StoreDiamondGetPktReq()
    {
        this.m_strAction = PACKET_DEFINE.STORE_DIAMOND_GET_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 发送获取商场钻石价格
	/// </summary>
	public static void SendStoreDiamondPrice()
	{
		StoreDiamondGetPktReq req = new StoreDiamondGetPktReq();
		
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}

