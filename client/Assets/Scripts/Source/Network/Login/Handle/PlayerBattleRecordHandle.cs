using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Network;
using Game.Base;
//战绩信息请求句柄
//Author sunyi
//2014-02-28
public class PlayerBattleRecordHandle
{
    /// <summary>
    /// 获取Action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.BATTLE_RECORD_GET_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketAck packet)
    {
        PlayerBattleRecordGetPktAck ack = (PlayerBattleRecordGetPktAck)packet;

        GAME_LOG.LOG("code :" + ack.header.code);
        GAME_LOG.LOG("desc :" + ack.header.desc);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.header.code != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.header.desc);
            return;
        }

        Role.role.GetBattleRecordProperty().Clear();

        for (int i = 0; i < ack.m_lstRecords.Count; i++)
        {
            Role.role.GetBattleRecordProperty().AddRecord(ack.m_lstRecords[i]);
        }

        GUIMenu menu = (GUIMenu)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_MENU);
        menu.Hiden();

        GUIBattleRecord record = (GUIBattleRecord)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BATTLERECORD);
        record.Show();

        return;
    }
}

