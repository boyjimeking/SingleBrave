using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


//  Friend.cs
//  Author: Lu Zexi
//  2013-11-26


/// <summary>
/// 好友
/// </summary>
public class Friend
{
    public int m_iID;  //好友ID
    public string m_strName;   //名字
    public string m_strSignature;   //签名
    public int m_iLevel;   //等级
    public int m_iAthleticPoint;   //当前竞技点  （需要查表获得当前等级对应的称号）
    public Hero m_cHero;   //英雄
    public bool m_bLike;   //是否喜欢该好友　　sanvey 12.4 add
    public long m_lLastLoginTime; //该好友最后登录时间
    public int[] m_lstWantGift;  //想要的礼物，可由好友赠予，最多3个
    public long m_lApplyTime;   //申请时间
    public int m_iState;    //是谁申请的
    public bool m_bSend;    //当天是伐已经送过礼物//

    public Friend()
    {
        this.m_lstWantGift = new int[3];
    }
}

/// <summary>
/// 好友礼物
/// </summary>
public class FriendGift
{
    public int m_iID;
    public int m_iFID;//好友id
    public int m_iNum;//数量
    public GiftType m_eType;//礼物类型
    public Friend m_cFriend;
    public FriendGiftItem m_cItem;
    public string m_strSprName;//图集名
    public string m_strName;//礼物名称

    public FriendGift()
    {

    }
}

/// <summary>
/// 好友列表协议数据
/// </summary>
public class FriendData
{
    public int m_iPID;
    public string m_strNickName;     //昵称//
    public int m_iRoloLevel;    //角色等级//
    public int[] m_lstWantGift = new int[3]; //需要的礼物//
    public string m_strSignature;   //签名//
    public int m_iLike; //是否喜欢 1是喜欢 0是不喜欢//
    public long m_iLoginTime;   //登陆时间//
    public int m_iAthleticsLevel = 1;  //竞技场等级//
    public int m_iHeroTableID;  //英雄tabelID//
    public int m_iHeroLv;   //英雄等级//
    public int m_iHeroHp;   //英雄血量//
    public int m_iAttack;   //英雄攻击//
    public int m_iDefend;   //英雄防御//
    public int m_iRecover;  //英雄回复力//

    public int m_requestPid; //好友请求id//
    public int m_targetPid;
    public long m_lApplyTime;

    public long m_iSendTime; //上次赠送礼物的时间//

    public Friend GetFriend()
    {
        Friend friend = new Friend();
        friend.m_iID = m_iPID;
        friend.m_strName = m_strNickName;
        friend.m_iLevel = m_iRoloLevel;
        friend.m_lstWantGift = m_lstWantGift;
        friend.m_strSignature = m_strSignature;
        friend.m_bLike = (m_iLike == 1) ? true : false;
        friend.m_lLastLoginTime = m_iLoginTime;
        friend.m_iAthleticPoint = m_iAthleticsLevel;
        friend.m_cHero = new Hero(m_iHeroTableID);
        friend.m_cHero.m_iLevel = m_iHeroLv;
        friend.m_cHero.m_iHp = m_iHeroHp;
        friend.m_cHero.m_iAttack = m_iAttack;
        friend.m_cHero.m_iDefence = m_iDefend;
        friend.m_cHero.m_iRevert = m_iRecover;
        friend.m_bSend = IsSend(m_iSendTime);
        return friend;
    }

    public Friend GetFriendApply()
    {
        Friend friend = new Friend();
        if (m_requestPid == Role.role.GetBaseProperty().m_iPlayerId)
        {
            friend.m_iID = m_targetPid;
        }
        else
        {
            friend.m_iID = m_requestPid;
        }
        friend.m_strName = m_strNickName;
        friend.m_iLevel = m_iRoloLevel;
        friend.m_lstWantGift = m_lstWantGift;
        friend.m_strSignature = m_strSignature;
        friend.m_iState = (m_requestPid == Role.role.GetBaseProperty().m_iPlayerId) ? 0 : 1;
        //NGUIDebug.Log(friend.m_iState.ToString());
        friend.m_lApplyTime = m_lApplyTime;
        friend.m_iAthleticPoint = m_iAthleticsLevel;
        friend.m_cHero = new Hero(m_iHeroTableID);
        friend.m_cHero.m_iLevel = m_iHeroLv;
        friend.m_cHero.m_iHp = m_iHeroHp;
        friend.m_cHero.m_iAttack = m_iAttack;
        friend.m_cHero.m_iDefence = m_iDefend;
        friend.m_cHero.m_iRevert = m_iRecover;
        return friend;
    }

    /// <summary>
    /// 判断是否已经送过东西
    /// </summary>
    /// <returns></returns>
    public bool IsSend(long sendTime)
    {
        DateTime dt = GAME_FUNCTION.UNIXTimeToCDateTime(sendTime);
        DateTime systemDt = GAME_FUNCTION.UNIXTimeToCDateTime(GAME_DEFINE.m_lServerTime);

        

        if ((dt.Year == systemDt.Year) && (dt.Month == systemDt.Month) && (dt.Day == systemDt.Day))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
/// <summary>
/// 好友礼物列表
/// </summary>
public class FriendGiftData
{
    public int m_iID;
    public int m_iPID;//玩家id
    public int m_iFID;//好友id
    public int m_iGiftID;//礼物id
    public int m_iNum;//数量
    public GiftType m_eType;//礼物类型

    public FriendGift GetFriendGift()
    {
        FriendGift fg = new FriendGift();

        fg.m_iID = m_iID;
        fg.m_iFID = m_iFID;
        fg.m_iNum = m_iNum;
        fg.m_eType = m_eType;
        fg.m_cFriend = Role.role.GetFriendProperty().GetFriend(m_iFID);
        fg.m_cItem = FriendGiftItemTableManager.GetInstance().GetGiftItem(m_iGiftID);

        FriendGiftItemTable table = FriendGiftItemTableManager.GetInstance().GetItem(m_iGiftID);
        fg.m_strSprName = table.SpiritName;
        fg.m_strName = table.Name;

        return fg;
    }
}

