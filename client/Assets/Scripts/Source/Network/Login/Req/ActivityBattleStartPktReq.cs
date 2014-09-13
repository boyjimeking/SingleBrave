using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

//  ActivityBattleStartPktReq.cs
//  Author: Lu Zexi
//  2014-01-08



/// <summary>
/// 活动战斗开始
/// </summary>
public class ActivityBattleStartPktReq : HTTPPacketRequest
{
    public int m_iPid;  //玩家ID
    public int m_iFubenID;   //副本ID
    public int m_iGateIndex;    //关卡索引

    public ActivityBattleStartPktReq()
    {
        this.m_strAction = PACKET_DEFINE.ACTIVITY_BATTLE_START_REQ;
    }

    // /// <summary>
    // /// 获取请求
    // /// </summary>
    // /// <returns></returns>
    // public override string GetRequire()
    // {
    //     string req = "pid=" + this.m_iPid + "&fuben_id=" + this.m_iFubenID + "&gate_index=" + this.m_iGateIndex;

    //     PACKET_HEAD.PACKET_REQ_END(ref req);

    //     return req;
    // }
}
