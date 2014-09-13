//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//
//using UnityEngine;
//using Game.Network;
//
////  GameDispatch.cs
////  Author: Lu Zexi
////  2013-11-12
//
//
//
///// <summary>
///// 游戏调度类
///// </summary>
//public class GameDispatch : HTTPDispatch
//{
//    public GameDispatch()
//    {
//        //
//    }
//
//    /// <summary>
//    /// 数据错误
//    /// </summary>
//    public override void OnDataError()
//    {
//        Debug.LogError("OnDataError");
//        return;
//    }
//
//    /// <summary>
//    /// 连接断开
//    /// </summary>
//    public override void OnDisconnect()
//    {
//        Debug.LogError("OnDisconnect");
//        return;
//    }
//
//    /// <summary>
//    /// 超时事件
//    /// </summary>
//    public override void OnTimeOut()
//    {
//        Debug.LogError("OnTimeOut");
//    }
//}
//
