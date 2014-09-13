using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  LeaderSkillTable.cs
//  Author: Lu Zexi
//  2013-12-02


/// <summary>
/// 队长技能表
/// </summary>
public class LeaderSkillTable : TableBase
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
    private Nature m_eAttackProperty;  //攻击加成属性
    public Nature AttackProperty
    {
        get { return this.m_eAttackProperty; }
    }
    private float m_fAttackRate;    //攻击加成比率
    public float AttackRate
    {
        get { return this.m_fAttackRate; }
    }
    private float m_fAllAttackRate; //全体攻击提升比率
    public float AllAttackRate
    {
        get { return this.m_fAllAttackRate; }
    }
    private Nature m_eHurtProperty; //受到某伤害属性
    public Nature HurtProperty
    {
        get { return this.m_eHurtProperty; }
    }
    private float m_fHurtRate;   //受到某属性伤害减少比率
    public float HurtRate
    {
        get { return this.m_fHurtRate; }
    }
    private float m_fSparkHurtRate; //SPARK伤害加成比率
    public float SparkHurtRate
    {
        get { return this.m_fSparkHurtRate; }
    }
    private float m_fSparkShuijingRate;  //SPARK水晶加成比率
    public float SparkShuijingRate
    {
        get { return this.m_fSparkShuijingRate; }
    }
    private float m_fSparkHeartRate;    //SPARK心加成比率
    public float SparkHeartRate
    {
        get { return this.m_fSparkHeartRate; }
    }
    private float m_fSparkGoldRate; //SPARK金币加成比率
    public float SparkGoldRate
    {
        get { return this.m_fSparkGoldRate; }
    }
    private float m_fSparkFarmRate; //SPARK农场点加成比率
    public float SparkFarmRate
    {
        get { return this.m_fSparkFarmRate; }
    }
    private float m_fHeartRecoverRate;   //心恢复加成比率
    public float HeartRecoverRate
    {
        get { return this.m_fHeartRecoverRate; }
    }
    private float m_fBBHPIncrease;  //吸水晶后BB槽增加量
    public float BBHPIncrease
    {
        get { return this.m_fBBHPIncrease; }
    }
    private float m_fRoundBBIncrease;    //回合后BB加成量
    public float RoundBBIncrease
    {
        get { return this.m_fRoundBBIncrease; }
    }
    private string m_strDesc;   //描述
    public string Desc
    {
        get { return this.m_strDesc; }
    }

    public LeaderSkillTable()
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
        this.m_iID = INT(lstInfo[index++]);
        this.m_strName = STRING(lstInfo[index++]);
        this.m_eAttackProperty = (Nature)INT(lstInfo[index++]);
        this.m_fAttackRate = FLOAT(lstInfo[index++]);
        this.m_fAllAttackRate = FLOAT(lstInfo[index++]);
        this.m_eHurtProperty = (Nature)INT(lstInfo[index++]);
        this.m_fHurtRate = FLOAT(lstInfo[index++]);
        this.m_fSparkHurtRate = FLOAT(lstInfo[index++]);
        this.m_fSparkShuijingRate = FLOAT(lstInfo[index++]);
        this.m_fSparkGoldRate = FLOAT(lstInfo[index++]);
        this.m_fSparkFarmRate = FLOAT(lstInfo[index++]);
        this.m_fSparkHeartRate = FLOAT(lstInfo[index++]);
        this.m_fHeartRecoverRate = FLOAT(lstInfo[index++]);
        this.m_fBBHPIncrease = FLOAT(lstInfo[index++]);
        this.m_fRoundBBIncrease = FLOAT(lstInfo[index++]);
        this.m_strDesc = STRING(lstInfo[index++]);
    }

}
