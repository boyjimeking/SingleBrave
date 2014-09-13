using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;


//  GameJoinHandle.cs
//  Author: Lu Zexi
//  2014-03-27



/// <summary>
/// 游戏加入句柄
/// </summary>
public class GameJoinHandle : HTTPHandleBase
{

    /// <summary>
    /// 获取ACTION
    /// </summary>
    /// <returns></returns>
    public override string GetAction()
    {
        return PACKET_DEFINE.GAME_JOIN_REQ;
    }


    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public override bool Excute(HTTPPacketBase packet)
    {
        GameJoinPktAck ack = (GameJoinPktAck)packet;

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return false;
        }

        GAME_SETTING.GAME_JOIN = true;
        GAME_SETTING.SaveGAME_JOIN();

        return true;
    }
}
