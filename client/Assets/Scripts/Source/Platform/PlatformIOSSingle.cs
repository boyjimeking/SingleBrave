using UnityEngine;
using System.Collections;


//	PlatformSingle.cs
//	Author: Lu Zexi
//	2014-09-20



/// <summary>
/// Platform single.
/// </summary>
public class PlatformIOSSingle : PlatformBase
{	
	/// <summary>
	/// 获取渠道号
	/// </summary>
	/// <returns></returns>
	public override string GetChannelName()
	{
		return GAME_SETTING.CHANNEL_NAME;
	}

	/// <summary>
	/// 登录
	/// </summary>
	public override void Login ()
	{
		SendAgent.SendPlayerInfoGetPktReq(GAME_SETTING.s_iUID);
	}
}

