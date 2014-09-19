using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Network;

//  ItemCombinePktReq.cs
//  Author: sanvey
//  2013-12-24

/// <summary>
/// 物品合成请求
/// </summary>
public class ItemCombinePktReq : HTTPPacketRequest
{
    public List<int> m_iCombinedId;  //合成物品ID
    public List<int> m_iCombineNum;  //合成物品数量
    public int m_iPid;  //Pid

    public ItemCombinePktReq()
    {
        this.m_strAction = PACKET_DEFINE.ITEM_COMBINED_REQ;
    }
}



/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	
	/// <summary>
	/// 合成物品请求
	/// </summary>
	/// <param name="pid"></param>
	/// <param name="ItemIds"></param>
	/// <param name="ItemNums"></param>
	public static void SendItemCombinedReq(int pid, List<int> ItemIds, List<int> ItemNums)
	{
		ItemCombinePktReq req = new ItemCombinePktReq();
		req.m_iPid = pid;
		req.m_iCombinedId = ItemIds;
		req.m_iCombineNum = ItemNums;
		SessionManager.GetInstance().SendReady(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}