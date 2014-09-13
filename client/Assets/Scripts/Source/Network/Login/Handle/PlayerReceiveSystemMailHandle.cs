using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Network;
using Game.Base;

//接收系统礼物句柄类
//Author sunyi
//2014-1-20
public class PlayerReceiveSystemMailHandle : HTTPHandleBase
{
    /// <summary>
    /// 获取Action
    /// </summary>
    /// <returns></returns>
    public override string GetAction()
    {
        return PACKET_DEFINE.PLAYER_RECEIVE_SYSTEM_MAIL_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public override bool Excute(HTTPPacketBase packet)
    {
        PlayerReceiveSystemMailPktAck ack = (PlayerReceiveSystemMailPktAck)packet;

        GAME_LOG.LOG("code :" + ack.m_iErrorCode);
        GAME_LOG.LOG("desc :" + ack.m_strErrorDes);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return false;
        }

        Role.role.GetBaseProperty().m_iDiamond = ack.m_cGiftMail.m_iDiamond;
        Role.role.GetBaseProperty().m_iGold = ack.m_cGiftMail.m_iGold;
        Role.role.GetBaseProperty().m_iFarmPoint = ack.m_cGiftMail.m_iFarmpoint;
        Role.role.GetBaseProperty().m_iFriendPoint = ack.m_cGiftMail.m_iFriendpoint;

        for (int i = 0; i < ack.m_cGiftMail.m_lstHeros.Count; i++)
        {
            Role.role.GetHeroProperty().AddHero(ack.m_cGiftMail.m_lstHeros[i]);
            Role.role.GetHeroBookProperty().Add(ack.m_cGiftMail.m_lstHeros[i].m_iTableID);  //接受系统礼物的英雄 加入图鉴
        }

        for (int i = 0; i < ack.m_cGiftMail.m_lstItems.Count; i++)
        {
            Role.role.GetItemProperty().AddItem(ack.m_cGiftMail.m_lstItems[i]);
            Role.role.GetItemBookProperty().AddItem(ack.m_cGiftMail.m_lstItems[i].m_iTableID);  //接受系统礼物的物品 加入图鉴
        }


        GUIMail mail = (GUIMail)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_MAIL);
        mail.ReFlashListView();

        GUIBackFrameTop top = (GUIBackFrameTop)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP);
        top.UpdateDiamond(Role.role.GetBaseProperty().m_iDiamond);
        top.UpdateGold();
        top.UpdateFarmPiont();

        return true;
    }
}

