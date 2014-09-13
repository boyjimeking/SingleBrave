//  FriendGetApplyListHandle.cs
//  Author: Cheng Xia
//  2013-1-13

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

/// <summary>
/// 好友申请列表句柄
/// </summary>
public class FriendGetApplyListHandle : HTTPHandleBase
{
    /// <summary>
    /// 获取action
    /// </summary>
    /// <returns></returns>
    public override string GetAction()
    {
        return PACKET_DEFINE.FRIEND_GETAPPLYLIST_REQ;
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public override bool Excute(HTTPPacketBase packet)
    {
        FriendGetApplyListPktAck ack = (FriendGetApplyListPktAck)packet;

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return false;
        }

        Role.role.GetFriendProperty().RemoveAllFriendApply();

        foreach (FriendData fd in ack.m_lstFriendData)
        {
            Friend friendApply = fd.GetFriendApply();
            Role.role.GetFriendProperty().AddFriendApply(friendApply);
        }


        GUIFriendApply friapp = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_FRIENDAPPLY) as GUIFriendApply;
        friapp.Show();



        //SendAgent.SendFriendGetGiftList(Role.role.GetBaseProperty().m_iPlayerId);

        //GUI_FUNCTION.LOADING_HIDEN();
        //GameManager.GetInstance().GetSceneManager().ChangeGameScene();

        return true;
    }
}
