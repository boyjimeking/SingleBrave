using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Resource;
using Game.Media;

//  TittleController.cs
//  Author: Lu Zexi
//  2013-11-13


/// <summary>
/// 标题GUI
/// </summary>
public class TittleController : CController
{
	/// <summary>
	/// Gets the layer.
	/// </summary>
	/// <returns>The layer.</returns>
	public override UILAYER GetLayer ()
	{
		return UILAYER.GUI_PANEL;
	}

	/// <summary>
	/// Gets the root path.
	/// </summary>
	/// <returns>The root path.</returns>
	public override GameObject GetAnchor ()
	{
		return GUI_DEFINE.ANCHOR_CENTER;
	}

	/// <summary>
	/// Init this instance.
	/// </summary>
	public override void Init()
	{
		base.Init();
		
		GameObject go = GameObject.Instantiate(TittlePage.s_cView.m_mapRes[TittlePage.RES_MAIN] as GameObject) as GameObject;
		SET_PARENT(go,this,true);
		TittlePage.s_cView.Init();

		RegistEvent(TittlePage.s_cView.Button , OnButton);
		MediaMgr.sInstance.PlayBGM(SOUND_DEFINE.BGM_MAIN);
	}

    /// <summary>
    /// on tittle button
    /// </summary>
    /// <param name="arg"></param>
	private void OnButton(INPUT_INFO info, object[] arg)
    {
		if (info.m_eType == INPUT_TYPE.CLICK )
        {
            MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_TITTLE_JOIN_IN);
            PlatformManager.GetInstance().Login();
        }
    }
}

