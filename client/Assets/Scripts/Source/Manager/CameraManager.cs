using System;
using System.Collections.Generic;
using Game.Base;
using UnityEngine;


//  CameraManager.cs
//  Author: Lu Zexi
//  2013-11-26




/// <summary>
/// 摄像头管理类
/// </summary>
public class CameraManager : Singleton<CameraManager>
{
    private const string BATTLE_PVP_CAMERA = "BATTLE_PVP/BATTLE_CAMERA";    //PVP摄像头
    private const string UIMODEL_CAMERA = "BATTLE_EDITOR/UIMODELCAMERA"; //UI模型摄像头
    private const string BATTLE3D_CAMERA = "BATTLE/BATTLE_CAMERA";    //战斗3D摄像头
    private const string UIHERO_UPGRADE = "HERO_LEVEL_UP/UIMODELCAMERA";   //英雄强化
    private const string UIHERO_EVOLUTION = "HERO_JINHUA/UIMODELCAMERA";   //英雄进化
    private const string UIHERO_SHOW = "HERO_SHOW/BATTLE_CAMERA";   //英雄图鉴
    private const string UIHERO_ZHAOHUAN = "ZHAOHUAN/Main Camera";   //英雄召唤门动画展示
    private const string UIHERO_CREATE = "HERO_CREATE/UIMODELCAMERA";    //英雄创建
    private const string UIHERO_ZHAOHUAN2 = "ZHAOHUAN/UIMODELCAMERA"; //英雄召唤模型部分展示
    private const string UIHERO_CHOOSE = "HERO_CHOOSE/UIMODELCAMERA";//角色选择模型展示
    private const string VILLAGE_CAMERA = "VILLAGE/UIMODELCAMERA";  //村庄摄像头
    private const string GUI_EFFECT_CAMERA = "GUI_EFFECT/Camera";//3D特效摄像头
    private const string GUI_ROOT_CAMERA = "ROOT/CAMERA";  //root ngui 摄像头

    private Camera m_cGUICamera;  //NGUI 摄像头
    private Camera m_cBattlePVPCamera;    //PVP摄像头
    private Camera m_cUIModelCamera;    //UI模型摄像头
    private Camera m_cBattle3DCamera;   //战场3D摄像头
    private Camera m_cUIHeroUpgradeCamera;    //英雄强化摄像头
    private Camera m_cUIHeroEvolutionCamera;  //英雄进化摄像头
    private Camera m_cUIHeroShowCamera; //英雄图鉴摄像头
    private Camera m_cUIHeroZhaoHuanCamera; //英雄召唤摄像头
    private Camera m_cUIHeroCreateCamera;   //英雄创建摄像头
    private Camera m_cUIHeroZhaoHuan2Camera;  //英雄召唤模型部分展示
    private Camera m_cUIHeroChooseCamera;//角色选择模型展示摄像头
    private Camera m_cUITownCamera;  //村庄摄像头
    private Camera m_cUIEffectCamera;//3D特效摄像头

