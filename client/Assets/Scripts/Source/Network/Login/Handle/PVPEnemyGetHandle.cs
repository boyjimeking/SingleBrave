using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Network;

//  PVPEnemyGetHandle.cs
//  Author: sanvey
//  2014-2-8

//竞技场对手获取请求应答句柄
public class PVPEnemyGetHandle
{
    /// <summary>
    /// 获得Action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.PVP_ENEMY_GET_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketRequest packet)
    {
        PVPEnemyGetPktAck ack = (PVPEnemyGetPktAck)packet;

        GAME_LOG.LOG("code :" + ack.m_iErrorCode);
        GAME_LOG.LOG("desc :" + ack.m_strErrorDes);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return;
        }

        if (ack.m_bOK)
        {
            GUIArenaFightReady tmp = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_ARENAFIGHTREADY) as GUIArenaFightReady;
            //tmp.m_iHeroTableID= ack.m_iHeroTableID;
            //tmp.m_iLv = ack.m_iLv;
            //tmp.m_iPVP_point = ack.m_iPVP_point;
            //tmp.m_iPVPlose_num = ack.m_iPVPlose_num;
            //tmp.m_iPVPwin_num = ack.m_iPVPwin_num;
            //tmp.m_strName = ack.m_strName;
            //tmp.m_iTpid = ack.m_iTpid;
            //tmp.m_strSignture = ack.m_strSignure;

            GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_ARENA).Hiden();
            GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
            tmp.Show();
        }

        return;
    }
}


