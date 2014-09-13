using System;
using System.Collections.Generic;
using UnityEngine;

//  CTweenItem.cs
//  Author: Lu Zexi
//  2013-11-26

/// <summary>
/// 补间类型
/// </summary>
public enum TWEEN_TYPE
{
    POSITION = 1,   //位移补间
    ALPHA = 2,  //Alpha补间
    SCALE = 3,  //放大缩小补间
}

/// <summary>
/// 曲线类型
/// </summary>
public enum TWEEN_LINE_TYPE
{ 
    Line = 0,   //直线
    ElasticInOut,   //弹性曲线
}


/// <summary>
/// CTween元素
/// </summary>
public class CTweenItem
{
    public delegate void FINISH_FUNC();

    public TWEEN_TYPE m_eType;    //类型
    public TWEEN_LINE_TYPE m_eLineType; //曲线类型
    public GameObject m_cGo;    //实体
    public float m_fDelay; //延迟时间
    public float m_fDuration;  //持续时间
    public float m_fStartTime;  //开始时间
    public float m_fStartAlpha;    //开始Alpha
    public float m_fEndAlpha;  //结束Alpha
    public Vector3 m_vecStartPos;  //开始位置
    public Vector3 m_vecEndPos;    //结束位置
    public object[] m_vecArgs;  //参数
    public FINISH_FUNC m_delFinish; //结束句柄

    public CTweenItem()
    { 
        //
    }
}
