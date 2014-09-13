using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

//获取活动副本优惠类型请求类
//Author:sunyi
//2014-1-10
public class ActivityFubenFavourablePktReq : HTTPPacketBase
{
    public int m_iPid;

    public ActivityFubenFavourablePktReq()
    {
        this.m_strAction = PACKET_DEFINE.ACTIVITY_FUBEN_FAVOURABLE_REQ;
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

