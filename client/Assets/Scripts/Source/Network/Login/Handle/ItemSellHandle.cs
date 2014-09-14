using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Network;

//  ItemSellHandle.cs
//  Author: sanvey
//  2013-12-23

//物品出售请求应答句柄
public class ItemSellHandle
{
    /// <summary>
    /// 获得Action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.ITEM_SELL_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketAck packet)
    {
        ItemSellPktAck ack = (ItemSellPktAck)packet;

        GAME_LOG.LOG("code :" + ack.header.code);
        GAME_LOG.LOG("desc :" + ack.header.desc);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.header.code != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.header.desc);
            return;
        }

        GUI_FUNCTION.MESSAGEM(MessageCallBack, "获得" + (ack.m_iGold - Role.role.GetBaseProperty().m_iGold) + "金币");
        Role.role.GetBaseProperty().m_iGold = ack.m_iGold;  //更新出售回来的金钱，服务器返回全量
        Role.role.GetItemProperty().UpdateItemByID(ack.m_iItemID, ack.m_iItemNum);
        return;
    }

    private static void MessageCallBack()
    {
        GUIBackFrameTop guitop = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP) as GUIBackFrameTop;
        guitop.UpdateGold();


        GUIPropsSales tmp = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_PROPSSALES) as GUIPropsSales;
        tmp.HidenSellDetail();

    }
}