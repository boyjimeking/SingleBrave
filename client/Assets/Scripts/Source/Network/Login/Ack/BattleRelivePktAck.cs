using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;


//  BattleRelivePktAck.cs
//  Author: Lu Zexi
//  2014-03-04


/// <summary>
/// 战斗复活应答数据
/// </summary>
public class BattleRelivePktAck : HTTPPacketAck
{
    public bool m_bRelive;  //是否复活
    public int m_iDiamond;  //砖石数

    // public BattleRelivePktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.BATTLE_RELIVE_REQ;
    // }
}


// /// <summary>
// /// 战斗复活数据包工厂类
// /// </summary>
// public class BattleRelivePktAckFactory : HTTPPacketFactory
// {

//     /// <summary>
//     /// 获取Action
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.BATTLE_RELIVE_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(IJSonObject json)
//     {
//         BattleRelivePktAck ack = PACKET_HEAD.PACKET_ACK_HEAD<BattleRelivePktAck>( json );

//         if (ack.header.code != 0)
//         {
//             return ack;
//         }

//         IJSonObject data = json["data"];

//         ack.m_bRelive = data["relive"].BooleanValue;
//         ack.m_iDiamond = data["diamond"].Int32Value;

//         return ack;
//     }
// }