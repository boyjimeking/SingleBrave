using System;
using System.Collections.Generic;
using Game.Base;
using UnityEngine;


//  CTween.cs
//  Author: Lu Zexi
//  2013-11-26



/// <summary>
/// CTween类
/// </summary>
public class CTween : Singleton<CTween>
{
    private List<CTweenItem> m_lstTween;    //tween列表

    public CTween()
    {
        this.m_lstTween = new List<CTweenItem>();
    }


    /// <summary>
    /// 初始化
    /// </summary>
    public void Initialize()
    {
        this.m_lstTween.Clear();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public void Destory()
    {
        this.m_lstTween.Clear();
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public void Update()
    {
        List<CTweenItem> delLst = new List<CTweenItem>();
        //foreach( CTweenItem item in this.m_lstTween )
        for (int i = 0; i<this.m_lstTween.Count ; i++ )
        {
            CTweenItem item = this.m_lstTween[i];
            float disTime = Time.time - item.m_fStartTime;
            if (disTime > item.m_fDelay + item.m_fDuration)
            {
                if (item.m_cGo != null)
                {
                    switch (item.m_eType)
                    {
                        case TWEEN_TYPE.POSITION:
                            item.m_cGo.transform.localPosition = item.m_vecEndPos;
                            break;
                        case TWEEN_TYPE.ALPHA:
                            UIPanel panle = item.m_cGo.GetComponent<UIPanel>();
                            UIWidget widget = item.m_cGo.GetComponent<UIWidget>();
                            if (panle != null) panle.alpha = item.m_fEndAlpha;
                            if (widget != null) widget.alpha = item.m_fEndAlpha;
                            break;
                        case TWEEN_TYPE.SCALE:
                            item.m_cGo.transform.localScale = item.m_vecEndPos;
                            break;
                    }
                }

                if (item.m_delFinish != null)
                {
                    item.m_delFinish();
                }
                delLst.Add(item);
            }
            else
            {
                if (disTime >= item.m_fDelay && item.m_cGo != null )
                {
                    switch (item.m_eType)
                    {
                        case TWEEN_TYPE.POSITION:
                            float posRate = MathLine(item.m_eLineType , (disTime - item.m_fDelay) / item.m_fDuration, 0, 1, 1);
                            Vector3 pos = Vector3.Lerp(item.m_vecStartPos, item.m_vecEndPos, posRate);
                            item.m_cGo.transform.localPosition = pos;
                            break;
                        case TWEEN_TYPE.ALPHA:
                            float alphaRate = MathLine(item.m_eLineType , (disTime - item.m_fDelay) / item.m_fDuration, 0, 1, 1);
                            float alpha = Mathf.Lerp(item.m_fStartAlpha, item.m_fEndAlpha, alphaRate);
                            UIPanel panle = item.m_cGo.GetComponent<UIPanel>();
                            UIWidget widget = item.m_cGo.GetComponent<UIWidget>();
                            if (panle != null) panle.alpha = alpha;
                            if (widget != null) widget.alpha = alpha;
                            break;
                        case TWEEN_TYPE.SCALE:
                            float scaleRate = MathLine(item.m_eLineType , (disTime - item.m_fDelay) / item.m_fDuration, 0, 1, 1);
                            Vector3 scale = Vector3.Lerp(item.m_vecStartPos, item.m_vecEndPos, scaleRate);
                            item.m_cGo.transform.localScale = scale;
                            break;
                    }
                }
            }
        }

        foreach (CTweenItem item in delLst)
        {
            this.m_lstTween.Remove(item);
        }
    }

    /// <summary>
    /// 获取数学曲线
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private float MathLine(TWEEN_LINE_TYPE type , float t , float b , float c , float d )
    {
        switch (type)
        { 
            case TWEEN_LINE_TYPE.Line:
                return CMath.Linear(t, b, c, d);
            case TWEEN_LINE_TYPE.ElasticInOut:
                return CMath.ElasticInOut(t, b, c, d);
        }
        return 1;
    }

    /// <summary>
    /// 增加补间元素
    /// </summary>
    /// <param name="item"></param>
    private void AddTween(CTweenItem item)
    {
        this.m_lstTween.Add(item);
    }

    /// <summary>
    /// 获取补间动画
    /// </summary>
    /// <param name="go"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public CTweenItem GetTween(GameObject go, TWEEN_TYPE type)
    {
        foreach (CTweenItem item in this.m_lstTween)
        {
            if (item.m_cGo == go && item.m_eType == type)
            {
                return item;
            }
        }
        return null;
    }

    /// <summary>
    /// 位移补间
    /// </summary>
    /// <param name="go"></param>
    /// <param name="delay"></param>
    /// <param name="duration"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    public static void TweenPosition( GameObject go , float delay , float duration , Vector3 start , Vector3 end , TWEEN_LINE_TYPE lineType , CTweenItem.FINISH_FUNC func )
    {
        if (go == null)
        {
            //Debug.LogError("TweenPosition gameobject is null.");
            return;
        }
        CTweenItem item = CTween.GetInstance().GetTween( go , TWEEN_TYPE.POSITION );
        if (item == null)
        {
            item = new CTweenItem();
            CTween.GetInstance().AddTween(item);
        }
        item.m_cGo = go;
        item.m_fDelay = delay;
        item.m_fDuration = duration;
        item.m_vecStartPos = start;
        item.m_vecEndPos = end;
        item.m_eType = TWEEN_TYPE.POSITION;
        item.m_eLineType = lineType;
        item.m_fStartTime = Time.time;
        item.m_delFinish = func;

        go.transform.localPosition = start;
    }

    /// <summary>
    /// 位移补间
    /// </summary>
    /// <param name="go"></param>
    /// <param name="delay"></param>
    /// <param name="duration"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    public static void TweenPosition(GameObject go, float delay, float duration, Vector3 start, Vector3 end, CTweenItem.FINISH_FUNC finish_func)
    {
        TweenPosition(go, delay, duration, start, end, TWEEN_LINE_TYPE.ElasticInOut, finish_func);
    }

    /// <summary>
    /// 位移补间
    /// </summary>
    /// <param name="go"></param>
    /// <param name="delay"></param>
    /// <param name="duration"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    public static void TweenPosition(GameObject go, float delay, float duration, Vector3 start, Vector3 end)
    {
        TweenPosition(go, delay, duration , start, end, TWEEN_LINE_TYPE.ElasticInOut , null);
    }

    /// <summary>
    /// 位移补间
    /// </summary>
    /// <param name="go"></param>
    /// <param name="duration"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    public static void TweenPosition(GameObject go, float duration, Vector3 start, Vector3 end, CTweenItem.FINISH_FUNC finish_func)
    {
        TweenPosition(go, 0, duration, start, end, TWEEN_LINE_TYPE.ElasticInOut, finish_func);
    }

    /// <summary>
    /// 位移补间
    /// </summary>
    /// <param name="go"></param>
    /// <param name="duration"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    public static void TweenPosition(GameObject go, float duration, Vector3 start, Vector3 end)
    {
        TweenPosition(go, 0, duration, start, end , TWEEN_LINE_TYPE.ElasticInOut , null );
    }

    /// <summary>
    /// 位移补间
    /// </summary>
    /// <param name="go"></param>
    /// <param name="duration"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    public static void TweenPosition(GameObject go, float duration , Vector3 end)
    {
        TweenPosition(go, 0, duration, go.transform.localPosition, end, TWEEN_LINE_TYPE.ElasticInOut, null);
    }

    /// <summary>
    /// alpha补间动画
    /// </summary>
    /// <param name="go"></param>
    /// <param name="delay"></param>
    /// <param name="duration"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    public static void TweenAlpha(GameObject go, float delay, float duration, float start, float end , CTweenItem.FINISH_FUNC finish_func )
    {
        if (go == null)
        {
            //Debug.LogError("TweenAlpha gameobject is null.");
            return;
        }
        CTweenItem item = CTween.GetInstance().GetTween(go, TWEEN_TYPE.ALPHA);
        if (item == null)
        {
            item = new CTweenItem();
            CTween.GetInstance().AddTween(item);
        }
        item.m_cGo = go;
        item.m_fDelay = delay;
        item.m_fDuration = duration;
        item.m_fStartAlpha = start;
        item.m_fEndAlpha = end;
        item.m_eType = TWEEN_TYPE.ALPHA;
        item.m_eLineType = TWEEN_LINE_TYPE.Line;
        item.m_fStartTime = Time.time;
        item.m_delFinish = finish_func;

        UIWidget widget = item.m_cGo.GetComponent<UIWidget>();
        UIPanel panel = item.m_cGo.GetComponent<UIPanel>();
        if (widget != null) widget.alpha = start;
        if (panel != null) panel.alpha = start;
    }

    /// <summary>
    /// alpha补间动画
    /// </summary>
    /// <param name="go"></param>
    /// <param name="delay"></param>
    /// <param name="duration"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    public static void TweenAlpha(GameObject go, float delay, float duration, float start, float end)
    {
        TweenAlpha(go, delay, duration, start, end, null);
    }

    /// <summary>
    /// alpha补间动画
    /// </summary>
    /// <param name="go"></param>
    /// <param name="duration"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    public static void TweenAlpha(GameObject go, float duration, float start, float end)
    {
        TweenAlpha(go, 0, duration, start, end , null );
    }


    /// <summary>
    /// 缩放补间
    /// </summary>
    /// <param name="go"></param>
    /// <param name="delay"></param>
    /// <param name="duration"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="func"></param>
    public static void TweenScale(GameObject go, float delay, float duration, Vector3 start, Vector3 end, CTweenItem.FINISH_FUNC func)
    {
        if (go == null)
        {
            //Debug.LogError("TweenScale gameobject is null.");
            return;
        }
        CTweenItem item = CTween.GetInstance().GetTween(go, TWEEN_TYPE.POSITION);
        if (item == null)
        {
            item = new CTweenItem();
            CTween.GetInstance().AddTween(item);
        }
        item.m_cGo = go;
        item.m_fDelay = delay;
        item.m_fDuration = duration;
        item.m_vecStartPos = start;
        item.m_vecEndPos = end;
        item.m_eType = TWEEN_TYPE.SCALE;
        item.m_eLineType = TWEEN_LINE_TYPE.Line;
        item.m_fStartTime = Time.time;
        item.m_delFinish = func;

        go.transform.localScale = start;
    }

    /// <summary>
    /// 缩放补间
    /// </summary>
    /// <param name="go"></param>
    /// <param name="delay"></param>
    /// <param name="duration"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    public static void TweenScale(GameObject go, float delay, float duration, Vector3 start, Vector3 end)
    {
        TweenScale(go, delay, duration, start, end, null);
    }

    /// <summary>
    /// 缩放补间
    /// </summary>
    /// <param name="go"></param>
    /// <param name="duration"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    public static void TweenScale(GameObject go , float duration, Vector3 start, Vector3 end)
    {
        TweenScale(go, 0, duration, start, end, null);
    }
}
