using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//玩家名称表
//Author sunyi
//2014-03-12
public class PlayerNameTable : TableBase
{
    private string m_strPlayerName;

    public string PlayerName
    {
        get { return m_strPlayerName; }
    }

    /// <summary>
    /// 读取文本
    /// </summary>
    /// <param name="lstInfo"></param>
    /// <param name="index"></param>
    public override void ReadText(List<string> lstInfo, ref int index)
    {
        this.m_strPlayerName = STRING(lstInfo[index++]);
    }
}

