using System.Collections.Generic;
using UnityEngine;

//  EffectAnimation.cs
//  Author: Lu Zexi
//  2014-01-08


/// <summary>
/// 特效动画
/// </summary>
[AddComponentMenu("Game/Effect/EffectAnimation")]
public class EffectAnimation : MonoBehaviour
{
    public float m_fDelayTime;  //延迟时间
    public string m_strAniName; //动画名字
	public float m_fCostTime;	//开销时间
    public WrapMode m_eMode;    //播放模式

    //临时
    private float m_fStartTime; //开始时间
	private float m_fAniStartTime;	//动画开始时间
    private Animation[] m_vecAni;   //动画组件

    // Use this for initialization
    void Start()
    {
        this.m_fStartTime = Time.fixedTime;
        this.m_vecAni = GetComponentsInChildren<Animation>();
		this.m_fAniStartTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //
    }

    //逻辑更新
    void FixedUpdate()
    {
        float disTime = Time.fixedTime - this.m_fStartTime;
        if (disTime < this.m_fDelayTime)
            return;
		
		if( this.m_fAniStartTime == 0 )
		{
			this.m_fAniStartTime = Time.fixedTime;
	        foreach (Animation item in this.m_vecAni)
	        {
	            if (item != null)
	            {
					if( m_fCostTime > 0 )
					{
						item[this.m_strAniName].speed = this.m_fCostTime / item[this.m_strAniName].length;
					}
	                item[this.m_strAniName].wrapMode = m_eMode;
	                item.Play(this.m_strAniName);
	            }
	        }
		}
    }

}
