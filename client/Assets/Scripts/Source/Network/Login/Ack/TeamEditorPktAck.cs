//  TeamEdiorPktAck.cs
//  Author: Cheng Xia
//  2013-12-19

using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Base;

using Game.Network;



/// <summary>
/// 团队编辑应答数据
/// </summary>
public class TeamEditorPktAck : HTTPPacketAck
{
    public int m_iPid;  //玩家ID

    // public TeamEditorPktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.TEAM_EDITOR_REQ;
    // }
}


// /// <summary>
// /// 团队编辑应答工厂类
// /// </summary>
// public class TeamEditorPktAckFactory : HTTPPacketFactory
// {
//     /// <summary>
//     /// 获取action
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.TEAM_EDITOR_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(IJSonObject json)
//     {
//         TeamEditorPktAck ack = PACKET_HEAD.PACKET_ACK_HEAD<TeamEditorPktAck>(json);

//         if (ack.m_iErrorCode != 0)
//         {
//             GAME_LOG.ERROR("Error . code desc " + ack.m_strErrorDes);
//             return ack;
//         }

//         Debug.Log("return true");
//         //IJSonObject data = json["data"];

//         //ack.m_iPid = data["pid"].Int32Value;

//         return ack;
//     }
// }