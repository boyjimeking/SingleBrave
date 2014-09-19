using UnityEngine;
using System.Collections;
using Game.Network;

//  HeroEquipUpdatePktReq.cs
//  Author: sanvey
//  2013-12-20

/// <summary>
/// 英雄装备道具请求
/// </summary>
public class HeroEquipUpdatePktReq : HTTPPacketRequest
{
    public int[] m_vecHeros;  //英雄列表
    public int[] m_vecItems;  //装备列表
    public int m_iPid;        //Pid

    public HeroEquipUpdatePktReq()
    {
        this.m_strAction = PACKET_DEFINE.HERO_EQUIP_UPDATE_REQ;
    }
}



/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 英雄装备更新请求
	/// </summary>
	/// <param name="pid"></param>
	/// <param name="heros"></param>
	/// <param name="items"></param>
	public static void SendHeroEquipUpdateReq(int pid, int[] heros, int[] items)
	{
		HeroEquipUpdatePktReq req = new HeroEquipUpdatePktReq();
		req.m_iPid = pid;
		req.m_vecHeros = heros;
		req.m_vecItems = items;
		
		SessionManager.GetInstance().SendReady(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}