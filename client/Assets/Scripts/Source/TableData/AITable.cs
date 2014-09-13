using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  AITable.cs
//  Author: Lu Zexi
//  2014-01-15



/// <summary>
/// AI数据表
/// </summary>
public class AITable : TableBase
{
    private int m_iID;  //ID
    public int ID
    {
        get { return this.m_iID; }
    }
    private string m_strName;   //名字
    public string Name
    {
        get { return this.m_strName; }
    }
    private int m_iMinAttackNum;    //最小攻击次数
    public int MinAttackNum
    {
        get { return this.m_iMinAttackNum; }
    }
    private int m_iMaxAttackNum;    //最大攻击次数
    public int MaxAttackNum
    {
        get { return this.m_iMaxAttackNum; }
    }
    private AIBB_CONDITION[] m_vecBBCondition; //BB技能条件
    public AIBB_CONDITION[] VecBBCondition
    {
        get { return this.m_vecBBCondition; }
    }
    private float[] m_vecBBArg; //BB技能条件参数
    public float[] VecBBArg
    {
        get { return this.m_vecBBArg; }
    }
    private AITARGET m_iTargetType; //目标
    public AITARGET TargetType
    {
        get { return this.m_iTargetType; }
    }
    private float m_fTargetArg; //目标参数
    public float TargetArg
    {
        get { return this.m_fTargetArg; }
    }

    public AITable()
    {
        this.m_vecBBCondition = new AIBB_CONDITION[2];
        this.m_vecBBArg = new float[2];
    }

    /// <summary>
    /// 读取文本
    /// </summary>
    /// <param name="lstInfo"></param>
    /// <param name="index"></param>
    public override void ReadText(List<string> lstInfo, ref int index)
    {
        this.m_iID = INT(lstInfo[index++]);
        this.m_strName = STRING(lstInfo[index++]);
        this.m_iMinAttackNum = INT(lstInfo[index++]);
        this.m_iMaxAttackNum = INT(lstInfo[index++]);
        for (int i = 0; i < this.m_vecBBCondition.Length; i++)
        {
            this.m_vecBBCondition[i] = (AIBB_CONDITION)INT(lstInfo[index++]);
            this.m_vecBBArg[i] = FLOAT(lstInfo[index++]);
        }
        this.m_iTargetType = (AITARGET)INT(lstInfo[index++]);
        this.m_fTargetArg = FLOAT(lstInfo[index++]);
    }

}
