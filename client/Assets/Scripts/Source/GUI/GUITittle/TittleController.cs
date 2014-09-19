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
public class TittleController : UIControllerBase
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
	/// Init this instance.
	/// </summary>
	public override void Init()
	{
		base.Init();

		GUIComponentEvent gui_event = TittlePage.s_cView.Button.AddComponent<GUIComponentEvent>();
		gui_event.AddIntputDelegate(OnButton);
		MediaMgr.sInstance.PlayBGM(SOUND_DEFINE.BGM_MAIN);
	}

    /// <summary>
    /// on tittle button
    /// </summary>
    /// <param name="arg"></param>
    private void OnButton(GUI_INPUT_INFO info, object[] arg)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_TITTLE_JOIN_IN);
            PlatformManager.GetInstance().Login();
        }
    }
}

