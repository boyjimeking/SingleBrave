using System;
using System.Collections.Generic;
using CodeTitans.JSon;
using Game.Network;

//  ActivityBattleStartPktAck.cs
//  Author: Lu Zexi
//  2014-01-08



/// <summary>
/// 活动战斗开始应答包
/// </summary>
public class ActivityBattleStartPktAck : HTTPPacketBase
{
    public int m_iBattleID; //战斗ID
    public int m_iDiscountType; //折扣类型

    public ActivityBattleStartPktAck()
    {
        this.m_strAction = PACKET_DEFINE.ACTIVITY_BATTLE_START_REQ;
    }
}



/// <summary>
/// 活动战斗开始工厂类
/// </summary>
public class ActivityBattleStartPktFactory : HTTPPacketFactory
{
    /// <summary>
    /// 获取action
    /// </summary>
    /// <returns></returns>
    public override string GetPacketAction()
    {
        return PACKET_DEFINE.ACTIVITY_BATTLE_START_REQ;
    }

    /// <summary>
    /// 创建数据包
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public override HTTPPacketBase Create(IJSonObject json)
    {
        ActivityBattleStartPktAck ack = PACKET_HEAD.PACKET_ACK_HEAD<ActivityBattleStartPktAck>(json);

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
