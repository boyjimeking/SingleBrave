using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  ItemCompositeTable.cs
//  Author: Lu Zexi
//  2013-12-11



/// <summary>
/// 物品合成表
/// </summary>
public class ItemCompositeTable : TableBase
{
    private int m_iID;  //合成物品ID
    public int ID
    {
        get { return this.m_iID; }
    }
    private string m_strName;   //名字
    public string Name
    {
        get { return this.m_strName; }
    }
    private int m_iType;    //类型
    public int Type
    {
        get { return this.m_iType; }
    }
    private List<int> m_lstNeedID;  //所需物品ID
    public List<int> LstNeedID
    {
        get { return new List<int>(this.m_lstNeedID); }
    }
    private List<int> m_lstNeedNum; //所需物品数量
    public List<int> LstNeedNum
    {
        get { return new List<int>(this.m_lstNeedNum); }
    }
    private int m_iNeedFarmPoint;
    public int NeedFarmPoint
    {
        get { return m_iNeedFarmPoint; }
    }

    public ItemCompositeTable()
    {
        this.m_lstNeedID = new List<int>();
        this.m_lstNeedNum = new List<int>();
    }

    /// <summary>
    /// 读取文本数据
    /// </summary>
    /// <param name="lstInfo"></param>
    /// <param name="index"></param>
    public override void ReadText(List<string> lstInfo, ref int index)
    {
        base.ReadText(lstInfo, ref index);
        this.m_lstNeedID.Clear();
        this.m_lstNeedNum.Clear();

        this.m_iID = INT(lstInfo[index++]);
        this.m_strName = STRING(lstInfo[index++]);
        this.m_iType = INT(lstInfo[index++]);
        string str = STRING(lstInfo[index++]);
        string[] lst = str.Split('|');

        for (int i = 0; i < lst.Length; i++)
        {
            if (string.IsNullOrEmpty(lst[i]))
                continue;
            string con = lst[i];
            string[] conlst = con.Split(':');
            this.m_lstNeedID.Add(INT(conlst[0]));
            this.m_lstNeedNum.Add(INT(conlst[1]));
        }

        this.m_iNeedFarmPoint = INT(lstInfo[index++]);
    }
}
