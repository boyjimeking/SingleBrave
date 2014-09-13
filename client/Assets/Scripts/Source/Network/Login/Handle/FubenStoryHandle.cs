using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;


//  fubenStoryHandle.cs
//  Author: Lu Zexi
//  2014-02-27



/// <summary>
/// 副本剧情设置句柄
/// </summary>
public class FubenStoryHandle
{
    /// <summary>
    /// 获取动作
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.FUBEN_STORY_REQ;
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketRequest packet)
    {
        FubenStoryPktAck ack = packet as FubenStoryPktAck;

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEM(null, ack.m_strErrorDes);
            return false;
        }

        AreaTable areaTabe = WorldManager.GetArea(WorldManager.s_iCurrentWorldId, WorldManager.s_iCurrentAreaIndex);
        DungeonTable dungeonTable = WorldManager.GetDungeonTable(areaTabe.ID, WorldManager.s_iCurrentDungeonIndex);
        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Hiden();
        GUI_FUNCTION.SHOW_STORY(dungeonTable.StoryID, GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_AREA).Show);

        return true;
    }
}
