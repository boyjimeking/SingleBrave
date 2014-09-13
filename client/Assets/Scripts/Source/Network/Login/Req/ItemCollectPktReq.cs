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

    // /// <summary>
    // /// 获取请求参数
    // /// </summary>
    // /// <returns></returns>
    // public override string GetRequire()
    // {
    //     string data = string.Empty;
    //     for (int i = 0; i < m_vecItemId.Length; i++)
    //     {
    //         data += m_vecItemId[i] + ":" + m_vecItemNum[i] + "|";
    //     }
    //     if (data.EndsWith("|"))
    //     {
    //         data = data.Remove(data.Length - 1);
    //     }

    //     string req = string.Format(
    //         "pid={0}&shan_c={1}&chuan_c={2}&tian_c={3}&lin_c={4}&getitems={5}&gold={6}&farmpoint={7}", m_iPid.ToString(),
    //         m_iShanClick.ToString(), m_iChuanClick.ToString(), m_iTianClick.ToString(), m_iLinClick.ToString(),
    //         data, m_iGold.ToString(), m_iFarmPoint.ToString());

    //     PACKET_HEAD.PACKET_REQ_END(ref req);

    //     return req;
    // }
}