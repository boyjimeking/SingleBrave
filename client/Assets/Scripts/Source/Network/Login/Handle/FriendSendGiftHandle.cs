//  FriendSendGiftHandle.cs
//  Author: Cheng Xia
//  2013-1-17

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

/// <summary>
/// 赠送好友礼物句柄
/// </summary>
public class FriendSendGiftHandle
{
    /// <summary>
    /// 获取action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.FRIEND_SENDGIFT_REQ;
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketAck packet)
    {
        FriendSendGiftPktAck ack = (FriendSendGiftPktAck)packet;

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.header.code != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.header.desc);
            
        }
        
        /*
        GUIFriendGiftGive gui_friendGiftGive = (GUIFriendGiftGive)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_FRIENDGIFTGIVE);

        foreach (Friend f in Role.role.GetFriendProperty().GetAll())
        {
            foreach (FriendSendData fsd in gui_friendGiftGive.m_lstFriendSendData)
            {
                if (f.m_iID == fsd.m_iFriendID)
                {
                    f.m_bSend = true;
                }
            }
        }

        gui_friendGiftGive.Reflash();
        */

        
    }
}
