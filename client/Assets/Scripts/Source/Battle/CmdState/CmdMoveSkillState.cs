using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Game.Gfx;
using Game.Base;
using Game.Resource;



//  CmdMOveSkillState.cs
//  Author: Lu Zexi
//  2013-11-29



/// <summary>
/// 命令移动释放单体技能
/// </summary>
public class CmdMoveSkillState : CmdStateBase
{
    private GfxObject m_cTargetObj;      //目标物体
    private BattleHero m_cTargetBattleHero; //目标英雄数据
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
    private GameObject m_cEffectHit;    //击中特效
    private float m_fEffectHitStartTime;    //击中特效开始时间
    private BBSkillTable m_cBBSkillTable; //英雄技能攻击表
    private List<bool> m_lstHitBool;    //是否击中列表
    private BATTLE_BUF m_eHIT_BUFF;   //击中BUFF
    private bool m_bSpark;  //是否spark
    private bool m_bCritical;   //是否暴击

    public CmdMoveSkillState(BattleHero battleHero, IGUIBattle gui)
        : base(battleHero,gui)
    {
        this.m_lstHitBool = new List<bool>();
    }

    /// <summary>
    /// 获取状态类型
    /// </summary>
    /// <returns></returns>
    public override CMD_TYPE GetCmdType()
    {
        return CMD_TYPE.STATE_MOVE_SKILL;
    }

    /// <summary>
    /// 设置目标对象
    /// </summary>
    /// <param name="obj"></param>
    public void Set(BattleHero target, Vector3 start, Vector3 end)
    {
        this.m_cTargetBattleHero = target;
        this.m_cTargetObj = target.GetGfxObject();
        this.m_vecStartPos = start;
        this.m_vecEndPos = end;
    }

    /// <summary>
    /// 进入状态
    /// </summary>
    /// <returns></returns>
    public override bool OnEnter()
    {
        base.OnEnter();
        Vector3 dis = this.m_vecEndPos - this.m_vecStartPos;
        float disTime = dis.magnitude / this.m_cBattleHero.m_fMoveSpeed;
        this.m_cControl.Move(this.m_vecEndPos, disTime);
        this.m_iStep = 0;

        this.m_bSpark = false;
        this.m_bCritical = false;
        this.m_fRandomOne = GAME_FUNCTION.RANDOM_ONE();
        this.m_fAttackStartTime = 0;
        this.m_cEffectSpell = null;
        this.m_fEffectSpellStartTime = 0;
        this.m_cDaoGuang = null;
        this.m_fDaoGuangStartTime = 0;
        this.m_cEffectSkill = null;
        this.m_fEffectSkillStartTime = 0;
        this.m_cEffectHit = null;
        this.m_fEffectHitStartTime = 0;
        this.m_cBBSkillTable = BBSkillTableManager.GetInstance().GetBBSkillTable(this.m_cBattleHero.m_iBBSkillTableID);
        this.m_lstHitBool.Clear();
        for (int i = 0; i < this.m_cBBSkillTable.LstHitTime.Count; i++)
            this.m_lstHitBool.Add(false);

        float hurt = this.m_cBattleHero.m_cAttack.GetFinalData() * this.m_cBattleHero.m_cBBAttack.GetFinalData() - this.m_cTargetBattleHero.m_cDefence.GetFinalData() / 3f;
        if ((this.m_cTargetBattleHero.m_eNature == Nature.Fire && this.m_cBattleHero.m_eNature == Nature.Wood) || (this.m_cTargetBattleHero.m_eNature == Nature.Wood && this.m_cBattleHero.m_eNature == Nature.Thunder)
            || (this.m_cTargetBattleHero.m_eNature == Nature.Thunder && this.m_cBattleHero.m_eNature == Nature.Water) || (this.m_cTargetBattleHero.m_eNature == Nature.Water && this.m_cBattleHero.m_eNature == Nature.Fire)
            )
        {
            hurt = hurt * BATTLE_DEFINE.BENATURE_RATE;
        }
        this.m_cTargetBattleHero.m_iDummyHP = (int)(this.m_cTargetBattleHero.m_iDummyHP - hurt);

        return true;
    }

