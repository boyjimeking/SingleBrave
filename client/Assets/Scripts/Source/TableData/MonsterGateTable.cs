using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  MonsterGateTable.cs
//  Author: Lu Zexi
//  2014-01-02



/// <summary>
/// 怪物关卡表
/// </summary>
public class MonsterGateTable : MonsterTable
{
    protected int m_iID;    //主键ID
    public int ID
    {
        get { return this.m_iID; }
    }
    protected int m_iGateID;  //关卡ID
    public int GateID
    {
        get { return m_iGateID; }
    }
    protected int m_iOrderID; //序列号
    public int OrderID
    {
        get { return this.m_iOrderID; }
    }

    /// <summary>
    /// 读取数据
    /// </summary>
    /// <param name="lstInfo"></param>
    /// <param name="index"></param>
    public override void ReadText(List<string> lstInfo, ref int index)
    {
        this.m_iID = INT(lstInfo[index++]);
        this.m_iGateID = INT(lstInfo[index++]);
        this.m_iOrderID = INT(lstInfo[index++]);
        base.ReadText(lstInfo, ref index);
    }

}
