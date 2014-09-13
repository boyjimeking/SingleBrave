using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Network;
using Game.Base;
//战绩信息请求句柄
//Author sunyi
//2014-02-28
public class PlayerBattleRecordHandle : HTTPHandleBase
{
    /// <summary>
    /// 获取Action
    /// </summary>
    /// <returns></returns>
    public override string GetAction()
    {
        return PACKET_DEFINE.BATTLE_RECORD_GET_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public override bool Excute(HTTPPacketBase packet)
    {
        PlayerBattleRecordGetPktAck ack = (PlayerBattleRecordGetPktAck)packet;

        GAME_LOG.LOG("code :" + ack.m_iErrorCode);
        GAME_LOG.LOG("desc :" + ack.m_strErrorDes);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return false;
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

        return true;
    }
}

