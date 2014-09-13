
using UnityEngine;
using Game.Gfx;


//  CmdStateBase.cs
//  Auth: Lu Zexi
//  2013-11-21



/// <summary>
/// 状态类型
/// </summary>
public enum CMD_TYPE
{
    STATE_NONE = 0,     //无
    STATE_DEFENCE = 1,  //防御
    STATE_SKILL,        //不移动单体释放技能
    STATE_ALL_SKILL,    //不移动全体释放技能
    STATE_MOVE_SKILL,   //移动单体释放技能
    STATE_MOVE_ALL_SKILL,   //移动全体释放技能
    STATE_MOVE_ATTACK,  //移动攻击
    STATE_ATTACK,       //非移动攻击
    STATE_HURT,         //受伤
    STATE_DIE,          //死亡状态
}


/// <summary>
/// 命令状态基础类
/// </summary>
public abstract class CmdStateBase
{
    protected BattleHero m_cBattleHero; //战场英雄
    protected GfxObject m_cObj;   //物体
    protected StateControl m_cControl;  //控制对象
    protected IGUIBattle m_cGUIBattle;   //GUI实例
    protected int m_iStep;    //步骤
    protected bool m_bEnable;   //是否可用
    protected bool m_bCanDrop;    //是否可以掉落

    public CmdStateBase(BattleHero battleHero, IGUIBattle gui)
    {
        this.m_cGUIBattle = gui;
        this.m_cBattleHero = battleHero;
        this.m_cObj = battleHero.GetGfxObject();
        this.m_cControl = this.m_cObj.GetStateControl();
    }

    /// <summary>
    /// 是否支持掉落
    /// </summary>
    /// <param name="candrop"></param>
    public void SetDrop(bool candrop)
    {
        this.m_bCanDrop = candrop;
    }

    /// <summary>
    /// 获取状态类型
    /// </summary>
    /// <returns></returns>
    public abstract CMD_TYPE GetCmdType();

    /// <summary>
    /// 进入事件
    /// </summary>
    /// <returns></returns>
    public virtual bool OnEnter()
    {
        this.m_bEnable = true;
        return true;
    }

    /// <summary>
    /// 退出事件
    /// </summary>
    /// <returns></returns>
    public virtual bool OnExit()
    {
        this.m_bEnable = false;
        return true;
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    /// <returns></returns>
    public abstract bool Update();

    /// <summary>
    /// 销毁
    /// </summary>
    public virtual void Destory()
    {
    }

}


