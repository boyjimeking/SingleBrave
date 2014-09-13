using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Resource;

//  LoadingScene.cs
//  Author: Lu Zexi
//  2013-11-19




/// <summary>
/// 加载场景
/// </summary>
public class LoadingScene : SceneBase
{
    public LoadingScene()
        :base(SCENE.SCENE_LOADING)
    { 
    }

    /// <summary>
    /// 进入场景事件
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
        GUIResouceLoading gui = (GUIResouceLoading)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_RESOURCE_LOADING);
        gui.Show();
    }

    /// <summary>
    /// 退出场景事件
    /// </summary>
    public override void OnExit()
    {
        base.OnExit();
        GUIResouceLoading gui = (GUIResouceLoading)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_RESOURCE_LOADING);
        gui.Hiden();
        gui.Destory();
    }
}

