using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

//道具槽扩展
//Author Sunyi
//2013-12-27
public class PlayerPropsSlotExpansionPktReq : HTTPPacketBase
{
    public int m_iPid;

    public PlayerPropsSlotExpansionPktReq()
    {
        this.m_strAction = PACKET_DEFINE.PROPS_EXPANSION_REQ;
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

