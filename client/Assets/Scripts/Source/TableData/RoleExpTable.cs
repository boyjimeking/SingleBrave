using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

//  RoleExpTable.cs
//  Author: Lu Zexi
//  2013-11-18

/// <summary>
/// 角色经验表
/// </summary>
public class RoleExpTable : TableBase
{
    private int m_iLevel;    //等级
    public int Level
    {
        get { return this.m_iLevel; }
    }
    private int m_iExp; //经验
    public int Exp
    {
        get { return this.m_iExp; }
    }
    private int m_iHP;  //体力
    public int HP
    {
        get { return this.m_iHP; }
    }
    private int m_iCost;    //领导力
    public int Cost
    {
        get { return this.m_iCost; }
    }
    private int m_iMaxFriend;   //最大好友数
    public int MaxFriend
    {
        get { return this.m_iMaxFriend; }
    }

    public RoleExpTable()
    { 
        //
    }

    /// <summary>
    /// 读取文本数据
    /// </summary>
    /// <param name="lstInfo"></param>
    /// <param name="index"></param>
    public override void ReadText(List<string> lstInfo, ref int index)
    {
        this.m_iLevel = INT(lstInfo[index++]);
        this.m_iExp = INT(lstInfo[index++]);
        this.m_iHP = INT(lstInfo[index++]);
        this.m_iCost = INT(lstInfo[index++]);
        this.m_iMaxFriend = INT(lstInfo[index++]);
    }
}
