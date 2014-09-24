using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



//  BattleFrient.cs
//  Author: Lu Zexi
//  2013-11-26




/// <summary>
/// 战场好友
/// </summary>
public class BattleFriend : CModel<BattleFriend>
{
    public int m_iID;  //战友ID
    public string m_strName;    //战友名字
    public int m_iLevel;    //战友等级
    public int m_iEquipItemTableID; //装备物品配置ID
    public Hero m_cLeaderHero;  //队长英雄
    public int m_iFriendPoint;  //好友点
    public bool m_bIsFriend;//是否为好友
    public string m_strSign;  //签名
    public int m_PvpExp; //竞技场经验

	// get battle friend by id
	public BattleFriend GetByID( int id )
	{
		foreach( BattleFriend item in s_lstData )
		{
			if( item.m_iID == id )
				return item;
		}
		return null;
	}
}

