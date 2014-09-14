using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Network;
//获取系统邮件数据应答类
//Author sunyi
//2014-1-17
public class PlayerGetSystemMailPktAck : HTTPPacketAck
{
    public List<Mail> m_lstMail = new List<Mail>();//邮件列表

    // public PlayerGetSystemMailPktAck() 
    // {
    //     this.m_strAction = PACKET_DEFINE.PLAYER_GET_SYSTEM_MAIL_REQ;
    // }
}

// /// <summary>
// /// 获取邮件列表数据包应答工厂类
// /// </summary>
// public class PlayerGetSystemMailPktAckFactory : HTTPPacketFactory
// {
//     /// <summary>
//     /// 获取数据包Action
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.PLAYER_GET_SYSTEM_MAIL_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(CodeTitans.JSon.IJSonObject json)
//     {
//         PlayerGetSystemMailPktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<PlayerGetSystemMailPktAck>(json);
//         if (pkt.header.code != 0)
//         {
//             return pkt;
//         }

//         var data = json["data"].ArrayItems;

//         foreach (var item in data)
//         {
//             Mail mail = new Mail();
//             mail.m_iID = item["id"].Int32Value;
//             mail.m_cType = (GiftType)item["gift_type"].Int32Value;
//             mail.m_iCount = item["gift_num"].Int32Value;
//             switch (mail.m_cType)
//             { 
//                 case GiftType.Diamond:
//                     mail.m_strTittle = "钻石";
//                     break;
//                 case GiftType.Gold:
//                     mail.m_strTittle = "金币";
//                     break;
//                 case GiftType.FriendPoint:
//                     mail.m_strTittle = "友情点";
//                     break;
//                 case GiftType.FarmPoint:
//                     mail.m_strTittle = "元气";
//                     break;
//                 case GiftType.Hero:
//                     mail.m_iHeroTableID = item["gift_tid"].Int32Value;
//                     HeroTable hero = HeroTableManager.GetInstance().GetHeroTable(mail.m_iHeroTableID);
//                     mail.m_strTittle = hero.Name;
//                     break;
//                 case GiftType.Item:
//                     mail.m_iItemTableID = item["gift_tid"].Int32Value;
//                     ItemTable itemTable = ItemTableManager.GetInstance().GetItem(mail.m_iItemTableID);
//                     mail.m_strTittle = itemTable.ShortName;
//                     break;
//                 default:
//                     break;
//             }
//             mail.m_strContent = item["gift_content"].StringValue;
//             mail.m_lDate = item["send_time"].Int32Value;

//             pkt.m_lstMail.Add(mail);
//         }

//         return pkt;
//     }
// }

