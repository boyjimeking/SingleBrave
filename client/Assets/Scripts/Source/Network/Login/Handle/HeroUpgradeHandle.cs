//  UpgradeHandle.cs
//  Author: Cheng Xia
//  2013-12-25

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

/// <summary>
/// 英雄升级句柄
/// </summary>
public class HeroUpgradeHandle
{
    /// <summary>
    /// 获取action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.HERO_UPGRADE_REQ;
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketRequest packet)
    {
        HeroUpgradePktAck ack = (HeroUpgradePktAck)packet;

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.m_strErrorDes);

            GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_MENU).Show();

            return false;
        }

        //更新金钱
        Role.role.GetBaseProperty().m_iGold = ack.m_iGold;  //更新出售回来的金钱，服务器返回全量
        GUIBackFrameTop guitop = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP) as GUIBackFrameTop;
        guitop.UpdateGold();
        //更新强化英雄数据
        Hero afterHero = Role.role.GetHeroProperty().GetHero(ack.m_cAfterHero.m_iID);
        afterHero.m_iLevel = ack.m_cAfterHero.m_iLevel;
        afterHero.m_iCurrenExp = ack.m_cAfterHero.m_iCurrenExp;
        afterHero.m_lGetTime = ack.m_cAfterHero.m_lGetTime;
        afterHero.m_iHp = ack.m_cAfterHero.m_iHp;
        afterHero.m_iAttack = ack.m_cAfterHero.m_iAttack;
        afterHero.m_iDefence = ack.m_cAfterHero.m_iDefense;
        afterHero.m_iRevert = ack.m_cAfterHero.m_iRevert;
        afterHero.m_iBBSkillLevel = ack.m_cAfterHero.m_iBBSkillLevel;
        afterHero.m_eGrowType = (GrowType)ack.m_cAfterHero.m_eGrowType;
        afterHero.m_iEquipID = ack.m_cAfterHero.m_iEquipId;
        afterHero.m_bLock = (ack.m_cAfterHero.m_iLock == 1);
        Role.role.GetHeroProperty().UpdateHero(afterHero);  //更新本地英雄内存
        //更新牺牲的英雄素材
        List<Hero> deleteHeros = new List<Hero>();
        foreach (int heroID in ack.m_lstDeleteHeros)
        {
            Hero delhero = Role.role.GetHeroProperty().GetHero(heroID);
            deleteHeros.Add(delhero);
            Role.role.GetHeroProperty().DelHero(heroID); //更新本地英雄内存
        }
        //展示强化动画界面
        GUIHeroUpgradeResult gui_upgradeHeroResult = (GUIHeroUpgradeResult)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_UPGRADEHERORESULT);
        gui_upgradeHeroResult.SetShowData(afterHero, deleteHeros, ack.m_iSuccessType, ack.m_upgradeProcess);
        gui_upgradeHeroResult.Show();

        return true;
    }
}