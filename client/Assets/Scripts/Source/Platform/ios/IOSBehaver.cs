using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//  IOSBehaver.cs
//  Author: Lu Zexi
//  2014-04-02



/// <summary>
/// IOS平台Behaver
/// </summary>
public class IOSBehaver : MonoBehaviour
{
    /// <summary>
    /// 支付回调
    /// </summary>
    /// <param name="verify"></param>
    public void onPaySuccessCallback( string verify)
    {
        PlatformManager.GetInstance().OnPaymentSuccessCallBack(verify);
    }

    /// <summary>
    /// 支付失败回调
    /// </summary>
    /// <param name="arg"></param>
    public void onPayFailCallback( string arg )
    {
        PlatformManager.GetInstance().OnPaymentFailCallBack(arg);
    }
}
