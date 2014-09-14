using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Network;
//战绩信息请求应答类
//Author sunyi
//2014-02-28
public class PlayerBattleRecordGetPktAck : HTTPPacketAck
{
    public List<int> m_lstRecords = new List<int>();//列表
    public string[] m_lstJsonStrs = new string[] {"login_times_link", "gold_earned", "gold_costed", "role_saled_gold", "stuff_saled_gold", "yuanqi_earned" ,
    "yuanqi_costed","friend_point_earned","friend_point_costed","hero_tujian_opened","stuff_tujian_opened","fire_hero_tujian_opened","water_hero_tujian_opened",
    "wood_hero_tujian_opened","thunder_hero_tujian_opened","light_hero_tujian_opened","dark_hero_tujian_opened","get_hero_num","get_stuff_num","send_gift_num",
    "receive_gift_num","strengthen_hero_num","strengthen_hero_used_num","evolution_num","mix_num","mix_material_num","produce_equ_num","village_source_gather_num",
    "anger_soul_max_num","cure_soul_max_num","anger_soul_total_num","cure_soul_total_num", "max_hurt_value","max_spark_value","total_spark_value",
    "total_hero_skill_times","mission_challage_num","mission_success_num","battle_win_num","box_show_num"};

    // public PlayerBattleRecordGetPktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.BATTLE_RECORD_GET_REQ;
    // }
}

// public class PlayerBattleRecordGetPktAckFactory : HTTPPacketFactory
// {
//     /// <summary>
//     /// 获取数据包Action
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.BATTLE_RECORD_GET_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(CodeTitans.JSon.IJSonObject json)
//     {
//         PlayerBattleRecordGetPktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<PlayerBattleRecordGetPktAck>(json);
//         if (pkt.header.code != 0)
//         {
//             return pkt;
//         }

//         var data = json["data"].ArrayItems;

//         if (pkt.m_lstRecords != null)
//         {
//             pkt.m_lstRecords.Clear();
//         }

//         foreach (var item in data)
//         {
//             pkt.m_lstRecords.Add(item["login_times"].Int32Value);
//             for (int i = 3; i < item.Count; i++)
//             {
//                 int j = item[pkt.m_lstJsonStrs[i-3]].Int32Value;
//                 pkt.m_lstRecords.Add(j);
//             }
//         }

//         return pkt;
//     }
// }