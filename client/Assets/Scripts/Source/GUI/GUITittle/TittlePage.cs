using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Resource;


//	TittlePage.cs
//	Author: Lu Zexi
//	2014-09-19



/// <summary>
/// Tittle page.
/// </summary>
public class TittlePage : CPage<TittlePage,TittleView,TittleController>
{
	public const string RES_MAIN = "_GUI_TITTLE";   //主资源地址

	/// <summary>
	/// Show this instance.
	/// </summary>
	public override void Show()
	{
		RequestCollection collect = RequestCollection.Create();
		collect.m_delCompleteCallback = OnLoadComplete;
		collect.RequestPrefab(RES_MAIN);
	}
}

