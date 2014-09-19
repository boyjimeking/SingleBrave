using UnityEngine;
using System.Collections;
using Game.Network;

//  SignatureUpdatePktReq.cs
//  Author: sanvey
//  2013-12-23

/// <summary>
/// 更新签名请求
/// </summary>
public class PlayerSignatureUpdatePktReq : HTTPPacketRequest
{
    public int m_iPid;  //Pid
    public string m_strSign;

    public PlayerSignatureUpdatePktReq()
    {
        this.m_strAction = PACKET_DEFINE.SIGN_UPDATE_REQ;
    }
}



/// <summary>
/// 发送代理
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 更新签名请求
	/// </summary>
	/// <param name="pid"></param>
	/// <param name="signature"></param>
	public static void SendSignatureUpdateReq(int pid, string signature)
	{
		PlayerSignatureUpdatePktReq req = new PlayerSignatureUpdatePktReq();
		req.m_strSign = signature;
		req.m_iPid = pid;
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
	}

}