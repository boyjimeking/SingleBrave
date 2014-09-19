using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Resource;
using UnityEngine;
using Game.Base;

//Lock面板类
//Author:Sunyi
//2014-2-17
public class GUILockPanel : GUIBase
{
    private const string RES_MAIN = "_GUI_LockPanel";//主资源地址

    private float m_fStartTime = 0;//启动时间
    private float m_fDuration = 0;//持续时间

    public GUILockPanel(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_LOCKPANEL, UILAYER.GUI_LOCKPANEL)
    { }

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
        }

        SetLocalPos(Vector3.zero);
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        SetLocalPos(Vector3.one * 0xFFFF);

        base.Hiden();
        Destory();
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    /// <returns></returns>
    public override bool Update()
    {
        base.Update();

        if (this.m_fStartTime != 0 && this.m_fDuration != 0)
        {
            float dis = GAME_TIME.TIME_FIXED() - this.m_fStartTime;
            if (dis > this.m_fDuration)
            {
                Hiden();
                this.m_fDuration = 0;
                this.m_fStartTime = 0;
            }
        }

        return true;
    }

    /// <summary>
    /// 记录启动时间
    /// </summary>
    /// <param name="time"></param>
    public void SetStartTime(DateTime time)
    {
        this.m_fStartTime = GAME_TIME.TIME_FIXED();
    }

    /// <summary>
    /// 设置启动时间
    /// </summary>
    /// <param name="time"></param>
    public void SetDurationTime(float time)
    {
        this.m_fDuration = time;
    }
}

