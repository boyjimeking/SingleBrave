using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Network;
using Game.Base;
//商城钻石价格句柄
//Author sunyi
//2014-02-28
public class StoreDiamonGetHandle
{
    /// <summary>
    /// 获取Action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.STORE_DIAMOND_GET_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketRequest packet)
    {
        StoreDiamonGetPktAck ack = (StoreDiamonGetPktAck)packet;

        GAME_LOG.LOG("code :" + ack.m_iErrorCode);
        GAME_LOG.LOG("desc :" + ack.m_strErrorDes);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return;
        }

        Role.role.GetStoreDiamondProperty().ClearAll();

        for (int i = 0; i < ack.m_lstStoreDiamondPrice.Count; i++)
        {
            Role.role.GetStoreDiamondProperty().AddDiamondPrice(ack.m_lstStoreDiamondPrice[i]);
        }

        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_STORE).Hiden();

        GUIBackFrameBottom backbottom = (GUIBackFrameBottom)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM);
        backbottom.HiddenHalf();

        GUIGem gem = (GUIGem)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_GEM);
        gem.SetLastGuiId(GUI_DEFINE.GUIID_STORE);
        gem.Show();

        return;
    }
}

