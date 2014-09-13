using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//累计登录奖励表
//Author sunyi
//2014-03-07
public class LoginRewardTable : TableBase
{
    private int m_iDays;//连续登录天数

    public int Days
    {
        get { return m_iDays; }
    }

    private GiftType m_iRewardType;//奖励类型

    public GiftType RewardType
    {
        get { return m_iRewardType; }
    }

    private int m_iCount;//奖励数量

    public int Count
    {
        get { return m_iCount; }
    }

    private int m_iRewardId;//奖励物品id

    public int RewardId
    {
        get { return m_iRewardId; }
    }

    /// <summary>
    /// 读取文本
    /// </summary>
    /// <param name="lstInfo"></param>
    /// <param name="index"></param>
    public override void ReadText(List<string> lstInfo, ref int index)
    {
        this.m_iDays = INT(lstInfo[index++]);
        this.m_iRewardType = (GiftType)INT(lstInfo[index++]);
        this.m_iCount = INT(lstInfo[index++]);
        this.m_iRewardId = INT(lstInfo[index++]);
    }
}

