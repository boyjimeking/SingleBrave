using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Game.Base;
using Game.Gfx;
using Game.Resource;


//  BattleHero.cs
//  Author: Lu Zexi
//  2013-11-26


//BUF状态(0:无,1:中毒,2:虚弱,3:破甲,4:破刃,5:麻痹,6:封印,7:攻击提升,8:防御提升,9:暴击提升,10,回复力提升,11:心出现率提升,12:水晶出现率提升,13:HP持续恢复,14:致死一击)
/// <summary>
/// 战斗减益BUF
/// </summary>
public enum BATTLE_BUF
{
    NONE = 0,   //无状态
    DU = 1, //中毒
    XURUO = 2,  //虚弱
    POJIA = 3,  //破甲
    POREN = 4,  //破刃
    MA = 5, //麻痹
    FENGYIN = 6, //封印
    ATTACK_UP = 7,  //攻击提升
    DEFENCE_UP = 8, //防御提升
    CRITICAL_UP = 9,    //暴击提升
    RECOVER_UP = 10,    //回复力提升
    HEART_RATE_UP = 11, //心出现率提升
    SHUIJING_RATE_UP = 12,  //水晶出现率提升
    HP_RECOVER = 13,    //HP持续回复
    DEAD_NOT = 14,  //致死一击
    CLAEN_BUFF = 15,    //清除BUFF
}


/// <summary>
/// 优惠类型
/// </summary>
public enum FAV_TYPE
{ 
    NONE = 0,  //无
    EXP_1_5 = 1,    //经验1.5倍
    EXP_2 = 2,      //经验2倍
    STRENGTH_HALF,  //体力减半
    ITEM_DROP_1_5,  //物品掉落率1.5倍
    FARM_NUM_2, //农场点2倍
    CATCH_RATE_1_5, //捕捉率1.5
    GOLD_NUM_2, //金币数2倍
}

/// <summary>
/// AI技能条件
/// </summary>
public enum AIBB_CONDITION
{ 
    ATTACK_NUM = 1, //N回合时触发
    SMALL_HP,   //小于N HP百分比
    RATE,   //N概率
    ROUND_AFTER,    //多少回合后
}



/// <summary>
/// AI目标
/// </summary>
public enum AITARGET
{ 
    RANDOM = 1, //随机
    MIN_HP ,    //最小HP
    MAX_HP, //最大HP
}

/// <summary>
/// 战场时间场景
/// </summary>
public enum BATTLE_TIME_SCENE
{ 
    BATTLE_INIT = 1,    //战斗初始化时
    BATTLE_START,   //战斗开始时
    BATTLE_END,     //战斗结束时
    BATTLE_ROUND_START, //回合开始时
    BATTLE_ROUND_END,  //回合结束时
    BATTLE_TARGET_DEAD, //目标死亡时
}

/// <summary>
/// 战场英雄
/// </summary>
public class BattleHero
{
    public const int MOVE_BACK_SPEED = 50; //移回的移动速度
    private const int HERO_MAX_NUM = 6; //英雄最多数量

    //战斗数据
    public bool m_bSelf; //是否是己方
    public int m_iIndex;    //战斗英雄位置索引
    public int m_iID;   //英雄唯一标识
    public int m_iTableID;  //配置表的对应ID
    public int m_iLevel;    //等级
    public int m_iHp;  //血量//
    public CPropertyValue m_cMaxHP;    //最大血量
    public CPropertyValue m_cAttack;   //攻击//
    public CPropertyValue m_cDefence;   //防御
    public CPropertyValue m_cRecover;  //恢复//
    public CPropertyValue m_cCritical;   //暴击率//
    public int m_iBBSkillLevel; //BB技能等级//
    public int m_iAttackMaxNum; //可攻击最大次数
    public int m_iBBMaxHP;  //BB最大能量槽

