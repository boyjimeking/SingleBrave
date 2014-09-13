
using System.Collections.Generic;
using Game.Base;
using Game.Resource;
using UnityEngine;
using Game.Network;

//  VersionHandle.cs
//  Author: Lu zexi
//  2013-12-11



/// <summary>
/// 版本句柄
/// </summary>
public class VersionHandle : HTTPHandleBase
{
    /// <summary>
    /// 获取Action
    /// </summary>
    /// <returns></returns>
    public override string GetAction()
    {
        return PACKET_DEFINE.VERSION_REQ;
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public override bool Excute(HTTPPacketBase packet)
    {
        VersionAckPkt pkt = (VersionAckPkt)packet;

        GUI_FUNCTION.LOADING_HIDEN();

        if (pkt.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, pkt.m_strErrorDes);
            return false;
        }

        //记录资源地址
        Debug.Log(pkt.m_strResTxtPath + " path");
        GAME_DEFINE.RES_PATH = pkt.m_strResTxtPath + GAME_DEFINE.RESOURCE_RES_PATH;
        GAME_DEFINE.RESOURCE_MODEL_PATH = pkt.m_strResTxtPath + GAME_DEFINE.RESOURCE_MODEL_PATH;
        GAME_DEFINE.RESOURCE_TEX_PATH = pkt.m_strResTxtPath + GAME_DEFINE.RESOURCE_TEX_PATH;
        GAME_DEFINE.RESOURCE_EFFECT_PATH = pkt.m_strResTxtPath + GAME_DEFINE.RESOURCE_EFFECT_PATH;
        GAME_DEFINE.RESOURCE_AVATAR_PATH = pkt.m_strResTxtPath + GAME_DEFINE.RESOURCE_AVATAR_PATH;
        GAME_DEFINE.RESOURCE_ITEM_PATH = pkt.m_strResTxtPath + GAME_DEFINE.RESOURCE_ITEM_PATH;
        GAME_DEFINE.RES_VERSION = pkt.m_iResVersion;

        if (pkt.m_iVersion != GAME_SETTING.VERSION)
        {
            PlatformManager.GetInstance().UpdateVersion(pkt.m_strVersionPath);
            return true;
        }

        //GAME_DEFINE.RESOURCE_GUI_PATH = "gui";
        //GAME_DEFINE.RESOURCE_EFFECT_PATH = "effect";
        //GAME_DEFINE.RESOURCE_TEX_PATH = "tex";
        //GAME_DEFINE.RESOURCE_MODEL_PATH = "model";
        //GAME_DEFINE.RESOURCE_TABLE_PATH = "table";

        //加载资源文件
        ResourcesManager.GetInstance().ClearProgress();

        GameManager.GetInstance().GetSceneManager().ChangeLoading();
		Debug.Log (GAME_DEFINE.RES_PATH);
        ResourcesManager.GetInstance().LoadResouce(GAME_DEFINE.RES_PATH, 0, -1, "res",null, Game.Resource.RESOURCE_TYPE.WEB_TEXT_STR, Game.Resource.ENCRYPT_TYPE.NORMAL, DownLoadCallBack2);

        return true;
    }


    /// <summary>
    /// 资源下载完成
    /// </summary>
    /// <param name="resName"></param>
    /// <param name="asset"></param>
    private void DownLoadCallBack2(string resName, object asset, object[] arg)
    {
        Debug.Log("ok 2" + resName);
    }

    /// <summary>
    /// 资源下载完成
    /// </summary>
    /// <param name="resName"></param>
    /// <param name="asset"></param>
    private void DownLoadCallBack3(string resName, object asset, object[] arg)
    {
        Debug.Log("ok 3" + resName);

        int version = (int)arg[0];
        string md5 = (string)arg[1];

        Debug.Log(version + " version");
        Debug.Log(md5 + " md5");

        PlayerPrefs.SetString(resName, md5);
        PlayerPrefs.SetInt(resName + "V", version);
        PlayerPrefs.Save();
    }
}
