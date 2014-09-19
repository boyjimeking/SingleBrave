using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

//商城-体力恢复接口请求包
//Author: sunyi
//2013-12-27
public class PlayerStrengthRecoverPktReq : HTTPPacketRequest
{
    public int m_iPid;

    public PlayerStrengthRecoverPktReq()
    {
        this.m_strAction = PACKET_DEFINE.STRENGTH_RECOVER_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	
	/// <summary>
	/// 发送体力恢复
	/// </summary>
	/// <param name="pid"></param>
	public static void SendStrengthRecoverReq(int pid)
	{
		PlayerStrengthRecoverPktReq req = new PlayerStrengthRecoverPktReq();
		req.m_iPid = pid;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}