    //抗性
    public CPropertyValue m_cOpposeDu;   //毒抗性
    public CPropertyValue m_cOpposeXuruo;    //虚弱抗性
    public CPropertyValue m_cOpposePojia;    //破甲抗性
    public CPropertyValue m_cOpposePoren;    //破刃抗性
    public CPropertyValue m_cOpposeFengyin;  //封印抗性
    public CPropertyValue m_cOpposeMa;   //麻痹抗性

    //状态概率
    public CPropertyValue m_cDuRate; //毒状概率
    public int m_iDuRound;  //中毒回合
    public CPropertyValue m_cXuRuoRate;  //虚弱概率
    public int m_iXuRuoRound;   //虚弱回合
    public CPropertyValue m_cPojiaRate;   //破甲概率
    public int m_iPojiaRound;   //破甲回合
    public CPropertyValue m_cPorenRate;   //破刃概率
    public int m_iPorenRound;   //破刃回合
    public CPropertyValue m_cMaRate; //麻痹概率
    public int m_iMaRound;  //麻痹回合
    public CPropertyValue m_cFengyinRate; //封印概率
    public int m_iFengyinRound; //封印回合

    //统计
    public int m_iTotalDamage;    //造成的总伤害值
    public int m_iTotalSuperDamage;  //鞭尸伤害总数
    public int m_iTotalSkillKillNum;    //用技能KO次数

    //战斗临时数据
    public float[] m_vecHitStartTime; //被对方英雄击中开始时间
    public float[] m_vecHitEndTime;   //被对方英雄击中结束时间
    public List<BATTLE_BUF> m_lstBUF = new List<BATTLE_BUF>();    //BUF//
    public List<int> m_lstBUF_Round = new List<int>();  //BUF回合数//
    public List<CPropertyValue> m_lstBUF_Arg = new List<CPropertyValue>();  //BUF参数//
    public int m_iAttackNum;    //可攻击次数
    public float m_fBBHP; //BB能量槽
    public bool m_bDefence; //防御态
    public bool m_bDead;    //是否死亡
    public float m_fDeadTime;   //死亡时间
    public bool m_bDropJudge;   //掉落判定
    public int m_iDummyHP;  //虚假的预测HP

    //装备
    public int m_iEquipActionNum; //装备事件执行次数
    public int m_iEquipEvent;   //装备事件
    public CPropertyValue m_cMissDefenceRate;   //无视防御
    public CPropertyValue m_cDecHurtRate;   //减少伤害概率
    public CPropertyValue m_cDecHurtRateArg;   //减少伤害比率
    public CPropertyValue m_cAbsorbDamageRate;  //吸血概率
    public CPropertyValue m_cAbsorbDamageRateArg;   //吸血比率
    public CPropertyValue m_cReboundRate;   //伤害反弹概率
    public CPropertyValue m_cReboundRateArg;    //伤害反弹比率
    public CPropertyValue m_cRecoverHurtRate;   //回复伤害概率
    public CPropertyValue m_cRecoverHurtRateArg;    //回复伤害比率
    public CPropertyValue m_cRecoverHurtRateArgEx;    //回复伤害系数
    public CPropertyValue m_cHurtIncBBHPRate;   //受伤时增加BB槽概率
    public CPropertyValue m_cHurtIncBBHP;   //受伤时增加BB槽数值

    //场景数据
    public Vector3 m_cStartPos;   //站位数据
    public Vector3 m_cAttackPos;  //攻击位置
    public Vector3 m_cUIStartPos;   //UI站立位置
    
    //攻击固定数据
    public string m_strEffectRes1;  //特效资源1释放
    public string m_strEffectRes2;  //特效资源2击中
    public int m_iHitDistance;  //打击点距离
    public int m_iHitRange; //打击点随机范围
    public int m_iHitNum;    //打击次数
    public List<int> m_lstHitTime = new List<int>();   //打击点时间
    public List<float> m_lstHitRate = new List<float>();    //击打数值百分比
    public UnityEngine.Object m_cResAttackSpell;  //施法特效
    public UnityEngine.Object m_cResAttackDaoGuang;   //刀光特效
    public UnityEngine.Object m_cResAttackHit;    //击中特效

