using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Network;

//  BuildingUpdatePktReq.cs
//  Author: sanvey
//  2013-12-30

/// <summary>
/// 建筑升级信息请求包
/// </summary>
public class BuildingUpdatePktReq : HTTPPacketRequest
{
    public int m_iPID;  //用户Pid
    public List<int> m_lstBuildType;  //建筑类型ID
    public List<int> m_lstBuildLevel; //目标建筑等级
    public List<int> m_lstBuildFarmPoint; //当前建筑剩余经验
    public int m_iFarmPoint;  //当前玩家农场点

    public BuildingUpdatePktReq()
    {
        this.m_strAction = PACKET_DEFINE.BUILDING_UPDATE_REQ;
    }
}



/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 发送更新建筑等级数据
	/// </summary>
	/// <param name="pid"></param>
	/// <param name="buildType"></param>
	/// <param name="buildLevel"></param>
	/// <param name="buildExp"></param>
	/// <param name="farmpoint"></param>
	public static void SendBuildingUpdateReq(int pid, List<int> buildType, List<int> buildLevel, List<int> buildExp, int farmpoint)
	{
		BuildingUpdatePktReq req = new BuildingUpdatePktReq();
		req.m_iPID = pid;
		req.m_iFarmPoint = farmpoint;
		req.m_lstBuildType = buildType;
		req.m_lstBuildLevel = buildLevel;
		req.m_lstBuildFarmPoint = buildExp;
		SessionManager.GetInstance().SendReady(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}