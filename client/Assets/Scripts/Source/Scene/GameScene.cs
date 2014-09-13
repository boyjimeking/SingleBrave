using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;

//  GameScene.cs
//  Author: Lu Zexi
//  2013-11-12



/// <summary>
/// 游戏场景
/// </summary>
public class GameScene : SceneBase
{
    public GameScene()
        :base(SCENE.SCENE_GAME)
    {
    }

    /// <summary>
    /// 进入场景事件
    /// </summary>
    public override void OnEnter()
    {
        if (Role.role.GetBaseProperty().m_iModelID == 0)
        //if(false)
        {
            Role.role.GetBaseProperty().m_iModelID++;
            GUI_FUNCTION.SHOW_STORY(GUIDE_FUNCTION.STORY_FIRST, StartScene);
            return;
        }
        else
        {
            GUIBackFrameTop backtop = (GUIBackFrameTop)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP);
            backtop.Show();

            GUIBackFrameBottom backbottom = (GUIBackFrameBottom)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM);
            backbottom.Show();

            GUIMain wordmain = (GUIMain)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_MAIN);
            wordmain.Show();

            GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_ANNOUNCEMENT).Show();

            GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();

            //if (GAME_SETTING.s_iLastLoginDays != Role.role.GetBaseProperty().m_iLoginTimes)
            //{
            //    UnityEngine.Debug.Log("days::  " + GAME_SETTING.s_iLastLoginDays);
            //    GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_LOGINREWARD).Show();
            //}
        }

        base.OnEnter();
    }

    /// <summary>
    /// 开始
    /// </summary>
    private void StartScene()
    {
        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();   //为了加载背景
        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_GUIDE_BATTLE).Show();

        //GUIBackFrameTop backtop = (GUIBackFrameTop)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP);
        //backtop.Show();

        //GUIBackFrameBottom backbottom = (GUIBackFrameBottom)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM);
        //backbottom.Show();

        //GUIMain wordmain = (GUIMain)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_MAIN);
        //wordmain.Show();

        //GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
    }

    /// <summary>
    /// 退出场景事件
    /// </summary>
    public override void OnExit()
    {
        base.OnExit();
        GameManager.GetInstance().GetGUIManager().Destory();
        Role.role.Destory();
        GUI_FUNCTION.DESTORY();
        ResourcesManager.GetInstance().UnloadUnusedResources();
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }

    /// <summary>
    /// 逻辑更新事件
    /// </summary>
    /// <returns></returns>
    public override bool Update()
    {
        return base.Update();
    }

}

