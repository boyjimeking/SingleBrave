using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Network;

//  PVPWeekRankGetHandle.cs
//  Author: sanvey
//  2014-2-8

//竞技场排行获取请求应答句柄
public class PVPWeekRankGetHandle : HTTPHandleBase
{
    /// <summary>
    /// 获得Action
    /// </summary>
    /// <returns></returns>
    public override string GetAction()
    {
        return PACKET_DEFINE.PVP_WEEK_RANK_GET_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public override bool Excute(HTTPPacketBase packet)
    {
        PVPWeekRankGetPktAck ack = (PVPWeekRankGetPktAck)packet;

        GAME_LOG.LOG("code :" + ack.m_iErrorCode);
        GAME_LOG.LOG("desc :" + ack.m_strErrorDes);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return false;
        }

        Role.role.GetBaseProperty().m_lstWeekRank = new List<RoleBaseProperty.PVPItem>();
        for (int i = 0; i < ack.m_lstWeekRank.Count; i++)
        {
            RoleBaseProperty.PVPItem tmp = new RoleBaseProperty.PVPItem();
            tmp.m_iHeroLv = ack.m_lstWeekRank[i].m_iHeroLv;
            tmp.m_iHeroTableID = ack.m_lstWeekRank[i].m_iHeroTableID;
            tmp.m_iLoseNum = ack.m_lstWeekRank[i].m_iLoseNum;
            tmp.m_iPoint = ack.m_lstWeekRank[i].m_iPoint;
            tmp.m_iWinNum = ack.m_lstWeekRank[i].m_iWinNum;
            tmp.m_strName = ack.m_lstWeekRank[i].m_strName;

            Role.role.GetBaseProperty().m_lstWeekRank.Add(tmp);
        }

        Role.role.GetBaseProperty().m_iMyWeekRank = ack.m_iMyWeek;

        if (!GAME_SETTING.s_bFirstCodeShow)  //第一次登陆只弹一次提示输入邀请码
        {
            GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_FESTA_INPUT).Show();
            //GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_HERO_CREATE).Show();
        }
        else
        {
            GameManager.GetInstance().GetSceneManager().ChangeGameScene();
        }


        return true;
    }
}