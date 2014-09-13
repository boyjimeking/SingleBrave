using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

//单位槽扩张请求类
//Author sunyi
//2013-12-27
public class HeroUnitSlotExpansionPktReq : HTTPPacketBase
{
    public int m_iPid;

    public HeroUnitSlotExpansionPktReq()
    {
        this.m_strAction = PACKET_DEFINE.UNIT_EXPANSION_REQ;
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

