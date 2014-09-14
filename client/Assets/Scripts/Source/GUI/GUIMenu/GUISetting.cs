using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using Game.Resource;
using Game.Media;

//设定类
//Author:Sunyi
//2013-11-27

public class GUISetting : GUIBase
{

    private const string RES_MAIN = "GUI_Setting";//主资源地址

    private const string TOPPANEL = "TopPanel";//导航栏地址
    private const string MAINPANEL = "ClipView";//主面板地址
    private const string BACKBUTTON = "TopPanel/Button_Back";//返回按钮地址

    private const string BTLSETTING_BGM_SLIDE = "ClipView/ScrollView/ClipView/SoundSettingPanel/Slider_BGM";//BGM音量进度条地址
    private const string BTLSETTING_SE_SLIDE = "ClipView/ScrollView/ClipView/SoundSettingPanel/Slider_SE";//SE音量进度条地址
    private const string BTLSETTING_BBPLAYER_BUTTON_ON = "ClipView/ScrollView/ClipView/PlaySettingPanel/Button_BBPlay_On";//BB技能音效开启按钮地址
    private const string BTLSETTING_BBPLAYER_BUTTON_OFF = "ClipView/ScrollView/ClipView/PlaySettingPanel/Button_BBPlay_Off";//BB技能音效关闭按钮地址
    private const string BTLSETTING_BEIJING_BUTTON_ON = "ClipView/ScrollView/ClipView/PlaySettingPanel/Button_Beijing_On";//背景音效开启按钮地址
    private const string BTLSETTING_BEIJING_BUTTON_OFF = "ClipView/ScrollView/ClipView/PlaySettingPanel/Button_Beijing_Off";//背景音效关闭按钮地址
    private const string BTLSETTING_BATTLE_BUTTON_ON = "ClipView/ScrollView/ClipView/PlaySettingPanel/Button_Zhandou_On";//战斗音效开启地址
    private const string BTLSETTING_BATTLE_BUTTON_OFF = "ClipView/ScrollView/ClipView/PlaySettingPanel/Button_Zhandou_Off";//战斗音效关闭按钮地址
    private const string CLIPVIEW = "ClipView/ScrollView";//滚动视图地址
    private const string BTLSETTING_BUTTON_BG = "Background";//开关按钮的背景图片地址

    private GameObject m_cTopPanel;//导航栏

    private GameObject m_cMainPanel;//主面板
    private GameObject m_cBtnBack;//返回按钮
    private GameObject m_cClipView;//滚动视图

    private UISlider m_cSlideBGM;//BGM音量控制
    private UISlider m_cSlideSE;//SE音量控制

    private GameObject m_cBtnBBSoundOn;//BB技能音效开启按钮
    private GameObject m_cBtnBBSoundOff;//BB技能音效关闭按钮
    private GameObject m_cBtnBgSoundOn;//背景音效开启按钮
    private GameObject m_cBtnBgSoundOff;//背景音效关闭按钮
    private GameObject m_cBtnBattleSoundOn;//战斗音效开启按钮
    private GameObject m_cBtnBattleSoundOff;//战斗音效关闭按钮
    private UISprite m_cBtnBBSoundOnBg;//BB技能音效开启按钮背景
    private UISprite m_cBtnBBSoundOffBg;//BB技能音效关闭按钮背景
    private UISprite m_cBtnBgSoundOnBg;//背景音效开启按钮背景
    private UISprite m_cBtnBgSoundOffBg;//背景音效关闭按钮背景
    private UISprite m_cBtnBattleSoundOnBg;//战斗音效开启按钮背景
    private UISprite m_cBtnBattleSoundOffBg;//战斗音效开启按钮背景

    private float m_fSlideSEValue = 0.5f;//SE音量大小
    private float m_fSlideBGMValue = 0.5f;//BGM音量大小

    private bool m_bIsBBSoundOn = false;//BB技能音效开启状态
    private bool m_bIsBgSoundOn = false;//背景音效开启状态
    private bool m_bIsBattleSoundOn = false;//战斗音效开启状态

