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
public class PVPEnemyGet5Handle : HTTPHandleBase
{
    /// <summary>
    /// 获得Action
    /// </summary>
    /// <returns></returns>
    public override string GetAction()
    {
        return PACKET_DEFINE.PVP_ENEMY_GET_5_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public override bool Excute(HTTPPacketBase packet)
    {
        PVPEnemyGet5PktAck ack = (PVPEnemyGet5PktAck)packet;

        GAME_LOG.LOG("code :" + ack.m_iErrorCode);
        GAME_LOG.LOG("desc :" + ack.m_strErrorDes);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return false;
        }


        GUIArenaFightReady tmp = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_ARENAFIGHTREADY) as GUIArenaFightReady;
        tmp.SetPVPData(ack.m_lstEnemys);


        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_ARENA).Hiden();
        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Show();
        tmp.Show();


        return true;
    }
}