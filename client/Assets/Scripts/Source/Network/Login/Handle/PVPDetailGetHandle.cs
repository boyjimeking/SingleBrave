using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Network;

//  PVPDetailGetHandle.cs
//  Author: sanvey
//  2014-2-8

//竞技场详细信息获取请求应答句柄
public class PVPDetailGetHandle : HTTPHandleBase
{
    /// <summary>
    /// 获得Action
    /// </summary>
    /// <returns></returns>
    public override string GetAction()
    {
        return PACKET_DEFINE.PVP_DETAIL_GET_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public override bool Excute(HTTPPacketBase packet)
    {
        PVPDetailGetPktAck ack = (PVPDetailGetPktAck)packet;

        GAME_LOG.LOG("code :" + ack.m_iErrorCode);
        GAME_LOG.LOG("desc :" + ack.m_strErrorDes);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return false;
        }

        GUIArenaBattleIntelligence tmp = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_ARENABATTLEINTELLIGENCE) as GUIArenaBattleIntelligence;
        tmp.m_lstRecord = new List<int>();
        tmp.m_lstRecord.Add(ack.m_iwin_num+ack.m_ilose_num);
        tmp.m_lstRecord.Add(ack.m_iwin_num);
        tmp.m_lstRecord.Add(ack.m_iwin_attack_max_link);
        tmp.m_lstRecord.Add(ack.m_iwin_attack_max);
        tmp.m_lstRecord.Add(ack.m_iwin_defence_max_link);
        tmp.m_lstRecord.Add(ack.m_iwin_defence_max);
        tmp.m_lstRecord.Add(ack.m_ikill_hero_num);
        tmp.m_lstRecord.Add(ack.m_ipvp_max_point);
        tmp.m_lstRecord.Add(ack.m_ikill_fire_hero_num);
        tmp.m_lstRecord.Add(ack.m_ikill_water_hero_num);
        tmp.m_lstRecord.Add(ack.m_ikill_wood_hero_num);
        tmp.m_lstRecord.Add(ack.m_ikill_thunder_hero_num);
        tmp.m_lstRecord.Add(ack.m_ikill_light_hero_num);
        tmp.m_lstRecord.Add(ack.m_ikill_dark_hero_num);
        tmp.m_lstRecord.Add(ack.m_ikill_star1_hero_num);
        tmp.m_lstRecord.Add(ack.m_ikill_star2_hero_num);
        tmp.m_lstRecord.Add(ack.m_ikill_star3_hero_num);
        tmp.m_lstRecord.Add(ack.m_ikill_star4_hero_num);
        tmp.m_lstRecord.Add(ack.m_ikill_star5_hero_num);
        tmp.m_lstRecord.Add(ack.m_ikill_all_num);
        tmp.m_lstRecord.Add(ack.m_iwin_time_up_num);
        tmp.m_lstRecord.Add(ack.m_iwin_perfect_num);
        tmp.m_lstRecord.Add(ack.m_iwin_skill_num);
        tmp.m_lstRecord.Add(ack.m_iover_kill_num);
        tmp.m_lstRecord.Add(ack.m_ihit_one_max_num);
        tmp.m_lstRecord.Add(ack.m_ispark_one_max_num);
        tmp.m_lstRecord.Add(ack.m_ihit_all_num);
        tmp.m_lstRecord.Add(ack.m_irecover_all_num);
        tmp.m_lstRecord.Add(ack.m_ispark_all_num);
        tmp.m_lstRecord.Add(ack.m_iskill_use_num);

        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_ARENA).Hiden();
        tmp.Show();

        return true;
    }
}


