using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Network;

//体力恢复句柄
//Author sunyi
//2013-12-27
public class PlayerStrengthRecoverHandle
{
    /// <summary>
    /// 获取Action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.STRENGTH_RECOVER_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketRequest packet)
    {
        PlayerStrengthRecoverPktAck ack = (PlayerStrengthRecoverPktAck)packet;

        GAME_LOG.LOG("code :" + ack.m_iErrorCode);
        GAME_LOG.LOG("desc :" + ack.m_strErrorDes);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return;
        }

        Role.role.GetBaseProperty().m_iDiamond = ack.m_iDiamondCount;
        Role.role.GetBaseProperty().m_iStrength = ack.m_iStrength;

        GUIBackFrameTop top = (GUIBackFrameTop)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP);
        top.UpdateDiamond(Role.role.GetBaseProperty().m_iDiamond);

        GUIBodyStrengthRestoration strength = (GUIBodyStrengthRestoration)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BODYSTRENGTHRESTORATION);
        if (strength.IsCurGui())

        {
            strength.Hiden();

            GUIStore store = (GUIStore)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_STORE);
            store.Show();
        }
        return;
    }
}

