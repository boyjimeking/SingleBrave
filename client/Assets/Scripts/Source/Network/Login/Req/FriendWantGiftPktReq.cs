//  FriendWantGiftPktReq.cs
//  Author: Cheng Xia
//  2013-1-20

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

/// <summary>
/// 好友期望礼物协议
/// </summary>
public class FriendWantGiftPktReq : HTTPPacketBase
{
    public int m_iPID;  //玩家ID
    public int[] m_iWantGiftIDs = new int[3];    //希望获取的m_iWantGiftID;

    public FriendWantGiftPktReq()
    {
        this.m_strAction = PACKET_DEFINE.FRIEND_WANTGIFT_REQ;
    }

    /// <summary>
    /// 获取数据
    /// </summary>
    /// <returns></returns>
    public override string GetRequire()
    {
        string reqStr = "pid=" + m_iPID;
        reqStr += "&gifts=";
        for (int i = 0; i < 3; i++)
        {
            reqStr += m_iWantGiftIDs[i];
            if (i < 2)
            {
                reqStr += "|";
            }
        }

        PACKET_HEAD.PACKET_REQ_END(ref reqStr);

        return reqStr;
    }

}
