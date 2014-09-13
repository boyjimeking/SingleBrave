using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  BBSkillTable.cs
//  Author: Lu Zexi
//  2013-12-02



/// <summary>
/// BB技能表
/// </summary>
public class BBSkillTable : TableBase
{
    private int m_iID;          //ID
    public int ID
    {
        get { return this.m_iID; }
    }
    private string m_strHeroName;   //英雄名
    public string HeroName
    {
        get { return this.m_strHeroName; }
    }
    private string m_strName;   //名字
    public string Name
    {
        get { return this.m_strName; }
    }
    private BBType m_eType;    //BB技能类型
    public BBType Type
    {
        get { return this.m_eType; }
    }
    private BBTargetType m_eTargetType;  //目标类型
    public BBTargetType TargetType
    {
        get { return this.m_eTargetType; }
    }
    private MoveType m_eMoveType;   //移动类型
    public MoveType MoveType
    {
        get { return this.m_eMoveType; }
    }
    private float[] m_vecLevelAttack; //等级相应攻击力
    public float[] VecLevelAttack
    {
        get { return this.m_vecLevelAttack; }
    }
    private float m_fXinDropRate;   //心掉落提升率
    public float XinDropRate
    {
        get { return this.m_fXinDropRate; }
    }
    private float m_fShuijingDropRate;  //水晶掉落率提升
    public float ShuijingDropRate
    {
        get { return this.m_fShuijingDropRate; }
    }
    private int[] m_vecBUF; //附带的BUF
    public int[] VecBUF
    {
        get { return this.m_vecBUF; }
    }
    private float[] m_vecBUFRate;   //BUF几率
    public float[] VecBUFRate
    {
        get { return this.m_vecBUFRate; }
    }
    private int[] m_vecBUFRound;    //BUF回合
    public int[] VecBUFRound
    {
        get { return this.m_vecBUFRound; }
    }
    private float[] m_vecBUFFArg;   //BUFF参数
    public float[] VecBUFFArg
    {
        get { return this.m_vecBUFFArg; }
    }
    private string m_strSpellEffect;    //施法特效
    public string SpellEffect
    {
        get { return this.m_strSpellEffect; }
    }
    private float m_fSpellEffectTime;   //施法特效持续时间
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
    private string m_strSkillEffect;    //技能特效
    public string SkillEffect
    {
        get { return this.m_strSkillEffect; }
    }
    private float m_fSkillEffectTime;   //技能特效持续时间
    public float SkillEffectTime
    {
        get { return this.m_fSkillEffectTime; }
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
    private int m_iHitDis;  //击打终点距离
    public int HitDis
    {
        get { return this.m_iHitDis; }
    }
    private int m_iHitRange;    //击打范围
    public int HitRange
    {
        get { return this.m_iHitRange; }
    }
    private string m_strDesc;   //描述
    public string Desc
    {
        get { return this.m_strDesc; }
    }
    private int m_iHitNum;  //击打次数
    public int HitNum
    {
        get { return this.m_iHitNum; }
    }
    private List<int> m_lstHitTime;   //击打时间
    public List<int> LstHitTime
    {
        get { return new List<int>(this.m_lstHitTime); }
    }
    private List<int> m_lstHitEndTime;  //击打结束时间
    public List<int> LstHitEndTime
    {
        get { return new List<int>(this.m_lstHitEndTime); }
    }
    private List<float> m_lstHitRate;   //击中比率
    public List<float> LstHitRate
    {
        get { return new List<float>(this.m_lstHitRate); } 
    }

    public BBSkillTable()
    {
        this.m_vecBUF = new int[2];
        this.m_vecBUFRate = new float[2];
        this.m_vecBUFRound = new int[2];
        this.m_vecBUFFArg = new float[2];
        this.m_vecLevelAttack = new float[10];
        this.m_lstHitTime = new List<int>();
        this.m_lstHitEndTime = new List<int>();
        this.m_lstHitRate = new List<float>();
    }

    /// <summary>
    /// 读取文本数据
    /// </summary>
    /// <param name="lstInfo"></param>
    /// <param name="index"></param>
    public override void ReadText(List<string> lstInfo, ref int index)
    {
        this.m_lstHitTime.Clear();
        this.m_lstHitEndTime.Clear();
        this.m_lstHitRate.Clear();

        this.m_iID = INT(lstInfo[index++]);
        this.m_strHeroName = STRING(lstInfo[index++]);
        this.m_strName = STRING(lstInfo[index++]);
        this.m_eType = (BBType)INT(lstInfo[index++]);
        this.m_eTargetType = (BBTargetType)INT(lstInfo[index++]);
        this.m_eMoveType = (MoveType)INT(lstInfo[index++]);
        for( int i = 0 ; i<this.m_vecLevelAttack.Length ; i++ )
        {
            this.m_vecLevelAttack[i] = FLOAT(lstInfo[index++]);
        }
        this.m_fXinDropRate = FLOAT(lstInfo[index++]);
        this.m_fShuijingDropRate = FLOAT(lstInfo[index++]);
        for (int i = 0; i < m_vecBUF.Length; i++)
        {
            this.m_vecBUF[i] = INT(lstInfo[index++]);
            this.m_vecBUFRate[i] = FLOAT(lstInfo[index++]);
            this.m_vecBUFRound[i] = INT(lstInfo[index++]);
            this.m_vecBUFFArg[i] = FLOAT(lstInfo[index++]);
        }
        this.m_strSpellEffect = STRING(lstInfo[index++]);
        this.m_fSpellEffectTime = FLOAT(lstInfo[index++]);
        this.m_strDaoGuangEffect = STRING(lstInfo[index++]);
        this.m_fDaoGuangTime = FLOAT(lstInfo[index++]);
        this.m_strSkillEffect = STRING(lstInfo[index++]);
        this.m_fSkillEffectTime = FLOAT(lstInfo[index++]);
        this.m_strHitEffect = STRING(lstInfo[index++]);
        this.m_fHitEffectTime = FLOAT(lstInfo[index++]);
        this.m_iHitDis = INT(lstInfo[index++]);
        this.m_iHitRange = INT(lstInfo[index++]);
        this.m_strDesc = STRING(lstInfo[index++]);
        this.m_iHitNum = INT(lstInfo[index++]);
        for (int i = 0; i < this.m_iHitNum; i++)
        {
            this.m_lstHitTime.Add(INT(lstInfo[index++]));
            this.m_lstHitEndTime.Add(INT(lstInfo[index++]));
            this.m_lstHitRate.Add(FLOAT(lstInfo[index++]));
        }
    }

}
