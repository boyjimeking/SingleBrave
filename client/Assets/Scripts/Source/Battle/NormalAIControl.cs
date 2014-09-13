
using System.Collections.Generic;
using UnityEngine;
using Game.Base;
using Game.Gfx;

//  NomalAIControl.cs
//  Author: Lu Zexi
//  2013-12-02





/// <summary>
/// 普通AI
/// </summary>
public class NormalAIControl : AIControl
{
    protected GfxObject m_cGfxObj;  //渲染实例

    private const float AI_IDLE_COST_TIME = 2F;  //AI间隔时间
    private const float AI_ATTACK_COST_TIME = 2F;  //AI最大随机时间
    private float m_fGoStartTime;   //AI开始选择时间
    private float m_fGoDisTime; //间隔时间
    private int m_iState;   //状态

    public NormalAIControl(  GfxObject obj )
        :base()
    {
        this.m_cGfxObj = obj;
        this.m_iState = 0;
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    /// <returns></returns>
    public override bool Update()
    {

        switch( this.m_iState )
        {
            case 0:
                this.m_iState++;
                break;
            case 1:
                this.m_cGfxObj.IdleState();
                this.m_fGoStartTime = GAME_TIME.TIME_FIXED();
                this.m_iState++;
                break;
            case 2:
                if (GAME_TIME.TIME_FIXED() - this.m_fGoStartTime > AI_IDLE_COST_TIME)
                {
                    this.m_iState++;
                }
                break;
            case 3:
                this.m_cGfxObj.GetStateControl().Attack();
                this.m_fGoStartTime = GAME_TIME.TIME_FIXED();
                this.m_iState++;
                break;
            case 4:
                if (this.m_cGfxObj.GetStateControl().GetCurrentState() != null && this.m_cGfxObj.GetStateControl().GetCurrentState().GetStateType() != STATE_TYPE.STATE_ATTACK)
                {
                    this.m_iState = 1;
                }
                break;
        }
        return true;
    }
}

