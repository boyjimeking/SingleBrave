using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Resource;


//  GUI_Hiden.cs
//  Author: Lu Zexi
//  2013-11-21



/// <summary>
/// GUI隐藏界面
/// </summary>
public class GUI_Hiden : GUIBase
{
    private const string RES_MAIN = "_GUI_HIDEN"; //主资源地址

    private const float RES_FIDEOUT_TIME =0.5F;
    private const float RES_FIDEIN_TIME =0.5F;

    private UIPanel m_cPanel;   //UI面板

    public GUI_Hiden(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_HIDEN, GUILAYER.GUI_LOADING)
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
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            this.m_cPanel = this.m_cGUIObject.GetComponent<UIPanel>();
            this.m_cPanel.alpha = 0;
        }
        //var tween = TweenAlpha.Begin(this.m_cGUIObject, 0.7f, 1);
        //tween.callWhenFinished
        SetLocalPos(Vector3.zero);
    }

    /// <summary>
    /// 淡进淡出
    /// </summary>
    public void FadeAll()
    {
        this.m_cPanel.alpha = 1;
        CTween.TweenAlpha(m_cGUIObject, 0, RES_FIDEIN_TIME, 1, 0, null);
        //TweenAlpha tmp = TweenAlpha.Begin(this.m_cGUIObject, RES_FIDEOUT_TIME, 1);
        //tmp.m_delFinish = FadeIn;
    }

    /// <summary>
    /// 淡出
    /// </summary>
    public void FadeOut()
    {
        this.m_cPanel.alpha = 0;
        CTween.TweenAlpha(m_cGUIObject, 0, RES_FIDEIN_TIME, 0, 1, null);
    }

    /// <summary>
    /// 淡进
    /// </summary>
    public void FadeIn()
    {
        this.m_cPanel.alpha =1;
        CTween.TweenAlpha(m_cGUIObject, RES_FIDEOUT_TIME, RES_FIDEIN_TIME, 1, 0, Hiden);
        //tmp.m_delFinish = Hiden;
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        base.Hiden();
        if (this.m_cPanel != null)
            this.m_cPanel.alpha = 0;
        //SetLocalPos(Vector3.one* 0xFFFF);
        Destory();
    }

}