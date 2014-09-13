using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// 场景枚举
/// </summary>
public enum SCENE
{ 
    SCENE_TITTLE = 1,   //标题场景
    SCENE_LOADING,  //加载场景
    SCENE_LOGIN,    //登录场景
    SCENE_GAME, //游戏场景
}


/// <summary>
/// 场景基础类
/// </summary>
public class SceneBase
{
    protected SCENE m_eSceneID; //场景ID

    public SceneBase(SCENE sc)
    {
        this.m_eSceneID = sc;
    }

    /// <summary>
    /// 获取场景ID
    /// </summary>
    /// <returns></returns>
    public SCENE GetSceneID()
    {
        return this.m_eSceneID;
    }

    /// <summary>
    /// 进入场景
    /// </summary>
    public virtual void OnEnter()
    {

        return;
    }

    /// <summary>
    /// 退出场景
    /// </summary>
    public virtual void OnExit()
    {
        return;
    }


    /// <summary>
    /// 逻辑更新
    /// </summary>
    /// <returns></returns>
    public virtual bool Update()
    {
        return true;
    }
}

