using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;


//  BattleReliveHandle.cs
//  Author: Lu Zexi
//  2014-03-04





/// <summary>
/// 战斗复活句柄
/// </summary>
public class BattleReliveHandle : HTTPHandleBase
{

    /// <summary>
    /// 获取action
    /// </summary>
    /// <returns></returns>
    public override string GetAction()
    {
        return PACKET_DEFINE.BATTLE_RELIVE_REQ;
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public override bool Excute(HTTPPacketBase packet)
    {
        BattleRelivePktAck ack = packet as BattleRelivePktAck;

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL_(null, ack.m_strErrorDes);
            return false;
        }

        if (!ack.m_bRelive)
        {
            return false;
        }

        Role.role.GetBaseProperty().m_iDiamond = ack.m_iDiamond;

        GUIBattleLose gui = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BATTLE_LOSE) as GUIBattleLose;
        gui.Relive();
        gui.Hiden();

        return true;
    }

}
