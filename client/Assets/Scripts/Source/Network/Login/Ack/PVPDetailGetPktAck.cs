using System;
using System.Collections.Generic;

using Game.Network;

//  PVPDetailGetPktAck.cs
//  Author: sanvey
//  2014-2-8


/// <summary>
/// 竞技场详细信息获得
/// </summary>
public class PVPDetailGetPktAck : HTTPPacketAck
{
    public int m_iwin_attack_max_link;
    public int m_iwin_attack_max;
    public int m_iwin_defence_max_link;
    public int m_iwin_defence_max;
    public int m_ikill_hero_num;
    public int m_ipvp_max_point;
    public int m_ikill_fire_hero_num;
    public int m_ikill_water_hero_num;
    public int m_ikill_wood_hero_num;
    public int m_ikill_thunder_hero_num;
    public int m_ikill_light_hero_num;
    public int m_ikill_dark_hero_num;
    public int m_ikill_star1_hero_num;
    public int m_ikill_star2_hero_num;
    public int m_ikill_star3_hero_num;
    public int m_ikill_star4_hero_num;
    public int m_ikill_star5_hero_num;
    public int m_ikill_all_num;
    public int m_iwin_time_up_num;
    public int m_iwin_perfect_num;
    public int m_iover_kill_num;
    public int m_ihit_one_max_num;
    public int m_ispark_one_max_num;
    public int m_ihit_all_num;
    public int m_irecover_all_num;
    public int m_ispark_all_num;
    public int m_iskill_use_num;
    public int m_iwin_num;
    public int m_iwin_skill_num;
    public int m_ilose_num;

    // public PVPDetailGetPktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.PVP_DETAIL_GET_REQ;
    // }
}


// /// <summary>
// /// 竞技场详细信息获得数据包工厂类
// /// </summary>
// public class PVPDetailGetPktAckFactory : HTTPPacketFactory
// {

//     /// <summary>
//     /// 获取数据包Action
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.PVP_DETAIL_GET_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(IJSonObject json)
//     {
//         PVPDetailGetPktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<PVPDetailGetPktAck>(json);

//         if (pkt.header.code != 0)
//         {
//             return pkt;
//         }

//         IJSonObject data = json["data"];

//         pkt.m_iwin_attack_max_link = data["win_attack_max_link"].Int32Value;
//         pkt.m_iwin_attack_max = data["win_attack_max"].Int32Value;
//         pkt.m_iwin_defence_max_link = data["win_defence_max_link"].Int32Value;
//         pkt.m_iwin_defence_max = data["win_defence_max"].Int32Value;
//         pkt.m_ikill_hero_num = data["kill_hero_num"].Int32Value;
//         pkt.m_ipvp_max_point = data["pvp_max_point"].Int32Value;
//         pkt.m_ikill_fire_hero_num = data["kill_fire_hero_num"].Int32Value;
//         pkt.m_ikill_water_hero_num = data["kill_water_hero_num"].Int32Value;
//         pkt.m_ikill_wood_hero_num = data["kill_wood_hero_num"].Int32Value;
//         pkt.m_ikill_thunder_hero_num = data["kill_thunder_hero_num"].Int32Value;
//         pkt.m_ikill_light_hero_num = data["kill_light_hero_num"].Int32Value;
//         pkt.m_ikill_dark_hero_num = data["kill_dark_hero_num"].Int32Value;
//         pkt.m_ikill_star1_hero_num = data["kill_star1_hero_num"].Int32Value;
//         pkt.m_ikill_star2_hero_num = data["kill_star2_hero_num"].Int32Value;
//         pkt.m_ikill_star3_hero_num = data["kill_star3_hero_num"].Int32Value;
//         pkt.m_ikill_star4_hero_num = data["kill_star4_hero_num"].Int32Value;
//         pkt.m_ikill_star5_hero_num = data["kill_star5_hero_num"].Int32Value;
//         pkt.m_ikill_all_num = data["kill_all_num"].Int32Value;
//         pkt.m_iwin_time_up_num = data["win_time_up_num"].Int32Value;
//         pkt.m_iwin_perfect_num = data["win_perfect_num"].Int32Value;
//         pkt.m_iover_kill_num = data["over_kill_num"].Int32Value;
//         pkt.m_ihit_one_max_num = data["hit_one_max_num"].Int32Value;
//         pkt.m_ispark_one_max_num = data["spark_one_max_num"].Int32Value;
//         pkt.m_ihit_all_num = data["hit_all_num"].Int32Value;
//         pkt.m_irecover_all_num = data["recover_all_num"].Int32Value;
//         pkt.m_ispark_all_num = data["spark_all_num"].Int32Value;
//         pkt.m_iskill_use_num = data["skill_use_num"].Int32Value;
//         pkt.m_iwin_num = data["win_num"].Int32Value;
//         pkt.m_iwin_skill_num = data["win_skill_num"].Int32Value;
//         pkt.m_ilose_num = data["lose_num"].Int32Value;

//         return pkt;
//     }
// }