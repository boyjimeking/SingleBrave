using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//玩家名称敏感词汇表
//Author sunyi
//2014-03-12
public class PlayerNameSensitiveWordTable : TableBase
{
    private string m_strSensitiveWord;

    public string SensitiveWord
    {
        get { return m_strSensitiveWord; }
    }

    /// <summary>
    /// 读取文本
    /// </summary>
    /// <param name="lstInfo"></param>
    /// <param name="index"></param>
    public override void ReadText(List<string> lstInfo, ref int index)
    {
        this.m_strSensitiveWord = STRING(lstInfo[index++]);
    }
}

