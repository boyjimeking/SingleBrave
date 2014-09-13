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
public class ItemCombinePktReq : HTTPPacketBase
{
    public List<int> m_iCombinedId;  //合成物品ID
    public List<int> m_iCombineNum;  //合成物品数量
    public int m_iPid;  //Pid

    public ItemCombinePktReq()
    {
        this.m_strAction = PACKET_DEFINE.ITEM_COMBINED_REQ;
    }

    /// <summary>
    /// 获取请求参数
    /// </summary>
    /// <returns></returns>
    public override string GetRequire()
    {
        string data = string.Empty;
        for (int i = 0; i < m_iCombinedId.Count; i++)
        {
            data += m_iCombinedId[i] + ":" + m_iCombineNum[i] + "|";
        }
        if (data.EndsWith("|"))
        {
            data = data.Remove(data.Length - 1);
        }

        string req = string.Format("pid={0}&combines={1}", m_iPid.ToString(), data);

        PACKET_HEAD.PACKET_REQ_END(ref req);

        return req;
    }
}