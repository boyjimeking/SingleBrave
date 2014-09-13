using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using Game.Resource;

//战斗菜单-设定
//Author sunyi
//2014-3-25
public class GUIBattleMenuSetting : GUIBase
{
    private const string RES_MAIN = "GUI_BattleMenuSetting";//主资源地址

    private const string CLIPVIEW = "BattleSetting/ScrollView";//滚动视图地址

    private const string PANEL_SETTINGSCROLLVIEW = "BattleSetting/ScrollView";//战斗设定中滚动panel地址
    private const string BTLSETTING_BGM_SLIDE = "BattleSetting/ScrollView/ClipView/SoundSettingPanel/Slider_BGM";//BGM音量进度条地址
    private const string BTLSETTING_SE_SLIDE = "BattleSetting/ScrollView/ClipView/SoundSettingPanel/Slider_SE";//SE音量进度条地址
    private const string BTLSETTING_BBPLAYER_BUTTON_ON = "BattleSetting/ScrollView/ClipView/PlaySettingPanel/Button_BBPlay_On";//BB技能音效开启按钮地址
    private const string BTLSETTING_BBPLAYER_BUTTON_OFF = "BattleSetting/ScrollView/ClipView/PlaySettingPanel/Button_BBPlay_Off";//BB技能音效关闭按钮地址
    private const string BTLSETTING_BEIJING_BUTTON_ON = "BattleSetting/ScrollView/ClipView/PlaySettingPanel/Button_Beijing_On";//背景音效开启按钮地址
    private const string BTLSETTING_BEIJING_BUTTON_OFF = "BattleSetting/ScrollView/ClipView/PlaySettingPanel/Button_Beijing_Off";//背景音效关闭按钮地址
    private const string BTLSETTING_BATTLE_BUTTON_ON = "BattleSetting/ScrollView/ClipView/PlaySettingPanel/Button_Zhandou_On";//战斗音效开启地址
    private const string BTLSETTING_BATTLE_BUTTON_OFF = "BattleSetting/ScrollView/ClipView/PlaySettingPanel/Button_Zhandou_Off";//战斗音效关闭按钮地址
    private const string BTLSETTING_BUTTON_BG = "Background";//开关按钮的背景图片地址

    private UIPanel m_cScrollViewPanel;//战斗设定滚动面板
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

    private bool m_bIsFirstGetHero = true;//是否是第一次展示获得英雄面板
    private bool m_bIsFirstGetItem = true;//是否是第一次展示获得物品面板
    private bool m_bIsBBSoundOn = false;//BB技能音效开启状态
    private bool m_bIsBgSoundOn = false;//背景音效开启状态
    private bool m_bIsBattleSoundOn = false;//战斗音效开启状态

    private GUIBattle m_cGUIBattle; //战斗UI

    public GUIBattleMenuSetting(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_BATTLE_MENU_SETTING, GUILAYER.GUI_PANEL3)
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
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_MAIN);
        }
        else
        {
            InitGUI();
        }
    }

    /// <summary>
    /// 创建GUI
    /// </summary>
    protected override void InitGUI()
    {
        base.Show();

        GUI_FUNCTION.AYSNCLOADING_HIDEN();

        if (this.m_cGUIObject == null)
        {
            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

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

            this.m_cScrollViewPanel = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(this.m_cGUIObject, PANEL_SETTINGSCROLLVIEW);

            this.m_cClipView = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, CLIPVIEW);
            this.m_cClipView.transform.localScale = Vector3.one;

            this.m_cBtnBBSoundOnBg = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cBtnBBSoundOn, BTLSETTING_BUTTON_BG);
            this.m_cBtnBBSoundOffBg = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cBtnBBSoundOff, BTLSETTING_BUTTON_BG);
            this.m_cBtnBgSoundOnBg = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cBtnBgSoundOn, BTLSETTING_BUTTON_BG);
            this.m_cBtnBgSoundOffBg = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cBtnBgSoundOff, BTLSETTING_BUTTON_BG);
            this.m_cBtnBattleSoundOnBg = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cBtnBattleSoundOn, BTLSETTING_BUTTON_BG);
            this.m_cBtnBattleSoundOffBg = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cBtnBattleSoundOff, BTLSETTING_BUTTON_BG);
        }

        this.m_cClipView.transform.localPosition = new Vector3(0, -13, 0);
        UIPanel panel = GUI_FINDATION.GET_OBJ_COMPONENT<UIPanel>(this.m_cGUIObject, CLIPVIEW);
        float y = -53.6f;
        panel.clipRange = new Vector4(panel.clipRange.x, y, panel.clipRange.z, panel.clipRange.w);

        SetSlideValues();//设置BGM和SE音量
        this.m_bIsBBSoundOn = GAME_SETTING.s_bSKEffectSwitch;
        this.m_bIsBgSoundOn = GAME_SETTING.s_bENEffectSwitch;
        this.m_bIsBattleSoundOn = GAME_SETTING.s_bATEffectSwitch;
        SetBBSoundBtnBg(this.m_bIsBBSoundOn);
        SetBGSoundBtnBg(this.m_bIsBgSoundOn);
        SetBattleSoundBtnBg(this.m_bIsBattleSoundOn);

        SetLocalPos(Vector3.zero);

    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        GAME_SETTING.SaveSetting();

        ResourcesManager.GetInstance().UnloadResource(RES_MAIN);

        Destory();
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
                if (ResourcesManager.GetInstance().GetProgress() >= 1f && ResourcesManager.GetInstance().IsComplete())
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
        base.Hiden();

        this.m_cClipView = null;
        this.m_cSlideBGM = null;
        this.m_cSlideSE = null;

        this.m_cBtnBBSoundOn = null;
        this.m_cBtnBBSoundOff = null;
        this.m_cBtnBgSoundOn = null;
        this.m_cBtnBgSoundOff = null;
        this.m_cBtnBattleSoundOn = null;
        this.m_cBtnBattleSoundOff = null;
        this.m_cBtnBBSoundOnBg = null;
        this.m_cBtnBBSoundOffBg = null;
        this.m_cBtnBgSoundOnBg = null;
        this.m_cBtnBgSoundOffBg = null;
        this.m_cBtnBattleSoundOnBg = null;
        this.m_cBtnBattleSoundOffBg = null;

        base.Destory();
    }
    /// <summary>
    /// 设置战斗GUI
    /// </summary>
    /// <param name="gui"></param>
    public void SetBattleGUI(GUIBattle gui)
    {
        this.m_cGUIBattle = gui;
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
            SoundManager.GetInstance().SetBGMVolume(GAME_SETTING.s_fBGM_Volume);
        });
        EventDelegate.Add(this.m_cSlideSE.onChange, delegate()
        {
            this.m_fSlideSEValue = this.m_cSlideSE.value;
            GAME_SETTING.s_fSE_Volume = this.m_fSlideSEValue;
            Debug.Log(this.m_fSlideSEValue);
        });

        GAME_SETTING.SaveSetting();
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
            GAME_SETTING.s_bENEffectSwitch = isOn;
            this.m_cGUIBattle.SwitchSceneEffect(isOn);
            GAME_SETTING.SaveSetting();
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
        }
        else
        {
            this.m_cBtnBgSoundOnBg.spriteName = "btn_ON";
            this.m_cBtnBgSoundOffBg.spriteName = "dark-btn_OFF";
        }
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
}

