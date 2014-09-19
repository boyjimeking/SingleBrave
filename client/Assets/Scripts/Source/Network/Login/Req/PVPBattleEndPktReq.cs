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
public class PVPBattleEndPktReq : HTTPPacketRequest
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
}



/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 发送战斗结束
	/// </summary>
	/// <param name="pid"></param>
	/// <param name="battle_id"></param>
	/// <param name="result"></param>
	public static void SendPVPBattleEnd(int pid , int tpid , int battle_id, int result ,
	                                    List<int> killHeros , int totalSkillKillNum , int totalSuperHurt , int roundMaxHurt , int roundMaxSpark ,
	                                    int totalHurt , int totalRecover , int totalSpark , int totalSkillNum , int winType
	                                    )
	{
		
		PVPBattleEndPktReq req = new PVPBattleEndPktReq();
		req.m_iPid = pid;
		req.m_iTpid = tpid;
		req.m_iBattleID = battle_id;
		req.m_iResult = result;
		req.m_lstKillHeroTableID = killHeros;
		req.m_iTotalSkillKillNum = totalSkillKillNum;
		req.m_iTotalSuperHurt = totalSuperHurt;
		req.m_iRoundMaxHurt = roundMaxHurt;
		req.m_iRoundMaxSpark = roundMaxSpark;
		req.m_iTotalHurt = totalHurt;
		req.m_iTotalRecover = totalRecover;
		req.m_iTotalSpark = totalSpark;
		req.m_iTotalSkillNum = totalSkillNum;
		req.m_iWinType = winType;
		
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}


