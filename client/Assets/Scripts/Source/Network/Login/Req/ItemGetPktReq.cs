using UnityEngine;
using System.Collections;
using Game.Network;

//  ItemGetPktReq.cs
//  Author: sanvey
//  2013-12-23

/// <summary>
/// 获取物品请求
/// </summary>
public class ItemGetPktReq : HTTPPacketRequest
{
    public int m_iPid;  //Pid

    public ItemGetPktReq()
    {
        this.m_strAction = PACKET_DEFINE.ITEM_GET_REQ;
    }
}



/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	
	/// <summary>
	/// 获取所有物品
	/// </summary>
	/// <param name="pid"></param>
	public static void SendItemGetReq(int pid)
	{
		ItemGetPktReq req = new ItemGetPktReq();
		req.m_iPid = pid;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}