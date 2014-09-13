using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  Mail.cs
//  Author: Lu Zexi
//  2013-11-26



/// <summary>
/// 邮件
/// </summary>
public class Mail
{
    public int m_iID;   //ID
    public string m_strTittle;  //标题
    public string m_strContent; //内容
    public long m_lDate;//日期
    public GiftType m_cType;//类型id
    public int m_iCount;//赠送的数量
    //public int m_iGold; //赠送的金币
    //public int m_iDiamond;  //赠送的钻石
    public int m_iItemTableID;  //赠送的物品
    public int m_iItemCount;//赠送的物品数量
    public int m_iHeroCount;//赠送的英雄数量
    public int m_iHeroTableID;  //赠送英雄
    //public int m_iFarmPoint;    //赠送农场点
    //public int m_iFriendPoint;//赠送友情点
}

/// <summary>
/// 奖品(1.钻石，2.金币，3.友情点，4.农场点，5.英雄，6.物品)
/// </summary>
public enum GiftType {
    Diamond = 1,
    Gold = 2,
    FriendPoint = 3,
    FarmPoint = 4,
    Hero = 5,
    Item = 6,
}
