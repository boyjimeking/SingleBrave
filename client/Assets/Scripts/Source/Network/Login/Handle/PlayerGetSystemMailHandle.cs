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
    public static void Excute(HTTPPacketRequest packet)
    {
        PlayerGetSystemMailPktAck ack = (PlayerGetSystemMailPktAck)packet;

        GAME_LOG.LOG("code :" + ack.m_iErrorCode);
        GAME_LOG.LOG("desc :" + ack.m_strErrorDes);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return;
        }

        Role.role.GetMailProperty().ClearMails();

        for(int i = 0;i < ack.m_lstMail.Count;i++)
        {
            Role.role.GetMailProperty().AddEmail(ack.m_lstMail[i]);
        }

        GUIMain main = (GUIMain)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_MAIN);
        main.Hiden();

        GUIMail mail = (GUIMail)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_MAIL);
        mail.Show();


        return;
    }
}

