using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;


//  GameJoinPktReq.cs
//  Author: Lu Zexi
//  2014-03-27





/// <summary>
/// 游戏加入请求
/// </summary>
public class GameJoinPktReq : HTTPPacketBase
{
    public string m_strDeviceID;    //设备ID
    public string m_strChannelName; //渠道名

    public GameJoinPktReq()
    {
        this.m_strAction = PACKET_DEFINE.GAME_JOIN_REQ;
    }

    /// <summary>
    /// 后去请求
    /// </summary>
    /// <returns></returns>
    public override string GetRequire()
    {
        string req = "";
        req = "device_id=" + this.m_strDeviceID + "&channel_id=" + this.m_strChannelName;
        return req;
    }

}
