using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;


//  PVPBattleStartPktAck.cs
//  Author: Lu Zexi
//  2014-02-11




/// <summary>
/// PVP战斗开始应答包
/// </summary>
public class PVPBattleStartPktAck : HTTPPacketAck
{
    public int m_iBattleID; //战斗ID
    public int m_iSportPoint;   //竞技点
    public Hero[] m_vecHeros;   //目标队伍英雄
    public int[] m_vecEquips;  //装备
    public int m_iLeaderIndex;  //队长索引

    // public PVPBattleStartPktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.PVP_BATTLE_START_REQ;
    //     this.m_vecHeros = new Hero[5];
    //     this.m_vecEquips = new int[5];
    // }
}



// /// <summary>
// /// 战斗开始数据工厂
// /// </summary>
// public class PVPBattleStartPktAckFactory : HTTPPacketFactory
// {
//     /// <summary>
//     /// 获取Action
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.PVP_BATTLE_START_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(IJSonObject json)
//     {
//         PVPBattleStartPktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<PVPBattleStartPktAck>(json);

//         if (pkt.m_iErrorCode != 0)
//         {
//             return pkt;
//         }

//         IJSonObject data = json["data"];

//         pkt.m_iBattleID = data["battle_id"].Int32Value;
//         pkt.m_iSportPoint = data["sport_point"].Int32Value;
//         pkt.m_iLeaderIndex = data["leader_index"].Int32Value;

//         IJSonObject heroData = data["heros"];
//         IJSonObject equipData = data["equips"];
//         for (int i = 0; i < equipData.Count; i++)
//         {
//             int item_tableID = 0;
//             pkt.m_vecEquips[i] = item_tableID;
//         }
//         for (int i = 0; i< heroData.Count ; i++ )
//         {
//             Hero hero = null;
//             IJSonObject item = heroData[i];
//             if (item != null && !item.IsNull )
//             {
//                 hero = new Hero(item["hero_id"].Int32Value);
//                 hero.m_iLevel = item["lv"].Int32Value;
//                 hero.m_iCurrenExp = item["exp"].Int32Value;
//                 hero.m_iHp = (int)item["hp"].SingleValue;
//                 hero.m_iAttack = (int)item["attack"].SingleValue;
//                 hero.m_iDefence = (int)item["defend"].SingleValue;
//                 hero.m_iRevert = (int)item["recover"].SingleValue;
//                 hero.m_iBBSkillLevel = item["bb_level"].Int32Value;
//             }
//             pkt.m_vecHeros[i] = hero;
//         }
//         return pkt;
//     }
// }