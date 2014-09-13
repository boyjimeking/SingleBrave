using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

//获取当前任务信息请求类
//Author:sunyi
//2013-12-11

public class PlayerTaskInfoPktReq : HTTPPacketRequest
{
    public int m_iPid;//玩家id

    public PlayerTaskInfoPktReq()
    {
        this.m_strAction = PACKET_DEFINE.GET_PLAYERTASKINFO_REQ;
    }

    // /// <summary>
    // /// 获取请求参数
    // /// </summary>
    // /// <returns></returns>
    // public override string GetRequire()
    // {
    //     string req = string.Format("pid={0}",this.m_iPid);

    //     PACKET_HEAD.PACKET_REQ_END(ref req);

    //     return req;
    // }
}

