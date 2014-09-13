using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Network;

//  HeroBookPktAck.cs
//  Author: Lu Zexi
//  2013-12-30



/// <summary>
/// 英雄图鉴答应数据
/// </summary>
public class HeroBookPktAck : HTTPPacketAck
{
    public List<int> m_lstHero = new List<int>();   //曾英雄ID

    // public HeroBookPktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.HERO_BOOK_REQ;
    // }

}


// /// <summary>
// /// 英雄图鉴数据工厂类
// /// </summary>
// public class HeroBookPktFactory : HTTPPacketFactory
// {

//     /// <summary>
//     /// 获取ACTION
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.HERO_BOOK_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(IJSonObject json)
//     {
//         HeroBookPktAck ack = PACKET_HEAD.PACKET_ACK_HEAD<HeroBookPktAck>(json);

//         if (ack.m_iErrorCode != 0)
//         {
//             return ack;
//         }

//         IJSonObject data = json["data"];

//         foreach (IJSonObject item in data.ArrayItems)
//         {
//             ack.m_lstHero.Add(item.Int32Value);
//         }

//         return ack;
//     }
// }