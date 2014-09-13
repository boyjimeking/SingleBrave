using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Network;
//获取系统邮件数据请求类
//Author sunyi
//2014-1-17
public class PlayerGetSystemMailPktReq : HTTPPacketBase
{
    public int m_iPlayerId;//玩家id

    public PlayerGetSystemMailPktReq()
    {
        this.m_strAction = PACKET_DEFINE.PLAYER_GET_SYSTEM_MAIL_REQ;
    }

    /// <summary>
    /// 获取请求参数
    /// </summary>
    /// <returns></returns>
    public override string GetRequire()
    {
        string req = string.Format("pid={0}", this.m_iPlayerId);

        PACKET_HEAD.PACKET_REQ_END(ref req);

        return req;
    }
}

