using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//  TABLE_READER.cs
//  Author: Lu Zexi
//  2013-11-13



/// <summary>
/// 配置表读取工具
/// </summary>
public class TABLE_READER
{
    /// <summary>
    /// 加载本地数据表
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static List<string> LOAD_LOCAL(string path)
    {
        TextAsset txt = Resources.Load(path) as TextAsset;

        return LOAD_CSV(txt.text);
    }

    /// <summary>
    /// 去除空字符
    /// </summary>
    /// <param name="lst"></param>
    /// <returns></returns>
    public static List<string> SPLIT_EMPTY(List<string> lst)
    {
        for (int i = 0; i < lst.Count;)
        {
            if ( string.IsNullOrEmpty( lst[i] ) || lst[i][0] == '#')
            {
                lst.RemoveAt(i);
                continue;
            }
            i++;
        }
        return lst;
    }

    /// <summary>
    /// 加载CSV
    /// </summary>
    /// <param name="csvText"></param>
    /// <returns></returns>
    public static List<string> LOAD_CSV(string csvText)
    {
        string[,] vecStr = SplitCsvGrid(csvText);
        List<string> lst = new List<string>();

        for (int j = 2; j < vecStr.GetLength(1)-2; j++)
        {
            for (int i = 0; i < vecStr.GetLength(0)-2; i++)
            {
                lst.Add(vecStr[i, j]);
            }
        }
        return lst;
    }

    // splits a CSV file into a 2D string array
    static public string[,] SplitCsvGrid(string csvText)
    {
        string[] lines = csvText.Split("\n"[0]);

        // finds the max width of row
        int width = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            string[] row = SplitCsvLine(lines[i]);
            width = Mathf.Max(width, row.Length);
        }

        // creates new 2D string grid to output to
        string[,] outputGrid = new string[width + 1, lines.Length + 1];
        for (int y = 0; y < lines.Length; y++)
        {
            string[] row = SplitCsvLine(lines[y]);
            for (int x = 0; x < row.Length; x++)
            {
                outputGrid[x, y] = row[x];

                // This line was to replace "" with " in my output. 
                // Include or edit it as you wish.
                outputGrid[x, y] = outputGrid[x, y].Replace("\"\"", "\"");
            }
        }

        return outputGrid;
    }

    /// <summary>
    /// 分离解析CSV行
    /// </summary>
    /// <param name="line"></param>
    /// <returns></returns>
    static public string[] SplitCsvLine(string line)
    {
        return (from System.Text.RegularExpressions.Match m in System.Text.RegularExpressions.Regex.Matches(line,
        @"(((?<x>(?=[,\r\n]+))|""(?<x>([^""]|"""")+)""|(?<x>[^,\r\n]+)),?)",
        System.Text.RegularExpressions.RegexOptions.ExplicitCapture)
                select m.Groups[1].Value).ToArray();
    }

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
}