    //BB技能
    public string m_strBBEffectRes1;  //特效1释放
    public string m_strBBEffectRes2;  //特效2击中
    public int m_iBBHitDistance;  //BB打击点距离
    public int m_iBBHitRange; //BB打击点随机范围
    public int m_iBBHitNum;    //BB打击次数
    public string m_strSkillName;    //技能描述
    public CPropertyValue m_cBBAttack; //BB攻击力
    public BBType m_eBBType = BBType.NONE; //BB类型
    public BBTargetType m_eBBTargetType;   //目标类型//
    public MoveType m_eBBMoveType;  //BB移动类型
    public List<int> m_lstBBHitTime = new List<int>();  //打击点时间
    public List<float> m_lstBBHitRate = new List<float>();    //击打数值百分比
    public UnityEngine.Object m_cResSklillSpell;  //施法特效
    public UnityEngine.Object m_cResSkillDaoGuang;   //刀光特效
    public UnityEngine.Object m_cResSkill;  //技能特效
    public UnityEngine.Object m_cResSkillHit;    //击中特效
    public Texture m_cResAvator;    //头像

    //掉落加成
    public CPropertyValue m_cFarmDropRateInc;  //农场点掉落率加成
    public CPropertyValue m_cJinbiDropRateInc;  //金币掉落率加成
    public CPropertyValue m_cXinDropRateInc;    //心掉落率加成
    public CPropertyValue m_cShuijingDropRateInc;   //水晶掉落率加成
    public CPropertyValue m_cItemDropRateInc;   //物品掉落率加成

    //怪物掉落率
    public float m_fCatchRate;  //扑捉率
    public int m_iCatchID;  //捕捉ID
    public float m_fFarmRate;   //农场点掉率
    public int m_iFarmDropNum;  //农场点掉落数量
    public int m_iFarmDropNumMax;   //农场点掉落最大数量
    public float m_fGoldRate;   //金币掉落率
    public int m_iGoldDropNum;  //金币掉落数量
    public int m_iGoldDropNumMax;   //金币掉落最大数量
    public CPropertyValue m_cHeartDropRate;   //原始心掉落率
    public int m_iHeartDropNum; //心掉落最大个数
    public CPropertyValue m_cShuijingDropRate;   //关卡水晶掉落率
    public int m_iShuijingDropMinNum;   //关卡水晶掉落最小个数
    public int m_iShuijingDropNum;  //关卡水晶掉落最大个数
    public float m_fDropItemRate;  //物品掉落概率

    //音效
    public AudioClip m_cSEHit1; //击打音效1
    public AudioClip m_cSEHit2; //击打音效2

    //数据表固定数据
    public string m_strName; //姓名//
    public Nature m_eNature;   //属性//
    public string m_strModel;    //模型名字//
    public string m_strAvatorM; //中头像
    public string m_strAvatorL; //大头像
    public string m_strAvatorA; //全身像
    public string m_strSEHit1;  //击打音效1
    public string m_strSEHit2;  //击打音效2
    public int m_iBBSkillTableID;   //BB技能配置ID//
    public float m_fMoveSpeed; //速度//
    public MoveType m_eMoveType;   //移动类型//
    public AITable m_cAITable;  //AI表

    //控制器
    private GfxObject m_cGfxObj;    //渲染实体
    private AIControl m_cAI;    //人工智能
    private CmdControl m_cCmdControl;   //命令控制器

