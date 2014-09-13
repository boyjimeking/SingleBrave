
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Game.Gfx;
using Game.Resource;
using Game.Base;




//  CmdAllSkillRecoverState.cs
//  Author: Lu Zexi
//  2013-12-23



/// <summary>
/// 全体恢复
/// </summary>
public class CmdAllSkillRecoverState : CmdStateBase
{
    private BattleHero[] m_vecTargetHero;   //目标列表
    private GfxObject[] m_vecTargetGfx; //目标实体

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

    public CmdAllSkillRecoverState(BattleHero hero, IGUIBattle gui)
        : base(hero , gui)
    {
        this.m_lstEffectHit = new List<GameObject>();
        this.m_lstHitBool = new List<bool>();
    }

    /// <summary>
    /// 获取命令状态
    /// </summary>
    /// <returns></returns>
    public override CMD_TYPE GetCmdType()
    {
        return CMD_TYPE.STATE_ALL_SKILL;
    }

    /// <summary>
    /// 设置参数
    /// </summary>
    /// <param name="heros"></param>
    public void Set(BattleHero[] heros)
    {
        this.m_vecTargetHero = heros;
        this.m_vecTargetGfx = new GfxObject[heros.Length];
        for (int i = 0; i < heros.Length ; i++)
        {
            if (this.m_vecTargetHero[i] != null )
            {
                this.m_vecTargetGfx[i] = this.m_vecTargetHero[i].GetGfxObject();
            }
        }
    }

    /// <summary>
    /// 进入事件
    /// </summary>
    /// <returns></returns>
    public override bool OnEnter()
    {
        this.m_cControl.Skill();
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
        if (effectRes != null)
        {
            for (int i = 0; i < this.m_vecTargetHero.Length; i++)
            {
                if (this.m_vecTargetHero[i] != null && !this.m_vecTargetHero[i].m_bDead)
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

        return base.OnEnter();
    }

    /// <summary>
    /// 退出事件
    /// </summary>
    /// <returns></returns>
    public override bool OnExit()
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

        return base.OnExit();
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    /// <returns></returns>
    public override bool Update()
    {
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
                    //击中跳伤害
                    for (int j = 0; j < this.m_vecTargetHero.Length; j++)
                    {
                        if (this.m_vecTargetHero[j] != null && !this.m_vecTargetHero[j].m_bDead)
                        {
                            BATTLE_FUNCTION.SKILL_RECOVER(this.m_cBattleHero, this.m_vecTargetHero[j], this.m_cBBSkillTable.LstHitRate[i], this.m_cGUIBattle);
                        }
                    }
                    this.m_lstHitBool[i] = true;
                    break;
                }
            }
        }

        if (this.m_cControl.GetCurrentState().GetStateType() != STATE_TYPE.STATE_SKILL && this.m_iStep == 0)
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
                return false;
            }
        }
        return true;
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
    }
}
