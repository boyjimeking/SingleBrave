using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Network;


public class PlayerPropsSlotExpansionHandle
{
    /// <summary>
    /// 获取Action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.PROPS_EXPANSION_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketRequest packet)
    {
        PlayerPropsSlotExpansionPktAck ack = (PlayerPropsSlotExpansionPktAck)packet;

        GAME_LOG.LOG("code :" + ack.m_iErrorCode);
        GAME_LOG.LOG("desc :" + ack.m_strErrorDes);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return;
        }

        Role.role.GetBaseProperty().m_iDiamond = ack.m_iDiamondCount;
        Role.role.GetBaseProperty().m_iMaxItem = ack.m_iMaxPropsCount;

        GUIBackFrameTop top = (GUIBackFrameTop)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP);
        top.UpdateDiamond(Role.role.GetBaseProperty().m_iDiamond);

        GUIPropsSlotExpansion propsSlot = (GUIPropsSlotExpansion)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_PROPSSLOTEXPANSION);
        propsSlot.Hiden();

        GUIStore store = (GUIStore)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_STORE);
        store.Show();

        return;
    }
}

