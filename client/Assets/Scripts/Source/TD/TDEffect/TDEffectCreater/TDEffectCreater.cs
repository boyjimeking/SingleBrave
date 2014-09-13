using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Base;


//
//
//


/// <summary>
/// 动画创建类
/// </summary>
public class TDEffectCreater 
{
    private int m_iCurIndex;//当前数量
    private int m_iMaxNum;//总数量
    private float m_iEffectSpeed;//动画播放速度
    
    //private Vector3 m_cVectorCenterPos;//中心坐标
    //private Vector3 m_cVectorDis;//范围

    private TDEffect[] m_lstEffects;//运行中
    private float m_lTimeStart;//开始时间
    //private float m_lTimeDis;//开启间隔

    private GameObject m_cEffectParent;
    private UIAtlas m_cAtlas;
    private string m_cEffectName;

    private float[] m_lstCreateTime;//创建时间列表
    public float[] LstCreateTime
    {
        get { return m_lstCreateTime; }
        set { m_lstCreateTime = value; }
    }
    private float[] m_lstCreatePosX;//创建坐标X列表
    public float[] LstCreatePosX
    {
        get { return m_lstCreatePosX; }
        set { m_lstCreatePosX = value; }
    }
    private float[] m_lstCreatePosY;//创建坐标Y列表
    public float[] LstCreatePosY
    {
        get { return m_lstCreatePosY; }
        set { m_lstCreatePosY = value; }
    }

    private float m_fOffsetY;//偏移坐标Y
    public float FOffsetY
    {
        get { return m_fOffsetY; }
        set { m_fOffsetY = value; }
    }
    private float m_fOffsetX;//偏移坐标X
    public float FOffsetX
    {
        get { return m_fOffsetX; }
        set { m_fOffsetX = value; }
    }

    public TDEffectCreater(GameObject parent,UIAtlas atlas,string spriteName)
    {
        Init();
        m_cEffectParent = parent;
        m_cAtlas = atlas;
        m_cEffectName = spriteName;
    }

    private void Init()
    {
       m_iEffectSpeed = 1f;
    }

    ///// <summary>
    ///// 设置生成数量
    ///// </summary>
    ///// <param name="num"></param>
    //public void SetMaxNum(int num)
    //{
    //    m_iMaxNum = num;
    //    m_lstEffects = new TDEffect[num];
    //}

    ///// <summary>
    ///// 生成间隔时间
    ///// </summary>
    ///// <param name="time"></param>
    //public void SetTimeDistance(float time)
    //{
    //    m_lTimeDis = time;
    //}

    ///// <summary>
    ///// 设置显示范围
    ///// </summary>
    ///// <param name="r"></param>
    //public void SetPosRange(Vector3 dis)
    //{
    //    m_cVectorDis = dis;
    //}

    public void SetEffectSpeed(float speed)
    {
        m_iEffectSpeed = speed;
    }

    ///// <summary>
    /////设置中心显示坐标
    ///// </summary>
    ///// <param name="center"></param>
    //public void SetPosCenter(Vector3 center)
    //{
    //    m_cVectorCenterPos = center;
    //}

    /// <summary>
    /// 开始
    /// </summary>
    public void Start()
    {
        m_lTimeStart = GAME_TIME.TIME_FIXED();
        m_iMaxNum = m_lstCreateTime.Length;
        m_lstEffects = new TDEffect[m_iMaxNum];
        m_iCurIndex = 0;
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public void Destroy()
    {
        for (int i = 0; i < m_iMaxNum; i++)
        {
            TDEffect effect = m_lstEffects[i];
            if (effect == null) continue;
            effect.Destory();
        }
    }

    /// <summary>
    /// 更新
    /// </summary>
    public void Update()
    {
        //insert
        if (m_iCurIndex < m_iMaxNum && (GAME_TIME.TIME_FIXED() - m_lTimeStart) > m_lstCreateTime[m_iCurIndex])
        {
            TDEffect effect = new TDEffect(m_cEffectParent.transform, m_cAtlas);
            Vector3 pos = new Vector3(m_lstCreatePosX[m_iCurIndex], m_lstCreatePosY[m_iCurIndex], 0);

            effect.SetLocalPos(pos + new Vector3(m_fOffsetX,m_fOffsetY,0));
            effect.PlayAnimation(m_cEffectName, Game.Base.TDAnimationMode.Once, m_iEffectSpeed);
           
            m_lstEffects[m_iCurIndex] = effect;
            m_iCurIndex++;
        }

        //update
        for (int i = 0; i < m_iMaxNum; i++)
        {
            TDEffect effect = m_lstEffects[i];
            if (effect == null) continue;
            if (!effect.IsPlay())
            {
                effect.Destory();
                m_lstEffects[i] = null;
            }
            effect.Update();
        }
       
    }


}