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
public class FriendDelHandle : HTTPHandleBase
{
    /// <summary>
    /// 获取action
    /// </summary>
    /// <returns></returns>
    public override string GetAction()
    {
        return PACKET_DEFINE.FRIEND_DEL_REQ;
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public override bool Excute(HTTPPacketBase packet)
    {
        FriendDelPktAck ack = (FriendDelPktAck)packet;

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return false;
        }

        GUIFriendInfoLike gui_friendInfoLike = (GUIFriendInfoLike)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_FRIENDINFOLIKE);
        Role.role.GetFriendProperty().RemoveFriend(gui_friendInfoLike.m_cFriend.m_iID);

        gui_friendInfoLike.Hiden();

        GUIFriendList gui_friendList = (GUIFriendList)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_FRIENDLIST);
        gui_friendList.Show();

        return true;
    }
}
