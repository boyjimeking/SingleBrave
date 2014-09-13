using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;
using UnityEngine;


//  AccountIOSPPLoginHandle.cs
//  Author: Lu Zexi
//  2014-04-04



/// <summary>
/// 帐号IOSPP助手登录句柄
/// </summary>
public class AccountIOSPPLoginHandle : HTTPHandleBase
{
    /// <summary>
    /// 获取ACTION
    /// </summary>
    /// <returns></returns>
    public override string GetAction()
    {
        return PACKET_DEFINE.ACCOUNT_IOS_PP_LOGIN;
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public override bool Excute(HTTPPacketBase packet)
    {
        AccountLoginIOSPPPktAck ack = packet as AccountLoginIOSPPPktAck;

        GUI_FUNCTION.LOADING_HIDEN();
        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return false;
        }

        if (ack.m_iResult == 0) //返回成功
        {
            GAME_DEFINE.s_strToken = ack.m_strToken;
            GAME_SETTING.s_iUID = ack.m_iUid;

            GAME_SETTING.SaveSetting();

            SendAgent.SendVersionReq();
        }
        else
        {
            Debug.Log("PP Login Result : " + ack.m_iResult);
        }

        return true;
    }
}
