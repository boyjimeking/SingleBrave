using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

//获取当前任务信息请求类
//Author:sunyi
//2013-12-11

public class PlayerTaskInfoPktReq : HTTPPacketRequest
{
    public int m_iPid;//玩家id

    public PlayerTaskInfoPktReq()
    {
        this.m_strAction = PACKET_DEFINE.GET_PLAYERTASKINFO_REQ;
    }
}



/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	
	/// <summary>
	/// 获取副本任务数据请求
	/// </summary>
	/// <param name="pid"></param>
	public static void SendPlayerTaskInfoGetPktReq(int pid)
	{
		PlayerTaskInfoPktReq req = new PlayerTaskInfoPktReq();
		req.m_iPid = pid;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}
}

