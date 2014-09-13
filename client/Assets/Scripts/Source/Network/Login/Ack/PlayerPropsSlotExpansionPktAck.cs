using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

//道具槽扩张应答类
//Author sunyi
//2013-12-27
public class PlayerPropsSlotExpansionPktAck : HTTPPacketBase
{
    public int m_iDiamondCount;//宝石数量
    public int m_iMaxPropsCount;//道具槽最大数量

    public PlayerPropsSlotExpansionPktAck()
    {
        this.m_strAction = PACKET_DEFINE.PROPS_EXPANSION_REQ;
    }
}

/// <summary>
/// 道具槽扩张应答工厂类
/// </summary>
public class PropsSlotExpansionPktAckFactory : HTTPPacketFactory
{
    /// <summary>
    /// 获取数据包Action
    /// </summary>
    /// <returns></returns>
    public override string GetPacketAction()
    {
        return PACKET_DEFINE.PROPS_EXPANSION_REQ;
    }

    /// <summary>
    /// 创建数据包
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public override HTTPPacketBase Create(CodeTitans.JSon.IJSonObject json)
    {
        PlayerPropsSlotExpansionPktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<PlayerPropsSlotExpansionPktAck>(json);
        if (pkt.m_iErrorCode != 0)
        {
            return pkt;
        }
        pkt.m_iDiamondCount = json["data"]["diamond"].Int32Value;
        pkt.m_iMaxPropsCount = json["data"]["max_item"].Int32Value;

        return pkt;
    }
}

