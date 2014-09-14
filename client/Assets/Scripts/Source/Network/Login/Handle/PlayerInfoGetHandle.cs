using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Network;

//  PlayerInfoGetHandle.cs
//  Author: sanvey
//  2013-12-11


/// <summary>
/// 获取玩家信息请求应答句柄
/// </summary>
public class PlayerInfoGetHandle
{
    /// <summary>
    /// 获取Action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.GET_PLAYINFO_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketAck packet)
    {
        PlayerInfoGetPktAck ack = (PlayerInfoGetPktAck)packet;
        GAME_LOG.LOG("code :" + ack.header.code);
        GAME_LOG.LOG("desc :" + ack.header.desc);

        GAME_LOG.LOG("user name: " + ack.m_strUserName);

        if (ack.header.code == 1)  //没有玩家，创建玩家
        {
            GUI_FUNCTION.LOADING_HIDEN();
            GUIHeroChoose choose = (GUIHeroChoose)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_HERO_CHOOSE);
            choose.Show();
            //GUIRoleCreate gui = (GUIRoleCreate)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_ROLE_CREATE);
            //gui.Show();
            return;
        }

        if (ack.header.code < 0)
        {
            GAME_LOG.ERROR(ack.header.desc);
            return;
        }

        //存储角色数据
        Role.role.GetBaseProperty().m_iPlayerId = ack.m_iPlayerId;   //用户ID
        Role.role.GetBaseProperty().m_strUserName = ack.m_strUserName;    //用户名字//
        Role.role.GetBaseProperty().m_iLevel = ack.m_iLevel;   //用户等级//
        Role.role.GetBaseProperty().m_iCurrentExp = ack.m_iCurrentExp;  //用户当前经验//
        Role.role.GetBaseProperty().m_iStrength = ack.m_iStrength;    //用户当前体力//
        Role.role.GetBaseProperty().m_iDiamond = ack.m_iDiamond;  //用户所有钻石//
        Role.role.GetBaseProperty().m_iGold = ack.m_iGold;    //用户所有金币//
        Role.role.GetBaseProperty().m_iFarmPoint = ack.m_iFarmPoint;   //用户农场点//
        Role.role.GetBaseProperty().m_iMaxHeroCount = ack.m_iMaxHeroCount;  //用户可以拥有最大的英雄数量 
        Role.role.GetBaseProperty().m_iMaxItem = ack.m_iMaxItem;  //用户可以拥有的最大物体
        Role.role.GetBaseProperty().m_iFriendPoint = ack.m_iFriendPoint;  //友情点//
        Role.role.GetBaseProperty().m_iSportPoint = ack.m_iSportPoint;  //用户竞技点//
        Role.role.GetBaseProperty().m_iCurrentTeam = ack.m_iCurrentTeam; //当前战斗队伍//
        Role.role.GetBaseProperty().m_iCreateTime = ack.m_iCreateTime;  //创建时间
        Role.role.GetBaseProperty().m_vecWantItems = ack.m_lstWantItems.ToArray(); //想要好友赠送的礼物
        Role.role.GetBaseProperty().m_strSignature = ack.m_strSignature; //想要好友赠送的礼物
        Role.role.GetBaseProperty().m_strZhaoDaiId = ack.m_strZhaoDaiId;  //招待ID
        Role.role.GetBaseProperty().m_iModelID = ack.m_iGuideStep;    //新手引导
        Role.role.GetBaseProperty().m_iLoginTimes = ack.m_iLoginTimes;//连续登录次数
        Role.role.GetBaseProperty().m_iMailCounts = ack.m_iMailCounts;//系统礼物总数
        Role.role.GetBaseProperty().m_iFriendApplyCount = ack.m_iFriendApplyCount;//好友申请总数
        Role.role.GetBaseProperty().m_iFriendGiftCount = ack.m_iFriendGiftCount;//好友礼物总数
        Role.role.GetBaseProperty().m_iSportTime = ack.m_iSportTime;
        Role.role.GetBaseProperty().m_iStrengthTime = ack.m_iStrengthTime;

        SendAgent.SendPlayerHeroInfoGetPktReq(ack.m_iPlayerId);

        return;
    }
}