using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  QueueTable.cs
//  Author: Lu Zexi
//  2013-11-18




/// <summary>
/// 怪物队列表
/// </summary>
public class MonsterTeamTable : TableBase
{
    private int m_iGateID;  //关卡ID
    public int GateID
    {
        get { return this.m_iGateID; }
    }
    private int m_iOrderID;   //序列
    public int OrderID
    {
        get { return this.m_iOrderID; }
    }
    private bool m_bFix;   //是否固定
    public bool Fix
    {
        get { return this.m_bFix; }
    }
    private string m_strBGSound;    //背景音乐
    public string BGSound
    {
        get { return this.m_strBGSound; }
    }
    private int[] m_vecMonster; //怪物
    public int[] VecMonster
    {
        get { return this.m_vecMonster; }
    }
    private float m_fHPFix; //怪物HP修正
    public float HPFix
    {
        get { return this.m_fHPFix; }
    }
    private float m_fAttackFix; //怪物攻击修正
    public float AttackFix
    {
        get { return this.m_fAttackFix; }
    }
    private float m_fDefenceFix;    //怪物防御修正
    public float DefenceFix
    {
        get { return this.m_fDefenceFix; }
    }
    private float m_fCriticalFix;   //怪物暴击修正
    public float CriticalFix
    {
        get { return this.m_fCriticalFix; }
    }
    private float m_fCatchFix;  //捕捉修正
    public float CatchFix
    {
        get { return this.m_fCatchFix; }
    }

    public MonsterTeamTable()
    {
        this.m_vecMonster = new int[6];
    }


    /// <summary>
    /// 读取文本
    /// </summary>
    /// <param name="lstInfo"></param>
    /// <param name="index"></param>
    public override void ReadText(List<string> lstInfo, ref int index)
    {

        this.m_iGateID = INT(lstInfo[index++]);
        this.m_iOrderID = INT(lstInfo[index++]);
        this.m_bFix = BOOL(lstInfo[index++]);
        this.m_strBGSound = STRING(lstInfo[index++]);
        for( int i = 0 ; i<this.m_vecMonster.Length ; i++ )
        {
            this.m_vecMonster[i] = INT(lstInfo[index++]);
        }
        
        this.m_fHPFix = FLOAT(lstInfo[index++]);
        this.m_fAttackFix = FLOAT(lstInfo[index++]);
        this.m_fDefenceFix = FLOAT(lstInfo[index++]);
        this.m_fCriticalFix = FLOAT(lstInfo[index++]);
        this.m_fCatchFix = FLOAT(lstInfo[index++]);
    }

}

