using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

//请求特殊副本优惠类型应答类
//Author sunyi
//2014-1-10
public class ActivityFubenFavourablePktAck : HTTPPacketAck
{
    public List<FAV_TYPE> m_lstFavType = new List<FAV_TYPE>();

    // public ActivityFubenFavourablePktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.ACTIVITY_FUBEN_FAVOURABLE_REQ;
    // }
}

///// <summary>
///// 请求特殊副本优惠类型应答工厂类
///// </summary>
//public class ActivityFubenFavourablePktAckFactory : HTTPPacketFactory
//{
//    /// <summary>
//    /// 获取数据包Action
//    /// </summary>
//    /// <returns></returns>
//    public override string GetPacketAction()
//    {
//        return PACKET_DEFINE.ACTIVITY_FUBEN_FAVOURABLE_REQ;
//    }
//
//    /// <summary>
//    /// 创建数据包
//    /// </summary>
//    /// <param name="json"></param>
//    /// <returns></returns>
//    public override HTTPPacketRequest Create(CodeTitans.JSon.IJSonObject json)
//    {
//        ActivityFubenFavourablePktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<ActivityFubenFavourablePktAck>(json);
//        if (pkt.header.code != 0)
//        {
//            return pkt;
//        }
//
//        for (int i = 1; i < json["data"].Count + 1; i++)
//        {
//            pkt.m_lstFavType.Add((FAV_TYPE)(json["data"][i.ToString()].Int32Value));
//        }
//
//        return pkt;
//    }
//}
