using UnityEngine;
using System.Collections;
using Game.Network;

//  BattleItemEditPktReq.cs
//  Author: sanvey
//  2013-12-25

/// <summary>
/// 战斗物品编辑请求
/// </summary>
public class BattleItemEditPktReq : HTTPPacketRequest
{
    public int[] m_vecItems;  //物品列表
    public int[] m_vecItemNums;  //物品数量
    public int m_iPid;  //Pid

    public BattleItemEditPktReq()
    {
        this.m_strAction = PACKET_DEFINE.BATTLE_ITEM_EDIT_REQ;
    }
}



/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	
	/// <summary>
	/// 发送编辑物品数据
	/// </summary>
	/// <param name="pid"></param>
	/// <param name="items"></param>
	public static void SendBattleItemEdit(int pid, int[] items,int[] nums)
	{
		BattleItemEditPktReq req = new BattleItemEditPktReq();
		req.m_iPid = pid;
		req.m_vecItems = items;
		req.m_vecItemNums = nums;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}