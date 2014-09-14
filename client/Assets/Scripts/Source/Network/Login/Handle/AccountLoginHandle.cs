using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Network;
using Game.Resource;

//  AccountLoginHandle.cs
//  Author: Lu Zexi
//  2013-11-14




/// <summary>
/// 帐号登录请求应答句柄
/// </summary>
public class AccountLoginHandle
{
    /// <summary>
    /// 获取Action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.ACCOUNT_LOGIN_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketAck packet)
    {
        AccountLoginPktAck ack = (AccountLoginPktAck)packet;
        GAME_LOG.LOG("code :" + ack.header.code);
        GAME_LOG.LOG("desc :" + ack.header.desc);
        GAME_LOG.LOG("data uid : " + ack.m_iUid);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.header.code != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.header.desc);
            return;
        }

        GAME_DEFINE.s_strToken = ack.m_strToken;

        GUIAccount gui = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_ACCOUNT) as GUIAccount;
        GAME_SETTING.s_iUID = ack.m_iUid;
        GAME_SETTING.s_bAccountBound = ack.m_bBound;
        GAME_SETTING.s_strUserName = gui.m_strUser;
        GAME_SETTING.s_strPassWord = gui.m_strPassword;
        GAME_SETTING.SaveSetting();

        SendAgent.SendVersionReq();
        //SendAgent.SendPlayerInfoGetPktReq(GAME_SETTING.s_iUID);

        return;
    }
}
