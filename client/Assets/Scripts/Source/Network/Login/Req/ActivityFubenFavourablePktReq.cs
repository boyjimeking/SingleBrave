using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

//获取活动副本优惠类型请求类
//Author:sunyi
//2014-1-10
public class ActivityFubenFavourablePktReq : HTTPPacketRequest
{
    public int m_iPid;

    public ActivityFubenFavourablePktReq()
    {
        this.m_strAction = PACKET_DEFINE.ACTIVITY_FUBEN_FAVOURABLE_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 发送获得特殊副本优惠类型请求
	/// </summary>
	/// <param name="pid"></param>
	public static void SendActivityFubenFavourableEeq(int pid)
	{
		ActivityFubenFavourablePktReq req = new ActivityFubenFavourablePktReq();
		req.m_iPid = pid;
		
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}

