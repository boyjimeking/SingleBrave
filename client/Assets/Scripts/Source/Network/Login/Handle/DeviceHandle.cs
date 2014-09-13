using System;
using System.Collections.Generic;
using Game.Network;


//  DeviceHandle.cs
//  Author: Lu Zexi
//  2014-04-24



/// <summary>
/// 设备号句柄
/// </summary>
public class DeviceHandle : HTTPHandleBase
{

    /// <summary>
    /// 获取action
    /// </summary>
    /// <returns></returns>
    public override string GetAction()
    {
        return PACKET_DEFINE.DEVICE_REQ;
    }

    public override bool Excute(HTTPPacketBase packet)
    {
        DevicePktAck ack = packet as DevicePktAck;

        GUI_FUNCTION.LOADING_HIDEN();

        if (packet.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, packet.m_strErrorDes);
            return false;
        }

        GAME_SETTING.DEVICE_ID = ack.m_strDeviceID;
        GAME_SETTING.SaveGAME_JOIN();
        SendAgent.SendGameJoin(PlatformManager.GetInstance().GetDeviceID(), PlatformManager.GetInstance().GetChannelName());

        return true;

    }
}
