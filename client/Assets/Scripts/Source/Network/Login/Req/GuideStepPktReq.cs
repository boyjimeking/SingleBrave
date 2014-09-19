using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;


//  GuideStepPktReq.cs
//  Author: Lu Zexi
//  2014-02-28



/// <summary>
/// 新手引导数据包请求
/// </summary>
public class GuideStepPktReq : HTTPPacketRequest
{
    public int m_iPID;  //玩家ID
    public int m_iGuideID;  //新手引导ID

    public GuideStepPktReq()
    {
        this.m_strAction = PACKET_DEFINE.GUIDE_STEP_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 发送新手引导
	/// </summary>
	/// <param name="pid"></param>
	/// <param name="guide_step"></param>
	public static void SendGuideStep(int pid, int guide_step)
	{
		GuideStepPktReq req = new GuideStepPktReq();
		req.m_iPID = pid;
		req.m_iGuideID = guide_step;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}
}
