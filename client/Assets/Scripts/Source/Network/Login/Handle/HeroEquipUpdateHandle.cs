using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Network;

//  HeroEquipUpdateHandle.cs
//  Author: sanvey
//  2013-12-20

//英雄装备更新请求应答句柄
public class HeroEquipUpdateHandle : HTTPHandleBase
{
    /// <summary>
    /// 获得Action
    /// </summary>
    /// <returns></returns>
    public override string GetAction()
    {
        return PACKET_DEFINE.HERO_EQUIP_UPDATE_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public override bool Excute(HTTPPacketBase packet)
    {
        HeroEquipUpdatePktAck ack = (HeroEquipUpdatePktAck)packet;

        GAME_LOG.LOG("code :" + ack.m_iErrorCode);
        GAME_LOG.LOG("desc :" + ack.m_strErrorDes);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return false;
        }

        //返回Code 0表示成功
        if (ack.m_iErrorCode == 0)
        {

        }

        SessionManager.GetInstance().CallBack();

        return true;
    }
}