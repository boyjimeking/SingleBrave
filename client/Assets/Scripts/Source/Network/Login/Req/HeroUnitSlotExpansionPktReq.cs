using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

//单位槽扩张请求类
//Author sunyi
//2013-12-27
public class HeroUnitSlotExpansionPktReq : HTTPPacketRequest
{
    public int m_iPid;

    public HeroUnitSlotExpansionPktReq()
    {
        this.m_strAction = PACKET_DEFINE.UNIT_EXPANSION_REQ;
    }
}



/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 发送单位槽扩张
	/// </summary>
	/// <param name="pid"></param>
	public static void SendUnitSlotExpansionReq(int pid)
	{
		HeroUnitSlotExpansionPktReq req = new HeroUnitSlotExpansionPktReq();
		req.m_iPid = pid;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}

