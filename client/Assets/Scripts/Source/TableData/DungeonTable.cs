using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



//  DungeonTable.cs
//  Author: Lu Zexi
//  2013-11-18




/// <summary>
/// 副本数据表
/// </summary>
public class DungeonTable : TableBase
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
    private string m_strDesc;   //说明
    public string Desc
    {
        get { return this.m_strDesc; }
    }
    private string m_strFavTime;    //优惠时间
    public string FavTime
    {
        get { return this.m_strFavTime; }
    }

    private FAV_TYPE m_eFavType;//优惠时间类型

    public FAV_TYPE FavType
    {
        get { return m_eFavType; }
    }

    private int m_iFavTimeDayOfWeek;//优惠时间星期几

    public int FavTimeDayOfWeek
    {
        get { return m_iFavTimeDayOfWeek; }
    }

    private int m_iFavTimeStart;//优惠时间开始时间

    public int FavTimeStart
    {
        get { return m_iFavTimeStart; }
    }

    private int m_iFavTimeEnd;//优惠时间结束时间

    public int FavTimeEnd
    {
        get { return m_iFavTimeEnd; }
    }
    
    private float m_fPosX;  //位置x
    public float PoxX
    {
        get { return this.m_fPosX; }
    }
    private float m_fPosY;  //位置y
    public float PosY
    {
        get { return this.m_fPosY; }
    }
    private int m_iStoryID; //剧情ID
    public int StoryID
    {
        get { return this.m_iStoryID; }
    }
    private int m_iGateNum; //关卡数量
    public int GateNum
    {
        get { return this.m_iGateNum; }
    }
    private int[] m_vecGate;    //关卡
    public int[] VecGate
    {
        get { return this.m_vecGate; }
    }

    public DungeonTable()
    { 
        this.m_vecGate = new int[10];
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
        this.m_strDesc = STRING(lstInfo[index++]);
        this.m_strFavTime = STRING(lstInfo[index++]);

        string[] tmp = FavTime.Split('|');
        if (tmp.Length >= 4)
        {
            this.m_eFavType = (FAV_TYPE)INT(tmp[0]);
            this.m_iFavTimeDayOfWeek = INT(tmp[1]);

            this.m_iFavTimeStart = INT(tmp[2].Split(':')[0]);
            this.m_iFavTimeEnd = INT(tmp[3].Split(':')[0]);
        }
        else
        {
            this.m_eFavType = FAV_TYPE.NONE;
            this.m_iFavTimeDayOfWeek = 0;
            this.m_iFavTimeStart = 0;
            this.m_iFavTimeEnd = 0;
        }
        this.m_fPosX = FLOAT(lstInfo[index++]);
        this.m_fPosY = FLOAT(lstInfo[index++]);
        this.m_iStoryID = INT(lstInfo[index++]);
        this.m_iGateNum = INT(lstInfo[index++]);
        for (int i = 0; i < this.m_vecGate.Length; i++)
        {
            this.m_vecGate[i] = INT(lstInfo[index++]);
        }
    }
    
}


