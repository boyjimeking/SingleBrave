using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  FuBen.cs
//  Author: Lu Zexi
//  2013-12-12






/// <summary>
/// PVE副本世界数据类
/// </summary>
public class FuBen
{
    public bool m_bActive;  //是否被激活
    public int m_iWorldID;  //世界ID
    public int m_iAreaIndex;    //区域索引
    public int m_iDungeonIndex; //副本索引
    public int m_iGateIndex;    //关卡索引
    //public bool m_bGateIsFinished;//是否完成当前关卡

    //剧情
    public bool m_bDungeonStory; //副本索引
}
