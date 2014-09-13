//  FriendUnlockLikePktReq.cs
//  Author: Cheng Xia
//  2013-1-13

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

/// <summary>
/// 好友喜欢解除获取协议
/// </summary>
public class FriendUnlockLikePktReq : HTTPPacketBase
{
    public int m_iPID;  //玩家ID
    public List<int> m_lstFriendPID;

    public FriendUnlockLikePktReq()
    {
        this.m_strAction = PACKET_DEFINE.FRIEND_UNLOCKLIKE_REQ;
    }

    /// <summary>
    /// 获取数据
    /// </summary>
    /// <returns></returns>
    public override string GetRequire()
    {
        string reqStr = "pid=" + m_iPID;
        reqStr += "&friends=" + m_lstFriendPID[0];

        PACKET_HEAD.PACKET_REQ_END(ref reqStr);

        return reqStr;
    }

}
