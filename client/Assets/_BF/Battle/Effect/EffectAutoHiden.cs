using UnityEngine;
using System.Collections;


//  EffectAutoHiden.cs
//  Author: Lu Zexi
//  2014-01-08


/// <summary>
/// 特效脚本自动隐藏
/// </summary>
[AddComponentMenu("Game/Effect/EffectAutoHiden")]
public class EffectAutoHiden : MonoBehaviour
{
    [SerializeField]
    public GameObject m_cTarget;    //目标物体
    [SerializeField]
    public float m_fHidenTime;   //隐藏时间
    [SerializeField]
    public float m_fDurationTime;   //持续时间

    private float m_fHidenStartTime;    //隐藏时间
    private float m_fDurationStartTime; //持续开始时间

	// Use this for initialization
	void Start ()
    {
        this.m_fDurationStartTime = Time.fixedTime;
        this.m_fHidenStartTime = -1;
	}


    void FixedUpdate()
    {
        if (this.m_fDurationStartTime >= 0)
        {
            if (Time.fixedTime - this.m_fDurationStartTime > this.m_fDurationTime)
            {
                this.m_cTarget.SetActive(false);
                this.m_fDurationStartTime = -1;
                this.m_fHidenStartTime = Time.fixedTime;
            }
        }
        else
        {
            if (this.m_fHidenStartTime >= 0)
            {
                if (Time.fixedTime - this.m_fHidenStartTime > this.m_fHidenTime)
                {
                    Debug.Log("show");
                    this.m_cTarget.SetActive(true);
                    this.m_fHidenStartTime = -1;
                    this.m_fDurationStartTime = Time.fixedTime;
                }
            }
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
