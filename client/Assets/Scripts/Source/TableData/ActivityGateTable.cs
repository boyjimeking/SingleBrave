using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//  ActivityGateTable.cs
//  Author: Lu Zexi
//  2014-01-02

/// <summary>
/// 活动关卡表
/// </summary>
public class ActivityGateTable : TableBase
{
    private int m_iID;  //唯一标识
    public int ID
    {
        get { return this.m_iID; }
    }
    private string m_strName;   //名字
    public string Name
    {
        get { return this.m_strName; }
    }
    private string m_strScene;  //场景
    public string Scene
    {
        get { return this.m_strScene; }
    }
    private string m_strBGSound;    //背景音乐
    public string BGSound
    {
        get { return this.m_strBGSound; }
    }
    private int m_iCostHP;  //消耗体力
    public int CostHP
    {
        get { return this.m_iCostHP; }
    }
    private int m_iMaxLayer;    //最大层数
    public int MaxLayer
    {
        get { return this.m_iMaxLayer; }
    }
    private int m_iMaxGold; //最大金币数
    public int MaxGold
    {
        get { return this.m_iMaxGold; }
    }
    private int m_iMaxFarm; //最大农场点
    public int MaxFarm
    {
        get { return this.m_iMaxFarm; }
    }
    private int[] m_vecLayerNum;    //层级序列
    public int[] VecLayerNum
    {
        get { return this.m_vecLayerNum; }
    }
    private int m_iJingYing;    //精英怪
    public int JingYing
    {
        get { return this.m_iJingYing; }
    }
    private float m_fJingYingRate;  //精英怪概率
    public float JingYingRate
    {
        get { return this.m_fJingYingRate; }
    }
    private float m_fDropRateMonsterL;  //宝箱怪概率
    public float DropRateMonsterL
    {
        get { return this.m_fDropRateMonsterL; }
    }
    private int m_iBoxID;   //宝箱ID
    public int BoxID
    {
        get { return this.m_iBoxID; }
    }
    private float m_fDropRateBox;   //宝箱掉落率
    public float DropRateBox
    {
        get { return this.m_fDropRateBox; }
    }
    private int m_iDropGold;    //掉落金币
    public int DropGold
    {
        get { return this.m_iDropGold; }
    }
    private float m_fDropGoldRate;  //金币掉率率
    public float DropGoldRate
    {
        get { return this.m_fDropGoldRate; }
    }
    private int m_iDropFarm;    //掉落农场点
    public int DropFarm
    {
        get { return this.m_iDropFarm; }
    }
    private float m_fDropFarmRate;    //掉落农场点几率
    public float DropFarmRate
    {
        get { return this.m_fDropFarmRate; }
    }
    private int m_iDropAnger;   //掉落怒气点
    public int DropAnger
    {
        get { return this.m_iDropAnger; }
    }
    private float m_fDropAngerRate; //掉落怒气率
    public float DropAgerRate
    {
        get { return this.m_fDropAngerRate; }
    }
    private int m_iDropHeart;   //掉落心
    public int DropHeart
    {
        get { return this.m_iDropHeart; }
    }
    private float m_fDropHeartRate; //掉落心几率
    public float DropHeartRate
    {
        get { return this.m_fDropHeartRate; }
    }
    private float m_fDropItemRate;  //物品掉落率
    public float DropItemRate
    {
        get { return this.m_fDropItemRate; }
    }
    private List<int> m_vecDropItemID;  //掉落ID
    public List<int> VecDropItemID
    {
        get { return this.m_vecDropItemID; }
    }
    private List<float> m_vecDropRate;  //掉落率
    public List<float> VecDropRate
    {
        get { return this.m_vecDropRate; }
    }
    private int m_iRewardFram; //奖励的农场点
    public int RewardFram
    {
        get { return this.m_iRewardFram; }
    }
    private int m_iRewardJinbi; //奖励金币
    public int RewardJinbi
    {
        get { return this.m_iRewardJinbi; }
    }
    private int m_iRewardExp; //获得经验
    public int RewardExp
    {
        get { return this.m_iRewardExp; }
    }
    private string m_strDesc;//描述
    public string Desc
    {
        get { return this.m_strDesc; }
    }

    public ActivityGateTable()
    {
        this.m_vecLayerNum = new int[15];
        this.m_vecDropItemID = new List<int>();
        this.m_vecDropRate = new List<float>();
    }

    /// <summary>
    /// 读取文本
    /// </summary>
    /// <param name="lstInfo"></param>
    /// <param name="index"></param>
    public override void ReadText(List<string> lstInfo, ref int index)
    {
        this.m_iID = INT(lstInfo[index++]);
        this.m_strName = STRING(lstInfo[index++]);
        this.m_strScene = STRING(lstInfo[index++]);
        this.m_strBGSound = STRING(lstInfo[index++]);
        this.m_iCostHP = INT(lstInfo[index++]);
        this.m_iMaxLayer = INT(lstInfo[index++]);
        this.m_iMaxGold = INT(lstInfo[index++]);
        this.m_iMaxFarm = INT(lstInfo[index++]);
        for( int i = 0 ; i<this.m_vecLayerNum.Length ; i++ )
        {
            this.m_vecLayerNum[i] = INT(lstInfo[index++]);
        }
        this.m_iJingYing = INT(lstInfo[index++]);
        this.m_fJingYingRate = FLOAT(lstInfo[index++]);
        this.m_fDropRateMonsterL = FLOAT(lstInfo[index++]);
        this.m_iBoxID = INT(lstInfo[index++]);
        this.m_fDropRateBox = FLOAT(lstInfo[index++]);
        this.m_iDropGold = INT(lstInfo[index++]);
        this.m_fDropGoldRate = FLOAT(lstInfo[index++]);
        this.m_iDropFarm = INT(lstInfo[index++]);
        this.m_fDropFarmRate = FLOAT(lstInfo[index++]);
        this.m_iDropAnger = INT(lstInfo[index++]);
        this.m_fDropAngerRate = FLOAT(lstInfo[index++]);
        this.m_iDropHeart = INT(lstInfo[index++]);
        this.m_fDropHeartRate = FLOAT(lstInfo[index++]);
        this.m_fDropItemRate = FLOAT(lstInfo[index++]);
        string strDropItem = STRING(lstInfo[index++]);
        string[] vecDropItem = strDropItem.Split('|');
        for (int i = 0; i < vecDropItem.Length; i++)
        {
            if (vecDropItem[i] == "")
                continue;
            this.m_vecDropItemID.Add(INT(vecDropItem[i].Split(':')[0]));
            this.m_vecDropRate.Add(FLOAT(vecDropItem[i].Split(':')[1]));
        }
        this.m_iRewardFram = INT(lstInfo[index++]);
        this.m_iRewardJinbi = INT(lstInfo[index++]);
        this.m_iRewardExp = INT(lstInfo[index++]);
        this.m_strDesc = STRING(lstInfo[index++]);
    }
}

