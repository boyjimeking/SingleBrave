using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

using UnityEngine;

//  MainApplication.cs
//  Author: Lu Zexi
//  2013-11-12



/// <summary>
/// 主类
/// </summary>
public class MainApplication : MonoBehaviour
{
    public delegate IEnumerator CoroutineFunction(params object[] args);    //回调委托方法
    public delegate void FUNC1();   //回调方法一
    public delegate void FUNC2(object[] arg);   //回调方法二
    private static MainApplication s_cInstance; //静态实例
    private UILabel m_cFPS; //FPS
    private float m_fFPSStartTime;  //FPS开始时间
    private int m_iFrameCount;  //帧数
    private int m_iFPS; //FPS
    private const float FPS_TIME = 1.5f; //FPS间隔
    private const int FPS_TARGET = 30;  //目标帧数

    /// <summary>
    /// 获取静态实例
    /// </summary>
    /// <returns></returns>
    public static MainApplication GetInstance()
    {
        return s_cInstance;
    }


    /// <summary>
    /// 构建
    /// </summary>
    void Awake()
    {
        Application.targetFrameRate = FPS_TARGET;

        s_cInstance = this;

        float defaultWHRate = 640f / 960f;
        float ScreenWHRate = (float)Screen.width / (float)Screen.height;
        bool isUseHResize = defaultWHRate >= ScreenWHRate ? false : true;
        UIRoot root = GameObject.Find("ROOT").GetComponent<UIRoot>();
        UIRoot effectRoot = GameObject.Find("GUI_EFFECT").GetComponent<UIRoot>();
        if (!isUseHResize)
        {
            float curScreenH = (float)Screen.width / defaultWHRate;
            float Hrate = curScreenH / Screen.height;
            root.manualHeight = (int)(960f / Hrate);
            effectRoot.manualHeight = (int)(960f / Hrate);
        }
        else
        {
            root.manualHeight = 960;
            effectRoot.manualHeight = 960;
        }

        //Platform GameObject
#if IOS
        GameObject ios = new GameObject("IOS");
        ios.AddComponent<IOSBehaver>();
#endif
#if IOSPP
        GameObject ios = new GameObject("IOSPPP");
        ios.AddComponent<IOSPPBehaver>();
#endif
    }

    //Game.Gfx.GfxObject gfx;
    /// <summary>
    /// 开始
    /// </summary>
    void Start()
    {
        Caching.maximumAvailableDiskSpace = 800 * 1024 * 1024;  //800兆最大
        this.m_cFPS = GUI_FINDATION.FIND_GAME_OBJECT("ROOT/ANCHOR_CENTER/BLACK_LEFT/FPS").GetComponent<UILabel>();
        this.m_cFPS.enabled = false;
#if GAME_TEST
        this.m_cFPS.enabled = true;
        this.m_fFPSStartTime = Time.realtimeSinceStartup;
        this.m_iFrameCount = Time.frameCount;
        this.m_iFPS = 30;
#endif
        GameManager.GetInstance().Initialize();
    }

    /// <summary>
    /// 渲染更新
    /// </summary>
    void Update()
    {
#if GAME_TEST
        float disTime = Time.realtimeSinceStartup - this.m_fFPSStartTime;
        if (disTime > FPS_TIME)
        {
            this.m_iFPS = (int)((Time.frameCount - this.m_iFrameCount) / (Time.realtimeSinceStartup - this.m_fFPSStartTime));
            this.m_fFPSStartTime = Time.realtimeSinceStartup;
            this.m_iFrameCount = Time.frameCount;
        }

        this.m_cFPS.text = "FPS " + this.m_iFPS;
        this.m_cFPS.text += "\n" + "PID: " + Role.role.GetBaseProperty().m_iPlayerId;
        this.m_cFPS.text += "\n" + "缓存: " + Caching.spaceOccupied;
        this.m_cFPS.text += "\n" + "版本号: " + GAME_SETTING.VERSION;
#endif
        GameManager.GetInstance().Render();
    }

    /// <summary>
    /// 延迟渲染
    /// </summary>
    void LateUpdate()
    {
        GameManager.GetInstance().LateRender();
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    void FixedUpdate()
    {
        GameManager.GetInstance().Update();
    }

    /// <summary>
    /// 测试GUI
    /// </summary>
    void OnGUI()
    {
#if GAME_TEST
        if (GUI.Button(new Rect(0, 0, 100, 30), "Clear Setting"))
        {
            GAME_SETTING.ClearSetting();
            Caching.CleanCache();
        }
#endif
    }

    /// <summary>
    /// 销毁
    /// </summary>
    void OnDestroy()
    {
        GameManager.GetInstance().Destory();
    }

    /// <summary>
    /// 退出
    /// </summary>
    void OnApplicationQuit()
    {
        GameManager.GetInstance().OnApplicationQuit();
    }

    /// <summary>
    /// 程序中断
    /// </summary>
    void OnApplicationPause(bool isPause)
    {
        GameManager.GetInstance().OnApplicationPause(isPause);
    }

    /// <summary>
    /// 协程运行回调
    /// </summary>
    /// <param name="cal"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public void GameCoroutine(CoroutineFunction cal, params object[] args)
    {
        StartCoroutine(cal(args));
    }

    /// <summary>
    /// 时间延迟事件
    /// </summary>
    public void TIME_EVENT( float time , FUNC1 func1 )
    {
        StartCoroutine(WaitTime(time,func1));
    }

    /// <summary>
    /// 时间延迟事件
    /// </summary>
    /// <param name="?"></param>
    /// <param name="func2"></param>
    public void TIME_EVENT( float time , FUNC2 func2 , object[] arg )
    {
        StartCoroutine(WaitTime(time, func2 , arg));
    }

    /// <summary>
    /// 等待时间
    /// </summary>
    /// <param name="time"></param>
    /// <param name="func1"></param>
    /// <returns></returns>
    private IEnumerator WaitTime(float time, FUNC1 func1)
    {
        yield return new WaitForSeconds(time);
        func1();
    }

    /// <summary>
    /// 等待时间
    /// </summary>
    /// <param name="time"></param>
    /// <param name="func2"></param>
    /// <param name="arg"></param>
    /// <returns></returns>
    private IEnumerator WaitTime(float time, FUNC2 func2 , object[] arg)
    {
        yield return new WaitForSeconds(time);
        func2(arg);
    }
}

