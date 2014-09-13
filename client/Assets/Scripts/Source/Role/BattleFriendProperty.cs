using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



//  BattleFriendProperty.cs
//  Author: Lu Zexi
//  2013-11-26



/// <summary>
/// 战友属性
/// </summary>
public class BattleFriendProperty
{
    private List<BattleFriend> m_lstFriend = new List<BattleFriend>(); //战友
    private BattleFriend m_cSelectFriend;  //选中参战的好友

    public BattleFriendProperty()
    {
        
        ////test
        //Hero hero1 = new Hero(16);
        //hero1.m_iID = 1;
        //hero1.m_iCurrenExp = 90;
        ////hero1.m_strName = "关羽";
        //hero1.m_iLevel = 13;
        //hero1.m_iHp = 1250;
        //hero1.m_iAttack = 560;
        //hero1.m_iDefence = 470;
        //hero1.m_iRevert = 420;
        //hero1.m_iCost = 6;
        //hero1.m_iBBSkillLevel = 2;
        //hero1.m_lGetTime = 12504;
        //hero1.m_iEquipID = 101;
        //hero1.m_bLock = true;
        //hero1.m_bNew = false;

        //BattleFriend f1 = new BattleFriend();
        //f1.m_iID = 1;
        //f1.m_strName = "吕布";
        //f1.m_iLevel = 20;
        //f1.m_cLeaderHero = hero1;
        //f1.m_iEquipItemTableID = 101;

        //BattleFriend f2 = new BattleFriend();
        //f2.m_iID = 2;
        //f2.m_strName = "吕布2";
        //f2.m_iLevel = 22;
        //f2.m_cLeaderHero = hero1;
        //f2.m_iEquipItemTableID = 102;

        //BattleFriend f3 = new BattleFriend();
        //f3.m_iID = 3;
        //f3.m_strName = "吕布3";
        //f3.m_iLevel = 23;
        //f3.m_cLeaderHero = hero1;
        //f3.m_iEquipItemTableID = 103;

        //BattleFriend f4 = new BattleFriend();
        //f4.m_iID = 4;
        //f4.m_strName = "吕布4";
        //f4.m_iLevel = 24;
        //f4.m_cLeaderHero = hero1;
        //f4.m_iEquipItemTableID = 104;

        //BattleFriend f5 = new BattleFriend();
        //f5.m_iID = 5;
        //f5.m_strName = "吕布5";
        //f5.m_iLevel = 25;
        //f5.m_cLeaderHero = hero1;
        //f5.m_iEquipItemTableID = 105;

        //BattleFriend f6 = new BattleFriend();
        //f6.m_iID = 6;
        //f6.m_strName = "吕布6";
        //f6.m_iLevel = 26;
        //f6.m_cLeaderHero = hero1;
        //f6.m_iEquipItemTableID = 106;

        //this.m_lstFriend.Add(f1);
        //this.m_lstFriend.Add(f2);
        //this.m_lstFriend.Add(f3);
        //this.m_lstFriend.Add(f4);
        //this.m_lstFriend.Add(f5);
        //this.m_lstFriend.Add(f6);
    }

    /// <summary>
    /// 获取所有战友
    /// </summary>
    /// <returns></returns>
    public List<BattleFriend> GetAll()
    {
        return new List<BattleFriend>(this.m_lstFriend);
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public void Destory()
    {
        this.m_lstFriend.Clear();
    }

    /// <summary>
    /// 增加战友
    /// </summary>
    /// <param name="friend"></param>
    public void AddBattleFriend(BattleFriend friend)
    {
        //避免重复添加
        if (!m_lstFriend.Exists(q => { return q.m_iID == friend.m_iID; }))
        {
            this.m_lstFriend.Add(friend);
        }
    }

    /// <summary>
    /// 删除战友
    /// </summary>
    /// <param name="id"></param>
    public void RemoveBattleFriend(int id)
    {
        foreach (BattleFriend item in this.m_lstFriend)
        {
            if (item.m_iID == id)
            {
                this.m_lstFriend.Remove(item);
                return;
            }
        }
    }

    /// <summary>
    /// 获取战友
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BattleFriend GetBattleFriend(int id)
    {
        foreach (BattleFriend item in this.m_lstFriend)
        {
            if (item.m_iID == id)
            {
                return item;
            }
        }

        return null;
    }

    /// <summary>
    /// 设置选中的好友
    /// </summary>
    /// <param name="id"></param>
    public bool SetSelectFriendID(int id)
    {
        m_cSelectFriend = m_lstFriend.Find(new Predicate<BattleFriend>((item) => { return item.m_iID == id; }));
        return true;
    }

    /// <summary>
    /// 获取选中参战的好友
    /// </summary>
    /// <returns></returns>
    public BattleFriend GetSelectFriend()
    {
        return m_cSelectFriend;
    }

    /// <summary>
    /// 删除所有战友
    /// </summary>
    public void RemoveAll()
    {
        this.m_lstFriend.Clear();
    }
}
