using System;
using System.Collections.Generic;
using System.Collections;
using Game.Gfx;
using UnityEngine;


//  CmdControl.cs
//  Author: Lu Zexi
//  2013-11-29



/// <summary>
/// 命令控制
/// </summary>
public class CmdControl
{
    private BattleHero m_cBattleHero;   //战场英雄
    private CmdStateBase m_cState;  //当前状态
    private CmdStateWrap m_cStateWrap;  //命令状态包
    private bool m_bCanDrop;    //是否支持掉落

    /// <summary>
    /// 命令状态包
    /// </summary>
    private class CmdStateWrap
    {
        public CmdAllSkillBuffState m_cAllSkillBuff;   //全体BUFF技能回复
        public CmdAllSkillRecoverState m_cAllSkillRecover;  //全体回复技能
        public CmdAllSkillState m_cAllSkill;       //不移动全体技能
        public CmdAttackState m_cAttack;       //不移动攻击
        public CmdDefenceState m_cDefence;       //防御
        public CmdMoveAllSkillState m_cMoveAllSkill;       //移动全体技能
        public CmdMoveAttackState m_cMoveAttack;       //移动攻击
        public CmdMoveSkillState m_cMoveSkill;       //移动单体技能
        public CmdSkillState m_cSkill;       //不移动单体技能
        public CmdDieState m_cDie;  //死亡命令


        public CmdStateWrap(BattleHero hero, IGUIBattle gui)
        {
            this.m_cAllSkillBuff = new CmdAllSkillBuffState(hero, gui);
            this.m_cAllSkillRecover = new CmdAllSkillRecoverState(hero, gui);
            this.m_cAllSkill = new CmdAllSkillState(hero,gui);
            this.m_cAttack = new CmdAttackState(hero, gui);
            this.m_cDefence = new CmdDefenceState(hero, gui);
            this.m_cMoveAllSkill = new CmdMoveAllSkillState(hero, gui);
            this.m_cMoveAttack = new CmdMoveAttackState(hero, gui);
            this.m_cMoveSkill = new CmdMoveSkillState(hero, gui);
            this.m_cSkill = new CmdSkillState(hero, gui);
            this.m_cDie = new CmdDieState(hero, gui);
        }
    }

    public CmdControl(BattleHero hero, IGUIBattle gui , bool canDrop )
    {
        this.m_bCanDrop = canDrop;
        this.m_cBattleHero = hero;
        this.m_cState = null;
        this.m_cStateWrap = new CmdStateWrap(hero, gui);
    }

    /// <summary>
    /// 获取命令状态
    /// </summary>
    /// <returns></returns>
    public CMD_TYPE GetCmdType()
    {
        if (this.m_cState == null)
            return CMD_TYPE.STATE_NONE;
        return this.m_cState.GetCmdType();
    }

    /// <summary>
    /// 死亡命令
    /// </summary>
    public void CmdDie()
    {
        if (this.m_cState != null)
            this.m_cState.OnExit();
        this.m_cState = this.m_cStateWrap.m_cDie;
        this.m_cState.OnEnter();
    }

    /// <summary>
    /// 全体BUFF
    /// </summary>
    /// <param name="vec"></param>
    public void CmdAllSkillBuff(BattleHero[] vec)
    {
        if (this.m_cState != null)
            this.m_cState.OnExit();
        this.m_cStateWrap.m_cAllSkillBuff.Set(vec);
        this.m_cStateWrap.m_cAllSkillBuff.SetDrop(this.m_bCanDrop);
        this.m_cState = this.m_cStateWrap.m_cAllSkillBuff;
        this.m_cState.OnEnter();
    }

    /// <summary>
    /// 全体回复技能
    /// </summary>
    /// <param name="vec"></param>
    public void CmdAllSkillRecover(BattleHero[] vec)
    {
        if (this.m_cState != null)
            this.m_cState.OnExit();
        this.m_cStateWrap.m_cAllSkillRecover.Set(vec);
        this.m_cStateWrap.m_cAllSkillRecover.SetDrop(this.m_bCanDrop);
        this.m_cState = this.m_cStateWrap.m_cAllSkillRecover;
        this.m_cState.OnEnter();
    }