    public BattleHero( )
    {
        this.m_vecHitStartTime = new float[HERO_MAX_NUM];
        this.m_vecHitEndTime = new float[HERO_MAX_NUM];
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public void Destory()
    {
        if (this.m_cGfxObj != null)
            this.m_cGfxObj.Destory();
        this.m_cAI = null;
        this.m_cCmdControl = null;

        this.m_cResAttackSpell = null;
        this.m_cResAttackDaoGuang = null;
        this.m_cResAttackHit = null;

        this.m_cResSklillSpell = null;
        this.m_cResSkillDaoGuang = null;
        this.m_cResSkill = null;
        this.m_cResSkillHit = null;

        this.m_cResAvator = null;

        this.m_cSEHit1 = null;
        this.m_cSEHit2 = null;
    }

    /// <summary>
    /// 设置命令控制器
    /// </summary>
    /// <param name="cmd"></param>
    public void SetCmdControl(CmdControl cmd)
    {
        this.m_cCmdControl = cmd;
    }

    /// <summary>
    /// 设置渲染实体
    /// </summary>
    /// <param name="obj"></param>
    public void SetGfx(GfxObject obj)
    {
        this.m_cGfxObj = obj;
    }

    /// <summary>
    /// 设置AI
    /// </summary>
    /// <param name="aiCon"></param>
    public void SetAI(AIControl aiCon)
    {
        this.m_cAI = aiCon;
    }

    /// <summary>
    /// 清除
    /// </summary>
    public void Clear()
    {
        for (int i = 0; i < this.m_vecHitStartTime.Length; i++)
        {
            this.m_vecHitStartTime[i] = 0;
            this.m_vecHitEndTime[i] = 0;
        }
        ClearBUF();
        this.m_bDefence = false;
    }

    /// <summary>
    /// BUF是否存在
    /// </summary>
    /// <param name="buf"></param>
    /// <returns></returns>
    public bool BUFExist(BATTLE_BUF buf)
    {
        for (int i = 0; i < this.m_lstBUF.Count; i++)
        {
            if (this.m_lstBUF[i] == buf)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 加入BUF
    /// </summary>
    /// <param name="buf"></param>
    /// <param name="round"></param>
    /// <param name="arg"></param>
    public BATTLE_BUF BUFAdd(BATTLE_BUF buf, int round, float arg)
    {
        //特殊处理清除所有BUFF
        if (buf == BATTLE_BUF.CLAEN_BUFF)
        {
            CleanDeBuf();
            return BATTLE_BUF.NONE;
        }
        bool exist = false;
        int existIndex = -1;
        CPropertyValue proValue = null;
        for (int i = 0; i < this.m_lstBUF.Count; i++)
        {
            if (this.m_lstBUF[i] == buf)
            {
                exist = true;
                existIndex = i;
                break;
            }
        }

        if (exist)//若已存在,替换
        {
            this.m_lstBUF_Round[existIndex] = round*2;
            proValue = this.m_lstBUF_Arg[existIndex];
            proValue.SetData(arg);
        }
        else
        {
            this.m_lstBUF.Add(buf);
            this.m_lstBUF_Round.Add(round * 2);
            proValue = new CPropertyValue(arg);
            this.m_lstBUF_Arg.Add( proValue );
        }

        switch (buf)
        {
            case BATTLE_BUF.DU: //毒
                break;
            case BATTLE_BUF.MA: //麻痹
                break;
            case BATTLE_BUF.FENGYIN:    //封印
                break;
            case BATTLE_BUF.XURUO:  //虚弱
                proValue.SetData(-GAME_DEFINE.XURUORate);
                if (!exist)
                    this.m_cRecover.AddRate(proValue);
                break;
            case BATTLE_BUF.POJIA:  //破甲
                proValue.SetData(-GAME_DEFINE.POJIARate);
                if (!exist)
                    this.m_cDefence.AddRate(proValue);
                break;
            case BATTLE_BUF.POREN:  //破刃
                proValue.SetData(-GAME_DEFINE.PORENRate);
                if (!exist)
                    this.m_cAttack.AddRate(proValue);
                break;
            case BATTLE_BUF.ATTACK_UP:  //攻击提升
                if (!exist)
                    this.m_cAttack.AddRate(proValue);
                break;
            case BATTLE_BUF.CRITICAL_UP:    //暴击率上升
                //proValue.SetData(GAME_DEFINE.BUFFCriticalRate);
                if (!exist)
                    this.m_cCritical.AddRate(proValue);
                break;
            case BATTLE_BUF.DEFENCE_UP: //防御提升
                if (!exist)
                    this.m_cDefence.AddRate(proValue);
                break;
            case BATTLE_BUF.RECOVER_UP: //回复提升
                if (!exist)
                    this.m_cRecover.AddRate(proValue);
                break;
            case BATTLE_BUF.HEART_RATE_UP:  //心掉率上升
                if (!exist)
                    this.m_cXinDropRateInc.AddValue(proValue);
                break;
            case BATTLE_BUF.SHUIJING_RATE_UP:   //水晶掉率上升
                if (!exist)
                    this.m_cShuijingDropRateInc.AddValue(proValue);
                break;
        }

        return buf;
    }

    /// <summary>
    /// BUFF清除
    /// </summary>
    /// <param name="buf"></param>
    public void BUFClean(BATTLE_BUF buf)
    {
        for (int i = 0; i < this.m_lstBUF.Count; )
        {
            if (this.m_lstBUF[i] == buf)
            {
                switch (buf)
                {
                    case BATTLE_BUF.XURUO:  //虚弱
                        this.m_lstBUF_Arg[i].Remove();
                        break;
                    case BATTLE_BUF.POJIA:  //破甲
                        this.m_lstBUF_Arg[i].Remove();
                        break;
                    case BATTLE_BUF.POREN:  //破刃
                        this.m_lstBUF_Arg[i].Remove();
                        break;
                    case BATTLE_BUF.ATTACK_UP:  //攻击提升
                        this.m_lstBUF_Arg[i].Remove();
                        break;
                    case BATTLE_BUF.CRITICAL_UP:    //暴击率上升
                        this.m_lstBUF_Arg[i].Remove();
                        break;
                    case BATTLE_BUF.DEFENCE_UP: //防御提升
                        this.m_lstBUF_Arg[i].Remove();
                        break;
                    case BATTLE_BUF.RECOVER_UP: //回复提升
                        this.m_lstBUF_Arg[i].Remove();
                        break;
                    case BATTLE_BUF.HEART_RATE_UP:  //心提升
                        this.m_lstBUF_Arg[i].Remove();
                        break;
                    case BATTLE_BUF.SHUIJING_RATE_UP:   //水晶提升
                        this.m_lstBUF_Arg[i].Remove();
                        break;
                }

                this.m_lstBUF.RemoveAt(i);
                this.m_lstBUF_Arg.RemoveAt(i);
                this.m_lstBUF_Round.RemoveAt(i);

                continue;
            }
            i++;
        }
    }

    /// <summary>
    /// 减少BUF
    /// </summary>
    public void BUFDec()
    {
        for (int i = 0; i < this.m_lstBUF.Count;)
        {
            if (this.m_lstBUF_Round[i] <= 1)
            {
                switch (this.m_lstBUF[i])
                {
                    case BATTLE_BUF.XURUO:  //虚弱
                        this.m_lstBUF_Arg[i].Remove();
                        break;
                    case BATTLE_BUF.POJIA:  //破甲
                        this.m_lstBUF_Arg[i].Remove();
                        break;
                    case BATTLE_BUF.POREN:  //破刃
                        this.m_lstBUF_Arg[i].Remove();
                        break;
                    case BATTLE_BUF.ATTACK_UP:  //攻击提升
                        this.m_lstBUF_Arg[i].Remove();
                        break;
                    case BATTLE_BUF.CRITICAL_UP:    //暴击率上升
                        this.m_lstBUF_Arg[i].Remove();
                        break;
                    case BATTLE_BUF.DEFENCE_UP: //防御提升
                        this.m_lstBUF_Arg[i].Remove();
                        break;
                    case BATTLE_BUF.RECOVER_UP: //回复提升
                        this.m_lstBUF_Arg[i].Remove();
                        break;
                    case BATTLE_BUF.HEART_RATE_UP:  //心率提升
                        this.m_lstBUF_Arg[i].Remove();
                        break;
                    case BATTLE_BUF.SHUIJING_RATE_UP:   //水晶率提升
                        this.m_lstBUF_Arg[i].Remove();
                        break;
                    case BATTLE_BUF.DEAD_NOT:   //致死一击不削减回合数
                        if (this.m_iHp > 1)
                        {
                            i++;
                            continue;
                        }
                        break;
                }

                this.m_lstBUF.RemoveAt(i);
                this.m_lstBUF_Arg.RemoveAt(i);
                this.m_lstBUF_Round.RemoveAt(i);
                continue;
            }
            this.m_lstBUF_Round[i]--;
            i++;
        }
    }

    /// <summary>
    /// 清除BUF
    /// </summary>
    public void ClearBUF()
    {
        this.m_lstBUF.Clear();
        this.m_lstBUF_Round.Clear();
        foreach (CPropertyValue item in this.m_lstBUF_Arg)
        {
            item.Remove();
        }
        this.m_lstBUF_Arg.Clear();
    }

    /// <summary>
    /// 清除异常BUFF
    /// </summary>
    public void CleanDeBuf()
    {
        BUFClean(BATTLE_BUF.DU);
        BUFClean(BATTLE_BUF.POJIA);
        BUFClean(BATTLE_BUF.POREN);
        BUFClean(BATTLE_BUF.XURUO);
        BUFClean(BATTLE_BUF.MA);
        BUFClean(BATTLE_BUF.FENGYIN);
    }

    /// <summary>
    /// 减少HP
    /// </summary>
    /// <param name="num"></param>
    public void DecHP(int num)
    {
        this.m_iHp -= num;
        if (this.m_iHp < 0)
        {
            this.m_iHp = 0;
            if (BUFExist(BATTLE_BUF.DEAD_NOT))
            {
                this.m_iHp = 1;
            }
        }
    }

    /// <summary>
    /// 增加HP
    /// </summary>
    /// <param name="num"></param>
    public void AddHP(int num)
    {
        this.m_iHp += num;
        if (this.m_iHp > this.m_cMaxHP.GetFinalData())
            this.m_iHp = (int)this.m_cMaxHP.GetFinalData();
    }

    /// <summary>
    /// 增加BBHP
    /// </summary>
    /// <param name="num"></param>
    public void AddBBHP(float num)
    {
        //无BB技能就不增加BBHP
        if (this.m_eBBType == BBType.NONE)
        {
            return;
        }

#if GAME_TEST
        this.m_fBBHP = this.m_iBBMaxHP;
#else
        this.m_fBBHP += num;
        if (this.m_fBBHP > this.m_iBBMaxHP)
            this.m_fBBHP = this.m_iBBMaxHP;
#endif
    }

    /// <summary>
    /// 清除BBHP
    /// </summary>
    public void ClearBBHP()
    {
#if GAME_TEST
        this.m_fBBHP = this.m_iBBMaxHP;
#else
        this.m_fBBHP = 0;
#endif
    }

    /// <summary>
    /// 获取渲染实体
    /// </summary>
    /// <returns></returns>
    public GfxObject GetGfxObject()
    {
        return this.m_cGfxObj;
    }

    /// <summary>
    /// 获取命令控制器
    /// </summary>
    /// <returns></returns>
    public CmdControl GetCmdControl()
    {
        return this.m_cCmdControl;
    }

    /// <summary>
    /// 获取AI控制器
    /// </summary>
    /// <returns></returns>
    public AIControl GetAIControl()
    {
        return this.m_cAI;
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public void Update()
    {
        this.m_cCmdControl.Update();
        this.m_cGfxObj.Update();
    }
}

