//  FriendGetGiftListHandle.cs
//  Author: Cheng Xia
//  2013-1-17

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;
using UnityEngine;

/// <summary>
/// 好友申请列表句柄
/// </summary>
public class FriendGetGiftListHandle
{
    /// <summary>
    /// 获取action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.FRIEND_GETGIFTLIST_REQ;
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketRequest packet)
    {
        FriendGetGiftListPktAck ack = (FriendGetGiftListPktAck)packet;

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return false;
        }

        Role.role.GetFriendProperty().RemoveAllGift();

        foreach (FriendGiftData fgd in ack.m_lstFriendGiftData)
        {
            FriendGift fg = fgd.GetFriendGift();
            Role.role.GetFriendProperty().AddFriendGift(fg);
        }
        GUIFriendGift frigift = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_FRIENDGIFT) as GUIFriendGift;
        frigift.Show();

        //SendAgent.SendPVPInfoGetReq(Role.role.GetBaseProperty().m_iPlayerId);

        return true;
    }
}