    public CameraManager()
    {
        //
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Initialize()
    {
        //GUI摄像头初始化的时候不用隐藏
        this.m_cGUICamera = GameObject.Find(GUI_ROOT_CAMERA).GetComponent<Camera>();

        this.m_cBattlePVPCamera = GameObject.Find(BATTLE_PVP_CAMERA).GetComponent<Camera>();
        HidenBattlePVPCamera();
        this.m_cUIModelCamera = GameObject.Find(UIMODEL_CAMERA).GetComponent<Camera>();
        HidenUIModelCamera();
        this.m_cBattle3DCamera = GameObject.Find(BATTLE3D_CAMERA).GetComponent<Camera>();
        HidenBattle3DCamera();
        this.m_cUIHeroUpgradeCamera = GameObject.Find(UIHERO_UPGRADE).GetComponent<Camera>();
        HidenUIHeroUpgradeCamera();
        this.m_cUIHeroEvolutionCamera = GameObject.Find(UIHERO_EVOLUTION).GetComponent<Camera>();
        HidenUIHeroEvolutionCamera();
        this.m_cUIHeroShowCamera = GameObject.Find(UIHERO_SHOW).GetComponent<Camera>();
        HidenUIHeroShowCamera();
        this.m_cUIHeroZhaoHuanCamera = GameObject.Find(UIHERO_ZHAOHUAN).GetComponent<Camera>();
        HidenUIHeroZhaoHuanCamera();
        this.m_cUIHeroZhaoHuan2Camera = GameObject.Find(UIHERO_ZHAOHUAN2).GetComponent<Camera>();
        HidenUIHeroZhaoHuan2Camera();
        //this.m_cUIHeroCreateCamera = GameObject.Find(UIHERO_CREATE).GetComponent<Camera>();
        //HidenUIHeroCreateCamera();
        this.m_cUIHeroChooseCamera = GameObject.Find(UIHERO_CHOOSE).GetComponent<Camera>();
        HidenUIHeroChooseCamera();
        this.m_cUITownCamera = GameObject.Find(VILLAGE_CAMERA).GetComponent<Camera>();
        HidenUITownCamera();
        this.m_cUIEffectCamera = GameObject.Find(GUI_EFFECT_CAMERA).GetComponent<Camera>();
        HidenGUIEffectCamera();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public void Destory()
    {
        this.m_cUIModelCamera = null;
        this.m_cBattle3DCamera = null;
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public void Update()
    {
    }


    /// <summary>
    /// 获取UI
    /// </summary>
    /// <returns></returns>
    public Camera GetUIModelCamera()
    {
        return this.m_cUIModelCamera;
    }

    /// <summary>
    /// 获取战场模型摄像头
    /// </summary>
    /// <returns></returns>
    public Camera GetBattle3DCamera()
    {
        return this.m_cBattle3DCamera;
    }

    /// <summary>
    /// 获取英雄升级摄像头
    /// </summary>
    /// <returns></returns>
    public Camera GetUIHeroUpgradeCamera()
    {
        return this.m_cUIHeroUpgradeCamera;
    }

    /// <summary>
    /// 获取英雄进化摄像头
    /// </summary>
    /// <returns></returns>
    public Camera GetUIHeroEvolutionCamera()
    {
        return this.m_cUIHeroEvolutionCamera;
    }

    /// <summary>
    /// 隐藏UI模型摄像头
    /// </summary>
    public void HidenUIModelCamera()
    {
        if (this.m_cUIModelCamera != null)
            this.m_cUIModelCamera.enabled = false;
    }

    /// <summary>
    /// 展示UI模型摄像头
    /// </summary>
    public void ShowUIModelCamera()
    {
        if (this.m_cUIModelCamera != null)
            this.m_cUIModelCamera.enabled = true;
    }

    /// <summary>
    /// 隐藏战场3D摄像头
    /// </summary>
    public void HidenBattle3DCamera()
    {
        if (this.m_cBattle3DCamera != null)
            this.m_cBattle3DCamera.enabled = false;
    }

    /// <summary>
    /// 展示战场3D摄像头
    /// </summary>
    public void ShowBattle3DCamera()
    {
        if (this.m_cBattle3DCamera != null)
            this.m_cBattle3DCamera.enabled = true;
    }

    /// <summary>
    /// 展示UI英雄升级摄像头
    /// </summary>
    public void ShowUIHeroUpgradeCamera()
    {
        if (this.m_cUIHeroUpgradeCamera != null)
            this.m_cUIHeroUpgradeCamera.enabled = true;
    }

    /// <summary>
    /// 隐藏UI英雄升级摄像头
    /// </summary>
    public void HidenUIHeroUpgradeCamera()
    {
        if (this.m_cUIHeroUpgradeCamera != null)
            this.m_cUIHeroUpgradeCamera.enabled = false;
    }

    /// <summary>
    /// 展示UI英雄进化摄像头
    /// </summary>
    public void ShowUIHeroEvolutionCamera()
    {
        if (this.m_cUIHeroEvolutionCamera != null)
            this.m_cUIHeroEvolutionCamera.enabled = true;
    }

    /// <summary>
    /// 隐藏UI英雄进化摄像头
    /// </summary>
    public void HidenUIHeroEvolutionCamera()
    {
        if (this.m_cUIHeroEvolutionCamera != null)
            this.m_cUIHeroEvolutionCamera.enabled = false;
    }

    /// <summary>
    /// 展示英雄图鉴摄像头
    /// </summary>
    public void ShowUIHeroShowCamera()
    {
        if (this.m_cUIHeroShowCamera != null)
            this.m_cUIHeroShowCamera.enabled = true;
    }

    /// <summary>
    /// 隐藏英雄图鉴摄像头
    /// </summary>
    public void HidenUIHeroShowCamera()
    {
        if (this.m_cUIHeroShowCamera != null)
            this.m_cUIHeroShowCamera.enabled = false;
    }

    /// <summary>
    /// 展示英雄召唤摄像头
    /// </summary>
    public void ShowUIHeroZhaoHuanCamera()
    {
        if (this.m_cUIHeroZhaoHuanCamera != null)
            this.m_cUIHeroZhaoHuanCamera.enabled = true;
    }

    /// <summary>
    /// 隐藏英雄召唤摄像头
    /// </summary>
    public void HidenUIHeroZhaoHuanCamera()
    {
        if (this.m_cUIHeroZhaoHuanCamera != null)
            this.m_cUIHeroZhaoHuanCamera.enabled = false;
    }

    /// <summary>
    /// 展示战斗PVP摄像头
    /// </summary>
    public void ShowBattlePVPCamera()
    {
        if (this.m_cBattlePVPCamera != null)
            this.m_cBattlePVPCamera.enabled = true;
    }

    /// <summary>
    /// 隐藏战斗PVP摄像头
    /// </summary>
    public void HidenBattlePVPCamera()
    {
        if (this.m_cBattlePVPCamera != null)
            this.m_cBattlePVPCamera.enabled = false;
    }

    /// <summary>
    /// 展示英雄创建摄像头
    /// </summary>
    public void ShowUIHeroCreateCamera()
    {
        if (this.m_cUIHeroCreateCamera != null)
            this.m_cUIHeroCreateCamera.enabled = true;
    }

    /// <summary>
    /// 隐藏英雄创建摄像头
    /// </summary>
    public void HidenUIHeroCreateCamera()
    {
        if (this.m_cUIHeroCreateCamera != null)
            this.m_cUIHeroCreateCamera.enabled = false;
    }

    /// <summary>
    /// 展示英雄召唤模型摄像头
    /// </summary>
    public void ShowUIHeroZhaoHuan2Camera()
    {
        if (this.m_cUIHeroZhaoHuan2Camera != null)
            this.m_cUIHeroZhaoHuan2Camera.enabled = true;
    }

    /// <summary>
    /// 隐藏英雄召唤模型摄像头
    /// </summary>
    public void HidenUIHeroZhaoHuan2Camera()
    {
        if (this.m_cUIHeroZhaoHuan2Camera != null)
            this.m_cUIHeroZhaoHuan2Camera.enabled = false;
    }


    /// <summary>
    /// 展示英雄选择模型摄像头
    /// </summary>
    public void ShowUIHeroChooseCamera()
    {
        if (this.m_cUIHeroChooseCamera != null)
            this.m_cUIHeroChooseCamera.enabled = true;
    }

    /// <summary>
    /// 隐藏英雄选择模型摄像头
    /// </summary>
    public void HidenUIHeroChooseCamera()
    {
        if (this.m_cUIHeroChooseCamera != null)
            this.m_cUIHeroChooseCamera.enabled = false;
    }

    /// <summary>
    /// 展示村庄像头
    /// </summary>
    public void ShowUITownCamera()
    {
        if (this.m_cUITownCamera != null)
            this.m_cUITownCamera.enabled = true;
    }

    /// <summary>
    /// 隐藏村庄摄像头
    /// </summary>
    public void HidenUITownCamera()
    {
        if (this.m_cUITownCamera != null)
            this.m_cUITownCamera.enabled = false;
    }

    /// <summary>
    /// 展示3d摄像头
    /// </summary>
    public void ShowGUIEffectCamera()
    {
        if (this.m_cUIEffectCamera != null)
        {
            this.m_cUIEffectCamera.enabled = true;
        }
    }
    /// <summary>
    /// 隐藏3d摄像头
    /// </summary>
    public void HidenGUIEffectCamera()
    {
        if (this.m_cUIEffectCamera != null)
        {
            this.m_cUIEffectCamera.enabled = false;
        }
    }

    /// <summary>
    /// 隐藏GUI摄像头
    /// </summary>
    public void HidenGUICamera()
    {
        if (this.m_cGUICamera != null)
        {
            this.m_cGUICamera.enabled = false;
        }
    }

    /// <summary>
    /// 展示GUI摄像头
    /// </summary>
    public void ShowGUICamera()
    {
        if (this.m_cGUICamera != null)
        {
            this.m_cGUICamera.enabled = true;
        }
    }
}