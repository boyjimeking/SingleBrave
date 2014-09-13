using System;
using System.Collections.Generic;
using Game.Resource;

//  TittleScene.cs
//  Author: Lu Zexi
//  2013-11-12


/// <summary>
/// 标题场景
/// </summary>
public class TittleScene : SceneBase
{
    public TittleScene()
        : base(SCENE.SCENE_TITTLE)
    { 
        //
    }

    /// <summary>
    /// 场景进入
    /// </summary>
    public override void OnEnter()
    {

        if (!GAME_SETTING.GAME_JOIN)
        {
            //GAME_SETTING.GAME_JOIN = true;
            //GAME_SETTING.SaveGAME_JOIN();
            if (string.IsNullOrEmpty(GAME_SETTING.DEVICE_ID))
            {
                SendAgent.SendGetDeviceID();
            }
            else
            {
                SendAgent.SendGameJoin(PlatformManager.GetInstance().GetDeviceID(), PlatformManager.GetInstance().GetChannelName());
            }
        }

        base.OnEnter();
        GUITittle guitittle = (GUITittle)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_TITTLE);
        guitittle.Show();
    }

    /// <summary>
    /// 退出场景
    /// </summary>
    public override void OnExit()
    {
        GUITittle guitittle = (GUITittle)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_TITTLE);
        guitittle.Destory();

        GUIAccount guiAccount = (GUIAccount)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_ACCOUNT);
        guiAccount.Destory();

        base.OnExit();
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    /// <returns></returns>
    public override bool Update()
    {
        return base.Update();
    }
}


