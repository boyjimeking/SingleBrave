using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

//道具槽扩展
//Author Sunyi
//2013-12-27
public class PlayerPropsSlotExpansionPktReq : HTTPPacketRequest
{
    public int m_iPid;

    public PlayerPropsSlotExpansionPktReq()
    {
        this.m_strAction = PACKET_DEFINE.PROPS_EXPANSION_REQ;
    }
}



/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 发送道具槽扩张
	/// </summary>
	/// <param name="pid"></param>
	public static void SendPropsSlotExpansionReq(int pid)
	{
		PlayerPropsSlotExpansionPktReq req = new PlayerPropsSlotExpansionPktReq();
		req.m_iPid = pid;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}

