using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;


//  BUILD_EDITOR.cs
//  Author: Lu Zexi
//  2014-04-02




/// <summary>
/// 打包编辑
/// </summary>
public class BUILD_EDITOR
{
    private const string PP_IOS_VER = "1.0.0";
    private const string IOS_VER = "1.0.0";

    /// <summary>
    /// 设置Windows定义
    /// </summary>
    [MenuItem("GameTool/SetDefine/SetWindowsDefine")]
    public static void SetWindowsDefine()
    {
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, "GAME_TEST_LOAD");
    }

    /// <summary>
    /// 设置IOS定义
    /// </summary>
    [MenuItem("GameTool/SetDefine/SetIOSDefine")]
    public static void SetIOSDefine()
    {
        PlayerSettings.bundleIdentifier = "com.youqing.linbingdouzhe";
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iPhone, "IOS");
        //设置IOS的Icon
        Texture2D[] icons = new Texture2D[4];
        icons[0] = AssetDatabase.LoadMainAssetAtPath("Assets/_BF/icon/ios/app_icon_144.PNG") as Texture2D;
        icons[1] = AssetDatabase.LoadMainAssetAtPath("Assets/_BF/icon/ios/app_icon_114.PNG") as Texture2D;
        icons[2] = AssetDatabase.LoadMainAssetAtPath("Assets/_BF/icon/ios/app_icon_72.PNG") as Texture2D;
        icons[3] = AssetDatabase.LoadMainAssetAtPath("Assets/_BF/icon/ios/app_icon_57.PNG") as Texture2D;
        PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.iPhone, icons);
        //设置版本号
        PlayerSettings.bundleVersion = IOS_VER;
    }

    /// <summary>
    /// 设置IOSPP助手
    /// </summary>
    [MenuItem("GameTool/SetDefine/SetIOSPPDefine")]
    public static void SetIOSPPDefine()
    {
        PlayerSettings.bundleIdentifier = "com.youqing.linbingdouzhePP";
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iPhone, "IOSPP");  //设置全局脚步预定义
        //设置IOSPP的Icon
        Texture2D[] icons = new Texture2D[4];
        icons[0] = AssetDatabase.LoadMainAssetAtPath("Assets/_BF/icon/iospp/app_icon_144.PNG") as Texture2D;
        icons[1] = AssetDatabase.LoadMainAssetAtPath("Assets/_BF/icon/iospp/app_icon_114.PNG") as Texture2D;
        icons[2] = AssetDatabase.LoadMainAssetAtPath("Assets/_BF/icon/iospp/app_icon_72.PNG") as Texture2D;
        icons[3] = AssetDatabase.LoadMainAssetAtPath("Assets/_BF/icon/iospp/app_icon_57.PNG") as Texture2D;
        PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.iPhone, icons);
        //设置版本号
        PlayerSettings.bundleVersion = PP_IOS_VER;
    }

    /// <summary>
    /// 设置Android定义
    /// </summary>
    [MenuItem("GameTool/SetDefine/SetAndroidDefine")]
    public static void SetAndroidDefine()
    {
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "ANDROID");
    }

    /// <summary>
    /// 设置windows测试定义
    /// </summary>
    [MenuItem("GameTool/SetDefine/SetWindowsTestDefine")]
    public static void SetWindowsTestDefine()
    {
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, "GAME_TEST_LOAD;GAME_TEST");
    }
}