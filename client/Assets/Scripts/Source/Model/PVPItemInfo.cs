using UnityEngine;
using System.Collections;


/// <summary>
/// 排名显示单项
/// </summary>
public class PVPItemInfo : CModel<PVPItemInfo>
{
	public string m_strName;
	public int m_iHeroTableID;
	public int m_iHeroLv;
	public int m_iPoint;
	public int m_iWinNum;
	public int m_iLoseNum;
}