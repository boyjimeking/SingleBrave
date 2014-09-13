using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Network;
using Game.Resource;

//  AccountAutoRegistHandle.cs
//  Author: Lu Zexi
//  2013-11-14




/// <summary>
/// 自动注册请求应答句柄
/// </summary>
public class AccountAutoRegistHandle : HTTPHandleBase
{
    /// <summary>
    /// 获取Action
    /// </summary>
    /// <returns></returns>
    public override string GetAction()
    {
        return PACKET_DEFINE.AUTO_REGIST_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public override bool Excute(HTTPPacketBase packet)
    {
        AccountAutoRegistPktAck ack = (AccountAutoRegistPktAck)packet;
        GAME_LOG.LOG("code :" + ack.m_iErrorCode);
        GAME_LOG.LOG("desc :" + ack.m_strErrorDes);
        GAME_LOG.LOG("data userName : " + ack.m_strUsrName);
        GAME_LOG.LOG("data password : " + ack.m_strPassword);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return false;
        }

        GAME_DEFINE.s_strToken = ack.m_strToken;

        GAME_SETTING.s_strUserName = ack.m_strUsrName;
        GAME_SETTING.s_strPassWord = ack.m_strPassword;
        GAME_SETTING.s_bAccountBound = false;
        GAME_SETTING.s_iUID = ack.m_iUid;
        GAME_SETTING.SaveSetting();

        SendAgent.SendVersionReq();
        //SendAgent.SendPlayerInfoGetPktReq(GAME_SETTING.s_iUID);

        return true;
    }
}
