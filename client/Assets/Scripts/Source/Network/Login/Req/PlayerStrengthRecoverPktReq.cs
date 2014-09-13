using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

//商城-体力恢复接口请求包
//Author: sunyi
//2013-12-27
public class PlayerStrengthRecoverPktReq : HTTPPacketRequest
{
    public int m_iPid;

    public PlayerStrengthRecoverPktReq()
    {
        this.m_strAction = PACKET_DEFINE.STRENGTH_RECOVER_REQ;
    }

    // /// <summary>
    // /// 获取请求参数
    // /// </summary>
    // /// <returns></returns>
    // public override string GetRequire()
    // {
    //     string req = string.Format("pid={0}", this.m_iPid);

    //     PACKET_HEAD.PACKET_REQ_END(ref req);

    //     return req;
    // }
}

