using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Base;


//  SceneManager.cs
//  Author: Lu Zexi
//  2013-11-12



/// <summary>
/// 场景管理类
/// </summary>
public class SceneManager
{
    private TittleScene m_cTittleScene; //标题场景
    private LoadingScene m_cLoadingScene;   //加载场景
    private LoginScene m_cLoginScene;   //登录场景
    private GameScene m_cGameScene; //游戏场景

    private SceneBase m_cNowScene;  //当前场景


    public SceneManager()
    {
        this.m_cTittleScene = new TittleScene();
        this.m_cLoadingScene = new LoadingScene();
        this.m_cGameScene = new GameScene();
        this.m_cLoginScene = new LoginScene();
    }


    /// <summary>
    /// 获取当前场景
    /// </summary>
    /// <returns></returns>
    public SceneBase GetCurScene()
    {
        return this.m_cNowScene;
    }

    /// <summary>
    /// 切换标题场景
    /// </summary>
    public void ChangeTittle()
    {
        if (this.m_cNowScene != null)
        {
            this.m_cNowScene.OnExit();
        }
        this.m_cNowScene = this.m_cTittleScene;
        this.m_cNowScene.OnEnter();
    }

    /// <summary>
    /// 切换加载场景
    /// </summary>
    public void ChangeLoading()
    {
        if (this.m_cNowScene != null)
        {
            this.m_cNowScene.OnExit();
        }
        this.m_cNowScene = this.m_cLoadingScene;
        this.m_cNowScene.OnEnter();
    }

    /// <summary>
    /// 切换登录场景
    /// </summary>
    public void ChangeLoginScene()
    {
        if (this.m_cNowScene != null)
        {
            this.m_cNowScene.OnExit();
        }
        this.m_cNowScene = this.m_cLoginScene;
        this.m_cLoginScene.OnEnter();
    }

    /// <summary>
    /// 切换游戏场景
    /// </summary>
    public void ChangeGameScene()
    {
        if (this.m_cNowScene != null)
        {
            this.m_cNowScene.OnExit();
        }
        this.m_cNowScene = this.m_cGameScene;
        this.m_cNowScene.OnEnter();
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public void Update()
    {
        if (this.m_cNowScene != null)
        {
            this.m_cNowScene.Update();
        }
    }

}

