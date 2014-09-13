//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Tween the object's position.
/// </summary>

[AddComponentMenu("NGUI/Tween/Tween Position")]
public class TweenPosition : UITweener
{
	public Vector3 from;
	public Vector3 to;

	Transform mTrans;

	public Transform cachedTransform { get { if (mTrans == null) mTrans = transform; return mTrans; } }
	public Vector3 position { get { return cachedTransform.localPosition; } set { cachedTransform.localPosition = value; } }

	protected override void OnUpdate (float factor, bool isFinished) { cachedTransform.localPosition = from * (1f - factor) + to * factor; }

	/// <summary>
	/// Start the tweening operation.
	/// </summary>

	static public TweenPosition Begin (GameObject go, float duration, Vector3 pos)
	{
		TweenPosition comp = UITweener.Begin<TweenPosition>(go, duration);
		comp.from = comp.position;
		comp.to = pos;

		if (duration <= 0f)
		{
			comp.Sample(1f, true);
			comp.enabled = false;
		}
		return comp;
	}

    static public TweenPosition Begin(GameObject go, float duration, Vector3 start, Vector3 end)
    {
        go.transform.localPosition = start;
        TweenPosition comp = UITweener.Begin<TweenPosition>(go, duration);
        comp.from = comp.position;
        comp.to = end;

        if (duration <= 0f)
        {
            comp.Sample(1f, true);
            comp.enabled = false;
        }
        return comp;
    }

    /// <summary>
    /// 开始位移
    /// </summary>
    /// <param name="go">位移物体</param>
    /// <param name="dely">延迟时间</param>
    /// <param name="duration">持续时间</param>
    /// <param name="start">开始位置</param>
    /// <param name="end">结束位置</param>
    /// <returns></returns>
    static public TweenPosition Begin(GameObject go,float delay , float duration, Vector3 start, Vector3 end)
    {
        go.transform.localPosition = start;
        TweenPosition comp = UITweener.Begin<TweenPosition>(go , delay , duration);
        comp.from = start;
        comp.to = end;

        if (duration <= 0f)
        {
            comp.Sample(1f, true);
            comp.enabled = false;
        }

        return comp;
    }
}
