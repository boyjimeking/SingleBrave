using System;
using System.Collections.Generic;
using Game.Network;

//  DevicePktReq.cs
//  Author: Lu Zexi
//  2014-04-24




/// <summary>
/// 设备号请求
/// </summary>
public class DevicePktReq : HTTPPacketBase
{

    public DevicePktReq()
        : base()
    {
        this.m_strAction = PACKET_DEFINE.DEVICE_REQ;
    }
}