    /// <summary>
    /// 退出事件
    /// </summary>
    /// <returns></returns>
    public override bool OnExit()
    {
        {   //BUFF
            if (this.m_eHIT_BUFF != BATTLE_BUF.NONE)
            {
                GameObject bufEffect = null;
                switch (this.m_eHIT_BUFF)
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
                    bufEffect.transform.parent = this.m_cTargetObj.GetParent();
                    bufEffect.transform.localPosition = this.m_cTargetBattleHero.m_cStartPos;
                }
                this.m_eHIT_BUFF = BATTLE_BUF.NONE;
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

        if (this.m_cEffectHit != null)
        {
            GameObject.Destroy(this.m_cEffectHit);
        }
        
        this.m_cEffectSpell = null;
        this.m_cDaoGuang = null;
        this.m_cEffectSkill = null;
        this.m_cEffectHit = null;

        return base.OnExit();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Destory();
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

        if (this.m_cEffectHit != null)
        {
            GameObject.Destroy(this.m_cEffectHit);
        }

        this.m_cEffectSpell = null;
        this.m_cDaoGuang = null;
        this.m_cEffectSkill = null;
        this.m_cEffectHit = null;
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
        if (this.m_cEffectHit != null && this.m_fEffectHitStartTime > 0 && GAME_TIME.TIME_FIXED() - this.m_fEffectHitStartTime > this.m_cBBSkillTable.HitEffectTime * GAME_DEFINE.FPS_FIXED)
        {
            GameObject.Destroy(this.m_cEffectHit);
            this.m_cEffectHit = null;
        }

        //伤害判定
        if (this.m_fAttackStartTime > 0)
        {
            for (int i = this.m_lstHitBool.Count - 1; i >= 0; i--)
            {
                float dis = this.m_cBBSkillTable.LstHitTime[i] * GAME_DEFINE.FPS_FIXED;
                //击打N下判定
                if (dis < GAME_TIME.TIME_FIXED() - this.m_fAttackStartTime && !this.m_lstHitBool[i] && this.m_cTargetBattleHero != null && !this.m_cTargetBattleHero.m_bDead)
                {
                    //击中跳伤害
                    BATTLE_BUF buf = BATTLE_FUNCTION.SKILL_HURT(this.m_cBattleHero, this.m_cTargetBattleHero, this.m_cBBSkillTable.LstHitRate[i], this.m_fRandomOne, this.m_cGUIBattle, ref this.m_bSpark, ref this.m_bCritical, this.m_bCanDrop);
                    if (buf != BATTLE_BUF.NONE)
                        this.m_eHIT_BUFF = buf;

                    this.m_cTargetBattleHero.m_vecHitStartTime[this.m_cBattleHero.m_iIndex] = this.m_fAttackStartTime + this.m_cBBSkillTable.LstHitTime[i] * GAME_DEFINE.FPS_FIXED;
                    this.m_cTargetBattleHero.m_vecHitEndTime[this.m_cBattleHero.m_iIndex] = this.m_fAttackStartTime + this.m_cBBSkillTable.LstHitEndTime[i] * GAME_DEFINE.FPS_FIXED;
                    this.m_lstHitBool[i] = true;
                    break;
                }
            }
        }

        //技能释放状态判定
        if (this.m_cControl.GetCurrentState().GetStateType() != STATE_TYPE.STATE_MOVE && this.m_iStep == 0)
        {
            this.m_iStep++;
            this.m_cControl.Skill();

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
                Debug.Log("skill effect ");
                this.m_cEffectSkill = GameObject.Instantiate(effectRes) as GameObject;
                this.m_cEffectSkill.transform.parent = this.m_cObj.GetParent();
                this.m_cEffectSkill.transform.localPosition = this.m_cTargetObj.GetLocalPos();
                this.m_cEffectSkill.transform.localScale = this.m_cObj.GetLocalScale();
                this.m_fEffectSkillStartTime = GAME_TIME.TIME_FIXED();
            }

            //击中特效
            effectRes = null;
            if (GAME_SETTING.s_bSKEffectSwitch)
                effectRes = this.m_cBattleHero.m_cResSkillHit;
            if (effectRes != null)
            {
                this.m_cEffectHit = GameObject.Instantiate(effectRes) as GameObject;
                this.m_cEffectHit.transform.parent = this.m_cObj.GetParent();
                this.m_cEffectHit.transform.localPosition = this.m_cTargetObj.GetLocalPos();
                this.m_cEffectHit.transform.localScale = this.m_cObj.GetLocalScale();
                this.m_fEffectHitStartTime = GAME_TIME.TIME_FIXED();
            }
        }

        //返回状态判定
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
            if (this.m_cDaoGuang == null && this.m_cEffectHit == null && this.m_cEffectSkill == null && this.m_cEffectSpell == null && finish)
            {
                this.m_iStep++;
                this.m_cControl.Idle();
                return false;
            }
        }

        return true;
    }
}
