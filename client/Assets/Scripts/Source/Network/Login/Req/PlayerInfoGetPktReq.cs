using UnityEngine;
using System.Collections;
using Game.Network;

//  PlayerInfoGetPktReq.cs
//  Author: sanvey
//  2013-12-11

/// <summary>
/// 获取玩家信息请求包
/// </summary>
public class PlayerInfoGetPktReq : HTTPPacketRequest
{
    public int m_iUID;  //用户UID

    public PlayerInfoGetPktReq()
    {
        this.m_strAction = PACKET_DEFINE.GET_PLAYINFO_REQ;
    }
}




/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	
	/// <summary>
	/// 获取玩家信息请求
	/// </summary>
	public static void SendPlayerInfoGetPktReq(int uid )
	{
		PlayerInfoGetPktReq req = new PlayerInfoGetPktReq();
		req.m_iUID = uid;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION,
		                                  req,
		                                  PlayerInfoGetHandle.Excute,
		                                  player_info_get_process);
	}

	/// <summary>
	/// Player_info_get_process the specified req.
	/// </summary>
	/// <param name="req">Req.</param>
	public static HTTPPacketAck player_info_get_process(HTTPPacketRequest req)
	{
		PlayerInfoGetPktAck ack = new PlayerInfoGetPktAck();
		ack.header.code = 1;
		return ack;
	}

}