    public GUISetting(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_SETTING, GUILAYER.GUI_PANEL)
    { }

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
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            this.m_cTopPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, TOPPANEL);
            this.m_cTopPanel.transform.localPosition = new Vector3(-420, 270, 0);

            this.m_cMainPanel = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, MAINPANEL);
            this.m_cMainPanel.transform.localPosition = new Vector3(640, 0, 0);

            this.m_cBtnBack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BACKBUTTON);
            GUIComponentEvent backEvent = this.m_cBtnBack.AddComponent<GUIComponentEvent>();
            backEvent.AddIntputDelegate(OnClickBackButton);

            this.m_cClipView = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, CLIPVIEW);
            this.m_cClipView.transform.localScale = Vector3.one;

            this.m_cSlideBGM = GUI_FINDATION.GET_OBJ_COMPONENT<UISlider>(this.m_cGUIObject, BTLSETTING_BGM_SLIDE);

            this.m_cSlideSE = GUI_FINDATION.GET_OBJ_COMPONENT<UISlider>(this.m_cGUIObject, BTLSETTING_SE_SLIDE);

            this.m_cBtnBBSoundOn = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTLSETTING_BBPLAYER_BUTTON_ON);
            GUIComponentEvent bbSoundOnEvent = this.m_cBtnBBSoundOn.AddComponent<GUIComponentEvent>();
            bbSoundOnEvent.AddIntputDelegate(OnClickBBSoundButton, true);

            this.m_cBtnBBSoundOff = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTLSETTING_BBPLAYER_BUTTON_OFF);
            GUIComponentEvent bbSoundOffEvent = this.m_cBtnBBSoundOff.AddComponent<GUIComponentEvent>();
            bbSoundOffEvent.AddIntputDelegate(OnClickBBSoundButton, false);

            this.m_cBtnBgSoundOn = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTLSETTING_BEIJING_BUTTON_ON);
            GUIComponentEvent bgSoundOnEvent = this.m_cBtnBgSoundOn.AddComponent<GUIComponentEvent>();
            bgSoundOnEvent.AddIntputDelegate(OnClickBgSoundButton, true);

            this.m_cBtnBgSoundOff = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTLSETTING_BEIJING_BUTTON_OFF);
            GUIComponentEvent bgSoundOffEvent = this.m_cBtnBgSoundOff.AddComponent<GUIComponentEvent>();
            bgSoundOffEvent.AddIntputDelegate(OnClickBgSoundButton, false);

            this.m_cBtnBattleSoundOn = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTLSETTING_BATTLE_BUTTON_ON);
            GUIComponentEvent battleSoundOnEvent = this.m_cBtnBattleSoundOn.AddComponent<GUIComponentEvent>();
            battleSoundOnEvent.AddIntputDelegate(OnClickBattleSoundButton, true);

            this.m_cBtnBattleSoundOff = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTLSETTING_BATTLE_BUTTON_OFF);
            GUIComponentEvent battleSoundOffEvent = this.m_cBtnBattleSoundOff.AddComponent<GUIComponentEvent>();
            battleSoundOffEvent.AddIntputDelegate(OnClickBattleSoundButton, false);

            this.m_cBtnBBSoundOnBg = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cBtnBBSoundOn, BTLSETTING_BUTTON_BG);
            this.m_cBtnBBSoundOffBg = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cBtnBBSoundOff, BTLSETTING_BUTTON_BG);
            this.m_cBtnBgSoundOnBg = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cBtnBgSoundOn, BTLSETTING_BUTTON_BG);
            this.m_cBtnBgSoundOffBg = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cBtnBgSoundOff, BTLSETTING_BUTTON_BG);
            this.m_cBtnBattleSoundOnBg = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cBtnBattleSoundOn, BTLSETTING_BUTTON_BG);
            this.m_cBtnBattleSoundOffBg = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cBtnBattleSoundOff, BTLSETTING_BUTTON_BG);
        }

        this.m_cClipView.transform.localPosition = new Vector3(0, -123, 0);
        UIPanel panel = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(this.m_cGUIObject, CLIPVIEW);
        float y = 69.0f;
        panel.clipRange = new Vector4(panel.clipRange.x, y, panel.clipRange.z, panel.clipRange.w);

        SetSlideValues();//设置BGM和SE音量
        this.m_bIsBBSoundOn = GAME_SETTING.s_bSKEffectSwitch;
        this.m_bIsBgSoundOn = GAME_SETTING.s_bENEffectSwitch;
        this.m_bIsBattleSoundOn = GAME_SETTING.s_bATEffectSwitch;
        SetBBSoundBtnBg(this.m_bIsBBSoundOn);
        SetBGSoundBtnBg(this.m_bIsBgSoundOn);
        SetBattleSoundBtnBg(this.m_bIsBattleSoundOn);

        this.m_cGUIMgr.SetCurGUIID(this.m_iID);

        SetLocalPos(Vector3.zero);

        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(-420, 270, 0), new Vector3(0, 270, 0));
        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEOUT_GUI_TIME, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(640, 0, 0), Vector3.zero);

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_SYS_SETTING));
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //base.Hiden();

        GAME_SETTING.SaveSetting();
        GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

        CTween.TweenPosition(this.m_cTopPanel, GAME_DEFINE.FADEIN_GUI_TIME, new Vector3(0, 270, 0), new Vector3(-420, 270, 0));
        CTween.TweenPosition(this.m_cMainPanel, GAME_DEFINE.FADEIN_GUI_TIME, Vector3.zero, new Vector3(640, 0, 0) , Destory);

        ResourceMgr.UnloadUnusedResources();
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
    /// 返回按钮点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickBackButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            GUIMenu menu = (GUIMenu)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_MENU);
            menu.Show();
        }
    }

    /// <summary>
    /// 设置BGM和SE音量
    /// </summary>
    private void SetSlideValues()
    {
        this.m_fSlideSEValue = GAME_SETTING.s_fSE_Volume;
        this.m_fSlideBGMValue = GAME_SETTING.s_fBGM_Volume;

        this.m_cSlideSE.value = this.m_fSlideSEValue;
        this.m_cSlideBGM.value = this.m_fSlideBGMValue;

        EventDelegate.Add(this.m_cSlideBGM.onChange, delegate()
        {
            this.m_fSlideBGMValue = this.m_cSlideBGM.value;
            GAME_SETTING.s_fBGM_Volume = this.m_fSlideBGMValue;
			MediaMgr.sInstance.BGM_VOLUME = GAME_SETTING.s_fBGM_Volume;
//            MediaMgr.SetBGMVolume(GAME_SETTING.s_fBGM_Volume);
        });
        EventDelegate.Add(this.m_cSlideSE.onChange, delegate()
        {
            this.m_fSlideSEValue = this.m_cSlideSE.value;
            GAME_SETTING.s_fSE_Volume = this.m_fSlideSEValue;
            Debug.Log(this.m_fSlideSEValue);
        });

    }

    /// <summary>
    /// 开启或者关闭BB技能音效
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickBBSoundButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            bool isOn = (bool)args[0];
            if (isOn)
            {
                SetBBSoundBtnBg(isOn);
            }
            else
            {
                SetBBSoundBtnBg(isOn);
            }
        }
    }

    /// <summary>
    /// 开启或者关闭背景音效
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickBgSoundButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            bool isOn = (bool)args[0];
            if (isOn)
            {
                SetBGSoundBtnBg(isOn);
            }
            else
            {
                SetBGSoundBtnBg(isOn);
            }
        }
    }


    /// <summary>
    /// 开启或者关闭战斗音效
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickBattleSoundButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            bool isOn = (bool)args[0];
            if (isOn)
            {
                SetBattleSoundBtnBg(isOn);
            }
            else
            {
                SetBattleSoundBtnBg(isOn);
            }
        }
    }

    /// <summary>
    /// 设置BB技能开关按钮背景图片
    /// </summary>
    /// <param name="isOn"></param>
    private void SetBBSoundBtnBg(bool isOn)
    {
        if (isOn)
        {
            Debug.Log("BB ON");
            this.m_cBtnBBSoundOnBg.spriteName = "dark-btn_ON";
            this.m_cBtnBBSoundOffBg.spriteName = "btn_OFF";
            GAME_SETTING.s_bSKEffectSwitch = true;
        }
        else
        {
            Debug.Log("BB OFF");
            this.m_cBtnBBSoundOnBg.spriteName = "btn_ON";
            this.m_cBtnBBSoundOffBg.spriteName = "dark-btn_OFF";
            GAME_SETTING.s_bSKEffectSwitch = false;
        }
        GAME_SETTING.SaveSetting();
    }

    /// <summary>
    /// 设置背景音效开关按钮背景图片
    /// </summary>
    /// <param name="isOn"></param>
    private void SetBGSoundBtnBg(bool isOn)
    {
        if (isOn)
        {
            Debug.Log("BG ON");
            this.m_cBtnBgSoundOnBg.spriteName = "dark-btn_ON";
            this.m_cBtnBgSoundOffBg.spriteName = "btn_OFF";
            GAME_SETTING.s_bENEffectSwitch = true;
        }
        else
        {
            Debug.Log("BG OFF");
            this.m_cBtnBgSoundOnBg.spriteName = "btn_ON";
            this.m_cBtnBgSoundOffBg.spriteName = "dark-btn_OFF";
            GAME_SETTING.s_bENEffectSwitch = false;
        }
        GAME_SETTING.SaveSetting();
    }

    /// <summary>
    /// 设置战斗音效开关按钮背景图片
    /// </summary>
    /// <param name="isOn"></param>
    private void SetBattleSoundBtnBg(bool isOn)
    {
        if (isOn)
        {
            Debug.Log("BA ON");
            this.m_cBtnBattleSoundOnBg.spriteName = "dark-btn_ON";
            this.m_cBtnBattleSoundOffBg.spriteName = "btn_OFF";
            GAME_SETTING.s_bATEffectSwitch = true;
        }
        else
        {
            Debug.Log("BA OFF");
            this.m_cBtnBattleSoundOnBg.spriteName = "btn_ON";
            this.m_cBtnBattleSoundOffBg.spriteName = "dark-btn_OFF";
            GAME_SETTING.s_bATEffectSwitch = false;
        }
        GAME_SETTING.SaveSetting();
    }

    /// <summary>
    /// 逻辑更新
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
}

