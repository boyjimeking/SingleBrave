using UnityEngine;
using System.Collections;
using Game.Network;

//  GetPlayerInfoPktReq.cs
//  Author: sanvey
//  2013-12-11

/// <summary>
/// 创建玩家信息请求包
/// </summary>
public class PlayerCreatePktReq : HTTPPacketBase
{
    public int m_strUID;  //用户UID
    public string m_strNickName;  //用好昵称
    public int m_iSelectHeroIndex;  //选择英雄索引
    public string m_strDeviceID;    //设备ID
    public string m_strChannel; //渠道

    public PlayerCreatePktReq()
    {
        this.m_strAction = PACKET_DEFINE.CREATE_PLAY_REQ;
    }

    /// <summary>
    /// 获取请求参数
    /// </summary>
    /// <returns></returns>
    public override string GetRequire()
    {
        string req = string.Format("uid={0}&nickname={1}&select_hero_index={2}&deviceID={3}&channel={4}", m_strUID, m_strNickName, m_iSelectHeroIndex,this.m_strDeviceID , this.m_strChannel);

        PACKET_HEAD.PACKET_REQ_END(ref req);

        return req;
    }
}