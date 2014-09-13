using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Network;

//  BuildingInfoGetHandle.cs
//  Author: sanvey
//  2013-12-13


/// <summary>
/// 获取建筑信息请求应答句柄
/// </summary>
public class BuildingInfoGetHandle
{
    /// <summary>
    /// 获取Action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.GET_BUILDING_GET_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketRequest packet)
    {
        BuildingInfoGetPktAck ack = (BuildingInfoGetPktAck)packet;
        GAME_LOG.LOG("code :" + ack.m_iErrorCode);
        GAME_LOG.LOG("desc :" + ack.m_strErrorDes);

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);
            return false;
        }

        //存储建筑数据
        Building equip = new Building(BUILDING_TYPE.EQUIP, ack.m_iEquipLv, ack.m_iEquipExp);
        Role.role.GetBuildingProperty().AddBuild(equip);

        Building item = new Building(BUILDING_TYPE.ITEM, ack.m_iItemLv, ack.m_iItemExp);
        Role.role.GetBuildingProperty().AddBuild(item);


        Building shan = new Building(BUILDING_TYPE.SHAN, ack.m_iShanLv, ack.m_iShanExp);
        shan.m_iCollectNum = ack.m_iShanCollectNum;
        shan.m_lCollectTime = ack.m_lShanClickTime;
        Role.role.GetBuildingProperty().AddBuild(shan);

        Building chuan = new Building(BUILDING_TYPE.CHUAN, ack.m_iChuanLv, ack.m_iChuanExp);
        chuan.m_iCollectNum = ack.m_iChuanCollectNum;
        chuan.m_lCollectTime = ack.m_lChuanClickTime;
        Role.role.GetBuildingProperty().AddBuild(chuan);

        Building tian = new Building(BUILDING_TYPE.TIAN, ack.m_iTianLv, ack.m_iTianExp);
        tian.m_iCollectNum = ack.m_iTianCollectNum;
        tian.m_lCollectTime = ack.m_lTianClickTime;
        Role.role.GetBuildingProperty().AddBuild(tian);

        Building lin = new Building(BUILDING_TYPE.LIN, ack.m_iLinLv, ack.m_iLinExp);
        lin.m_iCollectNum = ack.m_iLinCollectNum;
        lin.m_lCollectTime = ack.m_lLinClickTime;
        Role.role.GetBuildingProperty().AddBuild(lin);

        SendAgent.SendItemGetReq(Role.role.GetBaseProperty().m_iPlayerId);

        return true;
    }
}