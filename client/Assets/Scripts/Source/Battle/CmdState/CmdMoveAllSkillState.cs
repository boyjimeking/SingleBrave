using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Gfx;
using Game.Resource;
using Game.Base;



//  CmdMoveAllSkillState.cs
//  Author: Lu Zexi
//  2013-11-29



/// <summary>
/// 移动全体技能命令状态
/// </summary>
public class CmdMoveAllSkillState : CmdStateBase
{
    private BattleHero[] m_vecTargetHero;   //目标列表
    private GfxObject[] m_vecTargetGfx; //目标实体
    private Vector3 m_vecStartPos;  //起始位置
    private Vector3 m_vecEndPos;    //攻击结束位置

    private float m_fRandomOne; //暴击随机数
    private float m_fAttackStartTime;   //攻击开始时间

    private GameObject m_cEffectSpell;  //施法特效
    private float m_fEffectSpellStartTime;  //施法特效开始时间
    private GameObject m_cDaoGuang;   //刀光特效
    private float m_fDaoGuangStartTime; //刀光持续时间
    private GameObject m_cEffectSkill;    //击中特效
    private float m_fEffectSkillStartTime;    //击中特效开始时间
    private List<GameObject> m_lstEffectHit;    //击中特效列表
    private float m_fEffectHitStartTime;    //击中特效开始时间
    private BBSkillTable m_cBBSkillTable; //英雄技能攻击表
    private List<bool> m_lstHitBool;    //是否击中列表
    private BATTLE_BUF[] m_eHIT_BUFF;   //击中BUFF
    private bool[] m_vecSpark;  //是否Spark
    private bool[] m_vecCritical;   //是否暴击

    public CmdMoveAllSkillState(BattleHero hero, IGUIBattle gui)
        : base(hero, gui)
    {
        this.m_lstHitBool = new List<bool>();
        this.m_lstEffectHit = new List<GameObject>();
        this.m_eHIT_BUFF = new BATTLE_BUF[6];
        this.m_vecSpark = new bool[6];
        this.m_vecCritical = new bool[6];
    }

    /// <summary>
    /// 获取命令类型
    /// </summary>
    /// <returns></returns>
    public override CMD_TYPE GetCmdType()
    {
        return CMD_TYPE.STATE_MOVE_ALL_SKILL;
    }

    /// <summary>
    /// 设置参数
    /// </summary>
    /// <param name="heros"></param>
    public void Set(BattleHero[] heros , Vector3 start , Vector3 end )
    {
        this.m_vecStartPos = start;
        this.m_vecEndPos = end;
        this.m_vecTargetHero = heros;
        this.m_vecTargetGfx = new GfxObject[heros.Length];
        for (int i = 0; i < this.m_vecTargetHero.Length; i++)
        {
            if (this.m_vecTargetHero[i] != null)
                this.m_vecTargetGfx[i] = this.m_vecTargetHero[i].GetGfxObject();
        }
    }

    /// <summary>
    /// 进入事件
    /// </summary>
    /// <returns></returns>
    public override bool OnEnter()
    {
        Vector3 dis = this.m_vecEndPos - this.m_vecStartPos;
        float disTime = dis.magnitude / this.m_cBattleHero.m_fMoveSpeed;
        this.m_cControl.Move(this.m_vecEndPos, disTime);

        for (int i = 0; i < this.m_eHIT_BUFF.Length;i++ )
        {
            this.m_eHIT_BUFF[i] = BATTLE_BUF.NONE;
        }
        for (int i = 0; i < this.m_vecSpark.Length; i++)
        {
            this.m_vecSpark[i] = false;
            this.m_vecCritical[i] = false;
        }
        this.m_iStep = 0;
        this.m_fRandomOne = GAME_FUNCTION.RANDOM_ONE();
        this.m_fAttackStartTime = 0;
        this.m_cEffectSpell = null;
        this.m_fEffectSpellStartTime = 0;
        this.m_cDaoGuang = null;
        this.m_fDaoGuangStartTime = 0;
        this.m_cEffectSkill = null;
        this.m_fEffectSkillStartTime = 0;
        this.m_lstEffectHit.Clear();
        this.m_fEffectHitStartTime = 0;
        this.m_cBBSkillTable = BBSkillTableManager.GetInstance().GetBBSkillTable(this.m_cBattleHero.m_iBBSkillTableID);
        this.m_lstHitBool.Clear();
        for (int i = 0; i < this.m_cBBSkillTable.LstHitTime.Count; i++)
            this.m_lstHitBool.Add(false);

        for (int i = 0; i < this.m_vecTargetHero.Length; i++)
        {
            if (this.m_vecTargetHero[i] != null)
            {
                float hurt = this.m_cBattleHero.m_cAttack.GetFinalData() * this.m_cBattleHero.m_cBBAttack.GetFinalData() - this.m_vecTargetHero[i].m_cDefence.GetFinalData() / 3f;
                if ((this.m_vecTargetHero[i].m_eNature == Nature.Fire && this.m_cBattleHero.m_eNature == Nature.Wood) || (this.m_vecTargetHero[i].m_eNature == Nature.Wood && this.m_cBattleHero.m_eNature == Nature.Thunder)
                    || (this.m_vecTargetHero[i].m_eNature == Nature.Thunder && this.m_cBattleHero.m_eNature == Nature.Water) || (this.m_vecTargetHero[i].m_eNature == Nature.Water && this.m_cBattleHero.m_eNature == Nature.Fire)
                    )
                {
                    hurt = hurt * BATTLE_DEFINE.BENATURE_RATE;
                }
                this.m_vecTargetHero[i].m_iDummyHP = (int)(this.m_vecTargetHero[i].m_iDummyHP - hurt);
            }
        }

        return base.OnEnter();
    }

