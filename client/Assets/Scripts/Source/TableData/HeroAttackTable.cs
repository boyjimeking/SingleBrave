using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  HeroAttackTable.cs
//  Author: Lu Zexi
//  2013-12-02




/// <summary>
/// 英雄攻击表
/// </summary>
public class HeroAttackTable : TableBase
{
    private int m_iID;  //英雄ID
    public int ID
    {
        get { return this.m_iID; }
    }
    private string m_strSpellEffect;    //施法特效
    public string SpellEffect
    {
        get { return this.m_strSpellEffect; }
    }
    private float m_fSpellEffectTime;   //施法持续时间
    public float SpellEffectTime
    {
        get { return this.m_fSpellEffectTime; }
    }
    private string m_strDaoGuangEffect; //刀光特效
    public string DaoGuangEffect
    {
        get { return this.m_strDaoGuangEffect; }
    }
    private float m_fDaoGuangTime;  //刀光特效持续时间
    public float DaoGuangTime
    {
        get { return this.m_fDaoGuangTime; }
    }
    private string m_strHitEffect;  //击中特效
    public string HitEffect
    {
        get { return this.m_strHitEffect; }
    }
    private float m_fHitEffectTime; //击中特效持续时间
    public float HitEffectTime
    {
        get { return this.m_fHitEffectTime; }
    }
    private int m_iHitDis;  //击中距离
    public int HitDis
    {
        get { return this.m_iHitDis; }
    }
    private int m_iHitRange;    //随机范围
    public int HitRange
    {
        get { return this.m_iHitRange; }
    }
    private int m_iHitNum;  //击打次数
    public int HitNum
    {
        get { return this.m_iHitNum; }
    }
    private List<int> m_lstHitTime; //击打时间
    public List<int> LstHitTime
    {
        get { return new List<int>(this.m_lstHitTime); }
    }
    private List<int> m_lstHitEndTime;  //击打结束时间
    public List<int> LstHitEndTime
    {
        get { return new List<int>(this.m_lstHitEndTime); }
    }
    private List<float> m_lstHitRate;   //击打比率
    public List<float> LstHitRate
    {
        get { return new List<float>(this.m_lstHitRate); }
    }

    public HeroAttackTable()
    {
        this.m_lstHitTime = new List<int>();
        this.m_lstHitEndTime = new List<int>();
        this.m_lstHitRate = new List<float>();
    }

    /// <summary>
    /// 读取文本
    /// </summary>
    /// <param name="lstInfo"></param>
    /// <param name="index"></param>
    public override void ReadText(List<string> lstInfo, ref int index)
    {
        this.m_lstHitTime.Clear();
        this.m_lstHitEndTime.Clear();
        this.m_lstHitRate.Clear();

        this.m_iID = INT(lstInfo[index++]);
        this.m_strSpellEffect = STRING(lstInfo[index++]);
        this.m_fSpellEffectTime = FLOAT(lstInfo[index++]);
        this.m_strDaoGuangEffect = STRING(lstInfo[index++]);
        this.m_fDaoGuangTime = FLOAT(lstInfo[index++]);
        this.m_strHitEffect = STRING(lstInfo[index++]);
        this.m_fHitEffectTime = FLOAT(lstInfo[index++]);
        this.m_iHitDis = INT(lstInfo[index++]);
        this.m_iHitRange = INT(lstInfo[index++]);
        this.m_iHitNum = INT(lstInfo[index++]);
        for (int i = 0; i < this.m_iHitNum; i++)
        {
            this.m_lstHitTime.Add(INT(lstInfo[index++]));
            this.m_lstHitEndTime.Add(INT(lstInfo[index++]));
            this.m_lstHitRate.Add(FLOAT(lstInfo[index++]));
        }
    }

}
