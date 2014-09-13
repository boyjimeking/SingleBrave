using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  IOSPPBehaver.cs
//  Author: Lu Zexi
//  2014-04-02


/// <summary>
/// IOSPP主类
/// </summary>
public class IOSPPBehaver : MonoBehaviour
{
#if IOSPP
    string text = "111111";

    // Use this for initialization
    //void Start()
    //{
    //    Bonjour.initSDK(76, "04569029582680d7602989feb0a0a7e2", false, 10, true, true, true, "close recharge message", true, true, true, true, "Main Camera");
    //}
	
    //// Update is called once per frame
    //void Update () {
	
    //}



    //void OnGUI()
    //{

    //    //showLoginViewButton
    //    if (GUI.Button(new Rect(0, 44, 120, 120), "Login"))
    //    {
    //        Bonjour.showLoginView();
    //    }


    //    //showCenterViewButton
    //    if (GUI.Button(new Rect(200, 44, 120, 120), "PP Center"))
    //    {
    //        Bonjour.showCenterView();
    //    }


    //    if (GUI.Button(new Rect(0, 200, 120, 120), "1 RMB"))
    //    {
    //        Bonjour.exchangeGoods(1, "123", "1 rmb goods", "0", 0);
    //    }

    //    if (GUI.Button(new Rect(200, 200, 120, 120), "3 RMB"))
    //    {
    //        Bonjour.exchangeGoods(3, "123", "3 rmb goods", "0", 0);

    //    }

    //    GUILayout.Label(text, GUILayout.Width(210));

   // }
	
    /// <summary>
    /// 支付结果
    /// </summary>
    /// <param name="paramNoti"></param>
	void U3D_payResultCallBack(string paramNoti)
    {
        Debug.Log("U3D_payResultCallBack-" + paramNoti);
		if(int.Parse(paramNoti) == (int)Common.PPPayResultCode.PPPayResultCodeSucceed)
        {
            PlatformManager.GetInstance().OnPaymentSuccessCallBack(paramNoti);
            return;
		}
        PlatformManager.GetInstance().OnPaymentFailCallBack(paramNoti);
        //text = "U3D_payResultCallBack" + paramNoti;
	}
	
    /// <summary>
    /// 关闭SDK页面
    /// </summary>
    /// <param name="paramNoti"></param>
	void U3D_closePageViewCallBack(string paramNoti)
    {
		System.Console.WriteLine("U3D_closePageViewCallBack-" + paramNoti);
        //text = "U3D_closePageViewCallBack" + paramNoti;
        GUI_FUNCTION.LOADING_HIDEN();
	}
	
    /// <summary>
    /// 关闭WEB页面
    /// </summary>
    /// <param name="paramNoti"></param>
	void U3D_closeWebViewCallBack(string paramNoti)
    {
		System.Console.WriteLine("U3D_closeWebViewCallBack-" + paramNoti);
        //text = "U3D_closeWebViewCallBack" + paramNoti;
         GUI_FUNCTION.LOADING_HIDEN();
	}
	
    /// <summary>
    /// 登录回调
    /// </summary>
    /// <param name="paramNoti"></param>
	void U3D_loginCallBack(string paramNoti)
    {
		//System.Console.WriteLine("username:"+Bonjour.currentUserName());
		System.Console.WriteLine("U3D_loginCallBack------token" + paramNoti +"-userid:"+Bonjour.currentUserId());
        SendAgent.SendAccountIOSPPLogin(Bonjour.currentUserId(), paramNoti);
	}
	
	
    /// <summary>
    /// 登出回调
    /// </summary>
	void U3D_logOffCallBack()
    {
        System.Console.WriteLine("U3D_logOffCallBack-");
        GAME_SETTING.ClearAccount();
        GameManager.GetInstance().GetSceneManager().ChangeTittle();
	}

	/// <summary>
	/// 获取用户名
	/// </summary>
	/// <param name="paramCurrentUserName"></param>
	void U3D_currentUserName(string paramCurrentUserName)
    {
        System.Console.WriteLine("paramCurrentUserName-" + paramCurrentUserName);
	}
	
    /// <summary>
    /// 更新回调
    /// </summary>
	void U3D_ppVerifyingUpdatePassCallBack()
    {
        //Bonjour.showLoginView();
	}
#endif
}
