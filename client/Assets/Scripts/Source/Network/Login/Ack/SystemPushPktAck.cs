using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;
using Game.Base;
using CodeTitans.JSon;

//  SystemPushPktAck.cs
//  Author: Lu Zexi
//  2014-02-24



/// <summary>
/// 系统推送
/// </summary>
public class SystemPushPktAck : HTTPPacketBase
{
    public int m_iEmailNum; //邮件数
    public int m_iApplyNum;    //好友申请信息
    public int m_iFriendGiftNum;    //好友礼物信息

    public int m_iStrength;
    public int m_iStrengthTime;
    public int m_iSportPoint;
    public int m_iSportTime;
    public int m_iDiamond;

    public List<FriendData> m_lstFriendData = new List<FriendData>();   //好友信息

    public SystemPushPktAck()
    {
        this.m_strAction = PACKET_DEFINE.SYSTEM_PUSH_REQ;
    }
}


/// <summary>
/// 系统推送工厂类
/// </summary>
public class SystemPushPktAckFactory : HTTPPacketFactory
{
    /// <summary>
    /// 获取ACTION
    /// </summary>
    /// <returns></returns>
    public override string GetPacketAction()
    {
        return PACKET_DEFINE.SYSTEM_PUSH_REQ;
    }

    public override HTTPPacketBase Create(IJSonObject json)
    {
        SystemPushPktAck ack = PACKET_HEAD.PACKET_ACK_HEAD<SystemPushPktAck>(json);

        if (ack.m_iErrorCode != 0)
        {
            GAME_LOG.ERROR("Error . code desc " + ack.m_strErrorDes);
            return ack;
        }

        IJSonObject data = json["data"];

        ack.m_iEmailNum = data["gift_count"].Int32Value;
        ack.m_iApplyNum = data["apply_info"].Int32Value;
        ack.m_iFriendGiftNum = data["gifts"].Int32Value;

        ack.m_iStrength = data["strength"].Int32Value;
        ack.m_iSportPoint = data["sport_point"].Int32Value;
        ack.m_iStrengthTime = data["strength_up_time"].Int32Value;
        ack.m_iSportTime = data["sport_up_time"].Int32Value;
        ack.m_iDiamond = data["diamond"].Int32Value;

        IJSonObject fri_data = data["friends"];

        foreach (IJSonObject item in fri_data.ArrayItems)
        {
            FriendData fd = new FriendData();
            fd.m_iPID = item["pid"].Int32Value;
            fd.m_strNickName = item["nickname"].ToString();
            fd.m_iRoloLevel = item["lv"].Int32Value;
            fd.m_lstWantGift[0] = item["want_item1"].Int32Value;
            fd.m_lstWantGift[1] = item["want_item2"].Int32Value;
            fd.m_lstWantGift[2] = item["want_item3"].Int32Value;
            fd.m_strSignature = item["signature"].ToString();
            fd.m_iLike = item["like"].Int32Value;
            fd.m_iLoginTime = item["login_time"].Int64Value;
            fd.m_iAthleticsLevel = item["pvp_point"].Int32Value;
            fd.m_iHeroTableID = item["hero_id"].Int32Value;
            fd.m_iHeroLv = item["hero_lv"].Int32Value;
            fd.m_iHeroHp = (int)item["hero_hp"].SingleValue;
            fd.m_iAttack = (int)item["hero_attack"].SingleValue;
            fd.m_iDefend = (int)item["hero_defend"].SingleValue;
            fd.m_iRecover = (int)item["hero_recover"].SingleValue;
            fd.m_iSendTime = item["give_time"].Int64Value;
            ack.m_lstFriendData.Add(fd);
        }

        return ack;
    }

}