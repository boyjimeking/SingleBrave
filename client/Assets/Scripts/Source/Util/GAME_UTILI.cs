using System;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;


//  GAME_UTILI.cs
//  Author: Lu Zexi
//  2013-11-21




/// <summary>
/// 游戏工具类
/// </summary>
public class GAME_FUNCTION
{

    //从UNIX时间戳转为日期字符串
    public static string UNIXTimeToDateString(long time)
    {
        DateTime date = UNIXTimeToCDateTime(time);
        return date.ToString("yyyy/MM/dd hh:mm:ss");
    }

    //从UNIX时间戳转为日期字符串
    public static string UNIXTimeToDateTimeString(long time)
    {
        DateTime date = UNIXTimeToCDateTime(time);
        return date.ToString("yyyy-MM-dd");
    }

    /// <summary>
    /// 时间转换
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static DateTime UNIXTimeToCDateTime(long time)
    {
        long timeL = time * 10000000 + (new DateTime(1970, 1, 1, 8, 0, 0).Ticks);

        DateTime date = new DateTime(timeL);
        return date;
    }

    /// <summary>
    /// 字符串编码转换
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string DEFAULTToUNICODE(string str)
    {
        try
        {
            byte[] temp = Encoding.Default.GetBytes(str);
            string result = Encoding.Unicode.GetString(temp);
            return result;
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.StackTrace);
            return null;
        }
        return null;
    }

    /// <summary>
    /// 由概率集得到1次随机落在何处
    /// </summary>
    /// <param name="perLst"></param>
    /// <returns></returns>
    public static int BET(params float[] perLst)
    {
        return RANDOM_BET(1, perLst)[0];
    }

    /// <summary>
    /// 由指定概率集得到N次随机落在何处
    /// </summary>
    /// <param name="num"></param>
    /// <param name="perLst"></param>
    /// <returns></returns>
    public static int[] BET(int num, params float[] perLst)
    {
        return RANDOM_BET(num, perLst);
    }

    /// <summary>
    /// 由指定概率集得到N次随机落在何处
    /// </summary>
    /// <param name="num"></param>
    /// <param name="perLst"></param>
    /// <returns></returns>
    public static int[] RANDOM_BET(int num, float[] perLst)
    {
        if (perLst == null || num <= 0)
            return null;

        int typeNum = perLst.Length;
        int[] selectPos = new int[num];
        float[] vecRandom = new float[num];

        for (int i = 0; i < num; i++)
        {
            selectPos[i] = -1;
            vecRandom[i] = RANDOM_ONE();
        }

        for (int i = 0; i < num; i++)
        {
            float sumPos = 0;
            for (int j = RANDOM(0,typeNum) , k = 0; k < perLst.Length; k++, j++)
            {
                sumPos += perLst[j % typeNum];
                //Debug.Log("sum + " + sumPos + " -- " + perLst[j % typeNum] + " -- " + vecRandom[i]);
                if (sumPos >= vecRandom[i])
                {
                    selectPos[i] = j % typeNum;
                    break;
                }
            }

        }

        return selectPos;
    }

    /// <summary>
    /// 随机圆内点
    /// </summary>
    /// <returns></returns>
    public static Vector2 RANDOM_IN_CIRCLE()
    {
        return UnityEngine.Random.insideUnitCircle;
    }

    /// <summary>
    /// 随机球内点
    /// </summary>
    /// <returns></returns>
    public static Vector3 RANDOM_IN_SPHERE()
    {
        return UnityEngine.Random.insideUnitSphere;
    }

    /// <summary>
    /// 随机球上点
    /// </summary>
    /// <returns></returns>
    public static Vector3 RANDOM_ON_SPHERE()
    {
        return UnityEngine.Random.onUnitSphere;
    }

    /// <summary>
    /// 随机0-1浮点数
    /// </summary>
    /// <returns></returns>
    public static float RANDOM_ONE()
    {
        return RANDOM(0f, 1f);
    }

    /// <summary>
    /// 随机范围内浮点数
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static float RANDOM(float min, float max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    /// <summary>
    /// 随机范围内整数
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int RANDOM(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    /// <summary>
    /// 获取指定字符串
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static string STRING(int id)
    {
        return StringTableManager.GetInstance().GetString(id);
    }

    /// <summary>
    /// 延迟执行
    /// </summary>
    /// <param name="time"></param>
    /// <param name="func1"></param>
    public static void EXCUTE_DELAY( float time , MainApplication.FUNC1 func1 )
    {
        MainApplication.GetInstance().TIME_EVENT(time, func1);
    }

    /// <summary>
    /// 延迟执行
    /// </summary>
    /// <param name="time"></param>
    /// <param name="func2"></param>
    /// <param name="arg"></param>
    public static void EXCUTE_DELAY(float time, MainApplication.FUNC2 func2, params object[] arg)
    {
        MainApplication.GetInstance().TIME_EVENT(time, func2 , arg);
    }
}
