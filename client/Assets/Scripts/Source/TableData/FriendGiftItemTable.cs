//  FriendGiftItemTable.cs
//  Author: ChengXia
//  2013-1-15

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 好友礼物表
/// </summary>

class FriendGiftItemTable : TableBase
{
    private int m_iID;  //礼物ID
    public int ID
    {
        get { return this.m_iID; }
    }

    private int m_iTypeID;  //类型ID
    public int TypeID
    {
        get { return this.m_iTypeID; }
    }

    private int m_iTableID; //物品ID
    public int TableID
    {
        get { return this.m_iTableID; }
    }

    private int m_iNum; //物品数量
    public int Num
    {
        get { return this.m_iNum; }
    }

    private int m_iSpIndex; //图集索引
    public int SpIndex
    {
        get { return this.m_iSpIndex; }
    }

    private string m_strSpiritName; //图集名
    public string SpiritName
    {
        get { return this.m_strSpiritName; }
    }

    private string m_strNumText; //数字文本
    public string NumText
    {
        get { return this.m_strNumText; }
    }

    private int m_iBorderType; //边框类型
    public int BorderType
    {
        get { return this.m_iBorderType; }
    }

    private string m_strName; //名字
    public string Name
    {
        get { return this.m_strName; }
    }

    private string m_strDesc; //礼物描述
    public string Desc
    {
        get { return this.m_strDesc; }
    }

    public FriendGiftItemTable()
        : base()
    { 
    }

    /// <summary>
    /// 读取数据
    /// </summary>
    /// <param name="lstInfo"></param>
    /// <param name="index"></param>
    public override void ReadText(List<string> lstInfo, ref int index)
    {
        this.m_iID = INT(lstInfo[index++]);
        this.m_iTypeID = INT(lstInfo[index++]);
        this.m_iTableID = INT(lstInfo[index++]);
        this.m_iNum = INT(lstInfo[index++]);
        this.m_iSpIndex = INT(lstInfo[index++]);
        this.m_strSpiritName = STRING(lstInfo[index++]);
        this.m_strNumText = STRING(lstInfo[index++]);
        this.m_iBorderType = INT(lstInfo[index++]);
        this.m_strName = STRING(lstInfo[index++]);
        this.m_strDesc = STRING(lstInfo[index++]);
    }
}
