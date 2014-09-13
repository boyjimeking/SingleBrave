//  FriendGetListPktReq.cs
//  Author: Cheng Xia
//  2013-1-13

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

/// <summary>
/// 好友列表获取协议
/// </summary>
public class FriendGetListPktReq : HTTPPacketRequest
{
    public int m_iPID;  //玩家ID

    public FriendGetListPktReq()
    {
        this.m_strAction = PACKET_DEFINE.FRIEND_GETLIST_REQ;
    }

    // /// <summary>
    // /// 获取数据
    // /// </summary>
    // /// <returns></returns>
    // public override string GetRequire()
    // {
    //     string reqStr = "pid=" + m_iPID;

    //     PACKET_HEAD.PACKET_REQ_END(ref reqStr);

    //     return reqStr;
    // }

}
