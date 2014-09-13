using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

//玩家英雄列表
//Author:sunyi
//2013-12-11

//获取玩家英雄列表请求数据包
public class PlayerHeroInfoPacketReq : HTTPPacketRequest
{

    public int m_iPlayerId;//玩家id

    public PlayerHeroInfoPacketReq()
    {
        this.m_strAction = PACKET_DEFINE.GET_PALYERHEROINFO_REQ;
    }

    // /// <summary>
    // /// 获取请求参数
    // /// </summary>
    // /// <returns></returns>
    // public override string GetRequire()
    // {
    //     string req = string.Format("pid={0}", m_iPlayerId);

    //     PACKET_HEAD.PACKET_REQ_END(ref req);

    //     return req;
    // }
}

