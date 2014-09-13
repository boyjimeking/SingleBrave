using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeTitans.JSon;
using Game.Network;

//获取当前任务信息应答类
//Author:sunyi
//2013-12-11
public class PlayerTaskInfoPktAck : HTTPPacketBase
{

    public List<TaskData> lstTasks = new List<TaskData>();

    public PlayerTaskInfoPktAck() 
    {
        this.m_strAction = PACKET_DEFINE.GET_PLAYERTASKINFO_REQ;
    }

    public class TaskData
    { 
        public int worldId; //开启的世界id
        public int active; //世界活跃状态
        public int areaIndex; //区域id
        public int dungeonIndex;// 副本id
        public int gateIndex; //关卡id
        //剧情
        public bool dungeonStory;   //副本剧情
    }

}

/// <summary>
/// 获取当前任务信息应答数据包工厂类
/// </summary>
public class PlayerTaskInfoPktAckFactory : HTTPPacketFactory
{
    /// <summary>
    /// 获取数据包Action
    /// </summary>
    /// <returns></returns>
    public override string GetPacketAction()
    {
        return PACKET_DEFINE.GET_PLAYERTASKINFO_REQ;
    }


    /// <summary>
    /// 创建数据包
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public override HTTPPacketBase Create(IJSonObject json)
    {
        PlayerTaskInfoPktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<PlayerTaskInfoPktAck>(json);
        if (pkt.m_iErrorCode != 0)
        {
            return pkt;
        }

        var data = json["data"].ArrayItems;
        foreach (var item in data)
        {
            PlayerTaskInfoPktAck.TaskData taskData = new PlayerTaskInfoPktAck.TaskData();
            taskData.worldId = item["world_id"].Int32Value;
            taskData.active = item["active"].Int32Value;
            taskData.areaIndex = item["area_index"].Int32Value;
            taskData.dungeonIndex = item["fuben_index"].Int32Value;
            taskData.gateIndex = item["gate_index"].Int32Value;
            taskData.dungeonStory = item["fuben_story"].Int32Value == 1;

            pkt.lstTasks.Add(taskData);
        }
        return pkt;

    }
}