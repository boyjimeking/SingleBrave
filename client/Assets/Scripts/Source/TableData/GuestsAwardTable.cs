using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  GuestsAwardTable.cs
//  Author: Sanvey
//  2013-11-26


/// <summary>
/// 招待奖励表
/// </summary>
public class GuestsAwardTable : TableBase
{

    private int m_iGuestNum; //邀请数量

    public int GuestNum
    {
        get { return m_iGuestNum; }
        set { m_iGuestNum = value; }
    }

    private string m_strTitle;  //标题

    public string  Title
    {
        get { return m_strTitle; }
        set { m_strTitle = value; }
    }

    private string m_strDesc;  //描述

    public string Desc
    {
        get { return m_strDesc; }
        set { m_strDesc = value; }
    }

    private GiftType m_iGiftType;  //奖励(1:物品,2:农场点,3:金币,4:砖石,5:友情点,6:英雄)

    public GiftType GiftType
    {
        get { return m_iGiftType; }
        set { m_iGiftType = value; }
    }

    private int m_iGiftId;  //奖励ID

    public int GiftId
    {
        get { return m_iGiftId; }
        set { m_iGiftId = value; }
    }

    private int m_iGiftNum;  //奖励数量

    public int GiftNum
    {
        get { return m_iGiftNum; }
        set { m_iGiftNum = value; }
    }


    
    /// <summary>
    /// 读取文本数据
    /// </summary>
    /// <param name="lstInfo"></param>
    /// <param name="index"></param>
    public override void ReadText(List<string> lstInfo, ref int index)
    {
        this.m_iGuestNum = INT(lstInfo[index++]);
        this.m_strTitle = STRING(lstInfo[index++]);
        this.m_strDesc = STRING(lstInfo[index++]);
        this.m_iGiftType = (GiftType)INT(lstInfo[index++]);
        this.m_iGiftId = INT(lstInfo[index++]);
        this.m_iGiftNum = INT(lstInfo[index++]); ;
    }
}
