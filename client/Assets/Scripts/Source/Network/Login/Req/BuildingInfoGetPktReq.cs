using UnityEngine;
using System.Collections;
using Game.Network;

//  BuildingInfoGetPktReq.cs
//  Author: sanvey
//  2013-12-13

/// <summary>
/// 获取建筑信息请求包
/// </summary>
public class BuildingInfoGetPktReq : HTTPPacketRequest
{
    public int m_iPID;  //用户Pid

    public BuildingInfoGetPktReq()
    {
        this.m_strAction = PACKET_DEFINE.GET_BUILDING_GET_REQ;
    }
}



/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	
	/// <summary>
	/// 获取建筑信息数据请求
	/// </summary>
	/// <param name="pid"></param>
	public static void SendBuildInfoGetPktReq(int pid)
	{
		BuildingInfoGetPktReq req = new BuildingInfoGetPktReq();
		req.m_iPID = pid;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}
