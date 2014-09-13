
using System.Collections.Generic;


//  MonsterBaoxiangTable.cs
//  Author: Lu Zexi
//  2013-12-06




/// <summary>
/// 特殊怪物表
/// </summary>
public class MonsterBaoxiangTable : MonsterTable
{
    private int m_iID;  //唯一ID
    public int ID
    {
        get { return this.m_iID; }
    }
    
    public MonsterBaoxiangTable()
    { 
        //
    }

    /// <summary>
    /// 读取文本
    /// </summary>
    /// <param name="lstInfo"></param>
    /// <param name="index"></param>
    public override void ReadText(List<string> lstInfo, ref int index)
    {
        this.m_iID = INT(lstInfo[index++]);
        base.ReadText(lstInfo, ref index);
    }

}
