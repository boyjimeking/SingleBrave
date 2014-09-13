//  FriendSendGiftPktReq.cs
//  Author: Cheng Xia
//  2013-1-17

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

/// <summary>
/// 赠送好友礼物协议
/// </summary>
public class FriendSendGiftPktReq : HTTPPacketBase
{
    public int m_iPID;  //玩家ID
    public List<FriendSendData> m_lstFriendSendData;
     
    public FriendSendGiftPktReq()
    {
        this.m_strAction = PACKET_DEFINE.FRIEND_SENDGIFT_REQ;
    }

    /// <summary>
    /// 获取数据
    /// </summary>
    /// <returns></returns>
    public override string GetRequire()
    {
        string reqStr = "pid=" + m_iPID;
        reqStr += "&sendinfo=";
        for (int i = 0; i < m_lstFriendSendData.Count; i++)
        {
            reqStr += m_lstFriendSendData[i].m_iFriendID + ":" + m_lstFriendSendData[i].m_iGiftID;

            if (i < (m_lstFriendSendData.Count - 1))
            {
                reqStr += "|";
            }
        }

        PACKET_HEAD.PACKET_REQ_END(ref reqStr);

        return reqStr;
    }

}
