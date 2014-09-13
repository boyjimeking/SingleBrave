using System;
using System.Collections.Generic;

using Game.Network;

//  VersionAckPkt.cs
//  Author: Lu Zexi
//  2013-12-11



/// <summary>
/// 版本应答数据包
/// </summary>
public class VersionAckPkt : HTTPPacketAck
{
    //版本
    public int m_iVersion;  //版本
    public string m_strVersionPath; //版本更新地址
    public string m_strResTxtPath;  //资源文件地址
    public int m_iResVersion;   //资源版本

    // public VersionAckPkt()
    // {
    //     this.m_strAction = PACKET_DEFINE.VERSION_REQ;
    // }
}



// /// <summary>
// /// 版本应答数据包工厂类
// /// </summary>
// public class VersionAckPktFactory : HTTPPacketFactory
// {
//     /// <summary>
//     /// 获取Action
//     /// </summary>
//     /// <returns></returns>
//     public override string GetPacketAction()
//     {
//         return PACKET_DEFINE.VERSION_REQ;
//     }

//     /// <summary>
//     /// 创建数据包
//     /// </summary>
//     /// <param name="json"></param>
//     /// <returns></returns>
//     public override HTTPPacketRequest Create(IJSonObject json)
//     {
//         VersionAckPkt ack = PACKET_HEAD.PACKET_ACK_HEAD<VersionAckPkt>(json);

//         IJSonObject data = json["data"];
//         ack.m_iVersion = data["version"].Int32Value;
// //#if UNITY_EDITOR
// //        ack.m_strVersionPath = data["wintest_version_path"].StringValue;
// //        ack.m_strResTxtPath = data["wintest_res_path"].StringValue;
// #if UNITY_STANDALONE
//         ack.m_strVersionPath = data["win_version_path"].StringValue;
//         ack.m_strResTxtPath = data["win_res_path"].StringValue;
//         ack.m_iResVersion = data["win_version"].Int32Value;
// #elif UNITY_IPHONE
//         ack.m_strVersionPath = data["ios_version_path"].StringValue;
//         ack.m_strResTxtPath = data["ios_res_path"].StringValue;
//         ack.m_iResVersion = data["ios_version"].Int32Value;
// #elif UNITY_ANDROID
//         ack.m_strVersionPath = data["android_version_path"].StringValue;
//         ack.m_strResTxtPath = data["android_res_path"].StringValue;
//         ack.m_iResVersion = data["ios_version"].Int32Value;
// #endif
//         return ack;
//     }

// }
