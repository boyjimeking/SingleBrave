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
public class PVPWeekRankGetHandle
{
    /// <summary>
    /// 获得Action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.PVP_WEEK_RANK_GET_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketAck packet)
    {
        PVPWeekRankGetPktAck ack = (PVPWeekRankGetPktAck)packet;

        GAME_LOG.LOG("code :" + ack.header.code);
        GAME_LOG.LOG("desc :" + ack.header.desc);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.header.code != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.header.desc);
            return;
        }
		
		for (int i = 0; i < PVPItemInfo.Count; i++)
        {
            PVPItemInfo tmp = new PVPItemInfo();
            tmp.m_iHeroLv = ack.m_lstWeekRank[i].m_iHeroLv;
            tmp.m_iHeroTableID = ack.m_lstWeekRank[i].m_iHeroTableID;
            tmp.m_iLoseNum = ack.m_lstWeekRank[i].m_iLoseNum;
            tmp.m_iPoint = ack.m_lstWeekRank[i].m_iPoint;
            tmp.m_iWinNum = ack.m_lstWeekRank[i].m_iWinNum;
            tmp.m_strName = ack.m_lstWeekRank[i].m_strName;

			PVPItemInfo.Add(tmp);
        }

        Role.role.GetBaseProperty().m_iMyWeekRank = ack.m_iMyWeek;

        if (!GAME_SETTING.s_bFirstCodeShow)  //第一次登陆只弹一次提示输入邀请码
        {
            GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_FESTA_INPUT).Show();
            //GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_HERO_CREATE).Show();
        }
        else
        {
			CScene.Switch<GameScene>();
        }


        return;
    }
}