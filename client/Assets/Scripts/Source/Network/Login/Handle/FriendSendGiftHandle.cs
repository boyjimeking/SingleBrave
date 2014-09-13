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
    public static void Excute(HTTPPacketRequest packet)
    {
        FriendSendGiftPktAck ack = (FriendSendGiftPktAck)packet;

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return false;
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

        return true;
    }
}
