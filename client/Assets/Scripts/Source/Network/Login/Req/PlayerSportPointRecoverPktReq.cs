using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

//竞技点恢复请求类
//Author Sunyi
//2013-12-27
public class PlayerSportPointRecoverPktReq : HTTPPacketRequest
{
    public int m_iPid;

    public PlayerSportPointRecoverPktReq()
    {
        this.m_strAction = PACKET_DEFINE.BATTLEPOING_RECOVER_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 发送竞技点恢复
	/// </summary>
	/// <param name="pid"></param>
	public static void SendSportPointRecoverReq(int pid)
	{
		PlayerSportPointRecoverPktReq req = new PlayerSportPointRecoverPktReq();
		req.m_iPid = pid;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}

