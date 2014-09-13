using System;
using System.Collections.Generic;
using System.Collections;


//  TableBase.cs
//  Author: Lu Zexi
//  2013-11-12


/// <summary>
/// 基础数据表类
/// </summary>
public class TableBase
{
    /// <summary>
    /// 读取文本数据
    /// </summary>
    /// <param name="lstInfo"></param>
    /// <param name="index"></param>
    public virtual void ReadText(List<string> lstInfo, ref int index) { }

    /// <summary>
    /// 写入文本数据
    /// </summary>
    /// <param name="path"></param>
    public virtual void WriteText(List<string> lstInfo) { }

    /// <summary>
    /// 转换整型
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int INT(string str)
    {
        if (str == "" || str == null)
        {
            return 0;
        }
        return int.Parse(str);
    }

    /// <summary>
    /// 转换长整型
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static long LONG(string str)
    {
        if (str == "" || str == null)
        {
            return 0;
        }
        return long.Parse(str);
    }

    /// <summary>
    /// 转换字符串
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string STRING(string str)
    {
        if (str == "" || str == null)
        {
            return "";
        }
        return str;
    }

    /// <summary>
    /// 转换浮点数
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static float FLOAT(string str)
    {
        if (str == "" || str == null)
        {
            return 0;
        }
        return float.Parse(str);
    }

    /// <summary>
    /// 转换Bool
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool BOOL(string str)
    {
        if (str == "" || str == null)
        {
            return false;
        }
        return bool.Parse(str);
    }

}

