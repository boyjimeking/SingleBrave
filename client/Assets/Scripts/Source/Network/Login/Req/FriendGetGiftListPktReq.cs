//  FriendGetGiftListPktReq.cs
//  Author: Cheng Xia
//  2013-1-17

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

/// <summary>
/// 好友礼物列表获取协议
/// </summary>
public class FriendGetGiftListPktReq : HTTPPacketBase
{ 
    public int m_iPID;  //玩家ID
      
    public FriendGetGiftListPktReq()
    {
        this.m_strAction = PACKET_DEFINE.FRIEND_GETGIFTLIST_REQ;
    }

    /// <summary>
    /// 获取数据
    /// </summary>
    /// <returns></returns>
    public override string GetRequire()
    {
        string reqStr = "pid=" + m_iPID;

        PACKET_HEAD.PACKET_REQ_END(ref reqStr);

        return reqStr;
    }

}
