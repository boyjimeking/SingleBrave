using UnityEngine;
using System.Collections;

public class Common  {

	

	
	/**
	 * @brief PPWeb页面关闭回调参数
	 */
	public  enum PPWebViewCode{
	    /**
	     * 返回充值页面
	     */
		PPWebViewCodeRecharge = 1,
	    /**
	     * 返回充值并且兑换页面
	     */
	    PPWebViewCodeRechargeAndExchange = 2,
	    /**
	     * 返回其他页面
	     */
	    PPWebViewCodeOther = 3,


	};


	/**
	 * @brief 错误范围，用来标识错误是接口返回的还是SDK返回的。
	 */
	public enum PPPageCode{
	    /**
	     * 关闭接口为登录页面
	     */
		PPLoginViewPageCode	= 1,
	    /**
	     * 关闭接口为注册
	     */
	    PPRegisterViewPageCode = 2,
	    /**
	     * 关闭接口为其他
	     */
	    PPOtherViewPageCode = 3,
	}
	
	
	/**
	 * @brief 错误范围，用来标识错误是接口返回的还是SDK返回的。
	 */
	public  enum PPPayResultCode{
	    /**
	     * 购买成功
	     */
		PPPayResultCodeSucceed	= 0,
	    /**
	     * 禁止访问
	     */
	    PPPayResultCodeForbidden = 1,
	    /**
	     * 该用户不存在
	     */
	    PPPayResultCodeUserNotExist = 2,
	    /**
	     * 必选参数丢失
	     */
	    PPPayResultCodeParamLost = 3,
	    /**
	     * PP币余额不足
	     */
	    PPPayResultCodeNotSufficientFunds = 4,
	    /**
	     * 该游戏数据不存在
	     */
	    PPPayResultCodeGameDataNotExist = 5,
	    /**
	     * 开发者数据不存在
	     */
	    PPPayResultCodeDeveloperNotExist = 6,
	    /**
	     * 该区数据不存在
	     */
	    PPPayResultCodeZoneNotExist = 7,
	    /**
	     * 系统错误
	     */
	    PPPayResultCodeSystemError = 8,
	    /**
	     * 购买失败
	     */
	    PPPayResultCodeFail = 9,
	    /**
	     * 与开发商服务器通信失败，如果长时间未收到商品请联系PP客服：电话：020-38276673　 QQ：800055602
	     */
	    PPPayResultCodeCommunicationFail = 10,
	    /**
	     * 开发商服务器未成功处理该订单，如果长时间未收到商品请联系PP客服：电话：020-38276673　 QQ：800055602
	     */
	    PPPayResultCodeUntreatedBillNo = 11,
	    /**
	     * 用户中途取消
	     */
	    PPPayResultCodeCancel = 12,
	    /**
	     * 非法访问，可能用户已经下线
	     */
	    PPPayResultCodeUserOffLine = 88,
	}
	
	
}
