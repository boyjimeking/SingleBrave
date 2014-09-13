//  FriendAcceptApplyPktReq.cs
//  Author: Cheng Xia
//  2013-1-13

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

/// <summary>
/// 接受好友申请协议
/// </summary>
public class FriendAcceptApplyPktReq : HTTPPacketRequest
{
    public int m_iPID;  //玩家ID
    public int m_iFriendPID;

    public FriendAcceptApplyPktReq()
    {
        this.m_strAction = PACKET_DEFINE.FRIEND_ACCEPTAPPLY_REQ;
    }

    // /// <summary>
    // /// 获取数据
    // /// </summary>
    // /// <returns></returns>
    // public override string GetRequire()
    // {
    //     string req = "pid=" + m_iPID;
    //     req += "&fid=" + m_iFriendPID;

    //     PACKET_HEAD.PACKET_REQ_END(ref req);

    //     return req;
    // }

}
