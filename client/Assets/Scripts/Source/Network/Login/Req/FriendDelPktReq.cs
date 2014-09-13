//  FriendDelPktReq.cs
//  Author: Cheng Xia
//  2013-1-15

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

/// <summary>
/// 好友删除协议
/// </summary>
public class FriendDelPktReq : HTTPPacketBase
{
    public int m_iPID;  //玩家ID
    public int m_iFriendPID;

    public FriendDelPktReq()
    {
        this.m_strAction = PACKET_DEFINE.FRIEND_DEL_REQ;
    }

    /// <summary>
    /// 获取数据
    /// </summary>
    /// <returns></returns>
    public override string GetRequire()
    {
        string reqStr = "pid=" + m_iPID;
        reqStr += "&fid=" + m_iFriendPID;

        PACKET_HEAD.PACKET_REQ_END(ref reqStr);

        return reqStr;
    }

}
