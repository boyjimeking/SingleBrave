

using UnityEngine;
using Game.Gfx;

//  CmdDefenceState.cs
//  Auth: Lu Zexi
//  2013-11-21



/// <summary>
/// 防御命令状态
/// </summary>
public class CmdDefenceState : CmdStateBase
{
    public CmdDefenceState(BattleHero hero, IGUIBattle gui)
        : base(hero, gui)
    {
    }

    /// <summary>
    /// 获取状态类型
    /// </summary>
    /// <returns></returns>
    public override CMD_TYPE GetCmdType()
    {
        return CMD_TYPE.STATE_DEFENCE;
    }

    /// <summary>
    /// 进入状态
    /// </summary>
    /// <returns></returns>
    public override bool OnEnter()
    {
        base.OnEnter();
        this.m_cControl.Idle();
        return true;
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    /// <returns></returns>
    public override bool Update()
    {
        return false;
    }

}

