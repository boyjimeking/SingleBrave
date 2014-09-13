
using System.Collections.Generic;
using System.Collections;
using Game.Base;
using CodeTitans.JSon;
using Game.Network;

//  BattleGateStartPktAck.cs
//  Author: Lu Zexi
//  2013-12-19



/// <summary>
/// 战斗关卡开始应答数据包
/// </summary>
public class BattleGateStartPktAck : HTTPPacketBase
{
    public int m_iBattleID; //战斗ID
    public int m_iDiscountType; //折扣类型

    public BattleGateStartPktAck()
    {
        this.m_strAction = PACKET_DEFINE.BATTLE_GATE_START_REQ;
    }
}



/// <summary>
/// 战斗关卡工厂类
/// </summary>
public class BattleGateStartFactory : HTTPPacketFactory
{
    /// <summary>
    /// 获取action
    /// </summary>
    /// <returns></returns>
    public override string GetPacketAction()
    {
        return PACKET_DEFINE.BATTLE_GATE_START_REQ;
    }

    /// <summary>
    /// 创建数据包
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public override HTTPPacketBase Create(IJSonObject json)
    {
        BattleGateStartPktAck ack = PACKET_HEAD.PACKET_ACK_HEAD<BattleGateStartPktAck>(json);

        if (ack.m_iErrorCode != 0)
        {
            return ack;
        }

        IJSonObject data = json["data"];
        ack.m_iBattleID = data["battle_id"].Int32Value;
        ack.m_iDiscountType = data["discount_type"].Int32Value;
        return ack;
    }
}
