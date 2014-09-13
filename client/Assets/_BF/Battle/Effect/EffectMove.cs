using UnityEngine;
using System.Collections;


//  EffectMove.cs
//  Author: Lu Zexi
//  2014-01-08


/// <summary>
/// 移动特效
/// </summary>
[AddComponentMenu("Game/Effect/EffectMove")]
public class EffectMove : MonoBehaviour
{
    public float m_fDelayTime;  //延迟时间
    public float m_fMoveSpeed;  //移动速度
    public GameObject m_cTarget;    //目标

    //临时
    private float m_fMoveTime;   //移动时间
    private float m_fStartTime; //开始时间
    private Vector3 m_cStartPos;    //开始位置

	// Use this for initialization
	void Start ()
    {
        this.m_fStartTime = Time.fixedTime;
        this.m_cStartPos = this.transform.position;
        this.m_fMoveTime = (this.m_cStartPos - this.m_cTarget.transform.position).magnitude / this.m_fMoveSpeed;
		Debug.Log("move cost time : " + this.m_fMoveTime);
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    //逻辑更新
    void FixedUpdate()
    {
        float disTime = Time.fixedTime - this.m_fStartTime;
        if (disTime < this.m_fDelayTime)
            return;

        float rate = (disTime - this.m_fDelayTime) / this.m_fMoveTime;
        this.transform.position = Vector3.Lerp(this.m_cStartPos, this.m_cTarget.transform.position, rate);
    }
}
