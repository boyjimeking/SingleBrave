using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

//竞技点恢复应答类
//Author sunyi
//2013-12-27
public class PlayerSportPointRecoverPktAck : HTTPPacketBase
{
    public int m_iDiamondCount;//宝石数量
    public int m_iSportPoint;//最大竞技点

    public PlayerSportPointRecoverPktAck()
    {
        this.m_strAction = PACKET_DEFINE.BATTLEPOING_RECOVER_REQ;
    }
}

/// <summary>
/// 竞技点恢复数据包应答工厂类
/// </summary>
public class SportPointRecoverPktAckFactory : HTTPPacketFactory
{
    /// <summary>
    /// 获取数据包Action
    /// </summary>
    /// <returns></returns>
    public override string GetPacketAction()
    {
        return PACKET_DEFINE.BATTLEPOING_RECOVER_REQ;
    }

    /// <summary>
    /// 创建数据包
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public override HTTPPacketBase Create(CodeTitans.JSon.IJSonObject json)
    {
        PlayerSportPointRecoverPktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<PlayerSportPointRecoverPktAck>(json);
        if (pkt.m_iErrorCode != 0)
        {
            return pkt;
        }

        pkt.m_iDiamondCount = json["data"]["diamond"].Int32Value;
        pkt.m_iSportPoint = json["data"]["sport_point"].Int32Value;

        return pkt;
    }
}


