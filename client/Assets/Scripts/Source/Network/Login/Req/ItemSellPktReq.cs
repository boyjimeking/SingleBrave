using UnityEngine;
using System.Collections;
using Game.Network;

//  ItemSellPktReq.cs
//  Author: sanvey
//  2014-1-2

/// <summary>
/// 物品出售请求
/// </summary>
public class ItemSellPktReq : HTTPPacketBase
{
    public int m_iItemId;   //出售物品ID，不是tableID
    public int m_iItemNum;  //出售数量
    public int m_iPid;  //Pid

    public ItemSellPktReq()
    {
        this.m_strAction = PACKET_DEFINE.ITEM_SELL_REQ;
    }

    /// <summary>
    /// 获取请求参数
    /// </summary>
    /// <returns></returns>
    public override string GetRequire()
    {
        string req = string.Format("pid={0}&id={1}&num={2}", m_iPid.ToString(), m_iItemId.ToString(), m_iItemNum.ToString());

        PACKET_HEAD.PACKET_REQ_END(ref req);

        return req;
    }
}