using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Network;

//  ItemCollectPktReq.cs
//  Author: sanvey
//  2013-12-27

/// <summary>
/// 物品合成请求
/// </summary>
public class ItemCollectPktReq : HTTPPacketRequest
{
    public int m_iGold;        //采集金币
    public int m_iFarmPoint;   //采集农场点
    public int m_iShanClick;   //山点击次数
    public int m_iChuanClick;  //川点击次数
    public int m_iTianClick;   //田点击次数
    public int m_iLinClick;    //林点击次数
    public int[] m_vecItemId;  //物品tableID
    public int[] m_vecItemNum; //物品采集数量

    public int m_iPid;  //Pid

    public ItemCollectPktReq()
    {
        this.m_strAction = PACKET_DEFINE.ITEM_COLLECT_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	
	/// <summary>
	/// 发送物品采集数据
	/// </summary>
	/// <param name="pid"></param>
	/// <param name="shan_c"></param>
	/// <param name="chuan_c"></param>
	/// <param name="tian_c"></param>
	/// <param name="lin_c"></param>
	/// <param name="gold"></param>
	/// <param name="farmpoint"></param>
	/// <param name="itemTableId"></param>
	/// <param name="itemNum"></param>
	public static void SendItemCollectReq(int pid, int shan_c, int chuan_c, int tian_c, int lin_c, int gold, int farmpoint, int[] itemTableId, int[] itemNum)
	{
		ItemCollectPktReq req = new ItemCollectPktReq();
		req.m_iFarmPoint = farmpoint;
		req.m_iGold = gold;
		req.m_iPid = pid;
		req.m_iShanClick = shan_c;
		req.m_iChuanClick = chuan_c;
		req.m_iTianClick = tian_c;
		req.m_iLinClick = lin_c;
		req.m_vecItemId = itemTableId;
		req.m_vecItemNum = itemNum;
		SessionManager.GetInstance().SendReady(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}