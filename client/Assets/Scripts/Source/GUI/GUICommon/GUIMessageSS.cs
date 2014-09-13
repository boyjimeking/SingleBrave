using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


//  GUIMessageSS.cs
//  Author: Lu Zexi
//  2014-01-08



/// <summary>
/// GUI迷你消息框
/// </summary>
public class GUIMessageSS: GUIBase
{
    private string RES_MAIN = "_GUI_MessageSS";   //主资源

    private string GUI_TEXT = "Lab_Content";   //文本

    private UILabel m_cLabel;   //文本


    public GUIMessageSS(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_MESSAGESS, GUILAYER.GUI_MESSAGE)
    {
        //
    }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show( )
    {
        base.Show();
        if (this.m_cGUIObject == null)
        {
            this.m_cGUIObject = GameObject.Instantiate(Resources.Load(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            GUIComponentEvent ce = this.m_cGUIObject.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnPanel);

            this.m_cLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, GUI_TEXT);
        }

        SetLocalPos(Vector3.zero);
    }
    
    /// <summary>
    /// 展示内容
    /// </summary>
    /// <param name="content"></param>
    public void Show(string content)
    {
        Show();
        this.m_cLabel.text = content;
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        base.Hiden();
        //SetLocalPos(Vector3.one * 0xFFFF);
        Destory();
    }

    /// <summary>
    /// 点击面板
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnPanel(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();
        }
    }
}