    /// <summary>
    /// 不移动全体技能命令
    /// </summary>
    public void CmdAllSkill( BattleHero[] vec )
    {
        if (this.m_cState != null)
            this.m_cState.OnExit();
        this.m_cStateWrap.m_cAllSkill.Set(vec);
        this.m_cStateWrap.m_cAllSkill.SetDrop(this.m_bCanDrop);
        this.m_cState = this.m_cStateWrap.m_cAllSkill;
        this.m_cState.OnEnter();
    }

    /// <summary>
    /// 不移动攻击命令
    /// </summary>
    public void CmdAttackState( BattleHero target )
    {
        if (this.m_cState != null)
            this.m_cState.OnExit();
        this.m_cStateWrap.m_cAttack.Set(target);
        this.m_cStateWrap.m_cAttack.SetDrop(this.m_bCanDrop);
        this.m_cState = this.m_cStateWrap.m_cAttack;
        this.m_cState.OnEnter();
    }

    /// <summary>
    /// 防御命令
    /// </summary>
    public void CmdDefence()
    {
        if (this.m_cState != null)
            this.m_cState.OnExit();
        this.m_cState = this.m_cStateWrap.m_cDefence;
        this.m_cState.OnEnter();
    }

    /// <summary>
    /// 移动全体技能命令
    /// </summary>
    public void CmdMoveAllSkill( BattleHero[] vec , Vector3 start , Vector3 end )
    {
        if (this.m_cState != null)
            this.m_cState.OnExit();
        this.m_cStateWrap.m_cMoveAllSkill.Set(vec, start, end);
        this.m_cStateWrap.m_cMoveAllSkill.SetDrop(this.m_bCanDrop);
        this.m_cState = this.m_cStateWrap.m_cMoveAllSkill;
        this.m_cState.OnEnter();
    }

    /// <summary>
    /// 移动攻击命令
    /// </summary>
    public void CmdMoveAttack( BattleHero target , Vector3 start , Vector3 end )
    {
        if (this.m_cState != null)
            this.m_cState.OnExit();
        this.m_cStateWrap.m_cMoveAttack.Set(target, start, end);
        this.m_cStateWrap.m_cMoveAttack.SetDrop(this.m_bCanDrop);
        this.m_cState = this.m_cStateWrap.m_cMoveAttack;
        this.m_cState.OnEnter();
    }

    /// <summary>
    /// 移动单体技能命令
    /// </summary>
    public void CmdMoveSkill( BattleHero target , Vector3 start , Vector3 end )
    {
        if (this.m_cState != null)
            this.m_cState.OnExit();
        this.m_cStateWrap.m_cMoveSkill.Set(target, start, end);
        this.m_cStateWrap.m_cMoveSkill.SetDrop(this.m_bCanDrop);
        this.m_cState = this.m_cStateWrap.m_cMoveSkill;
        this.m_cState.OnEnter();
    }

    /// <summary>
    /// 不移动单体技能命令
    /// </summary>
    public void CmdSkill( BattleHero target )
    {
        if (this.m_cState != null)
            this.m_cState.OnExit();
        this.m_cStateWrap.m_cSkill.Set(target);
        this.m_cStateWrap.m_cSkill.SetDrop(this.m_bCanDrop);
        this.m_cState = this.m_cStateWrap.m_cSkill;
        this.m_cState.OnEnter();
    }


    /// <summary>
    /// 逻辑更新
    /// </summary>
    /// <returns></returns>
    public bool Update()
    {
        if (this.m_cState == null)
            return false;
        if (!this.m_cState.Update())
        {
            if (this.m_cState != null)
                this.m_cState.OnExit();
            this.m_cState = null;
            return false;
        }
        //回来时判定为结束
        if (this.m_cBattleHero != null && this.m_cBattleHero.GetGfxObject() != null && !this.m_cBattleHero.m_bDead && this.m_cBattleHero.GetGfxObject().GetStateControl().GetCurrentState().GetStateType() == STATE_TYPE.STATE_MOVE_BACK)
            return false;
        return true;
    }

}
