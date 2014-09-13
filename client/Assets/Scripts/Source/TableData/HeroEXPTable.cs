//Micro.Sanvey
//2013.12.4
//sanvey.china@gmail.com

using System.Collections.Generic;

/// <summary>
/// 英雄经验曲线表
/// </summary>
public class HeroEXPTable : TableBase
{

    private int m_iLevel;  //等级
    public int Level
    {
        get { return this.m_iLevel; }
    }
    private int[] m_vecEXP;  //经验
    public int[] VecEXP
    {
        get { return this.m_vecEXP; }
    }


    public HeroEXPTable()
        : base()
    {
        this.m_vecEXP = new int[3];
    }


    /// <summary>
    /// 读取数据
    /// </summary>
    /// <param name="lstInfo"></param>
    /// <param name="index"></param>
    public override void ReadText(List<string> lstInfo, ref int index)
    {
        this.m_iLevel = INT(lstInfo[index++]);
        for (int i = 0; i < this.m_vecEXP.Length; i++)
        {
            this.m_vecEXP[i] = INT(lstInfo[index++]);
        }

    }
}
