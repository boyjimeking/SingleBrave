using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Network;
//战绩记录数据请求类
//Author sunyi
//2014-02-28
public class PlayerBattleRecordGetPktReq : HTTPPacketBase
{
    public int m_iPid;//玩家id

    public PlayerBattleRecordGetPktReq()
    {
        this.m_strAction = PACKET_DEFINE.BATTLE_RECORD_GET_REQ;
    }

    /// <summary>
    /// 获取请求参数
    /// </summary>
    /// <returns></returns>
    public override string GetRequire()
    {
        string req = string.Format("pid={0}", this.m_iPid);

        PACKET_HEAD.PACKET_REQ_END(ref req);

        return req;
    }
}

