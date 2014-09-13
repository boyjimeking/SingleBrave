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

    // /// <summary>
    // /// 获取请求参数
    // /// </summary>
    // /// <returns></returns>
    // public override string GetRequire()
    // {
    //     string req = "";
    //     for (int i = 0; i < m_vecItems.Length; i++)
    //     {
    //         int tmp = m_vecItems[i];
    //         if (tmp == -1)
    //         {
    //             req += "pos" + i + "=-1&pos" + i + "_n=0&";
    //         }
    //         else
    //         {
    //             req += "pos" + i + "=" + m_vecItems[i] + "&pos" + i + "_n=" + m_vecItemNums[i] + "&";
    //         }
    //     }

    //     if (req.EndsWith("&"))
    //     {
    //         req = req.Remove(req.Length - 1);
    //     }

    //     req = "pid=" + m_iPid + "&" + req;

    //     PACKET_HEAD.PACKET_REQ_END(ref req);

    //     return req;
    // }
}