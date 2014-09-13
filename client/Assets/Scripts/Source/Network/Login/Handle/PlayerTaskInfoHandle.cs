using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Network;

//获取当前任务信息请求句柄
//Author:sunyi
//2013-12-11

/// <summary>
/// 获取当前任务信息请求句柄
/// </summary>
public class PlayerTaskInfoHandle
{
    /// <summary>
    /// 获得Action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.GET_PLAYERTASKINFO_REQ;
    }


    public static void Excute(HTTPPacketRequest packet)
    {
        PlayerTaskInfoPktAck ack = (PlayerTaskInfoPktAck)packet;
        GAME_LOG.LOG("code :" + ack.m_iErrorCode);
        GAME_LOG.LOG("desc :" + ack.m_strErrorDes);

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return;
        }

        for (int i = 0; i < ack.lstTasks.Count; i++)
        {
            FuBen fuben = new FuBen();

            fuben.m_iWorldID = ack.lstTasks[i].worldId;//世界id
            fuben.m_bActive = (ack.lstTasks[i].active == 0) ? false : true;//是否激活
            fuben.m_iAreaIndex = ack.lstTasks[i].areaIndex;//区域索引
            fuben.m_iDungeonIndex = ack.lstTasks[i].dungeonIndex;//副本索引
            fuben.m_iGateIndex = ack.lstTasks[i].gateIndex;//关卡索引

            //剧情
            fuben.m_bDungeonStory = ack.lstTasks[i].dungeonStory;   //副本剧情

            Role.role.GetFubenProperty().AddFuben(fuben);

        }

        SendAgent.SendBuildInfoGetPktReq(Role.role.GetBaseProperty().m_iPlayerId);

        return;
    }
}