    /// <summary>
    /// 退出事件
    /// </summary>
    /// <returns></returns>
    public override bool OnExit()
    {
        {   //BUFF
            for (int i = 0; i < this.m_eHIT_BUFF.Length; i++)
            {
                if (this.m_eHIT_BUFF[i] != BATTLE_BUF.NONE)
                {
                    GameObject bufEffect = null;
                    switch (this.m_eHIT_BUFF[i])
                    {
                        case BATTLE_BUF.DU: //毒
                            bufEffect = GameObject.Instantiate(this.m_cGUIBattle.m_cResDebuffDu) as GameObject;
                            break;
                        case BATTLE_BUF.MA: //麻痹
                            bufEffect = GameObject.Instantiate(this.m_cGUIBattle.m_cResDebuffMa) as GameObject;
                            break;
                        case BATTLE_BUF.FENGYIN:    //封印
                            bufEffect = GameObject.Instantiate(this.m_cGUIBattle.m_cResDebuffFengyin) as GameObject;
                            break;
                        case BATTLE_BUF.XURUO:  //虚弱
                            bufEffect = GameObject.Instantiate(this.m_cGUIBattle.m_cResDebuffXuruo) as GameObject;
                            break;
                        case BATTLE_BUF.POJIA:  //破甲
                            bufEffect = GameObject.Instantiate(this.m_cGUIBattle.m_cResDebuffPojia) as GameObject;
                            break;
                        case BATTLE_BUF.POREN:  //破刃
                            bufEffect = GameObject.Instantiate(this.m_cGUIBattle.m_cResDebuffPoren) as GameObject;
                            break;
                    }

                    if (bufEffect != null)
                    {
                        bufEffect.transform.parent = this.m_vecTargetGfx[i].GetParent();
                        bufEffect.transform.localPosition = this.m_vecTargetHero[i].m_cStartPos;
                    }
                    this.m_eHIT_BUFF[i] = BATTLE_BUF.NONE;
                }
            }
        }

        if (this.m_cEffectSkill != null)
        {
            GameObject.Destroy(this.m_cEffectSkill);
        }
        if (this.m_cDaoGuang != null)
        {
            GameObject.Destroy(this.m_cDaoGuang);
        }
        if (this.m_cEffectSpell != null)
        {
            GameObject.Destroy(this.m_cEffectSpell);
        }
        this.m_cEffectSpell = null;
        this.m_cDaoGuang = null;
        this.m_cEffectSkill = null;

        foreach (GameObject item in this.m_lstEffectHit)
        {
            if (item != null)
            {
                GameObject.Destroy(item);
            }
        }
        this.m_lstEffectHit.Clear();

        return base.OnExit();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        if (this.m_cEffectSkill != null)
        {
            GameObject.Destroy(this.m_cEffectSkill);
        }
        if (this.m_cDaoGuang != null)
        {
            GameObject.Destroy(this.m_cDaoGuang);
        }
        if (this.m_cEffectSpell != null)
        {
            GameObject.Destroy(this.m_cEffectSpell);
        }

        this.m_cEffectSpell = null;
        this.m_cDaoGuang = null;
        this.m_cEffectSkill = null;

        foreach (GameObject item in this.m_lstEffectHit)
        {
            if (item != null)
            {
                GameObject.Destroy(item);
            }
        }
        this.m_lstEffectHit.Clear();

        base.Destory();
    }


