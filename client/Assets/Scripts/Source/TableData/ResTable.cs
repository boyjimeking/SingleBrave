using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;


//  ResTable.cs
//  Author: Lu Zexi
//  2014-01-26


/// <summary>
/// 资源表
/// </summary>
public class ResTable : TableBase
{
    private string m_strResName;    //资源名
    public string ResName
    {
        get { return this.m_strResName; }
    }
    private int m_iVersion; //版本
    public int Version
    {
        get { return this.m_iVersion; }
    }
    private string m_strPath;    //地址
    public string Path
    {
        get { return this.m_strPath; }
    }

    private string m_strMd5;    //MD5
    public string MD5
    {
        get { return this.m_strMd5; }
    }

    private int m_iLength;  //大小
    public int Length
    {
        get { return this.m_iLength; }
    }
    private uint m_iCRC; //CRC
    public uint CRC
    {
        get { return this.m_iCRC; }
    }

    public ResTable()
    { 
        //
    }


    /// <summary>
    /// 读取数据
    /// </summary>
    /// <param name="lstInfo"></param>
    /// <param name="index"></param>
    public override void ReadText(List<string> lstInfo, ref int index)
    {
        UnityEngine.Debug.Log(lstInfo[index]);
        this.m_strResName = STRING(lstInfo[index++]);
        this.m_strPath = STRING(lstInfo[index++]);
        this.m_strMd5 = STRING(lstInfo[index++]);
        this.m_iLength = INT(lstInfo[index++]);
    }


    /// <summary>
    /// 读取XML
    /// </summary>
    /// <param name="lst"></param>
    public void ReadXml(XmlNode node )
    {
        this.m_strResName = node["name"].InnerText;
        this.m_iVersion = int.Parse(node["version"].InnerText);
        this.m_strPath = node["path"].InnerText;
        this.m_strMd5 = "";
        this.m_iCRC = Convert.ToUInt32(node["crc"].InnerText,16);
    }

}
