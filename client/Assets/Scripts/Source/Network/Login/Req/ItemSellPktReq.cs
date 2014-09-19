using UnityEngine;
using System.Collections;
using Game.Network;

//  ItemSellPktReq.cs
//  Author: sanvey
//  2014-1-2

/// <summary>
/// 物品出售请求
/// </summary>
public class ItemSellPktReq : HTTPPacketRequest
{
    public int m_iItemId;   //出售物品ID，不是tableID
    public int m_iItemNum;  //出售数量
    public int m_iPid;  //Pid

    public ItemSellPktReq()
    {
        this.m_strAction = PACKET_DEFINE.ITEM_SELL_REQ;
    }
}



/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 发送道具出售数据请求
	/// </summary>
	/// <param name="pid"></param>
	/// <param name="itemID"></param>
	/// <param name="itemNum"></param>
	public static void SendItemSellReq(int pid, int itemID, int itemNum)
	{
		ItemSellPktReq req = new ItemSellPktReq();
		req.m_iItemId = itemID;
		req.m_iItemNum = itemNum;
		req.m_iPid = pid;
		
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}