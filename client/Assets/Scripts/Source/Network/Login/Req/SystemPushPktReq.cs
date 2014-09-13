
using System.Collections.Generic;
using Game.Network;



//  SystemPushPktReq.cs
//  Author: Lu Zexi
//  2014-02-24



/// <summary>
/// 系统推送
/// </summary>
public class SystemPushPktReq : HTTPPacketBase
{
    public int m_iPID;  //玩家ID

    public SystemPushPktReq()
    {
        this.m_strAction = PACKET_DEFINE.SYSTEM_PUSH_REQ;
    }

    /// <summary>
    /// 获取请求
    /// </summary>
    /// <returns></returns>
    public override string GetRequire()
    {
        string req = "pid=" + this.m_iPID;

        PACKET_HEAD.PACKET_REQ_END(ref req);

        return req;
    }

}
