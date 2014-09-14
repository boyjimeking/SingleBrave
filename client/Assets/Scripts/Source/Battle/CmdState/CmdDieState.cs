using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Resource;
using Game.Media;
using Game.Gfx;


//  CmdDieState.cs
//  Author: Lu Zexi
//  2013-12-02



/// <summary>
/// 死亡状态
/// </summary>
public class CmdDieState : CmdStateBase
{
    public CmdDieState(BattleHero hero, IGUIBattle gui)
        : base(hero, gui)
    { 
        //
    }

    /// <summary>
    /// 获取命令状态
    /// </summary>
    /// <returns></returns>
    public override CMD_TYPE GetCmdType()
    {
        return CMD_TYPE.STATE_DIE;
    }

    /// <summary>
    /// 进入事件
    /// </summary>
    /// <returns></returns>
    public override bool OnEnter()
    {
		MediaMgr.sInstance.PlayBGM(SOUND_DEFINE.SE_BATTLE_DEAD);
        this.m_cControl.Die();
        return base.OnEnter();
    }

    /// <summary>
    /// 退出事件
    /// </summary>
    /// <returns></returns>
    public override bool OnExit()
    {
        return base.OnExit();
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    /// <returns></returns>
    public override bool Update()
    {
        if (this.m_cControl.GetCurrentState().GetStateType() != STATE_TYPE.STATE_DIE)
        {
            this.m_cBattleHero.m_bDead = true;
            return false;
        }
        return true;
    }
}
