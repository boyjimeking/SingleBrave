using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Network;

//  SignatureUpdateHandle.cs
//  Author: sanvey
//  2013-12-23

//获取物品请求应答句柄
public class PlayerSignatureUpdateHandle : HTTPHandleBase
{
    /// <summary>
    /// 获得Action
    /// </summary>
    /// <returns></returns>
    public override string GetAction()
    {
        return PACKET_DEFINE.SIGN_UPDATE_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public override bool Excute(HTTPPacketBase packet)
    {
        PlayerSignatureUpdatePktAck ack = (PlayerSignatureUpdatePktAck)packet;

        GAME_LOG.LOG("code :" + ack.m_iErrorCode);
        GAME_LOG.LOG("desc :" + ack.m_strErrorDes);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return false;
        }

        Role.role.GetBaseProperty().m_strSignature = ack.m_strSign;

        return true;
    }
}


