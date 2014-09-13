using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  GUIBattleArena.cs
//  Author: Lu Zexi
//  2014-02-10



/// <summary>
/// 竞技场GUI
/// </summary>
public class GUIBattleArena : GUIBattlePVP
{
    public GUIBattleArena( GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_ARENA_BATTLE, GUILAYER.GUI_PANEL)
    { 
    }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        base.Show();
        this.m_delFinishCallBack = FinishCall;
        this.m_delEndCallBack = EndCall;
    }

    /// <summary>
    /// 胜利结束回调
    /// </summary>
    private void FinishCall()
    {
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Show();
        Hiden();
        
        //统计
        List<int> heros = new List<int>();
        BattleHero[] target = GetVecEnemy();
        int skillKillSum = 0;
        int superDamageSum = 0;
        for (int i = 0; i < target.Length ;i++ )
        {
            if (target[i] != null && target[i].m_bDead)
            {
                heros.Add(target[i].m_iTableID);
                skillKillSum += target[i].m_iTotalSkillKillNum;
                superDamageSum += target[i].m_iTotalSuperDamage;
            }
        }

        GUIArenaFightReady fightReady = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_ARENAFIGHTREADY) as GUIArenaFightReady;

        SendAgent.SendPVPBattleEnd(Role.role.GetBaseProperty().m_iPlayerId, fightReady.m_iSelectId, Role.role.GetBaseProperty().m_iBattleID, this.m_iBattleResult,
            heros, skillKillSum, superDamageSum, this.m_iRoundMaxHurt, this.m_iRoundMaxSpark, 
            this.m_iTotalHurt , this.m_iTotalRecover , this.m_iTotalSpark , this.m_iTotalSkill , this.m_iWinType
            );
    }

    /// <summary>
    /// 失败结束回调
    /// </summary>
    private void EndCall()
    {
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Show();
        Hiden();
        //统计
        List<int> heros = new List<int>();
        BattleHero[] target = GetVecEnemy();
        int skillKillSum = 0;
        int superDamageSum = 0;
        for (int i = 0; i < target.Length; i++)
        {
            if (target[i] != null && target[i].m_bDead)
            {
                heros.Add(target[i].m_iTableID);
                skillKillSum += target[i].m_iTotalSkillKillNum;
                superDamageSum += target[i].m_iTotalSuperDamage;
            }
        }

        GUIArenaFightReady fightReady = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_ARENAFIGHTREADY) as GUIArenaFightReady;

        SendAgent.SendPVPBattleEnd(Role.role.GetBaseProperty().m_iPlayerId, fightReady.m_iSelectId, Role.role.GetBaseProperty().m_iBattleID, this.m_iBattleResult,
            heros, skillKillSum, superDamageSum, this.m_iRoundMaxHurt, this.m_iRoundMaxSpark,
            this.m_iTotalHurt, this.m_iTotalRecover, this.m_iTotalSpark, this.m_iTotalSkill, this.m_iWinType
            );
    }
}
