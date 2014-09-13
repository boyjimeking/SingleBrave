using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;


//  GameJoinPktAck.cs
//  Author: Lu Zexi
//  2014-03-27



/// <summary>
/// 游戏进入返回
/// </summary>
public class GameJoinPktAck : HTTPPacketAck
{
    // public GameJoinPktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.GAME_JOIN_REQ;
    // }
}




// /// <summary>
// /// 游戏进入数据包工厂类
// /// </summary>
// public class GameJoinPktAckFactory : HTTPPacketFactory
// {

//     /// <summary>
//     /// 获取游戏ACTION
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.GAME_JOIN_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(IJSonObject json)
//     {
//         GameJoinPktAck ack = PACKET_HEAD.PACKET_ACK_HEAD<GameJoinPktAck>(json);

//         if (ack.m_iErrorCode != 0)
//         {
//             return ack;
//         }

//         return ack;
//     }

// }
