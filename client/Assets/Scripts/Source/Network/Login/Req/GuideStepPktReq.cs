using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;


//  GuideStepPktReq.cs
//  Author: Lu Zexi
//  2014-02-28



/// <summary>
/// 新手引导数据包请求
/// </summary>
public class GuideStepPktReq : HTTPPacketBase
{
    public int m_iPID;  //玩家ID
    public int m_iGuideID;  //新手引导ID

    public GuideStepPktReq()
    {
        this.m_strAction = PACKET_DEFINE.GUIDE_STEP_REQ;
    }

    /// <summary>
    /// 获取请求
    /// </summary>
    /// <returns></returns>
    public override string GetRequire()
    {
        string req = "";

        req += "pid=" + this.m_iPID + "&guide_id=" + this.m_iGuideID;

        PACKET_HEAD.PACKET_REQ_END(ref req);

        return req;
    }

}
