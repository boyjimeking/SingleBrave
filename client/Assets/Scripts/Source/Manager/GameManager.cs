using System;
using System.Collections.Generic;

using Game.Resource;
using Game.Base;

//  GameManager.cs
//  Author: Lu Zexi
//  2013-11-12


/// <summary>
/// 游戏管理类
/// </summary>
public class GameManager : Singleton<GameManager>
{
    private GUIManager m_cGUIManager;   //GUI管理
    private SceneManager m_cSceneManager;   //场景管理

    public GameManager()
    {
        this.m_cGUIManager = new GUIManager();
        this.m_cSceneManager = new SceneManager();
    }

    /// <summary>
    /// 获取GUI管理类
    /// </summary>
    /// <returns></returns>
    public GUIManager GetGUIManager()
    {
        return this.m_cGUIManager;
    }

    /// <summary>
    /// 获取场景管理类
    /// </summary>
    /// <returns></returns>
    public SceneManager GetSceneManager()
    {
        return this.m_cSceneManager;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Initialize()
    {
        GAME_SETTING.LoadSetting();
        CameraManager.GetInstance().Initialize();
        SoundManager.GetInstance().Inititalize();
        this.m_cGUIManager.Initialize();
        this.m_cSceneManager.ChangeTittle();
        PlatformManager.GetInstance().Init();
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public void Update()
    {
        this.m_cGUIManager.Update();
        this.m_cSceneManager.Update();
        SessionManager.GetInstance().Update();
        SoundManager.GetInstance().Update();
        ResourcesManager.GetInstance().Update();
    }

    /// <summary>
    /// 渲染更新
    /// </summary>
    public void Render()
    {
        this.m_cGUIManager.Render();
    }

    /// <summary>
    /// 延迟渲染
    /// </summary>
    public void LateRender()
    { 
        //
        CTween.GetInstance().Update();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public void Destory()
    {
        this.m_cGUIManager.Destory();
        CTween.GetInstance().Destory();
        CameraManager.GetInstance().Destory();
        ResourcesManager.GetInstance().Destory();
    }

    /// <summary>
    /// 应用退出
    /// </summary>
    public void OnApplicationQuit()
    { 
        //
    }

    /// <summary>
    /// 应用中断
    /// </summary>
    public void OnApplicationPause(bool isPause)
    {
        PlatformManager.GetInstance().OnApplicationPause(isPause);
    }

}

