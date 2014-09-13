using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

//竞技点恢复请求类
//Author Sunyi
//2013-12-27
public class PlayerSportPointRecoverPktReq : HTTPPacketBase
{
    public int m_iPid;

    public PlayerSportPointRecoverPktReq()
    {
        this.m_strAction = PACKET_DEFINE.BATTLEPOING_RECOVER_REQ;
    }

    /// <summary>
    /// 获取请求参数
    /// </summary>
    /// <returns></returns>
    public override string GetRequire()
    {
        string req = string.Format("pid={0}", this.m_iPid);

        PACKET_HEAD.PACKET_REQ_END(ref req);

        return req;
    }
}

