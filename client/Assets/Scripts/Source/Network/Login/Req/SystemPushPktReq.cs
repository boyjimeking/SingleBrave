
using System.Collections.Generic;
using Game.Network;



//  SystemPushPktReq.cs
//  Author: Lu Zexi
//  2014-02-24



/// <summary>
/// 系统推送
/// </summary>
public class SystemPushPktReq : HTTPPacketRequest
{
    public int m_iPID;  //玩家ID

    public SystemPushPktReq()
    {
        this.m_strAction = PACKET_DEFINE.SYSTEM_PUSH_REQ;
    }

}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 发送推送请求
	/// </summary>
	public static void SendSystemPush( int pid )
	{
		SystemPushPktReq req = new SystemPushPktReq();
		req.m_iPID = pid;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}
