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
public class ActivityBattleEndPktReq :HTTPPacketRequest
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
}


/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 发送活动战斗结束请求
	/// </summary>
	/// <param name="pid"></param>
	/// <param name="fubenID"></param>
	/// <param name="gateIndex"></param>
	/// <param name="gold"></param>
	/// <param name="farm"></param>
	/// <param name="heros"></param>
	/// <param name="items"></param>
	/// <param name="itemsNum"></param>
	/// <param name="readyItem"></param>
	public static void SendActivityBattleEndReq(int pid , int battle_id , int fubenID, int gateIndex, int gold, int farm, int friendbattle_id, List<int> heros, List<int> items, List<int> itemsNum, int[] readyItem,
	                                            int RecordMaxShuijingNum, int TotalShuijingNum, int RecordMaxXinNum, int TotalXinNum, int RoundMaxHurt, int RoundMaxSparkNum, int TotalSparkNum, int TotalSkillNum , int totalBoxMonster
	                                            )
	{
		ActivityBattleEndPktReq req = new ActivityBattleEndPktReq();
		req.m_iPid = pid;
		req.m_iBattleID = battle_id;
		req.m_iFubenID = fubenID;
		req.m_iGateIndex = gateIndex;
		req.m_iGold = gold;
		req.m_iFarm = farm;
		req.m_iFriendBattleID = friendbattle_id;
		req.m_lstHero = heros;
		req.m_lstItem = items;
		req.m_lstItemNum = itemsNum;
		req.m_vecConsume = readyItem;
		
		//统计
		req.m_iRecordMaxShuijingNum = RecordMaxShuijingNum;
		req.m_iTotalShuijingNum = TotalShuijingNum;
		req.m_iRecordMaxXinNum = RecordMaxXinNum;
		req.m_iTotalXinNum = TotalXinNum;
		req.m_iRoundMaxHurt = RoundMaxHurt;
		req.m_iRoundMaxSparkNum = RoundMaxSparkNum;
		req.m_iTotalSparkNum = TotalSparkNum;
		req.m_iTotalSkillNum = TotalSkillNum;
		req.m_iTotalBoxMonster = totalBoxMonster;
		
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}
