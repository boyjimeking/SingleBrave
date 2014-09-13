using UnityEngine;
using System.Collections;
using Game.Network;

//  SignatureUpdatePktReq.cs
//  Author: sanvey
//  2013-12-23

/// <summary>
/// 更新签名请求
/// </summary>
public class PlayerSignatureUpdatePktReq : HTTPPacketRequest
{
    public int m_iPid;  //Pid
    public string m_strSign;

    public PlayerSignatureUpdatePktReq()
    {
        this.m_strAction = PACKET_DEFINE.SIGN_UPDATE_REQ;
    }

    // /// <summary>
    // /// 获取请求参数
    // /// </summary>
    // /// <returns></returns>
    // public override string GetRequire()
    // {
    //     string req = string.Format("pid={0}&signature={1}", m_iPid.ToString(), m_strSign);
    //     PACKET_HEAD.PACKET_REQ_END(ref req);
    //     return req;
    // }
}