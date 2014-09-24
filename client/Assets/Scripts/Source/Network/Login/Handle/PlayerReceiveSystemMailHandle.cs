using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Network;
using Game.Base;

//接收系统礼物句柄类
//Author sunyi
//2014-1-20
public class PlayerReceiveSystemMailHandle
{
    /// <summary>
    /// 获取Action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.PLAYER_RECEIVE_SYSTEM_MAIL_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketAck packet)
    {
        PlayerReceiveSystemMailPktAck ack = (PlayerReceiveSystemMailPktAck)packet;

        GAME_LOG.LOG("code :" + ack.header.code);
        GAME_LOG.LOG("desc :" + ack.header.desc);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.header.code != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.header.desc);
            return;
        }

        Role.role.GetBaseProperty().m_iDiamond = ack.m_cGiftMail.m_iDiamond;
        Role.role.GetBaseProperty().m_iGold = ack.m_cGiftMail.m_iGold;
        Role.role.GetBaseProperty().m_iFarmPoint = ack.m_cGiftMail.m_iFarmpoint;
        Role.role.GetBaseProperty().m_iFriendPoint = ack.m_cGiftMail.m_iFriendpoint;

        for (int i = 0; i < ack.m_cGiftMail.m_lstHeros.Count; i++)
        {
            Role.role.GetHeroProperty().AddHero(ack.m_cGiftMail.m_lstHeros[i]);
			new HeroBook().AddBook(ack.m_cGiftMail.m_lstHeros[i].m_iTableID);  //接受系统礼物的英雄 加入图鉴
        }

        for (int i = 0; i < ack.m_cGiftMail.m_lstItems.Count; i++)
        {
            Role.role.GetItemProperty().AddItem(ack.m_cGiftMail.m_lstItems[i]);
            new ItemBook().AddItem(ack.m_cGiftMail.m_lstItems[i].m_iTableID);  //接受系统礼物的物品 加入图鉴
        }


        GUIMail mail = (GUIMail)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_MAIL);
        mail.ReFlashListView();

        GUIBackFrameTop top = (GUIBackFrameTop)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP);
        top.UpdateDiamond(Role.role.GetBaseProperty().m_iDiamond);
        top.UpdateGold();
        top.UpdateFarmPiont();

        return;
    }
}

