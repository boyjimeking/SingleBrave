using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Resource;
using Game.Media;

//  GUITittle.cs
//  Author: Lu Zexi
//  2013-11-13


/// <summary>
/// 标题GUI
/// </summary>
public class GUITittle : UIControllerBase<GUITittle , TittleView>
{
    private const string RES_MAIN = "_GUI_TITTLE";   //主资源地址

	public override GUILAYER GetLayer ()
	{
		return GUILAYER.GUI_PANEL;
	}

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        base.Show();

		UnityEngine.Object res_obj = Resources.Load(RES_MAIN);
		this.m_cMain = GameObject.Instantiate(res_obj) as GameObject;
		this.m_cMain.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
		this.m_cMain.transform.localScale = Vector3.one;
		
		this.m_cView = this.m_cMain.AddComponent<TittleView>();
		//            this.m_cButton = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_BUTTON);
		GUIComponentEvent gui_event = this.m_cView.Button.AddComponent<GUIComponentEvent>();
		gui_event.AddIntputDelegate(OnButton);
		
		MediaMgr.sInstance.PlayBGM(SOUND_DEFINE.BGM_MAIN);
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
		base.Hiden();
		Destroy();
    }

    /// <summary>
    /// 按钮事件
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

