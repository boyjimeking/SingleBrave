using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

//  PayIOSVerifyHandle.cs
//  Author: Lu Zexi
//  2014-04-02



/// <summary>
/// 支付验证句柄
/// </summary>
public class PayIOSVerifyHandle
{

    /// <summary>
    /// 获取ACTION
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.PAY_IOS_VERIFY;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketRequest packet)
    {
        PayIOSVerifyPktAck ack = packet as PayIOSVerifyPktAck;

        GUI_FUNCTION.LOADING_HIDEN();
        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return;
        }

        if (ack.m_iResult == 2)
        {
            GUI_FUNCTION.MESSAGEM(ReSend, "验证超时,点击后重新验证");
            return;
        }

        if (ack.m_iResult != 1)
        {
            GUI_FUNCTION.MESSAGEL(null, "支付验证失败,请与客服联系");
            return;
        }

        Role.role.GetBaseProperty().m_iDiamond = ack.m_iDiamond;
        GUIBackFrameTop gui = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP) as GUIBackFrameTop;
        gui.UpdateDiamond(ack.m_iDiamond);

        GUI_FUNCTION.MESSAGES("恭喜您充值成功");

        return;
    }


    /// <summary>
    /// 重新发送
    /// </summary>
    private static void ReSend()
    {
        SendAgent.SendPayIOSVerify(Role.role.GetBaseProperty().m_iPlayerId, Role.role.GetPayProperty().m_iPayID, Role.role.GetPayProperty().m_strVerify);
    }

}
