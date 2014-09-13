using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

//  PVPBattleStartPktReq.cs
//  Author: Lu Zexi
//  2014-02-11



/// <summary>
/// PVP战斗开始
/// </summary>
public class PVPBattleStartPktReq : HTTPPacketRequest
{
    public int m_iPid;  //Pid
    public int m_iTpid; //目标ID

    public PVPBattleStartPktReq()
    {
        this.m_strAction = PACKET_DEFINE.PVP_BATTLE_START_REQ;
    }

    // /// <summary>
    // /// 获取请求参数
    // /// </summary>
    // /// <returns></returns>
    // public override string GetRequire()
    // {
    //     string req = string.Format("pid={0}&tpid={1}", m_iPid, m_iTpid);
    //     PACKET_HEAD.PACKET_REQ_END(ref req);
    //     return req;
    // }
}