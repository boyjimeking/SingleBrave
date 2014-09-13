using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Network;

//竞技点恢复句柄类
//Author sunyi
//2013-12-27
public class PlayerSportPointRecoverHandle : HTTPHandleBase
{
    /// <summary>
    /// 获取Action
    /// </summary>
    /// <returns></returns>
    public override string GetAction()
    {
        return PACKET_DEFINE.BATTLEPOING_RECOVER_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public override bool Excute(HTTPPacketBase packet)
    {
        PlayerSportPointRecoverPktAck ack = (PlayerSportPointRecoverPktAck)packet;

        GAME_LOG.LOG("code :" + ack.m_iErrorCode);
        GAME_LOG.LOG("desc :" + ack.m_strErrorDes);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return false;
        }

        Role.role.GetBaseProperty().m_iDiamond = ack.m_iDiamondCount;
        Role.role.GetBaseProperty().m_iSportPoint = ack.m_iSportPoint;

        GUIBackFrameTop top = (GUIBackFrameTop)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP);
        top.UpdateDiamond(Role.role.GetBaseProperty().m_iDiamond);
        top.UpdateSportPoint(Role.role.GetBaseProperty().m_iSportPoint);

        GUIFistfightPointRestoration sportPoint = (GUIFistfightPointRestoration)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_FISTFIGHTPOINTRESTORATION);
        sportPoint.Hiden();

        GUIStore store = (GUIStore)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_STORE);
        store.Show();
        return true;
    }
}

