using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using UnityEngine;


//  RoleBaseProperty.cs
//  Author: Lu Zexi
//  2013-11-14


/// <summary>
/// 角色基础属性
/// </summary>
public class RoleBaseProperty
{
    public int m_iPlayerId;   //用户ID
    public string m_strUserName;    //用户名字//
    public int m_iLevel;   //用户等级//
    public int m_iCurrentExp;  //用户当前经验//
    public int strength;
    public int sportpoint;
    public int m_iStrength  //用户当前体力//
    {
        get { return strength; }
        set
        {
            GUIBackFrameTop top = (GUIBackFrameTop)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP);
            top.m_bIsUpdateIng = true;
            if (strength != value)
            {
                m_fStrengthNext += (strength - value) * GUIBackFrameTop.STRENGTH_PER;
            }
            strength = value;
            top.m_bIsUpdateIng = false;
        }
    }
    public int m_iSportPoint  //用户竞技点//
    {
        get { return sportpoint; }
        set
        {
            GUIBackFrameTop top = (GUIBackFrameTop)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP);
            top.m_bIsUpdateIng = true;
            if (sportpoint != value)
            {
               this.m_fSportNext += (sportpoint - value) * GUIBackFrameTop.PVP_PER;
            }
            sportpoint = value;
            top.m_bIsUpdateIng = false;
        }
    }
    public int m_iDiamond;  //用户所有钻石//
    public int m_iGold;    //用户所有金币//
    public int m_iFarmPoint;   //用户农场点//
    public long m_iSportTime;
    public long m_iStrengthTime;
    public int m_iFriendPoint;  //友情点//
    public int m_iCurrentTeam;  //当前队伍索引
    public int m_iMaxHeroCount;  //用户可以拥有最大的英雄数量  sanvey 11.29 add 
    public int m_iMaxItem;  //用户可以拥有的最大物体
    public int m_iCreateTime;  //创建时间
    public int[] m_vecWantItems; //想要好友赠送的礼物
    public int m_iBattleID; //战斗ID
    public string m_strZhaoDaiId; //招待ID
    public string m_strSignature;  //玩家签名
    public int m_iPVPExp;  //用户当前竞技经验
    public int m_iPVPMaxExp;  //用户最大竞技经验
    public int m_iPVPWin;  //用户竞技胜利场次
    public int m_iPVPLose;  //用户竞技失败场次
    public int m_iPVPRank; //用户竞技场排行
    //上一次竞技场敌方信息
    public int m_iEnemyPVPEXP; //竞技场敌方经验
    public string m_strEnemyName; //竞技场敌方名称
    public string m_strEnemySignture; //竞技场敌方名称
    public int m_iEnemyLevel;//竞技场敌方等级
    public int m_iEnemyPid; //竞技场敌方Pid
    public List<PVPItem> m_lstWeekRank = new List<PVPItem>();  //所有排名
    public int m_iMyWeekRank;  //我的周积分排名
    public int m_iMyWeekPoint; //我的周积分
    public int m_iModelID;    //新手引导模块
    public int m_iLoginTimes;//连续登录次数
    public int m_iMailCounts;//系统礼物总数
    public int m_iFriendApplyCount;//好友申请总数
    public int m_iFriendGiftCount;//好友礼物总数
    //top gui data
    public float m_fTopTime = -1;  //顶部界面时间
    public float m_fTopTimeSport = -1;  //顶部界面时间
    public float m_fStrengthNext ;   //体力计数器
    public float m_fSportNext;         //竞技点计数
    public bool m_bIsNeedShowStory; //是否显示下一张引导
    public int m_iStoryID;

    /// <summary>
    /// 排名显示单项
    /// </summary>
    public class PVPItem
    {
        public string m_strName;
        public int m_iHeroTableID;
        public int m_iHeroLv;
        public int m_iPoint;
        public int m_iWinNum;
        public int m_iLoseNum;

    }

    public RoleBaseProperty()
    {
        //this.m_iPlayerId = 32525353;
        //this.m_strUserName = "刘备";
        //this.m_iLevel = 12;
        //this.m_iCurrentExp = 40;
        //this.m_iStrength = 16;
        //this.m_iDiamond = 88;
        //this.m_iGold = 885588;
        //this.m_iFarmPoint = 888888;
        //this.m_iSportPoint = 2;
        //this.m_iFriendPoint = 40;
        //this.m_iWorld = 1;
        //this.m_iArea = 2;
        //this.m_iTask = 2;
        //this.m_iCustomPass = 3;
        //this.m_iCurrentTeam = 0;
        //this.m_iMaxHeroCount = 50;
        //this.m_iMaxFriendCount = 40;
        //this.m_iMaxItem=60;
        //this.m_iCurrentAthleticsEXP = 120;
        //this.m_iCurrentAthleticsLevel = 2;
        ////数据表
        //this.m_iCost = GetMaxCost(m_iLevel);
    }

}
