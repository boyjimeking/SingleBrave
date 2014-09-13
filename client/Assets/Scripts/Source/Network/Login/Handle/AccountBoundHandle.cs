using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;
using Game.Resource;


//  AccountBoundHandle.cs
//  Author: Lu Zexi
//  2014-02-18



/// <summary>
/// 帐号绑定句柄
/// </summary>
public class AccountBoundHandle : HTTPHandleBase
{
    /// <summary>
    /// 获取ACTION
    /// </summary>
    /// <returns></returns>
    public override string GetAction()
    {
        return PACKET_DEFINE.ACCOUNT_BOUND_REQ;
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public override bool Excute(HTTPPacketBase packet)
    {
        AccountBoundPktAck ack = packet as AccountBoundPktAck;

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return false;
        }

        GUIAccount gui = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_ACCOUNT) as GUIAccount;

        GAME_SETTING.s_bAccountBound = true;
        GAME_SETTING.s_iUID = ack.m_iUid;
        GAME_SETTING.s_strUserName = gui.m_strUser;
        GAME_SETTING.s_strPassWord = gui.m_strPassword;

        GAME_SETTING.SaveSetting();

        GUI_FUNCTION.MESSAGES("帐号绑定成功");

        if (GameManager.GetInstance().GetSceneManager().GetCurScene().GetSceneID() == SCENE.SCENE_GAME)
        {
            gui.Hiden();
        }
        else
        {
            gui.HidenImmediately();
        }

        return true;
    }

}
