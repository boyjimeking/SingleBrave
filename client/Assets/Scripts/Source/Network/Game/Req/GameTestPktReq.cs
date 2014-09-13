using UnityEngine;
using System.Collections;

using Game.Network;

//  GameTestPktReq.cs
//  Author: Lu Zexi
//  2013-11-13


/// <summary>
/// 游戏测试数据包
/// </summary>
public class GameTestPktReq : HTTPPacketBase
{
    public GameTestPktReq()
    {
        this.m_strAction = PACKET_DEFINE.GAME_TEST_REQ;
    }
}

