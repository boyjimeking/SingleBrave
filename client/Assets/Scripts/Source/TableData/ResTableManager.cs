using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using System.Xml;

//  ResTableManager.cs
//  Author: Lu Zexi
//  2014-01-26





/// <summary>
/// 资源表管理类
/// </summary>
public class ResTableManager : Singleton<ResTableManager>
{
    private List<ResTable> m_lstRes = new List<ResTable>(); //资源表

    public ResTableManager()
    { 
        //
    }

    /// <summary>
    /// 获取所有资源表
    /// </summary>
    /// <returns></returns>
    public List<ResTable> GetAll()
    {
        return new List<ResTable>(this.m_lstRes);
    }

    /// <summary>
    /// 加载文本数据
    /// </summary>
    /// <param name="content"></param>
    public void LoadText(string content)
    {
        List<string> lst = new List<string>();
        string[] lst1 = content.Split('\n');

        for (int i = 0; i < lst1.Length; i++)
        {
            string[] tmp = lst1[i].Split(',');
            for (int j = 0; j < tmp.Length; j++)
            {
                if (tmp[j].Length > 0 && tmp[j][0] != '\n' && tmp[j][0] != '\r')
                {
                    lst.Add(tmp[j]);
                }
            }
        }
        int index = 0;


        for (; index < lst.Count; )
        {
            ResTable table = new ResTable();
            table.ReadText(lst, ref index);
            this.m_lstRes.Add(table);
        }
    }

    /// <summary>
    /// 加载
    /// </summary>
    /// <param name="content"></param>
    public void Load( string content)
    {
        CXML xml = new CXML(content);

        this.m_lstRes.Clear();

        List<XmlNode> lst = xml.GetNodes("res");

        for (int i = 0; i < lst.Count; i++)
        {
            ResTable table = new ResTable();
            table.ReadXml(lst[i]);
            this.m_lstRes.Add(table);
        }
    }

}
