using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;


//  HeroBookPktReq.cs
//  Author: Lu Zexi
//  2013-12-30


/// <summary>
/// 英雄图鉴信息请求
/// </summary>
public class HeroBookPktReq : HTTPPacketRequest
{
    public int m_iPid;  //玩家ID

    public HeroBookPktReq()
        : base()
    {
        this.m_strAction = PACKET_DEFINE.HERO_BOOK_REQ;
    }

    // /// <summary>
    // /// 获取需求
    // /// </summary>
    // /// <returns></returns>
    // public override string GetRequire()
    // {
    //     string req = "pid=" + this.m_iPid;

    //     PACKET_HEAD.PACKET_REQ_END(ref req);

    //     return req;
    // }

}
