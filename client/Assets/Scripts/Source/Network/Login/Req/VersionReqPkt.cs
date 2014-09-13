using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;


//  VersionReqPkt.cs
//  Author: Lu Zexi
//  2013-12-11



/// <summary>
/// 版本请求数据包
/// </summary>
public class VersionReqPkt : HTTPPacketBase
{
    public VersionReqPkt()
    {
        this.m_strAction = PACKET_DEFINE.VERSION_REQ;
    }

    /// <summary>
    /// 获取请求
    /// </summary>
    /// <returns></returns>
    public override string GetRequire()
    {
        string req = "" + DateTime.Now.Ticks;
        return req;
    }
}
