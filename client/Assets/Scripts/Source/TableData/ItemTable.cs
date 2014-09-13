using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  ItemTable.cs
//  Author: Lu Zexi
//  2013-11-20




/// <summary>
/// 物品表类
/// </summary>
public class ItemTable : TableBase
{
    private int m_iID;  //唯一标识
    public int ID
    {
        get { return this.m_iID; }
    }
    private string m_strName;  //名字
    public string Name
    {
        get { return this.m_strName; }
    }
    private string m_strShortName;  //缩略名
    public string ShortName
    {
        get { return this.m_strShortName; }
    }
    private int m_iType;    //类型
    public int Type
    {
        get { return this.m_iType; }
    }
    private string m_strSpiritName; //图集名
    public string SpiritName
    {
        get { return this.m_strSpiritName; }
    }
    private int m_iMaxNum;  //可叠加数量
    public int MaxNum
    {
        get { return this.m_iMaxNum; }
    }
    private int m_iBattleMaxNum;  //战斗物品可叠加数量
    public int BattleMaxNum
    {
        get { return this.m_iBattleMaxNum; }
    }
    private int m_iPrice;    //出售价格
    public int Price
    {
        get { return this.m_iPrice; }
    }
    private string m_strDesc;   //描述
    public string Desc
    {
        get { return this.m_strDesc.Replace("\\n", "\n"); ; }
    }
    private string m_strDetail;
    public string Detail  //详细介绍
    {
        get { return m_strDetail.Replace("\\n", "\n"); }
    }
    private string m_strSellWaring;
    public string SellWarning  //出售警示文字
    {
        get { return m_strSellWaring.Replace("\\n", "\n"); }
    }
    private int m_iEventID; //事件ID
    public int EventID
    {
        get { return this.m_iEventID; }
    }
    private float m_fAttack;  //攻击力增加比率
    public float Attack
    {
        get { return this.m_fAttack; }
    }
    private float m_fDefence; //防御力增加比率
    public float Defence
    {
        get { return this.m_fDefence; }
    }
    private float m_fRecover; //回复力增加比率
    public float Recover
    {
        get { return this.m_fRecover; }
    }
    private float m_fMaxHP;   //最大HP增加比率
    public float MaxHP
    {
        get { return this.m_fMaxHP; }
    }

    public ItemTable()
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
        this.m_strName = STRING(lstInfo[index++]);
        this.m_strShortName = STRING(lstInfo[index++]);
        this.m_iType = INT(lstInfo[index++]);
        this.m_strSpiritName = STRING(lstInfo[index++]);
        this.m_iMaxNum = INT(lstInfo[index++]);
        this.m_iBattleMaxNum = INT(lstInfo[index++]);
        this.m_iPrice = INT(lstInfo[index++]);
        this.m_strDesc = STRING(lstInfo[index++]);
        this.m_strDetail = STRING(lstInfo[index++]);
        this.m_strSellWaring = STRING(lstInfo[index++]);
        this.m_iEventID = INT(lstInfo[index++]);
        this.m_fAttack = FLOAT(lstInfo[index++]);
        this.m_fDefence = FLOAT(lstInfo[index++]);
        this.m_fRecover = FLOAT(lstInfo[index++]);
        this.m_fMaxHP = FLOAT(lstInfo[index++]);

    }

}
