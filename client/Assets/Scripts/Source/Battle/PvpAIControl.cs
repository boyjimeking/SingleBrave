using System;
using System.Collections;
using System.Collections.Generic;
using Game.Base;
using UnityEngine;

//  PvpAIControl.cs
//  Author: Lu Zexi
//  2014-02-07



/// <summary>
/// PVP人工智能
/// </summary>
public class PvpAIControl : AIControl
{
    protected IGUIBattle m_cGUIBattle;   //战斗总体类
    protected BattleHero m_cBattleHero;   //AI主体
    private const float AI_COST_TIME = 0F;  //AI间隔时间
    private const float AI_RANDOM_TIME = 1F;  //AI最大随机时间
    private float m_fGoStartTime;   //AI开始选择时间
    private float m_fGoDisTime; //间隔时间

    public PvpAIControl(BattleHero hero, IGUIBattle gui)
        :base()
    {
        this.m_cBattleHero = hero;
        this.m_cGUIBattle = gui;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public override void Initialize()
    {
        base.Initialize();
        if (!this.m_cBattleHero.BUFExist(BATTLE_BUF.MA))
        {
            this.m_cBattleHero.m_iAttackNum = this.m_cBattleHero.m_iAttackMaxNum;
            this.m_cGUIBattle.SetUIHeroAttackNum(this.m_cBattleHero);
        }
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destroy()
    {
        base.Destroy();
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    /// <returns></returns>
    public override bool Update()
    {
        if (this.m_cBattleHero == null)
            return false;

        //当英雄没有指令时
        if (!this.m_cBattleHero.GetCmdControl().Update())
        {
            //当英雄没有攻击次数时
            if (this.m_cBattleHero.m_iAttackNum <= 0 || this.m_cBattleHero.m_bDead)
                return false;

            if (this.m_fGoStartTime <= 0)
            {
                this.m_fGoStartTime = GAME_TIME.TIME_FIXED();
                this.m_fGoDisTime = AI_COST_TIME + GAME_FUNCTION.RANDOM(0, AI_RANDOM_TIME);
            }
            else
            {
                float disTime = GAME_TIME.TIME_FIXED() - this.m_fGoStartTime;
                if (disTime > this.m_fGoDisTime && Time.timeScale >= 1)
                {
                    //有技能释放和无技能释放
                    if (this.m_cBattleHero.m_iBBMaxHP > 0 && this.m_cBattleHero.m_fBBHP >= this.m_cBattleHero.m_iBBMaxHP && !this.m_cBattleHero.BUFExist(BATTLE_BUF.FENGYIN))
                    {
                        this.m_cGUIBattle.GeneratorSkillShow(this.m_cBattleHero);

                        this.m_cBattleHero.m_fBBHP = 0;
                        this.m_cGUIBattle.SetUIHeroBBHP(this.m_cBattleHero);

                        if (this.m_cBattleHero.m_eBBType == BBType.ATTACK)  //BB技能为攻击类型
                        {
                            BattleHero target = null;
                            if (this.m_cBattleHero.m_bSelf)
                                target = this.m_cGUIBattle.GetTargetAuto();
                            else
                                target = this.m_cGUIBattle.GetSelfAuto();

                            BattleHero[] vecTarget = null;
                            if (this.m_cBattleHero.m_bSelf)
                                vecTarget = this.m_cGUIBattle.GetVecEnemy();
                            else
                                vecTarget = this.m_cGUIBattle.GetVecSelf();

                            switch (this.m_cBattleHero.m_eBBMoveType)
                            {
                                case MoveType.None:
                                    switch (this.m_cBattleHero.m_eBBTargetType)
                                    {
                                        case BBTargetType.TargetOne:
                                            this.m_cBattleHero.GetCmdControl().CmdSkill(target);
                                            break;
                                        case BBTargetType.TargetRandom:
                                        case BBTargetType.TargetAll:
                                            this.m_cBattleHero.GetCmdControl().CmdAllSkill(vecTarget);
                                            break;
                                    }
                                    break;
                                case MoveType.Normal:
                                    switch (this.m_cBattleHero.m_eBBTargetType)
                                    {
                                        case BBTargetType.TargetOne:
                                            this.m_cBattleHero.GetCmdControl().CmdMoveSkill(target, this.m_cBattleHero.m_cStartPos, target.m_cAttackPos);
                                            break;
                                        case BBTargetType.TargetRandom:
                                        case BBTargetType.TargetAll:
                                            this.m_cBattleHero.GetCmdControl().CmdMoveAllSkill(vecTarget, this.m_cBattleHero.m_cStartPos, this.m_cGUIBattle.GetBattleBBPos().transform.localPosition);
                                            break;
                                    }
                                    break;
                            }
                        }
                        else if (this.m_cBattleHero.m_eBBType == BBType.RECOVER)  //BB技能为恢复类型
                        {
                            BattleHero[] vecTarget = null;
                            if (this.m_cBattleHero.m_bSelf)
                                vecTarget = this.m_cGUIBattle.GetVecSelf();
                            else
                                vecTarget = this.m_cGUIBattle.GetVecEnemy();

                            this.m_cBattleHero.GetCmdControl().CmdAllSkillRecover(vecTarget);
                        }
                        else if (this.m_cBattleHero.m_eBBType == BBType.BUFF)  //BB技能为BUFF类型
                        {
                            BattleHero[] vecTarget = null;
                            if (this.m_cBattleHero.m_bSelf)
                                vecTarget = this.m_cGUIBattle.GetVecSelf();
                            else
                                vecTarget = this.m_cGUIBattle.GetVecEnemy();

                            this.m_cBattleHero.GetCmdControl().CmdAllSkillBuff(vecTarget);
                        }
                    }
                    else//普通攻击
                    {
                        BattleHero target = null;
                        if (this.m_cBattleHero.m_bSelf)
                            target = this.m_cGUIBattle.GetTargetAuto();
                        else
                            target = this.m_cGUIBattle.GetSelfAuto();

                        if (target == null)
                        {
                            UnityEngine.Debug.LogError(" target is null ");
                        }

                        switch (this.m_cBattleHero.m_eMoveType)
                        {
                            case MoveType.None:
                                this.m_cBattleHero.GetCmdControl().CmdAttackState(target);
                                break;
                            case MoveType.Normal:
                                this.m_cBattleHero.GetCmdControl().CmdMoveAttack(target, this.m_cBattleHero.m_cStartPos, target.m_cAttackPos);
                                break;
                        }
                    }

                    this.m_cBattleHero.m_iAttackNum--;
                    this.m_cGUIBattle.SetUIHeroAttackNum(this.m_cBattleHero);
                    this.m_fGoStartTime = 0;
                }
            }
        }

        return true;
    }
}
