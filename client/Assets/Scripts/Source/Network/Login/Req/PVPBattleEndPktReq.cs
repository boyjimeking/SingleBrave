using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;


//  PVPBattleEndPktReq.cs
//  Author: Lu Zexi
//  2014-02-11




/// <summary>
/// PVP战斗结束
/// </summary>
public class PVPBattleEndPktReq : HTTPPacketBase
{
    public int m_iBattleID; //炸浓度ID
    public int m_iPid;  //角色ID
    public int m_iTpid; //对手ID
    public int m_iResult;   //结果
    public List<int> m_lstKillHeroTableID;  //杀死的英雄配置ID
    public int m_iTotalSkillKillNum;    //技能KO次数
    public int m_iTotalSuperHurt;   //鞭尸伤害总数
    public int m_iRoundMaxHurt; //回合内最大伤害
    public int m_iRoundMaxSpark;    //回合内SPARK次数
    public int m_iTotalHurt;    //累计伤害总数
    public int m_iTotalRecover; //累计回复数
    public int m_iTotalSpark;   //累计SPARK
    public int m_iTotalSkillNum;    //累计技能使用次数
    public int m_iWinType;  //胜利类型

    public PVPBattleEndPktReq()
    {
        this.m_strAction = PACKET_DEFINE.PVP_BATTLE_END_REQ;
    }

    /// <summary>
    /// 获取请求参数
    /// </summary>
    /// <returns></returns>
    public override string GetRequire()
    {
        string str = "" + string.Format("pid={0}&battle_id={1}&result={2}", this.m_iPid, this.m_iBattleID, this.m_iResult);

        str += "&kill_hero=";
        for (int i = 0; i < this.m_lstKillHeroTableID.Count; i++ )
        {
            if (i == 0)
            {
                str += this.m_lstKillHeroTableID[i];
            }
            else
            {
                str += "|" + this.m_lstKillHeroTableID[i];
            }
        }
        str += "&tpid=" + this.m_iTpid;
        str += "&skill_kill="+this.m_iTotalSkillKillNum;
        str += "&super_hurt=" + this.m_iTotalSuperHurt;
        str += "&round_maxhurt=" + this.m_iRoundMaxHurt;
        str += "&round_maxspark=" + this.m_iRoundMaxSpark;
        str += "&total_hurt=" + this.m_iTotalHurt;
        str += "&total_recover=" + this.m_iTotalRecover;
        str += "&total_spark=" + this.m_iTotalSpark;
        str += "&total_skill=" + this.m_iTotalSkillNum;
        str += "&win_type=" + this.m_iWinType;

        PACKET_HEAD.PACKET_REQ_END(ref str);

        return str;
    }
}