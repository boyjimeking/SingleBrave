using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;
using CodeTitans.JSon;

//  FubenStoryPktAck.cs
//  Author: Lu Zexi
//  2014-02-27



/// <summary>
/// 副本剧情设置应答数据包
/// </summary>
public class FubenStoryPktAck : HTTPPacketBase
{
    public FubenStoryPktAck()
    {
        this.m_strAction = PACKET_DEFINE.FUBEN_STORY_REQ;
    }
}



/// <summary>
/// 副本剧情设置数据包工厂类
/// </summary>
public class FubenStoryPktAckFactory : HTTPPacketFactory
{
    /// <summary>
    /// 获取ACTION
    /// </summary>
    /// <returns></returns>
    public override string GetPacketAction()
    {
        return PACKET_DEFINE.FUBEN_STORY_REQ;
    }

    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public override HTTPPacketBase Create(IJSonObject json)
    {
        FubenStoryPktAck ack = PACKET_HEAD.PACKET_ACK_HEAD<FubenStoryPktAck>(json);

        return ack;
    }

}