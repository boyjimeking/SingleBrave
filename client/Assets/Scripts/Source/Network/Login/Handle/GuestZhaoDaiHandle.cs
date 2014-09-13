using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Network;

//GuestZhaoDaiHandle
//Author: sunyi
//2013-12-11

//招待请求应答句柄
public class GuestZhaoDaiHandle : HTTPHandleBase
{
    public static Action CallBack;
    
    /// <summary>
    /// 获得Action
    /// </summary>
    /// <returns></returns>
    public override string GetAction()
    {
        return PACKET_DEFINE.GUEST_ZHAODAI_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public override bool Excute(HTTPPacketBase packet)
    {
        GuestZhaoDaiPktAck ack = (GuestZhaoDaiPktAck)packet;

        GAME_LOG.LOG("code :" + ack.m_iErrorCode);
        GAME_LOG.LOG("desc :" + ack.m_strErrorDes);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return false;
        }

        if (ack.m_bOk)
        {
            //if (ack.m_cHero != null)
            //{
            //    Hero hero = new Hero(ack.m_cHero.m_iTableID);
            //    //存储英雄信息
            //    hero.m_iID = ack.m_cHero.m_iID;// 英雄id
            //    hero.m_iTableID = ack.m_cHero.m_iTableID;// 配置表id
            //    hero.m_iCurrenExp = ack.m_cHero.m_iCurrenExp;// 英雄经验
            //    hero.m_lGetTime = ack.m_cHero.m_lGetTime;// 英雄创建时间
            //    hero.m_iHp = ack.m_cHero.m_iHp;// 英雄血量
            //    hero.m_iAttack = ack.m_cHero.m_iAttack;// 英雄攻击力
            //    hero.m_iDefence = ack.m_cHero.m_iDefense;// 英雄恢复力
            //    hero.m_iRevert = ack.m_cHero.m_iRevert;// 英雄id
            //    hero.m_iBBSkillLevel = ack.m_cHero.m_iBBSkillLevel;// 英雄BB技能
            //    hero.m_eGrowType = (GrowType)ack.m_cHero.m_eGrowType;// 英雄成长类型
            //    hero.m_iLevel = ack.m_cHero.m_iLevel;  // 英雄等级
            //    hero.m_iEquipID = ack.m_cHero.m_iEquipId;
            //    hero.m_bLock = (ack.m_cHero.m_iLock == 1);
            //    Role.role.GetHeroProperty().AddHero(hero);
            //}

            //if (ack.m_cItem != null)
            //{
            //    Item tmp = new Item(ack.m_cItem.m_iItem_id);
            //    tmp.m_iID = ack.m_cItem.m_iId;
            //    tmp.m_iNum = ack.m_cItem.m_iItem_num;
            //    Role.role.GetItemProperty().AddItem(tmp);
            //}

            //Role.role.GetBaseProperty().m_iGold += ack.m_iGold;
            //Role.role.GetBaseProperty().m_iFarmPoint += ack.m_iFarm;
            //Role.role.GetBaseProperty().m_iFriendPoint += ack.m_iFriendPoint;
            //Role.role.GetBaseProperty().m_iDiamond += ack.m_iDiamond;

            if (CallBack!=null)
            {
                CallBack();
            }
        }
        else
        {
            GUI_FUNCTION.MESSAGEM(null, "邀请码不存在");
        }

        return true;
    }
}

