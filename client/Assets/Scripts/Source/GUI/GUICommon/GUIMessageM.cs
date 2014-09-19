using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


//  GUIMessageM.cs
//  Author: Lu Zexi
//  2014-01-08



/// <summary>
/// GUI中消息框
/// </summary>
public class GUIMessageM: GUIBase
{
    private string RES_MAIN = "_GUI_MessageM";   //主资源

    public delegate void CALL_BACK( );   //回调接口
    public delegate void CALL_BACK1(bool yes);  //是否按钮回调

    private string GUI_TEXT = "Lab_Content";   //文本
    private string GUI_BTN_SURE = "Btn_Sure";    //确定按钮

    private string GUI_BTN_YES = "Btn_Yes"; //YES按钮
    private string GUI_BTN_NO = "Btn_No"; //NO按钮

    private UILabel m_cLabel;   //文本
    private GameObject m_cBtn;  //确定按钮
    private GameObject m_cBtnYes;   //YES按钮
    private GameObject m_cBtnNo;    //NO按钮

    private CALL_BACK m_delCallBack;    //回调接口
    private CALL_BACK1 m_delCallBack1;  //是或否回调接口
    

    public GUIMessageM(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_MESSAGEM, UILAYER.GUI_MESSAGE)
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

            this.m_cBtn = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_BTN_SURE);
            GUIComponentEvent ce = this.m_cBtn.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnBtn);

            this.m_cBtnYes = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_BTN_YES);
            ce = this.m_cBtnYes.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnYesOrNoBtn, true);

            this.m_cBtnNo = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_BTN_NO);
            ce = this.m_cBtnNo.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnYesOrNoBtn, false);

            this.m_cLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, GUI_TEXT);
        }

        SetLocalPos(Vector3.zero);
    }

    /// <summary>
    /// 设置按钮图片
    /// </summary>
    /// <param name="str1"></param>
    /// <param name="str2"></param>
    public void SetButtonImg(string str1, string str2)
    {
        this.m_cBtnYes.GetComponentInChildren<UISprite>().spriteName = str1;
        this.m_cBtnYes.GetComponent<UIImageButton>().normalSprite = str1;
        this.m_cBtnYes.GetComponent<UIImageButton>().hoverSprite = str2;
        this.m_cBtnYes.GetComponent<UIImageButton>().pressedSprite = str2;
        this.m_cBtnYes.GetComponent<UIImageButton>().disabledSprite = str2;
    }

    /// <summary>
    /// 设置取消按钮图片
    /// </summary>
    /// <param name="str1"></param>
    /// <param name="str2"></param>
    public void SetCancelButtonImg(string str1, string str2)
    {
        this.m_cBtnNo.GetComponentInChildren<UISprite>().spriteName = str1;
        this.m_cBtnNo.GetComponent<UIImageButton>().normalSprite = str1;
        this.m_cBtnNo.GetComponent<UIImageButton>().hoverSprite = str2;
        this.m_cBtnNo.GetComponent<UIImageButton>().pressedSprite = str2;
        this.m_cBtnNo.GetComponent<UIImageButton>().disabledSprite = str2;
    }

    /// <summary>
    /// 展示
    /// </summary>
    /// <param name="callBack"></param>
    public void Show(CALL_BACK callBack, string content)
    {
        Show();
        this.m_cBtn.SetActive(true);
        this.m_cBtnYes.SetActive(false);
        this.m_cBtnNo.SetActive(false);
        this.m_delCallBack = callBack;
        this.m_cLabel.text = content;
    }

    /// <summary>
    /// 展示
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="content"></param>
    public void Show(CALL_BACK1 callback, string content)
    {
        Show();
        this.m_cBtn.SetActive(false);
        this.m_cBtnYes.SetActive(true);
        this.m_cBtnNo.SetActive(true);
        this.m_delCallBack1 = callback;
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
    /// 确定按钮事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnBtn(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (this.m_delCallBack != null)
            {
                this.m_delCallBack();
            }
            Hiden();
        }
    }

    /// <summary>
    /// 是或否按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnYesOrNoBtn(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            bool yes = (bool)args[0];

            if (this.m_delCallBack1 != null)
            {
                this.m_delCallBack1(yes);
            }

            Hiden();
        }
    }
}
