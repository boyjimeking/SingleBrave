using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Network;
using Game.Base;

//获取系统邮件数据句柄类
//Author sunyi
//2014-1-17
public class PlayerGetSystemMailHandle
{
    /// <summary>
    /// 获取Action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.PLAYER_GET_SYSTEM_MAIL_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketAck packet)
    {
        PlayerGetSystemMailPktAck ack = (PlayerGetSystemMailPktAck)packet;

        GAME_LOG.LOG("code :" + ack.header.code);
        GAME_LOG.LOG("desc :" + ack.header.desc);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.header.code != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.header.desc);
            return;
        }
		
		Mail.Clear();

        for(int i = 0;i < ack.m_lstMail.Count;i++)
        {
			Mail.Add(ack.m_lstMail[i]);
        }

        GUIMain main = (GUIMain)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_MAIN);
        main.Hiden();

        GUIMail guimail = (GUIMail)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_MAIL);
        guimail.Show();


        return;
    }
}

