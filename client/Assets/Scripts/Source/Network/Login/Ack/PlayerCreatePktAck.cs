using System;
using System.Collections.Generic;

using Game.Network;

//  PlayerCreatePktAck.cs
//  Author: sanvey
//  2013-12-11


/// <summary>
/// 创建玩家应答数据包
/// </summary>
public class PlayerCreatePktAck : HTTPPacketAck
{

    //public int m_iWorld;   //用户开启世界//
    //public int m_iArea;    //用户开启区域//
    //public int m_iTask;    //用户开启副本//
    //public int m_iCustomPass;  //用户开启关卡//
    //public int m_iMaxEXP;   //用户当前等级升级需要经验
    //public int m_iCurrentAthleticsEXP;  //当前用户竞技点经验 （需要查表获得下一级的经验点）
    //public int m_iCurrentAthleticsLevel; //当前竞技等级  （需要查表获得当前等级对应的称号）


    public int m_iUid; //用户Uid
    public int m_iPlayerId;   //用户ID
    public string m_strUserName;    //用户名字//
    public int m_iLevel;   //用户等级//
    public int m_iCurrentExp;  //用户当前经验//
    public int m_iStrength;    //用户当前体力//
    public int m_iStrengthTime;  //用户最近一次体力回复时间
    public int m_iDiamond;  //用户所有钻石//
    public int m_iGold;    //用户所有金币//
    public int m_iFarmPoint;   //用户农场点//
    public int m_iMaxHeroCount;  //用户可以拥有最大的英雄数量 
    public int m_iMaxItem;  //用户可以拥有的最大物体
    public int m_iFriendPoint;  //友情点//
    public int m_iSportPoint;  //用户竞技点//
    public int m_iSportTime;  //用户最近一次竞技点回复时间
    public int m_iCurrentTeam; //当前战斗队伍//
    public int m_iCreateTime;  //创建时间
    public string m_strSignature;  //玩家签名
    public string m_strZhaoDaiId; //玩家招待ID
    public List<int> m_lstWantItems = new List<int>(); //想要好友赠送的礼物
    public int m_iGuideStep;    //新手引导
    public int m_iLoginTimes;//连续登录次数
    public int m_iMailCounts;//系统礼物总数
    public int m_iFriendApplyCount;//好友申请总数
    public int m_iFriendGiftCount;//好友礼物总数

    // public PlayerCreatePktAck()
    // {
    //     this.m_strAction = PACKET_DEFINE.CREATE_PLAY_REQ;
    // }
}


// /// <summary>
// /// 创建玩家应答数据包工厂类
// /// </summary>
// public class PlayerCreatePktAckFactory : HTTPPacketFactory
// {

//     /// <summary>
//     /// 获取数据包Action
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.CREATE_PLAY_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(IJSonObject json)
//     {
//         PlayerCreatePktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<PlayerCreatePktAck>(json);

//         if (pkt.m_iErrorCode != 0)
//         {
//             return pkt;
//         }

//         IJSonObject data = json["data"]["player"];
//         pkt.m_iUid = data["uid"].Int32Value;
//         pkt.m_iPlayerId = data["id"].Int32Value;
//         pkt.m_strUserName = data["nickname"].StringValue;
//         pkt.m_iLevel = data["lv"].Int32Value;
//         pkt.m_iCurrentExp = data["exp"].Int32Value;
//         pkt.m_iStrength = data["strength"].Int32Value;
//         pkt.m_iDiamond = data["diamond"].Int32Value;
//         pkt.m_iGold = data["gold"].Int32Value;
//         pkt.m_iFarmPoint = data["farm_point"].Int32Value;
//         pkt.m_iMaxHeroCount = data["max_hero"].Int32Value;
//         pkt.m_iMaxItem = data["max_item"].Int32Value;
//         pkt.m_iFriendPoint = data["friend_point"].Int32Value;
//         pkt.m_iSportPoint = data["sport_point"].Int32Value;
//         pkt.m_iCurrentTeam = data["curr_team"].Int32Value;
//         pkt.m_iCreateTime = data["create_time"].Int32Value;
//         pkt.m_lstWantItems.Add(data["want_item1"].Int32Value);
//         pkt.m_lstWantItems.Add(data["want_item2"].Int32Value);
//         pkt.m_lstWantItems.Add(data["want_item3"].Int32Value);
//         pkt.m_strSignature = data["signature"].StringValue;
//         pkt.m_strZhaoDaiId = data["zhaodaiid"].StringValue;
//         pkt.m_iGuideStep = data["newbie_step"].Int32Value;
//         pkt.m_iLoginTimes = data["login_times"].Int32Value;
//         pkt.m_iMailCounts = json["data"]["mail_count"].Int32Value;
//         pkt.m_iStrengthTime = data["strength_up_time"].Int32Value;
//         pkt.m_iSportTime = data["sport_up_time"].Int32Value;

//         return pkt;
//     }
// }