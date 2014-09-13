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
public class BuildingUpdatePktReq : HTTPPacketBase
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

    /// <summary>
    /// 获取请求参数
    /// </summary>
    /// <returns></returns>
    public override string GetRequire()
    {
        string data = string.Empty;

        for (int i = 0; i < m_lstBuildType.Count; i++)
        {
            data += m_lstBuildType[i] + ":" + m_lstBuildLevel[i] + ":" + m_lstBuildFarmPoint[i] + "|";
        }
        if (data.EndsWith("|"))
        {
            data = data.Remove(data.Length - 1);
        }

        string req = string.Format("pid={0}&build_infos={1}&farmpoint={2}",
            m_iPID, data, m_iFarmPoint);

        PACKET_HEAD.PACKET_REQ_END(ref req);

        return req;
    }
}