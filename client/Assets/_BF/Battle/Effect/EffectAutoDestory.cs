using UnityEngine;
using System.Collections;


//  EffectAutoDestory.cs
//  Author: Lu Zexi
//  2013-12-03



/// <summary>
/// 特效自动销毁脚本
/// </summary>
[AddComponentMenu("Game/Effect/EffectAutoDestory")]
public class EffectAutoDestory : MonoBehaviour
{	
	public float m_fDestroyTime;

	// Use this for initialization
	IEnumerator Start () {
        yield return new WaitForSeconds(m_fDestroyTime);
		Destroy(gameObject);
	}


}
