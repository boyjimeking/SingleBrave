
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Game.Base;
using Game.Resource;

//  GUIBackGround.cs
//  Author: Lu Zexi
//  2013-12-17



/// <summary>
/// GUI背景
/// </summary>
public class GUIBackGround : GUIBase
{
    private const string RES_MAIN = "_GUI_BackGround"; //主资源

    public GUIBackGround(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_BACKGROUND, GUILAYER.GUI_BACKGROUND)
    { 
    }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        base.Show();
        if (this.m_cGUIObject == null)
        {
            this.m_cGUIObject = GameObject.Instantiate(Resources.Load(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GUI_FINDATION.FIND_GAME_OBJECT(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;
        }
        SetLocalPos(Vector3.zero);
    }

    /// <summary>
    /// 隐藏 
    /// </summary>
    public override void Hiden()
    {
        base.Hiden();
        SetLocalPos(Vector3.one*0xFFFF);
        Destory();
    }
}
