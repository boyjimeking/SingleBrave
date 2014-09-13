using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;


//  GUIAcccount.cs
//  Author: Lu Zexi
//  2014-02-18


/// <summary>
/// 帐号管理GUI
/// </summary>
public class GUIAccount : GUIBase
{
    private const string RES_MAIN = "_GUI_AcountInfo"; //主资源

    private const string GUI_MAIN = "MainPanel";    //主界面
    private const string GUI_REGIST = "MainPanel/Btn_Regist";    //注册按钮
    private const string GUI_LOGIN = "MainPanel/Btn_Login";    //登录按钮
    private const string GUI_LOGOUT = "MainPanel/Btn_Logout";    //登录按钮
    private const string GUI_USER_INPUT = "MainPanel/Input_Acount";   //用户名输入
    private const string GUI_PASSWORD_INPUT = "MainPanel/Input_Password";   //密码输入

    private GameObject m_cMain; //主界面
    private GameObject m_cBtnRegist;    //注册按钮
    private GameObject m_cBtnLogin; //登录按钮
    private GameObject m_cBtnLogout;    //注销
    private UIInput m_cInputUser;   //用户名输入
    private UIInput m_cInputPassword;   //密码输入

    public string m_strUser;   //用户名
    public string m_strPassword;   //密码
    public BoxCollider Box; //盒子碰撞器

    public GUIAccount(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_ACCOUNT, GUILAYER.GUI_PANEL2)
    { 
        //
    }

    /// <summary>
    /// 创建GUI
    /// </summary>
    private void CreateGUI()
    {
        if (this.m_cGUIObject == null)
        {
            this.m_cGUIObject = GameObject.Instantiate(Resources.Load(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GUI_FINDATION.FIND_GAME_OBJECT(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            this.Box = this.m_cGUIObject.GetComponent<BoxCollider>();

            this.m_cMain = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_MAIN);
            this.m_cBtnRegist = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_REGIST);
            GUIComponentEvent ce = this.m_cBtnRegist.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnRegist);
            this.m_cBtnLogin = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_LOGIN);
            ce = this.m_cBtnLogin.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnLogin);
            this.m_cBtnLogout = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_LOGOUT);
            ce = this.m_cBtnLogout.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnLogout);
            this.m_cInputUser = GUI_FINDATION.GET_OBJ_COMPONENT<UIInput>(this.m_cGUIObject, GUI_USER_INPUT);
            ce = this.m_cInputUser.gameObject.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnInputUser);

            this.m_cInputPassword = GUI_FINDATION.GET_OBJ_COMPONENT<UIInput>(this.m_cGUIObject, GUI_PASSWORD_INPUT);
            ce = this.m_cInputPassword.gameObject.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnInputPassword);


            ce = this.m_cGUIObject.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnBack);
        }
    }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        base.Show();
        CreateGUI();

        if (GAME_SETTING.s_strUserName != "")
        {
            this.m_cInputUser.value = GAME_SETTING.s_strUserName;
            this.m_cInputPassword.value = GAME_SETTING.s_strPassWord;
            if (GAME_SETTING.s_bAccountBound)
            {
                this.m_cBtnLogout.SetActive(true);
                this.m_cBtnRegist.SetActive(false);
            }
            else
            {
                this.m_cBtnLogout.SetActive(false);
                this.m_cBtnRegist.SetActive(true);
            }
        }
        else
        {
            this.m_cBtnLogout.SetActive(false);
            this.m_cBtnRegist.SetActive(true);
        }
        CTween.TweenPosition(this.m_cGUIObject, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640,0,0),Vector3.zero);
    }

    /// <summary>
    /// 立即展示
    /// </summary>
    public override void ShowImmediately()
    {
        base.ShowImmediately();
        CreateGUI();

        GUITittle title = (GUITittle)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_TITTLE);
        title.SetButtons(true);

        if (GAME_SETTING.s_strUserName != "")
        {
            this.m_cInputUser.value = GAME_SETTING.s_strUserName;
            this.m_cInputPassword.value = GAME_SETTING.s_strPassWord;
            if (GAME_SETTING.s_bAccountBound)
            {
                this.m_cBtnLogout.SetActive(true);
                this.m_cBtnRegist.SetActive(false);
            }
            else
            {
                this.m_cBtnLogout.SetActive(false);
                this.m_cBtnRegist.SetActive(true);
            }
        }
        else
        {
            this.m_cBtnLogout.SetActive(false);
            this.m_cBtnRegist.SetActive(true);
        }

        SetLocalPos(Vector3.zero);
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //base.Hiden();
        GUI_FUNCTION.AYSNCLOADING_HIDEN();
        CTween.TweenPosition(this.m_cGUIObject, GAME_DEFINE.FADEIN_GUI_TIME, GAME_DEFINE.FADEOUT_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0) , Destory);
  //      SetLocalPos(Vector3.one * 0xFFFFF);
        GUITittle title = (GUITittle)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_TITTLE);
        title.SetButtons(false);
    }

    /// <summary>
    /// 立即消失
    /// </summary>
    public override void HidenImmediately()
    {
        base.HidenImmediately();
        //SetLocalPos(Vector3.one * 0xFFFFF);
        Destory();
        GUITittle title = (GUITittle)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_TITTLE);
        title.SetButtons(false);
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();
        base.Destory();
    }

    /// <summary>
    /// 注册按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnRegist(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.m_strUser = this.m_cInputUser.value;
            this.m_strPassword = this.m_cInputPassword.value;

            SendAgent.SendAccountBoundReq(GAME_SETTING.s_strUserName, GAME_SETTING.s_strPassWord, this.m_strUser, this.m_strPassword);
        }
    }

    /// <summary>
    /// 登录按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnLogin(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.m_strUser = this.m_cInputUser.value;
            this.m_strPassword = this.m_cInputPassword.value;

            SendAgent.SendAccountLogin(this.m_strUser, this.m_strPassword);
            //SendAgent.SendVersionReq();
        }
    }

    /// <summary>
    /// 登出
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnLogout(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            GUI_FUNCTION.MESSAGEL_(LogoutYesOrNo, "确定要注销帐号吗?");
        }
    }

    /// <summary>
    /// 是否确定注销对话框
    /// </summary>
    /// <param name="yes"></param>
    private void LogoutYesOrNo( bool yes )
    {
        if (yes)
        {
            GAME_SETTING.ClearAccount();
            GameManager.GetInstance().GetSceneManager().ChangeTittle();
        }
    }

    /// <summary>
    /// 输入用户名
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnInputUser(GUI_INPUT_INFO info, object[] args)
    {
        //
    }

    /// <summary>
    /// 输入密码
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnInputPassword(GUI_INPUT_INFO info, object[] args)
    {
        //
    }

    /// <summary>
    /// 返回按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnBack(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        { 
            Hiden();
        }
    }
}
