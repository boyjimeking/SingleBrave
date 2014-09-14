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
public class GameJoinHandle
{

    /// <summary>
    /// 获取ACTION
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.GAME_JOIN_REQ;
    }


    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketAck packet)
    {
        GameJoinPktAck ack = (GameJoinPktAck)packet;

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.header.code != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.header.desc);
            
        }

        GAME_SETTING.GAME_JOIN = true;
        GAME_SETTING.SaveGAME_JOIN();

        
    }
}
