using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;


//  GameJoinPktReq.cs
//  Author: Lu Zexi
//  2014-03-27





/// <summary>
/// 游戏加入请求
/// </summary>
public class GameJoinPktReq : HTTPPacketRequest
{
    public string m_strDeviceID;    //设备ID
    public string m_strChannelName; //渠道名

    public GameJoinPktReq()
    {
        this.m_strAction = PACKET_DEFINE.GAME_JOIN_REQ;
    }

}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 发送游戏加入数据
	/// </summary>
	/// <param name="deviceID"></param>
	/// <param name="channelName"></param>
	public static void SendGameJoin(string deviceID, string channelName)
	{
		GameJoinPktReq req = new GameJoinPktReq();
		req.m_strDeviceID = deviceID;
		req.m_strChannelName = channelName;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}


