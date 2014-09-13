using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Network;
//接收系统礼物请求类
//Author Sunyi
//2014-1-20
public class PlayerReceiveSystemEmailPktReq : HTTPPacketBase
{
    public int m_iPlayerId;//玩家id
    public string m_strGiftIds;//接收的礼物id字符串

    public PlayerReceiveSystemEmailPktReq()
    {
        this.m_strAction = PACKET_DEFINE.PLAYER_RECEIVE_SYSTEM_MAIL_REQ;
    }

    /// <summary>
    /// 获取请求参数
    /// </summary>
    /// <returns></returns>
    public override string GetRequire()
    {
        string req = string.Format("pid={0}&gifts={1}", this.m_iPlayerId, m_strGiftIds);

        PACKET_HEAD.PACKET_REQ_END(ref req);

        return req;
    }
}

