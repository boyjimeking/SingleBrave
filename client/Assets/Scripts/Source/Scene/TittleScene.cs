using System;
using System.Collections.Generic;
using Game.Resource;

//  TittleScene.cs
//  Author: Lu Zexi
//  2013-11-12


/// <summary>
/// 标题场景
/// </summary>
public class TittleScene : CScene
{
    public TittleScene()
    {
        //
    }

    /// <summary>
    /// 场景进入
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
		TittlePage.sInstance.Show();
    }

    /// <summary>
    /// 退出场景
    /// </summary>
    public override void OnExit()
    {
		TittlePage.sInstance.Hiden();

        base.OnExit();
    }
}


