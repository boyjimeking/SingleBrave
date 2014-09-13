using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;



//  BattleRelivePktReq.cs
//  Author: Lu Zexi
//  2014-03-04


/// <summary>
/// 战斗复活请求
/// </summary>
public class BattleRelivePktReq : HTTPPacketRequest
{
    public int m_iPID;  //玩家ID
    public int m_iReliveNum;    //复活次数

    public BattleRelivePktReq()
    {
        this.m_strAction = PACKET_DEFINE.BATTLE_RELIVE_REQ;
    }

    // /// <summary>
    // /// 获取请求
    // /// </summary>
    // /// <returns></returns>
    // public override string GetRequire()
    // {
    //     string req = "pid=" + this.m_iPID + "&num=" + this.m_iReliveNum;

    //     PACKET_HEAD.PACKET_REQ_END(ref req);

    //     return req;
    // }

}
