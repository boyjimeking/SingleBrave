using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;



//  ItemBookPktReq.cs
//  Author: Lu Zexi 
//  2013-12-30



/// <summary>
/// 物品图鉴请求数据包
/// </summary>
public class ItemBookPktReq : HTTPPacketRequest
{
    public int m_iPid;  //玩家ID

    public ItemBookPktReq()
    {
        this.m_strAction = PACKET_DEFINE.ITEM_BOOK_REQ;
    }
}



/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	
	/// <summary>
	/// 发送物品图鉴请求
	/// </summary>
	/// <param name="pid"></param>
	public static void SendItemBookReq(int pid)
	{
		ItemBookPktReq req = new ItemBookPktReq();
		req.m_iPid = pid;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}