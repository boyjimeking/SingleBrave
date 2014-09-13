
//using UnityEngine;
//using Game.Gfx;


////  CmdHurtState.cs
////  Auth: Lu Zexi
////  2013-11-21




///// <summary>
///// 攻击状态
///// </summary>
//public class CmdHurtState : CmdStateBase
//{

//    public CmdHurtState(GfxObject obj, StateControl control)
//        : base(obj, control)
//    {
//        //
//    }

//    /// <summary>
//    /// 获取状态类型
//    /// </summary>
//    /// <returns></returns>
//    public override CMD_TYPE GetCmdType()
//    {
//        return CMD_TYPE.STATE_HURT;
//    }

//    /// <summary>
//    /// 进入状态
//    /// </summary>
//    /// <returns></returns>
//    public override bool OnEnter()
//    {
//        this.m_cControl.Hurt();
//        return true;
//    }

//    /// <summary>
//    /// 逻辑更新
//    /// </summary>
//    /// <returns></returns>
//    public override bool Update()
//    {
//        if (this.m_cControl.GetCurrentState() == null)
//            return false;
//        if (this.m_cControl.GetCurrentState().GetStateType() != STATE_TYPE.STATE_HURT)
//        {
//            return false;
//        }

//        return true;
//    }

//}

