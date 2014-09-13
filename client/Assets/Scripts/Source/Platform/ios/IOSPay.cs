using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

//  IOSPay.cs
//  Author: Lu Zexi
//  2014-03-31




/// <summary>
/// IOS支付
/// </summary>
public class IOSPay
{
    private IntPtr m_cInstance; //实例

    public IOSPay()
    {
        //
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        this.m_cInstance = _purchasePluginsInit();
    }

    /// <summary>
    /// 支付
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public int Pay( string id )
    {
        //_restorePurchaseStart(this.m_cInstance);
        int res = _requestProductPurchase(id, this.m_cInstance);
        //_restorePurchaseStart(this.m_cInstance);
        return res;
    }

    /// <summary>
    /// 支付结果验证码
    /// </summary>
    /// <returns></returns>
    public string PayVerify()
    {
        return _getPurchaseReceipt(this.m_cInstance);
    }

    /// <summary>
    /// 更新版本
    /// </summary>
    /// <param name="path"></param>
    public void UpdateVersion(string path)
    {
        _Update(path);
    }

    [DllImport("__Internal")]
    private static extern IntPtr _purchasePluginsInit();

    [DllImport("__Internal")]
    private static extern int _requestProductPurchase(string id, IntPtr instance);

    [DllImport("__Internal")]
    private static extern void _restorePurchaseStart(IntPtr instance);

    [DllImport("__Internal")]
    private static extern string _getPurchaseReceipt(IntPtr instance);

    [DllImport("__Internal")]
    private static extern void _Update(string path);
}
