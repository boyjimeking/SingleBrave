using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Network;

//  HeroSellHandle.cs
//  Author: sanvey
//  2013-12-17

//英雄出售请求应答句柄
public class HeroSellHandle
{
    /// <summary>
    /// 获得Action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.HERO_SELL_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketRequest packet)
    {
        HeroSellPktAck ack = (HeroSellPktAck)packet;

        GAME_LOG.LOG("code :" + ack.m_iErrorCode);
        GAME_LOG.LOG("desc :" + ack.m_strErrorDes);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return false;
        }

        //更新出售回来的金钱，服务器返回全量
        int getMoney = ack.m_iGetGold;
        Role.role.GetBaseProperty().m_iGold = ack.m_iGold;
        GUIBackFrameTop guitop = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP) as GUIBackFrameTop;
        guitop.UpdateGold();

        foreach (int item in ack.m_lstDeleteIDs)
        {
            Role.role.GetHeroProperty().DelHero(item);
        }

        GUI_FUNCTION.MESSAGEM(MessageOk, "出售获得 " + getMoney + " 金币");

        return true;
    }

    /// <summary>
    /// 出售确定
    /// </summary>
    public void MessageOk()
    {
        GUIHeroSell herosell = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_HEROSELL) as GUIHeroSell;

        herosell.Show();
    }
}