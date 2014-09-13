//  FriendAcceptGiftPktReq.cs
//  Author: Cheng Xia
//  2013-1-17

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

/// <summary>
/// 接受好友礼物协议
/// </summary>
public class FriendAcceptGiftPktReq : HTTPPacketRequest
{  
    public int m_iPID;  //玩家ID
    public int[] m_vecWantsGift;    //想要的礼物
    public List<int> m_lstFriendGifts = new List<int>();    //接收的礼物
      
    public FriendAcceptGiftPktReq()
    {
        this.m_vecWantsGift = new int[3];
        this.m_strAction = PACKET_DEFINE.FRIEND_ACCEPTGIFT_REQ;
    }

    // /// <summary>
    // /// 获取数据
    // /// </summary>
    // /// <returns></returns>
    // public override string GetRequire()
    // {
    //     string reqStr = "pid=" + m_iPID;
    //     reqStr += "&wantsGift=" + this.m_vecWantsGift[0];
    //     for (int i = 1; i<this.m_vecWantsGift.Length ; i++  )
    //     {
    //         reqStr += "|" + this.m_vecWantsGift[i];
    //     }
    //     reqStr += "&gifts=";
    //     for (int i = 0; i < m_lstFriendGifts.Count; i++)
    //     {
    //         reqStr += m_lstFriendGifts[i];

    //         if (i < (m_lstFriendGifts.Count - 1))
    //         {
    //             reqStr += "|";
    //         }
    //     }

    //     PACKET_HEAD.PACKET_REQ_END(ref reqStr);

    //     return reqStr;
    // }

}
