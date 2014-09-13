using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 制作人员表
/// </summary>
public class ProductionStaffTable : TableBase
{
    private int m_iId; //标识ID
    public int ID
    {
        get
        {
            return m_iId;
        }
    }

    private int m_iPositionId; //职位ID
    public int PositionID
    {
        get
        {
            return m_iPositionId;
        }
    }

    private string m_cPositionName; //职位名称
    public string PositionName
    {
        get
        {
            return m_cPositionName;
        }
    }

    private string m_cStaffName; //制作人名字
    public string StaffName
    {
        get
        {
            return m_cStaffName;
        }
    }

    public override void ReadText(List<string> lstInfo, ref int index)
    {
        this.m_iId = INT(lstInfo[index++]);
        this.m_iPositionId = INT(lstInfo[index++]);
        this.m_cPositionName = STRING(lstInfo[index++]);
        this.m_cStaffName = STRING(lstInfo[index++]);
    }
}
