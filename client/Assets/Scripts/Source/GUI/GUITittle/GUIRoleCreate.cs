
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Game.Resource;
using System.Text;

//  GUIRoleCreate.cs
//  Author: Lu Zexi
//  2013-12-13



/// <summary>
/// 创建角色GUI
/// </summary>
public class GUIRoleCreate : GUIBase
{
    private const string RES_MAIN = "GUI_RoleCreate";

    private const string ROLE_NAME_INPUT = "Input";  //角色名输入框
    private const string GUI_SURE_BTN = "Btn_Ok"; //确定按钮
    private const string BTN_NEXT = "Btn_Next";//随机名字按钮地址

    private UIInput m_cInput;   //输入狂
    private GameObject m_cSureBtn;  //确定按钮
    private GameObject m_cBtnNext;//随机名字按钮
    private string m_strName;   //角色名字

    private int m_iSelectHeroIndex;//选择的英雄索引

    public GUIRoleCreate(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_ROLE_CREATE, GUILAYER.GUI_PANEL)
    { 
        //
    }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        this.m_eLoadingState = LOADING_STATE.NONE;
        if (this.m_cGUIObject == null)
        {
            this.m_eLoadingState = LOADING_STATE.START;
            GUI_FUNCTION.AYSNCLOADING_SHOW();
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_MAIN);
        }
        else
        {
            InitGUI();
        }
    }

    /// <summary>
    /// 初始化GUI
    /// </summary>
    protected override void InitGUI()
    {
        base.Show();
        GUI_FUNCTION.AYSNCLOADING_HIDEN();
        if (this.m_cGUIObject == null)
        {
			this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GUI_FINDATION.FIND_GAME_OBJECT(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            this.m_cInput = GUI_FINDATION.GET_OBJ_COMPONENT<UIInput>(this.m_cGUIObject, ROLE_NAME_INPUT);
            GUIComponentEvent ce = this.m_cInput.gameObject.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnName);

            this.m_cSureBtn = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_SURE_BTN);
            ce = this.m_cSureBtn.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnSureBtn);

            this.m_cBtnNext = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_NEXT);
            ce = this.m_cBtnNext.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnClickNextButtonEvent);
        }

        this.m_cInput.characterLimit = 8;

        SetNameLabel();

        SetLocalPos(Vector3.one);
    }

    /// <summary>
    /// 设置名字
    /// </summary>
    private void SetNameLabel()
    {
        List<PlayerNameTable> lstNames = PlayerNameTableManager.GetInstance().GetAll();

        int randomMaxCount = GAME_FUNCTION.RANDOM(1, 4);

        int randomIndex = 0;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < randomMaxCount; i++)
        {
            randomIndex = GAME_FUNCTION.RANDOM(0, lstNames.Count - 1);
            sb.Append(lstNames[randomIndex].PlayerName);
        }

        this.m_strName = sb.ToString();
        this.m_cInput.defaultText = sb.ToString();
        this.m_cInput.value = sb.ToString();
    }

    /// <summary>
    /// 随机选取名字点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickNextButtonEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            SetNameLabel();
        }
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <returns></returns>
    public override bool Update()
    {
        //资源加载等待
        switch (this.m_eLoadingState)
        {
            case LOADING_STATE.START:
                this.m_eLoadingState++;
                return false;
            case LOADING_STATE.LOADING:
                if (ResourceMgr.GetProgress() >= 1f && ResourceMgr.IsComplete())
                {
                    this.m_eLoadingState++;
                }
                return false;
            case LOADING_STATE.END:
                InitGUI();
                this.m_eLoadingState++;
                break;
        }

        return base.Update();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Destory();
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        base.Hiden();
        ResourceMgr.UnloadUnusedResources();
        Destory();
    }

    /// <summary>
    /// 名字输入
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnName(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.INPUT)
        { 
            //
        }
    }

    /// <summary>
    /// 设置选择的英雄索引
    /// </summary>
    /// <param name="index"></param>
    public void SetSelectHeroIndex(int index) 
    {
        this.m_iSelectHeroIndex = index;
    }

    /// <summary>
    /// 确定按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnSureBtn(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            bool isEllegle = true;
            Debug.Log("name  " + this.m_cInput.value);
            List<PlayerNameSensitiveWordTable> lstSensitiveWords = PlayerNameSensitiveWordTableManager.GetInstance().GetAll();
            for (int i = 0; i < lstSensitiveWords.Count; i++)
            {
                if (this.m_cInput.value.Equals(lstSensitiveWords[i].SensitiveWord))
                {
                    isEllegle = false;
                    GUI_FUNCTION.MESSAGEM(null, "您的输入不合法，含有敏感词汇，请重新输入！");
                }
            }

            if (this.m_cInput.value.Length > 8)
            {
                isEllegle = false;
                GUI_FUNCTION.MESSAGEM(null, "名称长度已超过8个字符，请重新输入！");
            }

            if (isEllegle)
            {
                SendAgent.SendPlayerCreatePktReq(this.m_cInput.value, GAME_SETTING.s_iUID, this.m_iSelectHeroIndex);
            }
            
        }
    }
}