    /// <summary>
    /// 逻辑更新
    /// </summary>
    /// <returns></returns>
    public override bool Update()
    {
        if (this.m_cControl.GetCurrentState() == null)
            return false;

        //施法特效判断销毁
        if (this.m_cEffectSpell != null && this.m_fEffectSpellStartTime > 0 && GAME_TIME.TIME_FIXED() - this.m_fEffectSpellStartTime > this.m_cBBSkillTable.SpellEffectTime * GAME_DEFINE.FPS_FIXED)
        {
            GameObject.Destroy(this.m_cEffectSpell);
            this.m_cEffectSpell = null;
        }

        //刀光特效判断销毁
        if (this.m_cDaoGuang != null && this.m_fDaoGuangStartTime > 0 && GAME_TIME.TIME_FIXED() - this.m_fDaoGuangStartTime > this.m_cBBSkillTable.DaoGuangTime * GAME_DEFINE.FPS_FIXED)
        {
            GameObject.Destroy(this.m_cDaoGuang);
            this.m_cDaoGuang = null;
        }

        //技能特效判断销毁
        if (this.m_cEffectSkill != null && this.m_fEffectSkillStartTime > 0 && GAME_TIME.TIME_FIXED() - this.m_fEffectSkillStartTime > this.m_cBBSkillTable.SkillEffectTime * GAME_DEFINE.FPS_FIXED)
        {
            GameObject.Destroy(this.m_cEffectSkill);
            this.m_cEffectSkill = null;
        }

        //击中特效销毁判定
        if (this.m_lstEffectHit.Count > 0 && this.m_fEffectHitStartTime > 0 && GAME_TIME.TIME_FIXED() - this.m_fEffectHitStartTime > this.m_cBBSkillTable.HitEffectTime * GAME_DEFINE.FPS_FIXED)
        {
            foreach (GameObject item in this.m_lstEffectHit)
            {
                GameObject.Destroy(item);
            }
            this.m_lstEffectHit.Clear();
        }

        //伤害判定
        if (this.m_fAttackStartTime > 0)
        {
            for (int i = this.m_lstHitBool.Count - 1; i >= 0; i--)
            {
                float dis = this.m_cBBSkillTable.LstHitTime[i] * GAME_DEFINE.FPS_FIXED;
                //击打N下判定
                if (dis < GAME_TIME.TIME_FIXED() - this.m_fAttackStartTime && !this.m_lstHitBool[i])
                {
                    if (this.m_cBBSkillTable.TargetType == BBTargetType.TargetRandom)
                    {
                        int index = GAME_FUNCTION.RANDOM(0, this.m_vecTargetHero.Length);
                        for (int j = 0; j < this.m_vecTargetHero.Length; j++)
                        {
                            if (this.m_vecTargetHero[index] != null && !this.m_vecTargetHero[index].m_bDead)
                            {
                                break;
                            }
                            index = (index + 1) % this.m_vecTargetHero.Length;
                        }
                        BATTLE_BUF buf = BATTLE_FUNCTION.SKILL_HURT(this.m_cBattleHero, this.m_vecTargetHero[index], this.m_cBBSkillTable.LstHitRate[i], this.m_fRandomOne, this.m_cGUIBattle, ref this.m_vecSpark[index], ref this.m_vecCritical[index], this.m_bCanDrop);
                        if (buf != BATTLE_BUF.NONE)
                            this.m_eHIT_BUFF[index] = buf;

                        this.m_vecTargetHero[index].m_vecHitStartTime[this.m_cBattleHero.m_iIndex] = this.m_fAttackStartTime + this.m_cBBSkillTable.LstHitTime[i] * GAME_DEFINE.FPS_FIXED;
                        this.m_vecTargetHero[index].m_vecHitEndTime[this.m_cBattleHero.m_iIndex] = this.m_fAttackStartTime + this.m_cBBSkillTable.LstHitEndTime[i] * GAME_DEFINE.FPS_FIXED;

                        //随机选择对象释放特效
                    }
                    else
                    {
                        //击中跳伤害
                        for (int j = 0; j < this.m_vecTargetHero.Length; j++)
                        {
                            if (this.m_vecTargetHero[j] != null && !this.m_vecTargetHero[j].m_bDead)
                            {
                                BATTLE_BUF buf = BATTLE_FUNCTION.SKILL_HURT(this.m_cBattleHero, this.m_vecTargetHero[j], this.m_cBBSkillTable.LstHitRate[i], this.m_fRandomOne, this.m_cGUIBattle, ref this.m_vecSpark[j], ref this.m_vecCritical[j], this.m_bCanDrop);
                                if (buf != BATTLE_BUF.NONE)
                                    this.m_eHIT_BUFF[j] = buf;

                                this.m_vecTargetHero[j].m_vecHitStartTime[this.m_cBattleHero.m_iIndex] = this.m_fAttackStartTime + this.m_cBBSkillTable.LstHitTime[i] * GAME_DEFINE.FPS_FIXED;
                                this.m_vecTargetHero[j].m_vecHitEndTime[this.m_cBattleHero.m_iIndex] = this.m_fAttackStartTime + this.m_cBBSkillTable.LstHitEndTime[i] * GAME_DEFINE.FPS_FIXED;
                            }
                        }
                    }

                    this.m_lstHitBool[i] = true;
                    break;
                }
            }
        }

        //攻击状态判定
        if (this.m_cControl.GetCurrentState().GetStateType() != STATE_TYPE.STATE_MOVE && this.m_iStep == 0)
        {
            this.m_iStep++;

            this.m_fAttackStartTime = GAME_TIME.TIME_FIXED();

            //施法特效
            Object effectRes = this.m_cBattleHero.m_cResSklillSpell;
            if (effectRes != null)
            {
                this.m_cEffectSpell = GameObject.Instantiate(effectRes) as GameObject;
                this.m_cEffectSpell.transform.parent = this.m_cObj.GetParent();
                this.m_cEffectSpell.transform.localPosition = this.m_cObj.GetLocalPos();
                this.m_cEffectSpell.transform.localScale = this.m_cObj.GetLocalScale();
                this.m_fEffectSpellStartTime = GAME_TIME.TIME_FIXED();
            }

            //刀光
            effectRes = null;
            if (GAME_SETTING.s_bSKEffectSwitch)
                effectRes = this.m_cBattleHero.m_cResSkillDaoGuang;
            if (effectRes != null)
            {
                this.m_cDaoGuang = GameObject.Instantiate(effectRes) as GameObject;
                this.m_cDaoGuang.transform.parent = this.m_cObj.GetParent();
                this.m_cDaoGuang.transform.localPosition = this.m_cObj.GetLocalPos();
                this.m_cDaoGuang.transform.localScale = this.m_cObj.GetLocalScale();
                this.m_fDaoGuangStartTime = GAME_TIME.TIME_FIXED();
            }

            //技能特效
            effectRes = null;
            if (GAME_SETTING.s_bSKEffectSwitch)
                effectRes = this.m_cBattleHero.m_cResSkill;
            if (effectRes != null)
            {
                this.m_cEffectSkill = GameObject.Instantiate(effectRes) as GameObject;
                this.m_cEffectSkill.transform.parent = this.m_cObj.GetParent();
                this.m_cEffectSkill.transform.localPosition = Vector3.zero;
                this.m_cEffectSkill.transform.localScale = this.m_cObj.GetLocalScale();
                this.m_fEffectSkillStartTime = GAME_TIME.TIME_FIXED();
            }

            //击中特效
            effectRes = null;
            if (GAME_SETTING.s_bSKEffectSwitch)
                effectRes = this.m_cBattleHero.m_cResSkillHit;
            if (effectRes != null && this.m_cBBSkillTable.TargetType == BBTargetType.TargetAll)
            {
                for (int i = 0; i < this.m_vecTargetHero.Length; i++)
                { 
                    if( this.m_vecTargetHero[i]!= null && !this.m_vecTargetHero[i].m_bDead )
                    {
                        GameObject tmpEffect = GameObject.Instantiate(effectRes) as GameObject;
                        tmpEffect.transform.parent = this.m_cObj.GetParent();
                        tmpEffect.transform.localPosition = this.m_vecTargetGfx[i].GetLocalPos();
                        tmpEffect.transform.localScale = this.m_cObj.GetLocalScale();
                        this.m_lstEffectHit.Add(tmpEffect);
                        this.m_fEffectHitStartTime = GAME_TIME.TIME_FIXED();
                    }
                }
            }

            this.m_cControl.Skill();
        }

        //移动返回状态
        if (this.m_cControl.GetCurrentState().GetStateType() != STATE_TYPE.STATE_SKILL && this.m_iStep == 1)
        {
            this.m_iStep++;
            Vector3 dis = this.m_vecEndPos - this.m_vecStartPos;
            float disTime = dis.magnitude / BattleHero.MOVE_BACK_SPEED;
            this.m_cControl.MoveBack(this.m_vecStartPos, disTime);
        }

        //结束判定
        if (this.m_cControl.GetCurrentState().GetStateType() != STATE_TYPE.STATE_MOVE_BACK && this.m_iStep == 2)
        {
            //判定击中伤害结束
            bool finish = true;
            foreach (bool item in this.m_lstHitBool)
            {
                if (!item)
                {
                    finish = false;
                }
            }
            if (this.m_cDaoGuang == null && this.m_cEffectSkill == null && this.m_cEffectSpell == null && this.m_lstEffectHit.Count <= 0 && finish)
            {
                this.m_iStep++;
                this.m_cControl.Idle();
                return false;
            }
        }
        return true;
    }
}

