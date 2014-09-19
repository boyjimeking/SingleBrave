using UnityEngine;
using System.Collections;
using Game.Network;

//  GetPlayerInfoPktReq.cs
//  Author: sanvey
//  2013-12-11

/// <summary>
/// 创建玩家信息请求包
/// </summary>
public class PlayerCreatePktReq : HTTPPacketRequest
{
    public int m_strUID;  //用户UID
    public string m_strNickName;  //用好昵称
    public int m_iSelectHeroIndex;  //选择英雄索引
    public string m_strDeviceID;    //设备ID
    public string m_strChannel; //渠道

    public PlayerCreatePktReq()
    {
        this.m_strAction = PACKET_DEFINE.CREATE_PLAY_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 创建玩家请求
	/// </summary>
	public static void SendPlayerCreatePktReq(string nickName, int uid, int select_hero_index)
	{
		PlayerCreatePktReq req = new PlayerCreatePktReq();
		req.m_strNickName = nickName;
		req.m_strUID = uid;
		req.m_iSelectHeroIndex = select_hero_index;
		req.m_strChannel = PlatformManager.GetInstance().GetChannelName();
		req.m_strDeviceID = PlatformManager.GetInstance().GetDeviceID();
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}
}

