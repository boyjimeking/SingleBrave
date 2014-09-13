using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

#if IOSPP

public class Bonjour {
	
	/**
		 * 初始化SDK，
		 * @param paramAppId,设置游戏的AppId
		 * @param paramAppKey，设置游戏的AppKey
		 * @param paramRechargeAmount，设置充值页面的默认金额，
		 * @param paramIsNSlogData，设置是否控制台打印日志（仅iOS端）
		 * @param paramIsLongComet，设置游戏客户端与游戏服务端链接方式是否为长连接【如果游戏服务端能主动与游戏客户端交互。例如发放道具则为长连接。此处设置影响充值并兑换的方式】
		 * @param paramIsLogOutPushLoginView,设置从PP中心注销后是否自动弹出登录页面
		 * @param paramIsOpenRecharge，设置是否开启充值PP币功能
		 * @param paramIsDeviceOrientationLandscapeLeft,设置游戏是否支持设备当前Home键盘在左方向
		 * @param paramIsDeviceOrientationLandscapeRight,设置游戏是否支持设备当前Home键盘在右方向
		 * @param paramIsDeviceOrientationPortrait,设置游戏是否支持设备当前Home键盘在上方向
		 * @param paramIsDeviceOrientationPortraitUpsideDown,设置游戏是否支持设备当前Home键盘在下方向
		 * @param paramSendMsgNotiClass GameObject的名称,指定UnitySendMessage将通知发送到哪个对象
		 */
	[DllImport("__Internal")]
     private static extern void U3D_initSDK (int paramAppId,string paramAppKey,bool paramIsNSlogData,int paramRechargeAmount,
                                bool paramIsLongComet,bool paramIsLogOutPushLoginView,bool paramIsOpenRecharge,
								string paramCloseRechargeAlertMessage,
                                bool paramIsDeviceOrientationLandscapeLeft,
								bool paramIsDeviceOrientationLandscapeRight,bool paramIsDeviceOrientationPortrait,
								bool paramIsDeviceOrientationPortraitUpsideDown,
								string paramSendMsgNotiClass);
     public static void initSDK (int paramAppId,string paramAppKey,bool paramIsNSlogData,int paramRechargeAmount,
                                bool paramIsLongComet,bool paramIsLogOutPushLoginView,bool paramIsOpenRecharge,
								string paramCloseRechargeAlertMessage,
                                bool paramIsDeviceOrientationLandscapeLeft,
								bool paramIsDeviceOrientationLandscapeRight,
								bool paramIsDeviceOrientationPortrait,
								bool paramIsDeviceOrientationPortraitUpsideDown,
								string paramSendMsgNotiClass)
     {
		if (Application.platform != RuntimePlatform.OSXEditor)
        {
           U3D_initSDK(paramAppId,paramAppKey,paramIsNSlogData,paramRechargeAmount,
                                paramIsLongComet,paramIsLogOutPushLoginView,paramIsOpenRecharge,
				paramCloseRechargeAlertMessage,
                                paramIsDeviceOrientationLandscapeLeft,
								paramIsDeviceOrientationLandscapeRight,
								paramIsDeviceOrientationPortrait,
								paramIsDeviceOrientationPortraitUpsideDown,
								paramSendMsgNotiClass);

		}
     }
	
	
	/**
		 * 弹出登录页面窗口 
		 * 
		 */
	[DllImport("__Internal")]
     private static extern void U3D_showLoginView ();
     public static void showLoginView ()
     {
		if (Application.platform != RuntimePlatform.OSXEditor)
        {
            U3D_showLoginView();
		}
     }
	
	
	/**
		 * 弹出PP中心页面窗口 
		 * 
		 */	
	[DllImport("__Internal")]
     private static extern void U3D_showCenterView ();
     public static void showCenterView ()
     {
		if (Application.platform != RuntimePlatform.OSXEditor)
        {
            U3D_showCenterView();
		}
     }
	


		/**
		 * 兑换道具 
		 * @param price，商品的价格
		 * @param BillNo，商品的订单号。从游戏服务端自行获取，char（20），保证唯一
		 * @param BillTitle，商品的名称
		 * @param RoleId，角色ID，游戏
		 * @param ZoneId，分区ID，此ID要和开发者中心后台的分区ID对应，否则充值出现提示分区参数错误。
		 * 
		 */	
	[DllImport("__Internal")]
     private static extern void U3D_exchangeGoods(double paramPrice,string paramBillNo,string paramBillTitle,
                                          string paramRoleId,int paramZoneId);
     public static void exchangeGoods(double paramPrice,string paramBillNo,string paramBillTitle,
                                          string paramRoleId,int paramZoneId)
     {
		if (Application.platform != RuntimePlatform.OSXEditor)
        {
			U3D_exchangeGoods(paramPrice,paramBillNo,paramBillTitle,paramRoleId,paramZoneId);
		}
     }
	

	/**
		 * 获取当前用户名
		 * @return 回调给Main
		 * 当前用户名 --唯一性   
		 */	
	[DllImport("__Internal")]
     private static extern void U3D_currentUserName();
     public static void currentUserName()
     {
		if (Application.platform != RuntimePlatform.OSXEditor)
        {
			U3D_currentUserName();
		}
     }
	
	
	/**
		 * 获取当前用户Id 
		 * @return 回调给Main
		 * 当前用户Id --唯一性
		 */	
	[DllImport("__Internal")]
     private static extern int  U3D_currentUserId();
     public static int currentUserId()
     {
		if (Application.platform != RuntimePlatform.OSXEditor)
        {
            return U3D_currentUserId();
		}
		return 0;
     }
	

	/**
		 * 注销用户，清除自动登录状态 
		 * @return 无返回
		 * 
		 */	
	[DllImport("__Internal")]
     private static extern void  U3D_PPlogout();
     public static void PPlogout()
     {
		if (Application.platform != RuntimePlatform.OSXEditor)
        {
            U3D_PPlogout();
		}
     }
	

}
#endif