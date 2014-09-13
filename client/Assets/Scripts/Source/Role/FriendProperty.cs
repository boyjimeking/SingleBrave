using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


//  FriendProperty.cs
//  Author: Lu Zexi
//  2013-11-26



/// <summary>
/// 好友属性
/// </summary>
public class FriendProperty
{
    private List<Friend> m_lstFriend;   //好友列表

    private List<Friend> m_lstFriendApply;  //好友申请列表//

    private List<FriendGift> m_lstFriendGift;   //好友礼物列表//

    public FriendProperty()
    {
        this.m_lstFriend = new List<Friend>();
        this.m_lstFriendApply = new List<Friend>();
        this.m_lstFriendGift = new List<FriendGift>();
    }


    /// <summary>
    /// 获取所有好友
    /// </summary>
    /// <returns></returns>
    public List<Friend> GetAll()
    {
        return new List<Friend>(this.m_lstFriend);
    }

    /// <summary>
    /// 销毁所有
    /// </summary>
    public void Destory()
    {
        this.m_lstFriend.Clear();
        this.m_lstFriendApply.Clear();
        this.m_lstFriendGift.Clear();
    }

    /// <summary>
    /// 是否为我的好友
    /// </summary>
    /// <param name="friendPid"></param>
    /// <returns></returns>
    public bool IsMyFriend(int friendPid)
    {
        return m_lstFriend.Exists(q => { return q.m_iID == friendPid; });
    }

    /// <summary>
    /// 增加好友
    /// </summary>
    public void AddFriend( Friend friend )
    {
        this.m_lstFriend.Add(friend);
    }

    /// <summary>
    /// 删除所有好友
    /// </summary>
    public void RemoveAllFriends()
    {
        this.m_lstFriend.Clear();
    }

    /// <summary>
    /// 删除好友
    /// </summary>
    /// <param name="id"></param>
    public void RemoveFriend( int id )
    {
        foreach (Friend friend in this.m_lstFriend)
        {
            if (friend.m_iID == id)
            {
                this.m_lstFriend.Remove(friend);
                return;
            }
        }
    }

    /// <summary>
    /// 获取指定好友
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Friend GetFriend(int id)
    {
        foreach (Friend item in this.m_lstFriend)
        {
            if (item.m_iID == id)
            {
                return item;
            }
        }

        return null;
    }


    /// <summary>
    /// 获取所有好友申请
    /// </summary>
    /// <returns></returns>
    public List<Friend> GetAllApply()
    {
        List<Friend> frilst = new List<Friend>(this.m_lstFriendApply);
        List<Friend> applyMe = frilst.FindAll(q => { return q.m_iState == 1; });
        List<Friend> Meapply = frilst.FindAll(q => { return q.m_iState == 0; });

        applyMe.Sort((q1, q2) => { return q2.m_lApplyTime.CompareTo(q1.m_lApplyTime); });
        Meapply.Sort((q1, q2) => { return q2.m_lApplyTime.CompareTo(q1.m_lApplyTime); });

        frilst.Clear();
        frilst.AddRange(applyMe);
        frilst.AddRange(Meapply);

        return frilst;
    }

    /// <summary>
    /// 增加好友列表中好友申请
    /// </summary>
    public void AddFriendApply( Friend friend )
    {
        this.m_lstFriendApply.Add(friend);
    }

    /// <summary>
    /// 删除好友列表中好友申请
    /// </summary>
    /// <param name="id"></param>
    public void RemoveFriendApply( int id )
    {
        foreach (Friend friend in this.m_lstFriendApply)
        {
            if (friend.m_iID == id)
            {
                this.m_lstFriendApply.Remove(friend);
                return;
            }
        }
    }

    /// <summary>
    /// 删除所有好友申请
    /// </summary>
    public void RemoveAllFriendApply()
    {
        this.m_lstFriendApply.Clear();
    }

    /// <summary>
    /// 获取指定申请好友列表好友
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Friend GetFriendApply(int id)
    {
        foreach (Friend item in this.m_lstFriendApply)
        {
            if (item.m_iID == id)
            {
                return item;
            }
        }

        return null;
    }


    /// <summary>
    /// 获取好友礼物
    /// </summary>
    /// <returns></returns>
    public List<FriendGift> GetAllGift()
    {
        return new List<FriendGift>(this.m_lstFriendGift);
    }

    /// <summary>
    /// 删除礼物
    /// </summary>
    public void RemoveAllGift()
    {
        this.m_lstFriendGift.Clear();
    }

    /// <summary>
    /// 增加好友礼物列表
    /// </summary>
    public void AddFriendGift(FriendGift friend)
    {
        this.m_lstFriendGift.Add(friend);
    }

    /// <summary>
    /// 删除好友礼物列表
    /// </summary>
    /// <param name="id"></param>
    public void RemoveFriendGift(int id)
    {
        foreach (FriendGift friend in this.m_lstFriendGift)
        {
            if (friend.m_iID == id)
            {
                this.m_lstFriendGift.Remove(friend);
                return;
            }
        }
    }

    /// <summary>
    /// 获取指定好友礼物列表
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public FriendGift GetFriendGift(int id)
    {
        foreach (FriendGift item in this.m_lstFriendGift)
        {
            if (item.m_iID == id)
            {
                return item;
            }
        }

        return null;
    }

}
