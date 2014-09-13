using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


//  EffectRealAnimation.cs
//  Author: Lu Zexi
//  2014-01-22




/// <summary>
/// 特效真实动画类
/// </summary>
[AddComponentMenu("Game/Effect/EffectRealAnimation")]
public class EffectRealAnimation : MonoBehaviour
{
    private List<Animation> m_lstAnimation = new List<Animation>(); //动画队列
    private List<AnimationState> m_lstAniState = new List<AnimationState>();    //动画状态队列

    private float m_fStartTime; //动画开始时间

    public string AniName;  //动画名字

    void Start()
    {
        this.m_lstAnimation = new List<Animation>(this.GetComponentsInChildren<Animation>());
        Play();
    }

    /// <summary>
    /// 播放
    /// </summary>
    /// <param name="aniName"></param>
    public void Play()
    {
        for (int i = 0; i < this.m_lstAnimation.Count; i++)
        {
            AnimationState state = this.m_lstAnimation[i][this.AniName];
            this.m_lstAniState.Add(state);
            this.m_lstAnimation[i].Play(this.AniName);
        }
        this.m_fStartTime = Time.realtimeSinceStartup;
    }

    public void Update()
    {
        float disTime = Time.realtimeSinceStartup - this.m_fStartTime;
        for (int i = 0; i < this.m_lstAniState.Count; i++)
        {
            this.m_lstAniState[i].time = disTime;
        }
    }

}
