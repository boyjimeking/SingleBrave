using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

//获取战友信息接口请求包
//Author sunyi
//2013-12-23
public class FriendFightPktReq : HTTPPacketBase
{
    public int m_iPid;

    public FriendFightPktReq()
    {
        this.m_strAction = PACKET_DEFINE.FRIEND_FIGHT_REQ;
    }

    /// <summary>
    /// 获取请求参数
    /// </summary>
    /// <returns></returns>
    public override string GetRequire()
    {
        string req = string.Format("pid={0}", this.m_iPid);

        PACKET_HEAD.PACKET_REQ_END(ref req);

        return req;
    }
}

