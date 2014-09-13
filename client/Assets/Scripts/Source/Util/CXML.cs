using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;


//  CXML.cs
//  Author: Lu Zexi
//  2014-02-05



/// <summary>
/// XML类
/// </summary>
public class CXML
{
    private XmlDocument m_cXmlDoc;  //XML DOC

    public CXML()
    {
        this.m_cXmlDoc = new XmlDocument();
        XmlNode node = this.m_cXmlDoc.CreateElement("root");
        this.m_cXmlDoc.AppendChild(node);
    }

    public CXML(string content)
    {
        this.m_cXmlDoc = new XmlDocument();
        if (content == "")
        {
            content = "<root></root>";
        }
        this.m_cXmlDoc.LoadXml(content);
        if (this.m_cXmlDoc.DocumentElement == null)
        {
            XmlNode node = this.m_cXmlDoc.CreateElement("root");
            this.m_cXmlDoc.AppendChild(node);
        }
    }

    /// <summary>
    /// 加载
    /// </summary>
    /// <param name="str">内容</param>
    public void Load(string str)
    {
        if (str == "")
        {
            str = "<root></root>";
        }
        this.m_cXmlDoc.LoadXml(str);
        if (this.m_cXmlDoc.DocumentElement == null)
        {
            XmlNode node = this.m_cXmlDoc.CreateElement("root");
            this.m_cXmlDoc.AppendChild(node);
        }
    }

    /// <summary>
    /// 加载
    /// </summary>
    /// <param name="stream"></param>
    public void Load(Stream stream)
    {
        this.m_cXmlDoc.Load(stream);
        if (this.m_cXmlDoc.DocumentElement == null)
        {
            XmlNode node = this.m_cXmlDoc.CreateElement("root");
            this.m_cXmlDoc.AppendChild(node);
        }
    }

    /// <summary>
    /// 是否包含某KEY值
    /// </summary>
    /// <param name="key">关键字</param>
    /// <returns>是否存在</returns>
    public bool ContainNode(string key)
    {
        XmlNode node = this.m_cXmlDoc.DocumentElement;
        if (node == null)
            return false;
        return node[key] != null;
    }

    /// <summary>
    /// 是否包含KEY值
    /// </summary>
    /// <param name="node">节点</param>
    /// <param name="key">关键字</param>
    /// <returns>是否存在</returns>
    public bool ContainNode(XmlNode node, string key , string value)
    {
        return node[key].InnerText == value;
    }

    /// <summary>
    /// 增加根节点
    /// </summary>
    /// <param name="key">关键字</param>
    /// <returns>增加的节点</returns>
    public XmlNode AddNode(string key)
    {
        XmlNode node = this.m_cXmlDoc.DocumentElement;
        if (node == null)
            return null;
        XmlNode tmpNode = this.m_cXmlDoc.CreateElement(key);
        return node.AppendChild(tmpNode);
    }

    /// <summary>
    /// 增加节点
    /// </summary>
    /// <param name="node">节点</param>
    /// <param name="key">关键字</param>
    /// <returns>增加的节点</returns>
    public XmlNode AddNode(XmlNode node, string key)
    {
        if (node == null)
            return null;
        XmlNode tmp = this.m_cXmlDoc.CreateElement(key);
        return node.AppendChild(tmp);
    }

    /// <summary>
    /// 增加节点
    /// </summary>
    /// <param name="node">节点</param>
    /// <param name="key">关键字</param>
    /// <returns>增加的节点</returns>
    public XmlNode AddNode(XmlNode node, string key, string val)
    {
        if (node == null)
            return null;
        XmlNode tmp = this.m_cXmlDoc.CreateElement(key);
        tmp.InnerText = val;
        return node.AppendChild(tmp);
    }


    /// <summary>
    /// 获取节点
    /// </summary>
    /// <param name="key">关键字</param>
    /// <returns>节点</returns>
    public XmlNode GetNode(string key)
    {
        XmlNode node = this.m_cXmlDoc.DocumentElement;
        if (node == null)
            return null;
        XmlNode ch = node[key];
        return ch;
    }

    /// <summary>
    /// 获取节点列表
    /// </summary>
    /// <param name="key">关键字</param>
    /// <returns>节点列表</returns>
    public List<XmlNode> GetNodes(string key)
    {
        XmlNode node = this.m_cXmlDoc.DocumentElement;
        if (node == null)
            return null;
        List<XmlNode> lst = new List<XmlNode>();
        foreach (XmlNode item in node.ChildNodes)
        {
            if (item.Name == key)
            {
                lst.Add(item);
            }
        }
        return lst;
    }

    /// <summary>
    /// 获取节点列表
    /// </summary>
    /// <param name="node">节点</param>
    /// <param name="key">关键字</param>
    /// <returns>节点列表</returns>
    public List<XmlNode> GetNodes(XmlNode node, string key)
    {
        if (node == null)
            return null;
        List<XmlNode> lst = new List<XmlNode>();
        foreach (XmlNode item in node.ChildNodes)
        {
            if (item.Name == key)
            {
                lst.Add(item);
            }
        }
        return lst;
    }

    /// <summary>
    /// 获取节点
    /// </summary>
    /// <param name="node">节点</param>
    /// <param name="key">关键字</param>
    /// <returns>节点</returns>
    public XmlNode GetNode(XmlNode node, string key)
    {
        if (node == null)
            return null;
        return node[key];
    }

    /// <summary>
    /// 设置值
    /// </summary>
    /// <param name="key">关键字</param>
    /// <param name="val">值</param>
    public void SetNode(string key, string val)
    {
        XmlNode node = this.m_cXmlDoc.DocumentElement;
        if (node == null)
            return;
        XmlNode ch = node[key];
        ch.InnerText = val;
    }

    /// <summary>
    /// 设置节点值
    /// </summary>
    /// <param name="node">节点</param>
    /// <param name="key">关键字</param>
    /// <param name="val">值</param>
    public void SetNode(XmlNode node, string key, string val)
    {
        if (node == null)
            return;
        XmlNode tmp = node[key];
        if (tmp == null)
            return;
        tmp.InnerText = val;
    }

    /// <summary>
    /// 保存XmlWriter
    /// </summary>
    /// <param name="writer">写入者</param>
    public void Save(XmlWriter writer)
    {
        this.m_cXmlDoc.Save(writer);
    }

    /// <summary>
    /// 保存Stream
    /// </summary>
    /// <param name="stream">数据流</param>
    public void Save(Stream stream)
    {
        this.m_cXmlDoc.Save(stream);
    }

    /// <summary>
    /// 保存TxWrite
    /// </summary>
    /// <param name="txWrite">文本写入者</param>
    public void Save(TextWriter txWrite)
    {
        this.m_cXmlDoc.Save(txWrite);
    }

    /// <summary>
    /// 转为字符串
    /// </summary>
    /// <returns>字符串</returns>
    public string ToString()
    {
        MemoryStream mStream = new MemoryStream();

        this.m_cXmlDoc.Save(mStream);

        String str = Encoding.Default.GetString(mStream.GetBuffer());

        return str;
    }
}

