using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;


//  FubenStoryPktReq.cs
//  Author: Lu Zexi
//  2014-02-27



/// <summary>
/// 副本剧情请求
/// </summary>
public class FubenStoryPktReq : HTTPPacketRequest
{
    public int m_iPID;  //玩家ID
    public int m_iWorldID;  //世界ID
    public int m_iAreaIndex;    //区域索引
    public int m_iFubenIndex;   //副本索引
    public int m_iGateIndex;    //关卡索引

    public FubenStoryPktReq()
    {
        this.m_strAction = PACKET_DEFINE.FUBEN_STORY_REQ;
    }
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 发送副本剧情设置
	/// </summary>
	/// <param name="worldid"></param>
	/// <param name="area_index"></param>
	/// <param name="fuben_index"></param>
	/// <param name="gate_index"></param>
	public static void SendFubenStory( int pid , int worldid, int area_index, int fuben_index, int gate_index)
	{
		FubenStoryPktReq req = new FubenStoryPktReq();
		req.m_iPID = pid;
		req.m_iWorldID = worldid;
		req.m_iAreaIndex = area_index;
		req.m_iFubenIndex = fuben_index;
		req.m_iGateIndex = gate_index;
		
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}


