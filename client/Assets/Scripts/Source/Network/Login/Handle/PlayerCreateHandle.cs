using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Network;

//  PlayerCreateHandle.cs
//  Author: sanvey
//  2013-12-11


/// <summary>
/// 创建玩家信息请求应答句柄
/// </summary>
public class PlayerCreateHandle : HTTPHandleBase
{
    /// <summary>
    /// 获取Action
    /// </summary>
    /// <returns></returns>
    public override string GetAction()
    {
        return PACKET_DEFINE.CREATE_PLAY_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public override bool Excute(HTTPPacketBase packet)
    {
        PlayerCreatePktAck ack = (PlayerCreatePktAck)packet;
        GAME_LOG.LOG("code :" + ack.m_iErrorCode);
        GAME_LOG.LOG("desc :" + ack.m_strErrorDes);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return false;
        }

        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_ROLE_CREATE).Hiden();

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
        UnityEngine.Debug.Log("model id =========================== " + ack.m_iGuideStep);
        Role.role.GetBaseProperty().m_iLoginTimes = ack.m_iLoginTimes;//连续登录次数
        Role.role.GetBaseProperty().m_iMailCounts = ack.m_iMailCounts;//系统礼物总数
        Role.role.GetBaseProperty().m_iFriendApplyCount =0;//好友申请总数
        Role.role.GetBaseProperty().m_iFriendGiftCount = 0;//好友礼物总数
        Role.role.GetBaseProperty().m_iSportTime = ack.m_iSportTime;
        Role.role.GetBaseProperty().m_iStrengthTime = ack.m_iStrengthTime;

        SendAgent.SendPlayerHeroInfoGetPktReq(Role.role.GetBaseProperty().m_iPlayerId);

        GAME_SETTING.SaveNewDungeonOfNewArea(false);//开启玩家第一次进入区域时显示new的动画特效
        return true;
    }
}