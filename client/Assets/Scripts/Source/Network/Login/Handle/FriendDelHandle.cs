//  FriendDelHandle.cs
//  Author: Cheng Xia
//  2013-1-15

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

/// <summary>
/// 好友删除句柄
/// </summary>
public class FriendDelHandle
{
    /// <summary>
    /// 获取action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.FRIEND_DEL_REQ;
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketAck packet)
    {
        FriendDelPktAck ack = (FriendDelPktAck)packet;

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.header.code != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.header.desc);
            
        }

        GUIFriendInfoLike gui_friendInfoLike = (GUIFriendInfoLike)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_FRIENDINFOLIKE);
        Role.role.GetFriendProperty().RemoveFriend(gui_friendInfoLike.m_cFriend.m_iID);

        gui_friendInfoLike.Hiden();

        GUIFriendList gui_friendList = (GUIFriendList)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_FRIENDLIST);
        gui_friendList.Show();

        
    }
}
