using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Network;

//接收系统礼物数据应答类
//Author sunyi
//2014-1-20
public class PlayerReceiveSystemMailPktAck : HTTPPacketAck
{
    public class GiftMail
    {
        public int m_iDiamond;//宝石数
        public int m_iGold;//金币数
        public int m_iFarmpoint;//农场点数
        public int m_iFriendpoint;//友情点数
        public List<Hero> m_lstHeros;//英雄
        public List<Item> m_lstItems;//物品
    }

    public GiftMail m_cGiftMail;//礼物

    // public PlayerReceiveSystemMailPktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.PLAYER_RECEIVE_SYSTEM_MAIL_REQ;
    // }
 }

// /// <summary>
// /// 接收系统礼物数据应答工厂类
// /// </summary>
// public class PlayerReceiveSystemMailPktAckFactory : HTTPPacketFactory
// {
//     /// <summary>
//     /// 获取数据包Action
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.PLAYER_RECEIVE_SYSTEM_MAIL_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(CodeTitans.JSon.IJSonObject json)
//     {
//         PlayerReceiveSystemMailPktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<PlayerReceiveSystemMailPktAck>(json);
//         if (pkt.m_iErrorCode != 0)
//         {
//             return pkt;
//         }

//         pkt.m_cGiftMail = new PlayerReceiveSystemMailPktAck.GiftMail();
//         pkt.m_cGiftMail.m_lstHeros = new List<Hero>();
//         pkt.m_cGiftMail.m_lstItems = new List<Item>();

//         pkt.m_cGiftMail.m_iDiamond = json["data"]["diamond"].Int32Value;
//         pkt.m_cGiftMail.m_iGold = json["data"]["gold"].Int32Value;
//         pkt.m_cGiftMail.m_iFarmpoint = json["data"]["farmpoint"].Int32Value;
//         pkt.m_cGiftMail.m_iFriendpoint = json["data"]["friendpoint"].Int32Value;

//         IEnumerable<IJSonObject> heros = json["data"]["heros"].ArrayItems;
        
//         foreach (IJSonObject heroData in heros)
//         {
//             int herotableId = heroData["hero_id"].Int32Value;
//             Hero hero = new Hero(herotableId);
//             hero.m_iLevel = heroData["lv"].Int32Value;
//             hero.m_iCurrenExp = heroData["exp"].Int32Value;
//             hero.m_iHp = (int)heroData["hp"].SingleValue;
//             hero.m_iAttack = (int)heroData["attack"].SingleValue;
//             hero.m_iDefence = (int)heroData["defend"].SingleValue;
//             hero.m_iRevert = (int)heroData["recover"].SingleValue;
//             hero.m_iBBSkillLevel = heroData["bb_level"].Int32Value;
//             hero.m_eGrowType = (GrowType)heroData["grow_type"].Int32Value;
//             hero.m_iEquipID = (int)heroData["equip_id"].Int32Value;
//             hero.m_bNew = true;
//             int isLocked = heroData["lock"].Int32Value;
//             if (isLocked == 0)
//             {
//                 hero.m_bLock = false;
//             }
//             else
//             {
//                 hero.m_bLock = true;
//             }
//             hero.m_lGetTime = heroData["create_time"].Int32Value;
//             hero.m_iID = heroData["id"].Int32Value;
            
//             pkt.m_cGiftMail.m_lstHeros.Add(hero);
//         }

//         IEnumerable<IJSonObject> items = json["data"]["items"].ArrayItems;

//         foreach (IJSonObject itemData in items)
//         {
//             int tableId = itemData["item_id"].Int32Value;
//             Item giftItem = new Item(tableId);
//             giftItem.m_iID = itemData["id"].Int32Value;
//             giftItem.m_iNum = itemData["item_num"].Int32Value;
//             pkt.m_cGiftMail.m_lstItems.Add(giftItem);
//         }

//         return pkt;
//     }
// }

