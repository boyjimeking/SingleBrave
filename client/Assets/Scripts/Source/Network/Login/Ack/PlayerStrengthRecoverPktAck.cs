using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

//商城-恢复体力数据包应答类
//Author:Sunyi
//2013-12-27
public class PlayerStrengthRecoverPktAck : HTTPPacketAck
{
    public int m_iDiamondCount;//宝石数量
    public int m_iStrength;//最大体力

    // public PlayerStrengthRecoverPktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.STRENGTH_RECOVER_REQ;
    // }
}

// /// <summary>
// /// 体力恢复数据包应答工厂类
// /// </summary>
// public class StrengthRecoverPktAckFactory : HTTPPacketFactory
// {
//     /// <summary>
//     /// 获取数据包Action
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.STRENGTH_RECOVER_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(CodeTitans.JSon.IJSonObject json)
//     {
//         PlayerStrengthRecoverPktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<PlayerStrengthRecoverPktAck>(json);
//         if (pkt.m_iErrorCode != 0)
//         {
//             return pkt;
//         }

//         pkt.m_iDiamondCount = json["data"]["diamond"].Int32Value;
//         pkt.m_iStrength = json["data"]["strength"].Int32Value;

//         return pkt;
//     }
// }
