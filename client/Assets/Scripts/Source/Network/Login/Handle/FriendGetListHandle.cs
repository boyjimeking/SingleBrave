//  FriendGetListHandle.cs
//  Author: Cheng Xia
//  2013-1-13

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

/// <summary>
/// 好友列表句柄
/// </summary>
public class FriendGetListHandle
{
    /// <summary>
    /// 获取action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.FRIEND_GETLIST_REQ;
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketRequest packet)
    {
        FriendGetListPktAck ack = (FriendGetListPktAck)packet;

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return false;
        }

        Role.role.GetFriendProperty().RemoveAllFriends();
        foreach (FriendData fd in ack.m_lstFriendData)
        {
            Friend friend = fd.GetFriend();
            Role.role.GetFriendProperty().AddFriend(friend);
        }

        SendAgent.SendPVPInfoGetReq(Role.role.GetBaseProperty().m_iPlayerId);

        //SendAgent.SendFriendGetApplyListReq(Role.role.GetBaseProperty().m_iPlayerId);
        

        //GUI_FUNCTION.LOADING_HIDEN();
        //GameManager.GetInstance().GetSceneManager().ChangeGameScene();

        return true;
    }
}
