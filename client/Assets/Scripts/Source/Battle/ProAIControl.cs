using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;


//  ProAIControl.cs
//  Author: Lu Zexi
//  2014-01-15



/// <summary>
/// 专业AI控制类
/// </summary>
public class ProAIControl : AIControl
{
    protected IGUIBattle m_cGUIBattle;   //战斗总体类
    protected BattleHero m_cBattleHero;   //AI主体
    private const float AI_COST_TIME = 0F;  //AI间隔时间
    private const float AI_RANDOM_TIME = 0.5F;  //AI最大随机时间
    private float m_fGoStartTime;   //AI开始选择时间
    private float m_fGoDisTime; //间隔时间
    private AITable m_cAITable; //AI表
    private int m_iNowAttackNum;    //当前攻击次数
    private int m_iBBConditionStartRound;   //BB技能条件开始时间

    public ProAIControl(BattleHero hero, IGUIBattle gui, AITable ai)
        :base()
    {
        this.m_cBattleHero = hero;
        this.m_cGUIBattle = gui;
        this.m_cAITable = ai;
        this.m_iBBConditionStartRound = 0;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public override void Initialize()
    {
        base.Initialize();
        if (!this.m_cBattleHero.BUFExist(BATTLE_BUF.MA))
        {
            int atNum = GAME_FUNCTION.RANDOM(this.m_cAITable.MinAttackNum, this.m_cAITable.MaxAttackNum + 1);
            this.m_cBattleHero.m_iAttackNum = atNum;
            this.m_cBattleHero.m_iAttackMaxNum = atNum;
            this.m_iNowAttackNum = 0;
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
                if (disTime > this.m_fGoDisTime)
                {
                    bool bbSkill = true;
                    bool exist = false;

                    if (this.m_cBattleHero.BUFExist(BATTLE_BUF.FENGYIN))
                    {
                        bbSkill = false;
                    }

                    switch (this.m_cAITable.VecBBCondition[0])
                    {
                        case AIBB_CONDITION.SMALL_HP: //HP小于N百分比
                            exist = true;
                            if (this.m_cBattleHero.m_iHp * 1f / this.m_cBattleHero.m_cMaxHP.GetFinalData() > this.m_cAITable.VecBBArg[0])
                            {
                                bbSkill = false;
                            }
                            break;
                        case AIBB_CONDITION.ROUND_AFTER:    //多少回合后触发
                            exist = true;
                            if ((this.m_cGUIBattle.GetRoundNum() < this.m_cAITable.VecBBArg[0]))
                            {
                                bbSkill = false;
                            }
                            break;
                    }

                    if (this.m_iBBConditionStartRound <= 0 && bbSkill && exist)
                    {
                        this.m_iBBConditionStartRound = this.m_cGUIBattle.GetRoundNum() - 1;
                    }

                    exist = false;
                    if (bbSkill)
                    {
                        switch (this.m_cAITable.VecBBCondition[1])
                        {
                            case AIBB_CONDITION.ATTACK_NUM: //N回合攻击时触发
                                exist = true;
                                if (((this.m_cGUIBattle.GetRoundNum() - 1 - this.m_iBBConditionStartRound) % this.m_cAITable.VecBBArg[1]) != 0 || this.m_iNowAttackNum != 0)
                                {
                                    bbSkill = false;
                                }
                                break;
                            case AIBB_CONDITION.RATE: //N概率
                                exist = true;
                                if (GAME_FUNCTION.RANDOM_ONE() > this.m_cAITable.VecBBArg[1])
                                {
                                    bbSkill = false;
                                }
                                break;
                        }
                    }

                    if (!exist && bbSkill)  //不存在BB AI
                    {
                        bbSkill = false;
                    }

                    if (!bbSkill)
                    {
                        BattleHero target = this.m_cGUIBattle.GetSelfAuto(); ;
                        switch (this.m_cAITable.TargetType)
                        {
                            case AITARGET.RANDOM: //随机目标
                                target = this.m_cGUIBattle.GetSelfAuto();
                                break;
                            case AITARGET.MIN_HP: //N概率最小生命
                                if (GAME_FUNCTION.RANDOM_ONE() <= this.m_cAITable.TargetArg)
                                {
                                    target = this.m_cGUIBattle.GetMinHPSelf();
                                }
                                break;
                            case AITARGET.MAX_HP: //N概率最大生命
                                if (GAME_FUNCTION.RANDOM_ONE() <= this.m_cAITable.TargetArg)
                                {
                                    target = this.m_cGUIBattle.GetMaxHPSelf();
                                }
                                break;
                        }

                        if (target != null)
                        {
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
                    }
                    else
                    {
                        BattleHero target = this.m_cGUIBattle.GetSelfAuto();
                        switch (this.m_cAITable.TargetType)
                        {
                            case AITARGET.RANDOM: //随机目标
                                target = this.m_cGUIBattle.GetSelfAuto();
                                break;
                            case AITARGET.MIN_HP: //N概率最小生命
                                if (GAME_FUNCTION.RANDOM_ONE() <= this.m_cAITable.TargetArg)
                                {
                                    target = this.m_cGUIBattle.GetMinHPSelf();
                                }
                                break;
                            case AITARGET.MAX_HP: //N概率最大生命
                                if (GAME_FUNCTION.RANDOM_ONE() <= this.m_cAITable.TargetArg)
                                {
                                    target = this.m_cGUIBattle.GetMaxHPSelf();
                                }
                                break;
                        }

                        if (this.m_cBattleHero.m_eBBType == BBType.ATTACK && target != null )//攻击
                        {

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
                                            this.m_cBattleHero.GetCmdControl().CmdAllSkill(this.m_cGUIBattle.GetVecSelf());
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
                                            this.m_cBattleHero.GetCmdControl().CmdMoveAllSkill(this.m_cGUIBattle.GetVecSelf(), this.m_cBattleHero.m_cStartPos, this.m_cGUIBattle.GetBattleBBPos().transform.localPosition);
                                            break;
                                    }
                                    break;
                            }
                        }
                        else if (this.m_cBattleHero.m_eBBType == BBType.RECOVER)  //恢复
                        {
                            this.m_cBattleHero.GetCmdControl().CmdAllSkillRecover(this.m_cGUIBattle.GetVecEnemy());
                        }
                        else if (this.m_cBattleHero.m_eBBType == BBType.BUFF)  //BUFF
                        {
                            this.m_cBattleHero.GetCmdControl().CmdAllSkillBuff(this.m_cGUIBattle.GetVecEnemy());
                        }
                    }

                    this.m_cBattleHero.m_iAttackNum--;
                    this.m_iNowAttackNum++;
                    this.m_fGoStartTime = 0;
                }
            }
        }
        else
        {
            this.m_cGUIBattle.HidenTargetBUF(this.m_cBattleHero.m_iIndex);
        }

        return true;
    }

}
