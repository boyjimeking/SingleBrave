using System;
using System.Collections.Generic;

using Game.Network;

//  PVPEnemyGetPktAck.cs
//  Author: sanvey
//  2014-2-8


/// <summary>
/// 竞技场对手获取获得
/// </summary>
public class PVPEnemyGetPktAck : HTTPPacketAck
{
    public bool m_bOK;
    public int m_iTpid; //对手ID
    public string m_strName;
    public int m_iHeroTableID;
    public int m_iLv;
    public int m_iPVP_point;
    public int m_iPVPwin_num;
    public int m_iPVPlose_num;
    public string m_strSignure;

    // public PVPEnemyGetPktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.PVP_ENEMY_GET_REQ;
    // }
}


// /// <summary>
// /// 竞技场对手获取获得数据包工厂类
// /// </summary>
// public class PVPEnemyGetPktAckFactory : HTTPPacketFactory
// {

//     /// <summary>
//     /// 获取数据包Action
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.PVP_ENEMY_GET_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(IJSonObject json)
//     {
//         PVPEnemyGetPktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<PVPEnemyGetPktAck>(json);

//         if (pkt.m_iErrorCode != 0)
//         {
//             return pkt;
//         }

//         IJSonObject data = json["data"];

//         pkt.m_bOK = data["ok"].BooleanValue;
//         pkt.m_iTpid = data["tpid"].Int32Value;
//         pkt.m_strName = data["name"].StringValue;
//         pkt.m_iHeroTableID = data["hero_id"].Int32Value;
//         pkt.m_iLv = data["lv"].Int32Value;
//         pkt.m_iPVP_point = data["pvp_point"].Int32Value;
//         pkt.m_iPVPwin_num = data["win_num"].Int32Value;
//         pkt.m_iPVPlose_num = data["lose_num"].Int32Value;
//         pkt.m_strSignure = data["signature"].StringValue;

//         return pkt;
//     }
// }