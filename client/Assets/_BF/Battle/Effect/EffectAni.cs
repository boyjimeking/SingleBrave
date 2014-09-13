using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Game/Effect/EffectAni")]
public class EffectAni : MonoBehaviour {

    public List<string> m_strAniName; //动画名字
	// Use this for initialization
	void Start () {
        foreach (string name in m_strAniName)
        {
            animation.PlayQueued(name);
        }
	}
}
