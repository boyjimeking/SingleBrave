using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;



//  ItemBookPktReq.cs
//  Author: Lu Zexi 
//  2013-12-30



/// <summary>
/// 物品图鉴请求数据包
/// </summary>
public class ItemBookPktReq : HTTPPacketBase
{
    public int m_iPid;  //玩家ID

    public ItemBookPktReq()
    {
        this.m_strAction = PACKET_DEFINE.ITEM_BOOK_REQ;
    }

    /// <summary>
    /// 获取需求
    /// </summary>
    /// <returns></returns>
    public override string GetRequire()
    {
        string req = "pid=" + this.m_iPid;

        PACKET_HEAD.PACKET_REQ_END(ref req);

        return req;
    }
}
