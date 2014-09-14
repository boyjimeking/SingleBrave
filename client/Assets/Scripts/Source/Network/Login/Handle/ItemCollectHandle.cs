using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Network;
using UnityEngine;

//  ItemCollectHandle.cs
//  Author: sanvey
//  2013-12-24

//物品采集请求应答句柄
public class ItemCollectHandle
{
    /// <summary>
    /// 获得Action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.ITEM_COLLECT_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketAck packet)
    {
        ItemCollectPktAck ack = (ItemCollectPktAck)packet;

        GAME_LOG.LOG("code :" + ack.header.code);
        GAME_LOG.LOG("desc :" + ack.header.desc);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.header.code != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.header.desc);
            return;
        }

        //返回Code 0表示成功
        if (ack.header.code == 0)
        {
            //清空零时
            Role.role.GetItemProperty().RemoveTmpItem();

            Role.role.GetBaseProperty().m_iGold = ack.m_iGold;  //更新采集回来的金币，服务器返回全量
            Role.role.GetBaseProperty().m_iFarmPoint = ack.m_iFarmPoint;  //更新采集回来的农场点，服务器返回全量
            //更新农场点金币
            GUIBackFrameTop guitop = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP) as GUIBackFrameTop;
            guitop.UpdateGold();
            guitop.UpdateFarmPiont();

            //更新各个采集数据
            Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.SHAN).m_iCollectNum = ack.m_cShan.m_iCollectNum;
            Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.SHAN).m_lCollectTime = ack.m_cShan.m_lCollectTime;

            Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.CHUAN).m_iCollectNum = ack.m_cChuan.m_iCollectNum;
            Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.CHUAN).m_lCollectTime = ack.m_cChuan.m_lCollectTime;

            Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.TIAN).m_iCollectNum = ack.m_cTian.m_iCollectNum;
            Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.TIAN).m_lCollectTime = ack.m_cTian.m_lCollectTime;

            Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.LIN).m_iCollectNum = ack.m_cLin.m_iCollectNum;
            Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.LIN).m_lCollectTime = ack.m_cLin.m_lCollectTime;

            //更新最新获得物品
            foreach (var q in ack.m_lstItems)
            {
                Item tmp = new Item(q.m_iItem_id);
                tmp.m_iID = q.m_iId;
                tmp.m_iNum = q.m_iItem_num;
                tmp.m_bNew = true;
                Role.role.GetItemProperty().AddItem(tmp);
            }

            //test
            //foreach (Item item in Role.role.GetItemProperty().GetAllItem())
            //{
            //    if (item.m_iID<10)
            //    {
            //        Debug.LogError("ID:  " + item.m_iID + "  TableID:  " + item.m_iTableID + "  Num:  " + item.m_iNum);
            //    }
            //}
        }

        SessionManager.GetInstance().CallBack();

        return;
    }
}