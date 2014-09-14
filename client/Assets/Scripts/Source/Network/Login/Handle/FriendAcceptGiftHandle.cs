//  FriendAcceptGiftHandle.cs
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
public class FriendAcceptGiftHandle
{
    /// <summary>
    /// 获取action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.FRIEND_ACCEPTGIFT_REQ;
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketAck packet)
    {
        FriendAcceptGiftPktAck ack = (FriendAcceptGiftPktAck)packet;

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.header.code != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.header.desc);
            
        }

        Role.role.GetBaseProperty().m_iGold = ack.m_iGold;
        Role.role.GetBaseProperty().m_iFarmPoint = ack.m_iFarmpoint;
        Role.role.GetBaseProperty().m_iFriendPoint = ack.m_iFriendpoint;

        for (int i = 0; i < ack.m_lstItems.Count; i++)
        {
            Role.role.GetItemProperty().AddItem(ack.m_lstItems[i]);
            Role.role.GetItemBookProperty().AddItem(ack.m_lstItems[i].m_iTableID);  //更新图鉴
        }

        GUIBackFrameTop top = (GUIBackFrameTop)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP);
        top.UpdateGold();
        top.UpdateFarmPiont();

        if (Role.role.GetFriendProperty().GetAllGift() != null)
        {
            Role.role.GetBaseProperty().m_iFriendGiftCount = Role.role.GetFriendProperty().GetAllGift().Count;
        }

        GUIBackFrameBottom tmpb = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;

        if (tmpb.IsShow())
        {
            tmpb.SetFriendApllyNumber(Role.role.GetBaseProperty().m_iFriendApplyCount + Role.role.GetBaseProperty().m_iFriendGiftCount);
        }

        
    }
}
