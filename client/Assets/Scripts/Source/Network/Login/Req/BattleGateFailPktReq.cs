using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;


//  BattleGateFailReq.cs
//  Author: Lu Zexi
//  2014-03-05



/// <summary>
/// 关卡战斗失败请求
/// </summary>
public class BattleGateFailPktReq : HTTPPacketRequest
{
    public int m_iPID;  //玩家ID
    public int m_iBattleID; //战斗ID
    public int[] m_vecReadyItemNum; //战斗物品数量

    public BattleGateFailPktReq()
    {
        this.m_strAction = PACKET_DEFINE.BATTLE_GATE_FAIL_REQ;
    }

    // /// <summary>
    // /// 获取请求
    // /// </summary>
    // /// <returns></returns>
    // public override string GetRequire()
    // {
    //     string req = "";
    //     req += "pid=" + this.m_iPID + "&battle_id=" + this.m_iBattleID;
    //     req += "&readyitem=" + this.m_vecReadyItemNum[0];
    //     for (int i = 1; i < this.m_vecReadyItemNum.Length; i++)
    //     {
    //         req += "|" + this.m_vecReadyItemNum[i];
    //     }

    //     PACKET_HEAD.PACKET_REQ_END(ref req);

    //     return req;
    // }

}
