using System;
using System.Collections.Generic;
using CodeTitans.JSon;
using Game.Network;

//  BuildingInfoGetPktAck.cs
//  Author: sanvey
//  2013-12-13


/// <summary>
/// 获取建筑信息应答数据包
/// </summary>
public class BuildingInfoGetPktAck : HTTPPacketBase
{

    public int m_iPid;        //用户Pid
    public int m_iEquipLv;    //装备合成建筑等级
    public int m_iEquipExp;   //装备合成建筑经验
    public int m_iItemLv;     //消耗品合成建筑等级
    public int m_iItemExp;    //消耗品合成建筑经验

    public int m_iShanLv;     //山建筑等级
    public int m_iShanExp;    //山建筑经验
    public int m_iChuanLv;    //川建筑等级
    public int m_iChuanExp;   //川建筑经验
    public int m_iTianLv;     //田建筑等级
    public int m_iTianExp;    //田建筑经验
    public int m_iLinLv;      //林建筑等级
    public int m_iLinExp;     //林建筑经验
    public int m_iShanCollectNum;     //山资源点击剩余数量
    public int m_iChuanCollectNum;    //川资源点击剩余数量
    public int m_iTianCollectNum;     //田资源点击剩余数量
    public int m_iLinCollectNum;      //林资源点击剩余数量
    public long m_lShanClickTime;      //山资源点击时间
    public long m_lChuanClickTime;     //川资源点击时间
    public long m_lTianClickTime;      //田资源点击时间
    public long m_lLinClickTime;       //林资源点击时间

    public BuildingInfoGetPktAck()
    {
        this.m_strAction = PACKET_DEFINE.GET_BUILDING_GET_REQ;
    }
}


/// <summary>
/// 获取建筑信息应答数据包工厂类
/// </summary>
public class BuildingInfoGetPktAckFactory : HTTPPacketFactory
{

    /// <summary>
    /// 获取数据包Action
    /// </summary>
    /// <returns></returns>
    public override string GetPacketAction()
    {
        return PACKET_DEFINE.GET_BUILDING_GET_REQ;
    }

    /// <summary>
    /// 创建数据包
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public override HTTPPacketBase Create(IJSonObject json)
    {
        BuildingInfoGetPktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<BuildingInfoGetPktAck>(json);

        if (pkt.m_iErrorCode != 0)
        {
            return pkt;
        }

        IJSonObject data = json["data"];

        pkt.m_iPid = data["pid"].Int32Value;
        pkt.m_iEquipLv = data["equip_lv"].Int32Value;
        pkt.m_iEquipExp = data["equip_exp"].Int32Value;
        pkt.m_iItemLv = data["item_lv"].Int32Value;
        pkt.m_iItemExp = data["item_exp"].Int32Value;

        pkt.m_iShanLv = data["shan_lv"].Int32Value;
        pkt.m_iShanExp = data["shan_exp"].Int32Value;
        pkt.m_iChuanLv = data["chuan_lv"].Int32Value;
        pkt.m_iChuanExp = data["chuan_exp"].Int32Value;
        pkt.m_iTianLv = data["tian_lv"].Int32Value;
        pkt.m_iTianExp = data["tian_exp"].Int32Value;
        pkt.m_iLinLv = data["lin_lv"].Int32Value;
        pkt.m_iLinExp = data["lin_exp"].Int32Value;
        pkt.m_iShanCollectNum = data["shan_collect"].Int32Value;
        pkt.m_iChuanCollectNum = data["chuan_collect"].Int32Value;
        pkt.m_iTianCollectNum = data["tian_collect"].Int32Value;
        pkt.m_iLinCollectNum = data["lin_collect"].Int32Value;
        pkt.m_lShanClickTime = data["shan_c_time"].Int64Value;
        pkt.m_lChuanClickTime = data["chuan_c_time"].Int64Value;
        pkt.m_lTianClickTime = data["tian_c_time"].Int64Value;
        pkt.m_lLinClickTime = data["lin_c_time"].Int64Value;

        return pkt;

    }
}