using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;


//  PayIOSPPVerifyHandle.cs
//  Author: Lu Zexi
//  2014-04-04




/// <summary>
/// IOS PP助手支付验证句柄
/// </summary>
public class PayIOSPPVerifyHandle
{
    /// <summary>
    /// 获取ACTION
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.PAY_IOS_PP_VERIFY;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketAck packet)
    {
        PayIOSPPVerifyPktAck ack = packet as PayIOSPPVerifyPktAck;

        GUI_FUNCTION.LOADING_HIDEN();

        //设置重新发送状态
        PlatformPPIOS tmp = PlatformManager.GetInstance().Platform as PlatformPPIOS;

        if (ack.header.code != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.header.desc);

            return;
        }

        if (ack.m_iResult == 2)
        {
            GUI_FUNCTION.MESSAGEM(ReSend, "验证超时,点击后重新验证");

            if (tmp != null) //开始重新发送
                tmp.m_eState = PlatformPPIOS.PayState.ReStart;

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

        if (tmp!=null)  //发送成功
            tmp.m_eState = PlatformPPIOS.PayState.Sucess;

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
