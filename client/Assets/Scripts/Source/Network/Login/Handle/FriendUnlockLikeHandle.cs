//  FriendUnlockLikeHandle.cs
//  Author: Cheng Xia
//  2013-1-13

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

/// <summary>
/// 好友喜欢锁定句柄
/// </summary>
public class FriendUnlockLikeHandle : HTTPHandleBase
{
    /// <summary>
    /// 获取action
    /// </summary>
    /// <returns></returns>
    public override string GetAction()
    {
        return PACKET_DEFINE.FRIEND_UNLOCKLIKE_REQ;
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public override bool Excute(HTTPPacketBase packet)
    {
        FriendUnlockLikePktAck ack = (FriendUnlockLikePktAck)packet;

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return false;
        }

        GUIFriendInfoLike gui_friendInfoLike = (GUIFriendInfoLike)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_FRIENDINFOLIKE);

        gui_friendInfoLike.m_cFriend.m_bLike = false;
        gui_friendInfoLike.ReflashBtn();

        return true;
    }
}
