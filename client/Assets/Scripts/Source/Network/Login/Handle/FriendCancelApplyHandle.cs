//  FriendCancelApplyHandle.cs
//  Author: Cheng Xia
//  2013-1-13

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

/// <summary>
/// 好友取消句柄
/// </summary>
public class FriendCancelApplyHandle : HTTPHandleBase
{
    /// <summary>
    /// 获取action
    /// </summary>
    /// <returns></returns>
    public override string GetAction()
    {
        return PACKET_DEFINE.FRIEND_CANCELAPPLY_REQ;
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public override bool Excute(HTTPPacketBase packet)
    {
        FriendCancelApplyPktAck ack = (FriendCancelApplyPktAck)packet;

        GUIFriendApply gui_friendApply = (GUIFriendApply)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_FRIENDAPPLY);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return false;
        }

        Role.role.GetFriendProperty().RemoveFriendApply(gui_friendApply.m_cFirend.m_iID);
        gui_friendApply.Show();

        return true;
    }
}
