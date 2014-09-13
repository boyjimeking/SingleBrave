using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Network;

//获取商城钻石购买数据请求类
//Author sunyi
//2014-02-28
public class StoreDiamondGetPktReq : HTTPPacketBase
{
    public StoreDiamondGetPktReq()
    {
        this.m_strAction = PACKET_DEFINE.STORE_DIAMOND_GET_REQ;
    }

    /// <summary>
    /// 获取请求
    /// </summary>
    /// <returns></returns>
    public override string GetRequire()
    {
        return base.GetRequire();
    }
}

