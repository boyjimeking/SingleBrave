using System;
using System.Collections.Generic;
using CodeTitans.JSon;
using Game.Network;

//  PVPEnemyGetPktAck.cs
//  Author: sanvey
//  2014-2-8


/// <summary>
/// 竞技场对手获取获得
/// </summary>
public class PVPEnemyGet5PktAck : HTTPPacketBase
{
    public List<PVPEnemyData> m_lstEnemys;



    public PVPEnemyGet5PktAck()
    {
        this.m_strAction = PACKET_DEFINE.PVP_ENEMY_GET_5_REQ;
    }
}


/// <summary>
/// 竞技场对手获取获得数据包工厂类
/// </summary>
public class PVPEnemyGet5PktAckFactory : HTTPPacketFactory
{

    /// <summary>
    /// 获取数据包Action
    /// </summary>
    /// <returns></returns>
    public override string GetPacketAction()
    {
        return PACKET_DEFINE.PVP_ENEMY_GET_5_REQ;
    }

    /// <summary>
    /// 创建数据包
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public override HTTPPacketBase Create(IJSonObject json)
    {
        PVPEnemyGet5PktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<PVPEnemyGet5PktAck>(json);

        if (pkt.m_iErrorCode != 0)
        {
            return pkt;
        }

        List<IJSonObject> data = new List<IJSonObject>(json["data"].ArrayItems);
        pkt.m_lstEnemys = new List<PVPEnemyData>();
        foreach (IJSonObject item in data)
        {
            PVPEnemyData tmp = new PVPEnemyData();
            tmp.m_iTpid = item["id"].Int32Value;
            tmp.m_strName = item["nickname"].StringValue;
            tmp.m_iHeroTableID = item["hero_id"].Int32Value;
            tmp.m_iLv = item["lv"].Int32Value;
            tmp.m_iPVP_point = item["pvp_point"].Int32Value;
            tmp.m_iPVPWeek_point = item["pvp_weekpoint"].Int32Value;
            tmp.m_iPVPwin_num = item["win_num"].Int32Value;
            tmp.m_iPVPlose_num = item["lose_num"].Int32Value;
            tmp.m_iPlayLv = item["player_lv"].Int32Value;
            tmp.m_strSignure = item["signature"].StringValue;

            pkt.m_lstEnemys.Add(tmp);
        }


        return pkt;
    }
}

public class PVPEnemyData
{
    public int m_iTpid; //对手ID
    public string m_strName;
    public int m_iHeroTableID;
    public int m_iLv;
    public int m_iPVP_point;
    public int m_iPVPWeek_point;
    public int m_iPVPwin_num;
    public int m_iPVPlose_num;
    public string m_strSignure;
    public int m_iPlayLv;
}