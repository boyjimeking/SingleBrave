using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;
using Game.Base;


//  ActivityBattleEndPktReq.cs
//  Author: Lu Zexi
//  2014-01-08



/// <summary>
/// 活动战斗结束请求
/// </summary>
public class ActivityBattleEndPktReq :HTTPPacketBase
{
    public int m_iPid;  //玩家ID
    public int m_iBattleID; //战斗ID
    public int m_iFubenID;   //副本索引
    public int m_iGateIndex;    //关卡索引
    public int m_iGold; //金币数量
    public int m_iFarm; //农场点
    public int m_iFriendBattleID;   //战友ID
    public List<int> m_lstHero = new List<int>();   //英雄列表
    public List<int> m_lstItem = new List<int>();   //物品列表
    public List<int> m_lstItemNum = new List<int>();    //物品数量
    public int[] m_vecConsume = new int[5]; //消耗品剩余数量

    //统计信息
    public int m_iRecordMaxShuijingNum;    //单场战斗记录水晶数
    public int m_iTotalShuijingNum;    //总水晶数量
    public int m_iRecordMaxXinNum;    //单场战斗记录心数量
    public int m_iTotalXinNum; //总心数量
    public int m_iRoundMaxHurt;    //回合内最大总伤害
    public int m_iRoundMaxSparkNum;    //回合内最多Spark次数
    public int m_iTotalSparkNum;   //总Spark次数
    public int m_iTotalSkillNum;   //总技能使用次数
    public int m_iTotalBoxMonster;  //总宝箱怪出现次数

    public ActivityBattleEndPktReq()
    {
        this.m_strAction = PACKET_DEFINE.ACTIVITY_BATTLE_END_REQ;
    }

    /// <summary>
    /// 获取请求
    /// </summary>
    /// <returns></returns>
    public override string GetRequire()
    {
        string req = "";

        req = "pid=" + this.m_iPid + "&battle_id=" + this.m_iBattleID + "&fuben_id=" + this.m_iFubenID + "&gate_index=" + this.m_iGateIndex;
        req += "&gold=" + this.m_iGold + "&farmpoint=" + this.m_iFarm + "&friendbattle_id=" + this.m_iFriendBattleID;

        //统计信息
        req += "&max_shuijing=" + this.m_iRecordMaxShuijingNum + "&total_shuijing=" + this.m_iTotalShuijingNum + "&max_xin=" + this.m_iRecordMaxXinNum + "&total_xin=" + this.m_iTotalXinNum +
            "&round_max_hurt=" + this.m_iRoundMaxHurt + "&round_spark_num=" + this.m_iRoundMaxSparkNum + "&total_spark_num=" + this.m_iTotalSparkNum + "&total_skill_num=" + this.m_iTotalSkillNum +
            "&total_box_monster=" + this.m_iTotalBoxMonster;

        req += "&heros=";
        UnityEngine.Debug.Log("heros " + this.m_lstHero.Count);
        if (this.m_lstHero != null && this.m_lstHero.Count > 0)
        {
            req += this.m_lstHero[0];
            for (int i = 1; i < this.m_lstHero.Count; i++)
            {
                req += "|" + this.m_lstHero[i];
            }
        }

        req += "&items=";
        if ( this.m_lstItem != null && this.m_lstItem.Count > 0)
        {
            req += this.m_lstItem[0] + ":" + this.m_lstItemNum[0];
            for (int i = 1; i < this.m_lstItem.Count; i++)
            {
                req += "|" + this.m_lstItem[i] + ":" + this.m_lstItemNum[i];
            }
        }

        req += "&readyitem=" + this.m_vecConsume[0];
        for (int i = 1; i < this.m_vecConsume.Length; i++)
        {
            req += "|" + this.m_vecConsume[i];
        }

        PACKET_HEAD.PACKET_REQ_END(ref req);

        return req;
    }
}
