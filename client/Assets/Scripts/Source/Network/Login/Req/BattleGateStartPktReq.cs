using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;


//  BattleGateStartPktReq.cs
//  Author: Lu Zexi
//  2013-12-19



/// <summary>
/// 战斗关卡请求数据包
/// </summary>
public class BattleGateStartPktReq : HTTPPacketRequest
{
    public int m_iPid;  //玩家ID
    public int m_iWorldID;  //世界ID
    public int m_iAreaIndex;    //区域索引
    public int m_iFubenIndex;   //副本索引
    public int m_iGateIndex;    //关卡索引

    public BattleGateStartPktReq()
    {
        this.m_strAction = PACKET_DEFINE.BATTLE_GATE_START_REQ;
    }

    // /// <summary>
    // /// 获取请求
    // /// </summary>
    // /// <returns></returns>
    // public override string GetRequire()
    // {
    //     string req = "pid="+this.m_iPid+"&world_id="+this.m_iWorldID+"&area_index="+this.m_iAreaIndex+"&fuben_index="+this.m_iFubenIndex+"&gate_index="+this.m_iGateIndex;

    //     PACKET_HEAD.PACKET_REQ_END(ref req);

    //     return req;
    // }
}
