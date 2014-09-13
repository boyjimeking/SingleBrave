using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Network;

//获取玩家英雄列表请求应答句柄
//Author: sunyi
//2013-12-11

//获取玩家英雄列表请求应答句柄
public class PlayerHeroInfoHandle : HTTPHandleBase
{
    /// <summary>
    /// 获得Action
    /// </summary>
    /// <returns></returns>
    public override string GetAction()
    {
        return PACKET_DEFINE.GET_PALYERHEROINFO_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public override bool Excute(HTTPPacketBase packet)
    {
        PlayerHeroInfoPktAck ack = (PlayerHeroInfoPktAck)packet;

        GAME_LOG.LOG("code :" + ack.m_iErrorCode);
        GAME_LOG.LOG("desc :" + ack.m_strErrorDes);

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return false;
        }

        for (int i = 0; i < ack.heros.Count; i++)
        {
            Hero hero = new Hero(ack.heros[i].m_iTableID);
            //存储英雄信息
            hero.m_iID = ack.heros[i].m_iID;// 英雄id
            hero.m_iTableID = ack.heros[i].m_iTableID;// 配置表id
            hero.m_iCurrenExp = ack.heros[i].m_iCurrenExp;// 英雄经验
            hero.m_lGetTime = ack.heros[i].m_lGetTime;// 英雄创建时间
            hero.m_iHp = ack.heros[i].m_iHp;// 英雄血量
            hero.m_iAttack = ack.heros[i].m_iAttack;// 英雄攻击力
            hero.m_iDefence = ack.heros[i].m_iDefense;// 英雄恢复力
            hero.m_iRevert = ack.heros[i].m_iRevert;// 英雄id
            hero.m_iBBSkillLevel = ack.heros[i].m_iBBSkillLevel;// 英雄BB技能
            hero.m_eGrowType = (GrowType)ack.heros[i].m_eGrowType;// 英雄成长类型
            hero.m_iLevel = ack.heros[i].m_iLevel;  // 英雄等级
            hero.m_iEquipID = ack.heros[i].m_iEquipId;
            hero.m_bLock = (ack.heros[i].m_iLock == 1);
            Role.role.GetHeroProperty().AddHero(hero);
        }

        SendAgent.SendTeamInfoGetPktReq(Role.role.GetBaseProperty().m_iPlayerId);

        return true;
    }
}

