using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;
using Game.Media;

//商城界面GUIStore.cs
//Author:SunYi
//2013-11-22

public class GUIStore : GUIBase
{

    private const string RES_MAIN = "GUI_Store";//主资源地址

    private const string BUTTON_PANEL = "ButtonPanel";//按钮主页面地址
    private const string TOPPANEL = "TopPanel";//导航栏地址
    private const string BUTTON_BACK = "TopPanel/Button_Back";//返回按钮地址
    private const string BUTTON_BAOSHI = "ButtonPanel/Btn_Baoshi";//宝石购入按钮资源
    private const string BUTTON_TILI = "ButtonPanel/Btn_Tili";//体力恢复按钮资源
    private const string BUTTON_DANWEICAO = "ButtonPanel/Btn_DanWeiCao";//单位槽扩张按钮资源
    private const string BUTTON_DAOJUCAO = "ButtonPanel/Btn_DaoJuCao";//道具槽扩张按钮资源
    private const string BUTTON_GEDOUDIAN = "ButtonPanel/Btn_GeDouDian";//格斗点恢复按钮资源

    private GameObject m_cBtnBaoshi;//宝石购入按钮
    private GameObject m_cBtnTili;//体力恢复按钮
    private GameObject m_cBtnDanWeiCao;//单位槽扩张按钮
    private GameObject m_cBtnDaoJuCao;//道具槽扩张按钮
    private GameObject m_cBtnGeDouDian;//格斗点按钮

    private GameObject m_cButtonPanel;//按钮页面
    private GameObject m_cTopPanel;//导航栏按钮
    private GameObject m_cBtnBack;//返回按钮
    private bool m_bHasShow;  //加载过showobject

    public GUIStore(GUIManager guiMgr) 
        : base(guiMgr, GUI_DEFINE.GUIID_STORE, UILAYER.GUI_PANEL) 
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
            this.m_cGUIObject = GameObject.Instantiate(ResourceMgr.LoadAsset(GAME_DEFINE.RESOURCE_GUI_CACHE, RES_MAIN) as UnityEngine.Object) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            this.m_cButtonPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_PANEL);
            this.m_cButtonPanel.transform.localPosition = new Vector3(640, 0, 0);

            this.m_cTopPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, TOPPANEL);
            this.m_cTopPanel.transform.localPosition = new Vector3(-420, 270, 0);

            this.m_cBtnBaoshi = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_BAOSHI);
            GUIComponentEvent guiBaoshiEvent = this.m_cBtnBaoshi.AddComponent<GUIComponentEvent>();
            guiBaoshiEvent.AddIntputDelegate(OnClickBaoshiButton);

            this.m_cBtnTili = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_TILI);
            GUIComponentEvent guiTiliEvent = this.m_cBtnTili.AddComponent<GUIComponentEvent>();
            guiTiliEvent.AddIntputDelegate(OnClickTiliButton);

            this.m_cBtnDanWeiCao = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_DANWEICAO);
            GUIComponentEvent guiDanWeiCaoEvent = this.m_cBtnDanWeiCao.AddComponent<GUIComponentEvent>();
            guiDanWeiCaoEvent.AddIntputDelegate(OnClickDanWeiCaoButton);

            this.m_cBtnDaoJuCao = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_DAOJUCAO);
            GUIComponentEvent guiDaoJuCaoEvent = this.m_cBtnDaoJuCao.AddComponent<GUIComponentEvent>();
            guiDaoJuCaoEvent.AddIntputDelegate(OnClickDaoJuCaoButton);

            this.m_cBtnGeDouDian = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_GEDOUDIAN);
            GUIComponentEvent guiGeDouDianEvent = this.m_cBtnGeDouDian.AddComponent<GUIComponentEvent>();
            guiGeDouDianEvent.AddIntputDelegate(OnClickGeDouDianButton);

            this.m_cBtnBack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BUTTON_BACK);
            GUIComponentEvent guiBackEvent = this.m_cBtnBack.AddComponent<GUIComponentEvent>();
            guiBackEvent.AddIntputDelegate(OnClickBackButton);

        }

        //播放主背景音乐
		MediaMgr.sInstance.PlayBGM(SOUND_DEFINE.BGM_MAIN);
//        MediaMgr.PlayBGM(SOUND_DEFINE.BGM_MAIN);
        //GAME_UTILI.EXCUTE_DELAY(GAME_DEFINE.FADEOUT_GUI_TIME, FadeIn);

        this.m_cGUIMgr.SetCurGUIID(this.m_iID);
        SetLocalPos(Vector3.zero);

        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-420, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cButtonPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_MAIN_MENU));
    }


    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //base.Hiden();

        //SetLocalPos(Vector3.one * 0XFFFF);
       // CTween.TweenPosition(this.m_cButtonPanel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 0, 0), new Vector3(640, 0, 0));

        //CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-420, 270, 0),Destory);

        ResourceMgr.UnloadUnusedResources();

        Destory();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        this.m_cBtnBaoshi = null;
        this.m_cBtnTili = null;
        this.m_cBtnDanWeiCao = null;
        this.m_cBtnDaoJuCao = null;
        this.m_cBtnGeDouDian = null;

        this.m_cButtonPanel = null;
        this.m_cTopPanel = null;
        this.m_cBtnBack = null;

        base.Destory();
    }

    /// <summary>
    /// 返回主页按钮事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickBackButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            ShowLockPanel();

            Hiden();

            GUIBackFrameBottom backbottom = (GUIBackFrameBottom)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM);
            backbottom.ShowHalf();
            GUIMain main = (GUIMain)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_MAIN);
            main.Show();
        }
    }

    /// <summary>
    /// 宝石购入按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void OnClickBaoshiButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            SendAgent.SendStoreDiamondPrice();

            ShowLockPanel();
        }
    }

    /// <summary>
    /// 体力恢复按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void OnClickTiliButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            ShowLockPanel();

            Hiden();

            GUIBodyStrengthRestoration bodyStrengthRestoration = (GUIBodyStrengthRestoration)this.m_cGUIMgr
                .GetGUI(GUI_DEFINE.GUIID_BODYSTRENGTHRESTORATION);
            bodyStrengthRestoration.Show();
        }
    }

    /// <summary>
    /// 单位槽按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void OnClickDanWeiCaoButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            ShowLockPanel();

            Hiden();

            Debug.Log("ssdd");
            GUIUnitSlotExpansion unitSlotExpansion = (GUIUnitSlotExpansion)this.m_cGUIMgr
                .GetGUI(GUI_DEFINE.GUIID_UNITSLOTEXPANSION);
            unitSlotExpansion.Show();
        }
    }

    /// <summary>
    /// 道具槽按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void OnClickDaoJuCaoButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            ShowLockPanel();

            Hiden();

            GUIPropsSlotExpansion propsSlotExpansion = (GUIPropsSlotExpansion)this.m_cGUIMgr
                .GetGUI(GUI_DEFINE.GUIID_PROPSSLOTEXPANSION);
            propsSlotExpansion.Show();
        }
    }

    /// <summary>
    /// 格斗点按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void OnClickGeDouDianButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            ShowLockPanel();

            Hiden();

            GUIFistfightPointRestoration fistfightPointRestoration = (GUIFistfightPointRestoration)this.m_cGUIMgr
                .GetGUI(GUI_DEFINE.GUIID_FISTFIGHTPOINTRESTORATION);
            fistfightPointRestoration.Show();
        }
    }

    /// <summary>
    /// 展示lock面板
    /// </summary>
    private void ShowLockPanel()
    {

        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();
    }

}

