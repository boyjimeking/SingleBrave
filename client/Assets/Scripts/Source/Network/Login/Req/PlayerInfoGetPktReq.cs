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
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}


