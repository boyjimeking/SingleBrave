//  FriendFindHandle.cs
//  Author: Cheng Xia
//  2013-1-13

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

/// <summary>
/// 好友查找句柄
/// </summary>
public class FriendFindHandle : HTTPHandleBase
{
    /// <summary>
    /// 获取action
    /// </summary>
    /// <returns></returns>
    public override string GetAction()
    {
        return PACKET_DEFINE.FRIEND_FIND_REQ;
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public override bool Excute(HTTPPacketBase packet)
    {
        FriendFindPktAck ack = (FriendFindPktAck)packet;

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return false;
        }

        GUIFriendSearch friSearch = (GUIFriendSearch)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_FRIENDSEARCH);
        friSearch.Hiden();

        GUIFriendInfo friinfo = (GUIFriendInfo)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_FRIENDINFO);
        friinfo.SetFrindInfo(ack.m_friendData.GetFriend());
        friinfo.Show();

        return true;
    }
}
