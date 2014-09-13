using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;
using Game.Base;
	
//获取活动副本优惠类型
//Author sunyi
//2014-1-10
public class ActivityFubenFavourableHandle : HTTPHandleBase
{
    /// <summary>
    /// 获取Action
    /// </summary>
    /// <returns></returns>
    public override string GetAction()
    {
        return PACKET_DEFINE.ACTIVITY_FUBEN_FAVOURABLE_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public override bool Excute(HTTPPacketBase packet)
    {
        ActivityFubenFavourablePktAck ack = (ActivityFubenFavourablePktAck)packet;

        GAME_LOG.LOG("code :" + ack.m_iErrorCode);
        GAME_LOG.LOG("desc :" + ack.m_strErrorDes);

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return false;
        }

        ActivityTableManager.GetInstance().ClearActivityDungeonFavType();

        for (int i = 0; i < ack.m_lstFavType.Count; i++)
        {
            ActivityTableManager.GetInstance().AddActivityDungeonFavType(ack.m_lstFavType[i]);
        }

        SendAgent.SendFriendGetListReq(Role.role.GetBaseProperty().m_iPlayerId);

        //GUI_FUNCTION.LOADING_HIDEN();
        //GameManager.GetInstance().GetSceneManager().ChangeGameScene();

        return true;
    }
}

